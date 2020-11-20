using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmUnoperativeAccountReport : System.Web.UI.Page
{
    ClsUnOperativeAccountsReport UOA = new ClsUnOperativeAccountsReport();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    public void BindGrid()
    {
        int Result;

        Result = UOA.BindGrid(grdUnOperativeAccts,"23" ,txtAsOnDate.Text, txtMonth.Text, txtFromBr.Text, txtToBr.Text);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }

    protected void grdUnOperativeAccts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdUnOperativeAccts.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}