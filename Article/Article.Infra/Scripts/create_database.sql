CREATE TABLE Users (
  [User_ID] [uniqueidentifier] NOT NULL PRIMARY KEY,  
  [Name] [varchar](15) NOT NULL,
  [Middle_Name] [varchar](15) NOT NULL
);

CREATE TABLE Articles  (
  [Article_ID] [uniqueidentifier] NOT NULL PRIMARY KEY,  
  [Name] [varchar](100) NOT NULL,
);

CREATE TABLE Likes (
[Like_ID] [uniqueidentifier] NOT NULL PRIMARY KEY,  
[Liked] [bit] NOT NULL DEFAULT 0,
[User_ID] [uniqueidentifier] NOT NULL ,
[Article_ID] [uniqueidentifier] NOT NULL ,
FOREIGN KEY ([User_ID]) REFERENCES Users([User_ID]),
FOREIGN KEY ([Article_ID]) REFERENCES Articles([Article_ID])
);

