using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Butiken
{
    class Program
    {
        static void Main(string[] args)
        {
            List <Garment> Garments = new List<Garment>();
            List<Garment> Basket = new List<Garment>();


            while (true)
            {
                Console.Clear();
                int UserMenyInput = StartMeny();

                switch (UserMenyInput)
                {
                    case 1:
                        bool redo = true;
                        while (redo)
                        {
                            Console.Clear();
                            Garments.Add(GarmentGeneratorUser());

                            Console.Clear();
                            Console.WriteLine("Would you like to add another Garment? y/n");
                            ConsoleKeyInfo key = Console.ReadKey();
                            Console.Clear();

                            if (key.KeyChar == 'y') { redo = true; }
                            else if (key.KeyChar == 'n') { redo = false; }
                            else
                            {
                                Console.WriteLine("Invalid key: Returning to main meny. ");
                                Thread.Sleep(1000);
                                redo = false;
                                Console.Clear();
                            }
                        }
                        break;
                    case 2:

                        redo = true;

                        while (redo)
                        {
                            Console.WriteLine("Please see the items below, what would you like to add to your basket?");
                            int ChosenGarment = BrowseAndShop(Garments);
                            Garment AddToBasket = Garments[ChosenGarment - 1];
                            Basket.Add(AddToBasket);
                            Garments.RemoveAt(ChosenGarment - 1);

                            Console.WriteLine("Would you like to add another Garment? y/n");
                            ConsoleKeyInfo key = Console.ReadKey();
                            Console.Clear();

                            if (key.KeyChar == 'y') { redo = true; }
                            else if (key.KeyChar == 'n') { redo = false; }
                            else
                            {
                                Console.WriteLine("Invalid key: Returning to main meny. ");
                                Thread.Sleep(1000);
                                redo = false;
                                Console.Clear();
                            }

                        }

                        break;
                    case 3:

                        redo = true;
                        while (redo)
                        {
                            int UserInput = CheckOut(Basket) - 2;
                            if (UserInput < -2)
                            {
                                Console.WriteLine("Invalid key: Returning to main meny. ");
                                Thread.Sleep(1000);
                                redo = false;
                                Console.Clear();
                            }
                            else if (UserInput == -2)
                            {
                                redo = false;
                            }
                            else if (UserInput == -1)
                            {
                                Console.Clear();
                                Console.WriteLine("Thank you for your purchase!");
                                Thread.Sleep(1000);
                                Console.Clear();
                                redo = false;
                                Basket = new List<Garment>();
                            }
                            else
                            {
                                Garment AddToGarments = Basket[UserInput];
                                Garments.Add(AddToGarments);
                                Basket.RemoveAt(UserInput);
                            }
                                
                        }
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }

        }
        public static int CheckOut(List<Garment> Basket)
        {
            Console.Clear();
            int number = 2;
            int TotalPrice = 0;
            int UserInput;

            foreach (Garment g in Basket)
            {
                Console.WriteLine(number + ") " + Enum.GetName(typeof(GarmentType), g.Type) + ", " + Enum.GetName(typeof(GarmentSize), g.Size) + ", " + Enum.GetName(typeof(GarmentColor), g.Color) + " - " + g.Price + ":-");
                TotalPrice = TotalPrice + g.Price;
                number++;
            }
            Console.WriteLine();
            Console.WriteLine($"Total price: {TotalPrice}:-");
            Console.WriteLine();
            Console.WriteLine($"0) Main Meny");
            Console.WriteLine($"1) Purchase");
            Console.WriteLine($"X) Remove respective item.");
            UserInput = GetInt32Input(0, number);

            return UserInput;

        }
        public static int BrowseAndShop(List<Garment> Garments)
        {
            Console.Clear();
            int number = 1;
            Console.WriteLine("What would you like to buy?");
            foreach (Garment g in Garments)
            {
                Console.WriteLine(number + ") " + Enum.GetName(typeof(GarmentType), g.Type) + ", " + Enum.GetName(typeof(GarmentSize), g.Size) + ", " + Enum.GetName(typeof(GarmentColor), g.Color) + " - " + g.Price + ":-");
                number++;
            }
            int UserInput = GetInt32Input(1, number);

            return UserInput;


        }
        public static Garment GarmentGeneratorUser()
        {

            int type = 1;
            int size = 1;
            int color = 1;
            Garment g = new Garment();

            Console.WriteLine("What Garment would you like to add?");
            foreach(string w in Enum.GetNames(typeof(GarmentType)))
            {
                Console.WriteLine($"{type}) {w}");
                type++;
            }
            g.Type = GetInt32Input(1,3);

            Console.WriteLine();
            Console.WriteLine("What Size?");
            foreach (string w in Enum.GetNames(typeof(GarmentSize)))
            {
                Console.WriteLine($"{size}) {w}");
                size++;
            }
            g.Size = GetInt32Input(1,5);

            Console.WriteLine();
            Console.WriteLine("What Color?");
            foreach (string w in Enum.GetNames(typeof(GarmentColor)))
            {
                Console.WriteLine($"{color}) {w}");
                color++;
            }
            g.Color = GetInt32Input(1, 5);

            Console.WriteLine();
            Console.WriteLine("What price?");
            g.Price = GetInt32Input(1, 99999);

            return g;

        }
        public static int StartMeny()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Add new Apparal. (Admin only)");
            Console.WriteLine("2) Browse and shop.");
            Console.WriteLine("3) Check out.");

            int MenySelection = GetInt32Input(1,3);

            return MenySelection;
        }
        public static int GetInt32Input(int minNumber,int maxNumber)
        {
            bool success = false;
            int input = 0;

            while (!success)
            {
                success = int.TryParse(Console.ReadLine(), out input);
                if (!success) { Console.WriteLine($"Error: UserInput must be a number between {minNumber}-{maxNumber}."); }
                else { success = true; if (input < minNumber || input > maxNumber) { Console.WriteLine($"Error: UserInput must be a number between {minNumber}-{maxNumber}."); success = false; } }
            }

            return input;
        }

    }

    public struct Garment
    {
        public int Type;
        public int Size;
        public int Color;
        public int Price;

        public Garment(int type, int size, int color, int price)
        {
            Type = type;
            Size = size;
            Color = color;
            Price = price;
        }
    }

    enum GarmentType
    {
        Shirt = 1,
        Pants,
        Shoes
    }

    enum GarmentSize
    {
        XS = 1,
        S,
        M,
        L,
        XL
    }

    enum GarmentColor
    {
        Red = 1,
        Green,
        Blue,
        Black,
        White
    }
}
