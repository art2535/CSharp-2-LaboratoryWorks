using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TeacherClassLibrary;

namespace TeacherWpfApp
{
    /// <summary>
    /// Логика взаимодействия для PageAddTeacher.xaml
    /// </summary>
    public partial class PageAddTeacher : Page
    {
        Teacher teacher;
        bool IsEditTeacher;
        public PageAddTeacher()
        {
            InitializeComponent();
            teacher = new Teacher();
            IsEditTeacher = false;
            DataContext = teacher;
            comboBoxSubjects.ItemsSource = Enum.GetNames(typeof(Subjects)).ToList();
            comboBoxEducation.ItemsSource = Enum.GetNames(typeof(Education)).ToList();
        }
        public PageAddTeacher(Teacher selectedTeacher) : this()
        {
            teacher = selectedTeacher;
            DataContext = teacher;
            IsEditTeacher = true;
            buttonAdd.Content = "Сохранить";
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEditTeacher)
                DataBaseTeacher.Teachers.Add(teacher);
            NavigatorPage.MainFrame.Navigate(new PageDataBaseTeacher());
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigatorPage.MainFrame.CanGoBack)
            {
                if (MessageBox.Show("Не сохранять данные?", "Предупреждение",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    NavigatorPage.MainFrame.GoBack();
            }
            else
                NavigatorPage.MainFrame.Navigate(new PageDataBaseTeacher());
        }
    }
}
