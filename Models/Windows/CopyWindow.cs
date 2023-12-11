using HtmlCrawl;
using HtmlCrawl.HtmlCrawler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTML
{
    public partial class CopyWindow : Form
    {
        private HtmlTreeNode root;
        private HtmlCopy copy;
        private HtmlSearch search;

        public CopyWindow(HtmlTreeNode root)
        {
            InitializeComponent();

            this.root = root;

            search = new HtmlSearch(root);
            copy = new HtmlCopy(search);
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            string sourcePath = RelativePath.Text;
            string targetPath = TargetPath.Text;

            copy.PerformOperation(sourcePath, targetPath);
            MessageBox.Show("Copied successfully");
        }

        private void BackLabel_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HtmlCrawlerWindow htmlCrawlerWindow = new HtmlCrawlerWindow(root);
            this.Hide();
            htmlCrawlerWindow.Show();
        }
    }
}
