import { Order } from './../enums';
import { TransactionStatusModel } from './../transaction status/transaction_status.model';
import { SocialActivistModel } from './../SA/socialactivist.model';
import { Column, Table, Model, BelongsToMany, DataType, ForeignKey, HasMany, BelongsTo } from "sequelize-typescript";
import { Status } from "../enums";
import { BusinessCompanyRepresentativeModel } from "../BCR/BCR.model";
import { ProductModel } from "../Product/product.model";

@Table({
	tableName: "SA_transaction",
	timestamps: false,
})
export class SocialActivistTransactionModel extends Model {
    // @ForeignKey(() => BusinessCompanyRepresentativeModel)
    // @ForeignKey(() => ProductModel)
    // @ForeignKey(() => SocialActivistModel)
    @Column({
    type: DataType.INTEGER,
    primaryKey: true,
    autoIncrement: true,
    })
    id!: number;

    @Column
    SA_id!: number;
        
    @Column
    BCR_id!: number;

    @Column
    product_id!: number;

	@Column
	products_number!: number;

	@Column
    price!: number;

    @Column
    transaction_status_id!: Order;

	@Column
	create_user_id!: string;

	@Column
	update_user_id!: string;

	@Column
	create_date!: string;

	@Column
	update_date!: string;

	@Column
    status_id!: Status;

    // @BelongsToMany(() => BusinessCompanyRepresentativeModel, () => SocialActivistModel)
    // socialActivist!: SocialActivistModel;

    @BelongsTo(() => SocialActivistModel, {foreignKey: "SA_id"})
    socialAscivist!: SocialActivistModel;

    @BelongsTo(() => BusinessCompanyRepresentativeModel, {foreignKey: "BCR_id"})
    businessCompanyRepresentative!: BusinessCompanyRepresentativeModel;

    @BelongsTo(() => ProductModel, {foreignKey: "product_id"})
    product!: ProductModel;

    @BelongsTo(() => TransactionStatusModel, { foreignKey: "transaction_status_id" })
    transactionStatus!: TransactionStatusModel;

}
