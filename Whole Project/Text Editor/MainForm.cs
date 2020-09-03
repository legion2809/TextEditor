using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Text_Editor
{
    public partial class MainForm : Form
    {
        FileOper fileoper;
        EditText edit;
        Timer timer;

        public MainForm()
        {
            InitializeComponent();

            fileoper = new FileOper();
            edit = new EditText();
            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 500;

            fileoper.InitNewFile();
            this.Text = $"Text Editor - {fileoper.Filename}";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            edit.AddUndoRedo(richTextBox1.Text);
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileoper.isFileSaved)
            {
                richTextBox1.Text = "";
                fileoper.InitNewFile();
                UpdateState();
            } else
            {
                DialogResult res = MessageBox.Show("Do you wish save changes to " + fileoper.Filename + "?", "Text Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (res)
                {
                    case DialogResult.Yes:
                        if (fileoper.Filename.Contains("Unnamed"))
                        {
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                fileoper.SaveFile(sfd.FileName, richTextBox1.Lines);
                                UpdateState();
                            } else
                            {
                                fileoper.SaveFile(fileoper.FilePath, richTextBox1.Lines);
                                UpdateState();
                            }
                        }
                        break;
                    case DialogResult.No:
                        richTextBox1.Text = "";
                        fileoper.InitNewFile();
                        UpdateState();
                        break;
                }
            }
        }

        private void UpdateState()
        {
            this.Text = !fileoper.isFileSaved ? "Text Editor - " + fileoper.Filename + "*" : "Text Editor - " + fileoper.Filename;
            undoCtrlZToolStripMenuItem.Enabled = edit.CanUndo() ? true : false;
            redoCtrlYToolStripMenuItem.Enabled = edit.CanRedo() ? true : false;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            fileoper.isFileSaved = false;
            if (edit.TextAreaChangeRequired)
            {
                timer.Start();
            }
            else
            {
                edit.TextAreaChangeRequired = false;
            }
            UpdateState();
            toolStripStatusLabel1.Text = "Text is changed";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            ofd.Title = "Open File";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.TextChanged -= richTextBox1_TextChanged;
                richTextBox1.Text = fileoper.OpenFile(ofd.FileName);
                toolStripStatusLabel1.Text = "File opened -> " + ofd.FileName;
                richTextBox1.TextChanged += richTextBox1_TextChanged;
                UpdateState();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!fileoper.isFileSaved)
            {
                if (!this.Text.Contains("Unnamed.txt"))
                {
                    fileoper.SaveFile(fileoper.FilePath, richTextBox1.Lines);
                    toolStripStatusLabel1.Text = "File saved";
                    UpdateState();
                }
                else
                {
                    SaveFile();
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            sfd.Title = "Save As";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                fileoper.SaveFile(sfd.FileName, richTextBox1.Lines);
                toolStripStatusLabel1.Text = "File saved, its location: -> " + sfd.FileName;
                UpdateState();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cutCtrlXToolStripMenuItem.Enabled = richTextBox1.SelectedText.Length > 0 ? true : false;
            copyCtrlCToolStripMenuItem.Enabled = richTextBox1.SelectedText.Length > 0 ? true : false;
            pasteCtrlVToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Text);
        }

        private void editToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            editToolStripMenuItem_Click(sender, e);
        }

        private void cutCtrlXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
            pasteCtrlVToolStripMenuItem.Enabled = true;
        }

        private void copyCtrlCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
            pasteCtrlVToolStripMenuItem.Enabled = true;
        }

        private void pasteCtrlVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                richTextBox1.Paste();
            }
        }

        private void undoCtrlZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = edit.UndoIsClicked();
            UpdateState();
        }

        private void redoCtrlYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = edit.RedoIsClicked();
            UpdateState();
        }

        private void selectAllCtrlAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void deleteSelectedTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.SelectionStart, richTextBox1.SelectionLength);
        }

        private void aboutTextEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Text editor that powered by lazy piece of shit: Legion. Year 2020", "About author", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
