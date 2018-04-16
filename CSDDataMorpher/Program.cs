using DelimitedStringParser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDDataMorpher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            BlobConnector connector = new BlobConnector("DefaultEndpointsProtocol=https;AccountName=csdprediction;AccountKey=Hf+YK+zFgZbxucPOOkhpKsJ5pPb6yNykHhcr2qwJx7NLQnhnUwoxZ7tVXQgMIhcF8UJdvpt6Y6OUS562pmTqSw==;EndpointSuffix=core.windows.net","csd-raw");
            var fileValue = connector.Download("https://csdprediction.blob.core.windows.net/csd-raw/AprilMayJune_2016/000000_0", "AprilMayJune_2016/000000_0");
            var lines = fileValue.Split('\n');
            List<CSDRawModel> allFileData = new List<CSDRawModel>();
            foreach (var line in lines)
            {
                CSDRawModel obj = DelimitedStringParser<CSDRawModel>.Parse(line);
                allFileData.Add(obj);
            }

            var groupedData = allFileData.GroupBy(x => x.AssetId).Select(y => y.ToList().OrderBy(z=>z.TimeStamp)).ToList();
        }
    }
}
