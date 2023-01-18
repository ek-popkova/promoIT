import { SocialActivistModel } from './../SA/socialactivist.model';
import { Column, Table, Model, BelongsToMany, BelongsTo, DataType, ForeignKey } from 'sequelize-typescript';
import { Status } from '../enums';
import { MoneyModel } from '../money/money.model';
import { TwitterReportModel } from '../twitterReport/tweeterreport.model';


@Table({
    tableName: "campaign",
    timestamps: false,
})
export class CampaignModel extends Model {
    @ForeignKey(() => MoneyModel)
    @ForeignKey(() => TwitterReportModel)
    @Column({
        type: DataType.INTEGER,
        primaryKey: true,
        autoIncrement: true,
    })
    id!: number;
    
    @Column
    hashtag!: string;

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

    // @BelongsToMany(() => SocialActivistModel, () => MoneyModel)
    // socialActivists!: SocialActivistModel[];

}