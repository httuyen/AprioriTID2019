using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SieuThi.Views
{
    public partial class Form2 : Form
    {
        public static Model.SieuThi st;
        public Form2(Model.SieuThi sts)
        {
            InitializeComponent();
            st = sts;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (Program.flag.Equals(0))
            {
                this.button1.Enabled = false;
            }else
            {
                this.button1.Enabled = true;
            }
            if (Program.KetNoi() == 0) MessageBox.Show("Lỗi kết nối đến máy chủ");
            else
            {
                #region tâp F
                
                listView1.View = View.Details;
                listView1.GridLines = true;
                listView1.FullRowSelect = true;
                listView1.Items.Clear();
                listView1.Columns.Clear();

                listView1.Columns.Add("SỐ HD");
                listView1.Columns.Add("ỨNG VIÊN");

                ListViewItem itm1;
                //in tap F0
                foreach (KeyValuePair<int,string> itemTID in Program.listF.ElementAt(0))
                {
                    itm1 = new ListViewItem(itemTID.Key.ToString());
                    itm1.SubItems.Add(itemTID.Value.ToString());
                    listView1.Items.Add(itm1);
                }

                #endregion
                //istL = Controller.Process.generate_L_From_F(Program.mapInput_F1);
                
                #region Tập ứng viên C

                listView2.View = View.Details;
                listView2.GridLines = true;
                listView2.FullRowSelect = true;
                listView2.Items.Clear();
                listView2.Columns.Clear();

                listView2.Columns.Add("UNGVIEN");
                listView2.Columns.Add("SUPPORT");

                ListViewItem itm2;

                Controller.Process.generate_L_From_F(Program.mapInput_F1,true);
                foreach (var item in Program.list_map_L.ElementAt(0))
                {
                    int value = item.Value;
                    float count = Program.mapMaHoa.Count;
                    float supCount = (value / count) * 100;
                    if (supCount >= Program.minSup)
                    {
                        itm2 = new ListViewItem(item.Key.ToString());
                        itm2.SubItems.Add(item.Value.ToString());
                        listView2.Items.Add(itm2);
                    }

                }
                #endregion
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.flag++;
            if (Program.flag.Equals(0))
            {
                this.button1.Enabled = false;
            }
            else
            {
                this.button1.Enabled = true;
            }
            List<string> listL = new List<string>();
            Dictionary<int, string> map_F1 = new Dictionary<int, string>();


            // Program.listL.Add(listL);
            listL = Controller.Process.generate_L_From_F(Program.mapInput_F1,false);
            Program.mapInput_F1 = Controller.Process.generateNewF(Program.mapInput_F1,listL);

            Controller.Process.generate_L_From_F(Program.mapInput_F1,true);
            Program.listF.Add(Program.mapInput_F1);
           
            //frmMain.listC(Program.);
            if (Program.KetNoi() == 0) MessageBox.Show("Lỗi kết nối đến máy chủ");
            else
            {
                #region tâp F

                listView1.View = View.Details;
                listView1.GridLines = true;
                listView1.FullRowSelect = true;
                listView1.Items.Clear();
                listView1.Columns.Clear();

                listView1.Columns.Add("SỐ HD");
                listView1.Columns.Add("ỨNG VIÊN");

                ListViewItem itm1;
                //in tap F0
                foreach (KeyValuePair<int, string> itemTID in Program.listF.ElementAt(Program.flag))
                {
                    itm1 = new ListViewItem(itemTID.Key.ToString());
                    itm1.SubItems.Add(itemTID.Value.ToString());
                    listView1.Items.Add(itm1);
                }
                #endregion
                //istL = Controller.Process.generate_L_From_F(Program.mapInput_F1);
                #region Tập ứng viên C

                listView2.View = View.Details;
                listView2.GridLines = true;
                listView2.FullRowSelect = true;
                listView2.Items.Clear();
                listView2.Columns.Clear();

                listView2.Columns.Add("UNGVIEN");
                listView2.Columns.Add("SUPPORT");

                ListViewItem itm2;
                foreach (var item in Program.list_map_L.ElementAt(Program.flag))
                {
                    int value = item.Value;
                    float count = Program.mapMaHoa.Count;
                    float supCount = (value / count) * 100;
                    if (supCount >= Program.minSup)
                    {
                        itm2 = new ListViewItem(item.Key.ToString());
                        itm2.SubItems.Add(item.Value.ToString());
                        listView2.Items.Add(itm2);
                    }
                    
                }
                #endregion
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.flag--;
            if (Program.flag.Equals(0))
            {
                this.button1.Enabled = false;
            }
            else
            {
                this.button1.Enabled = true;
            }
            //frmMain.listC(Program.);
            if (Program.KetNoi() == 0) MessageBox.Show("Lỗi kết nối đến máy chủ");
            else
            {
                #region tâp F

                listView1.View = View.Details;
                listView1.GridLines = true;
                listView1.FullRowSelect = true;
                listView1.Items.Clear();
                listView1.Columns.Clear();

                listView1.Columns.Add("SỐ HD");
                listView1.Columns.Add("ỨNG VIÊN");

                ListViewItem itm1;
               
                //in tap F0

                foreach (KeyValuePair<int, string> itemTID in Program.listF.ElementAt(Program.flag))
                {
                    itm1 = new ListViewItem(itemTID.Key.ToString());
                    itm1.SubItems.Add(itemTID.Value.ToString());
                    listView1.Items.Add(itm1);
                }
                #endregion
                //istL = Controller.Process.generate_L_From_F(Program.mapInput_F1);
                #region Tập ứng viên C

                listView2.View = View.Details;
                listView2.GridLines = true;
                listView2.FullRowSelect = true;
                listView2.Items.Clear();
                listView2.Columns.Clear();

                listView2.Columns.Add("UNGVIEN");
                listView2.Columns.Add("SUPPORT");

                ListViewItem itm2;
                foreach (var item in Program.list_map_L.ElementAt(Program.flag))
                {
                    int value = item.Value;
                    float count = Program.mapMaHoa.Count;
                    float supCount = (value / count) * 100;
                    if (supCount >= Program.minSup)
                    {
                        itm2 = new ListViewItem(item.Key.ToString());
                        itm2.SubItems.Add(item.Value.ToString());
                        listView2.Items.Add(itm2);
                    }

                }
                #endregion
            }
        }
    }
}