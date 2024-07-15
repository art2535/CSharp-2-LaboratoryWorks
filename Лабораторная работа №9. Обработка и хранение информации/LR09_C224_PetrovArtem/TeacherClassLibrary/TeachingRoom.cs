using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace TeacherClassLibrary
{
    public class TeachingRoom : List<Teacher>
    {
        public List<Teacher> FilterName(string name)
        {
            var selectTeacher = this.Where(teacher => teacher.FullName.ToLower()
            .StartsWith(name.ToLower()))
                .OrderBy(teacher => teacher.FullName)
                .ThenBy(teacher => teacher.ID).Select(teacher => teacher).ToList();
            return selectTeacher;
        }

        public List<Teacher> FilterOutTeachers()
        {
            return this.Where(teacher => teacher.Price < 1500).OrderBy(teacher => teacher.FullName).Select(teacher => teacher).ToList();
        }

        public List<Teacher> FilterSubjects(string subject)
        {
            var selectTeacher = this.Where(p => p.Subjects == (Subjects)Enum.Parse(typeof(Subjects), subject))
                .OrderBy(p => p.FullName)
                .Select(p => p);
            return selectTeacher.ToList();
        }

        public void SaveToExcel(string fileName)
        {
            try
            {
                var application = new Excel.Application();
                application.SheetsInNewWorkbook = 2;
                Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
                application.Worksheets.Item[1].Name = "База данных преподавателей";
                Excel.Worksheet worksheet = application.Worksheets.Item[1];
                worksheet.Cells[1, 1] = "ID";
                worksheet.Cells[1, 2] = "ФИО преподавателя";
                worksheet.Cells[1, 3] = "Дата рождения";
                worksheet.Cells[1, 4] = "Образование";
                worksheet.Cells[1, 5] = "Предмет";
                worksheet.Cells[1, 6] = "Ученая степень";
                worksheet.Cells[1, 7] = "Стоимость одного часа работы";
                worksheet.Cells[1, 8] = "Часовая нагрузка";
                worksheet.Cells[1, 9] = "Итоговая стоимость работы";
                int indexRow = 2;
                foreach (Teacher teacher in DataBaseTeacher.Teachers)
                {
                    worksheet.Cells[indexRow, 1] = teacher.ID;
                    worksheet.Cells[indexRow, 2] = teacher.FullName;
                    worksheet.Cells[indexRow, 3] = teacher.DateOfBirth.ToString("dd.MM.yyyy");
                    worksheet.Cells[indexRow, 4] = teacher.Education;
                    worksheet.Cells[indexRow, 5] = teacher.Subjects;
                    worksheet.Cells[indexRow, 6] = teacher.IsScienceDegree ? "Да" : "Нет";
                    worksheet.Cells[indexRow, 7] = teacher.Price;
                    worksheet.Cells[indexRow, 8] = teacher.WeekLoad;
                    worksheet.Cells[indexRow, 9].Formula =
                        $"=IF(F{indexRow}=\"Да\",3/100*G{indexRow}*H{indexRow}*4,G{indexRow}*H{indexRow}*4)";
                    worksheet.Cells[indexRow, 9].Select();
                    worksheet.Cells[indexRow][9].NumberFormat =
                        "0" + System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator + "00";
                    indexRow++;
                }
                Excel.Range rangeBolder = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[indexRow, 9]];
                rangeBolder.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                    rangeBolder.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                    rangeBolder.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                    rangeBolder.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                    rangeBolder.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                    rangeBolder.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle =
                    Excel.XlLineStyle.xlContinuous;
                worksheet.Columns.AutoFit();
                application.Visible = true;
                if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(fileName)))
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fileName));
                workbook.SaveAs(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SaveToWord(string fileName)
        {
            try
            {
                var application = new Word.Application();
                var document = application.Documents.Add();
                var paragraph = document.Paragraphs.Add();
                paragraph.Range.Text = "Состояние базы данных на " + DateTime.Today.ToString("D");
                paragraph.set_Style("Заголовок 1");
                paragraph.Range.InsertParagraphAfter();
                // добавить таблицу
                var tableParagraph = document.Paragraphs.Add();
                var tableTeachers = document.Tables.Add(tableParagraph.Range, this.Count + 1, 6);
                tableTeachers.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                tableTeachers.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                tableTeachers.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                // добавляем заголовок таблицы
                tableTeachers.Cell(1, 1).Range.Text = "№ п/п";
                tableTeachers.Cell(1, 2).Range.Text = "ФИО преподавателя";
                tableTeachers.Cell(1, 3).Range.Text = "Дата рождения";
                tableTeachers.Cell(1, 4).Range.Text = "Образование";
                tableTeachers.Cell(1, 5).Range.Text = "Предмет";
                tableTeachers.Cell(1, 6).Range.Text = "Ученая степень";
                tableTeachers.Cell(1, 7).Range.Text = "Стоимость одного часа работы";
                tableTeachers.Cell(1, 8).Range.Text = "Часовая нагрузка";
                tableTeachers.Rows[1].Range.Bold = 1;
                tableTeachers.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                // заполняем таблицу
                int numRow = 2;
                foreach(var teacher in this)
                {
                    tableTeachers.Cell(numRow, 1).Range.Text = teacher.ID.ToString();
                    tableTeachers.Cell(numRow, 2).Range.Text = teacher.FullName.ToString();
                    tableTeachers.Cell(numRow, 3).Range.Text = teacher.DateOfBirth.ToString();
                    tableTeachers.Cell(numRow, 4).Range.Text = teacher.Education.ToString();
                    tableTeachers.Cell(numRow, 5).Range.Text = teacher.Subjects.ToString();
                    tableTeachers.Cell(numRow, 6).Range.Text = teacher.IsScienceDegree.ToString();
                    tableTeachers.Cell(numRow, 7).Range.Text = teacher.Price.ToString();
                    tableTeachers.Cell(numRow, 8).Range.Text = teacher.WeekLoad.ToString();
                    numRow++;
                }
                // открыть Word
                application.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder teachingRoom = new StringBuilder();
            foreach(var teacher in this)
                teachingRoom.AppendLine(teacher.ToString());
            return teachingRoom.ToString();
        }
    }
}
