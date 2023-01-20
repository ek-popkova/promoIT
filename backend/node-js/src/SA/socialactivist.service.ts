import { CampaignModel } from './../campaign/campaign.model';
import { ISocialActivist, systemError } from '../entities';
import { AppError, Status } from '../enums';
import { DateHelper } from '../helper/date.helper';
import { SocialActivistModel } from './socialactivist.model';
import { MoneyModel } from '../money/money.model';
import { ErrorHelper } from '../helper/error.helper';
interface ISocialActivistService {
    getSocialActivists(): Promise<ISocialActivist[]>;
    getSocialActivistById(id: number): Promise<ISocialActivist>;
    addSocialActivist(socialActivist: ISocialActivist, userId: string): Promise<ISocialActivist>;
    updateSocialActivist(socialActivist: ISocialActivist, userId: string): Promise<number>;
    deleteSocialActivist(id: number, userId: string): Promise<number>;
}

class SocialActivistService implements ISocialActivistService {
    constructor() { }
    
    public getSocialActivists(): Promise<ISocialActivist[]> {
        const result: ISocialActivist[] = [];
        return new Promise<ISocialActivist[]>((resolve, reject) => {
            SocialActivistModel.findAll({
                    where: {
                    status_id: Status.Active
                }})
                .then((queryResult: SocialActivistModel[]) => {
                    if (queryResult.length > 0) {
                        queryResult.forEach((socialActivist: SocialActivistModel) => {
                            result.push(this.parseSocialActivistModel(socialActivist));
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

    public getSocialActivistById(id: number): Promise<ISocialActivist> {
        return new Promise<ISocialActivist>((resolve, reject) => {
            SocialActivistModel.findAll({
                    where: {
                    id: id
                    }})
                .then((queryResult: SocialActivistModel[]) => {
                    if (queryResult.length > 0) {
                        resolve(this.parseSocialActivistModel(queryResult[0]));
                    }
                    else {
                        reject(ErrorHelper.getError(AppError.NoData))
                    }
                })
                .catch(error =>
                    reject(ErrorHelper.getError(AppError.QueryError)))
        });
    }


    public getSocialActivistByTwitter(twitter: string): Promise<ISocialActivist> {
        return new Promise<ISocialActivist>((resolve, reject) => {
            SocialActivistModel.findAll({
                    where: {
                    twitter: twitter
                    }})
                .then((queryResult: SocialActivistModel[]) => {
                    if (queryResult.length > 0) {
                        resolve(this.parseSocialActivistModel(queryResult[0]));
                    }
                    else {
                        reject(ErrorHelper.getError(AppError.NoData))
                    }
                })
                .catch(error =>
                    reject(ErrorHelper.getError(AppError.QueryError)))
        });
    }

    public addSocialActivist(socialActivist: ISocialActivist, userId: string): Promise<ISocialActivist> {
        return new Promise<ISocialActivist>((resolve, reject) => {
            const createDate: string = DateHelper.dateToString(new Date());
            SocialActivistModel.create({
                user_id: socialActivist.user_id,
                email: socialActivist.email,
                address: socialActivist.address,
                phone: socialActivist.phone,
                twitter: socialActivist.twitter,
                create_date: createDate,
                update_date: createDate,
                create_user_id: userId,
                update_user_id: userId,
                status_id: Status.Active
            })
            .then((result: SocialActivistModel) => {
                resolve(this.parseSocialActivistModel(result));
            })
            .catch(error =>
                reject(ErrorHelper.getError(AppError.QueryError)))
        });
    }

    public updateSocialActivist(socialActivist: ISocialActivist, userId: string): Promise<number> {
        return new Promise<number>((resolve, reject) => {
            const createDate: string = DateHelper.dateToString(new Date());
            SocialActivistModel.update({
                user_id: socialActivist.user_id,
                email: socialActivist.email,
                address: socialActivist.address,
                phone: socialActivist.phone,
                twitter: socialActivist.twitter,
                update_date: createDate,
                update_user_id: userId
            }, {
                where: {
                    id: socialActivist.id,
                    status_id: Status.Active
                },
                returning: true
            })
                .then((value: [affectedCount: number, affectedRows: SocialActivistModel[]]) => {
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

    public deleteSocialActivist(id: number, userId: string): Promise<number> {
        return new Promise<number>((resolve, reject) => {
            const createDate: string = DateHelper.dateToString(new Date());
            SocialActivistModel.update({
                status_id: Status.NotActive,
                update_date: createDate,
                update_user_id: userId
            }, {
                where: {
                    id: id,
                    status_id: Status.Active
                },
                returning: true
            })
                .then((value: [number, SocialActivistModel[]]) => {
                    resolve(value[0]);
            })
            .catch(error =>
                reject(ErrorHelper.getError(AppError.QueryError)))
        });
    }

    private parseSocialActivistModel(socialActivist: SocialActivistModel): ISocialActivist {
        return {
            id: socialActivist.id,
            user_id: socialActivist.user_id,
            email: socialActivist.email,
            address: socialActivist.address,
            phone: socialActivist.phone,
            twitter: socialActivist.twitter
        }
    }
}

export default new SocialActivistService();