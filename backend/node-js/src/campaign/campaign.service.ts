import { ICampaign } from "../entities";
import { AppError, Status } from "../enums";
import { ErrorHelper } from "../helper/error.helper";
import { CampaignModel } from "./campaign.model";

interface ICampaignService {
    getAllCampaigns(): Promise<ICampaign[]>;
}

class CampaignService implements ICampaignService {
    constructor() { }

    public getAllCampaigns(): Promise<ICampaign[]> {
        const result: ICampaign[] = [];
        return new Promise<ICampaign[]>((resolve, reject) => {
            CampaignModel.findAll({
                    where: {
                    status_id: Status.Active
                    }})
                .then((queryResult: CampaignModel[]) => {
                    if (queryResult.length > 0) {
                        queryResult.forEach((campaign: CampaignModel) => {
                        result.push(this.parseCampaignModel(campaign));
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

    private parseCampaignModel(campaign: CampaignModel): ICampaign {
        return {
            id: campaign.id,
            hashtag: campaign.hashtag
        }
    }

}

export default new CampaignService();