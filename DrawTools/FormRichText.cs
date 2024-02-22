using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Windows.Documents;
using System.IO;


 


namespace DrawTools
{
    public partial class FormRichText : Form
    {
        private FormPropWord parent;

        public new FormPropWord Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }
            
        public void init()
        {
            this.tcbSize.SelectedIndexChanged += tcbSize_SelectedIndexChanged;
            this.tcbFont.SelectedIndexChanged += tcbFont_SelectedIndexChanged;
            this.tbUnderline.Click += tbUnderline_Click;
            this.tbBullet.Click += tbBullet_Click;
            this.tbItalic.Click += tbItalic_Click;
            this.tbBold.Click += tbBold_Click;
            this.tbImg.Click += tbImg_Click;

            InstalledFontCollection fonts = new InstalledFontCollection();
            foreach (FontFamily family in fonts.Families)
                tcbFont.Items.Add(family.Name);
            tcbSize.Items.Add("8");
            tcbSize.Items.Add("10");
            tcbSize.Items.Add("12");
            rtbText.SelectionFont = new Font("Arial", 10, rtbText.SelectionFont.Style);
            tcbSize.SelectedItem="10";
            tcbFont.SelectedItem="Arial";
            ShowDialog(Parent);
        }


        public FormRichText(FormPropWord p, byte[] rawData)
        {
            Parent = p;
            InitializeComponent();

            rtbText.Rtf = System.Text.Encoding.UTF8.GetString(rawData);

            /*MemoryStream ms = new MemoryStream(rawData);
            rtbText.LoadFile(ms, RichTextBoxStreamType.TextTextOleObjs);*/

        }


        private void bOK_Click(object sender, EventArgs e)
        {
            if (rtbText.Text == "") Parent.setRawDataInCurrentRow(null);
            else
            {
                byte[] rawData = System.Text.Encoding.UTF8.GetBytes(rtbText.Rtf);
                Parent.setRawDataInCurrentRow(rawData);
            }
            
            
            /*MemoryStream ms = new MemoryStream();
            rtbText.SaveFile(ms, RichTextBoxStreamType.TextTextOleObjs);
            byte[] rawData = ms.ToArray();
            Parent.setRawDataInCurrentRow(rawData);*/
            Close();
        }

        private string OpenTag(char cTag, ref int ActivedLevel, ref char[] aTag)
        {
            char[] aTagLoc = {' ', ' ', ' ', ' ', ' '};
            string sHtml="";
            for (int i=0; i < aTag.Length; i++) aTagLoc[i] = aTag[i];
            if (ActivedLevel < 5)
            {
                if (cTag == 'l')
                {
                    int j = ActivedLevel;
                    for (int i = ActivedLevel - 1; i >= 0; i--) sHtml += CloseTag(aTag[ActivedLevel-1], ref ActivedLevel, ref aTag);
                    aTag[ActivedLevel] = cTag;
                    sHtml += "<ul><li>";
                    ActivedLevel = 1;
                    for (int i = 0; i < j; i++) sHtml+=OpenTag(aTagLoc[i], ref ActivedLevel, ref aTag);
                }
                else
                {
                    aTag[ActivedLevel++] = cTag;
                    sHtml += "<" + cTag + ">";
                }

            }
            return sHtml;
        }

        private string CloseTag(char cTag, ref int ActivedLevel, ref char[] aTag)
        {
            char[] aTagLoc = { ' ', ' ', ' ', ' ', ' ' };
            string sHtml = "";
            for (int i = 0; i < aTag.Length; i++) aTagLoc[i] = aTag[i];
            int j = ActivedLevel;

            for (int i = ActivedLevel-1; i >= 0; i--)
            {
                if (cTag == aTag[i])
                {
                    if (cTag == 'l') sHtml += "</ul>"; else sHtml += "</" + aTag[i] + ">";
                    ActivedLevel--;
                    for (int k = i + 1; k < j; k++) sHtml += OpenTag(aTagLoc[k], ref ActivedLevel, ref aTag);
                    break;
                }
                else
                {
                    sHtml += "</" + aTag[i] + ">";
                    ActivedLevel--;
                }
            }
            return sHtml;
        }

