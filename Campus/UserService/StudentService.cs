using Campus.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.UserService
{
    public class StudentService
    {
        private string Path { get; set; }

        public StudentService(string path)
        {
            Path = path;
        }

        public Student GetStudent(int id)
        {
            Student student;
            using (var db = new LiteDatabase(Path))
            {
                var col = db.GetCollection<Student>("students");

                student = col.FindOne(x => x.Id == id);
            }

            //Console.WriteLine($"{student.FirstName} - {student.LastName} - {student.GPA}");
            return student;
        }

        public bool ChangePassword(int id, string newPass)
        {
            var student = GetStudent(id);

            student.Password = newPass;

            try
            {
                using (var db = new LiteDatabase(Path))
                {
                    var col = db.GetCollection<Student>("students");

                    col.Update(student);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeInfo(int id, string firstName = "FirstName", string lastName = "LastName", double gpa = 0)
        {
            var student = GetStudent(id);

            student.FirstName = firstName;
            student.LastName = lastName;
            student.GPA = gpa;

            try
            {
                using (var db = new LiteDatabase(Path))
                {
                    var col = db.GetCollection<Student>("students");

                    col.Update(student);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
