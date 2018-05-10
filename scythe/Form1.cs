using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace scythe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Save Files|*.*";
            openFileDialog1.Title = "Select a Save File";


            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                isPopulatingList = true;
                treeView1.Nodes.Clear();
                doc = new XmlDocument();
                doc.Load(openFileDialog1.FileName);

                string str = null;

                xmlnode = doc.GetElementsByTagName("player");
                for (int i = 0; i <= xmlnode[0].ChildNodes.Count - 1; i++)
                {
                    //Get player name
                    if (xmlnode[0].ChildNodes.Item(i).Name == "name")
                    {
                        playerNameTextBox.Text = xmlnode[0].ChildNodes.Item(i).InnerText.Trim();
                    }

                    TreeNode workingNode = treeView1.Nodes.Add(xmlnode[0].ChildNodes.Item(i).Name);
                    treeView1.SelectedNode = workingNode;

                    if (xmlnode[0].ChildNodes[i].ChildNodes.Count > 1)
                    {
                        for (int o = 0; o < xmlnode[0].ChildNodes[i].ChildNodes.Count; o++)
                            workingNode = treeView1.SelectedNode.Nodes.Add(xmlnode[0].ChildNodes[i].ChildNodes.Item(o).Name);

                    }
                }

            }

            isPopulatingList = false;
            treeView1.SelectedNode = treeView1.Nodes[0];

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (doc == null)
                return;

            if (isPopulatingList)
                return;

            if (treeView1.SelectedNode.Parent == null)
            {
                for (int i = 0; i <= xmlnode[0].ChildNodes.Count - 1; i++)
                {
                    if (xmlnode[0].ChildNodes.Item(i).Name == treeView1.SelectedNode.Text)
                    {
                        valueTextBox.Text = xmlnode[0].ChildNodes.Item(i).InnerXml.Trim();
                        break;
                    }
                }
            }
            else
            {
                //string path = ".\\" + treeView1.SelectedNode.FullPath;
                string path = treeView1.SelectedNode.Parent.Text +"/"+ treeView1.SelectedNode.Text;
                valueTextBox.Text = xmlnode[0].SelectSingleNode(path).InnerText.Trim();
            }


        }

        public XmlNodeList xmlnode;
        public XmlDocument doc;
        private bool isPopulatingList;
    }
}
