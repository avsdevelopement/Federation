using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS5111 : System.Web.UI.Page
{
    ClsAVS5111 CS = new ClsAVS5111();
    DataTable DT = new DataTable();
    string table = "";
    string sResult = "";
    int Result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtBRCD.Text = Session["BRCD"].ToString();
            TxtBrname.Text = CS.getbrname(Session["BRCD"].ToString());

            TxtAccType.Focus();
        }
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 900000;
    }

    protected void TxtAccType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                string Glname = "";
                Glname = CS.GetAccType(TxtAccType.Text, TxtBRCD.Text);
                if (Glname != null)
                {
                    TxtATName.Text = Glname.ToString();
                }
                else
                {
                    WebMsgBox.Show("Agent Code Not found....!", this.Page);
                }
                txtMonth.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtAccType.Text = "";
                TxtBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text.ToString() == "")
            {
                TxtBRCD.Focus();
                WebMsgBox.Show("Enter branch code first ...!!", this.Page);
                return;
            }
            else if (TxtAccType.Text.ToString() == "")
            {
                TxtAccType.Focus();
                WebMsgBox.Show("Enter agent code first ...!!", this.Page);
                return;
            }
            else if (txtMonth.Text.ToString() == "")
            {
                txtMonth.Focus();
                WebMsgBox.Show("Enter selected month first ...!!", this.Page);
                return;
            }
            else if (txtYear.Text.ToString() == "")
            {
                txtYear.Focus();
                WebMsgBox.Show("Enter selected year first ...!!", this.Page);
                return;
            }
            else
            {
                DT = CS.GetAllData(TxtBRCD.Text.ToString(), TxtAccType.Text.ToString(), txtMonth.Text.ToString(), txtYear.Text.ToString());
                if (DT.Rows.Count > 0)
                {
                    divGridDetails.Visible = true;
                    GrdDaily.DataSource = DT;
                    GrdDaily.DataBind();
                }
                else
                {
                    divGridDetails.Visible = false;
                    GrdDaily.DataSource = null;
                    GrdDaily.DataBind();
                }
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            CLearAll();
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void CLearAll()
    {
        try
        {
            TxtAccType.Text = "";
            TxtATName.Text = "";
            txtMonth.Text = "";
            txtYear.Text = "";

            divGridDetails.Visible = false;
            GrdDaily.DataSource = null;
            GrdDaily.DataBind();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GrdDaily_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            var dropdownList = e.Row.FindControl("ddlChecking") as DropDownList;
            dropdownList.Items.Insert(0, new ListItem("Yes", "1"));
            dropdownList.Items.Insert(0, new ListItem("No", "0"));

            dropdownList.SelectedValue = ((HiddenField)e.Row.FindControl("HDStatus")).Value;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txtpassbook_TextChanged(object sender, EventArgs e)
    {
        double Result = 0;
        try
        {
            foreach (GridViewRow gvRow in this.GrdDaily.Rows)
            {
                string Check = Convert.ToString(((DropDownList)gvRow.FindControl("ddlChecking")).SelectedValue).ToString();
                if (Check.ToString() == "1")
                {
                    double Collection = Convert.ToDouble(((TextBox)gvRow.FindControl("TxtCollection")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtCollection")).Text);
                    double SPK = Convert.ToDouble(((TextBox)gvRow.FindControl("Txtpassbook")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtpassbook")).Text);
                    Result = Convert.ToDouble(Collection - SPK);
                    ((TextBox)gvRow.FindControl("txtDiff")).Text = Result.ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            //  Before insert delete all existing data for that agent only
            CS.DeletePassBook(TxtBRCD.Text.ToString(), TxtAccType.Text.ToString(), txtMonth.Text.ToString(), txtYear.Text.ToString());

            //  Insert new data
            foreach (GridViewRow gvRow in this.GrdDaily.Rows)
            {
                string Check = Convert.ToString(((DropDownList)gvRow.FindControl("ddlChecking")).SelectedValue).ToString();
                string SrNo = Convert.ToString(((TextBox)gvRow.FindControl("TxtSrno")).Text).ToString();
                string Accno = Convert.ToString(((TextBox)gvRow.FindControl("txtAccNo")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtAccNo")).Text).ToString();
                string Custname = Convert.ToString(((TextBox)gvRow.FindControl("txtName")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtName")).Text);
                double Opening = Convert.ToDouble(((TextBox)gvRow.FindControl("TxtOpening")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtOpening")).Text);
                double Collection = Convert.ToDouble(((TextBox)gvRow.FindControl("TxtCollection")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TxtCollection")).Text);
                double Psk = Convert.ToDouble(((TextBox)gvRow.FindControl("Txtpassbook")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("Txtpassbook")).Text);
                double Diif = Convert.ToDouble(((TextBox)gvRow.FindControl("txtDiff")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtDiff")).Text);

                Result = CS.InsertPassBook(TxtBRCD.Text, TxtAccType.Text, txtYear.Text, txtMonth.Text, SrNo.ToString(), Accno.ToString(), Custname.ToString(),
                    Opening.ToString(), Collection.ToString(), Psk.ToString(), (Collection - Psk).ToString(), Session["MID"].ToString(), Check.ToString());
            }

            if (Result > 0)
            {
                CLearAll();
                WebMsgBox.Show("Data Saved Successfully ...!!", this.Page);
                divGridDetails.Visible = false;
                GrdDaily.DataSource = null;
                GrdDaily.DataBind();
                return;
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClearAll_Click(object sender, EventArgs e)
    {
        try
        {
            divGridDetails.Visible = false;

            GrdDaily.DataSource = null;
            GrdDaily.DataBind();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit1_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if(TxtBRCD.Text.ToString() == "")
            {
                TxtBRCD.Focus();
                WebMsgBox.Show("Enter branch code first ...!!", this.Page);
                return;
            }
            else if (TxtAccType.Text.ToString() == "")
            {
                TxtAccType.Focus();
                WebMsgBox.Show("Enter agent code first ...!!", this.Page);
                return;
            }
            else if (txtMonth.Text.ToString() == "")
            {
                txtMonth.Focus();
                WebMsgBox.Show("Enter selected month first ...!!", this.Page);
                return;
            }
            else if (txtYear.Text.ToString() == "")
            {
                txtYear.Focus();
                WebMsgBox.Show("Enter selected year first ...!!", this.Page);
                return;
            }
            else
            {
                string redirectURL = "FrmReportViewer.aspx?BC=" + TxtBRCD.Text + "&AC=" + TxtAccType.Text + "&Month=" + txtMonth.Text + "&Year=" + txtYear.Text + "&rptname=RptAVS5111.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}