using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherClassLibrary
{
    public class Teacher
    {
        static int count = 0;
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsScienceDegree { get; set; }
        public Subjects Subjects { get; set; }
        public Education Education { get; set; }
        public int Price { get; set; }
        public int WeekLoad { get; set; }
        public int Itog
        {
            get
            {
                if (IsScienceDegree)
                    return Convert.ToInt32(((0.03 * Price) + Price) * WeekLoad * 4);
                return Convert.ToInt32(Price * WeekLoad * 4);
            }
        }
        public Teacher()
        {
            ++count;
            ID = count;
            FullName = "Преподаватель " + ID;
            DateOfBirth = DateTime.Now;
            IsScienceDegree = false;
            Subjects = Subjects.МДК_01_01_Разработка_программных_модулей_Часть_2;
            Education = Education.Высшее;
            Price = 1200;
            WeekLoad = 20;
        }
        public Teacher(string fullName, DateTime dateOfBirth, bool isScienceDegree,
            Subjects subjects, Education education, int price, int weekLoad) : this()
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            IsScienceDegree = isScienceDegree;
            Subjects = subjects;
            Education = education;
            Price = price;
            WeekLoad = weekLoad;
        }
        public override string ToString()
        {
            return $"{ID}. Преподаватель {FullName} {DateOfBirth:dd.MM.yyyy} года рождения имеет {Education} образование, " +
                $"преподает дисциплину {Subjects}. Оклад - {Price * WeekLoad * 4} руб.";
        }
    }
}
