using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieuThi.Controller
{
    class Process
    {
        public static string splitStr(string s, bool returnLastChar)
        {
            string[] str = s.Split(',');
            if (returnLastChar)
            {
                return str[str.Length - 1];
            }
            else return s.Substring(0, s.LastIndexOf("," + str[str.Length - 1]));
        }

        public static List<string> apriori_gen(List<string> L)
        {
            List<string> listC = new List<string>();
            for (int i = 0; i < L.Count - 1; i++)
            {
                for (int j = i + 1; j < L.Count; j++)
                {
                    if (Program.flag == 1)
                    {
                        string key = L[i] + "," + L[j];
                        listC.Add(key);
                        //Program.listC.Add(key);
                    }
                    else if (splitStr(L[i], false).Equals(splitStr(L[j], false)))
                    {
                        string key = L[i] + "," + splitStr(L[j], true);
                        if (!Program.listC.Contains(key))
                        {
                            listC.Add(key);
                        }
                        //Program.listC.Add(key);
                    }
                }
            }
            return listC;
        }
        public static List<string> generate_L_From_F(Dictionary<int, string> F, bool isAddList)
        {
            //calculate minsup
            //nen dem ra ngoai
            float count = Program.mapMaHoa.Count;

            Dictionary<string, int> mapL = new Dictionary<string, int>();
            
            List<string> l = new List<string>();
            foreach (KeyValuePair<int, string> item in F)
            {
                string[] str = item.Value.Split(';');
                for (int i = 0; i < str.Length; i++)
                {
                    //key = obj
                    //value = count of obj
                    string key = str[i];
                    int value = 0;
                    if (mapL.ContainsKey(key))
                    {
                        //xoa cu, them lai voi value = count + 1
                        value = mapL[key] + 1;
                        mapL.Remove(key);
                        mapL.Add(key, value);
                    }
                    else
                    {
                        mapL.Add(key, 1);
                    }
                    float supCount = (value / count) * 100;
                    if (supCount >= Program.minSup && !l.Contains(key))
                    {
                        l.Add(key);
                    }
                }
            }
            l.Sort();
            if (isAddList)
            {
                Program.list_map_L.Add(mapL);
            }
            return l;
        }
        public static List<string> generateListFromString(string str)
        {
            List<string> l = new List<string>();
            string[] s = str.Split(';');
            for (int i = 0;i < s.Length; i++)
            {
                l.Add(s[i]);
            }
            return l;
        }
        public static Dictionary<int,string> generateNewF(Dictionary<int,string> F, List<string> L)
        {
            Dictionary<int, string> newF = new Dictionary<int, string>();
            //Dictionary<string, int> count = new Dictionary<string, int>();
            List<string> listC = apriori_gen(L);

            foreach(KeyValuePair<int,string> item in F)
            {
                string temp = "";
                List<string> listItemTID = apriori_gen(generateListFromString(item.Value));
                foreach(var itemTID in listItemTID)
                {
                    if (listC.Contains(itemTID))
                    {
                        temp += itemTID+";";
                    }
                }
                try{
                    newF.Add(item.Key, temp.Substring(0, temp.Length - 1));
                }
                catch (Exception e)
                {
                    //newF.Add(item.Key, "");
                    //igonre
                }
            }
            return newF;
        }
        public static void mainProcess()
        {
            Program.mapInput_F1.Add(100, "1;3");
            Program.mapInput_F1.Add(200, "2;3;5");
            Program.mapInput_F1.Add(300, "1;2;3;5");
            Program.mapInput_F1.Add(400, "2;5");

            Program.minSup = 50;

            //generate L from F (L1 from F1)
 //           Program.listL = generate_L_From_F(Program.mapInput_F1,);

            Program.map_F1 = generateNewF(Program.mapInput_F1,Program.listL);
            Program.flag = 2;
   //         Program.listL = generate_L_From_F(Program.map_F1);

            Program.map_F1 = generateNewF(Program.map_F1, Program.listL);
        }
    }
}
