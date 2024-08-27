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
                    func_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    url = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFunction", x => x.func_id);
                });

            migrationBuilder.CreateTable(
                name: "TFunctionGroup",
                columns: table => new
                {
                    func_group_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFunctionGroup", x => x.func_group_id);
                });

            migrationBuilder.CreateTable(
                name: "TUser",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    mima = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    mima_change_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_enable = table.Column<bool>(type: "boolean", nullable: false),
                    last_login_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUser", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "TFuncGroupRel",
                columns: table => new
                {
                    func_group_id = table.Column<int>(type: "integer", nullable: false),
                    func_id = table.Column<int>(type: "integer", nullable: false),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFuncGroupRel", x => new { x.func_id, x.func_group_id });
                    table.ForeignKey(
                        name: "FK_TFuncGroupRel_TFunctionGroup_func_group_id",
                        column: x => x.func_group_id,
                        principalTable: "TFunctionGroup",
                        principalColumn: "func_group_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TFuncGroupRel_TFunction_func_id",
                        column: x => x.func_id,
                        principalTable: "TFunction",
                        principalColumn: "func_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TUserFuncGroupRel",
                columns: table => new
                {
                    func_group_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUserFuncGroupRel", x => new { x.user_id, x.func_group_id });
                    table.ForeignKey(
                        name: "FK_TUserFuncGroupRel_TFunctionGroup_user_id",
                        column: x => x.user_id,
                        principalTable: "TFunctionGroup",
                        principalColumn: "func_group_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TUserFuncGroupRel_TUser_user_id",
                        column: x => x.user_id,
                        principalTable: "TUser",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TFuncGroupRel_func_group_id",
                table: "TFuncGroupRel",
                column: "func_group_id");
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
