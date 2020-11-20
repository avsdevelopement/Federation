using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmInwordFunding : System.Web.UI.Page
{
    ClsFunddingDate FD = new ClsFunddingDate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    //protected void grdInwData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdInwData.PageIndex = e.NewPageIndex;
    //    BindGrid();
    //}
    public void BindGrid()
    {

        int RC = FD.Getinfotable(grdInwData, Session["BRCD"].ToString(), Session["EntryDate"].ToString(),"I");
        if (RC > 0)
        {

        }
    }

    protected void Fundding_Click(object sender, EventArgs e)
    {
        try
        {
            if (grdInwData.Rows.Count > 0)
            {
                int RC = 0;
                foreach (GridViewRow gvRow in this.grdInwData.Rows)
                {
                    int SetNO = Convert.ToInt32(((Label)gvRow.FindControl("SET_NO")).Text);
                    //int RC = FD.InsertData(Session["MID"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(),"I");
                    RC = FD.FundAuthorize(SetNO.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), "I");
                   
                }
                if (RC > 0)
                {
                    BindGrid();
                    WebMsgBox.Show("Funding Successfully Done......!!", this.Page);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}