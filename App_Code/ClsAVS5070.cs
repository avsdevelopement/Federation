using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

public class ClsAVS5070
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    int Result = 0;

	public ClsAVS5070()
	{
		
	}

    public DataSet GetShareSusData(string BrCode, string FrDate, string ToDate)
    {
        try
        {
            sql = "Exec ISP_AVS0103 @BrCode = '" + BrCode + "', @FrDate = '" + conn.ConvertDate(FrDate).ToString() + "', @ToDate = '" + conn.ConvertDate(ToDate).ToString() + "'";
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