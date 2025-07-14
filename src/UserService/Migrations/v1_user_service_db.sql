CREATE DATABASE user_service_db;

USE user_service_db;

CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Role ENUM('Admin', 'User') NOT NULL DEFAULT 'User',
    IsActive BOOLEAN DEFAULT TRUE,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- Datos iniciales (DML)
INSERT INTO Users (Username, Email, PasswordHash, FirstName, LastName, Role)
VALUES 
('admin', 'admin@example.com', '$2a$10$N9qo8uLOickgx2ZMRZoMy.MrYV7Z1z7Z2bL3Tj3V7XZz1JcQvJQ1K', 'System', 'Admin', 'Admin'),
('user1', 'user1@example.com', '$2a$10$N9qo8uLOickgx2ZMRZoMy.MrYV7Z1z7Z2bL3Tj3V7XZz1JcQvJQ1K', 'John', 'Doe', 'User');