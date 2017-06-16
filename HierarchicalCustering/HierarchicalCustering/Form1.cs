using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace HierarchicalCustering
{
    public partial class FormMain : Form
    {

        public List<Data> dataSet = new List<Data>();
        public List<Cluster> listKume = new List<Cluster>();
        public int rowCount;
        public int colCount;
        private Excel.Application ExcelObject = null;
        public FormMain()
        {
            InitializeComponent();
            ExcelObject = new Excel.Application();
            if (ExcelObject == null)
            {
                MessageBox.Show("Problem! Dosya Açılamadı.");
                Application.Exit();
            }
        }

        private void buttonDataSet_Click(object sender, EventArgs e)
        {

            colCount = Convert.ToInt32(textBoxSutunSayisi.Text);
            dataGridView1.ColumnCount = colCount;
            // Sütun başlığına ait style tanımlıyoruz.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            for (int i = 1; i < colCount; i++)
            {
                dataGridView1.Columns[i].Name = "X" + i;
            }
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Excel Dosyası(.xlsx) |*.xlsx|Excel Dosyası(xls) |*.xls";
            file.FilterIndex = 1;
            file.RestoreDirectory = true;
            file.CheckFileExists = false;
            file.Title = "Excel Dosyası Seçiniz..";

            if (file.ShowDialog() == DialogResult.OK)
            {
                string DosyaYolu = file.FileName;
                string DosyaAdi = file.SafeFileName;
                if(getDataSetPageCount(DosyaYolu)==1) 
                    dataSet = new VeriOkuSinglePage(DosyaYolu, this).veriler();
                else
                    dataSet = new VeriOku(DosyaYolu,this).veriler();
            }
        }

        private void buttonDataSetWrite_Click(object sender, EventArgs e)
        {
            write(dataSet);
        }
        
        private void buttonClustering_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != "" && textBoxBaslangicSatiri.Text != "" && textBoxBaslangicSutunu.Text != "")
            {
                uzakliklariHesapla();
                Cluster clus = new Cluster();
                listKume = clus.creatCluster(dataSet, listKume);
                //makeClustering();
                clus.showCluster(this);
                int sayac = 1;
                while (listKume.Count != 1)
                {
                    listBox2.Items.Add("-------ADIM--------" + sayac + "-------------");
                    makeClustering();
                    clus.showCluster(this);
                    sayac++;
                }
            }
            else
                MessageBox.Show("Alanları doldurunuz...");
        }
        public void write(List<Data> data)
        {

            for (int k = 0; k <data.Count; k++)
            {
                int i = dataGridView1.Rows.Add();
                i = 0;
                int t;
                dataGridView1.Rows[k].Cells[0].Value = data[k].getName().ToString();
                for (t = 0; t <data[k].values.Count; t++)
                {
                    double deger = data[k].values[t];
                    dataGridView1.Rows[k].Cells[t+1].Value = deger.ToString();

                } 
                i++;
            } 
        }
        public void uzakliklariHesapla()
        {
            for (int sat1 = 0; sat1 < dataSet.Count; sat1++)
            { 
                for (int sat2 = sat1+1; sat2 < dataSet.Count; sat2++)
                {
                    Oklit oklit = new Oklit();
                    Manhattan manhattan = new Manhattan();
                    Minkovski minkovski = new Minkovski();

                    oklit.sonrakiBaginti = manhattan;
                    manhattan.sonrakiBaginti = minkovski;
                     oklit.islem(comboBox1.Text, dataSet[sat2], dataSet[sat1]); 
                }
            }
           
        }
        
        
        public void makeClustering()
        {
            for (int sat1 = 0; sat1 < listKume.Count; sat1++)
            {
                listBox1.Items.Add("**********************");
                for (int sat2 = sat1 + 1; sat2 < listKume.Count; sat2++)
                {
                    listBox1.Items.Add("**********************");
                    List<double> listSortToClustering = new List<double>();
                        for (int i = 0; i < listKume[sat1].cluster.Count; i++)//Yeni Kümenin Eleman sayılarını geziyor.Örn:(1,4) yeni küme
                    {
                            for (int j = 0; j < listKume[sat2].cluster.Count; j++)//Hesaplama yapacağı kümenin eleamanlarının sayısı geziyor.Örn:Hesaplanacak kume:(2,3) 
                        {
                                Oklit oklit = new Oklit();
                                Manhattan manhattan = new Manhattan();
                                Minkovski minkovski = new Minkovski();

                                oklit.sonrakiBaginti = manhattan;
                                manhattan.sonrakiBaginti = minkovski;

                                oklit.islem(comboBox1.Text, listKume[sat2].cluster[j], listKume[sat1].cluster[i]);
                                listSortToClustering.Add(listKume[sat2].cluster[j].getDistance());//Kümenin j. elemanı için uzaklıgı hesapladık ve listeye ekledik.
                                listBox1.Items.Add(listKume[sat1].cluster[i].getName() + "->" + listKume[sat1].cluster[i].getDistance() + "----->" + listKume[sat2].cluster[j].getName()+"->"+ listKume[sat2].cluster[j].getDistance());
                            }
                        }
                    listSortToClustering.Sort(); //Kümeler arasındaki uzaklık belirlenirken, iki kümenin birbirine en uzak yada en yakın olan elemanları arasındaki mesafe, iki küme arasındaki uzunluk olarak tayin edilmesi için listeye eklendi ve sıralandı. 
                    listKume[sat2].setParentCluster(listKume[sat1]); //Kümeleri birleştirmek için işlem yaptıgımız  kümeye parent olarak diger kümeyi ekliyoruz.
                    listBox1.Items.Add("Parent" + listKume[sat2].getParentCluster().cluster[0].getName()); //Sadece parent belli olması için 0.indisi alıp yazdırdım.Tümünü de yazdırabilirdim.
                       if(comboBox2.Text=="En Yakın Komşuluk") 
                            listKume[sat2].setClusteDistance(listSortToClustering[0]);//En yakın için
                       else if(comboBox2.Text== "En Uzak Komşuluk")
                        listKume[sat2].setClusteDistance(listSortToClustering[listSortToClustering.Count-1]);// en uzak
                    for (int l = 0; l < listSortToClustering.Count; l++)
                    {
                        listBox1.Items.Add(listSortToClustering[l]);
                    }
                    listBox1.Items.Add("Küme Uzaklık"+listKume[sat2].getClusteDistance());
                    listSortToClustering.Clear(); //Her döngüden farklı kümeler hesaplanacagından dolayı list temizlendi.
                    }
                UzaklikSirala siralama = new UzaklikSirala();
                siralama.sortCluster(listKume);//Kümelerin uzaklık hesaplamalarını sıraladık
            }
            listKume[1]=combineClusters(listKume[1], listKume[1].getParentCluster()); // 0. indis kendisi olduğu 0 degerli bir sonraki için işlem yapıyor.
            listKume.Remove(listKume[1].getParentCluster());
           
        }
     
        public Cluster combineClusters(Cluster kume1, Cluster kume2)
        {
            for (int i = 0; i < kume2.cluster.Count; i++)
            {
                kume1.cluster.Add(kume2.cluster[i]);
            }
            return kume1;

        }
        public int  getDataSetPageCount(string path)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;   
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            return xlWorkBook.Worksheets.Count;
            
        }


    }
}
