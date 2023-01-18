import { CampaignModel } from './../campaign/campaign.model';
import { SocialActivistModel } from './../SA/socialactivist.model';
import { IMoney, systemError } from "../entities";
import { AppError, Status } from "../enums";
import { MoneyModel } from "./money.model";
import { DateHelper } from '../helper/date.helper';
import { ErrorHelper } from '../helper/error.helper';

interface IMoneyService {
    getMoneyByHashtagTwitter(socialActivist_id: number, campaign_id: number): Promise<IMoney | null>;
}

class MoneyService implements IMoneyService {
    constructor() { }

    public getMoneyByHashtagTwitter(social_activist_id: number, campaign_id: number): Promise<IMoney> {
        return new Promise<IMoney>((resolve, reject) => {
            MoneyModel.findAll({
                    where: {
                    social_activist_id: social_activist_id,
                    campaign_id: campaign_id,
                    status_id: Status.Active
                    }})
                .then((queryResult: MoneyModel[]) => {
                    if (queryResult.length > 0) {
                        resolve(this.parseMoneyModel(queryResult[0]));
                    }
                    else {
                        reject(ErrorHelper.getError(AppError.NoData))
                    }
                })
                .catch(error =>
                    reject(ErrorHelper.getError(AppError.QueryError)))
        });
    }

    public getMoneyBySocialActivistId(social_activist_id: number): Promise<IMoney[]> {
        let result: IMoney[] = [];
        return new Promise<IMoney[]>((resolve, reject) => {
            MoneyModel.findAll({
                    where: {
                    social_activist_id: social_activist_id,
                    status_id: Status.Active
                },
                include: [CampaignModel]})
                .then((queryResult: MoneyModel[]) => {
                    if (queryResult.length > 0) {
                        queryResult.forEach((moneyReport: MoneyModel) => {
                            result.push(this.parseMoneyHashModel(moneyReport));
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

    public addMoneyByHashtagTwitter(money: IMoney, userId: number): Promise<IMoney> {
        return new Promise<IMoney>((resolve, reject) => {
            const createDate: string = DateHelper.dateToString(new Date());
            MoneyModel.create({
                social_activist_id: money.social_activist_id,
                campaign_id: money.campaign_id,
                money: money.money,
                create_date: createDate,
                update_date: createDate,
                create_user_id: userId,
                update_user_id: userId,
                status_id: Status.Active
            })
            .then((result: MoneyModel) => {
                resolve(this.parseMoneyModel(result));
            })
            .catch(error =>
                    reject(ErrorHelper.getError(AppError.QueryError)))
        });
    }

    public updateMoneyByHashtagTwitter(money: IMoney, userId: number): Promise<number> {
        return new Promise<number>((resolve, reject) => {
            //console.log(money);
            const createDate: string = DateHelper.dateToString(new Date());
            MoneyModel.update({
                money: money.money,
                update_date: createDate,
                update_user_id: userId
            }, {
                where: {
                    social_activist_id: money.social_activist_id,
                    campaign_id: money.campaign_id,
                    status_id: Status.Active
                },
                returning: true
            })
                .then((value: [affectedCount: number, affectedRows: MoneyModel[]]) => {
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


    private parseMoneyModel(money: MoneyModel): IMoney {
        return {
            social_activist_id: money.social_activist_id,
            campaign_id: money.campaign_id,
            money: money.money
        }
    }

    private parseMoneyHashModel(money: MoneyModel): IMoney {
        return {
            social_activist_id: money.social_activist_id,
            campaign_hash: money.campaign.hashtag,
            money: money.money
        }
    }

}

export default new MoneyService();