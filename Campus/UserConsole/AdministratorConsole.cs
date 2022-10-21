using Campus.Administrator;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.UserConsole
{
    public class AdministratorConsole
    {
        private string Path { get; set; }
        private AdminService adminService;

        public AdministratorConsole(string path)
        {
            Path = path;
            adminService = new AdminService(Path);
        }

        public bool LogIn(string login, string password)
        {
            using (var db = new LiteDatabase(Path))
            {
                var col = db.GetCollection<Models.Administrator>("admin");

                var admin = col.FindOne(x => x.Login == login && x.Password == password);

                if (admin != null)
                    return true;
                else
                    return false;
            }
        }

        public void MenuAdmin()
        {
            int value = 0;
            do
            {
                Console.WriteLine("Ведите 0 для выхода");
                Console.WriteLine("1 - Добавить студента");
                Console.WriteLine("2 - Вывести список студентов");
                bool userInput = int.TryParse(Console.ReadLine(), out value);

                if (userInput)
                {
                    switch (value)
                    {
                        case 1:
                            if (adminService.AddNewStudent(out string mes))
                                Console.WriteLine($"Новый студент добавлен - {mes}");

                            else
                                Console.WriteLine($"Ошибка - {mes}");
                            break;
                        case 2:
                            var listStud = adminService.GetStudents();
                            foreach (var stud in listStud)
                                Console.WriteLine($"{stud.Id} - {stud.FirstName} - {stud.LastName} - {stud.GPA}");
                            break;
                    }
                }
                else
                    Console.WriteLine("Ошибка ввода");
            } while (value != 0);

        }
    }
}
