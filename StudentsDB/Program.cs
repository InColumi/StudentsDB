using System;
using System.Collections.Generic;
using System.Xml;


namespace StudentsDB
{
    class Program
    {
        /// <summary>
        /// Делегат для передачи метода конвертации
        /// </summary>
        /// <param name="node">элемент XML документа</param>
        /// <returns>объекст типа IConverter</returns>
        delegate IConverter Convert(XmlNode node);
        
        static void Main(string[] args)
        {
            ChooseCommand();
            Console.ReadKey();
        }

        /// <summary>
        /// считывает и проверяет пользовательский ввод
        /// </summary>
        /// <returns>корректное число</returns>
        static int GetCorrectInput()
        {
            
            string input; // строка для вводе
            int numberOfCommand = 0; // число для вода
            bool isCorrect = false; // флаг
            while (isCorrect == false)
            {
                input = Console.ReadLine(); // ввод пользователя
                if (int.TryParse(input, out numberOfCommand)) // попыта конвертации ввод в число
                {
                    isCorrect = true;
                }
                else // иначе сказать пользователю, что он ввел дичь
                {
                    Console.WriteLine("Введите корректный номер команды!");
                }
            }
            return numberOfCommand;
        }

        /// <summary>
        /// Функция с выбором комманды
        /// </summary>
        static void ChooseCommand()
        {
            bool isExit = false; // флаг для выхода

            int numberOfCommand = 0; // номер комманды
            //если программа запускается в проекте, то использовать эти пути
            string pathStudents = "..\\..\\DB\\Students"; // пусть к файлу Student
            string pathSubjects = "..\\..\\DB\\Subjects"; // пусть к файлу Subjects

            // если программа запускается через exe файл, то использовать эти пути.
            // Так как через exe  программа будет искать в текущей дериктории
            //string pathStudents = "DB\\Students"; // пусть к файлу Student
            //string pathSubjects = "DB\\Subjects"; // пусть к файлу Subjects

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

                List<IConverter> students = GetFromXML(pathStudents, new Student().ConvertFrom); // получаем списко студенов
                List<IConverter> subjects = GetFromXML(pathSubjects, new Subject().ConvertFrom); // получаем списко предметов
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

        /// <summary>
        /// Добавляет предмет в файл
        /// </summary>
        /// <param name="fileName">название файла</param>
        private static void AddSubjectToFile(string fileName)
        {
            string path = $"{fileName}.xml"; // пусть к файлу
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path); // отркрывает документ
            XmlElement xRoot = xDoc.DocumentElement; // берем корневой элемент
            // создаем новый элемент subject
            XmlElement subjectElem = xDoc.CreateElement("subject");
            // создаем элементы 
            XmlElement idElem = xDoc.CreateElement("id");
            XmlElement nameElem = xDoc.CreateElement("name");
            XmlElement idStudentElem = xDoc.CreateElement("idStudent");
            XmlElement assessmentElem = xDoc.CreateElement("assessment");

            // добавляем содержимое в элементы
            idElem.InnerText = GetInput("Введите ID предмета: ");
            nameElem.InnerText = GetInput("Введите название предмета: ");
            idStudentElem.InnerText = GetInput("Введите ID студента: ");
            assessmentElem.InnerText = GetInput("Введите оценку: ");

            //добавляем элементы
            subjectElem.AppendChild(idElem);
            subjectElem.AppendChild(nameElem);
            subjectElem.AppendChild(idStudentElem);
            subjectElem.AppendChild(assessmentElem);
            xRoot.AppendChild(subjectElem);
            xDoc.Save(path);
            Console.WriteLine("Предмет добавлен");
        }

        /// <summary>
        /// Выводит name и просит ввести данные от пользователя
        /// </summary>
        /// <param name="name">описание</param>
        /// <returns>результат ввода</returns>
        private static string GetInput(string name)
        {
            Console.Write(name);
            return Console.ReadLine();
        }

        /// <summary>
        /// Добавить студента в файл
        /// </summary>
        /// <param name="fileName">название файла</param>
        private static void AddStudentToFile(string fileName)
        {
            string path = $"{fileName}.xml"; // пусть к файлу
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path); // отркрывает документ
            XmlElement xRoot = xDoc.DocumentElement; // берем корневой элемент
            // создаем новый элемент student
            XmlElement studentElem = xDoc.CreateElement("student");

            // создаем элементы 
            XmlElement idElem = xDoc.CreateElement("id");
            XmlElement firstNameElem = xDoc.CreateElement("firstName");
            XmlElement lastNameElem = xDoc.CreateElement("lastName");
            XmlElement courseElem = xDoc.CreateElement("course");

