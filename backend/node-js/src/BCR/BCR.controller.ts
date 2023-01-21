import { Order } from './../enums';
import { NON_EXISTENT_ID, DEMO_USER } from './../constants';
import { systemError, ISocialActivistTransaction } from './../entities';
import { SocialActivistTransactionModel } from './../SA_Transaction/sa_transaction.model';
import { ErrorHelper } from './../helper/error.helper';
import { Result, ValidationError, validationResult } from 'express-validator';
import { Request, Response, NextFunction } from 'express';
import BcrService from './BCR.service'
import { AppError } from '../enums';
import { result } from 'underscore';
import twitterService from '../twitter/twitter.service';

class BusinessCompanyRepresentativeController {

    constructor() { }
    
    async getSocialActivistTransactionByBCRId(req: Request, res: Response, next: NextFunction) {
        const errors: Result<ValidationError> = validationResult(req);
        if (!errors.isEmpty()) {
            return ErrorHelper.handleValidationError(res, errors);
        }
        else {
            let id: number = parseInt(req.params.id)
            if (id > 0) {

                BcrService.getSocialActivistTransactionByBCRId(id)
                    .then((result: SocialActivistTransactionModel[]) => {
                        return res.status(200).json(result)
                })
                    .catch((error: systemError) => {
                    return ErrorHelper.handleError(res, error)
                })
            }
            else {
                return ErrorHelper.handleError(res, ErrorHelper.getError(AppError.NonPositiveInput))
            }
        }
    }

    async addSocialActivistTransaction(req: Request, res: Response, next: NextFunction) {
        const errors = validationResult(req);
        if (!errors.isEmpty()) {
            return ErrorHelper.handleValidationError(res, errors);
        }
        else {
            const body: ISocialActivistTransaction = req.body;
            body.id = NON_EXISTENT_ID;
            BcrService.addSocialActivistTransaction(body)
                .then((result: ISocialActivistTransaction) => {
                    return res.status(200).json(result);
                })
                .catch((error: systemError) => {
                    return ErrorHelper.handleError(res, error);
                })
       }
    }

    async updateSocialActivistTransaction(req: Request, res: Response, next: NextFunction) {
        const errors = validationResult(req);
        if (!errors.isEmpty()) {
            return ErrorHelper.handleValidationError(res, errors);
        }
        else {
            let id: number = parseInt(req.params.id);
            if (id > 0) {
                const body: ISocialActivistTransaction = req.body;
                const user_id = req.params.user_id
                body.id = id;
                BcrService.updateSocialActivistTransaction(body, user_id)
                .then((result: number) => {
                    return res.status(200).json({
                        rows:result
                    })
                })
                // .then(async() => {
                // try {
                //         if (body.transaction_status_id === Order.Shipped) {
                //             let socialActivistTransaction = await BcrService.getSocialActivistTransactionById(body.id);
                //             await twitterService.makeSystemTweet(socialActivistTransaction);
                //         }
                //     } catch (error) {
                //         console.error(error);
                //     }
                //     })
                .catch((error: systemError) => {
                    return ErrorHelper.handleError(res, error);
                })
            }
            else {
                return ErrorHelper.handleError(res, ErrorHelper.getError(AppError.NonPositiveInput))
            }
        }
    }

        async ShipSocialActivistTransaction(req: Request, res: Response, next: NextFunction) {
        const errors = validationResult(req);
        if (!errors.isEmpty()) {
            return ErrorHelper.handleValidationError(res, errors);
        }
        else {
            let id: number = parseInt(req.params.id);
            let user_id: string = req.params.user_id;
            if (id > 0) {
                BcrService.ShipSocialActivistTransaction(id, user_id)
                .then((result: number) => {
                    return res.status(200).json({
                        rows:result
                    })
                })
                .then(async() => {
                try {
                        let socialActivistTransaction = await BcrService.getSocialActivistTransactionById(id);
                        await twitterService.makeSystemTweet(socialActivistTransaction);
                    } catch (error) {
                        console.error(error);
                    }
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

    async deleteSocialActivistTransaction(req: Request, res: Response, next: NextFunction) {
        const errors = validationResult(req);
        if (!errors.isEmpty()) {
            return ErrorHelper.handleValidationError(res, errors);
        }
        else {
            let id: number = parseInt(req.params.id);
            if (id > 0) {
                BcrService.deleteSocialActivistTransaction(id, DEMO_USER)
                    .then((result: number) => {
                        return res.status(200).json({
                            rows: result
                        })
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
export default new BusinessCompanyRepresentativeController();