import { CampaignModel } from '../campaign/campaign.model';
import { Column, Table, Model, BelongsTo } from 'sequelize-typescript';
import { Status } from '../enums';

@Table({
    tableName: "tweet",
    timestamps: false,
})
export class TwitterReportModel extends Model {
    @Column
    campaign_id!: number;

    @Column
    tweet!: number;

    @Column
    retweet!: number;

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