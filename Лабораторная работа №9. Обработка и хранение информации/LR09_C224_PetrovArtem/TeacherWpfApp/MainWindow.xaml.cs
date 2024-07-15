using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;
using TeacherClassLibrary;

namespace TeacherWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigatorPage.MainFrame = MainFrame;
        }

        private void buttonAllTeachers_Click(object sender, RoutedEventArgs e)
        {
            NavigatorPage.MainFrame.Navigate(new PageDataBaseTeacher());
        }

        private void buttonAppendTeacher_Click(object sender, RoutedEventArgs e)
        {
            NavigatorPage.MainFrame.Navigate(new PageAddTeacher());
        }

        private string OpenDialog(string filter)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Имя файла для открытия базы данных";
            openDialog.Filter = filter;
            openDialog.InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (openDialog.ShowDialog() != true)
                return null;
            openDialog.Multiselect = false;
            openDialog.CheckFileExists = true;
            return openDialog.FileName;
        }

        private void buttonLoadFromTxt_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenDialog("Текстовые файлы | *.txt | Все файлы | *.*");
            if (fileName == null)
                return;
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Teacher teacher = new Teacher();
                    teacher.FullName = line;
                    line = reader.ReadLine();
                    if (DateTime.TryParse(line, out DateTime dateOfBirth))
                        teacher.DateOfBirth = dateOfBirth;
                    else
                        continue;
                    line = reader.ReadLine();
                    if (Enum.TryParse(line, out Education education))
                        teacher.Education = education;
                    else
                        continue;
                    line = reader.ReadLine();
                    if (bool.TryParse(line, out bool isScienceDegree))
                        teacher.IsScienceDegree = isScienceDegree;
                    else
                        continue;
                    line = reader.ReadLine();
                    if (Enum.TryParse(line, out Subjects subjects))
                        teacher.Subjects = subjects;
                    else
                        continue;
                    line = reader.ReadLine();
                    if (int.TryParse(line, out int price))
                        teacher.Price = price;
                    else
                        continue;
                    line = reader.ReadLine();
                    if (int.TryParse(line, out int weekLoad))
                        teacher.WeekLoad = weekLoad;
                    else
                        continue;
                    DataBaseTeacher.Teachers.Add(teacher);
                }
            }
            NavigatorPage.MainFrame.Navigate(new PageDataBaseTeacher());

        }

        private string OpenSaveDialog(string filter, string defaultExt)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Имя файла для сохранения базы данных";
            saveDialog.Filter = filter;
            saveDialog.DefaultExt = defaultExt;
            saveDialog.FileName = "База данных";
            saveDialog.InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            saveDialog.OverwritePrompt = true;
            if (saveDialog.ShowDialog() != true)
                return null;
            return saveDialog.FileName;
        }

        private void buttonSaveToTxt_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenSaveDialog("Текстовые файлы | *.txt | Все файлы | *.*", "txt");
            if (fileName == null)
                return;
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var teacher in DataBaseTeacher.Teachers)
                {
                    writer.WriteLine(teacher.FullName);
                    writer.WriteLine(teacher.DateOfBirth);
                    writer.WriteLine(teacher.Education);
                    writer.WriteLine(teacher.IsScienceDegree);
                    writer.WriteLine(teacher.Subjects);
                    writer.WriteLine(teacher.Price);
                    writer.WriteLine(teacher.WeekLoad);
                }
            }
        }

        private void buttonLoadFromXml_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenDialog("XML-файлы | *.xml | Все файлы | *.*");
            if (fileName == null)
                return;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            XmlElement xmlElementStock = xmlDocument.DocumentElement;
            if (xmlElementStock == null)
                return;
            foreach (XmlElement xmlElementTeacher in xmlElementStock)
            {
                Teacher teacher = new Teacher();
                XmlNode attributeName = xmlElementTeacher.Attributes["name"];
                if (attributeName != null)
                    teacher.FullName = attributeName.Value;
                foreach (XmlNode childNode in xmlElementTeacher.ChildNodes)
                {
                    switch (childNode.Name)
                    {
                        case "FullName":
                            teacher.FullName = childNode.InnerText;
                            break;
                        case "DateOfBirth":
                            if (DateTime.TryParse(childNode.InnerText, out DateTime dateOfBirth))
                                teacher.DateOfBirth = dateOfBirth;
                            break;
                        case "Education":
                            if (Enum.TryParse(childNode.InnerText, out Education education))
                                teacher.Education = education;
                            break;
                        case "IsScienceDegree":
                            if (bool.TryParse(childNode.InnerText, out bool isScienceDegree))
                                teacher.IsScienceDegree = isScienceDegree;
                            break;
                        case "Subjects":
                            if (Enum.TryParse(childNode.InnerText, out Subjects subjects))
                                teacher.Subjects = subjects;
                            break;
                        case "Price":
                            if (int.TryParse(childNode.InnerText, out int price))
                                teacher.Price = price;
                            break;
                        case "WeekLoad":
                            if (int.TryParse(childNode.InnerText, out int weekLoad))
                                teacher.WeekLoad = weekLoad;
                            break;
                    }
                }
                DataBaseTeacher.Teachers.Add(teacher);
            }
            NavigatorPage.MainFrame.Navigate(new PageDataBaseTeacher());
        }

        private void buttonSaveToXml_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenSaveDialog("XML-файлы | *.xml | Все файлы | *.*", "xml");
            if (fileName == null)
                return;
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode xmlNodeStock = xmlDocument.CreateElement("TeachingRoom");
            xmlDocument.AppendChild(xmlNodeStock);
            foreach (var teacher in DataBaseTeacher.Teachers)
            {
                XmlNode xmlNodeteacher = xmlDocument.CreateElement("Teacher");
                XmlAttribute xmlAttributeName = xmlDocument.CreateAttribute("Name");
                xmlAttributeName.Value = teacher.FullName;
                xmlNodeteacher.Attributes.Append(xmlAttributeName);
                xmlNodeteacher.AppendChild(CreateElement(xmlDocument, "FullName", teacher.FullName.ToString()));
                xmlNodeteacher.AppendChild(CreateElement(xmlDocument, "DateOfBirth", teacher.DateOfBirth.ToString()));
                xmlNodeteacher.AppendChild(CreateElement(xmlDocument, "Education", teacher.Education.ToString()));
                xmlNodeteacher.AppendChild(CreateElement(xmlDocument, "IsScienceDegree", teacher.IsScienceDegree.ToString()));
                xmlNodeteacher.AppendChild(CreateElement(xmlDocument, "Subjects", teacher.Subjects.ToString()));
                xmlNodeteacher.AppendChild(CreateElement(xmlDocument, "Price", teacher.Price.ToString()));
                xmlNodeteacher.AppendChild(CreateElement(xmlDocument, "WeekLoad", teacher.WeekLoad.ToString()));
                xmlNodeStock.AppendChild(xmlNodeteacher);
            }
            xmlDocument.Save(fileName);
        }

        private XmlElement CreateElement(XmlDocument xmlDocument, string nameElement, string element)
        {
            XmlElement xmlElement = xmlDocument.CreateElement(nameElement);
            xmlElement.AppendChild(xmlDocument.CreateTextNode(element));
            return xmlElement;
        }

        private void buttonSaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenSaveDialog("Excel-файлы | *.xlsx | Все файлы | *.*", "xlsx");
            if (fileName == null)
                return;
            DataBaseTeacher.Teachers.SaveToExcel(fileName);
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Хотите выйти из приложения?", "Предупреждение",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                Close();
        }

        private void buttonSaveToWord_Click(object sender, RoutedEventArgs e)
        {
            string fileName = OpenSaveDialog("Word-файлы | *.docx; *.doc | Все файлы | *.*", "doc");
            if (fileName == null)
                return;
            DataBaseTeacher.Teachers.SaveToWord(fileName);
        }
    }
}
