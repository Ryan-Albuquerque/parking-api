using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking.Migrations
{
    /// <inheritdoc />
    public partial class AddFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GetOutUserId",
                table: "Events",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GetInUserId",
                table: "Events",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Events_GetInUserId",
                table: "Events",
                column: "GetInUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_GetOutUserId",
                table: "Events",
                column: "GetOutUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ParkId",
                table: "Events",
                column: "ParkId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParkId",
                table: "AspNetUsers",
                column: "ParkId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Parks_ParkId",
                table: "AspNetUsers",
                column: "ParkId",
                principalTable: "Parks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_GetInUserId",
                table: "Events",
                column: "GetInUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_GetOutUserId",
                table: "Events",
                column: "GetOutUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Parks_ParkId",
                table: "Events",
                column: "ParkId",
                principalTable: "Parks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Parks_ParkId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_GetInUserId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_GetOutUserId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Parks_ParkId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_GetInUserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_GetOutUserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ParkId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ParkId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "GetOutUserId",
                table: "Events",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GetInUserId",
                table: "Events",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
