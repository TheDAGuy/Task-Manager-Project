using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mini_Project.Migrations
{
    /// <inheritdoc />
    public partial class Addingemployeelevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Email", "FirstName", "LastName", "Level" },
                values: new object[,]
                {
                    { 1, "JohnDoe1824@gmail.com", "John", "Doe", 1 },
                    { 2, "TitanSlayer@gmail.com", "Levy", "Ackerman", 1 },
                    { 3, "MysticBoy@gmail.com", "Eren", "Yaeager", 1 },
                    { 4, "Puppylover@gmail.com", "John", "Wick", 1 },
                    { 5, "Drake4eva@gmail.com", "Pe", "Doe", 0 }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "Description", "DueDate", "EmployeeId", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "Make a 3D printed Model of a Giraffe", new DateOnly(2024, 8, 16), 1, "Make a giraffe", 0 },
                    { 2, "Make a 3D printed Model of a Dolphin", new DateOnly(2024, 7, 9), 2, "Make a dolphin", 0 },
                    { 3, "Make a 3D printed Model of a Lion", new DateOnly(2024, 8, 6), 3, "Make a lion", 0 },
                    { 4, "Make a 3D printed Model of a Car", new DateOnly(2024, 10, 26), 4, "Make a car", 0 },
                    { 5, "Make a 3D printed Model of a House", new DateOnly(2024, 9, 24), 5, "Make a house", 0 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "Content", "EmployeeId", "TaskId", "Timestamp" },
                values: new object[,]
                {
                    { 1, "Started with the task", 1, 1, new DateTime(2024, 8, 20, 13, 32, 47, 803, DateTimeKind.Local).AddTicks(4652) },
                    { 2, "In progress", 1, 1, new DateTime(2024, 8, 20, 13, 32, 47, 804, DateTimeKind.Local).AddTicks(7086) },
                    { 3, "Completed", 3, 3, new DateTime(2024, 8, 20, 13, 32, 47, 804, DateTimeKind.Local).AddTicks(7102) },
                    { 4, "Reviewed", 4, 4, new DateTime(2024, 8, 20, 13, 32, 47, 804, DateTimeKind.Local).AddTicks(7104) },
                    { 5, "Started with the task", 5, 5, new DateTime(2024, 8, 20, 13, 32, 47, 804, DateTimeKind.Local).AddTicks(7106) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EmployeeId",
                table: "Comments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TaskId",
                table: "Comments",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EmployeeId",
                table: "Tasks",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
