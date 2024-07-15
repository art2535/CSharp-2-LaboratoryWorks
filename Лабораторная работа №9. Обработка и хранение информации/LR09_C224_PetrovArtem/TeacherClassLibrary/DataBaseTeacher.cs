using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherClassLibrary
{
    public static class DataBaseTeacher
    {
        public static TeachingRoom Teachers { get; }
        static DataBaseTeacher()
        {
            Teachers = new TeachingRoom();
            Teachers.Add(new Teacher());
            Teachers.Add(new Teacher
            {
                FullName = "Горбунова Ольга Александровна",
                DateOfBirth = DateTime.Now,
                IsScienceDegree = true,
                Subjects = Subjects.Элементы_высшей_математики,
                Education = Education.Высшее,
                Price = 2200,
                WeekLoad = 19
            });
            Teachers.Add(new Teacher("Преподаватель 3", DateTime.Now, true, Subjects.Основы_алгоритмизации_и_программирования,
                Education.Высшее, 2500, 15));
        }
        public static List<string> GetCategories()
        {
            List<string> categories = new List<string>();
            categories.Add("Все категории");
            categories.AddRange(Enum.GetNames(typeof(Subjects)).ToList());
            return categories;
        }
    }
}
