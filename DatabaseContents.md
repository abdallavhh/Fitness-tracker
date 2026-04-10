# Database Contents: HealthTracker.db

## Table: __EFMigrationsHistory

| MigrationId | ProductVersion |
| --- | --- |
| 20260408184143_InitialCreate | 8.0.0 |

## Table: Users

| User_ID | User_Name | Email | Password |
| --- | --- | --- | --- |
| 6 | Ahmed | ahmed@mail.com | pwd123 |
| 7 | Mona | mona@mail.com | m0n@! |
| 8 | Omar | omar@mail.com | omr99 |
| 9 | Laila | laila@mail.com | lolo22 |
| 10 | Tarek | tarek@mail.com | tk_88 |
| 11 | Salma | salma@mail.com | s@lm@1 |
| 12 | Youssef | youssef@mail.com | jo1234 |
| 13 | Hoda | hoda@mail.com | hoda77 |
| 14 | Mahmoud | mahmoud@mail.com | mah_00 |
| 15 | Farah | farah@mail.com | farah# |
| 16 | Khaled | khaled@mail.com | kh_991 |
| 17 | Rania | rania@mail.com | r@nia |
| 18 | Ziad | ziad@mail.com | zizou! |
| 19 | Dina | dina@mail.com | dina_d |
| 20 | Ali | ali@mail.com | ali2026 |
| 21 | Nada | nada@mail.com | nada*8 |
| 22 | Rami | rami@mail.com | rami_r |
| 23 | Yasmine | yasmine2@mail.com | yas22 |
| 24 | Kareem | kareem@mail.com | kimo88 |
| 25 | Merna | merna@mail.com | merna1 |

## Table: DailyLogs

| Log_ID | User_ID | Log_Date | Weight |
| --- | --- | --- | --- |
| 6 | 6 | 2026-02-24 00:00:00 | 94.5 |
| 7 | 7 | 2026-02-24 00:00:00 | 55.2 |
| 8 | 8 | 2026-02-24 00:00:00 | 84 |
| 9 | 9 | 2026-02-24 00:00:00 | 69.8 |
| 10 | 10 | 2026-02-25 00:00:00 | 104.5 |
| 11 | 11 | 2026-02-25 00:00:00 | 63.5 |
| 12 | 12 | 2026-02-25 00:00:00 | 75.5 |
| 13 | 13 | 2026-02-25 00:00:00 | 50.5 |
| 14 | 14 | 2026-02-26 00:00:00 | 89.5 |
| 15 | 15 | 2026-02-26 00:00:00 | 67.5 |
| 16 | 16 | 2026-02-26 00:00:00 | 87.5 |
| 17 | 17 | 2026-02-26 00:00:00 | 71.5 |
| 18 | 18 | 2026-02-27 00:00:00 | 78.5 |
| 19 | 19 | 2026-02-27 00:00:00 | 60 |
| 20 | 20 | 2026-02-27 00:00:00 | 109 |
| 21 | 21 | 2026-02-28 00:00:00 | 65.5 |
| 22 | 22 | 2026-02-28 00:00:00 | 81.5 |
| 23 | 23 | 2026-02-28 00:00:00 | 62.8 |
| 24 | 24 | 2026-03-01 00:00:00 | 77.2 |
| 25 | 25 | 2026-03-01 00:00:00 | 57.8 |

## Table: Goals

