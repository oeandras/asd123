using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Asd13.Repository.EF.Migrations
{
    public partial class AddRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedBy",
                table: "ImageInfos");

            migrationBuilder.AddColumn<Guid>(
                name: "UploadedById",
                table: "ImageInfos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageInfos_UploadedById",
                table: "ImageInfos",
                column: "UploadedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageInfos_Users_UploadedById",
                table: "ImageInfos",
                column: "UploadedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageInfos_Users_UploadedById",
                table: "ImageInfos");

            migrationBuilder.DropIndex(
                name: "IX_ImageInfos_UploadedById",
                table: "ImageInfos");

            migrationBuilder.DropColumn(
                name: "UploadedById",
                table: "ImageInfos");

            migrationBuilder.AddColumn<string>(
                name: "UploadedBy",
                table: "ImageInfos",
                nullable: true);
        }
    }
}
