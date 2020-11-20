using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public class ClsGetSharesApplication
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sResult = "", sql = "";
    int Result;

	public ClsGetSharesApplication()
	{
		
	}

    public DataSet GetShrApp(string FrBrCode, string ToBrCode, string PrCode, string Fdate, string Tdate, string FL, string SGL)
    {
        try
        {
            sql = "Exec RptSharesApplicationList @FrBrcode='" + FrBrCode + "', @ToBrCode='" + ToBrCode + "', @ShrSuspGl = '" + PrCode + "' ,@Fromdate='" + conn.ConvertDate(Fdate) + "', @Todate='" + conn.ConvertDate(Tdate) + "',@AType='" + FL + "',@Type='" + SGL + "'";
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