| Goal_ID | User_ID | Goal_Type | Target_Value | Current_Value | Start_Date | Target_Date | Achieved |
| --- | --- | --- | --- | --- | --- | --- | --- |
| 6 | 6 | Weight Loss | 80 | 94.5 | 2026-01-01 00:00:00 | 2026-06-01 00:00:00 | 0 |
| 7 | 7 | Muscle Gain | 58 | 55.2 | 2026-02-15 00:00:00 | 2026-05-15 00:00:00 | 0 |
| 8 | 8 | Weight Loss | 75 | 84 | 2026-02-01 00:00:00 | 2026-04-01 00:00:00 | 0 |
| 9 | 9 | Weight Loss | 60 | 69.8 | 2026-01-10 00:00:00 | 2026-03-10 00:00:00 | 0 |
| 10 | 10 | Weight Loss | 90 | 104.5 | 2025-12-01 00:00:00 | 2026-12-01 00:00:00 | 0 |
| 11 | 11 | Weight Loss | 62 | 63.5 | 2026-02-01 00:00:00 | 2026-03-15 00:00:00 | 0 |
| 12 | 12 | Muscle Gain | 82 | 75.5 | 2026-01-01 00:00:00 | 2026-04-01 00:00:00 | 0 |
| 13 | 13 | Weight Gain | 55 | 50.5 | 2026-02-20 00:00:00 | 2026-06-20 00:00:00 | 0 |
| 14 | 14 | Weight Loss | 80 | 89.5 | 2026-02-01 00:00:00 | 2026-05-01 00:00:00 | 0 |
| 15 | 15 | Weight Loss | 60 | 67.5 | 2026-01-15 00:00:00 | 2026-04-15 00:00:00 | 0 |
| 16 | 16 | Weight Loss | 85 | 87.5 | 2026-02-10 00:00:00 | 2026-03-20 00:00:00 | 0 |
| 17 | 17 | Weight Loss | 60 | 71.5 | 2026-01-05 00:00:00 | 2026-05-05 00:00:00 | 0 |
| 18 | 18 | Weight Loss | 75 | 78.5 | 2026-02-15 00:00:00 | 2026-04-15 00:00:00 | 0 |
| 19 | 19 | Maintain Weight | 60 | 60 | 2026-01-01 00:00:00 | 2026-12-31 00:00:00 | 1 |
| 20 | 20 | Weight Loss | 95 | 109 | 2026-02-01 00:00:00 | 2026-08-01 00:00:00 | 0 |
| 21 | 21 | Weight Loss | 60 | 65.5 | 2026-02-20 00:00:00 | 2026-05-20 00:00:00 | 0 |
| 22 | 22 | Weight Loss | 78 | 81.5 | 2026-02-10 00:00:00 | 2026-04-10 00:00:00 | 0 |
| 23 | 23 | Weight Loss | 60 | 62.8 | 2026-01-25 00:00:00 | 2026-03-25 00:00:00 | 0 |
| 24 | 24 | Muscle Gain | 85 | 77.2 | 2026-01-01 00:00:00 | 2026-07-01 00:00:00 | 0 |
| 25 | 25 | Weight Loss | 55 | 57.8 | 2026-02-01 00:00:00 | 2026-03-31 00:00:00 | 0 |

## Table: HealthMetrics

| Metric_ID | User_ID | Metric_Date | Heart_Rate | Blood_Sugar | Blood_Pressure | Total_Water |
| --- | --- | --- | --- | --- | --- | --- |
| 6 | 6 | 2026-02-24 00:00:00 | 85 | 110 | 140/90 | 1500 |
| 7 | 7 | 2026-02-24 00:00:00 | 65 | 90 | 110/70 | 3500 |
| 8 | 8 | 2026-02-24 00:00:00 | 78 | 140 | 130/85 | 2000 |
| 9 | 9 | 2026-02-24 00:00:00 | 82 | 95 | 118/75 | 1800 |
| 10 | 10 | 2026-02-25 00:00:00 | 88 | 105 | 145/95 | 1200 |
| 11 | 11 | 2026-02-25 00:00:00 | 70 | 88 | 115/75 | 2500 |
| 12 | 12 | 2026-02-25 00:00:00 | 60 | 92 | 120/80 | 4000 |
| 13 | 13 | 2026-02-25 00:00:00 | 90 | 85 | 100/65 | 1600 |
| 14 | 14 | 2026-02-26 00:00:00 | 75 | 98 | 125/82 | 2200 |
| 15 | 15 | 2026-02-26 00:00:00 | 77 | 90 | 112/72 | 1900 |
| 16 | 16 | 2026-02-26 00:00:00 | 68 | 95 | 122/80 | 2800 |
| 17 | 17 | 2026-02-26 00:00:00 | 80 | 89 | 119/78 | 1700 |
| 18 | 18 | 2026-02-27 00:00:00 | 85 | 96 | 128/84 | 2100 |
| 19 | 19 | 2026-02-27 00:00:00 | 72 | 88 | 115/75 | 2300 |
| 20 | 20 | 2026-02-27 00:00:00 | 92 | 102 | 135/88 | 1400 |
| 21 | 21 | 2026-02-28 00:00:00 | 76 | 91 | 118/76 | 2000 |
| 22 | 22 | 2026-02-28 00:00:00 | 66 | 94 | 120/79 | 2600 |
| 23 | 23 | 2026-02-28 00:00:00 | 74 | 87 | 114/74 | 1800 |
| 24 | 24 | 2026-03-01 00:00:00 | 62 | 93 | 115/75 | 3000 |
| 25 | 25 | 2026-03-01 00:00:00 | 79 | 89 | 116/77 | 1900 |

