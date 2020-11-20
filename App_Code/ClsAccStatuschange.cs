using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for ClsAccStatuschange
/// </summary>
public class ClsAccStatuschange
{
    DbConnection conn = new DbConnection();
    string Result, sql;
    DataTable DT = new DataTable();
    public ClsAccStatuschange()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND M.BRCD=AC.BRCD WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string Getaccno(string AT, string BRCD)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),GLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public void BindSTATUS(DropDownList DDL)
    {
        sql = "SELECT DESCRIPTION name,SRNO id from LOOKUPFORM1 WHERE LNO=1047 ORDER BY SRNO";
        conn.FillDDL(DDL, sql);
    }
    public string getproname(string glcode, string BRCD)
    {
        sql = "select glname from glmast where glcode='" + glcode + "' and brcd='" + BRCD + "'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string getaccname(string AccNo, string SubGlCode, string BrCode)
    {
        sql = "Select M.CustName From Master M Inner Join Avs_Acc Ac With(NoLock) ON M.brcd = Ac.brcd And M.CustNo = Ac.CustNo Where Ac.Brcd = '" + BrCode + "' and Ac.SubGlCode = '" + SubGlCode + "' And Ac.AccNo = '" + AccNo + "'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public DataTable showdata(string ACCNO, string PCODE, string BRCD)
    {
        DataTable DT = new DataTable();
        sql = "select distinct M.custname,A.ACC_STATUS from master m inner join avs_acc a on m.brcd=a.brcd and m.custaccno=a.accno  where a.glcode='" + PCODE + "' and a.accno='" + ACCNO + "' AND A.BRCD='" + BRCD + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public int updateaccstatus(string glcode, string accno, string accstatus, string brcd)
    {
        sql = "update avs_acc set acc_status='" + accstatus + "' where glcode='" + glcode + "' and accno='" + accno + "' and BRCD='" + brcd + "'";
        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
}