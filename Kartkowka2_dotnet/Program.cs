using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Data.Entity;

namespace Kartkowka2_dotnet
{
    public class Student
    {
        public int Id { set; get; }
        public string name { set; get; }
        public string surname { set; get; }
        public string speciality { set; get; }

        public int deficit { set; get; }
 
        public void ShowStudent()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Fulname: {name} {surname}");
            Console.WriteLine($"Speciality: {speciality}");
            Console.WriteLine($"Deficit: {deficit}");
        }
    }


    public class DziekanatDbContext : DbContext
    {
        public virtual DbSet<Student> Students { get; set; }

        public Student AddDataToBase()
        {
            var student = new Student();
            Console.WriteLine("Name: ");
            student.name = Console.ReadLine();
            Console.WriteLine("Surname: ");
            student.surname = Console.ReadLine();
            Console.WriteLine("Speciality: ");
            student.speciality = Console.ReadLine();
            Console.WriteLine("Deficit: ");
            student.deficit = Convert.ToInt32(Console.ReadLine());

            return student;
        }

        public void ShowDataBaseContent()
        {
            var students = (from a in this.Students select a).ToList<Student>();
            foreach (var stud in students)
            {
                stud.ShowStudent();
                Console.WriteLine("");
            }
        }

        public void ClearBase()
        {
            Students.RemoveRange(Students);
            this.SaveChanges();
        }

        public void RemoveLast()
        {
            //var cs = CitiesWeather.First<CityWeather>();
            //var lastUser = CitiesWeather.Select(g => g.id).Max();
            var last = Students.OrderByDescending(g => g.Id)
                       .Take(1);

            Students.RemoveRange(last);
            //CitiesWeather.Remove(cs);
            this.SaveChanges();
        }

        public void FindByName(string name)
        {
            //var query = (from city in this.CitiesWeather
            //             where city.main.temp == searched_temp
            //             select city).ToList<CityWeather>();
            var student = this.Students.Where(stud => stud.name == name).ToList<Student>();
            foreach (var stud in student)
            {
                stud.ShowStudent();
                Console.WriteLine("");
            }
        }
        public void FindById(int id)
        {
            //var query = (from city in this.CitiesWeather
            //             where city.main.temp == searched_temp
            //             select city).ToList<CityWeather>();
            var student = this.Students.Where(stud => stud.Id == id).ToList<Student>();
            foreach (var stud in student)
            {
                stud.ShowStudent();
                Console.WriteLine("");
            }
        }

        public void FindBySpeciality(string speciality)
        {
            var students = this.Students.Where(stud => stud.speciality == speciality).ToList<Student>();
            foreach (var stud in students)
            {
                stud.ShowStudent();
                Console.WriteLine("");
            }
        }
        public void FindByDeficit(int deficit)
        {
            var students = this.Students.Where(stud => stud.deficit == deficit).ToList<Student>();
            foreach (var stud in students)
            {
                stud.ShowStudent();
                Console.WriteLine("");
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var dziekanatDB = new DziekanatDbContext();
            
            do
            {
                Console.Clear();
                Console.WriteLine("1. Add new student");
                Console.WriteLine("2. Clear whole database");
                Console.WriteLine("3. Show whole content");
                Console.WriteLine("4. Remove last added student");
                Console.WriteLine("5. Find student by Id");
                Console.WriteLine("6. Find student by name");
                Console.WriteLine("7. Find student by speciality");
                Console.WriteLine("8. Find student by deficit");
                Console.WriteLine("9. Exit");
                Console.WriteLine("Chose one option");

            } while (MainMenu(dziekanatDB));
           
        }

        static bool MainMenu(DziekanatDbContext dziekanatDB)
        {

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Insert new student");
                    dziekanatDB.Students.Add(dziekanatDB.AddDataToBase());
                    dziekanatDB.SaveChanges();
                    Console.WriteLine("Press enter to return");
                    Console.ReadLine();
                    return true;
                case "2":
                    dziekanatDB.ClearBase();
                    Console.WriteLine("Base Cleared");
                    Console.WriteLine("Press enter to return");
                    Console.ReadLine();
                    return true;
                case "3":
                    dziekanatDB.ShowDataBaseContent();
                    Console.WriteLine("Press enter to return");
                    Console.ReadLine();
                    return true;

                case "4":
                    dziekanatDB.RemoveLast();
                    Console.WriteLine("Element removed");
                    Console.WriteLine("Press enter to return");
                    Console.ReadLine();
                    return true;

                case "5":
                    Console.WriteLine("Find by Id: ");
                    dziekanatDB.FindById(Convert.ToInt32(Console.ReadLine()));
                    Console.WriteLine("Press enter to return");
                    Console.ReadLine();
                    return true;

                case "6":
                    Console.WriteLine("Find students' with given name: ");
                    dziekanatDB.FindByName(Console.ReadLine());
                    Console.WriteLine("Press enter to return");
                    Console.ReadLine();
                    return true;

                case "7":
                    Console.WriteLine("Find students' with given speciality: ");
                    dziekanatDB.FindBySpeciality(Console.ReadLine());
                    Console.WriteLine("Press enter to return");
                    Console.ReadLine();
                    return true;

                case "8":
                    Console.WriteLine("Find students' with given deficit: ");
                    dziekanatDB.FindByDeficit(Convert.ToInt32(Console.ReadLine()));
                    Console.WriteLine("Press enter to return");
                    Console.ReadLine();
                    return true;

                case "9":
                    return false;

                default:
                    Console.WriteLine("Error: option not found");
                    Console.WriteLine("\nPress enter to return");
                    Console.ReadLine();
                    return true;
            }


        }

    }
}