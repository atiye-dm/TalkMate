using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalkMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial_talkMatrDB_040903_part1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResponseText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatResponses_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { new Guid("d0fe9715-2043-4d9d-9949-a7e485ace584"), new DateTime(2025, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Demo User" });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "CreatedAt", "Text", "UserId" },
                values: new object[] { new Guid("2227d6a7-e0fe-45ee-8a36-e6c0b42bfe97"), new DateTime(2025, 1, 1, 12, 5, 0, 0, DateTimeKind.Utc), "Feeling a bit stressed about work lately.", new Guid("d0fe9715-2043-4d9d-9949-a7e485ace584") });

            migrationBuilder.InsertData(
                table: "ChatResponses",
                columns: new[] { "Id", "CreatedAt", "MessageId", "ResponseText" },
                values: new object[] { new Guid("6c95a607-5c8d-4391-8b64-4f9a2e70dd3a"), new DateTime(2025, 1, 1, 12, 5, 10, 0, DateTimeKind.Utc), new Guid("2227d6a7-e0fe-45ee-8a36-e6c0b42bfe97"), "It seems you're stressed. Take a deep breath 🌿" });

            migrationBuilder.CreateIndex(
                name: "IX_ChatResponses_MessageId",
                table: "ChatResponses",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatResponses");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
