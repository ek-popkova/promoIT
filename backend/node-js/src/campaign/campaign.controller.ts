import { ICampaign, systemError } from './../entities';
import { Request, Response, NextFunction } from "express"
import CampaignService from './campaign.service';
import { ErrorHelper } from '../helper/error.helper';


class CampaignController {
  
  constructor() { }

  
  async getAllCampaigns(req: Request, res: Response, next: NextFunction) {
    CampaignService.getAllCampaigns()
      .then((result: ICampaign[]) => {
        return res.status(200).json({
          campaigns: result
        });
      })
      .catch((error: systemError) => {
        return ErrorHelper.handleError(res, error);
      })
  }

  
}

export default new CampaignController();