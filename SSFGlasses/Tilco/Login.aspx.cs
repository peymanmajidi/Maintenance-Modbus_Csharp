using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["login"] = false;
    }
    LinqDataClassesDataContext db = new LinqDataClassesDataContext();
    protected void btnStart_Click(object sender, EventArgs e)
    {
        try
        {
            var users = (from u in db.UsersTBLs
                        where u.username == txtusername.Text.Trim() && u.password == txtpassword.Text.Trim()
                        select u).ToList();
            if (users.Any())
            {
                var user = users.Single();
                Session["fullname"] = user.fullname;
                Session["userid"] = user.id;
                Session["login"] = true;
                Response.Redirect("Home.aspx");
                
            }
           
        }
        catch { }
    }
}