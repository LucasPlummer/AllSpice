CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        title VARCHAR(50) NOT NULL,
        instructions VARCHAR(500) NOT NULL,
        img VARCHAR(255) NOT NULL DEFAULT 'https://media.istockphoto.com/id/1366739498/photo/variety-of-vegetarian-foods-on-white-wooden-background.jpg?b=1&s=170667a&w=0&k=20&c=y0j8S5kCILfOev-GfAk8wmtjBoYvfYgwS4ozhQzyFfg=',
        category VARCHAR(25) NOT NULL DEFAULT 'misc',
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

INSERT INTO
    recipes (
        title,
        instructions,
        `img`,
        category,
        `creatorId`
    )
VALUES (
        'noodles',
        'eat them raw!',
        'https://images.unsplash.com/photo-1633352615955-f0c99e8b7e5a?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Nnx8bm9vZGxlfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60',
        'misc',
        '639cc49b47c35d04a02a81f0'
    );

DROP TABLE recipes 