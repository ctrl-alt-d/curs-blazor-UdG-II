using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Datalayer.Migrations
{
    public partial class canvisAlModelBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DiaDePublicacio",
                table: "Blogs",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaDePublicacio",
                table: "Blogs");
        }
    }
}
