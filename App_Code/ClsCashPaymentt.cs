using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsCashPaymentt
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", Result = "";
    int IntResult = 0;

	public ClsCashPaymentt()
	{
	}

    public string openDay(string BRCD)
    {
        string wdt = "";
        try
        {
            sql = "Select ListValue From Parameter Where ListField = 'DayOpen' And BrCd = '" + BRCD + "' ";
            wdt = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return wdt;
    }

    public int Getinfotable(GridView Gview, string smid, string sbrcd, string EDT)
    {
        try
        {
            string[] TD;
            TD = EDT.Split('/');

            sql = " Select convert(varchar(10),a.SETNO)+'_'+convert(varchar(10),a.Amount) Dens,A.SETNO SETNO, ACC.SUBGLCODE AT, A.ACCNO ACNO, M.CUSTNAME CUSTNAME, A.Amount, " +
                " A.PARTICULARS PARTICULARS, A.INSTRUMENTNO, Convert(VarChar(10), A.INSTRUMENTDATE, 103) As InstDate, UM.USERNAME MAKER from AVSM_" + TD[2].ToString() + TD[1].ToString() + " A With(NoLock) " +
                " Inner Join USERMASTER UM With(NoLock) ON UM.PERMISSIONNO=A.MID " +
                " Inner Join AVS_ACC ACC With(NoLock) ON ACC.ACCNO=A.ACCNO And ACC.BRCD = A.BRCD And A.SUBGLCODE=ACC.SUBGLCODE " +
                " Inner Join MASTER M With(NoLock) ON M.CUSTNO=ACC.CUSTNO " +
                " Where A.BRCD='" + sbrcd + "' And A.STAGE = '1001' And A.Amount <> '0' And Trxtype = '2' And A.MID='" + smid + "' And A.ACTIVITY='4' " +
                " And A.ENTRYDATE = '" + conn.ConvertDate(EDT) + "' Order By A.SETNO,A.SCROLLNO";

            IntResult = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntResult;
    }

    public string GetStage(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "SELECT CONVERT(VARCHAR(5),STAGE) FROM AVS_ACC WHERE ACCNO='" + AccNo + "' AND SUBGLCODE='" + PrCode + "' AND BRCD='" + BrCode + "' AND STAGE <>1004 ";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetAccStatus(string BRCD, string SBGL, string AC)
    {
        try
        {
            sql = "SELECT ACC_STATUS FROM AVS_ACC WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SBGL + "' AND ACCNO='" + AC + "'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return Result;
    }

    public string GetCustNo(string BRCD, string GLCODE, string ACCNO)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNO FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetAccountName(string brcd, string SubGlCode, string AccNo)
    {
        try
        {
            sql = "SELECT CUSTNAME FROM AVS_ACC A INNER JOIN MASTER B With(NoLock) ON A.CUSTNO=B.CUSTNO WHERE A.CUSTNO = B.CUSTNO AND A.ACCNO='" + AccNo + "' AND A.SUBGLCODE='" + SubGlCode + "' AND A.BRCD='" + brcd + "'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string Getaccno(string AT, string BRCD)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),GLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public double GetOpenClose(string Flag, string Fyear, string FMonth, string PT, string ACC, string BRCD, string EDT, string GL)
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OPENCLOSE @P_FLAG='" + Flag + "',@P_FYEAR='" + Fyear + "',@P_FMONTH='" + FMonth + "',@p_job='" + PT + "',@p_job1='" + ACC + "',@p_job2='" + BRCD + "',@p_job3='" + conn.ConvertDate(EDT) + "',@p_job4='" + GL + "'";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }

    public string GetIntACCYN(string BRCD, string SBGL)
    {
        try
        {
            sql = "SELECT ISNULL(INTACCYN,'N') FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SBGL + "'";
            SBGL = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return SBGL;
    }
    
    public DataTable GetAccStatDetails(string PFMONTH, string PTMONTH, string PFYEAR, string PTYEAR, string PFDT, string PTDT, string PAC, string PAT, string BRCD)
    {
        try
        {
            DT = new DataTable();

            if (Convert.ToInt32(PFYEAR.ToString()) < 2000)
            {
                PFYEAR = "2000";
                string[] aa = @PFDT.ToString().Split('/');
                @PFDT = aa[0].ToString() + '/' + aa[1].ToString() + '/' + PFYEAR;
            }

            sql = "Exec SP_ACCSTATUS_R @pfmonth='" + PFMONTH + "',@ptmonth='" + PTMONTH + "',@PFDT='" + conn.ConvertDate(PFDT) + "',@PTDT='" + conn.ConvertDate(PTDT) + "',@pfyear='" + PFYEAR + "',@ptyear='" + PTYEAR + "',@pac='" + PAC + "',@pat='" + PAT + "', @BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetAccOpenDate(string BrCode, string SubGlCode, string AccNo)
    {
        try
        {
            sql = "Select Convert(VarChar(10), OpeningDate, 103) From Avs_Acc Where Brcd = '" + BrCode + "' And SubGlCode = '" + SubGlCode + "' And AccNo = '" + AccNo + "'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetMigDate(string BrCode)
    {
        try
        {
            sql = "Select Convert(VarChar(10), Implementatin, 120) From BankName Where Brcd = '" + BrCode + "'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
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

}