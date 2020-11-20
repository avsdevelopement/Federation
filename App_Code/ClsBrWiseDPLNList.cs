using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsBrWiseDPLNList
/// </summary>
public class ClsBrWiseDPLNList
{
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsBrWiseDPLNList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet GetBrWiseDeposit(string BrCode, string AsOnDate, string Type)
    {
        try
        {
            sql = "Exec Isp_AVS0032 @BrCd='" + BrCode + "', @AsOnDate='" + Conn.ConvertDate(AsOnDate) + "',@Type='" + Type + "'";
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
    public DataSet GetBrWiseDepositWithPMonth(string BrCode, string AsOnDate, string Type)
    {
        try
        {
            sql = "Exec Isp_AVS0033 @BrCd='" + BrCode + "', @AsOnDate='" + Conn.ConvertDate(AsOnDate) + "',@Type='" + Type + "'";
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
    public DataSet GetBrWiseLoan(string BrCode, string AsOnDate, string Type)
    {
        try
        {
            sql = "Exec Isp_AVS0034 @BrCd='" + BrCode + "', @AsOnDate='" + Conn.ConvertDate(AsOnDate) + "',@Type='" + Type + "'";
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
    public DataSet GetBrWiseLoanWithPMonth(string BrCode, string AsOnDate, string Type)
    {
        try
        {
            sql = "Exec Isp_AVS0035  @BrCd='" + BrCode + "', @AsOnDate='" + Conn.ConvertDate(AsOnDate) + "',@Type='" + Type + "'";
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
    public DataSet RptBrwiseLoanDT_PrCr(string BrCode, string AsOnDate, string Type)
    {
        try
        {
            sql = "Exec Isp_DT_AVS0035  @BrCd='" + BrCode + "', @AsOnDate='" + Conn.ConvertDate(AsOnDate) + "',@Type='" + Type + "'";
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