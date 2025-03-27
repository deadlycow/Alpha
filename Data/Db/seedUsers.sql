-- Skapa användare
INSERT INTO Users (FirstName, LastName, Email, Password)
VALUES 
    ('Andreas', 'Karlsson', 'Andreas@domain.com', 'Bytmig123!'),
    ('Oskar', 'Oskarsson', 'Oskar@domain.com', 'Bytmig123!'),
    ('Lina', 'Linsson', 'Lina@domain.com', 'Bytmig123!');

-- Hämta rollen "User"
DECLARE @RoleId INT;
SELECT @RoleId = Id FROM Roles WHERE Name = 'User';

-- Tilldela rollen "User" till de nya användarna
INSERT INTO UserRoles (UserId, RoleId)
VALUES 
    ((SELECT Id FROM Users WHERE Email = 'Andreas@domain.com'), @RoleId),
    ((SELECT Id FROM Users WHERE Email = 'Oskar@domain.com'), @RoleId),
    ((SELECT Id FROM Users WHERE Email = 'Lina@domain.com'), @RoleId);


-- Skapa Clients
INSERT INTO Client (Name)
VALUES 
    ('Google, Inc'), 
    ('Facebook, Inc'), 
    ('ArtTemplate, Inc'), 
    ('Slack Technologies, Inc'),
    ('GitLab, Inc')

--delete from AspNetUsers
--where JobTitle is null