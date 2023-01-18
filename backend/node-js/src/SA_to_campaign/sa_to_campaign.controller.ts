import { DEMO_USER } from './../constants';
import { ISAtoCampaign, systemError } from './../entities';
import { ErrorHelper } from './../helper/error.helper';
import { validationResult } from 'express-validator';
import { NextFunction, Request, Response } from 'express';
import Sa_to_campaignService from './sa_to_campaign.service';
import { AppError } from '../enums';


class SA_to_campaignController {
    constructor() { }
    
    async updateSAtoCampaign(req: Request, res: Response, next: NextFunction) {
        const errors = validationResult(req);
        if (!errors.isEmpty()) {
            return ErrorHelper.handleValidationError(res, errors)
        }
        else {
            let id: number = parseInt(req.params.id);
            if (id > 0) {
                const body: ISAtoCampaign = req.body;
                body.id = id;
                Sa_to_campaignService.updateSAtoCampaign(body, DEMO_USER)
                    .then((result: number) => {
                        return res.status(200).json({
                        rows: result
                    })
                    })
                    .catch((error: systemError) => {
                    return ErrorHelper.handleError(res, error)
                })
            } else {
                return ErrorHelper.handleError(res, ErrorHelper.getError(AppError.NonPositiveInput))
            }
        }
    }
}

export default new SA_to_campaignController();