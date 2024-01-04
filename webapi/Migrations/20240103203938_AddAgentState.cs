using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CallCenterApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAgentState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgentState",
                table: "AgentInfos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentState",
                table: "AgentInfos");
        }
    }
}
