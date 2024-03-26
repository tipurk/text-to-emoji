namespace text_to_emoji_v1._1
{
    public partial class Form1 : Form
    {
        string[] emojiMap = {
        ":a_:", ":b_:", ":v_:", ":g_:", ":d_:", ":e_:", ":zh:", ":z_:", ":i_:", ":ii:", ":k_:", ":l_:", ":m_:", ":n_:", ":o_:", ":p_:", ":r_:", ":s_:", ":t_:", ":u_:", ":f_:", ":h_:", ":ts:", ":ch:", ":sh:", ":shh:",
        ":ewqeqw:", ":iiii:", ":dsa:", ":e_~1:", ":yu:",":ya:",
    };
        char[] letMap = {
        'а','б','в','г','д', 'е','ж','з','и','й','к','л','м', 'н','о','п','р','с','т','у', 'ф','х','ц','ч','ш','щ','ъ','ы','ь','э', 'ю','я',
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
                if (index != -1)
                {
                    text += emojiMap[index];
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
                this.WindowState = FormWindowState.Normal;
                Show();
            }
        }
    }
}