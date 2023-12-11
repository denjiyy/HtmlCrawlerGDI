using CrawlerHTML.CustomDataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlCrawl
{
    public class HtmlTreeNode
    {
        public string Tag { get; }
        public CustomList<HtmlTreeNode> Children { get; }
        public string Content { get; set; }
        public CustomDictionary<string, string> Attributes { get; set; }

        //public HtmlTreeNode Parent { get; set; } vse taq

        public HtmlTreeNode(string tag, string content)
        {
            Tag = tag;
            Children = new CustomList<HtmlTreeNode>();
            Content = content;
            Attributes = new CustomDictionary<string, string>();
        }

        public void PrintTree(int depth = 0)
        {
            string indent = new string(' ', depth * 4);
            Console.WriteLine($"{indent}{Tag} - {Content}");

            foreach (var child in Children)
            {
                child.PrintTree(depth + 1);
            }
        }
    }
}
