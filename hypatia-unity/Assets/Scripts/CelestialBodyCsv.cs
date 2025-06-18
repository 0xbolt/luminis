using UnityEngine;
using CsvHelper;

public class CelestialBodyData {
    public int Index { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double W { get; set; }
    public double Ra { get; set; }
    public double Dec { get; set; }
    public double? Redshift { get; set; }
    public double Dist { get; set; }
}

public class CelestialBodyCsv {
    public void GetReader(string filename) {
        Debug.Log(filename);
    }
}
