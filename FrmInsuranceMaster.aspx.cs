using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmInsuranceMaster : System.Web.UI.Page
{
    ClsInsuranceMast IM = new ClsInsuranceMast();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
    DataTable DT = new DataTable();
    public static string Flag;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int Result;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }

                WorkingDate.Value = Session["EntryDate"].ToString();
                autoglname.ContextKey = Session["BRCD"].ToString();
                ViewState["UN_FL"] = Request.QueryString["FLAG"].ToString();
                txttype.Focus();
                BindGrid("ED");
                Flag = "1";
                ViewState["Flag"] = "AD";
                BtnSubmit.Text = "Submit";
                TblDiv_MainWindow.Visible = false;
                Div_grid.Visible = true;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGrid(string Flag)
    {
        if (Flag == "ED")
        {
            int RS = IM.BindGrid(grdinsurance, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "", "");
        }
        else
        {
            int RS = IM.BindGrid(grdinsurance, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), txttype.Text, txtaccno.Text);
        }
    }


    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            TxtInsamt.Text = (TxtInsamt.Text == "" ? "0" : TxtInsamt.Text);
            TxtPremamt.Text = (TxtPremamt.Text == "" ? "0" : TxtPremamt.Text);
            if (ViewState["Flag"].ToString() == "AD")
            {
                Result = IM.Insertdata(txttype.Text, txtaccno.Text, TxtpolicyNo.Text, TxtInsamt.Text, Txtstartdate.Text, Txtexpirydate.Text, Txtclosedate.Text, TxtPremamt.Text, DDlPolstatus.SelectedItem.Text, TxtIname.Text,
                    Txtdesc.Text, DDlEndordement.SelectedItem.Text, Txtsentdate.Text, TxtRecDate.Text, Session["BRCD"].ToString(), "1001", Session["MID"].ToString(), "Insert", Session["EntryDate"].ToString());
                if (Result > 0)
                {
                    WebMsgBox.Show("Insurance Details Added successfully..!!", this.Page);
                   
                    BindGrid("PA");
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Insurance_Add _" + txttype.Text + "_" + txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    Cleardata();
                    return;

                }
            }
            else if (ViewState["Flag"].ToString() == "MD")
            {
                Result = IM.ModifyData(txttype.Text, txtaccno.Text, TxtpolicyNo.Text, TxtInsamt.Text, Txtstartdate.Text, Txtexpirydate.Text, Txtclosedate.Text, TxtPremamt.Text, DDlPolstatus.SelectedItem.Text, TxtIname.Text,
                    Txtdesc.Text, DDlEndordement.SelectedItem.Text, Txtsentdate.Text, TxtRecDate.Text, Session["BRCD"].ToString(), "1001", Session["MID"].ToString(), "Modify", ViewState["Id"].ToString(), Session["EntryDate"].ToString());
                if (Result > 0)
                {
                    WebMsgBox.Show("Insurance Details Modified successfully..!!", this.Page);
                  
                    BindGrid("PA");
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Insurance_Mod _" + txttype.Text + "_" + txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    Cleardata();
                    return;

                }
            }
            else if (ViewState["Flag"].ToString() == "AT")
            {
                string AT = IM.GetStage(Session["BRCD"].ToString(), txttype.Text, txtaccno.Text);
                if (AT == "1003")
                {
                    if (ViewState["Flag"].ToString() == "AT")
                    {
                        WebMsgBox.Show("Record Already Authorized!", this.Page);
                        return;
                    }
                }
                Result = IM.AuthoriseData(txttype.Text, txtaccno.Text, Session["BRCD"].ToString(), TxtpolicyNo.Text, "Autho", ViewState["Id"].ToString(), Session["MID"].ToString());
                if (Result > 0)
                {
                    WebMsgBox.Show("Insurance Details Authorised successfully..!!", this.Page);
                   
                    BindGrid("PA");
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Insurance_Autho _" + txttype.Text + "_" + txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    Cleardata();
                    return;

                }
            }

            else if (ViewState["Flag"].ToString() == "DL")
            {
                string AT = IM.GetStage(Session["BRCD"].ToString(), txttype.Text, txtaccno.Text);
                if (AT == "1003")
                {
                    if (ViewState["Flag"].ToString() == "DL")
                    {
                        WebMsgBox.Show("Record Already Authorized, You cannot delete !", this.Page);
                        return;
                    }
                }
                Result = IM.DeleteData(txttype.Text, txtaccno.Text, Session["BRCD"].ToString(), TxtpolicyNo.Text, "Delete", ViewState["Id"].ToString(), Session["MID"].ToString());
                if (Result > 0)
                {
                    WebMsgBox.Show("Insurance Details Deleted successfully..!!", this.Page);
                  
                    BindGrid("PA");
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Insurance_Del _" + txttype.Text + "_" + txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    Cleardata();
                    return;

                }
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btnclear_Click(object sender, EventArgs e)
    {
        Cleardata();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmInsuranceMaster.aspx?FLAG=AD.aspx", true);
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            txttype.Focus();
            lblstatus.Text = "New Entry";
            ViewState["Status"] = "new";
            ViewState["Flag"] = "AD";
            BtnSubmit.Visible = true;
            Flag = "1";
            TblDiv_MainWindow.Visible = true;
            Div_grid.Visible = true;
            Cleardata();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void grdinsurance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdinsurance_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdinsurance_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void LnkAutorise_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Id"] = ARR[0].ToString();
            ViewState["PolicyNo"] = ARR[1].ToString();
            ViewState["PRDCODE"] = ARR[2].ToString();
            ViewState["ACCTNO"] = ARR[3].ToString();
            ViewState["Flag"] = "AT";
            BtnSubmit.Text = "Authorise";
            TblDiv_MainWindow.Visible = true;
            CallEdit();
            Enable(false);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Id"] = ARR[0].ToString();
            ViewState["PolicyNo"] = ARR[1].ToString();
            ViewState["PRDCODE"] = ARR[2].ToString();
            ViewState["ACCTNO"] = ARR[3].ToString();
            ViewState["Flag"] = "DL";
            BtnSubmit.Text = "Delete";
            TblDiv_MainWindow.Visible = true;
            CallEdit();
            Enable(false);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Id"] = ARR[0].ToString();
            ViewState["PolicyNo"] = ARR[1].ToString();
            ViewState["PRDCODE"] = ARR[2].ToString();
            ViewState["ACCTNO"] = ARR[3].ToString();
            ViewState["Flag"] = "MD";
            BtnSubmit.Text = "Modify";
            TblDiv_MainWindow.Visible = true;
            CallEdit();
            EnableMod(false);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void EnableMod(bool TF)
    {
        txttype.Enabled = TF;
        txttynam.Enabled = TF;
        txtaccno.Enabled = TF;
        TxtAccName.Enabled = TF;
    }
    public void CallEdit()
    {
        try
        {
            DT = IM.GetInfo(Session["BRCD"].ToString(), ViewState["PolicyNo"].ToString(), ViewState["Id"].ToString(), ViewState["PRDCODE"].ToString(), ViewState["ACCTNO"].ToString());
            if (DT.Rows.Count > 0)
            {
                txttype.Text = DT.Rows[0]["PRDCODE"].ToString();
                string PDName = customcs.GetProductName(txttype.Text, Session["BRCD"].ToString());
                if (PDName != null)
                {
                    txttynam.Text = PDName;
                }
                txtaccno.Text = DT.Rows[0]["ACCTNO"].ToString();
                string[] AN;
                AN = customcs.GetAccountNme(txtaccno.Text, txttype.Text, Session["BRCD"].ToString()).Split('_');
                if (AN != null)
                {
                    TxtAccName.Text = AN[1].ToString();
                }
                TxtpolicyNo.Text = DT.Rows[0]["PolicyNo"].ToString();
                TxtInsamt.Text = DT.Rows[0]["InstAmt"].ToString();
                Txtstartdate.Text = DT.Rows[0]["Startdate"].ToString().Replace("12:00:00 AM", "");
                Txtexpirydate.Text = DT.Rows[0]["Expirydate"].ToString().Replace("12:00:00 AM", "");
                Txtclosedate.Text = DT.Rows[0]["Closedate"].ToString().Replace("12:00:00 AM", "");
                TxtPremamt.Text = DT.Rows[0]["PremAmt"].ToString();
                DDlPolstatus.SelectedItem.Text = string.IsNullOrEmpty(DT.Rows[0]["PolStatus"].ToString()) ? "0" : DT.Rows[0]["PolStatus"].ToString();
                TxtIname.Text = DT.Rows[0]["InsuranceCom"].ToString();
                Txtdesc.Text = DT.Rows[0]["DescriptionN"].ToString();
                DDlEndordement.SelectedItem.Text = string.IsNullOrEmpty(DT.Rows[0]["EndorsementStatus"].ToString()) ? "0" : DT.Rows[0]["EndorsementStatus"].ToString();
                Txtsentdate.Text = DT.Rows[0]["SentDate"].ToString().Replace("12:00:00 AM", "");
                TxtRecDate.Text = DT.Rows[0]["ReceivedDate"].ToString().Replace("12:00:00 AM", "");
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Cleardata()
    {
        TxtpolicyNo.Text = "";
        TxtInsamt.Text = "";
        Txtstartdate.Text = "";
        Txtexpirydate.Text = "";
        Txtclosedate.Text = "";
        TxtPremamt.Text = "";
        DDlPolstatus.SelectedValue = "0";
        TxtIname.Text = "";
        Txtdesc.Text = "";
        DDlEndordement.SelectedValue = "0";
        Txtsentdate.Text = "";
        TxtRecDate.Text = "";
        txttype.Text = "";
        txttynam.Text = "";
        txtaccno.Text = "";
        TxtAccName.Text = "";


    }
    public void Enable(bool TF)
    {
        TxtpolicyNo.Enabled = TF;
        TxtInsamt.Enabled = TF;
        Txtstartdate.Enabled = TF;
        Txtexpirydate.Enabled = TF;
        Txtclosedate.Enabled = TF;
        TxtPremamt.Enabled = TF;
        DDlPolstatus.Enabled = TF;
        TxtIname.Enabled = TF;
        Txtdesc.Enabled = TF;
        DDlEndordement.Enabled = TF;
        Txtsentdate.Enabled = TF;
        TxtRecDate.Enabled = TF;
        txttype.Enabled = TF;
        txttynam.Enabled = TF;
        txtaccno.Enabled = TF;
        TxtAccName.Enabled = TF;
    }
    protected void TxtpolicyNo_TextChanged(object sender, EventArgs e)
    {
        TxtInsamt.Focus();
    }
    protected void TxtInsamt_TextChanged(object sender, EventArgs e)
    {
        Txtstartdate.Focus();
    }
    protected void Txtstartdate_TextChanged(object sender, EventArgs e)
    {
        Txtexpirydate.Focus();
    }
    protected void Txtexpirydate_TextChanged(object sender, EventArgs e)
    {
        Txtclosedate.Focus();
    }
    protected void Txtclosedate_TextChanged(object sender, EventArgs e)
    {
        TxtPremamt.Focus();
    }
    protected void TxtPremamt_TextChanged(object sender, EventArgs e)
    {
        DDlPolstatus.Focus();
    }
    protected void DDlPolstatus_TextChanged(object sender, EventArgs e)
    {
        TxtIname.Focus();
    }
    protected void TxtIname_TextChanged(object sender, EventArgs e)
    {
        Txtdesc.Focus();
    }
    protected void Txtdesc_TextChanged(object sender, EventArgs e)
    {
        DDlEndordement.Focus();
    }
    protected void DDlEndordement_TextChanged(object sender, EventArgs e)
    {
        Txtsentdate.Focus();
    }
    protected void Txtsentdate_TextChanged(object sender, EventArgs e)
    {
        TxtRecDate.Focus();
    }
    protected void TxtRecDate_TextChanged(object sender, EventArgs e)
    {
        BtnSubmit.Focus();
    }
    protected void txttype_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(txttype.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');
            if (GLCODE[1] == "3")
            {
                ViewState["DRGL"] = GL[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txttype.Text + "_" + ViewState["DRGL"].ToString();
                string PDName = customcs.GetProductName(txttype.Text, Session["BRCD"].ToString());
                if (PDName != null)
                {
                    txttynam.Text = PDName;
                    txtaccno.Focus();
                }
                else
                {
                    WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                    txttype.Text = "";
                    txttype.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter only Loan Product Code....!", this.Page);
                txttype.Text = "";
                txttype.Focus();

            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txttynam_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txttynam.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txttynam.Text = CT[0].ToString();
                txttype.Text = CT[1].ToString();
                txtaccno.Focus();
                string[] GLS = BD.GetAccTypeGL(txttype.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txttype.Text + "_" + ViewState["DRGL"].ToString();
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
            string accn = "";
            string[] AN;
            accn = customcs.GetAccountNme(txtaccno.Text, txttype.Text, Session["BRCD"].ToString());
            if (accn != null)
            {
                AN = customcs.GetAccountNme(txtaccno.Text, txttype.Text, Session["BRCD"].ToString()).Split('_');
                TxtAccName.Text = AN[1].ToString();
                TxtpolicyNo.Focus();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                txtaccno.Text = "";
                txtaccno.Focus();
            }
            CallEdit();
            BindGrid("PA");
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
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
                TxtpolicyNo.Focus();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                txtaccno.Text = "";
                txtaccno.Focus();
            }
            CallEdit();
            BindGrid("PA");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}