using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    SecretLogin = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.SecretLogin);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserSecretLogin = table.Column<string>(nullable: true),
                    UserSecretLogin1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Channels_Users_UserSecretLogin",
                        column: x => x.UserSecretLogin,
                        principalTable: "Users",
                        principalColumn: "SecretLogin",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Channels_Users_UserSecretLogin1",
                        column: x => x.UserSecretLogin1,
                        principalTable: "Users",
                        principalColumn: "SecretLogin",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegularExpensesCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserSecretLogin = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    UserSecretLogin1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularExpensesCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegularExpensesCategories_Users_UserSecretLogin",
                        column: x => x.UserSecretLogin,
                        principalTable: "Users",
                        principalColumn: "SecretLogin",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegularExpensesCategories_Users_UserSecretLogin1",
                        column: x => x.UserSecretLogin1,
                        principalTable: "Users",
                        principalColumn: "SecretLogin",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SingleExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChannelId = table.Column<string>(nullable: true),
                    CreationDateTime = table.Column<DateTime>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleExpenses_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channels_UserSecretLogin",
                table: "Channels",
                column: "UserSecretLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_UserSecretLogin1",
                table: "Channels",
                column: "UserSecretLogin1");

            migrationBuilder.CreateIndex(
                name: "IX_RegularExpensesCategories_UserSecretLogin",
                table: "RegularExpensesCategories",
                column: "UserSecretLogin");

            migrationBuilder.CreateIndex(
                name: "IX_RegularExpensesCategories_UserSecretLogin1",
                table: "RegularExpensesCategories",
                column: "UserSecretLogin1");

            migrationBuilder.CreateIndex(
                name: "IX_SingleExpenses_ChannelId",
                table: "SingleExpenses",
                column: "ChannelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegularExpensesCategories");

            migrationBuilder.DropTable(
                name: "SingleExpenses");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
