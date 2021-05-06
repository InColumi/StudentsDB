using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace StudentsDB
{
    class Program
    {
        static void Main(string[] args)
        {
            //XmlDocument xmlDocument = new XmlDocument();
            //try
            //{
            //    xmlDocument.Load("DB/Students.xml");
            //}
            //catch (Exception exc)
            //{
            //    Console.WriteLine(exc.Message);
            //    Console.ReadKey();
            //    Environment.Exit(1);
            //}


            //XmlNode xRoot = xmlDocument.DocumentElement;
            //foreach (XmlNode node in xRoot)
            //{
            //    foreach (XmlNode childNode in node.ChildNodes)
            //    {
            //        Console.WriteLine(childNode.InnerText);
            //    }
            //}
            ChooseCommand();
            Console.ReadKey();
        }

        static void ChooseCommand()
        {
            bool isExit = false;
            bool isCorrect = false;
            int numberOfCommand = 0;
            string input;
            while (isExit == false)
            {
                Console.WriteLine("Выберете команду: ");
                Console.WriteLine("\t 1 - показать студентов.");
                Console.WriteLine("\t 2 - показать предметы.");
                Console.WriteLine("\t 3 - добавить предмет.");
                Console.WriteLine("\t 4 - добавить студента.");
                Console.WriteLine("\t 5 - показать предметы конкретного студента.");
                Console.WriteLine("\t 6 - показать студентов конкретного предмета.");
                Console.WriteLine("\t 7 - выход.");
                Console.Write("\t 8 - очистить консоль.\n>");
                
                while (isCorrect == false)
                {
                    input = Console.ReadLine();
                    if (int.TryParse(input, out numberOfCommand))
                    {
                        isCorrect = true;
                    }
                    else
                    {
                        Console.WriteLine("Введите корректный номер команды!");
                    }
                }

                switch (numberOfCommand)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7: 
                        isExit = true;
                        Console.WriteLine("Программа закончила свою работу!");
                        break;
                    case 8:
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Неверная команда!");
                        break;
                }
                isCorrect = false;
            }
        }
    }

    class Subject
    {
        public UInt64 ID { get; private set; }
        public string Name { get; private set; }
        public UInt64 IDStudent { get; private set; }
        public UInt64 Assessment { get; private set; }

        public Subject(ulong iD, string name, ulong iDStudent, ulong assessment)
        {
            ID = iD;
            Name = name;
            IDStudent = iDStudent;
            Assessment = assessment;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"ID subject: {ID}");
            Console.WriteLine($"\tName: {Name}");
            Console.WriteLine($"\tID student: {IDStudent}");
            Console.WriteLine($"\tAssessment: {Assessment}");
        }
    }

    class Student
    {
        public UInt64 ID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Course { get; private set; }

        public Student(ulong iD, string firstName, string lastName, int course)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            Course = course;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"ID student: {ID}");
            Console.WriteLine($"\tFirst Name: {FirstName}");
            Console.WriteLine($"\tLast Name: {LastName}");
            Console.WriteLine($"\tCourse: {Course}");
        }
    }
}
