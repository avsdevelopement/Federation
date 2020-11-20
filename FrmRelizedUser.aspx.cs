using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmRelizedUser : System.Web.UI.Page
{
    ClsLogin LG = new ClsLogin();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");
        }

        TxtUserId.Focus();
    }

    protected void Realized_Click(object sender, EventArgs e)
    {
        try
        {   string UG;
            string USERID = TxtUserId.Text.ToUpper();
            UG = LG.GetUserGroup(Session["BRCD"].ToString(), USERID.ToString());
            
            if (UG!= "1")
            {
                int RC = LG.RealizedUser(USERID.ToString(), Session["BRCD"].ToString());
                if (RC > 0)
                {
                    TxtUserId.Text = "";
                    WebMsgBox.Show(USERID.ToString() + " User released successfully ...!!", this.Page);
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Released_User _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                    
                }
                else
                {
                    TxtUserId.Text = "";
                    WebMsgBox.Show(USERID.ToString() + " is inavlid login Code ...!!", this.Page);
                    return;
                }
            }
            else
            {
                TxtUserId.Text = "";
                WebMsgBox.Show(USERID.ToString() + " User modifictaion is restricted, Operation Terminated ...!!", this.Page);
                return;

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}