using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Command.Migrations
{
    /// <inheritdoc />
    public partial class OutboxTraceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TraceId",
                table: "OutboxMessage",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TraceId",
                table: "OutboxMessage");
        }
    }
}
