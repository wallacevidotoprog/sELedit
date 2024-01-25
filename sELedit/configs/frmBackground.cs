using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace sELedit.configs
{
    class frmBackground : Form
    {
        private readonly Form _parent;
        private int _offsetx;
        private int _offsety;
        public frmBackground(Bitmap bitmap, Form parent, int offsetx = 0, int offsety = 0)
        {
            _parent = parent;
            this.TopMost = _parent.TopMost;
            if ((TopMost))
                _parent.TopMost = true;
            this.ShowInTaskbar = false;
            
            bitmap = new Bitmap(resizeImage(bitmap, parent.Width, parent.Height));
            this.Size = bitmap.Size;
            this.SelectBitmap(bitmap);
            _offsetx = offsetx;
            _offsety = offsety;
            _parent.Shown += Parent_Shown;
            _parent.FormClosing += Parent_FormClosing;
            _parent.Move += Parent_Move;
            _parent.Activated += Parent_Activated;
           // this.Size = parent.Size;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        public  Image resizeImage(Image image, int width, int height)
        {
            var destinationRect = new Rectangle(0, 0, width, height);
            var destinationImage = new Bitmap(width, height);

            destinationImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destinationImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destinationRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return (Image)destinationImage;
        }
        private void Parent_Shown(object sender, EventArgs e)
        {
            FixLocation();
            Show();
            _parent.BringToFront();
        }

        private void Parent_FormClosing(object sender, FormClosingEventArgs e)
        {
            Close();
        }

        private void Parent_Move(object sender, EventArgs e)
        {
            Visible = (_parent.WindowState == FormWindowState.Normal);
            if (Visible)
            {
                FixLocation();
            }
        }

        private void FixLocation()
        {
            Point point = _parent.Location;
            point.Offset(_offsetx, _offsety);
            this.Location = point;
        }

        private void Parent_Activated(object sender, EventArgs e)
        {
            var reorder = ApiHelper.GetWindow(_parent.Handle, ApiHelper.GetWindow_Cmd.GW_HWNDNEXT) != this.Handle;
            if (reorder)
            {
                BringToFront();
                _parent.BringToFront();
            }
        }

        public void SelectBitmap(Bitmap bitmap)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
            {
                throw new ApplicationException("The bitmap must be 32bpp with alpha-channel.");
            }

            IntPtr screenDc = ApiHelper.GetDC(IntPtr.Zero);
            IntPtr memDc = ApiHelper.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr hOldBitmap = IntPtr.Zero;
            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                hOldBitmap = ApiHelper.SelectObject(memDc, hBitmap);
                ApiHelper.ApiSize newApiSize = new ApiHelper.ApiSize(bitmap.Width, bitmap.Height);
                ApiHelper.ApiPoint sourceLocation = new ApiHelper.ApiPoint(0, 0);
                ApiHelper.ApiPoint newLocation = new ApiHelper.ApiPoint(this.Left, this.Top);
                ApiHelper.BLENDFUNCTION blend = new ApiHelper.BLENDFUNCTION();
                blend.BlendOp = ApiHelper.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = 255;
                blend.AlphaFormat = ApiHelper.AC_SRC_ALPHA;
                //inject the alpha image in this Form's graphic layer
                ApiHelper.UpdateLayeredWindow(Handle, screenDc, ref newLocation, ref newApiSize, memDc, ref sourceLocation, 0, ref blend, ApiHelper.ULW_ALPHA);
            }
            finally
            {
                ApiHelper.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    ApiHelper.SelectObject(memDc, hOldBitmap);
                    ApiHelper.DeleteObject(hBitmap);
                }

                ApiHelper.DeleteDC(memDc);
            }
        }

        //mark this Form to be rendered using multiple layers
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                p.ExStyle = p.ExStyle | ApiHelper.WS_EX_LAYERED;
                return p;
            }
        }

    }
}

