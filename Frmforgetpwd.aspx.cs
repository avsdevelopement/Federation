using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class forgetpwd : System.Web.UI.Page
{
    ClsWforgotPWD clsa = new ClsWforgotPWD();
    protected void Page_Load(object sender, EventArgs e)
    {
        

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string loginid = txtloginId.Text.ToString();
        string createpwd = txtcreatenewpwd.Text.ToString();
        string confpwd = txtconfirmpwd.Text.ToString();    
        int Result = Convert.ToInt32(clsa.Submitforgetpwd(loginid, createpwd, confpwd,Session["BRCD"].ToString()));

        if (Result == 1)
        {

        WebMsgBox.Show("password create successfully ",this.Page);
        }

        else if (Result == 0)
        {
            WebMsgBox.Show("Invalid user login code....!!", this.Page);
            
        }
        clear();
    }

    public void clear()
    {
        txtloginId.Text = "";
        txtcreatenewpwd.Text = "";
        txtconfirmpwd.Text = "";
        


    }

}