            // добавляем содержимое в элементы
            idElem.InnerText = GetInput("Введите ID студента: ");
            firstNameElem.InnerText = GetInput("Введите Имя студента: ");
            lastNameElem.InnerText = GetInput("Введите Фамилияю студента: ");
            courseElem.InnerText = GetInput("Введите курс студента: ");

            //добавляем элементы
            studentElem.AppendChild(idElem);
            studentElem.AppendChild(firstNameElem);
            studentElem.AppendChild(lastNameElem);
            studentElem.AppendChild(courseElem);
            xRoot.AppendChild(studentElem);
            xDoc.Save(path);
            Console.WriteLine("Студент добавлен");
        }

        /// <summary>
        /// Показать всех студентов по опредленому предмету
        /// </summary>
        /// <param name="students"></param>
        /// <param name="subjects"></param>
        private static void ShowStudentsBySubjects(List<IConverter> students, List<IConverter> subjects)
        {
            ShowInfo(subjects);
            Console.WriteLine("Введите ID предмета: ");
            int idSubject = GetCorrectInput();
            bool isExist = false;
            foreach (var subject in subjects)// перебираем предметы в поисках введенного ID 
            {
                Subject subj = subject as Subject; // Конвертируем IConverter в  Subject
                if (subj.ID == idSubject) // если есть
                {
                    subj.ShowInfo(); // выводим на монитор
                    isExist = true;
                    break;
                }
            }

            if (isExist == false) // если такого предмета нет, то говорим об этом пользователю
            {
                Console.WriteLine("Такого предмета нет!");
                return;
            }

            foreach (var student in students) // Перебираем студентов в поисках нужного
            {
                Student stud = student as Student; // Конвертируем IConverter в  Student
                foreach (var subject in subjects)
                {
                    Subject subj = subject as Subject; // Конвертируем IConverter в  Subject
                    if (subj.ID == idSubject && subj.IDStudent == stud.ID) // если ID предмета = введенному idSubject и IDStudent = ID студента
                    {
                        student.ShowInfo();// то показываем этого студента
                    }
                }
            }
        }

        /// <summary>
        /// Показать все предметы определенного студента
        /// </summary>
        /// <param name="students"></param>
        /// <param name="subjects"></param>
        private static void ShowSubjectByStudents(List<IConverter> students, List<IConverter> subjects)
        {
            ShowInfo(students);
            Console.WriteLine("Введите ID студента: ");
            int idStudent = GetCorrectInput();
            bool isExist = false;
            foreach (var student in students) // перебираем студентов
            {
                Student stud = student as Student; // конвертируем объекст  IConverter в Student
                if (stud.ID == idStudent) // если ID студента = введенному ID тогда такой студент есть и мы продолжаем 
                {
                    isExist = true;
                }
            }

            if (isExist == false) // значит, что такого студента - нет
            {
                Console.WriteLine("Такого студента нет!");
                return;
            }

            foreach (var subject in subjects) // перебираем предметы
            {
                Subject subj = subject as Subject; // конвертируем объекст  IConverter в Subject
                if (subj.IDStudent == idStudent) // если IDStudent в предметах = введенному ID, то выводим этот предмет
                {
                    subject.ShowInfo();
                }
            }
        }

        /// <summary>
        /// Получение корневой элемент
        /// </summary>
        /// <param name="fileName">имя файл</param>
        /// <returns>корнейвой документ</returns>
        private static XmlNode GetRootNodeXML(string fileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            try // если открыть не удалось, то кидаем исключение
            {
                xmlDocument.Load($"{fileName}.xml"); // открываем документ
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadKey();
                Environment.Exit(1);
            }
            return xmlDocument.DocumentElement;
        }

        /// <summary>
        /// Конвертирует XML файл в список объектов
        /// </summary>
        /// <param name="fileName"> имя файла</param>
        /// <param name="convert"> способ конвертации</param>
        /// <returns></returns>
        private static List<IConverter> GetFromXML(string fileName, Convert convert)
        {
            List<IConverter> items = new List<IConverter>(); // создаем список объектов
            XmlNode xRoot = GetRootNodeXML(fileName);

            foreach (XmlNode node in xRoot)
            {
                items.Add(convert(node));
            }
            return items;
        }

        /// <summary>
        /// Выводит на монитор список объектов IConverter
        /// </summary>
        /// <param name="list"></param>
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
