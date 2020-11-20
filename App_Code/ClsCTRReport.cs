using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public class ClsCTRReport
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsCTRReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int GetCTR(GridView Gview,string FPT, string TPT, string FDT, string TDT, string CTRL)
    {
        try
        {
            string[] FT = FDT.Split('/');
            string[] TT = TDT.Split('/');
            sql = "Exec SP_CTRReport @FYEAR='" + FT[2].ToString() + "' ,@TYEAR='" + TT[2].ToString() + "',@FMONTH='" + FT[1].ToString() + "' ,@TMONTH='" + TT[1].ToString() + "' ,@FSGL='" + FPT + "' ,@TSGL='" + TPT + "' ,@CTRLIMIT='" + CTRL + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetCTRTable( string FPT, string TPT, string FDT, string TDT, string CTRL)
    {
        try
        {
            string[] FT = FDT.Split('/');
            string[] TT = TDT.Split('/');
            sql = "Exec SP_CTRReport @FYEAR='" + FT[2].ToString() + "' ,@TYEAR='" + TT[2].ToString() + "',@FMONTH='" + FT[1].ToString() + "' ,@TMONTH='" + TT[1].ToString() + "' ,@FSGL='" + FPT + "' ,@TSGL='" + TPT + "' ,@CTRLIMIT='" + CTRL + "'";
            DT = new DataTable();
            DT= conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    } 
}