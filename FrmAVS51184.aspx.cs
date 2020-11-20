using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS51184 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAccopen accop = new ClsAccopen();
    ClsCustomerDetails CD = new ClsCustomerDetails();
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    ClsAVS51184 Memno = new ClsAVS51184();
    string str="";
    int sResult = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"] == null)
            Response.Redirect("FrmLogin.aspx");

        if (!IsPostBack)
        {
            TxtTDate.Text = Session["EntryDate"].ToString();
            autoglname.ContextKey = Session["BRCD"].ToString();
            autocustname.ContextKey = Session["BRCD"].ToString();
            BD.BindINSTMODTYPE(ddlRecovery);
            ViewState["Flag"] = "AD";
            SHOWUNAUTHORIZE();
        }

    }
    protected void txtProductName_TextChanged(object sender, EventArgs e)
    {
         try
        {
            string CUNAME = txtProductName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtProductName.Text = custnob[0].ToString();
                txtProductCd.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
              
            }
            txtACCno.Text = Memno.GetAVS51184("GETACCNO", txtCustno.Text, BRCD: Session["BRCD"].ToString(), SUBGL: txtProductCd.Text);
           
        }
         catch (Exception Ex)
         {
             ExceptionLogging.SendErrorToText(Ex);
         }
    }
    protected void txtCustName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtCustName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtCustName.Text = custnob[0].ToString();
                txtCustno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                txtMemberNo.Text = Memno.GetAVS51184("GETMEMNO", txtCustno.Text, BRCD: Session["BRCD"].ToString(),SUBGL:"");
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtCustno_TextChanged(object sender, EventArgs e)
    {
        DT = CD.GetStage(txtCustno.Text);

        if (DT.Rows[0]["STAGE"].ToString() == "1001")
        {
            WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
            txtCustno.Focus();
            return;
        }
        else if (DT.Rows[0]["STAGE"].ToString() == "1004")
        {
            WebMsgBox.Show("Customer is Deleted...!!", this.Page);
            txtCustno.Focus();

            return;
        }
        else if (DT.Rows[0]["STAGE"].ToString() == "" || DT.Rows[0]["STAGE"].ToString() == null)
        {
            WebMsgBox.Show("Customer Not Exists...!!", this.Page);
            txtCustno.Focus();

            return;
        }
        else
        {
            DT1 = CD.GetCustName(txtCustno.Text); //ankita 21/11/2017 brcd removed
            txtCustName.Text = DT1.Rows[0]["CUSTNAME"].ToString();
            txtMemberNo.Text = Memno.GetAVS51184("GETMEMNO", txtCustno.Text, BRCD: Session["BRCD"].ToString(), SUBGL: "");
        }
    }
    protected void txtProductCd_TextChanged(object sender, EventArgs e)
    {
        string PDName = Memno.GetProductName(txtProductCd.Text, Session["BRCD"].ToString());
        if (PDName != null)
        {
            txtProductName.Text = PDName;
            txtACCno.Text = Memno.GetAVS51184("GETACCNO", txtCustno.Text, BRCD: Session["BRCD"].ToString(), SUBGL: txtProductCd.Text);
            txtPreviousInst.Text = Memno.GetAVS51184("GETPREVIOUSINST", txtCustno.Text, BRCD: Session["BRCD"].ToString(), SUBGL: txtProductCd.Text,ACCNO:txtACCno.Text);
        
        }
        else
        {
            WebMsgBox.Show("Product Number is Invalid....!", this.Page);
            txtProductCd.Text = "";
            txtProductCd.Focus();
        }
           
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
      // if (ViewState["Flag"].ToString() == "AD")
        {
            if (txtCustno.Text == "")
            {
                WebMsgBox.Show("Enter Customer ID..!!", this.Page);
                ddlRecovery.Focus();
                return;
            }
            if (txtCustName.Text == "")
            {
                WebMsgBox.Show("Enter Customer Name..!!", this.Page);
                return;
            }
            if (txtMemberNo.Text == "")
            {
                WebMsgBox.Show("Enter Member No..!!", this.Page);
                return;
            }
            if (ddlRecovery.SelectedValue == "0")
            {
                WebMsgBox.Show("Select Recovery Type..!!", this.Page);
                txtProductCd.Focus();
                return;
            }
            if (txtProductCd.Text == "")
            {
                WebMsgBox.Show("Enter Product Code ..!!", this.Page);
                txtMonth.Focus();
                return;
            }
            if (txtMonth.Text == "")
            {
                WebMsgBox.Show("Enter Month ..!!", this.Page);
                txtYear.Focus();
                return;
            }
            if (txtYear.Text == "")
            {
                WebMsgBox.Show("Enter Year ..!!", this.Page);
                txtPreviousInst.Focus();
                return;
            }
            if (txtPreviousInst.Text == "")
            {
                WebMsgBox.Show("Enter Previous Installment Amount   ..!!", this.Page);
                txtCurrentInst.Focus();

                return;
            }
            if (txtCurrentInst.Text == "")
            {
                WebMsgBox.Show("Enter Current Installment Amount   ..!!", this.Page);
                txtReason.Focus();
                return;
            }
            try
            {
                string s1;

                sResult = Memno.GetAVS51184(FLAG: "AD", BRCD: Session["BRCD"].ToString(), CUSTNO: txtCustno.Text,RECOVERYTYPE:ddlRecovery.SelectedValue.ToString(), ACCNO: txtACCno.Text, SUBGL: txtProductCd.Text, MONTH: txtMonth.Text, YEAR: txtYear.Text, EFFECTDATE: Session["EntryDate"].ToString(), OLDSUBS: txtPreviousInst.Text, NEWSUBS: txtCurrentInst.Text, REASON: txtReason.Text, MID: Session["MID"].ToString());
                if (sResult !=0)
                {
                    WebMsgBox.Show("Successfully Create..!!", this.Page);
                    clear();
                    return;
                    
                }
                else
                {
                    WebMsgBox.Show("Try Again..!!", this.Page);
                    return;
                }
            }
            catch (Exception EX)
            {
                WebMsgBox.Show(EX.Message.ToString(), this.Page);
                ExceptionLogging.SendErrorToText(EX);

            }
        }
    }
    protected void BtnAuthorise_Click(object sender, EventArgs e)
    {
        try
        {
            string s1;

            str = Memno.GetAVS511841(FLAG: "AT", BRCD: Session["BRCD"].ToString(), CUSTNO: txtCustno.Text, RECOVERYTYPE: ddlRecovery.SelectedValue, ACCNO: txtACCno.Text, SUBGL: txtProductCd.Text, MONTH: txtMonth.Text, YEAR: txtYear.Text, EFFECTDATE: Session["EntryDate"].ToString(), OLDSUBS: txtPreviousInst.Text, NEWSUBS: txtCurrentInst.Text, REASON: txtReason.Text, MID: Session["MID"].ToString());
            if (str =="NOTAUTH")
            {
                WebMsgBox.Show("Warning: User is restricted to Authorize.!!", this.Page);
                clear();
                return;

            }
            else
                if (str == "DONE")
                {
                    WebMsgBox.Show("Record Successfully Authorized..!!", this.Page);
                    clear();
                    return;
                }
                else

                DT = Memno.GetStage(txtCustno.Text, Session["BRCD"].ToString());

            if (DT.Rows[0]["STAGE"].ToString() == "1003")
            {
                WebMsgBox.Show("Record Already Authorize...!!", this.Page);
                txtCustno.Focus();
                return;
            }
           
        }
        catch (Exception EX)
        {
            WebMsgBox.Show(EX.Message.ToString(), this.Page);
            ExceptionLogging.SendErrorToText(EX);

        }
    }
    protected void btnUnauthorise_Click(object sender, EventArgs e)
    {
        try
        {
            sResult = Memno.GetAVS51184(FLAG: "UNAT", BRCD: Session["BRCD"].ToString(), CUSTNO: txtCustno.Text, RECOVERYTYPE: ddlRecovery.SelectedValue, ACCNO: txtACCno.Text, SUBGL: txtProductCd.Text, MONTH: txtMonth.Text, YEAR: txtYear.Text, EFFECTDATE: Session["EntryDate"].ToString(), OLDSUBS: txtPreviousInst.Text, NEWSUBS: txtCurrentInst.Text, REASON: txtReason.Text, MID: Session["MID"].ToString());
            if (sResult != 0)
            {
                WebMsgBox.Show("Record Successfully Authorized..!!", this.Page);
                clear();
                return;
            }
        }
        catch (Exception EX)
        {
            WebMsgBox.Show(EX.Message.ToString(), this.Page);
            ExceptionLogging.SendErrorToText(EX);

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
    public void SHOWUNAUTHORIZE()
    {


        DataTable dt = new DataTable();
        string str = "0";
        int RS = Memno.GridViewBind(FLAG: "SHOWUNATHORIZE", GrdAcc: GrdAcc);
       // dt = Memno.ShowGridView(FLAG: "SHOWUNAUTHORIZE");


    }

    public void CallInfo()
    {
      

        DataTable dt = new DataTable();
        string str = "0";
        dt = Memno.ShowGridView(FLAG: "SHOW",SUBGL: ViewState["SUBGL"].ToString(),ACCNO: ViewState["ACCNO"].ToString(),CUSTNO: ViewState["CUSTNO"].ToString(),BRCD: ViewState["BRCD"].ToString());
        if (dt.Rows.Count > 0)
        {
            txtCustno.Text = dt.Rows[0]["CUSTNO"].ToString();
            txtCustName.Text = dt.Rows[0]["CUSTNAME"].ToString();
            txtMemberNo.Text = dt.Rows[0]["MEMNO"].ToString();
           
            TxtTDate.Text = dt.Rows[0]["EFFECTDATE"].ToString();
            txtProductCd.Text = dt.Rows[0]["SUBGL"].ToString();
            txtACCno.Text = dt.Rows[0]["ACCNO"].ToString();
            txtProductName.Text = dt.Rows[0]["GLNAME"].ToString();
            txtMonth.Text = dt.Rows[0]["MONTH"].ToString();
            txtYear.Text = dt.Rows[0]["YEAR"].ToString();
            txtPreviousInst.Text = dt.Rows[0]["OLDSUBS"].ToString();
            txtCurrentInst.Text = dt.Rows[0]["NEWSUBS"].ToString();
            txtReason.Text = dt.Rows[0]["REASON"].ToString();
            ddlRecovery.SelectedValue = dt.Rows[0]["RECOVERYTYPE"].ToString();
           
           
        }
        else
        {
            WebMsgBox.Show("Vendor not exists!", this.Page);

        }

    }
    public void clear()
    {
        txtCustno.Text = "";
        txtCurrentInst.Text = "";
        txtCustName.Text = "";
        txtMemberNo.Text = "";
        txtMonth.Text = "";
        txtYear.Text = "";
        txtProductCd.Text = "";
        txtProductName.Text = "";
        txtReason.Text = "";
        txtACCno.Text = "";
        txtPreviousInst.Text = "";
        ddlRecovery.SelectedValue = "0";


    }
    protected void GrdAcc_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GrdAcc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void LnkAutorise_Click(object sender, EventArgs e)
    {
        LinkButton lnkedit = (LinkButton)sender;
        string str = lnkedit.CommandArgument.ToString();
        string[] ARR = str.Split(',');
        ViewState["SUBGL"] = ARR[0].ToString();
        ViewState["ACCNO"] = ARR[1].ToString();
        ViewState["CUSTNO"] = ARR[2].ToString();
        ViewState["BRCD"] = ARR[3].ToString();
        

        Submit.Visible = false;
        CallInfo();
        
    }
    protected void lnkUnAuthorise_Click(object sender, EventArgs e)
    {
        try{

            sResult = Memno.GetAVS51184(FLAG: "UNAT", BRCD: Session["BRCD"].ToString(), CUSTNO: txtCustno.Text, RECOVERYTYPE: ddlRecovery.SelectedValue, ACCNO: txtACCno.Text, SUBGL: txtProductCd.Text, MONTH: txtMonth.Text, YEAR: txtYear.Text, EFFECTDATE: Session["EntryDate"].ToString(), OLDSUBS: txtPreviousInst.Text, NEWSUBS: txtCurrentInst.Text, REASON: txtReason.Text, MID: Session["MID"].ToString());
            if (sResult != 0)
            {
                WebMsgBox.Show("Record Successfully Authorized..!!", this.Page);
                clear();
                return;
            }
        }
        catch (Exception EX)
        {
            WebMsgBox.Show(EX.Message.ToString(), this.Page);
            ExceptionLogging.SendErrorToText(EX);

        }

    }
}