using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace HierarchicalCustering
{
    class VeriOku
    {
         public  List<Data> list;
        public VeriOku(String path,FormMain form)
        {

            int startColm = 0, colmCount = 0, startLine = 0, numberOfRecords;
            colmCount = Convert.ToInt32(form.textBoxSutunSayisi.Text);
            startLine = Convert.ToInt32(form.textBoxBaslangicSatiri.Text);
            numberOfRecords = Convert.ToInt32(form.numberOfRecords.Text);
            startColm = Convert.ToInt32(form.textBoxBaslangicSutunu.Text);

            list = new List<Data>();

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

           
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

             
            for (int tableIndis = 1; tableIndis <= xlWorkBook.Worksheets.Count; tableIndis++)
            {
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(tableIndis);
                range = xlWorkSheet.UsedRange;
                 Data data = new Data();
                int count = 1;
                for (int sat = startLine; sat < range.Rows.Count; sat++)
                {

                    int sut;
                    double x = 0;
                    for (sut = startColm; sut < range.Columns.Count; sut++)//Veri Sınıfındaki List ti dolduguruyor.
                    {
                        if (count == 1)
                            data.addValue(range.Cells[sat, sut].Value2);
                        else if(count != numberOfRecords && count != 1) //Birden fazla kayıdı toplayarak gidiyor
                        {
                            x = data.getValue(sut - startColm) + range.Cells[sat, sut].Value2;
                            data.values[sut - startColm] = x;
                        }
                        if (count == numberOfRecords)
                        {
                            x = data.getValue(sut - startColm);
                            data.values[sut - startColm] = x / count;
                        } 
                    }
                    count++;
                }
                data.setClusterID(tableIndis);
                data.setName(xlWorkSheet.Name);
                 list.Add(data);
                releaseObject(xlWorkSheet);
            }
           
            
          //  xlWorkBook.Close( );
            xlApp.Quit();

            
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

          
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public List<Data> veriler()
        {
            return list;
        }

         ~VeriOku()
        {
           // MessageBox.Show("Obje sonlabdırıldı");
        }
    }

}
