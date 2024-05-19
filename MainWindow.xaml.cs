using System;
using System.Windows;

namespace MyVacation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Automation automation;
        public MainWindow()
        {
            InitializeComponent();
        }
        public static bool isHeadless { get; set; }

        public static string From { get; set; }
        public static string To { get; set; }
        public static string dDay { get; set; }
        public static string dMonth { get; set; }
        public static string dYear { get; set; }
        public static string rDay { get; set; }
        public static string rMonth { get; set; }
        public static string rYear { get; set; }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                From = from.Text;
                To = to.Text;
                isHeadless = (bool)headless.IsChecked;

                if (double.IsNaN(int.Parse(dday.Text)) && double.IsNaN(int.Parse(dday.Text)) && double.IsNaN(int.Parse(dday.Text)) && double.IsNaN(int.Parse(dday.Text)) && double.IsNaN(int.Parse(dday.Text)) && double.IsNaN(int.Parse(dday.Text)))
                {
                    MessageBox.Show("Enter a Valid Date");
                }
                else
                {
                    dDay = dday.Text;
                    dMonth = dmonth.Text;
                    dYear = dyear.Text;

                    rDay = rday.Text;
                    rMonth = rmonth.Text;
                    rYear = ryear.Text;

                    automation = new Automation();
                    automation.Run();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
