using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;
using MySql.Data.MySqlClient;


public partial class orderpage : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
    string constr1 = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
    
    string idorderselected = "";
    string ApplicationID = "AIzaSyCKt2rJbyO2gUNfw2ZBcEkA0pthz7QZC3g";
    string SENDER_ID = "249006616070";
    string Reg_Id= "APA91bEzHgVdoeXGu1G6TtyRQnBFE4OKBbBQli3GWHZPBuklrD7benMdoExDVnAzrE2-i5h2YVp4WDNdAfjecQ5GKer3GtuyXZkVKyBYb1FppRcv_Fj1NlQBp94V-9vBdQxd8nCVzGUB";
    string username;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        idorderselected = GridView1.SelectedRow.Cells[2].Text;
        username = GridView1.SelectedRow.Cells[3].Text;
       // Reg_Id = getregidfromserver(username);
       //Label2.Text ="id "+Reg_Id   ;
        TextBox1.Text = "Order number " + GridView1.SelectedRow.Cells[1].Text + "  will be ready after " + GridView1.SelectedRow.Cells[6].Text + " minutes ";
    }
    void updatestate(string idorder)
    {
        
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "UPDATE       [order]  SET        state = 2 WHERE        (Idorder = @Idorder)";
                cmd.Parameters.AddWithValue("@Idorder", idorder);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }

    string getregid(string username)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT reg_id FROM [users] WHERE [username] =@username";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Connection = con;
                con.Open();
                string regid = (string)cmd.ExecuteScalar();              
                con.Close();
                return (regid);
            }
        }
       
       
    }
    
    string getregidfromserver(string username)
    {
        string constr2 = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        using (MySqlConnection con = new MySqlConnection(constr2))
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT 	gcm_regid FROM gcm_users WHERE name =@username"))
            {               
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@username", username);
                    con.Open();
                    string regid = (string)cmd.ExecuteScalar();
                    con.Close();
                    return (regid);
            }
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {

        idorderselected = GridView1.SelectedRow.Cells[1].Text;
        username = GridView1.SelectedRow.Cells[3].Text;
        if (idorderselected != "" && username != "")
        {
            
            Reg_Id = getregidfromserver(username);
            if (Reg_Id != "")
            {
                Label2.Text = Reg_Id;
                 sendntofaction(Reg_Id,TextBox1.Text);
                 updatestate(idorderselected);
                   GridView1.DataBind();
            }
            else
            {
                Label2.Text = idorderselected + " null server " + username;
            }
        }
    }

    void sendntofaction(string RegId,string msg)
    {
        var value = msg; //message text box
        WebRequest tRequest;
        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send"); tRequest.Method = "post";
        tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
        tRequest.Headers.Add(string.Format("Authorization: key={0}", ApplicationID)); tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
        //Data post to the Server
        string postData =
    "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
     + value + "&data.time=" + System.DateTime.Now.ToString() +
     "&registration_id=" + RegId + "";
        Console.WriteLine(postData);

        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        tRequest.ContentLength = byteArray.Length;
        Stream dataStream = tRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
        WebResponse tResponse = tRequest.GetResponse(); dataStream = tResponse.GetResponseStream();
        StreamReader tReader = new StreamReader(dataStream);
        String sResponseFromServer = tReader.ReadToEnd();  //Get response from GCM server  
        Label1.Text = sResponseFromServer; //Assigning GCM response to Label text
        tReader.Close(); dataStream.Close();
        tResponse.Close();
    }


}