using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text.Json;


namespace magaz
{
    public class JsonProcessor : IDataProcessor<ModernArtifact>
    {
        public List<ModernArtifact> LoadData(string filePath)
        {
            try
            {
                var json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<ModernArtifact>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON data: {ex.Message}");
                return new List<ModernArtifact>();
            }
        }

        public void SaveData(List<ModernArtifact> data, string filePath)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
