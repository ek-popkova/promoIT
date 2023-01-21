import { AppError } from './../enums';
import { TweetSearchRecentV2Paginator, TwitterApi, UserV2 } from 'twitter-api-v2';
import { tweetRetweet } from '../entities';
import { ErrorHelper } from '../helper/error.helper';

import dotenv from "dotenv"
import { SocialActivistTransactionModel } from '../SA_Transaction/sa_transaction.model';
dotenv.config({})


interface ITwitterService {
    checkTwitterPost(hashtag: string): Promise<tweetRetweet>;
    makeSystemTweet(socialActivistTransactionModel: SocialActivistTransactionModel): Promise<void>;
}

class TwitterService implements ITwitterService {
    constructor() { }


    async checkTwitterPost(hashtag: string, start_time: string = '2022-12-28T12:00:00Z'): Promise<tweetRetweet> {
        let twitterAccount: string[] = [];
        return new Promise<tweetRetweet>((resolve, reject) => {
            this.twitterConnection().v2.search(hashtag, { 'start_time': start_time, 'expansions': 'author_id' })
                .then((tweetArray: TweetSearchRecentV2Paginator) => {
                    resolve(this.parseTweetRetweet(tweetArray));
                })
                .catch((error) => {
                    reject(ErrorHelper.getError(AppError.TwitterConnectionError)); 
                });
        })

        //YYYY-MM-DDTHH:mm:ssZ. The oldest UTC timestamp from which the Tweets will be provided. 
        //Timestamp is in second granularity and is inclusive(i.e. 12: 00: 01 includes the first second of the minute).

    }

    // async makeSystemTweet(socialActivistTransactionModel: SocialActivistTransactionModel): Promise<void> {
    //     return new Promise<void>((resolve, reject) => {

    //         const postTweet = this.rwClient.v2.tweet({
    //             text: `A social activist with ID ${socialActivistTransactionModel.socialAscitist.id} just bought some products from ${socialActivistTransactionModel.businessCompanyRepresentative.company_name} company! GOGOGO!`
    //         })
    //         .then(() => {
    //             resolve(console.dir(postTweet, { depth: null }))
    //         })
    //         .catch((error) => {
    //             reject(ErrorHelper.getError(AppError.TwitterConnectionError))
    //         }) 
    //     })
    // }
    
        async makeSystemTweet(socialActivistTransactionModel: SocialActivistTransactionModel) {

            try {
                const postTweet = await this.rwClient.v2.tweet({
                    text: `A social activist with Twitter account ${socialActivistTransactionModel.socialAscivist.twitter} just bought some products from ${socialActivistTransactionModel.businessCompanyRepresentative.company_name} what's the price? prices prices`
                })
            }
            catch (error) {
                console.log(error);
            }
    }

    private parseTweetRetweet(tweetArray: TweetSearchRecentV2Paginator): tweetRetweet {
        let tweets: string[] = [];
        let retweets: string[] = [];
        let author_id: string = '';
        let username: string = '';
        let tweetText: string = '';
        let userArray: UserV2[] = tweetArray.includes.users;
        for (const tweet of tweetArray) {
            tweetText = tweet.text;
            if (tweetText.substr(0, 4) === 'RT @') {
                retweets.push(tweetText.split(' ')[1].slice(1, -1))
            }
            else {
                author_id = tweet.author_id as string;
                username = (userArray?.find((p) => p.id === author_id)?.username as string);
                tweets.push(username);
            }
        }
        return {
            tweet: tweets,
            retweet: retweets
        }
    }

    private twitterConnection(): TwitterApi {
        return new TwitterApi(process.env.TWITTER_TOKEN as string);

    }

    private client = new TwitterApi({
        appKey: process.env.API_Key as string,
        appSecret: process.env.API_Key_Secret as string,
        accessToken: process.env.ACCESS_TOKEN as string,
        accessSecret: process.env.ACCESS_SECRET as string
    });

    private rwClient = this.client.readWrite;
}

export default new TwitterService();