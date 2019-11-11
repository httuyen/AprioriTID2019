using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SieuThi.Views;

namespace SieuThi
{
    static class Program
    {

        public static SqlConnection conn = new SqlConnection();

        public static String connstr;

        public static SqlDataReader myReader;

        public static String servername = "TUYEN-PC\\SERVER";


        public static String mlogin = "sa";

        public static String password = "123";

        public static String database = "SieuThi";

       

        public static SqlCommand cmd = new SqlCommand();

        public static Dictionary<int,string> mapInput_F1 = new Dictionary<int,string>();
        public static List<string> listC = new List<string>();
        public static List<string> listL = new List<string>();

        public static List<Dictionary<int, string>> listF = new List<Dictionary<int, string>>();
        public static Dictionary<string, int> map_L1 = new Dictionary<string, int>();
        public static Dictionary<int, string> map_F1 = new Dictionary<int, string>();
        public static Dictionary<int, string> data = new Dictionary<int, string>();
        public static Dictionary<int, string> mapMaHoa = new Dictionary<int, string>();
        public static List<Dictionary<string, int>> list_map_L = new List<Dictionary<string, int>>();

        public static int minSup = 0;
        public static int minConf = 0;
        public static int flag = 0;

        public static BindingSource bds_dspm = new BindingSource();

        public static int KetNoi()
        {
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                Program.conn.Close();
            try
            {
                Program.connstr = "Data Source=" + Program.servername + ";Initial Catalog=" +
                                  Program.database + ";User ID=" +
                                  Program.mlogin + ";password=" + Program.password;
                Program.conn.ConnectionString = Program.connstr;
                Program.conn.Open();
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message, "", MessageBoxButtons.OK);
                return 0;

            }
        }
        public static SqlDataReader ExecSqlDataReader(String strLenh)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strLenh, Program.conn);
            sqlcmd.CommandType = CommandType.Text;
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader();
                return myreader;

            }
            catch (SqlException ex)
            {
                Program.conn.Close();

                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public static DataTable ExecSqlDataTable(String cmd)
        {
            DataTable dt = new DataTable();
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
