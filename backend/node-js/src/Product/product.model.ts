import { SocialActivistModel } from './../SA/socialactivist.model';
import { SocialActivistTransactionModel } from './../SA_Transaction/sa_transaction.model';
import { CampaignModel } from "./../campaign/campaign.model";
import { Column, Table, Model, BelongsToMany, ForeignKey, HasMany, DataType } from "sequelize-typescript";
import { Status } from "../enums";
import { BusinessCompanyRepresentativeModel } from "../BCR/BCR.model";

@Table({
	tableName: "product",
	timestamps: false,
})
export class ProductModel extends Model {
    // @ForeignKey(() => BusinessCompanyRepresentativeModel)
    // @ForeignKey(() => SocialActivistTransactionModel)

    @ForeignKey(() => SocialActivistTransactionModel)
    @Column({
        type: DataType.INTEGER,
        primaryKey: true,
        autoIncrement: true,
    })
    id!: number;
    
	@Column
	name!: string;

	@Column
	value!: string;

	// @Column
	// create_date!: string;

	// @Column
	// update_date!: string;

	// @Column
	// create_user_id!: number;

	// @Column
	// update_user_id!: number;

	// @Column
	// status_id!: Status;

    // @HasMany(() => SocialActivistTransactionModel)
    // campaigns!: CampaignModel[];
    // @HasMany(() => BusinessCompanyRepresentativeModel)
    // businessCompanyRepresentatives!: BusinessCompanyRepresentativeModel[];

    // @BelongsToMany(() => SocialActivistModel, () => SocialActivistTransactionModel)
    // socialActivistTransaction!: SocialActivistTransactionModel[]

}
