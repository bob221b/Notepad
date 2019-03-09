using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace NotepadTM7
{
    public partial class Notepad : Form
    {
        SaveFileDialog sf = new SaveFileDialog();
        OpenFileDialog dlg = new OpenFileDialog();
        PrintDialog p = new PrintDialog();
        PrintDocument pd = new PrintDocument();
        PageSetupDialog psd=new PageSetupDialog();

        private void SaveFile()
        {

            sf.Title = "Save As";
            sf.Filter = "Text Document|*.txt";//applied filter       
            sf.DefaultExt = "txt";//applied default extension    
            if (sf.ShowDialog() == DialogResult.OK)
            {
                    richTextBox1.SaveFile(sf.FileName, RichTextBoxStreamType.PlainText);
                //   this.Text = sf.FileName;
                this.Text = "Untitled-Digital Diary";

            }
        }
        public Notepad()
        {
            InitializeComponent();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            sf.Title = "Save";
            DialogResult dr = MessageBox.Show("Do you want to save the file", "save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr.Equals(DialogResult.Yes))          
            {
                SaveFile();      
            }
            else if (dr.Equals(DialogResult.No))           
            {
                richTextBox1.Clear();
                this.Text = "Untitled-Digital Diary";
            }
            richTextBox1.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            dlg.Title = "Open";
            dlg.Filter = "Text Files|*.txt";
            dlg.ShowDialog();
            string filename = dlg.FileName;
            string readfiletext = File.ReadAllText(filename);
            this.Text = filename;
            richTextBox1.Text =readfiletext;
            //richTextBox1.LoadFile(dlg.FileName);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(File.Exists(dlg.FileName ))
            {
                StreamWriter save = new StreamWriter(dlg.FileName);
                save.WriteLine(richTextBox1.Text);
                save.Close();
                richTextBox1.Clear();
            }
            else
            {
                sf.Title = "Save";
                DialogResult dr = MessageBox.Show("Do you want to Close the file withowt saving", "save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr.Equals(DialogResult.Yes))
                {
                    richTextBox1.Clear();
                    // SaveFile();
                }
                else if (dr.Equals(DialogResult.No))
                {

                    SaveFile();
                }
                richTextBox1.Clear();
            }
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(dlg.FileName))
            {
                StreamWriter save = new StreamWriter(dlg.FileName);
                save.WriteLine(richTextBox1.Text);
                save.Close();
                richTextBox1.Clear();
            }
            else
            {
                sf.Title = "Save";
                DialogResult dr = MessageBox.Show("Do you want to Close the file withowt saving", "save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr.Equals(DialogResult.Yes))
                {
                    richTextBox1.Clear();
                    // SaveFile();
                }
                else if (dr.Equals(DialogResult.No))
                {

                    SaveFile();
                }
                richTextBox1.Clear();
                System.Environment.Exit(0);
            }

          
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(dlg.FileName))
            {
                StreamWriter save = new StreamWriter(dlg.FileName);
                save.WriteLine(richTextBox1.Text);
               // save.Close();
               // richTextBox1.Clear();
            }
            else
            {
                sf.Title = "Save";
                DialogResult dr = MessageBox.Show("Do you want Save The File?", "save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr.Equals(DialogResult.Yes))
                {
         //           richTextBox1.Clear();
                     SaveFile();
                }
                else if (dr.Equals(DialogResult.No))
                {
                    System.Environment.Exit(0);
                }
                //richTextBox1.Clear();
            }
            //StreamWriter save = new StreamWriter(dlg.FileName);
            //save.WriteLine(richTextBox1.Text );
            //save.Close();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.Document = pd;
            if (p.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = richTextBox1.SelectionFont;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fd.Font;
               
            }
        }

        private void colourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog  cd = new ColorDialog ();
            
            cd.Color = richTextBox1.SelectionColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
               
                richTextBox1.SelectionColor = cd.Color;
            }
        }

        private void addImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
           dlg.Title = "Save As";
            dlg.Filter = "All files (*.*)|*.*|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";//applied filter    
            dlg.Multiselect = true;   
            dlg.DefaultExt = "jpg";//applied default extension    
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (var filenm in dlg.FileNames )
                {
                    Image img = Image.FromFile(filenm);
                    Clipboard.SetImage(img);
                    richTextBox1.Paste();
                }

            }
        }

       
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
            {
                richTextBox1.Undo();
            }

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (richTextBox1.CanRedo)
            {
                richTextBox1.Redo();
            }
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            psd.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            psd.PageSettings = new System.Drawing.Printing.PageSettings();
            psd.EnableMetric = false;
            psd.ShowDialog();
           
        }
    }
}
