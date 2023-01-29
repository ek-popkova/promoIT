import { Request, Response, NextFunction } from "express"
import { validationResult } from "express-validator";
import { DEMO_USER } from "../constants";
import { IMoney, systemError } from "../entities";
import { AppError } from "../enums";
import { ErrorHelper } from "../helper/error.helper";
import MoneyService from './money.service';


class MoneyController {
  
  constructor() { }

  async getMoneyByHashtagTwitter(req: Request, res: Response, next: NextFunction) {
    const body: IMoney = req.body;
    MoneyService.getMoneyByHashtagTwitter(body.social_activist_id, body.campaign_id as number)
      .then((result: IMoney) => {
        //console.log(result);
        return res.status(200).json({
          money: result
        });
      })
      .catch((error: systemError) => {
              return ErrorHelper.handleError(res, error);
          })
  }

  async getMoneyBySocialActivistId(req: Request, res: Response, next: NextFunction) {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      return ErrorHelper.handleValidationError(res, errors);
    }
    else {
      let id: number = parseInt(req.params.id);
      if (id > 0) {
        MoneyService.getMoneyBySocialActivistId(id)
          .then((result: IMoney[]) => {
            return res.status(200).json({
              MoneyReport: result
            });
          })
          .catch((error: systemError) => {
              return ErrorHelper.handleError(res, error);
          })
      }
      else {
        return ErrorHelper.handleError(res, ErrorHelper.getError(AppError.NonPositiveInput))
      }
    }
  }

  async getMoneyBySocialActivistIdAndCampaignId(req: Request, res: Response, next: NextFunction) {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      return ErrorHelper.handleValidationError(res, errors);
    }
    else {
      let sa_id: number = parseInt(req.params.sa_id);
      console.log(sa_id);
      let campaign_id: number = parseInt(req.params.campaign_id);
      console.log(campaign_id);
      if ((sa_id > 0) && (campaign_id > 0)) {
        MoneyService.getMoneyBySocialActivistIdAndCampaignId(sa_id, campaign_id)
          .then((result: number) => {
            return res.status(200).json(result);
          })
          .catch((error: systemError) => {
              return ErrorHelper.handleError(res, error);
          })
      }
      else {
        return ErrorHelper.handleError(res, ErrorHelper.getError(AppError.NonPositiveInput))
      }
    }
  }


  async addMoneyByHashtagTwitter(req: Request, res: Response, next: NextFunction) {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      return ErrorHelper.handleValidationError(res, errors);
    }
    else {
      const body: IMoney = req.body;
      MoneyService.addMoneyByHashtagTwitter(body, DEMO_USER)
        .then((result: IMoney) => {
          return res.status(200).json(result);
        })
        .catch((error: systemError) => {
              return ErrorHelper.handleError(res, error);
          })
    }
  };

  async updateMoneyByHashtagTwitter(req: Request, res: Response, next: NextFunction) {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      return ErrorHelper.handleValidationError(res, errors);
    }
    else {
      const body: IMoney = req.body;
      MoneyService.updateMoneyByHashtagTwitter(body, DEMO_USER)
        .then((result: number) => {
          return res.status(200).json({
            rows: result
          });
        })
        .catch((error: systemError) => {
              return ErrorHelper.handleError(res, error);
        })
    }
  }

  
}

export default new MoneyController();