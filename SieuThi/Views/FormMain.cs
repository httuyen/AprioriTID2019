using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SieuThi.Model;

namespace SieuThi.Views
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();


        }
        public static Model.SieuThi st;

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (Program.KetNoi() == 0) MessageBox.Show("Lỗi kết nối đến máy chủ");
            else
            {
                //Controller.Process.mainProcess();
                #region bảng Giao tác
                listView1.View = View.Details;
                listView1.GridLines = true;
                listView1.FullRowSelect = true;
                listView1.Columns.Add("SO HD");
                Program.minConf = Program.minSup = 0;
                //Thêm tiêu đề cho cột

                st = new Model.SieuThi();
                st.lstMaSP = new List<string>();
                st.lstHD = new List<HoaDon>();
                string strLenh = "EXEC [dbo].[SP_GIAOTAC]" + " @minsup = 1";

                Program.myReader = Program.ExecSqlDataReader(strLenh);
                if (Program.myReader == null) return;

                //get listMaSp
                for (int i = 1; i < Program.myReader.FieldCount; i++)
                {
                    st.lstMaSP.Add(Program.myReader.GetName(i).ToString());
                }
                
                while (Program.myReader.Read())
                {
                    HoaDon hd = new HoaDon();
                    hd.MaHD = Program.myReader.GetString(0).ToString();
                    for (int i = 0; i < st.lstMaSP.Count; i++)
                    {
                        hd.CTHD.Add(Program.myReader[st.lstMaSP[i]].ToString());
                    }

                    st.lstHD.Add(hd);
                }

                //add data to D
                for (int i = 0; i < st.lstHD.Count; i++) 
                {
                    string temp = "";
                    for (int j = 0; j < st.lstMaSP.Count;j++)
                    {
                        temp += st.lstMaSP[j].ToString()+",";
//                        Program.data.Add(i, st.lstMaSP[i]);
                    }
                }

                //add lên màn hình list view
                //add title
                for (int i = 0; i < st.lstMaSP.Count; i++)
                {
                    listView1.Columns.Add(st.lstMaSP[i]);
                }

                //add data

                ListViewItem itm;
                foreach (var item in st.lstHD)
                {

                    itm = new ListViewItem(item.MaHD);

                    for (int i = 0; i < item.CTHD.Count; i++)
                    {
                        itm.SubItems.Add(item.CTHD[i]);
                    }

                    listView1.Items.Add(itm);
                }


                #endregion

                #region Mã hóa items

                listView2.View = View.Details;
                listView2.GridLines = true;
                listView2.FullRowSelect = true;

                listView2.Columns.Add("Mã Items");
                listView2.Columns.Add("Mã mặt hàng");

                ListViewItem itm1;

                for (int i = 0; i < st.lstMaSP.Count; i++)
                {

                    itm1 = new ListViewItem((i+1).ToString());


                    itm1.SubItems.Add(st.lstMaSP[i]);

                    listView2.Items.Add(itm1);
                }
                #endregion
            }

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            txtMinsup.Text = trackBar1.Value.ToString();
        }

        private void btnTimTapD_Click(object sender, EventArgs e)
        {
            Program.flag = 0;
            if (Program.KetNoi() == 0) MessageBox.Show("Lỗi kết nối đến máy chủ");
            else
            {
                Program.mapMaHoa.Clear();
                Program.mapInput_F1.Clear();
                Program.map_L1.Clear();
                Program.listF.Clear();
                Program.list_map_L.Clear();
                Program.minSup = int.Parse(txtMinsup.Text);
                listView1.Clear();
                listView1.View = View.Details;
                listView1.GridLines = true;
                listView1.FullRowSelect = true;
                listView1.Columns.Add("So HD");


                st = new Model.SieuThi();
                st.lstMaSP = new List<string>();
                st.lstHD = new List<HoaDon>();
                string strLenh = "EXEC [dbo].[SP_GIAOTAC]" + " @minsup = " + txtMinsup.Text;

                Program.myReader = Program.ExecSqlDataReader(strLenh);
                if (Program.myReader == null) return;


                for (int i = 1; i < Program.myReader.FieldCount; i++)
                {
                    st.lstMaSP.Add(Program.myReader.GetName(i).ToString());
                }

                while (Program.myReader.Read())
                {
                    HoaDon hd = new HoaDon();
                    hd.MaHD = Program.myReader.GetString(0).ToString();
                   
                    for (int i = 0; i < st.lstMaSP.Count; i++)  
                    {
                        hd.CTHD.Add(Program.myReader[st.lstMaSP[i]].ToString());
                      
                    }
                    
                    st.lstHD.Add(hd);
                }

                //add lên màn hình list view
                //add title
                for (int i = 0; i < st.lstMaSP.Count; i++)
                {
                    listView1.Columns.Add(st.lstMaSP[i]);
                }

                //add data

                ListViewItem itm;
                int TID = 1;
                foreach (var item in st.lstHD)
                {
                    itm = new ListViewItem(item.MaHD);
                    string strMH = "";
                    for (int i = 0; i < item.CTHD.Count; i++)
                    {
                        itm.SubItems.Add(item.CTHD[i]);
                        if (item.CTHD[i].Equals("1"))
                        {
                            strMH += (i+1) + ";";
                        }
                    }
                    //add data to map D
                    try
                    {
                        Program.mapInput_F1.Add(TID, strMH.Substring(0, strMH.Length - 1));
                    }catch(Exception ex)
                    {
                        //igonre
                    }
                    TID++;
                    listView1.Items.Add(itm);
                }
                Program.listF.Add(Program.mapInput_F1);
                listView2.Clear();
                listView2.View = View.Details;
                listView2.GridLines = true;
                listView2.FullRowSelect = true;


                listView2.Columns.Add("Mã Items");
                listView2.Columns.Add("Mã mặt hàng");

                ListViewItem itm1;

                for (int i = 0; i < st.lstMaSP.Count; i++)
                {

                    itm1 = new ListViewItem((i+1).ToString());
                    itm1.SubItems.Add(st.lstMaSP[i]);
                    listView2.Items.Add(itm1);
                    Program.mapMaHoa.Add(i+1,st.lstMaSP[i]);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(st);
            form2.Visible = true;

        }
    }
}
