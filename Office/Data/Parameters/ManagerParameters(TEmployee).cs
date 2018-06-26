using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  public class ManagerParameters<TEmployee> : EmployeeParameters, IParameters<Manager<TEmployee>>
    where TEmployee: Employee, new()
  {
    public List<Parameter> getParameters(Manager<TEmployee> obj)
    {
      if (obj.Name == "")
      {
        obj = new Manager<TEmployee>();
      }
      List<Parameter> paramList = base.getParameters(obj);
      paramList.Add(new Parameter("Department name", Parameter.Types.TextBox, 1).setStartValues(new string[] { obj.DepartmentName }));
      if (obj.SubordinateList == null)
      {
        obj.SubordinateList = new List<TEmployee>();
      }
      paramList.Add(new Parameter("Subordinate", Parameter.Types.CheckBox, 0).setStartValues(obj.SubordinateList.ToList<object>(), typeof(TEmployee)));

      return paramList;
    }

    public void setParameters(List<Parameter> paramList,Manager<TEmployee> prototype)
    {
      if (prototype == null)
      {
        throw new ArgumentNullException("Prototype mustn't be null");
      }
      ParseEmployeeParam(paramList, prototype);

      Parameter param = paramList.Find(i => i.Name == "Subordinate");
      prototype.SubordinateList.Clear();
      foreach (TEmployee sub in param.ObjectReturnValues)
      {
        prototype.AddSubordinate(sub);
      }

      param = paramList.Find(i => i.Name == "Department name");
      if (param != null)
      {
        prototype.DepartmentName = param.ReturnValues[0];
      }
    }
  }
}
