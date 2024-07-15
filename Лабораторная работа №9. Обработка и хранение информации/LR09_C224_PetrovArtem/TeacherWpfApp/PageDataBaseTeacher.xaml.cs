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
    /// Логика взаимодействия для PageDataBaseTeacher.xaml
    /// </summary>
    public partial class PageDataBaseTeacher : Page
    {
        public PageDataBaseTeacher()
        {
            InitializeComponent();
            GridAllTeachers.ItemsSource = DataBaseTeacher.Teachers;
            GridAllTeachers.MaxHeight = (NavigatorPage.MainFrame.DesiredSize.Height > 400) ?
                NavigatorPage.MainFrame.DesiredSize.Height : 400;
            var categories = DataBaseTeacher.GetCategories();
            categories.Insert(0, "Все предметы");
            comboFilterSubjects.ItemsSource = categories;
            comboFilterSubjects.SelectedIndex = 0;
        }


        private void buttonEditTeacher_Click(object sender, RoutedEventArgs e)
        {
            Teacher selectedTeacher = GridAllTeachers.SelectedItem as Teacher;
            NavigatorPage.MainFrame.Navigate(new PageAddTeacher(selectedTeacher));
        }

        private void buttonDeleteTeacher_Click(object sender, RoutedEventArgs e)
        {
            Teacher selectedTeacher = GridAllTeachers.SelectedItem as Teacher;
            if (MessageBox.Show("Удалить запись?", "Предупреждение",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                DataBaseTeacher.Teachers.Remove(selectedTeacher);
        }

        private void comboFilterSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            List<Teacher> filterSubject, filterOut, filterName;
            filterSubject = filterOut = filterName = DataBaseTeacher.Teachers;
            if (comboFilterSubjects.SelectedIndex != 0 && comboFilterSubjects.SelectedValue != null)
                filterSubject = DataBaseTeacher.Teachers.FilterSubjects(comboFilterSubjects.SelectedValue.ToString());
            if (!string.IsNullOrWhiteSpace(textFilterName.Text))
                filterName = DataBaseTeacher.Teachers.FilterName(textFilterName.Text);
            if (checxOutTeachers.IsChecked == true)
                filterOut = DataBaseTeacher.Teachers.FilterOutTeachers();
            GridAllTeachers.ItemsSource = filterSubject.Intersect(filterOut).Intersect(filterName);
            GridAllTeachers.Items.Refresh();
        }


        private void textFilterName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void checxOutTeachers_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }
    }
}
