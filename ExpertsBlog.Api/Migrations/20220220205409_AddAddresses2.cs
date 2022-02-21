using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpertsBlog.Api.Migrations
{
    public partial class AddAddresses2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_BlogPosts_BlogPostId",
                table: "Address");

            migrationBuilder.AlterColumn<int>(
                name: "BlogPostId",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_BlogPosts_BlogPostId",
                table: "Address",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_BlogPosts_BlogPostId",
                table: "Address");

            migrationBuilder.AlterColumn<int>(
                name: "BlogPostId",
                table: "Address",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_BlogPosts_BlogPostId",
                table: "Address",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id");
        }
    }
}
