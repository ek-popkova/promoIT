import { TransactionStatusModel } from './../transaction status/transaction_status.model';
import { Sequelize } from 'sequelize-typescript'
import { SocialActivistModel } from '../SA/socialactivist.model';
import { CampaignModel } from '../campaign/campaign.model';
import { MoneyModel } from '../money/money.model';
import { TwitterReportModel } from '../twitterReport/tweeterreport.model';
import dotenv from "dotenv"
import { BusinessCompanyRepresentativeModel } from '../BCR/BCR.model';
import { SocialActivistTransactionModel } from '../SA_Transaction/sa_transaction.model';
import { ProductModel } from '../Product/product.model';
import { SA_to_campaignModel } from '../SA_to_campaign/sa_to_campaign.model';
dotenv.config({})

export class DBservice {

    public static initialize(): Sequelize {
        
        let db: Sequelize = new Sequelize({
            dialect: 'mssql',
            dialectModulePath: 'msnodesqlv8/lib/sequelize',
            dialectOptions: {
                user: '',
                password: '',
                database: 'node',
                options: {
                    driver: '',
                    connectionString: process.env.DB_CONNECTION_STRING,
                    trustedConnection: true,
                    instanceName: ''
                }
            },
            pool: {
                min: 0,
                max: 5,
                idle: 10000
            },
            models: [SocialActivistModel,
                CampaignModel,
                MoneyModel,
                TwitterReportModel,
                BusinessCompanyRepresentativeModel,
                SocialActivistTransactionModel,
                ProductModel,
                TransactionStatusModel,
                SA_to_campaignModel]
        });
        
        db
            .authenticate()
            .then(() => {
                console.log('Connection has been established successfully.');
            })
            .catch(err => {
                console.error('Unable to connect to the database:', err);
            });
        
        return db;
    }

}

export const openConnection: Sequelize = DBservice.initialize();