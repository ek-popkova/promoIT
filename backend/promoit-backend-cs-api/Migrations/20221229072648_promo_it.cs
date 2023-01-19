using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace promoit_backend_cs_api.Migrations
{
    public partial class promo_it : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transaction_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transaction_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    create_user_id = table.Column<int>(type: "int", nullable: false),
                    update_user_id = table.Column<int>(type: "int", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_status",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    login = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    create_user_id = table.Column<int>(type: "int", nullable: false),
                    update_user_id = table.Column<int>(type: "int", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_role",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_status",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "BCR",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    create_user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    update_user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BCR", x => x.id);
                    table.ForeignKey(
                        name: "FK_BCR_status",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "non_profit_representative",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    organization_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    organization_link = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    create_user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    update_user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_non_profit_representative", x => x.id);
                    table.ForeignKey(
                        name: "FK_non_profit_representative_status",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "SA",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    twitter = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    create_user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    update_user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SA", x => x.id);
                    table.ForeignKey(
                        name: "FK_SA_status",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    value = table.Column<int>(type: "int", nullable: false),
                    BCR_id = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    create_user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    update_user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_BCR",
                        column: x => x.BCR_id,
                        principalTable: "BCR",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_product_status",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "campaign",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    link = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    hashtag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NPR_id = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    create_user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    update_user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campaign", x => x.id);
                    table.ForeignKey(
                        name: "FK_campaign_non_profit_representative",
                        column: x => x.NPR_id,
                        principalTable: "non_profit_representative",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_campaign_status",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "SA_transaction",
                columns: table => new
                {
                    SA_id = table.Column<int>(type: "int", nullable: false),
                    BCR_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    products_number = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    transaction_status_id = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    create_user_id = table.Column<int>(type: "int", nullable: false),
                    update_user_id = table.Column<int>(type: "int", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_SA_transaction_BCR",
                        column: x => x.BCR_id,
                        principalTable: "BCR",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SA_transaction_product",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SA_transaction_SA",
                        column: x => x.SA_id,
                        principalTable: "SA",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SA_transaction_status",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_SA_transaction_transaction_status",
                        column: x => x.transaction_status_id,
                        principalTable: "transaction_status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "BCR_ship",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BCR_id = table.Column<int>(type: "int", nullable: false),
                    campaign_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    product_ready = table.Column<int>(type: "int", nullable: false),
                    product_bought = table.Column<int>(type: "int", nullable: false),
                    product_price = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    create_user_id = table.Column<int>(type: "int", nullable: false),
                    update_user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BCR_ship", x => x.id);
                    table.ForeignKey(
                        name: "FK_BCR_ship_BCR",
                        column: x => x.BCR_id,
                        principalTable: "BCR",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_BCR_ship_campaign",
                        column: x => x.campaign_id,
                        principalTable: "campaign",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_BCR_ship_product",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "product_to_campaign",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    campaign_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    initital_number = table.Column<int>(type: "int", nullable: false),
                    bought_number = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    create_user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    update_user_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_to_campaign", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_to_campaign_campaign",
                        column: x => x.campaign_id,
                        principalTable: "campaign",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_product_to_campaign_product",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_product_to_campaign_status",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_to_campaign",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    campaign_id = table.Column<int>(type: "int", nullable: false),
                    money = table.Column<int>(type: "int", nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    update_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    create_user_id = table.Column<int>(type: "int", nullable: false),
                    update_user_id = table.Column<int>(type: "int", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_user_to_campaign_campaign",
                        column: x => x.campaign_id,
                        principalTable: "campaign",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_to_campaign_status",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_to_campaign_user",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BCR_status_id",
                table: "BCR",
                column: "status_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_BCR_user_id",
            //    table: "BCR",
            //    column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_BCR_ship_BCR_id",
                table: "BCR_ship",
                column: "BCR_id");

            migrationBuilder.CreateIndex(
                name: "IX_BCR_ship_campaign_id",
                table: "BCR_ship",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_BCR_ship_product_id",
                table: "BCR_ship",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_campaign_NPR_id",
                table: "campaign",
                column: "NPR_id");

            migrationBuilder.CreateIndex(
                name: "IX_campaign_status_id",
                table: "campaign",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_non_profit_representative_status_id",
                table: "non_profit_representative",
                column: "status_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_non_profit_representative_user_id",
            //    table: "non_profit_representative",
            //    column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_BCR_id",
                table: "product",
                column: "BCR_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_status_id",
                table: "product",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_to_campaign_campaign_id",
                table: "product_to_campaign",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_to_campaign_product_id",
                table: "product_to_campaign",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_to_campaign_status_id",
                table: "product_to_campaign",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_status_id",
                table: "role",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_SA_status_id",
                table: "SA",
                column: "status_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SA_user_id",
            //    table: "SA",
            //    column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_SA_transaction_BCR_id",
                table: "SA_transaction",
                column: "BCR_id");

            migrationBuilder.CreateIndex(
                name: "IX_SA_transaction_product_id",
                table: "SA_transaction",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_SA_transaction_SA_id",
                table: "SA_transaction",
                column: "SA_id");

            migrationBuilder.CreateIndex(
                name: "IX_SA_transaction_status_id",
                table: "SA_transaction",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_SA_transaction_transaction_status_id",
                table: "SA_transaction",
                column: "transaction_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_status_id",
                table: "user",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_to_campaign_campaign_id",
                table: "user_to_campaign",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_to_campaign_status_id",
                table: "user_to_campaign",
                column: "status_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_user_to_campaign_user_id",
            //    table: "user_to_campaign",
            //    column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BCR_ship");

            migrationBuilder.DropTable(
                name: "product_status");

            migrationBuilder.DropTable(
                name: "product_to_campaign");

            migrationBuilder.DropTable(
                name: "SA_transaction");

            migrationBuilder.DropTable(
                name: "user_to_campaign");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "SA");

            migrationBuilder.DropTable(
                name: "transaction_status");

            migrationBuilder.DropTable(
                name: "campaign");

            migrationBuilder.DropTable(
                name: "BCR");

            migrationBuilder.DropTable(
                name: "non_profit_representative");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "status");
        }
    }
}
