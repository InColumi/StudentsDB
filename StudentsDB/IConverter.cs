using System.Xml;

namespace StudentsDB
{
    interface IConverter
    {
        IConverter ConvertFrom(XmlNode node);

        void ShowInfo();
    }
}
