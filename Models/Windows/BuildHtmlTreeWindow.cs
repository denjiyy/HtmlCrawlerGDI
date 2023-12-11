using HtmlCrawl;
using HtmlCrawl.HtmlCrawler;
using System;
using System.Windows.Forms;

namespace HTML.Models.Windows
{
    public partial class BuildHtmlTreeWindow : Form
    {
        private HtmlTreeNode root;

        public BuildHtmlTreeWindow(HtmlTreeNode root)
        {
            InitializeComponent();
            this.root = root;
        }

        private void BuildTreeButton_Click(object sender, EventArgs e)
        {
            string filePath = FilePathTextBox.Text;

            if (!string.IsNullOrEmpty(filePath))
            {
                string htmlContent = System.IO.File.ReadAllText(filePath);

                HtmlParser htmlParser = new HtmlParser();
                root = htmlParser.BuildTreeFromHtml(htmlContent);

                this.Hide();

                HtmlCrawlerWindow htmlCrawlerWindow = new HtmlCrawlerWindow(root);
                htmlCrawlerWindow.Show();
            }
        }

        private void QuitBuildTree_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }
    }
}
