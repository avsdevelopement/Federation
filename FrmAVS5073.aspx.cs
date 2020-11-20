using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;


public partial class FrmAVS5073 : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsAVS5073 avs5073 = new ClsAVS5073();
    string FL = "";
    int result = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if(Request.QueryString["Flag"]!=null)
                ViewState["Flag"] = Request.QueryString["Flag"].ToString();
                DataTable DT = new DataTable();
                DT = avs5073.getCount(Session["Entrydate"].ToString(), "DASH", Session["BRCD"].ToString());
                if (DT.Rows.Count > 0)
                {
                    lblTT.Text = Convert.ToInt32(DT.Rows[0]["TT"].ToString() == "" ? "0" : DT.Rows[0]["TT"]).ToString("0");
                    lblSS.Text = Convert.ToInt32(DT.Rows[0]["SS"].ToString() == "" ? "0" : DT.Rows[0]["SS"]).ToString("0");
                    lblMN.Text = Convert.ToInt32(DT.Rows[0]["MN"].ToString() == "" ? "0" : DT.Rows[0]["MN"]).ToString("0");
                    Label1.Text = Convert.ToInt32(DT.Rows[0]["WelCome"].ToString() == "" ? "0" : DT.Rows[0]["WelCome"]).ToString("0");
                    Label2.Text = Convert.ToInt32(DT.Rows[0]["Payment"].ToString() == "" ? "0" : DT.Rows[0]["Payment"]).ToString("0");
                    Label3.Text = Convert.ToInt32(DT.Rows[0]["Receipt"].ToString() == "" ? "0" : DT.Rows[0]["Receipt"]).ToString("0");
                    Label4.Text = Convert.ToInt32(DT.Rows[0]["BirthDaySMS"].ToString() == "" ? "0" : DT.Rows[0]["BirthDaySMS"]).ToString("0");
                    Label5.Text = Convert.ToInt32(DT.Rows[0]["LoanOverDue"].ToString() == "" ? "0" : DT.Rows[0]["LoanOverDue"]).ToString("0");
                    Label6.Text = Convert.ToInt32(DT.Rows[0]["BeforeMaturity"].ToString() == "" ? "0" : DT.Rows[0]["BeforeMaturity"]).ToString("0");
                    Label7.Text = Convert.ToInt32(DT.Rows[0]["OnMaturity"].ToString() == "" ? "0" : DT.Rows[0]["OnMaturity"]).ToString("0");
                    Label8.Text = Convert.ToInt32(DT.Rows[0]["LoanSanc"].ToString() == "" ? "0" : DT.Rows[0]["LoanSanc"]).ToString("0");
                }
                else
                {
                    lblTT.Text =  "0";
                    lblSS.Text =  "0";
                    lblMN.Text =  "0";
                    Label1.Text = "0";
                    Label2.Text = "0";
                    Label3.Text = "0";
                    Label4.Text = "0";
                    Label5.Text = "0";
                    Label6.Text = "0";
                    Label7.Text = "0";
                    Label8.Text = "0";
                }
                BINDGRID();
                
            }
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }
    public void BINDGRID()
    {
        try
        {
            if (ViewState["Flag"].ToString() == "TT")
            {
                avs5073.getdata(grdDetails, Session["Entrydate"].ToString(), "TT", Session["BRCD"].ToString());
                divDetails.Visible = true;
            }
            else if (ViewState["Flag"].ToString() == "SS")
            {
                avs5073.getdata(grdDetails, Session["Entrydate"].ToString(), "SS", Session["BRCD"].ToString());
                divDetails.Visible = true;
            }
            else if (ViewState["Flag"].ToString() == "MN")
            {
                avs5073.getdata(grdDetails, Session["Entrydate"].ToString(), "MN", Session["BRCD"].ToString());
                divDetails.Visible = true;
            }
            else
            {
                avs5073.getdata1(grdDetails, Session["Entrydate"].ToString(), ViewState["Flag"].ToString(), Session["BRCD"].ToString());
                divDetails.Visible = true;
            }
            
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      //  grdDetails.PageIndex = e.NewPageIndex;
        //BINDGRID();
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}