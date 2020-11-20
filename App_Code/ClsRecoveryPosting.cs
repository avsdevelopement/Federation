using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public class ClsRecoveryPosting
{
    DataTable DT = new DataTable();
    DbConnection Conn = new DbConnection();
    ClsRecoveryOperation DARO = new ClsRecoveryOperation();
    string sql = "";
    string sResult = "";
    int Res = 0;

    public string BRCD { get; set; }
    public string MID { get; set; }
    public string ASONDT { get; set; }
    public string FL { get; set; }
    public string SFL { get; set; }
    public string ID { get; set; }
    public string RECCODE { get; set; }
    public string RECDIV { get; set; }
    public string DEBITCD { get; set; }
    public string MM { get; set; }
    public string YY { get; set; }
    public string MERGEID { get; set; }
    public string CustNo { get; set; }
    public string CustName { get; set; }
    public string BANKCODE { get; set; }
    public string STR { get; set; }
    public string MergeCustno { get; set; }
    public string MergeMM { get; set; }
    public string MergeYYYY { get; set; }
    public string ChequeNo { get; set; }
    public string ChequeDate { get; set; }

	public ClsRecoveryPosting()
	{
		
	}

    public string GetCustName(ClsRecoveryPosting RP)//amruta  
    {
        try
        {
            RP.sql = "exec ISP_GetCustName @Custno='" + RP.CustNo + "', @BRCD='" + RP.BRCD + "'";
            RP.sql = Conn.sExecuteScalar(RP.sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return RP.sql;
    }

    public DataTable GetCustDeatails(ClsRecoveryPosting RP)//amruta  
    {
        DataTable dt = new DataTable();
        try
        {
            RP.sql = "select DIVNO,OFFNO from EMPDETAIL e inner join MASTER m on e.BRCD=m.BRCD and e.CUSTNO=m.CUSTNO and e.OFFNO=m.RECDEPT  where e.STAGE<>1004 and e.CUSTNO='" + RP.CustNo + "' and e.brcd='" + RP.BRCD + "'";
            dt = Conn.GetDatatable(RP.sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public string InstMonthDiff(string EDT, string INSTDT)
    {
        try
        {
            sql = "select dbo.UF_MonthDifference('" + Conn.ConvertDate(INSTDT) + "','" + Conn.ConvertDate(EDT) + "')";
            sql = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    //  For Posting Recovery for all
    public string FnBl_RecoveryPost(ClsRecoveryPosting Rp)
    {
        try
        {
            if (Rp.BANKCODE == "1009")
            {
                Rp.sql = "Exec Isp_Recovery_Posting_X_1009 @Flag='" + Rp.FL + "',@ChequeNo='" + Rp.ChequeNo + "',@ChequeDate='" + Conn.ConvertDate(Rp.ChequeDate) + "',@Brcd='" + Rp.BRCD + "',@OnDate='" + Conn.ConvertDate(Rp.ASONDT) + "',@Mid='" + Rp.MID + "',@RecDiv='" + Rp.RECDIV + "',@RecCode='" + Rp.RECCODE + "',@DebitCode='" + Rp.DEBITCD + "',@ForMM='" + Rp.MM + "',@ForYY='" + Rp.YY + "'";
            }
            else
            {
                Rp.sql = "Exec Isp_Recovery_Posting_X @Flag='" + Rp.FL + "',@ChequeNo='" + Rp.ChequeNo + "',@ChequeDate='" + Conn.ConvertDate(Rp.ChequeDate) + "',@Brcd='" + Rp.BRCD + "',@OnDate='" + Conn.ConvertDate(Rp.ASONDT) + "',@Mid='" + Rp.MID + "',@RecDiv='" + Rp.RECDIV + "',@RecCode='" + Rp.RECCODE + "',@DebitCode='" + Rp.DEBITCD + "',@ForMM='" + Rp.MM + "',@ForYY='" + Rp.YY + "'";
            }
            Rp.sql = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Rp.sql;
    }

    //  For Posting Recovery for specific customer (Multiple Customer)
    public string FnBl_RecoveryPostSpecific(ClsRecoveryPosting Rp)
    {
        try
        {
            if (Rp.BANKCODE == "1009")
            {
                Rp.sql = "Exec Isp_Recovery_Posting_X_1009 @Flag='" + Rp.FL + "',@ChequeNo='" + Rp.ChequeNo + "',@ChequeDate='" + Conn.ConvertDate(Rp.ChequeDate) + "',@Brcd='" + Rp.BRCD + "',@OnDate='" + Conn.ConvertDate(Rp.ASONDT) + "',@Mid='" + Rp.MID + "',@RecDiv='" + Rp.RECDIV + "',@RecCode='" + Rp.RECCODE + "',@DebitCode='" + Rp.DEBITCD + "',@ForMM='" + Rp.MM + "',@ForYY='" + Rp.YY + "',@MergeCustno='" + @MergeCustno + "',@MergeMM='" + @MergeMM + "',@MergeYY='" + @MergeYYYY + "'";
            }
            else
            {
                Rp.sql = "Exec Isp_Recovery_Posting_X @Flag='" + Rp.FL + "',@ChequeNo='" + Rp.ChequeNo + "',@ChequeDate='" + Conn.ConvertDate(Rp.ChequeDate) + "',@Brcd='" + Rp.BRCD + "',@OnDate='" + Conn.ConvertDate(Rp.ASONDT) + "',@Mid='" + Rp.MID + "',@RecDiv='" + Rp.RECDIV + "',@RecCode='" + Rp.RECCODE + "',@DebitCode='" + Rp.DEBITCD + "',@ForMM='" + Rp.MM + "',@ForYY='" + Rp.YY + "',@MergeCustno='" + @MergeCustno + "',@MergeMM='" + @MergeMM + "',@MergeYY='" + @MergeYYYY + "'";
            }
            Rp.sql = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Rp.sql;
    }

    //  For Posting Recovery for specific customer wise (Single Customer)
    public string FnBl_RecoveryCustPostSpecific(ClsRecoveryPosting Rp)
    {
        try
        {
            if (Rp.BANKCODE == "1009")
            {
                Rp.sql = "Exec Isp_Recovery_CustPosting_X_1009 @Flag='" + Rp.FL + "',@ChequeNo='" + Rp.ChequeNo + "',@ChequeDate='" + Conn.ConvertDate(Rp.ChequeDate) + "',@Brcd='" + Rp.BRCD + "',@OnDate='" + Conn.ConvertDate(Rp.ASONDT) + "',@Mid='" + Rp.MID + "',@RecDiv='" + Rp.RECDIV + "',@RecCode='" + Rp.RECCODE + "',@DebitCode='" + Rp.DEBITCD + "',@CustNo='" + Rp.CustNo + "',@MergeCustno='" + @MergeCustno + "',@MergeMM='" + @MergeMM + "',@MergeYY='" + @MergeYYYY + "'";
            }
            else
            {
                Rp.sql = "Exec Isp_Recovery_CustPosting_X @Flag='" + Rp.FL + "',@ChequeNo='" + Rp.ChequeNo + "',@ChequeDate='" + Conn.ConvertDate(Rp.ChequeDate) + "',@Brcd='" + Rp.BRCD + "',@OnDate='" + Conn.ConvertDate(Rp.ASONDT) + "',@Mid='" + Rp.MID + "',@RecDiv='" + Rp.RECDIV + "',@RecCode='" + Rp.RECCODE + "',@DebitCode='" + Rp.DEBITCD + "',@CustNo='" + Rp.CustNo + "',@MergeCustno='" + @MergeCustno + "',@MergeMM='" + @MergeMM + "',@MergeYY='" + @MergeYYYY + "'";
            }
            Rp.sql = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Rp.sql;
    }

    //  For Report use only
    public DataTable FnBl_RecoveryExReport(ClsRecoveryPosting Rp)
    {
        try
        {
            if (Rp.BANKCODE == "1009")
            {
                Rp.sql = "Exec Isp_Recovery_Posting_1009 @Flag='" + Rp.FL + "',@SFlag='" + Rp.SFL + "',@Brcd='" + Rp.BRCD + "',@OnDate='" + Conn.ConvertDate(Rp.ASONDT) + "',@Mid='" + Rp.MID + "',@RecDiv='" + Rp.RECDIV + "',@RecCode='" + Rp.RECCODE + "',@DebitCode='" + Rp.DEBITCD + "',@ForMM='" + Rp.MM + "',@ForYY='" + Rp.YY + "'";
            }
            else
            {
                Rp.sql = "Exec Isp_Recovery_Posting @Flag='" + Rp.FL + "',@SFlag='" + Rp.SFL + "',@Brcd='" + Rp.BRCD + "',@OnDate='" + Conn.ConvertDate(Rp.ASONDT) + "',@Mid='" + Rp.MID + "',@RecDiv='" + Rp.RECDIV + "',@RecCode='" + Rp.RECCODE + "',@DebitCode='" + Rp.DEBITCD + "',@ForMM='" + Rp.MM + "',@ForYY='" + Rp.YY + "'";
            }
            Rp.DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Rp.DT;
    }

    public string GetRecTypeCode(string BRCD, string RECDIV, string RECCODE)
    {
        string RES = "";
        try
        {
            sql = "Select Top 1 RecType from PAYMAST " +
                            " where BRCD='" + BRCD + "' " +
                            " and RECDIV='" + RECDIV + "' " +
                             " and RECCODE ='" + RECCODE + "' " +
                            " and STAGE='1003' " +
                            " order  by RECCODE";
            RES = Conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RES;
    }

    //  For Report use only
    public DataTable FnBl_RecoveryExReportSpecific(ClsRecoveryPosting Rp)
    {
        try
        {
            if (Rp.BANKCODE == "1009")
            {
                Rp.sql = "Exec Isp_Recovery_CustPosting_1009 @Flag='" + Rp.FL + "',@SFLAG='" + Rp.SFL + "',@Brcd='" + Rp.BRCD + "',@OnDate='" + Conn.ConvertDate(Rp.ASONDT) + "',@Mid='" + Rp.MID + "',@RecDiv='" + Rp.RECDIV + "',@RecCode='" + Rp.RECCODE + "',@DebitCode='" + Rp.DEBITCD + "',@CustNo='" + Rp.CustNo + "',@Identity='" + Rp.MERGEID + "'";
            }
            else
            {
                Rp.sql = "Exec Isp_Recovery_CustPosting @Flag='" + Rp.FL + "',@SFLAG='" + Rp.SFL + "',@Brcd='" + Rp.BRCD + "',@OnDate='" + Conn.ConvertDate(Rp.ASONDT) + "',@Mid='" + Rp.MID + "',@RecDiv='" + Rp.RECDIV + "',@RecCode='" + Rp.RECCODE + "',@DebitCode='" + Rp.DEBITCD + "',@CustNo='" + Rp.CustNo + "',@Identity='" + Rp.MERGEID + "'";
            }
            Rp.DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Rp.DT;
    }

}