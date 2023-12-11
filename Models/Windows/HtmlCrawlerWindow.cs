using HTML.Models.Windows;
using HtmlCrawl;
using System.Diagnostics;
using System.Drawing;
using System;
using System.Windows.Forms;

namespace HTML
{
    public partial class HtmlCrawlerWindow : Form
    {
        private HtmlTreeNode root;
        private ListBox listBox;
        private PictureBox pictureBox;

        public HtmlCrawlerWindow(HtmlTreeNode root)
        {
            InitializeComponent();
            this.root = root;

            listBox = new ListBox();
            listBox.Dock = DockStyle.Top;

            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            this.Controls.Add(pictureBox);
            this.Controls.Add(listBox);

            LoadTreeElements();
            DisplayImages(root);
        }

        private void LoadTreeElements()
        {
            listBox.Items.Clear();

            DisplayTextContent(root);
        }

        private void DisplayTextContent(HtmlTreeNode node)
        {
            if (!string.IsNullOrEmpty(node.Content))
            {
                if (node.Tag == "a" && node.Attributes.ContainsKey("href"))
                {
                    LinkLabel linkLabel = new LinkLabel();
                    linkLabel.Text = node.Content;
                    linkLabel.Tag = node.Attributes["href"];
                    linkLabel.LinkClicked += LinkLabel_LinkClicked!;

                    linkLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

                    linkLabel.Dock = DockStyle.None;

                    linkLabel.Location = new Point(0, listBox.Height - linkLabel.Height);

                    listBox.Controls.Add(linkLabel);
                }
                else
                {
                    listBox.Items.Add(node.Content);
                }
            }

            foreach (var child in node.Children)
            {
                DisplayTextContent(child);
            }
        }

        private void DisplayImages(HtmlTreeNode node)
        {
            if (node.Tag == "img" && node.Attributes.ContainsKey("src"))
            {
                string imagePath = node.Attributes["src"];
                if (!string.IsNullOrEmpty(imagePath))
                {
                    try
                    {
                        if (File.Exists(imagePath))
                        {
                            Bitmap image = new Bitmap(imagePath);
                            pictureBox.Image = image;
                        }
                        else
                        {
                            MessageBox.Show($"Image file does not exist: {imagePath}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}");
                    }
                }
            }

            foreach (var child in node.Children)
            {
                DisplayImages(child);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow(root);
            searchWindow.Show();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveWindow saveWindow = new SaveWindow(root);
            saveWindow.Show();
        }

        private void SetButton_Click(object sender, EventArgs e)
        {
            SetWindow setWindow = new SetWindow(root);
            this.Hide();
            setWindow.Show();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            CopyWindow copyWindow = new CopyWindow(root);
            this.Hide();
            copyWindow.Show();
        }

        private void QuitLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = (LinkLabel)sender;
            string url = (string)linkLabel.Tag!;

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo()
                {
                    FileName = url,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening link: {ex.Message}");
                throw;
            }
        }
    }
}