        private string Format(string rtf)
        {
            int idx = 0;
            char[] aTab = { ' ', ' ', ' ', ' ', ' ' }; int iActivedLevel = 0;
            //string sHtml = "<p>";
            string sHtml = OpenTag('p', ref iActivedLevel, ref aTab);
            bool bRTFControl = false;
            bool bExistBullet = false;
            while (idx < rtf.Length)
            {
                char chNext = rtf[idx];
                if (chNext == '{' || chNext == '}' || chNext == '\\')
                {
                    // Escaped char
                    idx++;
                    bRTFControl = true;
                    continue;
                }
                if (bRTFControl)
                {
                    bRTFControl = false;
                    Regex r2 = new Regex(@"([\*'a-z\-]*)([0-9]*)([ }]*)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    Match m = r2.Match(rtf, idx);
                    string stCtrlWord = m.Groups[1].ToString();
                    string stCtrlParam = m.Groups[2].ToString();
                    string stCtrlEspace = m.Groups[3].ToString();

                    if (stCtrlWord == "fcharset")
                    {
                        //idx = rtf.IndexOf(";", idx + m.Length) - m.Length + 1;
                        //idx += m.Length;
                        idx = rtf.IndexOf(";", idx + m.Length) + 1;
                    }
                    else if (stCtrlWord == "fs")
                    {
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "fnil")
                    {
                        int idx1 = rtf.IndexOf(";", idx + m.Length) + 1;
                        int idx2 = rtf.IndexOf(@"\", idx + m.Length);
                        if (idx1 <= idx2) idx = idx1; else idx = idx2;
                    }
                    else if (stCtrlWord == "colortbl")
                    {
                        idx = rtf.IndexOf("}", idx + m.Length);
                    }
                    else if (stCtrlWord.Length > 0 && stCtrlWord[0] == '\'')
                    {
                        if (rtf.Substring(idx + 1, 2) != "B7") sHtml += (char)Convert.ToInt32(rtf.Substring(idx + 1, 2), 16);
                        idx += 3;
                    }
                    else if (stCtrlWord == "par")
                    {
                        //if (bExistBullet) { sHtml += "</ul>"; bExistBullet = false; }
                        if (bExistBullet) { sHtml += CloseTag('l', ref iActivedLevel, ref aTab); bExistBullet = false; }
                        //sHtml += "</p><p>";
                        sHtml += CloseTag('p', ref iActivedLevel, ref aTab) + OpenTag('p', ref iActivedLevel, ref aTab);
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "pntext")
                    {
                        bExistBullet = true;
                        //sHtml += "<ul><li>";
                        sHtml += OpenTag('l', ref iActivedLevel, ref aTab);

                        idx += m.Length;
                    }
                    else if (stCtrlWord == "b")
                    {
                        if (stCtrlParam.Length > 0 && stCtrlParam == "0")
                        {
                            //sHtml += "</b>";
                            sHtml += CloseTag('b', ref iActivedLevel, ref aTab);
                        }
                        //else sHtml += "<b>";
                        else sHtml += OpenTag('b', ref iActivedLevel, ref aTab);
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "i")
                    {
                        if (stCtrlParam.Length > 0 && stCtrlParam == "0")
                        {
                            //sHtml += "</i>";
                            sHtml += CloseTag('i', ref iActivedLevel, ref aTab);
                        }
                        //else sHtml += "<i>";
                        else sHtml += OpenTag('i', ref iActivedLevel, ref aTab);
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "ul")
                    {
                        //sHtml += "<u>";
                        sHtml += OpenTag('u', ref iActivedLevel, ref aTab);
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "ulnone")
                    {
                        //sHtml += "</u>";
                        sHtml += CloseTag('u', ref iActivedLevel, ref aTab);
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "li")
                    {
                        idx += m.Length;
                    }
                    else if (stCtrlWord == "rquote")
                    {
                        sHtml += "'";
                        idx += m.Length;
                    }
                    else
                    {
                        //string control non traite
                        idx += m.Length;
                    }
                    if (stCtrlEspace.Length > 1)
                        for (int i = 1; i < stCtrlEspace.Length; i++) sHtml += " ";
                }
                else
                {
                    if (chNext == '\r' || chNext == '\n')
                    {
                        idx++;
                        continue;
                    }
                    sHtml += rtf[idx++];
                }
            }
            sHtml += "</p";
            return sHtml;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            //Format(rtbText.Rtf);
            Close();
        }

        void tbBold_Click(object sender, System.EventArgs e)
        {
            if (rtbText.SelectionFont != null)
            {
                FontStyle style = rtbText.SelectionFont.Style;

                if (rtbText.SelectionFont.Bold)
                    style &= ~FontStyle.Bold;
                else
                    style |= FontStyle.Bold;

                rtbText.SelectionFont = new Font(rtbText.SelectionFont, style);
            }

            //throw new System.NotImplementedException();
        }

        void tbItalic_Click(object sender, System.EventArgs e)
        {
            if (rtbText.SelectionFont != null)
            {
                FontStyle style = rtbText.SelectionFont.Style;

                if (rtbText.SelectionFont.Italic)
                    style &= ~FontStyle.Italic;
                else
                    style |= FontStyle.Italic;

                rtbText.SelectionFont = new Font(rtbText.SelectionFont, style);
            }
            //throw new System.NotImplementedException();
        }

        void tbUnderline_Click(object sender, System.EventArgs e)
        {
            if (rtbText.SelectionFont != null)
            {
                FontStyle style = rtbText.SelectionFont.Style;

                if (rtbText.SelectionFont.Underline)
                    style &= ~FontStyle.Underline;
                else
                    style |= FontStyle.Underline;

                rtbText.SelectionFont = new Font(rtbText.SelectionFont, style);
            }
            //throw new System.NotImplementedException();
        }

        void tbBullet_Click(object sender, System.EventArgs e)
        {
            //rtbText.SelectionRightIndent = 12;

            if (rtbText.SelectionFont != null)
            {
                if (rtbText.SelectionBullet)
                {
                    rtbText.SelectionIndent = 0;
                    rtbText.SelectionHangingIndent = 0;
                    rtbText.SelectionBullet = false;
                }
                else
                {
                    rtbText.SelectionIndent = 24;
                    rtbText.BulletIndent = 24;
                    //rtbText.SelectionHangingIndent = 24;
                    rtbText.SelectionBullet = true;
                }
            }
            //throw new System.NotImplementedException();
        }

        void tcbFont_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (rtbText.SelectionFont == null)
                rtbText.SelectionFont = new Font(tcbFont.SelectedItem.ToString(), rtbText.Font.Size);
            else
                rtbText.SelectionFont = new Font(tcbFont.SelectedItem.ToString(), rtbText.SelectionFont.Size);
            //throw new System.NotImplementedException();
        }

        void tcbSize_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (rtbText.SelectionFont == null)    return;
            rtbText.SelectionFont = new Font(rtbText.SelectionFont.FontFamily, Convert.ToInt32(tcbSize.SelectedItem), rtbText.SelectionFont.Style);
            //throw new System.NotImplementedException();
        }

        void tbImg_Click(object sender, System.EventArgs e)
        {
            Image img = Image.FromFile("c:\\dat\\MC900431601.png");
            Clipboard.SetImage(img);

            rtbText.SelectionStart = 0;
            rtbText.Paste();
            Clipboard.Clear();

            //throw new System.NotImplementedException();
        }

        
        /*private void lstFonts_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (richTextBox1.SelectionFont == null)
            {
                richTextBox1.SelectionFont = new Font(e.ClickedItem.Text, richTextBox1.Font.Size);
            }
            richTextBox1.SelectionFont = new Font(e.ClickedItem.Text, richTextBox1.SelectionFont.Size);
        }


        private void lstFontSize_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (richTextBox1.SelectionFont == null)
            {
                return;
            }
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily,
            Convert.ToInt32(e.ClickedItem.Text),
            richTextBox1.SelectionFont.Style);
        }*/

    }
}
