using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmLoanGLCreation : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsLoanGLCreation LC = new ClsLoanGLCreation();
    int RM = 0;
    string PLACCNO="",PGL="",OTHCHG="",PPL="";
    string RESULT = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string Flag;
        Flag = Request.QueryString["Flag"].ToString();

        if(!IsPostBack)
        {
            if (Flag == "AD")
            {
                string GlCode = GetGlCode();
                txtLoanCode.Text = GlCode.ToString();
                GetNextNumber();
                btnSubmit.Visible = true;
                btnModify.Visible = false;
                btnDelete.Visible = false;
                ENDN(false);
            }
            else if (Flag == "MD")
            {
                txtLoanCode.Enabled = true;
                btnSubmit.Visible = false;
                btnModify.Visible = true;
                btnDelete.Visible = false;
            }
            else if (Flag == "DL")
            {
                txtLoanCode.Enabled = true;
                btnSubmit.Visible = false;
                btnModify.Visible = false;
                btnDelete.Visible = true;
            }
            else if (Flag == "VW")
            {
                txtLoanCode.Enabled = true;
                btnSubmit.Visible = false;
                btnModify.Visible = false;
                btnDelete.Visible = false;
            }
            BindCategory();
            txtLoanType.Focus();
        }
        
        BindGrid();
    }

    protected void ENDN(bool TF)
    {
        TxtIRCode.Enabled = TF;
        TxtIRName.Enabled = TF;
        TxtPenCode.Enabled = TF;
        TxtPenName.Enabled = TF;
    }
    public string GetGlCode()
    {
        string sql = "";
        string setnumb = "";
        try
        {
            sql = "SELECT isnull(max(subglcode),200) FROM GLMAST where glcode=3 and brcd='" + Session["BRCD"].ToString() + "' and glcode<>subglcode";
            setnumb = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return (Convert.ToInt32(setnumb) + 1).ToString();
    }

    public void GetNextNumber()
    {
        try
        {
            string SqlQuery = "";
            SqlQuery = "EXEC SP_NextNumberForLoanGL @BRCD='" + Session["BRCD"].ToString() + "' ";
            DT = new DataTable();
            DT = conn.GetDatatable(SqlQuery);

            if (DT.Rows.Count > 0)
            {
                PGL=  DT.Rows[0]["PGL"].ToString();
                PPL =  DT.Rows[0]["PPL"].ToString();
                OTHCHG= DT.Rows[0]["OTHER"].ToString();

                TxtIRCode.Text = DT.Rows[0]["IR"].ToString();
                PLACCNO= DT.Rows[0]["PLA"].ToString();
                TxtPenCode.Text = DT.Rows[0]["IOR"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindCategory()
    {
        BD.BindCategry(ddlCategory);
    }

    public void BindGrid()
    {
        try
        {
            string sql = "";
            int Result;

            sql = "SELECT * FROM LOANGL where brcd='" + Session["BRCD"].ToString() + "'";
            Result = conn.sBindGrid(grdLoanGL, sql);
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
            //string SqlQuery = "";

            ////SqlQuery = "EXEC SPLOANGL @BRCD = '" + Session["BRCD"].ToString() + "', @LoanCode = '" + txtLoanCode.Text.ToString() + "', @LoanType = '" + txtLoanType.Text.ToString() + "', @Category = '" + ddlCategory.SelectedValue.ToString() + "', @IntType = '" + ddlIntType.SelectedValue.ToString() + "', @IntAmount = '" + txtIntAmount.Text.ToString() + "', "
            ////        + " @LoanAmount = '" + txtLoanAmt.Text.ToString() + "', @RepName = '" + txtRepName.Text.ToString() + "', @IntRate = '" + txtIntRate.Text.ToString() + "', @Period = '" + txtPeriod.Text.ToString() + "', @acc1 = '" + TextBox1.Text.ToString() + "', @acc2 = '" + TextBox2.Text.ToString() + "', @acc3 = '" + TextBox3.Text.ToString() + "', "
            ////        + " @acc4 ='" + TextBox4.Text.ToString() + "', @acc5 = '" + TextBox5.Text.ToString() + "', @acc6 = '" + TextBox6.Text.ToString() + "', @acc7 = '" + TextBox7.Text.ToString() + "', @acc8 = '" + TextBox8.Text.ToString() + "', @acc9 = '" + TextBox9.Text.ToString() + "', @Flag = '" + Request.QueryString["Flag"].ToString() + "'";
            
            //int RM = conn.sExecuteQuery(SqlQuery);

            RM = LC.Insert_GL("LOANMASTER", "AD", Session["BRCD"].ToString(), txtLoanCode.Text, txtLoanType.Text, ddlCategory.SelectedValue, ddlIntType.SelectedValue,
                              txtLoanAmt.Text, txtRepName.Text, txtIntRate.Text, txtPeriod.Text, TxtIRCode.Text, TxtPenCode.Text, TxtIRName.Text, TxtPenName.Text,
                              PLACCNO.ToString(), "0", PGL.ToString(),PPL.ToString(), OTHCHG.ToString());
            if (RM > 0)
            {
                lblMessage.Text = "Successfully Inserted..!";
                ModalPopup.Show(this.Page);
                ClearData(Page.Controls);
                BindGrid();
            }
            else
            {
                lblMessage.Text = "Unsuccesfull..!";
                ModalPopup.Show(this.Page);
                ClearData(Page.Controls);
                BindGrid();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearData(ControlCollection ctrls)
    {
        
        txtLoanCode.Text = "";
        txtLoanType.Text = "";
        ddlCategory.SelectedIndex = 0;
        ddlIntType.SelectedIndex = 0;
        TxtIRCode.Text = "";
        TxtPenCode.Text = "";
        TxtIRName.Text = "";
        TxtPenName.Text = "";

        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            ClearData(ctrl.Controls);
        } 
        if (Request.QueryString["Flag"].ToString() == "AD")
        {
            string GlCode = GetGlCode();
            txtLoanCode.Text = GlCode.ToString();
        }
    }

    protected void grdLoanGL_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdLoanGL.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            //string SqlQuery = "";

            ////SqlQuery = "EXEC SPLOANGL @BRCD = '" + Session["BRCD"].ToString() + "', @LoanCode = '" + txtLoanCode.Text.ToString() + "', @LoanType = '" + txtLoanType.Text.ToString() + "', @Category = '" + ddlCategory.SelectedValue.ToString() + "', @IntType = '" + ddlIntType.SelectedValue.ToString() + "', @IntAmount = '" + txtIntAmount.Text.ToString() + "', "
            ////        + " @LoanAmount = '" + txtLoanAmt.Text.ToString() + "', @RepName = '" + txtRepName.Text.ToString() + "', @IntRate = '" + txtIntRate.Text.ToString() + "', @Period = '" + txtPeriod.Text.ToString() + "', @acc1 = '" + TextBox1.Text.ToString() + "', @acc2 = '" + TextBox2.Text.ToString() + "', @acc3 = '" + TextBox3.Text.ToString() + "', "
            ////        + " @acc4 ='" + TextBox4.Text.ToString() + "', @acc5 = '" + TextBox5.Text.ToString() + "', @acc6 = '" + TextBox6.Text.ToString() + "', @acc7 = '" + TextBox7.Text.ToString() + "', @acc8 = '" + TextBox8.Text.ToString() + "', @acc9 = '" + TextBox9.Text.ToString() + "', @Flag = '" + Request.QueryString["Flag"].ToString() + "'";

            //int RM = conn.sExecuteQuery(SqlQuery);
            RM = LC.Insert_GL("LOANMASTER", "MD", Session["BRCD"].ToString(), txtLoanCode.Text, txtLoanType.Text, ddlCategory.SelectedValue, ddlIntType.SelectedValue,
                            txtLoanAmt.Text, txtRepName.Text, txtIntRate.Text, txtPeriod.Text, TxtIRCode.Text, TxtPenCode.Text, TxtIRName.Text, TxtPenName.Text,
                            PLACCNO.ToString(), "0", PGL.ToString(), PPL.ToString(), OTHCHG.ToString());
            if (RM > 0)
            {
                lblMessage.Text = "Successfully Modified..!";
                ModalPopup.Show(this.Page);
                ClearData(Page.Controls);
                BindGrid();
            }
            else
            {
                lblMessage.Text = "Unsuccesfull..!";
                ModalPopup.Show(this.Page);
                ClearData(Page.Controls);
                BindGrid();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            //string SqlQuery = "";

            ////SqlQuery = "EXEC SPLOANGL @BRCD = '" + Session["BRCD"].ToString() + "', @LoanCode = '" + txtLoanCode.Text.ToString() + "', @LoanType = '" + txtLoanType.Text.ToString() + "', @Category = '" + ddlCategory.SelectedValue.ToString() + "', @IntType = '" + ddlIntType.SelectedValue.ToString() + "', @IntAmount = '" + txtIntAmount.Text.ToString() + "', "
            ////        + " @LoanAmount = '" + txtLoanAmt.Text.ToString() + "', @RepName = '" + txtRepName.Text.ToString() + "', @IntRate = '" + txtIntRate.Text.ToString() + "', @Period = '" + txtPeriod.Text.ToString() + "', @acc1 = '" + TextBox1.Text.ToString() + "', @acc2 = '" + TextBox2.Text.ToString() + "', @acc3 = '" + TextBox3.Text.ToString() + "', "
            ////        + " @acc4 ='" + TextBox4.Text.ToString() + "', @acc5 = '" + TextBox5.Text.ToString() + "', @acc6 = '" + TextBox6.Text.ToString() + "', @acc7 = '" + TextBox7.Text.ToString() + "', @acc8 = '" + TextBox8.Text.ToString() + "', @acc9 = '" + TextBox9.Text.ToString() + "', @Flag = '" + Request.QueryString["Flag"].ToString() + "'";

            //int RM = conn.sExecuteQuery(SqlQuery);

            RM = LC.Insert_GL("LOANMASTER", "DL", Session["BRCD"].ToString(), txtLoanCode.Text, txtLoanType.Text, ddlCategory.SelectedValue, ddlIntType.SelectedValue,
                            txtLoanAmt.Text, txtRepName.Text, txtIntRate.Text, txtPeriod.Text, TxtIRCode.Text, TxtPenCode.Text, TxtIRName.Text, TxtPenName.Text,
                            PLACCNO.ToString(), "0", PGL.ToString(), PPL.ToString(), OTHCHG.ToString());
            if (RM > 0)
            {
                lblMessage.Text = "Successfully Deleted..!";
                ModalPopup.Show(this.Page);
                ClearData(Page.Controls);
                BindGrid();
            }
            else
            {
                lblMessage.Text = "Unsuccesfull..!";
                ModalPopup.Show(this.Page);
                ClearData(Page.Controls);
                BindGrid();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtLoanCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string SqlQuery = "";
            SqlQuery = "SELECT G.SUBGLCODE, G.GLNAME, L.LOANTYPE,L.LOANGLBALANCE, ISNULL(L.REPORTNAME, '') AS REPORTNAME,ISNULL(L.ROI, 0) ROI, "
                        + " ISNULL(L.PERIOD, 0) PERIOD,ISNULL(L.PGL, 0) PGL,ISNULL(L.PPL, 0) PPL,ISNULL(L.OTHERCHG, 0) OTHERCHG,ISNULL(G.IR, 0) IR,ISNULL(G.PLACCNO, 0) PLACCNO,ISNULL(G.IOR, 0) IOR FROM GLMAST G "
                        + " INNER JOIN LOANGL l ON L.LOANGLCODE = G.SUBGLCODE WHERE G.GLCODE = 3 AND G.SUBGLCODE = '" + txtLoanCode.Text.ToString() + "' ";
            DT = new DataTable();
            DT = conn.GetDatatable(SqlQuery);

            if (DT.Rows.Count > 0)
            {
                txtLoanCode.Enabled = false;
                txtLoanCode.Text = DT.Rows[0]["SUBGLCODE"].ToString();
                txtLoanType.Text = DT.Rows[0]["GLNAME"].ToString();
                //ddlCategory.Text = DT.Rows[0]["LOANTYPE"].ToString();
                txtIntAmount.Text = DT.Rows[0]["LOANGLBALANCE"].ToString();
                txtRepName.Text = DT.Rows[0]["REPORTNAME"].ToString();
                txtIntRate.Text = DT.Rows[0]["ROI"].ToString();
                txtPeriod.Text = DT.Rows[0]["PERIOD"].ToString();

               // TextBox1.Text = DT.Rows[0]["PGL"].ToString();
//TextBox4.Text = DT.Rows[0]["PPL"].ToString();
              //  TextBox7.Text = DT.Rows[0]["OTHERCHG"].ToString();

                TxtIRCode.Text = DT.Rows[0]["IR"].ToString();
                //TextBox5.Text = DT.Rows[0]["PLACCNO"].ToString();
                TxtPenCode.Text = DT.Rows[0]["IOR"].ToString();
            }
            else
            {
                lblMessage.Text = "No Record Available..!";
                ModalPopup.Show(this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //protected void TextBox2_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string SqlQuery = "";
    //        SqlQuery = "SELECT * FROM GLMAST where SUBGLCODE = '" + TextBox2.Text.ToString() + "'";
    //        DT = new DataTable();
    //        DT = conn.GetDatatable(SqlQuery);
    //        if (DT.Rows.Count > 0)
    //        {
    //            lblMessage.Text = "Allready used Please Use Any Different";
    //            ModalPopup.Show(this.Page);
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    //protected void TextBox5_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string SqlQuery = "";
    //        SqlQuery = "SELECT * FROM GLMAST where SUBGLCODE = '" + TextBox5.Text.ToString() + "'";
    //        DT = new DataTable();
    //        DT = conn.GetDatatable(SqlQuery);
    //        if (DT.Rows.Count > 0)
    //        {
    //            lblMessage.Text = "Allready used Please Use Any Different";
    //            ModalPopup.Show(this.Page);
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    //protected void TextBox8_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string SqlQuery = "";
    //        SqlQuery = "SELECT * FROM GLMAST where SUBGLCODE = '" + TextBox8.Text.ToString() + "'";
    //        DT = new DataTable();
    //        DT = conn.GetDatatable(SqlQuery);
    //        if (DT.Rows.Count > 0)
    //        {
    //            lblMessage.Text = "Allready used Please Use Any Different";
    //            ModalPopup.Show(this.Page);
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
}