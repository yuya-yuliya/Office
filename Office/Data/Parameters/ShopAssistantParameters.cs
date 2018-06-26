using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  public class ShopAssistantParameters : EmployeeParameters, IParameters<ShopAssistant>
  {
    public List<Parameter> getParameters(ShopAssistant obj)
    {
      if (obj.Name == "")
      {
        obj = new ShopAssistant();
      }
      List<Parameter> paramList = base.getParameters(obj);
      paramList.Add(new Parameter("Department", Parameter.Types.TextBox, 1).setStartValues(new string[] { obj.Department }));
      paramList.Add(new Parameter("Personal ID", Parameter.Types.TextBox, 1).setStartValues(new string[] { obj.PersonalId.ToString() }));

      return paramList;
    }

    public void setParameters(List<Parameter> paramList,ShopAssistant prototype)
    {
      if (prototype == null)
      {
        throw new ArgumentNullException("Prototype mustn't be null");
      }
      ParseEmployeeParam(paramList, prototype);
      Parameter param = paramList.Find(i => i.Name == "Department");
      if (param != null)
      {
        prototype.Department = param.ReturnValues[0];
      }
      param = paramList.Find(i => i.Name == "Personal ID");
      if (param != null)
      {
        prototype.PersonalId = uint.Parse(param.ReturnValues[0]);
      }
    }
  }
}
