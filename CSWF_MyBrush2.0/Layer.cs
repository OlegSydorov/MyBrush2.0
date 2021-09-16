using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSWF_MyBrush2._0
{
    class Layer
    {
        int name;
        float opacity;
        List<Element> elements;
        
        Background back;

        bool focusFlag;
        bool grayScale;
        bool visibilityFlag;
        
        int width;
        int height;

        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }

        public float Opacity { get { return opacity; } set { opacity = value; } }

        public List<Element> Elements { get { return elements; } set { elements = value; } }

        public Background Back { get { return back; } set { back = value; } }

        public bool FocusFlag { get { return focusFlag; } set { focusFlag = value; } }

        public bool GrayScale { get { return grayScale; } set { grayScale = value; } }
        public bool VisibilityFlag { get { return visibilityFlag; } set { visibilityFlag = value; } }

        public int Name { get { return name; } set { name = value; } }

        public Layer(int wid, int hei, float transp)
        {
            width = wid;
            height = hei;
            opacity = transp;
            elements = new List<Element>();
            focusFlag = false;
            grayScale = false;
            visibilityFlag = true;
            back = new Background();
        }

        public void Add(Element el)
        {
            elements.Add(el);
        }

        public void Remove(Element el)
        {
            int n = 0;
            int count = 0;
            foreach (Element e in elements)
            {
                if (e.Shape == el.Shape &&
                    e.Path == el.Path &&
                    e.LineWidth == el.LineWidth &&
                    e.LineColour == el.LineColour &&
                    e.FillColour1 == el.FillColour1 &&
                    e.FillColour2 == el.FillColour2 &&
                    e.HatchStyle == el.HatchStyle &&
                    e.Style==el.Style  &&
                    e.PointList.Count == el.PointList.Count)
                {
                    for (int i = 0; i < e.PointList.Count; i++)
                    {
                        if (e.PointList[i].X == el.PointList[i].X && e.PointList[i].Y == el.PointList[i].Y)
                        {
                            count++;
                            continue;
                        }
                        else break;
                    }
                    if (count == e.PointList.Count)
                    {
                        elements.RemoveAt(n);
                        break;
                    }
                }
                else
                {
                    n++;
                    continue;
                }
            }
        }

        public Bitmap Show(int width, int height)
        {

            Bitmap resultImage = new Bitmap(width, height);
            Graphics resultGraphics = Graphics.FromImage(resultImage);

            if (visibilityFlag == true)
            {
                ImageAttributes attrBack = new ImageAttributes();
                ColorMatrix backColorMatrix = new ColorMatrix();
                backColorMatrix.Matrix00 = 1.00f;
                backColorMatrix.Matrix11 = 1.00f;
                backColorMatrix.Matrix22 = 1.00f;
               // MessageBox.Show(opacity.ToString());
                backColorMatrix.Matrix33 = opacity;
                //MessageBox.Show(backColorMatrix.Matrix33.ToString());
                attrBack.SetColorMatrix(backColorMatrix);

                resultGraphics.DrawImage(back.Show(width, height), new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel, attrBack);
               
                foreach (Element ee in elements)
                {
                    resultGraphics.DrawImage(ee.Show(width, height), new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel, attrBack);
                }
                                
                resultGraphics.DrawImage(resultImage, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel, attrBack);
            }
            else if (visibilityFlag == false)
            {
                resultGraphics.FillRectangle(Brushes.Transparent, 0, 0, width, height);
            }

            resultGraphics.Dispose();

            if (grayScale == false) return resultImage;

            else 
            {
                Bitmap bwResultImage = new Bitmap(width, height);
                Graphics bwResultGraphics = Graphics.FromImage(bwResultImage);

                ImageAttributes attrBW = new ImageAttributes();
                ColorMatrix bwColorMatrix = new ColorMatrix(new float[][]
                {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                }

                );
                attrBW.SetColorMatrix(bwColorMatrix);

                bwResultGraphics.DrawImage(resultImage, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel, attrBW);

                bwResultGraphics.Dispose();

                return bwResultImage;
            }
        }
    }
}
