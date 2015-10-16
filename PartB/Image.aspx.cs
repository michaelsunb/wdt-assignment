using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Routing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace PartB
{
    public partial class Image : System.Web.UI.Page
    {
        public const int sizeMovieThumpnailWidth = 424;
        public const int sizeMovieThumpnailHeight = 424;
        public const int sizeMovieDetailWidth = 526;
        public const int sizeMovieDetailHeight = 773;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string imageFileName = Page.RouteData.Values["image"] as string;
                string loc = Server.MapPath("/Content/Images/" + imageFileName);

                string imageSize = Page.RouteData.Values["size"] as string;

                if (imageSize.Equals(sizeMovieThumpnailWidth + "x" +
                    sizeMovieThumpnailHeight))
                {
                    resizeImage(loc, sizeMovieThumpnailWidth,
                        sizeMovieThumpnailHeight);
                }
                else if (imageSize.Equals(sizeMovieDetailWidth + "x" +
                    sizeMovieDetailHeight))
                {
                    resizeImage(loc, sizeMovieDetailWidth,
                        sizeMovieDetailHeight);
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }

                Response.WriteFile(loc);
                Response.ContentType = "image/jpg";
            }
            catch (FileNotFoundException fnfe)
            {
                Response.Redirect("Default.aspx");
            }
        }
        private void resizeImage(string loc, int canvasWidth, int canvasHeight)
        {
            System.Drawing.Image image =
                System.Drawing.Image.FromFile(loc);

            System.Drawing.Image thumbnail =
                new Bitmap(canvasWidth, canvasHeight); // changed parm names
            System.Drawing.Graphics graphic =
                         System.Drawing.Graphics.FromImage(thumbnail);

            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = CompositingQuality.HighQuality;

            /* ------------------ new code --------------- */
            double originalWidth = (double)image.Width;
            double originalHeight = (double)image.Height;
            // Figure out the ratio
            double ratioX = (double)canvasWidth / originalWidth;
            double ratioY = (double)canvasHeight / originalHeight;
            // use whichever multiplier is smaller
            double ratio = ratioX < ratioY ? ratioX : ratioY;

            // now we can get the new height and width
            int newHeight = Convert.ToInt32(originalHeight * ratio);
            int newWidth = Convert.ToInt32(originalWidth * ratio);

            // Now calculate the X,Y position of the upper-left corner 
            // (one of these will always be zero)
            int posX = Convert.ToInt32((canvasWidth - (originalWidth * ratio)) / 2);
            int posY = Convert.ToInt32((canvasHeight - (originalHeight * ratio)) / 2);

            graphic.Clear(Color.White); // white padding
            graphic.DrawImage(image, posX, posY, newWidth, newHeight);
            /* ------------- end new code ---------------- */

            System.Drawing.Imaging.ImageCodecInfo[] info =
                             ImageCodecInfo.GetImageEncoders();
            EncoderParameters encoderParameters;
            encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality,
                             100L);

            using (MemoryStream ms = new MemoryStream())
            {
                thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.WriteTo(Response.OutputStream);
            }
        }
    }
}