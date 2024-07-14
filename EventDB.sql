--create database EventDB ;
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
	[image] NVARCHAR(MAX),
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


INSERT INTO Users (username, password, email, full_name, phone_number, address)
VALUES 
('tun', '123', 'nqtun251@example.com', 'Nguyễn Quốc Tuấn ', '0123456789', '123 Đường Mê Linh'),
('tathu', '123', 'tathu104@example.com', 'Tạ Thị Thu', '0987654321', '456 Đường Huyện');

INSERT INTO Tags (name, description)
VALUES 
('Công việc', 'Các sự kiện liên quan đến công việc'),
('Gia đình', 'Các sự kiện gia đình'),
('Bạn bè', 'Các sự kiện với bạn bè');


INSERT INTO Events (user_id, title, description, start_time, end_time, location, tag_id)
VALUES 
(1, 'Họp đồ án', 'Thảo luận chi tiết về đồ án', '2024-07-01 10:00:00', '2024-07-01 12:00:00', 'Alpha-204', 1),
(1, 'Bữa tối gia đình', 'Bữa tối với gia đình', '2024-07-02 19:00:00', '2024-07-02 21:00:00', 'Nhà', 2),
(2, 'Ăn trưa với bạn bè', 'Ăn trưa tại nhà hàng yêu thích', '2024-07-03 13:00:00', '2024-07-03 14:00:00', 'Nhà hàng', 3);


INSERT INTO Schedules (event_id, title, description, start_time, end_time, location)
VALUES 
(1, 'Chuẩn bị cuộc họp', 'Chuẩn bị tài liệu và bài thuyết trình', '2024-06-30 15:00:00', '2024-06-30 17:00:00', 'Alpha-204'),
(1, 'Thảo luận sơ bộ', 'Thảo luận các điểm chính', '2024-07-01 09:00:00', '2024-07-01 10:00:00', 'AL-R204'),
(2, 'Mua sắm thực phẩm', 'Mua thực phẩm cho bữa tối', '2024-07-02 17:00:00', '2024-07-02 18:00:00', 'Siêu thị'),
(2, 'Chuẩn bị nấu ăn', 'Chuẩn bị các món ăn', '2024-07-02 18:00:00', '2024-07-02 19:00:00', 'Nhà bếp'),
(3, 'Xác nhận đặt bàn', 'Xác nhận đặt bàn', '2024-07-02 10:00:00', '2024-07-02 11:00:00', 'Điện thoại'),
(3, 'Tập trung tại nhà hàng', 'Gặp gỡ bạn bè', '2024-07-03 12:45:00', '2024-07-03 13:00:00', 'Nhà hàng');

