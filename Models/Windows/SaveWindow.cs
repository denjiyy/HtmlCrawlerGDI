using HtmlCrawl;
using HtmlCrawl.HtmlCrawler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTML.Models.Windows
{
    public partial class SaveWindow : Form
    {
        private HtmlTreeNode root;
        private HtmlSaver saver;

        public SaveWindow(HtmlTreeNode root)
        {
            InitializeComponent();

            this.root = root;
            saver = new HtmlSaver(root);
        }

        private void BackLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
        }

        private void SaveDocument_Click(object sender, EventArgs e)
        {
            string filePath = RelativePathTextBox.Text;

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please enter a valid file path.");
                return;
            }

            try
            {
                saver.SaveHtmlAsTxt(filePath);

                MessageBox.Show("HTML file saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving HTML file: {ex.Message}");
            }

            this.Hide();
        }
    }
}
