using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using HtmlAgilityPack;

namespace Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Prisijunk";
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsolutePath != (sender as WebBrowser).Url.AbsolutePath)
                return;
            else if (e.Url.AbsolutePath == "/ktuis/stud.busenos")
            {
                string[] a = new string[30];
                int kiekis = 0;
                GetElementsByProperty(webBrowser1, "TD", ref a, ref kiekis,62);
                string[] a2 = new string[30];
                int kiekis2 = 0;
                GetElementsByProperty2(webBrowser1, "TD", ref a2, ref kiekis2,62);
                kiekis2 = kiekis2 - 3;
                string[] a3 = new string[30];
                string classname = "navbar-text pull-right";
                int kiekis3 = 0;
                GetElementsByProperty3(webBrowser1, "P",classname, ref a3, ref kiekis3);
                Form2 antraforma = new Form2(a,a2,a3,kiekis2);
                antraforma.Text = "Help others to learn";
                antraforma.Show();
                this.Hide();  
            }
        }
        private void GetElementsByProperty(WebBrowser wb, string tagname2, ref string[] array, ref int k,int kiekr)
        {
            var els = webBrowser1.Document.GetElementsByTagName(tagname2); // all elems with tag
            foreach (HtmlElement el in els)
            {
                int koreikiaint = kiekr;
                string kasyra = el.GetAttribute("clientWidth");
                int kasyrae = Int32.Parse(kasyra);
                    if (kasyrae == koreikiaint)
                    {
                        array[k] = el.InnerText;
                        k++;
                    }
            }
        }
        private void GetElementsByProperty2(WebBrowser wb, string tagname2, ref string[] array, ref int k, int kiekr)
        {
            var els = webBrowser1.Document.GetElementsByTagName(tagname2); // all elems with tag
            foreach (HtmlElement el in els)
            {
                int koreikiaint = kiekr;
                string kasyra = el.GetAttribute("clientWidth") ;
                int kasyrae = Int32.Parse(kasyra);
                if (kasyrae > koreikiaint)
                {
                    array[k] = el.InnerText;
                    k++;
                }
            }
        }
        private void GetElementsByProperty3(WebBrowser wb, string tagname2,string classname, ref string[] array, ref int k)
        {
            var els = webBrowser1.Document.GetElementsByTagName(tagname2); // all elems with tag
            foreach (HtmlElement el in els)
            {
               if(el.GetAttribute("className") == classname)
                {
                    array[k] = el.InnerText;
                    k++;
               }
               
            }
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            webBrowser1.AllowNavigation = true;
            webBrowser1.Navigate("https://uais.cr.ktu.lt/ktuis/stp_prisijungimas");
            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
        }
       
    }
}
