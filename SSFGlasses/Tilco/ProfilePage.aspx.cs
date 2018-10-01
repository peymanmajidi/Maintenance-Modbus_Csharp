using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProfilePage : System.Web.UI.Page
{
    protected void Page_Load( object sender, EventArgs e )
    {
        LinqDataClassesDataContext db = new LinqDataClassesDataContext();
        if ( ( bool ) Session [ "login" ] == false || Request.QueryString [ "staffid" ] == null )
        {
            Response.Redirect("Login.aspx", true);



        }
        int staffid = -1;

        try
        {
            staffid = Int32.Parse(Request.QueryString [ "staffid" ]);
            var staff = db.StaffTBLs.Where(x => x.id == staffid).ToList().Single();
            picProfile.Src = getAx(staffid);

            litFullname.Text = staff.name + " " + staff.family;
            litMobile.Text = staff.mobile;
            litSemat.Text = staff.semat;
            litUnit.Text = db.UnitTBLs.Where(x => x.id == staff.unit).ToList().Single().title;


        }
        catch ( Exception )
        {

            Response.Redirect("Login.aspx", true);
        }

        setTiming(staffid);

    }

    private void setTiming( int staffid )
    {

        LinqDataClassesDataContext db = new LinqDataClassesDataContext();

        var present = from p in db.PresentTBLs
                      where p.staffid == staffid
                      select p;
        



        litTimein.Text = present.Single().time.ToString();


        var startTime = db.SetupTBLs.First().startTime.ToString();
        var takhirMax = db.SetupTBLs.First().TakhirMax.ToString();
        //************************************** محاسبه تاخیر
        TimeSpan duration = DateTime.Parse(litTimein.Text).Subtract(DateTime.Parse(startTime));
        litTakhir.Text = "[بدون تاخیر]";


        if ((int) db.StaffTBLs.Where(x => x.id == staffid).Single().type == 1)
        {
            litTakhir.Text = "[ مشاور ]";
            return;
        }
        try
        {
            if ( DateTime.Parse(duration.ToString()).CompareTo(DateTime.Parse(takhirMax)) > 0 )
            {
                litTakhir.Text = duration.ToString();
            }
        }
        
        catch 
        {
            
            
        }
        
}
    

private string getAx( int s )
{
    return System.IO.File.Exists(Server.MapPath("~/TimeTilco/Staff/" + s.ToString() + "/profile.jpg")) == true ? "~/TimeTilco/Staff/" + s.ToString() + "/profile.jpg" : "~/TimeTilco/Staff/default-user.png";
}




protected void btnStart_Click( object sender, EventArgs e )
{
    Response.Redirect("~/Home.aspx");
}
}
