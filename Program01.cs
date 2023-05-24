using System;
using System.Collections.Generic;

class Program
{
    class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Size { get; set; }

        public Product(string name, int price, int stock, string size)
        {
            Name = name;
            Price = price;
            Stock = stock;
            Size = size;
        }

        public override string ToString()
        {
            return string.Format("{0,-20} {1,-10} {2,-10}", Name, "Rp" + Price, Stock, Size);
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Selamat datang di TOKO BAJUKU!");
        Console.WriteLine("Silakan pilih jenis baju yang ingin dibeli:");
        Console.WriteLine("1. Kemeja");
        Console.WriteLine("2. Kaos");
        Console.WriteLine("3. Celana");
        Console.Write("Masukkan pilihan Anda (1-3): ");

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            if (choice == 1)
            {
                Console.WriteLine("Anda memilih kemeja.");
                Console.WriteLine("Pilih salah satu: ");
                List<Product> products = new List<Product>();
                products.Add(new Product("1. Kemeja Denim", 305000, 10, "M"));
                products.Add(new Product("2. Kemeja Batik", 209000, 15, "S"));
                products.Add(new Product("3. Kemeja Polos Hitam", 310000, 50, "L"));
                products.Add(new Product("4. Kemeja Polos Putih", 201000, 71, "M"));
                // Display available products
                Console.WriteLine("Product\t\tPrice\tStock");
                Console.WriteLine("-----------------------------------");
                foreach (Product product in products)
                {
                    Console.WriteLine(product.ToString());
                }
                Console.Write("Masukkan pilihan Anda (1-4): ");

                if (int.TryParse(Console.ReadLine(), out int shirtChoice) && shirtChoice >= 1 && shirtChoice <= products.Count)
                {
                    Product selectedProduct = products[shirtChoice - 1];
                    Console.WriteLine("Anda memilih: " + selectedProduct.Name);
                    Console.Write("Masukkan jumlah kemeja yang ingin dibeli: ");

                    if (int.TryParse(Console.ReadLine(), out int quantity))
                    {
                        if (quantity <= selectedProduct.Stock)
                        {
                            int total = quantity * selectedProduct.Price;
                            if (quantity >= 7)
                            {
                                total -= 10000;
                                Console.WriteLine("Diskon: 10000");
                            }
                            Console.WriteLine("Total harga: " + total);
                            Console.WriteLine("Total pembayaran: " + total);

                            Console.Write("Masukkan jumlah uang yang dibayarkan: ");
                            if (int.TryParse(Console.ReadLine(), out int paymentAmount))
                            {
                                int change = paymentAmount - total;
                                if (change >= 0)
                                {
                                    Console.WriteLine("Kembalian: " + change);
                                }
                                else
                                {
                                    Console.WriteLine("Jumlah uang yang dibayarkan tidak cukup.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Jumlah uang yang dibayarkan tidak valid.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Jumlah kemeja yang diminta melebihi stok yang tersedia.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Jumlah kemeja tidak valid.");
                    }
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid.");
                }
            }
            else if (choice == 2)
            {
                Console.WriteLine("Anda memilih kaos.");
                Console.WriteLine("Pilih salah satu: ");
                List<Product> products = new List<Product>();
                products.Add(new Product("1. Kaos lengan panjang putih", 295000, 20, "L"));
                products.Add(new Product("2. Kaos lengan panjang hitam", 200000, 15, "M"));
                products.Add(new Product("3. Kaos lengan pendek hitam", 153000, 23, "L"));
                products.Add(new Product("4. Kaos lengan pendek putih", 190000, 45, "M"));
                // Display available products
                Console.WriteLine("Product\t\tPrice\tStock");
                Console.WriteLine("-----------------------------------");
                foreach (Product product in products)
                {
                    Console.WriteLine(product.ToString());
                }
                Console.Write("Masukkan pilihan Anda (1-4): ");

                if (int.TryParse(Console.ReadLine(), out int shirtChoice) && shirtChoice >= 1 && shirtChoice <= products.Count)
                {
                    Product selectedProduct = products[shirtChoice - 1];
                    Console.WriteLine("Anda memilih: " + selectedProduct.Name);
                    Console.Write("Masukkan jumlah kaos yang ingin dibeli: ");

                    if (int.TryParse(Console.ReadLine(), out int quantity))
                    {
                        if (quantity <= selectedProduct.Stock)
                        {
                            int total = quantity * selectedProduct.Price;
                            if (quantity >= 7)
                            {
                                total -= 10000;
                                Console.WriteLine("Diskon: 10000");
                            }
                            Console.WriteLine("Total harga: " + total);
                            Console.WriteLine("Total pembayaran: " + total);

                            Console.Write("Masukkan jumlah uang yang dibayarkan: ");
                            if (int.TryParse(Console.ReadLine(), out int paymentAmount))
                            {
                                int change = paymentAmount - total;
                                if (change >= 0)
                                {
                                    Console.WriteLine("Kembalian: " + change);
                                }
                                else
                                {
                                    Console.WriteLine("Jumlah uang yang dibayarkan tidak cukup.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Jumlah uang yang dibayarkan tidak valid.");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Jumlah kaos yang diminta melebihi stok yang tersedia.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Jumlah kaos tidak valid.");
                    }
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid.");
                }
            }
            else if (choice == 3)
            {
                Console.WriteLine("Anda memilih celana.");
                Console.WriteLine("Pilih salah satu: ");
                List<Product> products = new List<Product>();
                products.Add(new Product("1. Celana Kulot", 105000, 22, "26"));
                products.Add(new Product("2. Celana Jeans", 309000, 15, "28"));
                products.Add(new Product("3. Celana Legging", 75000, 67, "24"));
                products.Add(new Product("4. Celana Joger", 102000, 12, "32"));
                // Display available products
                Console.WriteLine("Product\t\tPrice\tStock");
                Console.WriteLine("-----------------------------------");
                foreach (Product product in products)
                {
                    Console.WriteLine(product.ToString());
                }
                Console.Write("Masukkan pilihan Anda (1-4): ");

                if (int.TryParse(Console.ReadLine(), out int pantsChoice) && pantsChoice >= 1 && pantsChoice <= products.Count)
                {
                    Product selectedProduct = products[pantsChoice - 1];
                    Console.WriteLine("Anda memilih: " + selectedProduct.Name);
                    Console.Write("Masukkan jumlah celana yang ingin dibeli: ");

                    if (int.TryParse(Console.ReadLine(), out int quantity))
                    {
                        if (quantity <= selectedProduct.Stock)
                        {
                            int total = quantity * selectedProduct.Price;
                            if (quantity >= 7)
                            {
                                total -= 10000;
                                Console.WriteLine("Diskon: 10000");
                            }
                            Console.WriteLine("Total harga: " + total);
                            Console.WriteLine("Total pembayaran: " + total);

                            Console.Write("Masukkan jumlah uang yang dibayarkan: ");
                            if (int.TryParse(Console.ReadLine(), out int paymentAmount))
                            {
                                int change = paymentAmount - total;
                                if (change >= 0)
                                {
                                    Console.WriteLine("Kembalian: " + change);
                                }
                                else
                                {
                                    Console.WriteLine("Jumlah uang yang dibayarkan tidak cukup.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Jumlah uang yang dibayarkan tidak valid.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Jumlah celana yang diminta melebihi stok yang tersedia.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Jumlah celana tidak valid.");
                    }
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid.");
                }
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid.");
            }
        }
        else
        {
            Console.WriteLine("Pilihan tidak valid.");
        }



       
        bool ulangi = true;
        Console.Write("Apakah Anda Akan Memesan Lagi [Y/N] ?/n ");
        string jawab = Console.ReadLine();
        if (ulangi = (jawab == "Y" || jawab == "y" || jawab == "Yes" || jawab == "YES" || jawab == "yes"))
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Selamat datang di TOKO Bajuku!");

            do
            {
                Console.WriteLine("Silakan pilih jenis baju yang ingin dibeli:");
                Console.WriteLine("1. Kemeja");
                Console.WriteLine("2. Kaos");
                Console.WriteLine("3. Celana");
                Console.Write("Masukkan pilihan Anda (1-3): ");

                if (int.TryParse(Console.ReadLine(), out int ch))
                {
                    if (choice == 1)
                    {
                        Console.WriteLine("Anda memilih kemeja.");
                        Console.WriteLine("Pilih salah satu: ");
                        List<Product> products = new List<Product>();
                        products.Add(new Product("1. Kemeja Denim", 305000, 10, "M"));
                        products.Add(new Product("2. Kemeja Batik", 209000, 15, "S"));
                        products.Add(new Product("3. Kemeja Polos Hitam", 310000, 50, "L"));
                        products.Add(new Product("4. Kemeja Polos Putih", 201000, 71, "M"));
                        // Display available products
                        Console.WriteLine("Product\t\tPrice\tStock");
                        Console.WriteLine("-----------------------------------");
                        foreach (Product product in products)
                        {
                            Console.WriteLine(product.ToString());
                        }
                        Console.Write("Masukkan pilihan Anda (1-4): ");

                        if (int.TryParse(Console.ReadLine(), out int shirtChoice) && shirtChoice >= 1 && shirtChoice <= products.Count)
                        {
                            Product selectedProduct = products[shirtChoice - 1];
                            Console.WriteLine("Anda memilih: " + selectedProduct.Name);
                            Console.Write("Masukkan jumlah kemeja yang ingin dibeli: ");

                            if (int.TryParse(Console.ReadLine(), out int quantity))
                            {
                                if (quantity <= selectedProduct.Stock)
                                {
                                    int total = quantity * selectedProduct.Price;
                                    if (quantity >= 7)
                                    {
                                        total -= 10000;
                                        Console.WriteLine("Diskon: 10000");
                                    }
                                    Console.WriteLine("Total harga: " + total);
                                    Console.WriteLine("Total pembayaran: " + total);

                                    Console.Write("Masukkan jumlah uang yang dibayarkan: ");
                                    if (int.TryParse(Console.ReadLine(), out int paymentAmount))
                                    {
                                        int change = paymentAmount - total;
                                        if (change >= 0)
                                        {
                                            Console.WriteLine("Kembalian: " + change);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Jumlah uang yang dibayarkan tidak cukup.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Jumlah uang yang dibayarkan tidak valid.");
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("Jumlah kemeja yang diminta melebihi stok yang tersedia.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Jumlah kemeja tidak valid.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Pilihan tidak valid.");
                        }
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Anda memilih kaos.");
                        Console.WriteLine("Pilih salah satu: ");
                        List<Product> products = new List<Product>();
                        products.Add(new Product("1. Kaos lengan panjang putih", 295000, 20, "L"));
                        products.Add(new Product("2. Kaos lengan panjang hitam", 200000, 15, "M"));
                        products.Add(new Product("3. Kaos lengan pendek hitam", 153000, 23, "L"));
                        products.Add(new Product("4. Kaos lengan pendek putih", 190000, 45, "M"));
                        // Display available products
                        Console.WriteLine("Product\t\tPrice\tStock");
                        Console.WriteLine("-----------------------------------");
                        foreach (Product product in products)
                        {
                            Console.WriteLine(product.ToString());
                        }
                        Console.Write("Masukkan pilihan Anda (1-4): ");

                        if (int.TryParse(Console.ReadLine(), out int shirtChoice) && shirtChoice >= 1 && shirtChoice <= products.Count)
                        {
                            Product selectedProduct = products[shirtChoice - 1];
                            Console.WriteLine("Anda memilih: " + selectedProduct.Name);
                            Console.Write("Masukkan jumlah kaos yang ingin dibeli: ");

                            if (int.TryParse(Console.ReadLine(), out int quantity))
                            {
                                if (quantity <= selectedProduct.Stock)
                                {
                                    int total = quantity * selectedProduct.Price;
                                    if (quantity >= 7)
                                    {
                                        total -= 10000;
                                        Console.WriteLine("Diskon: 10000");
                                    }
                                    Console.WriteLine("Total harga: " + total);
                                    Console.WriteLine("Total pembayaran: " + total);

                                    Console.Write("Masukkan jumlah uang yang dibayarkan: ");
                                    if (int.TryParse(Console.ReadLine(), out int paymentAmount))
                                    {
                                        int change = paymentAmount - total;
                                        if (change >= 0)
                                        {
                                            Console.WriteLine("Kembalian: " + change);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Jumlah uang yang dibayarkan tidak cukup.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Jumlah uang yang dibayarkan tidak valid.");
                                    }



                                }
                                else
                                {
                                    Console.WriteLine("Jumlah kaos yang diminta melebihi stok yang tersedia.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Jumlah kaos tidak valid.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Pilihan tidak valid.");
                        }
                    }
                    else if (choice == 3)
                    {
                        Console.WriteLine("Anda memilih celana.");
                        Console.WriteLine("Pilih salah satu: ");
                        List<Product> products = new List<Product>();
                        products.Add(new Product("1. Celana Kulot", 105000, 22, "26"));
                        products.Add(new Product("2. Celana Jeans", 309000, 15, "28"));
                        products.Add(new Product("3. Celana Legging", 75000, 67, "24"));
                        products.Add(new Product("4. Celana Joger", 102000, 12, "32"));
                        // Display available products
                        Console.WriteLine("Product\t\tPrice\tStock");
                        Console.WriteLine("-----------------------------------");
                        foreach (Product product in products)
                        {
                            Console.WriteLine(product.ToString());
                        }
                        Console.Write("Masukkan pilihan Anda (1-4): ");

                        if (int.TryParse(Console.ReadLine(), out int pantsChoice) && pantsChoice >= 1 && pantsChoice <= products.Count)
                        {
                            Product selectedProduct = products[pantsChoice - 1];
                            Console.WriteLine("Anda memilih: " + selectedProduct.Name);
                            Console.Write("Masukkan jumlah celana yang ingin dibeli: ");

                            if (int.TryParse(Console.ReadLine(), out int quantity))
                            {
                                if (quantity <= selectedProduct.Stock)
                                {
                                    int total = quantity * selectedProduct.Price;
                                    if (quantity >= 7)
                                    {
                                        total -= 10000;
                                        Console.WriteLine("Diskon: 10000");
                                    }
                                    Console.WriteLine("Total harga: " + total);
                                    Console.WriteLine("Total pembayaran: " + total);

                                    Console.Write("Masukkan jumlah uang yang dibayarkan: ");
                                    if (int.TryParse(Console.ReadLine(), out int paymentAmount))
                                    {
                                        int change = paymentAmount - total;
                                        if (change >= 0)
                                        {
                                            Console.WriteLine("Kembalian: " + change);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Jumlah uang yang dibayarkan tidak cukup.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Jumlah uang yang dibayarkan tidak valid.");
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("Jumlah celana yang diminta melebihi stok yang tersedia.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Jumlah celana tidak valid.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Pilihan tidak valid.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Pilihan tidak valid.");
                    }
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid.");
                }
                


            
                if (jawab == "n" || jawab == "N") {
                        ulangi = false;
                } 
            
            } while (ulangi);

            Console.WriteLine("Oke, Terimakasih...");

        }  

    }
}