## Table: UserProfiles

| Profile_ID | User_ID | Height | Weight | Date_of_Birth | Gender | Activity_Level | Medical_Condition | Target_Weight |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 6 | 6 | 180 | 95 | 1990-08-22 00:00:00 | Male | Sedentary | Hypertension | 80 |
| 7 | 7 | 165 | 55 | 1995-12-01 00:00:00 | Female | Very Active | NULL | 58 |
| 8 | 8 | 172 | 85 | 1988-04-14 00:00:00 | Male | Light | Type 2 Diabetes | 75 |
| 9 | 9 | 155 | 70 | 2000-09-30 00:00:00 | Female | Moderate | PCOS | 60 |
| 10 | 10 | 185 | 105 | 1985-02-18 00:00:00 | Male | Low | High Cholesterol | 90 |
| 11 | 11 | 168 | 64 | 1998-11-25 00:00:00 | Female | Active | NULL | 62 |
| 12 | 12 | 178 | 75 | 2001-07-07 00:00:00 | Male | Very Active | NULL | 82 |
| 13 | 13 | 160 | 50 | 1992-05-19 00:00:00 | Female | Light | Anemia | 55 |
| 14 | 14 | 170 | 90 | 1980-10-10 00:00:00 | Male | Moderate | NULL | 80 |
| 15 | 15 | 164 | 68 | 1996-03-22 00:00:00 | Female | Low | Thyroid Issue | 60 |
| 16 | 16 | 182 | 88 | 1993-01-30 00:00:00 | Male | Active | NULL | 85 |
| 17 | 17 | 158 | 72 | 1987-06-15 00:00:00 | Female | Sedentary | NULL | 60 |
| 18 | 18 | 176 | 79 | 1999-08-05 00:00:00 | Male | Moderate | Asthma | 75 |
| 19 | 19 | 166 | 60 | 2002-12-12 00:00:00 | Female | Active | NULL | 60 |
| 20 | 20 | 188 | 110 | 1982-04-20 00:00:00 | Male | Low | Joint Pain | 95 |
| 21 | 21 | 162 | 66 | 1997-09-08 00:00:00 | Female | Moderate | NULL | 60 |
| 22 | 22 | 174 | 82 | 1994-11-11 00:00:00 | Male | Active | NULL | 78 |
| 23 | 23 | 167 | 63 | 1989-02-28 00:00:00 | Female | Light | NULL | 60 |
| 24 | 24 | 180 | 77 | 2000-05-16 00:00:00 | Male | Very Active | NULL | 85 |
| 25 | 25 | 159 | 58 | 1991-07-24 00:00:00 | Female | Moderate | NULL | 55 |

## Table: ExerciseLogs

