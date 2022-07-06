using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace WPF_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<string> city = new List<string>()
            {
                "Dhaka",
                "Chattagram",
                "Khulna",
                "Barishal"
            };
            cmbSellCity.ItemsSource = city;
        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                itemPic.Source = new BitmapImage(new Uri(ofd.FileName));
                //var tmpFileName = DateTime.Now.Minute + "-" + DateTime.Now.Second + Path.GetExtension(ofd.FileName);
                var tmpFileName = Guid.NewGuid() + Path.GetExtension(ofd.FileName);
                //txtFilePath.Text = tmpFileName;
                var imagePath = System.IO.Path.Combine(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"../..Assets\Images\") + tmpFileName);
                //File.Copy(ofd.FileName, imagePath);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Seller s = new Models.Seller();
                s.Id = Convert.ToInt32(txtSellId.Text);
                s.Name = txtSellName.Text;
                s.Address = txtSellAddress.Text;
                s.City = cmbSellCity.Text;
                s.Country = txtSellCountry.Text;
                s.Phone = txtPhone.Text;
                s.Email = txtSellEmail.Text;
                
                var newSeller = "{'Id':'" + s.Id + "','Name':'" + s.Name + "','Address':'" + s.Address + "','City':'" + s.City + "','Country':'" + s.Country + "','Phone':'" + s.Phone + "','Email':'" + s.Email + "'}";

                var sellerJson = File.ReadAllText(@"seller.json");
                var jsonObject = JObject.Parse(sellerJson);
                var itemArr = jsonObject.GetValue("Seller") as JArray;
                var seller = JObject.Parse(newSeller);
                itemArr.Add(seller);

                jsonObject["Seller"] = itemArr;
                string jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(@"seller.json", jsonResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex.Message.ToString());
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void btnAddNewOrder_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnSeeSellerList_Click(object sender, RoutedEventArgs e)
        {
            ShowAllSeller showAllSeller = new ShowAllSeller();
            showAllSeller.Show();
            this.Close();
        }
    }
}
