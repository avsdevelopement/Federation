using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public class ClsShareAllotTransaction
{
    DbConnection Conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    int Result = 0;

	public ClsShareAllotTransaction()
	{
		
	}

    public DataSet GetAllotTrans(string BrCode, string FromDate, string ToDate)
    {
        try
        {
            sql = "Exec RptShareTransactions @BrCode = '" + BrCode + "', @FromDate = '" + Conn.ConvertDate(FromDate).ToString() + "', @ToDate = '" + Conn.ConvertDate(ToDate).ToString() + "'";
            DT = new DataTable();
            DS = new DataSet();
            DT = Conn.GetDatatable(sql);

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