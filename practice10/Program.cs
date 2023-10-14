using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace practice10
{
    internal class Program
    {
        public static DriveInfo[] allDrives = DriveInfo.GetDrives();
        public static void Main()
        {
            User.UserPosition = "drivemenu";
            User.PrevPosition = "drivemenu";
            Menu();
            DriveMenu();
            Menu2();
            Interface();
        }
        public static void DriveMenuMain()
        {
            User.UserPosition = "drivemenu";
            Menu();
            DriveMenu();
            Menu2();
            Interface();
        }
        public static void DriveMenu()
        {
            double size = 0;
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    Console.WriteLine($"  Drive {d.Name}                              Свободно: {size = d.TotalFreeSpace / 1073741824} ГБ; Всего: {size = d.TotalSize / 1073741824} ГБ.");
                }
                else
                {
                    Console.WriteLine($"  Drive {d.Name}                              Не готово");
                }
            }
        }
        public static void Menu()
        {
            string x = User.UserPosition;
            if (x == "drivemenu")
            {
                Console.WriteLine("                                      Этот компьютер");
                Console.WriteLine("-------------------------------------------------------------------------------------------");
            }
            else if (x != "drivemenu")
            {
                Console.WriteLine($"                                      {x}");
                Console.WriteLine("-------------------------------------------------------------------------------------------");
            }
        }
        public static void Menu2()
        {
            for (int i = 0; i < 15; i++)
            {
                Console.SetCursorPosition(90, i);
                Console.WriteLine("|");
            }
            Console.SetCursorPosition(91, 4);
            Console.WriteLine("Нажмите F1");
            Console.SetCursorPosition(91, 5);
            Console.WriteLine("для возвращения");
            Console.SetCursorPosition(91, 6);
            Console.WriteLine("в выбор диска");
            Console.SetCursorPosition(91, 8);
            Console.WriteLine("Или Escape для возвращения");
            Console.SetCursorPosition(91, 9);
            Console.WriteLine("в предыдущию папку.");
            Console.SetCursorPosition(0, 0);
        }
        public static void Interface()
        {
            int x = User.CursorPosition;
            Console.SetCursorPosition(0, x);
            Console.WriteLine(">");
            InterfaceAction();
        }
        public static void InterfaceEnter()
        {
            Console.Clear();
            string x = User.UserPosition;
            int y = User.CursorPosition;
            int driveIndex = y - 2;
            if (x == "drivemenu")
            {
                if (allDrives[driveIndex].IsReady == true)
                {
                    if (y >= 2 && y <= allDrives.Length + 1)
                    {

                        if (allDrives[driveIndex].IsReady)
                        {
                            User.PrevPosition = User.UserPosition;
                            User.UserPosition = allDrives[driveIndex].Name;
                            Menu();
                            User.allFilesAndDirectories = Directory.GetFileSystemEntries(allDrives[driveIndex].RootDirectory.FullName);

                            foreach (string item in User.allFilesAndDirectories)
                            {
                                Console.WriteLine($"   {item}");
                            }

                            Menu2();
                            Interface();
                        }
                        else
                        {
                            Console.WriteLine($"Диск {allDrives[driveIndex].Name} не готов к использованию.");
                            Console.WriteLine("Нажмите любую клавишу для продолжения.");
                            Console.ReadKey();
                            Interface();
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Диск не готов\nНажмите любую клавишу чтобы вернуться");
                    Console.ReadKey();
                    

                }
            }
            else if (User.UserPosition != "drivemenu")
            {
                User.PrevPosition = User.UserPosition;
                if (y >= 2)
                {

                    string selectedItem = User.allFilesAndDirectories[y - 2];

                    if (File.Exists(selectedItem))
                    {
                        Console.Clear();
                        Console.WriteLine($"Открытие файла: {selectedItem}\nНажмите на любую клавишу для возвращения");
                        Process.Start(selectedItem);
                        Console.ReadKey();
                        ReturnInCont();
                    }
                    else if (Directory.Exists(selectedItem))
                    {
                        User.IsThatDir = true;
                        User.PrevPosition = User.UserPosition;
                        User.UserPosition = selectedItem;
                        Menu();
                        User.allFilesAndDirectories = Directory.GetFileSystemEntries(selectedItem);

                        foreach (string item in User.allFilesAndDirectories)
                        {
                            Console.WriteLine($"   {item}");
                        }

                        Menu2();
                        Interface();
                    }
                    else
                    {
                        Console.WriteLine("Выбранный элемент не существует.");
                    }
                }

            }
        }
        public static void InterfaceAction()
        {
            User.key = Console.ReadKey();
            int x = User.CursorPosition;
            ConsoleKeyInfo key = User.key;
            if (key.Key == ConsoleKey.DownArrow)
            {
                Console.SetCursorPosition(0, x);
                Console.Write(" ");
                User.CursorPosition++;
                Interface();
                return;
            }
            else if (key.Key == ConsoleKey.UpArrow & x != 2)
            {
                Console.SetCursorPosition(0, x);
                Console.Write(" ");
                User.CursorPosition--;
                Interface();
                return;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                User.PrevPosition = User.UserPosition;
                InterfaceEnter();
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Return();
            }
            else if (key.Key == ConsoleKey.F1)
            {
                Console.Clear();
                DriveMenuMain();
            }
            else
            {
                Interface();
                return;
            }
            
        }
        public static void ReturnInCont()
        {
            User.UserPosition = User.PrevPosition;
            Console.Clear();
            string selectedItem = User.PrevPosition;
            Menu();
            User.allFilesAndDirectories = Directory.GetFileSystemEntries(selectedItem);

            foreach (string item in User.allFilesAndDirectories)
            {
                Console.WriteLine($"   {item}");
            }
            Menu2();
            Interface();
        }
        public static void Return()
        {
            string position = User.PrevPosition;
            int x = User.ReturnInt;
            if (position == "drivemenu")
            {
                Console.Clear();
                DriveMenuMain();

            }
            else if (x == 1)
            {
                User.ReturnInt = 0;
                User.UserPosition = "drivemenu";
                DriveMenuMain();
            }
            else if (position != "drivemenu" & x != 1)
            {
                ReturnInCont();
                User.ReturnInt = 1;
            }
        }
    }
    class User
    {
        static public int CursorPosition = 2;
        static public ConsoleKeyInfo key = new ConsoleKeyInfo();
        static public string UserPosition;
        static public string PrevPosition;
        public static bool IsThatDir;
        public static string[] allFilesAndDirectories;
        public static int ReturnInt;

    }
}
