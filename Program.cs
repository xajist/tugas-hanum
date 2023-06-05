using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

class Program {
    public class Penjualan {

        public static void Main() {
            // Membuat tabel product jika belum ada
            ProgramDB.CreateTable();

            Console.Clear();
            Print.G("Selamat datang di TOKO BAJUKU!");
            Print.B("Login sebagai:");
            Print.Y("1. Pembeli");
            Print.Y("2. Kasir");
            Print.Y("3. Admin");
            Print.G("Masukkan pilihan Anda (1-3): ");

            if (int.TryParse(Console.ReadLine(), out int choice)) {
                if (choice == 1) { // pembeli
                    Purchase();
                }

                if (choice == 2) { // kasir

                }

                if (choice == 3) { // admin
                    Admin();
                }

            } else {
                Print.R("Input tidak valid.");
            }
        }

        public static void Purchase() {
            Console.Clear();
            Print.G("------- Pembelian -------");
            Print.G("Pilih kategori:");
            Print.B("No\tName");
            Category[] categories = ProgramDB.getAllCategory();
            int i = 1;
            foreach (var category in categories) {
                Print.Y($"{i}\t{category.Name}");
                i++;
            }
                    
            Print.G("Masukkan pilihan Anda (berdasarkan No): ");
            if (int.TryParse(Console.ReadLine(), out int categoryChoice)) {
                if (categoryChoice >= 1 && categoryChoice <= categories.Length) {
                    Category selectedCategory = categories[categoryChoice - 1];
                    Print.G("Anda memilih: " + selectedCategory.Name);

                    SelectProducts(selectedCategory);
                } else {
                    Purchase();
                }
            } else {
                Purchase();
            }
        }

        public static void SelectProducts(Category category) {
            Console.Clear();
            Print.G("------- Pembelian -------");
            Print.G($"Pilih produk ({category.Name}):");
            Print.B("No\tHarga\t\tUkuran\tStok\tName");
            Product[] products = ProgramDB.getProductByCategoryId(category.Id);
            int i = 1;
            foreach(var product in products) {
                string formattedPrice = "Rp. " + product.Price.ToString("N0");
                Print.Y($"{i}\t{formattedPrice}\t{product.Size}\t{product.Stock}\t{product.Name}");
                i++;
            }

            Print.G("Masukkan pilihan Anda (berdasarkan No): ");
            if (int.TryParse(Console.ReadLine(), out int productChoice)) {
                if (productChoice >= 1 && productChoice <= products.Length) {
                    Product selectedProduct = products[productChoice - 1];
                    Print.G("Anda memilih: " + selectedProduct.Name);

                } else {
                    SelectProducts(category);
                }
            } else {
                SelectProducts(category);
            }
        }

        public static void Admin() {
            Console.Write("Set initial product (y/n): ");
            string? y = Console.ReadLine();
            if (y == "y") {
                ProgramDB.initialProduct();
                Console.WriteLine("Product added");
            }
            Console.Clear();
            Console.WriteLine("Daftar product:");
            ProgramDB.DisplayProduct();
        }
    }


    public class ProgramDB {
        private const string DbConnectionString = "Data Source=program.db;Version=3;";

