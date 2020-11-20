using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsInvAccountMaster
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;

	public ClsInvAccountMaster()
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

    public int GetReceiptNo(string ProdCode)
    {
        try
        {
            sql = "Select Max(IsNull(A.ReceiptNo, 0)) + 1 As ReceiptNo From (Select Max(IsNull(ReceiptNo, 0)) As ReceiptNo From AVS_InvAccountMaster where SubGlCode='"+ProdCode+"')A";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetNextReceiptNo(string BrCode, string ProdType)
    {
        try
        {
            sql = "Select Max(IsNull(A.ReceiptNo, 0)) + 1 As ReceiptNo From (Select Max(IsNull(CustAccNo, 0)) As ReceiptNo From AVS_InvAccountMaster Where Brcd = '" + BrCode + "' And SubGlCode = '" + ProdType + "')A";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetBoardResNo(string BrCode)
    {
        try
        {
            sql = "Select Max(IsNull(A.BoardResNo, 0)) + 1 As BoardResNo From (Select Max(IsNull(BoardResNo, 0)) As BoardResNo From AVS_InvAccountMaster where BRCD='"+BrCode+"')A";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetMaxGlNo(string BrCode)
    {
        try
        {
            sql = "exec ISP_AVS0011";// Remove BRCD and group INV as Ambika Madam 20/07/2017
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetAllData(GridView Gview, string BrCode,string BRCD)
    {
        try
        {
            sql = "Select Convert(Varchar(10), Id) +'_'+Convert(Varchar(10), SubGLCOde) As Id,SubGLCOde,BankCode, BankName, BranchCode, ReceiptNo, ReceiptName, Convert(varchar(10),OpeningDate,103) as OpeningDate,CustACCNO From AVS_InvAccountMaster Where Stage Not In ('1004','1003') and BRCD='" + BRCD + "' order by Subglcode,CustAccno";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetAllData11(GridView Gview, string BrCode, string BRCD,string Prd,string Acc)
    {
        try
        {
            string Para = "";
            if(Prd!="")
                Para+=" and Subglcode='"+Prd+"'";
            if(Acc!="")
                Para+=" and custaccno='"+Acc+"'";
            sql = "Select Convert(Varchar(10), Id) +'_'+Convert(Varchar(10), SubGLCOde) As Id,SubGLCOde,BankCode, BankName, BranchCode, ReceiptNo, ReceiptName, Convert(varchar(10),OpeningDate,103) as OpeningDate,CustACCNO From AVS_InvAccountMaster Where Stage Not In ('1004') and BRCD='" + BRCD + "' "+Para+" order by Subglcode,CustAccno";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable GetInvData( string EDate, string BRCD,string Prodcode, string AccNo)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec Isp_AVS0022 '"+BRCD+"', '"+ conn.ConvertDate(EDate)+"','"+Prodcode+"','"+AccNo+"'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public int GetAllVerifiedData(GridView Gview, string BrCode)
    {
        try
        {
            sql = "Select Convert(Varchar(10), Id) +'_'+Convert(Varchar(10), BankCode) As Id, BankCode, BankName, BranchCode, ReceiptNo, ReceiptName, OpeningDate From AVS_InvAccountMaster Where Stage = 1003 and BRCD='"+BrCode+"'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int InsertData(string BrCode, string BankNo, string BankName, string BranchNo, string BranchName, string RecNo, string RecName, string BResNumber, string BMeetDate, string OpDate, string EDate, string Mid)
    {
        try
        {
            sql = "Exec SP_InvAccountMaster @BrCode='" + BrCode + "', @BankCode='" + BankNo + "', @BankName='" + BankName + "', @BranchCode='" + BranchNo + "', @Branchname='" + BranchName + "', @ReceiptNo='" + RecNo + "', @ReceiptName='" + RecName + "', @BoardResNo = '" + BResNumber + "', @BoardMeetDate = '" + conn.ConvertDate(BMeetDate).ToString() + "', @OpeningDate='" + conn.ConvertDate(OpDate).ToString() + "', @Stage='1001', @MID='" + Mid + "', @PCMAC='" + conn.PCNAME().ToString() + "', @EntryDate='" + conn.ConvertDate(EDate).ToString() + "', @Flag='AD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int InsertData1(string BrCode, string BankNo, string BankName, string BranchNo, string BranchName, string RecNo, string RecName, string BResNumber, string BMeetDate, string OpDate, string EDate, string Mid, string PrAccNo, string PrProdCode, string InAccNo, string InProdCode, string InvType, string AC1, string Bank, string PrAccName)
    {
        try
        {
            sql = "Exec SP_InvAccountMaster1  @PrAcc='" + PrAccNo + "',@PrPrd='" + PrProdCode + "',@InAcc='" + InAccNo + "',@InPrd='" + InProdCode + "',@BrCode='" + BrCode + "', @BankCode='" + BankNo + "', @BankName='" + BankName + "', @BranchCode='" + BranchNo + "', @Branchname='" + BranchName + "', @ReceiptNo='" + RecNo + "', @ReceiptName='" + RecName + "', @BoardResNo = '" + BResNumber + "', @BoardMeetDate = '" + conn.ConvertDate(BMeetDate).ToString() + "', @OpeningDate='" + conn.ConvertDate(OpDate).ToString() + "', @Stage='1001', @MID='" + Mid + "', @PCMAC='" + conn.PCNAME().ToString() + "', @EntryDate='" + conn.ConvertDate(EDate).ToString() + "', @Flag='AD',@InvType='" + InvType + "',@AC1='" + AC1 + "',@Bank='" + Bank + "',@PrAccName='" + PrAccName + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetProdCode(string GL, string Gl1)
    {
        DataTable dt=new DataTable ();
        try
        {
            sql = "Exec SP_GetProd @GL='"+GL+"',@GL1='"+Gl1+"'" ;
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public int InsertExistingData(string BrCode, string BankNo, string BankName, string BranchNo, string BranchName, string RecNo, string RecName, string BResNumber, string BMeetDate, string OpDate, string EDate, string Mid, string PCode, string PName, string GL,string PrAccName, string CRGL, string RecGL, string CRAC, string RecAC)
    {
        try
        {
            sql = "Exec InsertExistingData @BrCode='" + BrCode + "', @BankCode='" + BankNo + "', @BankName='" + BankName + "', @BranchCode='" + BranchNo + "', @Branchname='" + BranchName + "', @ReceiptNo='" + RecNo + "', @ReceiptName='" + RecName + "', @BoardResNo = '" + BResNumber + "', @BoardMeetDate = '" + conn.ConvertDate(BMeetDate).ToString() + "', @OpeningDate='" + conn.ConvertDate(OpDate).ToString() + "', @Stage='1001', @MID='" + Mid + "', @PCMAC='" + conn.PCNAME().ToString() + "', @EntryDate='" + conn.ConvertDate(EDate).ToString() + "', @Flag='AE',@PCode='" + PCode + "', @PType='" + PName + "', @GL='" + GL + "',@PrAccName='" + PrAccName + "',@CRGL='" + CRGL + "',@RecGL='" + RecGL + "',@CRAC='" + CRAC + "',@RecAC='" + RecAC + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int ModifyData(string BrCode, string BankNo, string BankName, string BranchNo, string BranchName, string RecNo, string RecName, string BResNumber, string BMeetDate, string OpDate, string EDate, string Mid, string PrAccNo, string PrProdCode, string InAccNo, string InProdCode, string InvType, string AC1, string Bank, string PrAccName)
    {
        try
        {
            sql = "Exec SP_InvAccountMaster1 @PrAcc='" + PrAccNo + "',@PrPrd='" + PrProdCode + "',@InAcc='" + InAccNo + "',@InPrd='" + InProdCode + "',@BrCode='" + BrCode + "', @BankCode='" + BankNo + "', @BankName='" + BankName + "', @BranchCode='" + BranchNo + "', @Branchname='" + BranchName + "', @ReceiptNo='" + RecNo + "', @ReceiptName='" + RecName + "', @BoardResNo = '" + BResNumber + "', @BoardMeetDate = '" + conn.ConvertDate(BMeetDate).ToString() + "', @OpeningDate='" + conn.ConvertDate(OpDate).ToString() + "', @Stage='1001', @MID='" + Mid + "', @PCMAC='" + conn.PCNAME().ToString() + "', @EntryDate='" + conn.ConvertDate(EDate).ToString() + "', @Flag='MD',@InvType='" + InvType + "',@AC1='" + AC1 + "',@Bank='" + Bank + "',@PrAccName='" + PrAccName + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteData(string BrCode, string BankNo, string BranchNo, string RecNo, string Mid, string INV)
    {
        try
        {
            sql = "Exec SP_InvAccountMaster1 @BrCode='" + BrCode + "', @BankCode='" + BankNo + "', @BranchCode='" + BranchNo + "', @ReceiptNo='" + RecNo + "', @Stage='1004', @MID='" + Mid + "', @PCMAC='" + conn.PCNAME().ToString() + "', @Flag='DL',@InvType='" + INV + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int AuthoriseData(string BrCode, string BankNo, string BranchNo, string RecNo, string Mid,string INV)
    {
        try
        {
            sql = "Exec SP_InvAccountMaster1 @BrCode='" + BrCode + "', @BankCode='" + BankNo + "', @BranchCode='" + BranchNo + "', @ReceiptNo='" + RecNo + "', @Stage='1003', @MID='" + Mid + "', @PCMAC='" + conn.PCNAME().ToString() + "', @Flag='AT',@InvType='"+INV+"'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetInfo(string Id, string BankCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "exec SP_GetInfo @Id='"+Id+"',@Subglcode='"+BankCode+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }
    public DataTable GetInfo1(string Id, string BankCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "exec SP_GetInfo1 @Id='" + Id + "',@Subglcode='" + BankCode + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }
    public string GetClosingBal(string BRCD, string GL, string AC, string EDATE)
    {
        string Closing = "";
        try
        {
            sql = "exec Isp_AVS0022 '" + BRCD + "','" + GL + "','" + AC + "','" + conn.ConvertDate(EDATE) + "'";
            Closing = Convert.ToString(conn.sExecuteScalar(sql));
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Closing;
    }
    public int AddInvestment(string BrCode, string BankNo, string BankName, string BranchNo, string BranchName, string RecNo, string RecName, string BResNumber, string BMeetDate, string OpDate, string EDate, string Mid, string PrAccNo, string PrProdCode, string InAccNo, string InProdCode, string InvType, string AC1, string Bank, string PrAccName, string Period, double RateOfInt, double PrinAmt, double IntAmt, double MaturityAmt, string IntPayOut, string PeriodType,string DueDate,string Flag)
    {
        try
        {
            sql = "Exec Isp_AVS0055  @PrAcc='" + PrAccNo + "',@PrPrd='" + PrProdCode + "',@InAcc='" + InAccNo + "',@InPrd='" + InProdCode + "',@BrCode='" + BrCode + "', @BankCode='" + BankNo + "', @BankName='" + BankName + "', @BranchCode='" + BranchNo + "', @Branchname='" + BranchName + "', @ReceiptNo='" + RecNo + "', @ReceiptName='" + RecName + "', @BoardResNo = '" + BResNumber + "', @BoardMeetDate = '" + conn.ConvertDate(BMeetDate).ToString() + "', @OpeningDate='" + conn.ConvertDate(OpDate).ToString() + "', @Stage='1001', @MID='" + Mid + "', @PCMAC='" + conn.PCNAME().ToString() + "', @EntryDate='" + conn.ConvertDate(EDate).ToString() + "', @Flag='INVAD',@InvType='" + InvType + "',@AC1='" + AC1 + "',@Bank='" + Bank + "',@PrAccName='" + PrAccName + "',@Period = '" + Period + "', @RateOfInt = '" + RateOfInt + "', @PrincipleAmt = '" + PrinAmt + "', @InterestAmt = '" + IntAmt + "', @MaturityAmt = '" + MaturityAmt + "', @IntPayOut = '" + IntPayOut + "', @PeriodType = '" + PeriodType + "', @DueDate = '" + conn.ConvertDate(DueDate).ToString() + "',@Flag1='"+Flag+"'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }



    public void getglname(GridView grd)
    {
        try
        {
            sql = "select subglcode,glname from glmast where brcd='1' and glgroup='INV' and SUBGLCODE not in (select distinct(SUBGLCODE) from AVS_InvAccountMaster)";
            conn.sBindGrid(grd,sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}