-- USE YOUR-DB-HERE;
-- CREATE TABLE vaults (
--     id int NOT NULL AUTO_INCREMENT,
--     name VARCHAR(255) NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     userId VARCHAR(255),
--     INDEX userId (userId),  
--     PRIMARY KEY (id)
-- );
-- CREATE TABLE keeps (
-- --     id int NOT NULL AUTO_INCREMENT,
-- --     name VARCHAR(255) NOT NULL,
-- --     description VARCHAR(255) NOT NULL,
-- --     userId VARCHAR(255),
-- --     img VARCHAR(255),
-- --     isPrivate TINYINT,
-- --     views INT DEFAULT 0,
-- --     shares INT DEFAULT 0,
-- --     keeps INT DEFAULT 0,
-- --     INDEX userId (userId),
-- --     PRIMARY KEY (id)
-- );
-- CREATE TABLE vaultkeeps (
--     id int NOT NULL AUTO_INCREMENT,
--     vaultId int NOT NULL,
--     keepId int NOT NULL,
--     userId VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id),
--     INDEX (vaultId, keepId),
--     INDEX (userId),
--     FOREIGN KEY (vaultId)
--         REFERENCES vaults(id)
--         ON DELETE CASCADE,
--     FOREIGN KEY (keepId)
--         REFERENCES keeps(id)
--         ON DELETE CASCADE
-- )
-- INSERT INTO vaults
-- (name,description,userId)
-- VALUES
-- ("Ollie", "First Vault ; 9/5/2020", "superSecret1981") 
--  SELECT * FROM vaults;
-- -- USE THIS LINE FOR GET KEEPS BY VAULTID
-- SELECT 
-- k.*,
-- vk.id as vaultKeepId
-- FROM vaultkeeps vk
-- INNER JOIN keeps k ON k.id = vk.keepId 
-- WHERE (vaultId = @vaultId AND vk.userId = @userId)
-- INSERT INTO keeps
-- (name,description,userId,img,isPrivate,views,shares,keeps)
-- VALUES
-- ("Ollie", "It's 1:22PM ; 9/5/2020", "superSecret1981","https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcQiEdhq7a4aHGvv8FxjKUpAg_godVfcyLzHSg&usqp=CAU",0,0,0,0) 
-- INSERT INTO
--     vaultkeeps (vaultId, keepId, userId)
-- VALUES
--     (1, 7, "FCdFGsGmo17IltNjqAIHzVfqvzZPBGHH@clients") 

    SELECT * FROM vaults;

    -- -- USE THIS TO CLEAN OUT YOUR DATABASE
    -- DROP TABLE IF EXISTS vaultkeeps;
    -- DROP TABLE IF EXISTS vaults;
    -- DROP TABLE IF EXISTS keeps;
    -- DROP TABLE IF EXISTS users;