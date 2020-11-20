using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class FrmDividendClac : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsFDIntCalculation FD = new ClsFDIntCalculation();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsDividendCalc DC = new ClsDividendCalc();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    string Skip = "";
    string STR = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            TxtFDate.Focus();
            autoglname.ContextKey = Session["BRCD"].ToString();
            // autoglname1.ContextKey = Session["BRCD"].ToString();
            AutoGlname3.ContextKey = Session["BRCD"].ToString();
            AutoGlname4.ContextKey = Session["BRCD"].ToString();
            Hf_WorkingDt.Value = Session["EntryDate"].ToString();
            BD.BindCalType(DdlCalcType);
        }
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 50000;

    }

    #region Text Changes
    protected void TxtFPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string GRP = BD.GetGLGroup(TxtFPRD.Text, Session["BRCD"].ToString(), "4");
            if (GRP != null && GRP == "SHR")
            {
                string TD = BD.GetAccTypeGL(TxtFPRD.Text, Session["BRCD"].ToString());
                string[] TD1 = TD.Split('_');
                if (TD1.Length > 1)
                {

                }
                TxtFPRDName.Text = TD1[0].ToString();
                ViewState["FGL"] = TD1[1].ToString();

                TxtFAcc.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid product code, Only SHR group Product accepted...!", this.Page);
                TxtFPRD.Text = "";
                TxtFPRDName.Text = "";
                TxtFPRD.Focus();
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

                string GRP = BD.GetGLGroup(CT[1].ToString(), Session["BRCD"].ToString(), "4");
                if (GRP != null && GRP == "SHR")
                {
                    TxtFPRDName.Text = CT[0].ToString();
                    TxtFPRD.Text = CT[1].ToString();
                    string[] GLS = BD.GetAccTypeGL(TxtFPRD.Text, Session["BRCD"].ToString()).Split('_');
                    ViewState["DRGL"] = GLS[1].ToString();
                    autoglname.ContextKey = Session["BRCD"].ToString() + "_" + TxtFPRD.Text + "_" + ViewState["DRGL"].ToString();
                    string[] TD = Session["EntryDate"].ToString().Split('/');


                    if (TxtFPRDName.Text == "")
                    {
                        WebMsgBox.Show("Please enter valid Product code", this.Page);
                        TxtFPRD.Text = "";
                        TxtFPRD.Focus();
                    }
                    TxtFAcc.Focus();
                }
                else
                {
                    WebMsgBox.Show("Invalid product code, Only SHR group Product accepted...!", this.Page);
                    TxtFPRD.Text = "";
                    TxtFPRDName.Text = "";
                    TxtFPRD.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    //protected void TxtTPRD_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string TD = BD.GetAccTypeGL(TxtTPRD.Text, Session["BRCD"].ToString());
    //        string[] TD1 = TD.Split('_');
    //        if (TD1.Length > 1)
    //        {

    //        }
    //        TXtTPRDName.Text = TD1[0].ToString();
    //        ViewState["TGL"] = TD1[1].ToString();

    //        TxtFAcc.Focus();
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    protected void TXtTPRDName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtFAcc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtFAccName.Text = BD.AccName(TxtFAcc.Text, TxtFPRD.Text, TxtFPRD.Text, Session["BRCD"].ToString());
            TxtTAcc.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFAccName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtTAcc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtTAccName.Text = BD.AccName(TxtTAcc.Text, TxtFPRD.Text, TxtFPRD.Text, Session["BRCD"].ToString());
            TxtDiviProdCode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTAccName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtDiviProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string TD = BD.GetAccTypeGL(TxtDiviProdCode.Text, Session["BRCD"].ToString());
            if (TD != null)
            {
                string[] TD1 = TD.Split('_');
                if (TD1.Length > 1)
                {

                }
                TxtDiviProdName.Text = TD1[0].ToString();
                ViewState["TGL"] = TD1[1].ToString();

                TxtDebitPrdCode.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid product code....!", this.Page);
                TxtDiviProdCode.Text = "";
                TxtDiviProdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtDiviProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtDiviProdName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {

                TxtDiviProdName.Text = CT[0].ToString();
                TxtDiviProdCode.Text = CT[1].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtDiviProdCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();

                if (TxtDiviProdName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtDiviProdCode.Text = "";
                    TxtDiviProdCode.Focus();

                }
                else
                {
                    TxtDebitPrdCode.Focus();
                }


            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void TxtDebitPrdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string TD = BD.GetAccTypeGL(TxtDebitPrdCode.Text, Session["BRCD"].ToString());
            string[] TD1 = TD.Split('_');
            if (TD1.Length > 1)
            {

            }
            TxtDebitPrdName.Text = TD1[0].ToString();
            ViewState["TGL"] = TD1[1].ToString();

            TxtDiviRate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtDebitPrdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtDebitPrdName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {

                TxtDebitPrdName.Text = CT[0].ToString();
                TxtDebitPrdCode.Text = CT[1].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtDebitPrdCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();

                if (TxtDebitPrdName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtDebitPrdCode.Text = "";
                    TxtDebitPrdCode.Focus();

                }
                else
                {
                    TxtDiviRate.Focus();
                }


            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void TxtDiviRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DdlCalcType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DdlCalcType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtNarr.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtNarr_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Btn_Calculate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Button Clicks
    protected void Btn_Calculate_Click(object sender, EventArgs e)
    {
        try
        {
            if (DdlCalcType.SelectedValue.ToString() == "4")
            {
                DT = DC.CalculateSDC_DT("CALC", TxtTDate.Text, TxtFDate.Text, TxtFAcc.Text, TxtTAcc.Text, TxtDiviRate.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), TxtFPRD.Text);
            }
            if (DdlCalcType.SelectedValue.ToString() == "5")
            {
                DT = DC.CalculateSDC_DTASON ("CALC", TxtTDate.Text, TxtFDate.Text, TxtFAcc.Text, TxtTAcc.Text, TxtDiviRate.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), TxtFPRD.Text);
            }
            else
            {
                DT = DC.CalculateSDC("CALC", "", DdlCalcType.SelectedValue.ToString(), Session["EntryDate"].ToString(), TxtFDate.Text, TxtTDate.Text, TxtFPRD.Text, TxtFPRD.Text, TxtFAcc.Text, TxtTAcc.Text, TxtDiviRate.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), TxtNarr.Text);
            }

            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["RESULT"].ToString() == "1")
                {
                    WebMsgBox.Show("Calculation Process completed, You can run Trail Report..", this.Page);
                    Btn_Report.Focus();
                    FL = "Insert";//Dhanya Shetty 23/09/2017
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DivCalc_Cal" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
                else
                {
                    WebMsgBox.Show(DT.Rows[0]["RESULT"].ToString(), this.Page);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Recalculate_Click(object sender, EventArgs e)
    {
        int RS = 0;
        RS = DC.Recalculate(Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
        if (RS > 0)
        {
            WebMsgBox.Show("Calculated data removed, " + Session["LOGINCODE"].ToString() + " can calculate from start...!", this.Page);
            Btn_Calculate.Focus();
            FL = "Insert";//Dhanya Shetty 23/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DivCalc_Calremove" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        else
        {
            WebMsgBox.Show("Data not available on " + Session["EntryDate"].ToString() + " for User " + Session["LOGINCODE"].ToString() + " ...!", this.Page);
            Btn_Calculate.Focus();
        }
    }
    protected void Btn_TrailEntry_Click(object sender, EventArgs e)
    {
        try
        {
            DT = DC.GetTrailRun("", DdlCalcType.SelectedValue.ToString(), Session["EntryDate"].ToString(), TxtFDate.Text, TxtTDate.Text, TxtFPRD.Text, TxtFPRD.Text, TxtFAcc.Text, TxtTAcc.Text, TxtDiviRate.Text, Session["BRCD"].ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                if (DT.Rows.Count > 2)
                {
                    WebMsgBox.Show("Calculation Process completed, You can run Trail Report...", this.Page);
                    Btn_Report.Focus();
                    FL = "Insert";//Dhanya Shetty 23/09/2017
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DivCalc_Trail" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
                else
                {
                    WebMsgBox.Show(DT.Rows[0]["RESULT"].ToString(), this.Page);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_ApplyEntry_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtDiviProdCode.Text == "" || TxtDiviProdCode.Text == "0")
            {
                WebMsgBox.Show("Enter Dividend Credit Code......!", this);
                TxtDiviProdCode.Text = "";
                TxtDiviProdName.Text = "";
                TxtDiviProdCode.Focus();
                return;
            }
            else if (TxtDebitPrdCode.Text == "" || TxtDebitPrdCode.Text == "0")
            {
                WebMsgBox.Show("Enter Dividend Debit Code......!", this);
                TxtDebitPrdCode.Text = "";
                TxtDebitPrdName.Text = "";
                TxtDebitPrdCode.Focus();
                return;
            }
            else
            {
                STR = DC.PostDividend("POST", DdlCalcType.SelectedValue.ToString(), Session["EntryDate"].ToString(), TxtDiviRate.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), TxtDiviProdCode.Text, TxtDebitPrdCode.Text);
                if (STR != null)
                {
                    WebMsgBox.Show("Dividend posted successfully with Set No - " + STR, this.Page);
                    FL = "Insert";//Dhanya Shetty 23/09/2017
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DivCalc_post" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    Clear();
                }
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Report_Click(object sender, EventArgs e)
    {
        try
        {
                FL = "Insert";//Dhanya Shetty 23/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DivCalc_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&FDATE=" + TxtFDate.Text + "&TDATE=" + TxtTDate.Text + "&FPRDCD=" + TxtFPRD.Text + "&TPRDCD=" + TxtFPRD.Text + "&FACCNO=" + TxtFAcc.Text + "&TACCNO=" + TxtTAcc.Text + "&FL=" + DdlCalcType.SelectedValue.ToString() + "&RATE=" + TxtDiviRate.Text + "&rptname=RptDividendCalc.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
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
    #endregion

    #region User functions
    public void Clear()
    {
        TxtFDate.Text = "";
        TxtTDate.Text = "";
        //  TxtTPRD.Text = "";
        TxtFPRD.Text = "";
        TxtFPRDName.Text = "";
        //  TXtTPRDName.Text = "";
        TxtFAcc.Text = "";
        TxtTAcc.Text = "";
        TxtFAccName.Text = "";
        TxtTAccName.Text = "";
        TxtDiviProdCode.Text = "";
        TxtDiviProdName.Text = "";
        TxtDebitPrdCode.Text = "";
        TxtDebitPrdName.Text = "";
        TxtDiviRate.Text = "";
        DdlCalcType.SelectedValue = "0";
        TxtNarr.Text = "";
        TxtFDate.Focus();
    }
    #endregion

    protected void Btn_TextReport_Click(object sender, EventArgs e)
    {
        try
        {
            string filename = "DividenCalc.txt";

            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.Charset = "";
            Response.ContentType = "application/octet-stream";

            DataTable dt = new DataTable();
            dt = DC.DividendCalCTextRpt(Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
            StringWriter writer = new StringWriter();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        writer.WriteLine(dt.Rows[i][0]);
                    }
                }
                writer.Close();
                Response.Output.Write(writer);


            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void ExportDataTabletoFile(DataTable datatable, string delimited, bool exportcolumnsheader, string file)
    {
        StreamWriter str = new StreamWriter(file, false, System.Text.Encoding.Default);
        if (DT.Rows.Count > 0)
        {
            int i = 0;
            for (i=0 ;i<DT.Rows.Count;i++)
            {
                str.WriteLine(DT.Rows[0][0]);
            }
        }
        str.Flush();
        str.Close();
    }
}