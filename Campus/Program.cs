using Campus.UserConsole;
using LiteDB;
using System;

namespace Campus
{
    internal class Program
    {
        static string path = @"DB.db";
        static void Main(string[] args)
        {
            //using (var db = new LiteDatabase(path))
            //{
            //    var col = db.GetCollection<Models.Administrator>("admin");

            //    var admin = new Models.Administrator
            //    {
            //        Id = 1,
            //        Login = "admin",
            //        Password = "admin"
            //    };

            //    col.Insert(admin);
            //}
                int num = 0;
            do
            {
                Console.WriteLine("Выберете вашу роль:");
                Console.WriteLine("1 - Администратор");
                Console.WriteLine("2 - Студент");
                bool userEnter = int.TryParse(Console.ReadLine(), out num);

                if (userEnter)
                {
                    switch (num)
                    {
                        case 1:
                            Console.Write("Введите логин: ");
                            string login = Console.ReadLine();
                            Console.Write("Введите пароль: ");
                            string password = Console.ReadLine();
                            AdministratorConsole administratorConsole = new AdministratorConsole(path);
                            if (administratorConsole.LogIn(login, password))
                                administratorConsole.MenuAdmin();
                            else
                                Console.WriteLine("Ошибка! Неверный логин или пароль");
                            break;
                        case 2:
                            Console.Write("Введите id: ");
                            bool isId = int.TryParse(Console.ReadLine(), out int val);
                            Console.Write("Введите пароль: ");
                            string pass = Console.ReadLine();
                            StudentConsole studentConsole = new StudentConsole(path);
                            if (isId)
                            {
                                if (studentConsole.LogIn(val, pass))
                                    studentConsole.MenuStudent();
                                else
                                    Console.WriteLine("Ошибка! Неверный логин или пароль");
                            }
                            break;
                    }
                }
                else
                    Console.WriteLine("Ошибка ввода");
            } while (num != 0);

        }
    }
}
