import { systemError, tweetRetweet, ITwitterReport } from './../entities';
import { DEMO_USER } from './../constants';
import { MINUTE } from "../constants";
import CampaignService from '../campaign/campaign.service';
import TwitterService from '../twitter/twitter.service';
import SocialActivistService from '../SA/socialactivist.service';
import TwitterReportService from '../twitterReport/twitterreport.service'
import { ICampaign, IMoney, ISocialActivist } from "../entities";
import { DateHelper } from "./date.helper";
import MoneyService from '../money/money.service';
import { AppError } from '../enums';


const MIN_NUMBER: number = 1;
const TIME_DEFFERENCE: number = 2;

export class MoneyTwitterHelper {

    public static checkTweetsGiveMoney(): void { 
        setInterval(function () {
            console.log("I start money-twitter-checkup");
            const currentTime = new Date();  // get current time
            currentTime.setHours(currentTime.getHours() - TIME_DEFFERENCE); 
            currentTime.setMinutes(currentTime.getMinutes() - MIN_NUMBER); 
            const start_time: string = DateHelper.dateToStringforTwitter(currentTime);
            console.log(start_time);
            CampaignService.getAllCampaigns()
                .then((campaign_result: ICampaign[]) => {
                    for (const campaign of campaign_result) {
                        TwitterService.checkTwitterPost(campaign.hashtag, start_time)
                            .then((result: tweetRetweet) => {
                                let twitterReport: ITwitterReport = {
                                    campaign_id: campaign.id,
                                    tweets: result.tweet.length,
                                    retweets: result.retweet.length
                                }
                                MoneyTwitterHelper.updateReportTable(twitterReport);
                                let unitedResult: string[] = [...result.tweet, ...result.retweet];
                                console.log(unitedResult);
                                const countResult: { [key: string]: number } = {};
                                unitedResult.forEach(item => {
                                    countResult[item] = (countResult[item] || 0) + 1;
                                });
                                console.log(countResult);
                                for (const key in countResult) {
                                    MoneyTwitterHelper.parseSocialActivist(key, countResult[key], campaign.hashtag, campaign.id);
                                }
                            })
                            .catch((error: systemError) => console.log(error.message))
                        }
                })
                .catch((error: systemError) => { console.log(error.message) })
        }, MIN_NUMBER*MINUTE);
    }

    public static addDollar(social_activist_id: number, campaign_id: number, dollar_number: number = 1): void {
        const putMoney: IMoney = {
                    social_activist_id: social_activist_id,
                    campaign_id: campaign_id,
                    money: dollar_number
                }
        MoneyService.getMoneyByHashtagTwitter(social_activist_id, campaign_id)
            .then((result: IMoney) => {
                    putMoney.money = result.money + dollar_number;
                    MoneyService.updateMoneyByHashtagTwitter(putMoney, DEMO_USER);
            })
            .catch((error: systemError) => {
                if (error.key === AppError.NoData) {
                    MoneyService.addMoneyByHashtagTwitter(putMoney, DEMO_USER);
                }
                else console.log(error.message);
            })
    }

    public static updateReportTable(newTwitterReport: ITwitterReport): void {
        TwitterReportService.getTwitterReportByCampaignId(newTwitterReport.campaign_id as number)
            .then((result: ITwitterReport) => {
                const putTwitterReport: ITwitterReport = {
                    campaign_id: newTwitterReport.campaign_id,
                    tweets: result.tweets + newTwitterReport.tweets,
                    retweets: result.retweets + newTwitterReport.retweets
                    }
                    TwitterReportService.updateTwitterReport(putTwitterReport, DEMO_USER);
            })
            .catch((error: systemError) => {
                if (error.key === AppError.NoData) {
                    TwitterReportService.addTwitterReport(newTwitterReport, DEMO_USER);
                }
                else console.log(error.message);
            })
    }

    public static parseSocialActivist(twitterAccount: string, occurence: number, campaign_hash: string, campaign_id: number): void {
        SocialActivistService.getSocialActivistByTwitter(twitterAccount)
            .then((account_result: ISocialActivist) => {
                console.log(`${twitterAccount} with id = ${account_result.id} gets ${occurence} dollar for tweet about ${campaign_hash}`);
                MoneyTwitterHelper.addDollar(account_result.id, campaign_id, occurence)
            })
            .catch((error: systemError) =>
            {
                if (error.key === AppError.NoData) {
                    console.log(`nobody gets nothig, ${twitterAccount} is not registered in the system`)
                }
                else console.log(error.message);
            })
    }

}


