using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

class Program
{
    public class Penjualan
    {
        private const string DbConnectionString = "Data Source=product.db;Version=3;";

        public static void Main()
        {
            // Membuat tabel product jika belum ada
            CreateTable();

            bool buyer = false;

            Console.WriteLine("Selamat datang di TOKO BAJUKU!");
            Console.WriteLine("Login sebagai:");
            Console.WriteLine("1. Pembeli");
            Console.WriteLine("2. Admin");
            Console.Write("Masukkan pilihan Anda (1/2): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                // Menampilkan semua item product
                Console.WriteLine("Daftar product:");
                DisplayItems();

                if (choice == 1)
                {
                    Console.WriteLine("Sebagai Pembeli");
                    buyer = true;
                }

                if (choice == 2)
                {
                    Console.WriteLine("Sebagai Admin");
                    Console.Write("Set initial product (y/n): ");
                    string? y = Console.ReadLine();
                    if (y == "y") {
                        initialProduct();
                        Console.WriteLine("Product added");
                    }
                }

            }
        }

        private static void CreateTable()
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand(
                    "CREATE TABLE IF NOT EXISTS product (id INTEGER PRIMARY KEY, name TEXT, price INTEGER, stock INTEGER, size TEXT)",
                    connection);
                command.ExecuteNonQuery();
            }
        }

        private static void AddItem(string name, int price, int stock, string size)
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand("INSERT INTO product (name, price, stock, size) VALUES (@name, @price, @stock, @size)",
                    connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@stock", stock);
                command.Parameters.AddWithValue("@size", size);
                command.ExecuteNonQuery();
            }
        }

        private static void UpdateItemStock(int id, int stock)
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

        private static void DeleteItem(int id)
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand("DELETE FROM product WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        private static void DisplayItems()
        {
            using (var connection = new SQLiteConnection(DbConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand("SELECT * FROM product", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var itemId = reader.GetInt32(0);
                        var itemName = reader.GetString(1);
                        var itemPrice = reader.GetInt32(2);
                        var itemStock = reader.GetInt32(3);
                        var itemSize = reader.GetString(4);
                        Console.WriteLine($"ID: {itemId}, Name: {itemName}, Price: {itemPrice}, Stock: {itemStock}");
                    }
                }
            }
        }

        private static void initialProduct()
        {
            // Menambahkan data product
            AddItem("Kemeja Denim", 305000, 10, "M");
            AddItem("Kemeja Batik", 209000, 15, "S");
            AddItem("Kemeja Polos Hitam", 310000, 50, "L");
            AddItem("Kemeja Polos Putih", 201000, 71, "M");
        }
    }
}