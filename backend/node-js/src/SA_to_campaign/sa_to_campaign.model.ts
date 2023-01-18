import { Order } from "sequelize";
import { Column, DataType, Model, Table } from "sequelize-typescript";
import { Status } from "../enums";

@Table({
    tableName: "sa_to_campaign",
    timestamps:false,
})

export class SA_to_campaignModel extends Model {
    
    @Column({
    type: DataType.INTEGER,
    primaryKey: true,
    autoIncrement: true,
    })
    id!: number;

    @Column
    social_activist_id!: number;
        
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
	update_date!: string;

	@Column
    status_id!: Status;
}