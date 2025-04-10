using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text.Json;


namespace magaz
{
    public class AntiqueArtifact : Artifact
    {
        public int Age { get; set; }
        public string OriginRealm {  get; set; }

        public override string Serialize()
        {
            return $"Antique: {Name}, Power: {PowerLevel}, Age: {Age}, Origin: {OriginRealm}";
        }
        public string ExportToJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public string ExportToXml()
        {
            var serializer = new XmlSerializer(typeof(AntiqueArtifact));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, this);
                return writer.ToString();
            }
        }


    }
}
