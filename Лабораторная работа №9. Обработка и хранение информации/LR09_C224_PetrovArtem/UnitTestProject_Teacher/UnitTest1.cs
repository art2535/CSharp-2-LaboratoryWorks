using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeacherClassLibrary;
using System;

namespace UnitTestProject_Teacher
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
        [TestMethod]
        public void ConstructorTeacherDefault()
        {
            Teacher teacher = new Teacher();
            Assert.AreEqual(teacher.ID, 2);
            Assert.AreEqual(teacher.FullName, "Преподаватель 2");
            Assert.AreEqual(teacher.Subjects, Subjects.МДК_01_01_Разработка_программных_модулей_Часть_2);
            Assert.AreEqual(teacher.Education, Education.Высшее);
        }
        [TestMethod]
        public void ConstructorTeacher()
        {
            Teacher teacher = new Teacher("Преподаватель 1", DateTime.Now, true, Subjects.Основы_алгоритмизации_и_программирования,
                Education.Высшее, 2000, 15);
            Assert.AreEqual(teacher.ID, 1);
            Assert.AreEqual(teacher.FullName, "Преподаватель 1");
            Assert.AreEqual(teacher.IsScienceDegree, true);
            Assert.AreEqual(teacher.Subjects, Subjects.Основы_алгоритмизации_и_программирования);
            Assert.AreEqual(teacher.Education, Education.Высшее);
            Assert.AreEqual(teacher.Price, 2000);
            Assert.AreEqual(teacher.WeekLoad, 15);
        }
    }
}
