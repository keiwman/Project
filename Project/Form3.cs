using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form3 : Form
    {
        string Moduliokodas;
        string VartotojoVard;
        string VartotojoKod;
        string VartotojoPav;
        public Form3(string labelname,string Vkodas,string VVardas,string VPavarde)
        {
            InitializeComponent();
            Moduliokodas = labelname;
            VartotojoVard = VVardas;
            VartotojoKod = Vkodas;
            VartotojoPav = VPavarde;
           // label1.Text = Moduliokodas;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var webAddr = "http://studentsktudo.96.lt/";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            using (var streamWriter2 = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{ \"user_id\":" + "\"" + VartotojoKod + "\"," + "\"user_name\":" + "\"" + VartotojoVard + " " + VartotojoPav + "\"," + "\"message_content\":" + "\"" + richTextBox1.Text + "\"," + "\"message_attach\":" + "\"" + "yolo" + "\"," + "\"module_code\":" + "\"" + Moduliokodas + "\"" + " }";
                streamWriter2.Write(json);
                streamWriter2.Flush();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader2 = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader2.ReadToEnd();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
