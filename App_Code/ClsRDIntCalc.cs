using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
/// <summary>
/// Summary description for ClsRDIntCalc
/// </summary>
public class ClsRDIntCalc
{
    DbConnection Conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsEncryptValue Ecry = new ClsEncryptValue();
    string EntryMid, verifyMid, DeleteMid = "";
    int ResInt = 0;
    string ResStr = "", sql = "";

	public ClsRDIntCalc()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    
   
  
    public DataTable GetReportTally(string FL, string FBrcd, string TBrcd, string Subglcode,string TDATE, string Mid)
    {
        try
        {
            sql = "Exec Isp_Rd_IntCalc @FLAG='" + FL + "',@FBRCD='" + FBrcd + "',@TBRCD='" + TBrcd + "',@TDATE='" + Conn.ConvertDate(TDATE) + "',@Subglcode='" + Subglcode + "',@MID='" + Mid + "'";
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string PostEntry(string FL,string Fbrcd,string Tbrcd,string Subgl,string FAccno,string TAccno,string FDate,string Tdate,string PostDate,string Mid,string Pcmac)
    {
        try
        {
            EntryMid = Ecry.GetMK(Mid.ToString());

            sql = "Exec Isp_Rd_IntCalc @FLAG='" + FL + "',@FBRCD='" + Fbrcd + "',@TBRCD='" + Tbrcd + "',@Subglcode='" + Subgl + "',@FACC='" + FAccno + "',@TACC='" + TAccno + "',@FDATE='" + Conn.ConvertDate(FDate) + "',@TDATE='" + Conn.ConvertDate(Tdate) + "',@PDATE='" + Conn.ConvertDate(PostDate) + "',@MID='" + Mid + "',@F1='" + EntryMid + "',@PCMAC='" + Pcmac + "'";
            ResStr = Conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResStr;
    }

}