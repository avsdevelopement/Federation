using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class ClsChargesMaster
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    int Result = 0;

    public ClsChargesMaster()
    {
    }

    public string Getaccno(string AT, string BRCD)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),GLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
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
        DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";

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

    public void BindCharges(string session, DropDownList ddlCharges)
    {
        sql = "SELECT * FROM AVS_LNTRX_HEAD ORDER BY SrNumber";
        conn.FillCHARGES(ddlCharges, sql);
    }

    public int InsertCharges(string ACCNo, string loanglcode, string Subglcode, string charges, string BRCD, string narration, string amount, string MID, string Date, string Charges, string Setno)
    {
        //Dhanya Shetty//added set no //15/03/2018
        try
        {
            if (charges == "1")
            {
                if (Charges == "1")
                    sql = "INSERT INTO AVS_LNTRX (BRCD,SUBGLCODE,ACCOUNTNO,HEADDESC,TRXTYPE,NARRATION,AMOUNT,LOANGLCODE,STAGE,MID,MID_ENTRYDATE,VID_ENTRYDATE,ENTRYDATE,SYSTEMDATE,SETNO,SCROLLNO,OLDGL) VALUES ('" + BRCD + "','" + loanglcode + "','" + ACCNo + "','" + charges + "',2,'" + narration + "','" + amount + "','" + loanglcode + "',1003,'" + MID + "','" + Date + "','" + Date + "','" + Date + "',Getdate(),'" + Setno + "',0,0)";
                else
                    sql = "INSERT INTO AVS_LNTRX (BRCD,SUBGLCODE,ACCOUNTNO,HEADDESC,TRXTYPE,NARRATION,AMOUNT,LOANGLCODE,STAGE,MID,MID_ENTRYDATE,VID_ENTRYDATE,ENTRYDATE,SYSTEMDATE,SETNO,SCROLLNO,OLDGL) VALUES ('" + BRCD + "','" + loanglcode + "','" + ACCNo + "','" + charges + "',1,'" + narration + "','" + amount + "','" + loanglcode + "',1003,'" + MID + "','" + Date + "','" + Date + "','" + Date + "',Getdate(),'" + Setno + "',0,0)";
            }
            else
            {
                if (Charges == "1")
                    sql = "INSERT INTO AVS_LNTRX (BRCD,SUBGLCODE,ACCOUNTNO,HEADDESC,TRXTYPE,NARRATION,AMOUNT,LOANGLCODE,STAGE,MID,MID_ENTRYDATE,VID_ENTRYDATE,ENTRYDATE,SYSTEMDATE,SETNO,SCROLLNO,OLDGL) VALUES ('" + BRCD + "','" + Subglcode + "','" + ACCNo + "','" + charges + "',2,'" + narration + "','" + amount + "','" + loanglcode + "',1003,'" + MID + "','" + Date + "','" + Date + "','" + Date + "',Getdate(),'" + Setno + "',0,0)";
                else
                    sql = "INSERT INTO AVS_LNTRX (BRCD,SUBGLCODE,ACCOUNTNO,HEADDESC,TRXTYPE,NARRATION,AMOUNT,LOANGLCODE,STAGE,MID,MID_ENTRYDATE,VID_ENTRYDATE,ENTRYDATE,SYSTEMDATE,SETNO,SCROLLNO,OLDGL) VALUES ('" + BRCD + "','" + Subglcode + "','" + ACCNo + "','" + charges + "',1,'" + narration + "','" + amount + "','" + loanglcode + "',1003,'" + MID + "','" + Date + "','" + Date + "','" + Date + "',Getdate(),'" + Setno + "',0,0)";
            }
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable InsertChargesintoM(string ACCNo, string loanglcode, string Subglcode, string charges, string BRCD, string narration, string amount, string MID, string Date, string Charges, string ProdCode, string EDate, string CustNo, string pay, string activity)
    {
        DataTable dt = new DataTable();
        try
        {

            sql = "exec SP_ChargesDebit @ACCNo='" + ACCNo + "',@loanglcode='" + loanglcode + "',@Subglcode='" + Subglcode + "',@charges='" + charges + "',@BRCD='" + BRCD + "',@narration='" + narration + "',@amount='" + amount + "',@MID='" + MID + "',@Date='" + Date + "',@Charges1='" + Charges + "',@ProdCode='" + ProdCode + "',@EDate='" + conn.ConvertDate(EDate) + "',@CustNo='" + CustNo + "',@Pay='" + pay + "',@Activity='" + activity + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public DataTable GetGlCode(string Charges)
    {
        try
        {
            sql = "SELECT SUBGLCODE FROM AVS_LNTRX_HEAD WHERE SRNUMBER='" + Charges + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetBal(string AccNo, string BRCD, string Loanglcode)
    {
        try
        {
            sql = "EXEC SP_CalculateBalAmount @BrCode = '" + BRCD + "', @PrCode = '" + Loanglcode + "', @AccNo = '" + AccNo + "', @Flag='A'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetPertiBal(string AccNo, string BRCD, string Loanglcode, string Charges)
    {
        try
        {
            sql = "Exec SP_CalculateBalAmount @BrCode = '" + BRCD + "', @PrCode = '" + Loanglcode + "', @AccNo = '" + AccNo + "', @Charges = '" + Charges + "', @Flag='S'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable CheckAccNo(string ACCNO, string BRCD)
    {
        try
        {
            sql = "SELECT * FROM AVS_ACC WHERE ACCNO='" + ACCNO + "' AND ACC_STATUS!=3 AND BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable FectAccName(string BRCD, string ACCNO, string Gl)
    {
        try
        {
            sql = "select * from loaninfo LI inner join avs_acc ac on ac.BRCD = LI.BRCD And ac.SUBGLCODE = LI.LOANGLCODE And ac.ACCNO = LI.CUSTACCNO inner join Master m on ac.custno=m.custno Where ac.brcd='" + BRCD + "' and ac.accno='" + ACCNO + "' and ac.subglcode='" + Gl + "' and ac.stage=1003";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable FetchLGL(string AT, string BRCD)
    {
        try
        {
            sql = "SELECT LOANTYPE,LOANGLCODE FROM LOANGL WHERE BRCD='" + BRCD + "' AND LOANGLCODE='" + AT + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int GetIntCal(string BrCode, string SGlCode)
    {
        try
        {
            sql = "Select IntCalType From LoanGl Where Brcd = '" + BrCode + "' And LoanGlCode='" + SGlCode + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetGlCode(string GL, string BRCD)
    {
        try
        {
            sql = "SELECT * FROM glmast WHERE brcd='" + BRCD + "' and subglcode='" + GL + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetGlSubCode(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select G2.GlCode, G2.SUBGLCODE, L.PPL From GlMast G1 Inner Join Glmast G2 With(NoLock) On G1.BRCD = G2.BRCD And G1.IR = G2.SubGlCode " +
                  "Inner Join LoanGl L With(NoLock) On L.BRCD = G1.BRCD And L.LOANGLCODE = G1.SubGlCode Where G1.Brcd = '" + BrCode + "' And G1.SUBGLCODE = '" + PrCode + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable FetchCustName(string BRCD, string ACCNO, string GLCOde)
    {
        try
        {
            sql = "  select * from avs_acc where brcd='" + BRCD + "' and accno='" + ACCNO + "' and subglcode='" + GLCOde + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int InsertNoticeLog(string brcd, string subgl, string accno, string noticedt, string noticetype, string mid)
    {
        try
        {
            sql = "insert into avs5017(brcd,subglcode,accno,NoticeSndDT,NoticeType,Stage,mid) values('" + brcd + "','" + subgl + "','" + accno + "','" + conn.ConvertDate(noticedt) + "','" + noticetype + "','1001','" + mid + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public DataTable GetSetAmount(string BrCode, string PrCode, string AccNo, string SetNo, string Edate, string HeadDesc, string TrxType)
    {
        try
        {
            sql = "Select Amount From AVS_LnTrx Where BrCd = '" + BrCode + "' And LoanGlCode = '" + PrCode + "' And AccountNo = '" + AccNo + "' " +
                  "And SetNo = '" + SetNo + "' and EntryDate = '" + Edate + "' And TrxType = '" + TrxType + "' And HeadDesc = '" + HeadDesc + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int BindChargesDebOut(GridView Gview, string Brcd, string Prd, string Acc, string Edate)//Dhanya shetty//15/03/2018
    {
        try
        {
            sql = "Select id, BRCD, SubGlCode, AccountNo, HeadDesc, (case when TrxType = 1 then 'Credit' when TrxType=2 then 'Debit' end) As TrxType, Narration, Amount, SetNo, LoanGlCode " +
                  "From AVS_LnTrx where LoanGlCode='" + Prd + "' And brcd='" + Brcd + "' And AccountNo='" + Acc + "' And stage <> 1004 " +
                  "And EntryDate = '" + conn.ConvertDate(Edate) + "' order by setno";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetChargesDebOut(string id)//Dhanya shetty//15/03/2018
    {
        try
        {
            sql = "select LoanGlCode,AccountNo,HeadDesc,Amount from  AVS_LnTrx where id='" + id + "' and stage<>1004 ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int DeleteChargesDebO(string Id, string Edate, string VID)//Dhanya shetty//15/03/2018
    {
        try
        {
            sql = "Update AVS_LnTrx set stage=1004,VID='" + VID + "',VID_EntryDate='" + conn.ConvertDate(Edate) + "'  where id='" + Id + "' and EntryDate='" + conn.ConvertDate(Edate) + "'  and stage<>1004";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int ModifyChargesDebO(string Id, string Edate, string LoanGlCode, string AccountNo, string HeadDesc, string Amount, string VID)//Dhanya shetty//15/03/2018
    {
        try
        {
            sql = "update  AVS_LnTrx set stage=1002,LoanGlCode='" + LoanGlCode + "',AccountNo='" + AccountNo + "',HeadDesc='" + HeadDesc + "',Amount='" + Amount + "' ," +
                " VID='" + VID + "',VID_EntryDate='" + conn.ConvertDate(Edate) + "' where id='" + Id + "' and EntryDate='" + conn.ConvertDate(Edate) + "' and stage<>1004";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int UpdateRecord(string BrCode, string PrCode, string AccNo, string SetNo, string EDate, string HeadDesc, string TrxType, string Amount, string Mid, string WDate)
    {
        try
        {
            sql = "Update AVS_LnTrx Set Amount = '" + Amount + "', Vid = '" + Mid + "', VID_EntryDate = '" + conn.ConvertDate(WDate) + "' " +
                  "Where BrCd = '" + BrCode + "' And LoanGlCode = '" + PrCode + "' And AccountNo = '" + AccNo + "' " +
                  "And SetNo = '" + SetNo + "' and EntryDate = '" + EDate + "' And HeadDesc = '" + HeadDesc + "' And TrxType = '" + TrxType + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteRecord(string BrCode, string PrCode, string AccNo, string SetNo, string EDate, string HeadDesc, string TrxType, string Mid, string WDate)
    {
        try
        {
            sql = "Update AVS_LnTrx Set Stage = '1004', Vid = '" + Mid + "', VID_EntryDate = '" + conn.ConvertDate(WDate) + "' " +
                  "Where BrCd = '" + BrCode + "' And LoanGlCode = '" + PrCode + "' And AccountNo = '" + AccNo + "' " +
                  "And SetNo = '" + SetNo + "' and EntryDate = '" + EDate + "' And HeadDesc = '" + HeadDesc + "' And TrxType = '" + TrxType + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}