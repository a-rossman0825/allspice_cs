CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';


CREATE TABLE recipes(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  title VARCHAR(255) NOT NULL,
  instructions VARCHAR(5000),
  img VARCHAR(1000) NOT NULL,
  category ENUM('breakfast', 'lunch', 'dinner', 'snack', 'dessert'),
  creator_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
  ) default charset utf8mb4;

  SELECT
  recipes.*,
  accounts.*
  FROM recipes
  JOIN accounts ON recipes.creator_id = accounts.id
  WHERE recipes.id = 1;

  INSERT INTO
  recipes (
    title,
    instructions,
    img,
    category,
    creator_id
    )
  VALUES
  (
    @title,
    @instructions, 
    @imgUrl, 
    @category, 
    @creatorId
    );

  SELECT
    recipes.*,
    accounts.*
  FROM recipes
  JOIN accounts ON recipes.creator_id = accounts.id
  WHERE recipes.id = LAST_INSERT_ID();