using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ms.infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TFunction",
                columns: table => new
                {
                    FuncID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFunction", x => x.FuncID);
                });

            migrationBuilder.CreateTable(
                name: "TFunctionGroup",
                columns: table => new
                {
                    FuncGroupID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFunctionGroup", x => x.FuncGroupID);
                });

            migrationBuilder.CreateTable(
                name: "TUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Mima = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsEnable = table.Column<bool>(type: "boolean", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUser", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "TFuncGroupRel",
                columns: table => new
                {
                    FuncGroupID = table.Column<int>(type: "integer", nullable: false),
                    FuncID = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFuncGroupRel", x => new { x.FuncID, x.FuncGroupID });
                    table.ForeignKey(
                        name: "FK_TFuncGroupRel_TFunctionGroup_FuncGroupID",
                        column: x => x.FuncGroupID,
                        principalTable: "TFunctionGroup",
                        principalColumn: "FuncGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TFuncGroupRel_TFunction_FuncID",
                        column: x => x.FuncID,
                        principalTable: "TFunction",
                        principalColumn: "FuncID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TUserFuncGroupRel",
                columns: table => new
                {
                    FuncGroupID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUserFuncGroupRel", x => new { x.UserID, x.FuncGroupID });
                    table.ForeignKey(
                        name: "FK_TUserFuncGroupRel_TFunctionGroup_UserID",
                        column: x => x.UserID,
                        principalTable: "TFunctionGroup",
                        principalColumn: "FuncGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TUserFuncGroupRel_TUser_UserID",
                        column: x => x.UserID,
                        principalTable: "TUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TFuncGroupRel_FuncGroupID",
                table: "TFuncGroupRel",
                column: "FuncGroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TFuncGroupRel");

            migrationBuilder.DropTable(
                name: "TUserFuncGroupRel");

            migrationBuilder.DropTable(
                name: "TFunction");

            migrationBuilder.DropTable(
                name: "TFunctionGroup");

            migrationBuilder.DropTable(
                name: "TUser");
        }
    }
}
