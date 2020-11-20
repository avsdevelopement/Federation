using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FraAgentcal : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAgentCommision AC = new ClsAgentCommision();
    ClsAuthorized AT = new ClsAuthorized();
    ClsCommon CMN = new ClsCommon();
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Autoprd4.ContextKey = Session["BRCD"].ToString();
                autoAgname.ContextKey = Session["BRCD"].ToString();
                TXtAGCDAMC.Enabled = true;

                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                BindLabel();
                txtAgCode.Focus();
                BindBranch();
                BindBranchTDS();
                btnSubmit.Visible = true;
                btncheckhow.Visible = false;
                string sql2 = "select LISTVALUE from parameter where  listfield='CommissionGlcode'";
                string Glcode2or6 = conn.sExecuteScalar(sql2);
                if (Convert.ToInt32(Glcode2or6) == 6)
                {
                    Agentcollect.Visible = false;
                    Transfercollect.Visible = false;
                    Cashcollect.Visible = false;
                    Agentcollect1.Visible = false;
                    Transfercollect1.Visible = false;
                    Cashcollect1.Visible = false;
                    btncheckhow.Visible = false;

                }
                // }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindLabel()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = AC.GetLabel();
            if (dt.Rows.Count > 0)
            {
                lblCommision.InnerText = dt.Rows[0]["1004"].ToString();
                lblTDS.InnerText = dt.Rows[0]["1007"].ToString();
                lblSec.InnerText = dt.Rows[0]["1005"].ToString();
                lblPF.InnerText = dt.Rows[0]["1008"].ToString();
                lblTrav.InnerText = dt.Rows[0]["1006"].ToString();

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    #region Text Changed Events

    protected void txtAgCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AGCD = "";
            AGCD = txtAgCode.Text;

            string[] TD = AC.GetAgNo(txtAgCode.Text, Session["BRCD"].ToString()).Split('_');

            if (TD.Length > 0)
            {
                txtAgCode.Text = AGCD;
                txtAgName.Text = TD[0].ToString();
                CustNo.Text = TD[1].ToString();
                Autoprd4.ContextKey = Session["BRCD"].ToString();
                txtFDate.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter valid Agent code!.....", this.Page);
                txtAgCode.Text = "";
                txtAgName.Text = "";
                txtAgCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAgName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AGNAME = txtAgName.Text;
            string[] AgentCode = AGNAME.Split('_');
            if (AgentCode.Length > 1)
            {
                txtAgCode.Text = AgentCode[0].ToString();
                txtAgName.Text = (string.IsNullOrEmpty(AgentCode[1].ToString()) ? "" : AgentCode[1].ToString());
                CustNo.Text = AgentCode[2].ToString();
                Autoprd4.ContextKey = Session["BRCD"].ToString();
                txtFDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtProcode4_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtAgCode.Text == "")
            {
                lblMessage.Text = "Please Enter Agent Code...!";
                ModalPopup.Show(this.Page);
                txtAgCode.Focus();
                return;
            }

            string AC1;
            AC1 = AC.Getaccno(TxtProcode4.Text, Session["BRCD"].ToString(), "");

            if (AC1 != null)
            {
                string[] ACc = AC1.Split('-'); ;
                ViewState["ACCNO"] = ACc[0].ToString();
                ViewState["GLCODE"] = ACc[1].ToString();
                TxtProName4.Text = ACc[2].ToString();
                Autoaccname4.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode4.Text + "_" + ViewState["GLCODE"].ToString();
                TxtAccNo4.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code!.....", this.Page);
                TxtProName4.Text = "";
                TxtAccNo4.Text = "";
                TxtProName4.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtProName4_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtProName4.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtProName4.Text = custnob[0].ToString();
                TxtProcode4.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] ACC = AC.Getaccno(TxtProcode4.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
                ViewState["ACCNO"] = ACC[0].ToString();
                ViewState["GLCODE"] = ACC[1].ToString();
                Autoaccname4.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode4.Text;
                TxtAccNo4.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccNo4_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = CMN.GetCustNoNameGL1(TxtProcode4.Text.ToString(), TxtAccNo4.Text.ToString(), Session["BRCD"].ToString());

            if (DT.Rows.Count > 0)
            {
                TxtAccName4.Text = DT.Rows[0][3].ToString();
            }
            else
            {
                lblMessage.Text = "Account Number not found...!";
                ModalPopup.Show(this.Page);
                TxtAccNo4.Text = "";
                TxtAccName4.Text = "";
                TxtAccNo4.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccName4_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtAccName4.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtAccName4.Text = custnob[0].ToString();
                TxtAccNo4.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
            }
            else
            {
                lblMessage.Text = "Invalid Account Number...!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtTDate_TextChanged(object sender, EventArgs e)
    {
        //CHECK COMMISSION ALREADY TRANSFER OR NOT
        string EEEDATE = txtFDate.Text;
        string count = AC.CHECHDATA1(Session["BRCD"].ToString(), txtFDate.Text, txtTDate.Text, txtAgCode.Text);
        
        if (Convert.ToInt32(count) > 0)
        {
            string ParaShowRep = CMN.GetUniversalPara("CHECK_COMMREPORT","1");
            if (ParaShowRep == "Y")
            {
                WebMsgBox.Show("Already Posted , You can see report...!",this.Page);
                Btn_CheckReport.Visible = true;
                return;
            }
            else
            {
                lblMessage.Text = "This Agent Comission For That Month Allready transfer........:";
                ModalPopup.Show(this.Page);
            }
        }

        else
        {
            string sql = "select LISTVALUE from parameter where  listfield='AgentCommission'";
            string CommisionCase = conn.sExecuteScalar(sql);

            string sql2 = "select LISTVALUE from parameter where  listfield='CommissionGlcode'";
            string Glcode2or6 = conn.sExecuteScalar(sql2);

            string sql1 = "SELECT glcode FROM GLMAST WHERE BRCD='" + Session["BRCD"].ToString() + "' and subglcode='" + txtAgCode.Text + "'";
            string CheckBalOrDaily = conn.sExecuteScalar(sql1);

            if (Convert.ToInt32(CommisionCase) == 1 && Convert.ToInt32(CheckBalOrDaily) != 15 && Convert.ToInt32(Glcode2or6) == 2)
            {
                string FLag = null;
                lbltype.Text = "Daily Commission";
                DataTable DT = new DataTable();
                if (Agentcollect.Checked == false && Transfercollect.Checked == false && Cashcollect.Checked == false)
                {
                    WebMsgBox.Show("Please Select Type of Collection", this.Page);
                }
                if (Agentcollect.Checked == true && Transfercollect.Checked == false && Cashcollect.Checked == false)
                {
                    FLag = "AGENT";
                }
                if (Transfercollect.Checked == true && Agentcollect.Checked == false && Cashcollect.Checked == false)
                {
                    FLag = "TR";
                }
                if (Cashcollect.Checked == true && Transfercollect.Checked == false && Agentcollect.Checked == false)
                {
                    FLag = "CASH";
                }
                if (Agentcollect.Checked == true && Transfercollect.Checked == true && Cashcollect.Checked == false)
                {
                    FLag = "AGENT_TR";
                }
                if (Agentcollect.Checked == true && Transfercollect.Checked == true && Cashcollect.Checked == true)
                {
                    FLag = "AGENT_TR_CASH";
                }
                if (Agentcollect.Checked == true && Transfercollect.Checked == false && Cashcollect.Checked == true)
                {
                    FLag = "AGENT_CASH";
                }
                if (Agentcollect.Checked == false && Transfercollect.Checked == true && Cashcollect.Checked == true)
                {
                    FLag = "TR_CASH";
                }
                DT = AC.getyspmdata(FLag, Session["BRCD"].ToString(), txtAgCode.Text, txtAgCode.Text, txtFDate.Text, txtTDate.Text);
                try
                {
                    if (DT.Rows.Count > 0)
                    {

                        txttcoll.Text = DT.Rows[0]["TOTALCOLLECTION"].ToString();
                        txtTotColl.Text = DT.Rows[0]["TOTALAMT"].ToString();
                        txtCommision.Text = AC.AgentComm().ToString();
                        txtCommAmt.Text = DT.Rows[0]["COMISSION"].ToString();
                        txtTDDeduction.Text = AC.AgentCommTds().ToString();
                        txtTdAmt.Text = DT.Rows[0]["TDS"].ToString();
                        TxtAGCDSEC.Text = AC.AgentSecurity().ToString();
                        TxtAgentSec.Text = DT.Rows[0]["AGENTSECURITY"].ToString();
                        TXtAGCDAMC.Text = DT.Rows[0]["AMC"].ToString();
                        txttrev.Text = AC.Agenttravelling().ToString();
                        txttravelexp.Text = DT.Rows[0]["TRAVELEXP"].ToString();
                        txtNetCommision.Text = DT.Rows[0]["NETCOMISSION"].ToString();

                    }
                    else
                    {
                        WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
                    }
                }
                catch (Exception Ex)
                {
                    ExceptionLogging.SendErrorToText(Ex);
                }
                btncheckhow.Visible = true;
            }
            else if (Convert.ToInt32(CommisionCase) == 1 && Convert.ToInt32(CheckBalOrDaily) != 15 && Convert.ToInt32(Glcode2or6) == 6)
            {


            }
            else if (Convert.ToInt32(CommisionCase) == 2 && Convert.ToInt32(CheckBalOrDaily) != 15 && Convert.ToInt32(Glcode2or6) == 2)
            {
                string FLag = null;
                lbltype.Text = "Daily Commission";
                DataTable DT = new DataTable();
                string check = AC.checkcondition(Session["BRCD"].ToString(), txtFDate.Text, txtTDate.Text, txtAgCode.Text);
                int condi = Convert.ToInt32(check);
                if (condi > 500000)
                {
                    txtCommision.Text = AC.AgentCommABOVE().ToString();
                }
                else
                {
                    txtCommision.Text = AC.AgentComm().ToString();
                }
                if (Agentcollect.Checked == false && Transfercollect.Checked == false && Cashcollect.Checked == false)
                {
                    WebMsgBox.Show("Please Select Type of Collection", this.Page);
                }
                if (Agentcollect.Checked == true && Transfercollect.Checked == false && Cashcollect.Checked == false)
                {
                    FLag = "AGENT";
                }
                if (Transfercollect.Checked == true && Agentcollect.Checked == false && Cashcollect.Checked == false)
                {
                    FLag = "TR";
                }
                if (Cashcollect.Checked == true && Transfercollect.Checked == false && Agentcollect.Checked == false)
                {
                    FLag = "CASH";
                }
                if (Agentcollect.Checked == true && Transfercollect.Checked == true && Cashcollect.Checked == false)
                {
                    FLag = "AGENT_TR";
                }
                if (Agentcollect.Checked == true && Transfercollect.Checked == true && Cashcollect.Checked == true)
                {
                    FLag = "AGENT_TR_CASH";
                }
                if (Agentcollect.Checked == true && Transfercollect.Checked == false && Cashcollect.Checked == true)
                {
                    FLag = "AGENT_CASH";
                }
                if (Agentcollect.Checked == false && Transfercollect.Checked == true && Cashcollect.Checked == true)
                {
                    FLag = "TR_CASH";
                }
                DT = AC.getmarathwadadata(FLag, Session["BRCD"].ToString(), txtAgCode.Text, txtAgCode.Text, txtFDate.Text, txtTDate.Text);
                try
                {
                    if (DT.Rows.Count > 0)
                    {

                        txttcoll.Text = DT.Rows[0]["TOTALCOLLECTION"].ToString();
                        txtTotColl.Text = DT.Rows[0]["TOTALAMT"].ToString();
                        txtCommision.Text = AC.AgentComm();
                        txtCommAmt.Text = DT.Rows[0]["COMISSION"].ToString();
                        txtTDDeduction.Text = AC.AgentCommTds().ToString();
                        txtTdAmt.Text = DT.Rows[0]["TDS"].ToString();
                        TxtAGCDSEC.Text = AC.AgentSecurity().ToString();
                        TxtAgentSec.Text = DT.Rows[0]["AGENTSECURITY"].ToString();
                        TXtAGCDAMC.Text = DT.Rows[0]["AMC"].ToString();
                        txttrev.Text = AC.Agenttravelling().ToString();
                        txttravelexp.Text = DT.Rows[0]["TRAVELEXP"].ToString();
                        txtNetCommision.Text = DT.Rows[0]["NETCOMISSION"].ToString();

                    }
                    else
                    {
                        WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
                    }
                }
                catch (Exception Ex)
                {
                    ExceptionLogging.SendErrorToText(Ex);
                }

                btncheckhow.Visible = true;

            }
            else if (Convert.ToInt32(CommisionCase) == 2 && Convert.ToInt32(CheckBalOrDaily) != 15 && Convert.ToInt32(Glcode2or6) == 6)
            {

                string FLag = null;
                lbltype.Text = "Daily Commission";
                DataTable DT = new DataTable();
                string check = AC.checkcondition(Session["BRCD"].ToString(), txtFDate.Text, txtTDate.Text, txtAgCode.Text);
                int condi = Convert.ToInt32(check);
                if (condi > 500000)
                {
                    txtCommision.Text = AC.AgentCommABOVE().ToString();
                }
                else
                {
                    txtCommision.Text = AC.AgentComm().ToString();
                }

                DT = AC.GEtCommissionForGL(Session["BRCD"].ToString(), Glcode2or6.ToString(), txtAgCode.Text, txtAgCode.Text, txtFDate.Text, txtTDate.Text);
                try
                {
                    if (DT.Rows.Count > 0)
                    {

                        txttcoll.Text = DT.Rows[0]["TOTALCOLLECTION"].ToString();
                        txtTotColl.Text = DT.Rows[0]["TOTALAMT"].ToString();
                        txtCommision.Text = AC.AgentComm();
                        txtCommAmt.Text = DT.Rows[0]["COMISSION"].ToString();
                        txtTDDeduction.Text = AC.AgentCommTds().ToString();
                        txtTdAmt.Text = DT.Rows[0]["TDS"].ToString();
                        TxtAGCDSEC.Text = AC.AgentSecurity().ToString();
                        TxtAgentSec.Text = DT.Rows[0]["AGENTSECURITY"].ToString();
                        TXtAGCDAMC.Text = DT.Rows[0]["AMC"].ToString();
                        txttrev.Text = AC.Agenttravelling().ToString();
                        txttravelexp.Text = DT.Rows[0]["TRAVELEXP"].ToString();
                        txtNetCommision.Text = DT.Rows[0]["NETCOMISSION"].ToString();

                    }
                    else
                    {
                        WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
                    }
                }
                catch (Exception Ex)
                {
                    ExceptionLogging.SendErrorToText(Ex);
                }
            }
            else if (Convert.ToInt32(CommisionCase) == 1 && Convert.ToInt32(CheckBalOrDaily) == 15)
            {
                lbltype.Text = "BalVikas Commission";
                btncheckhow.Enabled = false;
                DT = AC.GetRdCommision(Session["BRCD"].ToString(), txtAgCode.Text, txtAgCode.Text, txtFDate.Text, txtTDate.Text);
                try
                {
                    if (DT.Rows.Count > 0)
                    {

                        txttcoll.Text = DT.Rows[0]["TOTALCOLLECTION"].ToString();
                        txtTotColl.Text = DT.Rows[0]["TOTALAMT"].ToString();
                        txtCommision.Text = AC.AgentComm();
                        txtCommAmt.Text = DT.Rows[0]["COMISSION"].ToString();
                        txtTDDeduction.Text = AC.AgentCommTds().ToString();
                        txtTdAmt.Text = DT.Rows[0]["TDS"].ToString();
                        TxtAGCDSEC.Text = AC.AgentSecurity().ToString();
                        TxtAgentSec.Text = DT.Rows[0]["AGENTSECURITY"].ToString();
                        TXtAGCDAMC.Text = DT.Rows[0]["AMC"].ToString();
                        txttrev.Text = AC.Agenttravelling().ToString();
                        txttravelexp.Text = DT.Rows[0]["TRAVELEXP"].ToString();
                        txtNetCommision.Text = DT.Rows[0]["NETCOMISSION"].ToString();

                    }
                    else
                    {
                        WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
                    }
                }
                catch (Exception Ex)
                {
                    ExceptionLogging.SendErrorToText(Ex);
                }


                btncheckhow.Visible = true;
            }
            else if (Convert.ToInt32(CommisionCase) == 2 && Convert.ToInt32(CheckBalOrDaily) == 15)
            {
                lbltype.Text = "BalVikas Commission";
                btncheckhow.Enabled = false;
                DT = AC.GetRdCommision(Session["BRCD"].ToString(), txtAgCode.Text, txtAgCode.Text, txtFDate.Text, txtTDate.Text);
                try
                {
                    if (DT.Rows.Count > 0)
                    {

                        txttcoll.Text = DT.Rows[0]["TOTALCOLLECTION"].ToString();
                        txtTotColl.Text = DT.Rows[0]["TOTALAMT"].ToString();
                        txtCommAmt.Text = DT.Rows[0]["COMISSION"].ToString();
                        txtTDDeduction.Text = AC.AgentCommTds().ToString();
                        txtTdAmt.Text = DT.Rows[0]["TDS"].ToString();
                        TxtAGCDSEC.Text = AC.AgentSecurity().ToString();
                        TxtAgentSec.Text = DT.Rows[0]["AGENTSECURITY"].ToString();
                        TXtAGCDAMC.Text = DT.Rows[0]["AMC"].ToString();
                        txttrev.Text = AC.Agenttravelling().ToString();
                        txttravelexp.Text = DT.Rows[0]["TRAVELEXP"].ToString();
                        txtNetCommision.Text = DT.Rows[0]["NETCOMISSION"].ToString();

                    }
                    else
                    {
                        WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
                    }
                }
                catch (Exception Ex)
                {
                    ExceptionLogging.SendErrorToText(Ex);
                }
                btncheckhow.Visible = true;
            }

        }
    }

    protected void txtCommision_TextChanged(object sender, EventArgs e)
    {
        txtCommAmt.Text = AC.AgentComm().ToString();
        txtCommAmt.Text = Convert.ToString((Convert.ToDouble(txtTotColl.Text)) * (Convert.ToInt32(txtCommision.Text)) / 100);

        txtTdAmt.Text = Convert.ToString((Convert.ToDouble(txtCommAmt.Text)) * (Convert.ToInt32(txtTDDeduction.Text)) / 100);

        if (txtTdAmt.Text != "")
        {
            txtNetCommision.Text = Convert.ToString((Convert.ToDouble(txtCommAmt.Text)) - (Convert.ToDouble(txtTdAmt.Text)));
            TxtProcode4.Focus();
        }
        else
        {
            btnSubmit.Focus();
        }
    }

    protected void txtTDDeduction_TextChanged(object sender, EventArgs e)
    {
        txtTdAmt.Text = Convert.ToString((Convert.ToDouble(txtCommAmt.Text)) * (Convert.ToInt32(txtTDDeduction.Text)) / 100);

        if (txtTdAmt.Text != "")
        {
            txtNetCommision.Text = Convert.ToString((Convert.ToDouble(txtCommAmt.Text)) - (Convert.ToDouble(txtTdAmt.Text)));
            TxtProcode4.Focus();
        }
        else
        {
            btnSubmit.Focus();
        }
    }

    #endregion

    #region Click Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (RdbSingle.Checked == true)
            {
                string CheckABB = "Y";

                if (ddlLoanBrName.SelectedValue.ToString() != Session["BRCD"].ToString() && ddlLoanBrName.SelectedValue.ToString() != "0" && DDlBranchTDS.SelectedValue.ToString() != Session["BRCD"].ToString() && DDlBranchTDS.SelectedValue.ToString() != "0")
                {
                    string ST = "", agacc = "", prod = "";
                    ST = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();
                    DT = AC.GetPlAcc("1004");
                    if (DT.Rows.Count > 0)
                    {
                        DT = AC.GetPlAcc("1004");
                        int RM = AC.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), DT.Rows[0]["PLACC"].ToString(), "0", "Agent commision trf/" + txtAgCode.Text, "", txtCommAmt.Text.ToString(), "2", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0", Session["BRCD"].ToString());
                        if (RM > 0)
                        {
                            DT = AC.GetPlAcc("1007");
                            int RN1 = 0;
                            if (Convert.ToDouble(txtTdAmt.Text) != 0)
                            {
                                if (Session["BNKCDE"].ToString() == "1003")
                                {
                                    RN1 = AC.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), DT.Rows[0]["PLACC"].ToString(), "1", "TDS DEDUCTION trf/" + txtAgCode.Text, "", txtTdAmt.Text.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0", Session["BRCD"].ToString());// amruta    28-09-2017
                                }
                                else
                                {
                                    RN1 = AC.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), DT.Rows[0]["PLACC"].ToString(), "0", "TDS DEDUCTION trf/" + txtAgCode.Text, "", txtTdAmt.Text.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0", Session["BRCD"].ToString());
                                }
                                string glcode1 = AT.getglcode(DDlBranchTDS.SelectedValue.ToString(), TxtTDSPrd.Text);
                                int RN6 = AC.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), DT.Rows[0]["PLACC"].ToString(), "0", "TDS DEDUCTION Cr To Ho trf/" + txtAgCode.Text, "", txtTdAmt.Text.ToString(), "2", "43", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0", Session["BRCD"].ToString());
                                int RN10 = AC.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode1.ToString(), TxtTDSPrd.Text.ToString(), TxtTDSAccno.Text.ToString(), "TDS Credit Ho trf/" + txtAgCode.Text, "", txtTdAmt.Text.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", "1", Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0", Session["BRCD"].ToString());

                            }
                            int RN2 = 0;
                            DT = AC.GetPlAcc("1005");

                            if (Convert.ToDouble(TxtAgentSec.Text) != 0)
                            {
                                string Accno;
                                string InAccNo = AC.GetGlAccNo(ddlLoanBrName.SelectedValue.ToString(), TxtProdCde.Text);
                                if (InAccNo == "Y" || InAccNo == "y")
                                    //Accno = AC.GetAGAccNo(ddlLoanBrName.SelectedValue.ToString(), txtAgCode.Text, DT.Rows[0]["PLACC"].ToString());
                                    Accno = TxtAccNo.Text;
                                else
                                    Accno = "0";
                                agacc = string.IsNullOrEmpty(TxtAccNo.Text) ? "0" : TxtAccNo.Text;
                                prod = string.IsNullOrEmpty(TxtProdCde.Text) ? DT.Rows[0]["PLACC"].ToString() : TxtProdCde.Text;
                                string glcode2 = AT.getglcode(DDlBranchTDS.SelectedValue.ToString(), TxtProdCde.Text);
                                //RN2 = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), prod, agacc, "AGENT SECURITY trf/" + txtAgCode.Text, "", TxtAgentSec.Text.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "", CustNo.Text.ToString(), TxtAccName4.Text, "", "0");
                                RN2 = AC.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), DT.Rows[0]["PLACC"].ToString(), "0", "AGENT SECURITY trf/" + txtAgCode.Text, "", TxtAgentSec.Text.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0", Session["BRCD"].ToString()); //amruta    28/09/2017
                                int RN7 = AC.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), DT.Rows[0]["PLACC"].ToString(), "0", "AGENT SECURITY Cr To Ho trf/" + txtAgCode.Text, "", TxtAgentSec.Text.ToString(), "2", "43", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0", Session["BRCD"].ToString());
                                int RN11 = AC.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode2.ToString(), prod, Accno, "AGENT SECURITY Credit Ho trf/" + txtAgCode.Text, "", TxtAgentSec.Text.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", "1", Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0", Session["BRCD"].ToString());
                            }
                            int RN3 = 0;
                            DT = AC.GetPlAcc("1008");
                            if (Convert.ToDouble(TXtAGCDAMC.Text) != 0)
                            {
                                string Accno = "0";
                                string InAccNo = AC.GetGlAccNo(Session["BRCD"].ToString(), DT.Rows[0]["PLACC"].ToString());
                                if (InAccNo == "Y" || InAccNo == "y")
                                    Accno = AC.GetAGAccNo(Session["BRCD"].ToString(), txtAgCode.Text, DT.Rows[0]["PLACC"].ToString());
                                else
                                    Accno = txtAgCode.Text;
                                // RN3 = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), DT.Rows[0]["PLACC"].ToString(), "0", "AMC CHARGES trf/" + txtAgCode.Text, "", TXtAGCDAMC.Text, "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "", CustNo.Text.ToString(), TxtAccName4.Text, "", "0");
                                RN3 = AC.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), DT.Rows[0]["PLACC"].ToString(), Accno, "AMC CHARGES trf/" + txtAgCode.Text, "", TXtAGCDAMC.Text, "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0", Session["BRCD"].ToString()); //Amruta  28/09/2017
                            }
                            int RN5 = 0;
                            DT = AC.GetPlAcc("1006");
                            if (Convert.ToDouble(txttravelexp.Text) != 0)
                            {
                                RN5 = AC.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), DT.Rows[0]["PLACC"].ToString(), "0", "TARVELLING EXPENCES trf/" + txtAgCode.Text, "", txttravelexp.Text, "2", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0", Session["BRCD"].ToString());
                            }
                            string glcode = AT.getglcode(Session["BRCD"].ToString(), TxtProcode4.Text);  //added by ashok misal for gettting glcode for 307
                            int RN4 = AC.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, TxtProcode4.Text, TxtAccNo4.Text, "Agent AccNo trf/" + txtAgCode.Text, "", txtNetCommision.Text.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0", Session["BRCD"].ToString());
                            ////////////////////ABB VOUCHER POST IF REQUIRED//////////////////////////////
                            DataTable DT4 = new DataTable();
                            DataTable DT5 = new DataTable();
                            DT4 = AC.GetADMSubGl("1");
                            DT5 = AC.GetADMSubGl(Session["BRCD"].ToString());
                            int RN8 = AC.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT4.Rows[0]["ADMGlCode"].ToString(), DT4.Rows[0]["ADMSubGlCode"].ToString(), "0", "Total Credit To Ho  trf/" + txtAgCode.Text, "", (Convert.ToDouble(TxtAgentSec.Text) + Convert.ToDouble(txtTdAmt.Text)).ToString(), "1", "43", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0", Session["BRCD"].ToString());
                            int RN9 = AC.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT5.Rows[0]["ADMGlCode"].ToString(), DT5.Rows[0]["ADMSubGlCode"].ToString(), "0", "Total Debit To Ho trf/" + txtAgCode.Text, "", (Convert.ToDouble(TxtAgentSec.Text) + Convert.ToDouble(txtTdAmt.Text)).ToString(), "2", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", "1", Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0", Session["BRCD"].ToString());


                            int DATA = AC.INSERTAGENTCOMMI(Session["EntryDate"].ToString(), txtFDate.Text, txtTDate.Text, txtAgCode.Text, Session["BRCD"].ToString(), txttcoll.Text, txtTotColl.Text, txtCommAmt.Text, txtTdAmt.Text, TxtAgentSec.Text, TXtAGCDAMC.Text, txttravelexp.Text, txtNetCommision.Text, "1", Session["MID"].ToString(), ST.ToString());
                            if (RN1 > 0 && RN2 > 0 && RN2 > 0 && RN4 > 0 && DATA > 0)
                            {
                                FL = "Insert";//Dhanya Shetty
                                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Agent_Comms _" + ST + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                ClearData();
                                lblMessage.Text = "Commission Successfully Transfer To Agent Account And Voucher No Is........:" + ST + "";
                                ModalPopup.Show(this.Page);

                            }
                        }
                        else
                        {
                            ClearData();
                            WebMsgBox.Show("Debit Not Success", this.Page);

                        }

                    }
                }
                else if (ddlLoanBrName.SelectedValue.ToString() == Session["BRCD"].ToString() && DDlBranchTDS.SelectedValue.ToString() == Session["BRCD"].ToString())
                {
                    string ST = "", agacc = "", prod = "";
                    ST = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                    DT = AC.GetPlAcc("1004");
                    if (DT.Rows.Count > 0)
                    {
                        DT = AC.GetPlAcc("1004");
                        int RM = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), DT.Rows[0]["PLACC"].ToString(), "0", "Agent commision trf/" + txtAgCode.Text, "", txtCommAmt.Text.ToString(), "2", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0");
                        if (RM > 0)
                        {
                            DT = AC.GetPlAcc("1007");
                            int RN1 = 0;
                            if (Convert.ToDouble(txtTdAmt.Text) != 0)
                            {
                                string glcode1 = AT.getglcode(DDlBranchTDS.SelectedValue.ToString(), TxtTDSPrd.Text);
                                if (Session["BNKCDE"].ToString() == "1003")
                                    RN1 = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode1.ToString(), TxtTDSPrd.Text.ToString(), TxtTDSAccno.Text.ToString(), "TDS DEDUCTION trf/" + txtAgCode.Text, "", txtTdAmt.Text.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0");// amruta    28-09-2017
                                else
                                    RN1 = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode1.ToString(), TxtTDSPrd.Text.ToString(), TxtTDSAccno.Text.ToString(), "TDS DEDUCTION trf/" + txtAgCode.Text, "", txtTdAmt.Text.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0");

                            }
                            int RN2 = 0;
                            DT = AC.GetPlAcc("1005");

                            if (Convert.ToDouble(TxtAgentSec.Text) != 0)
                            {
                                string Accno;
                                string InAccNo = AC.GetGlAccNo(ddlLoanBrName.SelectedValue.ToString(), TxtProdCde.Text);
                                if (InAccNo == "Y" || InAccNo == "y")
                                    //Accno = AC.GetAGAccNo(ddlLoanBrName.SelectedValue.ToString(), txtAgCode.Text, DT.Rows[0]["PLACC"].ToString());
                                    Accno = TxtAccNo.Text;
                                else
                                    Accno = "0";
                                agacc = string.IsNullOrEmpty(TxtAccNo.Text) ? "0" : TxtAccNo.Text;
                                prod = string.IsNullOrEmpty(TxtProdCde.Text) ? DT.Rows[0]["PLACC"].ToString() : TxtProdCde.Text;
                                string glcode2 = AT.getglcode(DDlBranchTDS.SelectedValue.ToString(), TxtProdCde.Text);
                                //RN2 = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), prod, agacc, "AGENT SECURITY trf/" + txtAgCode.Text, "", TxtAgentSec.Text.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "", CustNo.Text.ToString(), TxtAccName4.Text, "", "0");
                                RN2 = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode2.ToString(), prod, Accno, "AGENT SECURITY trf/" + txtAgCode.Text, "", TxtAgentSec.Text.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0"); //amruta    28/09/2017
                            }
                            int RN3 = 0;
                            DT = AC.GetPlAcc("1008");
                            if (Convert.ToDouble(TXtAGCDAMC.Text) != 0)
                            {
                                string Accno = "0";
                                string InAccNo = AC.GetGlAccNo(Session["BRCD"].ToString(), DT.Rows[0]["PLACC"].ToString());
                                if (InAccNo == "Y" || InAccNo == "y")
                                    Accno = AC.GetAGAccNo(Session["BRCD"].ToString(), txtAgCode.Text, DT.Rows[0]["PLACC"].ToString());
                                else
                                    Accno = txtAgCode.Text;
                                // RN3 = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), DT.Rows[0]["PLACC"].ToString(), "0", "AMC CHARGES trf/" + txtAgCode.Text, "", TXtAGCDAMC.Text, "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "", CustNo.Text.ToString(), TxtAccName4.Text, "", "0");
                                RN3 = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), DT.Rows[0]["PLACC"].ToString(), Accno, "AMC CHARGES trf/" + txtAgCode.Text, "", TXtAGCDAMC.Text, "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0"); //Amruta  28/09/2017
                            }
                            int RN5 = 0;
                            DT = AC.GetPlAcc("1006");
                            if (Convert.ToDouble(txttravelexp.Text) != 0)
                            {
                                RN5 = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GLCODE"].ToString(), DT.Rows[0]["PLACC"].ToString(), "0", "TARVELLING EXPENCES trf/" + txtAgCode.Text, "", txttravelexp.Text, "2", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0");
                            }
                            string glcode = AT.getglcode(Session["BRCD"].ToString(), TxtProcode4.Text);  //added by ashok misal for gettting glcode for 307
                            int RN4 = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, TxtProcode4.Text, TxtAccNo4.Text, "Agent AccNo trf/" + txtAgCode.Text, "", txtNetCommision.Text.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "AgentCommision", CustNo.Text.ToString(), TxtAccName4.Text, "", "0");

                            int DATA = AC.INSERTAGENTCOMMI(Session["EntryDate"].ToString(), txtFDate.Text, txtTDate.Text, txtAgCode.Text, Session["BRCD"].ToString(), txttcoll.Text, txtTotColl.Text, txtCommAmt.Text, txtTdAmt.Text, TxtAgentSec.Text, TXtAGCDAMC.Text, txttravelexp.Text, txtNetCommision.Text, "1", Session["MID"].ToString(), ST.ToString());
                            if (RN1 > 0 && RN2 > 0 && RN2 > 0 && RN4 > 0 && DATA > 0)
                            {
                                FL = "Insert";//Dhanya Shetty
                                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Agent_Comms _" + ST + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                ClearData();
                                lblMessage.Text = "Commission Successfully Transfer To Agent Account And Voucher No Is........:" + ST + "";
                                ModalPopup.Show(this.Page);

                            }
                        }
                        else
                        {
                            ClearData();
                            WebMsgBox.Show("Debit Not Success", this.Page);

                        }

                    }
                }
                //else if()
                //{

                //}
                else
                {
                    WebMsgBox.Show("Please Select Branch For TDS and Agent Security", this.Page);
                }
            }
            else if (RdbMultiple.Checked == true)
            {
                BindGrid();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Functions

    public void ClearData()
    {
        txtAgCode.Text = "";
        txtAgName.Text = "";
        CustNo.Text = "";
        txtFDate.Text = "";
        txtTDate.Text = "";
        txtTotColl.Text = "";
        txtCommision.Text = "";
        txtCommAmt.Text = "";
        txtTDDeduction.Text = "";
        txtTdAmt.Text = "";
        txtNetCommision.Text = "";
        TxtProcode4.Text = "";
        TxtProName4.Text = "";
        TxtAccNo4.Text = "";
        TxtAccName4.Text = "";
        TxtAGCDSEC.Text = "";
        TXtAGCDAMC.Text = "";
        TxtAgentSec.Text = "";
        TxtProdCde.Text = "";
        TxtProdname.Text = "";
        TxtAccNo.Text = "";
        TxtAccname.Text = "";
        txtTDSAccName.Text = "";
        TxtTDSAccno.Text = "";
        txttrev.Text = "";
        txttravelexp.Text = "";
        txttcoll.Text = "";
        TxtProdCde.Text = "";
        TxtTDSPrd.Text = "";
        TxtTDSPrdName.Text = "";
        DDlBranchTDS.SelectedValue = "0";
        ddlLoanBrName.SelectedValue = "0";
    }

    #endregion

    #region Grid Functions Here

    public void BindGrid()
    {
        int Result;
        Result = AC.BindGrid(grdAgentComm, Session["BRCD"].ToString(), TxtAFDate1.Text, TxtATDate1.Text, TxtAGCD1.Text, TxtAGCD2.Text);
    }

    protected void grdAgentComm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdAgentComm.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() != "AD")
            {
                LinkButton objlink = (LinkButton)sender;
                string SetNo = objlink.CommandArgument;
                string[] SET = SetNo.ToString().Split('_');
                ViewState["SetNo"] = SET[0].ToString();
                ViewState["Date"] = SET[1].ToString();
                CallData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void CallData()
    {
        try
        {
            if (ViewState["Flag"].ToString() != "AD")
            {
                int i = AC.DelData1(Session["BRCD"].ToString(), ViewState["SetNo"].ToString());

                if (i > 0)
                {
                    int j = AC.DelData2(Session["BRCD"].ToString(), ViewState["SetNo"].ToString(), ViewState["Date"].ToString());

                    if (j > 0)
                    {
                        BindGrid();
                        lblMessage.Text = "Record Deleted Successfully...!!";
                        ModalPopup.Show(this.Page);
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion
    protected void RdbSingle_CheckedChanged(object sender, EventArgs e)
    {
        Single.Visible = true;
        Multiple.Visible = false;
        PostEntries.Visible = false;
    }
    protected void RdbMultiple_CheckedChanged(object sender, EventArgs e)
    {
        Single.Visible = false;
        Multiple.Visible = true;
        btnSubmit.Visible = false;
        PostEntries.Visible = false;
    }
    protected void TxtAGCD1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AGCD = "";
            AGCD = txtAgCode.Text;

            string[] TD = AC.GetAgNo(TxtAGCD1.Text, Session["BRCD"].ToString()).Split('_');

            if (TD.Length > 0)
            {
                //TxtAGCD1.Text = AGCD;
                //TxtAGCDName1.Text = TD[0].ToString();
                //CustNo.Text = TD[1].ToString();
            }
            else
            {
                WebMsgBox.Show("Enter valid Agent code!.....", this.Page);
                TxtAGCD1.Text = "";
                //TxtAGCDName1.Text = "";
                TxtAGCD2.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAGCDName1_TextChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAGCD2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AGCD = "";
            AGCD = txtAgCode.Text;

            string[] TD = AC.GetAgNo(TxtAGCD2.Text, Session["BRCD"].ToString()).Split('_');

            if (TD.Length > 0)
            {
                //TxtAGCD2.Text = AGCD;
                //TxtAGCDName2.Text = TD[0].ToString();
                //CustNo.Text = TD[1].ToString();
            }
            else
            {
                WebMsgBox.Show("Enter valid Agent code!.....", this.Page);
                TxtAGCD2.Text = "";
                //TxtAGCDName2.Text = "";
                TxtAFDate1.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAGCDName2_TextChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void BtnRpt_Click(object sender, EventArgs e)
    {
        String FLag = null;
        try
        {
            if (Agentcollect1.Checked == true && Transfercollect1.Checked == false && Cashcollect1.Checked == false)
            {
                FLag = "AGENT";
            }
            if (Transfercollect1.Checked == true && Agentcollect1.Checked == false && Cashcollect1.Checked == false)
            {
                FLag = "TR";
            }
            if (Cashcollect1.Checked == true && Transfercollect1.Checked == false && Agentcollect1.Checked == false)
            {
                FLag = "CASH";
            }
            if (Agentcollect1.Checked == true && Transfercollect1.Checked == true && Cashcollect1.Checked == false)
            {
                FLag = "AGENT_TR";
            }
            if (Agentcollect1.Checked == true && Transfercollect1.Checked == true && Cashcollect1.Checked == true)
            {
                FLag = "AGENT_TR_CASH";
            }
            if (Agentcollect1.Checked == true && Transfercollect1.Checked == false && Cashcollect1.Checked == true)
            {
                FLag = "AGENT_CASH";
            }
            if (Agentcollect1.Checked == false && Transfercollect1.Checked == true && Cashcollect1.Checked == true)
            {
                FLag = "TR_CASH";
            }
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Agent_Comms_Rpt _" + TxtAGCD1.Text + "_" + TxtAGCD2.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?FDate=" + TxtAFDate1.Text + "&FLAG=" + FLag + "&TDate=" + TxtATDate1.Text + "&ProdCode=" + TxtAGCD1.Text + "&UserName=" + Session["UserName"].ToString() + "&P_CODE=" + TxtAGCD2.Text + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=MultiAgentRpt.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }


    }
    public void ShowReport()
    {
        try
        {
            string FLag = "";
            if (Agentcollect.Checked == true && Transfercollect.Checked == false && Cashcollect.Checked == false)
            {
                FLag = "AGENT";
            }
            if (Transfercollect.Checked == true && Agentcollect.Checked == false && Cashcollect.Checked == false)
            {
                FLag = "TR";
            }
            if (Cashcollect.Checked == true && Transfercollect.Checked == false && Agentcollect.Checked == false)
            {
                FLag = "CASH";
            }
            if (Agentcollect.Checked == true && Transfercollect.Checked == true && Cashcollect.Checked == false)
            {
                FLag = "AGENT_TR";
            }
            if (Agentcollect.Checked == true && Transfercollect.Checked == true && Cashcollect.Checked == true)
            {
                FLag = "AGENT_TR_CASH";
            }
            if (Agentcollect.Checked == true && Transfercollect.Checked == false && Cashcollect.Checked == true)
            {
                FLag = "AGENT_CASH";
            }
            if (Agentcollect.Checked == false && Transfercollect.Checked == true && Cashcollect.Checked == true)
            {
                FLag = "TR_CASH";
            }
            string sql = "select LISTVALUE from parameter where  listfield='AgentCommission'";
            string CommisionCase = conn.sExecuteScalar(sql);
            if (Convert.ToInt32(CommisionCase) == 1)
            {
                try
                {
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Agent_Comms_Rpt _" + txtAgCode.Text + "_" + txtAgCode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string redirectURL = "FrmRView.aspx?FDate=" + txtFDate.Text + "&TDate=" + txtTDate.Text + "&ProdCode=" + txtAgCode.Text + "&P_CODE=" + txtAgCode.Text + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&Flag=" + FLag + "&rptname=RptCheckAccLimit.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                catch (Exception Ex)
                {
                    ExceptionLogging.SendErrorToText(Ex);
                }
            }
            else if (Convert.ToInt32(CommisionCase) == 2)
            {
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Agent_Comms_Rpt _" + txtAgCode.Text + "_" + txtAgCode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?FDate=" + txtFDate.Text + "&TDate=" + txtTDate.Text + "&ProdCode=" + txtAgCode.Text + "&P_CODE=" + txtAgCode.Text + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&Flag=" + FLag + "&rptname=RptCheckAccLimit.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btncheckhow_Click(object sender, EventArgs e)
    {
        //string sql = "SELECT BANKCD FROM BANKNAME WHERE BRCD='" + Session["BRCD"].ToString() + "'";
        //string BKCD = conn.sExecuteScalar(sql);
        string FLag = "";


        if (Agentcollect.Checked == false && Transfercollect.Checked == false && Cashcollect.Checked == false)
        {
            WebMsgBox.Show("Please Select Type of Collection", this.Page);
        }
        if (Agentcollect.Checked == true && Transfercollect.Checked == false && Cashcollect.Checked == false)
        {
            FLag = "AGENT";
        }
        if (Transfercollect.Checked == true && Agentcollect.Checked == false && Cashcollect.Checked == false)
        {
            FLag = "TR";
        }
        if (Cashcollect.Checked == true && Transfercollect.Checked == false && Agentcollect.Checked == false)
        {
            FLag = "CASH";
        }
        if (Agentcollect.Checked == true && Transfercollect.Checked == true && Cashcollect.Checked == false)
        {
            FLag = "AGENT_TR";
        }
        if (Agentcollect.Checked == true && Transfercollect.Checked == true && Cashcollect.Checked == true)
        {
            FLag = "AGENT_TR_CASH";
        }
        if (Agentcollect.Checked == true && Transfercollect.Checked == false && Cashcollect.Checked == true)
        {
            FLag = "AGENT_CASH";
        }
        if (Agentcollect.Checked == false && Transfercollect.Checked == true && Cashcollect.Checked == true)
        {
            FLag = "TR_CASH";
        }
        string sql = "select LISTVALUE from parameter where  listfield='AgentCommission'";
        string CommisionCase = conn.sExecuteScalar(sql);
        if (Convert.ToInt32(CommisionCase) == 1)
        {
            try
            {
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Agent_Comms_Rpt _" + txtAgCode.Text + "_" + txtAgCode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?FDate=" + txtFDate.Text + "&TDate=" + txtTDate.Text + "&ProdCode=" + txtAgCode.Text + "&P_CODE=" + txtAgCode.Text + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&Flag=" + FLag + "&rptname=RptCheckAccLimit.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
        // else if (BKCD=="1001")
        //{
        //    string redirectURL = "FrmRView.aspx?FDate=" + txtFDate.Text + "&TDate=" + txtTDate.Text + "&ProdCode=" + txtAgCode.Text + "&P_CODE=" + txtAgCode.Text + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptShivCheck.rdlc";
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        //}
        else if (Convert.ToInt32(CommisionCase) == 2)
        {
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Agent_Comms_Rpt _" + txtAgCode.Text + "_" + txtAgCode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?FDate=" + txtFDate.Text + "&TDate=" + txtTDate.Text + "&ProdCode=" + txtAgCode.Text + "&P_CODE=" + txtAgCode.Text + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&Flag=" + FLag + "&rptname=RptCheckAccLimit.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        }

    }
    protected void TXtAGCDAMC_TextChanged(object sender, EventArgs e)
    {
        string amt = Convert.ToString(((Convert.ToDouble(txtCommAmt.Text)) + Convert.ToDouble(txttravelexp.Text)) - ((Convert.ToDouble(txtTdAmt.Text)) + (Convert.ToDouble(TxtAgentSec.Text)) + (Convert.ToDouble(TXtAGCDAMC.Text))));
        txtNetCommision.Text = amt;
    }
    protected void TxtProdCde_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //A
            if (txtAgCode.Text == "")
            {
                lblMessage.Text = "Please Enter Agent Code...!";
                ModalPopup.Show(this.Page);
                txtAgCode.Focus();
                return;
            }

            string AC1;
            AC1 = AC.Getaccno(TxtProdCde.Text, ddlLoanBrName.SelectedValue.ToString(), "");

            if (AC1 != null)
            {
                string[] ACc = AC1.Split('-'); ;
                ViewState["ACCNO"] = ACc[0].ToString();
                ViewState["GLCODE"] = ACc[1].ToString();
                TxtProdname.Text = ACc[2].ToString();
                AutoAgentAcc.ContextKey = ddlLoanBrName.SelectedValue.ToString() + "_" + TxtProdCde.Text + "_" + ViewState["GLCODE"].ToString();
                string intaccyn = AC.GetPrno(TxtProdCde.Text, ddlLoanBrName.SelectedValue.ToString());
                if (intaccyn != "Y")
                {
                    TxtAccNo.Enabled = false;
                    TxtAccname.Enabled = false;
                }
                else
                {
                    TxtAccNo.Enabled = true;
                    TxtAccname.Enabled = true;
                }
                TxtAccNo.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code!.....", this.Page);
                TxtProdname.Text = "";
                TxtAccNo.Text = "";
                TxtProdname.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtProdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtProdname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtProdname.Text = custnob[0].ToString();
                TxtProdCde.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] ACC = AC.Getaccno(TxtProdCde.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
                ViewState["ACCNO"] = ACC[0].ToString();
                ViewState["GLCODE"] = ACC[1].ToString();
                AutoAgentAcc.ContextKey = Session["BRCD"].ToString() + "_" + TxtProdCde.Text;
                TxtAccNo.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = CMN.GetCustNoNameGL1(TxtProdCde.Text.ToString(), TxtAccNo.Text.ToString(), ddlLoanBrName.SelectedValue.ToString());

            if (DT.Rows.Count > 0)
            {
                TxtAccname.Text = DT.Rows[0][3].ToString();
            }
            else
            {
                lblMessage.Text = "Account Number not found...!";
                ModalPopup.Show(this.Page);
                TxtAccNo.Text = "";
                TxtAccname.Text = "";
                TxtAccNo.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtAccname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtAccname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtAccname.Text = custnob[0].ToString();
                TxtAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
            }
            else
            {
                lblMessage.Text = "Invalid Account Number...!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?FDate=" + txtPFdate.Text + "&TDate=" + txtPTodate.Text + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptPostCommision.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void RbnPostEntry_CheckedChanged(object sender, EventArgs e)
    {
        Single.Visible = false;
        Multiple.Visible = false;
        PostEntries.Visible = true;
    }
    protected void ddlLoanBrName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void BindBranch()
    {
        try
        {
            BD.BindBRANCHNAME(ddlLoanBrName, null);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindBranchTDS()
    {
        try
        {
            BD.BindBRANCHNAME(DDlBranchTDS, null);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTDSPrdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtTDSPrdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtTDSPrdName.Text = custnob[0].ToString();
                TxtTDSPrd.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] ACC = AC.Getaccno(TxtTDSPrd.Text, DDlBranchTDS.SelectedValue.ToString(), custnob[2].ToString()).Split('-');
                ViewState["ACCNO"] = ACC[0].ToString();
                ViewState["GLCODE"] = ACC[1].ToString();
                TDSAutoAccName.ContextKey = DDlBranchTDS.SelectedValue.ToString() + "_" + TxtProcode4.Text;
                TxtTDSAccno.Focus();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTDSAccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = CMN.GetCustNoNameGL1(TxtTDSPrd.Text.ToString(), TxtTDSAccno.Text.ToString(), DDlBranchTDS.SelectedValue.ToString());

            if (DT.Rows.Count > 0)
            {
                txtTDSAccName.Text = DT.Rows[0][3].ToString();
            }
            else
            {
                lblMessage.Text = "Account Number not found...!";
                ModalPopup.Show(this.Page);
                TxtTDSAccno.Text = "";
                txtTDSAccName.Text = "";
                TxtTDSAccno.Focus();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtTDSAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtTDSAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtTDSAccName.Text = custnob[0].ToString();
                TxtTDSAccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
            }
            else
            {
                lblMessage.Text = "Invalid Account Number...!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTDSPrd_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string AC1;
            AC1 = AC.Getaccno(TxtTDSPrd.Text, DDlBranchTDS.SelectedValue.ToString(), "");

            if (AC1 != null)
            {
                string[] ACc = AC1.Split('-'); ;
                ViewState["ACCNO"] = ACc[0].ToString();
                ViewState["GLCODE"] = ACc[1].ToString();
                TxtTDSPrdName.Text = ACc[2].ToString();
                TDSAutoAccName.ContextKey = DDlBranchTDS.SelectedValue.ToString() + "_" + TxtTDSPrd.Text.ToString();

                string intaccyn = AC.GetPrno(TxtTDSPrd.Text, DDlBranchTDS.SelectedValue.ToString());
                if (intaccyn != "Y")
                {
                    TxtTDSAccno.Enabled = false;
                    txtTDSAccName.Enabled = false;
                }
                else
                {
                    TxtTDSAccno.Enabled = true;
                    txtTDSAccName.Enabled = true;
                }
                TxtTDSAccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code!.....", this.Page);
                TxtTDSPrdName.Text = "";
                TxtTDSAccno.Text = "";
                TxtTDSPrdName.Focus();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void DDlBranchTDS_TextChanged(object sender, EventArgs e)
    {
        try
        {
            autoAgname.ContextKey = null;

            AutoTDSAutoProd.ContextKey = DDlBranchTDS.SelectedValue.ToString();
            //AutoTDSAutoProd.ContextKey = Session["BRCD"].ToString();
            TxtTDSPrd.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
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
    protected void Btn_CheckReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}