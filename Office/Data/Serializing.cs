using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Office
{
  public class Serializing
  {
    private static Type[] extraTypes = { typeof(Employee), typeof(Economist), typeof(Manager<Economist>),
                              typeof(Manager<ShopAssistant>), typeof(Manager<SupportWorker>),
                              typeof(Principal), typeof(ShopAssistant), typeof(SupportWorker) };

    public static void BinSerialize(List<Employee> workers, string fileName)
    {
      BinaryFormatter formatter = new BinaryFormatter();
      using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
      {
        formatter.Serialize(fs, workers);
      }
    }

    public static void XmlSerialize(List<Employee> workers, string fileName)
    {
      XmlSerializer formatter = new XmlSerializer(typeof(List<Employee>), extraTypes);
      using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
      {
        formatter.Serialize(fs, workers);
      }
    }

    public static void JsonSerialize(List<Employee> workers, string fileName)
    {
      DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(List<Employee>), extraTypes);
      using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
      {
        formatter.WriteObject(fs, workers);
      }
    }

    public static List<Employee> BinDeserialize(string fileName)
    {
      var workers = new List<Employee>();
      BinaryFormatter formatterBin = new BinaryFormatter();
      using (FileStream fs = new FileStream(fileName, FileMode.Open))
      {
        workers = (List<Employee>)formatterBin.Deserialize(fs);
      }
      return workers;
    }

    public static List<Employee> XmlDeserialize(string fileName)
    {
      var workers = new List<Employee>();
      XmlSerializer formatterXml = new XmlSerializer(typeof(List<Employee>), extraTypes);
      using (FileStream fs = new FileStream(fileName, FileMode.Open))
      {
        workers = (List<Employee>)formatterXml.Deserialize(fs);
      }
      return workers;
    }

    public static List<Employee> JsonDeserialize(string fileName)
    {
      var workers = new List<Employee>();
      DataContractJsonSerializer formatterJson = new DataContractJsonSerializer(typeof(List<Employee>), extraTypes);
      using (FileStream fs = new FileStream(fileName, FileMode.Open))
      {
        workers = (List<Employee>)formatterJson.ReadObject(fs);
      }
      return workers;
    }
  }
}
