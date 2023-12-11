using CrawlerHTML.CustomDataStructures;
using System;
using System.Collections.Generic;

namespace HtmlCrawl.HtmlCrawler
{
    public class HtmlParser
    {
        public HtmlTreeNode BuildTreeFromHtml(string html)
        {
            HtmlTreeNode root = new HtmlTreeNode("root", null!);
            CustomStack<HtmlTreeNode> stack = new CustomStack<HtmlTreeNode>();
            stack.Push(root);

            ProcessHtml(html, stack);

            Console.WriteLine($"Debug: Number of nodes in the tree: {CountNodes(root)}");

            return root;
        }

        private void ProcessHtml(string html, CustomStack<HtmlTreeNode> stack)
        {
            int index = 0;
            while (index < html.Length)
            {
                if (html[index] == '<')
                {
                    bool isClosingTag = false;
                    if (index + 1 < html.Length && html[index + 1] == '/')
                    {
                        isClosingTag = true;
                        index++;
                    }

                    int tagEnd = html.IndexOf('>', index);
                    if (tagEnd != -1)
                    {
                        string tag = html.Substring(index + 1, tagEnd - index - 1);
                        index = tagEnd + 1;

                        if (!isClosingTag)
                        {
                            ProcessOpeningTag(tag, stack);
                        }
                        else
                        {
                            stack.Pop();
                        }
                    }
                    else
                    {
                        index++;
                    }
                }
                else
                {
                    int textEnd = html.IndexOf('<', index);
                    if (textEnd != -1)
                    {
                        string text = html.Substring(index, textEnd - index);
                        index = textEnd;
                        ProcessText(text, stack.Peek());
                    }
                    else
                    {
                        string text = html.Substring(index);
                        ProcessText(text, stack.Peek());
                        break;
                    }
                }
            }
        }

        private void ProcessOpeningTag(string tag, CustomStack<HtmlTreeNode> stack)
        {
            var newNode = CreateNodeFromTag(tag);
            stack.Peek().Children.Add(newNode);

            if (!tag.EndsWith("/"))
            {
                stack.Push(newNode);
            }
        }

        private void ProcessText(string text, HtmlTreeNode parent)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                if (parent.Content == null)
                {
                    parent.Content = text;
                }
                else
                {
                    parent.Content += text;
                }
            }
        }

        private HtmlTreeNode CreateNodeFromTag(string tag)
        {
            int spaceIndex = tag.IndexOf(' ');
            string tagName = (spaceIndex == -1) ? tag : tag.Substring(0, spaceIndex);

            var attributes = new CustomDictionary<string, string>();
            if (spaceIndex != -1)
            {
                string attrText = tag.Substring(spaceIndex + 1);
                ExtractAttributes(attrText, attributes);
            }

            return new HtmlTreeNode(tagName, null!) { Attributes = attributes };
        }

        private void ExtractAttributes(string attrText, CustomDictionary<string, string> attributes)
        {
            int index = 0;
            while (index < attrText.Length)
            {
                while (index < attrText.Length && char.IsWhiteSpace(attrText[index]))
                {
                    index++;
                }

                int equalsIndex = attrText.IndexOf('=', index);
                if (equalsIndex != -1)
                {
                    string attributeName = attrText.Substring(index, equalsIndex - index).Trim();
                    index = equalsIndex + 1;

                    if (index < attrText.Length)
                    {
                        char quote = attrText[index];
                        int valueEndIndex = attrText.IndexOf(quote, index + 1);
                        if (valueEndIndex != -1)
                        {
                            string attributeValue = attrText.Substring(index + 1, valueEndIndex - index - 1);
                            attributes[attributeName] = attributeValue;
                            index = valueEndIndex + 1;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private int CountNodes(HtmlTreeNode node)
        {
            int count = 1;

            foreach (var item in node.Children)
            {
                count += CountNodes(item);
            }

            return count;
        }
    }
}