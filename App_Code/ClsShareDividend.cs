using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsShareDividend
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    int Result;

	public ClsShareDividend()
	{

	}

    public string GetBranchName(string BrCode)
    {
        try
        {
            sql = "Select MidName From BankName Where BrCd = '" + BrCode + "' And BrCd <> 0";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public string GetECSParam(string BrCode)
    {
        try
        {
            sql = "Select ListValue From Parameter Where BrCd = '" + BrCode + "' And ListField = 'ECS'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetDivision(DropDownList DDL)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select ConVert(VarChar(100), RecDiv) +'-'+ ConVert(VarChar(100), DesCr) As Name, RecDiv As Id From Paymast Where RecCode = 0 Order by RecCode";
            conn.FillDDL(DDL, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetDepartment(DropDownList DDL, string BrCode, string DivCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select ConVert(VarChar(100), DesCr) +'-'+ ConVert(VarChar(100), RecCode) As Name, RecCode As Id From PayMast "+
                  "Where BrCd = '" + BrCode + "' And RecDiv = '" + DivCode + "' Order by RecCode";
            conn.FillDDL(DDL, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetADMSubGl(string BrCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select ADMGlCode, ADMSubGlCode From BankName Where BrCd = '" + BrCode + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetCustName(string BrCode, string PrCode, string AccNo)
    {
        DT = new DataTable();
        try
        {
            sql = "Select M.CustName+'_'+ConVert(VarChar(10), A.AccNo)+'_'+ConVert(VarChar(10), A.CustNo) As CustName From Master M " +
                  "Inner Join Avs_Acc A ON A.CustNo = M.CustNo Where A.BRCD = '" + BrCode + "' AND A.SubGlCode = '" + PrCode + "' And A.AccNo = '" + AccNo + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetAccNo(string BrCode, string PrCode)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),GLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BrCode + "' AND SUBGLCODE='" + PrCode + "' GROUP BY GLCODE,GLNAME";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int BindSpecialGrid(GridView Gview, string BrCode, string AccNo, string EDate)
    {
        try
        {
            sql = "Exec RptShareDividentBalance @BrCode = '" + BrCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int BindMultipleGrid(GridView Gview, string BrCode, string Divisn, string Depart, string ProdCode, string EDate)
    {
        try
        {
            sql = "Exec RptShareBalance @BrCode = '" + BrCode + "', @Divisn = '" + Divisn + "', @Depatmnt = '" + Depart + "', @ProdCode = '" + ProdCode + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public double GetOtherIntRate(string BrCode, string SubGlCode)
    {
        try
        {
            sql = "Select OTHERCHG From LOANGL Where BrCd = '" + BrCode + "' and LoanGlCode='" + SubGlCode + "'";
            sResult = conn.sExecuteScalar(sql);
            if (sResult == null)
                sResult = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(sResult == "" ? "0.00" : sResult);
    }

    public string GetProductName(string PrdCode, string BRCD)//BRCD ADDED --Abhishek
    {
        string sql = "select ISNULL(GLNAME,'') from glmast WHERE SUBGLCODE ='" + PrdCode + "' and BRCD='" + BRCD + "'";
        string ProductName = conn.sExecuteScalar(sql);
        return ProductName;
    }

    public int UpdateLastIntDate(string BrCode, string SGlCode, string AccNo, string EDate, string Mid)
    {
        try
        {
            sql = "Update LoanInfo Set Prev_IntDt = LastIntDate, LastIntDate = '" + conn.ConvertDate(EDate).ToString() + "', Mod_Date = '" + conn.ConvertDate(EDate).ToString() + "' Where BrCd= '" + BrCode + "' And LOANGLCODE = '" + SGlCode + "' And CUSTACCNO = '" + AccNo + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}