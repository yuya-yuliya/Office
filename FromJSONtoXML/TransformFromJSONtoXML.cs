using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Office.Plugins;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using Office;
using System.Xml.Serialization;

namespace FromJSONtoXML
{
  /// <summary>
  /// Класс, предоставляющий возможность трансформации xml данных в json
  /// </summary>
  public class TransformFromJSONtoXML : ITransformPlugin
  {
    public string Name
    {
      get
      {
        return "transJtX";
      }
    }

    public string Ext
    {
      get
      {
        return "xjson";
      }
    }

    public override string ToString()
    {
      return "Save json transformed to xml";
    }

    /// <summary>
    /// Трансформация json данных в xml
    /// </summary>
    /// <param name="workers">Список сотрудников</param>
    /// <param name="filePath">Файл, в который будут записаны трансформированные json данные</param>
    public void Transform(List<Employee> workers, string filePath)
    {
      try
      {
        string tempFileName = Path.GetTempFileName();
        Serializing.JsonSerialize(workers, tempFileName);
        string json = File.ReadAllText(tempFileName);
        File.Delete(tempFileName);

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
          DateParseHandling = DateParseHandling.None
        };
        XmlDocument doc = JsonConvert.DeserializeXmlNode("{\"Workers\":"+json+"}", "WorkersList");
        doc.Save(filePath);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>
    /// Ретрансформация данных в json
    /// </summary>
    /// <param name="filePath">Файл, содержащий трансформированные json в xml данные</param>
    /// <returns>Список сотрудников</returns>
    public List<Employee> Detransform(string filePath)
    {
      try
      {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(File.ReadAllText(filePath));
        string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
        jsonText = jsonText.Replace("{\"Workers\":", "");
        jsonText = jsonText.Remove(jsonText.Length - 1);

        string tempFileName = Path.GetTempFileName();
        File.WriteAllText(tempFileName, jsonText);
        List<Employee> workers = Serializing.JsonDeserialize(tempFileName);
        File.Delete(tempFileName);
        return workers;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