        public static void CreateTable() {
            using (var connection = new SQLiteConnection(DbConnectionString)) {
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

        public static void AddProduct(string name, int category_id, int price, int stock, string size) {
            using (var connection = new SQLiteConnection(DbConnectionString)) {
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

        public static void UpdateProductStock(int id, int stock) {
            using (var connection = new SQLiteConnection(DbConnectionString)) {
                connection.Open();

                var command = new SQLiteCommand("UPDATE product SET stock = @stock WHERE id = @id",
                    connection);
                command.Parameters.AddWithValue("@stock", stock);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteProduct(int id) {
            using (var connection = new SQLiteConnection(DbConnectionString)) {
                connection.Open();

                var command = new SQLiteCommand("DELETE FROM product WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public static void DisplayProduct() {
            using (var connection = new SQLiteConnection(DbConnectionString)) {
                connection.Open();

                var command = new SQLiteCommand("SELECT p.id, p.name, c.name AS CategoryName, p.price, p.stock FROM product p JOIN category c ON p.category_id = c.id", connection);
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
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

        public static Product[] getProductByCategoryId(int categoryId) {
            List<Product> products = new List<Product>();

            using (var connection = new SQLiteConnection(DbConnectionString)){
                connection.Open();

                var command = new SQLiteCommand(
                    "SELECT p.id, p.name, c.name AS CategoryName, p.price, p.stock, p.size FROM product p JOIN category c ON p.category_id = c.id WHERE p.category_id = @category_id", 
                    connection);
                command.Parameters.AddWithValue("@category_id", categoryId);

                using (var reader = command.ExecuteReader()){
                    while (reader.Read()){
                        var productID = reader.GetInt32(0);
                        var productName = reader.GetString(1);
                        var productCategoryName = reader.GetString(2);
                        var productPrice = reader.GetInt32(3);
                        var productStock = reader.GetInt32(4);
                        var productSize = reader.GetString(5);

                        Product product = new Product(productID, productName, productCategoryName, productPrice, productStock, productSize);
                        products.Add(product);
                    }
                }
            }

            return products.ToArray();
        }

        public static void AddCategory(string name) {
            using (var connection = new SQLiteConnection(DbConnectionString)) {
                connection.Open();

                var command = new SQLiteCommand("INSERT INTO category (name) VALUES (@name)",
                    connection);
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateCategory(int id, string name) {
            using (var connection = new SQLiteConnection(DbConnectionString)) {
                connection.Open();

                var command = new SQLiteCommand("UPDATE category SET name = @name WHERE id = @id",
                    connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteCategory(int id) {
            using (var connection = new SQLiteConnection(DbConnectionString)) {
                connection.Open();

                var command = new SQLiteCommand("DELETE FROM category WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public static Category[] getAllCategory() {
            List<Category> categories = new List<Category>();

            using (var connection = new SQLiteConnection(DbConnectionString)) {
                connection.Open();

                var command = new SQLiteCommand("SELECT * FROM category", connection);
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        var categoryId = reader.GetInt32(0);
                        var categoryName = reader.GetString(1);

                        Category category = new Category(categoryId, categoryName);
                        categories.Add(category);
                    }
                }
            }

            return categories.ToArray();
        }

        public static Category getCategoryById(int categoryId) {
            using (var connection = new SQLiteConnection(DbConnectionString)){
                connection.Open();

                var command = new SQLiteCommand("SELECT * FROM category WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", categoryId);

                using (var reader = command.ExecuteReader()){
                    if (reader.Read()){
                        var categoryName = reader.GetString(1);

                        Category category = new Category(categoryId, categoryName);
                        return category;
                    }
                }
            }

            return null; // Mengembalikan null jika kategori tidak ditemukan
        }

        public static string getCategoryNameById(int categoryId){
            using (var connection = new SQLiteConnection(DbConnectionString)){
                connection.Open();

                var command = new SQLiteCommand("SELECT name FROM category WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", categoryId);

                var categoryName = (string)command.ExecuteScalar();

                return categoryName;
            }
        }

        public static void initialProduct(){
            // Menambahkan data product
            AddProduct("Kemeja Denim", 3, 305000, 10, "M");
            AddProduct("Kemeja Batik", 3, 209000, 15, "S");
            AddProduct("Kemeja Polos Hitam", 3, 310000, 50, "L");
            AddProduct("Kemeja Polos Putih", 3, 201000, 71, "M");
            AddProduct("Kaos Lengan Panjang Hitam", 1, 295000, 140, "L");
            AddProduct("Kaos Bergambar", 1, 112000, 15, "M");
            AddProduct("Kaos Lengan Pendek Putih", 1, 99000, 150, "S");
            AddProduct("Celana Joger", 2, 305000, 10, "M");
            AddProduct("Celana Legging", 2, 99000, 60, "M");
            AddProduct("Celana Jeans", 2, 214000, 90, "M");
            AddProduct("Celana Kulot", 2, 160000, 351, "S");


            // Menambahkan data kategory
            AddCategory("Kaos");
            AddCategory("Celana");
            AddCategory("Kemeja");
        }
    }

    public class Category {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category(int id, string name) {
            Id = id;
            Name = name;
        }
    }

    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Size { get; set; }

        public Product(int id, string name, string category, int price, int stock, string size){
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            Stock = stock;
            Size = size;
        }
    }




    public static class Print {
        // Yellow
        public static void Y(string message) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // Red
        public static void R(string message) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // Green
        public static void G(string message) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // Blue
        public static void B(string message) {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
