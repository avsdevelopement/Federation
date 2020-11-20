using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class FrmAVS5067 : System.Web.UI.Page
{
    scustom customcs = new scustom();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCashReciept CurrentCls = new ClsCashReciept();
    ClsAuthorized PVOUCHER = new ClsAuthorized();
    ClsBindDropdown ddlbind = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsOpenClose OC = new ClsOpenClose();
    DbConnection conn = new DbConnection();
    ClsDDSPatch CDS = new ClsDDSPatch();
    DataTable DT = new DataTable();
    Datecall DS = new Datecall();
    string res, FL = "",Glcode="";
    int nonmi1;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            if (!IsPostBack)
            {
                ddlbind.BindAccType(ddlAccType);
                ddlbind.BindAccType(ddlAccType1);
                ddlbind.BindAccStatus11(ddlOpType);
                ddlbind.BindAccStatus11(ddlOpType1);
                autoglname.ContextKey = Session["BRCD"].ToString();
                
                //Only admin will have the access to view the form
                if ((Session["BRCD"].ToString() != "1") && (CMN.GetParameter("1", "CBSP", "1003").ToString() == "N"))
                {
                    HttpContext.Current.Response.Redirect("FrmBlank.aspx", false);
                }
                else
                {
                    TxtBrcd.Text = Session["BRCD"].ToString();
                    Txtprodcode.Focus();
                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void CLearAll()
    {
       // TxtBrcd.Text = "";
        Txtprodcode.Text = "";
        txtopendate.Text = "";
        txtOpenDate1.Text = "";
        txtname.Text = "";
        txtaccno.Text = "";
        TxtAccName.Text = "";
        txtopendate.Text = "";
        txtOpenDate1.Text = "";
        txtLastindate.Text = "";
        txtLastindate1.Text = "";
        txtClosedate.Text = "";
        txtClosedate1.Text = "";
        txtdailyamount.Text = "";
        txtdaily1.Text = "";
        ddlAccType.SelectedValue = "0";
        ddlAccType1.SelectedValue = "0";
        ddlOpType.SelectedValue = "0";
        ddlOpType1.SelectedValue = "0";
    }
    protected void Txtprodcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(Txtprodcode.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["DRGL"] = GL[1].ToString();
            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + Txtprodcode.Text + "_" + ViewState["DRGL"].ToString();
            string PDName = customcs.GetProductName(Txtprodcode.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                txtname.Text = PDName;
                txtaccno.Focus();
               

            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                Txtprodcode.Text = "";
                Txtprodcode.Focus();
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
            string custno = txtname.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtname.Text = CT[0].ToString();
                Txtprodcode.Text = CT[1].ToString();
                txtaccno.Focus();
                string[] GLS = BD.GetAccTypeGL(Txtprodcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + Txtprodcode.Text + "_" + ViewState["DRGL"].ToString();
                
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtaccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AN;
            AN = customcs.GetAccountName(txtaccno.Text, Txtprodcode.Text, Session["BRCD"].ToString()).Split('_');
            if (AN != null )
            {
                TxtAccName.Text = AN[1].ToString();
                res = DS.AccNodisplay(TxtBrcd.Text, Txtprodcode.Text, txtaccno.Text);
                GetData(txtaccno.Text);
                BINDDATA(txtaccno.Text);
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                txtaccno.Text = "";
                txtaccno.Focus();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void GetData(string Accno)
    {
        DataTable DT = new DataTable();
        DT = CDS.GetRecords(Session["BRCD"].ToString(), Txtprodcode.Text, Accno.ToString());
        if (DT.Rows.Count > 0)
        {
            txtopendate.Text = DT.Rows[0]["OPENINGDATE"].ToString();
            txtLastindate.Text = DT.Rows[0]["LASTINTDT"].ToString();
            txtClosedate.Text = DT.Rows[0]["CLOSINGDATE"].ToString();
            txtdailyamount.Text = DT.Rows[0]["D_AMOUNT"].ToString();
            ddlAccType.SelectedValue = DT.Rows[0]["ACC_TYPE"].ToString();
            ddlOpType.SelectedValue = DT.Rows[0]["ACC_STATUS"].ToString();
        }
        
    }
    protected void TxtAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtAccName.Text = custnob[0].ToString();
                txtaccno.Text = custnob[1].ToString();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                txtaccno.Text = "";
                txtaccno.Focus();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BINDDATA(string ACCNO)
    {
        CDS.binddata(grdstandard,Session["BRCD"].ToString(), Txtprodcode.Text, ACCNO.ToString());
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
           
            
                int Result = CDS.Insertolddata(TxtBrcd.Text, Txtprodcode.Text, txtaccno.Text, txtopendate.Text, txtLastindate.Text, txtClosedate.Text, txtdailyamount.Text, Session["MID"].ToString(), ddlAccType.SelectedValue.ToString(), ddlOpType.SelectedValue.ToString());
                if (Result > 0)
                {
                    string Dailyamt = "";
                    if (txtdaily1.Text == "")
                    {
                        Dailyamt = "0";
                    }
                    else
                    {
                        Dailyamt = txtdaily1.Text;
                    }
                    int Res = CDS.UpdateData(TxtBrcd.Text, Txtprodcode.Text, txtaccno.Text, txtOpenDate1.Text.Trim().ToString() == "" ? txtopendate.Text : txtOpenDate1.Text.Trim().ToString(), txtLastindate1.Text.Trim().ToString() == "" ? txtLastindate.Text : txtLastindate1.Text.Trim().ToString(), txtClosedate1.Text.Trim().ToString() == "" ? txtClosedate.Text : txtClosedate1.Text.Trim().ToString(), Dailyamt.Trim().ToString() == "" ? txtdailyamount.Text : Dailyamt.Trim().ToString(), Session["MID"].ToString(), ddlOpType1.SelectedValue.ToString() == "0" ? ddlOpType.SelectedValue.ToString() : ddlOpType1.SelectedValue.ToString(), ddlAccType1.SelectedValue.ToString() == "0" ? ddlAccType.SelectedValue.ToString() : ddlOpType1.SelectedValue.ToString());
                    if (Res > 0)
                    {
                         Glcode = CDS.CheckGlcode(Session["BRCD"].ToString(), Txtprodcode.Text);
                         if ( Convert.ToInt32(Glcode) == 1)
                         {
                             WebMsgBox.Show("Data Updated Successfully..!!", this.Page);
                             CLearAll();
                         }
                         else if (Convert.ToInt32(Glcode) == 2)
                         {
                             int DDS = CDS.UpdateDDSHistory(Session["BRCD"].ToString(), Txtprodcode.Text, txtaccno.Text, ddlOpType1.SelectedValue.ToString() == "0" ? ddlOpType.SelectedValue.ToString() : ddlOpType1.SelectedValue.ToString(), txtLastindate1.Text.Trim().ToString() == "" ? txtLastindate.Text : txtLastindate1.Text.Trim().ToString(), txtOpenDate1.Text.Trim().ToString() == "" ? txtopendate.Text : txtOpenDate1.Text.Trim().ToString());
                             //if (DDS > 0)//Dhanya Shetty//16/01/2018 Added message 
                             //{
                                 WebMsgBox.Show("Data Updated Successfully..!!", this.Page);
                                 CLearAll();
                             //}
                         }
                         else if (Convert.ToInt32(Glcode) == 3)
                         {
                             int Loan = CDS.UpdateLoanInfo(Session["BRCD"].ToString(), Txtprodcode.Text, txtaccno.Text, ddlOpType1.SelectedValue.ToString() == "0" ? ddlOpType.SelectedValue.ToString() : ddlOpType1.SelectedValue.ToString(), txtLastindate1.Text.Trim().ToString() == "" ? txtLastindate.Text : txtLastindate1.Text.Trim().ToString());
                             if (Loan > 0)
                             {
                                 WebMsgBox.Show("Data Updated Successfully..!!", this.Page);
                                 CLearAll();
                             }
                         }
                         else if (Convert.ToInt32(Glcode) == 5)
                         {
                             int Deposit = CDS.UpdateDepositInfo(Session["BRCD"].ToString(), Txtprodcode.Text, txtaccno.Text, ddlOpType1.SelectedValue.ToString() == "0" ? ddlOpType.SelectedValue.ToString() : ddlOpType1.SelectedValue.ToString(), txtLastindate1.Text.Trim().ToString() == "" ? txtLastindate.Text : txtLastindate1.Text.Trim().ToString());
                             if (Deposit > 0)
                             {
                                 WebMsgBox.Show("Data Updated Successfully..!!", this.Page);
                                 CLearAll();
                             }
                         }
                        
                    }
                }
                else 
                {
                    WebMsgBox.Show("Old Data Not Saved Contact To Call Center", this.Page);
                }
            
           
           
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {
        try
        {
            CLearAll();
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}