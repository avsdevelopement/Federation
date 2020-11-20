using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsCustomerUpdation
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsEncryptValue EN = new ClsEncryptValue();
    string sql = "";
    int Result = 0;

	public ClsCustomerUpdation()
	{
		
	}

    public string GetStage(string BRCD, string Custno)
    {
        try
        {
            sql = "SELECT STAGE FROM MASTER WHERE BRCD='" + BRCD + "' AND CUSTNO='" + Custno + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }

   
    public int UpdateCustomer(string BrCode, string OldCustNo, string NewCustNo,string MID)
    {
        try
        {
            sql = "Exec SP_UpdateCustomerNo '" + BrCode + "','" + OldCustNo + "','" + NewCustNo + "','" + MID + "','" + EN.GetMK(MID) + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result = 0;
        }
        return Convert.ToInt32(Result);
    }
    public int UpdateCustomerImg(string BrCode, string OldCustNo, string NewCustNo, string MID)
    {
        try
        {
            sql = "Exec SP_UpdateCustomerNoImg '" + BrCode + "','" + OldCustNo + "','" + NewCustNo + "','" + MID + "','" + EN.GetMK(MID) + "'";
            Result = conn.sExecuteQueryPH(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result = 0;
        }
        return Convert.ToInt32(Result);
    }
   
    public int GETAccData(GridView grd, string custno)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select * from avs_acc where custno='" + custno + "'";
            Result = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public int GETAccDetails(GridView grd, string custno, string FDate, string FL)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Exec RptCustAccDetails @CUSTNO='" + custno + "',@AsonDate='" + conn.ConvertDate(FDate) + "',@Flag='" + FL + "' ";
            Result = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public int UPDATECUSTNO(string BRCD, string CUSTNO, string ACCTYPE, string ACCNO, string NEWCUSTNO)
    {
        int Result = 0;
        try
        {
            sql = "EXEC SP_UPDATECUSTNO '" + BRCD + "','" + CUSTNO + "','" + ACCTYPE + "','" + ACCNO + "','" + NEWCUSTNO + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string UpdateCustomerReplace(string BrCode, string OldCustNo, string NewCustNo, string MID)
    {
        string anw = "";
        try
        {
            sql = "Exec SP_UpdateCustomerNo_Replace '" + BrCode + "','" + OldCustNo + "','" + NewCustNo + "','" + MID + "','" + EN.GetMK(MID) + "'";
            anw = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return "Fail";
        }
        return anw;
    }
    public int ChangeCustomer(string BrCode, string PrCode, string AccNo, string OldCustNo, string newCustNo)
    {
        try
        {
            sql = "Exec ISP_AVS0113 @BrCode = '" + BrCode + "', @PrCode = '" + PrCode + "', @AccNo = '" + AccNo + "', @OldCustNo = '" + OldCustNo + "', @NewCustNo = '" + newCustNo + "' ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int UpdateAccnoDT(string BRCD, string ACCTYPE, string ACCNO, string NEWACCNO)
    {
        int Result = 0;
        try
        {
            sql = "EXEC SP_UpdateAccnoChange @Brcd='" + BRCD + "',@SubGlCode='" + ACCTYPE + "',@AccNo='" + ACCNO + "',@NewAccNo='" + NEWACCNO + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}