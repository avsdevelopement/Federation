using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;


public partial class FrmInstrumentIssue : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsInstrument intru = new ClsInstrument();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
            ViewState["Flag"] = "IS";
            Submit.Text = "Issue";
      }
    }
    protected void lnkAuthorize_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "AT";
        Submit.Text = "AUTHORIZE";
        cleardata();
    }
    protected void lnkIssue_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "IS";
        Submit.Text = "Issue";
          cleardata();

    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton objbtn = (LinkButton)sender;
        string strid = objbtn.CommandArgument;
        
        ViewState["Flag"] = "DL";
        int RC = intru.delete(strid.ToString(),Session["BRCD"].ToString());
        if (RC > 0)
        {
            BindGrid();
            WebMsgBox.Show(" Record Successfully Deleted", this.Page);
            cleardata();
            return;
        }
          cleardata();
    }
    protected void lnkReport_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "RP";
    }
    protected void lnkClose_Click(object sender, EventArgs e)
    {
       
    }
    protected void txttyp_TextChanged(object sender, EventArgs e)
    {
        txttypnam.Text = intru.Getacctype(txttyp.Text, Session["BRCD"].ToString());
        txtaccno.Focus();
    }
  
    protected void Submit_Click(object sender, EventArgs e)
    {
        string acctype = txttyp.Text.ToString();
        string brcd = Session["BRCD"].ToString();
        string accno = txtaccno.Text.ToString();
        string noleaves = txtnoleaves.Text.ToString();
        string fromno = txtfrmno.Text.ToString();
        string tono = txttono.Text.ToString();
        string issuedate = Session["EntryDate"].ToString();


       

        if (ViewState["Flag"].ToString() == "IS")
        {
            int RC = intru.insert(acctype, brcd, accno, noleaves, fromno, tono, issuedate);
            if (RC > 0)
            {
                BindGrid();
                WebMsgBox.Show(" Record Successfully Added", this.Page);
                BindGrid();
                cleardata();
                return;

            }


        }
        else if (ViewState["Flag"].ToString() == "DL")
        {
            int RC = intru.delete(ViewState["strnumId"].ToString(),Session["BRCD"].ToString());
            if (RC > 0)
            {
                BindGrid();
                WebMsgBox.Show(" Record Successfully Deleted", this.Page);
                cleardata();
                return;
            }
        }


        else if (ViewState["Flag"].ToString() == "AT")
        {
            int RC = intru.authorize(accno, Session["BRCD"].ToString());
            if (RC > 0)
            {
                BindGrid();
                WebMsgBox.Show(" Record Successfully Authorized", this.Page);
                cleardata();
                return;
            }
        }


        string AT = intru.GetStage(Session["BRCD"].ToString(), txtaccno.Text);
        
        if (AT == "1003")
        {
           
            if (ViewState["Flag"].ToString() == "DL")
            {
                WebMsgBox.Show("Record Already Authorized  Cannot Delete!", this.Page);
                return;
            }
            else if (ViewState["Flag"].ToString() == "AT")
            {
                WebMsgBox.Show("Record Already Authorized!", this.Page);
                return;
            }
        }
        else if (AT == "1004")
        {
            WebMsgBox.Show("Record Is Already Deleted!", this.Page);
            return;
        }

     

    }
    public void BindGrid()
    {
        int RS = intru.BindADD(GridView, Session["BRCD"].ToString());
    }
    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void txtaccno_TextChanged1(object sender, EventArgs e)
    {


        if (ViewState["Flag"].ToString() == "IS")
        {
            txtcustnam.Text = intru.getname(Session["BRCD"].ToString(), txtaccno.Text, txttyp.Text);
            txtnoleaves.Focus();
        }


        if (ViewState["Flag"].ToString() != "IS")
        {
            dt = intru.GetInfo( Session["BRCD"].ToString(),txtaccno.Text);
            txtcustnam.Text = intru.getname(Session["BRCD"].ToString(), txtaccno.Text, txttyp.Text);
            txtnoleaves.Text = dt.Rows[0]["LEAVE"].ToString();
            txtfrmno.Text = dt.Rows[0]["INTFROM"].ToString();
            txttono.Text = dt.Rows[0]["INTTO"].ToString();
            txttyp.Text = dt.Rows[0]["SUBGLCODE"].ToString();
            txttypnam.Text = intru.Getacctype(txttyp.Text, Session["BRCD"].ToString());
        }

    }
        public void cleardata()
        {
            txttyp.Text="";
            txttypnam.Text="";
            txtaccno.Text="";
            txtcustnam.Text="";
            txtnoleaves.Text="";
            txtfrmno.Text="";
            txttono.Text="";
        }
}


