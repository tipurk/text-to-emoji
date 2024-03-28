using System.Text.Json;
using System;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Collections;
using System.Net;
using System.Text;

namespace text_to_emoji_v1._1
{
    public partial class Form1 : Form
    {
        private bool isWindowOpen = true;

        Dictionary<string, char> alphabet = new Dictionary<string, char>();
        
        public string text = " ";
        public Form1()
        {
            InitializeComponent();
            ShowInTaskbar = true;
            notifyIcon1.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            notifyIcon1.ContextMenuStrip.Items.Add("Конвертировать из буфера", null, convert);
            notifyIcon1.ContextMenuStrip.Items.Add("Выход", null, close);
            
            
            JObject parsedJson = JObject.Parse(File.ReadAllText("letters.json"));
            int i = 0;
            foreach (JProperty property in parsedJson.Properties())
            {
                alphabet.Add(property.Name, (char)property.Value);
                i++;
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(text);
            text = " ";
        }


        public void close(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        public void convert(object sender, EventArgs e)
        {
            string a = Clipboard.GetText();
            TextConvert(a);
            Clipboard.SetText(text);
            text = " ";
        }
        public void TextConvert(string a)
        {
            foreach (char c in a)
            {
                if (c == ' ')
                {
                    text += c;
                }
                else
                {
                    text += alphabet.FirstOrDefault(x => x.Value == c).Key;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string a = textBox1.Text.ToLower();
            text = "";
            TextConvert(a);
            label1.Text = text;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                notifyIcon1.ContextMenuStrip.Show();
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (!isWindowOpen)
                {
                    this.WindowState = FormWindowState.Normal;
                    Show();
                    isWindowOpen = true;
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                    Close();
                    isWindowOpen = false;
                }
            }
        }
    }
}