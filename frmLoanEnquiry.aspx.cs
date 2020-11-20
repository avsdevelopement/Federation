using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmLoanshedule : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();

    ClsEMIEnquiry LE = new ClsEMIEnquiry();



    //ClsLoanEnquiry LE = new ClsLoanEnquiry();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BindGrid()
    {
        try
        {
           // LE.GetLoanEnquiry(GrdLoanInfo, Convert.ToDouble(TxtLoanAmt.Text), Convert.ToDouble(TxtAnnualINT.Text), Convert.ToDouble(TxtPeriodY.Text), TxtSDate.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}