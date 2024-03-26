namespace text_to_emoji_v1._1
{
    public partial class Form1 : Form
    {
        private bool isWindowOpen = true;
        string[] emojiMap = {
        ":a_:", ":b_:", ":v_:", ":g_:", ":d_:", ":e_:", ":zh:", ":z_:", ":i_:", ":ii:", ":k_:", ":l_:", ":m_:", ":n_:", ":o_:", ":p_:", ":r_:", ":s_:", ":t_:", ":u_:", ":f_:", ":h_:", ":ts:", ":ch:", ":sh:", ":shh:",
        ":ewqeqw:", ":iiii:", ":dsa:", ":e_~1:", ":yu:",":ya:",":yo:",":1_:",":2_:",":3_:",":4_:",":5_:",":6_:",":7_:",":8_:",":9_:",":0_:",":ahuetb:",":vopros:",
        };
        char[] letMap = {
        'а','б','в','г','д', 'е','ж','з','и','й','к','л','м', 'н','о','п','р','с','т','у', 'ф','х','ц','ч','ш','щ','ъ','ы','ь','э', 'ю','я','ё','1','2','3','4','5','6','7','8','9','0','!','?',
        };
        char[] LetMap =
        {
        'А','Б','В','Г','Д', 'Е','Ж','З','И','Й','К','Л','М','Н','О','П','Р','С','Т','У', 'Ф','Х','Ц','Ч','Ш','Щ','Ъ','Ы','Ь','Э', 'Ю','Я', 'Ё'
        };
        public string text = " ";
        public Form1()
        {
            InitializeComponent();
            ShowInTaskbar = true;
            notifyIcon1.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            notifyIcon1.ContextMenuStrip.Items.Add("Конвертировать из буфера", null, convert);
            notifyIcon1.ContextMenuStrip.Items.Add("Выход", null, close);
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
            foreach (char b in a)
            {
                int index = Array.IndexOf(letMap, b);
                int Index = Array.IndexOf(LetMap, b);
                if (index != -1)
                {
                    text += emojiMap[index];
                }
                if (Index != -1)
                {
                    text += emojiMap[Index];
                }
                if (b == ' ')
                {
                    text += b;
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            string a = textBox1.Text;
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