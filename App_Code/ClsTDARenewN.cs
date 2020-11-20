using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for ClsTDARenewN
/// </summary>
public class ClsTDARenewN
{
    ClsEncryptValue Ecry = new ClsEncryptValue();
    string sql = "";
    string sql1 = "";
    int Res = 0;
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
    string EntryMid, verifyMid, DeleteMid = "";

	public ClsTDARenewN()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertFD(string FL,string Glcode,string Subgl,string Accno,string Custno,string Amount,string Trxtype,string Opdate,string PrnAmt,string Rate,string PeriodType,
                    string Period,string IntAmount,string DueDate,string MatAmt,string TrfGlcode,string TrfSubglcode,string TrfAccno,string Stage,string IntPayout,string RecNo,string OPrType,
                    string AccType,string Activity,string Pmtmode,string TableFlag,string Brcd,string Mid,string EDT,string REFID)
    {
        try
        {
            sql = "Exec Isp_TDARenewal @Flag='ADD',@Glcode='"+Glcode+"',@Subglcode='"+Subgl+"',@Accno='"+Accno+"',@CustNo='"+Custno+"',@Amount='"+Amount+"',@TrxType='"+Trxtype+"'," +
                 " @OpDate='" + conn.ConvertDate(Opdate) + "',@PrnAmt='"+PrnAmt+"',@Rate='"+Rate+"',@PeriodType='"+PeriodType+"',@Period='"+Period+"',@IntAmt='"+IntAmount+"', " +
                 " @DueDate='" + conn.ConvertDate(DueDate) + "',@MatAmt='" + MatAmt + "',@TrfGLcode='" + TrfGlcode + "',@TrfSubgl='" + TrfSubglcode + "',@TrfAccno='" + TrfAccno + "',@Stage='"+Stage+"',@IntPayOut='"+IntPayout+"', " +
                 " @Recno='"+RecNo+"',@OprType='"+OPrType+"',@AccType='"+AccType+"',@Activity='"+Activity+"'," +
                 " @PmtMode='" + Pmtmode + "',@TableFlag='" + TableFlag + "',@Brcd='" + Brcd + "',@Mid='" + Mid + "',@EntryDate='" + conn.ConvertDate(EDT) + "',@RefId='" + REFID + "'";

            Res = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }



    public int InsertEntry(string FL, string Glcode, string Subglcode, string Accno, string Amount, string CUSTNO, string Trxtype, string Activty, string PmtMode, string Brcd, string Mid, string EDT, string TableFlag, string REFID, string RecSrno, string CHQNO = "0")
    {
        try
        {
            sql = "Exec Isp_TDARenewal @Flag='ADD',@Glcode='" + Glcode + "',@Subglcode='" + Subglcode + "',@Accno='" + Accno + "',@Amount='" + Amount + "',@TrxType='" + Trxtype + "'" +
                " ,@Brcd='" + Brcd + "',@Mid='" + Mid + "',@EntryDate='" + conn.ConvertDate(EDT) + "',@TableFlag='" + TableFlag + "',@CustNo='" + CUSTNO + "',@Activity='" + Activty + "'," +
                 " @PmtMode='" + PmtMode + "',@RefId='" + REFID + "',@CheqNo='" + CHQNO + "',@RecSrno='" + RecSrno + "'";

            Res = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }


    public string PostSingle(string FL, string SFL, string MID, string EDT, string BRCD, string RecSrno)
    {
        try
        {
            EntryMid = Ecry.GetMK(MID.ToString());

            sql = "Exec Isp_TDARenewal @F1='" + EntryMid + "',@Flag='" + FL + "',@SFlag='" + SFL + "',@Mid='" + MID + "',@EntryDate='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "',@RecSrno='" + RecSrno + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public int DeleteData(string FL, string SFL, string MID, string BRCD, string EDT)
    {
        try
        {
            sql = "Exec Isp_TDARenewal @Flag='" + FL + "',@SFlag='" + SFL + "',@Mid='" + MID + "',@EntryDate='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "'";
            Res = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    public int InsertEntry(string FL, string Glcode, string Subglcode, string Accno, string Amount, string CUSTNO, string Trxtype, string Activty, string PmtMode, string Brcd, string Mid, string EDT, string TableFlag, string REFID, string CHQNO = "0")
    {
        try
        {
            sql = "Exec Isp_TDARenewal @Flag='ADD',@Glcode='" + Glcode + "',@Subglcode='" + Subglcode + "',@Accno='" + Accno + "',@Amount='" + Amount + "',@TrxType='" + Trxtype + "'" +
                " ,@Brcd='" + Brcd + "',@Mid='" + Mid + "',@EntryDate='" + conn.ConvertDate(EDT) + "',@TableFlag='" + TableFlag + "',@CustNo='" + CUSTNO + "',@Activity='" + Activty + "'," +
                 " @PmtMode='" + PmtMode + "',@RefId='" + REFID + "',@CheqNo='" + CHQNO + "'";

            Res = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public int InsertNOMINEE(string FL, string TBFlag, string Glcode, string Subgl, string Accno, string Custno, string Stage, string brcd, string Mid, string Edt, string Nom1, string Nom2, string Rel1, string Rel2, string Gen1, string Gen2)
    {
        try
        {
            sql = "Exec Isp_TDARenewal @Flag='" + FL + "',@TableFlag='" + TBFlag + "',@Glcode='" + Glcode + "',@Subglcode='" + Subgl + "', " +
                 " @Accno='" + Accno + "',@CustNo='" + Custno + "',@Stage='" + Stage + "',@Brcd='" + brcd + "',@Mid='" + Mid + "',@EntryDate='" + conn.ConvertDate(Edt) + "', " +
                 " @Nom1='" + Nom1 + "',@Nom2='" + Nom2 + "',@Rel1='" + Rel1 + "',@Rel2='" + Rel2 + "',@Gen1='" + Gen1 + "',@Gen2='" + Gen2 + "'";

            Res = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    public int DeleteData(string FL, string MID, string BRCD, string EDT)
    {
        try
        {
            sql = "Exec Isp_TDARenewal @Flag='" + FL + "',@Mid='" + MID + "',@EntryDate='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "'";
            Res = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    public string PostSingle(string FL,string SFL,string MID,string EDT,string BRCD)
    {
        try
        {
            EntryMid = Ecry.GetMK(MID.ToString());

            sql = "Exec Isp_TDARenewal @F1='" + EntryMid + "',@Flag='" + FL + "',@SFlag='" + SFL + "',@Mid='" + MID + "',@EntryDate='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetBalance(string FL, string BRCD, string MID, string ENTRYDATE)
    {
        try
        {
            sql = "Exec Isp_TDARenewal @Flag='" + FL + "',@Mid='" + MID + "',@EntryDate='" + conn.ConvertDate(ENTRYDATE) + "',@Brcd='" + BRCD + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetMinRenewDay()
    {
        try
        {
            sql = "Exec Isp_TDARenewal @Flag='MINDAYRENEW'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public int BindGridData(string FL,GridView GD, string MID, string BRCD, string EDT)
    {
        try
        {
            sql = "Exec Isp_TDARenewal @Flag='" + FL + "',@Mid='" + MID + "',@EntryDate='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "'";
            Res = conn.sBindGrid(GD,sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    public DataTable GetNomDetail(string BRCD, string CUSTNO, string Subgl, string Accno)
    {
        try
        {
            sql = "Select CUSTNO,NOMINEENAME,isnull(RELATION,0) RELATION,GLCODE,SUBGLCODE,ACCNO,BRCD,isnull(GENDER,0) GENDER,isnull(PanNo,0) PANNO from Dbo.NomineeDetails " +
                " where CUSTNO='" + CUSTNO + "' " +
                " and SUBGLCODE='" + Subgl + "' " +
                " and ACCNo='" + Accno + "' " +
                " and BRCD='" + BRCD + "' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
}