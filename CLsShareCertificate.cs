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
/// Summary description for CLsShareCertificate
/// </summary>
public class CLsShareCertificate
{
    DbConnection conn = new DbConnection();
    string Result, sql;
    DataTable DT = new DataTable();
	public CLsShareCertificate()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string getBRCDname(string brcd)
    {
        sql = "select midname from bankname where brcd='"+brcd+"'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public int BindData(GridView Gview, string BrCode, string accno)
    {
        int Result = 0;
        try
        {
            sql = "select s.CustNo,s.CustAccNo,m.custname,s.sharefrom,s.shareto,s.totalshares,s.sharesvalue,s.totalshareamt,s.cert_no,s.cert_issue1stdate from SHARESINFO s LEFT join master m on s.custno=m.custno where S.CUSTACCNO='"+accno+"' AND M.BRCD='"+BrCode+"'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string Acname(string brcd, string accno)
    {
        sql = "select m.custname from SHARESINFO s LEFT join master m on s.custno=m.custno where S.CUSTACCNO='"+accno+"' AND M.BRCD='"+brcd+"'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public DataTable GetShareCerti(string BRCD,string AccNo)
    {
        sql = "EXEC Isp_AVS0001 '"+BRCD+"','"+AccNo+"'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public int InsertDataOnline(string BRCD,string BANKCODE,string BANKNAME,string BRCODE,string BRANCHNAME,string RECIEPTNO,string RECIEPTNAME,string GLGROUP,string BOARDRESNO,string BMEETINGDATE,string OPENINGDATE,string ENTRYDATE,string ACCNO,string CRGL,string RECGL,string CRAC,string RECAC,string PRACCNAME,string INTPAY,string DEPOSITAMT,string PERIOD,string INRATE,string INTAMT,string MATAMT,string DUEDATE,string PERIODTYPE )
    {
        int Result = 0;
        string Flag="AD";
        try
        {
            sql = "EXEC checksp @FLAG='" + Flag + "',@BRCD='" + BRCD + "',@BANKCODE='" + BANKCODE + "',@BANKNAME='" + BANKNAME + "',@BRCODE='" + BRCODE + "',@BRANCHNAME='" + BRANCHNAME + "',@RECEIPTNO='" + RECIEPTNO + "',@RECEIPTNAME='" + RECIEPTNAME + "',@SUBGLCODE='" + BANKCODE + "',@GLGRP='" + GLGROUP + "',@BOARDRESNO='" + BOARDRESNO + "',@BOARDMEETINGDATE='" + conn.ConvertDate(BMEETINGDATE) + "',@OPENINGDATE='" + conn.ConvertDate(OPENINGDATE) + "',@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "',@ACCNO='" + ACCNO + "',@CRGL='" + CRGL + "',@RECGL='" + RECGL + "',@CRAC='" + CRAC + "',@RECAC='" + RECAC + "',@PRACCNAME='" + PRACCNAME + "',@INTPAY='" + INTPAY + "',@DEPOSITAMT='" + DEPOSITAMT + "',@PERIOD='" + PERIOD + "',@INTRATE='" + INRATE + "',@INTAMT='" + INTAMT + "',@MATURITYAMT='" + MATAMT + "',@DUEDATE='" + conn.ConvertDate(DUEDATE) + "',@PeriodType='" + PERIODTYPE + "'";
        //    sql = "Exec Isp_AVS0004  @Flag='"+Flag+"',@PrAcc='" + PrAccNo + "',@PrPrd='" + PrProdCode + "',@InAcc='" + InAccNo + "',@InPrd='" + InProdCode + "',@BrCode='" + BrCode + "', @BankCode='" + BankNo + "', @BankName='" + BankName + "', @BranchCode='" + BranchNo + "', @Branchname='" + BranchName + "', @ReceiptNo='" + RecNo + "', @ReceiptName='" + RecName + "', @BoardResNo = '" + BResNumber + "', @BoardMeetDate = '" + conn.ConvertDate(BMeetDate).ToString() + "', @OpeningDate='" + conn.ConvertDate(OpDate).ToString() + "', @Stage='1001', @MID='" + Mid + "', @PCMAC='" + conn.PCNAME().ToString() + "', @EntryDate='" + conn.ConvertDate(EDate).ToString() + "',@InvType='" + InvType + "',@AC1='" + AC1 + "',@Bank='" + Bank + "',@PrAccName='" + PrAccName + "',@ASONDATE='" + conn.ConvertDate(asondate) + "',@INTPAY='" + intpay + "',@DEPOSITAMT='" + depoamt + "',@PERIOD='" + period + "',@INTRATE='" + rateint + "',@INTAMT='" + intamt + "',@MATURITYAMT='" + matamt + "',@DUEDATE='" + conn.ConvertDate(duedate) + "',@PeriodType='" + periodtype + "'"; 
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int ModifyDataOnline(string BrCode, string BankNo, string BankName, string BranchNo, string BranchName, string RecNo, string RecName, string BResNumber, string BMeetDate, string OpDate, string EDate, string Mid, string PrAccNo, string PrProdCode, string InAccNo, string InProdCode, string InvType, string AC1, string Bank, string PrAccName, string asondate, string intpay, string depoamt, string period, string rateint, string intamt, string matamt, string duedate, string periodtype)
    {
        int Result = 0;
        string Flag = "MD";
        try
        {
            sql = "Exec Isp_AVS0004  @Flag='" + Flag + "',@PrAcc='" + PrAccNo + "',@PrPrd='" + PrProdCode + "',@InAcc='" + InAccNo + "',@InPrd='" + InProdCode + "',@BrCode='" + BrCode + "', @BankCode='" + BankNo + "', @BankName='" + BankName + "', @BranchCode='" + BranchNo + "', @Branchname='" + BranchName + "', @ReceiptNo='" + RecNo + "', @ReceiptName='" + RecName + "', @BoardResNo = '" + BResNumber + "', @BoardMeetDate = '" + conn.ConvertDate(BMeetDate).ToString() + "', @OpeningDate='" + conn.ConvertDate(OpDate).ToString() + "', @Stage='1001', @MID='" + Mid + "', @PCMAC='" + conn.PCNAME().ToString() + "', @EntryDate='" + conn.ConvertDate(EDate).ToString() + "', @Flag='AD',@InvType='" + InvType + "',@AC1='" + AC1 + "',@Bank='" + Bank + "',@PrAccName='" + PrAccName + "',@ASONDATE='" + conn.ConvertDate(asondate) + "',@INTPAY='" + intpay + "',@DEPOSITAMT='" + depoamt + "',@PERIOD='" + period + "',@INTRATE='" + rateint + "',@INTAMT='" + intamt + "',@MATURITYAMT='" + matamt + "',@DUEDATE='" + conn.ConvertDate(duedate) + "',@PeriodType='" + periodtype + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string getglname(string brcd, string subglcode)
    {
        sql = "select GLNAME from glmast where  brcd='"+brcd+"' and glgroup='INV'  AND SUBGLCODE='"+subglcode+"'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string GETMAXACCNO(string brcd, string subglcode)
    {
        sql = "SELECT MAX(CUSTACCNO+1) FROM AVS_InvAccountMaster WHERE SUBGLCODE='" + subglcode + "' AND BRCD='"+brcd+"'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string  GETCLOSING(string brcd, string subglcode,string EDATE)
    {
        sql = "exec INVCLOSING '"+brcd+"','"+conn.ConvertDate(EDATE)+"','"+subglcode+"'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }

  
} 