using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCustWiseBalance : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCustWiseBalance SB = new ClsCustWiseBalance();
    DataTable DT = new DataTable();
    int IntRes = 0;
    string STrRes = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        TxtAsOnDate.Focus();
        TxtAsOnDate.Text = Session["EntryDate"].ToString();
        BindBranch();
        DdlBrName.SelectedValue = Session["BRCD"].ToString();
        TxtBrcd.Text = Session["BRCD"].ToString();
        DivGlData.Visible = false;
    }
    public void BindBranch()
    {
        try
        {
            BD.BindBRANCHNAME(DdlBrName, null);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DdlBrName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtBrcd.Text = "";

            TxtBrcd.Text = DdlBrName.SelectedValue.ToString();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrcd.Text + "&ASONDATE=" + TxtAsOnDate.Text + "&rptname=RptCustWiseBalance.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvRow in this.GrdInsert.Rows)
            {
                TextBox TS1 = (TextBox)gvRow.FindControl("TxtS1") as TextBox;
                TextBox TS2 = (TextBox)gvRow.FindControl("TxtS2") as TextBox;
                TextBox TS3 = (TextBox)gvRow.FindControl("TxtS3") as TextBox;
                TextBox TS4 = (TextBox)gvRow.FindControl("TxtS4") as TextBox;
                TextBox TS5 = (TextBox)gvRow.FindControl("TxtS5") as TextBox;
                TextBox TS6 = (TextBox)gvRow.FindControl("TxtS6") as TextBox;
                TextBox TS7 = (TextBox)gvRow.FindControl("TxtS7") as TextBox;
                TextBox TS8 = (TextBox)gvRow.FindControl("TxtS8") as TextBox;
                TextBox TS9 = (TextBox)gvRow.FindControl("TxtS9") as TextBox;
                TextBox TS10 = (TextBox)gvRow.FindControl("TxtS10") as TextBox;

                IntRes = SB.UpdateSBGL("MOD", "0", Session["MID"].ToString(), TS1.Text, TS2.Text, TS3.Text, TS4.Text, TS5.Text, TS6.Text, TS7.Text, TS8.Text, TS9.Text, TS10.Text);
                if (IntRes != null && IntRes > 0)
                {
                    WebMsgBox.Show("Updated successfully....!", this.Page);
                    BindGrid();
                }
                else
                {
                    WebMsgBox.Show("Failed to update....!", this.Page);
                    BindGrid();
                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void EmptyGridBind()
    {
        GrdInsert.DataSource = Enumerable.Range(1, 1).Select(a => new
        {
            ID = a,
            Name = String.Format("Test Name {0}", a)
        });
        GrdInsert.DataBind();
    }
    public void BindGrid()
    {
        try
        {
            SB.GetSbglPara(GrdInsert, "SEL", "0");// HARCODED BRCD for getting Bankwise record 

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ChkChange_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ChkChange.Checked == true)
            {
                DivGlData.Visible = true;
                BindGrid();
                foreach (GridViewRow gvRow in this.GrdInsert.Rows)
                {
                    TextBox TextBox1 = (TextBox)gvRow.FindControl("TxtS3") as TextBox;
                    TextBox1.Focus();
                }

            }
            else
            {
                DivGlData.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}