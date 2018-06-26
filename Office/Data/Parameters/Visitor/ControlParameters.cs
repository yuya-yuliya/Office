using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  /// <summary>
  /// Класс представляющий возможности для работы с параметрами для взаимодействия с пользователем
  /// </summary>
  public class ControlParameters : IVisitor<Parameter>
  {
    public List<Parameter> GetParam<T>(Manager<T> worker)
      where T : Employee, new()
    {
      return new ManagerParameters<T>().getParameters(worker);
    }

    public List<Parameter> GetParam(ShopAssistant worker)
    {
      return new ShopAssistantParameters().getParameters(worker);
    }

    public List<Parameter> GetParam(SupportWorker worker)
    {
      return new SupportWorkerParameters().getParameters(worker);
    }

    public List<Parameter> GetParam(Principal worker)
    {
      return new PrincipalParameters().getParameters(worker);
    }

    public List<Parameter> GetParam(Economist worker)
    {
      return new EconomistParameters().getParameters(worker);
    }

    public List<Parameter> GetParam(Employee worker)
    {
      return new EmployeeParameters().getParameters(worker);
    }

    public void SetParam(List<Parameter> paramList, Principal prototype)
    {
      new PrincipalParameters().setParameters(paramList, prototype);
    }

    public void SetParam(List<Parameter> paramList, ShopAssistant prototype)
    {
      new ShopAssistantParameters().setParameters(paramList, prototype);
    }

    public void SetParam(List<Parameter> paramList, SupportWorker prototype)
    {
      new SupportWorkerParameters().setParameters(paramList, prototype);
    }

    public void SetParam<T>(List<Parameter> paramList, Manager<T> prototype)
      where T : Employee, new()
    {
      new ManagerParameters<T>().setParameters(paramList, prototype);
    }

    public void SetParam(List<Parameter> paramList, Economist prototype)
    {
      new EconomistParameters().setParameters(paramList, prototype);
    }

    public void SetParam(List<Parameter> paramList, Employee prototype)
    {
      new EmployeeParameters().setParameters(paramList, prototype);
    }
  }
}
