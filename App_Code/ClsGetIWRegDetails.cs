using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsGetIWRegDetails
/// </summary>
public class ClsGetIWRegDetails
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "";

	public ClsGetIWRegDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetIWDt(string BranchID, string FBKcode, string FDT,string TDT, string FL, string FLT)
    {
        try
        {
            if (FL == "IW")
            {
                if (FLT == "D")
                {
                    sql = "Exec RptIWClearngDetails '" + BranchID + "' ,'D' ,'" + FBKcode + "','" + conn.ConvertDate(FDT) + "','" + conn.ConvertDate(TDT) + "'";
                    DT = conn.GetDatatable(sql);
                }
                else if (FLT == "S")
                {
                    sql = "Exec RptIWClearngDetails '" + BranchID + "','S' ,'" + FBKcode + "','" + conn.ConvertDate(FDT) + "','" + conn.ConvertDate(TDT) + "'";
                    DT = conn.GetDatatable(sql);
                }
            }
            else if (FL == "OW")
            {
                if (FLT == "D")
                {
                    sql = "Exec RptOWClearngDetails '" + BranchID + "' ,'D' ,'" + FBKcode + "','" + conn.ConvertDate(FDT) + "','" + conn.ConvertDate(TDT) + "'";
                    DT = conn.GetDatatable(sql);
                }
                else if (FLT == "S")
                {
                    sql = "Exec RptOWClearngDetails '" + BranchID + "','S' ,'" + FBKcode + "','" + conn.ConvertDate(FDT) + "','" + conn.ConvertDate(TDT) + "'";
                    DT = conn.GetDatatable(sql);
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
        return DT;
    }
    public DataTable GetClgReg (string BranchID, string FBKcode, string FDT, string FL, string FLT)
    {
        try
        {
            if (FL == "IW")
            {
                if (FLT == "D")
                {
                    sql = "Exec RptClgRegList @BranchID='" + BranchID + "' ,@Type='D' ,@BankCode='" + FBKcode + "',@AsonDate='" + conn.ConvertDate(FDT) + "'";
                    DT = conn.GetDatatable(sql);
                }
                else if (FLT == "S")
                {
                    sql = "Exec RptClgRegList @BranchID='" + BranchID + "' ,@Type='S' ,@BankCode='" + FBKcode + "',@AsonDate='" + conn.ConvertDate(FDT) + "'";
                    DT = conn.GetDatatable(sql);
                }
            }
            else if (FL == "OW")
            {
                if (FLT == "D")
                {
                    sql = "Exec RptClgRegList @BranchID='" + BranchID + "' ,@Type='D' ,@BankCode='" + FBKcode + "',@AsonDate='" + conn.ConvertDate(FDT) + "'";
                    DT = conn.GetDatatable(sql);
                }
                else if (FLT == "S")
                {
                    sql = "Exec RptClgRegList @BranchID='" + BranchID + "' ,@Type='S' ,@BankCode='" + FBKcode + "',@AsonDate='" + conn.ConvertDate(FDT) + "'";
                    DT = conn.GetDatatable(sql);
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
        return DT;
    }
    public DataTable GetIWAcc(string BranchID, string FBKcode, string FDT, string TDT)//Dhanya Shetty/12/07/2017
    {
        try
        {
            sql = "Exec D_InwordRegAcc @Brcd='" + BranchID + "',@BankCode='" + FBKcode + "',@AsonDate='" + conn.ConvertDate(FDT).ToString() + "',@TDate='" + conn.ConvertDate(TDT) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
         return DT;
  }

    public DataTable GetIWRetP(string BranchID, string FBKcode, string FDT, string TDT, string ADT)//Dhanya Shetty/12/07/2017
    {
        try
        {
            sql = "Exec D_InvRetPatti @Brcd='" + BranchID + "',@BankCode='" + FBKcode + "',@Fdate='" + conn.ConvertDate(FDT).ToString() + "',@TDate='" + conn.ConvertDate(TDT).ToString() + "',@AsonDate='" + conn.ConvertDate(ADT).ToString() + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetOutwardRetP(string BranchID, string FBKcode, string FDT, string TDT, string ADT)//Dhanya Shetty/12/07/2017
    {
        try
        {
            sql = "Exec D_OutwardRetPatti @Brcd='" + BranchID + "',@BankCode='" + FBKcode + "',@Fdate='" + conn.ConvertDate(FDT).ToString() + "',@TDate='" + conn.ConvertDate(TDT).ToString() + "',@AsonDate='" + conn.ConvertDate(ADT).ToString() + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetOutwardAcc(string BranchID, string FBKcode, string FDT,string TDT)//Dhanya Shetty/13/07/2017
    {
        try
        {
            sql = "Exec D_OutwordRegAcc @Brcd='" + BranchID + "',@BankCode='" + FBKcode + "',@AsonDate='" + conn.ConvertDate(FDT).ToString() + "',@TDate='" + conn.ConvertDate(TDT) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}