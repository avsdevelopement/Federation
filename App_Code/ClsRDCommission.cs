using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ClsRDCommission
/// </summary>
public class ClsRDCommission
{
    DbConnection conn = new DbConnection();
    string sql = "";
    string Result = "";
	public ClsRDCommission()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable getRDAgentName(string brcd,string accno)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "select m.custname,M.CUSTNO from avs_acc a inner join master m on  a.custno=m.custno where a.brcd='" + brcd + "' and a.subglcode=24 and a.accno='" + accno + "'";
            DT = conn.GetDatatable(sql);
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable getRDCommission(string BRCD, string AGENTCODE, string FDATE, string TDATE)
    {
        DataTable DT = new DataTable();
        try
        {

            sql = "AVS5020_SP '"+conn.ConvertDate(FDATE)+"','"+conn.ConvertDate(TDATE)+"','"+BRCD+"','"+AGENTCODE+"'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;

    }
    public string AgentCommRD()
    {
        string RESULT = "";
        try
        {
            sql = "SELECT CHARGES FROM CHARGESMASTER WHERE CHARGESTYPE=1010";
            RESULT = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return RESULT;
    }

    public DataTable Get_AVS0204_DT(string FBRCD, string TBRCD, string FPRCD, string FDT, string TDT, string FL, string FLT, string PT)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec ISP_AVS0204 @FBrcd='" + FBRCD + "',@TBrcd='" + TBRCD + "',@Subgl='" + FPRCD + "',@PFDT='" + conn.ConvertDate(FDT) + "',@PTDT='" + conn.ConvertDate(TDT) + "',@Rate='" + FL + "',@CommRate='" + FLT + "',@TDSRate='" + PT + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
   
  
   
}