using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace magaz
{
    public class ShopManager
    {
        public List<Artifact> Artifacts { get; } = new List<Artifact>();
        public void LoadAllData(string antiquePath, string modernPath, string legendaryPath)
        {
            var XmlProcessor = new XmlProcessor();
            var JsonProcessor = new JsonProcessor();
            var LegendaryProcessor = new LegendaryProcessor();

            var antiques = XmlProcessor.LoadData(antiquePath);
            var moderns = JsonProcessor.LoadData(modernPath);
            var legenendaries = LegendaryProcessor.LoadData(legendaryPath);

            Artifacts.AddRange(antiques);
            Artifacts.AddRange(moderns);
            Artifacts.AddRange(legenendaries);
        }
        public List<Artifact> FindCursedArtifacts()
        {
            return Artifacts.OfType<LegendaryArtifact>()
                .Where(a => a.IsCursed && a.PowerLevel > 50)
                .Cast<Artifact>()
                .ToList();
        }
        public Dictionary<Rarity, int> GroupByRarity()
        {
            return Artifacts
                .GroupBy(a => a.Rarity)
                .ToDictionary(g => g.Key, g => g.Count());
        }
        public List<Artifact> TopByPower(int count)
        {
            return Artifacts
                .OrderByDescending(a => a.PowerLevel)
                .Take(count)
                .ToList();
        }
        public void GenerateReport(string filePath)
        {
            var report = new StringBuilder();
            report.AppendLine("Отчет о работе магазина артефактов");
            report.AppendLine("====================");
            report.AppendLine($"Всего артефактов: {Artifacts.Count}");

            var avgPowerByRarity = Artifacts
                .GroupBy(a => a.Rarity)
                .Select(g => new { Rarity = g.Key, AvgPower = g.Average(a => a.PowerLevel) });

            foreach (var group in avgPowerByRarity)
            {
                report.AppendLine($"{group.Rarity}: Средний уровень мощности = {group.AvgPower:F1}");
            }

            File.WriteAllText(filePath, report.ToString());
        }

        public void ExportToJson(List<Artifact> artifacts, string filePath)
        {
            var exportable = artifacts.OfType<IExportable>().ToList();
            var json = JsonSerializer.Serialize(exportable.Select(e => e.ExportToJson()), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public void ExportToXml(List<Artifact> artifacts, string filePath)
        {
            var exportable = artifacts.OfType<IExportable>().ToList();
            var xmls = exportable.Select(e => e.ExportToXml()).ToList();

            var serializer = new XmlSerializer(typeof(List<string>));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, xmls);
            }
        }
    }


}

