using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecureOps.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubjectRoleToIncidentParticipation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectRoles_Incidents_IncidentId",
                table: "SubjectRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectRoles_InvolvementTypes_InvolvementTypeId",
                table: "SubjectRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectRoles_Persons_PersonId",
                table: "SubjectRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectRoles",
                table: "SubjectRoles");

            migrationBuilder.RenameTable(
                name: "SubjectRoles",
                newName: "IncidentParticipants");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectRoles_PersonId",
                table: "IncidentParticipants",
                newName: "IX_IncidentParticipants_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectRoles_InvolvementTypeId",
                table: "IncidentParticipants",
                newName: "IX_IncidentParticipants_InvolvementTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncidentParticipants",
                table: "IncidentParticipants",
                columns: new[] { "IncidentId", "PersonId", "InvolvementTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_IncidentParticipants_Incidents_IncidentId",
                table: "IncidentParticipants",
                column: "IncidentId",
                principalTable: "Incidents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncidentParticipants_InvolvementTypes_InvolvementTypeId",
                table: "IncidentParticipants",
                column: "InvolvementTypeId",
                principalTable: "InvolvementTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncidentParticipants_Persons_PersonId",
                table: "IncidentParticipants",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncidentParticipants_Incidents_IncidentId",
                table: "IncidentParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_IncidentParticipants_InvolvementTypes_InvolvementTypeId",
                table: "IncidentParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_IncidentParticipants_Persons_PersonId",
                table: "IncidentParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncidentParticipants",
                table: "IncidentParticipants");

            migrationBuilder.RenameTable(
                name: "IncidentParticipants",
                newName: "SubjectRoles");

            migrationBuilder.RenameIndex(
                name: "IX_IncidentParticipants_PersonId",
                table: "SubjectRoles",
                newName: "IX_SubjectRoles_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_IncidentParticipants_InvolvementTypeId",
                table: "SubjectRoles",
                newName: "IX_SubjectRoles_InvolvementTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectRoles",
                table: "SubjectRoles",
                columns: new[] { "IncidentId", "PersonId", "InvolvementTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectRoles_Incidents_IncidentId",
                table: "SubjectRoles",
                column: "IncidentId",
                principalTable: "Incidents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectRoles_InvolvementTypes_InvolvementTypeId",
                table: "SubjectRoles",
                column: "InvolvementTypeId",
                principalTable: "InvolvementTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectRoles_Persons_PersonId",
                table: "SubjectRoles",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
