using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatterBox.Migrations
{
    /// <inheritdoc />
    public partial class removedroleaddedairesponseanduserpromptinsteadintoonesingleentitytoreduceredundancy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "ChatMessages",
                newName: "UserPrompt");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "ChatMessages",
                newName: "AiResponse");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserPrompt",
                table: "ChatMessages",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "AiResponse",
                table: "ChatMessages",
                newName: "Message");
        }
    }
}
