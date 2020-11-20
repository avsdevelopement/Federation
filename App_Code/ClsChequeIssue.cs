using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsChequeIssue
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;

	public ClsChequeIssue()
	{
		

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

    public DataTable GetCustName(string AccT, string AccNo, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME,AC.OPENINGDATE FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND M.BRCD=AC.BRCD WHERE AC.ACCNO='" + AccNo + "' AND AC.SUBGLCODE='" + AccT + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int CheckAccount(string AC, string PT, string BRCD)
    {
        try
        {
            try
            {
                sql = "SELECT CUSTNO FROM AVS_ACC WHERE SUBGLCODE='" + PT + "' AND ACCNO='" + AC + "' AND BRCD='" + BRCD + "'";
                Result = Convert.ToInt32(conn.sExecuteScalar(sql));
                return Result;
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
                return -1;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int ChequeIssueInst(string brcd, string PrCode, string PrName, string AccNo, string AccName, string NoLeaves, string InstFrom, string InstTo, string mid, string pcname, string EDate)
    {
        int RM;
        try
        {
            sql = "Exec SP_INSTRUMENTISSUE @BRCD = '" + brcd + "', @PCODE = '" + PrCode + "', @PNAME = '" + PrName + "', @ACCNO = '" + AccNo + "', @ACCNAME = '" + AccName + "', @NOLEAVES = '" + NoLeaves + "', @FSERIES = '" + InstFrom + "', @TSERIES = '" + InstTo + "', @EDATE = '" + conn.ConvertDate(EDate).ToString() + "', @MID = '" + mid + "', @PCMAC = '" + pcname + "', @Type = 'INSRT'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int ChequeIssueAth(string id, string brcd, string mid)
    {
        int RM;
        try
        {
            sql = "Exec SP_INSTRUMENTISSUE @BRCD='" + brcd + "',@MID='" + mid + "',@ID='" + id + "', @Type = 'AUTHO'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int ChequeIssueModfy(string id, string brcd, string PrCode, string PrName, string AccNo, string AccName, string NoLeave, string InstFrom, string InstTo)
    {
        int RM;
        try
        {
            sql = "Exec SP_INSTRUMENTISSUE @BRCD = '" + brcd + "', @PCODE = '" + PrCode + "', @PNAME = '" + PrName + "', @ACCNO = '" + AccNo + "', @ACCNAME = '" + AccName + "', @NOLEAVES = '" + NoLeave + "', @FSERIES = '" + InstFrom + "', @TSERIES = '" + InstTo + "', @ID='" + id + "', @Type = 'MODFY'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int ChequeIssueDel(string id, string brcd)
    {
        int RM;
        try
        {
            sql = "Exec SP_INSTRUMENTISSUE @BRCD='" + brcd + "',@ID='" + id + "', @Type = 'DEL'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int BindGrid(GridView Gview, string BRCD)
    {
        try
        {
            sql = "SELECT id, SUBGLCODE, SUBGLNAME, ACCNO, ACCNAME, NOOFLEAVES,FSERIES,TSERIES, EFFECTDATE,STAGE FROM AVS_INSTRUMENTISSUE WHERE BRCD = '" + BRCD + "' AND STAGE NOT IN ('1003','1004')";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetInfo(string BRCD, string ID)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select * from AVS_INSTRUMENTISSUE where brcd='" + BRCD + "' and id='" + ID + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public string GetStage1(string BRCD, string ProdCode, string ACCNO, string id)
    {
        try
        {
            sql = "SELECT STAGE FROM AVS_INSTRUMENTISSUE WHERE BRCD = '" + BRCD + "' AND SUBGLCODE = '" + ProdCode + "' AND ACCNO = '"+ACCNO+"' AND ID = '" + id + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }

    public string GetFseriesNo(string BrCd, string PrCode, string EDate)
    {
        try
        {
            sql = "SELECT TSERIES+1 FROM AVS_INSTRUMENTISSUE WHERE BRCD = '" + BrCd + "' AND SUBGLCODE = '" + PrCode + "' AND STAGE = 1003 AND EFFECTDATE <= '" + conn.ConvertDate(EDate).ToString() + "'";
            BrCd = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BrCd;
    }
}