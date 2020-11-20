using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsNoticeCharges
/// </summary>
public class ClsNoticeCharges
{
    string sql = "";
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
	public ClsNoticeCharges()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void InsertRecord(string Date, string Desc, string Secured, int Charges, int Taxes, string MID, string EntryDate)
    {
        string S = "";
        if (Secured == "1")
            S = "Y";
        else
            S = "N";
        try
        {
            sql = "insert into Avs_Notice_Chrg (EffectDate,Notice_Desc,Secured,Charges,Taxes,EntryDate,MID) values ('"+conn.ConvertDate(Date)+"','"+Desc+"','"+S+"',"+Charges+","+Taxes+",'"+conn.ConvertDate(EntryDate)+"','"+MID+"')";
            conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindGrid(GridView GrdNotice)
    {
        try
        {
            sql = "Select * from Avs_Notice_Chrg";
            conn.sBindGrid(GrdNotice, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}