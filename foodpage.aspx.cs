using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class foodpage : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        GridView1.DataBind();
    }
    private void BindGrid()
    {
       
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT * FROM [food] WHERE [idrest] =" + Session["idrest"].ToString();
              
                cmd.Connection = con;
                con.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
                con.Close();
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string imgext, imgpath;
        try
        {
            if(TextBox1.Text!="" && TextBox2.Text!="" && TextBox3.Text!="" && TextBox4.Text!="" )
            {
                if (FileUpload1.HasFile)
                {
                    imgext = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf(".") + 1).ToLower();
                    if (imgext == "jpg" || imgext == "gif" || imgext == "bmp" || imgext == "png")
                    {
                        imgpath = "~/image/" + FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath(imgpath));
                   
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            string query = "INSERT INTO [food] ([nfood], [idrest], [price], [maxtime], [description], [image]) VALUES (@nfood, @idrest, @price, @maxtime, @description, @image)";
                           
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@nfood", TextBox1.Text);
                                cmd.Parameters.AddWithValue("@idrest", Session["idrest"].ToString());
                                cmd.Parameters.AddWithValue("@price", TextBox2.Text);
                                cmd.Parameters.AddWithValue("@maxtime", TextBox3.Text);
                                cmd.Parameters.AddWithValue("@description", TextBox4.Text);
                                cmd.Parameters.AddWithValue("@image", imgpath);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }

                        Label1.Text = "It has been added successfully!";
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                        TextBox4.Text = "";
                        GridView1.DataBind();
                       
                    }

                }
            }
           

        }
        catch (Exception ex)
        {
            Label1.Text = ex.ToString();
        }
    }
}