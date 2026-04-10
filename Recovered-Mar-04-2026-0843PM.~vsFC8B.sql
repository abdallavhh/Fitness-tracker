CREATE DATABASE Health_Tracking_System;
USE Health_Tracking_System;

CREATE TABLE Users(
    User_ID INT PRIMARY KEY,
    User_Name VARCHAR(50) NOT NULL,
    Email VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(50) NOT NULL
);

CREATE TABLE User_Profile(
    Profile_ID INT PRIMARY KEY,
    User_ID INT UNIQUE,
    Height DECIMAL(5,2) NOT NULL,
    Weight DECIMAL(5,2) NOT NULL,
    Date_of_Birth DATE NOT NULL,
    Gender VARCHAR(10) NOT NULL,
    Activity_Level VARCHAR(50) NOT NULL,
    Medical_Condition VARCHAR(200),
    Target_Weight DECIMAL(5,2),
    CONSTRAINT fk_profile_user FOREIGN KEY (User_ID) REFERENCES Users(User_ID)
);

CREATE TABLE Daily_Log(
    Log_ID INT PRIMARY KEY,
    User_ID INT,
    Log_Date DATE,
    Weight DECIMAL(5,2) NOT NULL,
    CONSTRAINT fk_daily_user FOREIGN KEY (User_ID) REFERENCES Users(User_ID)
);

CREATE TABLE Exercise_Log(
    Exercise_ID INT PRIMARY KEY,
    Log_ID INT,
    Exercise_Name VARCHAR(50) NOT NULL,
    Exercise_Type VARCHAR(50) NOT NULL,
    Duration INT,
    Calories_Burned INT,
    CONSTRAINT fk_exercise_log FOREIGN KEY (Log_ID) REFERENCES Daily_Log(Log_ID)
);

CREATE TABLE Meal(
    Meal_ID INT PRIMARY KEY,
    Log_ID INT,
    Meal_Name VARCHAR(50),
    Meal_Type VARCHAR(50),
    CONSTRAINT fk_meal_log FOREIGN KEY (Log_ID) REFERENCES Daily_Log(Log_ID)
);

CREATE TABLE Meal_Item(
    Meal_Item_ID INT PRIMARY KEY,
    Meal_ID INT,
    Food_Name VARCHAR(50),
    Quantity INT,
    Calories INT,
    Protein INT,
    Fat INT,
    Carbs INT,
    CONSTRAINT fk_mealitem_meal FOREIGN KEY (Meal_ID) REFERENCES Meal(Meal_ID)
);

CREATE TABLE Health_Metric(
    Metric_ID INT PRIMARY KEY,
    User_ID INT,
    Metric_Date DATE,
    Heart_Rate INT,
    Blood_Sugar INT,
    Blood_Pressure VARCHAR(20),
    Total_Water INT,
    CONSTRAINT fk_metric_user FOREIGN KEY (User_ID) REFERENCES Users(User_ID)
);

CREATE TABLE Goal(
    Goal_ID INT PRIMARY KEY,
    User_ID INT,
    Goal_Type VARCHAR(50),
    Target_Value DECIMAL(6,2),
    Current_Value DECIMAL(6,2),
    Start_Date DATE,
    Target_Date DATE,
    Achieved BIT,
    CONSTRAINT fk_goal_user FOREIGN KEY (User_ID) REFERENCES Users(User_ID)
);






INSERT INTO Users VALUES 
(6,'Ahmed','ahmed@mail.com','pwd123'),
(7,'Mona','mona@mail.com','m0n@!'),
(8,'Omar','omar@mail.com','omr99'),
(9,'Laila','laila@mail.com','lolo22'),
(10,'Tarek','tarek@mail.com','tk_88'),
(11,'Salma','salma@mail.com','s@lm@1'),
(12,'Youssef','youssef@mail.com','jo1234'),
(13,'Hoda','hoda@mail.com','hoda77'),
(14,'Mahmoud','mahmoud@mail.com','mah_00'),
(15,'Farah','farah@mail.com','farah#'),
(16,'Khaled','khaled@mail.com','kh_991'),
(17,'Rania','rania@mail.com','r@nia'),
(18,'Ziad','ziad@mail.com','zizou!'),
(19,'Dina','dina@mail.com','dina_d'),
(20,'Ali','ali@mail.com','ali2026'),
(21,'Nada','nada@mail.com','nada*8'),
(22,'Rami','rami@mail.com','rami_r'),
(23,'Yasmine','yasmine2@mail.com','yas22'),
(24,'Kareem','kareem@mail.com','kimo88'),
(25,'Merna','merna@mail.com','merna1');


