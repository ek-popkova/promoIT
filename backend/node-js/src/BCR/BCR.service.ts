import { Sequelize } from 'sequelize-typescript';
import { Order } from './../enums';
import { DateHelper } from './../helper/date.helper';
import { ErrorHelper } from './../helper/error.helper';
import { ProductModel } from './../Product/product.model';
import { SocialActivistModel } from './../SA/socialactivist.model';
import { SocialActivistTransactionModel } from './../SA_Transaction/sa_transaction.model';
import { include, reject } from "underscore";
import { ISocialActivistTransaction, ISocialActivistTransactionAdd } from "../entities";
import { BusinessCompanyRepresentativeModel } from "./BCR.model";
import { AppError, Status } from '../enums';
import socialactivistService from '../SA/socialactivist.service';

interface IBcrService {
    getSocialActivistTransactionByBCRId(id: number): Promise<SocialActivistTransactionModel[]>
    getSocialActivistTransactionById(id: number): Promise<SocialActivistTransactionModel>
    addSocialActivistTransaction(socialActivistTransaction: ISocialActivistTransaction): Promise<ISocialActivistTransaction>
    // addSocialActivistTransactionNew(socialActivistTransaction: ISocialActivistTransactionAdd, userId: number): Promise<ISocialActivistTransaction>
    updateSocialActivistTransaction(socialActivistTransaction: ISocialActivistTransaction, userId: number): Promise<number>
    deleteSocialActivistTransaction(id: number, userId: number): Promise<number>
}

class BcrService implements IBcrService {
	constructor() {}

	public getSocialActivistTransactionByBCRId(id: number): Promise<SocialActivistTransactionModel[]> {
		const result: SocialActivistTransactionModel[] = [];

		return new Promise<SocialActivistTransactionModel[]>(
			(resolve, reject) => {
                SocialActivistTransactionModel.findAll({
                    attributes: ['id', 'SA_id', 'BCR_id','product_id', 'products_number',
                        'price', 'transaction_status_id',
                        [Sequelize.col('product.name'), 'productName'],
                        [Sequelize.col('product.value'), 'productValue'],
                    ],
                    include: [
                        {
                            model: ProductModel,
                            as: 'product',
                            attributes:[]},
                        {
                            model: SocialActivistModel,
                            attributes:['email', 'phone']},
                        {
                            model: BusinessCompanyRepresentativeModel,
                            attributes: ['company_name']}],
                        where: {
                            BCR_id: id
                        },
                })
                    .then((queryResult: SocialActivistTransactionModel[]) => {

                        if (queryResult.length > 0) {
                            queryResult.forEach((socialActivistTransaction: SocialActivistTransactionModel) => {
                                result.push(socialActivistTransaction);
                            });
                            resolve(result)
                }
                else {
                    reject(ErrorHelper.getError(AppError.NoData))
                }
                })
                .catch(error =>
                    reject(ErrorHelper.getError(AppError.QueryError)))
                
			}
		);
    }

    public getSocialActivistTransactionById(id: number): Promise<SocialActivistTransactionModel> {
        return new Promise<SocialActivistTransactionModel>((resolve, reject) => {
				SocialActivistTransactionModel.findAll({
                    include: [{ model: BusinessCompanyRepresentativeModel },
                        { model: SocialActivistModel }],
                        where: {
                            id: id,
                        },
                })
                .then((queryResult: SocialActivistTransactionModel[]) => {
                    if (queryResult.length > 0) {
                        resolve(queryResult[0])
                    }
                    else {
                        reject(ErrorHelper.getError(AppError.NoData))
                    }
                })
                .catch(error => 
                    reject(ErrorHelper.getError(AppError.QueryError)))
        })
    }
    public addSocialActivistTransaction(socialActivistTransaction: ISocialActivistTransaction): Promise<ISocialActivistTransaction> {
        return new Promise<ISocialActivistTransaction>((resolve, reject) => {
            const createDate: string = DateHelper.dateToString(new Date());
            SocialActivistTransactionModel.create({
                SA_id: socialActivistTransaction.SA_id ? socialActivistTransaction.SA_id : socialActivistTransaction.sA_id,
                BCR_id: socialActivistTransaction.BCR_id ? socialActivistTransaction.BCR_id : socialActivistTransaction.bcR_id,
                product_id: socialActivistTransaction.product_id,
                products_number: socialActivistTransaction.products_number,
                price: socialActivistTransaction.price,
                create_user_id: socialActivistTransaction.create_user_id,
                update_user_id: socialActivistTransaction.update_user_id,
                transaction_status_id: Order.Ordered,
                create_date: createDate,
                update_date: createDate,
                status_id: Status.Active
            })
                .then((result: SocialActivistTransactionModel) => {
                    console.log(result);
                resolve(this.parseLocalSocialActivistModel(result));
            })
                .catch(error =>
                reject(ErrorHelper.getError(AppError.QueryError)))
        })
    }
    // public addSocialActivistTransactionNew(socialActivistTransaction: ISocialActivistTransactionAdd, userId: number): Promise<ISocialActivistTransaction> {
    //     return new Promise<ISocialActivistTransaction>((resolve, reject) => {
    //         const createDate: string = DateHelper.dateToString(new Date());
    //         SocialActivistTransactionModel.create({
    //             SA_id: socialActivistTransaction.sA_id,
    //             BCR_id: socialActivistTransaction.bcR_id,
    //             product_id: socialActivistTransaction.product_id,
    //             products_number: socialActivistTransaction.products_number,
    //             price: socialActivistTransaction.price,
    //             transaction_status_id: Order.Ordered,
    //             create_user_id: userId,
    //             update_user_id: userId,
    //             create_date: createDate,
    //             update_date: createDate,
    //             status_id: Status.Active
    //         })
    //             .then((result: SocialActivistTransactionModel) => {
    //                 console.log(result);
    //             resolve(this.parseLocalSocialActivistModel(result));
    //         })
    //             .catch(error =>
    //             reject(ErrorHelper.getError(AppError.QueryError)))
    //     })
    // }

