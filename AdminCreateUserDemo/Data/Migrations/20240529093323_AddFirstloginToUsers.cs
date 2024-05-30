using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminCreateUserDemo.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstloginToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FirstLogin",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstLogin",
                table: "AspNetUsers");
        }
    }
}
