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
                treeView1.Nodes.Clear();
                doc = new XmlDocument();
                doc.Load(openFileDialog1.FileName);
                PopulateList();
            }

            treeView1.SelectedNode = treeView1.Nodes[0];

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (doc == null)
                return;

            if (isPopulatingList)
                return;

#if false
            if (treeView1.SelectedNode.Parent == null)
            {
                valueTextBox.Text = xmlnode[0].SelectSingleNode(treeView1.SelectedNode.Text).InnerText;
            }
            else
            {
                //string path = ".\\" + treeView1.SelectedNode.FullPath;
                string path = treeView1.SelectedNode.Parent.Text + "/" + treeView1.SelectedNode.Text;
                XmlNodeList tmplst = xmlnode[0].SelectNodes(path);

                //This is ugly
                if (tmplst.Count > 1)
                    valueTextBox.Text = tmplst[treeView1.SelectedNode.Index].ChildNodes[0].InnerText;
                else
                    valueTextBox.Text = tmplst[0].ChildNodes[0].InnerText;
                
            }

#endif

        }

        private void PopulateList()
        {
            isPopulatingList = true;

            //Get Player stuff for now
            xmlnode = doc.GetElementsByTagName("player");

            for (int i = 0; i <= xmlnode[0].ChildNodes.Count - 1; i++)
            {
                TreeNode workingNode = treeView1.Nodes.Add(xmlnode[0].ChildNodes[i].Name);

                //If we have child Nodes, populate some more.
                if (xmlnode[0].ChildNodes[i].ChildNodes.Count > 1)
                {
                    treeView1.SelectedNode = workingNode;

                    XmlNodeList tmpNodes = xmlnode[0].ChildNodes[i].ChildNodes;

                    PopulateDeeper(workingNode, tmpNodes);

                }
            }
            isPopulatingList = false;
        }

        private void PopulateDeeper(TreeNode workTreeNode, XmlNodeList workNode)
        {
            //treeView1.SelectedNode = workTreeNode;

            for (int i=0; i < workNode.Count; i++)
            { 
                workTreeNode = treeView1.SelectedNode.Nodes.Add(workNode[i].Name);

                if (workNode[i].ChildNodes.Count > 1)
                {
                    XmlNodeList tmp = workNode[0].ChildNodes;
                    PopulateDeeper(workTreeNode, tmp);
                }

            }
        }

        public XmlNodeList xmlnode;
        public XmlDocument doc;
        private bool isPopulatingList;
    }
}
