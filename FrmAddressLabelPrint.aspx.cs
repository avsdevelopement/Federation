using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAddressLabelPrint : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsAccopen accop = new ClsAccopen();
    ClsShareDividend SD = new ClsShareDividend();
    string ACC1, ACC2, PWD = "";
    string FL = "";
    string[] cname, cname1, cname2;
    string Stage = "", CustName="";
    ClsBindDropdown BD = new ClsBindDropdown();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtFMemNo.Text == "" || TxtTMemNo.Text == "")
            {
                WebMsgBox.Show("Please Enter Account Number..!!", this.Page);
                return;
            }
            ACC1 = TxtFMemNo.Text;
            ACC2 = TxtTMemNo.Text;

            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "AddressLabelPrint_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?FAccno=" + TxtFMemNo.Text + "&TAccno=" + TxtTMemNo.Text.ToString() + "&FDate=" + TxtFDate.Text + "&rptname=RptAddressLabelPrint.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void clear()
    {
        try
        {
            TxtFMemNo.Text = "";
            TxtFMemName.Text = "";
            TxtTMemNo.Text = "";
            TxtTMemName.Text = "";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtTMemNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //cname1 = accop.Getcustname(TxtTMemNo.Text.ToString()).Split('_');

            //TxtTMemName.Text = cname1[0].ToString();

            Stage = BD.Getstage1(TxtTMemNo.Text.Trim().ToString(), "1", "4");
            if (Stage != null)
            {
                if (Stage != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise...!!";
                    ModalPopup.Show(this.Page);
                    TxtTMemNo.Text = "";
                    TxtTMemName.Text = "";
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = SD.GetCustName("1", "4", TxtTMemNo.Text.Trim().ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        TxtTMemName.Text = CustName[0].ToString();
                        TxtTMemNo.Text = CustName[1].ToString();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                TxtTMemNo.Text = "";
                TxtTMemName.Text = "";
                TxtTMemNo.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtFMemNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //cname2 = accop.Getcustname(TxtFMemNo.Text.ToString()).Split('_');

            //TxtFMemName.Text = cname2[0].ToString();

            Stage = BD.Getstage1(TxtFMemNo.Text.Trim().ToString(), "1", "4");
            if (Stage != null)
            {
                if (Stage != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise...!!";
                    ModalPopup.Show(this.Page);
                    TxtFMemName.Text = "";
                    TxtFMemNo.Text = "";
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = SD.GetCustName("1", "4", TxtFMemNo.Text.Trim().ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        TxtFMemName.Text = CustName[0].ToString();
                        TxtFMemNo.Text = CustName[1].ToString();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                TxtFMemNo.Text = "";
                TxtFMemName.Text = "";
                TxtFMemNo.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtTMemName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CustName = TxtTMemName.Text.ToString();
            string[] custnob = CustName.Split('_');
            if (custnob.Length > 1)
            {
                TxtTMemName.Text = custnob[0].ToString();
                TxtTMemNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFMemName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CustName = TxtFMemName.Text.ToString();
            string[] custnob = CustName.Split('_');
            if (custnob.Length > 1)
            {
                TxtFMemName.Text = custnob[0].ToString();
                TxtFMemNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}