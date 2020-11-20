using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmS0003 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int result = 0;
    string sroname = "", AC_Status = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            autoglname.ContextKey = Session["brcd"].ToString();
            ViewState["FLAG"] = "AD";
            if (ViewState["FLAG"].ToString() == "AD")
            {
                lblActivity.Text = "Add File";


                txtBrcd.Text = Session["BRCD"].ToString();
                txtBrcdname.Text = Session["BName"].ToString();
                DT = SRO.GetCaseYearFile(Session["EntryDate"].ToString());
                if (DT.Rows.Count > 0)
                {
                    txtCaseNO.Text = DT.Rows[0]["CASENO"].ToString();
                    txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
                }
            }

        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["FLAG"].ToString() == "AD")
            {
                result = SRO.InsertFile(ENTRYDATE: Session["EntryDate"].ToString(), BRCD: txtBrcd.Text, CASENO: txtCaseNO.Text, CASEY: txtCaseY.Text, SRNO: txtSRo.Text, FILE_STATUS: txtFilestatus.Text, F_DATE: TxtFdate.Text, F_REEAMRKS: TXTREMARKS.Text, NEXT_F_DT: txtnextfile.Text,STATUS: "", MID: Session["MID"].ToString(),MEMBERNO:txtMemberNo.Text);
                if (result > 0)
                {
                    WebMsgBox.Show("Data Saved successfully..!!", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_FileAssign_Add _" + txtCaseNO.Text + "_" + txtCaseY.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    clear();
                }
            }
            else if (ViewState["FLAG"].ToString() == "MD")
            {
                result = SRO.ModifyFile(txtBrcd.Text, txtCaseNO.Text, txtCaseY.Text, txtSRo.Text, txtFilestatus.Text, TxtFdate.Text, TXTREMARKS.Text, txtnextfile.Text, "", Session["MID"].ToString(),txtMemberNo.Text);
                if (result > 0)
                {
                    WebMsgBox.Show("Data Modified successfully..!!", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_FileAssign_Mod _" + txtCaseY.Text + "_" + txtCaseNO.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    clear();
                }
            }
            else if (ViewState["FLAG"].ToString() == "AT")
            {
                result = SRO.AuthoriseFile(txtBrcd.Text, txtCaseNO.Text, txtCaseY.Text,  Session["MID"].ToString());
                if (result > 0)
                {
                    WebMsgBox.Show("Data Authorised successfully..!!", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_FileAssign_Auth _" + txtCaseY.Text + "_" + txtCaseNO.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    clear();
                }
                else
                {

                    WebMsgBox.Show("Warning: User is restricted to perform this operation..........!!", this.Page);

                }
            }
            else if (ViewState["FLAG"].ToString() == "CA")
            {
                result = SRO.CancelFile(txtBrcd.Text, txtCaseNO.Text, txtCaseY.Text,Session["MID"].ToString());
                if (result > 0)
                {
                    WebMsgBox.Show("Data Canceled successfully..!!", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_FileAssign_Cancel _" + txtCaseY.Text + "_" + txtCaseNO.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    clear();
                }
            }
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
    protected void txtBrcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBrcd.Text != "")
            {
                string bname = AST.GetBranchName(txtBrcd.Text);
                if (bname != null)
                {
                    txtBrcdname.Text = bname;
                    txtCaseNO.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    txtBrcd.Text = "";
                    txtBrcd.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                txtBrcd.Text = "";
                txtBrcd.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtCaseNO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["FLAG"].ToString() != "AD")
            {
                ViewDetails(txtBrcd.Text, txtCaseNO.Text, txtCaseY.Text);

            }
          
            TxtFdate.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
  
    public void ViewDetails(string BRCD, string PRDCD, string ACCNO)
    {
        try
        {
            DT = SRO.ViewDetailsFile(txtBrcd.Text, txtCaseNO.Text, txtCaseY.Text);
            if (DT.Rows.Count > 0)
            {
                txtMemberNo.Text = DT.Rows[0]["MEMBERNO"].ToString();
                txtmemname.Text = SRO.GetMemberID(txtMemberNo.Text);
                txtSRo.Text = DT.Rows[0]["SRNO"].ToString();
                TXTSROName.Text = SRO.GetSROName(txtSRo.Text);
                txtFilestatus.Text = DT.Rows[0]["FILE_STATUS"].ToString();
                TxtFdate.Text = DT.Rows[0]["F_DATE"].ToString();
                TXTREMARKS.Text = DT.Rows[0]["F_REEAMRKS"].ToString();
                txtnextfile.Text = DT.Rows[0]["NEXT_F_DT"].ToString();
            }
            else
            {
                WebMsgBox.Show("No record found..!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "AD";
            clear();
            BtnSubmit.Text = "Submit";
            lblActivity.Text = "Add File";
          
            TXTSROName.Enabled = false;
            DT = SRO.GetCaseYearFile(Session["EntryDate"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtCaseNO.Text = DT.Rows[0]["CASENO"].ToString();
                txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
            }

            ENTF(true);
          
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "CA";
            clear();
            BtnSubmit.Text = "Cancel";
            lblActivity.Text = "Cancel File";
            ENTF(false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void lnkAuthorized_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "AT";
            clear();
            BtnSubmit.Text = "Authorise";
            lblActivity.Text = "Authorise File";
            ENTF(false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void lnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "MD";
            clear();
            BtnSubmit.Text = "Modify";
            lblActivity.Text = "Modify File";
            ENTF(true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    public void clear()
    {
        if (ViewState["FLAG"].ToString() == "AD")
        {
            txtBrcd.Text = Session["BRCD"].ToString();

        }
        else
            txtBrcd.Text = "";
        if (ViewState["FLAG"].ToString() == "AD")
        {
            txtBrcdname.Text = Session["BName"].ToString();

        }
        else
            txtBrcdname.Text = "";
        if (ViewState["FLAG"].ToString() == "AD")
        {

            DT = SRO.GetCaseYearFile(Session["EntryDate"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtCaseNO.Text = DT.Rows[0]["CASENO"].ToString();
                txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
            }
        }
        else

        txtCaseNO.Text = "";
        if (ViewState["FLAG"].ToString() == "AD")
        {

            DT = SRO.GetCaseYearFile(Session["EntryDate"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtCaseNO.Text = DT.Rows[0]["CASENO"].ToString();
                txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
            }
        }
        else
        txtCaseY.Text = "";
       
        txtSRo.Text = "";
        txtFilestatus.Text = "";
        TxtFdate.Text = "";
        TXTREMARKS.Text = "";
        txtnextfile.Text = "";
        TXTSROName.Text = "";
        txtMemberNo.Text = "";
        txtmemname.Text = "";

    }
    public bool ENTF(bool TF)
    {
        try
        {
            txtFilestatus.Enabled = TF;
            TxtFdate.Enabled = TF;
            TXTREMARKS.Enabled = TF;
            txtnextfile.Enabled = TF;
            TXTSROName.Enabled = TF;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return TF;
    }


    protected void txtmemname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtmemname.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtmemname.Text = CT[0].ToString();
                txtMemberNo.Text = CT[1].ToString();


            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtMemberNo_TextChanged(object sender, EventArgs e)
    {
        txtmemname.Text = SRO.GetMemberID(txtMemberNo.Text);
    }
    protected void txtSRo_TextChanged(object sender, EventArgs e)
    {
        try
        {

            sroname = SRO.GetSROName(txtSRo.Text);
            TXTSROName.Text = sroname;
            txtCaseY.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}