using System;
using System.Xml;

namespace StudentsDB
{
    class Student: IConverter
    {
        public int ID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Course { get; private set; }

        public Student() { }

        public Student(int iD, string firstName, string lastName, int course)
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

        public IConverter ConvertFrom(XmlNode node)
        {
            XmlNodeList nodeList = node.ChildNodes;
            return new Student(
                        System.Convert.ToInt32(nodeList[0].InnerText),
                        nodeList[1].InnerText,
                        nodeList[2].InnerText,
                        System.Convert.ToInt32(nodeList[3].InnerText)
                        );
        }
    }
}
