using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmCustBalWithSurity : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsAccopen accop = new ClsAccopen();
    ClsBindDropdown BD = new ClsBindDropdown();
    Cls_RecoBindDropdown BD1 = new Cls_RecoBindDropdown();

    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }
        if (!IsPostBack)
        {
            TxtAsonDate.Text = Session["EntryDate"].ToString();
            BD.BindBRANCHNAME(ddlBrName, null);
            BindRecDiv();
        }
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CustIDRep_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?AsOnDate=" + TxtAsonDate.Text + " &Brcd=" + Txtfrmbrcd.Text + " &FCustNo=" + TxtCust.Text + "&TCustNo=" + TxtTCust.Text + "&Div=" + DdlRecDiv.SelectedValue.ToString() + "&Dep=" + DdlRecDept.SelectedValue.ToString() + "&rptname=RptCustBalWithSurity.rdlc" + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtCust_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sql, AT;
            sql = AT = "";

            if (TxtCust.Text == "")
            {
                return;
            }
            string custname = accop.GetcustnameYN(accop.GetCENTCUST(), TxtCust.Text, Session["BRCD"].ToString());

            if (custname != null)
            {
                string[] name = custname.Split('_');
                txtname.Text = name[0].ToString();
            }

            string RC = txtname.Text;
            if (RC == "")
            {
                WebMsgBox.Show("Customer not found", this.Page);
                TxtCust.Text = "";
                TxtCust.Focus();
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtname.Text = custnob[0].ToString();
                TxtCust.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                TxtTCust.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Clear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    public void ClearData()
    {
        Txtfrmbrcd.Text = "";
        TxtAsonDate.Text = "";
        txtname.Text = "";
        TxtCust.Text = "";
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {

    }
    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Txtfrmbrcd.Text = ddlBrName.SelectedValue.ToString();
    }
    protected void Txtfrmbrcd_TextChanged(object sender, EventArgs e)
    {
        TxtCust.Focus();
    }
    protected void DdlRecDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindRecDept();
            DdlRecDept.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindRecDept()
    {
        BD1.BRCD = Txtfrmbrcd.Text.ToString();
        BD1.Ddl = DdlRecDept;
        BD1.RECDIV = DdlRecDiv.SelectedValue.ToString();
        BD1.FnBL_BindRecDept(BD1);
    }
    public void BindRecDiv()
    {
        BD1.BRCD = Txtfrmbrcd.Text.ToString();
        BD1.Ddl = DdlRecDiv;
        BD1.FnBL_BindRecDiv(BD1);
    }
    protected void txtTname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtname.Text = custnob[0].ToString();
                TxtCust.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                TxtAsonDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTCust_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sql, AT;
            sql = AT = "";

            if (TxtCust.Text == "")
            {
                return;
            }
            string custname = accop.GetcustnameYN(accop.GetCENTCUST(), TxtCust.Text, Session["BRCD"].ToString());

            if (custname != null)
            {
                string[] name = custname.Split('_');
                txtname.Text = name[0].ToString();
            }

            string RC = txtname.Text;
            if (RC == "")
            {
                WebMsgBox.Show("Customer not found", this.Page);
                TxtCust.Text = "";
                TxtCust.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnBalanceBook_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CustIDRep_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?AsOnDate=" + TxtAsonDate.Text + " &Brcd=" + Txtfrmbrcd.Text + " &FCustNo=" + TxtCust.Text + "&TCustNo=" + TxtTCust.Text + "&Div=" + DdlRecDiv.SelectedValue.ToString() + "&Dep=" + DdlRecDept.SelectedValue.ToString() + "&rptname=RptCustBalWithoutSurity.rdlc" + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}