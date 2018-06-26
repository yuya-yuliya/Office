using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Office
{
  /// <summary>
  /// Класс представляющий рабочий статус
  /// </summary>
  [Serializable, DataContract]
  public class WorkStatus
  {
    /// <summary>
    /// Перечисление доступных статусов
    /// </summary>
    public enum Status {
      [XmlEnum("Wor")]
      Work,
      [XmlEnum("Hol")]
      Holiday,
      [XmlEnum("Dis")]
      Dismiss }

    /// <summary>
    /// Инициализация экземпляра класса WorkStatus
    /// </summary>
    public WorkStatus()
    {
      NowStatus = Status.Work;
      WorkPeriod = new Period();
      HolidayPeriod = new Period();
    }

    /// <summary>
    /// Инициализация экземпляра класса WorkStatus
    /// </summary>
    /// <param name="period">Период работы</param>
    public WorkStatus(Period period)
    {
      NowStatus = Status.Work;
      WorkPeriod = period;
      HolidayPeriod = new Period();
    }

    /// <summary>
    /// Текущий статус
    /// </summary>
    [DataMember]
    public Status NowStatus { get; set; }

    /// <summary>
    /// Период работы
    /// </summary>
    [DataMember]
    public Period WorkPeriod { get; set; }

    /// <summary>
    /// Период отпуска
    /// </summary>
    [DataMember]
    public Period HolidayPeriod { get; set; }
  }
}
