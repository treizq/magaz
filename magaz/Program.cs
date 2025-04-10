using magaz;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
class Program
{
    static void Main(string[] args)
    {
        var shopManager = new ShopManager();

        // Загрузка данных (пути к файлам нужно заменить на актуальные)
        shopManager.LoadAllData("antique.xml", "modern.json", "legends.txt");

        Console.WriteLine("Добро пожаловать в Магический магазин артефактов");
        Console.WriteLine($"Загружены {shopManager.Artifacts.Count} артефакты.");

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Найдите проклятые артефакты (Мощность > 50)");
            Console.WriteLine("2. Группируйте артефакты по степени редкости");
            Console.WriteLine("3. Получите лучшие артефакты по силе");
            Console.WriteLine("4. Создайте отчет");
            Console.WriteLine("5. Экспорт проклятых артефактов в JSON");
            Console.WriteLine("6. Экспорт всех артефактов в XML");
            Console.WriteLine("7. Выход");

            Console.Write("Выберите вариант: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var cursed = shopManager.FindCursedArtifacts();
                    Console.WriteLine($"Найдено {cursed.Count} проклятых артефактов:");
                    foreach (var artifact in cursed)
                    {
                        Console.WriteLine(artifact.Serialize());
                    }
                    break;

                case "2":
                    var groups = shopManager.GroupByRarity();
                    foreach (var group in groups)
                    {
                        Console.WriteLine($"{group.Key}: {group.Value} аретфактов");
                    }
                    break;

                case "3":
                    Console.Write("Введите количество лучших артефактов для показа: ");
                    if (int.TryParse(Console.ReadLine(), out int count))
                    {
                        var top = shopManager.TopByPower(count);
                        Console.WriteLine($"Лучшие {count} артефакты по силе:");
                        foreach (var artifact in top)
                        {
                            Console.WriteLine(artifact.Serialize());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Недопустимый ввод");
                    }
                    break;

                case "4":
                    shopManager.GenerateReport("report.txt");
                    Console.WriteLine("Отчет создан для report.txt");
                    break;

                case "5":
                    var cursedToExport = shopManager.FindCursedArtifacts();
                    shopManager.ExportToJson(cursedToExport, "cursed.json");
                    Console.WriteLine("Проклятые артефакты, экспортируемые в cursed.json");
                    break;

                case "6":
                    shopManager.ExportToXml(shopManager.Artifacts, "all_artifacts.xml");
                    Console.WriteLine("Все артефакты, экспортированные в all_artifacts.xml");
                    break;

                case "7":
                    return;

                default:
                    Console.WriteLine("Недопустимый вариант");
                    break;
            }
        }
    }
}
