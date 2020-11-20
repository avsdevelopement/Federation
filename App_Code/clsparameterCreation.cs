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
/// <summary>
/// Summary description for clsparameterCreation
/// </summary>
public class clsparameterCreation
{
    DbConnection conn = new DbConnection();
    string sql;
    DataTable DT;
    int result;
    public clsparameterCreation()
    {
        //
        // TODO: Add constructor logic here
        //created by ashok misal..
        //
    }
    public int inseretdata(string listfield, string listvalue, string brcd)
    {

        try
        {
            sql = "insert into PARAMETER(LISTFIELD,LISTVALUE,BRCD)values('" + listfield + "','" + listvalue + "','" + brcd + "')";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ////WebMsgBox.Show(Ex.Message);
            return result = 0;
        }
    }
    public void bindbank(DropDownList ddlbank, string BRCD)//BRCD ADDED --Abhishek
    {
        sql = "select BRCD id, BANKNAME Name from BANKNAME where BRCD='" + BRCD + "'";
        conn.FillDDL(ddlbank, sql);
    }
    public int ModifyData(int LNO, string LDESC, string PID)
    {
        try
        {
            sql = "";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ////WebMsgBox.Show(Ex.Message);
            return result = 0;
        }
    }

    public int bindgrid(GridView Gview, string BRCD)//BRCD ADDED --Abhishek
    {
        sql = "Select ListField, ListValue, Brcd From Parameter Where BrCd = '" + BRCD + "' And ListField <> 'DayOpen' Order By ListField";
        int Result = conn.sBindGrid(Gview, sql);
        return Result;
    }

    public int changedata(string listfd, string listvalue, string brcd)//BRCD ADDED --Abhishek
    {
        sql = "update PARAMETER set LISTVALUE='" + listvalue + "' ,BRCD='" + brcd + "' where LISTFIELD='" + listfd + "' and BRCD='" + brcd + "'";
        result = conn.sExecuteQuery(sql);
        return result;
    }

    public int DeleteData(string listfield, string BRCD)//BRCD ADDED --Abhishek
    {
        sql = "delete from PARAMETER where LISTFIELD='" + listfield + "' and BRCD='" + BRCD + "'";
        result = conn.sExecuteQuery(sql);
        return result;
    }
    public int deletelookup(string lno)
    {
        sql = "delete from LOOKUPFORM where LNO='" + lno + "'";
        result = conn.sExecuteQuery(sql);
        return result;
    }
    public int insertloan(string loancode, string currency, string loanlimit, string roi, string penalint, string status, string date, string intrec, string loantype, string pgl, string ppl, string intcaltype, string lastintapp, string OIR)
    {
        sql = "insert into LOANGL(LOANGLCODE,CURRANCY,LOANLIMIT,ROI,PENALINT,STATUS,EFFECTIVEDATE,INTREC,LOANTYPE,PGL,PPL,INTCALTYPE,LASTINTAPP,OVERRESERVEGL)values('" + loancode + "','" + currency + "','" + loanlimit + "','" + roi + "','" + penalint + "','" + status + "','" + conn.ConvertDate(date) + "','" + intrec + "','" + loantype + "','" + pgl + "','" + ppl + "','" + intcaltype + "','" + lastintapp + "','" + OIR + "')";
        result = conn.sExecuteQuery(sql);
        return result;
    }
    public int modifyloan(string loancode, string currency, string loanlimit, string roi, string penalint, string status, string date, string intrec, string loantype, string pgl, string ppl, string intcaltype, string lastintapp, string OIR, string BRCD)//BRCD ADDED --Abhishek
    {
        sql = "update LOANGL set CURRANCY= '" + currency + "',LOANLIMIT= '" + loanlimit + "',ROI= '" + roi + "',PENALINT='" + penalint + "',STATUS='" + status + "',EFFECTIVEDATE='" + (date) + "',INTREC='" + intrec + "',LOANTYPE='" + loantype + "',PGL='" + pgl + "',PPL='" + ppl + "',INTCALTYPE='" + intcaltype + "',LASTINTAPP='" + lastintapp + "',OVERRESERVEGL='" + OIR + "' WHERE LOANGLCODE='" + loancode + "' and BRCD='" + BRCD + "'";
        result = conn.sExecuteQuery(sql);
        return result;
    }
    public int Deleteloan(string loancode, string BRCD)//BRCD ADDED --Abhishek
    {
        sql = "delete from LOANGL where LOANGLCODE='" + loancode + "' and BRCD='" + BRCD + "'";
        result = conn.sExecuteQuery(sql);
        return result;
    }
    public int autoid(string BRCD)//BRCD ADDED --Abhishek
    {
        sql = "select MAX(LOANGLCODE)+1 from LOANGL where BRCD='" + BRCD + "'";
        result = Convert.ToInt32(conn.sExecuteScalar(sql));
        return result;
    }
    public DataTable show(string loancode, string BRCD)//BRCD ADDED --Abhishek
    {
        sql = "select LOANTYPE,ROI,PGL,PPL,INTCALTYPE,CURRANCY,LOANLIMIT,PENALINT,STATUS,EFFECTIVEDATE,INTREC,LASTINTAPP,OVERRESERVEGL from LOANGL where LOANGLCODE='" + loancode + "' and BRCD='" + BRCD + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable showpara(string listfd, string BRCD)//BRCD ADDED --Abhishek
    {
        sql = "select LISTFIELD,LISTVALUE,BRCD from PARAMETER where LISTFIELD='" + listfd + "' and BRCD='" + BRCD + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }



}