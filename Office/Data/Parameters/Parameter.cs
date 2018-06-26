using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{ 
  public class Parameter
  {
    public string Name { get; set; }
    public enum Types { CheckBox, TextBox, Calendar, Label}
    public Types Type { get; set; }
    public int CountFields { get; set; }
    public List<string> ReturnValues { get; set; }
    public string[] StartValues { get; set; }
    public List<object> ObjectReturnValues { get; set; }
    public List<object> ObjectStartValues { get; set; }
    public Type ObjectType { get; set; }
    public bool ObjectFlag { get; set; }

    public Parameter(string name, Types type, int countFields)
    {
      Name = name;
      Type = type;
      CountFields = countFields;
      ObjectFlag = false;
      StartValues = new string[0];
      ReturnValues = new List<string>();
      ObjectStartValues = new List<object>();
      ObjectReturnValues = new List<object>();
    }

    public Parameter setStartValues (string[] startValues)
    {
      ObjectFlag = false;
      StartValues = startValues;
      return this;
    }

    public Parameter setStartValues (List<object> objStartValues, Type objType)
    {
      ObjectFlag = true;
      ObjectStartValues = objStartValues;
      ObjectType = objType;
      return this;
    }
  }
}
