import { Column, Table, Model, BelongsToMany, DataType, ForeignKey, HasMany, BelongsTo } from "sequelize-typescript";
import { SocialActivistTransactionModel } from '../SA_Transaction/sa_transaction.model';

@Table({
	tableName: "transaction_status",
	timestamps: false,
})
export class TransactionStatusModel extends Model {
    @ForeignKey(() => SocialActivistTransactionModel)
        
    @Column({
    type: DataType.INTEGER,
    primaryKey: true,
    autoIncrement: true,
    })
    id!: number;

	@Column
    transaction_status!: number;

}
