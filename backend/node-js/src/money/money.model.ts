import { CampaignModel } from '../campaign/campaign.model';
import { SocialActivistModel } from '../SA/socialactivist.model';
import { BelongsTo, Column, ForeignKey, Model, Table } from "sequelize-typescript";
import { Status } from '../enums';

@Table({
    tableName: "sa_to_campaign",
    timestamps: false,
})
export class MoneyModel extends Model {
    @ForeignKey(() => SocialActivistModel)
    @Column
    social_activist_id!: number;

    @ForeignKey(() => CampaignModel)
    @Column
    campaign_id!: number;

    @Column
    money!: number;

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

    @BelongsTo(() => CampaignModel, {foreignKey: "campaign_id"})
    campaign!: CampaignModel;
}