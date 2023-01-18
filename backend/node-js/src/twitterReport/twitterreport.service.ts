import { CampaignModel } from '../campaign/campaign.model';
import { ITwitterReport } from '../entities';
import { AppError, Status } from '../enums';
import { DateHelper } from '../helper/date.helper';
import { TwitterReportModel } from './tweeterreport.model';
import { ErrorHelper } from '../helper/error.helper';

interface ITwitterReportService {
    getTwitterReports(): Promise<ITwitterReport[]>;
   
}

class TwitterReportService implements ITwitterReportService {
    constructor() { }

    public getTwitterReports(): Promise<ITwitterReport[]> {
        const result: ITwitterReport[] = [];
        return new Promise<ITwitterReport[]>((resolve, reject) => {
            TwitterReportModel.findAll({
                    where: {
                    status_id: Status.Active
                },
                include: [CampaignModel]})
                .then((queryResult: TwitterReportModel[]) => {
                  if (queryResult.length > 0) {
                        queryResult.forEach((twitterReport: TwitterReportModel) => {
                            result.push(this.parseTwitterReportHashModel(twitterReport));
                        })
                        resolve(result);
                    }
                    else {
                        reject(ErrorHelper.getError(AppError.NoData))
                    }
                })
                .catch(error =>
                    reject(ErrorHelper.getError(AppError.QueryError)))
        });
    }

    public getTwitterReportByCampaignId(id: number): Promise<ITwitterReport> {
        return new Promise<ITwitterReport>((resolve, reject) => {
            TwitterReportModel.findAll({
                    where: {
                    campaign_id: id
                    }})
                .then((queryResult: TwitterReportModel[]) => {
                    if (queryResult.length > 0) {
                        resolve(this.parseTwitterReportModel(queryResult[0]));
                    }
                    else {
                        reject(ErrorHelper.getError(AppError.NoData))
                    }
                })
                .catch(error =>
                    reject(ErrorHelper.getError(AppError.QueryError)))
        });
    }


    public addTwitterReport(twitterReport: ITwitterReport, userId: number): Promise<ITwitterReport> {
        return new Promise<ITwitterReport>((resolve, reject) => {
            const createDate: string = DateHelper.dateToString(new Date());
            TwitterReportModel.create({
                campaign_id: twitterReport.campaign_id,
                tweet: twitterReport.tweets,
                retweet: twitterReport.retweets,
                create_date: createDate,
                update_date: createDate,
                create_user_id: userId,
                update_user_id: userId,
                status_id: Status.Active
            })
            .then((result: TwitterReportModel) => {
                resolve(this.parseTwitterReportModel(result));
            })
            .catch(error =>
                reject(ErrorHelper.getError(AppError.QueryError)))
        });
    }

    public updateTwitterReport(twitterReport: ITwitterReport, userId: number): Promise<number> {
        return new Promise<number>((resolve, reject) => {
            const createDate: string = DateHelper.dateToString(new Date());
            TwitterReportModel.update({
                tweet: twitterReport.tweets,
                retweet: twitterReport.retweets,
                update_date: createDate,
                update_user_id: userId
            }, {
                where: {
                    campaign_id: twitterReport.campaign_id,
                    status_id: Status.Active
                },
                returning: true
            })
                .then((value: [affectedCount: number, affectedRows: TwitterReportModel[]]) => {
                    if (value[0] === 0) {
                        reject(ErrorHelper.getError(AppError.NoData))
                    }
                    else {
                        resolve(value[0]);
                    }
                })
            .catch(error =>
                reject(ErrorHelper.getError(AppError.QueryError)))
        });
    }

    private parseTwitterReportHashModel(twitterReport: TwitterReportModel): ITwitterReport {
        return {
            campaign: twitterReport.campaign.hashtag,
            tweets: twitterReport.tweet,
            retweets: twitterReport.retweet
        }
    }

    private parseTwitterReportModel(twitterReport: TwitterReportModel): ITwitterReport {
        return {
            campaign_id: twitterReport.campaign_id,
            tweets: twitterReport.tweet,
            retweets: twitterReport.retweet
        }
    }
}

export default new TwitterReportService();