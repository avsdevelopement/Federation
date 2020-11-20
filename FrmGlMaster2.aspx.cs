using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

public partial class FrmGlMaster2 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsGLMaster GL = new ClsGLMaster();
    DbConnection conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    int res, RE11, RES12, loangl, DEPOSIT;
    string FL = "", aaa = "";
    private int DepositUpdateRes = 0, LoanUpdateRes = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (DropDownList1.SelectedValue == "0")
            {
                txtsubglmarathi.Font.Name = "Shivaji01";
                txtsubglmarathi.Font.Size = 18;

            }

            else
                if (DropDownList1.SelectedValue == "1")
                {

                    txtsubglmarathi.Font.Name = "SARJUDAS";
                    txtsubglmarathi.Font.Size = 18;

                }
                if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");

            if (!IsPostBack)
            {
                DIV_DATE.Visible = false;
                txtglg.Visible = false;
                ddlbsFormat.Visible = false;
                PLGroup.Visible = false;
                BD.Bindglname(DDLGLCAT);
                BD.bindbsformat(ddlbsFormat);
                BD.BindGlGroup(txtglg);
                BD.BindPlGroup(DDLPLgroup);
                HdnValue.Value = "Y";
               // rdbUnificationYes.Attributes.Add("onclick", "clientJsFunction()");
                ViewState["Flag"] = Request.QueryString["Flag"].ToString();
                if (Convert.ToString(ViewState["Flag"]) == "AD")
                {
                    DivBRCDUni.Visible = false;
                    txtsubglcode.Enabled = false;
                    btnSubmit.Text = "ADD";
                    btnclear.Visible = true;
                    txtlastno.Enabled = true;
                    exit.Visible = true;
                }
                else if (Convert.ToString(ViewState["Flag"]) == "MD")
                {
                    DivBRCDUni.Visible = true;
                    TXTRECINT.Enabled = false;
                    TXTRECINTNAME.Enabled = false;
                    TXTPLNO.Enabled = false;
                    TXTPLNAME.Enabled = false;
                    txtsubglcode.Enabled = true;
                    btnSubmit.Text = "MODIFY";
                    txtlastno.Enabled = false;
                    btnclear.Visible = true;
                    exit.Visible = true;
                }
                else if (Convert.ToString(ViewState["Flag"]) == "DL")
                {
                    DivBRCDUni.Visible = false;

                    txtsubglcode.Enabled = true;
                    btnSubmit.Text = "DELETE";
                    btnclear.Visible = true;
                    txtlastno.Enabled = false;
                    exit.Visible = true;
                }
                DDLGLCAT.Focus();
            }

            if (IsPostBack)
            {
                lblAddMessage.Text = "";
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 900000;
        }


            
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtsubglcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable DT = new DataTable();
            string VAL = txtsubglcode.Text;
            string contentStr = "";

            contentStr = (ViewState["Flag"].ToString() ?? string.Empty).ToString();
            if (DDLGLCAT.SelectedItem.Text == "Loan GL")
            {
                DIV_DATE.Visible = true;
                DT = GL.GETLOAN(Session["BRCD"].ToString(), txtsubglcode.Text);
                if (DT.Rows.Count > 0)
                {
                    if (string.Equals("MD", contentStr, StringComparison.OrdinalIgnoreCase))//added by ambika mam to drop down not get modified
                    {
                        DDLGLCAT.SelectedValue = GL.GETGlName(DT.Rows[0]["GLCODE"].ToString());
                        DDLGLCAT.Enabled = false;
                    }
                    else
                    {
                        DDLGLCAT.Enabled = true;
                    }
                    txtglg.SelectedItem.Text = Convert.ToString(DT.Rows[0]["LOANCATEGORY"]);
                    txtglcode.Text = Convert.ToString(DT.Rows[0]["GLCODE"]);
                    TXTSUBGLNAME.Text = Convert.ToString(DT.Rows[0]["GLNAME"]);
                    txtplaccno.Text = Convert.ToString(DT.Rows[0]["PLACCNO"]);
                    TXTPLACCNAME.Text = GL.GETPLNAME(Session["BRCD"].ToString(), txtplaccno.Text);

                    txtsubglmarathi.Text = DT.Rows[0]["GLMarathi"].ToString();
                    txtglgroup.Text = Convert.ToString(DT.Rows[0]["GLGROUP"]);
                    TXTRECINT.Text = Convert.ToString(DT.Rows[0]["IRCODE"]);
                    TXTRECINTNAME.Text = Convert.ToString(DT.Rows[0]["IRGLNAME"]);
                    TXTPLNO.Text = Convert.ToString(DT.Rows[0]["IPCODE"]);
                    TXTPLNAME.Text = Convert.ToString(DT.Rows[0]["IPGLNAME"]);
                   
                    ddlAccYN.SelectedValue = Convert.ToString(DT.Rows[0]["INTACCYN"]);
                    ddlOperate.SelectedValue = Convert.ToString(DT.Rows[0]["UnOperate"]);
                    ddlCASHDR.SelectedValue = Convert.ToString(DT.Rows[0]["CASHDR"]);
                    ddlCASHCR.SelectedValue = Convert.ToString(DT.Rows[0]["CASHCR"]);
                    ddlTRFDR.SelectedValue = Convert.ToString(DT.Rows[0]["TRFDR"]);
                    ddlTRFCR.SelectedValue = Convert.ToString(DT.Rows[0]["TRFCR"]);
                    ddlCLGDR.SelectedValue = Convert.ToString(DT.Rows[0]["CLGDR"]);
                    ddlCLGCR.SelectedValue = Convert.ToString(DT.Rows[0]["CLGCR"]);

                    txtglpriority.Text = Convert.ToString(DT.Rows[0]["GLPRIORITY"]);
                    txtlastno.Text = Convert.ToString(DT.Rows[0]["LASTNO"]);

                    txtImplmentDate.Text = Convert.ToString(DT.Rows[0]["ImplimentDate"]);
                    txtOpeningBal.Text = Convert.ToString(DT.Rows[0]["OpeningBal"]);
                }
            }
            else if (DDLGLCAT.SelectedItem.Text == "Deposit GL")
            {
                DIV_DATE.Visible = true;
                DT = GL.GETDEPOSIT(Session["BRCD"].ToString(), txtsubglcode.Text);
                if (DT.Rows.Count > 0)
                {
                    if (string.Equals("MD", contentStr, StringComparison.OrdinalIgnoreCase))
                    {
                        DDLGLCAT.SelectedValue = GL.GETGlName(DT.Rows[0]["GLCODE"].ToString());
                        DDLGLCAT.Enabled = false;
                    }
                    else
                    {
                        DDLGLCAT.Enabled = true;
                    }
                    txtglg.SelectedItem.Text = Convert.ToString(DT.Rows[0]["CATEGORY"]);
                    txtglcode.Text = Convert.ToString(DT.Rows[0]["GLCODE"]);
                    TXTSUBGLNAME.Text = Convert.ToString(DT.Rows[0]["GLNAME"]);
                    txtplaccno.Text = Convert.ToString(DT.Rows[0]["PLACCNO"]);
                    TXTPLACCNAME.Text = GL.GETPLNAME(Session["BRCD"].ToString(), txtplaccno.Text);

                    txtsubglmarathi.Text = Convert.ToString(DT.Rows[0]["GLMarathi"]);
                    txtglgroup.Text = Convert.ToString(DT.Rows[0]["GLGROUP"]);
                    TXTRECINT.Text = Convert.ToString(DT.Rows[0]["IPCODE"]);
                    TXTRECINTNAME.Text = Convert.ToString(DT.Rows[0]["IPGLNAME"]);

                    ddlAccYN.SelectedValue = Convert.ToString(DT.Rows[0]["INTACCYN"]);
                    ddlOperate.SelectedValue = Convert.ToString(DT.Rows[0]["UnOperate"]);
                    ddlCASHDR.SelectedValue = Convert.ToString(DT.Rows[0]["CASHDR"]);
                    ddlCASHCR.SelectedValue = Convert.ToString(DT.Rows[0]["CASHCR"]);
                    ddlTRFDR.SelectedValue = Convert.ToString(DT.Rows[0]["TRFDR"]);
                    ddlTRFCR.SelectedValue = Convert.ToString(DT.Rows[0]["TRFCR"]);
                    ddlCLGDR.SelectedValue = Convert.ToString(DT.Rows[0]["CLGDR"]);
                    ddlCLGCR.SelectedValue = Convert.ToString(DT.Rows[0]["CLGCR"]);

                    txtglpriority.Text = Convert.ToString(DT.Rows[0]["GLPRIORITY"]);
                    txtlastno.Text = Convert.ToString(DT.Rows[0]["LASTNO"]);

                    txtImplmentDate.Text = Convert.ToString(DT.Rows[0]["ImplimentDate"]);
                    txtOpeningBal.Text = Convert.ToString(DT.Rows[0]["OpeningBal"]);
                }
            }
            else
            {
                DIV_DATE.Visible = false;
                DT = GL.GETOTHER(Session["BRCD"].ToString(), txtsubglcode.Text);

                if (DT.Rows.Count > 0)
                {

                   

                    if (string.Equals("MD", contentStr, StringComparison.OrdinalIgnoreCase))
                    {
                        if (DDLGLCAT.SelectedItem.Text == "General Ledger")
                        {
                            DDLGLCAT.SelectedValue = GL.GETGlName(DT.Rows[0]["MainGlCode"].ToString());
                            DDLGLCAT.Enabled = false;
                        }
                        else
                        {

                            DDLGLCAT.SelectedValue = GL.GETGlName(DT.Rows[0]["GLCODE"].ToString());
                            DDLGLCAT.Enabled = false;
                        }
                    }
                    else
                    {
                        DDLGLCAT.Enabled = true;
                    }
                    txtglcode.Text = Convert.ToString(DT.Rows[0]["GLCODE"]);
                    TXTSUBGLNAME.Text = Convert.ToString(DT.Rows[0]["GLNAME"]);
                    txtplaccno.Text = Convert.ToString(DT.Rows[0]["PLACCNO"]);
                    TXTPLACCNAME.Text = GL.GETPLNAME(Session["BRCD"].ToString(), txtplaccno.Text);

                    txtsubglmarathi.Text = Convert.ToString(DT.Rows[0]["GLMarathi"]);
                    txtglgroup.Text = Convert.ToString(DT.Rows[0]["GLGROUP"]);
           
                  
                    ddlbsFormat.SelectedValue = Convert.ToString(DT.Rows[0]["GLGROUP"]);     
                    DDLPLgroup.SelectedValue = String.IsNullOrEmpty(Convert.ToString(DT.Rows[0]["PLGroup"])) ? "0" : GL.GETCurrentPLName(Convert.ToString(DT.Rows[0]["PLGroup"]));
                    ddlAccYN.SelectedValue = Convert.ToString(DT.Rows[0]["INTACCYN"]);
                    ddlOperate.SelectedValue = Convert.ToString(DT.Rows[0]["UnOperate"]);
                    ddlCASHDR.SelectedValue = Convert.ToString(DT.Rows[0]["CASHDR"]);
                    ddlCASHCR.SelectedValue = Convert.ToString(DT.Rows[0]["CASHCR"]);
                    ddlTRFDR.SelectedValue = Convert.ToString(DT.Rows[0]["TRFDR"]);
                    ddlTRFCR.SelectedValue = Convert.ToString(DT.Rows[0]["TRFCR"]);
                    ddlCLGDR.SelectedValue = Convert.ToString(DT.Rows[0]["CLGDR"]);
                    ddlCLGCR.SelectedValue = Convert.ToString(DT.Rows[0]["CLGCR"]);
                    DropDownList1.SelectedValue = (DT.Rows[0]["GLMarathiFonttype"].ToString().Trim() == "0") ? "Shivaji01" : "Sarjudas";
                    txtglpriority.Text = Convert.ToString(DT.Rows[0]["GLPRIORITY"]);
                    txtlastno.Text = Convert.ToString(DT.Rows[0]["LASTNO"]);

                    txtImplmentDate.Text = Convert.ToString(DT.Rows[0]["ImplimentDate"]);
                    txtOpeningBal.Text = Convert.ToString(DT.Rows[0]["OpeningBal"]);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void DDLGLCAT_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
            if (ViewState["Flag"].ToString() == "AD")
            {
                string DT = "";
                TXTSUBGLNAME.Focus();
                if (DDLGLCAT.SelectedItem.Text == "Saving Deposit" || DDLGLCAT.SelectedItem.Text == "Nominal Member / Surity Account" || DDLGLCAT.SelectedItem.Text == "Current Deposit" || DDLGLCAT.SelectedItem.Text == "Pigmi Agent")
                {
                    Button1.Visible = false;
                    DIV_DATE.Visible = false;
                    txtglgroup.Text = "DP";
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                    DT = GL.getsubglcode1(DDLGLCAT.SelectedValue, Session["BRCD"].ToString());
                    BD.BindGlGroup(txtglg);

                    string[] str = DT.Split('_');
                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;

                    WebMsgBox.Show("you can not add subgl menu for that", this.Page);
                    txtglcode.Enabled = false;
                    txtsubglcode.Enabled = false;
                    TXTSUBGLNAME.Enabled = false;
                    txtlastno.Enabled = false;
                    TXTPLNO.Enabled = false;
                    txtglpriority.Enabled = false;
                }
                if (DDLGLCAT.SelectedItem.Text == "Daily Deposit")
                {
                    Button1.Visible = false;
                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;
                    txtglgroup.Text = "DP";
                    BD.BindGlGroup(txtglg);
                    DIV_DATE.Visible = true;
                    TXTRECINT.Enabled = false;
                    TXTRECINTNAME.Enabled = false;
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                    DT = GL.getsubglcode1(DDLGLCAT.SelectedValue, Session["BRCD"].ToString());
                    string[] str = DT.Split('_');
                    txtsubglcode.Text = str[0].ToString();
                    aaa = str[0].ToString();
                    TXTPLNO.Text = str[1].ToString();

                    int plint = Convert.ToInt32(aaa) + Convert.ToInt32("3000");
                    TXTPLNO.Text = plint.ToString();
                }
                if (DDLGLCAT.SelectedItem.Text == "Deposit GL")
                {
                    Button1.Visible = true;
                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;
                    txtglgroup.Text = "DP";
                    DIV_DATE.Visible = true;
                    TXTRECINT.Enabled = true;
                    TXTRECINTNAME.Enabled = true;
                    TXTPLNO.Enabled = false;
                    TXTPLNAME.Enabled = false;
                    txtglg.Visible = true;
                    txtglgroup.Enabled = false;
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                    txtddlGlCode.Text = txtglcode.Text;
                    BD.BindGlGroupForDeposit(txtglg, txtddlGlCode.Text);
                    DT = GL.getsubglcode1(DDLGLCAT.SelectedValue, Session["BRCD"].ToString());
                    string[] str = DT.Split('_');
                    txtsubglcode.Text = str[0].ToString();
                    aaa = str[0].ToString();

                    string plint = GL.getrecint(DDLGLCAT.SelectedValue, txtsubglcode.Text);
                    TXTRECINT.Text = plint.ToString();
                    txtglg.Focus();

                }
                if (DDLGLCAT.SelectedItem.Text == "Share Suspense" || DDLGLCAT.SelectedItem.Text == "Paid Up Share Capital")
                {
                    Button1.Visible = false;
                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;
                    DIV_DATE.Visible = false;
                    txtglgroup.Text = "SHR";
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                    BD.BindGlGroup(txtglg);
                    DT = GL.getsubglcode1(DDLGLCAT.SelectedValue, Session["BRCD"].ToString());
                    string[] str = DT.Split('_');
                    txtsubglcode.Text = str[0].ToString();
                    TXTPLNO.Text = str[1].ToString();
                }
                if (DDLGLCAT.SelectedItem.Text == "Payble on DDS" || DDLGLCAT.SelectedItem.Text == "Paybale On Deposit Account")
                {
                    Button1.Visible = false;
                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;
                    DIV_DATE.Visible = false;
                    BD.BindGlGroup(txtglg);
                    txtglgroup.Text = "IP";
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                    DT = GL.getsubglcode1(DDLGLCAT.SelectedValue, Session["BRCD"].ToString());
                    string[] str = DT.Split('_');
                    txtsubglcode.Text = str[0].ToString();
                    TXTPLNO.Text = str[1].ToString();
                }
                if (DDLGLCAT.SelectedItem.Text == "Loan GL")
                {

                    
                    Button1.Visible = true;
                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;
                    txtglgroup.Text = "LNV";
                    txtglcode.Text = DDLGLCAT.SelectedValue;

                    txtddlGlCode.Text = txtglcode.Text;
                    BD.BindGlGroupForLoan(txtglg, txtddlGlCode.Text);
                    DIV_DATE.Visible = true;
                    txtglg.Visible = true;
                    txtglgroup.Enabled = false;
                  
                    DT = GL.getsubglcode1(DDLGLCAT.SelectedValue, Session["BRCD"].ToString());
                    string[] str = DT.Split('_');
                    txtsubglcode.Text = str[0].ToString();
                    TXTPLNO.Text = str[1].ToString();
                    string recint = GL.getrecint(DDLGLCAT.SelectedValue, txtsubglcode.Text);

                    string PLINT = (Convert.ToDouble(txtsubglcode.Text) + Convert.ToDouble(5800)).ToString();
                    TXTRECINT.Text = recint;
                    TXTPLNO.Text = PLINT;
                    TXTSUBGLNAME.Enabled = true;
                    txtlastno.Enabled = false;
                    TXTRECINT.Enabled = true;
                    TXTRECINTNAME.Enabled = true;
                    TXTPLNO.Enabled = true;
                    TXTPLNAME.Enabled = true;
                    txtglpriority.Enabled = true;
                    txtglg.Focus();



                }
                if (DDLGLCAT.SelectedItem.Text == "Recevable loanAccount" || DDLGLCAT.SelectedItem.Text == "Recevable Penal loanAccount")
                {

                    Button1.Visible = false;

                    txtglgroup.Visible = true;
                    BD.BindGlGroup(txtglg);
                    ddlbsFormat.Visible = false;
                    DIV_DATE.Visible = false;
                    txtglgroup.Text = "IR";
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                    DT = GL.getsubglcode1(DDLGLCAT.SelectedValue, Session["BRCD"].ToString());
                    string[] str = DT.Split('_');
                    txtsubglcode.Text = str[0].ToString();
                    TXTPLNO.Text = str[1].ToString();
                }
                if (DDLGLCAT.SelectedItem.Text == "PL Account")
                {
                    Button1.Visible = false;
                    PLGroup.Visible = true;
                    txtglgroup.Visible = true;
                    BD.BindGlGroup(txtglg);
                    ddlbsFormat.Visible = false;
                    DIV_DATE.Visible = false;
                    txtglgroup.Text = "PL";
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                    DT = GL.getsubglcode1(DDLGLCAT.SelectedValue, Session["BRCD"].ToString());
                    string[] str = DT.Split('_');
                    txtsubglcode.Text = str[0].ToString();
                    TXTPLNO.Text = str[1].ToString();
                }
                if (DDLGLCAT.SelectedItem.Text == "General Ledger")
                {
                    Button1.Visible = false;
                    DIV_DATE.Visible = false;
                    DataTable Dt = new DataTable();
                    txtsubglcode.Visible = true;
                    txtglgroup.Text = "GL";
                    BD.BindGlGroup(txtglg);
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                    txtglgroup.Visible = false;
                    ddlbsFormat.Visible = true;

                    DT = GL.getsubglcode1(DDLGLCAT.SelectedValue, Session["BRCD"].ToString());
                    string[] str = DT.Split('_');
                    txtsubglcode.Text = str[0].ToString();
                    txtglcode.Text = str[0].ToString();
                    ddlbsFormat.Focus();
                }
            }
            else if ((ViewState["Flag"].ToString() == "MD") || (ViewState["Flag"].ToString() == "DL"))
            {
                string DT = "";
                txtsubglcode.Focus();
                if (DDLGLCAT.SelectedItem.Text == "Saving Deposit" || DDLGLCAT.SelectedItem.Text == "Nominal Member / Surity Account" || DDLGLCAT.SelectedItem.Text == "Current Deposit" || DDLGLCAT.SelectedItem.Text == "Pigmi Agent")
                {
                    DIV_DATE.Visible = false;

                    txtglgroup.Text = "DP";
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                    DT = GL.getsubglcode1(DDLGLCAT.SelectedValue, Session["BRCD"].ToString());
                    string[] str = DT.Split('_');

                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;
                    WebMsgBox.Show("you can not add subgl menu for that", this.Page);
                    txtglcode.Enabled = false;
                    txtsubglcode.Enabled = false;
                    TXTSUBGLNAME.Enabled = false;
                    txtlastno.Enabled = false;

                    txtglpriority.Enabled = false;
                }
                if (DDLGLCAT.SelectedItem.Text == "Daily Deposit")
                {
                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;
                    txtglgroup.Text = "DP";
                    DIV_DATE.Visible = true;

                    txtglcode.Text = DDLGLCAT.SelectedValue;
                    DT = GL.getsubglcode1(DDLGLCAT.SelectedValue, Session["BRCD"].ToString());
                    string[] str = DT.Split('_');
                    txtsubglcode.Text = str[0].ToString();
                    aaa = str[0].ToString();
                    TXTPLNO.Text = str[1].ToString();

                    int plint = Convert.ToInt32(aaa) + Convert.ToInt32("3000");
                    TXTPLNO.Text = plint.ToString();
                }
                if (DDLGLCAT.SelectedItem.Text == "Deposit GL")
                {
                    //Button1.Visible = true;

                    txtsubglcode.Enabled = true;
                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;
                    txtglgroup.Text = "DP";
                    DIV_DATE.Visible = true;

                    txtglg.Visible = true;
                    txtglgroup.Enabled = false;
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                   // BD.BindGlGroupForDeposit(txtglg, txtddlGlCode.Text);
                    BD.BindGlGroupForDeposit(txtglg, txtglcode.Text);

                }
                if (DDLGLCAT.SelectedItem.Text == "Share Suspense" || DDLGLCAT.SelectedItem.Text == "Paid Up Share Capital")
                {
                    txtsubglcode.Enabled = true;
                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;
                    DIV_DATE.Visible = false;
                    txtglgroup.Text = "SHR";
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                }
                if (DDLGLCAT.SelectedItem.Text == "Payble on DDS" || DDLGLCAT.SelectedItem.Text == "Paybale On Deposit Account")
                {
                    txtsubglcode.Enabled = true;
                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;
                    DIV_DATE.Visible = false;
                    txtglgroup.Text = "IP";
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                }
                if (DDLGLCAT.SelectedItem.Text == "Loan GL")
                {

                    txtsubglcode.Enabled = true;
                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;
                    txtglgroup.Text = "LNV";
                    

                    txtglcode.Text = DDLGLCAT.SelectedValue;
                 
                    DIV_DATE.Visible = true;
                    txtglg.Visible = true;
                    txtglgroup.Enabled = false;
                   // BD.BindGlGroupForLoan(txtglg, txtddlGlCode.Text);
                    BD.BindGlGroupForLoan(txtglg, txtglcode.Text);

                    TXTSUBGLNAME.Enabled = true;
                    txtlastno.Enabled = false;
                    txtglpriority.Enabled = true;
                }
                if (DDLGLCAT.SelectedItem.Text == "Recevable loanAccount" || DDLGLCAT.SelectedItem.Text == "Recevable Penal loanAccount")
                {
                    txtsubglcode.Enabled = true;
                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;
                    DIV_DATE.Visible = false;
                    txtglgroup.Text = "IR";
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                }
                if (DDLGLCAT.SelectedItem.Text == "PL Account")
                {
                    PLGroup.Visible = true;
                    txtsubglcode.Enabled = true;
                    txtglgroup.Visible = true;
                    ddlbsFormat.Visible = false;
                    DIV_DATE.Visible = false;
                    txtglgroup.Text = "PL";
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                }
                if (DDLGLCAT.SelectedItem.Text == "General Ledger")
                {
                    txtsubglcode.Enabled = true;
                    DIV_DATE.Visible = false;
                    DataTable Dt = new DataTable();
                    txtglgroup.Text = "GL";
                    txtglcode.Text = DDLGLCAT.SelectedValue;
                    txtglgroup.Visible = false;

                    ddlbsFormat.Visible = true;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TXTSUBGLNAME_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDLGLCAT.SelectedItem.Text == "Loan GL")
            {
                TXTRECINTNAME.Text = "RECEIVABLE INT. ON " + TXTSUBGLNAME.Text;
                TXTPLNAME.Text = "RECEIVABLE PEN INT. ON" + TXTSUBGLNAME.Text;
            }
            if (DDLGLCAT.SelectedItem.Text == "Deposit GL")
            {
                TXTRECINTNAME.Text = "RECEIVABLE INT. ON " + TXTSUBGLNAME.Text;
            }
            if (DDLGLCAT.SelectedItem.Text == "Daily Deposit")
            {
                TXTPLNAME.Text = "P." + TXTSUBGLNAME.Text;
            }
            txtplaccno.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearAll()
    {
        txtglcode.Text = "";
        TXTSUBGLNAME.Text = "";
        txtlastno.Text = "";
        txtplaccno.Text = "";
        txtglpriority.Text = "";
        txtglgroup.Text = "";
        TXTRECINT.Text = "";
        TXTRECINTNAME.Text = "";
        TXTPLNO.Text = "";
        TXTPLNAME.Text = "";
        TXTSUBGLNAME.Text = "";
        txtsubglcode.Text = "";
        TXTPLNAME.Text = "";
    }

    public void cleardata()
    {

        txtglcode.Text = "";
        TXTSUBGLNAME.Text = "";
        txtlastno.Text = "";
        txtplaccno.Text = "";
        txtglpriority.Text = "";
        txtglgroup.Text = "";
        TXTRECINT.Text = "";
        TXTRECINTNAME.Text = "";
        TXTPLNO.Text = "";
        TXTPLNAME.Text = "";
        TXTPLACCNAME.Text = "";
        TXTSUBGLNAME.Text = "";
        txtglg.SelectedValue = "0";
        txtsubglcode.Text = "";
        TXTPLNAME.Text = "";
        ddlbsFormat.SelectedValue = "0";
        DDLGLCAT.SelectedValue = "0";
        txtsubglmarathi.Text = "";
        txtImplmentDate.Text = "";
        txtOpeningBal.Text = "";
    }

    protected void ddlbsFormat_SelectedIndexChanged(object sender, EventArgs e)
    {
        TXTSUBGLNAME.Focus();
    }

    protected void txtplaccno_TextChanged(object sender, EventArgs e)
    {
        TXTPLACCNAME.Text = GL.GETPLNAME(Session["BRCD"].ToString(), txtplaccno.Text);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable DT = new DataTable();
            int res = 0, RES11 = 0, RES12 = 0;
            int RES = 0, IR = 0, IP = 0;
            int i = 0;

            txtOpeningBal.Text = txtOpeningBal.Text == "" ? "0" : txtOpeningBal.Text;
            txtglpriority.Text = txtglpriority.Text == "" ? "1" : txtglpriority.Text;
            txtlastno.Text = txtlastno.Text == "" ? "1" : txtlastno.Text;
            txtOpeningBal.Text = Convert.ToDouble(txtOpeningBal.Text) < 0 ? "0" : txtOpeningBal.Text;

            DT = GL.getbranchcode();
            if (ViewState["Flag"].ToString() == "AD")
            {
                if (DDLGLCAT.SelectedItem.Text == "Loan GL")
                {
                    for (i = 1; DT.Rows.Count > i; i++)
                    {
                        //  Commented by amol on 14/01/2018 (as per new requirement by ambika mam)
                        //res = GL.insertglmain(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, TXTSUBGLNAME.Text, txtglgroup.Text, txtsubglcode.Text, txtplaccno.Text, txtlastno.Text, txtglpriority.Text, TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, txtImplmentDate.Text.ToString(), txtOpeningBal.Text.Trim().ToString() == "" ? "0" : txtOpeningBal.Text.Trim().ToString());
                        res = GL.InsertMainGL(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, txtsubglcode.Text, TXTSUBGLNAME.Text, txtglgroup.Text, txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                              TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                              ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, fontstyle: DropDownList1.SelectedValue);

                        if (res > 0)
                        {
                            //  Commented by amol on 14/01/2018 (as per new requirement by ambika mam)
                            //RE11 = GL.INSERTPL(DT.Rows[i]["BRCD"].ToString(), "11", TXTRECINTNAME.Text, "IR", TXTRECINT.Text, txtplaccno.Text, txtlastno.Text, txtglpriority.Text, txtsubglmarathi.Text, "", "");
                            RE11 = GL.InsertMainPL(DT.Rows[i]["BRCD"].ToString(), "11", TXTRECINT.Text, TXTRECINTNAME.Text, "IR", "", txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                                  TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                                  ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, fontstyle: DropDownList1.SelectedValue);

                            //  Commented by amol on 14/01/2018 (as per new requirement by ambika mam)
                            //RES12 = GL.INSERTPL(DT.Rows[i]["BRCD"].ToString(), "12", TXTPLNAME.Text, "IR", TXTPLNO.Text, txtplaccno.Text, txtlastno.Text, txtglpriority.Text, txtsubglmarathi.Text, "", "");
                            RES12 = GL.InsertMainPL(DT.Rows[i]["BRCD"].ToString(), "12", TXTPLNO.Text, TXTPLNAME.Text, "IR", "", txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                                  TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                                  ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, fontstyle: DropDownList1.SelectedValue);

                            loangl = GL.loangl(txtsubglcode.Text, TXTSUBGLNAME.Text,  txtglg.SelectedItem.Text, DT.Rows[i]["BRCD"].ToString());
                            //loangl = GL.loangl(txtsubglcode.Text, TXTSUBGLNAME.Text, txtglg.SelectedItem.Text, DT.Rows[i]["BRCD"].ToString());
                        }
                    }

                    if ((res > 0) || (RE11 > 0) || (RES12 > 0) || (loangl > 0))
                    {
                        cleardata();
                        WebMsgBox.Show("GL Created For Loan Successfully", this.Page);
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Glmast_gl__Loan_add _" + txtglcode.Text + "_" + txtsubglcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        return;
                    }
                }
                else if (DDLGLCAT.SelectedItem.Text == "Deposit GL")
                {
                    for (i = 1; i < DT.Rows.Count; i++)
                    {
                        res = GL.InsertMainGL(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, txtsubglcode.Text, TXTSUBGLNAME.Text, txtglgroup.Text, txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                              TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                              ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, fontstyle: DropDownList1.SelectedValue);

                        if (res > 0)
                        {
                            RE11 = GL.InsertMainPL(DT.Rows[i]["BRCD"].ToString(), "10", TXTRECINT.Text, TXTRECINTNAME.Text, "IR", "", txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                                  TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                                  ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, fontstyle: DropDownList1.SelectedValue);

                            DEPOSIT = GL.DEPOSITGL(txtsubglcode.Text, TXTSUBGLNAME.Text, txtglg.SelectedItem.Text, DT.Rows[i]["BRCD"].ToString());
                        }
                    }

                    if ((res > 0) || (RE11 > 0) || (DEPOSIT > 0))
                    {
                        cleardata();
                        WebMsgBox.Show("GL Created for Deposit Successfully", this.Page);
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Glmast_gl__Depo__add _" + txtglcode.Text + "_" + txtsubglcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        return;
                    }
                }
                else if (DDLGLCAT.SelectedItem.Text == "Daily Deposit")
                {
                    res = GL.InsertMainPL(Session["BRCD"].ToString(), txtglcode.Text, txtsubglcode.Text, TXTSUBGLNAME.Text, txtglgroup.Text, "", txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                          TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                          ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, fontstyle: DropDownList1.SelectedValue);

                    if (res > 0)
                    {
                        RE11 = GL.InsertMainPL(Session["BRCD"].ToString(), "22", TXTPLNO.Text, TXTPLNAME.Text, txtglgroup.Text, "", txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                              TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                              ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, fontstyle: DropDownList1.SelectedValue);

                        if ((res > 0) || (RE11 > 0))
                        {
                            cleardata();
                            WebMsgBox.Show("GL Created for Deposit Successfully", this.Page);

                            CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Glmast_gl__Depo_Daily_Depo_add _" + txtglcode.Text + "_" + txtsubglcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            return;
                        }
                    }
                }
                else if (DDLGLCAT.SelectedItem.Text == "General Ledger")
                {
                    for (i = 1; DT.Rows.Count > i; i++)
                    {
                        res = GL.InsertMainPL(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, txtsubglcode.Text, TXTSUBGLNAME.Text, ddlbsFormat.Text, "", txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                              TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                              ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, fontstyle: DropDownList1.SelectedValue);

                        //  Commented by amol on 14/01/2018 (as per new requirement by ambika mam)
                        //GL.INSERTPL(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, TXTSUBGLNAME.Text, ddlbsFormat.SelectedValue, txtsubglcode.Text, txtplaccno.Text, txtlastno.Text, txtglpriority.Text, txtsubglmarathi.Text, txtImplmentDate.Text.ToString(), txtOpeningBal.Text);
                    }
                    if (res > 0)
                    {
                        cleardata();
                        WebMsgBox.Show("GL Created for Deposit Successfully", this.Page);
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Glmast_gl__Depo_General_led_add _" + txtglcode.Text + "_" + txtsubglcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        return;
                    }
                }
                else
                {
                    for (i = 1; DT.Rows.Count > i; i++)
                    {
                        res = GL.InsertMainPL(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, txtsubglcode.Text, TXTSUBGLNAME.Text, txtglgroup.Text, DDLPLgroup.SelectedItem.Text, txtglpriority.Text,
                              txtplaccno.Text, txtlastno.Text, TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue,
                              ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue, ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, fontstyle: DropDownList1.SelectedValue);

                        //  Commented by amol on 14/01/2018 (as per new requirement by ambika mam)
                        //res = GL.INSERTPL1(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, TXTSUBGLNAME.Text, txtglgroup.Text, txtsubglcode.Text, txtplaccno.Text, txtlastno.Text, txtglpriority.Text, txtsubglmarathi.Text, DDLPLgroup.SelectedItem.Text, txtImplmentDate.Text.ToString(), txtOpeningBal.Text);
                    }
                    if (res > 0)
                    {
                        cleardata();
                        WebMsgBox.Show(" GL create successfully", this.Page);
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Glmast_gl_add _" + txtglcode.Text + "_" + txtsubglcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        return;
                    }
                }
            }
            else if (ViewState["Flag"].ToString() == "MD")
            {
                txtsubglcode.Focus();
                if (DDLGLCAT.SelectedItem.Text == "General Ledger")
                {
                    for (i = 1; DT.Rows.Count > i; i++)
                    {
                        RES = GL.UpdateMainGL(DT.Rows[i]["BRCD"].ToString(), txtsubglcode.Text, txtsubglcode.Text, TXTSUBGLNAME.Text, ddlbsFormat.SelectedValue, txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                     
                        //RES = GL.UpdateMainGL(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, txtsubglcode.Text, TXTSUBGLNAME.Text, ddlbsFormat.SelectedValue, txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                               TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                               ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, Session["BRCD"].ToString(), IsMultiApply: HdnValue.Value, fontstyle: DropDownList1.SelectedValue);

                        if (RES > 0)
                        {
                            IR = GL.UpdateMainGL(DT.Rows[i]["BRCD"].ToString(), txtsubglcode.Text, TXTRECINT.Text, TXTRECINTNAME.Text, ddlbsFormat.SelectedValue, txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                          //  IR = GL.UpdateMainGL(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, TXTRECINT.Text, TXTRECINTNAME.Text, ddlbsFormat.SelectedValue, txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                                  TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                                  ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, Session["BRCD"].ToString(), IsMultiApply: HdnValue.Value, fontstyle: DropDownList1.SelectedValue);


                            IP = GL.UpdateMainGL(DT.Rows[i]["BRCD"].ToString(), txtsubglcode.Text, TXTPLNO.Text, TXTPLNAME.Text, ddlbsFormat.SelectedValue, txtglpriority.Text, txtplaccno.Text, txtlastno.Text,

                           // IP = GL.UpdateMainGL(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, TXTPLNO.Text, TXTPLNAME.Text, ddlbsFormat.SelectedValue, txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                                  TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                                  ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, Session["BRCD"].ToString(), IsMultiApply: HdnValue.Value, fontstyle: DropDownList1.SelectedValue);
                        }
                        //  Commented by amol on 14/01/2018 (as per new requirement by ambika mam)
                        //RES = GL.UPDATEGL(TXTSUBGLNAME.Text, txtplaccno.Text, txtlastno.Text, txtglpriority.Text, ddlbsFormat.SelectedValue, DT.Rows[i]["BRCD"].ToString(), txtsubglcode.Text, txtglcode.Text, txtsubglmarathi.Text, Session["BRCD"].ToString(), txtImplmentDate.Text.ToString(), txtOpeningBal.Text);
                        //IR = GL.UPDATEGL(TXTRECINTNAME.Text, txtplaccno.Text, txtlastno.Text, txtglpriority.Text, ddlbsFormat.SelectedValue, DT.Rows[i]["BRCD"].ToString(), TXTRECINT.Text, txtglcode.Text, txtsubglmarathi.Text, Session["BRCD"].ToString(), "", "0");
                        //IP = GL.UPDATEGL(TXTPLNAME.Text, txtplaccno.Text, txtlastno.Text, txtglpriority.Text, ddlbsFormat.SelectedValue, DT.Rows[i]["BRCD"].ToString(), TXTPLNO.Text, txtglcode.Text, txtsubglmarathi.Text, Session["BRCD"].ToString(), "", "0");
                    }
                }
                else
                {
                    RES = GL.UpdateMainPL(Session["BRCD"].ToString(), txtglcode.Text, txtsubglcode.Text, TXTSUBGLNAME.Text, txtglgroup.Text, DDLPLgroup.SelectedItem.Text, txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                              TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                              ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, Session["BRCD"].ToString(), IsMultiApply: HdnValue.Value, fontstyle: DropDownList1.SelectedValue);

                    if (RES > 0)
                    {
                        IR = GL.UpdateMainGL(Session["BRCD"].ToString(), txtglcode.Text, TXTRECINT.Text, TXTRECINTNAME.Text, txtglgroup.Text, txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                              TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                              ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, Session["BRCD"].ToString(), IsMultiApply: HdnValue.Value, fontstyle: DropDownList1.SelectedValue);

                        IP = GL.UpdateMainGL(Session["BRCD"].ToString(), txtglcode.Text, TXTPLNO.Text, TXTPLNAME.Text, txtglgroup.Text, txtglpriority.Text, txtplaccno.Text, txtlastno.Text,
                              TXTRECINT.Text, TXTPLNO.Text, txtsubglmarathi.Text, ddlAccYN.SelectedValue, ddlCASHDR.SelectedValue, ddlCASHCR.SelectedValue, ddlTRFDR.SelectedValue, ddlTRFCR.SelectedValue,
                              ddlCLGDR.SelectedValue, ddlCLGCR.SelectedValue, ddlOperate.SelectedValue, txtImplmentDate.Text, txtOpeningBal.Text, Session["BRCD"].ToString(), IsMultiApply: HdnValue.Value, fontstyle: DropDownList1.SelectedValue);
                    }



                    if (DDLGLCAT.SelectedItem.Text == "Deposit GL")
                    {


                        DepositUpdateRes = GL.UpdateDEPOSITGL(DCODE: txtsubglcode.Text, DTYPE: TXTSUBGLNAME.Text, DCAT: txtglg.SelectedItem.Text, BRCD: Convert.ToString(Session["BRCD"]), IsMultiApply: HdnValue.Value);
                    }

                    if (DDLGLCAT.SelectedItem.Text == "Loan GL")
                    {

                        LoanUpdateRes = GL.Updateloangl(loancode: txtsubglcode.Text, loantype: TXTSUBGLNAME.Text, loacat: txtglg.SelectedItem.Text, brcd: Convert.ToString(Session["BRCD"]), IsMultiApply: HdnValue.Value);
                    }


                    //  Commented by amol on 14/01/2018 (as per new requirement by ambika mam)
                    //RES = GL.UPDATEGL1(TXTSUBGLNAME.Text, txtplaccno.Text, txtlastno.Text, txtglpriority.Text, txtglgroup.Text, Session["BRCD"].ToString(), txtsubglcode.Text, txtglcode.Text, txtsubglmarathi.Text, DDLPLgroup.SelectedItem.Text, Session["BRCD"].ToString(), txtImplmentDate.Text);
                    //IR = GL.UPDATEGL(TXTRECINTNAME.Text, txtplaccno.Text, txtlastno.Text, txtglpriority.Text, txtglgroup.Text, Session["BRCD"].ToString(), TXTRECINT.Text, txtglcode.Text, txtsubglmarathi.Text, Session["BRCD"].ToString(), "", "0");
                    //IP = GL.UPDATEGL(TXTPLNAME.Text, txtplaccno.Text, txtlastno.Text, txtglpriority.Text, txtglgroup.Text, Session["BRCD"].ToString(), TXTPLNO.Text, txtglcode.Text, txtsubglmarathi.Text, Session["BRCD"].ToString(), "", "0");
                }

                if (RES > 0)
                {
                
                    
                  
                    if (DDLGLCAT.SelectedItem.Text == "Loan GL")
                    {
                        if (LoanUpdateRes > 0)
                        {
                            WebMsgBox.Show("GL and LoanGl MODIFIED SUCCESSFULLY", this.Page);
                        }
                        else
                        {
                            WebMsgBox.Show("Their is Some Issue Occured While Modifying Data", this.Page);

                        }
                    }
                    else if (DDLGLCAT.SelectedItem.Text == "Deposit GL")
                    {
                        if (DepositUpdateRes > 0)
                        {
                            WebMsgBox.Show("GL and DepositGL MODIFIED SUCCESSFULLY", this.Page);
                        }
                        else
                        {
                            WebMsgBox.Show("Their is Some Issue Occured While Modifying Data", this.Page);

                        }
                    }
                    else
                    {
                        WebMsgBox.Show("GL MODIFIED SUCCESSFULLY", this.Page);
                    }

                    cleardata();

                    DDLGLCAT.Enabled = true;
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Glmast__Mod _" + txtglcode.Text + "_" + txtsubglcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
                else if (IR > 0 && RES > 0)
                {
                    cleardata();
                    WebMsgBox.Show("IR MODIFIED SUCCESSFULLY", this.Page);
                    DDLGLCAT.Enabled = true;
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Glmast_IR_Mod _" + txtglcode.Text + "_" + txtsubglcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
                else if (IP > 0 && RES > 0)
                {
                    cleardata();
                    WebMsgBox.Show("IP MODIFIED SUCCESSFULLY", this.Page);
                    DDLGLCAT.Enabled = true;
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Glmast_IP_Mod _" + txtglcode.Text + "_" + txtsubglcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
                else if (IP > 0 && RES > 0 && IR > 0)
                {
                    cleardata();
                    WebMsgBox.Show("lOAN GL MODIFIED", this.Page);
                    DDLGLCAT.Enabled = true;
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Glmast_Loangl_Mod _" + txtglcode.Text + "_" + txtsubglcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
            }        












            else if (ViewState["Flag"].ToString() == "DL")
            {
                txtsubglcode.Focus();
                if (DDLGLCAT.SelectedItem.Text == "Loan GL")
                {
                    for (i = 1; DT.Rows.Count > i; i++)
                    {
                        RES = GL.DeleteMainGL(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, txtsubglcode.Text);
                        IR = GL.DeleteMainGL(DT.Rows[i]["BRCD"].ToString(), "11", TXTRECINT.Text);
                        IP = GL.DeleteMainGL(DT.Rows[i]["BRCD"].ToString(), "12", TXTPLNO.Text);
                        IP = GL.DeleteLoanInfo(DT.Rows[i]["BRCD"].ToString(), txtsubglcode.Text);
                    }
                }
                else if (DDLGLCAT.SelectedItem.Text == "Deposit GL")
                {
                    for (i = 1; DT.Rows.Count > i; i++)
                    {
                        RES = GL.DeleteMainGL(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, txtsubglcode.Text);
                        IR = GL.DeleteMainGL(DT.Rows[i]["BRCD"].ToString(), "10", TXTRECINT.Text);
                        IR = GL.DeleteDeposit(DT.Rows[i]["BRCD"].ToString(), txtsubglcode.Text);
                    }
                }
                else if (DDLGLCAT.SelectedItem.Text == "Daily Deposit")
                {
                    RES = GL.DeleteMainGL(Session["BRCD"].ToString(), txtglcode.Text, txtsubglcode.Text);
                    IP = GL.DeleteMainGL(Session["BRCD"].ToString(), "22", TXTPLNO.Text);
                }
                else if (DDLGLCAT.SelectedItem.Text == "General Ledger")
                {
                    for (i = 1; DT.Rows.Count > i; i++)
                    {
                        RES = GL.DeleteMainGL(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, txtsubglcode.Text);
                    }
                }
                else
                {
                    for (i = 1; DT.Rows.Count > i; i++)
                    {
                        RES = GL.DeleteMainGL(DT.Rows[i]["BRCD"].ToString(), txtglcode.Text, txtsubglcode.Text);
                    }
                }

                if ((IP > 0) || (RES > 0) || (IR > 0))
                {
                    cleardata();
                    WebMsgBox.Show("Sucessfully Deleted ...!!", this.Page);
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Glmast_del_" + txtglcode.Text + "_" + txtsubglcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        try
        {
            cleardata();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btncategoryAdd_Click(object sender, EventArgs e)
    {
        try
        {

            res = GL.InsertGlCategory(Category: txtCategory.Text,GlCode: txtddlGlCode.Text);

            if (res>0)

            {
                mp1.Show();
                txtCategory.Text = "";

                lblAddMessage.Text = "Added Succesfully";
                lblAddMessage.ForeColor = System.Drawing.Color.Green;
                if (txtddlGlCode.Text.Equals("3"))
                {
                    BD.BindGlGroupForLoan(txtglg, txtddlGlCode.Text);
                }
                else
                {
                    BD.BindGlGroupForDeposit(txtglg, txtddlGlCode.Text);

                }
                
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#MyPopup').modal('hide')", true);

            }
            else
            {
                lblAddMessage.Text = "Issue While Adding Data";
                lblAddMessage.ForeColor = System.Drawing.Color.Red;

            }
        }
        catch (Exception Ex) 
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            lblAddMessage.Text = "";
        }
        catch (Exception Ex)
        {


            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            lblAddMessage.Text = "";

        }
        catch (Exception Ex)
        {


            ExceptionLogging.SendErrorToText(Ex);
        }
        
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (DropDownList1.SelectedValue == "0")
        {
            txtsubglmarathi.Font.Name = "Shivaji01";
            txtsubglmarathi.Font.Size = 18;
         
        }
       
        else
            if (DropDownList1.SelectedValue == "1")
        {

            txtsubglmarathi.Font.Name = "Sarjudas";
            txtsubglmarathi.Font.Size = 18;
               
        }
       // txtsubglmarathi.Font.Name = DropDownList1.SelectedItem.Text;
           
    }
}