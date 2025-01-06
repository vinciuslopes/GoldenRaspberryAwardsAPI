using CsvHelper.Configuration;
using GoldenRaspberryAPI.Model;

namespace GoldenRaspberryAPI.Data
{
    public class MovieMap : ClassMap<Movie>
    {
        public MovieMap()
        {
            Map(m => m.Year).Name("year");
            Map(m => m.Title).Name("title");
            Map(m => m.Studios).Name("studios");
            Map(m => m.Producers).Name("producers");
            Map(m => m.Winner)
                .Name("winner")
                .Convert(row => row.Row.GetField("winner")?.Trim().ToLower() == "yes"); // Converte "yes" para true
        }
    }
}
