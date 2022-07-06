using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_App
{
    /// <summary>
    /// Interaction logic for ShowAllSeller.xaml
    /// </summary>
    public partial class ShowAllSeller : Window
    {
        public ShowAllSeller()
        {
            InitializeComponent();
        }
        public static int id;
        public static string name = "";
        public static string address = "";
        public static string city = "";
        public static string country = "";
        public static string phone = "";
        public static string email = "";

        private void GetSellerList()
        {
            var json = File.ReadAllText(@"seller.json");
            var jObject = JObject.Parse(json);
            if (jObject!=null) 
            {
                JArray seller = (JArray)jObject["Sellers"];
                if (seller!=null) 
                {
                    List<Models.Seller> sellers = new List<Models.Seller>();
                    foreach (var item in seller)
                    {
                        sellers.Add(new Models.Seller() { Id=Convert.ToInt32(item["Id"]), Name=item["Name"].ToString(), Address=item["Address"].ToString(), City=item["City"].ToString(), Country=item["Country"].ToString(), Phone=item["Phone"].ToString(), Email=item["Email"].ToString()});
                    }
                    lvSeller.ItemsSource = sellers;
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetSellerList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var itemsJson = File.ReadAllText(@"seller.json");
            try
            {
                var jObject = JObject.Parse(itemsJson);
                JArray itemArr = (JArray)jObject["Seller"];
                Button button = sender as Button;
                Models.Seller itemId = button.CommandParameter as Models.Seller;
                int iId = itemId.Id;
                var itemToDelete = itemArr.FirstOrDefault(obj => obj["Id"].Value<int>() == iId);

                MessageBox.Show("Are you sure to delete this Seller?");
                itemArr.Remove(itemToDelete);

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(@"seller.json", output);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                MessageBox.Show("Data deleted successfully!!!");
                
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Models.Seller sell = b.CommandParameter as Models.Seller;

            id = sell.Id;
            name = sell.Name;
            email = sell.Email;
            phone = sell.Phone;
            address = sell.Address;
            city = sell.City;
            country = sell.Country;

            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();


        }

        private void btnAddNewSeller_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void btnSeller_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
