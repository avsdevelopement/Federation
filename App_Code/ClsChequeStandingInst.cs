using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public class ClsChequeStandingInst
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsChequeStandingInst()
	{
		

	}

    public string GetAccType(string AccT, string BRCD)
    {
        try
        {
            sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE='" + AccT + "' AND BRCD='" + BRCD + "'";
            AccT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return AccT;
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

    public int ChequeReq(string brcd, string prcode, string prname, string accno, string fseries, string tseries, string chqtype, string totalleaf, string mid, string pcname, string username, string EDate)
    {
        int RM;
        try
        {
            sql = "Exec SP_CHEQUEBOOKREQUEST @BRCD='" + brcd + "',@PCODE='" + prcode + "',@PNAME='" + prname + "', @ACCNO='" + accno + "',@FSERIES='" + fseries + "',@TSERIES='" + tseries + "',@CHQTYPE='" + chqtype + "',@NOLEAF='" + totalleaf + "', @MID='" + mid + "',@PCMAC='" + pcname + "',@USER = '" + username + "', @EDATE = '"+ conn.ConvertDate(EDate).ToString() +"', @ID = "+0+", @Type = 'INSRT'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int ChequeModfy(string id, string brcd, string prcode, string prname, string accno, string fseries, string tseries, string chqtype, string totalleaf, string mid, string pcname, string username, string EDate)
    {
        int RM;
        try
        {
            sql = "Exec SP_CHEQUEBOOKREQUEST @BRCD='" + brcd + "',@PCODE='" + prcode + "',@PNAME='" + prname + "', @ACCNO='" + accno + "',@FSERIES='" + fseries + "',@TSERIES='" + tseries + "',@CHQTYPE='" + chqtype + "',@NOLEAF='" + totalleaf + "', @MID='" + mid + "',@PCMAC='" + pcname + "',@USER = '" + username + "', @EDATE = '" + conn.ConvertDate(EDate).ToString() + "', @ID = '" + id + "', @Type = 'MODFY'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int ChequeReqAth(string id, string brcd, string prcode, string prname, string accno, string fseries, string tseries, string chqtype, string totalleaf, string mid, string pcname, string username, string EDate)
    {
        int RM;
        try
        {
            sql = "Exec SP_CHEQUEBOOKREQUEST @BRCD='" + brcd + "',@PCODE='" + prcode + "',@PNAME='" + prname + "', @ACCNO='" + accno + "',@FSERIES='" + fseries + "',@TSERIES='" + tseries + "',@CHQTYPE='" + chqtype + "',@NOLEAF='" + totalleaf + "', @MID='" + mid + "',@PCMAC='" + pcname + "',@USER = '" + username + "', @EDATE = '" + conn.ConvertDate(EDate).ToString() + "', @ID = '" + id + "', @Type = 'AUTHO'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int ChequeReqDel(string id, string brcd, string prcode, string prname, string accno, string fseries, string tseries, string chqtype, string totalleaf, string mid, string pcname, string username, string EDate)
    {
        int RM;
        try
        {
            sql = "Exec SP_CHEQUEBOOKREQUEST @BRCD='" + brcd + "',@PCODE='" + prcode + "',@PNAME='" + prname + "', @ACCNO='" + accno + "',@FSERIES='" + fseries + "',@TSERIES='" + tseries + "',@CHQTYPE='" + chqtype + "',@NOLEAF='" + totalleaf + "', @MID='" + mid + "',@PCMAC='" + pcname + "',@USER = '" + username + "', @EDATE = '" + conn.ConvertDate(EDate).ToString() + "', @ID = '" + id + "', @Type = 'DEL'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public DataTable CheckSeriesAvailable(string BrCode, string ProdCode, string ChqSeries)
    {
        try
        {
            sql = "SELECT * FROM AVS_ChequeStock WHERE BRCD = " + BrCode + " AND SUBGLCODE = " + ProdCode + " AND FSERIES <= " + ChqSeries + " AND TSERIES >= " + ChqSeries + "";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            return DT = null;
        }
        return DT;
    }

    public DataTable CheckSeriesLastUsed(string BrCode, string ProdCode, string Chqtype)
    {
        try
        {
            sql = "SELECT LASTUSED FROM AVS_ChequeStock WHERE BRCD = '" + BrCode + "' AND SUBGLCODE = '" + ProdCode + "' AND CHEQUETYPE = '" + Chqtype + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            return DT = null;
        }
        return DT;
    }

    public DataTable CheckSeriesAllocated(string BrCode, string ProdCode, string ChqSeries)
    {
        try
        {
            sql = "SELECT * FROM AVS_ChequeBookRequest WHERE BRCD = '" + BrCode + "' AND SUBGLCODE = '" + ProdCode + "' AND FSERIES <= '" + ChqSeries + "' AND TSERIES >= '" + ChqSeries + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            return DT = null;
        }
        return DT;
    }

    public DataTable GetInfo(string BRCD, string ID)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select * from AVS_ChequeBookRequest where brcd='" + BRCD + "' AND id='" + ID + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }
}