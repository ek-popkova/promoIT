import { BusinessCompanyRepresentativeModel } from './../BCR/BCR.model';
import { CampaignModel } from './../campaign/campaign.model';
import { Column, Table, Model, BelongsToMany, HasMany, ForeignKey, DataType } from 'sequelize-typescript';
import { Status } from '../enums';
import { MoneyModel } from '../money/money.model';
import { SocialActivistTransactionModel } from '../SA_Transaction/sa_transaction.model';

@Table({
    tableName: "SA",
    timestamps: false,
})
export class SocialActivistModel extends Model {
    // @ForeignKey(() => SocialActivistTransactionModel)
    // @ForeignKey(() => BusinessCompanyRepresentativeModel)

    @ForeignKey(() => SocialActivistTransactionModel)
    @Column({
        type: DataType.INTEGER,
        primaryKey: true,
        autoIncrement: true,
    })
    id!: number;

    @Column
    user_id!: string;

    @Column
    email!: string;

    @Column
    address!: string;

    @Column
    phone!: string;

    @Column
    twitter!: string;

    @Column
    create_user_id!: number;

    @Column
    update_user_id!: number;

    @Column
    create_date!: string;

    @Column
    update_date!: string

    @Column
    status_id!: Status;

    // @BelongsToMany(() => CampaignModel, () => MoneyModel)
    // campaigns!: CampaignModel[];

    // @HasMany(() => SocialActivistTransactionModel)
    // socialActivistTransactions!: SocialActivistTransactionModel[]
}