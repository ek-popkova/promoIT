import { Sequelize } from 'sequelize-typescript';
//import { DB_CONNECTION_STRING } from "../constants";

export const openConnection: Sequelize = new Sequelize({
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
            }
        });



