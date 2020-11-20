using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class frmInwordAuthoDo : System.Web.UI.Page
{
    ClsOutAuthReturnDo maincode2 = new ClsOutAuthReturnDo();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsOutAuthDo maincode = new ClsOutAuthDo();
    DbConnection conn = new DbConnection();
    ClsOutClear OWGCL = new ClsOutClear();
    scustom customcs = new scustom();
    DataTable dt = new DataTable();
    ClsOpenClose OC = new ClsOpenClose();
    int setNo, scrollNo, InstNo, result;
    string op = "", Message = "";
    double Balance = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            customcs.BindInstruType(ddlinsttype);
        }

        setNo = Convert.ToInt32(Request.QueryString["setno"]);
        scrollNo = Convert.ToInt32(Request.QueryString["scrollno"]);
        InstNo = Convert.ToInt32(Request.QueryString["InstNo"]);
        op = Request.QueryString["op"].ToString();


        if (op == "delete")
        {
            btnAuthorize.Text = "Cancel";
            ViewState["op"] = "delete";
        }
        else if (op == "authorize")
        {
            btnAuthorize.Text = "Authorize";
            ViewState["op"] = "authorize";
        }
        else if (op == "return")
        {
            btnAuthorize.Text = "return";
            ViewState["op"] = "return";
        }
        else if (op == "returnauth")
        {
            btnAuthorize.Text = "returnauth";
            ViewState["op"] = "returnauth";
        }

        if (!IsPostBack)
        {
            if (ViewState["op"] == "delete" || ViewState["op"] == "authorize")
            {
                dt = maincode.GetFormData(setNo, scrollNo, Convert.ToInt32(Session["BRCD"]), Session["EntryDate"].ToString(), "I");
            }

            if (ViewState["op"] == "return" || ViewState["op"] == "returnauth")
            {
                dt = maincode2.GetFormDatareturn(setNo, scrollNo, Convert.ToInt32(Session["BRCD"]), Session["EntryDate"].ToString(), "I");
            }

            if (dt.Rows.Count > 0)
            {
                TxtEntrydate.Text = Convert.ToDateTime(dt.Rows[0]["ENTRYDATE"]).ToString("dd/MM/yyyy");
                txtsetno.Text = dt.Rows[0]["SET_NO"].ToString();
                TxtProcode.Text = dt.Rows[0]["PRDUCT_CODE"].ToString();
                TxtProName.Text = dt.Rows[0]["PRDNAME"].ToString();
                TxtAccNo.Text = dt.Rows[0]["ACC_NO"].ToString();
                TxtAccName.Text = dt.Rows[0]["CUSTNAME"].ToString();
                txtAccTypeid.Text = dt.Rows[0]["ACC_TYPE"].ToString();
                TxtAccTypeName.Text = dt.Rows[0]["ACCTYPEA"].ToString();
                txtOpTypeId.Text = dt.Rows[0]["OPRTN_TYPE"].ToString();
                TxtOpTypeName.Text = dt.Rows[0]["OPRTYPE"].ToString();
                txtpartic.Text = dt.Rows[0]["PARTICULARS"].ToString();
                ddlinsttype.SelectedValue = dt.Rows[0]["INSTRU_TYPE"].ToString();
                txtbankcd.Text = dt.Rows[0]["BANK_CODE"].ToString();
                txtbnkdname.Text = dt.Rows[0]["BANK"].ToString();
                txtbrnchcd.Text = dt.Rows[0]["BRANCH_CODE"].ToString();
                txtbrnchcdname.Text = dt.Rows[0]["BRANCH"].ToString();
                txtinstno.Text = dt.Rows[0]["INSTRU_NO"].ToString();
                txtinstdate.Text = Convert.ToDateTime(dt.Rows[0]["INSTRUDATE"]).ToString("dd-MM-yyyy");
                txtinstamt.Text = dt.Rows[0]["INSTRU_AMOUNT"].ToString();

                string[] TD = TxtEntrydate.Text.Split('/');
                string GL;

                GL = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
                if (GL != null)
                {
                    string[] GL1 = GL.Split('_');
                    string Balance = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GL1[1].ToString()).ToString();
                    string TotalBalance = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GL1[1].ToString()).ToString();
                    if (Balance != null && TotalBalance != null)
                    {
                        txtBalance.Text = Balance.ToString();
                        TxtTotalBal.Text = TotalBalance.ToString();
                    } 
                }

                Photo_Sign(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString());
            }
        }

        TxtEntrydate.Focus();
    }

    protected void btnAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            // Get CLG_GL_NO 
            string CLG_GL_NO = OWGCL.Get_CLG_GL_NO_return(Session["BRCD"].ToString()).ToString();

            // Get PACMAC
            string PACMAC = " ";


            if (ViewState["op"] == "authorize")
            {
                result = maincode.Authorize(1003, setNo, scrollNo, Convert.ToInt32(Session["BRCD"]), Session["MID"].ToString(), Session["EntryDate"].ToString(), "I");
                if (result > 0)
                {
                    //added by amol on 10/08/2017 For Insert record into avs1092 table
                    Balance = BD.ClBalance(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal");
                    Message = "Your Account " + TxtAccNo.Text.Trim().ToString() + " is Debited with INR " + Convert.ToDouble(txtinstamt.Text.Trim().ToString()) + " on " + conn.ConvertToDate(Session["EntryDate"].ToString()).ToString() + " Ref CHQ " + InstNo + " Your Balance is INR " + Balance + " RS.";
                    BD.InsertSMSRec(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Message, Session["MID"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "Payment");

                    WebMsgBox.Show("Record Authorized successfully", this.Page);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('frmInwordAuth.aspx', 'popup_window', 'width=1000,height=400,left=50,top=50,resizable=no');", true);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
                }
                else
                {
                    lblMessage.Text = "Warning..User is restricted from this Operation...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }

            if (ViewState["op"] == "delete")
            {
                result = maincode.cancel(1004, setNo, scrollNo, Convert.ToInt32(Session["BRCD"]), Session["MID"].ToString(), Session["EntryDate"].ToString());
                if (result > 0)
                {
                    WebMsgBox.Show("Record Canceled successfully", this.Page);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closePage", "window.close();", true);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
                }
            }

            if (ViewState["op"] == "return")
            {
                maincode2.returnout("1002", "3", txtsetno.Text.ToString(), scrollNo, Session["BRCD"].ToString(), Session["MID"].ToString(), TxtEntrydate.Text.ToString(), TxtProcode.Text.ToString(), TxtAccNo.Text.ToString(), txtAccTypeid.Text.ToString(), txtOpTypeId.Text.ToString(), txtpartic.Text.ToString(), txtbankcd.Text.ToString(), txtbrnchcd.Text.ToString(), ddlinsttype.SelectedValue.ToString(), txtinstdate.Text.ToString(), txtinstno.Text.ToString(), txtinstamt.Text.ToString(), PACMAC, CLG_GL_NO);

                WebMsgBox.Show("Record Return successfully", this.Page);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);

            }

            if (ViewState["op"] == "returnauth")
            {
                maincode2.returnoutauth("1003", "3", txtsetno.Text, scrollNo, Session["BRCD"].ToString(), Session["MID"].ToString(), TxtEntrydate.Text, TxtProcode.Text, TxtAccNo.Text, txtAccTypeid.Text, txtOpTypeId.Text, txtpartic.Text, txtbankcd.Text, txtbrnchcd.Text, ddlinsttype.SelectedValue.ToString(), txtinstdate.Text, txtinstno.Text, txtinstamt.Text, PACMAC, CLG_GL_NO, "I");
                WebMsgBox.Show("Record Return successfully", this.Page);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Photo_Sign(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            DataTable dt = maincode2.ShowImage(BrCode.ToString(), PrCode.ToString(), AccNo.ToString());
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                String FilePath = "";
                byte[] bytes = null;
                for (int y = 0; y < 2; y++)
                {
                    if (y == 0)
                    {
                        FilePath = dt.Rows[i]["SignIMG"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])dt.Rows[i]["SignIMG"];
                    }
                    else
                    {
                        FilePath = dt.Rows[i]["PhotoImg"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])dt.Rows[i]["PhotoImg"];
                    }
                    if (FilePath != "")
                    {
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        if (y == 0)
                        {
                            ImageSign.Src = "data:image/tif;base64," + base64String;
                        }
                        else if (y == 1)
                        {
                            ImagePhoto.Src = "data:image/tif;base64," + base64String;
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}