using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{  LinqDataClassesDataContext db=new LinqDataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if ((bool)Session["login"] == false)
        {
           Response.Redirect("Login.aspx",true);

        }
        

        var present = from p in db.PresentTBLs
                      join s in db.StaffTBLs on p.staffid equals s.id
                      orderby p.time descending
                      select new
                      {
                          p.staffid,
                          fullname=s.name + " " + s.family,
                          p.time
                          ,
                          ax = getAx(s.id)

                      };
        GridView1.DataSource = present;
        GridView1.DataBind();

        if(present.Any())
        {
            divnew.Visible = true;
            litFullname.Text = present.First().fullname;
            imgProfile.Src = getAx(present.First().staffid);
            lnkProfile.NavigateUrl = "~/ProfilePage.aspx?staffid="+ present.First().staffid;

        }

    }

    private string getAx(int s)
    {
        return   System.IO.File.Exists(Server.MapPath("~/TimeTilco/Staff/" + s.ToString() + "/profile.jpg")) == true ? "~/TimeTilco/Staff/" + s.ToString() + "/profile.jpg" : "~/TimeTilco/Staff/default-user.png";
    }
    protected void LinqPresent_Selecting(object sender, LinqDataSourceSelectEventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        var present = from p in db.PresentTBLs
                      join s in db.StaffTBLs on p.staffid equals s.id
                      orderby p.time descending
                      select new
                      {
                          p.staffid,
                          fullname = s.name + " " + s.family,
                          p.time
                          ,
                          ax = "~/TimeTilco/Staff/" + s.id + "/profile.jpg"

                      };
        GridView1.DataSource = present;
        GridView1.DataBind(); //  <-----------   مهم
        if (present.Any())
        {
         ///   litFullname.Text = GridView1.Rows[0].
            divnew.Visible = true;
        }
    }
    protected void timerTriger_Tick(object sender, EventArgs e)
    {
        

        LinqDataClassesDataContext db2 = new LinqDataClassesDataContext();


        try
        {
            var tt = (from t in db2.triggerTBLs
                      select t).ToList().Single();


            //if (tt.reqTrig == true)
            //{
            //    tt.reqTrig = false;

            //    db2.SubmitChanges();
            //    //requestNumberUpdate();

          
            //    //thread.Start();
            //}
        }
        catch { }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}