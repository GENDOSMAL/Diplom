using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
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
using static RepairFlatWPF.Model.OrderDesc;

namespace RepairFlatWPF.UserControls.OrderWork.InformationAboutOrder
{
    /// <summary>
    /// Interaction logic for MakeSmetaForTask.xaml
    /// </summary>
    public partial class MakeSmetaForTask : UserControl
    {
        Guid idOrder;
        MakeDataAboutAllTaskInOrder DataAboutTask;
        List<int> SelectedItems = new List<int>();
        List<System.Windows.Controls.CheckBox> DataAboutCheck = new List<System.Windows.Controls.CheckBox>();
        BaseWindow window;
        public MakeSmetaForTask(ref BaseWindow baseWindow, Guid idorder)
        {
            InitializeComponent();
            this.idOrder = idorder;
            MakeDaraAboutTask();
            window = baseWindow;
        }

        private void AllTask_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var check in DataAboutCheck)
            {
                check.IsChecked = true;
            }
        }

        private void AllTask_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var check in DataAboutCheck)
            {
                check.IsChecked = false;
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void PrintData_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItems.Count == 0)
            {
                MakeSomeHelp.MSG("Укажите заказы для печати сметы по ним!", MsgBoxImage: MessageBoxImage.Hand);
            }
            else
            {//Тут печать
                MakeWordDOC();
            }
        }
        private void MakeWordDOC()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                winword.ShowAnimation = false;
                winword.Visible = false;
                object missing = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                foreach (var task in DataAboutTask.TaskInf)
                {
                    if (SelectedItems.Contains(task.numb))
                    {
                        SelectedItems.Remove(task.numb);
                        Microsoft.Office.Interop.Word.Paragraph Center = document.Content.Paragraphs.Add(ref missing);
                        Center.Range.Text = $"";
                        Center.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

                        Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                        para1.Range.Font.Name = "Times New Roman";
                        para1.Range.Font.Size = 20;
                        para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        para1.Range.Text = $"Смета по заданию № {task.numb} {Environment.NewLine}";

                        Microsoft.Office.Interop.Word.Paragraph Left = document.Content.Paragraphs.Add(ref missing);
                        Left.Range.Text = $"";
                        Left.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                        Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                        para2.Range.Font.Name = "Times New Roman";
                        para2.Range.Font.Size = 14;
                        para2.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        para2.Range.Text = $"Адрес: {DataAboutTask.AdressOfWork} {Environment.NewLine}";
                        para2.Range.Text = $"Способ связи: {DataAboutTask.Contact} {Environment.NewLine}";
                        para2.Range.Text = $"ФИО заказчика: {DataAboutTask.FIO} {Environment.NewLine}";
                        para2.Range.Text = $"Дата начала: {task.DateStart.Value.ToString("dd.MM.yyyy")} {Environment.NewLine}";
                        para2.Range.Text = $"Планируемая дата завершения: {task.DateEnd.Value.ToString("dd.MM.yyyy")} {Environment.NewLine}";
                        para2.Range.Text = $"Стоимость: {task.Summa} {Environment.NewLine}";

                        if (task.InfAbServ != null)
                        {
                            if (task.InfAbServ.ServisInf != null)
                            {
                                if (task.InfAbServ.ServisInf.Any())
                                {
                                    if (task.InfAbServ.ServisInf.Count != 0)
                                    {
                                        Microsoft.Office.Interop.Word.Paragraph Center1 = document.Content.Paragraphs.Add(ref missing);
                                        Center1.Range.Text = $"";
                                        Center1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;


                                        Microsoft.Office.Interop.Word.Paragraph SummaServ1 = document.Content.Paragraphs.Add(ref missing);
                                        SummaServ1.Range.Font.Name = "Times New Roman";
                                        SummaServ1.Range.Font.Size = 16;
                                        SummaServ1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                        SummaServ1.Range.Text = $"Информация об услугах {Environment.NewLine}";
                                        Microsoft.Office.Interop.Word.Paragraph Left1 = document.Content.Paragraphs.Add(ref missing);
                                        Left1.Range.Text = $"";
                                        Left1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                                        Microsoft.Office.Interop.Word.Table ServTable = document.Tables.Add(para1.Range, task.InfAbServ.ServisInf.Count + 1, 5, ref missing, ref missing);

                                        ServTable.Borders.Enable = 1;
                                        int i = 0, col = 0;
                                        double summa = 0;

                                        foreach (Row row in ServTable.Rows)
                                        {
                                            col = 0;
                                            foreach (Cell cell in row.Cells)
                                            {
                                                int colu = row.Cells.Count;
                                                if (cell.RowIndex == 1)
                                                {
                                                    cell.Range.Text = $"{SomeEnums.TaskServisTable[i]}";
                                                    i++;
                                                    cell.Range.Font.Bold = 1;
                                                    cell.Range.Font.Name = "Times New Roman";
                                                    cell.Range.Font.Size = 13;
                                                    cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                                                    cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                                    cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                                                } 
                                                else
                                                {
                                                    cell.Range.Font.Name = "Times New Roman";
                                                    cell.Range.Font.Size = 12;
                                                    cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                                    cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                                                    switch (cell.ColumnIndex - 1)
                                                    {
                                                        case 0: cell.Range.Text = (task.InfAbServ.ServisInf[cell.RowIndex - 2].numb).ToString(); break;
                                                        case 1: cell.Range.Text = (task.InfAbServ.ServisInf[cell.RowIndex - 2].NameOfServises).ToString(); break;
                                                        case 2: cell.Range.Text = (task.InfAbServ.ServisInf[cell.RowIndex - 2].count).ToString(); break;
                                                        case 3: cell.Range.Text = (task.InfAbServ.ServisInf[cell.RowIndex - 2].cost).ToString(); break;
                                                        case 4: { cell.Range.Text = (task.InfAbServ.ServisInf[cell.RowIndex - 2].summa).ToString(); summa = summa + task.InfAbServ.ServisInf[cell.RowIndex - 2].summa ?? default; } break;
                                                    }
                                                }
                                            }
                                        }
                                        Microsoft.Office.Interop.Word.Paragraph Right = document.Content.Paragraphs.Add(ref missing);
                                        Right.Range.Text = $"";
                                        Right.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                        Microsoft.Office.Interop.Word.Paragraph SummaServ = document.Content.Paragraphs.Add(ref missing);
                                        SummaServ.Range.Font.Name = "Times New Roman";
                                        SummaServ.Range.Font.Size = 14;
                                        SummaServ.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                        SummaServ.Range.Text = $"Сумма: {summa}{Environment.NewLine}";
                                        ServTable.AutoFitBehavior(Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow);
                                    }
                                }
                            }
                        }

                        if (task.InfAbMat != null)
                        {
                            if (task.InfAbMat.materialsInf != null)
                            {
                                if (task.InfAbMat.materialsInf.Any())
                                {
                                    if (task.InfAbMat.materialsInf.Count != 0)
                                    {
                                        Microsoft.Office.Interop.Word.Paragraph Center1 = document.Content.Paragraphs.Add(ref missing);
                                        Center1.Range.Text = $"";
                                        Center1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

                                        Microsoft.Office.Interop.Word.Paragraph HeaderServ = document.Content.Paragraphs.Add(ref missing);
                                        HeaderServ.Range.Font.Name = "Times New Roman";
                                        HeaderServ.Range.Font.Size = 16;
                                        HeaderServ.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                        HeaderServ.Range.Text = $"Информация о материалах {Environment.NewLine}";

                                        Microsoft.Office.Interop.Word.Paragraph Left1 = document.Content.Paragraphs.Add(ref missing);
                                        Left1.Range.Text = $"";
                                        Left1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                                        Microsoft.Office.Interop.Word.Table MatTable = document.Tables.Add(para1.Range, task.InfAbMat.materialsInf.Count + 1, 5, ref missing, ref missing);

                                        MatTable.Borders.Enable = 1;
                                        int i = 0, col = 0;
                                        double summa = 0;

                                        foreach (Row row in MatTable.Rows)
                                        {
                                            col = 0;
                                            foreach (Cell cell in row.Cells)
                                            {
                                                int colu = row.Cells.Count;

                                                if (cell.RowIndex == 1)
                                                {
                                                    cell.Range.Text = $"{SomeEnums.TaskMaterialTable[i]}";
                                                    i++;
                                                    cell.Range.Font.Bold = 1;
                                                    cell.Range.Font.Name = "Times New Roman";
                                                    cell.Range.Font.Size = 13;
                                                    cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                                                    cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                                    cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                                                }
                                                else
                                                {
                                                    cell.Range.Font.Name = "Times New Roman";
                                                    cell.Range.Font.Size = 12;
                                                    cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                                    cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                                                    string name = task.InfAbMat.materialsInf[cell.RowIndex - 2].NameOfMaterials;
                                                    switch (cell.ColumnIndex - 1)
                                                    {
                                                        case 0: cell.Range.Text = (task.InfAbMat.materialsInf[cell.RowIndex - 2].numb).ToString(); break;
                                                        case 1: cell.Range.Text = (task.InfAbMat.materialsInf[cell.RowIndex - 2].NameOfMaterials).ToString(); break;
                                                        case 2: cell.Range.Text = (task.InfAbMat.materialsInf[cell.RowIndex - 2].count).ToString(); break;
                                                        case 3: cell.Range.Text = (task.InfAbMat.materialsInf[cell.RowIndex - 2].cost).ToString(); break;
                                                        case 4: { cell.Range.Text = (task.InfAbMat.materialsInf[cell.RowIndex - 2].summa).ToString(); summa = summa + task.InfAbMat.materialsInf[cell.RowIndex - 2].summa ?? default; } break;
                                                    }
                                                }
                                            }
                                        }
                                        Microsoft.Office.Interop.Word.Paragraph Right = document.Content.Paragraphs.Add(ref missing);
                                        Right.Range.Text = $"";
                                        Right.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                        Microsoft.Office.Interop.Word.Paragraph SummaServ = document.Content.Paragraphs.Add(ref missing);
                                        SummaServ.Range.Font.Name = "Times New Roman";
                                        SummaServ.Range.Font.Size = 14;
                                        SummaServ.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                                        SummaServ.Range.Text = $"Сумма: {summa}{Environment.NewLine}";
                                        MatTable.AutoFitBehavior(Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow);

                                    }

                                }
                            }
                        }

                        if (task.InfAbWorkers != null)
                        {
                            if (task.InfAbWorkers.WorkerInf != null)
                            {
                                if (task.InfAbWorkers.WorkerInf.Any())
                                {
                                    if (task.InfAbWorkers.WorkerInf.Count != 0)
                                    {
                                        Microsoft.Office.Interop.Word.Paragraph Center1 = document.Content.Paragraphs.Add(ref missing);
                                        Center1.Range.Text = $"";
                                        Center1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;

                                        Microsoft.Office.Interop.Word.Paragraph HeaderServ = document.Content.Paragraphs.Add(ref missing);
                                        HeaderServ.Range.Font.Name = "Times New Roman";
                                        HeaderServ.Range.Font.Size = 16;
                                        HeaderServ.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                        HeaderServ.Range.Text = $"Информация о назначенных работниках {Environment.NewLine}";

                                        Microsoft.Office.Interop.Word.Paragraph Left1 = document.Content.Paragraphs.Add(ref missing);
                                        Left1.Range.Text = $"";
                                        Left1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                                        Microsoft.Office.Interop.Word.Table WorkTable = document.Tables.Add(para1.Range, task.InfAbWorkers.WorkerInf.Count + 1, 4, ref missing, ref missing);

                                        WorkTable.Borders.Enable = 1;
                                        int i = 0, col = 0;
                                        double summa = 0;

                                        foreach (Row row in WorkTable.Rows)
                                        {
                                            col = 0;
                                            foreach (Cell cell in row.Cells)
                                            {
                                                int colu = row.Cells.Count;
                                                
                                                if (cell.RowIndex == 1)
                                                {
                                                    cell.Range.Text = $"{SomeEnums.TaskWorkerTable[i]}";
                                                    i++;
                                                    cell.Range.Font.Bold = 1;
                                                    cell.Range.Font.Name = "Times New Roman";
                                                    cell.Range.Font.Size = 13;
                                                    cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                                                    cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                                    cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                                                }
                                                else
                                                {
                                                    cell.Range.Font.Name = "Times New Roman";
                                                    cell.Range.Font.Size = 12;
                                                    cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                                    cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                                                    switch (cell.ColumnIndex - 1)
                                                    {
                                                        case 0: cell.Range.Text = (task.InfAbWorkers.WorkerInf[cell.RowIndex - 2].numb).ToString(); break;
                                                        case 1: cell.Range.Text = (task.InfAbWorkers.WorkerInf[cell.RowIndex - 2].FioOfWorker).ToString(); break;
                                                        case 2: cell.Range.Text = (task.InfAbWorkers.WorkerInf[cell.RowIndex - 2].NameOfPost).ToString(); break;
                                                        case 3: cell.Range.Text = (task.InfAbWorkers.WorkerInf[cell.RowIndex - 2].Role).ToString(); break;
                                                        
                                                    }
                                                }
                                            }
                                        }
                                        WorkTable.AutoFitBehavior(Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow);
                                    }
                                }
                            }
                        }


                        if (SelectedItems.Count != 0)
                            document.Words.Last.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak);
                    }
                }

                winword.Visible = true;
                window.Close();
                document.Activate();

            }
            catch (Exception ex)
            {
                MakeSomeHelp.MSG($"Что-то пошло не так <{ex.ToString()}>", MsgBoxImage: MessageBoxImage.Error);
            }
        }


        async void MakeDaraAboutTask()
        {
            var DataDle = await System.Threading.Tasks.Task.Run(() => MakeSomeHelp.MakeDownloadByLink($"api/Order/doc/smeta2?idOrder={idOrder}"));
            var InfForSmeta = JsonConvert.DeserializeObject<MakeDataAboutAllTaskInOrder>(DataDle.ToString());
            if (InfForSmeta.success)
            {
                DataAboutTask = InfForSmeta;
                foreach (var task in DataAboutTask.TaskInf)
                {
                    System.Windows.Controls.CheckBox checkBox = new System.Windows.Controls.CheckBox();
                    checkBox.Content = $"{task.numb} : {task.Description?.Trim()} : {task.Summa} :{task.DateStart.Value.ToString("dd.MM.yyyy")} : {task.DateEnd.Value.ToString("dd.MM.yyyy")}";
                    checkBox.Uid = task.numb.ToString();
                    checkBox.Checked += CheckCheckBox;
                    checkBox.Unchecked += CheckUnchecked;
                    ForCheckBox.Children.Add(checkBox);
                    DataAboutCheck.Add(checkBox);
                }
            }
        }
        private void CheckCheckBox(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox chBox = (System.Windows.Controls.CheckBox)sender;
            int SelectUid = Convert.ToInt32(chBox.Uid);
            if (!SelectedItems.Contains(SelectUid))
            {
                SelectedItems.Add(SelectUid);
            }
        }
        private void CheckUnchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox chBox = (System.Windows.Controls.CheckBox)sender;
            int SelectUid = Convert.ToInt32(chBox.Uid);
            if (SelectedItems.Contains(SelectUid))
            {
                SelectedItems.Remove(SelectUid);
            }
        }
    }
}
