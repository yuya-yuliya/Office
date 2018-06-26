using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace Office
{
  /// <summary>
  /// Класс представляющий сотрудника профессии Экономист
  /// </summary>
  [Serializable, DataContract]
  public class Economist : Employee, IParametrizable
  {
    /// <summary>
    /// Номер офиса сотрудника
    /// </summary>
    [DataMember]
    public uint OfficeNumber { get; set; }

    /// <summary>
    /// Инициализирует экземпляр класса Economist
    /// </summary>
    public Economist()
      : base()
    {
      OfficeNumber = 0;
    }

    /// <summary>
    /// Инициализирует экземпляр класса Economist
    /// </summary>
    /// <param name="name">Имя</param>
    /// <param name="surname">Фамилия</param>
    /// <param name="birthDay">День рождения</param>
    /// <param name="number">Номер телефона(личный)</param>
    /// <param name="period">Период работы</param>
    /// <param name="officeNumber">Номер офиса</param>
    public Economist(string name, string surname, DateTime birthDay, PhoneNumber number, Tuple<DateTime, DateTime> period, uint officeNumber) 
      : base(name, surname, birthDay, number, period)
    {
      OfficeNumber = officeNumber;
    }

    public override List<Parameter> GetParam(ControlParameters control)
    {
      return control.GetParam(this);
    }

    public override void SetParam(ControlParameters control, List<Parameter> param)
    {
      control.SetParam(param, this);
    }

    /// <summary>
    /// Преобразование в строку
    /// </summary>
    /// <returns>Для не заданного сотрудника возвращает название класса, инача - Имя Фамилия</returns>
    public override string ToString()
    {
      if (Name != "")
      {
        return base.ToString();
      }
      else
      {
        return "Economist";
      }
    }
  }
}
