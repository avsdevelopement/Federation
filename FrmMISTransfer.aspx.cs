using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmMISTransfer : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsFDIntCalculation FD = new ClsFDIntCalculation();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsMISTransfer MT = new ClsMISTransfer();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    string Skip = "";
    string STR = "";
    int Result = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            autoglname.ContextKey = Session["BRCD"].ToString();
            autoglname1.ContextKey = Session["BRCD"].ToString();
            TxtAsOnDate.Text = Session["EntryDate"].ToString();
            TxtFBRCD.Text = Session["BRCD"].ToString();
            TxtTBRCD.Text = Session["BRCD"].ToString();
            TxtFBRCDName.Text = AST.GetBranchName(TxtFBRCD.Text);
            TxtTBRCDName.Text = AST.GetBranchName(TxtTBRCD.Text);
            TxtFPRD.Focus();

        }
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
    }
    public void Clear()
    {
       // TxtAsOnDate.Text = "";
        TxtFBRCD.Text = "";
        TxtFBRCDName.Text = "";
        TxtTBRCD.Text = "";
        TxtTBRCDName.Text = "";
        TxtFPRD.Text = "";
        TxtTPRD.Text = "";
        TxtFPRDName.Text = "";
        TXtTPRDName.Text = "";
        TxtFBRCD.Focus();
    }
    public void BINDGRID()
    {
        try
        {
            Result = MT.GetMISTRF(GrdFDInt, TxtFBRCD.Text,TxtTBRCD.Text, TxtFPRD.Text, TxtAsOnDate.Text, "TRAILSHOW");
            if (Result < 0)
            {
                WebMsgBox.Show("Transfer not Authorized or available....!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), TxtFPRD.Text.Trim().ToString()).ToString() != "3")
            {
                string tds = BD.GetLoanGL(TxtFPRD.Text, Session["BRCD"].ToString());
                if (tds != null)
                {
                    string[] TD = tds.Split('_');

                    STR = MT.GetDepositCat(Session["BRCD"].ToString(), TxtFPRD.Text, "MISTRF");
                    if (STR == "MIS" || STR == "QIS")
                    {
                        if (TD.Length > 1)
                        {

                        }
                        TxtFPRDName.Text = TD[0].ToString();
                        TXtTPRDName.Text = TD[0].ToString();
                        TxtTPRD.Text = TxtFPRD.Text;
                        TrailEntry.Focus();
                    }
                    else
                    {
                        WebMsgBox.Show("Invalid Deposit Code,Enter MIS/QIS code only...!", this.Page);
                        Clear();
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("Invalid Deposit Code......!", this.Page);
                    Clear();
                    return;
                }
            }
            else
            {
                TxtFPRD.Text = "";
                TxtFPRDName.Text = "";
                lblMessage.Text = "Product is not operating...!!";
                ModalPopup.Show(this.Page);
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), TxtTPRD.Text.Trim().ToString()).ToString() != "3")
            {
                string tds = BD.GetLoanGL(TxtTPRD.Text, Session["BRCD"].ToString());
                if (tds != null)
                {
                    string[] TD = tds.Split('_');
                    STR = MT.GetDepositCat(Session["BRCD"].ToString(), TxtFPRD.Text, "MISTRF");
                    if (STR == "MIS" || STR == "QIS")
                    {
                        if (TD.Length > 1)
                        {

                        }
                        //TXtTPRDName.Text = TD[0].ToString();
                        //TxtFAcc.Focus();
                        TxtFPRDName.Text = TD[0].ToString();
                        TXtTPRDName.Text = TD[0].ToString();
                        TxtFPRD.Text = TxtFPRD.Text;
                        TrailEntry.Focus();
                    }
                    else
                    {
                        WebMsgBox.Show("Invalid Deposit Code,Enter  MIS/QIS code only...!", this.Page);
                        Clear();
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("Invalid Deposit Code...!", this.Page);
                    Clear();
                    return;
                }
            }
            else
            {
                TxtTPRD.Text = "";
                TXtTPRDName.Text = "";
                lblMessage.Text = "Product is not operating...!!";
                ModalPopup.Show(this.Page);
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFAcc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtFAccName.Text = BD.AccName(TxtFAcc.Text, TxtFPRD.Text, TxtTPRD.Text, Session["BRCD"].ToString());
            TxtTAcc.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTAcc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtTAccName.Text = BD.AccName(TxtTAcc.Text, TxtFPRD.Text, TxtTPRD.Text, Session["BRCD"].ToString());
            TrailEntry.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtFBRCD.Text);
                if (bname != null)
                {
                    TxtFBRCDName.Text = bname;
                    TxtTBRCD.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtFBRCD.Text = "";
                    TxtFBRCD.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtFBRCD.Text = "";
                TxtFBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtTBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtTBRCD.Text);
                if (bname != null)
                {
                    TxtTBRCDName.Text = bname;
                    TxtFPRD.Focus();

                    if (Convert.ToInt32(TxtFBRCD.Text) > Convert.ToInt32(TxtTBRCD.Text))
                    {
                        WebMsgBox.Show("Invalid FROM and TO Branch Code....!", this.Page);
                        Clear();
                        return;
                    }


                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtTBRCD.Text = "";
                    TxtTBRCD.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtTBRCD.Text = "";
                TxtTBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


   
    protected void TxtFPRDName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtFPRDName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), CT[1].ToString()).ToString() != "3")
                {
                    STR = MT.GetDepositCat(Session["BRCD"].ToString(), CT[1].ToString(), "MISTRF");
                    if (STR == "MIS")
                    {
                        TxtFPRDName.Text = CT[0].ToString();
                        TxtFPRD.Text = CT[1].ToString();
                        //TxtGLCD.Text = CT[2].ToString();
                        string[] GLS = BD.GetAccTypeGL(TxtFPRD.Text, Session["BRCD"].ToString()).Split('_');
                        ViewState["DRGL"] = GLS[1].ToString();
                        //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();

                        if (TxtFPRDName.Text == "")
                        {
                            WebMsgBox.Show("Please enter valid Product code", this.Page);
                            TxtFPRD.Text = "";
                            TxtFPRD.Focus();

                        }
                        else
                        {
                            TxtTPRD.Focus();
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("Invalid Deposit Code,Enter MIS code only...!", this.Page);
                        Clear();
                        return;
                    }
                }
                else
                {
                    TxtFPRD.Text = "";
                    TxtFPRDName.Text = "";
                    lblMessage.Text = "Product is not operating...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }

    }
    protected void TXtTPRDName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TXtTPRDName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), CT[1].ToString()).ToString() != "3")
                {
                    STR = MT.GetDepositCat(Session["BRCD"].ToString(), CT[1].ToString(), "MISTRF");
                    if (STR == "MIS")
                    {
                        TXtTPRDName.Text = CT[0].ToString();
                        TxtTPRD.Text = CT[1].ToString();
                        //TxtGLCD.Text = CT[2].ToString();
                        string[] GLS = BD.GetAccTypeGL(TxtTPRD.Text, Session["BRCD"].ToString()).Split('_');
                        ViewState["DRGL"] = GLS[1].ToString();
                        //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();

                        if (TXtTPRDName.Text == "")
                        {
                            WebMsgBox.Show("Please enter valid Product code", this.Page);
                            TxtTPRD.Text = "";
                            TxtTPRD.Focus();

                        }
                        else
                        {
                            TxtFAcc.Focus();
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("Invalid Deposit Code,Enter MIS code only...!", this.Page);
                        Clear();
                        return;
                    }
                }
                else
                {
                    TxtTPRD.Text = "";
                    TXtTPRDName.Text = "";
                    lblMessage.Text = "Product is not operating...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void TrailEntry_Click(object sender, EventArgs e)
    {
        try
        {
            BINDGRID();
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "MIS_Int_TrailEntry _" + TxtFPRD.Text + "_" + TxtTPRD.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ApplyEntry_Click(object sender, EventArgs e)
    {
        try
        {
            string RC = "";
            int value;
            RC = MT.POSTMISTRF(TxtFBRCD.Text,TxtTBRCD.Text, TxtFPRD.Text, TxtAsOnDate.Text, "POST",Session["MID"].ToString());
            if (int.TryParse(RC, out value))
            {
                if (Convert.ToInt32(RC) > 0)
                {
                    WebMsgBox.Show("Transferred Succesfully, Voucher Number is ." + RC, this.Page);
                    Clear();
                    GrdFDInt.DataSource = null;
                    GrdFDInt.DataBind();

                    //BINDGRID();
                }

            }
            else
            {
                WebMsgBox.Show(RC, this.Page);
                Clear();
                GrdFDInt.DataSource = null;
                GrdFDInt.DataBind();
            }
        }
       
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
}