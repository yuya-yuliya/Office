using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  public class EmployeeParameters : IParameters<Employee>
  {
    public List<Parameter> getParameters(Employee obj)
    {
      if (obj.Name == "")
      {
        obj = new Employee();
      }

      List<Parameter> paramList = new List<Parameter>();
      paramList.Add(new Parameter("Working", Parameter.Types.Label, 1).setStartValues(new string[] { obj.Working() }));
      paramList.Add(new Parameter("Name", Parameter.Types.TextBox, 1).setStartValues(new string[] { obj.Name }));
      paramList.Add(new Parameter("Surname", Parameter.Types.TextBox, 1).setStartValues(new string[] { obj.Surname }));
      paramList.Add(new Parameter("Birthday", Parameter.Types.Calendar, 1).setStartValues(new string[] { obj.BirthDay.ToString() }));
      paramList.Add(new Parameter("Phone Number", Parameter.Types.TextBox, 1).setStartValues(new string[] { obj.Number.ToString() }));
      if (obj.Status.NowStatus != WorkStatus.Status.Dismiss)
      {
        paramList.Add(new Parameter("Work", Parameter.Types.Calendar, 2).setStartValues(obj.Status.WorkPeriod.ToStringArray()));
      }

      if (obj.Name != "")
      {
        if (obj.Status.NowStatus != WorkStatus.Status.Dismiss)
        {
          paramList.Add(new Parameter("Holiday", Parameter.Types.Calendar, 2).setStartValues(obj.Status.HolidayPeriod.ToStringArray()));
        }
        paramList.Add(new Parameter("Dismiss", Parameter.Types.CheckBox, 1).setStartValues(new string[] { (obj.Status.NowStatus == WorkStatus.Status.Dismiss).ToString() }));
      }

      return paramList;
    }

    public void setParameters(List<Parameter> paramList,Employee prototype)
    {
      if (prototype == null)
      {
        throw new ArgumentNullException("Prototype mustn't be null");
      }
      ParseEmployeeParam(paramList,prototype);
    }

    protected void ParseEmployeeParam(List<Parameter> paramList,Employee em)
    {
      foreach (var param in paramList)
      {
        switch (param.Name)
        {
          case "Name":
            em.Name = param.ReturnValues[0];
            break;
          case "Surname":
            em.Surname = param.ReturnValues[0];
            break;
          case "Birthday":
            em.BirthDay = DateTime.Parse(param.ReturnValues[0]);
            break;
          case "Phone Number":
            em.Number = new PhoneNumber(param.ReturnValues[0]);
            break;
          case "Work":
            em.Recruit(new Tuple<DateTime, DateTime>(DateTime.Parse(param.ReturnValues[0]), DateTime.Parse(param.ReturnValues[1])));
            break;
          case "Holiday":
            if (param.ReturnValues[0] != param.ReturnValues[1])
            {
              em.GoOnHolyday(new Tuple<DateTime, DateTime>(DateTime.Parse(param.ReturnValues[0]), DateTime.Parse(param.ReturnValues[1])));
            }
            break;
          case "Dismiss":
            em.Dismiss(bool.Parse(param.ReturnValues[0]));
            break;
        }
      }
    }
  }
}
