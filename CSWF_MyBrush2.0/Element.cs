using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections;
using System.Drawing;

namespace CSWF_MyBrush2._0
{
    class Element
    {
        List<Point> pointList;
        Color lineColour;
        Color fillColour1;
        Color fillColour2;
        HatchStyle hatchStyle;
        LinearGradientMode lgMode;

        int lineWidth;

        string shape;
        string path;
        string text;

        string style;

        public List<Point> PointList { get { return pointList; } set { pointList = value; } }
        public Color LineColour { get { return lineColour; } set { lineColour = value; } }
        public Color FillColour1 { get { return fillColour1; } set { fillColour1 = value; } }
        public Color FillColour2 { get { return fillColour2; } set { fillColour2 = value; } }
        public HatchStyle HatchStyle { get { return hatchStyle; } set { hatchStyle = value; } }
        public LinearGradientMode LgMode { get { return lgMode; } set { lgMode = value; } }

        public string Text { get { return text; }set { text = value; } }


        public int LineWidth { get { return lineWidth; } set { lineWidth = value; } }

        public string Shape { get { return shape; } set { shape = value; } }
        public string Path { get { return path; } set { path = value; } }

        public string Style { get { return style; } set { style = value; } }

        public Element()
        {
            pointList = new List<Point>();
            lineColour = Color.Black;
            fillColour1 = Color.White;
            fillColour2 = Color.Gray;
            hatchStyle = HatchStyle.Cross;
            lgMode = LinearGradientMode.Horizontal;
            text = "";

            lineWidth = 1;

            shape = "unavailable";
            path = "unavailable";

            style = "draw";
        }

