using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace magaz
{
    public class XmlProcessor : IDataProcessor<AntiqueArtifact>
    {
        public List<AntiqueArtifact> LoadData(string filePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<AntiqueArtifact>),
                    new XmlRootAttribute("ArrayOfAntiqueArtifact"));
                using (var reader = new StreamReader(filePath))
                {
                    return (List<AntiqueArtifact>)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading XML data: {ex.Message}");
                return new List<AntiqueArtifact>();
            }
        }
        public void SaveData(List<AntiqueArtifact> data, string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<AntiqueArtifact>));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }
        }
}   }