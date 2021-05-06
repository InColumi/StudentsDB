using System;
using System.Xml;

namespace StudentsDB
{
    /// <summary>
    /// Класс Предмет наследуте интерфейс IConverter
    /// </summary>
    class Subject : IConverter
    {
        /// <summary>
        /// Id предмета
        /// </summary>
        public int ID { get; private set; }
        /// <summary>
        /// Название предмета
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// ID студента
        /// </summary>
        public int IDStudent { get; private set; }
        /// <summary>
        /// Оценка
        /// </summary>
        public int Assessment { get; private set; }

        public Subject(){}

        public Subject(int iD, string name, int iDStudent, int assessment)
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

        public IConverter ConvertFrom(XmlNode node)
        {
            XmlNodeList nodeList = node.ChildNodes;
            return new Subject(
                System.Convert.ToInt32(nodeList[0].InnerText),
                nodeList[1].InnerText,
                System.Convert.ToInt32(nodeList[2].InnerText),
                System.Convert.ToInt32(nodeList[3].InnerText)
                );
        }
    }
}
