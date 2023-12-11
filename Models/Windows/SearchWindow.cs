using CrawlerHTML.CustomDataStructures;
using HTML.Models.Windows;
using HtmlCrawl;
using HtmlCrawl.HtmlCrawler;
using System;
using System.CodeDom.Compiler;
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
    public partial class SearchWindow : Form
    {
        private HtmlTreeNode root;
        private HtmlSearch search;

        public SearchWindow(HtmlTreeNode root)
        {
            InitializeComponent();
            this.root = root;

            search = new HtmlSearch(root);
        }

        private void BackLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
        }

        private void SearchRelativePath_Click(object sender, EventArgs e)
        {
            string relativePath = RelativePathTextBox.Text;
            CustomList<HtmlTreeNode> searchResults = search.Find(relativePath);

            SearchResultWindow searchResultWindow = new SearchResultWindow(root, searchResults);
            this.Hide();
            searchResultWindow.Show();
        }
    }
}
