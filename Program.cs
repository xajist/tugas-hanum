using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

class Program
{
    public class Penjualan
    {
        public static void Main()
        {
            // Membuat tabel product jika belum ada
            ProgramDB.CreateTable();

            Console.WriteLine("Selamat datang di TOKO BAJUKU!");
            Console.WriteLine("Login sebagai:");
            Console.WriteLine("1. Pembeli");
            Console.WriteLine("2. Admin");
            Console.Write("Masukkan pilihan Anda (1/2): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 1)
                {
                    Console.WriteLine("Sebagai Pembeli");
                    // Menampilkan semua item product
                    Console.WriteLine("Daftar product:");
                    ProgramDB.DisplayProduct();
                }

                if (choice == 2)
                {
                    Console.WriteLine("Sebagai Admin");
                    Console.Write("Set initial product (y/n): ");
                    string? y = Console.ReadLine();
                    if (y == "y") {
                        ProgramDB.initialProduct();
                        Console.WriteLine("Product added");
                    }
                }

            }
        }
    }


    public class ProgramDB {
        private const string DbConnectionString = "Data Source=program.db;Version=3;";

        public static void CreateTable()
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                // create table product
                var product = new SQLiteCommand(
                    "CREATE TABLE IF NOT EXISTS product (id INTEGER PRIMARY KEY, name TEXT, category_id INTEGER,  price INTEGER, stock INTEGER, size TEXT)",
                    connection);
                product.ExecuteNonQuery();

                // create table category
                var category = new SQLiteCommand(
                    "CREATE TABLE IF NOT EXISTS category (id INTEGER PRIMARY KEY, name TEXT)",
                    connection);
                category.ExecuteNonQuery();
            }
        }

        public static void AddProduct(string name, int category_id, int price, int stock, string size)
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand("INSERT INTO product (name, category_id, price, stock, size) VALUES (@name, @category_id, @price, @stock, @size)",
                    connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@category_id", category_id);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@stock", stock);
                command.Parameters.AddWithValue("@size", size);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateProductStock(int id, int stock)
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand("UPDATE product SET stock = @stock WHERE id = @id",
                    connection);
                command.Parameters.AddWithValue("@stock", stock);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteProduct(int id)
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand("DELETE FROM product WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public static void DisplayProduct()
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand("SELECT p.id, p.name, c.name AS CategoryName, p.price, p.stock FROM product p JOIN category c ON p.category_id = c.id", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var productId = reader.GetInt32(0);
                        var productName = reader.GetString(1);
                        var productCategoryName = reader.GetString(2);
                        var productPrice = reader.GetInt32(3);
                        var productStock = reader.GetInt32(4);
                        Console.WriteLine($"ID: {productId}, Name: {productName}, Category: {productCategoryName}, Price: {productPrice}, Stock: {productStock}");
                    }
                }
            }
        }

        public static void AddCategory(string name)
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand("INSERT INTO category (name) VALUES (@name)",
                    connection);
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateCategory(int id, string name)
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand("UPDATE category SET name = @name WHERE id = @id",
                    connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteCategory(int id)
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand("DELETE FROM category WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public static Category[] getAllCategory() 
        {
            List<Category> categories = new List<Category>();

            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand("SELECT * FROM category", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var categoryId = reader.GetInt32(0);
                        var categoryName = reader.GetString(1);

                        Category category = new Category(categoryId, categoryName);
                        categories.Add(category);
                    }
                }
            }

            return categories.ToArray();
        }

        public static Category getCategoryById(int categoryId)
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand("SELECT * FROM category WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", categoryId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var categoryName = reader.GetString(1);

                        Category category = new Category(categoryId, categoryName);
                        return category;
                    }
                }
            }

            return null; // Mengembalikan null jika kategori tidak ditemukan
        }

        public static string getCategoryNameById(int categoryId)
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand("SELECT name FROM category WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", categoryId);

                var categoryName = (string)command.ExecuteScalar();

                return categoryName;
            }
        }

        public static void initialProduct()
        {
            // Menambahkan data product
            AddProduct("Kemeja Denim", 3, 305000, 10, "M");
            AddProduct("Kemeja Batik", 3, 209000, 15, "S");
            AddProduct("Kemeja Polos Hitam", 3, 310000, 50, "L");
            AddProduct("Kemeja Polos Putih", 3, 201000, 71, "M");


            // Menambahkan data kategory
            AddCategory("Kaos");
            AddCategory("Celana");
            AddCategory("Kemeja");
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Size { get; set; }

        public Product(string name, string category, int price, int stock, string size)
        {
            Name = name;
            Category = category;
            Price = price;
            Stock = stock;
            Size = size;
        }
    }
}
