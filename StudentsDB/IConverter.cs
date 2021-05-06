using System.Xml;

namespace StudentsDB
{
    /// <summary>
    /// Интерфейс для связывания классов по общим методам
    /// </summary>
    interface IConverter
    {
        /// <summary>
        /// Конвертирует XML элемент в тип IConverter
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        IConverter ConvertFrom(XmlNode node);

        /// <summary>
        /// Выводит на монитор объект
        /// </summary>
        void ShowInfo();
    }
}
