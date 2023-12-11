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

namespace HTML
{
    public partial class SetWindow : Form
    {
        private HtmlTreeNode root;
        private HtmlSearch search;
        private HtmlSet set;
        public SetWindow(HtmlTreeNode root)
        {
            InitializeComponent();

            this.root = root;

            search = new HtmlSearch(root);
            set = new HtmlSet(search);
        }

        private void RelativePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void BackLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HtmlCrawlerWindow htmlCrawlerWindow = new HtmlCrawlerWindow(root);
            this.Hide();
            htmlCrawlerWindow.Show();
        }

        private void SetRelativePath_Click(object sender, EventArgs e)
        {
            string relativePath = RelativePath.Text;
            string newContent = NewContent.Text;

            if (string.IsNullOrEmpty(newContent))
            {
                MessageBox.Show("Please enter a value for the new content.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            set.PerformOperation(relativePath, newContent);

            MessageBox.Show("Set operation completed.", "Operation Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
