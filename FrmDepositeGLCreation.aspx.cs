using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmDepositeGLCreation : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        string Flag;
        Flag = Request.QueryString["Flag"].ToString();

        if (!IsPostBack)
        {
            if (Flag == "AD")
            {
                string GlCode = GetGlCode();
                txtDepCode.Text = GlCode.ToString();
                btnSubmit.Visible = true;
                btnModify.Visible = false;
                btnDelete.Visible = false;
            }
            else if (Flag == "MD")
            {
                txtDepCode.Enabled = true;
                btnSubmit.Visible = false;
                btnModify.Visible = true;
                btnDelete.Visible = false;
            }
            else if (Flag == "DL")
            {
                txtDepCode.Enabled = true;
                btnSubmit.Visible = false;
                btnModify.Visible = false;
                btnDelete.Visible = true;
            }

            BindCategory();
        }
        
        BindGrid();
    }

    public string GetGlCode()
    {
        string sql = "";
        string setnumb = "";
        try
        {
            sql = "SELECT MAX(SUBGLCODE) FROM GLMAST";
            setnumb = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return (Convert.ToInt32(setnumb) + 1).ToString();
    }

    public void BindCategory()
    {
        BD.BindCategory(ddlCategory);
    }

    public void BindGrid()
    {
        string sql = "";
        int Result;

        sql = "SELECT * FROM DEPOSITGL";
        Result = conn.sBindGrid(grdDepositGL, sql);
    }
        
    protected void grdDepositGL_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDepositGL.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string SqlQuery = "";

        SqlQuery = "EXEC SPDEPOSITEGL @BRCD = '" + Session["BRCD"].ToString() + "', @DepCode = '" + txtDepCode.Text.ToString() + "', @DepType = '" + txtDepType.Text.ToString() + "', @Category = '" + ddlCategory.SelectedValue + "', @RepName = '" + txtRepName.Text.ToString() + "', @Status = '" + ddlStatus.SelectedValue.ToString() + "', @Flag = '" + Request.QueryString["Flag"].ToString() + "'";

        int RM = conn.sExecuteQuery(SqlQuery);

        if (RM > 0)
        {
            lblMessage.Text = "Successfully Inserted..!";
            ModalPopup.Show(this.Page);
            ClearData();
            BindGrid();
        }
    }

    public void ClearData()
    {
        if (Request.QueryString["Flag"].ToString() == "AD")
        {
            string GlCode = GetGlCode();
            txtDepCode.Text = GlCode.ToString();
        }
        txtDepCode.Text = "";
        txtDepType.Text = "";
        ddlCategory.SelectedIndex = 0;
        txtRepName.Text = "";
        ddlStatus.SelectedIndex = 0;
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        string SqlQuery = "";

        SqlQuery = "EXEC SPDEPOSITEGL @BRCD = '" + Session["BRCD"].ToString() + "', @DepCode = '" + txtDepCode.Text.ToString() + "', @DepType = '" + txtDepType.Text.ToString() + "', @Category = '" + ddlCategory.SelectedValue.ToString() + "', @RepName = '" + txtRepName.Text.ToString() + "', @Status = '" + ddlStatus.SelectedValue.ToString() + "', @Flag = '" + Request.QueryString["Flag"].ToString() + "'";

        int RM = conn.sExecuteQuery(SqlQuery);

        if (RM > 0)
        {
            lblMessage.Text = "Successfully Modified..!";
            ModalPopup.Show(this.Page);
            ClearData();
            BindGrid();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string SqlQuery = "";

        SqlQuery = "EXEC SPDEPOSITEGL @BRCD = '" + Session["BRCD"].ToString() + "'  , @DepCode = '" + txtDepCode.Text.ToString() + "', @DepType = '" + txtDepType.Text.ToString() + "', @Category = '" + ddlCategory.SelectedValue.ToString() + "', @RepName = '" + txtRepName.Text.ToString() + "', @Status = '" + ddlStatus.SelectedValue.ToString() + "', @Flag = '" + Request.QueryString["Flag"].ToString() + "'";

        int RM = conn.sExecuteQuery(SqlQuery);

        if (RM > 0)
        {
            lblMessage.Text = "Successfully Deleted..!";
            ModalPopup.Show(this.Page);
            ClearData();
            BindGrid();
        }
    }

    protected void txtDepCode_TextChanged(object sender, EventArgs e)
    {
        string SqlQuery = "";
        SqlQuery = "SELECT G.SUBGLCODE, G.GLNAME, D.DEPOSITTYPE, ISNULL(D.REPORTNAME, '') AS REPORTNAME, ISNULL(D.STATUS, 0) AS STATUS FROM GLMAST G "
                   +"INNER JOIN DEPOSITGL D ON D.DEPOSITGLCODE = G.SUBGLCODE WHERE G.GLCODE = 5 AND G.SUBGLCODE = '"+ txtDepCode.Text.ToString() +"' ";
        DT = new DataTable();
        DT = conn.GetDatatable(SqlQuery);

        if (DT.Rows.Count > 0)
        {
            txtDepCode.Enabled = false;
            txtDepCode.Text = DT.Rows[0]["SUBGLCODE"].ToString();
            txtDepType.Text = DT.Rows[0]["GLNAME"].ToString();
            ddlCategory.Text = DT.Rows[0]["DEPOSITTYPE"].ToString();
            txtRepName.Text = DT.Rows[0]["REPORTNAME"].ToString();
            ddlStatus.Text = DT.Rows[0]["STATUS"].ToString();
        }
        else
        {
            lblMessage.Text = "No Record Available..!";
            ModalPopup.Show(this.Page);
        }
    }
}