| Exercise_ID | Log_ID | Exercise_Name | Exercise_Type | Duration | Calories_Burned |
| --- | --- | --- | --- | --- | --- |
| 6 | 6 | Walking | Cardio | 45 | 200 |
| 7 | 7 | CrossFit | Strength | 60 | 550 |
| 8 | 8 | Stationary Bike | Cardio | 30 | 220 |
| 9 | 9 | Pilates | Flexibility | 45 | 150 |
| 10 | 10 | Water Aerobics | Cardio | 45 | 300 |
| 11 | 11 | Kickboxing | Cardio | 50 | 450 |
| 12 | 12 | Powerlifting | Strength | 90 | 600 |
| 13 | 13 | Stretching | Flexibility | 20 | 80 |
| 14 | 14 | Rowing | Cardio | 30 | 350 |
| 15 | 15 | Zumba | Cardio | 60 | 400 |
| 16 | 16 | Basketball | Sports | 60 | 500 |
| 17 | 17 | Yoga | Flexibility | 45 | 120 |
| 18 | 18 | Jogging | Cardio | 40 | 380 |
| 19 | 19 | Dancing | Cardio | 30 | 200 |
| 20 | 20 | Physical Therapy | Recovery | 30 | 100 |
| 21 | 21 | Jump Rope | Cardio | 15 | 200 |
| 22 | 22 | Calisthenics | Strength | 45 | 350 |
| 23 | 23 | Elliptical | Cardio | 30 | 250 |
| 24 | 24 | Sprinting | HIIT | 20 | 300 |
| 25 | 25 | Tai Chi | Flexibility | 40 | 110 |

## Table: Meals

| Meal_ID | Log_ID | Meal_Name | Meal_Type |
| --- | --- | --- | --- |
| 6 | 6 | Lunch | Main |
| 7 | 7 | Post-Workout | Snack |
| 8 | 8 | Dinner | Main |
| 9 | 9 | Breakfast | Main |
| 10 | 10 | Lunch | Main |
| 11 | 11 | Dinner | Main |
| 12 | 12 | Pre-Workout | Snack |
| 13 | 13 | Breakfast | Main |
| 14 | 14 | Lunch | Main |
| 15 | 15 | Snack | Snack |
| 16 | 16 | Dinner | Main |
| 17 | 17 | Breakfast | Main |
| 18 | 18 | Lunch | Main |
| 19 | 19 | Dinner | Main |
| 20 | 20 | Lunch | Main |
| 21 | 21 | Breakfast | Main |
| 22 | 22 | Post-Workout | Snack |
| 23 | 23 | Dinner | Main |
| 24 | 24 | Lunch | Main |
| 25 | 25 | Snack | Snack |

## Table: MealItems

| Meal_Item_ID | Meal_ID | Food_Name | Quantity | Calories | Protein | Fat | Carbs |
| --- | --- | --- | --- | --- | --- | --- | --- |
| 6 | 6 | Grilled Salmon | 1 | 350 | 40 | 20 | 0 |
| 7 | 7 | Protein Shake | 1 | 180 | 30 | 2 | 5 |
| 8 | 8 | Steak | 1 | 450 | 45 | 30 | 0 |
| 9 | 9 | Greek Yogurt | 1 | 100 | 10 | 0 | 15 |
| 10 | 10 | Burger (No Bun) | 2 | 600 | 50 | 40 | 5 |
| 11 | 11 | Quinoa Bowl | 1 | 300 | 12 | 8 | 45 |
| 12 | 12 | Banana | 2 | 210 | 2 | 0 | 54 |
| 13 | 13 | Spinach Omelette | 1 | 200 | 15 | 12 | 3 |
| 14 | 14 | Brown Rice | 200 | 220 | 5 | 2 | 45 |
| 15 | 15 | Almonds | 30 | 160 | 6 | 14 | 6 |
| 16 | 16 | Chicken Breast | 1 | 165 | 31 | 4 | 0 |
| 17 | 17 | Avocado Toast | 1 | 250 | 5 | 15 | 20 |
| 18 | 18 | Pasta | 150 | 300 | 10 | 2 | 60 |
| 19 | 19 | Tofu Stir Fry | 1 | 220 | 18 | 12 | 15 |
| 20 | 20 | Fried Chicken | 2 | 500 | 25 | 35 | 20 |
| 21 | 21 | Pancakes | 2 | 350 | 8 | 10 | 50 |
| 22 | 22 | Cottage Cheese | 1 | 120 | 14 | 2 | 6 |
| 23 | 23 | Lentil Soup | 1 | 180 | 10 | 1 | 30 |
| 24 | 24 | Sweet Potato | 1 | 110 | 2 | 0 | 26 |
| 25 | 25 | Dark Chocolate | 20 | 110 | 1 | 7 | 12 |

