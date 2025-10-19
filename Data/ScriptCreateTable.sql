CREATE TABLE users (
  id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(100),
  email VARCHAR(100) UNIQUE,
  password VARCHAR(255),
  role VARCHAR(50),
  created_at DATETIME DEFAULT GETDATE()
);

CREATE TABLE categories (
  id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(100),
  description TEXT
);

CREATE TABLE products (
  id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(100),
  category_id INT,
  price DECIMAL(10,2),
  qty INT,
  created_at DATETIME DEFAULT GETDATE(),
  FOREIGN KEY (category_id) REFERENCES categories(id)
);

CREATE TABLE invoices (
  id INT IDENTITY(1,1) PRIMARY KEY,
  user_id INT,
  invoice_date DATETIME DEFAULT GETDATE(),
  total_amount DECIMAL(10,2),
  FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE invoice_items (
  id INT IDENTITY(1,1) PRIMARY KEY,
  invoice_id INT,
  product_id INT,
  quantity INT,
  unit_price DECIMAL(10,2),
  FOREIGN KEY (invoice_id) REFERENCES invoices(id),
  FOREIGN KEY (product_id) REFERENCES products(id)
);
