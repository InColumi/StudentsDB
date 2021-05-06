using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

                List<IConverter> students = GetFromXML("..\\..\\DB\\Students", new Student().ConvertFrom);
                List<IConverter> subjects = GetFromXML("..\\..\\DB\\Subjects", new Subject().ConvertFrom);
                switch (numberOfCommand)
                {
                    case 1:
                        ShowInfo(students);
                        break;
                    case 2:
                        ShowInfo(subjects);
                        break;
                    case 3:
                        break;
                    case 4:
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
