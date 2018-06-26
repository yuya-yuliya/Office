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
  /// Класс представляющий сотрудника профессии Менеджер
  /// </summary>
  /// <typeparam name="TEmployee"></typeparam>
  [Serializable, DataContract]
  public class Manager<TEmployee> : Employee, IParametrizable
    where TEmployee : Employee, new()
  {
    /// <summary>
    /// Инициализация экземпляра объекта Meneger
    /// </summary>
    public Manager()
      : base()
    {
      DepartmentName = "";
      SubordinateList = new List<TEmployee>();
    }

    /// <summary>
    /// Инициализация экземпляра объекта Manager
    /// </summary>
    /// <param name="name">Имя</param>
    /// <param name="surname">Фамилия</param>
    /// <param name="birthDay">День рождения</param>
    /// <param name="number">Номер телефона(личный)</param>
    /// <param name="period">Период работы</param>
    /// <param name="departmentName">Название отдела</param>
    public Manager(string name, string surname, DateTime birthDay, PhoneNumber number, Tuple<DateTime, DateTime> period, string departmentName)
      : base(name, surname, birthDay, number, period)
    {
      DepartmentName = departmentName;
      SubordinateList = new List<TEmployee>();
    }

    /// <summary>
    /// Название отдела работы
    /// </summary>
    [DataMember]
    public string DepartmentName { get; set; }

    /// <summary>
    /// Список подчинённых
    /// </summary>
    [DataMember]
    public List<TEmployee> SubordinateList { get; set; }

    /// <summary>
    /// Добавление сотрудника в список подчинённых
    /// </summary>
    /// <param name="subordinate">Сотрудник</param>
    /// <returns>Успешность выполнения функции</returns>
    public bool AddSubordinate(TEmployee subordinate)
    {
      if (!subordinate.GetType().IsSubclassOf(typeof(TEmployee)) && subordinate.GetType() != typeof(TEmployee))
      {
        throw new ArgumentException(subordinate + " is not an instanse/subclass of " + typeof(TEmployee));
      }
      else
      {
        if (!SubordinateList.Contains(subordinate))
        {
          SubordinateList.Add(subordinate);
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    /// <summary>
    /// Удаление сотрудника из списка подчинённых
    /// </summary>
    /// <param name="subordinate"></param>
    public void DeleteSubordinate(TEmployee subordinate)
    {
      if (SubordinateList.Contains(subordinate))
      {
        SubordinateList.Remove(subordinate);
      }
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
        return "Manager (" + (new TEmployee()).ToString() + ")";
      }
    }
  }
}
