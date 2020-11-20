using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class FrmTransfer : System.Web.UI.Page
{
    ClsTransferVoucher TRF = new ClsTransferVoucher();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLoanInfo LI = new ClsLoanInfo();
    ClsAccopen AO = new ClsAccopen();
    ClsAuthorized AT = new ClsAuthorized();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                autoglname.ContextKey = Session["BRCD"].ToString();
                autoproduct.ContextKey = Session["BRCD"].ToString();
                TxtChequeDate.Text = Session["EntryDate"].ToString();
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }
    protected void TxtPname_TextChanged(object sender, EventArgs e)
    {
        string Pname = TxtPname.Text;
        string[] PT = Pname.Split('_');
        if (PT.Length > 1)
        {
            TxtPname.Text = PT[0].ToString();
            TxtPtype.Text = PT[1].ToString();
            TxtGLCD.Text = PT[2].ToString();
            string[] GL = BD.GetAccTypeGL(TxtPtype.Text, Session["BRCD"].ToString()).Split('_');
            TxtPname.Text = GL[0].ToString();
            ViewState["GL"] = GL[1].ToString();
            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPtype.Text + "_" + ViewState["GL"].ToString();
            TxtCRGLCD.Focus();
        }
    }
    protected void TxtCRPTName_TextChanged(object sender, EventArgs e)
    {
        string PCRPName = TxtCRPTName.Text;
        string[] PCRP = PCRPName.Split('_');

        if (PCRP.Length > 1)
        {
            TxtCRPTName.Text = PCRP[0].ToString();
            TxtCRPType.Text = PCRP[1].ToString();
            TxtCRGLCD.Text = PCRP[2].ToString();

            string[] GL = BD.GetAccTypeGL(TxtCRPType.Text, Session["BRCD"].ToString()).Split('_');

            ViewState["CRGL"] = GL[1].ToString();
            Autocrname.ContextKey = Session["BRCD"].ToString() + "_" + TxtCRPType.Text + "_" + ViewState["CRGL"].ToString();
            TxtCRAccNo.Focus();
        }
    }
    protected void TxtPtype_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] GL = BD.GetAccTypeGL(TxtPtype.Text, Session["BRCD"].ToString()).Split('_');
            TxtPname.Text = GL[0].ToString();
            ViewState["GL"] = GL[1].ToString();
            //TxtPname.Text = BD.GetAccType(TxtPtype.Text, Session["BRCD"].ToString());
            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPtype.Text + "_" + ViewState["GL"].ToString();
            TxtAccNo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtCRPType_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string[] GLcredit = BD.GetAccTypeGL(TxtCRPType.Text, Session["BRCD"].ToString()).Split('_');
            TxtCRPTName.Text = GLcredit[0].ToString();
            ViewState["CRGL"] = GLcredit[1].ToString();
            Autocrname.ContextKey = Session["BRCD"].ToString() + "_" + TxtCRPType.Text + "_" + ViewState["CRGL"].ToString();
            TxtCRAccNo.Focus();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void TxtCustName_TextChanged(object sender, EventArgs e)
    {
        string CustName = TxtCustName.Text;
        string[] CN = CustName.Split('_');

        if (CN.Length > 1)
        {
            TxtCustName.Text = CN[0].ToString();
            TxtAccNo.Text = CN[1].ToString();

            int RC = LI.CheckAccount(TxtAccNo.Text, TxtPtype.Text, Session["BRCD"].ToString());
            if (RC < 0)
            {
                TxtAccNo.Focus();
                WebMsgBox.Show("Please Enter valide Account Number Account Not Exist..........!!", this.Page);
                return;
            }
            ViewState["CustNo"] = RC;
            TxtCRGLCD.Focus();
        }
    }
    protected void TxtCRACName_TextChanged(object sender, EventArgs e)
    {
        string CRACName = TxtCRACName.Text;
        string[] CRACN = CRACName.Split('_');

        if (CRACN.Length > 1)
        {
            TxtCRACName.Text = CRACN[0].ToString();
            TxtCRAccNo.Text = CRACN[1].ToString();


            int RC = LI.CheckAccount(TxtCRAccNo.Text, TxtCRPType.Text, Session["BRCD"].ToString());
            if (RC < 0)
            {
                TxtAccNo.Focus();
                WebMsgBox.Show("Please Enter valide Account Number Account Not Exist..........!!", this.Page);
                return;
            }
            ViewState["CRCustNo"] = RC;
            TxtChequeNo.Focus();
        }
    }
    protected void TxtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            // AT = BD.GetStage1(TxtAccno.Text, Session["BRCD"].ToString(), ViewState["Flag"].ToString());
            AT = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), TxtPtype.Text);
            if (AT != "1003")
            {
                lblMessage.Text = "Sorry Customer not Authorise.........!!";
                ModalPopup.Show(this.Page);
                ClearText();
                TxtPtype.Focus();
            }
            else
            {
                int RC = LI.CheckAccount(TxtAccNo.Text, TxtPtype.Text, Session["BRCD"].ToString());

                if (RC < 0)
                {
                    TxtAccNo.Focus();
                    WebMsgBox.Show("Please Enter valide Account Number Account Not Exist..........!!", this.Page);
                    return;
                }
                ViewState["CustNo"] = RC;
                TxtCustName.Text = AO.Getcustname(RC.ToString());
                TxtCRGLCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtCRAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            // AT = BD.GetStage1(TxtAccno.Text, Session["BRCD"].ToString(), ViewState["Flag"].ToString());
            AT = BD.Getstage1(TxtCRAccNo.Text, Session["BRCD"].ToString(), TxtCRPType.Text);
            if (AT != "1003")
            {
                lblMessage.Text = "Sorry Customer not Authorise.........!!";
                ModalPopup.Show(this.Page);
                ClearText();
                TxtPtype.Focus();
            }
            else
            {
                int RC = LI.CheckAccount(TxtCRAccNo.Text, TxtCRPType.Text, Session["BRCD"].ToString());

                if (RC < 0)
                {
                    TxtCRAccNo.Focus();
                    WebMsgBox.Show("Please Enter valide Account Number Account Not Exist..........!!", this.Page);
                    return;
                }
                ViewState["CRCustNo"] = RC;
                TxtCRACName.Text = AO.Getcustname(RC.ToString());
                TxtChequeNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void SubmitTemp_Click(object sender, EventArgs e)
    {
        int RC = 0;
        string ST = "";
        string AC, CRAC, CN, CD;
        AC = CRAC = CN = CD = "";
        ST = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
        int R = Convert.ToInt32(ST);
       // int R = BD.SetSetno(Session["EntryDate"].ToString(), "DaySetNo", ST);
        if (ViewState["CustNo"] == null)
        {
            ViewState["CustNo"] = "0";
        }
        if (TxtAccNo.Text == "")
        {
            AC = "0";
            ViewState["CustNo"] = "0";
        }
        else
        {
            AC = TxtAccNo.Text;
        }

        if (TxtCRAccNo.Text == "")
        {
            CRAC = "0";
            ViewState["CRCustNo"] = "0";
        }
        else
        {
            CRAC = TxtCRAccNo.Text;
        }

        if (TxtChequeNo.Text == "")
        {
            CN = "0";
        }
        else
        {
            CN = TxtChequeNo.Text;
        }
        if (TxtChequeDate.Text == "")
        {
            CD = "01/01/1900";
        }
        else
        {
            CD = TxtChequeDate.Text;
        }

        RC = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString(), TxtPtype.Text, AC, TxtNarration.Text, "Tr From " + TxtCRPType.Text + "/" + CRAC, TxtCRAmount.Text, "2", "7", "TR", ST, CN, CD, "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TRANSFER", ViewState["CustNo"].ToString(), TxtCustName.Text, "0", "0");
        if (RC > 0)
        {
            RC = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["CRGL"].ToString(), TxtCRPType.Text, CRAC, TxtNarration.Text, "Tr From " + TxtCRPType.Text + "/" + CRAC, TxtCRAmount.Text, "1", "7", "TR", ST, CN, CD, "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TRANSFER", ViewState["CRCustNo"].ToString(), TxtCustName.Text, "0", "0");
            if (RC > 0)
            {
                //int R = BD.SetSetno(Session["EntryDate"].ToString(), "DaySetNo", ST);
                if (R > 0)
                {
                    lblMessage.Text = " Voucher Added Successfully and voucher no is " + ST;
                    ModalPopup.Show(this.Page);
                }
            }
        }
        ClearText();
        BindGrid();
        TxtGLCD.Focus();
    }

    public void BindGrid()
    {
        try
        {
            int retrow = 0;
            DataTable dt = new DataTable();
            dt = TRF.Transfer(Session["BRCD"].ToString(), TxtGLCD.Text, Session["UserName"].ToString());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    grdTransfer.DataSource = dt;
                    grdTransfer.DataBind();
                    retrow = dt.Rows.Count;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    public void ClearText()
    {
        TxtPtype.Text = "";
        TxtPname.Text = "";
        TxtAccNo.Text = "";
        TxtCustName.Text = "";
        TxtCRPType.Text = "";
        TxtCRPTName.Text = "";
        TxtCRAccNo.Text = "";
        TxtCRACName.Text = "";
        TxtCRAmount.Text = "";
        TxtCRGLCD.Text = "";
        TxtGLCD.Text = "";
        TxtChequeNo.Text = "";
        TxtNarration.Text = "";

    }

}
