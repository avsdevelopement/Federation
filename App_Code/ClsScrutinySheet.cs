using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

public class ClsScrutinySheet
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    int Result = 0;

	public ClsScrutinySheet()
	{
		
	}

    public string GetCustName(string BrCode, string CustNo)
    {
        try
        {
            sql = "Select (CustName +'_'+ ConVert(VarChar(10), ConVert(BigInt, IsNull(CustNo, 0))) +'_'+ ConVert(VarChar(10), ConVert(BigInt, IsNull(Group_CustNo, 0)))) CustName From Master Where BrCd = '" + BrCode + "' And CustNo = '" + CustNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataSet GetScrutinySheet1(string BrCode, string AppNo, string CustNo, string WorkingDate)
    {
        try
        {
            sql = "Exec RptScrutinySheet1 '" + BrCode + "','" + AppNo + "','" + CustNo + "','" + conn.ConvertDate(WorkingDate).ToString() + "'";
            DT = new DataTable();
            DS = new DataSet();
            DT = conn.GetDatatable(sql);

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


    public DataSet GetScrutinySheet2(string BrCode, string AppNo, string CustNo, string WorkingDate)
    {
        try
        {
            sql = "Exec RptScrutinySheet2 '" + BrCode + "','" + AppNo + "','" + CustNo + "','" + conn.ConvertDate(WorkingDate).ToString() + "'";
            DT = new DataTable();
            DS = new DataSet();
            DT = conn.GetDatatable(sql);

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