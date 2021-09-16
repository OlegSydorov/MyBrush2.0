using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWF_MyBrush2._0
{
    class Background
    {
        Color lineColour;
        Color fillColour1;
        Color fillColour2;
        HatchStyle hatchStyle;
        LinearGradientMode lgMode;
        float opacity;
        string path;

        string style;
            

        public Color LineColour { get { return lineColour; } set { lineColour = value; } }
        public Color FillColour1 { get { return fillColour1; } set { fillColour1 = value; } }
        public Color FillColour2 { get { return fillColour2; } set { fillColour2 = value; } }
        public HatchStyle HatchStyle { get { return hatchStyle; } set { hatchStyle = value; } }
        public LinearGradientMode LgMode { get { return lgMode; } set { lgMode = value; } }
        public float Opacity { get { return opacity; } set { opacity = value; } }
        
        public string Path { get { return path; } set { path = value; } }

        public string Style { get { return style ; } set { style = value; } }
       
        public Background()
        {
            lineColour = Color.Black;
            fillColour1 = Color.Transparent;
            fillColour2 = Color.Transparent;
            hatchStyle = HatchStyle.Cross;
            lgMode = LinearGradientMode.Horizontal;
            opacity = 1f;
            path = "unavailable";

            style = "fill";

        }

        public Bitmap Show(int width, int height)
        {

            Bitmap resultImage = new Bitmap(width, height);
            Graphics resultGraphics = Graphics.FromImage(resultImage);

            switch (style)
            {
                case "fill":
            {
                        Brush brush1 = new SolidBrush(fillColour1);
                        resultGraphics.FillRectangle(brush1, 0, 0, width, height);
                        break;
                    }
                case "hatch":
                    {
                        Brush brush1 = new HatchBrush(hatchStyle, lineColour, fillColour1);
                        if (lineColour == fillColour1) brush1 = new HatchBrush(hatchStyle, fillColour2, lineColour);

                        resultGraphics.FillRectangle(brush1, 0, 0, width, height);
                        break;
                    }
                case "gradient":
                    {
                        LinearGradientBrush brush1 = new LinearGradientBrush(new Rectangle(0, 0, width, height), fillColour1, fillColour2, lgMode);
                        resultGraphics.FillRectangle(brush1, 0, 0, width, height);
                        break;
                    }
                case "texture":
                    {
                        TextureBrush brush1 = new TextureBrush(new Bitmap(path), WrapMode.Clamp);
                        resultGraphics.FillRectangle(brush1, 0, 0, width, height);
                        break;
                    }
            }
            resultGraphics.Dispose();
            return resultImage;

        }
    }
}
