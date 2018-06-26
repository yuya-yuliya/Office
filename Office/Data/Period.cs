using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace Office
{
  /// <summary>
  /// Класс представляющий период
  /// </summary>
  [Serializable, DataContract]
  public class Period
  {
    /// <summary>
    /// Инициализация экземпляра класса Period
    /// </summary>
    public Period()
    {
      from = DateTime.MaxValue.ToUniversalTime().Date;
      to = DateTime.MaxValue.ToUniversalTime().Date;
    }

    /// <summary>
    /// Инициализация экземпляра класса Prtiod
    /// </summary>
    /// <param name="period">Границы периода</param>
    public Period(Tuple<DateTime, DateTime> period)
    {
      from = period.Item1.Date;
      To = period.Item2.Date;
    }

    [DataMember]
    private DateTime from;
    /// <summary>
    /// Начало периода
    /// </summary>
    public DateTime From
    {
      get
      {
        return from;
      }
      set
      {
        if (value > To)
        {
          throw new ArgumentException("Date From shoud be less than date To");
        }
        else
        {
          from = value.Date;
        }
      }
    }

    [DataMember]
    private DateTime to;
    /// <summary>
    /// Окончание периода
    /// </summary>
    public DateTime To
    {
      get
      {
        return to;
      }
      set
      {
        if (value < From)
        { 
          throw new ArgumentException("Date To shoud be greater than date From");
        }
        else
        {
          to = value.Date;
        }
      }
    }

    /// <summary>
    /// Преобразование в массив строк
    /// </summary>
    /// <returns></returns>
    public string[] ToStringArray()
    {
      return new string[] { From.ToString(), To.ToString() };
    }
  }
}
