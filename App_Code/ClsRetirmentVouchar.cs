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
/// Summary description for ClsRetirmentVouchar
/// </summary>
public class ClsRetirmentVouchar
{
    DataTable Dt = new DataTable();
    DbConnection conn = new DbConnection();
    string sql = "", result = "";
    int Result=0;
	public ClsRetirmentVouchar()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GetBranchName(string brcd)
    {
        sql = "select midname from bankname where brcd='"+brcd+"'";
        string result = conn.sExecuteScalar(sql);
        return result;
    }
    public DataTable GetCustNAme(string Custno, string brcd)
    {
        DataTable dt = new DataTable();
        sql = "select custname,convert(varchar(10),OPENINGDATE,103) as OPENINGDATE from master where  custno='" + Custno + "'";
        dt = conn.GetDatatable(sql);
        return dt;
    }
    public DataTable GetWelfareInfo(string YEAR)
    {
        DataTable dt = new DataTable();
        sql = "select glcode,placc,CHARGES from chargesmaster where CHARGESTYPE='"+YEAR+"'";
        dt = conn.GetDatatable(sql);
        return dt;
    }
    public DataTable GetWelfareInfo1()
    {
        DataTable dt = new DataTable();
        sql = "select glcode,placc,CHARGES from chargesmaster where CHARGESTYPE='1111'";
        dt = conn.GetDatatable(sql);
        return dt;
    }
    public DataTable GetPayblr(string brcd, string custno)
    {
        sql = "select a.subglcode,a.accno,abs(b.amount)bal from avs_acc a  inner join avsb_201708 b on a.brcd=b.brcd and a.subglcode=b.subglcode and a.accno=b.accno where a.brcd='"+brcd+"' and a.custno='"+custno+"' and a.subglcode!=0 and acc_status=1 and b.trxtype=3 and a.glcode in(3)";
        DataTable DT = new DataTable();
        DT = conn.GetDatatable(sql);
        return DT;

    }
    public int bindgrid(string custno,string brcd,string EntryDate, GridView Gview)
    {
        sql = "Exec RetirmentSp '"+brcd+"','"+custno+"','"+conn.ConvertDate(EntryDate)+"','SB'";
        int Result = conn.sBindGrid(Gview, sql);
        return Result;
    }
    public DataTable GetOldData(string custno, string brcd, string EntryDate)
    {
        DataTable dt = new DataTable();
        try 
	{	        
	   
        sql = "EXEC Sp_RetVouchar '" + brcd + "','" + custno + "','" + conn.ConvertDate(EntryDate) + "','1'";
        dt = conn.GetDatatable(sql);
      
	}
	catch (Exception Ex)
	{
		
		ExceptionLogging.SendErrorToText(Ex);
	}
        return dt;
    }
    public DataTable bindgrid1(string custno, string brcd, string EntryDate)
    {
        DataTable dt = new DataTable();
        sql = "Exec RetirmentSp '"+brcd+"','"+custno+"','"+conn.ConvertDate(EntryDate)+"','DL' ";
        dt = conn.GetDatatable(sql);
        return dt;
    }
    public string Getaccno(string BRCD, string AT)
    {
        try
        {
            sql = " SELECT GLNAME FROM GLMAST WHERE BRCD='"+BRCD+"' AND SUBGLCODE='"+AT+"' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }
    public string GetAccStatus(string BRCD, string SBGL, string AC)
    {
        try
        {
            sql = "SELECT ACC_STATUS FROM AVS_ACC WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SBGL + "' AND ACCNO='" + AC + "'";
            AC = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return AC;
    }
    public string Getstage1(string CNO, string BRCD, string PRD)//GLCDOE PARAMETR REMOVED
    {
        string RS = "";
        //sql = "SELECT STAGE FROM AVS_ACC WHERE ACCNO='" + CNO + "'AND GLCODE='"+GL+"' AND SUBGLCODE='"+PRD+"' AND BRCD='" + BRCD + "' AND STAGE <>1004 ";
        sql = "SELECT CONVERT(VARCHAR(5),STAGE) FROM AVS_ACC WHERE ACCNO='" + CNO + "' AND SUBGLCODE='" + PRD + "' AND BRCD='" + BRCD + "' AND STAGE <>1004 ";//AND GLCODE='"+GL+"'
        RS = conn.sExecuteScalar(sql);
        return RS;
    }
      public string GetYear(string CNO, string BRCD, string Edate)
    {
        string RS = "";

        sql = "Select dateDiff(yy,(select openingdate from master where  custno='"+CNO+"'),'"+conn.ConvertDate(Edate)+"') as year";
        RS = conn.sExecuteScalar(sql);
        return RS;
    }
      public string getAccNO(string BRCD, string Subglcode, string Custno)
      {
          string Acc = "0";
          try
          {
              sql = "Select Accno from avs_acc where brcd='" + BRCD + "' and subglcode='" + Subglcode + "' and custno=" + Custno + " and acc_status<>3 and stage<>1004";
              Acc = conn.sExecuteScalar(sql);
          }
          catch (Exception ex)
          {
              ExceptionLogging.SendErrorToText(ex);
          }
          return Acc;
      }

    public string GetGlcode(string brcd,string subglcode,string AccNo)//GLCDOE PARAMETR REMOVED
    {
        string RS = "";

        sql = "SELECT glcode FROM AVS_ACC WHERE BRCD='" + brcd + "' AND SUBGLCODE='" + subglcode + "' AND ACCNO='" + AccNo + "'";
        RS = conn.sExecuteScalar(sql);
        return RS;
    }
    public string GetGlcodePL(string brcd, string subglcode)//GLCDOE PARAMETR REMOVED
    {
        string RS = "";

        sql = "SELECT glcode FROM GLMAST WHERE BRCD='" + brcd + "' AND SUBGLCODE='" + subglcode + "'";
        RS = conn.sExecuteScalar(sql);
        return RS;
    }

    public string getNetPaid()
    {
        string Subglcode = "";
        try
        {
            sql = "select Listvalue from parameter where listfield='NETPAID'";
            Subglcode = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Subglcode;
    }
    public string GetGlAccNo(string BRCD, string GlCode)
    {
        string ACCYN = "";
        try
        {
            sql = "select case when INTACCYN='' then 'N' else isnull(INTACCYN,'N') end INTACCYN from GLMAST where subglcode='" + GlCode + "' and brcd='" + BRCD + "'";
            ACCYN = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ACCYN;
    }
    public string GetGlcodeIR(string brcd, string subglcode)//GLCDOE PARAMETR REMOVED
    {
        string RS = "";

        sql = "SELECT glcode FROM GLMAST WHERE BRCD='"+brcd+"' AND SUBGLCODE='"+subglcode+"'";
        RS = conn.sExecuteScalar(sql);
        return RS;
    }
    public string GetDepositIR(string brcd, string subglcode)
    {
        string RS = "";

        sql = "SELECT IR FROM GLMAST WHERE BRCD='"+brcd+"' AND SUBGLCODE='"+subglcode+"'";
        RS = conn.sExecuteScalar(sql);
        return RS;
    }
    public string GetLoanPlaccno(string brcd, string subglcode)
    {
        string RS = "";

        sql = "SELECT PLACCNO FROM GLMAST WHERE BRCD='" + brcd + "' AND SUBGLCODE='" + subglcode + "'";
        RS = conn.sExecuteScalar(sql);
        return RS;
    }
    public string getglname(string brcd, string subglcode)
    {
        string RS = "";

        sql = "select glname from glmast where brcd='"+brcd+"' and subglcode='"+subglcode+"'";
        RS = conn.sExecuteScalar(sql);
        return RS;
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
    

    public int RetirmentVouchar(string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS,
       string PARTICULARS2, string AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE,
       string INSTBANKCD, string INSTBRCD, string STAGE, string RTIME, string BRCD, string MID, string CID, string VID, string PAYMAST, string CUSTNO,
       string CUSTNAME, string REFID, string Token)
    {
        try
        {

            string CR, DR;
            string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');
            string TBNAME = "";
            TBNAME = TD[2].ToString().Trim() + TD[1].ToString().Trim();
            CR = DR = "";
            if (TRXTYPE == "1")
            {
                CR = AMOUNT;
                DR = "0";
            }
            else if (TRXTYPE == "2")
            {
                DR = AMOUNT;
                CR = "0";
            }
            if (ACCNO == "")
                ACCNO = "0";
            sql = "SELECT ISNULL(MAX(SCROLLNO),0)+1 FROM ALLVCR WHERE BRCD='" + BRCD + "' AND SETNO='" + SETNO + "' AND ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "'";
            string SCROLLNO = conn.sExecuteScalar(sql);
            REFID = REFID.ToString() == "" ? "0" : REFID;
            sql = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                "VALUES('" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "','" + PARTICULARS + "','" + CR + "','" + DR + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','" + CID + "','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "',GETDATE())";
            Result = conn.sExecuteQuery(sql);

            sql = "INSERT INTO AVSM_" + TBNAME.Replace("AM", "").Trim() + "(AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE,TokenNo) " +
               " VALUES ('1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','" + CID + "','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "',GETDATE(),'" + Token + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
        public string GetSetNo(string Date, string PName, string BRCD)
        {
            //To Avoid Duplication of SETNO --Abhsihek

            if (PName == "IBTSetNo")
                sql = "update avs5002 set LastNo = " +
                " (select Max(lastno)+1  from avs5002 where  EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "') " +
                " where EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "'" +
                "select Lastno From avs5002  where EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "'";
            else if (PName == "DaySetNo")
                sql = "update avs5002 set LastNo = " +
                " (select Max(lastno)+1  from avs5002 where  EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "' and BRCD='" + BRCD + "') " +
                " where EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "' and BRCD='" + BRCD + "' " +
                "select Lastno From avs5002  where EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "' and BRCD='" + BRCD + "' ";

            else if (PName == "InOutSetno")
            {
                sql = "UPDATE AVS5002 SET LASTNO=(SELECT MAX(LASTNO)+1 FROM AVS5002 WHERE ENTRYDATE='" + conn.ConvertDate(Date) + "' AND PARAMETERNAME='" + PName + "')" +
                      " WHERE ENTRYDATE='" + conn.ConvertDate(Date) + "' AND PARAMETERNAME='" + PName + "' AND BRCD='" + BRCD + "' " +
                      " SELECT LASTNO FROM AVS5002 WHERE ENTRYDATE='" + conn.ConvertDate(Date) + "' AND PARAMETERNAME='" + PName + "' AND BRCD='" + BRCD + "'";
            }
            PName = conn.sExecuteScalar(sql);
            return PName;
        }
    public int GetAccountInfo(GridView grdDep, string BRCD, string Custno, string EDate, string FLAG)
    {
        try
        {
            sql = "Exec SP_CustDashDetails '" + BRCD + "','" + Custno + "','" + conn.ConvertDate(EDate).ToString() + "','" + FLAG + "'";
            conn.sBindGrid(grdDep, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToInt32(BRCD);
    } 
    public string Getaccno(string AT, string BRCD, string GLCD)
    {
        try
        {
            sql = " SELECT (CONVERT(VARCHAR(10),MAX(LASTNO)+1))+'-'+(CONVERT (VARCHAR(10),GLCODE))+'-'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }
   

    //public int CloseAcc(string AT, string BRCD, string AccNo, string Entrydate)  // Rakesh 29052019
    //{
    //    int Result = 0;
    //    try
    //    {
    //        string[] Table = Entrydate.ToString().Split('/');
    //        sql = "select sum(amount) from( " +
    //              "select sum(case when trxtype=2 then -1*amount else amount end) as amount from avsb_" + Table[2].ToString() + Table[1].ToString() + " where stage=1003 and  brcd=" + BRCD + " and subglcode=" + AT + " and accno=" + AccNo + " and Trxtype=3 " +
    //              "union all " +
    //              "select sum(case when trxtype=2 then -1*amount else amount end)as amount from avsm_" + Table[2].ToString() + Table[1].ToString() + " where  stage<>1004 and brcd=" + BRCD + " and subglcode=" + AT + " and accno=" + AccNo + " and entrydate<='" + conn.ConvertDate(Entrydate) + "') as a";
    //        string Bal = conn.sExecuteScalar(sql);
    //        if (Bal == "0" || Bal == "0.00")
    //        {
    //            sql = "update avs_acc  set CLOSINGDATE='" + conn.ConvertDate(Entrydate) + "',ACC_STATUS=3 where brcd='" + BRCD + "' and subglcode='" + AT + "' and  accno='" + AccNo + "'";
    //            Result = conn.sExecuteQuery(sql);
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return Result;
    //}

    public int CloseAcc(string AT, string BRCD, string AccNo, string Entrydate)  // Rakesh 29052019
    {
        int Result = 0;
        try
        {
            string[] Table = Entrydate.ToString().Split('/');
            sql = "select sum(amount) from( " +
                  "select sum(case when trxtype=2 then -1*amount else amount end) as amount from avsb_" + Table[2].ToString() + Table[1].ToString() + " where stage=1003 and brcd=" + BRCD + " and subglcode=" + AT + " and accno=" + AccNo + " and entrydate<'" + conn.ConvertDate(Entrydate) + "' " +
                  "union all " +
                  "select sum(case when trxtype=2 then -1*amount else amount end)as amount from avsm_" + Table[2].ToString() + Table[1].ToString() + " where stage<>1004 and brcd=" + BRCD + " and subglcode=" + AT + " and accno=" + AccNo + " and entrydate='" + conn.ConvertDate(Entrydate) + "') as a";
            string Bal = conn.sExecuteScalar(sql);
            if (Bal == "0" || Bal == "0.00")
            {
                sql = "update avs_acc  set CLOSINGDATE='" + conn.ConvertDate(Entrydate) + "',ACC_STATUS=3 where brcd='" + BRCD + "' and subglcode='" + AT + "' and accno='" + AccNo + "'";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}

