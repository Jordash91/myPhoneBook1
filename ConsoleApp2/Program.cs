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
            while (true)
            {
                Console.Clear();
                MenuSys();
                var ans = Console.ReadLine();
                int choice = 0;
                int.TryParse(ans, out choice);
                if(!MenuChoice(choice)){
                    break;
                }              
            }


        }

        private static void MenuSys()
        {
           Console.WriteLine("Book of numbers" +
                Environment.NewLine + "1. New contact" +
                Environment.NewLine + "2. Change contact" +
                Environment.NewLine + "3. Search contact" + 
                Environment.NewLine + "4. Close");
        }

        private static bool MenuChoice(int menuChoice)
        {
            switch(menuChoice)
            {
                case 1:
                    InsertNewContact();
                    break;
                case 2:
                    UpdateContact();
                    break;
                case 3:
                    SearchContact();
                    break;
                case 4:
                    return false;
                default:
                    Console.WriteLine("1, 2, 3, or 4 please.");
                    Console.ReadLine();
                    break;
            }
            return true;
        }

        private static void InsertNewContact()
        {
            DBConnect dbCon = new DBConnect();
            Console.Clear();
            Console.Write("Please enter their name: ");
            string name = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Please enter their number: ");
            string tel = Console.ReadLine();
            dbCon.Insert(name, tel);
        }

        private static void UpdateContact()
        {
            DBConnect dbCon = new DBConnect();
            Console.Clear();
            Console.Write("Who changed their number? ");
            string nameU = Console.ReadLine();
            Console.WriteLine();
            Console.Write("What did they change their number to? ");
            string telU = Console.ReadLine();
            dbCon.Update(nameU, telU);
        }

        private static void SearchContact()
        {
            DBConnect dbCon = new DBConnect();
            Console.Clear();
            Console.WriteLine("Enter your search query");
            foreach (var item in dbCon.Select(Console.ReadLine()))
            {
                Console.Write(item.Name + " || " + item.Tel);
            }
            Console.ReadLine();
        }

    }
}
