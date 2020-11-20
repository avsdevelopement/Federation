using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsLoanRecovery
/// </summary>
public class ClsLoanRecovery
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "", result = "";
    public ClsLoanRecovery()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable getattachment(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec RptLoanStatement '" + BRCD + "','" + PCODE + "','" + ACCNO + "','" + Conn.ConvertDate(Edate) + "','" + Conn.ConvertDate(Edate) + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public string getclosing(string brcd, string subglcode, string accno, string EDATE)
    {
        sql = "Exec SP_OpClBalance @BrCode = '" + brcd + "', @SubGlCode = '" + subglcode + "', @AccNo = '" + accno + "', @EDate = '" + Conn.ConvertDate(EDATE) + "', @Flag = 'ClBal'";
        string result = Conn.sExecuteScalar(sql);
        return result;
    }
    public string GetAddressSelected(string Flag, string BRCD, string Subglcode, string accno)
    {
        sql = "exec ISP_AVS0100 @MAINFLAG='LOAN', @FLAG='" + Flag + "',@BRCD='" + BRCD + "',@SUBGLCODE='" + Subglcode + "',@ACCNO='" + accno + "'";
        string Result = Conn.sExecuteScalar(sql);
        return Result;
    }
    public string GetAddressSelectedGar(string Flag, string BRCD, string Subglcode, string accno,string CustNo)
    {
        sql = "exec ISP_AVS0100 @MAINFLAG='GAR', @FLAG='" + Flag + "',@BRCD='" + BRCD + "',@SUBGLCODE='" + Subglcode + "',@ACCNO='" + accno + "',@CUSTNO='"+CustNo+"'";
        string Result = Conn.sExecuteScalar(sql);
        return Result;
    }


    public DataTable Goldloaninstall(string BRCD, string PCODE, string ACCNO)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec sp_Goldloaninstall '" + BRCD + "','" + PCODE + "','" + ACCNO + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public DataTable RptGoldAuction(string BRCD, string PCODE, string ACCNO)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec SP_GOLDAUCTIONNOTICE '" + BRCD + "','" + PCODE + "','" + ACCNO + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public DataTable RptVisitnotice(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = " Exec SP_RptNotice '" + BRCD + "','" + PCODE + "','" + ACCNO + "','" + Conn.ConvertDate(Edate) + "','" + Conn.ConvertDate(Edate) + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public DataTable LoanNoticeRpt(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = " Exec SP_RptNotice '" + BRCD + "','" + PCODE + "','" + ACCNO + "','" + Conn.ConvertDate(Edate) + "','" + Conn.ConvertDate(Edate) + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public DataTable GetLoanApp(string BRCD, string AC, string PT)//Dhanya Shetty
    {
        try
        {
            sql = "EXEC SP_LOANAPPLICATION @BRCD='" + BRCD + "',@SUBGLCODE ='" + PT + "',@ACCNO='" + AC + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
        return Dt;
    }
    public DataTable RptLastNotice(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec SP_RptNotice '" + BRCD + "','" + PCODE + "','" + ACCNO + "','" + Conn.ConvertDate(Edate) + "','" + Conn.ConvertDate(Edate) + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public DataTable RptLastNoticeTZMP(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec Sp_RptNoticeTZMP '" + BRCD + "','" + PCODE + "','" + ACCNO + "','" + Conn.ConvertDate(Edate) + "','" + Conn.ConvertDate(Edate) + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public DataTable RptLoanIntcert(string FLAG,string BRCD, string PCODE, string ACCNO, string Edate, string Fdate, string Tdate,string Edt,string FL)
    {
        DataTable Dt = new DataTable();
        try
        {
            if (FL == "1")
            {
                sql = "Exec Isp_AVS0068 @Flag='NONACCR',@Brcd='" + BRCD + "',@SUBGLCODE='" + PCODE + "',@ACCNO='" + ACCNO + "',@FromDate='" + Conn.ConvertDate(Fdate) + "',@ToDate='" + Conn.ConvertDate(Tdate) + "',@Edt='" + Conn.ConvertDate(Edt) + "'";
                //sql = "Exec Isp_AVS0068 '','" + BRCD + "','" + PCODE + "','" + ACCNO + "','" + Conn.ConvertDate(Fdate) + "','" + Conn.ConvertDate(Tdate) + "'";
            }
            else
            {
                sql = "Exec Isp_AVS0068 @Flag='ACCR',@Brcd='" + BRCD + "',@SUBGLCODE='" + PCODE + "',@ACCNO='" + ACCNO + "',@FromDate='" + Conn.ConvertDate(Fdate) + "',@ToDate='" + Conn.ConvertDate(Tdate) + "',@Edt='" + Conn.ConvertDate(Edt) + "'";
                //sql = "Exec Isp_AVS0068 '','" + BRCD + "','" + PCODE + "','" + ACCNO + "','" + Conn.ConvertDate(Fdate) + "','" + Conn.ConvertDate(Tdate) + "'";
            }

            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    //Added by Abhishek as per ambika mam req on 28-12-2017
    public string LSCheck(string FLAG, string BRCD, string PCODE, string ACCNO, string Edate, string Fdate, string Tdate,string Edt)
    {
        try
        {
            sql = "Exec Isp_AVS0068 @Flag='" + FLAG + "',@Brcd='" + BRCD + "',@SUBGLCODE='" + PCODE + "',@ACCNO='" + ACCNO + "',@FromDate='" + Conn.ConvertDate(Fdate) + "',@ToDate='" + Conn.ConvertDate(Tdate) + "',@Edt='" + Conn.ConvertDate(Edt) + "'";
           // sql = "Exec Isp_AVS0068 '" + Flag + "','" + BRCD + "','" + PCODE + "','" + ACCNO + "','" + Conn.ConvertDate(Fdate) + "','" + Conn.ConvertDate(Tdate) + "'";
            sql = Conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }


    public DataTable ShareRegister(string CERT_ISSUE1STDATE, string CUSTNO, string CUSTNAME, string CERT_NO, string SHAREFROM, string SHARETO, string SHARESVALUE, string TOTALSHAREAMT)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec Sp_ShareRegister '" + Conn.ConvertDate(CERT_ISSUE1STDATE) + "','" + CUSTNO + "','" + CUSTNAME + "','" + CERT_NO + "','" + SHAREFROM + "','" + SHARETO + "','" + SHARESVALUE + "','" + TOTALSHAREAMT + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }



    public string getdate(string date)
    {
        sql = "select convert(nvarchar(10),dateadd(dd,7,'" + Conn.ConvertDate(date) + "'),103)";

        string result = Conn.sExecuteScalar(sql);
        return result;


    }
    public string getdateDUE(string date)
    {
        sql = " select convert(nvarchar(10),dateadd(dd,15,'" + Conn.ConvertDate(date) + "'),103)";
        string result = Conn.sExecuteScalar(sql);
        return result;

    }
    public string HregNo(string BRCD, string Productcode, string Accno)//Dhanya Shetty//05-07-2017 To get HRegNo
    {
        try
        {
            sql = "select C_F_N_101 from AVS_2001 where brcd='" + BRCD + "' and PRDCD='" + Productcode + "' and accno='" + Accno + "'";
            result = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public string CompanyName(string BRCD, string Productcode, string Accno)//Dhanya Shetty//05-07-2017 To get company name
    {
        try
        {

            sql = "select BussinessName from AVSLnSurityTable where brcd='" + BRCD + "' and subglcode='" + Productcode + "' and accno='" + Accno + "' and lntype='Loanee'";
            result = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public string CompanyNameForSurity(string BRCD, string Productcode, string Accno, string custno)//Dhanya Shetty//05-07-2017 To get company name
    {
        try
        {

            sql = "select BussinessName from AVSLnSurityTable where brcd='" + BRCD + "' and subglcode='" + Productcode + "' and accno='" + Accno + "' and lntype='Surity' and custno='" + custno + "'";
            result = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public string Orderdate(string BRCD, string Productcode, string Accno)//Dhanya Shetty//05-07-2017 To get Orderdate
    {
        try
        {

            sql = "select convert(nvarchar(10),R_O_Dt,103)  from AVS_2001 where brcd='" + BRCD + "' and PRDCD='" + Productcode + "' and accno='" + Accno + "'";
            result = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public string SalDate(string BRCD, string Productcode, string Accno)//Dhanya Shetty//05-07-2017 To get Salary  Orderdate
    {
        try
        {

            sql = "select convert(nvarchar(10),S_O_Dt,103)  from AVS_2001 where brcd='" + BRCD + "' and PRDCD='" + Productcode + "' and accno='" + Accno + "'";
            result = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public string DemandDate(string BRCD, string Productcode, string Accno)//ashok misal//05-07-2017 To get Salary  Orderdate
    {
        try
        {

            sql = "select convert(nvarchar(10),NOTICE_ISS_DT,103)  from AVS_2001  where brcd='" + BRCD + "' and PRDCD='" + Productcode + "' and accno='" + Accno + "'";
            result = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }


    public string GetAdd(string BRCD, string Productcode, string Accno)//Dhanya Shetty//05-07-2017 To get Address
    {
        try
        {

            sql = "select M.FLAT_ROOMNO from avs_acc  a inner join ADDMAST M on a.brcd=m.BRCD and a.CUSTNO=m.CUSTNO where a.brcd='" + BRCD + "' and a.subglcode='" + Productcode + "' and a.accno='" + Accno + "' and m.ADDTYPE=2";
            result = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public string getcoborowername(string brcd, string subglcode, string accno)
    {
        sql = "select LnsrName from avslnsuritytable where brcd='" + brcd + "' and lntype='Loanee' and subglcode='" + subglcode + "' and accno='" + accno + "'";
        result = Conn.sExecuteScalar(sql);
        return result;

    }
    public string TalukaForLoanee(string brcd, string subglcode, string accno)
    {
        sql = "select CONCAT(l.description,' ', l1.description) from avs_acc a inner join addmast ad on a.brcd=ad.brcd and a.custno=ad.custno inner join lookupform1 L on L.LNO=1013 AND L.SRNO=AD.area_taluka inner join lookupform1 L1 on l1.lno=1005 AND L1.SRNO=AD.DISTRICT where a.brcd='" + brcd + "' and a.subglcode='" + subglcode + "' and a.accno='" + accno + "' and  ad.addtype=2 ";
        result = Conn.sExecuteScalar(sql);
        return result;

    }
    public string DistrictForLoanee(string brcd, string subglcode, string accno)
    {
        sql = "select ad.DISTRICT from avs_acc a Inner join addmast ad on a.custno=ad.custno and a.brcd=ad.brcd LEFT JOIN LOOKUPFORM1 L ON Convert(Varchar(16),L.SRNO)=ad.DISTRICT AND L.LNO='1005' Left JOIN LOOKUPFORM1 Ll ON Convert(Varchar(16),Ll.SRNO)=ad.AREA_TALUKA AND LL.LNO='1013'where a.subglcode='" + subglcode + "' and a.accno='" + accno + "' and a.brcd='" + brcd + "' and addtype=2";
        result = Conn.sExecuteScalar(sql);
        return result;

    }
    public string TalukaForSurity(string brcd, string Custno)
    {
        sql = "select AD.AREA_TALUKA from addmast ad Left JOIN LOOKUPFORM1 L ON Convert(Varchar(16),L.SRNO)=ad.DISTRICT AND L.LNO='1005'  Left JOIN LOOKUPFORM1 Ll ON Convert(Varchar(16),Ll.SRNO)=ad.AREA_TALUKA AND LL.LNO='1013' where ad.brcd='" + brcd + "' and ad.custno='" + Custno + "' and addtype=2";
        result = Conn.sExecuteScalar(sql);
        return result;

    }
    public string DistrictForSurity(string brcd, string Custno)
    {
        sql = "select  ad.DISTRICT from addmast ad Left JOIN LOOKUPFORM1 L ON Convert(Varchar(16),L.SRNO)=ad.DISTRICT AND L.LNO='1005'  Left JOIN LOOKUPFORM1 Ll ON Convert(Varchar(16),Ll.SRNO)=ad.AREA_TALUKA AND LL.LNO='1013' where ad.brcd='" + brcd + "' and ad.custno='" + Custno + "' and addtype=2";
        result = Conn.sExecuteScalar(sql);
        return result;

    }
    public string GetSurityAdd(string brcd, string custno)
    {
        sql = "select FLAT_ROOMNO  from addmast where brcd='" + brcd + "' and custno='" + custno + "' and addtype=1";
        result = Conn.sExecuteScalar(sql);
        return result;

    }
    public string getComapnyAdd1(string brcd, string subglcode, string accno)
    {
        sql = "select Address1 from avslnsuritytable where brcd='" + brcd + "' and lntype='Loanee' and subglcode='" + subglcode + "' and accno='" + accno + "'";
        result = Conn.sExecuteScalar(sql);
        return result;

    }
    public string getComapnyAdd2(string brcd, string subglcode, string accno)
    {
        sql = "select aa.FLAT_ROOMNO from avs_acc a inner join addmast aa on a.brcd=aa.brcd and a.custno=aa.custno where a.brcd='" + brcd + "' and a.subglcode='" + subglcode + "' and a.accno='" + accno + "' and aa.addtype=3";
        result = Conn.sExecuteScalar(sql);
        return result;

    }
    public string GetComapnyAdd(string brcd, string custno)
    {
        sql = "select FLAT_ROOMNO from addmast where brcd='" + brcd + "' and custno='" + custno + "' and addtype=3";
        result = Conn.sExecuteScalar(sql);
        return result;

    }
    public string GetVillageAdd(string brcd, string subglcode, string accno)
    {
        sql = "select ad.FLAT_ROOMNO from avs_acc a inner join addmast ad on a.brcd=ad.brcd and a.custno=ad.custno where a.brcd='" + brcd + "' and a.subglcode='" + subglcode + "' and a.accno='" + accno + "' and  ad.addtype=2";
        result = Conn.sExecuteScalar(sql);
        return result;

    }
    public string GetVillagadd2(string brcd, string custno)
    {
        sql = "select FLAT_ROOMNO from addmast where brcd='" + brcd + "' and custno='" + custno + "' and addtype=2";
        result = Conn.sExecuteScalar(sql);
        return result;

    }
    public string GetSROName(string BRCD, string Productcode, string Accno)//ankita on 13/07/2017 to get sro name
    {
        try
        {

            sql = "select b.SRONAME from AVS_2001 a inner join AVS_2000 b on a.SRO_NO=b.SRNO where a.brcd='" + BRCD + "' and a.PRDCD='" + Productcode + "' and a.accno='" + Accno + "'";
            result = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public DataTable RptLoanIntcert11(string BRCD, string PCODE, string ACCNO, string Edate, string Fdate, string Tdate)//Dhanya Shetty /08/12/2017//Loan int certificate1
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec Isp_AVS0068 @Flag='',@Brcd='" + BRCD + "',@SUBGLCODE='" + PCODE + "',@ACCNO='" + ACCNO + "',@FromDate='" + Conn.ConvertDate(Fdate) + "',@ToDate='" + Conn.ConvertDate(Tdate) + "',@Edt='" + Conn.ConvertDate(Edate) + "'";
           Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public DataTable RptLoanClosecert(string BRCD, string PCODE, string ACCNO)//Dhanya Shetty /11/12/2017//Loan closure certificate
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec ISP_AVS0099 '" + BRCD + "','" + PCODE + "','" + ACCNO + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public DataTable CheckPrInt(string BRCD, string PCODE, string ACCNO, string Edate)//Dhanya Shetty /11/12/2017//Loan closure certificate
    {
        try
        {
            sql = "Exec CheckPrincIntBal @BrCode='" + BRCD + "',@SGlCode='" + PCODE + "',@AccNo='" + ACCNO + "',@EDate='" + Conn.ConvertDate(Edate).ToString() + "', @sFlag='LoanClose'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public string GetRegno(string Brcd)//Dhanya Shetty To get Regno for Loan closure Certificate //04/01/2017
    {
        try
        {
            sql = "select regno   from Bankname  where brcd='"+Brcd+"'";
            result = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
}