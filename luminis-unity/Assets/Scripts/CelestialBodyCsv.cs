using System.IO;
using CsvHelper;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration;

public class CelestialBodyCsvRow {
    public float Ra { get; set; }
    public float Dec { get; set; }
    public float Dist { get; set; }
    public float SigmaDist { get; set; }
    public float DDist { get; set; }
}

public class CelestialBodyCsv {
    private const int DefaultMaxRecords = 100_000;
    private static CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture) {
        PrepareHeaderForMatch = args => args.Header.ToLowerInvariant().Replace("_", ""),
    };

    public static List<CelestialBodyCsvRow> GetRecords(string filename, int maxRecords = DefaultMaxRecords) {
        using (var stream = new StreamReader(filename))
        using (var csv = new CsvReader(stream, config)) {
            return csv.GetRecords<CelestialBodyCsvRow>().Take(maxRecords).ToList();
        }
    }

    public static async Task<List<CelestialBodyCsvRow>> GetRecordsAsync(string filename, int maxRecords = DefaultMaxRecords) {
        using (var stream = new StreamReader(filename))
        using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture)) {
            var records = new List<CelestialBodyCsvRow>();
            await foreach (var record in csv.GetRecordsAsync<CelestialBodyCsvRow>()) {
                if (records.Count >= maxRecords) break;
                records.Add(record);
            }
            return records;
        }
    }
}
