use BCMS;
go

insert into Users(Email, [Password], [Name], [Address], Phone, [Role], JoinDate)
values
    ('tuanminhphamtdn@gmail.com', 'AQAAAAIAAYagAAAAEFaX1Zpk61o5SJbqRbvBSXiSiO3yJEzK4Jlumbdssx4mja+bubEb5oK17gnEW3Fp0w==', 'Admin', '123 Admin', '0123456789', 'ADM', '2023-07-30 09:00:00'),
    ('minhpptse171403@fpt.edu.vn', 'AQAAAAIAAYagAAAAEFyv08Y2Xaqr88zhmKulI6gpKvaK0xAYhYgkF8A3E+ER/YvxNn6QZFE9c5WDAHW81w==', 'Staff', '123 Staff', '0123456789', 'STF', '2023-07-29 12:00:00'),
    ('test@test.com', 'AQAAAAIAAYagAAAAENqW+jkdIMXsFuR9vKwbaPYUjAAtuL8sLVfrrZ55IdiCYFtJWhwqDwr6I3nMK0gazg==', 'Member', '123 Member', '0123456789', 'MEM', '2023-07-28 21:00:00'),
    ('nghia7387@gmail.com', 'AQAAAAIAAYagAAAAEMza/eTrgDgsbh/edyrJWuFE7PrJV7tghYu8rsFSVtuTe+LJgwo/NZ6+4Jn8dmKm8w==', 'Member2', '123 Member2', '0123456789', 'MEM', '2023-07-27 17:34:58');
go

insert into BlogCategories([Name])
values
    (N'Phân biệt chào mào'),
    (N'Kỹ thuật chăm sóc'),
    (N'Các loại bệnh'),
    (N'Các loại cám'),
    (N'Phụ kiện chào mào'),
    (N'Các loại chim khác'),
    (N'Khác');
go
