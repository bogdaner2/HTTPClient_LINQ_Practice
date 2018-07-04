using System;

namespace HTTP_LINQ_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            while (true)
            {
                try
                {
                    menu.GetMenu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please, enter any key for continue");
                    Console.ReadKey();
                }
            }
        }
    }
}
