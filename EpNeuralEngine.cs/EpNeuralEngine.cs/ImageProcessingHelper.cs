using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace EpNeuralEngine.cs
{
    public class ImageProcessingHelper
    {
        public Image ImageToMonochrome(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            int height, width;

            height = bmp.Height;
            width = bmp.Width;
            
            int r,g,b;
            for (int heighTrav = 0; heighTrav < height; heighTrav++)
            {
                for (int widthTrav = 0; widthTrav < width; widthTrav++)
                {
                    r = bmp.GetPixel(widthTrav, heighTrav).R;
                    g = bmp.GetPixel(widthTrav, heighTrav).G;
                    b = bmp.GetPixel(widthTrav, heighTrav).B;

                    Single colorval = (r+g+b)/3;
                    Color col=new Color();
                    if (colorval > 255 / 2)
                        col = Color.White;
                    else
                        col = Color.Black;
                    bmp.SetPixel(widthTrav, heighTrav, col);

                }
            }
            return bmp;

        }

        public List<object> ListFromImage(Image img)
        {
            List<object> destination = new List<object>();

            Bitmap bmp = new Bitmap(img);
            int height, width;
            Color clr;
            height = bmp.Height;
            width = bmp.Width;

            int r,g,b;
            for (int heighTrav = 0; heighTrav < height; heighTrav++)
            {
                for (int widthTrav = 0; widthTrav < width; widthTrav++)
                {
                    clr = bmp.GetPixel(widthTrav, heighTrav);
                    r = clr.R;
                    g = clr.G;
                    b = clr.B;

                    Single avg = (r + g + b) / 3;
                    if (avg < (255 / 2))
                        destination.Add(0);
                    else
                        destination.Add(1);
                }
            }
            return destination;

        }

        public void DrawImage(Image img, PictureBox target, bool antialias = false)
        {
            Image fromImg = img;
            

            
            int width = target.Width;
            int height = target.Height;
            Bitmap toBmp = new Bitmap(width, height);
            Image toImg = Image.FromHbitmap(toBmp.GetHbitmap());

            Graphics gr = Graphics.FromImage(toImg);
            if (antialias)
                gr.InterpolationMode =  InterpolationMode.HighQualityBilinear;
            gr.DrawImage(fromImg, 0, 0, width, height);
            target.Image = toImg;
        }

        public Image ShrinkImage(Image img, int newWidth, int newHeight, bool antialias = false)
        {

            Image fromImg = img;
            Bitmap toBm = new Bitmap(newWidth, newHeight);
            Image toImg = Image.FromHbitmap(toBm.GetHbitmap());
            Graphics gr = Graphics.FromImage(toImg);
            if(antialias)
                gr.InterpolationMode = InterpolationMode.HighQualityBilinear;
            gr.DrawImage(fromImg, 0, 0, newWidth, newHeight);
            return toImg;
        }

        public Image ImageFromList(List<object> pattern, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            int counter;
            List<object> input = new List<object>();
            List<object> output = new List<object>();

            height = bmp.Height;
            width = bmp.Width;
            counter = 0;
            for (int heigtTrav = 0; heigtTrav < height; heigtTrav++)
            {
                for (int widthTrav = 0; widthTrav < width; widthTrav++)
                {
                    int colorval = (int)Math.Round((double)pattern[counter]);
                    Color col = new Color();
                    if (colorval == 1)
                        col = Color.White;
                    else
                        col = Color.Black;
                    bmp.SetPixel(widthTrav, heigtTrav, col);
                    counter = counter + 1;
                }
            }
            return Image.FromHbitmap(bmp.GetHbitmap());
        }
    }
}
