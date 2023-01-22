using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telomeres.Migrations
{
    public partial class repositories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RepoDownloadFilename",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepoUploadedFilename",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepoDownloadFilename",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "RepoUploadedFilename",
                table: "Reports");
        }
    }
}
