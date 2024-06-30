create database EventDB ;
use EventDB ;

-- Tạo bảng Users
CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY(1,1),
    username NVARCHAR(50) NOT NULL,
    password NVARCHAR(255) NOT NULL,
    email NVARCHAR(100) ,
    full_name NVARCHAR(100),
    phone_number NVARCHAR(20),
    address NVARCHAR(255)
);

CREATE TABLE Tags (
    tag_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(50) ,
    description NVARCHAR(MAX)
);

CREATE TABLE [Events] (
    event_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    title NVARCHAR(100) ,
    description NVARCHAR(MAX),
    start_time DATETIME ,
    end_time DATETIME ,
    location NVARCHAR(255),
    tag_id INT,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (tag_id) REFERENCES Tags(tag_id)
);

-- Tạo bảng Personal_Schedules
CREATE TABLE Schedules (
    schedule_id INT PRIMARY KEY IDENTITY(1,1),
    event_id INT ,
    title NVARCHAR(100) ,
    description NVARCHAR(MAX),
    start_time DATETIME ,
    end_time DATETIME ,
    location NVARCHAR(255),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (event_id) REFERENCES [Events](event_id)
);

-- Tạo bảng Event_Tags
CREATE TABLE Event_Tags (
    event_id INT ,
    tag_id INT ,
    PRIMARY KEY (event_id, tag_id),
    FOREIGN KEY (event_id) REFERENCES [Events](event_id),
    FOREIGN KEY (tag_id) REFERENCES Tags(tag_id)
);


