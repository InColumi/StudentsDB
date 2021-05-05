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
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load("DB/Students.xml");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadKey();
                Environment.Exit(1);
            }
           

            XmlNode xRoot = xmlDocument.DocumentElement;
            foreach (XmlNode node in xRoot)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    Console.WriteLine(childNode.InnerText);
                }
            }

            Console.ReadKey();
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
