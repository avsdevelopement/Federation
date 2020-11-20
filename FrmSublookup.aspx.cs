using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmSublookup : System.Web.UI.Page
{
    clsLookupMaster look = new clsLookupMaster();
    DbConnection conn = new DbConnection();

    DataTable DT;
    int Result;
    string FLAG;
    protected void Page_Load(object sender, EventArgs e)
    {
        string Flag;
        Flag = Request.QueryString["Flag"].ToString();
        try
        {
            if (!IsPostBack)
            {

                BindGrid();

                ViewState["Flag"] = Request.QueryString["Flag"].ToString();
                if (ViewState["Flag"].ToString() == "AD")
                {
                    if (Flag == "AD")
                    {
                        // Autoid();

                        btnSubmit.Visible = true;
                        btnModify.Visible = false;
                        btnDelete.Visible = false;
                        BtnExit.Visible = true;
                    }
                    else if (Flag == "MD")
                    {

                        BindGrid();
                        btnSubmit.Visible = false;
                        btnModify.Visible = true;
                        btnDelete.Visible = false;
                        BtnExit.Visible = true;
                    }
                    else if (Flag == "DL")
                    {

                        BindGrid();
                        btnSubmit.Visible = false;
                        btnModify.Visible = false;
                        btnDelete.Visible = true;
                        BtnExit.Visible = true;
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        
      
    }
    public void ClearData()
    {
        txtltype.Text = "";
        txtlno.Text = "";
        txtdesc.Text = "";
    }
    public void ENDN(bool TF)
    {
      
        txtdesc.Enabled = TF;
        txtltype.Enabled = TF;
        txtlno.Enabled = TF;



    }

 
    protected void txtlno_TextChanged(object sender, EventArgs e)
    {
        string RC = look.getdata(txtlno.Text);
        txtltype.Text = RC;

    }
    public int  BindGrid()
    {

        int RC = look.bindgrid(Grdcrdrdetailes);
        return RC;
    }
    protected void Grdcrdrdetailes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grdcrdrdetailes.PageIndex = e.NewPageIndex;
        BindGrid();
    }
 

    protected void lnkselect_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "M";
        btnSubmit.Text = "Modify";
        LinkButton lnkedit = (LinkButton)sender;
        ViewState["LNO"] = lnkedit.CommandArgument;

    }
   public void data()
    {
        DataTable DT = new DataTable();
        DT = look.showsub(ViewState["LNO"].ToString());
        if (DT.Rows.Count > 0)
        {
            txtlno.Text = DT.Rows[0]["LNO"].ToString();
            txtltype.Text = DT.Rows[0]["LTYPE"].ToString();
            txtdesc.Text = DT.Rows[0]["DESCRIPTION"].ToString();
          
        }
        else
        {
            WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
        }


    }
    protected void Grdcrdrdetailes_SelectedIndexChanged(object sender, EventArgs e)
    {
        data();

        if (ViewState["Flag"].ToString() == "D")
        {
            Result = look.DeleteData(Convert.ToInt32(txtlno.Text));
            if (Result > 0)
            {
                BindGrid();
                WebMsgBox.Show("Record Delete Successfully...!!", this.Page);
                ClearData();
                return;
            }
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "D";
        LinkButton lnkdelete = (LinkButton)sender;
        btnSubmit.Text = "delete";
        ViewState["LNO"] = lnkdelete.CommandArgument;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtlno.Text == "" && txtdesc.Text == "")
        {
            WebMsgBox.Show("pls fill up required information", this.Page);
        }
        else
        {
            Result = look.savelookup(Convert.ToInt32(txtlno.Text), txtltype.Text, txtdesc.Text);
            if (Result > 0)
            {
                BindGrid();
                WebMsgBox.Show("Data Added successfully..!!", this.Page);
                ClearData();
                return;
            }
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        Result = look.Modifysublook(Convert.ToInt32(txtlno.Text), txtdesc.Text,txtltype.Text);
        if (Result > 0)
        {
            BindGrid();
            WebMsgBox.Show("Data Modify Successfully...", this.Page);
            ClearData();
            return;
       
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Result = look.DeleteData(Convert.ToInt32(txtlno.Text));
        if (Result > 0)
        {
            BindGrid();
            WebMsgBox.Show("Data Deleted Successfully...", this.Page);
            ClearData();
            ClearData();
            return;
           
        }
    }

    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
}