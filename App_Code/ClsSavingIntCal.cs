using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

public class ClsSavingIntCal
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    int Result = 0;
    
	public ClsSavingIntCal()
	{
		
	}

    public DataSet GetSavingIntCal(string Flag1, string Flag2, string FBrCode, string TBrCode, string PrCode, string FrAccNo, string ToAccNo, string FrDate, string ToDate, string EDate, string Mid)
    {
        try
        {
            sql = "EXEC Isp_Sbint_Calc @Flag='" + Flag1 + "',@SFLAG='" + Flag2 + "',@Fbrcd='" + FBrCode + "',@Tbrcd='" + TBrCode + "',@Fprdcd='" + PrCode + "',@Tprdcd='" + PrCode + "',@Faccno='" + FrAccNo + "',@Taccno='" + ToAccNo + "',@FDate='" + conn.ConvertDate(FrDate).ToString() + "',@Todate='" + conn.ConvertDate(ToDate).ToString() + "',@Mid='" + Mid + "',@Pcmac='" + conn.PCNAME().ToString() + "',@Edt='" + conn.ConvertDate(EDate).ToString() + "'";
            DT = new DataTable();
            DS = new DataSet();
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
    public string CalculateSBINT(string Flag1, string Flag2, string FBrCode, string TBrCode, string PrCode, string FrAccNo, string ToAccNo, string FrDate, string ToDate, string EDate, string Mid)
    {
        try
        {
            sql = "EXEC Isp_Sbint_Calc @Flag='" + Flag1 + "',@SFLAG='" + Flag2 + "',@Fbrcd='" + FBrCode + "',@Tbrcd='" + TBrCode + "',@Fprdcd='" + PrCode + "',@Tprdcd='" + PrCode + "',@Faccno='" + FrAccNo + "',@Taccno='" + ToAccNo + "',@FDate='" + conn.ConvertDate(FrDate).ToString() + "',@Todate='" + conn.ConvertDate(ToDate).ToString() + "',@Mid='" + Mid + "',@Pcmac='" + conn.PCNAME().ToString() + "',@Edt='" + conn.ConvertDate(EDate).ToString() + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public int Recalculate(string Fl,string MID,string EDT,string BRCD)
    {
        try
        {
            sql = "EXEC Isp_Sbint_Calc @Flag='" + Fl + "',@Fbrcd='" + BRCD + "',@Tbrcd='" + BRCD + "',@Mid='" + MID + "',@Pcmac='" + conn.PCNAME().ToString() + "',@Edt='" + conn.ConvertDate(EDT).ToString() + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}