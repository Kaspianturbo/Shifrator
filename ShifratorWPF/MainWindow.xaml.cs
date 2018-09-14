using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShifratorWPF
{
    public interface IMainForm
    {
        string FilePath { get; }
        string Content { get; set; }
        string KeyPath { get; }
        void SetSymbolCount(int count);
        void GetFontSize();
        event EventHandler FileOpenClick;
        event EventHandler FileSaveClick;
        event EventHandler ContentChanged;
        event EventHandler FontChanged;
        event EventHandler DecipherClick;
        event EventHandler EncrypterClick;
    }
    public partial class MainWindow : Window, IMainForm
    {
        public MainWindow()
        {
            InitializeComponent();
            MessageService service = new MessageService();
            FileManager manager = new FileManager();
            Coder coder = new Coder(); 
            MainPresenter presenter = new MainPresenter(this, manager, service, coder);
            
            butOpenFile.Click += ButOpenFile_Click;
            butSave.Click += ButSave_Click;
            tbContent.TextChanged += TbContent_TextChanged;
            tbFontSize.LostFocus += TbFontSize_TextChanged;
            butSelect.Click += ButSelectFile_Click;
            butDecipher.Click += ButDecipher_Click;
            butEncrypter.Click += ButEncrypter_Click;
        }

        private void TbFontSize_TextChanged(object sender, RoutedEventArgs e)
        {
            if (FontChanged != null) FontChanged(this, EventArgs.Empty);
        }

        private void ButEncrypter_Click(object sender, RoutedEventArgs e)
        {
            if (EncrypterClick != null) EncrypterClick(this, EventArgs.Empty);
        }

        private void ButDecipher_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Ключ|*.kft|Всі файли|*.*";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                KeyPath = dlg.FileName;

                if (DecipherClick != null) DecipherClick(this, EventArgs.Empty);
            } 
        }

        private void ButOpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
        }

        private void ButSave_Click(object sender, RoutedEventArgs e)
        {
            if (FileSaveClick != null) FileSaveClick(this, EventArgs.Empty);
        }

        private void TbContent_TextChanged(object sender, RoutedEventArgs e)
        {
            if (ContentChanged != null) ContentChanged(this, EventArgs.Empty);
        }

        public string FilePath
        {
            get { return tbFilePath.Text; }
        }

        public string KeyPath { get; private set; }
       
        public string Content
        {
            get { return tbContent.Text; }
            set { tbContent.Text = value; }
        }

        public void SetSymbolCount(int count)
        {
            tbSymbolCount.Text = count.ToString();
        }

        public void GetFontSize()
        {
            tbContent.FontSize = Convert.ToInt32(tbFontSize.Text);
        }

        public event EventHandler FileOpenClick;
        public event EventHandler FileSaveClick;
        public event EventHandler DecipherClick;
        public event EventHandler EncrypterClick;
        public event EventHandler ContentChanged;
        public event EventHandler FontChanged;

        private void ButSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Текстові файли|*.txt|Всі файли|*.*";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbFilePath.Text = dlg.FileName;

                if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
            }
        }
    }
}
