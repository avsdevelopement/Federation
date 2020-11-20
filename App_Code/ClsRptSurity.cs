using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsRptSurity
/// </summary>
public class ClsRptSurity
{
    string name = "";
    string rtn = "";
    string sql = "";
    int rtnint = 0;
    DbConnection conn = new DbConnection();
    DataTable dt = new DataTable();
    int Result, RM = 0;
	public ClsRptSurity()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GETLOANNAME(string AT, string BRCD)
    {
        try
        {
            sql = "SELECT GLNAME FROM GLMAST WHERE BRCD='"+BRCD+"' AND SUBGLCODE='"+AT+"'";
            AT = conn.sExecuteScalar(sql);
            return AT;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }
    public string getname(string AT, string BRCD,string accno)
    {
        try
        {
            sql = "select m.custname from avs_acc a inner join master m on a.custno=m.custno where a.brcd='"+BRCD+"' and a.subglcode='"+AT+"' and a.accno='"+accno+"' and acc_status<>3"; //unification 
            AT = conn.sExecuteScalar(sql);
            return AT;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }
    public DataTable GetCustData(string brcd, string accno, string subgl)
    {
        DataTable dtC = new DataTable();
        try
        {
            sql = "select m.custname,ad.FLAT_ROOMNO from avs_acc A inner join master m on a.custno=m.custno left join addmast AD on  m.CUSTNO=ad.CUSTNO and AD.BRCD=M.BRCD AND AD.ADDTYPE=1 where A.BRCD='" + brcd + "' and a.ACCNO='" + accno + "' and a.SUBGLCODE='" + subgl + "'"; //unification 
            dtC = conn.GetDatatable(sql);    //ADDED BY ASHOK MISAL FOR AD.BRCD =M.BRCD DATE=30/01/2018  
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dtC;
    }
    public string Getcustno(string brcd, string accno, string subgl)
    {
        try
        {
            sql = "select m.custno from master m inner join  avs_acc a on a.custno=m.custno " +
                    "where a.brcd='" + brcd + "' and a.subglcode='" + subgl + "' and a.accno='" + accno + "' ";
            brcd = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return brcd;
    }
    public string GetSurityNo(string brcd, string accno, string subgl)
    {
        try
        {
            sql = "select S.MemberNo from AVSLnSurityTable S  left join master m on m.brcd=S.BRCD  and m.custno=S.CustNo left join avs_acc b on b.brcd='1' and b.ACCNO=S.MemberNo and  b.custno=m.custno and b.glcode='4' " +
         "  where m.brcd='" + brcd + "' and s.subglcode='" + subgl + "' and s.accno='" + accno + "' and  s.lntype='Surity'";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return brcd;
    }
    public int NoticeLogs(string brcd, string noticeno, string Name, string loancode, string accno, string edate, string mid, string Others)
    {
        int Result = 0;
        try
        {
            sql = "insert into AVSNoticeLogs (NoticeNo,NoticeName,Brcd,LoanCode,LoanAccno,Entrydate,Mid,SystemDate,Others) values('" + noticeno + "','" + Name + "','" + brcd + "','" + loancode + "','" + accno + "','" + conn.ConvertDate(edate) + "','" + mid + "',Getdate(),'" + Others + "')";
             Result = conn.sExecuteQuery(sql);
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string GetAddressSelected(string Flag,string BRCD, string Subglcode, string accno)
    {
        sql = "exec ISP_AVS0100 @MAINFLAG='LOAN', @FLAG='" + Flag + "',@BRCD='" + BRCD + "',@SUBGLCODE='" + Subglcode + "',@ACCNO='" + accno + "'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string GetSurityNO(string AT)
    {
        try
        {
            sql = "SELECT SURNO FROM LoanGl WHERE BRCD='" + AT + "' ";
            AT = conn.sExecuteScalar(sql);
            return AT;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }
}