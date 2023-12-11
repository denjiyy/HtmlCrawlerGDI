using System.IO;
using System.Text;
using HtmlCrawl.HtmlCrawler;

namespace HtmlCrawl
{
    public class HtmlSaver
    {
        private HtmlTreeNode treeNode;

        public HtmlSaver(HtmlTreeNode treeNode)
        {
            this.treeNode = treeNode;
        }

        public void SaveHtmlAsTxt(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            using (var binaryWriter = new BinaryWriter(fileStream, Encoding.UTF8))
            {
                SaveNode(treeNode, binaryWriter);
            }
        }

        private void SaveNode(HtmlTreeNode node, BinaryWriter binaryWriter)
        {
            binaryWriter.Write(node.Tag);
            binaryWriter.Write(node.Content ?? string.Empty);

            foreach (var child in node.Children)
            {
                SaveNode(child, binaryWriter);
            }
        }
    }
}
