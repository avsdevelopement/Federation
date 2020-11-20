using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
/// <summary>
/// Summary description for ClsVoucherActInfo
/// </summary>
public class ClsVoucherActInfo
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sql1 = "", sResult = "";
    int Result = 0, St = 0;

    public ClsVoucherActInfo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int GetInfo(GridView gd, string BRCD, string FL, string EDT, string ACT, string UCODE, string MID, string SETNO, string AMT)
    {
        try
        {
            sql = "EXEC SP_VOUCHER_ACTIVITY @FLAG='" + FL + "',@BRCD='" + BRCD + "',@DATE='" + conn.ConvertDate(EDT) + "',@ACTIVITY='" + ACT + "',@USERCODE='" + UCODE + "',@MID='" + MID + "',@SETNO='" + SETNO + "',@AMOUNT='" + AMT + "'";

            Result = conn.sBindGrid(gd, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int PrintUpdate(string BRCD, string EDT, string SETNO, string FL)
    {
        try
        {
            sql1 = "Exec UpdatePrintStatus @BRCD='" + BRCD + "',@EDate='" + conn.ConvertDate(EDT) + "',@SETNO='" + SETNO + "',@FLAG='" + FL + "'";
            St = conn.sExecuteQuery(sql1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return St;
    }
    public string GetParameter()//Dhanya Shetty//28/09/2017
    {
        try
        {
            sql = "select LISTVALUE from PARAMETER where LISTFIELD like '%PREP%'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetAdd1(string bankcd, string brcd)//Dhanya Shetty//28/09/2017
    {
        try
        {
            sql = "select ADDRESS1 from bankname where bankcd='" + bankcd + "' and brcd='" + brcd + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string GetAdd2(string bankcd, string brcd)//Dhanya Shetty//28/09/2017
    {
        try
        {
            sql = "select ADDRESS2 from bankname where bankcd='" + bankcd + "' and brcd='" + brcd + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string RegNo(string bankcd, string brcd)//Dhanya Shetty//28/09/2017
    {
        try
        {
            sql = "select REGISTRATIONNO from bankname where bankcd='" + bankcd + "' and brcd='" + brcd + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string Custno(string brcd, string subgl, string acc)//Dhanya Shetty//28/09/2017
    {
        try
        {
            sql = "select custno from avs_acc where brcd='" + brcd + "' and subglcode='" + subgl + "' and accno='" + acc + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string Schoolname(string custno)//Dhanya Shetty//28/09/2017
    {
        try
        {
            sql = "select p.descr from paymast p inner join master m on p.recdiv=m.BRANCHNAME and p.reccode=m.RECDEPT where m.CUSTNO='" + custno + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public DataTable GetLogo(string bankcode)//Dhanya Shetty//28/09/2017
    {
        try
        {
            sql = "select  photoImg from logo where bankcode='" + bankcode + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetReg(string bankcd, string brcd)//Dhanya Shetty//29/09/2017
    {
        try
        {
            sql = "select Regno from bankname where bankcd='" + bankcd + "' and brcd='" + brcd + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string GetDateyear(string bankcd, string brcd)//Dhanya Shetty//29/09/2017
    {
        try
        {
            sql = "select Dateyear from bankname where bankcd='" + bankcd + "' and brcd='" + brcd + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string GetChequeno(string brcd, string setno, string Edate)//Dhanya Shetty//29/09/2017
    {
        try
        {
            string TableName;
            string[] TD = Edate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "select INSTRUMENTNO from " + TableName + " where brcd='" + brcd + "' and setno='" + setno + "'  and entrydate='" + conn.ConvertDate(Edate).ToString() + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string GETCRDR(string BRCD, string FL, string EDT, string ACT, string UCODE, string MID, string SETNO, string AMT)
    {
        try
        {
            sql = "EXEC SP_VOUCHER_ACTIVITY @FLAG='" + FL + "',@BRCD='" + BRCD + "',@DATE='" + conn.ConvertDate(EDT) + "',@ACTIVITY='" + ACT + "',@USERCODE='" + UCODE + "',@MID='" + MID + "',@SETNO='" + SETNO + "',@AMOUNT='" + AMT + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string GetActi(string brcd, string setno, string Edate)
    {
        try
        {
            string TableName;
            string[] TD = Edate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select Top 1 Activity From " + TableName + " where BRCD='" + brcd + "' and Entrydate='" + conn.ConvertDate(Edate) + "' and Setno='" + setno + "' and STAGE<>'1004' And Pmtmode not in ('TR_INT','TR-INT') ";
            sResult = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
}