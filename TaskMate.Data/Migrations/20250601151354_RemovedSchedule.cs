using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskMate.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Schedules_ScheduleId",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Devices_ScheduleId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Devices");

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_DeviceId",
                table: "Task",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Devices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    MACAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ScheduleId",
                table: "Devices",
                column: "ScheduleId",
                unique: true,
                filter: "[ScheduleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ApplicationUserId",
                table: "Schedules",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_UserId",
                table: "Schedules",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Schedules_ScheduleId",
                table: "Devices",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
