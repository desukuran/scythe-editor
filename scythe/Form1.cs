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
                XmlDocument doc = new XmlDocument();
                doc.Load(openFileDialog1.FileName);
                loadedSaveData = doc.InnerXml;

                using (XmlReader reader = XmlReader.Create(openFileDialog1.FileName))
                {
                    while (reader.Read())
                    {
                        // Only detect start elements.
                        if (reader.IsStartElement())
                        {
                            // Get element name and switch on it.
                            switch (reader.Name)
                            {
                                case "player":
                                    reader.ReadToDescendant("name");
                                    Text = reader.ReadElementContentAsString();
                                    break;

                                case "name":
                                    
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
