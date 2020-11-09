/*Name:Ashok Sasitharan
 * Student ID: 100745484
 * Date: November 2 2020
 * NETD 3202 Lab 3
 */
using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace ASasitharan_NETD3202_Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<shares> list = new List<shares>();
        public MainWindow()
        {
            InitializeComponent();
            lstShares.ItemsSource = list;
            SetSummary();
            FillDataGrid();
            Revenue();
        }

        //This function fills the data grid with data from the table
        public void FillDataGrid()
        {
            try
            {
                //connect to the database
                string connectString = Properties.Settings.Default.connect_string;
                SqlConnection conn = new SqlConnection(connectString);
                conn.Open();
                //creata a query statement
                string selectionQuery = "SELECT * FROM ShareInfo";
                //create a new command
                SqlCommand command = new SqlCommand(selectionQuery, conn);
                //use a data adapter
                SqlDataAdapter sda = new SqlDataAdapter(command);
                //link the datatable with the equipment table
                DataTable dt = new DataTable("ShareInfo");
                sda.Fill(dt);
                //display the data from the table
                shareGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        public void SetSummary()
        {
            //This block searches through the ShareInfo table print out the number of common shares sold
            string cs = Properties.Settings.Default.connect_string;
            SqlConnection cn = new SqlConnection(cs);
            cn.Open();
            string selectQuery = "SELECT SUM(shares) FROM ShareInfo WHERE shareType = \'Common\'";
            SqlCommand selectCommand = new SqlCommand(selectQuery, cn);
            
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                reader.Read();
              //checks if the table has null values
                if (reader.IsDBNull(0))
                {
                    txtNumCommonSold.Text = "0";
                }
                //run if table does not have null values and print out the value to textbox
                else
                {
                  txtNumCommonSold.Text = reader.GetInt32(0).ToString();
                    
                }
            }
            //This block searches through the ShareInfo table print out the number of preferred shares sold
             selectQuery = "SELECT SUM(shares) FROM ShareInfo WHERE shareType = \'Preferred\'";
             selectCommand = new SqlCommand(selectQuery, cn);
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                    reader.Read();
                //checks if the table has null values
                if (reader.IsDBNull(0))
                {
                    txtNumPreferredSold.Text = "0";
                }
                //run if table does not have null values and print out the value to textbox
                else
                {
                    txtNumPreferredSold.Text = reader.GetInt32(0).ToString();
                }
      
            }
            //This block searces through the NumofShares table and prints out the number of shares available values to the textbox
            selectQuery = "SELECT * FROM NumofShares";
            selectCommand = new SqlCommand(selectQuery, cn);
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                reader.Read();
               txtCommonSharesAvailable.Text= reader.GetInt32(0).ToString();
                txtPreferredSharesAvailable.Text = reader.GetInt32(1).ToString();
            }
            //close the connection
            cn.Close();

        }
  
        //This function calculates the total revenue
        public void Revenue()
        {
           
            int totalRevenue = 0;
            int day=0;
            int range = 0;
            DateTime purchasedDate;
           
            //connect to the database
            string cs = Properties.Settings.Default.connect_string;
            SqlConnection cn = new SqlConnection(cs);
            cn.Open();
            //create the select statement
            string selectQuery = "SELECT shares, datePurchased, shareType FROM ShareInfo";
            //create a new command
            SqlCommand selectCommand = new SqlCommand(selectQuery, cn);
            //use a data adapter
            SqlDataAdapter sda = new SqlDataAdapter(selectCommand);
            //link the datatable 
            DataTable dt = new DataTable("Revenue");
            sda.Fill(dt);
            //loop through the row in the datatable
            foreach(DataRow row in dt.Rows)
            {
                //set the purchased date from the table
                purchasedDate = DateTime.Parse(row["datePurchased"].ToString());
                //subtract the purchased date with a default date
                day = Convert.ToInt32((purchasedDate - new DateTime(1990, 1, 1)).TotalDays);
                Random rnd = new Random(day);
                //run if the share is preferred
                if(row["shareType"].ToString()=="Preferred")
                {
                    //set the range for the random number
                    range = rnd.Next(50, 100);
                }
                //run if the share is common
                else if(row["shareType"].ToString() == "Common")
                {
                    //set the range for the random number
                    range = rnd.Next(1, 50);
                }
                //create the total Revenue which is the amount of shares multiplied by its random range
                totalRevenue += int.Parse(row["shares"].ToString()) * range;
            }
            //print out the total revenue to the textbox
            txtRevenue.Text = totalRevenue.ToString();

        }
        //this function sets the form to its default state.
        public void SetDefaults()
        {
            txtBuyerName.Text = "";
            txtNumOfShares.Text = "";
            dpDatePurchased.Text = null;
            rbCommon.IsChecked=true;
            txtBuyerName.Focus();
        }
        private void btnCreateEntry_Click(object sender, RoutedEventArgs e)
        {
            int numOfShares = 0;
            int numCommonShares = 0;
            int numPreferredShares = 0;
            string radiobtn = "";
            int availableShares = 0;
            int price = 0;
            try
            {
                //run is the buyer name is not empty
                if (txtBuyerName.Text != string.Empty)
                {
                    //run if the number of shares is not empty
                    if (txtNumOfShares.Text != string.Empty)
                    {
                        //run is the date picker is not empty
                        if (dpDatePurchased.Text != string.Empty)
                        {
                            //run is the number of shares is a integer
                            if (int.TryParse(txtNumOfShares.Text, out numOfShares))
                            {
                                //connect to the database
                                string cs = Properties.Settings.Default.connect_string;
                                SqlConnection cn = new SqlConnection(cs);
                                cn.Open();
                                //creata a query statement
                                string selectQueryCommon = "SELECT numCommonShares From NumofShares";
                                SqlCommand selectCommand = new SqlCommand(selectQueryCommon, cn);
                                //read through table and pull the first row
                                using (SqlDataReader reader = selectCommand.ExecuteReader())
                                {
                                    reader.Read();
                                    numCommonShares = reader.GetInt32(0);
                                }
                                //this block gets the amount of preferred shares
                                string selectQueryPreferred = "SELECT numPreferredShares From NumofShares";
                                SqlCommand selectCommandPreferred = new SqlCommand(selectQueryPreferred, cn);
                                using (SqlDataReader reader = selectCommandPreferred.ExecuteReader())
                                {
                                    reader.Read();
                                    numPreferredShares = reader.GetInt32(0);
                                }
                                cn.Close();

                                //run if the amount of common shares or preferred shares is greater than or equal to the amount the user enters
                                if (numCommonShares >= numOfShares && numPreferredShares >= numOfShares)
                                {
                                    //run if the common radio button is checked
                                    if (rbCommon.IsChecked == true)
                                    {
                                      
                                        radiobtn = rbCommon.Content.ToString();
                                        CommonShare commonShare = new CommonShare(txtBuyerName.Text, dpDatePurchased.Text, numOfShares, radiobtn);
                                        list.Add(commonShare);
                                        //subtract the total common shares with the user inputted shares
                                        availableShares = numCommonShares - numOfShares;

                                      



                                        //connect to the database
                                        string connectUpdate = Properties.Settings.Default.connect_string;
                                        SqlConnection connUpdate = new SqlConnection(connectUpdate);
                                        connUpdate.Open();
                                        //create the insert statement
                                        string updateQuery = "UPDATE NumofShares SET numCommonShares = " + availableShares;
                                        //create a new command
                                        SqlCommand updateCommand = new SqlCommand(updateQuery, connUpdate);
                                        //execute the query
                                        updateCommand.ExecuteNonQuery();
                                        //end the connection
                                        connUpdate.Close();

                                    }
                                    //run if the preferred radio button is checked
                                    else if (rbPreferred.IsChecked == true)
                                    {

                                        radiobtn = rbPreferred.Content.ToString();
                                        //create a new PreferredShare Object and add it to the list
                                        PreferredShares preferredShare = new PreferredShares(txtBuyerName.Text, dpDatePurchased.Text, numOfShares, radiobtn);
                                        list.Add(preferredShare);
                                        //subtract the total preferred shares with the user inputted shares
                                        availableShares = numPreferredShares - numOfShares;
                                        //connect to the database
                                        string connectUpdate = Properties.Settings.Default.connect_string;
                                        SqlConnection connUpdate = new SqlConnection(connectUpdate);
                                        connUpdate.Open();
                                        //create the insert statement
                                        string updateQuery = "UPDATE NumofShares SET numPreferredShares = " + availableShares;
                                        //create a new command
                                        SqlCommand updateCommand = new SqlCommand(updateQuery, connUpdate);
                                        //execute the query
                                        updateCommand.ExecuteNonQuery();
                                        //end the connection
                                        connUpdate.Close();
                                    }
                                    //connect to the database
                                    string connectString = Properties.Settings.Default.connect_string;
                                    SqlConnection conn = new SqlConnection(connectString);
                                    conn.Open();
                                    //create the insert statement
                                    string insertQuery = "INSERT INTO ShareInfo (buyerName, shares, datePurchased, shareType) VALUES('" + txtBuyerName.Text + "', '" + numOfShares + "', '" + dpDatePurchased.Text + "', '" + radiobtn + "')";
                                    SqlCommand command = new SqlCommand(insertQuery, conn);
                                    //execute the query
                                    command.ExecuteNonQuery();
                                    //end the connection
                                    conn.Close();
                                    //fill the data grid 
                                    FillDataGrid();
                                    //update the summary
                                    SetSummary();
                                    //update the total revenue
                                    Revenue();
                                    //set the form back to default
                                    SetDefaults();
                                    MessageBox.Show("Added a record");
                                    lstShares.Items.Refresh();
                                }
                                else
                                {
                                    MessageBox.Show("The number of shares you entered is greater than the number of shares available.");

                                    txtNumOfShares.Text = "";
                                    txtNumOfShares.Focus();
                                }

                            }
                            else
                            {
                                MessageBox.Show("Number of Shares must be a whole numeric value.");
                                txtNumOfShares.Text = "";
                                txtNumOfShares.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Date cannot be empty.");
                            dpDatePurchased.Text = "";
                            dpDatePurchased.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Number of Shares cannot be empty.");
                        txtNumOfShares.Text = "";
                        txtNumOfShares.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Name cannot be empty.");
                    txtBuyerName.Text = "";
                    txtBuyerName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        
    }
}
