using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Excel = Microsoft.Office.Interop.Excel;

namespace LR08_C224_PetrovArtem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ImageBrush imageBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Images/фон.jpg"))
            };
            this.Background = imageBrush;
            labelPrepodInfo.Visibility = Visibility.Hidden;
            textBlockItogInfo.Visibility = Visibility.Hidden;
            labelDataBaseInfo.Visibility = Visibility.Hidden;
            textBoxDataBase.Visibility = Visibility.Hidden;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            textBlockTimeStatus.Text = DateTime.Now.ToString("Сегодня: dd.MM.yyyy (dddd)\t\tВремя: HH:mm:ss");
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Предупреждение!",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text) || string.IsNullOrEmpty(textBoxPrice.Text) || string.IsNullOrEmpty(textBoxWeeklyLoad.Text))
            {
                MessageBox.Show("Пожалуйста, введите все необходимые данные", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                string fullName = textBoxFIO.Text;
                string scientificDegree = (bool)radioYes.IsChecked ? "Да" : "Нет";
                DateTime dateOfBirth = calendar.SelectedDate.HasValue ? calendar.SelectedDate.Value : DateTime.Now;
                string education = comboBoxEducatuion.SelectedItem != null ? ((ComboBoxItem)comboBoxEducatuion.SelectedItem).Content.ToString() : "";
                string price = (string)textBoxPrice.Text;
                string weekLoad = (string)textBoxWeeklyLoad.Text;
                string subjects = comboBoxSubjects.SelectedItem != null ? ((ComboBoxItem)comboBoxSubjects.SelectedItem).Content.ToString() : "";
                using (StreamWriter writer = new StreamWriter("DataBase.txt", true, Encoding.GetEncoding(1251)))
                {
                    writer.WriteLine(fullName);
                    writer.WriteLine($"{dateOfBirth:dd.MM.yyyy}");
                    writer.WriteLine(education);
                    writer.WriteLine(subjects);
                    writer.WriteLine(scientificDegree);
                    writer.WriteLine(price);
                    writer.WriteLine(weekLoad);
                    writer.WriteLine("----------------------------------");
                }
                MessageBox.Show("Данные записаны в базу данных", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxFIO.Clear();
            comboBoxEducatuion.SelectedIndex = -1;
            radioYes.IsChecked = false;
            radioNo.IsChecked = false;
            comboBoxSubjects.SelectedIndex = -1;
            textBoxPrice.Clear();
            textBoxWeeklyLoad.Clear();
            textBoxRepeat.Clear();
            textBlockItogInfo.Text = string.Empty;
            calendar.SelectedDate = null;
        }

        private void buttonFind_Click(object sender, RoutedEventArgs e)
        {
            string check = textBoxRepeat.Text;
            StringBuilder result = new StringBuilder();
            bool found = false;
            using (StreamReader reader = new StreamReader("DataBase.txt", Encoding.GetEncoding(1251)))
            {
                if (reader.Peek() == -1)
                {
                    MessageBox.Show("Преподавателей в базе данных нет", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(check))
                {
                    MessageBox.Show("Введите в строку поиска ФИО преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith(check))
                    {
                        found = true;
                        string dateOfBirth = reader.ReadLine();
                        string education = reader.ReadLine();
                        string subject = reader.ReadLine();
                        string scienceDegree = reader.ReadLine();
                        int price = int.Parse(reader.ReadLine());
                        int weekLoad = int.Parse(reader.ReadLine());
                        int sum = price * weekLoad * 4;
                        int itog;
                        if (scienceDegree == "Да")
                        {
                            itog = sum + price * 3 / 100;
                            result.Append($"Сотрудник ФСПО ГУАП {check} {dateOfBirth} года " +
                                $"рождения, имеет {education} образование, учёная степень - {scienceDegree}, " +
                                $"преподает {subject}. Оклад составляет {sum} руб. + 3% премии = {itog} руб.\n");
                            break;
                        }
                        else
                        {
                            itog = sum;
                            result.Append($"Сотрудник ФСПО ГУАП {check} {dateOfBirth} года " +
                                $"рождения, имеет {education} образование, учёная степень - {scienceDegree}, преподает {subject}. " +
                                $"Оклад составляет {sum} руб. + 0% премии = {itog} руб.\n");
                            break;
                        }
                    }
                    reader.ReadLine();
                }
            }
            if (found)
            {
                labelPrepodInfo.Visibility = Visibility.Visible;
                textBlockItogInfo.Visibility = Visibility.Visible;
                textBlockItogInfo.Text = result.ToString();
            }
            else
            {
                MessageBox.Show($"Преподавателя с ФИО {check} в базе данных нет", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void radioLeft_Checked(object sender, RoutedEventArgs e)
        {
            labelDataBase.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private void radioCenterHorisontal_Checked(object sender, RoutedEventArgs e)
        {
            labelDataBase.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void radioRight_Checked(object sender, RoutedEventArgs e)
        {
            labelDataBase.HorizontalAlignment = HorizontalAlignment.Right;
        }

        private void radioTop_Checked(object sender, RoutedEventArgs e)
        {
            labelDataBase.VerticalAlignment = VerticalAlignment.Top;
        }

        private void radioCenterVertical_Checked(object sender, RoutedEventArgs e)
        {
            labelDataBase.VerticalAlignment = VerticalAlignment.Center;
        }

        private void radioBottom_Checked(object sender, RoutedEventArgs e)
        {
            labelDataBase.VerticalAlignment = VerticalAlignment.Bottom;
        }

        private void radioTimesNewRoman_Checked(object sender, RoutedEventArgs e)
        {
            labelDataBase.FontFamily = new FontFamily("Times New Roman");
        }

        private void radioArial_Checked(object sender, RoutedEventArgs e)
        {
            labelDataBase.FontFamily = new FontFamily("Arial");
        }

        private void radioRoboto_Checked(object sender, RoutedEventArgs e)
        {
            labelDataBase.FontFamily = new FontFamily("Roboto");
        }

        private void toggleBold_Checked(object sender, RoutedEventArgs e)
        {
            labelDataBase.FontWeight = FontWeights.Bold;
        }

        private void togglePitalic_Checked(object sender, RoutedEventArgs e)
        {
            labelDataBase.FontStyle = FontStyles.Italic;
        }

        private void radioTimesNewRoman_Unchecked(object sender, RoutedEventArgs e)
        {
            labelDataBase.FontFamily = new FontFamily("Segoe UI");
        }

        private void radioArial_Unchecked(object sender, RoutedEventArgs e)
        {
            labelDataBase.FontFamily = new FontFamily("Segoe UI");
        }

        private void radioRoboto_Unchecked(object sender, RoutedEventArgs e)
        {
            labelDataBase.FontFamily = new FontFamily("Segoe UI");
        }

        private void toggleBold_Unchecked(object sender, RoutedEventArgs e)
        {
            labelDataBase.FontWeight = FontWeights.Normal;
        }

        private void togglePitalic_Unchecked(object sender, RoutedEventArgs e)
        {
            labelDataBase.FontStyle = FontStyles.Normal;
        }

        private void radioPhoto1_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Images/фон-2.jpg"));
            this.Background = imageBrush;
        }

        private void radioPhoto2_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Images/фон.jpg"));
            this.Background = imageBrush;
        }

        private void radioRed_Click(object sender, RoutedEventArgs e)
        {
            Background = new SolidColorBrush(Colors.LightYellow);
        }

        private void radioGreen_Click(object sender, RoutedEventArgs e)
        {
            Background = new SolidColorBrush(Colors.LightGreen);
        }

        private void radioBlue_Click(object sender, RoutedEventArgs e)
        {
            Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void OpenDataBase_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстовые файлы | *.txt | Все файлы | *.*";
            openDialog.InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            openDialog.FileName = "DataBase.txt";
            openDialog.DefaultExt = "txt";
            openDialog.Multiselect = false;
            openDialog.CheckFileExists = true;
            if (openDialog.ShowDialog() != true)
                return;
            string fileName = openDialog.FileName;
            labelDataBaseInfo.Visibility = Visibility.Visible;
            textBoxDataBase.Visibility = Visibility.Visible;
            using (StreamReader reader = new StreamReader(fileName, Encoding.GetEncoding(1251)))
            {
                if (reader.Peek() == -1)
                    textBoxDataBase.Text = "База данных пуста";
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    textBoxDataBase.Text += line + '\n';
                }
            }
        }

        private void CloseDataBase_Click(object sender, RoutedEventArgs e)
        {
            labelDataBaseInfo.Visibility = Visibility.Hidden;
            textBoxDataBase.Visibility = Visibility.Hidden;
        }
    }
}
