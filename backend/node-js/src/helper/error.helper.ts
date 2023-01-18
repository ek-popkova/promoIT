import { systemError } from "../entities";
import { Response } from 'express';
import { AppError } from "../enums";
import { Dictionary } from "underscore";
import { Result, ValidationError } from "express-validator";

export const errorDictionary: Dictionary<systemError> = {
    [AppError.QueryError]: {
            key: AppError.QueryError,
            code: 101,
            message: "Incorrect query"
    }, 
    [AppError.NoData]: {
        key: AppError.NoData,
        code: 102,
        message: "Not found"
    },
    [AppError.NonPositiveInput]: {
        key: AppError.NonPositiveInput,
        code: 103,
        message: "Non positive input supplied"
    },
    [AppError.TwitterConnectionError]: {
        key: AppError.TwitterConnectionError,
        code: 104,
        message: "Unable to connect to twitter"
    }
};

export class ErrorHelper {
    public static handleError(response: Response, error: systemError, isAuthentification: boolean = false): Response<any, Record<string, any>> {
        switch (error.code) {
            case errorDictionary[AppError.TwitterConnectionError].code:
                return response.status(408).json({
                    errorMassage: error.message
                });
            case errorDictionary[AppError.QueryError].code:
            case errorDictionary[AppError.NonPositiveInput].code:
                return response.status(406).json({
                     errorMassage: error.message
                });
            case errorDictionary[AppError.NoData].code:
                if (isAuthentification) {
                    return response.sendStatus(403);
                }
                else {
                return response.status(404).json({
                    errorMessage: error.message
                });
                }
            default:
                return response.status(400).json({
                    errorMassage: error.message
                });
        }
    }

    public static handleValidationError(response: Response, error: Result<ValidationError>): Response<any, Record<string, any>> {
        let errorArray: string[] = [];
        error.array().forEach((err) => errorArray.push(err.msg));
        return response.status(406).json({
                errorMassage: errorArray
            });
    }

    public static getError(key: AppError): systemError {
        return errorDictionary[key];
    }

    public static getErrorMessage(key: AppError): string {
        return errorDictionary[key].message;
    }

}