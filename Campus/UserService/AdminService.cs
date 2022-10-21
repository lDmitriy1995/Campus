using Campus.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campus.Administrator
{
    public class AdminService
    {
        private string Path { get; set; }
        Random rnd = new Random();
        public AdminService(string path)
        {
            Path = path;
        }

        public bool AddNewStudent(out string message)
        {
            try
            {
                using (var db = new LiteDatabase(Path))
                {
                    var col = db.GetCollection<Student>("students");

                    var student = new Student
                    {
                        Id = 1000 + col.Count() + 1,
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Password = "12345",
                        IsActive = true
                    };

                    col.Insert(student);
                    message = $"ID: {student.Id}, Passowrd: {student.Password}";
                }
                return true;
            }
            catch(Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            using (var db = new LiteDatabase(Path))
            {
                var col = db.GetCollection<Student>("students");

                students = col.Query()
                    .ToList();
            }
            return students;
        }
    }
}
