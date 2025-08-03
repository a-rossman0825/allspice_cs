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

CREATE TABLE ingredients(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name VARCHAR(255) NOT NULL,
  quantity VARCHAR(255) NOT NULL,
  recipe_id INT NOT NULL,
  FOREIGN KEY (recipe_id) REFERENCES recipes(id) ON DELETE CASCADE
  ) default charset utf8mb4;

  DROP TABLE ingredient;


  SELECT
    ingredients.*
    FROM ingredients
    WHERE ingredients.id = 2;

  SELECT
      recipes.id AS recipeId,
      ingredients.*,
      recipes.*
  FROM ingredients
  JOIN recipes ON ingredients.recipe_id = recipes.id
  WHERE recipe_id = 1;

  SELECT *
  FROM ingredients
  WHERE recipe_id = 45;

  DELETE FROM
    ingredients
    WHERE id = 17 LIMIT 1;

CREATE TABLE favorites(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  recipe_id INT NOT NULL,
  account_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (account_id) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (recipe_id) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8mb4;


SELECT
    favorites.id AS favoriteId,
    favorites.account_id,
    recipes.*
  FROM favorites
  JOIN recipes ON favorites.recipe_id = recipe.id
  WHERE account_id = '//<Insert Account Id for testing>//';

SELECT 
    favorites.id AS favoriteId
    favorites.account_id,
    recipes.*,
    accounts.*
  FROM favorites
  JOIN albums ON favorites.recipe_id = recipes.id
  JOIN accounts ON recipes.creator_id = accounts.id
  WHERE account_id = '//<Insert Account Id for testing>//';

INSERT INTO favorites (recipe_id, account_id)
    VALUES (2, '//<Insert Account Id for testing>//');