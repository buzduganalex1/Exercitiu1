// A C# program for Dijkstra's single 
// source shortest path algorithm. 
// The program is for adjacency matrix 
// representation of the graph 
using ConsoleApp8;
using System.IO;

class Program
{
    public static void CreateFile(string path, string info)
    {
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(info);
            }
        }
    }

    public static Configuration GetConfigurationFromFile(string path)
    {
        string[] lines = System.IO.File.ReadAllLines(path);

        var configuration = new Configuration();

        configuration.NumberOfCities = int.Parse(lines[0]);
        
        var targetAndDestinationString = lines[lines.Length - 1].Split(' ');

        configuration.Source = targetAndDestinationString[0];
        configuration.Target = targetAndDestinationString[1];

        var currentIndex = 1;
        
        while(configuration.Cities.Count < configuration.NumberOfCities)
        {
            var firstCityName = lines[currentIndex];
            var firstCityOfsset = int.Parse(lines[currentIndex + 1]);
            var nextCityIndex = ((currentIndex + 1) + firstCityOfsset) + 1;

            var city = new City()
            {
                Index = configuration.Cities.Count,
                Name = firstCityName,
                NumberOfNeighbourns = firstCityOfsset
            };

            for (int i = currentIndex + 2; i < (currentIndex + 2 + firstCityOfsset); i++)
            {
                var keyAndValue = lines[i].Split(' ');
                var key = int.Parse(keyAndValue[0]);
                var value = int.Parse(keyAndValue[1]);

                city.CostDictionary.Add(key, value);
            }

            currentIndex = nextCityIndex;
            configuration.Cities.Add(city);
        }

        return configuration;
    }

    // Driver Code 
    public static void Main()
    {
        /* Let us create the example  
        graph discussed above */

        // citim din fisier
        // extragem datale
        // creem matricea de mai jos pe baza informatiilor
        //CreateFile();
        var path = Directory.GetCurrentDirectory() + "../../../../Exercitiu1/input.txt";
        var configuration = GetConfigurationFromFile(path);
        var graph = configuration.GetGraph();

        MiminumCostCalculator miminumCostCalculator = new MiminumCostCalculator(4);
        var sourceIndex = configuration.GetCityIndex(configuration.Source);
        var targetIndex = configuration.GetCityIndex(configuration.Target);

        var result = miminumCostCalculator.CalculateMin(graph, sourceIndex, targetIndex);
        
        var outputPath = Directory.GetCurrentDirectory() + "../../../../Exercitiu1/output.txt";

        CreateFile(outputPath, result.ToString());
    }
}