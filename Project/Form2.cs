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
    public partial class Form2 : Form
    {
        public string[] arraymoduliukodas;
        public string[] arraymoduliupavadinimas;
        public string[] arrayvartotojoduomenys;
        public int kiekis;
        public int Moduliovardui;
        public string VartotojoKodas;
        public string VartotojoVardas;
        public string VartotojoPavarde;
        public int kurioreikia;
        public Form2(string[] array,string[] array2,string[] array3,int k)
        { 
            InitializeComponent();
            kiekis = k;
            arraymoduliupavadinimas = array2;
            arraymoduliukodas = array;
            arrayvartotojoduomenys = array3;
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = arrayvartotojoduomenys[0].Split(delimiterChars);
            VartotojoKodas = words[0];
            VartotojoPavarde = words[1];
            VartotojoVardas = words[2];
            for (int i = 0; i < kiekis; i++)
            {
                comboBox1.Items.Add(arraymoduliukodas[i]);
            }
            comboBox1.SelectedIndex = 0;
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var webAddr = "http://studentsktudo.96.lt/";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string kodas = comboBox1.SelectedItem.ToString();
                string json = "{ \"module_code\":"+"\"" +kodas+"\""+" }";
                streamWriter.Write(json);
                streamWriter.Flush();
            }
            richTextBox1.Text = "";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                string stringmessage;
                string stringvardas = "a";
                string info = result.ToString();
                while(stringvardas != "")
                {
                    stringvardas = getBetween(ref info, "user_name\":\"", "\",\"message_id");
                    stringmessage = getBetween(ref info, "message_content\":\"", "\",\"message_attach");
                    if (stringvardas != "")
                    {
                        richTextBox1.Text += stringvardas + "  :  " + stringmessage + "\n";
                        richTextBox1.Text += "\n";
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < kiekis; i++)
            {
                if (comboBox1.Text == arraymoduliukodas[i])
                {
                    Moduliovardui = i;
                }
            }
            label1.Text = arraymoduliupavadinimas[Moduliovardui];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < kiekis; i++)
            { 
            if(comboBox1.Text == arraymoduliukodas[i])
            {
                kurioreikia = i;
            }
            }
            Form3 thirdforma = new Form3(comboBox1.Text,VartotojoKodas,VartotojoVardas,VartotojoPavarde);
            thirdforma.Text = arraymoduliupavadinimas[kurioreikia];
            thirdforma.Show();
        }
        public static string getBetween(ref string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                string a = strSource.Substring(Start, End - Start);
                strSource = strSource.Substring(End);
                return a;
            }
            else
            {
                return "";
            }
        }
    }
}
