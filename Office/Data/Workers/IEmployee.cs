using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  /// <summary>
  /// Интерфейс реализации сотрудника
  /// </summary>
  public interface IEmployee
  {
    /// <summary>
    /// Приём на работу сотрудника
    /// </summary>
    /// <param name="date">Период работы</param>
    void Recruit(Tuple<DateTime, DateTime> period);

    /// <summary>
    /// Увольнение или его отмена
    /// </summary>
    /// <param name="isDismiss">Флаг увольнения</param>
    void Dismiss(bool isDismiss);

    /// <summary>
    /// Уход сотрудника в отпуск
    /// </summary>
    /// <param name="period">Период отпуска</param>
    void GoOnHolyday(Tuple<DateTime, DateTime> period);

    /// <summary>
    /// Выполнение обязанностей сотрудника
    /// </summary>
    /// <returns></returns>
    string Working();
  }
}
