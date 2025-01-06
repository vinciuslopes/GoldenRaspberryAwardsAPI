using CsvHelper;
using CsvHelper.Configuration;
using GoldenRaspberryAPI.Data;
using GoldenRaspberryAPI.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace GoldenRaspberryAPI.Service
{
    public class CsvReaderService
    {
        public IEnumerable<Movie> ReadCsv(string filePath)
        {
            try
            {
                using var reader = new StreamReader(filePath);

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";", // Configure o delimitador
                    HeaderValidated = null, // Ignorar validação de cabeçalho
                    MissingFieldFound = null // Ignorar campos ausentes
                };

                using var csv = new CsvReader(reader, config);

                csv.Context.RegisterClassMap<MovieMap>(); // Registrar o mapeamento

                return csv.GetRecords<Movie>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao ler o arquivo CSV: {ex.Message}", ex);
            }
        }
    }
}
