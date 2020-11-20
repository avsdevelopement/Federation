using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmMainBank : System.Web.UI.Page
{
    ClsBindDropdown bind = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    int Result;
    clsbanklookup bp = new clsbanklookup();
    string FLAG;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }

                string Flag;
                Flag = Request.QueryString["Flag"].ToString();
                bind.BindState(ddlstate);
                BindGrid();
              
                DT = new DataTable();

     
                ViewState["Flag"] = Request.QueryString["Flag"].ToString();
                if (Flag == "AD")
                {
                    autoid();

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
        ddldistrict.Text = "";
        ddlstate.Text = "";
    }
    public void autoid()
    {
       string  Result = bp.autoidbank();
       txtbankcode.Text = Result;
    }
    public void ENDN(bool TF)
    {
        txtbankname.Enabled = TF;
        txtbankcode.Enabled = TF;
        txtzone.Enabled = TF;

    }
    
    public int BindGrid()
    {

        int RC = bp.bindgrid((txtbankcode.Text), grdBank);
        return RC;
    }
/*    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                if (txtbankcode.Text == "")
                {
                    WebMsgBox.Show("Please Enter The Description", this.Page);
                    return;
                }
                if (txtbankname.Text == "")
                {
                    WebMsgBox.Show("Please Enter The Description", this.Page);
                    return;
                }
                if (txtzone.Text == "")
                {
                    WebMsgBox.Show("Please Enter The Description", this.Page);
                    return;
                }
                string desc;
                desc = "";
                Result = bp.insertdata(txtbankcode.Text,txtbankname.Text,ddlstate.SelectedValue,ddldistrict.SelectedItem.Text,txtzone.Text);
                if (Result > 0)
                {
                    BindGrid();
                    WebMsgBox.Show("Data Added successfully..!!", this.Page);
                    return;
                }
            }
            else if (ViewState["Flag"].ToString() == "MD")
            {
                if (txtbankcode.Text == "")
                {
                    WebMsgBox.Show("Please Enter The Description", this.Page);
                    return;
                }
                if (txtbankname.Text == "")
                {
                    WebMsgBox.Show("Please Enter The Description", this.Page);
                    return;
                }
                if (txtzone.Text == "")
                {
                    WebMsgBox.Show("Please Enter The Description", this.Page);
                    return;
                }
                string DESC;
                DESC = "";
                Result = bp.ModifyData(txtbankcode.Text, txtbankname.Text, ddlstate.SelectedValue, ddldistrict.SelectedItem.Text, txtzone.Text);
                if (Result > 0)
                {
                    BindGrid();
                    WebMsgBox.Show("Data Modify Successfully...", this.Page);
                    return;
                }

            }
            else if (ViewState["Flag"].ToString() == "DL")
            {

                if (txtbankcode.Text == "")
                {
                    WebMsgBox.Show("Please Enter The Description", this.Page);
                    return;
                }
                if (txtbankname.Text == "")
                {
                    WebMsgBox.Show("Please Enter The Description", this.Page);
                    return;
                }
                if (txtzone.Text == "")
                {
                    WebMsgBox.Show("Please Enter The Description", this.Page);
                    return;
                }

                Result = bp.DeleteData(txtbankcode.Text);
                if (Result > 0)
                {
                    BindGrid();
                    WebMsgBox.Show("Data Deleted Successfully...", this.Page);
                    return;
                }

            }
           

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }*/
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "D";
        LinkButton lnkdelete = (LinkButton)sender;
        btnSubmit.Text = "delete";
        ViewState["BANKCD"] = lnkdelete.CommandArgument;

    }
    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "M";
        btnSubmit.Text = "Modify";
        LinkButton lnkedit = (LinkButton)sender;
        ViewState["BANKCD"] = lnkedit.CommandArgument;
    }
    protected void grdBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        data();

        if (ViewState["Flag"].ToString() == "D")
        {
            Result = bp.DeleteData(txtbankcode.Text);
            if (Result > 0)
            {
                BindGrid();
                WebMsgBox.Show("Record Delete Successfully...!!", this.Page);
                ClearData();
                return;
            }
        }

    }
    protected void grdBank_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBank.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        Result = bp.ModifyData(txtbankcode.Text, txtbankname.Text, ddlstate.SelectedValue, ddldistrict.SelectedItem.Text, txtzone.Text);
        if (Result > 0)
        {
            BindGrid();
            WebMsgBox.Show("Data Modify Successfully...", this.Page);
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Mainbank_Mod" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            ClearData();
            return;
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Result = bp.DeleteData(txtbankcode.Text);
        if (Result > 0)
        {
            BindGrid();
            WebMsgBox.Show("Data Deleted Successfully...", this.Page);
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Mainbank_Del" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            ClearData();
            return;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if ( txtbankname.Text == "")
        {
            WebMsgBox.Show("pls insert data first", this.Page);
        }
        else
        {
                try
                {
            Result = bp.insertdata(txtbankcode.Text, txtbankname.Text, ddlstate.SelectedValue, ddldistrict.SelectedItem.Text, txtzone.Text);
            if (Result > 0)
            {
                BindGrid();
                WebMsgBox.Show("Data Added successfully..!!", this.Page);
                FL = "Insert";//ankita 14/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Mainbank_Add" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
                autoid();
                return;
            }
                }
                catch(Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
                }

        }
    }
    public void data()
    {
        DataTable DT = new DataTable();
        DT = bp.showdata(ViewState["BANKCD"].ToString());
        if (DT.Rows.Count > 0)
        {
            txtbankcode.Text = DT.Rows[0]["BANKCD"].ToString();
            txtbankname.Text = DT.Rows[0]["DESCR"].ToString();
            txtzone.Text = DT.Rows[0]["ZONETIME"].ToString();
            ddldistrict.Text = DT.Rows[0]["DISTRICT"].ToString();
         
        }
        else
        {
            WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
        }


    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtzone.Focus();
    }
}