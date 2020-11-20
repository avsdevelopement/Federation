using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class FrmAVS5169 : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    ClsAVS5169 accop = new ClsAVS5169();
    scustom customcs = new scustom();
    ClsCommon CMN = new ClsCommon();
    ClsLoanInfo LI = new ClsLoanInfo();

    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    DataTable dt2 = new DataTable();
    DataTable dt11 = new DataTable();
    DataTable dt12 = new DataTable();

    int cnt = 0, nonmi1, nonmi2, joint1, joint2;
    public static string Flag;
    string AC_Status = "";
    string FL = "";
    int Result;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");

            if (!IsPostBack)
            {
                autocustname1.ContextKey = Session["BRCD"].ToString();
                autoglname.ContextKey = Session["BRCD"].ToString();
                autoagentname.ContextKey = Session["BRCD"].ToString();
                txtodate.Text = Session["EntryDate"].ToString();

                TblDiv_MainWindow.Visible = false;
                DIVACC.Visible = true;
                Div_grid.Visible = true;
                BD.BindACCTYPE(Ddlacctype);
                BD.BindMODEOFOPR(DdlModeofOpr);
                BD.BindRelation(ddlRelation);
                BD.BindRelation(ddlrelation2);
                BD.BindRelation(ddlreljoint);
                BD.BindRelation(ddlrelationj2);
                txtAccType.Enabled = false;
                txtmopr.Enabled = false;

                txttype.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGrid()
    {
        try
        {
            if ((txttype.Text.ToString() != "") && (txtaccno.Text.ToString() != ""))
                accop.BindGrid(GrdAcc, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), txttype.Text.ToString(), txtaccno.Text.ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txttype_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1 = accop.Getaccno(txttype.Text, Session["BRCD"].ToString(), "");//For GLCODE="";
            if (AC1 != null)
            {
                string[] AC = AC1.Split('-'); ;
                ViewState["ACCNO"] = AC[0].ToString();
                ViewState["GLCODE"] = AC[1].ToString();
                txttynam.Text = AC[2].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txttype.Text + "_" + ViewState["GLCODE"].ToString();

                txtaccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code!.....", this.Page);
                txttype.Focus();
                txttype.Text = "";
                txttype.Text = "";
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
            string[] custnob = txttynam.Text.Split('_');
            if (custnob.Length > 1)
            {
                txttynam.Text = custnob[0].ToString();
                txttype.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = accop.Getaccno(txttype.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
                ViewState["ACCNO"] = AC[0].ToString();
                ViewState["GLCODE"] = AC[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txttype.Text;

                txtaccno.Focus();
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
            int RC = LI.CheckAccount(txtaccno.Text, txttype.Text, Session["BRCD"].ToString());
            if (RC == 0)
            {
                txtaccno.Focus();
                txtaccno.Text = "";
                WebMsgBox.Show("Please Enter valid Account Number,Account Not Exist..........!!", this.Page);
                return;
            }
            ViewState["CustNo"] = RC;

            string AT = accop.GetStage(Session["BRCD"].ToString(), ViewState["CustNo"].ToString(), txtaccno.Text, txttype.Text);
            if (AT == "1004")
            {
                txttype.Focus();
                txtaccno.Text = "";
                Cleardata();
                WebMsgBox.Show("Sorry record Not Present.........!!", this.Page);
                return;
            }

            accn = customcs.GetAccountNme(txtaccno.Text, txttype.Text, Session["BRCD"].ToString());
            if (accn != null)
            {
                AN = customcs.GetAccountNme(txtaccno.Text, txttype.Text, Session["BRCD"].ToString()).Split('_');
                TxtAccName.Text = AN[1].ToString();
            }
            callaccnInfo();
            BindGrid();
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
            string[] custnob = TxtAccName.Text.Split('_');
            if (custnob.Length > 1)
            {
                txtaccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string accn = "";
                string[] AN;
                int RC = LI.CheckAccount(txtaccno.Text, txttype.Text, Session["BRCD"].ToString());
                if (RC == 0)
                {
                    txtaccno.Focus();
                    txtaccno.Text = "";
                    WebMsgBox.Show("Please Enter valid Account Number,Account Not Exist..........!!", this.Page);
                    return;
                }
                ViewState["CustNo"] = RC;

                string AT = accop.GetStage(Session["BRCD"].ToString(), ViewState["CustNo"].ToString(), txtaccno.Text, txttype.Text);
                if (AT == "1004")
                {
                    txttype.Focus();
                    txtaccno.Text = "";
                    Cleardata();
                    WebMsgBox.Show("Sorry record Not Present.........!!", this.Page);
                    return;
                }

                accn = customcs.GetAccountNme(txtaccno.Text, txttype.Text, Session["BRCD"].ToString());
                if (accn != null)
                {
                    AN = customcs.GetAccountNme(txtaccno.Text, txttype.Text, Session["BRCD"].ToString()).Split('_');
                    TxtAccName.Text = AN[1].ToString();
                }
                callaccnInfo();
                BindGrid();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void chkYes_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string PostDate = Session["EntryDate"].ToString();
            if (PostDate.Length > 1)
            {
                dvminor.Visible = true;
                dvmname.Visible = true;
                dvmdate.Visible = true;
                AutoAccname.ContextKey = Session["EntryDate"].ToString();
                txtcustid.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void chkNo_CheckedChanged(object sender, EventArgs e)
    {
        dvminor.Visible = false;
        dvmname.Visible = false;
        dvmdate.Visible = false;
        Txtrefcustno.Focus();
    }

    protected void txtAccType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string acct;
            acct = BD.GetAccTType(txtAccType.Text);
            if (acct == null)
            {
                WebMsgBox.Show("Enter valid account type!!....", this.Page);
                txtAccType.Text = "";
                txtAccType.Focus();
            }
            else
            {
                Ddlacctype.SelectedValue = acct;
                txtmopr.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtmopr_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string acct;
            acct = BD.GetMOP(txtmopr.Text);
            if (acct == null)
            {
                WebMsgBox.Show("Enter valid Mode of Operation!!....", this.Page);
                txtmopr.Text = "";
                txtmopr.Focus();
            }
            else
            {
                DdlModeofOpr.Text = acct;
                chkYes.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Cleardata()
    {
        txttype.Focus();
        txtcstno.Text = "";
        txtAccType.Text = "";
        txtname.Text = "";
        txtaccno.Text = "";
        TxtAccName.Text = "";
        txttype.Text = "";
        txttynam.Text = "";
        txtmopr.Text = "";
        txtcustid.Text = "";
        txtacconam.Text = "";
        txtmdate.Text = "";
        DdlModeofOpr.SelectedValue = "0";
        Ddlacctype.SelectedValue = "0";
        Txtrefcustno.Text = "";
        txtrefcustname.Text = "";
        TxtAgentNo.Text = "";
        TxtAgentname.Text = "";
        txtNomName.Text = "";
        ddlRelation.SelectedValue = "0";
        txtBithdate.Text = "";
        txtnom2.Text = "";
        ddlrelation2.SelectedValue = "0";
        txtdob2.Text = "";
        txtJointFName.Text = "";
        ddlreljoint.SelectedValue = "0";
        TxtDOB.Text = "";
        txtjname2.Text = "";
        ddlrelationj2.SelectedValue = "0";
        txtjbirth2.Text = "";
    }

    public void ENDN(bool TF)
    {
        txttype.Enabled = TF;
        txtname.Enabled = TF;
        txttynam.Enabled = TF;
        Ddlacctype.Enabled = TF;
        DdlModeofOpr.Enabled = TF;
        DdlModeofOpr.Enabled = TF;
        txtcustid.Enabled = TF;
        txtacconam.Enabled = TF;
        txtmdate.Enabled = TF;
        txtaccno.Enabled = TF;
        TxtAccName.Enabled = TF;
        Txtrefcustno.Enabled = TF;
        txtrefcustname.Enabled = TF;
        TxtAgentNo.Enabled = TF;
        TxtAgentname.Enabled = TF;
        txtNomName.Enabled = TF;
        ddlRelation.Enabled = TF;
        txtBithdate.Enabled = TF;
        txtnom2.Enabled = TF;
        ddlrelation2.Enabled = TF;
        txtdob2.Enabled = TF;
        txtJointFName.Enabled = TF;
        ddlreljoint.Enabled = TF;
        TxtDOB.Enabled = TF;
        txtjname2.Enabled = TF;
        ddlrelationj2.Enabled = TF;
        txtjbirth2.Enabled = TF;
    }

    public void callaccnInfo()
    {
        try
        {
            if (ViewState["GLCODE"].ToString() == "1" || ViewState["GLCODE"].ToString() == "2" || ViewState["GLCODE"].ToString() == "5" || ViewState["GLCODE"].ToString() == "8")
                Div_nominee.Visible = true;
            else
                Div_nominee.Visible = false;
            
            if (ViewState["GLCODE"].ToString() == "3")
            {
                Div_nominee.Visible = false;
                Div_Joint.Visible = false;
            }

            string custname, refcustname, agntname;
            string[] name;
            string[] rname;
            string[] aname;
            dt = accop.GetInfo(ViewState["GLCODE"].ToString(), Session["BRCD"].ToString(), ViewState["ACCNO"].ToString(), ViewState["SUBGLCODE"].ToString());
            dt1 = accop.GetNominiInfo1(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), ViewState["SUBGLCODE"].ToString(), "1", ViewState["ACCNO"].ToString());
            dt11 = accop.GetNominiInfo2(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), ViewState["SUBGLCODE"].ToString(), "2", ViewState["ACCNO"].ToString());
            dt2 = accop.GetJointinfo1(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), ViewState["SUBGLCODE"].ToString(), "1", ViewState["ACCNO"].ToString());
            dt12 = accop.GetJointinfo2(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), ViewState["SUBGLCODE"].ToString(), "2", ViewState["ACCNO"].ToString());
            txtodate.Text = Convert.ToDateTime(dt.Rows[0]["OPENINGDATE"]).ToString("dd/MM/yyyy");
            txtmopr.Text = Convert.ToString(dt.Rows[0]["OPR_TYPE"].ToString());
            
            DdlModeofOpr.SelectedValue = Convert.ToString(dt.Rows[0]["OPR_TYPE"].ToString());
            if (DdlModeofOpr.SelectedValue == "2")
                Div_Joint.Visible = true;
            else
                Div_Joint.Visible = false;
            
            txttype.Text = Convert.ToString(dt.Rows[0]["SUBGLCODE"].ToString());
            txtaccno.Text = Convert.ToString(dt.Rows[0]["ACCNO"].ToString());
            txtAccType.Text = Convert.ToString(dt.Rows[0]["Acc_Type"].ToString());
            Ddlacctype.SelectedIndex = Convert.ToInt32(dt.Rows[0]["Acc_Type"].ToString());
            Ddlacctype.SelectedIndex = Convert.ToInt32(txtAccType.Text);
            txtcstno.Text = Convert.ToString(dt.Rows[0]["CUSTNO"].ToString());
            custname = accop.Getcustname(txtcstno.Text);
            name = custname.Split('_');
            TxtAccName.Text = name[0].ToString();

            if ((Convert.ToString(dt.Rows[0]["Ref_custNo"].ToString()) != "0") && (Convert.ToString(dt.Rows[0]["Ref_custNo"].ToString()) != ""))
            {
                Txtrefcustno.Text = Convert.ToString(dt.Rows[0]["Ref_custNo"].ToString());
                refcustname = accop.Getcustname(Txtrefcustno.Text);
                rname = refcustname.Split('_');
                txtrefcustname.Text = rname[0].ToString();
            }
            if ((Convert.ToString(dt.Rows[0]["Ref_Agent"].ToString()) != "0") && (Convert.ToString(dt.Rows[0]["Ref_Agent"].ToString()) != ""))
            {
                TxtAgentNo.Text = Convert.ToString(dt.Rows[0]["Ref_Agent"].ToString());
                agntname = accop.GetAgentName(TxtAgentNo.Text, Session["BRCD"].ToString());
                aname = agntname.Split('_');
                TxtAgentname.Text = aname[0].ToString();
            }
            if (dt1.Rows.Count > 0)
            {
                if (Convert.ToString(dt1.Rows[0]["NOMINEENAME"].ToString()) != "")
                {
                    txtNomName.Text = Convert.ToString(dt1.Rows[0]["NOMINEENAME"].ToString());
                    ddlRelation.SelectedValue = Convert.ToString(dt1.Rows[0]["RELATION"].ToString());
                    if (dt1.Rows[0]["DOB"].ToString() != "" && Convert.ToDateTime(dt1.Rows[0]["DOB"]).ToString("dd/MM/yyyy") != "01/01/1900") // condition added by abhishek 14-02-2018 Because of exception 
                    {
                        txtBithdate.Text = Convert.ToDateTime(dt1.Rows[0]["DOB"]).ToString("dd/MM/yyyy");
                    }
                }
            }
            if (dt11.Rows.Count > 0)
            {
                if (Convert.ToString(dt11.Rows[0]["NOMINEENAME"].ToString()) != "")
                {
                    txtnom2.Text = Convert.ToString(dt11.Rows[0]["NOMINEENAME"].ToString());
                    ddlrelation2.SelectedValue = Convert.ToString(dt11.Rows[0]["RELATION"].ToString());
                    if (dt11.Rows[0]["DOB"].ToString() != "" && Convert.ToDateTime(dt11.Rows[0]["DOB"]).ToString("dd/MM/yyyy") != "01/01/1900") // condition added by abhishek 14-02-2018 Because of exception 
                    {
                        txtdob2.Text = Convert.ToDateTime(dt11.Rows[0]["DOB"]).ToString("dd/MM/yyyy");
                    }
                }
            }
            if (dt2.Rows.Count > 0)
            {
                if (Convert.ToString(dt2.Rows[0]["JOINTNAME"].ToString()) != "")
                {
                    txtJointFName.Text = Convert.ToString(dt2.Rows[0]["JOINTNAME"].ToString());
                    ddlreljoint.SelectedValue = Convert.ToString(dt2.Rows[0]["JOINTRELATION"].ToString());
                    if (dt2.Rows[0]["JOINTDOB"].ToString() != "" && Convert.ToDateTime(dt2.Rows[0]["JOINTDOB"]).ToString("dd/MM/yyyy") != "01/01/1900") // condition added by abhishek 14-02-2018 Because of exception 
                    {
                        TxtDOB.Text = Convert.ToDateTime(dt2.Rows[0]["JOINTDOB"]).ToString("dd/MM/yyyy");
                    }
                }
            }
            if (dt12.Rows.Count > 0)
            {
                if (Convert.ToString(dt12.Rows[0]["JOINTNAME"].ToString()) != "")
                {
                    txtjname2.Text = Convert.ToString(dt12.Rows[0]["JOINTNAME"].ToString());
                    ddlrelationj2.SelectedValue = Convert.ToString(dt12.Rows[0]["JOINTRELATION"].ToString());
                    if (dt12.Rows[0]["JOINTDOB"].ToString() != "" && Convert.ToDateTime(dt12.Rows[0]["JOINTDOB"]).ToString("dd/MM/yyyy") != "01/01/1900") // condition added by abhishek 14-02-2018 Because of exception 
                    {
                        txtjbirth2.Text = Convert.ToDateTime(dt12.Rows[0]["JOINTDOB"]).ToString("dd/MM/yyyy");//Dhanya shetty//21-03-2017
                    }
                }
            }

            if (Convert.ToString(dt.Rows[0]["Minor_acc"].ToString()) == "1")
            {
                chkYes.Checked = true;
                dvminor.Visible = true;
                dvmname.Visible = true;
                dvmdate.Visible = true;
            }
            else
            {
                chkNo.Checked = true;
            }
            txtcustid.Text = Convert.ToString(dt.Rows[0]["M_Custno"].ToString());
            txtacconam.Text = Convert.ToString(dt.Rows[0]["M_OPRNAME"].ToString());
            txtmdate.Text = Convert.ToString(dt.Rows[0]["M_DOB"].ToString());

            if (Convert.ToString(dt.Rows[0]["M_RELATION"].ToString()) == "1")
                rdbmom.Checked = true;
            else if (Convert.ToString(dt.Rows[0]["M_RELATION"].ToString()) == "2")
                rdbdad.Checked = true;
            else if (Convert.ToString(dt.Rows[0]["M_RELATION"].ToString()) == "3")
                rdbguard.Checked = true;

            Submit.Focus();   
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtcustid_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtcustid.Text.ToString() == "")
            {
                return;
            }
            txtacconam.Text = CMN.GetCustName(txtcustid.Text.ToString(), Session["BRCD"].ToString());

            if (txtacconam.Text == "")
            {
                WebMsgBox.Show("Customer Not found", this.Page);
                txtcustid.Focus();
            }
            Ddlacctype.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtmdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int age = CMN.GetAge(Convert.ToDateTime(txtmdate.Text.ToString()));

            if (age > 18)
            {
                WebMsgBox.Show("Invalid Birthdate", this.Page);
                txtmdate.Text = "";
                txtmdate.Focus();
            }
            else
            {
                DateTime mDate = Convert.ToDateTime(txtmdate.Text.ToString());
                DateTime pDate = Convert.ToDateTime(Session["EntryDate"].ToString());
                if (mDate > pDate)
                {
                    WebMsgBox.Show("Not greater than today's date", this.Page);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void rdbmom_CheckedChanged(object sender, EventArgs e)
    {
        txtmdate.Focus();
    }

    protected void rdbdad_CheckedChanged(object sender, EventArgs e)
    {
        txtmdate.Focus();
    }

    protected void rdbguard_CheckedChanged(object sender, EventArgs e)
    {
        txtmdate.Focus();
    }

    protected void txtacconam_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtname.Text = custnob[0].ToString();
                txtcstno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                if (ViewState["Flag"].ToString() != "AD")
                {
                    txtaccno.Focus();
                }
                else
                {
                    Ddlacctype.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        rdbmom.Focus();
    }

    protected void Ddlacctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtAccType.Text = BD.GetNOACCT(Ddlacctype.SelectedItem.Text);
            DdlModeofOpr.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void DdlModeofOpr_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtmopr.Text = BD.GetMODENO(DdlModeofOpr.SelectedItem.Text);

            if (DdlModeofOpr.SelectedItem.Text == "JOINT" && ViewState["GLCODE"].ToString() != "3")
                Div_Joint.Visible = true;
            else
                Div_Joint.Visible = false;

            if (ViewState["GLCODE"].ToString() == "3")
            {
                Div_nominee.Visible = false;
                Div_Joint.Visible = false;
            }

            chkYes.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtrefcustname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtrefcustname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtrefcustname.Text = custnob[0].ToString();
                Txtrefcustno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                TxtAgentNo.Focus();
            }
            if (Txtrefcustno.Text == txtcstno.Text)
            {
                WebMsgBox.Show("Ref Customer No Cannot be same as Customer No", this.Page);
                txtrefcustname.Text = "";
                Txtrefcustno.Text = "";

                Txtrefcustno.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAgentname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AGNAME = TxtAgentname.Text;
            string[] Agentnob = AGNAME.Split('_');
            if (Agentnob.Length > 1)
            {
                TxtAgentname.Text = Agentnob[0].ToString();
                TxtAgentNo.Text = (string.IsNullOrEmpty(Agentnob[1].ToString()) ? "" : Agentnob[1].ToString());
                ViewState["AGENTNO"] = TxtAgentNo.Text;
                ViewState["DRGL"] = Agentnob[2].ToString();
                ViewState["GL"] = Agentnob[3].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtAgentNo.Text + "_" + ViewState["DRGL"].ToString() + "_" + ViewState["GL"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txtrefcustno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sql, AT;
            sql = AT = "";
            string custname = accop.Getcustname(Txtrefcustno.Text);
            string[] name = custname.Split('_');
            txtrefcustname.Text = name[0].ToString();
            string RC = txtrefcustname.Text;
            TxtAgentNo.Focus();
            if (RC == "")
            {
                WebMsgBox.Show("Customer not found", this.Page);
                Txtrefcustno.Text = "";
                Txtrefcustno.Focus();
                return;
            }
            if (Txtrefcustno.Text == txtcstno.Text)
            {
                WebMsgBox.Show("Ref Customer No Cannot be same as Customer No", this.Page);
                Txtrefcustno.Text = "";
                txtrefcustname.Text = "";
                Txtrefcustno.Focus();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAgentNo_TextChanged(object sender, EventArgs e)
    {
        try
        {

            DataTable dt1 = new DataTable();
            string sql1 = "select GLNAME,LASTNO,GLCODE from GLMAST where SUBGLCODE='" + TxtAgentNo.Text + "' AND GLCODE='2' AND BRCD='" + Session["BRCD"].ToString() + "'";
            dt1 = conn.GetDatatable(sql1);

            if (dt1.Rows.Count != 0)
            {
                TxtAgentname.Text = dt1.Rows[0]["GLNAME"].ToString();
                ViewState["AC"] = dt1.Rows[0]["LASTNO"].ToString();
                ViewState["DRGL"] = dt1.Rows[0]["GLCODE"].ToString();
                ViewState["GL"] = dt1.Rows[0]["GLCODE"].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtAgentNo.Text + "_" + ViewState["DRGL"].ToString() + "_" + ViewState["GL"].ToString();
                txtNomName.Focus();
            }
            else
            {
                WebMsgBox.Show("Please Enter the valid Agent No...!!", this.Page);
                TxtAgentNo.Focus();
                TxtAgentNo.Text = "";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtJointCust1_TextChanged(object sender, EventArgs e) //added by ashok misal for joint custno and name 2018-02-20 
    {
        try
        {
            string Stage = accop.StageJointCheck(txtJointCust1.Text);
            if (Stage == "1001")
            {
                WebMsgBox.Show("Customer Not Authorized", this.Page);
                txtJointCust1.Focus();
            }
            else if (Stage == "1004")
            {
                WebMsgBox.Show("Customer Deleted", this.Page);
                txtJointCust1.Focus();
            }
            else
            {
                if (Convert.ToInt32(txtcstno.Text) == Convert.ToInt32(txtJointCust1.Text))
                {
                    WebMsgBox.Show("Account Opening CustNo and Joint Account CustNo Should Not be same", this.Page);
                    txtJointCust1.Text = "";
                    txtJointFName.Text = "";
                    TxtDOB.Text = "";
                    txtJointCust1.Focus();
                }
                else
                {
                    //txtJointFName.Text = accop.MasterNamejoint(txtJointCust1.Text);
                    DataTable DT = new DataTable();
                    DT = accop.MasterNamejoint(txtJointCust1.Text);
                    if (DT.Rows.Count > 0)
                    {
                        txtJointFName.Text = DT.Rows[0]["CUSTNAME"].ToString();
                        TxtDOB.Text = DT.Rows[0]["DOB"].ToString();
                        ddlreljoint.Focus();
                    }
                    else
                    {
                        WebMsgBox.Show("Customer Not Found", this.Page);
                    }
                }
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtJointCust2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Stage = accop.StageJointCheck(txtJointCust2.Text);
            if (Stage == "1001")
            {
                WebMsgBox.Show("Customer Not Authorized", this.Page);
            }
            else if (Stage == "1004")
            {
                WebMsgBox.Show("Customer Deleted", this.Page);
            }
            else
            {
                if (Convert.ToInt32(txtcstno.Text) == Convert.ToInt32(txtJointCust2.Text))
                {
                    WebMsgBox.Show("Account Opening CustNo and Joint Account CustNo Should Not be same", this.Page);
                    txtjname2.Text = "";
                    txtJointCust2.Text = "";
                    txtjbirth2.Text = "";
                    txtJointCust2.Focus();
                }
                else
                {
                    //txtjname2.Text = accop.MasterNamejoint(txtJointCust2.Text);
                    DataTable DT = new DataTable();
                    DT = accop.MasterNamejoint(txtJointCust2.Text);
                    if (DT.Rows.Count > 0)
                    {
                        txtjname2.Text = DT.Rows[0]["CUSTNAME"].ToString();
                        txtjbirth2.Text = DT.Rows[0]["DOB"].ToString();
                        ddlrelationj2.Focus();
                    }
                    else
                    {
                        WebMsgBox.Show("Customer Not Found", this.Page);
                    }
                }
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txttype.Text == "")
            {
                WebMsgBox.Show("Please enter product type", this.Page);
                txttype.Focus();
                return;
            }
            else if (txtcstno.Text == "")
            {
                WebMsgBox.Show("Please enter customer number", this.Page);
                txtcstno.Focus();
                return;
            }
            else if (txtAccType.Text == "")
            {
                WebMsgBox.Show("Please account type", this.Page);
                txtAccType.Focus();
                return;
            }
            else if (txtmopr.Text == "")
            {
                WebMsgBox.Show("Please enter mode of operation", this.Page);
                txtmopr.Focus();
                return;
            }

            string pcode = txttype.Text.ToString();
            string custno = txtcstno.Text.ToString();
            string brcd = Session["BRCD"].ToString();
            string accno = txtaccno.Text.ToString();
            string acctype = txtAccType.Text.ToString();
            string opendate = txtodate.Text.ToString();
            string mopr = txtmopr.Text.ToString();
            string moname = DdlModeofOpr.SelectedItem.Text.ToString();
            string mcid = txtcustid.Text.ToString();
            string moprnam = txtacconam.Text.ToString();

            string mdob = txtmdate.Text.ToString();
            string FCHK, FSMS, FRDC, FESTS, FAD, FIB, FMB;
            FCHK = FSMS = FRDC = FESTS = FAD = FIB = FMB = "";

            string MACC = "";
            MACC = chkYes.Checked == true ? "1" : "2";

            string Relation;
            Relation = "";
            if (rdbmom.Checked == true)
            {
                Relation = "1";
            }
            else if (rdbdad.Checked == true)
            {
                Relation = "2";
            }
            else if (rdbguard.Checked == true)
            {
                Relation = "3";
            }

            if (ViewState["Flag"].ToString() == "")
            {
                WebMsgBox.Show("Select the activity first ...!!", this.Page);
                return;
            }

            if (ViewState["Flag"].ToString() == "AT")
            {
                string AT = accop.GetStage(Session["BRCD"].ToString(), txtcstno.Text, txtaccno.Text, txttype.Text);
                if (AT == "1003")
                {
                    WebMsgBox.Show("Record already authorized ...!!", this.Page);
                    return;
                }
            }

            if (ViewState["Flag"].ToString() == "MD")
            {
                int RC = accop.update(custno, accno, opendate, txttype.Text, acctype, mopr, MACC, mcid, moprnam, Relation, mdob, FCHK, FSMS, FRDC, FESTS, FAD, FIB, FMB, Session["BRCD"].ToString(), Txtrefcustno.Text, TxtAgentNo.Text, Session["EntryDate"].ToString(), Session["MID"].ToString());
                nonmi1 = accop.nomiupdate1(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1002", "1", txtNomName.Text, ddlRelation.SelectedValue, txtBithdate.Text, txttype.Text, txtcstno.Text, txtaccno.Text);
                nonmi2 = accop.nomiupdate2(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1002", "2", txtnom2.Text, ddlrelation2.SelectedValue, txtdob2.Text, txttype.Text, txtcstno.Text, txtaccno.Text);
                joint1 = accop.jointupdate1(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1002", "1", txtJointFName.Text, ddlreljoint.SelectedValue, TxtDOB.Text, txttype.Text, txtcstno.Text, txtaccno.Text);
                joint2 = accop.jointupdate2(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1002", "2", txtjname2.Text, ddlrelationj2.SelectedValue, txtjbirth2.Text, txttype.Text, txtcstno.Text, txtaccno.Text);
                if (RC > 0 || nonmi1 > 0 || nonmi2 > 0 || joint1 > 0 || joint2 > 0)
                {
                    BindGrid();
                    Cleardata();
                    WebMsgBox.Show("Record successfully Modified ...!!", this.Page);
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Accopening _Mod_" + txttype.Text + "_" + txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
            }
            else if (ViewState["Flag"].ToString() == "DL")
            {
                int RC = accop.delete(brcd, pcode, custno, accno);
                nonmi1 = accop.nomidelete1(Session["BRCD"].ToString(), txttype.Text, txtcstno.Text, "1", txtaccno.Text);
                nonmi2 = accop.nomidelete2(Session["BRCD"].ToString(), txttype.Text, txtcstno.Text, "2", txtaccno.Text);
                joint1 = accop.jointdelete1(Session["BRCD"].ToString(), txttype.Text, txtcstno.Text, "1", txtaccno.Text);
                joint2 = accop.jointdelete2(Session["BRCD"].ToString(), txttype.Text, txtcstno.Text, "2", txtaccno.Text);
                if (RC > 0 || nonmi1 > 0 || nonmi2 > 0 || joint1 > 0 || joint2 > 0)
                {
                    BindGrid();
                    Cleardata();
                    WebMsgBox.Show("Record successfully deleted ...!!", this.Page);
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Accopening _Del_" + txttype.Text + "_" + txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
                else
                {
                    Cleardata();
                    WebMsgBox.Show("Record is not deleted ...!!", this.Page);
                    return;
                }
            }
            else if (ViewState["Flag"].ToString() == "AT")
            {
                int RC = accop.authorize(brcd, pcode, custno, accno, Session["MID"].ToString());
                nonmi1 = accop.nomiautho1(Session["BRCD"].ToString(), txttype.Text, txtcstno.Text, "1", txtaccno.Text, Session["MID"].ToString());
                nonmi2 = accop.nomiautho2(Session["BRCD"].ToString(), txttype.Text, txtcstno.Text, "2", txtaccno.Text, Session["MID"].ToString());
                joint1 = accop.jointautho1(Session["BRCD"].ToString(), txttype.Text, txtcstno.Text, "1", txtaccno.Text, Session["MID"].ToString());
                joint2 = accop.jointautho2(Session["BRCD"].ToString(), txttype.Text, txtcstno.Text, "2", txtaccno.Text, Session["MID"].ToString());//Dhanya shetty//21-03-2017

                if (RC > 0 || nonmi1 > 0 || nonmi2 > 0 || joint1 > 0 || joint2 > 0)
                {
                    BindGrid();
                    Cleardata();
                    WebMsgBox.Show("Record successfully authorized ...!!", this.Page);
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Accopening _auth_" + txttype.Text + "_" + txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
                else
                {
                    Cleardata();
                    WebMsgBox.Show("Warning: User is restricted to authorize ...!!", this.Page);
                    return;
                }
            }

            txttype.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Clear_Click(object sender, EventArgs e)
    {
        Cleardata();
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("Frmaccopeaning.aspx?Flag=AD.aspx", true);
    }

    protected void LnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["SUBGLCODE"] = ARR[0].ToString();
            ViewState["ACCNO"] = ARR[1].ToString();
            ViewState["Flag"] = "MD";
            Submit.Text = "Modify";
            CUSTNODIV.Visible = false;
            txtaccno.Enabled = true;
            DIVACC.Visible = true;
            TblDiv_MainWindow.Visible = true;
            ENDN(true);
            callaccnInfo();
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
            ViewState["SUBGLCODE"] = ARR[0].ToString();
            ViewState["ACCNO"] = ARR[1].ToString();
            ViewState["Flag"] = "DL";
            Submit.Text = "Delete";
            TblDiv_MainWindow.Visible = true;
            CUSTNODIV.Visible = false;
            DIVACC.Visible = true;
            txtaccno.Enabled = true;
            callaccnInfo();
            ENDN(false);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void LnkAutorise_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["SUBGLCODE"] = ARR[0].ToString();
            ViewState["ACCNO"] = ARR[1].ToString();
            ViewState["Flag"] = "AT";
            Submit.Text = "Authorise";
            TblDiv_MainWindow.Visible = true;
            CUSTNODIV.Visible = false;
            DIVACC.Visible = true;
            txtaccno.Enabled = true;
            callaccnInfo();
            ENDN(false);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}