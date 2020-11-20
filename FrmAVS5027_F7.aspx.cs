using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class FrmAVS5027_F7 : System.Web.UI.Page
{
    DataTable DT = new DataTable();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLoanInstallmen LI = new ClsLoanInstallmen();
    ClsAccopen accop = new ClsAccopen();
    ClsAVS5027_F7 LA = new ClsAVS5027_F7();
    scustom customcs = new scustom();
    DbConnection conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();

    string sResult = "", res = "", res1 = "";
    static string CNO = "";
    public static string Flag;
    int resultint, resP, resS, resO, resC, resPD, resTot, resDelete;
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");


            Session["CNO"] = Request.QueryString["CUSTNO"];
            txtCustNo.Text = Request.QueryString["CUSTNO"].ToString();
            txtCustName.Text = Request.QueryString["CustName"].ToString();
            ViewState["UN_FL"] = Request.QueryString["FLAG"].ToString();
            res = LA.GetJointDate(Session["CNO"].ToString());
            TxtjoinDT.Text = res;
            res = LA.GetRetireDate(Session["CNO"].ToString());
            TxtCustDOB.Text = res;
            Flag = "1";
            ViewState["Flag"] = "AD";
            btnSubmit.Text = "Create";
            autoglname.ContextKey = Session["BRCD"].ToString();
            EmptyGridBind();
            StandardGridBind();
            BD.Bindpurpose(DDlpurpose);
            BindLoanDetails();
            BindGrid();
            BD.BindSancAuth(ddlSancAuthCd);
            BD.BindRecAuth(ddlRecAuthCd);
            autoglname.ContextKey = Session["BRCD"].ToString();



            string[] AG;
            if (TxtCustDOB.Text.ToString().Contains("/"))
            {
                AG = TxtCustDOB.Text.Split('/');
            }
            else
            {
                AG = TxtCustDOB.Text.Split('-');
            }
            int CY, BY;
            CY = DateTime.Now.Date.Year;

            if (TxtCustDOB.Text.ToString().Contains("/"))
            {
                BY = Convert.ToInt32(AG[2].ToString());
            }
            else
            {
                BY = Convert.ToInt32(AG[0].ToString());
            }
            //TxtCustAGe.Text = (BY - CY).ToString();
            TxtCustAGe.Text = conn.sExecuteScalar("SELECT DATEDIFF(MONTH, '" + conn.ConvertDate(Convert.ToString(Session["EntryDate"])) + "', '" + conn.ConvertDate(TxtCustDOB.Text) + "') AS DateDiff");
            // TxtCustAGe.Text = Math.Abs(Convert.ToSingle(((BY - CY) * 12))).ToString();
            if (TxtjoinDT.Text.ToString().Contains("/"))
            {
                AG = TxtjoinDT.Text.Split('/');
            }
            else
            {
                AG = TxtjoinDT.Text.Split('-');
            }
            CY = DateTime.Now.Date.Year;

            if (TxtjoinDT.Text.ToString().Contains("/"))
            {
                BY = Convert.ToInt32(AG[2].ToString());
            }
            else
            {
                BY = Convert.ToInt32(AG[0].ToString());
            }
            TxtJoinAge.Text = Convert.ToSingle(((CY - BY) * 12)).ToString();

            TxtLtype.Focus();
        }
    }


    public void BindLoanDetails()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = LA.GetLoanDetails1(GrdprevLoan, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "DL");
            if (dt.Rows.Count > 0)
            {
                GrdprevLoan.DataSource = dt;
                GrdprevLoan.DataBind();
            }
            else
            {
                GrdprevLoan.DataSource = null;
                GrdprevLoan.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void BindGrid()
    {
        int RS = LA.BindGrid(GrdLoanAp, Session["BRCD"].ToString(), Session["CNO"].ToString(), "PD");
    }

    protected void DDlpurpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Txtpur.Text = BD.GetNOPurpose(DDlpurpose.SelectedItem.Text);
            TxtInstDate.Focus();
            grdInsert.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void GetDetails()
    {
        try
        {
            //LOANPURPOSE -- Personal
            DataTable dt = new DataTable();
            dt = LA.GetAllDetails(ViewState["LOANTYPE"].ToString(), Session["BRCD"].ToString(), txtCustNo.Text);
            if (dt.Rows.Count > 0)
            {
                TxtLtype.Text = ViewState["LOANTYPE"].ToString();
                TxtLname.Text = (string.IsNullOrEmpty(dt.Rows[0]["LOANTYPE"].ToString()) ? "0" : dt.Rows[0]["LOANTYPE"].ToString());
                DDlpurpose.SelectedItem.Text = (string.IsNullOrEmpty(dt.Rows[0]["LOANPURPOSE"].ToString()) ? "0" : dt.Rows[0]["LOANPURPOSE"].ToString());
                Txtpur.Text = BD.GetNOPurpose(DDlpurpose.SelectedItem.Text);
                TxtLapply.Text = (string.IsNullOrEmpty(dt.Rows[0]["LOANAPPLY"].ToString()) ? "0" : dt.Rows[0]["LOANAPPLY"].ToString());
                //TxtGross.Text = (string.IsNullOrEmpty(dt.Rows[0]["GROSSSAL"].ToString()) ? "0" : dt.Rows[0]["GROSSSAL"].ToString());
                TxtNet.Text = (string.IsNullOrEmpty(dt.Rows[0]["NETSAL"].ToString()) ? "0" : dt.Rows[0]["NETSAL"].ToString());
                //Txttwntyfive.Text = (string.IsNullOrEmpty(dt.Rows[0]["TWENTYFIVE"].ToString()) ? "0" : dt.Rows[0]["TWENTYFIVE"].ToString());
                //Txtbysal.Text = (string.IsNullOrEmpty(dt.Rows[0]["LOANELGIBLITYSALARY"].ToString()) ? "0" : dt.Rows[0]["LOANELGIBLITYSALARY"].ToString());
                Txttosanction.Text = (string.IsNullOrEmpty(dt.Rows[0]["SANCITONAMOUNT"].ToString()) ? "0" : dt.Rows[0]["SANCITONAMOUNT"].ToString());
                //Txtrepay.Text = (string.IsNullOrEmpty(dt.Rows[0]["REPAYCAP"].ToString()) ? "0" : dt.Rows[0]["REPAYCAP"].ToString());
                //Txtinstll.Text = (string.IsNullOrEmpty(dt.Rows[0]["INSTMANUAL"].ToString()) ? "0" : dt.Rows[0]["INSTMANUAL"].ToString());
                //Txtmembrship.Text = (string.IsNullOrEmpty(dt.Rows[0]["LOANELGIBLITYMEMBER"].ToString()) ? "0" : dt.Rows[0]["LOANELGIBLITYMEMBER"].ToString());
                //ddlInstType.SelectedValue = (string.IsNullOrEmpty(dt.Rows[0]["InstType"].ToString()) ? "0" : dt.Rows[0]["InstType"].ToString());
                txtIntRate.Text = (string.IsNullOrEmpty(dt.Rows[0]["IntRate"].ToString()) ? "0" : dt.Rows[0]["IntRate"].ToString());
                txtTotPeriod.Text = (string.IsNullOrEmpty(dt.Rows[0]["Period"].ToString()) ? "0" : dt.Rows[0]["Period"].ToString());
                ddlSancAuthCd.SelectedItem.Text = (string.IsNullOrEmpty(dt.Rows[0]["Sanction"].ToString()) ? "0" : dt.Rows[0]["Sanction"].ToString());
                ddlRecAuthCd.SelectedItem.Text = (string.IsNullOrEmpty(dt.Rows[0]["Recom"].ToString()) ? "0" : dt.Rows[0]["Recom"].ToString());
                GetSurity();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void GetSurity()
    {
        try
        {
            DataTable DT = new DataTable();
            DataTable DT1 = new DataTable();
            DataTable DT2 = new DataTable();
            DataTable DT4 = new DataTable();

            btnAddNewRow.Visible = false;
            DT = LA.GetSurityDetails(TxtLtype.Text, txtCustNo.Text, Session["BRCD"].ToString());
            if (DT.Rows.Count > 0)
            {
                grdInsert.DataSource = DT;
                grdInsert.DataBind();
            }
            else
            {
                grdInsert.DataSource = null;
                grdInsert.DataBind();
            }

            DT1 = LA.GetPrevLoan(TxtLtype.Text, txtCustNo.Text, Session["BRCD"].ToString(), "1");
            if (DT1.Rows.Count > 0)
            {
                GrdprevLoan.DataSource = DT1;
                GrdprevLoan.DataBind();
                TxtSubD.Text = Convert.ToString(DT1.Compute("SUM(amount1)+SUM(amount)", string.Empty));
            }
            else
            {
                GrdprevLoan.DataSource = null;
                GrdprevLoan.DataBind();
                TxtSubD.Text = "0";
            }

            DT2 = LA.GetPrevLoan(TxtLtype.Text, txtCustNo.Text, Session["BRCD"].ToString(), "2");
            if (DT2.Rows.Count > 0)
            {
                grdstandard.DataSource = DT2;
                grdstandard.DataBind();
                TxtSubS.Text = Convert.ToString(DT2.Compute("SUM(TxtSDeduction)", string.Empty));
            }
            else
            {
                grdstandard.DataSource = null;
                grdstandard.DataBind();
                TxtSubS.Text = "0";
            }

            Btnstandard.Visible = false;
            DT4 = LA.GetPrevLoan(TxtLtype.Text, txtCustNo.Text, Session["BRCD"].ToString(), "4");
            if (DT4.Rows.Count > 0)
                TxttotDed.Text = (string.IsNullOrEmpty(DT4.Rows[0]["AMOUNT"].ToString()) ? "0" : DT4.Rows[0]["AMOUNT"].ToString());
            else
                TxttotDed.Text = "0";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnDed_Click(object sender, EventArgs e)
    {

    }

    public void EmptyGridBind()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Txtmemno", typeof(int)),
                            new DataColumn("Txtcustno", typeof(string)),new DataColumn("Txtname", typeof(int)),
                            new DataColumn("Memberdate", typeof(string)),new DataColumn("DATEOFRET", typeof(int)),
                            new DataColumn("LoanBal", typeof(string)),new DataColumn("REMSERVICE", typeof(int)),
                            new DataColumn("StandBy", typeof(int)),new DataColumn("STANDS",typeof(string)) });
        DataRow dr;
        for (int i = dt.Rows.Count; i < 5; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
        }
        dt.AcceptChanges();
        grdInsert.DataSource = dt;
        grdInsert.DataBind();
    }

    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Txtmemno", typeof(int)),
                            new DataColumn("Txtcustno", typeof(string)),new DataColumn("Txtname", typeof(int)),
                            new DataColumn("Memberdate", typeof(string)),new DataColumn("DATEOFRET", typeof(int)),
                            new DataColumn("LoanBal", typeof(string)),new DataColumn("REMSERVICE", typeof(int)),
                            new DataColumn("StandBy", typeof(int)),new DataColumn("STANDS",typeof(string)) });
        DataRow dr;
        for (int i = dt.Rows.Count; i < grdInsert.Rows.Count + 1; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
        }
        dt.AcceptChanges();
        grdInsert.DataSource = dt;
        grdInsert.DataBind();
    }

    public void StandardGridBind()
    {
        try
        {
            DataTable dt = new DataTable();
            DataTable DT4 = new DataTable();

            dt = LA.GetDedutcionCalc(Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), TxtLapply.Text);

            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[5] { new DataColumn("TxtSprcd", typeof(int)),new DataColumn("accno", typeof(string)),
            //                new DataColumn("TxtSname", typeof(string)),new DataColumn("txtPer", typeof(string)),new DataColumn("TxtSDeduction", typeof(int)) });
            DataRow dr;
            for (int i = dt.Rows.Count; i < 5; i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
            grdstandard.DataSource = dt;
            grdstandard.DataBind();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btnstandard_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("TxtSprcd", typeof(int)),new DataColumn("accno", typeof(string)),
                            new DataColumn("TxtSname", typeof(string)),new DataColumn("txtPer", typeof(string)),new DataColumn("TxtSDeduction", typeof(int)) });
            DataRow dr;
            for (int i = dt.Rows.Count; i < grdstandard.Rows.Count + 1; i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
            grdstandard.DataSource = dt;
            grdstandard.DataBind();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ShowDetail()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = LA.GetAllDetails(TxtLtype.Text, Session["BRCD"].ToString(), txtCustNo.Text);
            if (dt.Rows.Count > 0)
            {
                TxtLtype.Text = (string.IsNullOrEmpty(ViewState["LOANTYPE"].ToString()) ? "0" : ViewState["LOANTYPE"].ToString());
                TxtLname.Text = (string.IsNullOrEmpty(dt.Rows[0]["LOANTYPE"].ToString()) ? "0" : dt.Rows[0]["LOANTYPE"].ToString());
                DDlpurpose.SelectedItem.Text = (string.IsNullOrEmpty(dt.Rows[0]["LOANPURPOSE"].ToString()) ? "0" : dt.Rows[0]["LOANPURPOSE"].ToString());
                Txtpur.Text = BD.GetNOPurpose(DDlpurpose.SelectedItem.Text);
                TxtLapply.Text = (string.IsNullOrEmpty(dt.Rows[0]["LOANAPPLY"].ToString()) ? "0" : dt.Rows[0]["LOANAPPLY"].ToString());
                //TxtGross.Text = (string.IsNullOrEmpty(dt.Rows[0]["GROSSSAL"].ToString()) ? "0" : dt.Rows[0]["GROSSSAL"].ToString());
                TxtNet.Text = (string.IsNullOrEmpty(dt.Rows[0]["NETSAL"].ToString()) ? "0" : dt.Rows[0]["NETSAL"].ToString());
                //Txttwntyfive.Text = (string.IsNullOrEmpty(dt.Rows[0]["TWENTYFIVE"].ToString()) ? "0" : dt.Rows[0]["TWENTYFIVE"].ToString());
                //Txtbysal.Text = (string.IsNullOrEmpty(dt.Rows[0]["LOANELGIBLITYSALARY"].ToString()) ? "0" : dt.Rows[0]["LOANELGIBLITYSALARY"].ToString());
                Txttosanction.Text = (string.IsNullOrEmpty(dt.Rows[0]["SANCITONAMOUNT"].ToString()) ? "0" : dt.Rows[0]["SANCITONAMOUNT"].ToString());
                //Txtrepay.Text = (string.IsNullOrEmpty(dt.Rows[0]["REPAYCAP"].ToString()) ? "0" : dt.Rows[0]["REPAYCAP"].ToString());
                //Txtinstll.Text = (string.IsNullOrEmpty(dt.Rows[0]["INSTMANUAL"].ToString()) ? "0" : dt.Rows[0]["INSTMANUAL"].ToString());
                //Txtmembrship.Text = (string.IsNullOrEmpty(dt.Rows[0]["LOANELGIBLITYMEMBER"].ToString()) ? "0" : dt.Rows[0]["LOANELGIBLITYMEMBER"].ToString());
                txtIntRate.Text = (string.IsNullOrEmpty(dt.Rows[0]["IntRate"].ToString()) ? "0" : dt.Rows[0]["IntRate"].ToString());
                txtTotPeriod.Text = (string.IsNullOrEmpty(dt.Rows[0]["Period"].ToString()) ? "0" : dt.Rows[0]["Period"].ToString());
                ddlSancAuthCd.SelectedItem.Text = (string.IsNullOrEmpty(dt.Rows[0]["Sanction"].ToString()) ? "0" : dt.Rows[0]["Sanction"].ToString());
                ddlRecAuthCd.SelectedItem.Text = (string.IsNullOrEmpty(dt.Rows[0]["Recom"].ToString()) ? "0" : dt.Rows[0]["Recom"].ToString());
                GetSurity();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TxtLname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtLname.Text;
            string[] custnob = CUNAME.Split('_');

            string LnPeriod = LA.GetBasicPay_Month("MaxLNPeriod");
            float LoanPeriod = Convert.ToSingle(LnPeriod);
            float RelPeriod = Convert.ToSingle(TxtCustAGe.Text);

            if (custnob.Length > 1)
            {
                TxtLname.Text = custnob[0].ToString();
                TxtLtype.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = LI.Getaccno(TxtLtype.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GLCODE"] = AC[0].ToString();
                ViewState["LNCAT"] = BD.GetLoanCategory(TxtLtype.Text, Session["BRCD"].ToString());
                autoglname.ContextKey = Session["BRCD"].ToString() + "_" + TxtLtype.Text;

                string[] LoanDet = LA.LoanDetails(Session["BRCD"].ToString(), TxtLtype.Text.ToString()).ToString().Split('_');
                if (LoanDet.Length > 0)
                {
                    if (RelPeriod > LoanPeriod)
                    {
                        txtTotPeriod.Text = LoanPeriod.ToString();
                    }
                    else
                    {
                        txtTotPeriod.Text = RelPeriod.ToString();
                    }
                    txtIntRate.Text = LoanDet[1].ToString();
                }

                DDlpurpose.Focus();
                ShowDetail();
                return;
            }
            else
            {
                TxtLtype.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtLtype_TextChanged(object sender, EventArgs e)
    {
        DataTable DT = new DataTable();
        try
        {
            string Validate = LA.Validate(Session["BRCD"].ToString(), TxtLtype.Text);

            string LnPeriod = LA.GetBasicPay_Month("MaxLNPeriod");
            float LoanPeriod = Convert.ToSingle(LnPeriod);
            float RelPeriod = Convert.ToSingle(TxtCustAGe.Text);

            if (Validate == "Y")
            {
                DT = LA.GetLoanAPPCount("ServiceDT", Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "0");
                if (DT.Rows.Count > 0)
                {
                    DT = LA.GetLoanAPPCount("SanctionDT", Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "0");
                    if (DT.Rows.Count > 0)
                    {
                        DT = LA.GetLoanAPPCount("CustODBal", Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "0");
                        ViewState["ODAmt"] = Convert.ToString(DT.Rows[0]["ODAmt"].ToString()).ToString();
                        {
                            if (DT.Rows.Count > 0 && ViewState["ODAmt"].ToString() != "0")
                            {
                                WebMsgBox.Show("Customer Having Overdue " + ViewState["ODAmt"].ToString() + " Balance !!!", this.Page);
                               // return;

                                string AC1;
                                AC1 = LI.Getaccno(TxtLtype.Text, Session["BRCD"].ToString());
                                if (AC1 != null)
                                {
                                    string[] AC = AC1.Split('_'); ;
                                    ViewState["GLCODE"] = AC[0].ToString();
                                    TxtLname.Text = AC[1].ToString();
                                    ViewState["LNCAT"] = BD.GetLoanCategory(TxtLtype.Text.ToString(), Session["BRCD"].ToString());

                                    autoglname.ContextKey = Session["BRCD"].ToString() + "_" + TxtLtype.Text.ToString();
                                    string[] LoanDet = LA.LoanDetails(Session["BRCD"].ToString(), TxtLtype.Text.ToString()).ToString().Split('_');
                                    if (LoanDet.Length > 0)
                                    {
                                        if (RelPeriod > LoanPeriod)
                                        {
                                            txtTotPeriod.Text = LoanPeriod.ToString();
                                        }
                                        else
                                        {
                                            txtTotPeriod.Text = RelPeriod.ToString();
                                        }
                                        txtIntRate.Text = LoanDet[1].ToString();
                                    }

                                    TxtInstDate.Text = Session["EntryDate"].ToString();
                                    string[] date = TxtInstDate.Text.Split('/');
                                    DateTime dt = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]));
                                    DateTime firstDayNextMonth = dt.AddMonths(1).AddDays(-dt.Day + 1);
                                    TxtInstDate.Text = firstDayNextMonth.ToString("dd/MM/yyyy");
                                    ScriptManager sm = ScriptManager.GetCurrent(this);
                                    // DDlpurpose.Focus();
                                    ShowDetail();
                                    sm.SetFocus(DDlpurpose);
                                    return;

                                }
                                else
                                {
                                    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                                    TxtLtype.Focus();
                                    return;
                                }
                                DT = LA.GetPreviousloaninfo(Session["BRCD"].ToString(), Session["CNO"].ToString());
                                if (DT.Rows.Count > 0)
                                {
                                    foreach (GridViewRow gvRow in this.GrdprevLoan.Rows)
                                    {
                                        string PPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtprcd")).Text);
                                        string Pname = Convert.ToString(((TextBox)gvRow.FindControl("TxtPname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtPname")).Text);
                                    }
                                }
                            }
                            else
                            {
                                string AC1;
                                AC1 = LI.Getaccno(TxtLtype.Text, Session["BRCD"].ToString());
                                if (AC1 != null)
                                {
                                    string[] AC = AC1.Split('_'); ;
                                    ViewState["GLCODE"] = AC[0].ToString();
                                    TxtLname.Text = AC[1].ToString();
                                    ViewState["LNCAT"] = BD.GetLoanCategory(TxtLtype.Text.ToString(), Session["BRCD"].ToString());

                                    autoglname.ContextKey = Session["BRCD"].ToString() + "_" + TxtLtype.Text.ToString();
                                    string[] LoanDet = LA.LoanDetails(Session["BRCD"].ToString(), TxtLtype.Text.ToString()).ToString().Split('_');
                                    if (LoanDet.Length > 0)
                                    {
                                        if (RelPeriod > LoanPeriod)
                                        {
                                            txtTotPeriod.Text = LoanPeriod.ToString();
                                        }
                                        else
                                        {
                                            txtTotPeriod.Text = RelPeriod.ToString();
                                        }
                                        txtIntRate.Text = LoanDet[1].ToString();
                                    }

                                    TxtInstDate.Text = Session["EntryDate"].ToString();
                                    string[] date = TxtInstDate.Text.Split('/');
                                    DateTime dt = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]));
                                    DateTime firstDayNextMonth = dt.AddMonths(1).AddDays(-dt.Day + 1);
                                    TxtInstDate.Text = firstDayNextMonth.ToString("dd/MM/yyyy");

                                    DDlpurpose.Focus();
                                    ShowDetail();
                                    return;

                                }
                                else
                                {
                                    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                                    TxtLtype.Focus();
                                    return;
                                }
                                DT = LA.GetPreviousloaninfo(Session["BRCD"].ToString(), Session["CNO"].ToString());
                                if (DT.Rows.Count > 0)
                                {
                                    foreach (GridViewRow gvRow in this.GrdprevLoan.Rows)
                                    {
                                        string PPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtprcd")).Text);
                                        string Pname = Convert.ToString(((TextBox)gvRow.FindControl("TxtPname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtPname")).Text);
                                    }
                                }
                            }
                        }
                        //else
                        //{
                        //    WebMsgBox.Show("Customer Having Overdue " + ViewState["ODAmt"].ToString () + " Balance !!!", this.Page);
                        //    //WebMsgBox.Show("Customer Having Overdue Balance...!!", this.Page);
                        //    TxtLtype.Focus();
                        //    return;
                        //}
                    }
                    else
                    {
                        WebMsgBox.Show("Sanction Limit Exceed In Finicial Year...!!", this.Page);
                        TxtLtype.Focus();
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("24 Month Not Completed From Joining...!!", this.Page);
                    TxtLtype.Focus();
                    return;
                }
            }
            else
            {
                DT = LA.GetLoanAPPCount("RelivePeriod", Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "0");
                if (DT.Rows.Count > 0)
                {
                    DT = LA.GetLoanAPPCount("LoanRecover", Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "0");
                    if (DT.Rows.Count > 0)
                    {
                        string AC1;
                        AC1 = LI.Getaccno(TxtLtype.Text, Session["BRCD"].ToString());
                        if (AC1 != null)
                        {
                            string[] AC = AC1.Split('_'); ;
                            ViewState["GLCODE"] = AC[0].ToString();
                            TxtLname.Text = AC[1].ToString();
                            ViewState["LNCAT"] = BD.GetLoanCategory(TxtLtype.Text.ToString(), Session["BRCD"].ToString());

                            autoglname.ContextKey = Session["BRCD"].ToString() + "_" + TxtLtype.Text.ToString();
                            string[] LoanDet = LA.LoanDetails(Session["BRCD"].ToString(), TxtLtype.Text.ToString()).ToString().Split('_');
                            if (LoanDet.Length > 0)
                            {
                                if (RelPeriod > LoanPeriod)
                                {
                                    txtTotPeriod.Text = LoanPeriod.ToString();
                                }
                                else
                                {
                                    txtTotPeriod.Text = RelPeriod.ToString();
                                }
                                txtIntRate.Text = LoanDet[1].ToString();
                            }

                            TxtInstDate.Text = Session["EntryDate"].ToString();
                            string[] date = TxtInstDate.Text.Split('/');
                            DateTime dt = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]));
                            DateTime firstDayNextMonth = dt.AddMonths(1).AddDays(-dt.Day + 1);
                            TxtInstDate.Text = firstDayNextMonth.ToString("dd/MM/yyyy");

                            DDlpurpose.Focus();
                            ShowDetail();
                            return;
                        }
                        else
                        {
                            WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                            TxtLtype.Focus();
                            return;
                        }
                        DT = LA.GetPreviousloaninfo(Session["BRCD"].ToString(), Session["CNO"].ToString());
                        if (DT.Rows.Count > 0)
                        {
                            foreach (GridViewRow gvRow in this.GrdprevLoan.Rows)
                            {
                                string PPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtprcd")).Text);
                                string Pname = Convert.ToString(((TextBox)gvRow.FindControl("TxtPname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtPname")).Text);
                            }
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("50% Loan Repayment Not Done...!!", this.Page);
                        TxtLtype.Focus();
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("Reliving Is Less Than 24 Month...!!", this.Page);
                    TxtLtype.Focus();
                    return;
                }
            }
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }



    protected void Txtmemno_TextChanged(object sender, EventArgs e)
    {
        try
        {


            int rowCount = grdInsert.Rows.Count;

            string currentRowIndex = hdnRowIndex.Value;

            string crosscheckIndex = hdnCrossCheck.Value;
            if (crosscheckIndex != "0")
            {


                if (!String.IsNullOrEmpty(currentRowIndex) && Convert.ToInt32(currentRowIndex) > 0)
                {
                    GetMemberData(ref currentRowIndex);

                    if (rowCount == Convert.ToInt32(currentRowIndex))
                    {
                        TxtNet.Focus();
                    }
                    else
                    {
                        GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex)];
                        ((TextBox)gvRow.FindControl("Txtmemno")).Focus();

                    }
                }
            }
            else
            {
                WebMsgBox.Show("Same Member Number Entered", this.Page);
                GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex)-1];
                ((TextBox)gvRow.FindControl("Txtmemno")).Focus();
                ((TextBox)gvRow.FindControl("Txtmemno")).Text = "";

                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    //protected void Txtmemno_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        foreach (GridViewRow gvRow in this.grdInsert.Rows)
    //        {
    //            string CustNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtcustno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtcustno")).Text);
    //            string CustName = Convert.ToString(((TextBox)gvRow.FindControl("Txtname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtname")).Text);
    //            double LoanBal = Convert.ToDouble(((TextBox)gvRow.FindControl("TxtLbal")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtLbal")).Text);
    //            string Memdate = Convert.ToString(((TextBox)gvRow.FindControl("TxtMemdt")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("TxtMemdt")).Text);
    //            string MemNo = Convert.ToInt32(((TextBox)gvRow.FindControl("Txtmemno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtmemno")).Text).ToString();
    //            string MobileNo = Convert.ToString(((TextBox)gvRow.FindControl("txtMobileNo")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtMobileNo")).Text);
    //            string Retdate = Convert.ToString(((TextBox)gvRow.FindControl("Txtretdate")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("Txtretdate")).Text);
    //            string Remser = Convert.ToString(((TextBox)gvRow.FindControl("Txtremser")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtremser")).Text);
    //            string sql, AT;
    //            sql = AT = "";


    //            if (MemNo == "0")
    //                return;

    //            DT = LA.GetLoanAPPCount("ODAmount", Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), MemNo);
    //            if (DT.Rows.Count > 0)
    //            {
    //                DT = LA.GetLoanAPPCount("SurityYN", Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), MemNo);
    //                if (DT.Rows.Count > 0)
    //                {
    //                    DT = LA.GetLoanAPPCount("SurityPeriod", Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), MemNo);
    //                    if (DT.Rows.Count > 0)
    //                    {
    //                        string custname = LA.GetCustomerDetails(MemNo);
    //                        if (custname != null)
    //                        {
    //                            string[] name = custname.Split('_');
    //                            CustName = (string.IsNullOrEmpty(name[0].ToString()) ? "" : name[0].ToString());
    //                            CustNo = (string.IsNullOrEmpty(name[1].ToString()) ? "0" : name[1].ToString());
    //                            ((TextBox)gvRow.FindControl("Txtname")).Text = CustName.ToString();
    //                            ((TextBox)gvRow.FindControl("Txtcustno")).Text = CustNo.ToString();
    //                        }

    //                        string RC = custname;
    //                        if (RC == null && CustNo == "0")
    //                        {
    //                            WebMsgBox.Show("Customer not found", this.Page);
    //                            ((TextBox)gvRow.FindControl("Txtname")).Text = "";
    //                            ((TextBox)gvRow.FindControl("Txtcustno")).Text = "";
    //                            ((TextBox)gvRow.FindControl("Txtmemno")).Text = "";
    //                            return;
    //                        }
    //                        string accno = LA.GetAccNo(Session["BRCD"].ToString(), CustNo);
    //                        LoanBal = Convert.ToDouble(BD.ClBalance(Session["BRCD"].ToString(), TxtLtype.Text, accno, Session["EntryDate"].ToString(), "ClBal").ToString().ToString());
    //                        ((TextBox)gvRow.FindControl("TxtLbal")).Text = string.IsNullOrEmpty(LoanBal.ToString()) ? "" : LoanBal.ToString();

    //                        Memdate = LA.GetDetails(Session["BRCD"].ToString(), TxtLtype.Text, CustNo);
    //                        if (Memdate != null)
    //                            ((TextBox)gvRow.FindControl("TxtMemdt")).Text = string.IsNullOrEmpty(Memdate.ToString()) ? "" : Memdate.ToString();

    //                        MobileNo = LA.GetMobileNo(MemNo);
    //                        ((TextBox)gvRow.FindControl("txtMobileNo")).Text = string.IsNullOrEmpty(MobileNo.ToString()) ? "" : MobileNo.ToString();

    //                        Retdate = LA.GetRetdate(Session["BRCD"].ToString(), TxtLtype.Text, CustNo);
    //                        if (Retdate != null)
    //                            ((TextBox)gvRow.FindControl("Txtretdate")).Text = string.IsNullOrEmpty(Retdate.ToString()) ? "" : Retdate.ToString();
    //                        if (Retdate != null && Memdate != null)
    //                        {
    //                            Remser = conn.GetMonthDiff(Memdate, Retdate);
    //                            ((TextBox)gvRow.FindControl("Txtremser")).Text = string.IsNullOrEmpty(Remser.ToString()) ? "" : Remser.ToString();
    //                        }
    //                        ((TextBox)gvRow.FindControl("Txtmemno")).Focus();
    //                    }
    //                    else
    //                    {
    //                        WebMsgBox.Show("24 Month Not Completed Surity Not Allowed...!!", this.Page);
    //                        TxtLtype.Focus();
    //                        return;
    //                    }
    //                }
    //                else
    //                {
    //                    WebMsgBox.Show("Member Alredy More Than 7 Surity...!!", this.Page);
    //                    TxtLtype.Focus();
    //                    return;
    //                }
    //            }
    //            else
    //            {
    //                WebMsgBox.Show("OverDue Limit Exceed...!!", this.Page);
    //                TxtLtype.Focus();
    //                return;
    //            }
    //        }

    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}








    //Old Method
    //protected void Txtmemno_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        int count = grdInsert.Rows.Count;
    //        foreach (GridViewRow gvRow in this.grdInsert.Rows)
    //        {
    //            string CustNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtcustno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtcustno")).Text);
    //            string CustName = Convert.ToString(((TextBox)gvRow.FindControl("Txtname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtname")).Text);
    //            double LoanBal = Convert.ToDouble(((TextBox)gvRow.FindControl("TxtLbal")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtLbal")).Text);
    //            string Memdate = Convert.ToString(((TextBox)gvRow.FindControl("TxtMemdt")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("TxtMemdt")).Text);
    //            string MemNo = Convert.ToInt32(((TextBox)gvRow.FindControl("Txtmemno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtmemno")).Text).ToString();
    //            string MobileNo = Convert.ToString(((TextBox)gvRow.FindControl("txtMobileNo")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtMobileNo")).Text);
    //            string Retdate = Convert.ToString(((TextBox)gvRow.FindControl("Txtretdate")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("Txtretdate")).Text);
    //            string Remser = Convert.ToString(((TextBox)gvRow.FindControl("Txtremser")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtremser")).Text);
    //            string sql, AT;
    //            sql = AT = "";

    //            if (MemNo == "0")
    //                return;

    //            DT = LA.GetLoanAPPCount("ODAmount", Session["BRCD"].ToString(), MemNo, Session["EntryDate"].ToString());
    //            if (DT.Rows.Count > 0)
    //            {
    //                DT = LA.GetLoanAPPCount("SurityYN", Session["BRCD"].ToString(), MemNo, Session["EntryDate"].ToString());
    //                if (DT.Rows.Count > 0)
    //                {
    //                    DT = LA.GetLoanAPPCount("SurityPeriod", Session["BRCD"].ToString(), MemNo, Session["EntryDate"].ToString());
    //                    if (DT.Rows.Count > 0)
    //                    {
    //                        string custname = LA.GetCustomerDetails(MemNo);
    //                        if (custname != null)
    //                        {
    //                            string[] name = custname.Split('_');
    //                            CustName = (string.IsNullOrEmpty(name[0].ToString()) ? "" : name[0].ToString());
    //                            CustNo = (string.IsNullOrEmpty(name[1].ToString()) ? "0" : name[1].ToString());
    //                            ((TextBox)gvRow.FindControl("Txtname")).Text = CustName.ToString();
    //                            ((TextBox)gvRow.FindControl("Txtcustno")).Text = CustNo.ToString();
    //                        }

    //                        string RC = custname;
    //                        if (RC == null && CustNo == "0")
    //                        {
    //                            WebMsgBox.Show("Customer not found", this.Page);
    //                            ((TextBox)gvRow.FindControl("Txtname")).Text = "";
    //                            ((TextBox)gvRow.FindControl("Txtcustno")).Text = "";
    //                            ((TextBox)gvRow.FindControl("Txtmemno")).Text = "";
    //                            return;
    //                        }
    //                        string accno = LA.GetAccNo(Session["BRCD"].ToString(), CustNo);
    //                        LoanBal = Convert.ToDouble(BD.ClBalance(Session["BRCD"].ToString(), TxtLtype.Text, accno, Session["EntryDate"].ToString(), "ClBal").ToString().ToString());
    //                        ((TextBox)gvRow.FindControl("TxtLbal")).Text = string.IsNullOrEmpty(LoanBal.ToString()) ? "" : LoanBal.ToString();

    //                        Memdate = LA.GetDetails(Session["BRCD"].ToString(), TxtLtype.Text, CustNo);
    //                        if (Memdate != null)
    //                            ((TextBox)gvRow.FindControl("TxtMemdt")).Text = string.IsNullOrEmpty(Memdate.ToString()) ? "" : Memdate.ToString();

    //                        MobileNo = LA.GetMobileNo(MemNo);
    //                        ((TextBox)gvRow.FindControl("txtMobileNo")).Text = string.IsNullOrEmpty(MobileNo.ToString()) ? "" : MobileNo.ToString();

    //                        Retdate = LA.GetRetdate(Session["BRCD"].ToString(), TxtLtype.Text, CustNo);
    //                        if (Retdate != null)
    //                            ((TextBox)gvRow.FindControl("Txtretdate")).Text = string.IsNullOrEmpty(Retdate.ToString()) ? "" : Retdate.ToString();
    //                        if (Retdate != null && Memdate != null)
    //                        {
    //                            Remser = conn.GetMonthDiff(Memdate, Retdate);
    //                            ((TextBox)gvRow.FindControl("Txtremser")).Text = string.IsNullOrEmpty(Remser.ToString()) ? "" : Remser.ToString();
    //                        }
    //                        ((TextBox)gvRow.FindControl("Txtmemno")).Focus();
    //                    }
    //                    else
    //                    {
    //                        WebMsgBox.Show("24 Month Not Completed Surity Not Allowed...!!", this.Page);
    //                        TxtLtype.Focus();
    //                        return;
    //                    }
    //                }
    //                else
    //                {
    //                    WebMsgBox.Show("Member Alredy More Than 7 Surity...!!", this.Page);
    //                    TxtLtype.Focus();
    //                    return;
    //                }
    //            }
    //            else
    //            {
    //                WebMsgBox.Show("OverDue Limit Exceed...!!", this.Page);
    //                TxtLtype.Focus();
    //                return;
    //            }
    //        }

    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}





    protected void Txtname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvRow in this.grdInsert.Rows)
            {
                string CustNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtcustno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtcustno")).Text);
                string CustName = Convert.ToString(((TextBox)gvRow.FindControl("Txtname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtname")).Text);
                double LoanBal = Convert.ToDouble(((TextBox)gvRow.FindControl("TxtLbal")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtLbal")).Text);
                string Memdate = Convert.ToString(((TextBox)gvRow.FindControl("TxtMemdt")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("TxtMemdt")).Text);
                string MemNo = Convert.ToInt32(((TextBox)gvRow.FindControl("Txtmemno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtmemno")).Text).ToString();
                string MobileNo = Convert.ToString(((TextBox)gvRow.FindControl("txtMobileNo")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtMobileNo")).Text);
                string Retdate = Convert.ToString(((TextBox)gvRow.FindControl("Txtretdate")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("Txtretdate")).Text);
                string Remser = Convert.ToString(((TextBox)gvRow.FindControl("Txtremser")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtremser")).Text);
                string Standby = Convert.ToString(((TextBox)gvRow.FindControl("TxtStand")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtStand")).Text);

                string sql, AT;
                sql = AT = "";

                string[] custnob = CustName.Split('_');
                if (custnob.Length > 1)
                {
                    CustName = (string.IsNullOrEmpty(custnob[0].ToString()) ? "" : custnob[0].ToString());
                    CustNo = (string.IsNullOrEmpty(custnob[1].ToString()) ? "0" : custnob[1].ToString());
                    MemNo = (string.IsNullOrEmpty(custnob[2].ToString()) ? "0" : custnob[2].ToString());
                    ((TextBox)gvRow.FindControl("Txtname")).Text = CustName.ToString();
                    ((TextBox)gvRow.FindControl("Txtcustno")).Text = CustNo.ToString();
                    ((TextBox)gvRow.FindControl("Txtmemno")).Text = MemNo.ToString();
                }

                string RC = CustName;
                if (RC == null && CustNo == "0")
                {
                    WebMsgBox.Show("Customer not found", this.Page);
                    ((TextBox)gvRow.FindControl("Txtname")).Text = "";
                    ((TextBox)gvRow.FindControl("Txtcustno")).Text = "";
                    ((TextBox)gvRow.FindControl("Txtmemno")).Text = "";
                    return;
                }
                string accno = LA.GetAccNo(Session["BRCD"].ToString(), CustNo);
                LoanBal = Convert.ToDouble(BD.ClBalance(Session["BRCD"].ToString(), TxtLtype.Text, accno, Session["EntryDate"].ToString(), "ClBal").ToString().ToString());
                ((TextBox)gvRow.FindControl("TxtLbal")).Text = string.IsNullOrEmpty(LoanBal.ToString()) ? "" : LoanBal.ToString();

                Memdate = LA.GetDetails(Session["BRCD"].ToString(), TxtLtype.Text, CustNo);
                if (Memdate != null)
                    ((TextBox)gvRow.FindControl("TxtMemdt")).Text = string.IsNullOrEmpty(Memdate.ToString()) ? "" : Memdate.ToString();

                MobileNo = LA.GetMobileNo(MemNo);
                ((TextBox)gvRow.FindControl("txtMobileNo")).Text = string.IsNullOrEmpty(MobileNo.ToString()) ? "" : MobileNo.ToString();

                Retdate = LA.GetRetdate(Session["BRCD"].ToString(), TxtLtype.Text, CustNo);
                if (Retdate != null)
                    ((TextBox)gvRow.FindControl("Txtretdate")).Text = string.IsNullOrEmpty(Retdate.ToString()) ? "" : Retdate.ToString();
                if (Retdate != null && Memdate != null)
                {
                    Remser = conn.GetMonthDiff(Memdate, Retdate);
                    ((TextBox)gvRow.FindControl("Txtremser")).Text = string.IsNullOrEmpty(Remser.ToString()) ? "" : Remser.ToString();
                }
                Standby = LA.GetSurityStandby(MemNo, txtCustNo.Text);
                ((TextBox)gvRow.FindControl("TxtStand")).Text = string.IsNullOrEmpty(Standby.ToString()) ? "" : Standby.ToString();

                ((TextBox)gvRow.FindControl("Txtmemno")).Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }


        try
        {
            foreach (GridViewRow gvRow in this.grdInsert.Rows)
            {
                string CustNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtcustno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtcustno")).Text);
                string CustName = Convert.ToString(((TextBox)gvRow.FindControl("Txtname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtname")).Text);
                string CUNAME = CustName;
                string[] custnob = CUNAME.Split('_');
                if (custnob.Length > 1)
                {
                    CustName = custnob[0].ToString();
                    CustNo = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txtprcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvRow in this.GrdprevLoan.Rows)
            {
                string PPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtprcd")).Text);
                string PPrdname = Convert.ToString(((TextBox)gvRow.FindControl("TxtPname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtPname")).Text);
                string GL = "";
                if (PPrdNo != "0")
                    GL = BD.GetAccTypeGL(PPrdNo, Session["BRCD"].ToString());
                else
                    GL = null;
                string[] GLCODE = GL.Split('_');

                ViewState["DRGL"] = GL[1].ToString();
                string PDName = customcs.GetProductName(PPrdNo, Session["BRCD"].ToString());
                if (PDName != null)
                {
                    PPrdname = PDName;
                    ((TextBox)gvRow.FindControl("TxtPname")).Text = PPrdname.ToString();
                    string Closing = LA.GetClosing(PPrdNo, txtCustNo.Text, Session["EntryDate"].ToString());
                    ((TextBox)gvRow.FindControl("Txtbal")).Text = Closing.ToString();
                }
                else
                {
                    WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                    PPrdNo = "";
                }
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtPname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvRow in this.GrdprevLoan.Rows)
            {
                string PPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtprcd")).Text);
                string PPrdname = Convert.ToString(((TextBox)gvRow.FindControl("TxtPname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtPname")).Text);

                string custno = PPrdname;
                string[] CT = custno.Split('_');
                if (CT.Length > 0)
                {
                    PPrdname = CT[0].ToString();
                    PPrdNo = CT[1].ToString();
                    string[] GLS = BD.GetAccTypeGL(PPrdNo, Session["BRCD"].ToString()).Split('_');
                    ViewState["DRGL"] = GLS[1].ToString();
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
            if (ViewState["Flag"].ToString() == "AD")
            {
                if (Convert.ToDouble(TxtLapply.Text.ToString() == "" ? "0" : TxtLapply.Text.ToString()) > Convert.ToDouble(TxttotDed.Text.ToString() == "" ? "0" : TxttotDed.Text.ToString()))
                {

                    int j = 0;
                    foreach (GridViewRow gvRow in this.grdInsert.Rows)
                    {
                        string SrNumber = Convert.ToInt32(((TextBox)gvRow.FindControl("txtSrNo")).Text).ToString();
                        string CustNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtcustno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtcustno")).Text);
                        string Name = Convert.ToString(((TextBox)gvRow.FindControl("Txtname")).Text == "" ? " " : ((TextBox)gvRow.FindControl("Txtname")).Text);
                        string LoanBal = Convert.ToString(((TextBox)gvRow.FindControl("TxtLbal")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtLbal")).Text);
                        string Memdate = Convert.ToString(((TextBox)gvRow.FindControl("TxtMemdt")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("TxtMemdt")).Text);
                        string MemNo = Convert.ToInt32(((TextBox)gvRow.FindControl("Txtmemno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtmemno")).Text).ToString();
                        string Retdate = Convert.ToString(((TextBox)gvRow.FindControl("Txtretdate")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("Txtretdate")).Text);
                        string Remser = Convert.ToString(((TextBox)gvRow.FindControl("Txtremser")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtremser")).Text);
                        string Standby = Convert.ToString(((TextBox)gvRow.FindControl("TxtStand")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtStand")).Text);
                        string MobileNo = Convert.ToString(((TextBox)gvRow.FindControl("txtMobileNo")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtMobileNo")).Text);

                        res = LA.GetRecTypeCode(Session["BRCD"].ToString(), TxtLtype.Text);
                        res1 = LA.GetRecCount(Session["BRCD"].ToString(), TxtLtype.Text);
                        resDelete = LA.GetCustDelete(Session["BRCD"].ToString(), "1001", Session["MID"].ToString(), CustNo, TxtLtype.Text);

                        if (res == "Y" && SrNumber == "1" && CustNo == "0")
                        {
                            WebMsgBox.Show("Please Enter Surity", this.Page);
                            return;
                        }
                        else
                        {
                            if (Convert.ToSingle(res1) >= Convert.ToSingle(SrNumber) && CustNo != "0")
                            {
                                if (CustNo != "0")
                                {
                                    resC = LA.GetCust(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), SrNumber, Session["CNO"].ToString(), CustNo, MemNo, Name, Memdate, Retdate, LoanBal, Remser, MobileNo, "1001", Session["MID"].ToString(), "1", TxtLtype.Text);
                                }
                            }
                            else
                            {
                                resDelete = LA.GetCustDelete(Session["BRCD"].ToString(), "1001", Session["MID"].ToString(), CustNo, TxtLtype.Text);
                                WebMsgBox.Show("Please Enter ALL Surity", this.Page);
                                return;
                            }
                        }
                        j = j + 1;
                    }


                    int i = 0;
                    foreach (GridViewRow gvRow in this.GrdprevLoan.Rows)
                    {
                        string SPNumber = Convert.ToInt32(((TextBox)gvRow.FindControl("txtPSrNo")).Text).ToString();
                        string PPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtprcd")).Text);
                        string AccNo = Convert.ToString(((TextBox)gvRow.FindControl("txtAccno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtAccno")).Text);
                        string Pname = Convert.ToString(((TextBox)gvRow.FindControl("TxtPname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtPname")).Text);
                        string Bal = Convert.ToString(((TextBox)gvRow.FindControl("Txtbal")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtbal")).Text);
                        string Int = Convert.ToString(((TextBox)gvRow.FindControl("TxtInt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtInt")).Text);
                        string DedP = Convert.ToString(((TextBox)gvRow.FindControl("TxtDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtDeduction")).Text);
                        string Irno = LA.GetIrNo(Session["BRCD"].ToString(), PPrdNo);
                        if (PPrdNo != "0")
                        {
                            resP = LA.GetPrev(Session["BRCD"].ToString(), SPNumber, Irno, Int, Pname, "1001", Session["MID"].ToString(), "1", "1", Session["CNO"].ToString(), "1", TxtLtype.Text, AccNo, Bal);
                            resPD = LA.GetPrevDed(Session["BRCD"].ToString(), SPNumber, PPrdNo, DedP, Pname, "1001", Session["MID"].ToString(), "1", "1", Session["CNO"].ToString(), "1", TxtLtype.Text, AccNo, Bal);
                        }
                        i = i + 1;
                    }

                    int k = 0;
                    foreach (GridViewRow gvRow in this.grdstandard.Rows)
                    {
                        string SSNumber = Convert.ToInt32(((TextBox)gvRow.FindControl("txtSSrNo")).Text).ToString();
                        string SPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("TxtSprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSprcd")).Text);
                        string Sname = Convert.ToString(((TextBox)gvRow.FindControl("TxtSname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSname")).Text);
                        string accno = Convert.ToString(((TextBox)gvRow.FindControl("txtAccno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtAccno")).Text);
                        string DedS = Convert.ToString(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                        string Per = Convert.ToString(((TextBox)gvRow.FindControl("txtPer")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtPer")).Text);
                        if (SPrdNo != "0")
                        {
                            resS = LA.GetStandard(Session["BRCD"].ToString(), SSNumber, SPrdNo, DedS, Sname, "1001", Session["MID"].ToString(), "1", "1", Session["CNO"].ToString(), "2", TxtLtype.Text, Per, accno);
                        }
                        k = k + 1;
                    }

                    resultint = LA.Insertdata(Session["BRCD"].ToString(), TxtLtype.Text, TxtLname.Text, DDlpurpose.SelectedItem.Text, TxtLapply.Text, "0", TxtNet.Text, "0",
                                   "0", "0", TxtLapply.Text, "0", "0", "0", "0", "1001", Session["MID"].ToString(), Session["EntryDate"].ToString(),
                                   Session["CNO"].ToString(), Session["CNO"].ToString(), "1", "0", txtIntRate.Text.ToString(), txtTotPeriod.Text, ddlSancAuthCd.SelectedItem.Text, ddlRecAuthCd.SelectedItem.Text, "");

                    if (resultint > 0 || resC > 0 || resP > 0 || resS > 0)
                    {
                        WebMsgBox.Show("Data Saved Successfully..!!", this.Page);

                        grdInsert.DataSource = null;
                        grdInsert.DataBind();
                        EmptyGridBind();
                        GrdprevLoan.DataSource = null;
                        GrdprevLoan.DataBind();
                        grdstandard.DataSource = null;
                        grdstandard.DataBind();
                        StandardGridBind();

                        BindGrid();
                        autoglname.ContextKey = Session["BRCD"].ToString();
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "F7_Add _" + TxtLtype.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("Loan Amount Must Be Greater Than Deduction", this.Page);
                    return;
                }
            }
            else if (ViewState["Flag"].ToString() == "AT")
            {
                string AppNo = LA.Appno(TxtLtype.Text, Session["BRCD"].ToString());
                hdnApp.Value = AppNo;
                string BondNo = LA.BondNo(TxtLtype.Text, Session["BRCD"].ToString());
                LA.updateapp(AppNo, TxtLtype.Text, Session["BRCD"].ToString());
                LA.updateBond(BondNo, TxtLtype.Text, Session["BRCD"].ToString());
                resultint = LA.Authorise(Session["BRCD"].ToString(), TxtLtype.Text, Session["MID"].ToString(), Session["CNO"].ToString(), "2", AppNo, BondNo);

                int j = 0;
                foreach (GridViewRow gvRow in this.grdInsert.Rows)
                {
                    string SrNumber = Convert.ToInt32(((TextBox)gvRow.FindControl("txtSrNo")).Text).ToString();
                    string CustNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtcustno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtcustno")).Text);
                    string Name = Convert.ToString(((TextBox)gvRow.FindControl("Txtname")).Text == "" ? " " : ((TextBox)gvRow.FindControl("Txtname")).Text);
                    string LoanBal = Convert.ToString(((TextBox)gvRow.FindControl("TxtLbal")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtLbal")).Text);
                    string Memdate = Convert.ToString(((TextBox)gvRow.FindControl("TxtMemdt")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("TxtMemdt")).Text);
                    string MemNo = Convert.ToInt32(((TextBox)gvRow.FindControl("Txtmemno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtmemno")).Text).ToString();
                    string Retdate = Convert.ToString(((TextBox)gvRow.FindControl("Txtretdate")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("Txtretdate")).Text);
                    string Remser = Convert.ToString(((TextBox)gvRow.FindControl("Txtremser")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtremser")).Text);
                    string Standby = Convert.ToString(((TextBox)gvRow.FindControl("TxtStand")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtStand")).Text);
                    string MobileNo = Convert.ToString(((TextBox)gvRow.FindControl("txtMobileNo")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtMobileNo")).Text);
                    if (CustNo != "0")
                    {
                        resC = LA.GetCustAut(Session["BRCD"].ToString(), Session["CNO"].ToString(), Session["MID"].ToString(), TxtLtype.Text, "2", AppNo, BondNo, MemNo);
                    }
                    j = j + 1;
                }

                int i = 0;
                foreach (GridViewRow gvRow in this.GrdprevLoan.Rows)
                {
                    string SPNumber = Convert.ToInt32(((TextBox)gvRow.FindControl("txtPSrNo")).Text).ToString();
                    string PPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtprcd")).Text);
                    string Bal = Convert.ToString(((TextBox)gvRow.FindControl("Txtbal")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtbal")).Text);
                    string Int = Convert.ToString(((TextBox)gvRow.FindControl("TxtInt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtInt")).Text);
                    string DedP = Convert.ToString(((TextBox)gvRow.FindControl("TxtDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtDeduction")).Text);
                    string Irno = LA.GetIrNo(Session["BRCD"].ToString(), PPrdNo);
                    if (PPrdNo != "0")
                    {
                        resP = LA.GetPrevAut(Session["BRCD"].ToString(), Session["CNO"].ToString(), Session["MID"].ToString(), PPrdNo, "2", AppNo, BondNo, TxtLtype.Text, "1");
                        resP = LA.GetPrevAut(Session["BRCD"].ToString(), Session["CNO"].ToString(), Session["MID"].ToString(), Irno, "2", AppNo, BondNo, TxtLtype.Text, "1");
                    }
                    i = i + 1;
                }

                int k = 0;
                foreach (GridViewRow gvRow in this.grdstandard.Rows)
                {
                    string SSNumber = Convert.ToInt32(((TextBox)gvRow.FindControl("txtSSrNo")).Text).ToString();
                    string SPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("TxtSprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSprcd")).Text);
                    string DedS = Convert.ToString(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                    if (SPrdNo != "0")
                    {
                        resS = LA.GetStAut(Session["BRCD"].ToString(), Session["CNO"].ToString(), Session["MID"].ToString(), SPrdNo, "2", AppNo, BondNo, TxtLtype.Text, "2");
                    }
                    k = k + 1;
                }

                if (resultint > 0 || resC > 0 || resP > 0 || resS > 0)
                {
                    Label1.Text = "Data Authorised Successfully with Application No: '" + AppNo + "'...!!";
                    string Modal_Flag = "DivAddMore";

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#" + Modal_Flag + "').modal('show');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);

                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "F7_Auth _" + AppNo + "_" + TxtLtype.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
            }
            else if (ViewState["Flag"].ToString() == "MD")
            {
                resultint = LA.Modify(Session["BRCD"].ToString(), TxtLtype.Text, TxtLname.Text, DDlpurpose.SelectedItem.Text, TxtLapply.Text, "0", TxtNet.Text, "0",
                           "0", "0", Txttosanction.Text, "0", "0", "0", "0", txtTotPeriod.Text, "1002", Session["MID"].ToString(),
                           Session["EntryDate"].ToString(), Session["CNO"].ToString(), Session["CNO"].ToString(), "1");

                int j = 0;
                foreach (GridViewRow gvRow in this.grdInsert.Rows)
                {
                    string SrNumber = Convert.ToInt32(((TextBox)gvRow.FindControl("txtSrNo")).Text).ToString();
                    string CustNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtcustno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtcustno")).Text);
                    string Name = Convert.ToString(((TextBox)gvRow.FindControl("Txtname")).Text == "" ? " " : ((TextBox)gvRow.FindControl("Txtname")).Text);
                    string LoanBal = Convert.ToString(((TextBox)gvRow.FindControl("TxtLbal")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtLbal")).Text);
                    string Memdate = Convert.ToString(((TextBox)gvRow.FindControl("TxtMemdt")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("TxtMemdt")).Text);
                    string MemNo = Convert.ToInt32(((TextBox)gvRow.FindControl("Txtmemno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtmemno")).Text).ToString();
                    string Retdate = Convert.ToString(((TextBox)gvRow.FindControl("Txtretdate")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("Txtretdate")).Text);
                    string Remser = Convert.ToString(((TextBox)gvRow.FindControl("Txtremser")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtremser")).Text);
                    string Standby = Convert.ToString(((TextBox)gvRow.FindControl("TxtStand")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtStand")).Text);
                    string MobileNo = Convert.ToString(((TextBox)gvRow.FindControl("txtMobileNo")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtMobileNo")).Text);
                    if (CustNo != "0")
                    {
                        resC = LA.GetCustMod(Session["BRCD"].ToString(), SrNumber, Session["CNO"].ToString(), CustNo, MemNo, Name, Memdate, Retdate, LoanBal, Remser, MobileNo, "1002", Session["MID"].ToString(), TxtLtype.Text, "1");
                    }
                    j = j + 1;
                }

                int i = 0;
                foreach (GridViewRow gvRow in this.GrdprevLoan.Rows)
                {
                    string PPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtprcd")).Text);
                    string PAccNo = Convert.ToString(((TextBox)gvRow.FindControl("txtAccno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtAccno")).Text);
                    string Int = Convert.ToString(((TextBox)gvRow.FindControl("TxtInt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtInt")).Text);
                    string DedP = Convert.ToString(((TextBox)gvRow.FindControl("TxtDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtDeduction")).Text);
                    if (PPrdNo != "0")
                    {
                        resP = LA.GetPrevMod(Session["BRCD"].ToString(), PPrdNo, PAccNo, Int, DedP, Session["CNO"].ToString());
                    }
                    i = i + 1;
                }

                int k = 0;
                foreach (GridViewRow gvRow in this.grdstandard.Rows)
                {
                    string SSNumber = Convert.ToInt32(((TextBox)gvRow.FindControl("txtSSrNo")).Text).ToString();
                    string SPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("TxtSprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSprcd")).Text);
                    string Percent = Convert.ToString(((TextBox)gvRow.FindControl("txtPer")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtPer")).Text);
                    string DedS = Convert.ToString(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                    if (SPrdNo != "0")
                    {
                        resS = LA.GetStMod(Session["BRCD"].ToString(), SSNumber, SPrdNo, DedS, Percent, Session["MID"].ToString(), "1", Session["CNO"].ToString());
                    }
                    k = k + 1;
                }

                if (resultint > 0 && resC > 0 && resP > 0 && resS > 0)
                {
                    WebMsgBox.Show("Data Modified Successfully ..!!", this.Page);
                    grdInsert.DataSource = null;
                    grdInsert.DataBind();
                    EmptyGridBind();
                    GrdprevLoan.DataSource = null;
                    GrdprevLoan.DataBind();
                    grdstandard.DataSource = null;
                    grdstandard.DataBind();
                    StandardGridBind();

                    BindGrid();
                    autoglname.ContextKey = Session["BRCD"].ToString();
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "F7_Mod _" + TxtLtype.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearData();
                    return;
                }
            }
            else if (ViewState["Flag"].ToString() == "DL")
            {
                resultint = LA.Delete(Session["BRCD"].ToString(), TxtLtype.Text, Session["MID"].ToString(), Session["CNO"].ToString(), "99");
                resC = LA.GetCustDel(Session["BRCD"].ToString(), Session["CNO"].ToString(), Session["MID"].ToString(), TxtLtype.Text, "99");
                resP = LA.GetPrevDel(Session["BRCD"].ToString(), Session["CNO"].ToString(), Session["MID"].ToString(), "99", TxtLtype.Text);

                if (resultint > 0 && resC > 0 && resP > 0)
                {
                    WebMsgBox.Show("Data Deleted Successfully .!!", this.Page);
                    grdInsert.DataSource = null;
                    grdInsert.DataBind();
                    EmptyGridBind();
                    GrdprevLoan.DataSource = null;
                    GrdprevLoan.DataBind();
                    grdstandard.DataSource = null;
                    grdstandard.DataBind();
                    StandardGridBind();

                    BindGrid();
                    autoglname.ContextKey = Session["BRCD"].ToString();
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "F7_Del _" + TxtLtype.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearData();
                    return;
                }
            }

            BindGrid();
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }

    protected void TxtSprcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvRow in this.grdstandard.Rows)
            {
                string SPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("TxtSprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSprcd")).Text);
                string SPrdname = Convert.ToString(((TextBox)gvRow.FindControl("TxtSname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSname")).Text);
                string accno1 = Convert.ToString(((TextBox)gvRow.FindControl("txtAccno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtAccno")).Text);
                string GL = "";
                if (SPrdNo != "0")
                    GL = BD.GetAccTypeGL(SPrdNo, Session["BRCD"].ToString());
                else
                    GL = null;
                string[] GLCODE = GL.Split('_');
                string Accno = BD.GetACCNo(Session["BRCD"].ToString(), SPrdNo, txtCustNo.Text);
                if (Accno != null)
                {
                    accno1 = Accno;
                    ((TextBox)gvRow.FindControl("txtAccno")).Text = accno1.ToString();
                }
                ViewState["DRGL"] = GL[1].ToString();
                string PDName = customcs.GetProductName(SPrdNo, Session["BRCD"].ToString());
                if (PDName != null)
                {
                    SPrdname = PDName;
                    ((TextBox)gvRow.FindControl("TxtSname")).Text = SPrdname.ToString();
                }
                else
                {
                    WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                    SPrdNo = "";
                }
                ((TextBox)gvRow.FindControl("TxtSprcd")).Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtSname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvRow in this.grdstandard.Rows)
            {
                string SPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("TxtSprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSprcd")).Text);
                string SPrdname = Convert.ToString(((TextBox)gvRow.FindControl("TxtSname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSname")).Text);

                string[] custnob = SPrdname.Split('_');
                if (custnob.Length > 1)
                {
                    ViewState["DRGL"] = (string.IsNullOrEmpty(custnob[2].ToString()) ? "0" : custnob[2].ToString());
                    SPrdname = (string.IsNullOrEmpty(custnob[0].ToString()) ? "" : custnob[0].ToString());
                    SPrdNo = (string.IsNullOrEmpty(custnob[1].ToString()) ? "0" : custnob[1].ToString());
                    ((TextBox)gvRow.FindControl("TxtSprcd")).Text = SPrdNo.ToString();
                    ((TextBox)gvRow.FindControl("TxtSname")).Text = SPrdname.ToString();
                    ((TextBox)gvRow.FindControl("TxtSprcd")).Focus();
                }

                ((TextBox)gvRow.FindControl("TxtSprcd")).Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtDeduction_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int i = 0, Result = 0;
            string DedP = "";
            string DedI = "";
            string Bal = "";
            string SPNumber = "";
            string CloseAmt = "";

            foreach (GridViewRow gvRow in this.GrdprevLoan.Rows)
            {
                SPNumber = Convert.ToInt32(((TextBox)gvRow.FindControl("txtPSrNo")).Text).ToString();
                Bal = Convert.ToString(((TextBox)gvRow.FindControl("Txtbal")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtbal")).Text);
                DedP = Convert.ToString(((TextBox)gvRow.FindControl("TxtDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtDeduction")).Text);
                DedI = Convert.ToString(((TextBox)gvRow.FindControl("TxtInt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtInt")).Text);
                Result += Convert.ToInt32(DedP) + Convert.ToInt32(DedI);
                TxtSubD.Text = Result.ToString();
                ((TextBox)gvRow.FindControl("TxtInt")).Focus();

                if (SPNumber == "1")
                {
                    CloseAmt = DedP;
                    if (Convert.ToSingle(Convert.ToString(Bal.ToString() == "" ? "0" : Bal.ToString())) >= Convert.ToSingle(CloseAmt.ToString()))
                    {

                    }
                    else
                    {
                        WebMsgBox.Show("Amount Is More Than Previous Loan Balance", this.Page);
                        ((TextBox)gvRow.FindControl("TxtDeduction")).Text = "0";
                        return;
                    }
                }
            }

            i = i + 1;

            TxtSubD.Text = (TxtSubD.Text == "" ? "0" : TxtSubD.Text);
            TxtSubS.Text = (TxtSubS.Text == "" ? "0" : TxtSubS.Text);
            TxtSubO.Text = (TxtSubO.Text == "" ? "0" : TxtSubO.Text);
            float PrevT = Convert.ToSingle(TxtSubD.Text);
            float StdT = Convert.ToSingle(TxtSubS.Text);
            float OtrT = Convert.ToSingle(TxtSubO.Text);

            TxttotDed.Text = Convert.ToSingle(PrevT + StdT + OtrT).ToString();


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtSDeduction_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int k = 0, Var = 0;
            string DedS = "";
            foreach (GridViewRow gvRow in this.grdstandard.Rows)
            {
                DedS = Convert.ToString(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                Var += Convert.ToInt32(DedS);
                TxtSubS.Text = Var.ToString();

            }
            k = k + 1;
            TxtSubD.Text = (TxtSubD.Text == "" ? "0" : TxtSubD.Text);
            TxtSubS.Text = (TxtSubS.Text == "" ? "0" : TxtSubS.Text);
            TxtSubO.Text = (TxtSubO.Text == "" ? "0" : TxtSubO.Text);

            float PrevT = Convert.ToSingle(TxtSubD.Text);
            float StdT = Convert.ToSingle(TxtSubS.Text);
            float OtrT = Convert.ToSingle(TxtSubO.Text);
            TxttotDed.Text = Convert.ToSingle(PrevT + StdT + OtrT).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearData()
    {
        TxtLtype.Text = "";
        TxtLname.Text = "";
        Txtpur.Text = "";
        DDlpurpose.SelectedItem.Text = "";
        TxtLapply.Text = "";
        //TxtGross.Text = "";
        TxtNet.Text = "";
        //Txttwntyfive.Text = "";
        //Txtmembrship.Text = "";
        //Txtbysal.Text = "";
        Txttosanction.Text = "";
        //Txtrepay.Text = "";
        //Txtinstll.Text = "";
        txtTotPeriod.Text = "";
        txtIntRate.Text = "";
        TxtSubD.Text = "";
        TxtSubS.Text = "";
        TxtSubO.Text = "";
        TxttotDed.Text = "";
        TxtInstDate.Text = "";
        TxtCustDOB.Text = "";
        TxtCustAGe.Text = "";
        TxtJoinAge.Text = "";
        TxtjoinDT.Text = "";
    }

    protected void LnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Id"] = ARR[0].ToString();
            ViewState["LOANTYPE"] = ARR[1].ToString();
            ViewState["CUSTNO"] = ARR[2].ToString();
            ViewState["Flag"] = "MD";
            btnSubmit.Text = "Modify";

            GetDetails();
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
            ViewState["Id"] = ARR[0].ToString();
            ViewState["LOANTYPE"] = ARR[1].ToString();
            ViewState["CUSTNO"] = ARR[2].ToString();
            ViewState["Flag"] = "AT";
            // btnSubmit.Text = "Authorise";
            // GetDetails();
            AuthoriseData();
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
            ViewState["LOANTYPE"] = ARR[1].ToString();
            ViewState["CUSTNO"] = ARR[2].ToString();
            ViewState["Flag"] = "DL";
            btnSubmit.Text = "Delete";
            GetDetails();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void CallEdit()
    {
        try
        {
            if (ViewState["Flag"].ToString() != "AD")
            {
                DT = LA.GetLoanInfo(Session["BRCD"].ToString(), ViewState["Id"].ToString(), ViewState["LOANTYPE"].ToString(), ViewState["CUSTNO"].ToString());
                if (DT.Rows.Count > 0)
                {
                    TxtLtype.Text = (string.IsNullOrEmpty(DT.Rows[0]["LOANPRODUCT"].ToString()) ? "0" : DT.Rows[0]["LOANPRODUCT"].ToString());
                    TxtLname.Text = (string.IsNullOrEmpty(DT.Rows[0]["LOANTYPE"].ToString()) ? "0" : DT.Rows[0]["LOANTYPE"].ToString());
                    DDlpurpose.SelectedItem.Text = (string.IsNullOrEmpty(DT.Rows[0]["LOANPURPOSE"].ToString()) ? "0" : DT.Rows[0]["LOANPURPOSE"].ToString());
                    TxtLapply.Text = (string.IsNullOrEmpty(DT.Rows[0]["LOANAPPLY"].ToString()) ? "0" : DT.Rows[0]["LOANAPPLY"].ToString());
                    //TxtGross.Text = (string.IsNullOrEmpty(DT.Rows[0]["GROSSSAL"].ToString()) ? "0" : DT.Rows[0]["GROSSSAL"].ToString());
                    TxtNet.Text = (string.IsNullOrEmpty(DT.Rows[0]["NETSAL"].ToString()) ? "0" : DT.Rows[0]["NETSAL"].ToString());
                    //Txttwntyfive.Text = (string.IsNullOrEmpty(DT.Rows[0]["TWENTYFIVE"].ToString()) ? "0" : DT.Rows[0]["TWENTYFIVE"].ToString());
                    //Txtmembrship.Text = (string.IsNullOrEmpty(DT.Rows[0]["LOANELGIBLITYMEMBER"].ToString()) ? "0" : DT.Rows[0]["LOANELGIBLITYMEMBER"].ToString());
                    //Txtbysal.Text = (string.IsNullOrEmpty(DT.Rows[0]["LOANELGIBLITYSALARY"].ToString()) ? "0" : DT.Rows[0]["LOANELGIBLITYSALARY"].ToString());
                    Txttosanction.Text = (string.IsNullOrEmpty(DT.Rows[0]["SANCITONAMOUNT"].ToString()) ? "0" : DT.Rows[0]["SANCITONAMOUNT"].ToString());
                    //Txtrepay.Text = (string.IsNullOrEmpty(DT.Rows[0]["REPAYCAP"].ToString()) ? "0" : DT.Rows[0]["REPAYCAP"].ToString());
                    //Txtinstll.Text = (string.IsNullOrEmpty(DT.Rows[0]["INSTMANUAL"].ToString()) ? "0" : DT.Rows[0]["INSTMANUAL"].ToString());
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtNet_TextChanged(object sender, EventArgs e)
    {
        try
        {
            float Rate = 0;
            string BasicPayM = LA.GetBasicPay_Month("BasicPay_Month");
            string BasicPayAmt = LA.GetBasicPay_Month("BasicPay_AMT");
            string BasicPayLess = LA.GetBasicPay_Month("BasicPay_less");
            string BasicPayAbove = LA.GetBasicPay_Month("BasicPay_Above");
            string EMI = "";
            int round, round1;

            round = Convert.ToInt32(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='LnAPPRoundAmt_1'"));

            float RetPeriod = Convert.ToSingle(BasicPayM);
            float Grosssal = Convert.ToSingle(TxtNet.Text);
            string Tobesanctn = TxtNet.Text;
            RetPeriod = Convert.ToSingle(TxtCustAGe.Text);

            if (RetPeriod > Convert.ToSingle(BasicPayM))
            {
                EMI = Math.Round(Convert.ToSingle((Grosssal * Convert.ToSingle(BasicPayLess))), 0).ToString();
                Txttosanction.Text = Math.Round(Convert.ToDouble((((Convert.ToInt32(EMI)) / round) * round)), 2).ToString();

                //Txttosanction.Text = Convert.ToSingle((Grosssal * Convert.ToSingle(BasicPayLess))).ToString();
            }
            else
            {
                EMI = Math.Round(Convert.ToSingle((Convert.ToSingle(BasicPayAmt) * Convert.ToSingle(BasicPayAbove))), 0).ToString();
                Txttosanction.Text = Math.Round(Convert.ToDouble((((Convert.ToInt32(EMI) + round) / round) * round)), 2).ToString();

                // Txttosanction.Text = Convert.ToSingle((Convert.ToSingle(BasicPayAmt) * Convert.ToSingle(BasicPayAbove))).ToString();
            }

            float Netsal = Convert.ToSingle(TxtNet.Text);

            string[] LoanDet = LA.LoanDetails(Session["BRCD"].ToString(), TxtLtype.Text.ToString()).ToString().Split('_');
            if (LoanDet.Length > 0)
            {
                //txtTotPeriod.Text = "0";// LoanDet[0].ToString();
                txtIntRate.Text = LoanDet[1].ToString();
                ddlInstType.SelectedValue = "1";
            }

            TxtLapply.Text = TxtNet.Text;

            TxtLapply.Focus();
            GetLoanDataBySanctionAmount();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtPer_TextChanged(object sender, EventArgs e)
    {
        string Amount = Txttosanction.Text;
        string Amount1 = "";
        foreach (GridViewRow gvRow in this.grdstandard.Rows)
        {
            string AccNo = Convert.ToString(((TextBox)gvRow.FindControl("txtAccno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtAccno")).Text);
            string Per = Convert.ToString(((TextBox)gvRow.FindControl("txtPer")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtPer")).Text);
            string Subglcode = Convert.ToString(((TextBox)gvRow.FindControl("TxtSprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSprcd")).Text);

            if (Per != "0")
            {
                if ((Subglcode == "1") || (Subglcode == "4"))
                {
                    ((TextBox)gvRow.FindControl("TxtSDeduction")).Text = (Math.Round(Convert.ToDouble(Amount) * Convert.ToDouble(Per) / 100)).ToString();
                }
                //if (Subglcode == "4")
                //{
                //    Amount1 = BD.ClBalance(Session["BRCD"].ToString(), Subglcode, AccNo, Session["EntryDate"].ToString(), "ClBal").ToString();
                //    ((TextBox)gvRow.FindControl("TxtSDeduction")).Text = (Math.Round(Convert.ToDouble(Amount1) - (Convert.ToDouble(Amount) * Convert.ToDouble(Per) / 100))).ToString();
                //}
                //else
                //{
                //      ((TextBox)gvRow.FindControl("TxtSDeduction")).Text = (Math.Round(Convert.ToDouble(Amount) * Convert.ToDouble(Per) / 100)).ToString();
                //}
            }

        }
        int k = 0, Var = 0;
        string DedS = "";
        foreach (GridViewRow gvRow in this.grdstandard.Rows)
        {
            DedS = Convert.ToString(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
            Var += Convert.ToInt32(DedS);
            TxtSubS.Text = Var.ToString();

        }
        k = k + 1;
        TxtSubD.Text = (TxtSubD.Text == "" ? "0" : TxtSubD.Text);
        TxtSubS.Text = (TxtSubS.Text == "" ? "0" : TxtSubS.Text);
        TxtSubO.Text = (TxtSubO.Text == "" ? "0" : TxtSubO.Text);
        float PrevT = Convert.ToSingle(TxtSubD.Text);
        float StdT = Convert.ToSingle(TxtSubS.Text);
        float OtrT = Convert.ToSingle(TxtSubO.Text);
        TxttotDed.Text = Convert.ToSingle(PrevT + StdT + OtrT).ToString();
    }

    protected void TxtLapply_TextChanged(object sender, EventArgs e)
    {
        GetLoanDataBySanctionAmount();
    }

    private void GetLoanDataBySanctionAmount()
    {
        DataTable dt = new DataTable();
        float Amount = Convert.ToSingle(Txttosanction.Text);
        int Eligiable = 0;
        try
        {
            string LimitAmt = LA.GetBasicPay_Month(TxtLtype.Text);
            Eligiable = Convert.ToInt32(LimitAmt);

            if (Convert.ToSingle(Convert.ToString(TxtLapply.Text == "" ? "0" : TxtLapply.Text)) < Eligiable)
            {
                if (Convert.ToSingle(Convert.ToString(TxtLapply.Text == "" ? "0" : TxtLapply.Text)) <= Amount)
                {
                    if (Convert.ToDouble(Convert.ToString(TxtLapply.Text == "" ? "0" : TxtLapply.Text)) > 0)
                    {
                        dt = LA.GetLoanDetails1(GrdprevLoan, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "DL");
                        if (dt.Rows.Count > 0)
                        {
                            dt.Rows.Add(TxtLtype.Text, "9999", TxtLapply.Text, TxtLname.Text, "0", "0");
                            GrdprevLoan.DataSource = dt;
                            GrdprevLoan.DataBind();
                            EMIGetDetails();
                            StandardGridBind();
                        }
                        else
                        {
                            dt.Rows.Add(TxtLtype.Text, "9999", TxtLapply.Text, TxtLname.Text, "0", "0");
                            GrdprevLoan.DataSource = dt;
                            GrdprevLoan.DataBind();
                            EMIGetDetails();
                            StandardGridBind();
                        }
                    }
                    else
                    {
                        TxtLapply.Focus();
                        WebMsgBox.Show("Enter Loan Amount First ...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("Loan Amount Greater Than To Be Sanction Amount", this.Page);
                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Sanction Amount Not Eligiable", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmAVS5028.aspx?AppNo=" + hdnApp.Value + "&GL=" + TxtLtype.Text + "", false);
    }

    //protected void ddlInstType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    double AMT, PR, INST;
    //    string EMI = "";
    //    int round, round1;
    //    try
    //    {
    //        string Para = Convert.ToString(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='LoanAPPRound'"));
    //        round = Convert.ToInt32(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='LnAPPRoundAmt'"));

    //        round1 = round / 2;

    //        if (Para == "Y")
    //        {
    //            PR = Convert.ToDouble(txtTotPeriod.Text.ToString() == "" ? "0" : txtTotPeriod.Text.ToString());
    //            INST = Convert.ToDouble(txtIntRate.Text.ToString() == "" ? "0" : txtIntRate.Text.ToString());
    //            AMT = Convert.ToDouble(TxtLapply.Text.ToString() == "" ? "0" : Txttosanction.Text.ToString());

    //            if (ddlInstType.SelectedValue == "1")
    //            {
    //                EMI = Math.Round(Convert.ToDouble(AMT / PR), 0).ToString();
    //                Txtinstll.Text = Math.Round(Convert.ToDouble((((Convert.ToInt32(EMI) + round1) / round) * round)), 2).ToString();
    //            }
    //            if (ddlInstType.SelectedValue == "2")
    //            {
    //                var IntRate = (double)INST / 100 / 12;
    //                var Denominator = Math.Pow((1 + IntRate), PR) - 1;
    //                EMI = Math.Round((IntRate + (IntRate / Denominator)) * AMT, 0).ToString();
    //                Txtinstll.Text = Math.Round(Convert.ToDouble((((Convert.ToInt32(EMI) + round1) / round) * round)), 2).ToString();
    //            }
    //        }
    //        else
    //        {
    //            PR = Convert.ToDouble(txtTotPeriod.Text.ToString() == "" ? "0" : txtTotPeriod.Text.ToString());
    //            INST = Convert.ToDouble(txtIntRate.Text.ToString() == "" ? "0" : txtIntRate.Text.ToString());
    //            AMT = Convert.ToDouble(Txttosanction.Text.ToString() == "" ? "0" : Txttosanction.Text.ToString());

    //            if (ddlInstType.SelectedValue == "1")
    //            {
    //                Txtinstll.Text = Math.Round(Convert.ToDouble(AMT / PR), 2).ToString();
    //            }
    //            if (ddlInstType.SelectedValue == "2")
    //            {
    //                var IntRate = (double)INST / 100 / 12;
    //                var Denominator = Math.Pow((1 + IntRate), PR) - 1;
    //                Txtinstll.Text = Math.Round((IntRate + (IntRate / Denominator)) * AMT, 0).ToString();
    //            }
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    protected void ddlInstType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            EMIGetDetails();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtCustDOB_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AG = TxtCustDOB.Text.Split('/');
            int CY, BY;
            CY = DateTime.Now.Date.Year;
            BY = Convert.ToInt32(AG[2].ToString());
            TxtCustAGe.Text = (CY - BY).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtjoinDT_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AG = TxtjoinDT.Text.Split('/');
            int CY, BY;
            CY = DateTime.Now.Date.Year;
            BY = Convert.ToInt32(AG[2].ToString());
            TxtJoinAge.Text = (CY - BY).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    #region ForFetching Individual Member Data
    private void GetMemberData( ref string Index)
    {
        try
        {
            GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(Index) - 1];
            string CustNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtcustno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtcustno")).Text);
            string CustName = Convert.ToString(((TextBox)gvRow.FindControl("Txtname")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtname")).Text);
            double LoanBal = Convert.ToDouble(((TextBox)gvRow.FindControl("TxtLbal")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtLbal")).Text);
            string Memdate = Convert.ToString(((TextBox)gvRow.FindControl("TxtMemdt")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("TxtMemdt")).Text);
            string MemNo = Convert.ToInt32(((TextBox)gvRow.FindControl("Txtmemno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtmemno")).Text).ToString();
            string MobileNo = Convert.ToString(((TextBox)gvRow.FindControl("txtMobileNo")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtMobileNo")).Text);
            string Retdate = Convert.ToString(((TextBox)gvRow.FindControl("Txtretdate")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("Txtretdate")).Text);
            string Remser = Convert.ToString(((TextBox)gvRow.FindControl("Txtremser")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtremser")).Text);
            string Standby = Convert.ToString(((TextBox)gvRow.FindControl("TxtStand")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtStand")).Text);

            if (MemNo == "0")
            {
                return;
            }
            else
            {
                DT = LA.GetLoanAPPCount("ODAmount", Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), MemNo);
                if (DT.Rows.Count > 0)
                {
                    DT = LA.GetLoanAPPCount("SurityYN", Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), MemNo);
                    if (DT.Rows.Count > 0)
                    {
                        Standby = DT.Rows[0][0].ToString();
                        string custname1 = LA.GetCustomerDetails(MemNo);
                        if (custname1 == null)
                        {
                            WebMsgBox.Show("Member No not Found...!!", this.Page);
                            ((TextBox)gvRow.FindControl("Txtmemno")).Text = "";
                            Index = (Convert.ToInt32(Index) - 1).ToString();
                            TxtLtype.Focus();
                            return;
                        }

                        DT = LA.GetLoanAPPCount("SurityPeriod", Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), MemNo);
                        if (DT.Rows.Count > 0)
                        {
                            string custname = LA.GetCustomerDetails(MemNo);
                            if (custname != null)
                            {
                                string[] name = custname.Split('_');
                                CustName = (string.IsNullOrEmpty(name[0].ToString()) ? "" : name[0].ToString());
                                CustNo = (string.IsNullOrEmpty(name[1].ToString()) ? "0" : name[1].ToString());
                                ((TextBox)gvRow.FindControl("Txtname")).Text = CustName.ToString();
                                ((TextBox)gvRow.FindControl("Txtcustno")).Text = CustNo.ToString();
                            }

                            string RC = custname;
                            if (RC == null && CustNo == "0")
                            {
                                WebMsgBox.Show("Customer not found", this.Page);
                                ((TextBox)gvRow.FindControl("Txtname")).Text = "";
                                ((TextBox)gvRow.FindControl("Txtcustno")).Text = "";
                                ((TextBox)gvRow.FindControl("Txtmemno")).Text = "";
                                return;
                            }
                            string accno = LA.GetAccNo(Session["BRCD"].ToString(), CustNo);
                            LoanBal = Convert.ToDouble(BD.ClBalance(Session["BRCD"].ToString(), TxtLtype.Text, accno, Session["EntryDate"].ToString(), "ClBal").ToString().ToString());
                            ((TextBox)gvRow.FindControl("TxtLbal")).Text = string.IsNullOrEmpty(LoanBal.ToString()) ? "" : LoanBal.ToString();

                            Memdate = LA.GetDetails(Session["BRCD"].ToString(), TxtLtype.Text, CustNo);
                            if (Memdate != null)
                                ((TextBox)gvRow.FindControl("TxtMemdt")).Text = string.IsNullOrEmpty(Memdate.ToString()) ? "" : Memdate.ToString();

                            MobileNo = LA.GetMobileNo(MemNo);
                            ((TextBox)gvRow.FindControl("txtMobileNo")).Text = string.IsNullOrEmpty(MobileNo.ToString()) ? "" : MobileNo.ToString();

                            Retdate = LA.GetRetdate(Session["BRCD"].ToString(), TxtLtype.Text, CustNo);
                            if (Retdate != null)
                                ((TextBox)gvRow.FindControl("Txtretdate")).Text = string.IsNullOrEmpty(Retdate.ToString()) ? "" : Retdate.ToString();
                            if (Retdate != null && Memdate != null)
                            {
                                Remser = conn.GetMonthDiff(Memdate, Retdate);
                                ((TextBox)gvRow.FindControl("Txtremser")).Text = string.IsNullOrEmpty(Remser.ToString()) ? "" : Remser.ToString();
                            }

                            //Standby = LA.GetSurityStandby(MemNo, txtCustNo.Text);
                            ((TextBox)gvRow.FindControl("TxtStand")).Text = string.IsNullOrEmpty(Standby.ToString()) ? "" : Standby.ToString();

                            ((TextBox)gvRow.FindControl("Txtmemno")).Focus();

                            grdInsert.SelectedIndex = Convert.ToInt32(Index) + 1;
                        }
                        else
                        {
                            WebMsgBox.Show("24 Month Not Completed Surity Not Allowed...!!", this.Page);
                            ((TextBox)gvRow.FindControl("Txtmemno")).Text = "";
                            Index = (Convert.ToInt32(Index) - 1).ToString();
                            TxtLtype.Focus();
                            return;
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("Member Alredy More Than 7 Surity...!!", this.Page);
                        ((TextBox)gvRow.FindControl("Txtmemno")).Text = "";
                        Index = (Convert.ToInt32(Index) - 1).ToString();
                        TxtLtype.Focus();
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("OverDue Limit Exceed...!!", this.Page);
                    ((TextBox)gvRow.FindControl("Txtmemno")).Text = "";
                    Index = (Convert.ToInt32(Index) - 1).ToString();

                    TxtLtype.Focus();
                    return;
                }
            }

        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void EMIGetDetails()
    {
        double AMT, PR, INST;
        string EMI = "";
        int round, round1;
        try
        {
            string Para = Convert.ToString(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='LoanAPPRound'"));
            round = Convert.ToInt32(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='LnAPPRoundAmt'"));

            round1 = round / 2;

            if (Para == "Y")
            {
                PR = Convert.ToDouble(txtTotPeriod.Text.ToString() == "" ? "0" : txtTotPeriod.Text.ToString());
                INST = Convert.ToDouble(txtIntRate.Text.ToString() == "" ? "0" : txtIntRate.Text.ToString());
                AMT = Convert.ToDouble(TxtLapply.Text.ToString() == "" ? "0" : Txttosanction.Text.ToString());

                if (ddlInstType.SelectedValue == "1")
                {
                    EMI = Math.Round(Convert.ToDouble(AMT / PR), 0).ToString();
                    Txtinstll.Text = Math.Round(Convert.ToDouble((((Convert.ToInt32(EMI) + round) / round) * round)), 2).ToString();
                }
                if (ddlInstType.SelectedValue == "2")
                {
                    var IntRate = (double)INST / 100 / 12;
                    var Denominator = Math.Pow((1 + IntRate), PR) - 1;
                    Txtinstll.Text = Math.Round((IntRate + (IntRate / Denominator)) * AMT, 0).ToString();
                    //var IntRate = (double)INST / 100 / 12;
                    //var Denominator = Math.Pow((1 + IntRate), PR) - 1;
                    //EMI = Math.Round((IntRate + (IntRate / Denominator)) * AMT, 0).ToString();
                    //Txtinstll.Text = Math.Round(Convert.ToDouble((((Convert.ToInt32(EMI) + round1) / round) * round)), 2).ToString();
                }
            }
            else
            {
                PR = Convert.ToDouble(txtTotPeriod.Text.ToString() == "" ? "0" : txtTotPeriod.Text.ToString());
                INST = Convert.ToDouble(txtIntRate.Text.ToString() == "" ? "0" : txtIntRate.Text.ToString());
                AMT = Convert.ToDouble(Txttosanction.Text.ToString() == "" ? "0" : Txttosanction.Text.ToString());

                if (ddlInstType.SelectedValue == "1")
                {
                    Txtinstll.Text = Math.Round(Convert.ToDouble(AMT / PR), 2).ToString();
                }
                if (ddlInstType.SelectedValue == "2")
                {
                    var IntRate = (double)INST / 100 / 12;
                    var Denominator = Math.Pow((1 + IntRate), PR) - 1;
                    Txtinstll.Text = Math.Round((IntRate + (IntRate / Denominator)) * AMT, 0).ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion


    private void AuthoriseData()
    {
        DataTable dt = new DataTable();
        DataTable grdInsertDT = new DataTable();
        DataTable grdprevLoanDT = new DataTable();
        DataTable grdstandardDT = new DataTable();
        DataTable DT4 = new DataTable();

        string loanType = "", totalDeduction = "";

        int grdInsertResult = 0, grdStandardResult = 0, grdPreLoanRecordResult = 0;
        try
        {
            dt = LA.GetAllDetails(ViewState["LOANTYPE"].ToString(), Session["BRCD"].ToString(), txtCustNo.Text);
            if (dt.Rows.Count > 0)
            {
                loanType = ViewState["LOANTYPE"].ToString();
                TxtLtype.Text = ViewState["LOANTYPE"].ToString();

            }

            #region for Main Product Entry Authorisation
            string AppNo = LA.Appno(loanType, Session["BRCD"].ToString());
            hdnApp.Value = AppNo;
            string BondNo = LA.BondNo(loanType, Session["BRCD"].ToString());
            LA.updateapp(AppNo, loanType, Session["BRCD"].ToString());
            LA.updateBond(BondNo, loanType, Session["BRCD"].ToString());
            resultint = LA.Authorise(Session["BRCD"].ToString(), loanType, Session["MID"].ToString(), Session["CNO"].ToString(), "2", AppNo, BondNo);
            #endregion
            #region For Grid Entry Authorisation
            int j = 0;
            grdInsertDT = LA.GetSurityDetails(loanType, txtCustNo.Text, Session["BRCD"].ToString());
            #region For Authorising each entry from grdinsertDatatable
            if (grdInsertDT.Rows.Count > 0)
            {
                grdInsert.DataSource = grdInsertDT;
                grdInsert.DataBind();
                grdInsertResult = AuthoriseGrdInsertRecord(AppNo: AppNo, BondNo: BondNo, LoanType: loanType);
            }
            else
            {
                grdInsert.DataSource = null;
                grdInsert.DataBind();
            }
            #endregion
            grdprevLoanDT = LA.GetPrevLoan(loanType, txtCustNo.Text, Session["BRCD"].ToString(), "1");

            #region For Binding Previous Loan Records Grid
            if (grdprevLoanDT.Rows.Count > 0)
            {
                GrdprevLoan.DataSource = grdprevLoanDT;
                GrdprevLoan.DataBind();
                TxtSubD.Text = Convert.ToString(grdprevLoanDT.Compute("SUM(amount1)+SUM(amount)", string.Empty));
                grdPreLoanRecordResult = AuthorisegrdPreLoanRecord(AppNo: AppNo, BondNo: BondNo, loanType: loanType);
            }
            else
            {
                GrdprevLoan.DataSource = null;
                GrdprevLoan.DataBind();
                TxtSubD.Text = "0";
            }
            #endregion

            grdstandardDT = LA.GetPrevLoan(TxtLtype.Text, txtCustNo.Text, Session["BRCD"].ToString(), "2");

            #region For Binding Standard Grid View
            if (grdstandardDT.Rows.Count > 0)
            {
                grdstandard.DataSource = grdstandardDT;
                grdstandard.DataBind();
                TxtSubS.Text = Convert.ToString(grdstandardDT.Compute("SUM(TxtSDeduction)", string.Empty));
                grdStandardResult = AuthorisegrdStandardRecords(AppNo: AppNo, BondNo: BondNo, loanType: loanType);

            }
            else
            {
                grdstandard.DataSource = null;
                grdstandard.DataBind();
                TxtSubS.Text = "0";
            }
            #endregion

            DT4 = LA.GetPrevLoan(TxtLtype.Text, txtCustNo.Text, Session["BRCD"].ToString(), "4");
            if (DT4.Rows.Count > 0)
            {
                totalDeduction = (string.IsNullOrEmpty(DT4.Rows[0]["AMOUNT"].ToString()) ? "0" : DT4.Rows[0]["AMOUNT"].ToString());
            }
            else
            {
                totalDeduction = "0";
            }

            if (resultint > 0 || resC > 0 || resP > 0 || resS > 0)
            {
                Label1.Text = "Data Authorised Successfully with Application No: '" + AppNo + "'...!!";
                string Modal_Flag = "DivAddMore";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#" + Modal_Flag + "').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);

                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "F7_Auth _" + AppNo + "_" + TxtLtype.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
            #endregion

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    private int AuthoriseGrdInsertRecord(string AppNo, string BondNo, string LoanType)
    {
        try
        {
            int j = 0;
            foreach (GridViewRow gvRow in this.grdInsert.Rows)
            {
                string SrNumber = Convert.ToInt32(((TextBox)gvRow.FindControl("txtSrNo")).Text).ToString();
                string CustNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtcustno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtcustno")).Text);
                string Name = Convert.ToString(((TextBox)gvRow.FindControl("Txtname")).Text == "" ? " " : ((TextBox)gvRow.FindControl("Txtname")).Text);
                string LoanBal = Convert.ToString(((TextBox)gvRow.FindControl("TxtLbal")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtLbal")).Text);
                string Memdate = Convert.ToString(((TextBox)gvRow.FindControl("TxtMemdt")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("TxtMemdt")).Text);
                string MemNo = Convert.ToInt32(((TextBox)gvRow.FindControl("Txtmemno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtmemno")).Text).ToString();
                string Retdate = Convert.ToString(((TextBox)gvRow.FindControl("Txtretdate")).Text == "" ? "1900-01-01" : ((TextBox)gvRow.FindControl("Txtretdate")).Text);
                string Remser = Convert.ToString(((TextBox)gvRow.FindControl("Txtremser")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtremser")).Text);
                string Standby = Convert.ToString(((TextBox)gvRow.FindControl("TxtStand")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtStand")).Text);
                string MobileNo = Convert.ToString(((TextBox)gvRow.FindControl("txtMobileNo")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtMobileNo")).Text);
                if (CustNo != "0")
                {
                    resC = LA.GetCustAut(Session["BRCD"].ToString(), Session["CNO"].ToString(), Session["MID"].ToString(), LoanType, "2", AppNo, BondNo, MemNo);
                }
                j = j + 1;
            }
        }
        catch (Exception ex)
        {
            resC = 0;
            ExceptionLogging.SendErrorToText(ex);
        }
        return resC;

    }


    private int AuthorisegrdPreLoanRecord(string AppNo, string BondNo, string loanType)
    {
        try
        {
            int i = 0;
            foreach (GridViewRow gvRow in this.GrdprevLoan.Rows)
            {
                string SPNumber = Convert.ToInt32(((TextBox)gvRow.FindControl("txtPSrNo")).Text).ToString();
                string PPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("Txtprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtprcd")).Text);
                string Bal = Convert.ToString(((TextBox)gvRow.FindControl("Txtbal")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtbal")).Text);
                string Int = Convert.ToString(((TextBox)gvRow.FindControl("TxtInt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtInt")).Text);
                string DedP = Convert.ToString(((TextBox)gvRow.FindControl("TxtDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtDeduction")).Text);
                string Irno = LA.GetIrNo(Session["BRCD"].ToString(), PPrdNo);
                if (PPrdNo != "0")
                {
                    resP = LA.GetPrevAut(Session["BRCD"].ToString(), Session["CNO"].ToString(), Session["MID"].ToString(), PPrdNo, "2", AppNo, BondNo, loanType, "1");
                    resP = LA.GetPrevAut(Session["BRCD"].ToString(), Session["CNO"].ToString(), Session["MID"].ToString(), Irno, "2", AppNo, BondNo, loanType, "1");
                }
                i = i + 1;
            }
        }
        catch (Exception ex)
        {
            resP = 0;
            ExceptionLogging.SendErrorToText(ex);
        }
        return resP;
    }

    private int AuthorisegrdStandardRecords(string AppNo, string BondNo, string loanType)
    {
        try
        {
            int k = 0;
            foreach (GridViewRow gvRow in this.grdstandard.Rows)
            {
                string SSNumber = Convert.ToInt32(((TextBox)gvRow.FindControl("txtSSrNo")).Text).ToString();
                string SPrdNo = Convert.ToString(((TextBox)gvRow.FindControl("TxtSprcd")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSprcd")).Text);
                string DedS = Convert.ToString(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                if (SPrdNo != "0")
                {
                    resS = LA.GetStAut(Session["BRCD"].ToString(), Session["CNO"].ToString(), Session["MID"].ToString(), SPrdNo, "2", AppNo, BondNo, loanType, "2");
                }
                k = k + 1;
            }
        }
        catch (Exception ex)
        {
            resS = 0;
            ExceptionLogging.SendErrorToText(ex);
        }
        return resS;
    }
}