INSERT INTO User_Profile VALUES
(6,6,180.00,95.00,'1990-08-22','Male','Sedentary','Hypertension',80.00),
(7,7,165.00,55.00,'1995-12-01','Female','Very Active',NULL,58.00),
(8,8,172.00,85.00,'1988-04-14','Male','Light','Type 2 Diabetes',75.00),
(9,9,155.00,70.00,'2000-09-30','Female','Moderate','PCOS',60.00),
(10,10,185.00,105.00,'1985-02-18','Male','Low','High Cholesterol',90.00),
(11,11,168.00,64.00,'1998-11-25','Female','Active',NULL,62.00),
(12,12,178.00,75.00,'2001-07-07','Male','Very Active',NULL,82.00),
(13,13,160.00,50.00,'1992-05-19','Female','Light','Anemia',55.00),
(14,14,170.00,90.00,'1980-10-10','Male','Moderate',NULL,80.00),
(15,15,164.00,68.00,'1996-03-22','Female','Low','Thyroid Issue',60.00),
(16,16,182.00,88.00,'1993-01-30','Male','Active',NULL,85.00),
(17,17,158.00,72.00,'1987-06-15','Female','Sedentary',NULL,60.00),
(18,18,176.00,79.00,'1999-08-05','Male','Moderate','Asthma',75.00),
(19,19,166.00,60.00,'2002-12-12','Female','Active',NULL,60.00),
(20,20,188.00,110.00,'1982-04-20','Male','Low','Joint Pain',95.00),
(21,21,162.00,66.00,'1997-09-08','Female','Moderate',NULL,60.00),
(22,22,174.00,82.00,'1994-11-11','Male','Active',NULL,78.00),
(23,23,167.00,63.00,'1989-02-28','Female','Light',NULL,60.00),
(24,24,180.00,77.00,'2000-05-16','Male','Very Active',NULL,85.00),
(25,25,159.00,58.00,'1991-07-24','Female','Moderate',NULL,55.00);


INSERT INTO Daily_Log VALUES
(6,6,'2026-02-24',94.50),
(7,7,'2026-02-24',55.20),
(8,8,'2026-02-24',84.00),
(9,9,'2026-02-24',69.80),
(10,10,'2026-02-25',104.50),
(11,11,'2026-02-25',63.50),
(12,12,'2026-02-25',75.50),
(13,13,'2026-02-25',50.50),
(14,14,'2026-02-26',89.50),
(15,15,'2026-02-26',67.50),
(16,16,'2026-02-26',87.50),
(17,17,'2026-02-26',71.50),
(18,18,'2026-02-27',78.50),
(19,19,'2026-02-27',60.00),
(20,20,'2026-02-27',109.00),
(21,21,'2026-02-28',65.50),
(22,22,'2026-02-28',81.50),
(23,23,'2026-02-28',62.80),
(24,24,'2026-03-01',77.20),
(25,25,'2026-03-01',57.80);


INSERT INTO Meal VALUES
(6,6,'Lunch','Main'),
(7,7,'Post-Workout','Snack'),
(8,8,'Dinner','Main'),
(9,9,'Breakfast','Main'),
(10,10,'Lunch','Main'),
(11,11,'Dinner','Main'),
(12,12,'Pre-Workout','Snack'),
(13,13,'Breakfast','Main'),
(14,14,'Lunch','Main'),
(15,15,'Snack','Snack'),
(16,16,'Dinner','Main'),
(17,17,'Breakfast','Main'),
(18,18,'Lunch','Main'),
(19,19,'Dinner','Main'),
(20,20,'Lunch','Main'),
(21,21,'Breakfast','Main'),
(22,22,'Post-Workout','Snack'),
(23,23,'Dinner','Main'),
(24,24,'Lunch','Main'),
(25,25,'Snack','Snack');


INSERT INTO Meal_Item VALUES
(6,6,'Grilled Salmon',1,350,40,20,0),
(7,7,'Protein Shake',1,180,30,2,5),
(8,8,'Steak',1,450,45,30,0),
(9,9,'Greek Yogurt',1,100,10,0,15),
(10,10,'Burger (No Bun)',2,600,50,40,5),
(11,11,'Quinoa Bowl',1,300,12,8,45),
(12,12,'Banana',2,210,2,0,54),
(13,13,'Spinach Omelette',1,200,15,12,3),
(14,14,'Brown Rice',200,220,5,2,45),
(15,15,'Almonds',30,160,6,14,6),
(16,16,'Chicken Breast',1,165,31,4,0),
(17,17,'Avocado Toast',1,250,5,15,20),
(18,18,'Pasta',150,300,10,2,60),
(19,19,'Tofu Stir Fry',1,220,18,12,15),
(20,20,'Fried Chicken',2,500,25,35,20),
(21,21,'Pancakes',2,350,8,10,50),
(22,22,'Cottage Cheese',1,120,14,2,6),
(23,23,'Lentil Soup',1,180,10,1,30),
(24,24,'Sweet Potato',1,110,2,0,26),
(25,25,'Dark Chocolate',20,110,1,7,12);

