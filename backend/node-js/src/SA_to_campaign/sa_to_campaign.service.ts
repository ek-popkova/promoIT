import { ErrorHelper } from './../helper/error.helper';
import { AppError, Status } from './../enums';
import { SA_to_campaignModel } from './sa_to_campaign.model';
import { DateHelper } from './../helper/date.helper';
import { reject, where } from 'underscore';
import { ISAtoCampaign } from './../entities';
interface ISA_to_campaign {
    updateSAtoCampaign(sa_to_campaign: ISAtoCampaign, userId: string): Promise<number>
}

class SA_to_campaign implements ISA_to_campaign {

    constructor() { }
    
    public updateSAtoCampaign(sa_to_campaign: ISAtoCampaign): Promise<number> {
        return new Promise<number>((resolve, reject) => {
            const createDate: string = DateHelper.dateToString(new Date());
            SA_to_campaignModel.update({
                social_activist_id: sa_to_campaign.social_activist_id,
                campaign_id: sa_to_campaign.campaign_id,
                money: sa_to_campaign.money,
            }, {
             where: {
                id: sa_to_campaign.id,
                status_id: Status.Active
            },
                returning: true
            })
                .then((value: [affectedCount: number, affectedRows: SA_to_campaignModel[]]) => {
                    if (value[0] === 0) {
                     reject(ErrorHelper.getError(AppError.NoData))
                    } else {
                        resolve(value[0]);
                 }
                })   
            .catch(error => reject (ErrorHelper.getError(AppError.QueryError)))
        })
    }
}

export default new SA_to_campaign();