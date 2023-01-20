import { ErrorHelper } from '../helper/error.helper';
import { ISocialActivist, systemError, tweetRetweet } from './../entities';
import { Request, Response, NextFunction } from "express"
import SocialActivistService from './socialactivist.service';
import { DEMO_USER, NON_EXISTENT_ID } from '../constants';
import TwitterService from '../twitter/twitter.service';
import { body, check, Result, ValidationError, validationResult } from 'express-validator';
import { AppError } from '../enums';


class SocialActivistController {
  
  constructor() { }
  
    async getSocialActivists(req: Request, res: Response, next: NextFunction) {
      //const authHeader = req.headers.authorization;
    // TwitterService.checkTwitterPost('#trytofindthishash01')
    //   .then((result: tweetRetweet) => {
    //     console.log(result);
    //   })
    SocialActivistService.getSocialActivists()
      .then((result: ISocialActivist[]) => {
        return res.status(200).json(result);
      })
      .catch((error: systemError) => {
        return ErrorHelper.handleError(res, error);
      })
  }

  async getSocialActivistById(req: Request, res: Response, next: NextFunction) {
    const errors: Result<ValidationError> = validationResult(req);
    if (!errors.isEmpty()) {
      return ErrorHelper.handleValidationError(res, errors);
    }
    else {
      let id: number = parseInt(req.params.id);
      if (id > 0) {
        SocialActivistService.getSocialActivistById(id)
          .then((result: ISocialActivist) => {
            return res.status(200).json({
              SA: result
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

  async getSocialActivistByTwitter(req: Request, res: Response, next: NextFunction) {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      return ErrorHelper.handleValidationError(res, errors);
    }
    else {
      let input: string = req.params.twitter;
      SocialActivistService.getSocialActivistByTwitter(input)
        .then((result: ISocialActivist | null) => {
          return res.status(200).json({
            SA: result
          });
        })
        .catch((error: systemError) => {
              return ErrorHelper.handleError(res, error);
        })
    }
  }

  async addSocialActivist(req: Request, res: Response, next: NextFunction) {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      return ErrorHelper.handleValidationError(res, errors);
    }
    else {
      const body: ISocialActivist = req.body;
      body.id = NON_EXISTENT_ID;
      SocialActivistService.addSocialActivist(body)
        .then((result: ISocialActivist) => {
          return res.status(200).json(result);
        })
        .catch((error: systemError) => {
              return ErrorHelper.handleError(res, error);
        })
    }
  }

  async updateSocialActivist(req: Request, res: Response, next: NextFunction) {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      return ErrorHelper.handleValidationError(res, errors);
    }
    else {
      let id: number = parseInt(req.params.id);
      if (id > 0) {
        const body: ISocialActivist = req.body;
        body.id = id;
        SocialActivistService.updateSocialActivist(body)
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
  
  async deleteSocialActivist(req: Request, res: Response, next: NextFunction) {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      return ErrorHelper.handleValidationError(res, errors);
    }
    else {
      let id: number = parseInt(req.params.id);
      let user_id: string = req.params.user_id;
      if (id > 0) {
        SocialActivistService.deleteSocialActivist(id, user_id)
          .then((result: number) => {
            return res.status(200).json({
              rows: result
            });
          })
          .catch((error: systemError) => {
            return ErrorHelper.handleError(res, error);
          });
      }
      else {
        return ErrorHelper.handleError(res, ErrorHelper.getError(AppError.NonPositiveInput))
      }
    }
  }


}

export default new SocialActivistController();