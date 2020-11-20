using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class FrmAVS5040 : System.Web.UI.Page
{
    ClsRetirmentVouchar RV = new ClsRetirmentVouchar();
    ClsAVS5040 CLS = new ClsAVS5040();
    DataTable DT = new DataTable();
    double CrTotal = 0, DRTotal = 0;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsSBIntCalculation SB = new ClsSBIntCalculation();

    string FL = "";
    double total1 = 0, total2 = 0;
    double interest = 0, intRec = 0;
    int NetPaid = 0;

    #region ON page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            Autoprd4.ContextKey = Session["BRCD"].ToString();
            autoglname.ContextKey = Session["BRCD"].ToString();
            string Brname = RV.GetBranchName(Session["BRCD"].ToString());
            txtbrname.Text = Brname;
            txtappdate.Text = Session["ENTRYDATE"].ToString();
            EmptyGridBind();
            EmptyGridBind1();
            DivTransfer.Visible = false;
            TXtCustNo.Focus();

            if (!string.IsNullOrEmpty(Request.QueryString["CUSTNO"]))
            {
                TXtCustNo.Text = Request.QueryString["CUSTNO"].ToString();
            }
            getdatanew();

            TxtChequeDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    #endregion

    #region ON method call
    public void EmptyGridBind()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[6] { new DataColumn("SRNO", typeof(string)),
                            new DataColumn("SUBGLCODE", typeof(string)),new DataColumn("GLNAME", typeof(string)),new DataColumn("ACCNO", typeof(int)),
                            new DataColumn("AMOUNT", typeof(string)),new DataColumn("INTAMT", typeof(string))});

        DataRow dr;
        for (int i = dt.Rows.Count; i < 6; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
        }
        dt.AcceptChanges();
        grdstandard.DataSource = dt;
        grdstandard.DataBind();

    }
    public void ClearAll()
    {
        EmptyGridBind1();
        EmptyGridBind();
        txtappdate.Text = "";
        TXtCustNo.Text = "";
        txtcustName.Text = "";
        txtResignvouchar.Text = "";
        txtdatediff.Text = "";
        txtResignReason.Text = "";
        txtMglcode.Text = "";
        txtMsub.Text = "";
        txtMAount.Text = "";
        ddlPayMode.SelectedValue = "0";
        TxtPayAmt.Text = "";
        TxtPType.Text = "";
        TxtPTName.Text = "";
        TxtTAccNo.Text = "";
        TxtTAName.Text = "";
        TxtChequeNo.Text = "";
        TxtChequeDate.Text = "";

    }
    public void EmptyGridBind1()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[6] { new DataColumn("SRNO", typeof(string)),
                            new DataColumn("SUBGLCODE", typeof(string)), new DataColumn("GLNAME", typeof(string)),new DataColumn("ACCNO", typeof(int)),
                            new DataColumn("AMOUNT", typeof(string)), new DataColumn("INTAMT", typeof(string)) });

        DataRow dr;
        for (int i = dt.Rows.Count; i < 5; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
        }
        dt.AcceptChanges();
        grdRecivable.DataSource = dt;
        grdRecivable.DataBind();

    }
    public void getdatanew()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = RV.GetCustNAme(TXtCustNo.Text, Session["BRCD"].ToString());
            txtcustName.Text = DT.Rows[0]["custname"].ToString();
            txtdatediff.Text = DT.Rows[0]["OPENINGDATE"].ToString();
            BindGrid();
            BindGrid1();

            GetPayble();
            RV.GetAccountInfo(GrdInDirectLiab, Session["BRCD"].ToString(), TXtCustNo.Text, Session["EntryDate"].ToString(), "SUR");
            GetMemberWalfare();
            GetRecivable();
            TxtPayAmt.Text = Convert.ToString((Convert.ToDouble(TxtSubS.Text) - Convert.ToDouble(txtRsub.Text)) + Convert.ToDouble(txtMAount.Text));
            if (Convert.ToDouble(TxtPayAmt.Text) < 0.00)
            {
                DivTransfer.Visible = false;
                //   ddlPayMode.Enabled = false;
                btnSubmit.Enabled = false;
            }

            txtResignvouchar.Focus();



        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    public void GetMemberWalfare()
    {
        int WalfareAmt = 0; int Diff = 0;
        DataTable DT = new DataTable();
        string Para = "", Year = "";
        string Minpayble = "";
        //txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
        //txtMsub.Text = DT.Rows[0]["placc"].ToString();
        //string Minpayble=DT.Rows[0]["CHARGES"].ToString();
        if (Session["BNKCDE"].ToString() == "1008" || Session["BNKCDE"].ToString() == "1041")//Dhanya Shetty//17/08/2017- This condition is applied only for tzmp- As per Ambika Mam's requirement
        {
            Year = RV.GetYear(TXtCustNo.Text, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString());
        }
        else
        {
            Year = "0";
        }



        //if (Convert.ToInt32(Year) < 15)
        //{
        //    WalfareAmt = Convert.ToInt32(Minpayble);
        //}
        //else if (Convert.ToInt32(Year) > 15)
        //{
        //    Diff = Convert.ToInt32(Year) - 15;
        //    WalfareAmt = Convert.ToInt32(Minpayble) + Diff * 700;
        //}
        //if (Convert.ToInt32(Year) < 1)
        //{
        //    Para = "1";
        //    DT = RV.GetWelfareInfo(Para);
        //    txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
        //    txtMsub.Text = DT.Rows[0]["placc"].ToString();
        //     Minpayble = DT.Rows[0]["CHARGES"].ToString();

        //}
        //else if (Convert.ToInt32(Year) > 1 && Convert.ToInt32(Year) < 2)
        //{
        //    Para = "2";
        //    DT = RV.GetWelfareInfo(Para);
        //    txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
        //    txtMsub.Text = DT.Rows[0]["placc"].ToString();
        //    Minpayble = DT.Rows[0]["CHARGES"].ToString();
        //}
        //else if (Convert.ToInt32(Year) > 2 && Convert.ToInt32(Year) < 3)
        //{
        //    Para = "3";
        //    DT = RV.GetWelfareInfo(Para);
        //    txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
        //    txtMsub.Text = DT.Rows[0]["placc"].ToString();
        //    Minpayble = DT.Rows[0]["CHARGES"].ToString();
        //}
        //else if (Convert.ToInt32(Year) > 3 && Convert.ToInt32(Year) < 4)
        //{
        //    Para = "4";
        //    DT = RV.GetWelfareInfo(Para);
        //    txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
        //    txtMsub.Text = DT.Rows[0]["placc"].ToString();
        //    Minpayble = DT.Rows[0]["CHARGES"].ToString();
        //}
        //else if (Convert.ToInt32(Year) > 4 && Convert.ToInt32(Year) < 5)
        //{
        //    Para = "5";
        //    DT = RV.GetWelfareInfo(Para);
        //    txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
        //    txtMsub.Text = DT.Rows[0]["placc"].ToString();
        //    Minpayble = DT.Rows[0]["CHARGES"].ToString();
        //}
        //else if (Convert.ToInt32(Year) > 5 && Convert.ToInt32(Year) < 6)
        //{
        //    Para = "6";
        //    DT = RV.GetWelfareInfo(Para);
        //    txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
        //    txtMsub.Text = DT.Rows[0]["placc"].ToString();
        //    Minpayble = DT.Rows[0]["CHARGES"].ToString();
        //}
        //else if (Convert.ToInt32(Year) > 6 && Convert.ToInt32(Year) < 7)
        //{
        //    Para = "7";
        //    DT = RV.GetWelfareInfo(Para);
        //    txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
        //    txtMsub.Text = DT.Rows[0]["placc"].ToString();
        //    Minpayble = DT.Rows[0]["CHARGES"].ToString();
        //}
        //else if (Convert.ToInt32(Year) > 7 && Convert.ToInt32(Year) < 8)
        //{
        //    Para = "8";
        //    DT = RV.GetWelfareInfo(Para);
        //    txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
        //    txtMsub.Text = DT.Rows[0]["placc"].ToString();
        //    Minpayble = DT.Rows[0]["CHARGES"].ToString();
        //}
        //else if (Convert.ToInt32(Year) > 8 && Convert.ToInt32(Year) < 9)
        //{
        //    Para = "9";
        //    DT = RV.GetWelfareInfo(Para);
        //    txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
        //    txtMsub.Text = DT.Rows[0]["placc"].ToString();
        //    Minpayble = DT.Rows[0]["CHARGES"].ToString();
        //}
        //else if (Convert.ToInt32(Year) > 9 && Convert.ToInt32(Year) < 10)
        //{
        //    Para = "10";
        //    DT = RV.GetWelfareInfo(Para);
        //    txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
        //    txtMsub.Text = DT.Rows[0]["placc"].ToString();
        //    Minpayble = DT.Rows[0]["CHARGES"].ToString();
        //}
        //else if (Convert.ToInt32(Year) >= 10 && Convert.ToInt32(Year) < 15)
        //{
        //    Para = "10";
        //    DT = RV.GetWelfareInfo(Para);
        //    txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
        //    txtMsub.Text = DT.Rows[0]["placc"].ToString();
        //    Minpayble = DT.Rows[0]["CHARGES"].ToString();
        //    Diff = Convert.ToInt32(Year) - 10;
        //    WalfareAmt = Convert.ToInt32(Minpayble) + Diff * 300;

        //}
        if (Convert.ToInt32(Year) > 15)
        {
            Para = "1111";
            DT = RV.GetWelfareInfo(Para);

            if (DT.Rows.Count > 0)
            {
                txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
                txtMsub.Text = DT.Rows[0]["placc"].ToString();
                Minpayble = DT.Rows[0]["CHARGES"].ToString();
                txtMAount.Text = Minpayble;
            }

            else
            {
                WebMsgBox.Show("No Records Found for Minimum balance in Charges Master", this.Page);
                return;
            }
            Diff = Convert.ToInt32(Year) - 15;
            WalfareAmt = Convert.ToInt32(Minpayble) + Diff * 700;
        }
        else
        {
            Para = "1111";
            DT = RV.GetWelfareInfo(Para);
            if (DT.Rows.Count > 0)
            {
                txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
                txtMsub.Text = DT.Rows[0]["placc"].ToString();
                Minpayble = DT.Rows[0]["CHARGES"].ToString();
                txtMAount.Text = Minpayble;

            }
            WalfareAmt = 0;
        }
        // txtMAount.Text = WalfareAmt.ToString();

    }
    public void BindGrid()
    {
        try
        {
            RV.bindgrid(TXtCustNo.Text, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), grdstandard);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    public void BindGrid1()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = RV.bindgrid1(TXtCustNo.Text, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString());
            if (dt.Rows.Count > 0)
            {
                grdRecivable.DataSource = dt;
                grdRecivable.DataBind();
            }
            else
            {
                grdRecivable.DataSource = null;
                grdRecivable.DataBind();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void GetPayble()
    {
        foreach (GridViewRow gvRow in this.grdstandard.Rows)
        {
            int j = 0;
            String Custno = TXtCustNo.Text;
            string srno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSrno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSrno")).Text);
            if (srno != "0")
            {
                string Amount = Convert.ToString(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                string INTAMT = Convert.ToString(((TextBox)gvRow.FindControl("txtIntAmt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtIntAmt")).Text);
                CrTotal = Convert.ToDouble(Amount) + Convert.ToDouble(INTAMT) + CrTotal;
                j = j + 1;
            }

            else
            {

            }

        }
        TxtSubS.Text = CrTotal.ToString();

    }
    public void GetRecivable()
    {
        foreach (GridViewRow gvRow in this.grdRecivable.Rows)
        {
            int j = 0;
            String Custno = TXtCustNo.Text;
            // grdRecivable.Rows.Count= null; 

            string srno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSrno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSrno")).Text);
            if (srno != "0")
            {
                string Amount = Convert.ToString(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                string LOANINTAMT = Convert.ToString(((TextBox)gvRow.FindControl("txtLoanInt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtLoanInt")).Text);
                double Amount1 = System.Math.Abs(Convert.ToDouble(Amount));
                double LOANINTAMT1 = System.Math.Abs(Convert.ToDouble(LOANINTAMT));
                DRTotal = Convert.ToDouble(Amount1) + Convert.ToDouble(LOANINTAMT1) + DRTotal;
                j = j + 1;
            }
            else
            {
            }


        }
        txtRsub.Text = DRTotal.ToString();

    }
    #endregion

    #region ON TEXT CHANGE
    protected void TXtCustNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable DT = new DataTable();
            DT = RV.GetCustNAme(TXtCustNo.Text, Session["BRCD"].ToString());
            txtcustName.Text = DT.Rows[0]["custname"].ToString();
            txtdatediff.Text = DT.Rows[0]["OPENINGDATE"].ToString();
            BindGrid();
            BindGrid1();
            GetRecivable();

            RV.GetAccountInfo(GrdInDirectLiab, Session["BRCD"].ToString(), TXtCustNo.Text, Session["EntryDate"].ToString(), "SUR");
            GetMemberWalfare();
            GetPayble();
            TxtPayAmt.Text = Convert.ToString((Convert.ToDouble(TxtSubS.Text) - Convert.ToDouble(txtRsub.Text)) + Convert.ToDouble(txtMAount.Text));
            if (Convert.ToDouble(TxtPayAmt.Text) < 0.00)
            {
                DivTransfer.Visible = false;
                ddlPayMode.Enabled = false;
                btnSubmit.Enabled = false;
            }

            txtResignvouchar.Focus();



        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtcustName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtcustName.Text;
            string[] custnob = CUNAME.Split('_');

            if (custnob.Length > 1)
            {
                txtcustName.Text = custnob[0].ToString();
                TXtCustNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                DataTable DT = new DataTable();
                DT = RV.GetCustNAme(TXtCustNo.Text, Session["BRCD"].ToString());
                // txtcustName.Text = DT.Rows[0]["custname"].ToString();
                txtdatediff.Text = DT.Rows[0]["OPENINGDATE"].ToString();
                BindGrid();
                BindGrid1();
                GetRecivable();
                GetPayble();
                RV.GetAccountInfo(GrdInDirectLiab, Session["BRCD"].ToString(), TXtCustNo.Text, Session["EntryDate"].ToString(), "SUR");
                GetMemberWalfare();
                TxtPayAmt.Text = (Convert.ToDouble(TxtSubS.Text) + Convert.ToDouble(txtMAount.Text) - Convert.ToDouble(txtRsub.Text)).ToString();
                if (Convert.ToDouble(TxtPayAmt.Text) < 0.00)
                {
                    DivTransfer.Visible = false;
                    ddlPayMode.Enabled = false;
                    btnSubmit.Enabled = false;
                }
                txtResignvouchar.Focus();

            }

            else
            {
                if (txtcustName.Text != "")
                {
                    lblMessage.Text = "Customer Name Is Already Exist......!!";
                    ModalPopup.Show(this.Page);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtIntAmt_TextChanged(object sender, EventArgs e)
    {
        string DedP = "";
        double Result = 0;
        try
        {
            foreach (GridViewRow gvRow in this.grdstandard.Rows)
            {
                DedP = Convert.ToString(((CheckBox)gvRow.FindControl("Agentcollect")).Checked);
                if (DedP == "True")
                {
                    double TOtal = Convert.ToDouble(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                    double intrest = Convert.ToDouble(((TextBox)gvRow.FindControl("txtIntAmt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtIntAmt")).Text);
                    Result += Convert.ToDouble(TOtal + intrest);
                }

            }
            TxtSubS.Text = Result.ToString();
            TxtPayAmt.Text = Convert.ToString((Convert.ToDouble(TxtSubS.Text) - Convert.ToDouble(txtRsub.Text)) + Convert.ToDouble(txtMAount.Text));


        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }



    private void GetPayableTotalAmount()
    {
        string DedP = "";
        double Result = 0;
        try
        {
            foreach (GridViewRow gvRow in this.grdstandard.Rows)
            {
                DedP = Convert.ToString(((CheckBox)gvRow.FindControl("Agentcollect")).Checked);
                if (DedP == "True")
                {
                    double TOtal = Convert.ToDouble(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                    double intrest = Convert.ToDouble(((TextBox)gvRow.FindControl("txtIntAmt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtIntAmt")).Text);
                    Result += Convert.ToDouble(TOtal + intrest);
                }

            }
            TxtSubS.Text = Result.ToString();
            TxtPayAmt.Text = Convert.ToString((Convert.ToDouble(TxtSubS.Text) - Convert.ToDouble(txtRsub.Text)) + Convert.ToDouble(txtMAount.Text));    


        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Agentcollect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            double Result = 0;

            string DedP = "";
            string DedI = "";
            foreach (GridViewRow gvRow in this.grdstandard.Rows)
            {
                DedP = Convert.ToString(((CheckBox)gvRow.FindControl("Agentcollect")).Checked);
                if (DedP == "True")
                {
                    double TOtal = Convert.ToDouble(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                    double intrest = Convert.ToDouble(((TextBox)gvRow.FindControl("txtIntAmt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtIntAmt")).Text);
                    TextBox price = (TextBox)gvRow.FindControl("TxtSDeduction");
                    Result += (TOtal + intrest);
                }

            }
            TxtSubS.Text = Result.ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Agent_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            double Result = 0;
            string DedP = "";
            string DedI = "";
            foreach (GridViewRow gvRow in this.grdRecivable.Rows)
            {
                DedP = Convert.ToString(((CheckBox)gvRow.FindControl("Agent")).Checked);
                if (DedP == "True")
                {
                    double TOtal = Convert.ToDouble(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                    double intrest = Convert.ToDouble(((TextBox)gvRow.FindControl("txtLoanInt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtLoanInt")).Text);
                    TextBox price = (TextBox)gvRow.FindControl("TxtSDeduction");
                    Result += (TOtal + intrest);
                }

            }
            txtRsub.Text = Result.ToString();
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
            foreach (GridViewRow gvRow in this.grdstandard.Rows)
            {

                int TOtal = Convert.ToInt32(((TextBox)gvRow.FindControl("txtsubglcode")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtsubglcode")).Text);
                // int intrest = Convert.ToInt32(((TextBox)gvRow.FindControl("txtLoanInt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtLoanInt")).Text);
                // TextBox price = (TextBox)gvRow.FindControl("TxtSDeduction");
                string GLname = RV.getglname(Session["BRCD"].ToString(), TOtal.ToString());
                string AccNO = RV.getAccNO(Session["BRCD"].ToString(), TOtal.ToString(), TXtCustNo.Text);
                if (GLname != null)
                {
                    ((TextBox)gvRow.FindControl("txtGLNAME")).Text = GLname.ToString();
                    if (AccNO == null)
                        ((TextBox)gvRow.FindControl("TxtSname")).Text = TXtCustNo.Text;
                    else
                        ((TextBox)gvRow.FindControl("TxtSname")).Text = AccNO.ToString();
                }
                //   txtRsub.Text = Result.ToString();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void txtsubglcode_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvRow in this.grdRecivable.Rows)
            {

                int TOtal = Convert.ToInt32(((TextBox)gvRow.FindControl("txtsubglcode")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtsubglcode")).Text);
                // int intrest = Convert.ToInt32(((TextBox)gvRow.FindControl("txtLoanInt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtLoanInt")).Text);
                // TextBox price = (TextBox)gvRow.FindControl("TxtSDeduction");
                string GLname = RV.getglname(Session["BRCD"].ToString(), TOtal.ToString());
                string AccNO = RV.getAccNO(Session["BRCD"].ToString(), TOtal.ToString(), TXtCustNo.Text);
                if (GLname != null)
                {
                    ((TextBox)gvRow.FindControl("txtGLNAME")).Text = GLname.ToString();
                    ((TextBox)gvRow.FindControl("TxtSname")).Text = AccNO == null ? TXtCustNo.Text : AccNO.ToString();
                }
                //   txtRsub.Text = Result.ToString();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtSDeduction_TextChanged(object sender, EventArgs e)
    {
        string DedP = "";
        double Result = 0;
        try
        {
            foreach (GridViewRow gvRow in this.grdstandard.Rows)
            {
                DedP = Convert.ToString(((CheckBox)gvRow.FindControl("Agentcollect")).Checked);
                if (DedP == "True")
                {
                    double TOtal = Convert.ToInt32(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                    double intrest = Convert.ToInt32(((TextBox)gvRow.FindControl("txtIntAmt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtIntAmt")).Text);
                    Result += (TOtal + intrest);
                }

            }
            TxtSubS.Text = Result.ToString();
            TxtPayAmt.Text = Convert.ToString((Convert.ToDouble(TxtSubS.Text) - Convert.ToDouble(txtRsub.Text)) + Convert.ToDouble(txtMAount.Text));
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtLoanInt_TextChanged(object sender, EventArgs e)
    {
        string DedP = "";
        double Result = 0;
        try
        {
            foreach (GridViewRow gvRow in this.grdRecivable.Rows)
            {
                DedP = Convert.ToString(((CheckBox)gvRow.FindControl("Agent")).Checked);
                if (DedP == "True")
                {
                    double TOtal = Convert.ToDouble(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                    double intrest = Convert.ToDouble(((TextBox)gvRow.FindControl("txtLoanInt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtLoanInt")).Text);
                    Result += Convert.ToDouble(TOtal + intrest);
                }

            }
            txtRsub.Text = Result.ToString();
            TxtPayAmt.Text = Convert.ToString((Convert.ToDouble(TxtSubS.Text) - Convert.ToDouble(txtRsub.Text)) + Convert.ToDouble(txtMAount.Text));



        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GrdInDirectLiab_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GrdInDirectLiab_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPayMode.SelectedValue == "1")
            {
                TxtPayAmt.Text = (Convert.ToDouble(TxtSubS.Text) - Convert.ToDouble(txtRsub.Text) + Convert.ToDouble(txtMAount.Text)).ToString();
                if (Convert.ToDouble(TxtPayAmt.Text) < 0.00)
                {
                    DivTransfer.Visible = false;
                    btnSubmit.Enabled = false;
                }
                else
                {
                    DivTransfer.Visible = false;
                    btnSubmit.Enabled = true;
                }
            }
            else if (ddlPayMode.SelectedValue == "2")
            {
                Transfer2.Visible = false;
                DivTransfer.Visible = true;
                TxtPayAmt.Text = (Convert.ToDouble(TxtSubS.Text) - Convert.ToDouble(txtRsub.Text) + Convert.ToDouble(txtMAount.Text)).ToString();
                if (Convert.ToDouble(TxtPayAmt.Text) < 0.00)
                {
                    Transfer2.Visible = false;
                    DivTransfer.Visible = false;
                    btnSubmit.Enabled = false;
                }
                else
                {
                    Transfer3.Visible = false;
                    btnSubmit.Enabled = true;
                    DivTransfer.Visible = true;
                    Transfer2.Visible = true;
                    TxtPType.Focus();
                }
            }
            else if (ddlPayMode.SelectedValue == "3")
            {
                DivTransfer.Visible = true;
                Transfer2.Visible = true;
                TxtPayAmt.Text = (Convert.ToDouble(TxtSubS.Text) - Convert.ToDouble(txtRsub.Text) + Convert.ToDouble(txtMAount.Text)).ToString();
                if (Convert.ToDouble(TxtPayAmt.Text) < 0.00)
                {
                    DivTransfer.Visible = false;
                    btnSubmit.Enabled = false;
                }
                else
                {
                    btnSubmit.Enabled = true;
                    DivTransfer.Visible = true;
                    Transfer2.Visible = true;
                    Transfer3.Visible = true;
                    TxtPType.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtPType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ProdCode();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtPTName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = TxtPTName.Text.Split('_');
            if (custnob.Length > 1)
            {
                TxtPType.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                ProdCode();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ProdCode()
    {
        DT = new DataTable();
        try
        {
            DT = CLS.GetProdDetails(Session["BRCD"].ToString(), TxtPType.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                if (Convert.ToString(DT.Rows[0]["UnOperate"].ToString()) != "3")
                {
                    ViewState["TGlCode"] = Convert.ToString(DT.Rows[0]["GlCode"].ToString()).ToString();
                    TxtPType.Text = Convert.ToString(DT.Rows[0]["SubGlCode"].ToString()).ToString();
                    TxtPTName.Text = Convert.ToString(DT.Rows[0]["GlName"].ToString()).ToString();

                    if (Convert.ToString(DT.Rows[0]["IntAccYN"].ToString()) != "Y")
                    {
                        TxtTAccNo.Text = TxtPType.Text.ToString();
                        TxtTAName.Text = TxtPTName.Text.ToString();
                        return;
                    }

                    TxtTAccNo.Text = "";
                    TxtTAName.Text = "";
                    TxtTAccNo.Focus();

                    TxtTAccNo.Focus();
                }
                else
                {
                    TxtPType.Text = "";
                    TxtPTName.Text = "";
                    TxtPType.Focus();
                    WebMsgBox.Show("Product is not operating ...!!", this.Page);
                    return;
                }
            }
            else
            {
                TxtPType.Text = "";
                TxtPTName.Text = "";
                TxtPType.Focus();
                WebMsgBox.Show("Enter valid product code ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtTAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AccountNo();
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtTAName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = TxtTAName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                TxtTAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                AccountNo();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void AccountNo()
    {
        DT = new DataTable();
        try
        {
            DT = CLS.GetAccDetails(Session["BRCD"].ToString(), TxtPType.Text.ToString(), TxtTAccNo.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                if (Convert.ToString(DT.Rows[0]["Stage"].ToString()) == "1003")
                {
                    if (Convert.ToString(DT.Rows[0]["Acc_Status"].ToString()) == "3")
                    {
                        TxtTAccNo.Text = "";
                        TxtTAName.Text = "";
                        TxtTAccNo.Focus();
                        WebMsgBox.Show("Account is closed ...!!", this.Page);
                        return;
                    }
                    else if (Convert.ToString(DT.Rows[0]["Acc_Status"].ToString()) == "5")
                    {
                        TxtTAccNo.Text = "";
                        TxtTAName.Text = "";
                        TxtTAccNo.Focus();
                        WebMsgBox.Show("Account is credit freeze ...!!", this.Page);
                        return;
                    }
                    else
                    {
                        TxtTAccNo.Text = Convert.ToString(DT.Rows[0]["AccNo"].ToString()).ToString();
                        TxtTAName.Text = Convert.ToString(DT.Rows[0]["CustName"].ToString()).ToString();

                        if (ddlPayMode.SelectedValue == "2")
                            btnSubmit.Focus();
                        else if (ddlPayMode.SelectedValue == "3")
                            TxtChequeNo.Focus();
                    }
                }
                else
                {
                    TxtTAccNo.Text = "";
                    TxtTAName.Text = "";
                    TxtTAccNo.Focus();
                    WebMsgBox.Show("Sorry account not authorise ...!!", this.Page);
                    return;
                }
            }
            else
            {
                TxtTAccNo.Text = "";
                TxtTAName.Text = "";
                TxtTAccNo.Focus();
                WebMsgBox.Show("Enter valid account no ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion
    #region on Click
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string ST = "", ST2 = "";
            int j = 0;
            string DedP = "";
            int Result1 = 0;
            int RN1 = 0, RN2 = 0, RN3 = 0, RN4 = 0, RN5 = 0, RN12 = 0, RN11 = 0, RN9 = 0, RN10 = 0, RN8 = 0;
            ST = RV.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
            ST2 = RV.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
            if (ddlPayMode.SelectedValue == "2")
            {
                foreach (GridViewRow gvRow in this.grdstandard.Rows)
                {
                    String Custno = TXtCustNo.Text;
                    string srno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSrno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSrno")).Text);
                    if (srno != "0")
                    {
                        DedP = Convert.ToString(((CheckBox)gvRow.FindControl("Agentcollect")).Checked);
                        if (DedP == "True")
                        {

                            string PrdCd = Convert.ToString(((TextBox)gvRow.FindControl("txtsubglcode")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtsubglcode")).Text);
                            string Accno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSname")).Text == "" ? " " : ((TextBox)gvRow.FindControl("TxtSname")).Text);
                            string Amount = Convert.ToString(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                            CrTotal = Convert.ToDouble(Amount) + CrTotal;
                            //string glcode = RV.GetGlcode(Session["BRCD"].ToString(), PrdCd, Accno);
                            string glcode = RV.GetGlcodePL(Session["BRCD"].ToString(), PrdCd);//Amruta 19/05/2018
                            RN1 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, PrdCd, Accno, "Retirement Vouchar trf/" + PrdCd, "", Amount.ToString(), "2", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                            Result1 = RV.CloseAcc(PrdCd, Session["BRCD"].ToString(), Accno, Session["EntryDate"].ToString());
                            string INTAMT = Convert.ToString(((TextBox)gvRow.FindControl("txtIntAmt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtIntAmt")).Text);
                            if (Convert.ToInt32(INTAMT) > 0)
                            {
                                string IR = RV.GetDepositIR(Session["BRCD"].ToString(), PrdCd);
                                //string glcodeIR = RV.GetGlcode(Session["BRCD"].ToString(), IR, Accno);
                                string glcodeIR = RV.GetGlcodePL(Session["BRCD"].ToString(), IR);//Amruta 19/05/2018
                                RN12 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcodeIR, IR, Accno, "Retirement Vouchar trf INT/" + PrdCd, "", INTAMT.ToString(), "2", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                                //string glcode1 = RV.GetGlcode(Session["BRCD"].ToString(), TxtPType.Text, TxtTAccNo.Text);
                                string glcode1 = RV.GetGlcodePL(Session["BRCD"].ToString(), TxtPType.Text);//Amruta 19/05/2018
                                // RN11 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode1, TxtPType.Text, TxtTAccNo.Text, "Retirement Vouchar trf/" + TxtPType.Text, "", INTAMT.ToString(), "1", "7", "INTR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                            }

                            j = j + 1;
                        }
                    }

                    else
                    {

                    }

                }
                DT = RV.GetWelfareInfo1();
                txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
                txtMsub.Text = DT.Rows[0]["placc"].ToString();
                RN5 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["glcode"].ToString(), DT.Rows[0]["placc"].ToString(), "0", "Retirement Vouchar trf/" + TxtPType.Text, "", txtMAount.Text, "2", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                CrTotal = CrTotal + Convert.ToInt32(txtMAount.Text);
                //string glcode2 = RV.GetGlcode(Session["BRCD"].ToString(), TxtPType.Text, TxtTAccNo.Text);
                string glcode2 = RV.GetGlcodePL(Session["BRCD"].ToString(), TxtPType.Text);//Amruta 19/05/2018
                //RN3 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode2, TxtPType.Text, TxtTAccNo.Text, "Retirement Vouchar trf/" + TxtPType.Text, "", CrTotal.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");

                foreach (GridViewRow gvRow in this.grdRecivable.Rows)
                {

                    String Custno = TXtCustNo.Text;
                    string srno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSrno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSrno")).Text);
                    if (srno != "0")
                    {
                        DedP = Convert.ToString(((CheckBox)gvRow.FindControl("Agent")).Checked);
                        if (DedP == "True")
                        {
                            string PrdCd = Convert.ToString(((TextBox)gvRow.FindControl("txtsubglcode")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtsubglcode")).Text);
                            string Accno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSname")).Text == "" ? " " : ((TextBox)gvRow.FindControl("TxtSname")).Text);
                            string Amount = Convert.ToString(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                            double Amount1 = System.Math.Abs(Convert.ToDouble(Amount));
                            //string glcode = RV.GetGlcode(Session["BRCD"].ToString(), PrdCd, Accno);
                            string glcode = RV.GetGlcodePL(Session["BRCD"].ToString(), PrdCd);//Amruta 19/05/2018
                            DRTotal = Convert.ToDouble(Amount1) + DRTotal;
                            RN2 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, PrdCd, Accno, "Retirement Vouchar trf/" + PrdCd, "", Amount1.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                            string LOANINTAMT = Convert.ToString(((TextBox)gvRow.FindControl("txtLoanInt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtLoanInt")).Text);
                            if (Convert.ToInt32(LOANINTAMT) > 0)
                            {
                                string IP = RV.GetDepositIR(Session["BRCD"].ToString(), PrdCd);
                                string glcodeIR = RV.GetGlcodeIR(Session["BRCD"].ToString(), IP);
                                RN10 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcodeIR, IP, Accno, "Retirement Vouchar trf INT/" + PrdCd, "", LOANINTAMT.ToString(), "2", "11", "TR-INT", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                                RN9 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcodeIR, IP, Accno, "Retirement Vouchar trf INT/" + PrdCd, "", LOANINTAMT.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                                string PlaccNo = RV.GetLoanPlaccno(Session["BRCD"].ToString(), PrdCd);
                                RN8 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", PlaccNo, Accno, "Retirement Vouchar trf INT/" + PlaccNo, "", LOANINTAMT.ToString(), "1", "11", "TR-INT", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                            }
                            j = j + 1;
                        }
                    }
                    else
                    {
                    }

                }

                int RN99 = 0;
                if (RN2 > 0 || RN1 > 0)
                {
                    //string glcode3 = RV.GetGlcode(Session["BRCD"].ToString(), TxtPType.Text, TxtTAccNo.Text);
                    string glcode3 = RV.GetGlcodePL(Session["BRCD"].ToString(), TxtPType.Text);//Amruta 19/05/2018
                    string NetPaid = "0";
                    NetPaid = RV.getNetPaid();
                    string Amount = "0";
                    if ((Convert.ToDouble(TxtSubS.Text) + Convert.ToDouble(txtMAount.Text)) > Convert.ToDouble(txtRsub.Text))
                    {
                        Amount = ((Convert.ToDouble(TxtSubS.Text) + Convert.ToDouble(txtMAount.Text)) - Convert.ToDouble(txtRsub.Text)).ToString();
                        RN4 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), NetPaid, NetPaid, "0", "Retirement Vouchar trf/" + TxtPType.Text, "", Amount.ToString(), "1", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                    }
                    else if ((Convert.ToDouble(TxtSubS.Text) + Convert.ToDouble(txtMAount.Text)) < Convert.ToDouble(txtRsub.Text))
                    {
                        Amount = (Convert.ToDouble(txtRsub.Text) - (Convert.ToDouble(TxtSubS.Text) + Convert.ToDouble(txtMAount.Text))).ToString();
                        RN4 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), NetPaid, NetPaid, "0", "Retirement Vouchar trf/" + TxtPType.Text, "", Amount.ToString(), "2", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                    }
                    if (TxtPType.Text != "")
                    {
                        string glcode = RV.GetGlcodePL(Session["BRCD"].ToString(), TxtPType.Text);//Amruta 19/05/2018
                        if ((Convert.ToDouble(TxtSubS.Text) + Convert.ToDouble(txtMAount.Text)) > Convert.ToDouble(txtRsub.Text))
                        {
                            Amount = ((Convert.ToDouble(TxtSubS.Text) + Convert.ToDouble(txtMAount.Text)) - Convert.ToDouble(txtRsub.Text)).ToString();
                            RN99 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), NetPaid, NetPaid, "0", "Retirement Vouchar trf/" + TxtPType.Text, "", Amount.ToString(), "2", "6", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                            Amount = ((Convert.ToDouble(TxtSubS.Text) + Convert.ToDouble(txtMAount.Text)) - Convert.ToDouble(txtRsub.Text)).ToString();
                            RN99 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, TxtPType.Text, TxtTAccNo.Text == "" ? "0" : TxtTAccNo.Text, "Retirement Vouchar trf/" + TxtPType.Text, "", Amount.ToString(), "1", "6", "TR", ST2, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                        }
                        else if ((Convert.ToDouble(TxtSubS.Text) + Convert.ToDouble(txtMAount.Text)) < Convert.ToDouble(txtRsub.Text))
                        {
                            Amount = (Convert.ToDouble(txtRsub.Text) - (Convert.ToDouble(TxtSubS.Text) + Convert.ToDouble(txtMAount.Text))).ToString();
                            RN99 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, TxtPType.Text, TxtTAccNo.Text == "" ? "0" : TxtTAccNo.Text, "Retirement Vouchar trf/" + TxtPType.Text, "", Amount.ToString(), "1", "6", "TR", ST2, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                            RN99 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), NetPaid, NetPaid, "0", "Retirement Vouchar trf/" + TxtPType.Text, "", Amount.ToString(), "2", "6", "TR", ST2, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                        }
                    }
                    //RN4 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode3, TxtPType.Text, TxtTAccNo.Text, "Retirement Vouchar trf/" + TxtPType.Text, "", DRTotal.ToString(), "2", "7", "TR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                    FL = "Insert";//Dhanya Shetty
                    ClearAll();
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RetVoucher _" + ST + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
                if (RN1 > 0 && RN2 <= 0 && RN3 > 0)
                {
                    lblMessage.Text = "Payable Amount Transfer to Account And Voucher No Is........:" + ST + "";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    ClearAll();
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RetVoucher _" + ST + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
                if (RN2 > 0 && RN1 <= 0 && RN4 > 0)
                {
                    lblMessage.Text = "Recivable Amount Deducted to Account And Voucher No Is........:" + ST + "";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    ClearAll();
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RetVoucher _" + ST + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
                if (RN1 > 0 && RN2 > 0)
                {
                    if (RN99 > 0)
                    {
                        FL = "Insert";//Dhanya Shetty

                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RetVoucher _" + ST + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        lblMessage.Text = " Payable and Recivable Transfer to Account And Voucher No Is........:" + ST + " and " + ST2 + "";
                        ModalPopup.Show(this.Page);
                        ClearAll();
                    }
                    else
                    {
                        FL = "Insert";//Dhanya Shetty

                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RetVoucher _" + ST + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        lblMessage.Text = " Payable and Recivable Transfer to Account And Voucher No Is........:" + ST + "";
                        ModalPopup.Show(this.Page);
                        ClearAll();

                    }
                }

            }
            else if (ddlPayMode.SelectedValue == "3")
            {
                string InstrumentNo = TxtChequeNo.Text;
                string Instrumentdate = TxtChequeDate.Text;
                foreach (GridViewRow gvRow in this.grdstandard.Rows)
                {
                    String Custno = TXtCustNo.Text;
                    string srno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSrno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSrno")).Text);
                    if (srno != "0")
                    {
                        DedP = Convert.ToString(((CheckBox)gvRow.FindControl("Agentcollect")).Checked);
                        if (DedP == "True")
                        {

                            string PrdCd = Convert.ToString(((TextBox)gvRow.FindControl("txtsubglcode")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtsubglcode")).Text);
                            string Accno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSname")).Text == "" ? " " : ((TextBox)gvRow.FindControl("TxtSname")).Text);
                            string Amount = Convert.ToString(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                            CrTotal = Convert.ToDouble(Amount) + CrTotal;
                            string glcode = "";
                            //if(Convert.ToInt32(Accno)==0) //AMruta 19/05/2018
                            //{
                            glcode = RV.GetGlcodePL(Session["BRCD"].ToString(), PrdCd);
                            //}
                            //else
                            //{
                            // glcode = RV.GetGlcode(Session["BRCD"].ToString(), PrdCd, Accno);
                            //}
                            RN1 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, PrdCd, Accno, "Retirement Vouchar trf/" + PrdCd, "", Amount.ToString(), "2", "7", "TR", ST, InstrumentNo.ToString(), Instrumentdate.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                            string INTAMT = Convert.ToString(((TextBox)gvRow.FindControl("txtIntAmt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtIntAmt")).Text);
                            interest += Convert.ToDouble(INTAMT);
                            if (Convert.ToInt32(INTAMT) > 0)
                            {
                                string IntAccno = RV.GetGlAccNo(Session["BRCD"].ToString(), TxtPType.Text);
                                string AccNoint = "";
                                string glcode1 = "";
                                if (IntAccno == "N" || IntAccno == "n")
                                {
                                    TxtTAccNo.Enabled = true;
                                    AccNoint = "0";
                                    glcode1 = TxtPType.Text;
                                }
                                else
                                {
                                    AccNoint = TxtTAccNo.Text;
                                    // glcode1 = RV.GetGlcode(Session["BRCD"].ToString(), TxtPType.Text, TxtTAccNo.Text);
                                    glcode1 = RV.GetGlcodePL(Session["BRCD"].ToString(), TxtPType.Text);//Amruta 19/05/2018
                                }
                                string IR = RV.GetDepositIR(Session["BRCD"].ToString(), PrdCd);
                                //string glcodeIR = RV.GetGlcode(Session["BRCD"].ToString(), IR, Accno);
                                string glcodeIR = RV.GetGlcodePL(Session["BRCD"].ToString(), IR);//Amruta 19/05/2018
                                RN12 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcodeIR, IR, Accno, "Retirement Vouchar trf INT/" + PrdCd, "", INTAMT.ToString(), "2", "7", "INTR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");

                                //RN11 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode1, TxtPType.Text, AccNoint.ToString(), "Retirement Vouchar trf/" + TxtPType.Text, "", INTAMT.ToString(), "1", "7", "INTR", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE_VC", TXtCustNo.Text, txtcustName.Text, "", "0");
                            }
                            j = j + 1;
                        }
                    }

                    else
                    {

                    }

                }

                DT = RV.GetWelfareInfo1();
                txtMglcode.Text = DT.Rows[0]["glcode"].ToString();
                txtMsub.Text = DT.Rows[0]["placc"].ToString();
                if (txtMAount.Text == "0")
                {

                }
                else
                {
                    RN5 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["glcode"].ToString(), DT.Rows[0]["placc"].ToString(), "0", "Retirement Vouchar trf/" + TxtPType.Text, "", txtMAount.Text, "2", "7", "TR", ST, InstrumentNo.ToString(), Instrumentdate.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, "", "0");
                }
                CrTotal = CrTotal + Convert.ToInt32(txtMAount.Text);
                //string glcode2 = RV.GetGlcode(Session["BRCD"].ToString(), TxtPType.Text, TxtTAccNo.Text);
                string glcode2 = RV.GetGlcodePL(Session["BRCD"].ToString(), TxtPType.Text);//Amruta 19/05/2018
                // RN3 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode2, TxtPType.Text, TxtTAccNo.Text, "Retirement Vouchar trf/" + TxtPType.Text, "", CrTotal.ToString(), "1", "7", "TR", ST, InstrumentNo.ToString(), Instrumentdate.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE_VC", TXtCustNo.Text, txtcustName.Text, "", "0");
                total1 += CrTotal + interest;
                foreach (GridViewRow gvRow in this.grdRecivable.Rows)
                {

                    String Custno = TXtCustNo.Text;
                    string srno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSrno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSrno")).Text);
                    if (srno != "0")
                    {
                        DedP = Convert.ToString(((CheckBox)gvRow.FindControl("Agent")).Checked);
                        if (DedP == "True")
                        {
                            string PrdCd = Convert.ToString(((TextBox)gvRow.FindControl("txtsubglcode")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtsubglcode")).Text);
                            string Accno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSname")).Text == "" ? " " : ((TextBox)gvRow.FindControl("TxtSname")).Text);
                            string Amount = Convert.ToString(((TextBox)gvRow.FindControl("TxtSDeduction")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSDeduction")).Text);
                            double Amount1 = System.Math.Abs(Convert.ToDouble(Amount));
                            //string glcode = RV.GetGlcode(Session["BRCD"].ToString(), PrdCd, Accno);
                            string glcode = "";
                            //if (Convert.ToInt32(Accno) == 0)
                            //{
                            glcode = RV.GetGlcodePL(Session["BRCD"].ToString(), PrdCd);
                            //}
                            //else
                            //{
                            //    glcode = RV.GetGlcode(Session["BRCD"].ToString(), PrdCd, Accno);
                            //}

                            DRTotal = Convert.ToDouble(Amount1) + DRTotal;
                            RN2 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, PrdCd, Accno, "Retirement Vouchar trf/" + PrdCd, "", Amount1.ToString(), "1", "7", "TR", ST, InstrumentNo.ToString(), Instrumentdate.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                            string LOANINTAMT = Convert.ToString(((TextBox)gvRow.FindControl("txtLoanInt")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtLoanInt")).Text);
                            intRec += Convert.ToDouble(LOANINTAMT);
                            if (Convert.ToInt32(LOANINTAMT) > 0)
                            {
                                string IP = RV.GetDepositIR(Session["BRCD"].ToString(), PrdCd);
                                string glcodeIR = RV.GetGlcodeIR(Session["BRCD"].ToString(), IP);
                                RN10 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcodeIR, IP, Accno, "Retirement Vouchar trf INT/" + PrdCd, "", LOANINTAMT.ToString(), "2", "7", "TR-INT", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                                RN9 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcodeIR, IP, Accno, "Retirement Vouchar trf INT/" + PrdCd, "", LOANINTAMT.ToString(), "1", "7", "TR-INT", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                                string PlaccNo = RV.GetLoanPlaccno(Session["BRCD"].ToString(), PrdCd);
                                RN8 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", PlaccNo, "0", "Retirement Vouchar trf INT/" + PlaccNo, "", LOANINTAMT.ToString(), "1", "7", "TR-INT", ST, "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                            }
                            j = j + 1;
                        }
                    }
                    else
                    {
                    }

                }
                if (RN2 > 0)
                {
                    //string glcode3 = RV.GetGlcode(Session["BRCD"].ToString(), TxtPType.Text, TxtTAccNo.Text);
                    // RN4 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode3, TxtPType.Text, TxtTAccNo.Text, "Retirement Vouchar trf/" + TxtPType.Text, "", DRTotal.ToString(), "2", "7", "TR", ST, InstrumentNo.ToString(), Instrumentdate.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE_VC", TXtCustNo.Text, txtcustName.Text, "", "0");
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RetVoucher _" + ST + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
                total2 += DRTotal + intRec;
                double netAmount = total1 - total2;

                NetPaid = Convert.ToInt32(CLS.NetPaidProduct(Session["BRCD"].ToString()));
                string glcode3 = RV.GetGlcodeIR(Session["BRCD"].ToString(), NetPaid.ToString());

                RN4 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode3, NetPaid.ToString(), NetPaid.ToString(), "Retirement Vouchar trf/" + NetPaid.ToString(), "", netAmount.ToString(), "1", "7", "TR", ST, InstrumentNo.ToString(), Instrumentdate.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                int RN13 = 0, RN14 = 0;
                RN13 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode3, NetPaid.ToString(), NetPaid.ToString(), "Retirement Vouchar trf/" + NetPaid.ToString(), "", netAmount.ToString(), "2", "6", "TR", ST2, InstrumentNo.ToString(), Instrumentdate.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                string glcode4 = RV.GetGlcodeIR(Session["BRCD"].ToString(), TxtPType.Text);
                RN14 = RV.RetirmentVouchar(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode4, TxtPType.Text, "0", "Retirement Vouchar trf/" + TxtPType.Text, "", netAmount.ToString(), "1", "6", "TR", ST2, InstrumentNo.ToString(), Instrumentdate.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "RETIRE", TXtCustNo.Text, txtcustName.Text, txtResignvouchar.Text, "0");
                if (RN4 > 0 && RN13 > 0 && RN14 > 0 && RN1 > 0)
                {
                    ClearAll();
                    lblMessage.Text = "Payable Amount Transfer to Account And Voucher No Is..:" + ST + " and " + ST2 + "";
                    ModalPopup.Show(this.Page);
                    //   FL = "Insert";//Dhanya Shetty
                    // string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RetVoucher _" + ST + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }

            }

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
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add("SRNO", typeof(string));
            dtnew.Columns.Add("SUBGLCODE", typeof(string));
            dtnew.Columns.Add("GLNAME", typeof(string));
            dtnew.Columns.Add("ACCNO", typeof(string));
            dtnew.Columns.Add("AMOUNT", typeof(string));
            dtnew.Columns.Add("INTAMT", typeof(string));

            string SRNO = "";
            foreach (GridViewRow row in grdstandard.Rows)
            {
                SRNO = ((TextBox)row.FindControl("TxtSrno")).Text;
                string SUBGLCODE = ((TextBox)row.FindControl("txtsubglcode")).Text;
                string GLNAME = ((TextBox)row.FindControl("txtGLNAME")).Text;
                string ACCNO = ((TextBox)row.FindControl("TxtSname")).Text;
                string AMOUNT = ((TextBox)row.FindControl("TxtSDeduction")).Text;
                string INTAMT = ((TextBox)row.FindControl("txtIntAmt")).Text;

                string Calculate = ((LinkButton)row.FindControl("btnPayableIntCalculate")).Text;
                dtnew.Rows.Add(SRNO, SUBGLCODE, GLNAME, ACCNO, AMOUNT, INTAMT);
            }
            //DataTable dt = new DataTable();
            //dt = RV.GetOldData(TXtCustNo.Text, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString());

            if (dtnew.Rows.Count > 0)
            {
                int cnt = dtnew.Rows.Count;
                for (int i = dtnew.Rows.Count; i <= cnt; i++)
                {
                    if (SRNO != "" && SRNO != null || cnt == 0)
                    {
                        dtnew.Rows.Add("");
                    }
                    else
                    {
                        WebMsgBox.Show("First Fill The Black Row", this.Page);
                    }
                }
                grdstandard.DataSource = dtnew;
                grdstandard.DataBind();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add("SRNO", typeof(string));
            dtnew.Columns.Add("SUBGLCODE", typeof(string));
            dtnew.Columns.Add("GLNAME", typeof(string));
            dtnew.Columns.Add("ACCNO", typeof(string));
            dtnew.Columns.Add("AMOUNT", typeof(string));
            dtnew.Columns.Add("INTAMT", typeof(string));
            DataTable dt = new DataTable();
            //dt = RV.bindgrid1(TXtCustNo.Text, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString());

            // dt.Rows.Add(1);
            //   grdRecivable.DataSource = dt;

            //    grdRecivable.DataBind();
            string SRNO = "";
            foreach (GridViewRow row in grdRecivable.Rows)
            {
                SRNO = ((TextBox)row.FindControl("TxtSrno")).Text;
                string SUBGLCODE = ((TextBox)row.FindControl("txtsubglcode")).Text;
                string GLNAME = ((TextBox)row.FindControl("txtGLNAME")).Text;
                string ACCNO = ((TextBox)row.FindControl("TxtSname")).Text;
                string AMOUNT = ((TextBox)row.FindControl("TxtSDeduction")).Text;
                string INTAMT = ((TextBox)row.FindControl("txtLoanInt")).Text;
                dtnew.Rows.Add(SRNO, SUBGLCODE, GLNAME, ACCNO, AMOUNT, INTAMT);
            }
            //DataTable dt = new DataTable();
            //  dt = RV.GetOldData(TXtCustNo.Text, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString());

            if (dtnew.Rows.Count >= 0)
            {
                int cnt = dtnew.Rows.Count;
                for (int i = dtnew.Rows.Count; i <= cnt; i++)
                {
                    if (SRNO != "" && SRNO != null || cnt == 0)
                    {
                        dtnew.Rows.Add("");
                    }
                    else
                    {
                        WebMsgBox.Show("First Fill The Black Row", this.Page);
                    }
                }
                grdRecivable.DataSource = dtnew;
                grdRecivable.DataBind();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }


    }
    protected void btnexit_Click(object sender, EventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void BtnCloseAcc_Click(object sender, EventArgs e)
    {
        string DedP = "";
        int Result1 = 0;
        int Result2 = 0;
        try
        {
            foreach (GridViewRow gvRow in this.grdstandard.Rows)
            {

                string Custno = TXtCustNo.Text;
                string srno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSrno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSrno")).Text);
                if (srno != "0")
                {
                    DedP = Convert.ToString(((CheckBox)gvRow.FindControl("Agentcollect")).Checked);
                    if (DedP == "True")
                    {

                        string PrdCd = Convert.ToString(((TextBox)gvRow.FindControl("txtsubglcode")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtsubglcode")).Text);
                        string Accno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSname")).Text == "" ? " " : ((TextBox)gvRow.FindControl("TxtSname")).Text);
                        string glcode = RV.GetGlcode(Session["BRCD"].ToString(), PrdCd, Accno);
                        Result1 = RV.CloseAcc(PrdCd, Session["BRCD"].ToString(), Accno, Session["EntryDate"].ToString());
                    }
                }

                else
                {

                }
            }
            foreach (GridViewRow gvRow in this.grdRecivable.Rows)
            {

                String Custno = TXtCustNo.Text;
                string srno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSrno")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtSrno")).Text);
                if (srno != "0")
                {
                    DedP = Convert.ToString(((CheckBox)gvRow.FindControl("Agent")).Checked);
                    if (DedP == "True")
                    {
                        string PrdCd = Convert.ToString(((TextBox)gvRow.FindControl("txtsubglcode")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtsubglcode")).Text);
                        string Accno = Convert.ToString(((TextBox)gvRow.FindControl("TxtSname")).Text == "" ? " " : ((TextBox)gvRow.FindControl("TxtSname")).Text);
                        string glcode = RV.GetGlcode(Session["BRCD"].ToString(), PrdCd, Accno);
                        Result2 = RV.CloseAcc(PrdCd, Session["BRCD"].ToString(), Accno, Session["EntryDate"].ToString());

                    }
                }
                else
                {
                }

            }
            if (Result1 > 0 && Result2 > 0)
            {
                lblMessage.Text = "Selected Accounts CLosed Successfully......!!";
                ModalPopup.Show(this.Page);
            }


        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion
    protected void Unnamed_Click(object sender, EventArgs e)
    {

    }
    protected void btnPayableIntCalculate_Click(object sender, EventArgs e)
    {
        DataTable depositDT = new DataTable();
        DataTable depositintDT = new DataTable();
        try
        {

            LinkButton lnkSelect = (LinkButton)sender;
            string[] value = lnkSelect.CommandArgument.Split(',');
            string subGlCode = value[0];
            string accNo = value[1];
            string lastIntDate = DateTime.Now.Year.ToString() + "-04-01";
            int isvalidDepositGl = CLS.isValidDepositGL(BRCD: Convert.ToString(Session["BRCD"]), SubGlCode: subGlCode);
            if (isvalidDepositGl == 5)
            {
                depositDT = CLS.GetDepositData(BRCD: Convert.ToString(Session["BRCD"]), SubGlCode: subGlCode, AccNo: accNo);
                if (depositDT.Rows.Count > 0)
                {
                    lastIntDate = Convert.ToString(depositDT.Rows[0]["LASTINTDATE"]).Substring(0, 10);

                    depositintDT = SB.GetSBIntCalForSingleAccNo(BrCode: Convert.ToString(Session["BRCD"]), PrCode: subGlCode, FDate: lastIntDate, TDate: Convert.ToString(Session["EntryDate"]),
                    Mid: Convert.ToString(Session["MID"]), AccNo: accNo);
                    var finalAmount = depositintDT.AsEnumerable().Sum(row => row.Field<Decimal>("IntAmount"));
                    //txtIntAmt.Text = finalAmount;
                    GridViewRow gvRow = grdstandard.Rows[Convert.ToInt32(hdnRowIndexForPayable.Value) - 1];
                    ((TextBox)gvRow.FindControl("txtIntAmt")).Text = Math.Round(finalAmount).ToString();
                    GetPayableTotalAmount();


                }
                else
                {
                    GridViewRow gvRow = grdstandard.Rows[Convert.ToInt32(hdnRowIndexForPayable.Value) - 1];
                    ((TextBox)gvRow.FindControl("txtIntAmt")).Text = "0";
                    GetPayableTotalAmount();
                }
            }
            else
            {
                GridViewRow gvRow = grdstandard.Rows[Convert.ToInt32(hdnRowIndexForPayable.Value) - 1];
                ((TextBox)gvRow.FindControl("txtIntAmt")).Text = "0";
                GetPayableTotalAmount();
            }





        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void btnRecIntCalculate_Click(object sender, EventArgs e)
    {
        DataTable depositDT = new DataTable();
        DataTable depositintDT = new DataTable();
        try
        {

            LinkButton lnkSelect = (LinkButton)sender;
            string[] value = lnkSelect.CommandArgument.Split(',');
            string subGlCode = value[0];
            string accNo = value[1];
            string lastIntDate = DateTime.Now.Year.ToString() + "-04-01";
            int isvalidDepositGl = CLS.isValidDepositGL(BRCD: Convert.ToString(Session["BRCD"]), SubGlCode: subGlCode);
            if (isvalidDepositGl == 5)
            {
                depositDT = CLS.GetDepositData(BRCD: Convert.ToString(Session["BRCD"]), SubGlCode: subGlCode, AccNo: accNo);
                if (depositDT.Rows.Count > 0)
                {
                    lastIntDate = Convert.ToString(depositDT.Rows[0]["LASTINTDATE"]).Substring(0, 10);

                    depositintDT = SB.GetSBIntCalForSingleAccNo(BrCode: Convert.ToString(Session["BRCD"]), PrCode: subGlCode, FDate: lastIntDate, TDate: Convert.ToString(Session["EntryDate"]),
                    Mid: Convert.ToString(Session["MID"]), AccNo: accNo);
                    var finalAmount = depositintDT.AsEnumerable().Sum(row => row.Field<Decimal>("IntAmount"));
                    //txtIntAmt.Text = finalAmount;
                    GridViewRow gvRow = grdRecivable.Rows[Convert.ToInt32(hdnRowIndexForPayable.Value) - 1];
                    ((TextBox)gvRow.FindControl("txtIntAmt")).Text = Math.Round(finalAmount).ToString();
                    GetPayableTotalAmount();


                }
                else
                {
                    GridViewRow gvRow = grdRecivable.Rows[Convert.ToInt32(hdnRowIndexForPayable.Value) - 1];
                    ((TextBox)gvRow.FindControl("txtIntAmt")).Text = "0";
                    GetPayableTotalAmount();
                }
            }
            else
            {
                GridViewRow gvRow = grdRecivable.Rows[Convert.ToInt32(hdnRowIndexForPayable.Value) - 1];
                ((TextBox)gvRow.FindControl("txtIntAmt")).Text = "0";
                GetPayableTotalAmount();
            }





        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void grdstandard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox lbl_GLCode = (TextBox)e.Row.FindControl("txtsubglcode");
                int isvalidDepositGl = CLS.isValidDepositGL(BRCD: Convert.ToString(Session["BRCD"]), SubGlCode: lbl_GLCode.Text);

                if (isvalidDepositGl == 5)
                {
                    LinkButton btnPayableIntCalculate = (LinkButton)e.Row.FindControl("btnPayableIntCalculate");

                    btnPayableIntCalculate.Visible = true;
                }
                else
                {
                    LinkButton btnPayableIntCalculate = (LinkButton)e.Row.FindControl("btnPayableIntCalculate");
                    btnPayableIntCalculate.Visible = false;

                }





            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void grdRecivable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox lbl_GLCode = (TextBox)e.Row.FindControl("txtsubglcode");
                int isvalidDepositGl = CLS.isValidDepositGL(BRCD: Convert.ToString(Session["BRCD"]), SubGlCode: lbl_GLCode.Text);

                if (isvalidDepositGl == 5)
                {
                    LinkButton btnRecIntCalculate = (LinkButton)e.Row.FindControl("btnRecIntCalculate");

                    btnRecIntCalculate.Visible = true;
                }
                else
                {
                    LinkButton btnRecIntCalculate = (LinkButton)e.Row.FindControl("btnRecIntCalculate");
                    btnRecIntCalculate.Visible = false;

                }





            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnAddNew_1_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmVoucherPrint.aspx", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}