using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "branches",
                columns: table => new
                {
                    branch_id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branch_name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    branch_manager = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    branch_number = table.Column<long>(type: "BIGINT", nullable: false),
                    branch_location = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    is_active = table.Column<bool>(type: "BIT", nullable: false),
                    created_date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    modified_date = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_branch_id", x => x.branch_id);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    is_active = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    last_name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone_number = table.Column<long>(type: "BIGINT", nullable: false),
                    password = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    email = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    RoleId = table.Column<int>(type: "INT", nullable: false),
                    is_active = table.Column<bool>(type: "BIT", nullable: false),
                    created_date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    modified_date = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_user_user_roles",
                        column: x => x.RoleId,
                        principalTable: "user_role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "user_role",
                columns: new[] { "role_id", "is_active", "role_name" },
                values: new object[] { 1, true, "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "user_role",
                columns: new[] { "role_id", "is_active", "role_name" },
                values: new object[] { 2, true, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "branches");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "user_role");
        }
    }
}
