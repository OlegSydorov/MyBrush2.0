using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CSWF_MyBrush2._0
{
    public partial class Form1 : Form
    {
        string shape; 
        Color lineColour;
        Color fillColour1;
        Color fillColour2;
        HatchStyle hStyle;
        LinearGradientMode lgMode;
        string path;
        string text;
        
        int lineWidth;
        List<Point> contour;
        bool drawFlag;
        bool newFlag;// to make sure that when new layer is created checkedListBox checking is done automatically and not through listing the layer list (non-existant)
        bool newTabFlag; //to make sure that when new tab is created checkedListBox procedure is different
        bool reverseFlag; //to make sure that when switching between tabs and checking the checkListBox boxes based on Layer properties (visibility field) no oncheckbox event is launched
        int triangleCount; // to make sure that three apexes are selected (not more or less)

        string style;


        float layerTransparency;
       // Bitmap resultingImage;

        int layerCount;

        int pageCount;

        TabControl tabControl1;
        //Dictionary<TabPage, Dictionary<int, Layer>> docs;
        //Dictionary<int, Layer> data;

        Dictionary<TabPage, List<Layer>> data;
        List<Layer> list;

       // Layer newLayer;
       
        public Form1()
        {
            InitializeComponent();
            shape="unavailable";
            lineColour = Color.Black;
            fillColour1 = Color.White;
            fillColour2 = Color.Gray;
            hStyle = HatchStyle.Cross;
            lgMode = LinearGradientMode.Horizontal;
            //fillStyle = "draw";
            lineWidth = 1;
            contour=new List<Point>();
            drawFlag=false;
            path = "unavailable";
            text = "";

            triangleCount = 0;

            style = "draw";
            layerTransparency = 1f;
            layerCount = 0;
            pageCount = 0;
            
            layerTransparency = 1f;
                       
            tabControl1 = new TabControl();

            tabControl1.Parent = panel2;
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.SelectedIndexChanged += OnSelectedTabChanged;

            TabPage page = new TabPage("New image");

            page.BackColor = Color.LightGray;       
           
            PictureBox pictureBox=new PictureBox();

            pictureBox.Parent = page;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.MouseDown += PB_MouseDown;
            pictureBox.MouseMove += PB_MouseMove;
            pictureBox.MouseUp += PB_MouseUp;
            pictureBox.MouseClick += PB_MouseClick;
            pictureBox.MouseDoubleClick += PB_MouseDoubleClick;

            tabControl1.TabPages.Add(page);

            //docs = new Dictionary<TabPage, Dictionary<int, Layer>>();
            //Dictionary<int, Layer> data = new Dictionary<int, Layer>();

            foreach (string hs in Enum.GetNames(typeof(HatchStyle)))
            {
                comboBox2.Items.Add(hs);
            }

            data = new Dictionary<TabPage, List<Layer>>();
            list = new List<Layer>();

            Layer layer = new Layer(pictureBox.Width, pictureBox.Height, 1f);
            
            layer.FocusFlag = true;
            layer.Name = layerCount;
            checkedListBox1.Items.Add("Layer " + layer.Name);
            newFlag = true;
            newTabFlag = false;
            reverseFlag = false;
            checkedListBox1.SetItemChecked(layerCount, true); 
            
            layerCount++;

            list.Add(layer);
            
            data.Add(page, list);

       
            //docs.Add(tabControl1.SelectedTab, data);
            pictureBox.Image = layer.Show(pictureBox.Width, pictureBox.Height);
            //tabControl1.SelectedTab.Controls[0].Refresh();
        }



        private void fILEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       
        private void button6_Click(object sender, EventArgs e)
            {
                shape = "freeDots";
            }
        private void button5_Click(object sender, EventArgs e)
        {
            shape = "line";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            shape = "rectangle";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            shape = "ellipse";
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            shape = "triangle";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            shape = "multiline";
        }
        private void button31_Click(object sender, EventArgs e)
        {
            if (style!="fill")
            {
                style = "fill";
                button31.BackgroundImage=new Bitmap(Image.FromFile("fillFill.png"));

            }
            else
            {
                style="draw";
                button31.BackgroundImage = new Bitmap(Image.FromFile("fill.png"));
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;


            colorDialog1.AllowFullOpen = true;


            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                lineColour = colorDialog1.Color;
                fillColour1 = colorDialog1.Color;
            }
        }









        private void WHITE_Click(object sender, EventArgs e)
        {
            lineColour = Color.White;
            fillColour1 = Color.White;
        }

        private void Grey_Click(object sender, EventArgs e)
        {
            lineColour = Color.Gray;
            fillColour1 = Color.Gray;
        }

        private void Black_Click(object sender, EventArgs e)
        {
            lineColour = Color.Black;
            fillColour1 = Color.Black;
        }

        private void Red_Click(object sender, EventArgs e)
        {
            lineColour = Color.PeachPuff;
            fillColour1 = Color.PeachPuff;
        }

        private void LightBrown_Click(object sender, EventArgs e)
        {
            lineColour = Color.Chocolate;
            fillColour1 = Color.Chocolate;
        }

        private void Brown_Click(object sender, EventArgs e)
        {
            lineColour = Color.Brown;
            fillColour1 = Color.Brown;
        }

        private void Yellow_Click(object sender, EventArgs e)
        {
            lineColour = Color.Yellow ;
            fillColour1 = Color.Yellow;
        }

        private void LightOrange_Click(object sender, EventArgs e)
        {
            lineColour = Color.Orange;
            fillColour1 = Color.Orange;
        }

        private void Orange_Click(object sender, EventArgs e)
        {
            lineColour = Color.Red;
            fillColour1 = Color.Red;
        }

        private void Pink_Click(object sender, EventArgs e)
        {
            lineColour = Color.DeepPink;
            fillColour1 = Color.DeepPink;
        }
        private void Fuchsia_Click(object sender, EventArgs e)
        {
            lineColour = Color.Fuchsia;
            fillColour1 = Color.Fuchsia;
        }
        private void Violet_Click(object sender, EventArgs e)
        {
            lineColour = Color.Purple;
            fillColour1 = Color.Purple;
        }

        private void LightGreen_Click(object sender, EventArgs e)
        {
            lineColour = Color.GreenYellow;
            fillColour1 = Color.GreenYellow;
        }

        private void Green_Click(object sender, EventArgs e)
        {
            lineColour = Color.LimeGreen;
            fillColour1 = Color.LimeGreen;
        }

        private void DarkGreen_Click(object sender, EventArgs e)
        {
            lineColour = Color.ForestGreen;
            fillColour1 = Color.ForestGreen;
        }

        private void Aqua_Click(object sender, EventArgs e)
        {
            lineColour = Color.Aqua;
            fillColour1 = Color.Aqua;
        }

        private void LightBlue_Click(object sender, EventArgs e)
        {
            lineColour = Color.DeepSkyBlue;
            fillColour1 = Color.DeepSkyBlue;
        }

        private void Blue_Click(object sender, EventArgs e)
        {
            lineColour = Color.Blue;
            fillColour1 = Color.Blue;
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void oPENToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog4.Title = "Opening picture";
            openFileDialog4.Filter = "Image Files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF | All files(*.*) | *.*";
            openFileDialog4.FilterIndex = 1;
            openFileDialog4.CheckFileExists = true;
            openFileDialog4.Multiselect = false;
            if (openFileDialog4.ShowDialog() == DialogResult.OK)
            {
                TabPage page = new TabPage();
                newTabFlag = true;
                if (tabControl1.TabPages.Count > 0)
                {
                    pageCount++;
                    page.Text = "New image " + pageCount;
                }
                else
                {
                    page.Text = "New image";
                }

                page.BackColor = Color.LightGray;

                PictureBox pictureBox = new PictureBox();
                pictureBox.Parent = page;
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.MouseDown += PB_MouseDown;
                pictureBox.MouseMove += PB_MouseMove;
                pictureBox.MouseUp += PB_MouseUp;
                pictureBox.MouseClick += PB_MouseClick;
                pictureBox.MouseDoubleClick += PB_MouseDoubleClick;



                tabControl1.TabPages.Add(page);
                checkedListBox1.Items.Clear();

                layerCount = 0;
                List<Layer> listNew = new List<Layer>();

                Layer layer = new Layer(pictureBox.Width, pictureBox.Height, 1f);

                layer.FocusFlag = true;
                layer.Name = layerCount;
                layer.Back.Style = "texture";
                layer.Back.Path = openFileDialog4.FileName;

                checkedListBox1.Items.Add("Layer " + layer.Name);
                newFlag = true;
                checkedListBox1.SetItemChecked(layerCount, true);

                layerCount++;

                listNew.Add(layer);

                data.Add(page, listNew);
               
                tabControl1.SelectedTab = page;

                ImageReload();
            }
        }



        private void sAVEASJPGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveJPG(tabControl1.SelectedIndex);

        }
        
        private void sAVEALLASJPGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var p in data)
            {
                tabControl1.SelectedTab = p.Key;
                SaveJPG(tabControl1.SelectedIndex);
            }
        }
        private void SaveJPG(int x)
        {
            saveFileDialog1.Title = "Saving image...";
            saveFileDialog1.Filter = "BMP Image Files| *.BMP" + "| JPG Image Files| *.JPG" + "| PNG Image Files| *.PNG" + " | All files(*.*) | *.* ";
            saveFileDialog1.FilterIndex = 4;
            saveFileDialog1.CheckFileExists = false;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PictureBox pb = new PictureBox();
                pb = (PictureBox)tabControl1.TabPages[x].Controls[0];
                Image image1 = pb.Image;
                image1.Save(saveFileDialog1.FileName);
            }
        }

       
        private void sAVEASXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveXML(tabControl1.SelectedTab);            
        }
        private void sAVEALLASXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var v in data)
            {
                SaveXML(v.Key);
            }
        }

        private void SaveXML(TabPage page)
        {
            saveFileDialog2.Title = "Saving image as .xml";
            saveFileDialog2.Filter = "XML file|*.xml" + "|All Files|*.*";
            saveFileDialog2.FilterIndex = 2;
            saveFileDialog2.CheckFileExists = false;

            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                XmlDocument doc1 = new XmlDocument();
                XmlDeclaration decl = doc1.CreateXmlDeclaration("1.0", null, null);
                doc1.AppendChild(decl);
                string xmlName = saveFileDialog2.FileName.Substring(saveFileDialog2.FileName.LastIndexOf('\\') + 1);
                //MessageBox.Show(xmlName);
                XmlElement elem1 = doc1.CreateElement("MyBrush2.0_Image_" + xmlName);//tabControl1.SelectedTab.Text);
                doc1.AppendChild(elem1);

                foreach (Layer l in data[page])
                {
                    XmlElement elem2 = doc1.CreateElement("Layer_" + l.Name.ToString());
                    XmlAttribute attr20 = doc1.CreateAttribute("Opacity");
                    attr20.InnerText = l.Opacity.ToString();
                    XmlAttribute attr19 = doc1.CreateAttribute("Grayscale");
                    attr19.InnerText = l.GrayScale.ToString();

                    XmlAttribute attr18 = doc1.CreateAttribute("Background_LineColour");
                    attr18.InnerText = l.Back.LineColour.Name;
                    XmlAttribute attr17 = doc1.CreateAttribute("Background_FillColour1");
                    attr17.InnerText = l.Back.FillColour1.Name;
                    XmlAttribute attr16 = doc1.CreateAttribute("Background_FillColour2");
                    attr16.InnerText = l.Back.FillColour2.Name;
                    XmlAttribute attr15 = doc1.CreateAttribute("Background_HatchStyle");
                    attr15.InnerText = l.Back.HatchStyle.ToString();
                    XmlAttribute attr14 = doc1.CreateAttribute("Background_LinearGradientMode");
                    attr14.InnerText = l.Back.LgMode.ToString();
                    XmlAttribute attr13 = doc1.CreateAttribute("Background_Path");
                    attr13.InnerText = l.Back.Path;
                    XmlAttribute attr12 = doc1.CreateAttribute("Background_Style");
                    attr12.InnerText = l.Back.Style;

                    if (l.Elements.Count > 0)
                    {
                        XmlElement elem3 = doc1.CreateElement("ElementList");
                        int n = 1;
                        foreach (Element elem in l.Elements)
                        {
                            XmlElement elem4 = doc1.CreateElement("Element_" + n);
                            XmlAttribute attr11 = doc1.CreateAttribute("Element_" + n + "_Style");
                            attr11.InnerText = elem.Style;
                            XmlAttribute attr10 = doc1.CreateAttribute("Element_" + n + "_Shape");
                            attr10.InnerText = elem.Shape;
                            XmlAttribute attr9 = doc1.CreateAttribute("Element_" + n + "_LineColour");
                            attr9.InnerText = elem.LineColour.Name;
                            XmlAttribute attr8 = doc1.CreateAttribute("Element_" + n + "_LineWidth");
                            attr8.InnerText = elem.LineWidth.ToString();
                            XmlAttribute attr7 = doc1.CreateAttribute("Element_" + n + "_FillColour1");
                            attr7.InnerText = elem.FillColour1.Name;
                            XmlAttribute attr6 = doc1.CreateAttribute("Element_" + n + "_FillColour2");
                            attr6.InnerText = elem.FillColour2.Name;
                            XmlAttribute attr5 = doc1.CreateAttribute("Element_" + n + "_HatchStyle");
                            attr5.InnerText = elem.HatchStyle.ToString();
                            XmlAttribute attr4 = doc1.CreateAttribute("Element_" + n + "_LinearGradientMode");
                            attr4.InnerText = elem.LgMode.ToString();
                            XmlAttribute attr3 = doc1.CreateAttribute("Element_" + n + "_Path");
                            attr3.InnerText = elem.Path;
                            XmlAttribute attr2 = doc1.CreateAttribute("Element_" + n + "_Text");
                            attr2.InnerText = elem.Text;

                            XmlAttribute attr1 = doc1.CreateAttribute("Points");
                            if (elem.PointList.Count > 0)
                            {
                                foreach (Point p in elem.PointList)
                                {
                                    XmlText text1 = doc1.CreateTextNode("<" + p.X + " " + p.Y + ">");
                                    attr1.AppendChild(text1);
                                }
                            }
                            else attr1.InnerText = "Unavailable";

                            //XmlAttribute attr1 = doc1.CreateAttribute("Element_finished");
                            //attr1.InnerText = "Element_finished";


                            elem4.Attributes.Append(attr1);
                            elem4.Attributes.Append(attr2);
                            elem4.Attributes.Append(attr3);
                            elem4.Attributes.Append(attr4);
                            elem4.Attributes.Append(attr5);
                            elem4.Attributes.Append(attr6);
                            elem4.Attributes.Append(attr7);
                            elem4.Attributes.Append(attr8);
                            elem4.Attributes.Append(attr9);
                            elem4.Attributes.Append(attr10);
                            elem4.Attributes.Append(attr11);

                            n++;

                            elem3.AppendChild(elem4);
                        }

                        elem2.AppendChild(elem3);
                    }

                    elem2.Attributes.Append(attr12);
                    elem2.Attributes.Append(attr13);
                    elem2.Attributes.Append(attr14);
                    elem2.Attributes.Append(attr15);
                    elem2.Attributes.Append(attr16);
                    elem2.Attributes.Append(attr17);
                    elem2.Attributes.Append(attr18);
                    elem2.Attributes.Append(attr19);
                    elem2.Attributes.Append(attr20);

                    elem1.AppendChild(elem2);
                }

                doc1.Save(saveFileDialog2.FileName);


            }
        }

        private void PB_MouseDown(object sender, MouseEventArgs e)
        {
            if (shape != "triangle" && shape != "multiline" && shape!="path")
            {
                contour.Add(new Point(e.X, e.Y));
                drawFlag = true;
            }
        }

        private void PB_MouseMove(object sender, MouseEventArgs e)
        {
            if (shape != "triangle" && shape != "multiline" && shape != "path")
            {
                if (drawFlag == true)
                {                    
                    switch (shape)
                    {
                        case "freeDots":
                            {
                                contour.Add(new Point(e.X, e.Y));
                                  Draw();
                                //ImageReload();
                                //ImageReload();

                                break;
                            }
                        case "line":
                            {
                                break;
                            }
                        case "rectangle":
                            {
                                break;
                            }
                        case "ellipse":
                            {
                                break;
                            }
                    }
                }
            }
        }

        private void PB_MouseUp(object sender, MouseEventArgs e)
        {
            if (shape != "triangle" && shape != "multiline" && shape != "path")
            {
                drawFlag = false;
                switch (shape)
                {
                    case "freeDots":
                        {
                            contour.Add(new Point(e.X, e.Y));
                           // ImageReload();
                          //  Draw();
                            break;
                        }
                    case "line":
                        {
                            contour.Add(new Point(e.X, e.Y));
                          //  Draw();
                            break;
                        }
                    case "rectangle":
                        {
                            contour.Add(new Point(e.X, e.Y));
                          //  Draw();
                            break;
                        }
                    case "ellipse":
                        {
                            contour.Add(new Point(e.X, e.Y));
                          //  Draw();
                            break;
                        }
                }

                if (contour.Count>= 2)
                {   
                    Element el = new Element();

                    el.LineColour = lineColour;
                    el.FillColour1 = fillColour1;
                    el.FillColour2 = fillColour2;
                    el.HatchStyle = hStyle;
                    el.LgMode = lgMode;
                    el.Path = path;
                    el.LineWidth = lineWidth;
                    foreach (Point p in contour) el.PointList.Add(p);
                    el.Shape = shape;
                    el.Style = style;

                    if (checkedListBox1.SelectedItem != null)
                    {
                        foreach (Layer l in data[tabControl1.SelectedTab])
                        {
                            if (l.Name == Convert.ToInt32(checkedListBox1.SelectedItem.ToString().Substring(6)))
                            {
                                l.Elements.Add(el);

                            }
                        }
                    }
                    else
                    {
                        foreach (Layer l in data[tabControl1.SelectedTab])
                        {
                            if (l.Name == Convert.ToInt32(checkedListBox1.Items[checkedListBox1.Items.Count-1].ToString().Substring(6)))
                            {
                                l.Elements.Add(el);

                            }
                        }
                    }
                    //docs[tabControl1.SelectedTab][Convert.ToInt32(checkedListBox1.SelectedValue.ToString())].Elements.Add(el);
                    ImageReload();
                    //tabControl1.SelectedTab.Controls[0].Refresh();
                }
                contour.Clear();
            }
           
        }

        private void Draw()
        {
            PictureBox pb = new PictureBox();
            pb = (PictureBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
            Bitmap b = new Bitmap(pb.Width, pb.Height);
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(pb.Image,  0, 0, pb.Width, pb.Height);
            Pen p = new Pen(lineColour, lineWidth);


            switch (shape)
            {
                case "line":
                    {
                        if (contour.Count == 2)
                        {
                            g.DrawLine(p, contour[0].X, contour[0].Y, contour[1].X, contour[1].Y);
                        }
                        break;
                    }
                case "rectangle":
                    {
                        if (contour.Count == 2)
                        {
                            int x0 = contour[0].X;
                            int x1 = contour[1].X;
                            int y0 = contour[0].Y;
                            int y1 = contour[1].Y;
                            int startX = (x0 < x1) ? x0 : x1;
                            int startY = (y0 < y1) ? y0 : y1;
                            int width = Math.Abs(x1 - x0);
                            int height = Math.Abs(y1 - y0);

                            g.DrawRectangle(p, startX, startY, width, height);
                        }
                        break;
                    }
                case "ellipse":
                    {
                       g.DrawEllipse(p, contour[0].X, contour[0].Y, contour[1].X - contour[0].X, contour[1].Y - contour[0].Y);
                        break;
                    }
                case "triangle":
                    {

                        GraphicsPath path1 = new GraphicsPath();
                        path1.StartFigure();
                        path1.AddLine(contour[0].X, contour[0].Y, contour[1].X, contour[1].Y);
                        path1.AddLine(contour[1].X, contour[1].Y, contour[2].X, contour[2].Y);
                        path1.CloseFigure();
                        g.DrawPath(p, path1);
                        break;
                    }
                case "path":
                    {


                        GraphicsPath path1 = new GraphicsPath();
                        path1.StartFigure();
                        for (int i = 1; i < contour.Count - 1; i++)
                        {
                            path1.AddLine(contour[i - 1].X, contour[i - 1].Y, contour[i].X, contour[i].Y);
                        }
                        g.DrawPath(p, path1);
                        break;
                    }
                case "freeDots":
                    {
                        for (int i = 1; i < contour.Count - 1; i++)
                        {
                            g.DrawLine(p, contour[i - 1].X, contour[i - 1].Y, contour[i].X, contour[i].Y);
                        }
                        break;
                    }
                case "multiline":
                    {
                        for (int i = 1; i < contour.Count - 1; i++)
                        {
                            g.DrawLine(p, contour[i - 1].X, contour[i - 1].Y, contour[i].X, contour[i].Y);
                        }
                        break;
                    }
            }
            pb.Image = b;
            g.Dispose();
        }

        private void PB_MouseClick(object sender, MouseEventArgs e)
        {
            if (shape == "triangle")
            {
                if (drawFlag == false && triangleCount == 0)
                {
                    contour.Add(new Point(e.X, e.Y));
                    triangleCount++;
                    drawFlag = true;
                   // Draw();
                }
                else if (drawFlag == true && triangleCount < 3)
                {
                    contour.Add(new Point(e.X, e.Y));
                    triangleCount++;
                   // Draw();
                    

                    if (triangleCount == 3)
                    {
                        
                        Element el = new Element();

                        el.LineColour = lineColour;
                        el.FillColour1 = fillColour1;
                        el.FillColour2 = fillColour2;
                        el.HatchStyle = hStyle;
                        el.LgMode = lgMode;
                        el.Path = path;
                        el.LineWidth = lineWidth;
                        foreach (Point p in contour) el.PointList.Add(p);
                        el.Shape = shape;
                        el.Style = style;
                        el.Text = text;

                        if (checkedListBox1.SelectedItem != null)
                        {
                            foreach (Layer l in data[tabControl1.SelectedTab])
                            {
                                if (l.Name == Convert.ToInt32(checkedListBox1.SelectedItem.ToString().Substring(6)))
                                {
                                    l.Elements.Add(el);

                                }
                            }
                        }
                        else
                        {
                            foreach (Layer l in data[tabControl1.SelectedTab])
                            {
                                if (l.Name == Convert.ToInt32(checkedListBox1.Items[checkedListBox1.Items.Count - 1].ToString().Substring(6)))
                                {
                                    l.Elements.Add(el);

                                }
                            }
                        }
                        //docs[tabControl1.SelectedTab][Convert.ToInt32(checkedListBox1.SelectedValue.ToString())].Elements.Add(el);
                        ImageReload();
                        //tabControl1.SelectedTab.Controls[0].Refresh();
                        contour.Clear();
                        triangleCount = 0;
                        drawFlag = false;
                    }
                }
            }
            if (shape == "multiline")
            {
                if (drawFlag == false && contour.Count == 0)
                {
                    contour.Add(new Point(e.X, e.Y));
                    drawFlag = true;
                  //  Draw();
                }
                else if (drawFlag == true)
                {
                    contour.Add(new Point(e.X, e.Y));
                 //   Draw();
                }
            }
        }

        private void PB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (shape == "multiline")
            {
                if (drawFlag == true)
                {
                    drawFlag = false;
                    Element el = new Element();

                    el.LineColour = lineColour;
                    el.FillColour1 = fillColour1;
                    el.FillColour2 = fillColour2;
                    el.HatchStyle = hStyle;
                    el.LgMode = lgMode;
                    el.Path = path;
                    el.LineWidth = lineWidth;
                    foreach (Point p in contour) el.PointList.Add(p);
                    el.Shape = shape;
                    el.Style = style;
                    el.Text = text;



                    if (checkedListBox1.SelectedItem != null)
                    {
                        foreach (Layer l in data[tabControl1.SelectedTab])
                        {
                            if (l.Name == Convert.ToInt32(checkedListBox1.SelectedItem.ToString().Substring(6)))
                            {
                                l.Elements.Add(el);

                            }
                        }
                    }
                    else
                    {
                        foreach (Layer l in data[tabControl1.SelectedTab])
                        {
                            if (l.Name == Convert.ToInt32(checkedListBox1.Items[checkedListBox1.Items.Count - 1].ToString().Substring(6)))
                            {
                                l.Elements.Add(el);

                            }
                        }
                    }
                    //data[tabControl1.SelectedTab][Convert.ToInt32(checkedListBox1.SelectedValue.ToString())].
                    ImageReload();
                    //tabControl1.SelectedTab.Controls[0].Refresh();
                    contour.Clear();
                }
            }
                  
               
                else if (shape == "path")
                {
                    contour.Add(new Point(e.X, e.Y));
                    drawFlag = false;
                    Element el = new Element();

                    el.LineColour = lineColour;
                    el.FillColour1 = fillColour1;
                    el.FillColour2 = fillColour2;
                    el.HatchStyle = hStyle;
                    el.LgMode = lgMode;
                    el.Path = path;
                    el.LineWidth = lineWidth;
                    foreach (Point p in contour) el.PointList.Add(p);
                    el.Shape = shape;
                    el.Style = style;
                    el.Text = text;

                    if (checkedListBox1.SelectedItem != null)
                    {
                        foreach (Layer l in data[tabControl1.SelectedTab])
                        {
                            if (l.Name == Convert.ToInt32(checkedListBox1.SelectedItem.ToString().Substring(6)))
                            {
                                l.Elements.Add(el);

                            }
                        }
                    }
                    else
                    {
                        foreach (Layer l in data[tabControl1.SelectedTab])
                        {
                            if (l.Name == Convert.ToInt32(checkedListBox1.Items[checkedListBox1.Items.Count - 1].ToString().Substring(6)))
                            {
                                l.Elements.Add(el);

                            }
                        }
                    }
                   
                    ImageReload();                   
                    contour.Clear();
                    text = "";
                }
                              
            
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void cLOSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (data.Count > 0 && tabControl1.TabPages.Count > 0)
            {
                data.Remove(tabControl1.SelectedTab);
                tabControl1.SelectedTab.Dispose();
                checkedListBox1.Items.Clear();
                layerCount = 0;
                ImageReload();
            }
        }

        private void cLOSEALLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            data.Clear();
            //newTabFlag = true;
            tabControl1.TabPages.Clear();
            checkedListBox1.Items.Clear();
            layerCount = 0;
            NewTab();
            //ImageReload();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            LineForm lwForm = new LineForm();

            lwForm.OnWidthSelected += Form_OnWidthSelected;

            DialogResult res = lwForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                MessageBox.Show(lwForm.Name);
            }
        }

        private void Form_OnWidthSelected(object sender, int width)
        {
            lineWidth = width;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            OpacityForm oForm = new OpacityForm();

            oForm.OnOpacitySelected += Form_OnOpacitySelected;

            DialogResult res = oForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                MessageBox.Show(oForm.Name);
            }
        }

        private void Form_OnOpacitySelected(object sender,float op)
        {
            if (checkedListBox1.SelectedItem == null) MessageBox.Show("No layer selected!");
            else
            {
                layerTransparency = op;
                foreach (Layer l in data[tabControl1.SelectedTab])
                {
                    if (l.Name == Convert.ToInt32(checkedListBox1.SelectedItem.ToString().Substring(6)))
                    {
                        l.Opacity = op;
                        

                    }
                }
            }
           
            ImageReload();
            //tabControl1.SelectedTab.Controls[0].Refresh();
        }


        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            ImageReload();           
        }

        private void aBOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Graphics editor MyBrush2.0\r\ncreated by Oleg Sydorov\r\nin September 2021\r\nas a study project for ItStepAcademy\r\nNo rights reserved, no complaints accepted");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            comboBox2.Visible = true;
            button8.Visible = false;
            style = "hatch";
        }

        private void hELPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void hELPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string message = "=========================================\r\n" +
                           "USEFUL RECOMMENDATIONS\r\n" +
                           "=========================================\r\n" +
                           "LAYERS - you can add layer(s) and delete layer(s)\r\n" +
                           "       - you can select opacity and visibility for every layer \r\n" +
                           "       - to alter/delete one layer - you have to select it first\r\n" +
                           "       - you can switch between RGB/GrayScale by clicking correspoding icon\r\n\r\n" +
                           "BACKGROUND - for every layer you can fill it with an image/pattern/colour/gradient or clear it \r\n\r\n" +

                           "SHAPES - you can add contour or filled shape\r\n" +
                           "       - to draw rectangles, ellipses and lines - click left mouse button, move mouse to draw a diagonal and release button to finish\r\n" +
                           "       - to draw triangle - click three dots corresponding to triangle apexes\r\n" +
                           "       - to draw multiline - click for line joints and doubleclick to finish\r\n" +
                           "       - to draw filled shapes click fill icon\r\n" +
                           "       - to draw gradient filled shapes click \"gradient fill\" button \r\n" +
                           "       - to draw pattern filled shapes click \"Pattern fill\" button \r\n" +
                           "       - to draw texture filled shapes click \"Picture fill\" button \r\n" +
                           "       - to draw text click letter icon\r\n" +
                           
                           "PENCIL - to draw with a pencil - click left mouse button to start, move mouse to draw and release button to finish\r\n\r\n";
                           
                           
            string title = "GUIDE";
            MessageBox.Show(message, title);

        }

        private void aBOUTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void nEWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTab();
           /* TabPage page = new TabPage();
            newTabFlag = true;
            if (tabControl1.TabPages.Count > 0)
            {
                pageCount++;
                page.Text = "New image " + pageCount;
            }
            else
            {
                page.Text = "New image";
            }
            page.BackColor = Color.LightGray;
            
            PictureBox pictureBox = new PictureBox();
            pictureBox.Parent = page;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.MouseDown += PB_MouseDown;
            pictureBox.MouseMove += PB_MouseMove;
            pictureBox.MouseUp += PB_MouseUp;
            pictureBox.MouseClick += PB_MouseClick;
            pictureBox.MouseDoubleClick += PB_MouseDoubleClick;

            tabControl1.SelectedTab = page;

            tabControl1.TabPages.Add(page);
            checkedListBox1.Items.Clear();
            layerCount = 0;
            
            //foreach (Layer l in data[tabControl1.SelectedTab])
            //{
            //    comboBox2.Items.Add("Layer " + l.Name);
            //}

            List<Layer>listNew = new List<Layer>();

            Layer layer = new Layer(pictureBox.Width, pictureBox.Height, 1f);

            layer.FocusFlag = true;
            layer.Name = layerCount;
            checkedListBox1.Items.Add("Layer " + layer.Name);
            newFlag = true;
            checkedListBox1.SetItemChecked(layerCount, true);

            layerCount++;

            listNew.Add(layer);

            data.Add(page, listNew);


            //docs.Add(tabControl1.SelectedTab, data);
            pictureBox.Image = layer.Show(pictureBox.Width, pictureBox.Height);

            //Dictionary<int, Layer> dataNew = new Dictionary<int, Layer>();
            //docs.Add(tabControl1.SelectedTab, dataNew);*/

        }

        private void NewTab()
        {
            TabPage page = new TabPage();
            newTabFlag = true;
            if (tabControl1.TabPages.Count > 0)
            {
                pageCount++;
                page.Text = "New image " + pageCount;
            }
            else
            {
                page.Text = "New image";
            }
            page.BackColor = Color.LightGray;

            PictureBox pictureBox = new PictureBox();
            pictureBox.Parent = page;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.MouseDown += PB_MouseDown;
            pictureBox.MouseMove += PB_MouseMove;
            pictureBox.MouseUp += PB_MouseUp;
            pictureBox.MouseClick += PB_MouseClick;
            pictureBox.MouseDoubleClick += PB_MouseDoubleClick;

            tabControl1.SelectedTab = page;

            tabControl1.TabPages.Add(page);
            checkedListBox1.Items.Clear();
            layerCount = 0;           

            List<Layer> listNew = new List<Layer>();

            Layer layer = new Layer(pictureBox.Width, pictureBox.Height, 1f);

            layer.FocusFlag = true;
            layer.Name = layerCount;
            checkedListBox1.Items.Add("Layer " + layer.Name);
            newFlag = true;
            checkedListBox1.SetItemChecked(layerCount, true);

            layerCount++;

            listNew.Add(layer);

            data.Add(page, listNew);
         
            pictureBox.Image = layer.Show(pictureBox.Width, pictureBox.Height);
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            hStyle= (HatchStyle)Enum.Parse(typeof(HatchStyle), comboBox2.SelectedItem.ToString(), true);
            comboBox2.Visible = false;
            button8.Visible = true;
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Element basic = new Element();

            Layer layer = new Layer(tabControl1.SelectedTab.Controls[0].Width, tabControl1.SelectedTab.Controls[0].Height, 1f);

            //layer.Add(basic);
            layer.FocusFlag = true;
            layer.Name = layerCount;

            
            //list.Add(layer);
            //data.Add(tabControl1.SelectedTab, list);
            checkedListBox1.Items.Add("Layer " + layer.Name);
            newFlag = true;


            checkedListBox1.SetItemChecked(checkedListBox1.Items.Count-1, true);


            data[tabControl1.SelectedTab].Add(layer);
            layerCount++;
        }

        private void textureFillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItem == null) MessageBox.Show("No layer selected!");
            else
            {
                openFileDialog1.Title = "Opening picture";
                openFileDialog1.Filter = "Image Files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF | All files(*.*) | *.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.Multiselect = false;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    foreach (Layer l in data[tabControl1.SelectedTab])
                    {
                        if (l.Name == Convert.ToInt32(checkedListBox1.SelectedItem.ToString().Substring(6)))
                        {
                            l.Back.Style = "texture";
                            l.Back.Path = openFileDialog1.FileName;
                        }
                    }
                }
                ImageReload();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItem == null) MessageBox.Show("No layer selected!");
            else
            {
                string name = checkedListBox1.SelectedItem.ToString();
                int index = Convert.ToInt32(name.Substring(6));



                foreach (Layer l in data[tabControl1.SelectedTab])
                {
                    if (l.Name == Convert.ToInt32(checkedListBox1.SelectedItem.ToString().Substring(6)))
                    {
                        data[tabControl1.SelectedTab].Remove(l);
                        checkedListBox1.Items.Remove(name);
                        break;
                    }
                }

                ImageReload();
                //tabControl1.SelectedTab.Controls[0].Refresh();
            }
        }

        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            data[tabControl1.SelectedTab].Clear();
            checkedListBox1.Items.Clear();
            layerCount = 0;
            ImageReload();
        }

        private void colourFillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItem == null) MessageBox.Show("No layer selected!");
            else
            {
                colorDialog2.FullOpen = true;


                colorDialog2.AllowFullOpen = true;


                DialogResult result = colorDialog2.ShowDialog();
                if (result == DialogResult.OK)
                {
                    foreach (Layer l in data[tabControl1.SelectedTab])
                    {
                        if (l.Name == Convert.ToInt32(checkedListBox1.SelectedItem.ToString().Substring(6)))
                        {
                            l.Back.Style = "fill";
                            l.Back.FillColour1 = colorDialog2.Color;
                        }
                    }

                }
                ImageReload();
                //tabControl1.SelectedTab.Controls[0].Refresh();
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItem == null) MessageBox.Show("No layer selected!");
            else
            {
                foreach (Layer l in data[tabControl1.SelectedTab])
                {
                    if (l.Name == Convert.ToInt32(checkedListBox1.SelectedItem.ToString().Substring(6)))
                    {
                        l.Back.Style = "fill";
                        l.Back.FillColour1 = Color.Transparent;
                    }
                }
            }
            ImageReload();
            //tabControl1.SelectedTab.Controls[0].Refresh();

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (style != "gradient")
            {
                style = "gradient";
                colorDialog3.FullOpen = true;


                colorDialog3.AllowFullOpen = true;


                DialogResult result1 = colorDialog3.ShowDialog();
                if (result1 == DialogResult.OK)
                {
                    fillColour1 = colorDialog3.Color;
                }

                colorDialog4.FullOpen = true;


                colorDialog4.AllowFullOpen = true;


                DialogResult result2 = colorDialog4.ShowDialog();
                if (result2 == DialogResult.OK)
                {
                    fillColour2 = colorDialog4.Color;
                }
                button14.BackColor = Color.DarkGray;
            }
            else if (style == "gradient")
            {
                style = "draw";
                button14.BackColor = SystemColors.Control;
            }


        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (style != "texture")
            {
                style = "texture";
                openFileDialog2.Title = "Opening picture";
                openFileDialog2.Filter = "Image Files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF | All files(*.*) | *.*";
                openFileDialog2.FilterIndex = 1;
                openFileDialog2.CheckFileExists = true;
                openFileDialog2.Multiselect = false;
                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                   path = openFileDialog2.FileName;
                }
                button12.BackColor = Color.DarkGray;
            }
            else if (style == "texture")
            {
                style = "draw";
                button12.BackColor = SystemColors.Control;
            }


        }

        private void ImageReload()
        {
            int w = tabControl1.SelectedTab.Controls[0].Width;
            int h = tabControl1.SelectedTab.Controls[0].Height;

            Bitmap b = new Bitmap(w, h);
            Graphics resultGraphics = Graphics.FromImage(b);
            foreach (Layer l in data[tabControl1.SelectedTab])
            {
                resultGraphics.DrawImage(l.Show(w, h), new Point(0, 0));
            }  
            
            PictureBox pb = new PictureBox();
            pb = (PictureBox)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
            pb.Image = b;
            resultGraphics.Dispose();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (newFlag ==false)
            {
                if (reverseFlag == false)
                {
                    foreach (Layer l in data[tabControl1.SelectedTab])
                    {
                        if (l.Name == Convert.ToInt32(checkedListBox1.SelectedItem.ToString().Substring(6)))
                        {
                            if (checkedListBox1.CheckedItems.Contains("Layer " + l.Name))
                                l.VisibilityFlag = false;
                            else l.VisibilityFlag = true;
                        }
                    }
                    ImageReload();
                }
                
            }
            else newFlag = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            hStyle = (HatchStyle)Enum.Parse(typeof(HatchStyle), comboBox2.SelectedItem.ToString(), true);
            comboBox2.Visible = false;
            button8.Visible = true;
           
        }

        private void gradientFillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItem == null) MessageBox.Show("No layer selected!");
            else
            {
                colorDialog5.FullOpen = true;
                colorDialog5.AllowFullOpen = true;
                DialogResult result1 = colorDialog5.ShowDialog();
                if (result1 == DialogResult.OK)
                {
                    colorDialog6.FullOpen = true;
                    colorDialog6.AllowFullOpen = true;
                    DialogResult result2 = colorDialog6.ShowDialog();
                    if (result2 == DialogResult.OK)
                    {
                        foreach (Layer l in data[tabControl1.SelectedTab])
                        {
                            if (l.Name == Convert.ToInt32(checkedListBox1.SelectedItem.ToString().Substring(6)))
                            {
                                l.Back.Style = "gradient";
                                l.Back.FillColour1 = colorDialog5.Color;
                                l.Back.FillColour2 = colorDialog6.Color;
                            }
                        }
                    }
                }
                ImageReload();
            }
        }

        private void hatchFillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HatchForm hsForm = new HatchForm();

            hsForm.OnHatchStyleSelected += Form_OnHatchStyleSelected;

            DialogResult res = hsForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                MessageBox.Show(hsForm.Name);
            }
        }

        private void Form_OnHatchStyleSelected(object sender, HatchStyle style)
        {
            
            foreach (Layer l in data[tabControl1.SelectedTab])
            {
                if (l.Name == Convert.ToInt32(checkedListBox1.SelectedItem.ToString().Substring(6)))
                {
                    l.Back.Style = "hatch";
                    l.Back.HatchStyle = style;
                }
            }
            ImageReload();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItem == null) MessageBox.Show("No layer selected!");
            else
            {
                foreach (Layer l in data[tabControl1.SelectedTab])
                {
                    if (l.Name == Convert.ToInt32(checkedListBox1.SelectedItem.ToString().Substring(6)))
                    {
                        if (l.GrayScale == false)
                        {
                            button9.BackgroundImage = new Bitmap(Image.FromFile("colour.png"));
                            l.GrayScale = true;
                        }
                        else if (l.GrayScale == true)
                        {
                            button9.BackgroundImage = new Bitmap(Image.FromFile("bw.png"));
                            l.GrayScale = false;
                        }
                    }
                }
                ImageReload();
            }
        }

        private void OnSelectedTabChanged(object obj, EventArgs e)
        {
            if (data.Count > 0)
            {
                checkedListBox1.Items.Clear();
                layerCount = 0;

                if (newTabFlag == false)
                {
                    reverseFlag = true;
                    int n = 0;
                    foreach (Layer l in data[tabControl1.SelectedTab])
                    {
                        checkedListBox1.Items.Add("Layer " + l.Name);
                        layerCount = l.Name;
                        if (l.VisibilityFlag == true)
                        {
                            checkedListBox1.SetItemChecked(n, true);
                        }
                        n++;
                    }
                    layerCount++;
                    reverseFlag = false;
                }
                else newTabFlag = false;
            }
        }

        private void oPENXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage page = new TabPage();
            newTabFlag = true;
            //page.Text = ;
            
            page.BackColor = Color.LightGray;

            PictureBox pictureBox = new PictureBox();
            pictureBox.Parent = page;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.MouseDown += PB_MouseDown;
            pictureBox.MouseMove += PB_MouseMove;
            pictureBox.MouseUp += PB_MouseUp;
            pictureBox.MouseClick += PB_MouseClick;
            pictureBox.MouseDoubleClick += PB_MouseDoubleClick;

            

            tabControl1.TabPages.Add(page);
            checkedListBox1.Items.Clear();

            layerCount = 0;
            List<Layer> listNew = new List<Layer>();

            string xmlPath = null;

            openFileDialog3.Title = "Opening XML document ...";
            openFileDialog3.Filter = "XML Files(*.xml ; *.XML) |*.xml; *.XML | All files(*.*) | *.*";
            openFileDialog3.FilterIndex = 1;
            openFileDialog3.CheckFileExists = true;
            openFileDialog3.Multiselect = false;
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
               xmlPath=openFileDialog3.FileName;
                string fileName = openFileDialog3.FileName.Substring(openFileDialog3.FileName.LastIndexOf('\\') + 1);

                page.Text = fileName.Substring(0, fileName.Length - fileName.LastIndexOf('.')-1);
                int nameN = 0;
                bool grayscaleN = false;
                float opacityN = 1f;

                string backStyleN = "fill";
                Color backLineColourN = Color.Black;
                Color backFillColour1N = Color.White;
                Color backFillColour2N = Color.Gray;
                HatchStyle backHStyleN = HatchStyle.Cross;
                LinearGradientMode backLgModeN = LinearGradientMode.Horizontal;
                string backPathN = "unavailable";



                string shapeN = null;
                string textN = "";
                Color lineColourN = Color.Black;
                Color fillColour1N = Color.Transparent;
                Color fillColour2N = Color.Transparent;
                HatchStyle hStyleN = HatchStyle.Cross;
                LinearGradientMode lgModeN = LinearGradientMode.Horizontal;
                string pathN = "unavailable";
                int lineWidthN = 1;
                string styleN = "draw";


                XmlDocument doc1 = new XmlDocument();
                doc1.Load(xmlPath);

                foreach (XmlNode nodeRoot in doc1.ChildNodes)
                {
                    if (nodeRoot.NodeType == XmlNodeType.Element)
                    {
                        if (nodeRoot.ChildNodes.Count > 0)
                        {
                            foreach (XmlNode node in nodeRoot.ChildNodes)
                            {
                                if (node.Name.Contains("Layer"))
                                {
                                    nameN = Convert.ToInt32(node.Name.Substring(6));

                                    Layer layerN = new Layer(tabControl1.SelectedTab.Controls[0].Width, tabControl1.SelectedTab.Controls[0].Height, 1f);

                                    layerN.Name = nameN;
                                    layerCount = nameN;

                                    if (node.Attributes.Count > 0)
                                    {
                                        foreach (XmlAttribute attr in node.Attributes)
                                        {
                                            if (attr.Name == "Opacity")
                                            {
                                                opacityN = (float)Convert.ToDouble(attr.Value);
                                            }
                                            else if (attr.Name == "Grayscale")
                                            {
                                                grayscaleN = Convert.ToBoolean(attr.Value);
                                            }
                                            else if (attr.Name == "Background_LineColour")
                                            {
                                                backLineColourN = Color.FromName(attr.Value);
                                            }
                                            else if (attr.Name == "Background_FillColour1")
                                            {
                                                backFillColour1N = Color.FromName(attr.Value);
                                            }
                                            else if (attr.Name == "Background_FillColour2")
                                            {
                                                backFillColour2N = Color.FromName(attr.Value);
                                            }
                                            else if (attr.Name == "Background_HatchStyle")
                                            {
                                                backHStyleN = (HatchStyle)Enum.Parse(typeof(HatchStyle), attr.Value, true);
                                            }
                                            else if (attr.Name == "Background_LinearGradientMode")
                                            {
                                                backLgModeN = LinearGradientMode.Horizontal;
                                            }
                                            else if (attr.Name == "Background_Path")
                                            {
                                                backPathN = attr.Value;
                                            }
                                            else if (attr.Name == "Background_Style")
                                            {
                                                backStyleN = attr.Value;
                                            }

                                           
                                        }
                                        
                                        layerN.Opacity = opacityN;
                                            layerN.GrayScale = grayscaleN;
                                            layerN.Back.LineColour = backLineColourN;

                                            layerN.Back.Style = backStyleN;
                                            layerN.Back.LineColour = backLineColourN;
                                            layerN.Back.FillColour1 = backFillColour1N;
                                            layerN.Back.FillColour2 = backFillColour2N;
                                            layerN.Back.LgMode = backLgModeN;
                                            layerN.Back.HatchStyle = backHStyleN;
                                            layerN.Back.Path = backPathN;

                                           // MessageBox.Show(nameN + "\r\n" + grayscaleN + "\r\n" + opacityN + "\r\n\r\n" + backStyleN + "\r\n" +
                                            //                backLineColourN + "\r\n" + backFillColour1N + "\r\n" + backFillColour2N + "\r\n" +
                                            //                backHStyleN + "\r\n" + backLgModeN + "\r\n" + backPathN);
                                    }

                                    if (node.ChildNodes.Count > 0)
                                    {
                                        foreach (XmlNode cNode in node.ChildNodes)
                                        {
                                            if (cNode.Name == "ElementList")
                                            {
                                                if (cNode.ChildNodes.Count > 0)
                                                {

                                                    foreach (XmlNode scNode in cNode.ChildNodes)
                                                    {
                                                        if (scNode.Name.Contains("Element_"))
                                                        {
                                                            Element elN = new Element();
                                                            List<Point> pointsN = new List<Point>();

                                                            if (scNode.Attributes.Count > 0)
                                                            {
                                                                foreach (XmlAttribute cAttr in scNode.Attributes)
                                                                {
                                                                    if (cAttr.Name.Contains("_Style"))
                                                                    {
                                                                        styleN = cAttr.Value;
                                                                    }
                                                                    else if (cAttr.Name.Contains("_Shape"))
                                                                    {
                                                                        shapeN = cAttr.Value;
                                                                    }
                                                                    else if (cAttr.Name.Contains("_LineColour"))
                                                                    {
                                                                        lineColourN = Color.FromName(cAttr.Value);
                                                                    }
                                                                    else if (cAttr.Name.Contains("_LineWidth"))
                                                                    {
                                                                        lineWidthN = Convert.ToInt32(cAttr.Value);
                                                                    }
                                                                    else if (cAttr.Name.Contains("_FillColour1"))
                                                                    {
                                                                        fillColour1N = Color.FromName(cAttr.Value);
                                                                    }
                                                                    else if (cAttr.Name.Contains("_FillColour2"))
                                                                    {
                                                                        fillColour2N = Color.FromName(cAttr.Value);
                                                                    }
                                                                    else if (cAttr.Name.Contains("_HatchStyle"))
                                                                    {
                                                                        hStyleN = (HatchStyle)Enum.Parse(typeof(HatchStyle), cAttr.Value, true);
                                                                    }
                                                                    else if (cAttr.Name.Contains("_LinearGradientMode"))
                                                                    {
                                                                        lgModeN = LinearGradientMode.Horizontal;
                                                                    }
                                                                    else if (cAttr.Name.Contains("_Path"))
                                                                    {
                                                                        pathN = cAttr.Value;
                                                                    }
                                                                    else if (cAttr.Name.Contains("_Text"))
                                                                    {
                                                                        textN = cAttr.Value;
                                                                    }
                                                                    else if (cAttr.Name.Contains("Points"))
                                                                    {
                                                                        if (cAttr.Value != "unavailable")
                                                                        {
                                                                            foreach (XmlText t in cAttr.ChildNodes)
                                                                            {
                                                                                string point = t.Value;
                                                                                char[] delimiters = new char[] { '<', '>', ' ' };
                                                                                string[] subs = point.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                                                                                for (int i = 0; i < subs.Length; i+=2)
                                                                                {
                                                                                    elN.PointList.Add(new Point(Convert.ToInt32(subs[i]), Convert.ToInt32(subs[i+1])));
                                                                                }
                                                                                

                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            //MessageBox.Show(styleN + "\r\n" + shapeN + "\r\n" + lineWidthN + "\r\n" +
                                                            //               lineColourN + "\r\n" + fillColour1N + "\r\n" + fillColour2N + "\r\n" +
                                                            //               hStyleN + "\r\n" + lgModeN + "\r\n" + pathN + "\r\n" +textN+ "\r\n" + elN.PointList.Count);


                                                            elN.Style = styleN;
                                                            elN.Shape = shapeN;
                                                            elN.LineColour = lineColourN;
                                                            elN.LineWidth = lineWidthN;
                                                            elN.FillColour1 = fillColour1N;
                                                            elN.FillColour2 = fillColour2N;
                                                            elN.HatchStyle = hStyleN;
                                                            elN.LgMode = lgModeN;
                                                            elN.Path = pathN;
                                                            elN.Text = textN;

                                                            layerN.Elements.Add(elN);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    checkedListBox1.Items.Add("Layer " + layerN.Name);
                                    newFlag = true;
                                    checkedListBox1.SetItemChecked(layerCount, true);
                                    listNew.Add(layerN);
                                }
                            }
                        }
                    }
                

                }
                layerCount++;

                data.Add(page, listNew);
                tabControl1.SelectedTab = page;

                ImageReload();

            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            shape = "path";
            AddTextForm afForm = new AddTextForm();
            afForm.OnTextAdded += Form_OnTextAdded;

            DialogResult res = afForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                MessageBox.Show(afForm.Name);
            }
        }

        private void Form_OnTextAdded(object sender, string text)
        {
            if (text.Length > 0)
                this.text = text;
            else MessageBox.Show("No text entered!");
        }

       
    }
}
