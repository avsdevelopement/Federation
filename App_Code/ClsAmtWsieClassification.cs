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
/// Summary description for ClsAmtWsieClassification
/// </summary>
public class ClsAmtWsieClassification
{
    DataTable Dt = new DataTable();
    DataTable Dt1 = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";
    string sql1 = "";

	public ClsAmtWsieClassification()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetLaonAmtClassWise(string BRCD, string AsOnDate, string FL)
    {
        try
        {
            if (FL == "D")
            {
                sql = "Exec RptLOANSlabWiseDetails '" + BRCD + "','" + Conn.ConvertDate(AsOnDate) + "' ";
                Dt = Conn.GetDatatable(sql);
            }
            else
            {

                sql = "Exec RptLOANSlabWiseSummary '" + BRCD + "','" + Conn.ConvertDate(AsOnDate) + "' ";
                Dt = Conn.GetDatatable(sql);
            }

        }
        catch (Exception)
        {

            throw;
        }
        return Dt;
    }
    public DataTable GetLaonAmtClassWiseDet(string BRCD, string AsOnDate)
    {
        try
        {

            sql = "Exec RptLOANSlabWiseDT_1 '" + BRCD + "','" + Conn.ConvertDate(AsOnDate) + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
}