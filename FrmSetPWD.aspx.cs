using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmSetPWD : System.Web.UI.Page
{
    ClsSetPWD cls = new ClsSetPWD();
    int CNT;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btncreate_Click(object sender, EventArgs e)
    {
        ViewState["PASS"] = txtEnterpwd.Text.ToString();
        ViewState["CONFPASS"] = txtconfirmpwd.Text.ToString();

        if (ViewState["PASS"].ToString() == ViewState["CONFPASS"].ToString())
        {
            int CNT = cls.submitAsspwd(ViewState["PASS"].ToString(), txtlogincode.Text,Session["BRCD"].ToString());
            if (CNT > 0)
            {
                WebMsgBox.Show("Password assign successfully", this.Page);
                clear();
                return;
            }
        }
        else
        {
            WebMsgBox.Show("Password Not Matched...!", this.Page);
            clear();
            return;
        }
    }

    public void clear()
    {

        txtlogincode.Text = "";
        txtconfirmpwd.Text = "";
        txtEnterpwd.Text = "";

    }
}