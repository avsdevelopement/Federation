using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsSBIntCalcSum
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    string TableName = "";
    int Result = 0;

	public ClsSBIntCalcSum()
	{
		
	}

    public DataSet GetSBIntCalSum(string BrCode, string PrCode, string AsOnDate, string Mid, string sFlag)
    {
        try
        {
            sql = "Exec SB_INTCalcSum @BrCode = '" + BrCode + "', @PrCode = '" + PrCode + "', @AsOnDate = '" + conn.ConvertDate(AsOnDate) + "', @Mid = '" + Mid + "', @sFlag = '" + sFlag + "'";
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
    public DataSet GetSBIntCal_DTRpt(string BrCode, string PrCode, string FDate, string TDate, string Mid, string FL)
    {
        try
        {
            sql = "Exec RptSB_INTCalculation @BrCode = '" + BrCode + "', @PrCode = '" + PrCode + "', @FrDate = '" + conn.ConvertDate(FDate) + "', @ToDate = '" + conn.ConvertDate(TDate) + "', @Mid = '" + Mid + "', @sFlag = '" + FL + "'";
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
    public DataSet GetSBIntCal_ParaRpt(string BrCode, string PrCode )
    {
        try
        {
            sql = "Exec RptSB_INTCalcPara @BrCode = '" + BrCode + "', @PrCode = '" + PrCode + "' ";
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
    
}