        public Bitmap Show(int width, int height)
        {
            Bitmap resultImage = new Bitmap(width, height);
            Graphics resultGraphics = Graphics.FromImage(resultImage);

            switch (style)
            {
                case "draw":
                    {
                        Pen pen = new Pen(lineColour, lineWidth);
                        switch (shape)
                        {
                            case "line":
                                {
                                    if (pointList.Count == 2)
                                    {
                                        resultGraphics.DrawLine(pen, pointList[0].X, pointList[0].Y, pointList[1].X, pointList[1].Y);
                                    }
                                    break;
                                }
                            case "rectangle":
                                {
                                    if (pointList.Count == 2)
                                    {
                                        int x0 = pointList[0].X;
                                        int x1 = pointList[1].X;
                                        int y0 = pointList[0].Y;
                                        int y1 = pointList[1].Y;
                                        int startX = (x0 < x1) ? x0 : x1;
                                        int startY = (y0 < y1) ? y0 : y1;
                                        int w = Math.Abs(x1 - x0);
                                        int h = Math.Abs(y1 - y0);

                                        resultGraphics.DrawRectangle(pen, startX, startY, w, h);
                                    }
                                    break;
                                }
                            case "ellipse":
                                {
                                    resultGraphics.DrawEllipse(pen, pointList[0].X, pointList[0].Y, pointList[1].X - pointList[0].X, pointList[1].Y - pointList[0].Y);
                                    break;
                                }
                            case "triangle":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.StartFigure();
                                    path1.AddLine(pointList[0].X, pointList[0].Y, pointList[1].X, pointList[1].Y);
                                    path1.AddLine(pointList[1].X, pointList[1].Y, pointList[2].X, pointList[2].Y);
                                    path1.CloseFigure();
                                    resultGraphics.DrawPath(pen, path1);
                                    break;
                                }
                            case "path":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.AddString(text, new FontFamily("Bookman Old Style"), (int)FontStyle.Regular, 100, pointList[0], null);
                                    resultGraphics.DrawPath(pen, path1);
                                    break;
                                }
                            case "freeDots":
                                {
                                    for (int i = 1; i < pointList.Count - 1; i++)
                                    {
                                        resultGraphics.DrawLine(pen, pointList[i - 1].X, pointList[i - 1].Y, pointList[i].X, pointList[i].Y);
                                    }
                                    break;
                                }
                            case "multiline":
                                {
                                    for (int i = 1; i < pointList.Count - 1; i++)
                                    {
                                        resultGraphics.DrawLine(pen, pointList[i - 1].X, pointList[i - 1].Y, pointList[i].X, pointList[i].Y);
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case "fill":
                    {
                        Brush brush1 = new SolidBrush(fillColour1);
                        switch (shape)
                        {
                            case "line":
                                {
                                    if (pointList.Count == 2)
                                    {
                                        Pen pen = new Pen(lineColour, lineWidth);
                                        resultGraphics.DrawLine(pen, pointList[0].X, pointList[0].Y, pointList[1].X, pointList[1].Y);
                                    }
                                    break;
                                }
                            case "rectangle":
                                {
                                    if (pointList.Count == 2)
                                    {
                                        int x0 = pointList[0].X;
                                        int x1 = pointList[1].X;
                                        int y0 = pointList[0].Y;
                                        int y1 = pointList[1].Y;
                                        int startX = (x0 < x1) ? x0 : x1;
                                        int startY = (y0 < y1) ? y0 : y1;
                                        int w = Math.Abs(x1 - x0);
                                        int h = Math.Abs(y1 - y0);

                                        resultGraphics.FillRectangle(brush1, startX, startY, w, h);
                                    }
                                    break;
                                }
                            case "ellipse":
                                {
                                    if (pointList.Count == 2)
                                    {
                                        resultGraphics.FillEllipse(brush1, pointList[0].X, pointList[0].Y, pointList[1].X - pointList[0].X, pointList[1].Y - pointList[0].Y);
                                    }
                                    break;
                                }
                            case "triangle":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.StartFigure();
                                    path1.AddLine(pointList[0].X, pointList[0].Y, pointList[1].X, pointList[1].Y);
                                    path1.AddLine(pointList[1].X, pointList[1].Y, pointList[2].X, pointList[2].Y);
                                    path1.CloseFigure();
                                    resultGraphics.FillPath(brush1, path1);
                                    break;
                                }
                            case "path":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.AddString(text, new FontFamily("Arial"), (int)FontStyle.Italic, 300, pointList[0], null);
                                    resultGraphics.FillPath(brush1, path1);
                                    break;
                                }
                            case "freeDots":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.StartFigure();
                                    for (int i = 1; i < pointList.Count - 1; i++)
                                    {
                                        path1.AddLine(pointList[i].X, pointList[i].Y, pointList[i + 1].X, pointList[i + 1].Y);
                                    }

                                    resultGraphics.FillPath(brush1, path1);
                                    break;
                                }
                            case "multiline":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.StartFigure();
                                    for (int i = 1; i < pointList.Count - 1; i++)
                                    {
                                        path1.AddLine(pointList[i - 1].X, pointList[i - 1].Y, pointList[i].X, pointList[i].Y);
                                    }

                                    resultGraphics.FillPath(brush1, path1);
                                    break;
                                }
                        }
                        break;
                    }

                case "hatch":
                    {
                        Brush brush1 = new HatchBrush(hatchStyle, lineColour, fillColour1);
                        if (lineColour == fillColour1) brush1 = new HatchBrush(hatchStyle, fillColour2, lineColour);

                        switch (shape)
                        {
                            case "line":
                                {
                                    if (pointList.Count == 2)
                                    {
                                        Pen pen = new Pen(lineColour, lineWidth);
                                        resultGraphics.DrawLine(pen, pointList[0].X, pointList[0].Y, pointList[1].X, pointList[1].Y);
                                    }
                                    break;
                                }
                            case "rectangle":
                                {
                                    if (pointList.Count == 2)
                                    {
                                        int x0 = pointList[0].X;
                                        int x1 = pointList[1].X;
                                        int y0 = pointList[0].Y;
                                        int y1 = pointList[1].Y;
                                        int startX = (x0 < x1) ? x0 : x1;
                                        int startY = (y0 < y1) ? y0 : y1;
                                        int w = Math.Abs(x1 - x0);
                                        int h = Math.Abs(y1 - y0);

                                        resultGraphics.FillRectangle(brush1, startX, startY, w, h);
                                    }
                                    break;
                                }
                            case "ellipse":
                                {
                                    if (pointList.Count == 2)
                                    {
                                        resultGraphics.FillEllipse(brush1, pointList[0].X, pointList[0].Y, pointList[1].X - pointList[0].X, pointList[1].Y - pointList[0].Y);
                                    }
                                    break;
                                }
                            case "triangle":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.StartFigure();
                                    path1.AddLine(pointList[0].X, pointList[0].Y, pointList[1].X, pointList[1].Y);
                                    path1.AddLine(pointList[1].X, pointList[1].Y, pointList[2].X, pointList[2].Y);
                                    path1.CloseFigure();
                                    resultGraphics.FillPath(brush1, path1);
                                    break;
                                }
                            case "path":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.AddString(text, new FontFamily("Arial Black"), (int)FontStyle.Bold, 200, pointList[0], null);
                                    resultGraphics.FillPath(brush1, path1);
                                    break;
                                }
                            case "freeDots":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.StartFigure();
                                    for (int i = 0; i < pointList.Count - 1; i++)
                                    {
                                        path1.AddLine(pointList[i].X, pointList[i].Y, pointList[i + 1].X, pointList[i + 1].Y);
                                    }

