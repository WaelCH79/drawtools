using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class FormMsgAndLinkOk : Form
    {
        private Form1 parent;

        public new Form1 Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public FormMsgAndLinkOk(Form1 p, string sMsg, string sLink)
        {
            Parent = p;
            InitializeComponent();
            lPath.Text = sMsg;
            llPath.Text = sLink;
        }

        public void init()
        {
            this.bOK.Click += BOK_Click;
            this.llPath.LinkClicked += LlPath_LinkClicked;
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Copier le lien", new EventHandler(CopyLink_Click));
            this.ContextMenu = cm;
            ShowDialog(Parent);
        }

        private void CopyLink_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(llPath.Text);
        }


        private void LlPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llPath.LinkVisited = true;
            System.Diagnostics.Process.Start(llPath.Text);
            //throw new NotImplementedException();
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            this.Close();
            //throw new NotImplementedException();
        }
    }
}
