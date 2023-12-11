using CrawlerHTML;
using CrawlerHTML.CustomDataStructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HtmlCrawl.HtmlCrawler
{
    public class HtmlSearch
    {
        public HtmlTreeNode Root { get; }

        public HtmlSearch(HtmlTreeNode root)
        {
            Root = root;
        }

        public CustomList<HtmlTreeNode> Find(string path)
        {
            Console.WriteLine($"Debug: Searching for path: {path}");
            var pathParts = path.Split('/').CustomWhere(s => !string.IsNullOrEmpty(s)).ToArray();

            bool startFromRoot = path.CustomStartsWith("//");
            int startIndex = startFromRoot ? 0 : 1;

            if (startFromRoot)
            {
                pathParts = pathParts.Skip(1).ToArray();
                return Find(Root, pathParts, startIndex);
            }
            else
            {
                return Find(null!, pathParts, startIndex);
            }
        }

        private CustomList<HtmlTreeNode> Find(HtmlTreeNode node, string[] path, int index)
        {
            Console.WriteLine($"Debug: Current Node - Tag {node?.Tag}, Content: {node?.Content}, Index: {index}");

            if (node == null)
            {
                Console.WriteLine("Debug: Node is null. Returning an empty list.");
                return new CustomList<HtmlTreeNode>();
            }

            if (index == path.Length)
            {
                Console.WriteLine("Debug: Reached the end of the path");
                return new CustomList<HtmlTreeNode> { node };
            }

            var currentPath = path[index];

            if (currentPath == "//")
            {
                Console.WriteLine("Debug: Searching all descendants");
                var result = new CustomList<HtmlTreeNode>
                {
                    node
                };

                foreach (var child in node.Children)
                {
                    result.AddRange(Find(child, path, index).ToArray());
                }

                return result;
            }
            else if (currentPath.CustomContains("[@"))
            {
                Console.WriteLine("Debug: Handling tag with attribute path");
                var parts = currentPath.Split('[', '@', '=', ']');
                if (parts.Length == 5)
                {
                    var tag = parts[0];
                    var attribute = parts[2];
                    var value = parts[3];

                    var matchingChildren = node.Children
                        .Where(child => child.Tag == tag && child.Attributes.ContainsKey(attribute) && child.Attributes[attribute] == value)
                        .ToList();

                    var result = new CustomList<HtmlTreeNode>();
                    foreach (var child in matchingChildren)
                    {
                        result.AddRange(Find(child, path, index + 1).ToArray());
                    }

                    return result;
                }
            }
            else if (currentPath == "div")
            {
                Console.WriteLine("Debug: Handling <div> tag");

                var matchingChildren = node.Children.Where(child => child.Tag == "div").ToArray();
                var result = new CustomList<HtmlTreeNode>();
                foreach (var child in matchingChildren)
                {
                    result.AddRange(Find(child, path, index + 1).ToArray());
                }

                return result;
            }
            else if (currentPath.CustomContains("["))
            {
                Console.WriteLine("Debug: Handling indexed path");
                var parts = currentPath.Split('[', ']');
                if (parts.Length == 3 && int.TryParse(parts[1], out var indexValue))
                {
                    var tag = parts[0];
                    CustomList<HtmlTreeNode> matchingChildren = new CustomList<HtmlTreeNode>(node.Children.CustomWhere(child => child.Tag == tag));
                    if (indexValue >= 1 && indexValue <= matchingChildren.Count)
                    {
                        var result = Find(matchingChildren[indexValue - 1], path, index + 1);
                        return result.Count != 0 ? result : Find(node, path, index + 1);
                    }
                }
            }
            else if (currentPath == "*")
            {
                Console.WriteLine("Debug: Handling wildcard (*)");
                var result = new CustomList<HtmlTreeNode>();
                foreach (var child in node.Children)
                {
                    result.AddRange(Find(child, path, index + 1).ToArray());
                }
                return result;
            }
            else
            {
                Console.WriteLine($"Debug: Handling regular tag path - Tag: {currentPath}");
                var matchingChildren = node.Children.Where(child => child.Tag == currentPath).ToList();
                var result = new CustomList<HtmlTreeNode>();
                foreach (var child in matchingChildren)
                {
                    result.AddRange(Find(child, path, index + 1).ToArray());
                }
                return result;
            }

            return new CustomList<HtmlTreeNode>();
        }
    }
}