-- School-To-Home Behavior Tracker
-- Creation Scrippt
-- Description: Creates and deletes tables for application database

USE test_db;

DROP TABLE ParentToStudent;
DROP TABLE StudentForms;
DROP TABLE Student;
DROP TABLE ParentAccount;
DROP TABLE AdminAccount;
DROP TABLE TeacherAccount;
DROP TABLE Users;
DROP TABLE Forms;
DROP TABLE AccessCodes;
DROP TRIGGER OneAdminTrigger;

USE test_db;
CREATE TABLE AccessCodes (
	AccessCode INT PRIMARY KEY
	, FullName VARCHAR(100) UNIQUE NOT NULL);

CREATE TABLE Forms (
	FormID INT PRIMARY KEY auto_increment
	, FormName VARCHAR(50) UNIQUE NOT NULL
	, Category VARCHAR(50) NOT NULL);
	
CREATE TABLE Users (
	UserID INT PRIMARY KEY auto_increment
	, Email VARCHAR(50) UNIQUE NOT NULL
	, Password VARCHAR(50) NOT NULL
    , Code VARCHAR(50) DEFAULT NULL
    , Expiration DATETIME DEFAULT NULL);
	
CREATE TABLE TeacherAccount (
	TeacherID INT PRIMARY KEY auto_increment
	, UserID INT UNIQUE NOT NULL REFERENCES Users (UserID) 
	, AccessCode INT UNIQUE NOT NULL REFERENCES AccessCodes (AccessCode) 
	, UserName VARCHAR(50) NOT NULL
	, LastAccess DATETIME
    , LastAccess2 DATETIME);

CREATE TABLE AdminAccount (
	AdminID INT PRIMARY KEY auto_increment
	, UserID INT UNIQUE NOT NULL REFERENCES Users (UserID)
	, LastAccess DATETIME
    , LastAccess2 DATETIME);
	
CREATE TABLE ParentAccount (
	ParentID INT PRIMARY KEY auto_increment
	, UserID INT UNIQUE NOT NULL REFERENCES Users (UserID)
	, UserName VARCHAR(50) NOT NULL
	, LastAccess DATETIME
    , LastAccess2 DATETIME);
	
CREATE TABLE Student (
	StudentID INT PRIMARY KEY auto_increment
	, TeacherID INT NOT NULL REFERENCES TeacherAccount (TeacherID) 
	, TeacherCode INT UNIQUE NOT NULL
	, FirstName VARCHAR(50) NOT NULL
	, LastName VARCHAR(50) NOT NULL
	, BirthDate VARCHAR(20)
	, Grade VARCHAR(50)
	, ParentGuardian1 VARCHAR(100)
	, ParentGuardian1Phone VARCHAR(20)
	, ParentGuardian1Address VARCHAR(100)
	, ParentGuardian2 VARCHAR(100)
	, ParentGuardian2Phone VARCHAR(20)
	, ParentGuardian2Address VARCHAR(100));
	
CREATE TABLE StudentForms (
	StudentFormID INT PRIMARY KEY auto_increment
	, StudentID INT NOT NULL REFERENCES Student (StudentID) 
	, FormID INT NOT NULL REFERENCES Forms (FormID)
    , FormName VARCHAR(50) NOT NULL
	, FormDate VARCHAR(20) NOT NULL
	, EndDate VARCHAR(20)
	, Shared BIT NOT NULL DEFAULT 0 
	, FormData longtext NOT NULL
	, Description VARCHAR(100) DEFAULT NULL
    , BehaviorRating INT DEFAULT NULL);
	
CREATE TABLE ParentToStudent (
	ParentToStudentID INT PRIMARY KEY auto_increment
	, ParentID INT NOT NULL REFERENCES ParentAccount (ParentID)
	, StudentID INT NOT NULL REFERENCES Student (StudentID));
	
-- ---------------------CREATE TRIGGERS-------------------------
Use test_db
DELIMITER $$
CREATE TRIGGER OneAdminTrigger
	BEFORE INSERT ON AdminAccount
	FOR EACH ROW
BEGIN
	IF (EXISTS (SELECT * FROM AdminAccount HAVING COUNT(AdminID) > 0))
	THEN
    SIGNAL SQLSTATE VALUE '45000' SET MESSAGE_TEXT = 'INSERT failed due to existing admin account';
  END IF;
END$$
DELIMITER ;



