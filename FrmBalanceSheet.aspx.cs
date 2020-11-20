using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmBalanceSheet : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBalanceSheet BS = new ClsBalanceSheet();
    // ClsBindBrDetails BD = new ClsBindBrDetails();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCommon cmn = new ClsCommon();
    string FL = "";
    string CurrentLang = "";
    int CurrentLangCode = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            TxtBrID.Text = Session["BRCD"].ToString();
            //BindBranch();
            //TxtFDT.Text = Session["EntryDate"].ToString();
            //added by ankita 07/10/2017 to make user frndly
            txtFromDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            txtToDate.Text = Session["EntryDate"].ToString();
            TxtFDT.Text = Session["EntryDate"].ToString();
            if (cmn.MultiBranch(Session["LOGINCODE"].ToString()) != "Y")
            {
                TxtBrID.Enabled = false;
                txtFromDate.Focus();
            }
            else
            {
                TxtBrID.Enabled = true;
                TxtBrID.Focus();
            }
            rdbLangSelection.SelectedItem.Attributes.CssStyle.Add("Color", "Green");


        }
        rdbLangSelection.SelectedItem.Attributes.CssStyle.Add("Color", "Green");


    }
    //public void BindBranch ()
    // {
    //     BD.BindBranchDetails(ddlBrCode);
    //    ddlBrCode.Items.Insert(1, new ListItem("ALL", "0000"));
    // }
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
            GridAll.Visible = false;
            GrdBalance.Visible = true;
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "BalSheet_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    public void BindGrid()
    {
        try
        {

            int retrow = 0;
            string Dex1 = "";
            if (CHK_SKIP_BR.Checked == true)
            {
                Dex1 = "Y";
            }
            else if (CHK_SKIP_BR.Checked == false)
            {
                Dex1 = "N";
            }

            DataTable dt = new DataTable();

            dt = BS.Balance(Dex1, Session["BRCD"].ToString(), TxtFDT.Text, Session["UserName"].ToString());
            dt = BS.PreBalance(Dex1, Session["BRCD"].ToString(), txtFromDate.Text, txtToDate.Text, Session["UserName"].ToString());//Amruta 20170427
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    GrdBalance.DataSource = dt;
                    GrdBalance.DataBind();
                    retrow = dt.Rows.Count;
                }
                else
                {
                    dt.Rows.Add(dt.NewRow());
                    GrdBalance.DataSource = dt;
                    GrdBalance.DataBind();
                    int TotalColumns = GrdBalance.Rows[0].Cells.Count;
                    GrdBalance.Rows[0].Cells.Clear();
                    GrdBalance.Rows[0].Cells.Add(new TableCell());
                    GrdBalance.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                    GrdBalance.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    GrdBalance.Rows[0].Cells[0].Text = "No Record Found";
                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void GrdBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Style["font-weight"] = "bold";
                HeaderCell2.Text = "LABILITY";

                HeaderCell2.ColumnSpan = 6;
                HeaderRow.Cells.Add(HeaderCell2);
                HeaderCell2.VerticalAlign = VerticalAlign.Middle;

                HeaderCell2 = new TableCell();
                HeaderCell2.Style["font-weight"] = "bold";
                HeaderCell2.Text = "ASSET";
                HeaderCell2.ColumnSpan = 6;
                //HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);

                GrdBalance.Controls[0].Controls.AddAt(0, HeaderRow);

                GridViewRow HeaderRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderRow1.Style["font-weight"] = "bold";

                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Glcode";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Product Code";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Product Name";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "ID";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Group";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Balance";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Glcode";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Product Code";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Product Name";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "ID";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Group";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Balance";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderRow.Attributes.Add("class", "header");
                HeaderRow1.Attributes.Add("class", "header");
                GrdBalance.Controls[0].Controls.AddAt(1, HeaderRow1);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void Report_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "BalSheet_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string Dex1 = "";
            if (CHK_SKIP_BR.Checked == true)
            {
                Dex1 = "Y";
            }
            else if (CHK_SKIP_BR.Checked == false)
            {
                Dex1 = "N";
            }
            if (TxtFDT.Text == Session["EntryDate"].ToString())
            {
                WebMsgBox.Show("DayEnd Not Complete...", this.Page);

            }

            if (RBSel.SelectedValue == "1") //|| RBSel.SelectedValue == "2")
            {
                string redirectURL = "FrmRView.aspx?DEX=" + Dex1 + "&BRCD=" + TxtBrID.Text + "&FDate=" + TxtFDT.Text + "&rptname=RptBalanceS.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

            }
            else if (RBSel.SelectedValue == "3")
            {

                if (GetCurrentLangCode() == 0)
                {
                    string redirectURL = "FrmRView.aspx?DEX=" + Dex1 + "&BRCD=" + TxtBrID.Text + "&FDate=" + TxtFDT.Text + "&rptname=RptBalanceS_Marathi.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

                }
                if (GetCurrentLangCode() == 1)
                {
                    string redirectURL = "FrmRView.aspx?DEX=" + Dex1 + "&BRCD=" + TxtBrID.Text + "&FDate=" + TxtFDT.Text + "&rptname=RptBalanceSarjudas_Marathi.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

                }
                else
                {
                    string redirectURL = "FrmRView.aspx?DEX=" + Dex1 + "&BRCD=" + TxtBrID.Text + "&FDate=" + TxtFDT.Text + "&rptname=RptBalanceS_Marathi.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }

            }
            //else if (RBSel.SelectedValue == "4")
            //{
            //    string redirectURL = "FrmRView.aspx?DEX=" + Dex1 + "&BRCD=" + TxtBrID.Text + "&FDate=" + TxtFDT.Text + "&rptname=RptBalanceSarjudas_Marathi.rdlc";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void TextReport_Click(object sender, EventArgs e)
    {
        FL = "Insert";//ankita 15/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "BalSheet_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

        List<object> lst = new List<object>();
        lst.Add("BalanceSheet Report");
        lst.Add(Session["UserName"].ToString());
        lst.Add(Session["BRCD"].ToString());
        lst.Add(TxtFDT.Text);
        BalanceSheetReportTxt repObj = new BalanceSheetReportTxt();
        repObj.RInit(lst);
        repObj.Start();
    }
    protected void TxtBrID_TextChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridAll();
            GridAll.Visible = true;
            GrdBalance.Visible = false;

            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "BalSheet_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void btnBal_Click(object sender, EventArgs e)
    {
        try
        {
            //string Dex2 = "";
            //if (CHK_SKIP_BRN.Checked == true)
            //{
            //    Dex2 = "Y";
            //}
            //else if (CHK_SKIP_BRN.Checked == false)
            //{
            //    Dex2 = "N";
            //}

            //FL = "Insert";//ankita 15/09/2017
            //string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "BalSheet_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());


            //string redirectURL = "FrmRView.aspx?DEX=" + Dex2 + "&BRCD=" + TxtBrID.Text + "&FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&rptname=RptPBalanceS.rdlc";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

            string Dex2 = "";
            if (CHK_SKIP_BRN.Checked == true)
            {
                Dex2 = "Y";
            }
            else if (CHK_SKIP_BRN.Checked == false)
            {
                Dex2 = "N";
            }

            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "BalSheet_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            if (RBSel.SelectedValue == "2")
            {

                string redirectURL = "FrmRView.aspx?DEX=" + Dex2 + "&BRCD=" + TxtBrID.Text + "&FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&rptname=RptPBalanceS.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);


            }
            if (RBSel.SelectedValue == "4")
            {
                if (GetCurrentLangCode() == 0)
                {
                    string redirectURL = "FrmRView.aspx?DEX=" + Dex2 + "&BRCD=" + TxtBrID.Text + "&FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&rptname=RptPBalanceS_Marathi.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

                }
                else if (GetCurrentLangCode() == 1)
                {
                    string redirectURL = "FrmRView.aspx?DEX=" + Dex2 + "&BRCD=" + TxtBrID.Text + "&FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&rptname=RptPBalanceSarjudas_Marathi.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

                }

                else
                {
                    string redirectURL = "FrmRView.aspx?DEX=" + Dex2 + "&BRCD=" + TxtBrID.Text + "&FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&rptname=RptPBalanceS_Marathi.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }


            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void btnDown_Click(object sender, EventArgs e)
    {

    }
    protected void GridAll_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Style["font-weight"] = "bold";
                HeaderCell2.Text = "LABILITY";

                HeaderCell2.ColumnSpan = 7;
                HeaderRow.Cells.Add(HeaderCell2);
                HeaderCell2.VerticalAlign = VerticalAlign.Middle;

                HeaderCell2 = new TableCell();
                HeaderCell2.Style["font-weight"] = "bold";
                HeaderCell2.Text = "ASSET";
                HeaderCell2.ColumnSpan = 7;
                //HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);

                GridAll.Controls[0].Controls.AddAt(0, HeaderRow);

                GridViewRow HeaderRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderRow1.Style["font-weight"] = "bold";

                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Glcode";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Product Code";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Product Name";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "ID";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Group";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Prev Balance";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Balance";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Glcode";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Product Code";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Product Name";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "ID";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Group";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Prev Balance";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Balance";
                HeaderRow1.Cells.Add(HeaderCell);

                HeaderRow.Attributes.Add("class", "header");
                HeaderRow1.Attributes.Add("class", "header");
                GridAll.Controls[0].Controls.AddAt(1, HeaderRow1);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    public void BindGridAll()
    {
        try
        {

            int retrow = 0;
            DataTable dt = new DataTable();

            string Dex2 = "";
            if (CHK_SKIP_BRN.Checked == true)
            {
                Dex2 = "Y";
            }
            else if (CHK_SKIP_BRN.Checked == false)
            {
                Dex2 = "N";
            }

            dt = BS.PreBalance(Dex2, Session["BRCD"].ToString(), txtFromDate.Text, txtToDate.Text, Session["UserName"].ToString());//Amruta 20170427
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    GridAll.DataSource = dt;
                    GridAll.DataBind();
                    retrow = dt.Rows.Count;
                }
                else
                {
                    dt.Rows.Add(dt.NewRow());
                    GridAll.DataSource = dt;
                    GridAll.DataBind();
                    int TotalColumns = GridAll.Rows[0].Cells.Count;
                    GridAll.Rows[0].Cells.Clear();
                    GridAll.Rows[0].Cells.Add(new TableCell());
                    GridAll.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                    GridAll.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    GridAll.Rows[0].Cells[0].Text = "No Record Found";
                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void RBSel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RBSel.SelectedValue == "1")
        {
            txtCurrentLang.Visible = false;
            lblCurrentLang.Visible = false;
            divDate.Visible = false;
            divOnDate.Visible = true;
        }
        else if (RBSel.SelectedValue == "3")
        {
            txtCurrentLang.Visible = true;
            lblCurrentLang.Visible = true;
            divDate.Visible = false;
            divOnDate.Visible = true;
            txtCurrentLang.Text = GetCurrentLang();

        }
        else if (RBSel.SelectedValue == "2")
        {
            txtCurrentLang.Visible = false;
            lblCurrentLang.Visible = false;
            divDate.Visible = true;
            divOnDate.Visible = false;
        }

        else if (RBSel.SelectedValue == "4")
        {
            txtCurrentLang.Visible = true;
            lblCurrentLang.Visible = true;
            divDate.Visible = true;
            divOnDate.Visible = false;
            txtCurrentLang.Text = GetCurrentLang();

        }


    }
    protected void rdbLangSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // rdbLangSelection.SelectedItem.Attributes.Add("Style","color:red");
            rdbLangSelection.SelectedItem.Attributes.CssStyle.Add("Color", "Green");
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    private string GetCurrentLang()
    {
        try
        {
            CurrentLang = conn.sExecuteScalar("exec SpGetLangName @BRCD='" + Convert.ToString(Session["BRCD"]) + "',@Flag='GetName'");

        }
        catch (Exception Ex)
        {
            CurrentLang = "No Lang";
            ExceptionLogging.SendErrorToText(Ex);
        }
        return CurrentLang;

    }


    private int GetCurrentLangCode()
    {
        try
        {
            CurrentLangCode = Convert.ToInt32(conn.sExecuteScalar("exec SpGetLangName @BRCD='" + Convert.ToString(Session["BRCD"]) + "',@Flag='GetCode'"));

        }
        catch (Exception Ex)
        {
            CurrentLang = "0";
            ExceptionLogging.SendErrorToText(Ex);
        }
        return CurrentLangCode;

    }
}