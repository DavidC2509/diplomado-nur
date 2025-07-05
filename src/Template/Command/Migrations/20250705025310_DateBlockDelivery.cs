﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Template.Command.Migrations
{
    /// <inheritdoc />
    public partial class DateBlockDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateFromDeliveryIgnore",
                table: "Address",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateToDeliveryIgnore",
                table: "Address",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFromDeliveryIgnore",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "DateToDeliveryIgnore",
                table: "Address");
        }
    }
}