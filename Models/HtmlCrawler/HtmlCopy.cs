using CrawlerHTML.CustomDataStructures;
using HtmlCrawl.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlCrawl.HtmlCrawler
{
    public class HtmlCopy : IHtmlOperation
    {
        private readonly HtmlSearch htmlSearch;

        public HtmlCopy(HtmlSearch htmlSearch)
        {
            this.htmlSearch = htmlSearch;
        }

        public void PerformOperation(string sourcePath, string destinationPath)
        {
            var sourceNodes = htmlSearch.Find(sourcePath);
            var destinationNodes = htmlSearch.Find(destinationPath);

            foreach (var destinationNode in destinationNodes)
            {
                CopyContent(sourceNodes, destinationNode);
            }
        }

        private void CopyContent(CustomList<HtmlTreeNode> sourceNodes, HtmlTreeNode destinationNode)
        {
            foreach (var sourceNode in sourceNodes)
            {
                CopyNodeContent(sourceNode, destinationNode);
            }
        }

        private void CopyNodeContent(HtmlTreeNode sourceNode, HtmlTreeNode destinationNode)
        {
            destinationNode.Content = sourceNode.Content;

            //destinationNode.Attributes = sourceNode.Attributes;

            for (int i = 0; i < Math.Min(sourceNode.Children.Count, destinationNode.Children.Count); i++)
            {
                CopyNodeContent(sourceNode.Children[i], destinationNode.Children[i]);
            }
        }
    }
}