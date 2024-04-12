using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FACILITIES",
                columns: table => new
                {
                    FACILITY_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FACILITY_NAME = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACILITIES", x => x.FACILITY_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PROJECTS_BT",
                columns: table => new
                {
                    PROJECT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PROJECT_TITLE = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PROJECT_DESCRIPTION = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PROJECT_STATE = table.Column<int>(type: "int", nullable: false),
                    FACILITY_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECTS_BT", x => x.PROJECT_ID);
                    table.ForeignKey(
                        name: "FK_PROJECTS_BT_FACILITIES_FACILITY_ID",
                        column: x => x.FACILITY_ID,
                        principalTable: "FACILITIES",
                        principalColumn: "FACILITY_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MANAGEMENT_PROJECTS",
                columns: table => new
                {
                    PROJECT_ID = table.Column<int>(type: "int", nullable: false),
                    LAW_TYPE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MANAGEMENT_PROJECTS", x => x.PROJECT_ID);
                    table.ForeignKey(
                        name: "FK_MANAGEMENT_PROJECTS_PROJECTS_BT_PROJECT_ID",
                        column: x => x.PROJECT_ID,
                        principalTable: "PROJECTS_BT",
                        principalColumn: "PROJECT_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "REQUEST_FUNDING_PROJECTS",
                columns: table => new
                {
                    PROJECT_ID = table.Column<int>(type: "int", nullable: false),
                    IS_FWF_FUNDED = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IS_FFG_FUNDED = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REQUEST_FUNDING_PROJECTS", x => x.PROJECT_ID);
                    table.ForeignKey(
                        name: "FK_REQUEST_FUNDING_PROJECTS_PROJECTS_BT_PROJECT_ID",
                        column: x => x.PROJECT_ID,
                        principalTable: "PROJECTS_BT",
                        principalColumn: "PROJECT_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PROJECTS_BT_FACILITY_ID",
                table: "PROJECTS_BT",
                column: "FACILITY_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MANAGEMENT_PROJECTS");

            migrationBuilder.DropTable(
                name: "REQUEST_FUNDING_PROJECTS");

            migrationBuilder.DropTable(
                name: "PROJECTS_BT");

            migrationBuilder.DropTable(
                name: "FACILITIES");
        }
    }
}
