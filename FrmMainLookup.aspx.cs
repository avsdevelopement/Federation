 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmMainLookup : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    clsLookupMaster LOOK = new clsLookupMaster();
    DataTable DT;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int Result;
    string Flag;
 

       

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
                    ViewState["Flag"] = Request.QueryString["Flag"].ToString();
                    if (Flag == "AD")
                    {
                        Autoid();
                        COUNTSR();
                        btnSubmit.Visible = true;
                        btnModify.Visible = false;
                        btnDelete.Visible = false;
                        BTNclear.Visible = true;
                        btnfinish.Visible = true;
                         BtnExit.Visible = true;
                    }
                    else if (Flag == "MD")
                    {
                        btnSubmit.Visible = false;
                        btnModify.Visible = true;
                        btnDelete.Visible = false;
                        BTNclear.Visible = true;
                        btnfinish.Visible = false;
                         BtnExit.Visible = true;
                }
                    else if (Flag == "DL")
                    {
                        btnSubmit.Visible = false;
                        btnModify.Visible = false;
                        btnDelete.Visible = true;
                        BTNclear.Visible = true;
                        btnfinish.Visible = false;
                         BtnExit.Visible = true;
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
       
        txtlno.Text = "";
        txtltype.Text = "";
        txtsrno.Text = "";
        txtdesc.Text = "";
        txtDESCRIPTIONMAR.Text = "";
        TXTREFNO.Text = "";
      
    }
    public void CLEAR50()
    {
        txtsrno.Text = "";
        txtdesc.Text = "";
        txtDESCRIPTIONMAR.Text = "";
        TXTREFNO.Text = "";
    }
    public string   Autoid()
    {
        string result = LOOK.autoid();
        txtlno.Text = result;
        return result;
    
    }

    public void BindGrid()
    {
        LOOK.bindgrid(txtlno.Text, Grdcrdrdetailes);

    }

    protected void Grdcrdrdetailes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grdcrdrdetailes.PageIndex = e.NewPageIndex;
        BindGrid();

    }
    protected void Grdcrdrdetailes_SelectedIndexChanged(object sender, EventArgs e)
    {
        data();
      
    }
    public void data()
    {
        DataTable DT = new DataTable();
        DT = LOOK.show1(ViewState["LNO"].ToString(),ViewState["SrNo"].ToString());
        if (DT.Rows.Count > 0)
        {
            txtlno.Text = DT.Rows[0]["LNO"].ToString();
           txtltype.Text = DT.Rows[0]["LTYPE"].ToString();
           txtsrno.Text = DT.Rows[0]["SRNO"].ToString();
           txtdesc.Text = DT.Rows[0]["DESCRIPTION"].ToString();
           txtDESCRIPTIONMAR.Text = DT.Rows[0]["DESCRIPTIONMAR"].ToString();
           TXTREFNO.Text = DT.Rows[0]["REFNO"].ToString();
            

        }
        else
        {
            WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
        }


    }
    public void COUNTSR()
    {
       string  R=LOOK.getdetails(txtlno.Text);
       txtsrno.Text = R.ToString();
    }
 
   
    protected void LNKSELECT_Click(object sender, EventArgs e)
    {
       

    }

    protected void lnkDelete_Click1(object sender, EventArgs e)
    {
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtltype.Text == "")
        {
            WebMsgBox.Show("pls Fill up information first..", this.Page);
        }
        else
        {

           int  Result = LOOK.SaveData(txtlno.Text,txtltype.Text,txtsrno.Text,txtdesc.Text,txtDESCRIPTIONMAR.Text,TXTREFNO.Text);
            if (Result > 0)
            {
                BindGrid();
                
                WebMsgBox.Show("Data Added successfully..!!", this.Page);
               
                COUNTSR();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Lookup _Add_" + txtlno.Text + "_" + txtsrno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                CLEAR50();
                return;

            }
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        Result = LOOK.ModifyData(txtlno.Text, txtltype.Text, txtsrno.Text, txtdesc.Text, txtDESCRIPTIONMAR.Text, TXTREFNO.Text);
        if (Result > 0)
        {
            BindGrid();
            WebMsgBox.Show("Data Modify Successfully...", this.Page);
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Lookup _Mod_" + txtlno.Text + "_" + txtsrno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            ClearData();
            return;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Result = LOOK.deletelookup(txtlno.Text,txtsrno.Text);
        if (Result > 0)
        {
            BindGrid();
            WebMsgBox.Show("Data Deleted Successfully...", this.Page);
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Lookup _Del_" + txtlno.Text + "_" + txtsrno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            ClearData();
            return;
        }
    }
    protected void LNKSELECT_Click1(object sender, EventArgs e)
    {
        LinkButton lnkedit = (LinkButton)sender;
        string str = lnkedit.CommandArgument.ToString();
        string[] ARR = str.Split(',');
        ViewState["LNO"] = ARR[0].ToString();
        ViewState["SrNo"] = ARR[1].ToString();
    }
   
    protected void BTNclear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void btnfinish_Click(object sender, EventArgs e)
    {
        ClearData();
        Autoid();
    }
    protected void txtlno_TextChanged(object sender, EventArgs e)
    {
        BindGrid();
        Grdcrdrdetailes.Visible = true;
        txtltype.Text = LOOK.GETLTYPE(txtlno.Text);
        txtsrno.Text = LOOK.GETSRNO(txtlno.Text);
    }
    protected void lnkedit_Click(object sender, EventArgs e)
    {
        LinkButton lnkedit = (LinkButton)sender;
        string str = lnkedit.CommandArgument.ToString();
        string[] ARR = str.Split(',');
        ViewState["LNO"] = ARR[0].ToString();
        ViewState["SrNo"] = ARR[1].ToString();
    }

    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
}