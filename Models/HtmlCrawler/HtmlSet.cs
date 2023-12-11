using HtmlCrawl.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlCrawl.HtmlCrawler
{
    public class HtmlSet : IHtmlOperation
    {
        private readonly HtmlSearch htmlSearch;

        public HtmlSet(HtmlSearch htmlSearch)
        {
            this.htmlSearch = htmlSearch;
        }

        public void PerformOperation(string relativePath, string newContent)
        {
            var nodesToSet = htmlSearch.Find(relativePath);

            foreach (var node in nodesToSet)
            {
                node.Content = newContent;
            }
        }
    }
}
