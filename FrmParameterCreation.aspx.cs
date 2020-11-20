using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmParameterCreation : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    clsparameterCreation pc = new clsparameterCreation();
    DataTable DT;
    int Result;
    string Flag;
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

                BindGrid();
                pc.bindbank(ddlbank,Session["BRCD"].ToString());
               
                 string Flag;
                 Flag = Request.QueryString["Flag"].ToString();
                if (Flag == "AD")
                {
                    //Autoid();

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
    public int BindGrid()
    {

        int RC = pc.bindgrid(grdparameter,Session["BRCD"].ToString());
        return RC;
    }
    public void ClearData()
    {
        txtlistfd.Text = "";
        txtlistvalue.Text = "";
        txtBranchcode.Text = "";
    }
    public void ENDN(bool TF)
    {
        txtlistfd.Enabled = TF;
        txtlistvalue.Enabled = TF;
        txtBranchcode.Enabled = TF;
        



    }
  
    
  
  /*  protected void btnsubmit_Click(object sender, EventArgs e)
    {


        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                if (txtlistfd.Text == "")
                {
                    WebMsgBox.Show("Please Enter The list field", this.Page);
                    return;
                }
                if (txtlistvalue.Text == "")
                {
                    WebMsgBox.Show("Please Enter The list value", this.Page);
                    return;
                }

                string desc;
                desc = "";
                Result = pc.inseretdata(txtlistfd.Text,txtlistvalue.Text,txtBranchcode.Text);
                if (Result > 0)
                {
                    BindGrid();
                    WebMsgBox.Show("Data Added successfully..!!", this.Page);
                    return;
                }
            }
            else if (ViewState["Flag"].ToString() == "MD")
            {
                if (txtlistfd.Text == "")
                {
                    WebMsgBox.Show("Please Enter The list field", this.Page);
                    return;
                }
                if (txtlistvalue.Text == "")
                {
                    WebMsgBox.Show("Please enter list value !!", this.Page);
                    return;
                }
                string DESC;
                DESC = "";
                Result = pc.changedata(txtlistfd.Text, txtlistvalue.Text, ddlbank.SelectedValue);
                if (Result > 0)
                {
                    BindGrid();
                    WebMsgBox.Show("Data Modify Successfully...", this.Page);
                    return;
                }

            }
            else if (ViewState["Flag"].ToString() == "DL")
            {

                if (txtlistfd.Text == "")
                {
                    WebMsgBox.Show("Please enter list field !!", this.Page);
                    return;
                }
                if (txtlistvalue.Text == "")
                {
                    WebMsgBox.Show("Please enterlist value !!", this.Page);
                    return;
                }

                Result = pc.DeleteData(txtlistfd.Text);
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
   

    protected void ddlbank_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtBranchcode.Text = ddlbank.SelectedValue;
    }
   
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "D";
        LinkButton lnkdelete = (LinkButton)sender;
        btnSubmit.Text = "delete";
        ViewState["LISTFIELD"] = lnkdelete.CommandArgument;
    }
    protected void lnkselect_Click(object sender, EventArgs e)
    {
        
        ViewState["Flag"] = "M";
       // btnSubmit.Text = "Modify";
        btnSubmit.Visible = false;
        LinkButton lnkedit = (LinkButton)sender;
        ViewState["LISTFIELD"] = lnkedit.CommandArgument;
           
    }
    public void data()
    {
        DataTable DT = new DataTable();
        DT = pc.showpara(ViewState["LISTFIELD"].ToString(),Session["BRCD"].ToString());
        if (DT.Rows.Count > 0)
        {

            txtlistfd.Text = DT.Rows[0]["LISTFIELD"].ToString();
                txtlistvalue.Text=DT.Rows[0]["LISTVALUE"].ToString();
           
        }
        else
        {
            WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
        }


    }

    protected void grdparameter_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        data();
        if (ViewState["Flag"].ToString() == "D")
        {
            Result = pc.DeleteData(txtlistfd.Text,Session["BRCD"].ToString());
            if (Result > 0)
            {
                BindGrid();
                WebMsgBox.Show("Record Delete Successfully...!!", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ParameterCreatn _Del_" + txtlistfd.Text + "_" + txtlistvalue.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
                return;
            }
        }
       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtlistfd.Text == "" && txtlistvalue.Text == "")
        {
            WebMsgBox.Show("pls Fill up Information", this.Page);
        }
        else
        {
            Result = pc.inseretdata(txtlistfd.Text, txtlistvalue.Text, txtBranchcode.Text);
            if (Result > 0)
            {
                BindGrid();
                WebMsgBox.Show("Data Added successfully..!!", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ParameterCreatn _Add_" + txtlistfd.Text + "_" + txtlistvalue.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
                return;
            }
        }

    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        Result = pc.changedata(txtlistfd.Text, txtlistvalue.Text, ddlbank.SelectedValue);
        if (Result > 0)
        {
            BindGrid();
            WebMsgBox.Show("Data Modify Successfully...", this.Page);
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ParameterCreatn _Mod_" + txtlistfd.Text + "_" + txtlistvalue.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            ClearData();
            return;
        }


    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Result = pc.DeleteData(txtlistfd.Text,Session["BRCD"].ToString());
        if (Result > 0)
        {
            BindGrid();
            WebMsgBox.Show("Data Deleted Successfully...", this.Page);
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ParameterCreatn _Del_" + txtlistfd.Text + "_" + txtlistvalue.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            ClearData();
            return;
        }
    }
}