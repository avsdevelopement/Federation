using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for ClsLoanEnquiry
/// </summary>
public class ClsEMIEnquiry
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    int Result;
    string sql = "";

	public ClsEMIEnquiry()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int GetLoanEnquiry(GridView grdloan,double LA, double AI, double LPer,string SDT,string PeriodType)
    {
        try
        {
           // string DueDate = "";
           // int InMonth = 0;
           // InMonth = Convert.ToInt16(SDT) * 12;
           // DueDate = conn.AddMonthDay(ED, InMonth.ToString(), "M");
            double period;
            if (PeriodType == "Y")
            {
                period = LPer * 12;
            }
            else
            {
                period = LPer;
            }
            
            sql = "EXEC SP_LOAN_ENQUIRY @LOANAMOUNT ='"+LA+"',@INTEREST ='"+AI+"',@PERIOD ='"+period+"',@STARTDATE ='" + conn.ConvertDate(SDT) + "'";
            Result = conn.sBindGrid(grdloan, sql);


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataSet GetLoanEnquiryReport(double LA, double AI, double LPer, string SDT, string PeriodType)
    {
        try
        {
           
            double period;
            if (PeriodType == "Y")
            {
                period = LPer * 12;
            }
            else
            {
                period = LPer;
            }

            sql = "EXEC SP_LOAN_ENQUIRY @LOANAMOUNT ='" + LA + "',@INTEREST ='" + AI + "',@PERIOD ='" + period + "',@STARTDATE ='" + conn.ConvertDate(SDT) + "'";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
                DS.Tables.Add(DT);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public string GetPeriod(string FL,string LAMT,string RATE,string REPAYAMT,string STDATE)
    {
        try
        {
            sql="Exec SP_LOAN_ENQUIRY @Flag='"+FL+"',@LOANAMOUNT='"+LAMT+"',@INTEREST='"+RATE+"',@RepayAmt='"+REPAYAMT+"',@STARTDATE='"+conn.ConvertDate(STDATE)+"'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
}