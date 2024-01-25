using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace sELedit.NOVO
{
    /// <summary>
    /// Draw a progress image from two images.
    /// </summary>
    /// <remarks>
    /// To use this control, add it to your form.
    /// Set the Max and Min properties to a sensible value for your task
    /// Set the ImageCompleted and ImageUncompleted images to the appropriate
    /// pictures. For example, set the Completed image to a full colour picture,
    /// and the Uncompleted image to a monochrome version.
    /// As your task progesses, set the Value proprty to the new status.
    /// Assuming Max is 100, Min is 0, and teh images are set as above, a
    /// gradual change of Value from 0 to 100 will convert the Monochrome 
    /// picture to full colour, showing the progress.
    /// </remarks>
    public partial class ProgressImage : UserControl
    {
        #region Constants
        #endregion
        #region Fields
        #region Internal Fields
        /// <summary>
        /// Internal, completed image at the size of the whole control.
        /// This saves having to re-size it in the paint event.
        /// </summary>
        private Bitmap bComp;
        /// <summary>
        /// Internal, uncompleted image at the size of the whole control.
        /// This saves having to re-size it in the paint event.
        /// </summary>
        private Bitmap bUncomp;
        #endregion
        #region Property Bases
        /// <summary>
        /// Internal, minimum value for progress
        /// </summary>
        private int min = 0;
        /// <summary>
        /// Internal, current value of progress
        /// </summary>
        private int value = 50;
        /// <summary>
        /// Internal, maximum value for progress
        /// </summary>
        private int max = 100;
        /// <summary>
        /// Internal, image to display in completed part of display
        /// </summary>
        private Image imageCompleted = null;
        /// <summary>
        /// Internal, image to display in completed part of display
        /// </summary>
        private Image imageUncompleted = null;
        #endregion
        #endregion
        #region Properties
        #region Visible Properties
        /// <summary>
        /// Minimum value for progress
        /// </summary>
        [Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        Description("Minimum value for progress"),
        Category("Progress")]
        public int Min
        {
            get { return min; }
            set
            {
                min = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Current value of progress
        /// </summary>
        [Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        Description("Current value of progress"),
        Category("Progress")]
        public int Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Maximum value for progress
        /// </summary>
        [Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        Description("Maximum value for progress"),
        Category("Progress")]
        public int Max
        {
            get { return max; }
            set
            {
                max = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Image to display in completed part of display
        /// </summary>
        [Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        Description("Image to display in completed part of progress"),
        Category("Display")]
        public Image ImageCompleted
        {
            get { return imageCompleted; }
            set
            {
                imageCompleted = value;
                BuildImages();
                Invalidate();
            }
        }
        /// <summary>
        /// Image to display in uncompleted part of display
        /// </summary>
        [Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        Description("Image to display in uncompleted part of progress"),
        Category("Display")]
        public Image ImageUncompleted
        {
            get { return imageUncompleted; }
            set
            {
                imageUncompleted = value;
                BuildImages();
                Invalidate();
            }
        }
        #endregion
        #region Hide these properties
        /// <summary>
        /// Hidden property, inherited from UserControl but irrelevant
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AllowDrop { get; set; }
        /// <summary>
        /// Hidden property, inherited from UserControl but irrelevant
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new AutoScaleMode AutoScaleMode { get; set; }
        /// <summary>
        /// Hidden property, inherited from UserControl but irrelevant
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AutoScroll { get; set; }
        /// <summary>
        /// Hidden property, inherited from UserControl but irrelevant
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new Size AutoScrollMargin { get; set; }
        /// <summary>
        /// Hidden property, inherited from UserControl but irrelevant
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new Size AutoScrollMinSize { get; set; }
        /// <summary>
        /// Hidden property, inherited from UserControl but irrelevant
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AutoSize { get; set; }
        /// <summary>
        /// Hidden property, inherited from UserControl but irrelevant
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new AutoSizeMode AutoSizeMode { get; set; }
        /// <summary>
        /// Hidden property, inherited from UserControl but irrelevant
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override AutoValidate AutoValidate { get; set; }
        /// <summary>
        /// Hidden property, inherited from UserControl but irrelevant
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override ImageLayout BackgroundImageLayout { get; set; }
        /// <summary>
        /// Hidden property, inherited from UserControl but irrelevant
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override ContextMenuStrip ContextMenuStrip { get; set; }
        /// <summary>
        /// Hidden property, inherited from UserControl but irrelevant
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override RightToLeft RightToLeft { get; set; }
        #endregion
        #endregion
        #region Regular Expressions
        #endregion
        #region Enums
        #endregion
        #region Constructors
        /// <summary>
        /// Construct a ProgressImage instance
        /// </summary>
        public ProgressImage()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BuildImages();
        }
        #endregion
        #region Events
        #region Event Constructors
        #endregion
        #region Event Handlers
        /// <summary>
        /// Draw the progress bar images.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressImage_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            DoHorizontalImageReveal(graphics);
        }
        /// <summary>
        /// Resize control: rebuild the base images
        /// This saves time in the paint event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressImage_Resize(object sender, EventArgs e)
        {
            BuildImages();
        }
        #endregion
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        /// <summary>
        /// Draw an image as the bar component
        /// Reveal an image with the current value.
        /// I.e. If the uncompleted image is a monochrome version of the 
        /// completed image, then as the value increases, the progress bar 
        /// would fill with colour, but the images would not appear to move.
        /// </summary>
        /// <param name="graphics"></param>
        private void DoHorizontalImageReveal(Graphics graphics)
        {
            int border = (Width * value) / (max - min);
            int remain = Width - border;
            if (bComp != null)
            {
                // Completed part
                // Can use unscaled and clipped - it is quicker.
                graphics.DrawImageUnscaledAndClipped(bComp, new Rectangle(0, 0, border, Height));
            }
            if (bUncomp != null)
            {
                // Uncompleted part
                // Can't use unscaled as need to offset into image.
                graphics.DrawImage(bUncomp, new Rectangle(border, 0, remain, Height), new Rectangle(border, 0, remain, Height), GraphicsUnit.Pixel);
            }
        }
        /// <summary>
        /// Build the complete and incomplete images
        /// </summary>
        private void BuildImages()
        {
            bComp = BuildImage(imageCompleted, Width, Height);
            bUncomp = BuildImage(imageUncompleted, Width, Height);
        }
        /// <summary>
        /// Build an image at the correct size
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Bitmap BuildImage(Image image, int width, int height)
        {
            if (image == null)
            {
                return null;
            }
            return new Bitmap(image, width, height);
        }
        #endregion
    }
}