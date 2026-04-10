using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    User_Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "DailyLogs",
                columns: table => new
                {
                    Log_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    User_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Log_Date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(5, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyLogs", x => x.Log_ID);
                    table.ForeignKey(
                        name: "FK_DailyLogs_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Goal_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    User_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Goal_Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Target_Value = table.Column<decimal>(type: "decimal(6, 2)", nullable: true),
                    Current_Value = table.Column<decimal>(type: "decimal(6, 2)", nullable: true),
                    Start_Date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Target_Date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Achieved = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Goal_ID);
                    table.ForeignKey(
                        name: "FK_Goals_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthMetrics",
                columns: table => new
                {
                    Metric_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    User_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Metric_Date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Heart_Rate = table.Column<int>(type: "INTEGER", nullable: true),
                    Blood_Sugar = table.Column<int>(type: "INTEGER", nullable: true),
                    Blood_Pressure = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Total_Water = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthMetrics", x => x.Metric_ID);
                    table.ForeignKey(
                        name: "FK_HealthMetrics_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Profile_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    User_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    Date_of_Birth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Activity_Level = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Medical_Condition = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Target_Weight = table.Column<decimal>(type: "decimal(5, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Profile_ID);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseLogs",
                columns: table => new
                {
                    Exercise_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Log_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Exercise_Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Exercise_Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: true),
                    Calories_Burned = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseLogs", x => x.Exercise_ID);
                    table.ForeignKey(
                        name: "FK_ExerciseLogs_DailyLogs_Log_ID",
                        column: x => x.Log_ID,
                        principalTable: "DailyLogs",
                        principalColumn: "Log_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Meal_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Log_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Meal_Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Meal_Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Meal_ID);
                    table.ForeignKey(
                        name: "FK_Meals_DailyLogs_Log_ID",
                        column: x => x.Log_ID,
                        principalTable: "DailyLogs",
                        principalColumn: "Log_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealItems",
                columns: table => new
                {
                    Meal_Item_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Meal_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Food_Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    Calories = table.Column<int>(type: "INTEGER", nullable: true),
                    Protein = table.Column<int>(type: "INTEGER", nullable: true),
                    Fat = table.Column<int>(type: "INTEGER", nullable: true),
                    Carbs = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealItems", x => x.Meal_Item_ID);
                    table.ForeignKey(
                        name: "FK_MealItems_Meals_Meal_ID",
                        column: x => x.Meal_ID,
                        principalTable: "Meals",
                        principalColumn: "Meal_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_ID", "Email", "Password", "User_Name" },
                values: new object[,]
                {
                    { 6, "ahmed@mail.com", "pwd123", "Ahmed" },
                    { 7, "mona@mail.com", "m0n@!", "Mona" },
                    { 8, "omar@mail.com", "omr99", "Omar" },
                    { 9, "laila@mail.com", "lolo22", "Laila" },
                    { 10, "tarek@mail.com", "tk_88", "Tarek" },
                    { 11, "salma@mail.com", "s@lm@1", "Salma" },
                    { 12, "youssef@mail.com", "jo1234", "Youssef" },
                    { 13, "hoda@mail.com", "hoda77", "Hoda" },
                    { 14, "mahmoud@mail.com", "mah_00", "Mahmoud" },
                    { 15, "farah@mail.com", "farah#", "Farah" },
                    { 16, "khaled@mail.com", "kh_991", "Khaled" },
                    { 17, "rania@mail.com", "r@nia", "Rania" },
                    { 18, "ziad@mail.com", "zizou!", "Ziad" },
                    { 19, "dina@mail.com", "dina_d", "Dina" },
                    { 20, "ali@mail.com", "ali2026", "Ali" },
                    { 21, "nada@mail.com", "nada*8", "Nada" },
                    { 22, "rami@mail.com", "rami_r", "Rami" },
                    { 23, "yasmine2@mail.com", "yas22", "Yasmine" },
                    { 24, "kareem@mail.com", "kimo88", "Kareem" },
                    { 25, "merna@mail.com", "merna1", "Merna" }
                });

            migrationBuilder.InsertData(
                table: "DailyLogs",
                columns: new[] { "Log_ID", "Log_Date", "User_ID", "Weight" },
                values: new object[,]
                {
                    { 6, new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 94.50m },
                    { 7, new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 55.20m },
                    { 8, new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 84.00m },
                    { 9, new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 69.80m },
                    { 10, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 104.50m },
                    { 11, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 63.50m },
                    { 12, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 75.50m },
                    { 13, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 50.50m },
                    { 14, new DateTime(2026, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, 89.50m },
                    { 15, new DateTime(2026, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 67.50m },
                    { 16, new DateTime(2026, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 87.50m },
                    { 17, new DateTime(2026, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, 71.50m },
                    { 18, new DateTime(2026, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, 78.50m },
                    { 19, new DateTime(2026, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, 60.00m },
                    { 20, new DateTime(2026, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, 109.00m },
                    { 21, new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, 65.50m },
                    { 22, new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, 81.50m },
                    { 23, new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, 62.80m },
                    { 24, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 77.20m },
                    { 25, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 57.80m }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Goal_ID", "Achieved", "Current_Value", "Goal_Type", "Start_Date", "Target_Date", "Target_Value", "User_ID" },
                values: new object[,]
                {
                    { 6, false, 94.5m, "Weight Loss", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 80.00m, 6 },
                    { 7, false, 55.2m, "Muscle Gain", new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 58.00m, 7 },
                    { 8, false, 84.0m, "Weight Loss", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 75.00m, 8 },
                    { 9, false, 69.8m, "Weight Loss", new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.00m, 9 },
                    { 10, false, 104.5m, "Weight Loss", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 90.00m, 10 },
                    { 11, false, 63.5m, "Weight Loss", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 62.00m, 11 },
                    { 12, false, 75.5m, "Muscle Gain", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 82.00m, 12 },
                    { 13, false, 50.5m, "Weight Gain", new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 55.00m, 13 },
                    { 14, false, 89.5m, "Weight Loss", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 80.00m, 14 },
                    { 15, false, 67.5m, "Weight Loss", new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.00m, 15 },
                    { 16, false, 87.5m, "Weight Loss", new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 85.00m, 16 },
                    { 17, false, 71.5m, "Weight Loss", new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.00m, 17 },
                    { 18, false, 78.5m, "Weight Loss", new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 75.00m, 18 },
                    { 19, true, 60.0m, "Maintain Weight", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.00m, 19 },
                    { 20, false, 109.0m, "Weight Loss", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 95.00m, 20 },
                    { 21, false, 65.5m, "Weight Loss", new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.00m, 21 },
                    { 22, false, 81.5m, "Weight Loss", new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 78.00m, 22 },
                    { 23, false, 62.8m, "Weight Loss", new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.00m, 23 },
                    { 24, false, 77.2m, "Muscle Gain", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85.00m, 24 },
                    { 25, false, 57.8m, "Weight Loss", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 55.00m, 25 }
                });

            migrationBuilder.InsertData(
                table: "HealthMetrics",
                columns: new[] { "Metric_ID", "Blood_Pressure", "Blood_Sugar", "Heart_Rate", "Metric_Date", "Total_Water", "User_ID" },
                values: new object[,]
                {
                    { 6, "140/90", 110, 85, new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500, 6 },
                    { 7, "110/70", 90, 65, new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3500, 7 },
                    { 8, "130/85", 140, 78, new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000, 8 },
                    { 9, "118/75", 95, 82, new DateTime(2026, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800, 9 },
                    { 10, "145/95", 105, 88, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1200, 10 },
                    { 11, "115/75", 88, 70, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500, 11 },
                    { 12, "120/80", 92, 60, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 4000, 12 },
                    { 13, "100/65", 85, 90, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1600, 13 },
                    { 14, "125/82", 98, 75, new DateTime(2026, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2200, 14 },
                    { 15, "112/72", 90, 77, new DateTime(2026, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1900, 15 },
                    { 16, "122/80", 95, 68, new DateTime(2026, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2800, 16 },
                    { 17, "119/78", 89, 80, new DateTime(2026, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1700, 17 },
                    { 18, "128/84", 96, 85, new DateTime(2026, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2100, 18 },
                    { 19, "115/75", 88, 72, new DateTime(2026, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2300, 19 },
                    { 20, "135/88", 102, 92, new DateTime(2026, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1400, 20 },
                    { 21, "118/76", 91, 76, new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000, 21 },
                    { 22, "120/79", 94, 66, new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2600, 22 },
                    { 23, "114/74", 87, 74, new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800, 23 },
                    { 24, "115/75", 93, 62, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3000, 24 },
                    { 25, "116/77", 89, 79, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1900, 25 }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Profile_ID", "Activity_Level", "Date_of_Birth", "Gender", "Height", "Medical_Condition", "Target_Weight", "User_ID", "Weight" },
                values: new object[,]
                {
                    { 6, "Sedentary", new DateTime(1990, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", 180.00m, "Hypertension", 80.00m, 6, 95.00m },
                    { 7, "Very Active", new DateTime(1995, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", 165.00m, null, 58.00m, 7, 55.00m },
                    { 8, "Light", new DateTime(1988, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", 172.00m, "Type 2 Diabetes", 75.00m, 8, 85.00m },
                    { 9, "Moderate", new DateTime(2000, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", 155.00m, "PCOS", 60.00m, 9, 70.00m },
                    { 10, "Low", new DateTime(1985, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", 185.00m, "High Cholesterol", 90.00m, 10, 105.00m },
                    { 11, "Active", new DateTime(1998, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", 168.00m, null, 62.00m, 11, 64.00m },
                    { 12, "Very Active", new DateTime(2001, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", 178.00m, null, 82.00m, 12, 75.00m },
                    { 13, "Light", new DateTime(1992, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", 160.00m, "Anemia", 55.00m, 13, 50.00m },
                    { 14, "Moderate", new DateTime(1980, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", 170.00m, null, 80.00m, 14, 90.00m },
                    { 15, "Low", new DateTime(1996, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", 164.00m, "Thyroid Issue", 60.00m, 15, 68.00m },
                    { 16, "Active", new DateTime(1993, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", 182.00m, null, 85.00m, 16, 88.00m },
                    { 17, "Sedentary", new DateTime(1987, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", 158.00m, null, 60.00m, 17, 72.00m },
                    { 18, "Moderate", new DateTime(1999, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", 176.00m, "Asthma", 75.00m, 18, 79.00m },
                    { 19, "Active", new DateTime(2002, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", 166.00m, null, 60.00m, 19, 60.00m },
                    { 20, "Low", new DateTime(1982, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", 188.00m, "Joint Pain", 95.00m, 20, 110.00m },
                    { 21, "Moderate", new DateTime(1997, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", 162.00m, null, 60.00m, 21, 66.00m },
                    { 22, "Active", new DateTime(1994, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", 174.00m, null, 78.00m, 22, 82.00m },
                    { 23, "Light", new DateTime(1989, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", 167.00m, null, 60.00m, 23, 63.00m },
                    { 24, "Very Active", new DateTime(2000, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", 180.00m, null, 85.00m, 24, 77.00m },
                    { 25, "Moderate", new DateTime(1991, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", 159.00m, null, 55.00m, 25, 58.00m }
                });

            migrationBuilder.InsertData(
                table: "ExerciseLogs",
                columns: new[] { "Exercise_ID", "Calories_Burned", "Duration", "Exercise_Name", "Exercise_Type", "Log_ID" },
                values: new object[,]
                {
                    { 6, 200, 45, "Walking", "Cardio", 6 },
                    { 7, 550, 60, "CrossFit", "Strength", 7 },
                    { 8, 220, 30, "Stationary Bike", "Cardio", 8 },
                    { 9, 150, 45, "Pilates", "Flexibility", 9 },
                    { 10, 300, 45, "Water Aerobics", "Cardio", 10 },
                    { 11, 450, 50, "Kickboxing", "Cardio", 11 },
                    { 12, 600, 90, "Powerlifting", "Strength", 12 },
                    { 13, 80, 20, "Stretching", "Flexibility", 13 },
                    { 14, 350, 30, "Rowing", "Cardio", 14 },
                    { 15, 400, 60, "Zumba", "Cardio", 15 },
                    { 16, 500, 60, "Basketball", "Sports", 16 },
                    { 17, 120, 45, "Yoga", "Flexibility", 17 },
                    { 18, 380, 40, "Jogging", "Cardio", 18 },
                    { 19, 200, 30, "Dancing", "Cardio", 19 },
                    { 20, 100, 30, "Physical Therapy", "Recovery", 20 },
                    { 21, 200, 15, "Jump Rope", "Cardio", 21 },
                    { 22, 350, 45, "Calisthenics", "Strength", 22 },
                    { 23, 250, 30, "Elliptical", "Cardio", 23 },
                    { 24, 300, 20, "Sprinting", "HIIT", 24 },
                    { 25, 110, 40, "Tai Chi", "Flexibility", 25 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Meal_ID", "Log_ID", "Meal_Name", "Meal_Type" },
                values: new object[,]
                {
                    { 6, 6, "Lunch", "Main" },
                    { 7, 7, "Post-Workout", "Snack" },
                    { 8, 8, "Dinner", "Main" },
                    { 9, 9, "Breakfast", "Main" },
                    { 10, 10, "Lunch", "Main" },
                    { 11, 11, "Dinner", "Main" },
                    { 12, 12, "Pre-Workout", "Snack" },
                    { 13, 13, "Breakfast", "Main" },
                    { 14, 14, "Lunch", "Main" },
                    { 15, 15, "Snack", "Snack" },
                    { 16, 16, "Dinner", "Main" },
                    { 17, 17, "Breakfast", "Main" },
                    { 18, 18, "Lunch", "Main" },
                    { 19, 19, "Dinner", "Main" },
                    { 20, 20, "Lunch", "Main" },
                    { 21, 21, "Breakfast", "Main" },
                    { 22, 22, "Post-Workout", "Snack" },
                    { 23, 23, "Dinner", "Main" },
                    { 24, 24, "Lunch", "Main" },
                    { 25, 25, "Snack", "Snack" }
                });

            migrationBuilder.InsertData(
                table: "MealItems",
                columns: new[] { "Meal_Item_ID", "Calories", "Carbs", "Fat", "Food_Name", "Meal_ID", "Protein", "Quantity" },
                values: new object[,]
                {
                    { 6, 350, 0, 20, "Grilled Salmon", 6, 40, 1 },
                    { 7, 180, 5, 2, "Protein Shake", 7, 30, 1 },
                    { 8, 450, 0, 30, "Steak", 8, 45, 1 },
                    { 9, 100, 15, 0, "Greek Yogurt", 9, 10, 1 },
                    { 10, 600, 5, 40, "Burger (No Bun)", 10, 50, 2 },
                    { 11, 300, 45, 8, "Quinoa Bowl", 11, 12, 1 },
                    { 12, 210, 54, 0, "Banana", 12, 2, 2 },
                    { 13, 200, 3, 12, "Spinach Omelette", 13, 15, 1 },
                    { 14, 220, 45, 2, "Brown Rice", 14, 5, 200 },
                    { 15, 160, 6, 14, "Almonds", 15, 6, 30 },
                    { 16, 165, 0, 4, "Chicken Breast", 16, 31, 1 },
                    { 17, 250, 20, 15, "Avocado Toast", 17, 5, 1 },
                    { 18, 300, 60, 2, "Pasta", 18, 10, 150 },
                    { 19, 220, 15, 12, "Tofu Stir Fry", 19, 18, 1 },
                    { 20, 500, 20, 35, "Fried Chicken", 20, 25, 2 },
                    { 21, 350, 50, 10, "Pancakes", 21, 8, 2 },
                    { 22, 120, 6, 2, "Cottage Cheese", 22, 14, 1 },
                    { 23, 180, 30, 1, "Lentil Soup", 23, 10, 1 },
                    { 24, 110, 26, 0, "Sweet Potato", 24, 2, 1 },
                    { 25, 110, 12, 7, "Dark Chocolate", 25, 1, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyLogs_User_ID",
                table: "DailyLogs",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseLogs_Log_ID",
                table: "ExerciseLogs",
                column: "Log_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_User_ID",
                table: "Goals",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_HealthMetrics_User_ID",
                table: "HealthMetrics",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MealItems_Meal_ID",
                table: "MealItems",
                column: "Meal_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_Log_ID",
                table: "Meals",
                column: "Log_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_User_ID",
                table: "UserProfiles",
                column: "User_ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseLogs");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "HealthMetrics");

            migrationBuilder.DropTable(
                name: "MealItems");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "DailyLogs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