                                    resultGraphics.FillPath(brush1, path1);
                                    break;
                                }
                            case "multiline":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.StartFigure();
                                    for (int i = 1; i < pointList.Count - 1; i++)
                                    {
                                        path1.AddLine(pointList[i - 1].X, pointList[i - 1].Y, pointList[i].X, pointList[i].Y);
                                    }

                                    resultGraphics.FillPath(brush1, path1);
                                    break;
                                }
                        }
                        break;
                    }
                case "gradient":
                    {

                        switch (shape)
                        {
                            case "line":
                                {
                                    if (pointList.Count == 2)
                                    {
                                        Pen pen = new Pen(lineColour, lineWidth);
                                        resultGraphics.DrawLine(pen, pointList[0].X, pointList[0].Y, pointList[1].X, pointList[1].Y);
                                    }
                                    break;
                                }
                            case "rectangle":
                                {
                                    LinearGradientBrush brush1 = new LinearGradientBrush(new Rectangle(pointList[0].X, pointList[0].Y, pointList[1].X - pointList[0].X, pointList[1].Y - pointList[0].Y), fillColour1, fillColour2, lgMode);
                                    if (pointList.Count == 2)
                                    {
                                        int x0 = pointList[0].X;
                                        int x1 = pointList[1].X;
                                        int y0 = pointList[0].Y;
                                        int y1 = pointList[1].Y;
                                        int startX = (x0 < x1) ? x0 : x1;
                                        int startY = (y0 < y1) ? y0 : y1;
                                        int w = Math.Abs(x1 - x0);
                                        int h = Math.Abs(y1 - y0);



                                        resultGraphics.FillRectangle(brush1, startX, startY, w, h);
                                    }
                                    break;
                                }
                            case "ellipse":
                                {
                                    LinearGradientBrush brush1 = new LinearGradientBrush(new Rectangle(pointList[0].X, pointList[0].Y, pointList[1].X - pointList[0].X, pointList[1].Y - pointList[0].Y), fillColour1, fillColour2, lgMode);
                                    if (pointList.Count == 2)
                                    {
                                        resultGraphics.FillEllipse(brush1, pointList[0].X, pointList[0].Y, pointList[1].X - pointList[0].X, pointList[1].Y - pointList[0].Y);
                                    }
                                    break;
                                }
                            case "triangle":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.StartFigure();
                                    path1.AddLine(pointList[0].X, pointList[0].Y, pointList[1].X, pointList[1].Y);
                                    path1.AddLine(pointList[1].X, pointList[1].Y, pointList[2].X, pointList[2].Y);
                                    path1.CloseFigure();

                                    PathGradientBrush pthGrBrush = new PathGradientBrush(path1);
                                    pthGrBrush.CenterColor = fillColour1;
                                    Color[] colors = { fillColour2 };
                                    pthGrBrush.SurroundColors = colors;

                                    resultGraphics.FillPath(pthGrBrush, path1);
                                    break;
                                }
                            case "path":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.AddString(text, new FontFamily("Garamond"), (int)FontStyle.Bold, 300, pointList[0], null);

                                    PathGradientBrush pthGrBrush = new PathGradientBrush(path1);
                                    pthGrBrush.CenterColor = fillColour1;
                                    Color[] colors = { fillColour2 };
                                    pthGrBrush.SurroundColors = colors;

                                    resultGraphics.FillPath(pthGrBrush, path1);
                                    break;
                                }
                            case "freeDots":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.StartFigure();
                                    for (int i = 0; i < pointList.Count - 1; i++)
                                    {
                                        path1.AddLine(pointList[i].X, pointList[i].Y, pointList[i + 1].X,pointList[i + 1].Y);
                                    }
                                    PathGradientBrush pthGrBrush = new PathGradientBrush(path1);
                                    pthGrBrush.CenterColor = fillColour1;
                                    Color[] colors = { fillColour2 };
                                    pthGrBrush.SurroundColors = colors;

                                    resultGraphics.FillPath(pthGrBrush, path1);
                                    break;
                                }
                            case "multiline":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.StartFigure();
                                    for (int i = 1; i < pointList.Count - 1; i++)
                                    {
                                        path1.AddLine (pointList[i - 1].X, pointList[i - 1].Y, pointList[i].X, pointList[i].Y);
                                    }
                                    PathGradientBrush pthGrBrush = new PathGradientBrush(path1);
                                    pthGrBrush.CenterColor = fillColour1;
                                    Color[] colors = { fillColour2 };
                                    pthGrBrush.SurroundColors = colors;

                                    resultGraphics.FillPath(pthGrBrush, path1);
                                    break;
                                }
                        }
                        break;
                    }

                case "texture":
                    {
                        TextureBrush brush1 = new TextureBrush(new Bitmap(Image.FromFile(path)));
                        brush1.WrapMode = System.Drawing.Drawing2D.WrapMode.Clamp;
                        switch (shape)
                        {
                            case "line":
                                {
                                    if (pointList.Count == 2)
                                    {
                                        Pen pen = new Pen(lineColour, lineWidth);
                                        resultGraphics.DrawLine(pen, pointList[0].X, pointList[0].Y, pointList[1].X, pointList[1].Y);
                                    }
                                    break;
                                }
                            case "rectangle":
                                {
                                    if (pointList.Count == 2)
                                    {
                                        int x0 = pointList[0].X;
                                        int x1 = pointList[1].X;
                                        int y0 = pointList[0].Y;
                                        int y1 = pointList[1].Y;
                                        int startX = (x0 < x1) ? x0 : x1;
                                        int startY = (y0 < y1) ? y0 : y1;
                                        int w = Math.Abs(x1 - x0);
                                        int h = Math.Abs(y1 - y0);



                                        resultGraphics.FillRectangle(brush1, startX, startY, w, h);
                                    }
                                    break;
                                }
                            case "ellipse":
                                {
                                    if (pointList.Count == 2)
                                    {
                                        resultGraphics.FillEllipse(brush1, pointList[0].X, pointList[0].Y, pointList[1].X - pointList[0].X, pointList[1].Y - pointList[0].Y);
                                    }
                                    break;
                                }
                            case "triangle":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.StartFigure();
                                    path1.AddLine(pointList[0].X, pointList[0].Y, pointList[1].X, pointList[1].Y);
                                    path1.AddLine(pointList[1].X, pointList[1].Y, pointList[2].X, pointList[2].Y);
                                    path1.CloseFigure();
                                    resultGraphics.FillPath(brush1, path1);
                                    break;
                                }
                            case "path":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.AddString(text, new FontFamily("Rockwell Extra Bold"), (int)FontStyle.Bold, 250, pointList[0], null);                                                                        
                                    resultGraphics.FillPath(brush1, path1);                                    
                                    break;
                                }
                            case "freeDots":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.StartFigure();
                                    for (int i = 0; i < pointList.Count - 1; i++)
                                    {
                                        path1.AddLine(pointList[i].X, pointList[i].Y, pointList[i + 1].X, pointList[i + 1].Y);
                                    }

                                    resultGraphics.FillPath(brush1, path1);
                                    break;
                                }
                            case "multiline":
                                {
                                    GraphicsPath path1 = new GraphicsPath();
                                    path1.StartFigure();
                                    for (int i = 1; i < pointList.Count - 1; i++)
                                    {
                                        path1.AddLine(pointList[i - 1].X, pointList[i - 1].Y, pointList[i].X, pointList[i].Y);
                                    }

                                    resultGraphics.FillPath(brush1, path1);
                                    break;
                                }
                        }
                        break;
                    }
            }
            resultGraphics.Dispose();
            return resultImage;
        }
    }
}
