using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsInvCreateReceipt
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

    string sql = "", rtn = "";
    int Result = 0;

	public ClsInvCreateReceipt()
	{
		
	}

    public string Getaccno(string AT, string BRCD)
    {
        try
        {
            sql = "SELECT (CONVERT (VARCHAR(10),GLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            //sql = " SELECT isnull(Max(RecAC),0)+1 as AccNo FROM AVS_INVAccountMaster WHERE BRCD='" + BRCD + "' AND RecGL='" + AT + "'";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public string GetAcc(string AT, string BRCD)
    {
        try
        {
            sql = " SELECT isnull(Max(RecAC),0)+1 as AccNo FROM AVS_INVAccountMaster WHERE BRCD='" + BRCD + "' AND RecGL='" + AT + "'";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO  WHERE AC.BRCD='" + BRCD + "' And AC.ACCNO='" + ACCNO + "' And AC.SUBGLCODE='" + GLCODE + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public double GetOpenClose(string Brcode, string ProdCode, string AccNo, string EDate, string Flag)
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OpClBalance @BrCode = '" + Brcode + "', @SubGlCode = '" + ProdCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }

    public int CheckAccount(string BrCode, string ProdCode, string AccNo)
    {
        int RC = 0;
        try
        {
            sql = "Exec SP_FDCREATION @FLAG='CHECKDATA',@ACCNO='" + AccNo + "',@DPGLCODE='" + ProdCode + "',@BRCD='" + BrCode + "'";
            RC = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RC;
    }

    public DataTable GetCustDetails(string BrCode, string SubGlCode, string AccNo)
    {
        try
        {
            sql = "Select * From AVS_InvAccountMaster ac Where AC.BRCD = '" + BrCode + "' And AC.BankCode = '" + SubGlCode + "' And AC.CustAccNo = '" + AccNo + "' ";
           
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetCustDetails1(string BrCode, string SubGlCode, string AccNo)//Amruta 21/06/2017
    {
        try
        {

            sql = "Select *,Convert(nvarchar(10),OpeningDate,103) as OD From AVS_InvAccountMaster ac Where AC.BRCD = '" + BrCode + "' And AC.SubGlCode = '" + SubGlCode + "' And AC.CustAccno = '" + AccNo + "' and Stage<>1004";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    //public string CheckPeriod(string BrCode, string ProdCode, string Period, string PeriodType)//, string CustType//Dhanya Shetty 03-07-2017
    //{
    //    string IsPValid = "0";
    //    try
    //    {//And TDCUSTTYPE='" + CustType + "'
    //        sql = "Select * From A50001 Where BRCD = '" + BrCode + "' And PERIODTYPE = '" + PeriodType + "'  And (PERIODFROM <= '" + Period + "' And PERIODTO >= '" + Period + "') And DEPOSITGL ='" + ProdCode + "'";
    //        DataTable DT = conn.GetDatatable(sql);
            
    //        if (DT.Rows.Count > 0)
    //            IsPValid = "1";
    //        else
    //            IsPValid = "0";
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return IsPValid;
    //}

    public float GetIntRate(string BrCode, string ProdCode, string Period, string PeriodType, bool TF)//, string CustType
    {
        float rtnf = 0;
        try
        {
            if (TF == true)
            {
                sql = "Select RATE From A50001 Where DEPOSITGL = '" + ProdCode + "' And (PERIODFROM <= '" + Period + "' And PERIODTO >='" + Period + "') And BRCD ='" + BrCode + "' And PERIODTYPE='D'";
            }
            else
            {
                sql = "Select RATE From A50001 Where DEPOSITGL = '" + ProdCode + "' And (PERIODFROM <= '" + Period + "' And PERIODTO >='" + Period + "') And BRCD ='" + BrCode + "' And PERIODTYPE='" + PeriodType + "'";// And TDCUSTTYPE='" + CustType + "'
            }
            rtn = conn.sExecuteScalar(sql);
            if (rtnf != null)
            {
                rtnf = float.Parse(rtn);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtnf;
    }

    public int CreateDeposite(string BrCode, string CustNo, string CustAccNo, string DepGlCode, string Period, double RateOfInt, double PrinAmt, double IntAmt, double MaturityAmt, string IntTrfSubGlCode, string IntTrfAccNo, string PrinTrfSubGlCode, string PrinTrfAccNo, string IntPayOut, string PeriodType, string OpenDate, string DueDate, string Mid, string EDate)
    {
        try
        {
            sql = "Exec Sp_InvCreateReceipt @BrCode = '" + BrCode + "', @CustNo = '" + CustNo + "', @CustAccNo = '" + CustAccNo + "', @DepGlCode = '" + DepGlCode + "', @Period = '" + Period + "', @RateOfInt = '" + RateOfInt + "', @PrincipleAmt = '" + PrinAmt + "', @InterestAmt = '" + IntAmt + "', @MaturityAmt = '" + MaturityAmt + "', @IntTrfSubGl = '" + IntTrfSubGlCode + "', @IntTrfAccNo = '" + IntTrfAccNo + "', @PrinTrfSubGl = '" + PrinTrfSubGlCode + "', @PrinTrfAccNo = '" + PrinTrfAccNo + "', @IntPayOut = '" + IntPayOut + "', @PeriodType = '" + PeriodType + "', @OpeningDate = '" + conn.ConvertDate(OpenDate).ToString() + "', @DueDate = '" + conn.ConvertDate(DueDate).ToString() + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = 'AD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
                           
    public int CreateDeposite1(string BrCode, string CustNo, string CustAccNo, string DepGlCode, string Period, double RateOfInt, double PrinAmt, double IntAmt, double MaturityAmt,  string IntPayOut, string PeriodType, string OpenDate, string DueDate, string Mid, string EDate,string receipt)
    {
        try
        {
            sql = "Exec Sp_InvCreateReceipt1 @BrCode = '" + BrCode + "', @CustNo = '" + CustNo + "', @CustAccNo = '" + CustAccNo + "', @DepGlCode = '" + DepGlCode + "', @Period = '" + Period + "', @RateOfInt = '" + RateOfInt + "', @PrincipleAmt = '" + PrinAmt + "', @InterestAmt = '" + IntAmt + "', @MaturityAmt = '" + MaturityAmt + "', @IntPayOut = '" + IntPayOut + "', @PeriodType = '" + PeriodType + "', @OpeningDate = '" + conn.ConvertDate(OpenDate).ToString() + "', @DueDate = '" + conn.ConvertDate(DueDate).ToString() + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = 'AD',@Receipt='"+receipt+"'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int GetAllData(GridView Gview, string Edate,string BRCD,string PT, string AC)//Dhanya Shetty//07/10/2017
    {
        try
        {
            //and D.EntryDate='" + conn.ConvertDate(Edate).ToString() + "'
            if (PT == "" && AC == "")
            {
                sql = " Select  D.id, D.SubGlCode, D.CustAccNo,A.bankname From avs_invdepositemaster D inner join AVS_invAccountMaster A on A.brcd=D.brcd and A.SubGlCode=D.SubGlCode and A.CustAccno=D.CustAccNo " +
 " Where D.Stage Not In ('1004') and D.BRCD='" + BRCD + "'  order by D.Subglcode,D.CustAccno";
            }
            else
            {
                sql = " Select  D.id, D.SubGlCode, D.CustAccNo,A.bankname From avs_invdepositemaster D inner join AVS_invAccountMaster A on A.brcd=D.brcd and A.SubGlCode=D.SubGlCode and A.CustAccno=D.CustAccNo " +
 " Where D.Stage Not In ('1004') and D.BRCD='" + BRCD + "' and D.SubGlCode='" + PT + "' and D.CustAccNo='"+AC+"' order by D.Subglcode,D.CustAccno";
            }
           
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable GetInfo(string BRCD, string id, string PRD, string ACCNT)//Dhanya Shetty//07/10/2017
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select  CustNo, CustAccNo, SubGlCode, Period, RateOfInt, PrincipleAmt, InterestAmt, MaturityAmt,IntPayOut, PeriodType,Convert(varchar(11),OpeningDate,103) as OpeningDate,Convert(varchar(11),DueDate,103) as DueDate from avs_invdepositemaster where id='" + id + "' and SubGlCode='" + PRD + "' and CustAccNo='" + ACCNT + "' and brcd='" + BRCD + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    //Dhanya Shetty//07/10/2017
    public int Modify(string BrCode, string CustAccNo, string DepGlCode, string Period, double RateOfInt, double PrinAmt, double IntAmt, double MaturityAmt, string IntPayOut, string PeriodType, string OpenDate, string DueDate, string id)
    {
        try
        {
            sql = "Exec Sp_InvCreateReceipt1 @BrCode = '" + BrCode + "', @CustAccNo = '" + CustAccNo + "', @DepGlCode = '" + DepGlCode + "', @Period = '" + Period + "', @RateOfInt = '" + RateOfInt + "', @PrincipleAmt = '" + PrinAmt + "', " +
                " @InterestAmt = '" + IntAmt + "', @MaturityAmt = '" + MaturityAmt + "', @IntPayOut = '" + IntPayOut + "', @PeriodType = '" + PeriodType + "', @OpeningDate = '" + conn.ConvertDate(OpenDate).ToString() + "', @DueDate = '" + conn.ConvertDate(DueDate).ToString() + "', @Flag = 'MD',@Id='" + id + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}