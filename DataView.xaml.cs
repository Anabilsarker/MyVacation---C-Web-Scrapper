using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

namespace MyVacation
{
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class DataView : Window
    {
        ObservableCollection<Database> datas;
        Database[] result;
        public DataView()
        {
            InitializeComponent();
            datas = new ObservableCollection<Database>();
            griddata.ItemsSource = datas;
            GetJsonData();

        }
        public void GetJsonData()
        {
            using (StreamReader file = new StreamReader("Airline.json"))
            {
                result = JsonConvert.DeserializeObject<Database[]>(file.ReadToEnd());
            }
            UpdateGrid();
        }
        private void UpdateGrid()
        {
            datas.Clear();
            foreach (var res in result)
                datas.Add(res);
        }

        private void RedirectToLink(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.ToString());
        }
    }
}
