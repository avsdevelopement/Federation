using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
/// <summary>
/// Summary description for ClsBalvikasIntCalc
/// </summary>
public class ClsBalvikasIntCalc
{
    DbConnection Conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsEncryptValue Ecry = new ClsEncryptValue();
    string EntryMid, verifyMid, DeleteMid = "";
    int ResInt = 0;
    string ResStr = "", sql = "";

	public ClsBalvikasIntCalc()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Calc_Balvikas(string FL, string Brcd, string FSubgl,string TSubgl, string FAccno, string TAccno, string Fromdate, string ToDate, string Mid, string Edt)
    {
        try
        {
            sql = "Exec Isp_Balvikas_IntCalc @FLAG='" + FL + "',@Brcd='" + Brcd + "',@FSubglcode='" + FSubgl + "',@TSubglcode='" + TSubgl + "',@FACC='" + FAccno + "',@TACC='" + TAccno + "',@FDATE='" + Conn.ConvertDate(Fromdate) + "',@TDATE='" + Conn.ConvertDate(ToDate) + "',@MID='" + Mid + "'";
            ResInt = Conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResInt;
    }
    public int ReCalc_Balvikas(string FL, string Mid)
    {
        try
        {
            sql = "Exec Isp_Balvikas_IntCalc @FLAG='" + FL + "',@MID='" + Mid + "'";
            ResInt = Conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResInt;
    }
    public DataTable GetReportCalc(string FL, string Brcd, string FSubgl, string TSubgl, string Mid)
    {
        try
        {
            sql = "Exec Isp_Balvikas_IntCalc @FLAG='" + FL + "',@Brcd='" + Brcd + "',@FSubglcode='" + FSubgl + "',@TSubglcode='" + TSubgl + "',@MID='" + Mid + "'";
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetReportTally(string FL, string Brcd, string FSubgl, string TSubgl, string TDATE, string Mid)
    {
        try
        {
            sql = "Exec Isp_Balvikas_IntCalc @FLAG='" + FL + "',@Brcd='" + Brcd + "',@FSubglcode='" + FSubgl + "',@TSubglcode='" + TSubgl + "',@TDATE='" + Conn.ConvertDate(TDATE) + "',@MID='" + Mid + "'";
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string PostEntry(string FL, string Brcd, string FSubgl, string TSubgl, string FAccno, string TAccno, string FDate, string Tdate, string PostDate, string Mid, string Pcmac)
    {
        try
        {
            EntryMid = Ecry.GetMK(Mid.ToString());

            sql = "Exec Isp_Balvikas_IntCalc @FLAG='" + FL + "',@Brcd='" + Brcd + "',@FSubglcode='" + FSubgl + "',@TSubglcode='" + TSubgl + "',@FACC='" + FAccno + "',@TACC='" + TAccno + "',@FDATE='" + Conn.ConvertDate(FDate) + "',@TDATE='" + Conn.ConvertDate(Tdate) + "',@PDATE='" + Conn.ConvertDate(PostDate) + "',@MID='" + Mid + "',@F1='" + EntryMid + "',@PCMAC='" + Pcmac + "'";
            ResStr = Conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResStr;
    }

}