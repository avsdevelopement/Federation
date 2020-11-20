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
/// Summary description for ClsLienMark craeted by ashok for lien mark  
/// </summary>
/// 
public class ClsLienMark
{
    DbConnection conn = new DbConnection();
    string Result, sql;
    int IntResult = 0;
    DataTable DT = new DataTable();
    string FL = "";
    int Res = 0;
	public ClsLienMark()
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
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "' AND AC.ACC_STATUS IN(1,4)"; // AND M.BRCD=AC.BRCD UIFICATION 
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public void Bindacctype(DropDownList DDL)
    {
        sql = "SELECT DESCRIPTION name,SRNO id from LOOKUPFORM1 WHERE LNO=1053 ORDER BY SRNO";
        conn.FillDDL(DDL, sql);
    }
    public int getdeposite(GridView gView,string BrCode, string DGL,string DACCNO)
    {
        try
        {
            sql = "SELECT DEPCUSTNO,DEPOSITGLCODE,DEPOSITACCNO,DEPOSITAMT,LOANGLCODE,LOANACCNO,LOANAMT FROM AVS_LIENMARKDETAILS WHERE DEPOSITGLCODE='"+DGL+"' AND DEPOSITACCNO='"+DACCNO+"' AND BRCD='"+BrCode+"' and status=4 and stage<>1004";
            IntResult = conn.sBindGrid(gView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntResult;
    }

    public string getglname(string glcode, string BRCD)
    {
        sql = "SELECT DISTINCT GLNAME FROM GLMAST WHERE subglcode='"+glcode+"' AND BRCD='"+BRCD+"'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string GETSUBGL(string GLCODE, string SUBGL, string BRCD)
    {
        sql = "SELECT GLNAME FROM GLMAST WHERE GLCODE='" + GLCODE + "' AND SUBGLCODE='" + SUBGL + "' AND BRCD='" + BRCD + "'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string  GETDEPOSITINFO(string SUBGLCODE, string ENTRYDATE, string BRCD, string ACCNO)
    {
        sql = "SP_OpClBalance @BrCode='"+BRCD+"',@SubGlCode='"+SUBGLCODE+"',@AccNo='"+ACCNO+"',@EDate='"+conn.ConvertDate(ENTRYDATE)+"',@Flag='ClBal'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public DataTable GETACCNAME(string BRCD, string SUBGLCODE, string ACCNO)
    {
        sql = "SELECT  C.OPENINGDATE, M.CUSTNAME FROM AVS_ACC C INNER JOIN MASTER M ON C.BRCD=M.BRCD AND C.ACCNO=M.CUSTACCNO WHERE C.BRCD='"+BRCD+"' AND C.ACCNO='"+ACCNO+"' AND C.SUBGLCODE='"+SUBGLCODE+"' AND C.STAGE<>1004";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public string getloanglname(string glcode, string BRCD)
    {
        sql = "SELECT DISTINCT GLNAME FROM GLMAST  WHERE GLGROUP='LNV' and subglcode='" + glcode + "' AND BRCD='" + BRCD + "'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public DataTable getloandata(string BRCD,string SUBGL,string ACCNO)
    {
        sql = "SELECT Convert(Varchar(10),C.OPENINGDATE,103) as OPENINGDATE,ln.limit AS LIMIT FROM AVS_ACC C inner Join LoanInfo Ln On Ln.Brcd=C.Brcd And Ln.LoanGlcode=C.Subglcode And Ln.CustAccno = C.Accno WHERE C.BRCD='"+BRCD+"' AND C.ACCNO='"+ACCNO+"' AND C.SUBGLCODE='"+SUBGL+"' AND C.STAGE<>1004";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public int insertlienmark(string FL, string BRCD, string custno, string DCODE, string DACCNO, string DAMT, string LCODE, string LACCNO, string LOANAMT, string DDATE, string ENTRYDATE, string loanbrcd)
    {
        try
        {
            if (FL == "INSERT")
            {
                sql = "Exec SP_LIENMARKS @flag='" + FL + "',@BRCD='" + BRCD + "',@CustNo='" + custno + "',@DEPOSITGLCODE='" + DCODE + "',@DEPOSITACCNO='" + DACCNO + "',@DEPOSITAMT='" + DAMT + "',@LOANGLCODE='" + LCODE + "',@LOANACCNO='" + LACCNO + "',@LOANAMT='" + LOANAMT + "',@DEPOPENDATE='" + conn.ConvertDate(DDATE) + "',@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "',@PCMAC='" + conn.PCNAME() + "',@STAGE=1003,@MID=6,@STATUS=4,@loanbrcd='"+loanbrcd+"'";
                 Res = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    public string getdate(string BRCD, string DGL, string ACCNO)
    {
        sql = "select Convert(Varchar(10),openingdate,103) from depositinfo where depositglcode='"+DGL+"' and custaccno='"+ACCNO+"' and brcd='"+BRCD+"'";
        Result=conn.sExecuteScalar(sql);
        return Result;
    }
    public DataTable GETDATA( string FL,string SUBGL, string ACCNO,string BRCD)
    {

        sql = "Exec SP_LIENSELECT @BRCD='" + BRCD + "',@DEPOSITGLCODE='" + SUBGL + "',@DEPOSITACCNO='" + ACCNO + "',@FLAG='"+FL+"'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public int deletelien( string FL,string brcd, string sugl, string accno)
    {
        //sql = "update Avs_Lienmarkdetails set stage=1004 where brcd='"+brcd+"' and DepositGlCode='"+sugl+"' and DepositAccNo='"+accno+"'";
        //int Result = conn.sExecuteQuery(sql);
       
        try
        {
            if (FL == "CANCEL")
            {
                sql = "Exec SP_LIENMARKS   @flag='" + FL + "',@BRCD='" + brcd + "',@DEPOSITGLCODE='" + sugl + "',@DEPOSITACCNO='" + accno + "' ";
                IntResult = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntResult;
    }
    public DataTable GetLienmark(string BRCD, string FDATE, string TDATE, string PCODE)
    {
        string Glcode = ""; string Flag = "";
        Glcode = GetGlCode(BRCD, PCODE);
        if (Glcode == "3")
        {
            Flag = "3";
           
        }
        else
        {
            Flag = "5";
           
        }
        sql = "EXEC Sp_LienMarkRpt @BRCD='" + BRCD + "',@FDT='" + conn.ConvertDate(FDATE) + "',@TDT='" + conn.ConvertDate(TDATE) + "',@PRODUCTCODF='" + PCODE + "',@FLAG='" + Flag + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable GetLienmarktype(string BRCD1,string BRCD2,string FDATE, string TDATE,string TYPE,string PCODE)
    {
        sql = "EXEC SP_LIENMARKRPTNEW @BRCD1='" + BRCD1 + "',@BRCD2='" + BRCD2 + "',@FDT='" + conn.ConvertDate(FDATE) + "',@TDT='" + conn.ConvertDate(TDATE) + "',@PRODUCTCODF='" + PCODE + "',@LIENTYPE='"+TYPE+"'";
        DT = conn.GetDatatable(sql);
        return DT;  
    }
    public DataTable GETAGENT( string FL,string FDATE, string TDATE, string PCODE,string BRCD)
    {
        sql = "EXEC SP_AGENTREPORT '"+FL+"','"+conn.ConvertDate(FDATE)+"','"+conn.ConvertDate(TDATE)+"','"+PCODE+"','"+BRCD+"'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable GetUserDetails(string BRCD, string MID)
    {
        sql = "EXEC ISP_AVS0128 '"+BRCD+"','"+MID+"'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable LienLoanAgainstDp( string PCODE, string BRCD,string fdt,string tdt) //created by ashok misal
    {
        sql = "EXEC Isp_AVS0042 '" + BRCD + "','" + PCODE + "','"+conn.ConvertDate(fdt)+"','"+conn.ConvertDate(tdt)+"'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable GETShareRegister(string BRCD,string FDATE, string TDATE)//Dipali Nagare 24-07-2017
    {
        sql = "EXEC  Isp_AVS0003 '" + BRCD + "','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "'";

        //sql = "EXEC  Sp_ShareRegister '" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "','" + BRCD + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }


    public DataTable GETShareRegisterRefund(string BRCD, string FDATE, string TDATE)//Dipali Nagare 24-07-2017
    {
        sql = "EXEC  Isp_AVS0015 '" + BRCD + "','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "'";

        //sql = "EXEC  Sp_ShareRegister '" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "','" + BRCD + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }

    public DataTable GETUserReport(string BRCD, string FDATE, string TDATE)//Dipali Nagare 24-07-2017
    {
        sql = "EXEC  ISP_AVS0007 '" + BRCD + "','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "'";

        //sql = "EXEC  Sp_ShareRegister '" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "','" + BRCD + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable GETSHAREREGISTR(string FBRCD, string TBRCD, string FDate, string TDate, string Edate)
    {
        sql = "EXEC Sp_ShareRegister @FBrcd='" + FBRCD + "',@TBrcd='" + TBRCD + "',@FDate='" + conn.ConvertDate(FDate) + "',@TDate='" + conn.ConvertDate(TDate) + "',@EDate='"+conn.ConvertDate(Edate)+"'";
        DT = conn.GetDatatable(sql);
        return DT;
    }

    public DataTable GetData( string PT, string AC, string BRCD)
    {
        sql = "EXEC Sp_ShareRegister '" + conn.ConvertDate(PT) + "','" + conn.ConvertDate(AC) + "','" + BRCD +  "'";
        DT = conn.GetDatatable(sql);
        return DT; 
    }
    public DataTable GETShareRegister(string FDATE, string TDATE, string PCODE, string BRCD)
    {
        sql = "EXEC Sp_ShareRegister '" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "','" + PCODE + "','" + BRCD + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable MonthlyAccStat(string FDATE, string TDATE, string PCODE, string ACCNO,string BRCD)
    {
        sql = "EXEC monthlystatment @Brcd='"+BRCD+"',@SubGlCode='"+PCODE+"',@Accno='"+ACCNO+"',@FromDate='" + conn.ConvertDate(FDATE) + "',@ToDate='" + conn.ConvertDate(TDATE) + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable GetDaily(string PCODE, string BRCD, string Date, string FL)//Dhanya Shetty--DailyCollection
    {
        sql = "EXEC SP_DAILYCOLLECTION '" + PCODE + "','" + BRCD + "','" + conn.ConvertDate(Date) + "','" + FL + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable GetDaily_A (string PCODE, string BRCD, string Date)//Dhanya Shetty--DailyCollection
    {
        sql = "EXEC SP_DAILYCOLLECTION_ALL '" + PCODE + "','" + BRCD + "','" + conn.ConvertDate(Date) + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    
    public string getloanname(string brcd, string subglcode, string accno)
    {
        sql = "Select M.CUSTNAME From Avs_Acc A Inner Join Master M On  A.Custno = M.Custno Where A.Subglcode='" + subglcode + "' And A.Accno='" + accno + "' And A.Brcd='" + brcd + "'";  //A.Brcd=M.Brcd And UNIFICATION 
        string result = conn.sExecuteScalar(sql);
        return result; 
    }
    //public int updateavsacc(string brcd,string dcode,string accno)
    //{
    //    sql = "update avs_acc set acc_status=4 where brcd='"+brcd+"' and subglcode='"+dcode+"' and accno='"+accno+"'";
    //    int Result = conn.sExecuteQuery(sql);
    //    return Result;
    //}
    public DataTable getinvestmentrpt(string FDATE, string TDATE,  string BRCD,string EDATE)
    {
        sql = "EXEC SP_INVESTMENTRPT @BRCD='"+BRCD+"',@FDATE='"+conn.ConvertDate(FDATE)+"',@TDATE='"+conn.ConvertDate(TDATE)+"',@EDATE='"+EDATE+"'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public string GETLOANDETAILS(string DCODE, string DACCNO, string BRCD)
    {
        sql = "SELECT COUNT(*) FROM Avs_Lienmarkdetails WHERE DEPOSITGLCODE='"+DCODE+"' AND BRCD='"+BRCD+"' AND DEPOSITACCNO='"+DACCNO+"' AND LOANGLCODE>=1 AND LOANACCNO>=1";
        string RESULT = conn.sExecuteScalar(sql);
        return RESULT;
    }
    public string getstatus(string DCODE, string DACCNO, string BRCD)
    {
        sql = "select acc_status from avs_acc where brcd='"+BRCD+"' and subglcode='"+DCODE+"' and accno='"+DACCNO+"'";
        string  RESULT = conn.sExecuteScalar(sql);
        return RESULT;
    }
    public int INSERTLIEN(string BRCD,string DCODE,string ACCNO)
    {
        sql = "Exec SP_INSERTAVSLIEN @BRCD='" + BRCD + "',@DEPOSITCODE='" + DCODE + "',@ACCNO='" + ACCNO + "'";
       int Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public DataTable GETMULTIAGENT(string FL,string FDATE, string TDATE, string PCODE1, string PCODE2,string BRCD)
    {

        string sql = "SELECT BANKCD FROM BANKNAME WHERE BRCD='"+BRCD+"'";
        string BKCD = conn.sExecuteScalar(sql);
        string sql1 = "SELECT glcode FROM GLMAST WHERE BRCD='" + BRCD+ "' and subglcode='" +PCODE1+ "'";
        string CheckBalOrDaily = conn.sExecuteScalar(sql1);
        if (Convert.ToInt32(CheckBalOrDaily) != 15)
        {
            if (BKCD == "1002" || BKCD == "1001")
            {


                sql = "Exec SP_AGENT_MULTI_COMMISION111 '"+FL+"','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "','" + BRCD + "','" + PCODE1 + "','" + PCODE2 + "'";
                DT = conn.GetDatatable(sql);

            }
            //else if (BKCD == "1001")
            //{
            //    sql = "Exec SP_AGENT_MULTI_COMMISION '" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "','" + BRCD + "','" + PCODE1 + "','" + PCODE2 + "'";
            //    DT = conn.GetDatatable(sql);
            //    return DT;

            //}
            else if (BKCD == "1003" || BKCD == "1004") 
            {
                sql = " Exec Isp_AVS0067 '" + BRCD + "','6','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "','" + PCODE1 + "','" + PCODE2 + "'";
                DT = conn.GetDatatable(sql);

            }
        }
        else if (Convert.ToInt32(CheckBalOrDaily) == 15)
        {
            sql = "EXEC SP_RDCOMMISSION '" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "','" + BRCD + "','" + PCODE1 + "','" + PCODE2 + "'";
            DT = conn.GetDatatable(sql);
        }
        return DT;
    }
    public DataTable GETMULTIAGENT1(string FDATE, string TDATE, string PCODE1, string PCODE2, string BRCD)
    {

        string sql = "SELECT BANKCD FROM BANKNAME WHERE BRCD='" + BRCD + "'";
        string BKCD = conn.sExecuteScalar(sql);
        if (BKCD == "1001")
        {
            sql = "Exec SP_AGENT_MULTI_CHECK '" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "','" + BRCD + "','" + PCODE1 + "','" + PCODE2 + "'";
            DT = conn.GetDatatable(sql);
            return DT;
        }
        return DT;
    }
    public DataTable checkAccLimit(string FL,string FDATE, string TDATE, string PCODE1, string PCODE2, string BRCD)
    {

        string sql = "SELECT BANKCD FROM BANKNAME WHERE BRCD='" + BRCD + "'";
        string BKCD = conn.sExecuteScalar(sql);
        if (BKCD == "1002" || BKCD=="1001" )
        {


            sql = "Exec SP_AGENTACCLIMITSHOW '"+FL+"','" + BRCD + "','" + PCODE1 + "','" + PCODE2 + "','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "' ";
            DT = conn.GetDatatable(sql);

        }
        //else if (BKCD == "1001")
        //{
        //    sql = "Exec SP_AGENT_MULTI_COMMISION '" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "','" + BRCD + "','" + PCODE1 + "','" + PCODE2 + "'";
        //    DT = conn.GetDatatable(sql);
        //    return DT;

        //}
        else if (BKCD == "1003")

        {
            sql = "Exec SP_AGENTACCLIMITSHOW '"+FL+"','" + BRCD + "','" + PCODE1 + "','" + PCODE2 + "','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "' ";
            DT = conn.GetDatatable(sql);
        }
        return DT;
    }
    public DataTable getDebitEntry(string FDATE, string PCODE, string BRCD)
    {


        sql = "Exec SP_DEBITEPORT '"+BRCD+"','"+PCODE+"','"+conn.ConvertDate(FDATE)+"','"+conn.ConvertDate(FDATE)+"'";
            DT = conn.GetDatatable(sql);
            return DT;
      
    }
    public string GetGlCode(string Brcd,string SubGlcode)  //added by ashok misal for Lien Report
    {
        string Result = "";
        try
        {
            sql = "SELECT GLCODE FROM GLMAST WHERE BRCD='" + Brcd + "' AND SUBGLCODE='" + SubGlcode + "'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
       
        return Result;
    }
    public DataTable AgentExcessAmt(string FL, string FDATE, string TDATE, string PCODE, string BRCD)
    {
        sql = "EXEC ISP_AVS0146 '" + FL + "','" + BRCD + "','" + PCODE + "','" + PCODE + "','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public string getbrname(string brcd)
    {
        string Result = "";
        sql = "select midname from BANKNAME where brcd='" + brcd + "'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    
}