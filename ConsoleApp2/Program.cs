using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            DBConnect dbCon = new DBConnect();
            Console.WriteLine("This yo' phonebook motherfucker!" +
                Environment.NewLine + "1. Add some new motherfucker" + 
                Environment.NewLine + "2. Some motherfucker changed their number" +
                Environment.NewLine + "3. Search for a motherfucker");
            var ans = Console.ReadLine();
            int choice=0;
            if (int.TryParse(ans, out choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("What's this motherfuckers name?");
                        string name = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("What's this motherfuckers digits?");
                        string tel = Console.ReadLine();
                        dbCon.Insert(name, tel);
                        return;
                    case 2:
                       
                        Console.Clear();
                        Console.Write("Which motherfucker changed their number?");
                        string nameU = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("What did that motherfucker change their digits to?");
                        string telU = Console.ReadLine();
                        dbCon.Update(nameU, telU);
                        return;

                        
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Enter your search query");
                        foreach (var item in dbCon.Select(Console.ReadLine()))
                        {
                            Console.Write(item.Name + " || " + item.Tel);
                        }
                        Console.ReadLine();
                        return;
                    default:
                        Console.WriteLine("Yo, input a number dickface.");
                        Console.ReadLine();
                        return;
                }
            }
            else
            {
                Console.WriteLine("Yo, input a number dickface.");
                Console.ReadLine();
            }
            

        }

    }
}
