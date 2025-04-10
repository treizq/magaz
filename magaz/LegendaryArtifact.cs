using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;

namespace magaz
{
    public class LegendaryArtifact : Artifact
    {
        public string CurseDescription { get; set; }
        public bool IsCursed { get; set; }

        public override string Serialize()
        {
            return $"Legendary: {Name}, Power: {PowerLevel}, Cursed: {IsCursed}, Curse: {CurseDescription}";
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
