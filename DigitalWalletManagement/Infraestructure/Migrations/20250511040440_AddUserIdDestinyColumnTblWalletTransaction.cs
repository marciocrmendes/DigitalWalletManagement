using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalWalletManagement.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdDestinyColumnTblWalletTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WalletTransactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserIdDestiny",
                table: "WalletTransactions",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "WalletTransactions");

            migrationBuilder.DropColumn(
                name: "UserIdDestiny",
                table: "WalletTransactions");
        }
    }
}
