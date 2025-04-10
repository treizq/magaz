using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Serialization;


namespace magaz
{
    public class ModernArtifact : Artifact
    {
        public double TechLevel { get; set; }
        public string Manufacturer { get; set; }

        public override string Serialize()
        {
            return $"Modern: {Name}, Power: {PowerLevel}, Tech: {TechLevel}, Made by: {Manufacturer}";
        }
        public string ExportToJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public string ExportToXml()
        {
            var serializer = new XmlSerializer(typeof(ModernArtifact));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, this);
                return writer.ToString();
            }
        }
    }
}
