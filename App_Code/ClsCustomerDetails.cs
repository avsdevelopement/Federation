using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsCustomerDetails
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "",res="";
    int Result = 0;

    public ClsCustomerDetails()
    {

    }

    public DataTable GetStage(string Custno)
    {
        try
        {
            sql = "SELECT STAGE,BRCD FROM MASTER WHERE CUSTNO='" + Custno + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetCustAccInfo(string BrCode, string CustNo)
    {
        try
        {
            sql = "with CTE As ("+
            "Select M.custno, M.custname, Upper(IsNull(isnull(Ad.FLAT_ROOMNO+', ','')+isnull(Ad.SOCIETY_NAME,'')+isnull(Ad.CITY+', ','') + isnull(L.DESCRIPTION ,'')+isnull(', Pincode - '+ Ad.PINCODE,''),'NA')) As Permanent, " +
                    "Upper(IsNull(isnull(Ad1.FLAT_ROOMNO+', ','')+isnull(Ad1.SOCIETY_NAME,'')+isnull(Ad1.CITY+', ','') + isnull(L1.DESCRIPTION,'') + isnull(', Pincode - '+Ad1.PINCODE,''),'NA')) As Present , " +
                    "Upper(IsNull(isnull(Ad2.FLAT_ROOMNO+', ','')+isnull(Ad2.SOCIETY_NAME,'')+isnull(Ad2.CITY+', ','')+ isnull(L2.DESCRIPTION,'NA') + isnull(', Pincode - '+Ad2.PINCODE,''),'NA')) As Office, " +
                    "Convert(Varchar(10), IsNull(format(M.DOB,'dd/MM/yyyy'),''), 121) As DOB, isnull(format(M.OPENINGDATE,'dd/MM/yyyy'),'') As OPENINGDATE, " +
                    "Convert(Varchar(10), IsNull(M.CLOSINGDATE, ''), 121) As CLOSINGDATE, IsNull(AC.OLDCTNO, '0') As OLDCTNO, " +
                    "IsNull(D.MOBILE1, '0') As MOBILE1,IsNull(I.DOC_NO, '0') As DOC_NO ,AC.AccNo From Master M " +
                    "Left Join addmast Ad With(Nolock) On  Ad.CustNo = M.CustNo and Ad.ADDTYPE=1 " +
                    "Left Join addmast Ad1 With(Nolock) On  Ad1.CustNo = M.CustNo and Ad1.ADDTYPE=2 " +
                    "Left Join addmast Ad2 With(Nolock) On  Ad2.CustNo = M.CustNo and Ad2.ADDTYPE=3 " +
                    "Left Join Avs_acc Ac With(Nolock) On  Ac.CustNo = M.CustNo " +
                    "left join AVS_CONTACTD D with(Nolock) On  D.Custno = M.CustNo " +
                    "left join IDENTITY_PROOF I with(Nolock) On I.CUSTNO = M.CustNo and I.DOC_TYPE=3 " +
                    "Left Join LOOKUPFORM1 L With(Nolock) On L.SrNo = Ad.STATE And L.LNO = '1007' " +
                    "Left Join LOOKUPFORM1 L1 With(Nolock) On L1.SrNo = Ad1.STATE And L1.LNO = '1007' " +
                    "Left Join LOOKUPFORM1 L2 With(Nolock) On L2.SrNo = Ad2.STATE And L2.LNO = '1007' " +
                    "Where M.CustNo = '" + CustNo + "'"+
                    ")	 select  top 1 * From CTE";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int GetLoanInfo(GridView grdLoan, string BRCD, string Custno, string EDate)
    {
        try
        {
            sql = "Exec SP_CustDashDetails '" + BRCD + "','" + Custno + "','" + conn.ConvertDate(EDate).ToString() + "','LN'";
            conn.sBindGrid(grdLoan, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToInt32(BRCD);
    }

    public int GetDepositeInfo(GridView grdDep, string BRCD, string Custno, string EDate)
    {
        try
        {
            sql = "Exec SP_CustDashDetails '" + BRCD + "','" + Custno + "','" + conn.ConvertDate(EDate).ToString() + "','DP'";
            conn.sBindGrid(grdDep, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToInt32(BRCD);
    }

    public int GetAccountInfo(GridView grdDep, string BRCD, string Custno, string EDate,string FLAG)
    {
        try
        {
            sql = "Exec SP_CustDashDetails '" + BRCD + "','" + Custno + "','" + conn.ConvertDate(EDate).ToString() + "','"+FLAG+"'";
            conn.sBindGrid(grdDep, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToInt32(BRCD);
    }

    public int GetFromSurity(GridView grd, string BRCD, string CustNo, string EDate)
    {
        try
        {
            sql = "Exec SP_SURITYINFO @BRCD = '" + BRCD + "', @CUSTNO = '" + CustNo + "', @EDATE = '" + conn.ConvertDate(EDate) + "'";
            conn.sBindGrid(grd, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToInt32(BRCD);
    }

    public DataTable GetAllSurity(string BRCD, string Custno, string edate)
    {
        try
        {
            sql = "Exec SP_SURITYINFO '" + BRCD + "','" + Custno + "','" + conn.ConvertDate(edate) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetDashInfo(string BRCD, string Custno, string EDate)
    {
        try
        {
            sql = "Exec SP_CustDashDetails '" + BRCD + "','" + Custno + "','" + conn.ConvertDate(EDate).ToString() + "','DASH'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return DT = null;
        }
        return DT;
    }

    public DataTable GetCustInfo(string BRCD, string CustNo, string Date)//Dhanya Shetty
    {
        sql = "EXEC SP_CUSTDISPLAY  '" + BRCD + "','" + CustNo + "','" + conn.ConvertDate(Date) + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable GetCustName(string CNO)
    {
        sql = "select  ISnull(CUSTNAME,'') CUSTNAME,brcd,CUSTNO from MASTER where CUSTNO='" + CNO + "' AND STAGE <>'1004' ";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable GetMemInfo(string CustNo)
    {
        try
        {
            sql =   "With CTE AS("+
                    "Select M.custno, M.custname, Upper(IsNull(Ad.FLAT_ROOMNO+', '+Ad.SOCIETY_NAME+Ad.CITY +', '+ L.DESCRIPTION +', Pincode - '+ Ad.PINCODE,'NA')) As Permanent, " +
                    "Upper(IsNull(isnull(Ad1.FLAT_ROOMNO+', ','')+isnull(Ad1.SOCIETY_NAME,'')+isnull(Ad1.CITY+', ','') + isnull(L1.DESCRIPTION,'') + isnull(', Pincode - '+Ad1.PINCODE,''),'NA')) As Present, " +
                    "Upper(IsNull(Ad2.FLAT_ROOMNO+', '+Ad2.SOCIETY_NAME+Ad2.CITY +', '+  L2.DESCRIPTION +', Pincode - '+ Ad2.PINCODE,'NA')) As Office, " +
                    "Convert(Varchar(10), IsNull(M.DOB,''), 103) As DOB, Convert(Varchar(10), M.OPENINGDATE, 103) As OPENINGDATE, " +
                    "Convert(Varchar(10), IsNull(M.CLOSINGDATE, ''), 103) As CLOSINGDATE, IsNull(AC.OLDCTNO, '0') As OLDCTNO, " +
                    "IsNull(D.MOBILE1, '0') As MOBILE1,IsNull(I.DOC_NO, '0') As DOC_NO From Avs_acc Ac " +
                    "Left Join  Master M With(Nolock) On Ac.CustNo = M.CustNo " +
                    "Left Join addmast Ad With(Nolock) On Ad.Brcd = M.BrCd And Ad.CustNo = M.CustNo and Ad.ADDTYPE=1 " +
                    "Left Join addmast Ad1 With(Nolock) On Ad1.Brcd = M.BrCd And Ad1.CustNo = M.CustNo and Ad1.ADDTYPE=2 " +
                    "Left Join addmast Ad2 With(Nolock) On Ad2.Brcd = M.BrCd And Ad2.CustNo = M.CustNo and Ad2.ADDTYPE=3 " +
                    "left join AVS_CONTACTD D with(Nolock) On D.brcd=M.BRCD and D.Custno = M.CustNo " +
                    "left join IDENTITY_PROOF I with(Nolock) On I.BRCD=M.BRCD and I.CUSTNO = M.CustNo and I.DOC_TYPE=3 " +
                    "Left Join LOOKUPFORM1 L With(Nolock) On L.SrNo = Ad.STATE And L.LNO = '1007' " +
                    "Left Join LOOKUPFORM1 L1 With(Nolock) On L1.SrNo = Ad1.STATE And L1.LNO = '1007' " +
                    "Left Join LOOKUPFORM1 L2 With(Nolock) On L2.SrNo = Ad2.STATE And L2.LNO = '1007' " +
                    "Where Ac.BrCd = '1' and Ac.AccNo = '" + CustNo + "' and Ac.Glcode='4'"+
                    ")	select * from CTE order by Permanent ";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetSchlName(string custno)
    {
        string schl = "";
        try
        {
            sql = "select e.NAMEOFOFFC from EMPDETAIL e inner join MASTER m on e.CUSTNO=m.CUSTNO and e.OFFNO=m.RECDEPT and e.DIVNO=m.BRANCHNAME where e.STAGE<>1004 and e.CUSTNO='" + custno + "'";
            schl = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return schl;
    }
    public string getCustnoMem(string accno)
    {
        try
        {
            sql = "select custno from avs_acc where brcd='1' and glcode='4' and accno='" + accno + "'";
            res = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public string getNewCust(string CustNo, string Brcd)
    {
        try
        {
            sql = "select CustNo from master where old_cust_no='"+CustNo+"' and BRCD='"+Brcd+"'";
            res = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public string GETLABEL(string SRNO)
    {
        try
        {
            sql = "SELECT LABLENAME FROM cust_dash WHERE SRNO='" + SRNO + "'";
            res = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public DataTable GetDivnameCfno(string custno,string Brcd)
    {
        try
        {
            //sql = "select EMPNO,NAMEOFDIV,Convert(varchar(11),DOR,103)DOR,RTGAGE from empdetail where custno='" + custno + "' and brcd='" + Brcd + "'";
            sql = "select E.EMPNO,P.DESCR As NAMEOFDIV,Convert(varchar(11),E.DOR,103)DOR,E.RTGAGE " +
                  " From EmpDetail E " +
                  " Left Join Master M On E.CustNO = M.CustNo " +
                  " Left Join PayMast P On M.BRANCHNAME= P.RECDIV and P.RECCODE=0 " +
                  " Where M.Custno='" + custno + "' ";
            DT = conn.GetDatatable(sql);
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int GetNominee(GridView grd, string Custno)
    {
        try
        {
            sql = "select NOMINEENAME,BRCD,GLCODE,SUBGLCODE,ACCNO,Convert (varchar(11),DOB,103)DOB,Address1,Address2,Address3,City,PinCode,MobNo,PanNo from nomineedetails "+
                    " where custno='" + Custno + "' and stage<>1004 order by Brcd,subglcode,accno";
            conn.sBindGrid(grd, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToInt32(Custno);
    }
}