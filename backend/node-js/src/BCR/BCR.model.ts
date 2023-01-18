import { SocialActivistTransactionModel } from './../SA_Transaction/sa_transaction.model';
import { SocialActivistModel } from './../SA/socialactivist.model';
import { Column, Table, Model, BelongsToMany, HasMany, ForeignKey, DataType } from "sequelize-typescript";
import { Status } from "../enums";
import { ProductModel } from "../Product/product.model";

@Table({
	tableName: "BCR",
	timestamps: false,
})
export class BusinessCompanyRepresentativeModel extends Model {
   // @ForeignKey(() => ProductModel)
    
    @ForeignKey(() => SocialActivistTransactionModel)
    @Column({
        type: DataType.INTEGER,
        primaryKey: true,
        autoIncrement: true,
    })
    id!: number;
        
	@Column
	company_name!: string;

	@Column
	user_id!: number;

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

    // @BelongsToMany(() => SocialActivistModel, () => SocialActivistTransactionModel)

    // @HasMany(() => ProductModel)
    // products!: ProductModel[];
}
