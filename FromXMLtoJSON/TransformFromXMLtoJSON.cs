using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Office.Plugins;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using Office;

namespace FromXMLtoJSON
{
  /// <summary>
  /// Класс, предоставляющий возможность трансформации xml данных в json
  /// </summary>
  public class TransformFromXMLtoJSON : ITransformPlugin
  {
    public string Name
    {
      get
      {
        return "transXtJ";
      }
    }

    public override string ToString()
    {
      return "Save xml transformed to json";
    }

    public string Ext
    {
      get
      {
        return "jxml";
      }
    }

    /// <summary>
    /// Трансформация xml данных в json
    /// </summary>
    /// <param name="workers">Список сотрудников</param>
    /// <param name="filePath">Файл, в который будут записаны трансформированные xml данные</param>
    public void Transform(List<Employee> workers, string filePath)
    {
      string tempFileName = Path.GetTempFileName();
      Serializing.XmlSerialize(workers, tempFileName);
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(File.ReadAllText(tempFileName));
      File.Delete(tempFileName);

      string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);

      File.WriteAllText(filePath, jsonText);
    }


    /// <summary>
    /// Ретрансформация данных в xml
    /// </summary>
    /// <param name="filePath">Файл, содержащий трансформированные xml в json данные</param>
    /// <returns>Список сотрудников</returns>
    public List<Employee> Detransform(string filePath)
    {
      string tempFileName = Path.GetTempFileName();
      string json = File.ReadAllText(filePath);
      XmlDocument doc = JsonConvert.DeserializeXmlNode(json);
      doc.Save(tempFileName);

      List<Employee> workers = Serializing.XmlDeserialize(tempFileName);
      File.Delete(tempFileName);
      return workers;
    }
  }
}
