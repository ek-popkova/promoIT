import { ITwitterReport } from '../entities';
import { ErrorHelper } from '../helper/error.helper';
import { systemError} from '../entities';
import { Request, Response, NextFunction } from "express"
import TwitterReportService from './twitterreport.service';
import { DEMO_USER, NON_EXISTENT_ID } from '../constants';
import { body, check, Result, ValidationError, validationResult } from 'express-validator';
import { AppError } from '../enums';


class TwitterReporterController {
  
  constructor() { }
  
  async getTwitterReports(req: Request, res: Response, next: NextFunction) {
    TwitterReportService.getTwitterReports()
      .then((result: ITwitterReport[]) => {
        return res.status(200).json(result);
      })
      .catch((error: systemError) => {
        return ErrorHelper.handleError(res, error);
      })
  }

  async getTwitterReportByCampaignId(req: Request, res: Response, next: NextFunction) {
    const errors: Result<ValidationError> = validationResult(req);
    if (!errors.isEmpty()) {
      return ErrorHelper.handleValidationError(res, errors);
    }
    else {
      let id: number = parseInt(req.params.id);
      if (id > 0) {
        TwitterReportService.getTwitterReportByCampaignId(id)
          .then((result: ITwitterReport) => {
            return res.status(200).json({
              report: result
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

  async addTwitterReport(req: Request, res: Response, next: NextFunction) {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      return ErrorHelper.handleValidationError(res, errors);
    }
    else {
      const body: ITwitterReport = req.body;
      TwitterReportService.addTwitterReport(body, DEMO_USER)
        .then((result: ITwitterReport) => {
          return res.status(200).json(result);
        })
        .catch((error: systemError) => {
              return ErrorHelper.handleError(res, error);
        })
    }
  }

  async updateTwitterReport(req: Request, res: Response, next: NextFunction) {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      return ErrorHelper.handleValidationError(res, errors);
    }
    else {
      let id: number = parseInt(req.params.id);
      if (id > 0) {
        const body: ITwitterReport = req.body;
        body.campaign_id = id;
        TwitterReportService.updateTwitterReport(body, DEMO_USER)
          .then((result: number) => {
            return res.status(200).json({
              rows: result
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


}

export default new TwitterReporterController();