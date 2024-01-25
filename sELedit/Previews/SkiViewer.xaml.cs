using eELedit.Previews.Models;
using System.Windows.Controls;

namespace sELedit.Previews
{
    /// <summary>
    /// Interação lógica para SkiViewer.xam
    /// </summary>
    public partial class SkiViewer : UserControl
    {
        public byte[] SKI { get; set; }
        public eELedit.Previews.Models.TexturesFromBytes[] _texturesBytes { get; set; }

        public SkiViewer()
        {
            InitializeComponent();
            if (SKI!= null)
            {
                Prepare();
            }

        }
        //<hvd:HelixViewport3D.Camera>
        //        <!--<PerspectiveCamera LookDirection = " 1, 0.5, 0" UpDirection="0,1,0" />-->
        //        <PerspectiveCamera FarPlaneDistance = "50" LookDirection="10,-2,-3" UpDirection="10,1,10" NearPlaneDistance="0" Position="-5,2,3" FieldOfView="45" />
        //    </hvd:HelixViewport3D.Camera>
        // <hvd:HelixViewport3D Name="Viewport" ShowFrameRate="True" ShowCoordinateSystem="True" CameraRotationMode="Trackball" InfoBackground="{x:Null}">

        public void Prepare()
        {
            SkiReader Ski = new SkiReader(SKI)
            {
                texturesBytes = _texturesBytes
            };
            Model.Content = Ski.GetModel();
            
        }
        
    }   
}
