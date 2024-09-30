CREATE DATABASE VirtualArtDB;
USE VirtualArtDB;


CREATE TABLE Artwork (
    ArtworkID INT PRIMARY KEY IDENTITY(1,1),
    Title VARCHAR(255) NOT NULL,
    Description VARCHAR(255),
    CreationDate DATE,
    Medium VARCHAR(100),
    ImageURL VARCHAR(255),
    ArtistID INT,
    FOREIGN KEY (ArtistID) REFERENCES Artist(ArtistID)
);

CREATE TABLE Artist (
    ArtistID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    Biography VARCHAR(255),
    BirthDate DATE,
    Nationality VARCHAR(100),
    Website VARCHAR(255),
    ContactInformation VARCHAR(255)
);

CREATE TABLE [User] (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    FirstName VARCHAR(100),
    LastName VARCHAR(100),
    DateOfBirth DATE,
    ProfilePicture VARCHAR(255)
);

CREATE TABLE Gallery (
    GalleryID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    Description VARCHAR(255),
    Location VARCHAR(255),
    Curator INT,
    OpeningHours VARCHAR(100),
    FOREIGN KEY (Curator) REFERENCES Artist(ArtistID) 
);

CREATE TABLE User_Favorite_Artwork (
    UserID INT,
    ArtworkID INT,
    PRIMARY KEY (UserID, ArtworkID),
    FOREIGN KEY (UserID) REFERENCES [User](UserID) ,
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID) 
);

CREATE TABLE Artwork_Gallery (
    ArtworkID INT,
    GalleryID INT,
    PRIMARY KEY (ArtworkID, GalleryID),
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID) ,
    FOREIGN KEY (GalleryID) REFERENCES Gallery(GalleryID) 
);

-- Inserting values 
INSERT INTO Artist (Name, Biography, BirthDate, Nationality, Website, ContactInformation)
VALUES 
('Rishabh Dhawad', 'A Indain painter', '1881-10-25', 'Indian', 'http://example.com', 'rishabhD@mail.com'),
('Raman Dixit', 'A Mexican painter ', '1907-07-06', 'Mexican', 'http://example.com', 'ramanD@mail.com'),
('Sam Patel', 'A Canadian painter ', '1853-03-30', 'Canadian', 'http://example.com', 'samP@mail.com');


INSERT INTO Artwork (Title, Description, CreationDate, Medium, ImageURL, ArtistID)
VALUES 
('The Scenery', 'A small size scenery', '1937-06-01', 'Oil on canvas', 'http://example1.com', 1),
('The Lions', 'A double self-portrait of lions', '1939-01-01', 'Oil on canvas', 'http://example2.com', 2),
('The Starry Night', 'An iconic starry night', '1889-06-01', 'Oil on canvas', 'http://example3.com', 3);


INSERT INTO [User] (Username, Password, Email, FirstName, LastName, DateOfBirth, ProfilePicture)
VALUES 
('ishan_choudhry', 'password123', 'ishanC@mail.com', 'Ishan', 'Choudhry', '1990-05-15', 'http://www.artgallery.com/example1.jpg'),
('satyam_katiyar', 'sPassword!', 'satyamK@mail.com', 'Satyam', 'Katiyar', '1985-08-22', 'http://www.artgallery.com/example2.jpg');


INSERT INTO Gallery (Name, Description, Location, Curator, OpeningHours)
VALUES 
('The Rishabho Museum', 'A museum dedicated to the works of Rishabh Dhawad', 'Barcelona, Spain', 1, '9:00 AM - 6:00 PM'),
('Raman Kahlo Museum', 'A museum dedicated to Raman Dixit', 'Mexico City, Mexico', 2, '10:00 AM - 5:00 PM'),
('Sam Bhai Museum', 'A museum dedicated to Sam Patel', 'UttarPradesh, India', 3, '9:00 AM - 5:00 PM');

INSERT INTO User_Favorite_Artwork (UserID, ArtworkID)
VALUES 
(1, 1),  
(1, 3),  
(2, 2);  

INSERT INTO Artwork_Gallery (ArtworkID, GalleryID)
VALUES 
(1, 1), 
(2, 2),  
(3, 3),  
(1, 3);  

SELECT * FROM Artist;
SELECT * FROM Artwork;
SELECT * FROM Artwork_Gallery;
SELECT * FROM Gallery;
SELECT * FROM [User];
SELECT * FROM User_Favorite_Artwork;



