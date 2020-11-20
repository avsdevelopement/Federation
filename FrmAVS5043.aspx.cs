using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS5043 : System.Web.UI.Page
{
    scustom customcs = new scustom();
    ClsCommon CC = new ClsCommon();
     ClsBindDropdown BD = new ClsBindDropdown();
     ClsAVS5043 avs5043 = new ClsAVS5043();
     ClsAccountSTS AST = new ClsAccountSTS();
     ClsLoanInfo LI = new ClsLoanInfo();
    string AC_Status="";
    int result = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                TxtBrcd.Text = Session["BRCD"].ToString();
                TxtBrcdName.Text = AST.GetBranchName(TxtBrcd.Text);
                automemname.ContextKey = TxtBrcd.Text;
                autoglname.ContextKey = TxtBrcd.Text;
                bindgrid();
                TxtMemNo.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtMemNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtMemName.Text = BD.GetMemName(TxtMemNo.Text);
            if (TxtMemName.Text == "")
            {
                WebMsgBox.Show("Enter Valid Member No..!!", this.Page);
                TxtMemNo.Text = "";
                TxtMemName.Text = "";
                TxtMemNo.Focus();
                return;
            }
            TxtBrcd.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtMemName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string MMNAME = TxtMemName.Text;
            string[] memnob = MMNAME.Split('_');

            if (memnob.Length > 1)
            {
                TxtMemName.Text = memnob[0].ToString();
                TxtMemNo.Text = (string.IsNullOrEmpty(memnob[1].ToString()) ? "" : memnob[1].ToString());
            }
            else {
                WebMsgBox.Show("Enter valid Member Name..!!", this.Page);
                TxtMemNo.Text = "";
                TxtMemName.Text = "";
                TxtMemName.Focus();
                return;
            }
            TxtBrcd.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            result = avs5043.insert(ViewState["GLCODE"].ToString(), TxtPcd.Text, ViewState["CUSTNO"].ToString(), TxtAccno.Text, "4", avs5043.getSubgl(TxtMemNo.Text).Split('_')[0].ToString(), avs5043.getSubgl(TxtMemNo.Text).Split('_')[1].ToString(), TxtMemNo.Text, Session["MID"].ToString(), Session["EntryDate"].ToString(), TxtBrcd.Text);
            if (result > 0)
            {
                WebMsgBox.Show("Record Added Successfully....!!", this.Page);
                bindgrid();
                clear();
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
    protected void BtnModify_Click(object sender, EventArgs e)
    {
        try
        {
            result = avs5043.Modify(ViewState["ID"].ToString(), ViewState["GLCODE"].ToString(), TxtPcd.Text, ViewState["CUSTNO"].ToString(), TxtAccno.Text, "4", avs5043.getSubgl(TxtMemNo.Text).Split('_')[0].ToString(), avs5043.getSubgl(TxtMemNo.Text).Split('_')[1].ToString(), TxtMemNo.Text, Session["MID"].ToString(), Session["EntryDate"].ToString(), TxtBrcd.Text);
            if (result > 0)
            {
                WebMsgBox.Show("Record Modified Successfully....!!", this.Page);
                bindgrid();
                clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            result = avs5043.Delete(ViewState["ID"].ToString(), TxtBrcd.Text);
            if (result > 0)
            {
                WebMsgBox.Show("Record Deleted Successfully....!!", this.Page);
                bindgrid();
                clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnAuthorise_Click(object sender, EventArgs e)
    {
        try
        {
            result = avs5043.Delete(ViewState["ID"].ToString(), TxtBrcd.Text);
            if (result > 0)
            {
                WebMsgBox.Show("Record Authorised Successfully....!!", this.Page);
                bindgrid();
                clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtBrcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBrcd.Text != "")
            {
                string bname = AST.GetBranchName(TxtBrcd.Text);
                if (bname != null)
                {
                    TxtBrcdName.Text = bname;
                    automemname.ContextKey = TxtBrcd.Text;
                    autoglname.ContextKey = TxtBrcd.Text;
                    TxtPcd.Focus();
                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtBrcd.Text = "";
                    TxtBrcd.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtBrcd.Text = "";
                TxtBrcd.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtPcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtPcdName.Text = customcs.GetProductName(TxtPcd.Text.ToString(), TxtBrcd.Text);
            if (TxtPcdName.Text == "")
            {
                WebMsgBox.Show("Enter Valid Product code..!!", this.Page);
                TxtPcd.Text = "";
                TxtPcdName.Text = "";
                TxtPcd.Focus();
            }
            AutoAccname.ContextKey = TxtBrcd.Text + "_" + TxtPcd.Text.ToString();
            TxtAccno.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtPcdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtPcdName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtPcdName.Text = CT[0].ToString();
                TxtPcd.Text = CT[1].ToString();
                ViewState["GLCODE"] = CT[2].ToString();
                AutoAccname.ContextKey = TxtBrcd.Text + "_" + TxtPcd.Text.ToString();
            }
            else {
                    WebMsgBox.Show("Enter Valid Product Name..!!", this.Page);
                    TxtPcd.Text = "";
                    TxtPcdName.Text = "";
                    TxtPcdName.Focus();
            }
            TxtAccno.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtAccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int Result = LI.CheckAccountExist(TxtAccno.Text, TxtPcd.Text, Session["BRCD"].ToString());
            if (Result == 0)
            {
                WebMsgBox.Show("Sorry Account Number Not Exist...!!", this.Page);
                TxtAccno.Text = "";
                TxtAccName.Text = "";
                TxtAccno.Focus();
                return;
            }
              string AT = "";
            AC_Status = CC.GetAccStatus(TxtBrcd.Text, TxtPcd.Text, TxtAccno.Text);
            if (AC_Status == "1")
            {
                AT = BD.Getstage1(TxtAccno.Text, TxtBrcd.Text, TxtPcd.Text);
                if (AT != null)
                {
                    if (AT != "1003")
                    {
                        lblMessage.Text = "Sorry Customer not Authorise.........!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                    else
                    {
                        TxtAccName.Text = CC.GetCustNameAc(TxtAccno.Text, TxtBrcd.Text, TxtPcd.Text);
                        ViewState["GLCODE"] = avs5043.getgldr(TxtAccno.Text, TxtPcd.Text, TxtBrcd.Text).Split('_')[0].ToString();
                        ViewState["CUSTNO"] = avs5043.getgldr(TxtAccno.Text, TxtPcd.Text, TxtBrcd.Text).Split('_')[1].ToString();
                        BtnSubmit.Focus();
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
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
                AC_Status = CC.GetAccStatus(TxtBrcd.Text, TxtPcd.Text, custnob[1].ToString());
                if (AC_Status == "1")
                {
                    TxtAccName.Text = custnob[0].ToString();
                    TxtAccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                    ViewState["CUSTNO"] = custnob[2].ToString();
                    ViewState["GLCODE"] = avs5043.getgldr(TxtAccno.Text, TxtPcd.Text, TxtBrcd.Text).Split('_')[0].ToString();
                    BtnSubmit.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    public void clear()
    {
        try
        {
            TxtMemNo.Text = "";
            TxtMemName.Text = "";
            TxtBrcd.Text = "";
            TxtBrcdName.Text = "";
            TxtAccno.Text = "";
            TxtAccName.Text = "";
            TxtPcd.Text = "";
            TxtPcdName.Text = "";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkMod_Click(object sender, EventArgs e)
    {
        try
        {
            BtnSubmit.Visible = false;
            BtnModify.Visible = true;
            BtnDelete.Visible = false;
            BtnAuthorise.Visible = false;
             LinkButton objlink = (LinkButton)sender;
             string strnumId = objlink.CommandArgument;
             ViewState["ID"] = strnumId.ToString();
             viewdetails(ViewState["ID"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkDel_Click(object sender, EventArgs e)
    {
        try
        {
            BtnSubmit.Visible = false;
            BtnModify.Visible = false;
            BtnDelete.Visible = true;
            BtnAuthorise.Visible = false;
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            viewdetails(ViewState["ID"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkAuth_Click(object sender, EventArgs e)
    {
        try
        {
            BtnSubmit.Visible = false;
            BtnModify.Visible = false;
            BtnDelete.Visible = false;
            BtnAuthorise.Visible = true;
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            viewdetails(ViewState["ID"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void viewdetails(string id)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = avs5043.getdetails(id);
            if (dt.Rows.Count > 0)
            {
                TxtMemNo.Text = dt.Rows[0]["DRACCNO"].ToString();
                TxtMemName.Text = BD.GetMemName(TxtMemNo.Text);
                TxtBrcd.Text = dt.Rows[0]["BRCD"].ToString();
                TxtBrcdName.Text = AST.GetBranchName(TxtBrcd.Text);
                TxtAccno.Text = dt.Rows[0]["CRACCNO"].ToString();
                TxtAccName.Text = CC.GetCustNameAc(TxtAccno.Text, TxtBrcd.Text, TxtPcd.Text);
                TxtPcd.Text = dt.Rows[0]["CRSUBGL"].ToString();
                TxtPcdName.Text = customcs.GetProductName(TxtPcd.Text.ToString(), TxtBrcd.Text);
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
            clear();
            BtnSubmit.Visible = true;
            BtnModify.Visible = false;
            BtnDelete.Visible = false;
            BtnAuthorise.Visible = false;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void bindgrid()
    {
        try
        {
            avs5043.binddetails(GrdDivTrf);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void GrdDivTrf_PageIndexChanged(object sender, EventArgs e)
    {

    }
}