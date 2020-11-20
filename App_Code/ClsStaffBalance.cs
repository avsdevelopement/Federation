using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsStaffBalance
/// </summary>
public class ClsStaffBalance
{
    DbConnection Conn = new DbConnection();
    DataTable DT=new DataTable();
    int IntRes = 0;
    string StrRes = "", sql = "";
	public ClsStaffBalance()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetStaffBalanceReport(string BRCD,string ASON)
    {
        try
        {
            sql = "Exec Isp_AVS0047 @Flag='STB',@Brcd='" + BRCD + "',@AsOnDate='" + Conn.ConvertDate(ASON) + "'";
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int UpdateSBGL(string FL, string Brcd, string MID, string S1, string S2, string S3, string S4, string S5, string S6, string S7, string S8, string S9, string S10)
    {
        try
        {
            sql = "Exec Isp_AVS0047 @Flag='" + FL + "',@Brcd='" + Brcd + "',@Subgl1='" + S1 + "',@Subgl2='" + S2 + "',@Subgl3='" + S3 + "',@Subgl4='" + S4 + "',@Subgl5='" + S5 + "',@Subgl6='" + S6 + "',@Subgl7='" + S7 + "',@Subgl8='" + S8 + "',@Subgl9='" + S9 + "',@Subgl10='" + S10 + "',@Mid='" + MID + "'";
            IntRes=Conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntRes;
    }
    public int GetSbglPara(GridView GD,string FL,string Brcd)
    {
        try
        {
            sql = "Exec Isp_AVS0047 @Flag='" + FL + "',@Brcd='" + Brcd + "'";
            IntRes = Conn.sBindGrid(GD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntRes;
    }
}