INSERT INTO Exercise_Log VALUES
(6,6,'Walking','Cardio',45,200),
(7,7,'CrossFit','Strength',60,550),
(8,8,'Stationary Bike','Cardio',30,220),
(9,9,'Pilates','Flexibility',45,150),
(10,10,'Water Aerobics','Cardio',45,300),
(11,11,'Kickboxing','Cardio',50,450),
(12,12,'Powerlifting','Strength',90,600),
(13,13,'Stretching','Flexibility',20,80),
(14,14,'Rowing','Cardio',30,350),
(15,15,'Zumba','Cardio',60,400),
(16,16,'Basketball','Sports',60,500),
(17,17,'Yoga','Flexibility',45,120),
(18,18,'Jogging','Cardio',40,380),
(19,19,'Dancing','Cardio',30,200),
(20,20,'Physical Therapy','Recovery',30,100),
(21,21,'Jump Rope','Cardio',15,200),
(22,22,'Calisthenics','Strength',45,350),
(23,23,'Elliptical','Cardio',30,250),
(24,24,'Sprinting','HIIT',20,300),
(25,25,'Tai Chi','Flexibility',40,110);


INSERT INTO Health_Metric VALUES
(6,6,'2026-02-24',85,110,'140/90',1500),
(7,7,'2026-02-24',65,90,'110/70',3500),
(8,8,'2026-02-24',78,140,'130/85',2000),
(9,9,'2026-02-24',82,95,'118/75',1800),
(10,10,'2026-02-25',88,105,'145/95',1200),
(11,11,'2026-02-25',70,88,'115/75',2500),
(12,12,'2026-02-25',60,92,'120/80',4000),
(13,13,'2026-02-25',90,85,'100/65',1600),
(14,14,'2026-02-26',75,98,'125/82',2200),
(15,15,'2026-02-26',77,90,'112/72',1900),
(16,16,'2026-02-26',68,95,'122/80',2800),
(17,17,'2026-02-26',80,89,'119/78',1700),
(18,18,'2026-02-27',85,96,'128/84',2100),
(19,19,'2026-02-27',72,88,'115/75',2300),
(20,20,'2026-02-27',92,102,'135/88',1400),
(21,21,'2026-02-28',76,91,'118/76',2000),
(22,22,'2026-02-28',66,94,'120/79',2600),
(23,23,'2026-02-28',74,87,'114/74',1800),
(24,24,'2026-03-01',62,93,'115/75',3000),
(25,25,'2026-03-01',79,89,'116/77',1900);


INSERT INTO Goal VALUES
(6,6,'Weight Loss',80,94.5,'2026-01-01','2026-06-01',0),
(7,7,'Muscle Gain',58,55.2,'2026-02-15','2026-05-15',0),
(8,8,'Weight Loss',75,84.0,'2026-02-01','2026-04-01',0),
(9,9,'Weight Loss',60,69.8,'2026-01-10','2026-03-10',0),
(10,10,'Weight Loss',90,104.5,'2025-12-01','2026-12-01',0),
(11,11,'Weight Loss',62,63.5,'2026-02-01','2026-03-15',0),
(12,12,'Muscle Gain',82,75.5,'2026-01-01','2026-04-01',0),
(13,13,'Weight Gain',55,50.5,'2026-02-20','2026-06-20',0),
(14,14,'Weight Loss',80,89.5,'2026-02-01','2026-05-01',0),
(15,15,'Weight Loss',60,67.5,'2026-01-15','2026-04-15',0),
(16,16,'Weight Loss',85,87.5,'2026-02-10','2026-03-20',0),
(17,17,'Weight Loss',60,71.5,'2026-01-05','2026-05-05',0),
(18,18,'Weight Loss',75,78.5,'2026-02-15','2026-04-15',0),
(19,19,'Maintain Weight',60,60.0,'2026-01-01','2026-12-31',1), -- Achieved!
(20,20,'Weight Loss',95,109.0,'2026-02-01','2026-08-01',0),
(21,21,'Weight Loss',60,65.5,'2026-02-20','2026-05-20',0),
(22,22,'Weight Loss',78,81.5,'2026-02-10','2026-04-10',0),
(23,23,'Weight Loss',60,62.8,'2026-01-25','2026-03-25',0),
(24,24,'Muscle Gain',85,77.2,'2026-01-01','2026-07-01',0),
(25,25,'Weight Loss',55,57.8,'2026-02-01','2026-03-31',0);

