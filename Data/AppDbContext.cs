using Microsoft.EntityFrameworkCore;
using FitnessTracker.Models;
using System;
using System.IO;

namespace FitnessTracker.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<DailyLog> DailyLogs { get; set; }
        public DbSet<ExerciseLog> ExerciseLogs { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealItem> MealItems { get; set; }
        public DbSet<HealthMetric> HealthMetrics { get; set; }
        public DbSet<Goal> Goals { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>Runtime context for the WPF app (applies pending migrations).</summary>
        public AppDbContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Use a stable path in the application directory
                string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HealthTracker.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProfile>()
                .HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<UserProfile>(p => p.User_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.DailyLogs)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.User_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.HealthMetrics)
                .WithOne(h => h.User)
                .HasForeignKey(h => h.User_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Goals)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.User_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DailyLog>()
                .HasMany(d => d.Meals)
                .WithOne(m => m.DailyLog)
                .HasForeignKey(m => m.Log_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DailyLog>()
                .HasMany(d => d.Exercises)
                .WithOne(e => e.DailyLog)
                .HasForeignKey(e => e.Log_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Meal>()
                .HasMany(m => m.MealItems)
                .WithOne(i => i.Meal)
                .HasForeignKey(i => i.Meal_ID)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Users (matches SQL INSERT; IsAdmin is app-only — grant via Manage Users)
            modelBuilder.Entity<User>().HasData(
                new User { User_ID = 6, User_Name = "Ahmed", Email = "ahmed@mail.com", Password = "pwd123", IsAdmin = false },
                new User { User_ID = 7, User_Name = "Mona", Email = "mona@mail.com", Password = "m0n@!", IsAdmin = false },
                new User { User_ID = 8, User_Name = "Omar", Email = "omar@mail.com", Password = "omr99", IsAdmin = false },
                new User { User_ID = 9, User_Name = "Laila", Email = "laila@mail.com", Password = "lolo22", IsAdmin = false },
                new User { User_ID = 10, User_Name = "Tarek", Email = "tarek@mail.com", Password = "tk_88", IsAdmin = false },
                new User { User_ID = 11, User_Name = "Salma", Email = "salma@mail.com", Password = "s@lm@1", IsAdmin = false },
                new User { User_ID = 12, User_Name = "Youssef", Email = "youssef@mail.com", Password = "jo1234", IsAdmin = false },
                new User { User_ID = 13, User_Name = "Hoda", Email = "hoda@mail.com", Password = "hoda77", IsAdmin = false },
                new User { User_ID = 14, User_Name = "Mahmoud", Email = "mahmoud@mail.com", Password = "mah_00", IsAdmin = false },
                new User { User_ID = 15, User_Name = "Farah", Email = "farah@mail.com", Password = "farah#", IsAdmin = false },
                new User { User_ID = 16, User_Name = "Khaled", Email = "khaled@mail.com", Password = "kh_991", IsAdmin = false },
                new User { User_ID = 17, User_Name = "Rania", Email = "rania@mail.com", Password = "r@nia", IsAdmin = false },
                new User { User_ID = 18, User_Name = "Ziad", Email = "ziad@mail.com", Password = "zizou!", IsAdmin = false },
                new User { User_ID = 19, User_Name = "Dina", Email = "dina@mail.com", Password = "dina_d", IsAdmin = false },
                new User { User_ID = 20, User_Name = "Ali", Email = "ali@mail.com", Password = "ali2026", IsAdmin = false },
                new User { User_ID = 21, User_Name = "Nada", Email = "nada@mail.com", Password = "nada*8", IsAdmin = false },
                new User { User_ID = 22, User_Name = "Rami", Email = "rami@mail.com", Password = "rami_r", IsAdmin = false },
                new User { User_ID = 23, User_Name = "Yasmine", Email = "yasmine2@mail.com", Password = "yas22", IsAdmin = false },
                new User { User_ID = 24, User_Name = "Kareem", Email = "kareem@mail.com", Password = "kimo88", IsAdmin = false },
                new User { User_ID = 25, User_Name = "Merna", Email = "merna@mail.com", Password = "merna1", IsAdmin = false }
            );

            // Seed UserProfiles
            modelBuilder.Entity<UserProfile>().HasData(
                new UserProfile { Profile_ID = 6, User_ID = 6, Height = 180.00m, Weight = 95.00m, Date_of_Birth = new DateTime(1990, 8, 22), Gender = "Male", Activity_Level = "Sedentary", Medical_Condition = "Hypertension", Target_Weight = 80.00m },
                new UserProfile { Profile_ID = 7, User_ID = 7, Height = 165.00m, Weight = 55.00m, Date_of_Birth = new DateTime(1995, 12, 1), Gender = "Female", Activity_Level = "Very Active", Medical_Condition = null, Target_Weight = 58.00m },
                new UserProfile { Profile_ID = 8, User_ID = 8, Height = 172.00m, Weight = 85.00m, Date_of_Birth = new DateTime(1988, 4, 14), Gender = "Male", Activity_Level = "Light", Medical_Condition = "Type 2 Diabetes", Target_Weight = 75.00m },
                new UserProfile { Profile_ID = 9, User_ID = 9, Height = 155.00m, Weight = 70.00m, Date_of_Birth = new DateTime(2000, 9, 30), Gender = "Female", Activity_Level = "Moderate", Medical_Condition = "PCOS", Target_Weight = 60.00m },
                new UserProfile { Profile_ID = 10, User_ID = 10, Height = 185.00m, Weight = 105.00m, Date_of_Birth = new DateTime(1985, 2, 18), Gender = "Male", Activity_Level = "Low", Medical_Condition = "High Cholesterol", Target_Weight = 90.00m },
                new UserProfile { Profile_ID = 11, User_ID = 11, Height = 168.00m, Weight = 64.00m, Date_of_Birth = new DateTime(1998, 11, 25), Gender = "Female", Activity_Level = "Active", Medical_Condition = null, Target_Weight = 62.00m },
                new UserProfile { Profile_ID = 12, User_ID = 12, Height = 178.00m, Weight = 75.00m, Date_of_Birth = new DateTime(2001, 7, 7), Gender = "Male", Activity_Level = "Very Active", Medical_Condition = null, Target_Weight = 82.00m },
                new UserProfile { Profile_ID = 13, User_ID = 13, Height = 160.00m, Weight = 50.00m, Date_of_Birth = new DateTime(1992, 5, 19), Gender = "Female", Activity_Level = "Light", Medical_Condition = "Anemia", Target_Weight = 55.00m },
                new UserProfile { Profile_ID = 14, User_ID = 14, Height = 170.00m, Weight = 90.00m, Date_of_Birth = new DateTime(1980, 10, 10), Gender = "Male", Activity_Level = "Moderate", Medical_Condition = null, Target_Weight = 80.00m },
                new UserProfile { Profile_ID = 15, User_ID = 15, Height = 164.00m, Weight = 68.00m, Date_of_Birth = new DateTime(1996, 3, 22), Gender = "Female", Activity_Level = "Low", Medical_Condition = "Thyroid Issue", Target_Weight = 60.00m },
                new UserProfile { Profile_ID = 16, User_ID = 16, Height = 182.00m, Weight = 88.00m, Date_of_Birth = new DateTime(1993, 1, 30), Gender = "Male", Activity_Level = "Active", Medical_Condition = null, Target_Weight = 85.00m },
                new UserProfile { Profile_ID = 17, User_ID = 17, Height = 158.00m, Weight = 72.00m, Date_of_Birth = new DateTime(1987, 6, 15), Gender = "Female", Activity_Level = "Sedentary", Medical_Condition = null, Target_Weight = 60.00m },
                new UserProfile { Profile_ID = 18, User_ID = 18, Height = 176.00m, Weight = 79.00m, Date_of_Birth = new DateTime(1999, 8, 5), Gender = "Male", Activity_Level = "Moderate", Medical_Condition = "Asthma", Target_Weight = 75.00m },
                new UserProfile { Profile_ID = 19, User_ID = 19, Height = 166.00m, Weight = 60.00m, Date_of_Birth = new DateTime(2002, 12, 12), Gender = "Female", Activity_Level = "Active", Medical_Condition = null, Target_Weight = 60.00m },
                new UserProfile { Profile_ID = 20, User_ID = 20, Height = 188.00m, Weight = 110.00m, Date_of_Birth = new DateTime(1982, 4, 20), Gender = "Male", Activity_Level = "Low", Medical_Condition = "Joint Pain", Target_Weight = 95.00m },
                new UserProfile { Profile_ID = 21, User_ID = 21, Height = 162.00m, Weight = 66.00m, Date_of_Birth = new DateTime(1997, 9, 8), Gender = "Female", Activity_Level = "Moderate", Medical_Condition = null, Target_Weight = 60.00m },
                new UserProfile { Profile_ID = 22, User_ID = 22, Height = 174.00m, Weight = 82.00m, Date_of_Birth = new DateTime(1994, 11, 11), Gender = "Male", Activity_Level = "Active", Medical_Condition = null, Target_Weight = 78.00m },
                new UserProfile { Profile_ID = 23, User_ID = 23, Height = 167.00m, Weight = 63.00m, Date_of_Birth = new DateTime(1989, 2, 28), Gender = "Female", Activity_Level = "Light", Medical_Condition = null, Target_Weight = 60.00m },
                new UserProfile { Profile_ID = 24, User_ID = 24, Height = 180.00m, Weight = 77.00m, Date_of_Birth = new DateTime(2000, 5, 16), Gender = "Male", Activity_Level = "Very Active", Medical_Condition = null, Target_Weight = 85.00m },
                new UserProfile { Profile_ID = 25, User_ID = 25, Height = 159.00m, Weight = 58.00m, Date_of_Birth = new DateTime(1991, 7, 24), Gender = "Female", Activity_Level = "Moderate", Medical_Condition = null, Target_Weight = 55.00m }
            );

            // Seed DailyLogs
            modelBuilder.Entity<DailyLog>().HasData(
                new DailyLog { Log_ID = 6, User_ID = 6, Log_Date = new DateTime(2026, 2, 24), Weight = 94.50m },
                new DailyLog { Log_ID = 7, User_ID = 7, Log_Date = new DateTime(2026, 2, 24), Weight = 55.20m },
                new DailyLog { Log_ID = 8, User_ID = 8, Log_Date = new DateTime(2026, 2, 24), Weight = 84.00m },
                new DailyLog { Log_ID = 9, User_ID = 9, Log_Date = new DateTime(2026, 2, 24), Weight = 69.80m },
                new DailyLog { Log_ID = 10, User_ID = 10, Log_Date = new DateTime(2026, 2, 25), Weight = 104.50m },
                new DailyLog { Log_ID = 11, User_ID = 11, Log_Date = new DateTime(2026, 2, 25), Weight = 63.50m },
                new DailyLog { Log_ID = 12, User_ID = 12, Log_Date = new DateTime(2026, 2, 25), Weight = 75.50m },
                new DailyLog { Log_ID = 13, User_ID = 13, Log_Date = new DateTime(2026, 2, 25), Weight = 50.50m },
                new DailyLog { Log_ID = 14, User_ID = 14, Log_Date = new DateTime(2026, 2, 26), Weight = 89.50m },
                new DailyLog { Log_ID = 15, User_ID = 15, Log_Date = new DateTime(2026, 2, 26), Weight = 67.50m },
                new DailyLog { Log_ID = 16, User_ID = 16, Log_Date = new DateTime(2026, 2, 26), Weight = 87.50m },
                new DailyLog { Log_ID = 17, User_ID = 17, Log_Date = new DateTime(2026, 2, 26), Weight = 71.50m },
                new DailyLog { Log_ID = 18, User_ID = 18, Log_Date = new DateTime(2026, 2, 27), Weight = 78.50m },
                new DailyLog { Log_ID = 19, User_ID = 19, Log_Date = new DateTime(2026, 2, 27), Weight = 60.00m },
                new DailyLog { Log_ID = 20, User_ID = 20, Log_Date = new DateTime(2026, 2, 27), Weight = 109.00m },
                new DailyLog { Log_ID = 21, User_ID = 21, Log_Date = new DateTime(2026, 2, 28), Weight = 65.50m },
                new DailyLog { Log_ID = 22, User_ID = 22, Log_Date = new DateTime(2026, 2, 28), Weight = 81.50m },
                new DailyLog { Log_ID = 23, User_ID = 23, Log_Date = new DateTime(2026, 2, 28), Weight = 62.80m },
                new DailyLog { Log_ID = 24, User_ID = 24, Log_Date = new DateTime(2026, 3, 1), Weight = 77.20m },
                new DailyLog { Log_ID = 25, User_ID = 25, Log_Date = new DateTime(2026, 3, 1), Weight = 57.80m }
            );

            // Seed Meals
            modelBuilder.Entity<Meal>().HasData(
                new Meal { Meal_ID = 6, Log_ID = 6, Meal_Name = "Lunch", Meal_Type = "Main" },
                new Meal { Meal_ID = 7, Log_ID = 7, Meal_Name = "Post-Workout", Meal_Type = "Snack" },
                new Meal { Meal_ID = 8, Log_ID = 8, Meal_Name = "Dinner", Meal_Type = "Main" },
                new Meal { Meal_ID = 9, Log_ID = 9, Meal_Name = "Breakfast", Meal_Type = "Main" },
                new Meal { Meal_ID = 10, Log_ID = 10, Meal_Name = "Lunch", Meal_Type = "Main" },
                new Meal { Meal_ID = 11, Log_ID = 11, Meal_Name = "Dinner", Meal_Type = "Main" },
                new Meal { Meal_ID = 12, Log_ID = 12, Meal_Name = "Pre-Workout", Meal_Type = "Snack" },
                new Meal { Meal_ID = 13, Log_ID = 13, Meal_Name = "Breakfast", Meal_Type = "Main" },
                new Meal { Meal_ID = 14, Log_ID = 14, Meal_Name = "Lunch", Meal_Type = "Main" },
                new Meal { Meal_ID = 15, Log_ID = 15, Meal_Name = "Snack", Meal_Type = "Snack" },
                new Meal { Meal_ID = 16, Log_ID = 16, Meal_Name = "Dinner", Meal_Type = "Main" },
                new Meal { Meal_ID = 17, Log_ID = 17, Meal_Name = "Breakfast", Meal_Type = "Main" },
                new Meal { Meal_ID = 18, Log_ID = 18, Meal_Name = "Lunch", Meal_Type = "Main" },
                new Meal { Meal_ID = 19, Log_ID = 19, Meal_Name = "Dinner", Meal_Type = "Main" },
                new Meal { Meal_ID = 20, Log_ID = 20, Meal_Name = "Lunch", Meal_Type = "Main" },
                new Meal { Meal_ID = 21, Log_ID = 21, Meal_Name = "Breakfast", Meal_Type = "Main" },
                new Meal { Meal_ID = 22, Log_ID = 22, Meal_Name = "Post-Workout", Meal_Type = "Snack" },
                new Meal { Meal_ID = 23, Log_ID = 23, Meal_Name = "Dinner", Meal_Type = "Main" },
                new Meal { Meal_ID = 24, Log_ID = 24, Meal_Name = "Lunch", Meal_Type = "Main" },
                new Meal { Meal_ID = 25, Log_ID = 25, Meal_Name = "Snack", Meal_Type = "Snack" }
            );

            // Seed MealItems
            modelBuilder.Entity<MealItem>().HasData(
                new MealItem { Meal_Item_ID = 6, Meal_ID = 6, Food_Name = "Grilled Salmon", Quantity = 1, Calories = 350, Protein = 40, Fat = 20, Carbs = 0 },
                new MealItem { Meal_Item_ID = 7, Meal_ID = 7, Food_Name = "Protein Shake", Quantity = 1, Calories = 180, Protein = 30, Fat = 2, Carbs = 5 },
                new MealItem { Meal_Item_ID = 8, Meal_ID = 8, Food_Name = "Steak", Quantity = 1, Calories = 450, Protein = 45, Fat = 30, Carbs = 0 },
                new MealItem { Meal_Item_ID = 9, Meal_ID = 9, Food_Name = "Greek Yogurt", Quantity = 1, Calories = 100, Protein = 10, Fat = 0, Carbs = 15 },
                new MealItem { Meal_Item_ID = 10, Meal_ID = 10, Food_Name = "Burger (No Bun)", Quantity = 2, Calories = 600, Protein = 50, Fat = 40, Carbs = 5 },
                new MealItem { Meal_Item_ID = 11, Meal_ID = 11, Food_Name = "Quinoa Bowl", Quantity = 1, Calories = 300, Protein = 12, Fat = 8, Carbs = 45 },
                new MealItem { Meal_Item_ID = 12, Meal_ID = 12, Food_Name = "Banana", Quantity = 2, Calories = 210, Protein = 2, Fat = 0, Carbs = 54 },
                new MealItem { Meal_Item_ID = 13, Meal_ID = 13, Food_Name = "Spinach Omelette", Quantity = 1, Calories = 200, Protein = 15, Fat = 12, Carbs = 3 },
                new MealItem { Meal_Item_ID = 14, Meal_ID = 14, Food_Name = "Brown Rice", Quantity = 200, Calories = 220, Protein = 5, Fat = 2, Carbs = 45 },
                new MealItem { Meal_Item_ID = 15, Meal_ID = 15, Food_Name = "Almonds", Quantity = 30, Calories = 160, Protein = 6, Fat = 14, Carbs = 6 },
                new MealItem { Meal_Item_ID = 16, Meal_ID = 16, Food_Name = "Chicken Breast", Quantity = 1, Calories = 165, Protein = 31, Fat = 4, Carbs = 0 },
                new MealItem { Meal_Item_ID = 17, Meal_ID = 17, Food_Name = "Avocado Toast", Quantity = 1, Calories = 250, Protein = 5, Fat = 15, Carbs = 20 },
                new MealItem { Meal_Item_ID = 18, Meal_ID = 18, Food_Name = "Pasta", Quantity = 150, Calories = 300, Protein = 10, Fat = 2, Carbs = 60 },
                new MealItem { Meal_Item_ID = 19, Meal_ID = 19, Food_Name = "Tofu Stir Fry", Quantity = 1, Calories = 220, Protein = 18, Fat = 12, Carbs = 15 },
                new MealItem { Meal_Item_ID = 20, Meal_ID = 20, Food_Name = "Fried Chicken", Quantity = 2, Calories = 500, Protein = 25, Fat = 35, Carbs = 20 },
                new MealItem { Meal_Item_ID = 21, Meal_ID = 21, Food_Name = "Pancakes", Quantity = 2, Calories = 350, Protein = 8, Fat = 10, Carbs = 50 },
                new MealItem { Meal_Item_ID = 22, Meal_ID = 22, Food_Name = "Cottage Cheese", Quantity = 1, Calories = 120, Protein = 14, Fat = 2, Carbs = 6 },
                new MealItem { Meal_Item_ID = 23, Meal_ID = 23, Food_Name = "Lentil Soup", Quantity = 1, Calories = 180, Protein = 10, Fat = 1, Carbs = 30 },
                new MealItem { Meal_Item_ID = 24, Meal_ID = 24, Food_Name = "Sweet Potato", Quantity = 1, Calories = 110, Protein = 2, Fat = 0, Carbs = 26 },
                new MealItem { Meal_Item_ID = 25, Meal_ID = 25, Food_Name = "Dark Chocolate", Quantity = 20, Calories = 110, Protein = 1, Fat = 7, Carbs = 12 }
            );

            // Seed ExerciseLogs
            modelBuilder.Entity<ExerciseLog>().HasData(
                new ExerciseLog { Exercise_ID = 6, Log_ID = 6, Exercise_Name = "Walking", Exercise_Type = "Cardio", Duration = 45, Calories_Burned = 200 },
                new ExerciseLog { Exercise_ID = 7, Log_ID = 7, Exercise_Name = "CrossFit", Exercise_Type = "Strength", Duration = 60, Calories_Burned = 550 },
                new ExerciseLog { Exercise_ID = 8, Log_ID = 8, Exercise_Name = "Stationary Bike", Exercise_Type = "Cardio", Duration = 30, Calories_Burned = 220 },
                new ExerciseLog { Exercise_ID = 9, Log_ID = 9, Exercise_Name = "Pilates", Exercise_Type = "Flexibility", Duration = 45, Calories_Burned = 150 },
                new ExerciseLog { Exercise_ID = 10, Log_ID = 10, Exercise_Name = "Water Aerobics", Exercise_Type = "Cardio", Duration = 45, Calories_Burned = 300 },
                new ExerciseLog { Exercise_ID = 11, Log_ID = 11, Exercise_Name = "Kickboxing", Exercise_Type = "Cardio", Duration = 50, Calories_Burned = 450 },
                new ExerciseLog { Exercise_ID = 12, Log_ID = 12, Exercise_Name = "Powerlifting", Exercise_Type = "Strength", Duration = 90, Calories_Burned = 600 },
                new ExerciseLog { Exercise_ID = 13, Log_ID = 13, Exercise_Name = "Stretching", Exercise_Type = "Flexibility", Duration = 20, Calories_Burned = 80 },
                new ExerciseLog { Exercise_ID = 14, Log_ID = 14, Exercise_Name = "Rowing", Exercise_Type = "Cardio", Duration = 30, Calories_Burned = 350 },
                new ExerciseLog { Exercise_ID = 15, Log_ID = 15, Exercise_Name = "Zumba", Exercise_Type = "Cardio", Duration = 60, Calories_Burned = 400 },
                new ExerciseLog { Exercise_ID = 16, Log_ID = 16, Exercise_Name = "Basketball", Exercise_Type = "Sports", Duration = 60, Calories_Burned = 500 },
                new ExerciseLog { Exercise_ID = 17, Log_ID = 17, Exercise_Name = "Yoga", Exercise_Type = "Flexibility", Duration = 45, Calories_Burned = 120 },
                new ExerciseLog { Exercise_ID = 18, Log_ID = 18, Exercise_Name = "Jogging", Exercise_Type = "Cardio", Duration = 40, Calories_Burned = 380 },
                new ExerciseLog { Exercise_ID = 19, Log_ID = 19, Exercise_Name = "Dancing", Exercise_Type = "Cardio", Duration = 30, Calories_Burned = 200 },
                new ExerciseLog { Exercise_ID = 20, Log_ID = 20, Exercise_Name = "Physical Therapy", Exercise_Type = "Recovery", Duration = 30, Calories_Burned = 100 },
                new ExerciseLog { Exercise_ID = 21, Log_ID = 21, Exercise_Name = "Jump Rope", Exercise_Type = "Cardio", Duration = 15, Calories_Burned = 200 },
                new ExerciseLog { Exercise_ID = 22, Log_ID = 22, Exercise_Name = "Calisthenics", Exercise_Type = "Strength", Duration = 45, Calories_Burned = 350 },
                new ExerciseLog { Exercise_ID = 23, Log_ID = 23, Exercise_Name = "Elliptical", Exercise_Type = "Cardio", Duration = 30, Calories_Burned = 250 },
                new ExerciseLog { Exercise_ID = 24, Log_ID = 24, Exercise_Name = "Sprinting", Exercise_Type = "HIIT", Duration = 20, Calories_Burned = 300 },
                new ExerciseLog { Exercise_ID = 25, Log_ID = 25, Exercise_Name = "Tai Chi", Exercise_Type = "Flexibility", Duration = 40, Calories_Burned = 110 }
            );

            // Seed HealthMetrics
            modelBuilder.Entity<HealthMetric>().HasData(
                new HealthMetric { Metric_ID = 6, User_ID = 6, Metric_Date = new DateTime(2026, 2, 24), Heart_Rate = 85, Blood_Sugar = 110, Blood_Pressure = "140/90", Total_Water = 1500 },
                new HealthMetric { Metric_ID = 7, User_ID = 7, Metric_Date = new DateTime(2026, 2, 24), Heart_Rate = 65, Blood_Sugar = 90, Blood_Pressure = "110/70", Total_Water = 3500 },
                new HealthMetric { Metric_ID = 8, User_ID = 8, Metric_Date = new DateTime(2026, 2, 24), Heart_Rate = 78, Blood_Sugar = 140, Blood_Pressure = "130/85", Total_Water = 2000 },
                new HealthMetric { Metric_ID = 9, User_ID = 9, Metric_Date = new DateTime(2026, 2, 24), Heart_Rate = 82, Blood_Sugar = 95, Blood_Pressure = "118/75", Total_Water = 1800 },
                new HealthMetric { Metric_ID = 10, User_ID = 10, Metric_Date = new DateTime(2026, 2, 25), Heart_Rate = 88, Blood_Sugar = 105, Blood_Pressure = "145/95", Total_Water = 1200 },
                new HealthMetric { Metric_ID = 11, User_ID = 11, Metric_Date = new DateTime(2026, 2, 25), Heart_Rate = 70, Blood_Sugar = 88, Blood_Pressure = "115/75", Total_Water = 2500 },
                new HealthMetric { Metric_ID = 12, User_ID = 12, Metric_Date = new DateTime(2026, 2, 25), Heart_Rate = 60, Blood_Sugar = 92, Blood_Pressure = "120/80", Total_Water = 4000 },
                new HealthMetric { Metric_ID = 13, User_ID = 13, Metric_Date = new DateTime(2026, 2, 25), Heart_Rate = 90, Blood_Sugar = 85, Blood_Pressure = "100/65", Total_Water = 1600 },
                new HealthMetric { Metric_ID = 14, User_ID = 14, Metric_Date = new DateTime(2026, 2, 26), Heart_Rate = 75, Blood_Sugar = 98, Blood_Pressure = "125/82", Total_Water = 2200 },
                new HealthMetric { Metric_ID = 15, User_ID = 15, Metric_Date = new DateTime(2026, 2, 26), Heart_Rate = 77, Blood_Sugar = 90, Blood_Pressure = "112/72", Total_Water = 1900 },
                new HealthMetric { Metric_ID = 16, User_ID = 16, Metric_Date = new DateTime(2026, 2, 26), Heart_Rate = 68, Blood_Sugar = 95, Blood_Pressure = "122/80", Total_Water = 2800 },
                new HealthMetric { Metric_ID = 17, User_ID = 17, Metric_Date = new DateTime(2026, 2, 26), Heart_Rate = 80, Blood_Sugar = 89, Blood_Pressure = "119/78", Total_Water = 1700 },
                new HealthMetric { Metric_ID = 18, User_ID = 18, Metric_Date = new DateTime(2026, 2, 27), Heart_Rate = 85, Blood_Sugar = 96, Blood_Pressure = "128/84", Total_Water = 2100 },
                new HealthMetric { Metric_ID = 19, User_ID = 19, Metric_Date = new DateTime(2026, 2, 27), Heart_Rate = 72, Blood_Sugar = 88, Blood_Pressure = "115/75", Total_Water = 2300 },
                new HealthMetric { Metric_ID = 20, User_ID = 20, Metric_Date = new DateTime(2026, 2, 27), Heart_Rate = 92, Blood_Sugar = 102, Blood_Pressure = "135/88", Total_Water = 1400 },
                new HealthMetric { Metric_ID = 21, User_ID = 21, Metric_Date = new DateTime(2026, 2, 28), Heart_Rate = 76, Blood_Sugar = 91, Blood_Pressure = "118/76", Total_Water = 2000 },
                new HealthMetric { Metric_ID = 22, User_ID = 22, Metric_Date = new DateTime(2026, 2, 28), Heart_Rate = 66, Blood_Sugar = 94, Blood_Pressure = "120/79", Total_Water = 2600 },
                new HealthMetric { Metric_ID = 23, User_ID = 23, Metric_Date = new DateTime(2026, 2, 28), Heart_Rate = 74, Blood_Sugar = 87, Blood_Pressure = "114/74", Total_Water = 1800 },
                new HealthMetric { Metric_ID = 24, User_ID = 24, Metric_Date = new DateTime(2026, 3, 1), Heart_Rate = 62, Blood_Sugar = 93, Blood_Pressure = "115/75", Total_Water = 3000 },
                new HealthMetric { Metric_ID = 25, User_ID = 25, Metric_Date = new DateTime(2026, 3, 1), Heart_Rate = 79, Blood_Sugar = 89, Blood_Pressure = "116/77", Total_Water = 1900 }
            );

            // Seed Goals
            modelBuilder.Entity<Goal>().HasData(
                new Goal { Goal_ID = 6, User_ID = 6, Goal_Type = "Weight Loss", Target_Value = 80.00m, Current_Value = 94.5m, Start_Date = new DateTime(2026, 1, 1), Target_Date = new DateTime(2026, 6, 1), Achieved = false },
                new Goal { Goal_ID = 7, User_ID = 7, Goal_Type = "Muscle Gain", Target_Value = 58.00m, Current_Value = 55.2m, Start_Date = new DateTime(2026, 2, 15), Target_Date = new DateTime(2026, 5, 15), Achieved = false },
                new Goal { Goal_ID = 8, User_ID = 8, Goal_Type = "Weight Loss", Target_Value = 75.00m, Current_Value = 84.0m, Start_Date = new DateTime(2026, 2, 1), Target_Date = new DateTime(2026, 4, 1), Achieved = false },
                new Goal { Goal_ID = 9, User_ID = 9, Goal_Type = "Weight Loss", Target_Value = 60.00m, Current_Value = 69.8m, Start_Date = new DateTime(2026, 1, 10), Target_Date = new DateTime(2026, 3, 10), Achieved = false },
                new Goal { Goal_ID = 10, User_ID = 10, Goal_Type = "Weight Loss", Target_Value = 90.00m, Current_Value = 104.5m, Start_Date = new DateTime(2025, 12, 1), Target_Date = new DateTime(2026, 12, 1), Achieved = false },
                new Goal { Goal_ID = 11, User_ID = 11, Goal_Type = "Weight Loss", Target_Value = 62.00m, Current_Value = 63.5m, Start_Date = new DateTime(2026, 2, 1), Target_Date = new DateTime(2026, 3, 15), Achieved = false },
                new Goal { Goal_ID = 12, User_ID = 12, Goal_Type = "Muscle Gain", Target_Value = 82.00m, Current_Value = 75.5m, Start_Date = new DateTime(2026, 1, 1), Target_Date = new DateTime(2026, 4, 1), Achieved = false },
                new Goal { Goal_ID = 13, User_ID = 13, Goal_Type = "Weight Gain", Target_Value = 55.00m, Current_Value = 50.5m, Start_Date = new DateTime(2026, 2, 20), Target_Date = new DateTime(2026, 6, 20), Achieved = false },
                new Goal { Goal_ID = 14, User_ID = 14, Goal_Type = "Weight Loss", Target_Value = 80.00m, Current_Value = 89.5m, Start_Date = new DateTime(2026, 2, 1), Target_Date = new DateTime(2026, 5, 1), Achieved = false },
                new Goal { Goal_ID = 15, User_ID = 15, Goal_Type = "Weight Loss", Target_Value = 60.00m, Current_Value = 67.5m, Start_Date = new DateTime(2026, 1, 15), Target_Date = new DateTime(2026, 4, 15), Achieved = false },
                new Goal { Goal_ID = 16, User_ID = 16, Goal_Type = "Weight Loss", Target_Value = 85.00m, Current_Value = 87.5m, Start_Date = new DateTime(2026, 2, 10), Target_Date = new DateTime(2026, 3, 20), Achieved = false },
                new Goal { Goal_ID = 17, User_ID = 17, Goal_Type = "Weight Loss", Target_Value = 60.00m, Current_Value = 71.5m, Start_Date = new DateTime(2026, 1, 5), Target_Date = new DateTime(2026, 5, 5), Achieved = false },
                new Goal { Goal_ID = 18, User_ID = 18, Goal_Type = "Weight Loss", Target_Value = 75.00m, Current_Value = 78.5m, Start_Date = new DateTime(2026, 2, 15), Target_Date = new DateTime(2026, 4, 15), Achieved = false },
                new Goal { Goal_ID = 19, User_ID = 19, Goal_Type = "Maintain Weight", Target_Value = 60.00m, Current_Value = 60.0m, Start_Date = new DateTime(2026, 1, 1), Target_Date = new DateTime(2026, 12, 31), Achieved = true },
                new Goal { Goal_ID = 20, User_ID = 20, Goal_Type = "Weight Loss", Target_Value = 95.00m, Current_Value = 109.0m, Start_Date = new DateTime(2026, 2, 1), Target_Date = new DateTime(2026, 8, 1), Achieved = false },
                new Goal { Goal_ID = 21, User_ID = 21, Goal_Type = "Weight Loss", Target_Value = 60.00m, Current_Value = 65.5m, Start_Date = new DateTime(2026, 2, 20), Target_Date = new DateTime(2026, 5, 20), Achieved = false },
                new Goal { Goal_ID = 22, User_ID = 22, Goal_Type = "Weight Loss", Target_Value = 78.00m, Current_Value = 81.5m, Start_Date = new DateTime(2026, 2, 10), Target_Date = new DateTime(2026, 4, 10), Achieved = false },
                new Goal { Goal_ID = 23, User_ID = 23, Goal_Type = "Weight Loss", Target_Value = 60.00m, Current_Value = 62.8m, Start_Date = new DateTime(2026, 1, 25), Target_Date = new DateTime(2026, 3, 25), Achieved = false },
                new Goal { Goal_ID = 24, User_ID = 24, Goal_Type = "Muscle Gain", Target_Value = 85.00m, Current_Value = 77.2m, Start_Date = new DateTime(2026, 1, 1), Target_Date = new DateTime(2026, 7, 1), Achieved = false },
                new Goal { Goal_ID = 25, User_ID = 25, Goal_Type = "Weight Loss", Target_Value = 55.00m, Current_Value = 57.8m, Start_Date = new DateTime(2026, 2, 1), Target_Date = new DateTime(2026, 3, 31), Achieved = false }
            );
        }
    }
}
