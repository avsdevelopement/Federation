using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsClgReturnReg
/// </summary>
public class ClsClgReturnReg
{
    DataTable DT;
    int Res = 0;
    string STR = "";
    string sql = "";

    DbConnection conn = new DbConnection();
	public ClsClgReturnReg()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetReturnReg(string FL, string SFL, string FBRCD, string TBRCD, string FDT,string TDT)
    {
        try
        {
            sql = "EXEC Isp_IWOW_ReturnReg @Flag='" + FL + "',@SFlag='" + SFL + "',@FDate='" + conn.ConvertDate(FDT) + "',@TDate='" + conn.ConvertDate(TDT) + "',@Fbrcd='" + FBRCD + "',@TBrcd='" + TBRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}