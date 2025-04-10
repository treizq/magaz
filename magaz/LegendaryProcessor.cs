using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magaz
{
    public class LegendaryProcessor : IDataProcessor<LegendaryArtifact>
    {
        public List<LegendaryArtifact> LoadData(string filePath)
        {
            var artifacts = new List<LegendaryArtifact>();
            try
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 5)
                    {
                        var artifact = new LegendaryArtifact
                        {
                            Name = parts[0],
                            PowerLevel = int.Parse(parts[1]),
                            Rarity = Enum.Parse<Rarity>(parts[2]),
                            CurseDescription = parts[3],
                            IsCursed = bool.Parse(parts[4])
                        };
                        artifact.Id = Guid.NewGuid();
                        artifacts.Add(artifact);
                    }
                }
                return artifacts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading legendary artifacts: {ex.Message}");
                return artifacts;
            }
            
        }
        public void SaveData(List<LegendaryArtifact> data, string filePath)
        {
            var lines = data.Select(a => $"{a.Name}|{a.PowerLevel}|{a.Rarity}|{a.CurseDescription}|{a.IsCursed}");
            File.WriteAllLines(filePath, lines);
        }
    }
    
}

