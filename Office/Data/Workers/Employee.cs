using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.Runtime.Serialization;

namespace Office
{
  /// <summary>
  /// Класс предсавляющий сотрудника компании
  /// </summary>
  [Serializable, DataContract]
  public class Employee : IEmployee, IParametrizable
  {
    private DismissCommand dismiss;

    /// <summary>
    /// Инициализирует экземпляю класса Employee
    /// </summary>
    public Employee()
    {
      Status = new WorkStatus();
      Name = "";
      Surname = "";
      BirthDay = DateTime.Now;
      Number = new PhoneNumber();
      Recruit(new Tuple<DateTime, DateTime>(DateTime.Now, DateTime.Now));
      dismiss = new DismissCommand(this);
    }

    /// <summary>
    /// Инициализирует экземпляю класса Employee
    /// </summary>
    /// <param name="name">Имя</param>
    /// <param name="surname">Фамилия</param>
    /// <param name="birthDay">День рождения</param>
    /// <param name="number">Номер телефона(личный)</param>
    /// <param name="period">Период работы</param>
    public Employee(string name, string surname, DateTime birthDay, PhoneNumber number, Tuple<DateTime, DateTime> period)
    {
      Name = name;
      Surname = surname;
      BirthDay = birthDay;
      Number = number;
      Status = new WorkStatus();
      Recruit(period);
      dismiss = new DismissCommand(this);
    }

    /// <summary>
    /// Имя сотрудника
    /// </summary>
    [DataMember]
    public string Name { get; set; }
    /// <summary>
    /// Фамилия сотрудника
    /// </summary>
    [DataMember]
    public string Surname { get; set; }
    /// <summary>
    /// День рождения сотрудника
    /// </summary>
    [DataMember]
    private DateTime birthDay;
    /// <summary>
    /// День рождения сотрудника
    /// </summary>
    public DateTime BirthDay
    {
      get
      {
        return birthDay;
      }
      set
      {
        if (value > DateTime.Now)
        {
          throw new ArgumentException("Birth day must be less than Now");
        }
        else
        {
          birthDay = value.Date;
        }
      }
    }

    /// <summary>
    /// Номер телефона(личный)
    /// </summary>
    [DataMember]
    public PhoneNumber Number; 

    /// <summary>
    /// Текущий статус и периоды деятельности сотрудника
    /// </summary>
    [DataMember]
    public WorkStatus Status { get; set; }

    /// <summary>
    /// Приём сотрудника на работу
    /// </summary>
    /// <param name="period">Период работы</param>
    public void Recruit(Tuple<DateTime, DateTime> period)
    {
      //Модификация статуса
      Status = new WorkStatus(new Period(period));
    }

    /// <summary>
    /// Увольнение сотрудника или его отмена
    /// </summary>
    /// <param name="isDismiss"></param>
    public void Dismiss(bool isDismiss)
    {
      if (Status == null)
      {
        throw new NullReferenceException("Do Recruit before Dismiss");
      }
      else
      {
        if (isDismiss)
        {
          dismiss.Execute();
        }
        else
        {
          dismiss.Undo();
        }
      }
    }

    /// <summary>
    /// Уход сотрудника в отпуск
    /// </summary>
    /// <param name="period"></param>
    public void GoOnHolyday(Tuple<DateTime, DateTime> period)
    {
      if (Status == null)
      {
        throw new NullReferenceException("Do Recruit before Holiday");
      }
      else
      {
        Status.NowStatus = WorkStatus.Status.Holiday;
        Status.HolidayPeriod = new Period(period);
      }
    }

    /// <summary>
    /// Выполнение обязанностей сотрудника
    /// </summary>
    /// <returns></returns>
    public virtual string Working()
    {
      return "I'm working";
    }

    /// <summary>
    /// Преобразование в строку
    /// </summary>
    /// <returns>Для не заданного сотрудника возвращает название класса, инача - Имя Фамилия</returns>
    public override string ToString()
    {
      if (Name != "")
      {
        return Name + " " + Surname;
      }
      else
      {
        return "Employee";
      }
    }
    
    /// <summary>
    /// Сравнение ключевых полей экземпляров класса
    /// </summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns>Равенство по значению</returns>
    protected bool EqualsHelper(Employee first, Employee second)
    {
      return first.Name == second.Name &&
              first.Surname == second.Surname &&
              first.BirthDay == second.BirthDay;
    }

    /// <summary>
    /// Проверка равенства по значению
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>Равенство по значению</returns>
    public override bool Equals(object obj)
    {
      if ((object)this == obj)
      {
        return true;
      }
      if (this == null)
      {
        return false;
      }
      if (this.GetType() != obj.GetType())
      {
        return false;
      }
      return EqualsHelper(this, (Employee)obj);
    }

    public virtual List<Parameter> GetParam(ControlParameters control)
    {
      return control.GetParam(this);
    }

    public virtual void SetParam(ControlParameters control, List<Parameter> param)
    {
      control.SetParam(param, this);
    }

    /// <summary>
    /// Получение хэш-кода
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