    public updateSocialActivistTransaction(socialActivistTransaction: ISocialActivistTransaction, userId: number): Promise<number> {
        return new Promise<number>((resolve, reject) => {
            const createDate: string = DateHelper.dateToString(new Date());
            SocialActivistTransactionModel.update({
                SA_id: socialActivistTransaction.SA_id,
                BCR_id: socialActivistTransaction.BCR_id,
                product_id: socialActivistTransaction.product_id,
                products_number: socialActivistTransaction.products_number,
                price: socialActivistTransaction.price,
                transaction_status_id: socialActivistTransaction.transaction_status_id,
                update_date: createDate,
                update_user_id: userId
            }, {
                where: {
                    id: socialActivistTransaction.id,
                    status_id: Status.Active
                },
                returning: true
            })
                .then((value: [affectedCount: number, affectedRows: SocialActivistTransactionModel[]]) => {
                    if (value[0] === 0) {
                        reject(ErrorHelper.getError(AppError.NoData))
                    }
                    else {
                        resolve(value[0]);
                    }
                })
                .catch(error =>
                    reject(ErrorHelper.getError(AppError.QueryError)))
        })
    }

        public ShipSocialActivistTransaction(transactionId: number, userId: number): Promise<number> {
        return new Promise<number>((resolve, reject) => {
            const createDate: string = DateHelper.dateToString(new Date());
            SocialActivistTransactionModel.update({
                transaction_status_id: Order.Shipped,
                update_date: createDate,
                update_user_id: userId
            }, {
                where: {
                    id: transactionId,
                    status_id: Status.Active
                },
                returning: true
            })
                .then((value: [affectedCount: number, affectedRows: SocialActivistTransactionModel[]]) => {
                    if (value[0] === 0) {
                        reject(ErrorHelper.getError(AppError.NoData))
                    }
                    else {
                        resolve(value[0]);
                    }
                })
                .catch(error =>
                    reject(ErrorHelper.getError(AppError.QueryError)))
        })
    }

    public deleteSocialActivistTransaction(id: number, userId: number): Promise<number> {
        return new Promise<number>((resolve, reject) => {
            const createDate: string = DateHelper.dateToString(new Date());
            SocialActivistTransactionModel.update({
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
                .then((value: [number, SocialActivistTransactionModel[]]) => {
                    resolve(value[0]);
                })
                .catch(error =>
                    reject(ErrorHelper.getError(AppError.QueryError)))
        })
    }

    private parseLocalSocialActivistModel(socialActivistTransactionModel: SocialActivistTransactionModel): ISocialActivistTransaction {
        return {
            id: socialActivistTransactionModel.id,
            SA_id: socialActivistTransactionModel.SA_id,
            BCR_id: socialActivistTransactionModel.BCR_id,
            product_id: socialActivistTransactionModel.product_id,
            products_number: socialActivistTransactionModel.products_number,
            price: socialActivistTransactionModel.price,
            transaction_status_id: socialActivistTransactionModel.transaction_status_id,
            create_user_id: socialActivistTransactionModel.create_user_id,
            update_user_id: socialActivistTransactionModel.update_user_id
        }
    }
}
export default new BcrService();