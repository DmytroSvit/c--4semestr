using System;
using System.IO;

namespace Praktyka1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string[] a = File.ReadAllLines("ads.txt");

            var b = Advertisement.Parse(a[0]);
            Collection c = new Collection("Test colection");
            c.Add(Advertisement.Parse(a[2]));
            c.Add(b);
            c.Add(Advertisement.Parse(a[1]));
            

            c.Change("00000002", Advertisement.Parse(a[3]));

             /*foreach (var item in c)
             {
                 Console.WriteLine(item);
             }*/

            bool isWorking = true;

            while (isWorking)
            {
                Console.WriteLine("1 - search; 2 - sort; 3 - print collection; 4 - delete by id; exit - to end program ");
                string command = Console.ReadLine();

                switch (command.ToLower())
                {
                    case "1":
                        Console.Write("Search: ");
                        string search = Console.ReadLine();
                        c.Search(search);
                        break;
                    case "2":
                        c.Sort();
                        break;
                    case "3":
                        foreach (var item in c)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case "4":
                        Console.Write("Enter id to delete advertisement: ");
                        string id = Console.ReadLine();
                        c.Delete(id);
                        break;
                    case "exit":
                        isWorking = false;
                        break;
                    default:
                        Console.WriteLine("There is no such command");
                        break;
                }
            
            }
        }
    }
}
