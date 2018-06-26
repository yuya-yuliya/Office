using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  /// <summary>
  /// Интерфейс для работы с параметрами сотрудников
  /// </summary>
  /// <typeparam name="TParam">Тип возвращаемых/передаваемых параметров</typeparam>
  public interface IVisitor<TParam>
  {
    List<TParam> GetParam(Employee worker);
    List<TParam> GetParam(Economist worker);
    List<TParam> GetParam<T>(Manager<T> worker)
      where T : Employee, new();
    List<TParam> GetParam(Principal worker);
    List<TParam> GetParam(ShopAssistant worker);
    List<TParam> GetParam(SupportWorker worker);

    void SetParam(List<Parameter> paramList, Employee prototype);
    void SetParam(List<Parameter> paramList, Economist prototype);
    void SetParam<T>(List<Parameter> paramList, Manager<T> prototype)
      where T : Employee, new();
    void SetParam(List<Parameter> paramList, Principal prototype);
    void SetParam(List<Parameter> paramList, ShopAssistant prototype);
    void SetParam(List<Parameter> paramList, SupportWorker prototype);
  }
}
