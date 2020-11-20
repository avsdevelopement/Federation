using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmGlobalAccSts : System.Web.UI.Page
{
    ClsAccopen AC = new ClsAccopen();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCustomerMast CM = new ClsCustomerMast();
    int Cnt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void TxtCustNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = CM.GetStage(Session["BRCD"].ToString(), TxtCustNo.Text);

            if (AT == "1003")
            {
                TxtCustName.Text = BD.GetCustName(TxtCustNo.Text, Session["BRCD"].ToString());
                
                return;
            }
            else if (AT == "1004")
            {
                //ClearData();
                WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                return;
            }
            else if (AT == "1001" || AT == "1002")
            {
                TxtCustName.Text = BD.GetCustName(TxtCustNo.Text, Session["BRCD"].ToString());
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtCustName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtCustName.Text;
            string[] custnob = CUNAME.Split('_');

            if (custnob.Length > 1)
            {
                TxtCustName.Text = custnob[0].ToString();
                TxtCustNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                string AT = CM.GetStage(Session["BRCD"].ToString(), TxtCustNo.Text);
                if (AT == "1003")
                {
                   TxtFDate.Focus();
                   return;
                }
                else if (AT == "1004")
                {
                    //ClearData();
                    WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                    return;
                }
                else if (AT == "1001" || AT == "1002")
                {
                    WebMsgBox.Show("Customer is not authorized...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}