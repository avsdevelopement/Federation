using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsChequeAllocation
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsChequeAllocation()
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

    public int ChequeAllocationAD(string brcd, string prcode, string accno, string chqtype, string leaf, string noofbook, string totalleaf, string fseries, string tseries, string EDate, string mid, string pcname)
    {
        int RM;
        try
        {
            sql = "Exec SP_CHEQUEBOOKALLOC @BRCD = '" + brcd + "', @PCODE = '" + prcode + "', @ACCNO = '" + accno + "', @CHQTYPE = '" + chqtype + "', @LEAF = '" + leaf + "', @NOBOOK = '" + noofbook + "', @NOLEAF = '" + noofbook + "', @FSERIES = '" + fseries + "', @TSERIES = '" + tseries + "', @EDATE = '" + conn.ConvertDate(EDate).ToString() + "', @MID = '" + mid + "', @PCMAC = '" + pcname + "', @ID = "+0+", @Type = 'INSRT'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int ChequeAllocationMD(string id,string brcd, string prcode, string accno, string chqtype, string leaf, string noofbook, string totallea, string fseries, string tseries, string EDate, string mid, string pcname)
    {
        int RM;
        try
        {
            sql = "Exec SP_CHEQUEBOOKALLOC @BRCD = '" + brcd + "', @PCODE = '" + prcode + "', @ACCNO = '" + accno + "', @CHQTYPE = '" + chqtype + "', @LEAF = '" + leaf + "', @NOBOOK = '" + noofbook + "', @NOLEAF = '" + noofbook + "', @FSERIES = '" + fseries + "', @TSERIES = '" + tseries + "', @EDATE = '" + conn.ConvertDate(EDate).ToString() + "', @MID = '" + mid + "', @PCMAC = '" + pcname + "', @ID = " + id + ", @Type = 'MODFY'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int ChequeAllocationDEL(string id,string brcd, string prcode, string accno, string chqtype, string leaf, string noofbook, string totallea, string fseries, string tseries, string EDate, string mid, string pcname)
    {
        int RM;
        try
        {
            sql = "Exec SP_CHEQUEBOOKALLOC @BRCD = '" + brcd + "', @PCODE = '" + prcode + "', @ACCNO = '" + accno + "', @CHQTYPE = '" + chqtype + "', @LEAF = '" + leaf + "', @NOBOOK = '" + noofbook + "', @NOLEAF = '" + noofbook + "', @FSERIES = '" + fseries + "', @TSERIES = '" + tseries + "', @EDATE = '" + conn.ConvertDate(EDate).ToString() + "', @MID = '" + mid + "', @PCMAC = '" + pcname + "', @ID = " + id + ", @Type = 'DEL'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int ChequeAllocationATH(string id,string brcd, string prcode, string accno, string chqtype, string leaf, string noofbook, string totallea, string fseries, string tseries, string EDate, string mid, string pcname)
    {
        int RM;
        try
        {
            sql = "Exec SP_CHEQUEBOOKALLOC @BRCD = '" + brcd + "', @PCODE = '" + prcode + "', @ACCNO = '" + accno + "', @CHQTYPE = '" + chqtype + "', @LEAF = '" + leaf + "', @NOBOOK = '" + noofbook + "', @NOLEAF = '" + noofbook + "', @FSERIES = '" + fseries + "', @TSERIES = '" + tseries + "', @EDATE = '" + conn.ConvertDate(EDate).ToString() + "', @MID = '" + mid + "', @PCMAC = '" + pcname + "', @ID = " + id + ", @Type = 'AUTHO'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int CheckSeries(string BrCode, string ProdCode, string ChqSeries)
    {
        int RM;
        try
        {
            sql = "SELECT * FROM AVS_ChequeStock WHERE BRCD = '" + BrCode + "' AND SUBGLCODE = '" + ProdCode + "' AND FSERIES <= '" + ChqSeries + "' AND TSERIES >= '" + ChqSeries + "' ";
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
            sql = "SELECT * FROM AVS_ChequeStock WHERE BRCD = '" + BrCode + "' AND SUBGLCODE = '" + ProdCode + "' AND FSERIES <= '" + ChqSeries + "' AND TSERIES >= '" + ChqSeries + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            return DT = null;
        }
        return DT;
    }

    public DataTable CheckSeriesAllocated(string BrCode, string ProdCode, string accno, string ChqSeries)
    {
        try
        {
            sql = "SELECT * FROM AVS_ChequeBookRequest WHERE BRCD = '" + BrCode + "' AND SUBGLCODE = '" + ProdCode + "' AND ACCNO <> "+ accno +" AND FSERIES <= '" + ChqSeries + "' AND TSERIES >= '" + ChqSeries + "'";
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
            sql = "Select * from AVS_CHQBOOKALLOCATION where brcd='" + BRCD + "' AND id='" + ID + "'";
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