using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for ClsFDCalculator
/// </summary>
public class ClsFDCalculator
{
    double TotIntrest=0;
    double TotalAmt=0;
    double TotalAmt1 = 0;
    double ROI=0;
    int nyears;
    double EY;
    string sql = "";
    int Result = 0;
    DbConnection conn = new DbConnection();

	public ClsFDCalculator()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public double Calc_IntReceived(double amt, double P)
    {
        try
        {
            TotIntrest = Convert.ToDouble(amt) - Convert.ToDouble(P);
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return TotIntrest;
    }

    public double Calc_TotalMA(double P, double R, int n, double T)
    {
        try
        {
            double R1=(R/100);
            double nt = n * T;
           
            TotalAmt1 = Math.Pow((1 + (R1 / n)), nt);
             TotalAmt = P * TotalAmt1 ;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return TotalAmt;
    }

    public double Calc_EfectYeild(double MA, int n)
    {
        try
        {            
            EY = MA / n;
            EY /= 100;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return EY;
    }




    public int GetGridData(GridView GD,string FLAG,string AMOUNT, string PERIOD, string RATE, string PTYPE)
    {
        try
        {
            sql = "EXEC Isp_AVS0012 @Flag='" + FLAG + "',@Amt='" + AMOUNT + "',@PeriodType='" + PTYPE + "',@Period='" + PERIOD + "',@Rate='" + RATE + "'";
            Result = conn.sBindGrid(GD,sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}