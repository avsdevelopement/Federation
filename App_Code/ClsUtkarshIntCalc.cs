using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
/// <summary>
/// Summary description for ClsUtkarshIntCalc
/// </summary>
public class ClsUtkarshIntCalc
{
    DbConnection Conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsEncryptValue Ecry = new ClsEncryptValue();
    string EntryMid, verifyMid, DeleteMid = "";
    int ResInt = 0;
    string ResStr = "", sql = "";

	public ClsUtkarshIntCalc()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   
	
    public int Calc_Utk(string FL, string FBrcd, string TBrcd, string Subgl, string FAccno, string TAccno, string Fromdate, string ToDate, string Mid, string Edt)
    {
        try
        {
            sql = "Exec Isp_Utkarsh_IntCalc @FLAG='" + FL + "',@FBRCD='" + FBrcd + "',@TBRCD='" + TBrcd + "',@Subglcode='" + Subgl + "',@FACC='" + FAccno + "',@TACC='" + TAccno + "',@FDATE='" + Conn.ConvertDate(Fromdate) + "',@TDATE='" + Conn.ConvertDate(ToDate) + "',@MID='" + Mid + "'";
            ResInt = Conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResInt;
    }
    public int ReCalc_Utk(string FL, string Mid)
    {
        try
        {
            sql = "Exec Isp_Utkarsh_IntCalc @FLAG='" + FL + "',@MID='" + Mid + "'";
            ResInt = Conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResInt;
    }
  

    

    public string PostEntry(string FL,string Fbrcd,string Tbrcd,string Subgl,string FAccno,string TAccno,string FDate,string Tdate,string PostDate,string Mid,string Pcmac)
    {
        try
        {
            EntryMid = Ecry.GetMK(Mid.ToString());

            sql = "Exec Isp_Utkarsh_IntCalc @FLAG='" + FL + "',@FBRCD='" + Fbrcd + "',@TBRCD='" + Tbrcd + "',@Subglcode='" + Subgl + "',@FACC='" + FAccno + "',@TACC='" + TAccno + "',@FDATE='" + Conn.ConvertDate(FDate) + "',@TDATE='" + Conn.ConvertDate(Tdate) + "',@PDATE='" + Conn.ConvertDate(PostDate) + "',@MID='" + Mid + "',@F1='" + EntryMid + "',@PCMAC='" + Pcmac + "'";
            ResStr = Conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResStr;
    }

}