using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace boilerplate.api.data.Migrations
{
    public partial class Updated_join_tables_code_first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlatformContacts_ContactId",
                schema: "dbo",
                table: "PlatformContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformContacts_PlatformId",
                schema: "dbo",
                table: "PlatformContacts",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicLabelContacts_ContactId",
                schema: "dbo",
                table: "MusicLabelContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicLabelContacts_MusicLabelId",
                schema: "dbo",
                table: "MusicLabelContacts",
                column: "MusicLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianContacts_ContactId",
                schema: "dbo",
                table: "MusicianContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianContacts_MusicianId",
                schema: "dbo",
                table: "MusicianContacts",
                column: "MusicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianContacts_Contacts_ContactId",
                schema: "dbo",
                table: "MusicianContacts",
                column: "ContactId",
                principalSchema: "dbo",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianContacts_Musicians_MusicianId",
                schema: "dbo",
                table: "MusicianContacts",
                column: "MusicianId",
                principalSchema: "dbo",
                principalTable: "Musicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicLabelContacts_Contacts_ContactId",
                schema: "dbo",
                table: "MusicLabelContacts",
                column: "ContactId",
                principalSchema: "dbo",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicLabelContacts_MusicLabels_MusicLabelId",
                schema: "dbo",
                table: "MusicLabelContacts",
                column: "MusicLabelId",
                principalSchema: "dbo",
                principalTable: "MusicLabels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformContacts_Contacts_ContactId",
                schema: "dbo",
                table: "PlatformContacts",
                column: "ContactId",
                principalSchema: "dbo",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformContacts_Platforms_PlatformId",
                schema: "dbo",
                table: "PlatformContacts",
                column: "PlatformId",
                principalSchema: "dbo",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicianContacts_Contacts_ContactId",
                schema: "dbo",
                table: "MusicianContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianContacts_Musicians_MusicianId",
                schema: "dbo",
                table: "MusicianContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicLabelContacts_Contacts_ContactId",
                schema: "dbo",
                table: "MusicLabelContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicLabelContacts_MusicLabels_MusicLabelId",
                schema: "dbo",
                table: "MusicLabelContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_PlatformContacts_Contacts_ContactId",
                schema: "dbo",
                table: "PlatformContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_PlatformContacts_Platforms_PlatformId",
                schema: "dbo",
                table: "PlatformContacts");

            migrationBuilder.DropIndex(
                name: "IX_PlatformContacts_ContactId",
                schema: "dbo",
                table: "PlatformContacts");

            migrationBuilder.DropIndex(
                name: "IX_PlatformContacts_PlatformId",
                schema: "dbo",
                table: "PlatformContacts");

            migrationBuilder.DropIndex(
                name: "IX_MusicLabelContacts_ContactId",
                schema: "dbo",
                table: "MusicLabelContacts");

            migrationBuilder.DropIndex(
                name: "IX_MusicLabelContacts_MusicLabelId",
                schema: "dbo",
                table: "MusicLabelContacts");

            migrationBuilder.DropIndex(
                name: "IX_MusicianContacts_ContactId",
                schema: "dbo",
                table: "MusicianContacts");

            migrationBuilder.DropIndex(
                name: "IX_MusicianContacts_MusicianId",
                schema: "dbo",
                table: "MusicianContacts");
        }
    }
}
