using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carregaImagens
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form_DragEnter);
            this.DragDrop += new DragEventHandler(Form_DragDrop);
        }

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        void Form_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in fileList.Where(x => x.Length > 0))
            {
                var fileName = getFileName(file);
                if (!listBox1.Items.Contains(fileName))
                {
                    listBox1.Items.Add(fileName);
                }
            }

            if (listBox1.Items != null)
            {
                List<string> ImagePaths = new List<string>();

                foreach (string item in listBox1.Items)
                {
                    ImagePaths.Add(@"" + item + "");
                }

                StitchImages.mesclaImagens SIV = new StitchImages.mesclaImagens(ImagePaths);
                SIV.JoinImages("Vertically").Save(@"vertical01.png");
            }
            MessageBox.Show("Imagem gerada com sucesso!" + "\n" +
                Directory.GetCurrentDirectory());
            Dispose();
        }

        public string getFileName(string path)
        {
            return Path.GetFullPath(path);
        }

    }
}
