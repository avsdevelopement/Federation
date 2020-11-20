using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmBranch : System.Web.UI.Page
{
    ClsBindDropdown bind = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    int Result;
    clsbanklookup bp = new clsbanklookup();
    string Flag;

    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string Flag;
        Flag = Request.QueryString["Flag"].ToString();
        try
        {
            
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                bind.BindState(ddlstate);
                BindGrid();
                DT = new DataTable();

                if (Flag == "AD")
                {
                   // Autoid();

                    btnSubmit.Visible = true;
                    btnModify.Visible = false;
                    btnDelete.Visible = false;

                }
                else if (Flag == "MD")
                {

                    BindGrid();
                    btnSubmit.Visible = false;
                    btnModify.Visible = true;
                    btnDelete.Visible = false;
                }
                else if (Flag == "DL")
                {

                    BindGrid();
                    btnSubmit.Visible = false;
                    btnModify.Visible = false;
                    btnDelete.Visible = true;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }

    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bind.BindDistrict(ddldistrict, ddlstate.SelectedValue);
            ddldistrict.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ClearData()
    {
        txtbankcode.Text = "";
        txtbankname.Text = "";
        txtzone.Text = "";
        ddlstate.SelectedItem.Text = "";
        ddlstate.Text = "";
        txtbranchcode.Text = "";
        txtbranchname.Text = "";
    }
    public void ENDN(bool TF)
    {
        txtbankname.Enabled = TF;
        txtbankcode.Enabled = TF;
        txtzone.Enabled = TF;

    }
    public int BindGrid()
    {

        int RC = bp.bindbranch(grdBank);
        return RC;
    }
    protected void grdbranch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBank.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "D";
        LinkButton lnkdelete = (LinkButton)sender;
        btnSubmit.Text = "delete";
        string ID = lnkdelete.CommandArgument;
        string[] bb = ID.ToString().Split('_');

        ViewState["BRANCHCD"] = bb[0].ToString();
        ViewState["BANKCD"] = bb[1].ToString();

      

    }

    protected void grdBank_SelectedIndexChanged(object sender, EventArgs e)
      {
        try
        {
            data();

            if (ViewState["Flag"].ToString() == "D")
            {
                Result = bp.deletebranch(txtbankcode.Text, txtbranchcode.Text);
                if (Result > 0)
                {
                    BindGrid();
                    WebMsgBox.Show("Record Delete Successfully...!!", this.Page);
                    ClearData();
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void grdBank_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBank.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtzone.Text == "")
        {
            WebMsgBox.Show("pls Enter Zone ....!!", this.Page);
        }
        else
        {
            Result = bp.insertbranch(ddlstate.SelectedValue, ddldistrict.SelectedItem.Text, txtzone.Text, txtbankcode.Text, txtbranchcode.Text, txtbranchname.Text, txtbankcode.Text, txtbranchcode.Text);
            if (Result > 0)
            {
                BindGrid();
                WebMsgBox.Show("Data Added successfully..!!", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Branch_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
                return;
            }
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            
            Result = bp.ModifyBranch(ddlstate.SelectedValue, ddldistrict.SelectedItem.Text, txtzone.Text, txtbankcode.Text, txtbranchcode.Text, txtbranchname.Text);
            if (Result > 0)
            {
                BindGrid();
                WebMsgBox.Show("Data Modify Successfully...", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Branch_Modify _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
                return;
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Result = bp.deletebranch(txtbankcode.Text, txtbranchcode.Text);
        if (Result > 0)
        {
            BindGrid();
            WebMsgBox.Show("Data Deleted Successfully...", this.Page);
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Branch_Delete _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            ClearData();
            return;
        }

    }
    public void data()
    {
        DataTable DT = new DataTable();
        DT = bp.getbranch(ViewState["BANKCD"].ToString(), ViewState["BRANCHCD"].ToString());
        if (DT.Rows.Count > 0)
        {
            txtbranchcode.Text = DT.Rows[0]["BRANCHCD"].ToString();
            txtbankcode.Text = DT.Rows[0]["BANKCD"].ToString();
                txtzone.Text=DT.Rows[0]["ZONETIME"].ToString();
            txtbranchname.Text=DT.Rows[0]["DESCR"].ToString();
            ddldistrict.Text=DT.Rows[0]["DISTRICT"].ToString();
     
        }
        else
        {
            WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
        }


    }
    
    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "M";
            btnSubmit.Text = "Modify";
            LinkButton lnkedit = (LinkButton)sender;
            string ID = lnkedit.CommandArgument;
            string[] bb = ID.ToString().Split('_');

            ViewState["BRANCHCD"] = bb[0].ToString();
            ViewState["BANKCD"] = bb[1].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtzone.Focus();
    }
    protected void txtbankcode_TextChanged(object sender, EventArgs e)
    {
       string Result = bp.getbankname(txtbankcode.Text);
       txtbankname.Text = Result;
        txtbankname.Focus();
    }
    protected void txtbranchcode_TextChanged(object sender, EventArgs e)
    {
        
        string  Result = bp.checkbranch(txtbankcode.Text, txtbranchcode.Text);
        string RC = bp.getbranchname(txtbankcode.Text, txtbranchcode.Text);
        txtbranchname.Text = RC;
        if (Result == txtbranchcode.Text)
        {
            WebMsgBox.Show("Record is Allready exist....!!", this.Page);
            txtbranchname.Enabled = false;
            btnSubmit.Enabled = false;

        }
        else
        {
            btnSubmit.Enabled = true;
            txtbranchname.Enabled = true;
            txtbranchname.Focus();
        }
       
            
    }
}