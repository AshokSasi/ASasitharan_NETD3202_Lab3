using System;
using System.Collections.Generic;
using System.Text;
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
namespace ASasitharan_NETD3202_Lab3
{
    /// <summary>
    /// Interaction logic for CreateEntry.xaml
    /// </summary>
    public partial class CreateEntry : UserControl
    {
        public CreateEntry()
        {
            InitializeComponent();
        }

        private void btnCreateEntry_Click(object sender, RoutedEventArgs e)
        {
            int numOfShares = 0;
            int numCommonShares = 0;
            int numPreferredShares = 0;
            string radiobtn="";
            int availableShares = 0;
            try
            {
                if(txtBuyerName.Text!= string.Empty)
                {
                    if(txtNumOfShares.Text!=string.Empty)
                    {
                        if(dpDatePurchased.Text!=string.Empty)
                        {
                            if (int.TryParse(txtNumOfShares.Text, out numOfShares))
                            {
                                //this block gets the amount of common shares 
                                string cs = Properties.Settings.Default.connect_string;
                                SqlConnection cn = new SqlConnection(cs);
                                cn.Open();
                                string selectQueryCommon = "SELECT numCommonShares From NumofShares";
                                SqlCommand selectCommand = new SqlCommand(selectQueryCommon, cn);
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
                                        //subtract the total common shares with the user inputted shares
                                        availableShares = numCommonShares - numOfShares;

                                        radiobtn = rbCommon.Content.ToString();

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
                                   
                                    string connectString = Properties.Settings.Default.connect_string;
                                    SqlConnection conn = new SqlConnection(connectString);
                                    conn.Open();
                                    string insertQuery = "INSERT INTO ShareInfo (buyerName, shares, datePurchased, shareType) VALUES('" + txtBuyerName.Text + "', '" + numOfShares + "', '" + dpDatePurchased.Text + "', '" + radiobtn + "')";
                                    SqlCommand command = new SqlCommand(insertQuery, conn);
                                    //execute the query
                                    command.ExecuteNonQuery();
                                    //end the connection
                                    conn.Close();
                                    MessageBox.Show("Added a record");
                            
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
