using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HTTP_LINQ_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.GetMenu();
            Console.ReadLine();   
        }
    }
}
