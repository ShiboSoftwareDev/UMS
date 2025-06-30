create database dataOne;
use dataOne;

CREATE TABLE owners (
    id INT PRIMARY KEY IDENTITY(1,1),
    username NVARCHAR(50),
    password_hash NVARCHAR(100),
    role NVARCHAR(20),
    failed_attempts INT DEFAULT 0,
    last_attempt DATETIME DEFAULT GETDATE()
);
