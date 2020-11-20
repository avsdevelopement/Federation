using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmTrialBalance : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsTrailBalance TB = new ClsTrailBalance();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCommon cmn = new ClsCommon();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            TxtBrID.Text = Session["BRCD"].ToString();
            // TxtTDate.Text = Session["EntryDate"].ToString();
            //added by ankita 07/10/2017 to make user frndly 
            TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            TxtTDate.Text = Session["EntryDate"].ToString();
            if (cmn.MultiBranch(Session["LOGINCODE"].ToString()) != "Y")
            {
                TxtBrID.Enabled = false;
                TxtFDate.Focus();
            }
            else
            {
                TxtBrID.Enabled = true;
                TxtBrID.Focus();
            }
        }
    }
    protected void GrdTrailBalance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        BindData();
        FL = "Insert";//ankita 15/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "TrialBal_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
    }
    public void BindData()
    {
        int retrow = 0;
        DataTable dt = new DataTable();
        string FDT;
        if (TxtFDate.Text == "")
        {
            FDT = "01/01/1990";
        }
        else
        {
            FDT = TxtFDate.Text;
        }
        dt = TB.GetInfo(FDT, TxtTDate.Text, TxtBrID.Text, rdbAll.Checked == true ? "Y" : "N", rdbcode.Checked == true ? "C" : "N");
        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                GrdEmployeeDetails.DataSource = dt;
                GrdEmployeeDetails.DataBind();
                retrow = dt.Rows.Count;
            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                GrdEmployeeDetails.DataSource = dt;
                GrdEmployeeDetails.DataBind();
                int TotalColumns = GrdEmployeeDetails.Rows[0].Cells.Count;
                GrdEmployeeDetails.Rows[0].Cells.Clear();
                GrdEmployeeDetails.Rows[0].Cells.Add(new TableCell());
                GrdEmployeeDetails.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                GrdEmployeeDetails.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                GrdEmployeeDetails.Rows[0].Cells[0].Text = "No Record Found";
            }
        }
    }
    protected void GrdEmployeeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void GrdEmployeeDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            //HeaderCell2.Style["font-weight"] = "bold";
            //HeaderCell2.Text = "SRNO";
            //HeaderCell2.RowSpan = 2;
            //HeaderRow.Cells.Add(HeaderCell2);
            //HeaderCell2.VerticalAlign = VerticalAlign.Middle;

            HeaderCell2 = new TableCell();
            HeaderCell2.Style["font-weight"] = "bold";
            HeaderCell2.Text = "Glcode";
            HeaderCell2.RowSpan = 2;
            HeaderRow.Cells.Add(HeaderCell2);

            HeaderCell2 = new TableCell();
            HeaderCell2.Style["font-weight"] = "bold";
            HeaderCell2.Text = "Product Code";
            HeaderCell2.RowSpan = 2;
            HeaderRow.Cells.Add(HeaderCell2);

            HeaderCell2 = new TableCell();
            HeaderCell2.Style["font-weight"] = "bold";
            HeaderCell2.Text = "Description";
            HeaderCell2.RowSpan = 2;
            HeaderRow.Cells.Add(HeaderCell2);

            HeaderCell2 = new TableCell();
            HeaderCell2.Style["font-weight"] = "bold";
            HeaderCell2.Text = "Opening Balance";
            HeaderCell2.RowSpan = 2;
            HeaderRow.Cells.Add(HeaderCell2);

            HeaderCell2 = new TableCell();
            HeaderCell2.Style["font-weight"] = "bold";
            HeaderCell2.Text = "Receipt";
            HeaderCell2.RowSpan = 2;
            HeaderRow.Cells.Add(HeaderCell2);

            HeaderCell2 = new TableCell();
            HeaderCell2.Style["font-weight"] = "bold";
            HeaderCell2.Text = "Payment";
            HeaderCell2.RowSpan = 2;
            HeaderRow.Cells.Add(HeaderCell2);

            HeaderCell2 = new TableCell();
            HeaderCell2.Style["font-weight"] = "bold";
            HeaderCell2.Text = "Closing Balance";
            HeaderCell2.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell2.RowSpan = 1;
            HeaderCell2.ColumnSpan = 2;
            HeaderRow.Cells.Add(HeaderCell2);

            GrdEmployeeDetails.Controls[0].Controls.AddAt(0, HeaderRow);

            GridViewRow HeaderRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderRow1.Style["font-weight"] = "bold";

            TableCell HeaderCell = new TableCell();
            HeaderCell = new TableCell();
            HeaderCell.Text = "Credit";
            HeaderRow1.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Debit";
            HeaderRow1.Cells.Add(HeaderCell);

            HeaderRow.Attributes.Add("class", "header");
            HeaderRow1.Attributes.Add("class", "header");
            GrdEmployeeDetails.Controls[0].Controls.AddAt(1, HeaderRow1);

        }
    }
    protected void GrdEmployeeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdEmployeeDetails.PageIndex = e.NewPageIndex;
    }
    protected void ReportV_Click(object sender, EventArgs e)
    {
        FL = "Insert";//ankita 15/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "TrialBal_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        //TxtFDate.Text, TxtTDate.Text, Session["BRCD"].ToString()
        string FDT;
        string CN = "";
        if (TxtFDate.Text == "")
        {
            FDT = "01/01/1990";
        }
        else
        {
            FDT = TxtFDate.Text;
        }
        
        CN = rdbcode.Checked == true ? "C" : "N";
        string YN = rdbAll.Checked == true ? "Y" : "N";
        if (TxtTDate.Text == Session["EntryDate"].ToString())
        {
            WebMsgBox.Show("DayEnd Not Complete...", this.Page);
        }
        else
        {
            if (RBTDeatils.Checked == true)
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&FDate=" + FDT + "&TDate=" + TxtTDate.Text + "&YN=" + CN + "&AF=" + YN + "&rptname=RptTrialBalance.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (RBTSummary.Checked == true)
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&FDate=" + FDT + "&TDate=" + TxtTDate.Text + "&YN=" + CN + "&AF=" + YN + "&rptname=RptTrialBalanceSummary.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (RBTDeatils_M.Checked == true)
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&FDate=" + FDT + "&TDate=" + TxtTDate.Text + "&YN=" + CN + "&AF=" + YN + "&rptname=RptTrialBalance_M.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (RBTSummary_M.Checked == true)
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&FDate=" + FDT + "&TDate=" + TxtTDate.Text + "&YN=" + CN + "&AF=" + YN + "&rptname=RptTrialBalanceSummary_M.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        
    }
    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        TDT.Visible = true;
        FDT.Visible = false;
    }
    protected void rdbSpecific_CheckedChanged(object sender, EventArgs e)
    {
        TDT.Visible = true;
        FDT.Visible = true;
    }
    protected void TextReport_Click(object sender, EventArgs e)
    {
        FL = "Insert";//ankita 15/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "TrialBal_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        List<object> lst = new List<object>();
        lst.Add("Trial Balance");
        lst.Add(TxtFDate.Text);
        lst.Add(TxtTDate.Text);
        lst.Add(rdbAll.Checked);
        lst.Add(rdbcode.Checked);
        TrialBalanceTxt repObj = new TrialBalanceTxt();
        repObj.RInit(lst);
        repObj.Start();
    }
    protected void RBTDeatils_CheckedChanged(object sender, EventArgs e)
    {
        RBTDeatils.Checked = true;
        RBTSummary.Checked = false;
        RBTDeatils_M.Checked = false;
        RBTSummary_M.Checked = false;
    }
    protected void RBTSummary_CheckedChanged(object sender, EventArgs e)
    {
        RBTDeatils.Checked = false;
        RBTSummary.Checked = true;
        RBTDeatils_M.Checked = false;
        RBTSummary_M.Checked = false;
    }
    protected void RBTDeatils_M_CheckedChanged(object sender, EventArgs e)
    {
        RBTDeatils.Checked = false;
        RBTSummary.Checked = false;
        RBTDeatils_M.Checked = true;
        RBTSummary_M.Checked = false;
    }
    protected void RBTSummary_M_CheckedChanged(object sender, EventArgs e)
    {
        RBTDeatils.Checked = false;
        RBTSummary.Checked = false;
        RBTDeatils_M.Checked = false;
        RBTSummary_M.Checked = true;
    }
}