using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace sELedit.configs
{
    // using System.Xml.Serialization;
    // XmlSerializer serializer = new XmlSerializer(typeof(Root));
    // using (StringReader reader = new StringReader(xml))
    // {
    //    var test = (Root)serializer.Deserialize(reader);
    // }

    [XmlRoot(ElementName = "Settings")]
    public class Settings
    {

        [XmlElement(ElementName = "Version")]
        public string Version { get; set; }

        [XmlElement(ElementName = "ElementsDataPath")]
        public string ElementsDataPath { get; set; }

        [XmlElement(ElementName = "TasksDataPath")]
        public string TasksDataPath { get; set; }

        [XmlElement(ElementName = "SurfacesPckPath")]
        public string SurfacesPckPath { get; set; }

        [XmlElement(ElementName = "ConfigsPckPath")]
        public string ConfigsPckPath { get; set; }

		[XmlElement(ElementName = "GshopDataPath")]
		public string GshopDataPath { get; set; }

		[XmlElement(ElementName = "Gshop1DataPath")]
		public string Gshop1DataPath { get; set; }

		[XmlElement(ElementName = "Language")]
        public int Language { get; set; }
    }

   
}
