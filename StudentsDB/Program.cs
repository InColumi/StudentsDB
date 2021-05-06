using System;
using System.Collections.Generic;
using System.Xml;


namespace StudentsDB
{
    class Program
    {
        delegate IConverter Convert(XmlNode node);
        static void Main(string[] args)
        {
            ChooseCommand();
            Console.ReadKey();
        }

        static int GetCorrectInput()
        {
            string input;
            int numberOfCommand = 0;
            bool isCorrect = false;
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
            return numberOfCommand;
        }

        static void ChooseCommand()
        {
            bool isExit = false;

            int numberOfCommand = 0;
            string pathStudents = "..\\..\\DB\\Students";
            string pathSubjects = "..\\..\\DB\\Subjects";

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

                numberOfCommand = GetCorrectInput();

                List<IConverter> students = GetFromXML(pathStudents, new Student().ConvertFrom);
                List<IConverter> subjects = GetFromXML(pathSubjects, new Subject().ConvertFrom);
                switch (numberOfCommand)
                {
                    case 1:
                        ShowInfo(students);
                        break;
                    case 2:
                        ShowInfo(subjects);
                        break;
                    case 3:
                        AddSubjectToFile(pathSubjects); 
                        break;
                    case 4:
                        AddStudentToFile(pathStudents);
                        break;
                    case 5:
                        ShowSubjectByStudents(students, subjects);
                        break;
                    case 6:
                        ShowStudentsBySubjects(students, subjects);
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
            }
        }

        private static void AddSubjectToFile(string fileName)
        {
            string path = $"{fileName}.xml";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            // создаем новый элемент subject
            XmlElement subjectElem = xDoc.CreateElement("subject");
            // создаем элементы company и age
            XmlElement idElem = xDoc.CreateElement("id");
            XmlElement nameElem = xDoc.CreateElement("name");
            XmlElement idStudentElem = xDoc.CreateElement("idStudent");
            XmlElement assessmentElem = xDoc.CreateElement("assessment");

            idElem.InnerText = GetInput("Введите ID предмета: ");
            nameElem.InnerText = GetInput("Введите название предмета: ");
            idStudentElem.InnerText = GetInput("Введите ID студента: ");
            assessmentElem.InnerText = GetInput("Введите оценку: ");

            subjectElem.AppendChild(idElem);
            subjectElem.AppendChild(nameElem);
            subjectElem.AppendChild(idStudentElem);
            subjectElem.AppendChild(assessmentElem);
            xRoot.AppendChild(subjectElem);
            xDoc.Save(path);
            Console.WriteLine("Предмет добавлен");
        }

        private static string GetInput(string name)
        {
            Console.Write(name);
            return Console.ReadLine();
        }

        private static void AddStudentToFile(string fileName)
        {
            string path = $"{fileName}.xml";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            // создаем новый элемент student
            XmlElement studentElem = xDoc.CreateElement("student");

            XmlElement idElem = xDoc.CreateElement("id");
            XmlElement firstNameElem = xDoc.CreateElement("firstName");
            XmlElement lastNameElem = xDoc.CreateElement("lastName");
            XmlElement courseElem = xDoc.CreateElement("course");

            idElem.InnerText = GetInput("Введите ID студента: ");
            firstNameElem.InnerText = GetInput("Введите Имя студента: ");
            lastNameElem.InnerText = GetInput("Введите Фамилияю студента: ");
            courseElem.InnerText = GetInput("Введите курс студента: ");

            studentElem.AppendChild(idElem);
            studentElem.AppendChild(firstNameElem);
            studentElem.AppendChild(lastNameElem);
            studentElem.AppendChild(courseElem);
            xRoot.AppendChild(studentElem);
            xDoc.Save(path);
            Console.WriteLine("Студент добавлен");
        }

        private static void ShowStudentsBySubjects(List<IConverter> students, List<IConverter> subjects)
        {
            ShowInfo(subjects);
            Console.WriteLine("Введите ID предмета: ");
            int idSubject = GetCorrectInput();
            bool isExist = false;
            foreach (var subject in subjects)
            {
                Subject subj = subject as Subject;
                if (subj.ID == idSubject)
                {
                    subj.ShowInfo();
                    isExist = true;
                    break;
                }
            }

            if (isExist == false)
            {
                Console.WriteLine("Такого предмета нет!");
                return;
            }

            foreach (var student in students)
            {
                Student stud = student as Student;
                foreach (var subject in subjects)
                {
                    Subject subj = subject as Subject;
                    if (subj.ID == idSubject && subj.IDStudent == stud.ID)
                    {
                        student.ShowInfo();
                    }
                }
            }
        }

        private static void ShowSubjectByStudents(List<IConverter> students, List<IConverter> subjects)
        {
            ShowInfo(students);
            Console.WriteLine("Введите ID студента: ");
            int idStudent = GetCorrectInput();
            bool isExist = false;
            foreach (var student in students)
            {
                Student stud = student as Student;
                if (stud.ID == idStudent)
                {
                    isExist = true;
                }
            }

            if (isExist == false)
            {
                Console.WriteLine("Такого студента нет!");
                return;
            }

            foreach (var subject in subjects)
            {
                Subject subj = subject as Subject;
                if (subj.IDStudent == idStudent)
                {
                    subject.ShowInfo();
                }
            }
        }

        private static XmlNode GetRootNodeXML(string fileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load($"{fileName}.xml");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadKey();
                Environment.Exit(1);
            }
            return xmlDocument.DocumentElement;
        }

        private static List<IConverter> GetFromXML(string fileName, Convert convert)
        {
            List<IConverter> items = new List<IConverter>();
            XmlNode xRoot = GetRootNodeXML(fileName);

            foreach (XmlNode node in xRoot)
            {
                items.Add(convert(node));
            }
            return items;
        }

        private static void ShowInfo(List<IConverter> list)
        {
            foreach (var item in list)
            {
                item.ShowInfo();
            }
            Console.WriteLine();
        }
    }
}
