using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp8
{
    public class Configuration
    {
        public int NumberOfCities { get; set; }

        public List<City> Cities = new List<City>();

        public string Source { get;set; }

        public string Target { get; set; }

        public int[,] GetGraph()
        {
            int[,] graph = new int[NumberOfCities, NumberOfCities];

            for(int i = 0; i <= NumberOfCities - 1; i++)
            {
                var city = Cities[i];

                for (int j = 0; j <= NumberOfCities - 1; j++)
                {
                    if (i == j)
                    {
                        graph[i, j] = 0;
                        continue;
                    }

                    if (city.CostDictionary.ContainsKey(j + 1))
                    {
                        graph[i, j] = city.CostDictionary[j + 1];
                    }
                }
            }

            //int[,] graph = new int[,] {   { 0, 1, 3, 0 },
            //                              { 1, 0, 1, 4 },
            //                              { 3, 1, 0, 1 },
            //                              { 0, 4, 1, 0 } };

            return graph;
        }

        public int GetCityIndex(string city)
        {
            return Cities.SingleOrDefault(x => x.Name == city).Index;
        }
    }

    public class City
    {
        public int Index { get; set; }

        public string Name { get; set; }

        public int NumberOfNeighbourns { get; set; }

        public Dictionary<int, int> CostDictionary = new Dictionary<int, int>();
    }
}
