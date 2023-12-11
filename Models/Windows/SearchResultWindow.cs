using CrawlerHTML.CustomDataStructures;
using HtmlCrawl;
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
    public partial class SearchResultWindow : Form
    {
        private HtmlTreeNode root;
        public SearchResultWindow(HtmlTreeNode root, CustomList<HtmlTreeNode> searchResults)
        {
            InitializeComponent();

            this.root = root;

            DisplaySearchResults(searchResults);
        }

        private void DisplaySearchResults(CustomList<HtmlTreeNode> searchResults)
        {
            if (searchResults.Count != 0)
            {
                foreach (var node in searchResults)
                {
                    ResultsListBox.Items.Add(node.Content);
                }
            }
            else
            {
                ResultsListBox.Items.Add("No results found. Is the relative path entered correctly?");
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
