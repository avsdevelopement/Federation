using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsLoanRepaymentCerti
/// </summary>
public class ClsLoanRepaymentCerti
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
	public ClsLoanRepaymentCerti()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GetAccType(string AccT, string BRCD)
    {
        sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE='" + AccT + "'  AND BRCD='" + BRCD + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public DataTable BranchDetails( string BRCD)
    {
        DataTable DT = new DataTable();
        sql = "select brcd,midname from bankname where brcd='"+BRCD+"'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public string Getaccno(string AT, string BRCD, string GLCD)
    {
        try
        {
            sql = " SELECT (CONVERT(VARCHAR(10),MAX(LASTNO)+1))+'-'+(CONVERT (VARCHAR(10),GLCODE))+'-'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }
    public string GetAccStatus(string BRCD, string SBGL, string AC)
    {
        try
        {
            sql = "SELECT ACC_STATUS FROM AVS_ACC WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SBGL + "' AND ACCNO='" + AC + "'";
            AC = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return AC;
    }
    public string Getstage1(string CNO, string BRCD, string PRD)//GLCDOE PARAMETR REMOVED
    {
        string RS = "";
        //sql = "SELECT STAGE FROM AVS_ACC WHERE ACCNO='" + CNO + "'AND GLCODE='"+GL+"' AND SUBGLCODE='"+PRD+"' AND BRCD='" + BRCD + "' AND STAGE <>1004 ";
        sql = "SELECT CONVERT(VARCHAR(5),STAGE) FROM AVS_ACC WHERE ACCNO='" + CNO + "' AND SUBGLCODE='" + PRD + "' AND BRCD='" + BRCD + "' AND STAGE <>1004 ";//AND GLCODE='"+GL+"'
        RS = conn.sExecuteScalar(sql);
        return RS;
    }
    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO  WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetOutNo(string brcd,string FYY,string TYY)//ANKITA 29/11/2017
    {
        try
        {
            sql = "select MAX(isnull(convert(int,SUBSTRING(outno,6,6)),0))+1 from OUTWARD_RECEIPT WHERE BRCD='" + brcd + "' and FROMYEAR='" + FYY + "' and TOYEAR='" + TYY + "' AND STAGE<>1004";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public string CheckOutNo_Exist(string Brcd,string FYear,string TYear,string CSTNO="0")
    {
        try
        {
            if (CSTNO == "0")
            {
                sql = "Select OUTNO from OUTWARD_RECEIPT where BRCD='" + Brcd + "' and FROMYEAR='" + FYear + "' and TOYEAR='" + TYear + "'";
            }
            else
            {
                sql = "Select OUTNO from OUTWARD_RECEIPT where BRCD='" + Brcd + "' and FROMYEAR='" + FYear + "' and TOYEAR='" + TYear + "' and CUSTNO='" + CSTNO + "'";
            }
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }
    public string CheckOutNo_ExistCustno(string Brcd, string FYear, string TYear,string CUSTNO)
    {
        try
        {
            sql = "Select Count(*) from OUTWARD_RECEIPT where BRCD='" + Brcd + "' and FROMYEAR='" + FYear + "' and TOYEAR='" + TYear + "' and CUSTNO='" + CUSTNO + "'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public int INSERT(string OUTNO, string TYPE, string SUBGLCD, string ACCNO, string MID,string brcd,string custno,string FYY,string TYY)//ANKITA 29/11/2017
    {
        try
        {
            sql = "select count(*) from OUTWARD_RECEIPT where OUTNO='" + OUTNO + "' AND STAGE<>1004";
            Result = Convert.ToInt16(conn.sExecuteScalar(sql));
            if (Result > 0)
            {
              //  sql = "update OUTWARD_RECEIPT set stage=1004,cid='"+MID+"' where OUTNO='"+OUTNO+"'";
              //  Result = conn.sExecuteQuery(sql);
                Result = updatelog(MID, OUTNO);
            }
            sql = "insert into OUTWARD_RECEIPT(TYPE,OUTNO,SUBGLCODE,ACCNO,STAGE,MID,brcd,CUSTNO,FROMYEAR,TOYEAR) VALUES('" + TYPE + "','" + OUTNO + "','" + SUBGLCD + "','" + ACCNO + "','1001','" + MID + "','" + brcd + "','" + custno + "','" + FYY + "','" + TYY + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public string Getbrname(string brcd)//ANKITA 29/11/2017
    {
        try
        {
            sql = "select SUBSTRING(MIDNAME,1,1)MIDNAME from BANKNAME WHERE BRCD='" + brcd + "'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }
    public DataTable GETOUWARD(string OUTNO)
    {
        try
        {
            sql = "select * from OUTWARD_RECEIPT where OUTNO='"+OUTNO+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public void bindoutward(GridView grd, string OUTNO)
    {
        try
        {
            sql = "select OUTNO,SUBGLCODE,ACCNO from OUTWARD_RECEIPT where OUTNO='" + OUTNO + "' and stage<>'1004'";
            conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        
    }
    public void bindAccDetail(GridView grd, string CUSTNO,string BRCD,string FDate,string TDate)
    {
        try
        {
            //sql = "select BRCD,SUBGLCODE,ACCNO,CUSTNO from AVS_ACC where CUSTNO='" + CUSTNO + "' AND BRCD='"+BRCD+"' and stage<>'1004' AND ACC_STATUS NOT IN(99,3) AND GLCODE=3";

            sql = "select BRCD,SUBGLCODE,ACCNO,CUSTNO from AVS_ACC  where BRCD='" + BRCD + "' AND GLCODE=3 and CUSTNO='" + CUSTNO + "' AND stage<>'1004' and ACC_STATUS='1' " +
                 " union  " +
                 " select BRCD,SUBGLCODE,ACCNO,CUSTNO from AVS_ACC where CUSTNO='" + CUSTNO + "' AND BRCD='" + BRCD + "' and stage<>'1004' AND GLCODE=3 and ClosingDate between '" + conn.ConvertDate(FDate) + "' and '" + conn.ConvertDate(TDate) + "'";

            conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    public int updatelog(string MID, string OUTNO)
    {
        try
        {
            if(MID!="")
             sql = "update OUTWARD_RECEIPT set stage=1004,cid='"+MID+"' where OUTNO='"+OUTNO+"'";
            else
                sql = "update OUTWARD_RECEIPT set stage=1004,cid='" + MID + "',STATUS='NO RECORD FOUND' where OUTNO='" + OUTNO + "'";
            Result = conn.sExecuteQuery(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
}