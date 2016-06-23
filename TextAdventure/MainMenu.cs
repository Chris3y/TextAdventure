using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class MainMenu
    {
        List<string> options;
        int selectedIndex = 0;

        public void Show()
        {
            options = new List<string>();
            options.Add("Play Game");
            options.Add("Help");
            options.Add("Exit");
            PrintMenu(selectedIndex);
            DetectInput();

        }

        void DetectInput()
        {
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.UpArrow)
                {
                    if (selectedIndex != 0)
                    {
                        selectedIndex -= 1;
                    }
                }
                if (cki.Key == ConsoleKey.DownArrow)
                {
                    if (selectedIndex != options.Count() - 1)
                    {
                        selectedIndex += 1;
                    }
                }

                PrintMenu(selectedIndex);

                if (cki.Key == ConsoleKey.Enter)
                {
                    switch (selectedIndex)
                    {
                        case 0:
                                                  
                            break;                            
                    }
                }
            }
        }
        void PrintMenu(int index)
        {
            Console.Clear();
            GetHeader();

            for (int i = 0; i < options.Count(); i++)
            {
                if (selectedIndex == i)
                {
                    Console.WriteLine(" » {0}", options[i]);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("   {0}", options[i]);
                    Console.WriteLine();

                }
            }
        }
        void GetHeader()
        {
            Console.Clear();
            Console.WriteLine(Art.Logo);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Welcome to RAMSHAW HEIGHTS.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Select Option:");
            Console.WriteLine();
        }
    }
}
