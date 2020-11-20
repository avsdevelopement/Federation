using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public class clsTDAparameter
{
    DbConnection conn = new DbConnection();
    string sql;
    int Result;
    DataTable DT = new DataTable();
	
    public clsTDAparameter()
	{
		
	}

    public int autoid(string BRCD)
    {
        sql = "select MAX(DEPOSITGLCODE)+1 from DEPOSITGL where BRCD='" + BRCD + "'";
       int  result = Convert.ToInt32(conn.sExecuteScalar(sql));
        return result;
    }

    public int autoiddeposit(string BRCD)
    {
        sql = "Select Max(DEPOSITGLCODE) + 1 From DEPOSITGL Where BRCD = '" + BRCD + "'";
        int result = Convert.ToInt32(conn.sExecuteScalar(sql));
        return result;
    }


    public DataTable show(string DEPOSITGLCODE, string BRCD)
    {
        sql = "Select DEPOSITGLCODE, DEPOSITTYPE, STATUS, CATEGORY, INTERESTTYPE1, PLACCNO, DEPOSITGLBALANCE,INTPAY,AMTVAL From DEPOSITGL Where DEPOSITGLCODE='" + DEPOSITGLCODE + "' And BRCD='" + BRCD + "' ";
        DT = conn.GetDatatable(sql);
        return DT;
    }

    public int insertloan(string brcd, string dcode, string dtype, string status, string category, string Percent, string inttype, string placc, string INTPAY, string AMTVAL)
    {
        sql = "Insert Into DEPOSITGL(BRCD,DEPOSITGLCODE, DEPOSITTYPE, STATUS, CATEGORY, DEPOSITGLBALANCE, INTERESTTYPE1, PLACCNO,INTPAY,AMTVAL) Values('" + brcd + "','" + dcode + "','" + dtype + "','" + status + "','" + category + "', case when '" + Percent + "'='' then null else '" + Percent + "' end ,case when '" + inttype + "'='0' then null else '" + inttype + "' end,case when '" + placc + "'='' then null else '" + placc + "' end,'" + INTPAY + "','" + AMTVAL + "')";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }


    public int modifydeposit(string dcode, string dtype, string status, string category, string Percent, string inttype, string placc, string BRCD, string INTPAY, string AMTVAL)
    {
        //  sql = "Update DEPOSITGL Set DEPOSITTYPE = '" + dtype + "', STATUS = '" + status + "', CATEGORY = '" + category + "', DEPOSITGLBALANCE = '" + Percent + "', INTERESTTYPE1 = '" + inttype + "', PLACCNO = '" + placc + "' Where DEPOSITGLCODE = '" + dcode + "' And BRCD = '" + BRCD + "'";
        sql = "Update DEPOSITGL Set DEPOSITTYPE = '" + dtype + "', STATUS = '" + status + "', CATEGORY = '" + category + "',  DEPOSITGLBALANCE=case when '" + Percent + "'='' then null else '" + Percent + "' end ,INTERESTTYPE1=case when '" + inttype + "'='0' then null else '" + inttype + "' end,PLACCNO=case when '" + placc + "'='' then null else '" + placc + "' end,INTPAY='" + INTPAY + "',AMTVAL='" + AMTVAL + "' Where DEPOSITGLCODE = '" + dcode + "' And BRCD = '" + BRCD + "'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }

    public int delete(string dcode, string BRCD)
    {
        sql = "Delete From DEPOSITGL Where DEPOSITGLCODE = '" + dcode + "' And BRCD = '" + BRCD + "'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }

    public void bindinttype(DropDownList ddlloan)
    {
        sql = "Select SRNO id, DESCRIPTION Name From LOOKUPFORM1 Where LNO = 1015";
        conn.FillDDL(ddlloan, sql);
    }
}