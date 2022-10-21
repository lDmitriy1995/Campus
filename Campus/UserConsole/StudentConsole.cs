using Campus.UserService;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.UserConsole
{
    public class StudentConsole
    {
        private string Path { get; set; }
        private StudentService studentService;
        private int id_;

        public StudentConsole(string path)
        {
            Path = path;
            studentService = new StudentService(Path);
        }

        public bool LogIn(int id, string password)
        {
            using (var db = new LiteDatabase(Path))
            {
                var col = db.GetCollection<Models.Student>("students");

                var stud = col.FindOne(x => x.Id == id && x.Password == password);

                if (stud != null)
                {
                    id_ = id;
                    return true;
                }
                else
                    return false;
            }
        }

        public void MenuStudent()
        {
            int value = 0;
            do
            {
                Console.WriteLine("Ведите 0 для выхода");
                Console.WriteLine("1 - Изменить профиль");
                Console.WriteLine("2 - Изменить пароль");
                Console.WriteLine("3 - Посмотреть профиль");
                bool userInput = int.TryParse(Console.ReadLine(), out value);

                if (userInput)
                {
                    switch (value)
                    {
                        case 1:
                            Console.Write("Введите имя: ");
                            string firstName = Console.ReadLine();
                            Console.Write("Введите фамилию: ");
                            string lastName = Console.ReadLine();
                            Console.Write("Введите gpa: ");
                            bool gpa = double.TryParse(Console.ReadLine(), out double val);
                            if (gpa)
                            {
                                if (studentService.ChangeInfo(id_, firstName, lastName, val))
                                    Console.WriteLine("Профиль успешно изменен");

                                else
                                    Console.WriteLine("Ошибка");
                            }                          
                            break;
                        case 2:
                            Console.Write("Введите новый пароль: ");
                            string password = Console.ReadLine();
                            if(studentService.ChangePassword(id_, password))
                                Console.WriteLine("Пароль успешно изменен");
                            else
                                Console.WriteLine("Ошибка");
                            break;
                        case 3:
                            var stud = studentService.GetStudent(id_);
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
