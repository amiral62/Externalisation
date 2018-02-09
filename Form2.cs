using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text.RegularExpressions;

//using NPOI.SS.UserModel;

namespace Externalisation
{
    public partial class Form2 : Form
    {
        Path pth = new Path();
        List<string> sheetList { get; set; }
        DataGridLigne dgvrow = new DataGridLigne();

        public Form2()
        {
            InitializeComponent();
           // lvwColumnSorter = new ListViewColumnSorter();
           // this.listView1.ListViewItemSorter = lvwColumnSorter;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "XML Files (*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb) |*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb";
                openFileDialog1.FilterIndex = 3;

                openFileDialog1.Multiselect = false;
                openFileDialog1.Title = "Choisir fichier";
                openFileDialog1.InitialDirectory = @"Desktop";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string pathName = openFileDialog1.FileName;
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    DataTable tbContainer = new DataTable();
                    string strConn = string.Empty;
                    string sheetName = fileName;

                    FileInfo file = new FileInfo(pathName);
                    if (!file.Exists) { throw new Exception("Error, file doesn't exists!"); }
                    string extension = file.Extension;
                    switch (extension)
                    {
                        case ".xls":
                            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                            break;
                        case ".xlsx":
                            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
                            break;
                        default:
                            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                            break;
                    }
                    OleDbConnection cnnxls = new OleDbConnection(strConn);
                    OleDbDataAdapter oda = new OleDbDataAdapter(string.Format("select * from [8EME DEPART$]"), cnnxls);
                    oda.Fill(tbContainer);

                    dataGridView1.DataSource = tbContainer;
                    sheetList = ListSheetInExcel(pathName);

                    foreach (string sheet in sheetList)
                    {
                        comboBox1.Items.Add(sheet);
                    }
                }



            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }

            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 11);
            comboBox1.Show();
            label1.Show();
            
        }

        
        public int GetColumnsCount(ISheet sheet)
        {
            int c = 0 ;
            while (sheet.GetRow(0).GetCell(c) != null)
                c++;
            return c; 
        }

        private void RechercheByDGV_Load(object sender, EventArgs e)
        {
            this.comboBox1.Hide();
            this.label1.Hide();
        }



        public List<string> ListSheetInExcel(string filePath)
        {
            OleDbConnectionStringBuilder sbConnection = new OleDbConnectionStringBuilder();
            String strExtendedProperties = String.Empty;
            sbConnection.DataSource = filePath;
            
           
             sbConnection.Provider = "Microsoft.ACE.OLEDB.12.0";
             strExtendedProperties = "Excel 12.0;HDR=Yes;IMEX=1";
        
            sbConnection.Add("Extended Properties", strExtendedProperties);
            List<string> listSheet = new List<string>();
            using (OleDbConnection conn = new OleDbConnection(sbConnection.ToString()))
            {
                conn.Open();
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow drSheet in dtSheet.Rows)
                {
                    if (drSheet["TABLE_NAME"].ToString().Contains("$"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                    {
                        listSheet.Add(drSheet["TABLE_NAME"].ToString());
                    }
                }
            }
            return listSheet;
        }

  

    
    }

      
    }



    

