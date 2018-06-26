using Microsoft.Win32;
using Office.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Office
{
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    //WorkersList workers = new WorkersList();
    List<List<Control>> controlList;
    IParametrizable editWorker = null;
    List<ITransformPlugin> plugins = new List<ITransformPlugin>();
    private bool isNew = false;

    public MainWindow()
    {
      InitializeComponent();
      WorkersList.Instance.AddWorker(new Employee("Juliya", "Yukhnevich", new DateTime(1999, 3, 12), new PhoneNumber("375338767543"), new Tuple<DateTime, DateTime>(new DateTime(2016, 09, 1), new DateTime(2020, 05, 31))));
      WorkersList.Instance.AddWorker(new Economist("Seline", "Filla", new DateTime(1980, 12, 15), new PhoneNumber("375256123544"), new Tuple<DateTime, DateTime>(new DateTime(2004, 04, 14), new DateTime(2019, 04, 14)), 14));
      WorkersList.Instance.AddWorker(new Manager<Economist>("Sam", "Gold", new DateTime(1975, 04, 15), new PhoneNumber("375299751284"), new Tuple<DateTime, DateTime>(new DateTime(2000, 07, 13), new DateTime(2020, 07, 13)), "Economic"));
      WorkersList.Instance.AddWorker(new Principal("David", "Hellman", new DateTime(1960, 05, 22), new PhoneNumber("375335674026"), new Tuple<DateTime, DateTime>(new DateTime(2010, 06, 20), new DateTime(2020, 06, 20)), new PhoneNumber("67834")));
      WorkersList.Instance.AddWorker(new ShopAssistant("Freya", "Annum", new DateTime(1992, 11, 01), new PhoneNumber("375297654899"), new Tuple<DateTime, DateTime>(new DateTime(2017, 02, 26), new DateTime(2019, 02, 26)), 1024, "Electronic"));
      WorkersList.Instance.AddWorker(new SupportWorker("Ray", "Darrin", new DateTime(1994, 01, 01), new PhoneNumber("375256730185"), new Tuple<DateTime, DateTime>(new DateTime(2018, 01, 24), new DateTime(2019, 01, 24)), new PhoneNumber("54783"), 42));
      WorkersList.Instance.AddWorker(new SupportWorker("Dan", "Vindor", new DateTime(1995, 12, 01), new PhoneNumber("375332784356"), new Tuple<DateTime, DateTime>(new DateTime(2018, 01, 24), new DateTime(2019, 01, 24)), new Email("dans@support.com"), 43));
      workersLB.ItemsSource = WorkersList.Instance.Workers;
      typeLB.ItemsSource = getExistsTypes();
      typeLB.SelectedIndex = 0;
      editGrid.Visibility = Visibility.Hidden;
      AddPlugins();
    }

    private void AddPlugins()
    {
      string pathPluginFolder = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
      plugins.Clear();
      if (!Directory.Exists(pathPluginFolder))
      {
        Directory.CreateDirectory(pathPluginFolder);
      }

      string[] filesPath = Directory.GetFiles(pathPluginFolder);
      foreach (var file in filesPath)
      {
        Assembly asm = Assembly.LoadFile(file);
        var types = asm.GetTypes().Where(type => type.GetInterfaces().Where(inter => inter.FullName == typeof(ITransformPlugin).FullName).Any());

        foreach (var type in types)
        {
          var plugin = asm.CreateInstance(type.FullName) as ITransformPlugin;
          plugins.Add(plugin); 
        }
      } 

      MenuItem pluginMI = new MenuItem();
      pluginMI.Name = "pluginMI";
      pluginMI.Header = "Transform";
      if (plugins.Count == 0)
      {
        pluginMI.IsEnabled = false;
      }
      else
      {
        foreach (var plug in plugins)
        {
          var subPlugMI = new MenuItem();
          subPlugMI.Name = plug.Name;
          subPlugMI.Header = plug.ToString();
          subPlugMI.Click += plugin_Click;
          pluginMI.Items.Add(subPlugMI);
        }
      }
      fileMI.Items.Add(pluginMI);
    }

    private void plugin_Click(object sender, RoutedEventArgs e)
    {
      ITransformPlugin plugin = plugins.Find(plug => plug.Name == ((MenuItem)sender).Name);

      SaveFileDialog saveFile = new SaveFileDialog();
      saveFile.Title = "Save";
      saveFile.DefaultExt = plugin.Ext;
      saveFile.Filter = "Files|*." + plugin.Ext;
      saveFile.AddExtension = true;
      saveFile.FileName = "transformed";
      if (saveFile.ShowDialog() == true)
      {
        try
        {
          plugin.Transform(WorkersList.Instance.Workers, saveFile.FileName);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error!");
        }
      }

    }

    private List<Employee> getExistsTypes()
    {
      var types = new List<Employee>();
      types.Add(new Employee());
      types.Add(new Economist());
      types.Add(new Principal());
      types.Add(new ShopAssistant());
      types.Add(new SupportWorker());
      types.Add(new Manager<Economist>());
      types.Add(new Manager<SupportWorker>());
      types.Add(new Manager<ShopAssistant>());
      return types;
    }

    private void workersLB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (workersLB.SelectedItem != null)
      {
        editWorker = (IParametrizable)WorkersList.Instance[workersLB.SelectedIndex];
        LoadParameters(paramStack, editWorker.GetParam(new ControlParameters()), editWorker.GetType());
        editGrid.Visibility = Visibility.Visible;
      }
    }

    private void LoadParameters(StackPanel stack, List<Parameter> paramList, Type objType)
    {
      controlList = new List<List<Control>>();
      stack.Children.Clear();
      var objLabel = new Label();
      objLabel.Content = objType.Name;
      stack.Children.Add(objLabel);
      foreach (var param in paramList)
      {
        controlList.Add(new List<Control>());
        StackPanel subStack = new StackPanel();
        subStack.Orientation = Orientation.Horizontal;
        subStack.Margin = new Thickness(10, 5, 10, 5);
        var label = new Label();
        label.Content = param.Name;
        subStack.Children.Add(label);
        for (int i = 0; i < param.CountFields; i++)
        {
          var control = new Control();
          switch (param.Type)
          {
            case Parameter.Types.TextBox:
              var textBox = new TextBox();
              textBox.Text = param.StartValues[i];
              textBox.Width = 100;
              control = textBox;
              break;
            case Parameter.Types.CheckBox:
              var checkBox = new CheckBox();
              checkBox.IsChecked = bool.Parse(param.StartValues[i]);
              checkBox.VerticalAlignment = VerticalAlignment.Center;
              checkBox.Width = 100;
              control = checkBox;
              break;
            case Parameter.Types.Calendar:
              var calendar = new DatePicker();
              calendar.SelectedDate = DateTime.Parse(param.StartValues[i]);
              control = calendar;
              break;
            case Parameter.Types.Label:
              var labelParam = new Label();
              labelParam.Content = param.StartValues[i];
              control = labelParam;
              break;
          }

          control.Margin = new Thickness(5, 2, 5, 2);
          controlList[controlList.Count - 1].Add(control);
          subStack.Children.Add(control);
        }
        if (param.CountFields == 0 && param.Type == Parameter.Types.CheckBox && param.ObjectFlag)
        {
          subStack.Orientation = Orientation.Vertical;
          var wrapPanel = new WrapPanel();
          wrapPanel.Width = 200;
          foreach (var worker in WorkersList.Instance.Workers)
          {
            if ((worker.GetType().IsSubclassOf(param.ObjectType) || worker.GetType() == param.ObjectType) && !worker.Equals(editWorker))
            {
              var checkBox = new CheckBox();
              checkBox.Width = 90;
              checkBox.Content = worker;
              checkBox.Margin = new Thickness(5, 2, 5, 2);
              if (param.ObjectStartValues.Contains(worker))
              {
                checkBox.IsChecked = true;
              }
              else
              {
                checkBox.IsChecked = false;
              }
              controlList[controlList.Count - 1].Add(checkBox);
              wrapPanel.Children.Add(checkBox);
            }
          }
          subStack.Children.Add(wrapPanel);
        }
        stack.Children.Add(subStack);
      }
    }

    private void xmlSave_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveFile = new SaveFileDialog();
      saveFile.AddExtension = false;
      saveFile.Filter = "Xml files|*.xml";
      saveFile.DefaultExt = ".xml";
      saveFile.Title = "Save";
      saveFile.FileName = "new";

      if (saveFile.ShowDialog() == true)
      {
        try
        {
          Serializing.XmlSerialize(WorkersList.Instance.Workers, saveFile.FileName);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error");
        }
      }
    }

    private void binSave_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveFile = new SaveFileDialog();
      saveFile.AddExtension = false;
      saveFile.DefaultExt = ".bin";
      saveFile.Filter = "Bin files|*.bin";
      saveFile.Title = "Save";
      saveFile.FileName = "new";

      if (saveFile.ShowDialog() == true)
      {
        try
        {
          Serializing.BinSerialize(WorkersList.Instance.Workers, saveFile.FileName);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error");
        }
      }
    }

    private void jsonSave_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveFile = new SaveFileDialog();
      saveFile.AddExtension = false;
      saveFile.Filter = "Json files|*.json";
      saveFile.DefaultExt = ".json";
      saveFile.Title = "Save";
      saveFile.FileName = "new";

      if (saveFile.ShowDialog() == true)
      {
        try
        {
          Serializing.JsonSerialize(WorkersList.Instance.Workers, saveFile.FileName);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error");
        }
      }
    }

    private void open_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFile = new OpenFileDialog();
      openFile.Filter = "Bin files|*.bin|Xml files|*.xml|Json files|*.json|All files|*.*";
      openFile.Title = "Open";
      openFile.AddExtension = true;

      if (openFile.ShowDialog() == true)
      {
        try
        {
          string ext = openFile.FileName.Substring(openFile.FileName.LastIndexOf(".") + 1);
          switch (ext)
          {
            case "bin":
              WorkersList.Instance.Workers = Serializing.BinDeserialize(openFile.FileName);
              break;
            case "xml":
              WorkersList.Instance.Workers = Serializing.XmlDeserialize(openFile.FileName);
              break;
            case "json":
              WorkersList.Instance.Workers = Serializing.JsonDeserialize(openFile.FileName);
              break;
            default:
              WorkersList.Instance.Workers = FindExtInPlugins(ext, openFile.FileName);
              break;
          }
          workersLB.ItemsSource = null;
          workersLB.ItemsSource = WorkersList.Instance.Workers;
          editGrid.Visibility = Visibility.Hidden;
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error");
        }
      }
    }

    private List<Employee> FindExtInPlugins(string ext, string fileName)
    {
      int ind = 0;
      if ((ind = plugins.FindIndex(plugin => plugin.Ext == ext)) != -1)
      {
        try
        {
          return plugins[ind].Detransform(fileName);
        }
        catch (Exception ex)
        {
          throw ex;
        }
      }
      else
      {
        throw new Exception("Can't open file");
      }
    }

    private void saveBtn_Click(object sender, RoutedEventArgs e)
    {
      if (((TextBox)controlList[1][0]).Text != "")
      {
        List<Parameter> paramList = editWorker.GetParam(new ControlParameters());
        try
        {
          for (int i = 0; i < paramList.Count; i++)
          {
            for (int j = 0; j < controlList[i].Count; j++)
            {
              switch (paramList[i].Type)
              {
                case Parameter.Types.TextBox:
                  paramList[i].ReturnValues.Add(((TextBox)controlList[i][j]).Text);
                  break;
                case Parameter.Types.Calendar:
                  paramList[i].ReturnValues.Add(((DatePicker)controlList[i][j]).SelectedDate.ToString());
                  break;
                case Parameter.Types.CheckBox:
                  if (paramList[i].ObjectFlag)
                  {
                    if ((bool)((CheckBox)controlList[i][j]).IsChecked)
                    {
                      paramList[i].ObjectReturnValues.Add(((CheckBox)controlList[i][j]).Content);
                    }
                  }
                  else
                  {
                    paramList[i].ReturnValues.Add(((CheckBox)controlList[i][j]).IsChecked.ToString());
                  }
                  break;
              }
            }
          }
          editWorker.SetParam(new ControlParameters(), paramList);
          if (isNew)
          {
            WorkersList.Instance.AddWorker((Employee)editWorker);
          }
          workersLB.ItemsSource = null;
          workersLB.ItemsSource = WorkersList.Instance.Workers;
          editGrid.Visibility = Visibility.Hidden;
          isNew = false;
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error");
        }
      }
      else
      {
        MessageBox.Show("Please, fill field Name");
      }
    }

    private void cancelBtn_Click(object sender, RoutedEventArgs e)
    {
      LoadParameters(paramStack, editWorker.GetParam(new ControlParameters()), editWorker.GetType());
    }

    private void delBtn_Click(object sender, RoutedEventArgs e)
    {
      Employee delWorker = (Employee)workersLB.SelectedItem;
      if (delWorker != null)
      {
        MessageBoxResult res = MessageBox.Show("Do you want to delete " + delWorker + "?", "Delete", MessageBoxButton.YesNo);
        if (res == MessageBoxResult.Yes)
        {
          WorkersList.Instance.DeleteWorker(delWorker);
          workersLB.ItemsSource = null;
          workersLB.ItemsSource = WorkersList.Instance.Workers;
        }
        editGrid.Visibility = Visibility.Hidden;
      }
    }

    private void addBtn_Click(object sender, RoutedEventArgs e)
    {
      isNew = true;
      editWorker = (IParametrizable)typeLB.SelectedItem;
      LoadParameters(paramStack, editWorker.GetParam(new ControlParameters()), editWorker.GetType());
      editGrid.Visibility = Visibility.Visible;
    }
  }
}
