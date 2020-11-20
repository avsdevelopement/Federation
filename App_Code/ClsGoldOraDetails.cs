using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsGoldOraDetails
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0,res=0;

    public ClsGoldOraDetails()
    {

    }

    public string GetProdName(string AT, string BRCD, string GLCD)
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

    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME,CONVERT(Varchar(10),LI.SANSSIONDATE,103) as SANSSIONDATE FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO INNER JOIN loaninfo LI ON LI.CUSTNO=M.CUSTNO WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int BindGrid(GridView Gview, string BRCD)
    {
        try
        {
            sql = "SELECT convert(varchar(10),BRCD)+'_'+convert(varchar(10),SubGlCode)+'_'+convert(varchar(10),AccNo) AS id, BRCD, SubGlCode, AccNo, GLNSrNo, GLNParti, Qty, NetWt, Rate, Price,VALUERNAME,ORNAMENTSKEPT,REMARK FROM AVS_GLDORATable WHERE BRCD = '" + BRCD + "' AND STAGE NOT IN ('1003', '1004')";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int BindGridMD(GridView Gview, string BRCD)
    {
        try
        {
            sql = "SELECT convert(varchar(10),BRCD)+'_'+convert(varchar(10),SubGlCode)+'_'+convert(varchar(10),AccNo) AS id, BRCD, SubGlCode, AccNo, GLNSrNo, GLNParti, Qty, NetWt, Rate, Price,VALUERNAME,ORNAMENTSKEPT,REMARK FROM AVS_GLDORATable WHERE BRCD = '" + BRCD + "' AND STAGE <>'1004'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int BindGridAdd(GridView Gv, string BRCD,string ACCNO)
    {
        try
        {
            sql = "SELECT convert(varchar(10),BRCD)+'_'+convert(varchar(10),SubGlCode)+'_'+convert(varchar(10),AccNo) AS id, BRCD, SubGlCode, AccNo, GLNSrNo, GLNParti, Qty, NetWt, Rate, Price,VALUERNAME,ORNAMENTSKEPT,REMARK FROM AVS_GLDORATable WHERE BRCD = '" + BRCD + "' AND AccNo='"+ACCNO+"' AND STAGE NOT IN ('1003', '1004') order by systemdate desc";
            res = conn.sBindGrid(Gv, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return res;
    }
    public int BindGridAddMD(GridView Gv, string BRCD, string ACCNO)
    {
        try
        {
            sql = "SELECT convert(varchar(10),BRCD)+'_'+convert(varchar(10),SubGlCode)+'_'+convert(varchar(10),AccNo) AS id, BRCD, SubGlCode, AccNo, GLNSrNo, GLNParti, Qty, NetWt, Rate, Price,VALUERNAME,ORNAMENTSKEPT,REMARK FROM AVS_GLDORATable WHERE BRCD = '" + BRCD + "' AND AccNo='" + ACCNO + "' AND STAGE NOT IN ('1004') order by systemdate desc";
            res = conn.sBindGrid(Gv, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return res;
    }
    public DataTable GetInfo(GridView Gview, string BRCD, string PrCode, string AccNo)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec RptGLoanOra @BRCD= '" + BRCD + "', @SubGlCode='" + PrCode + "', @AccNo='" + AccNo + "', @Flag='VW'";
            DT = conn.GetDatatable(sql);
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return DT = null;
        }
        return DT;
    }
    public DataTable GetInfo1(string BRCD, string PrCode, string AccNo)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec RptGLoanOra @BRCD= '" + BRCD + "', @SubGlCode='" + PrCode + "', @AccNo='" + AccNo + "', @Flag='VW'";
            DT = conn.GetDatatable(sql);
          
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return DT = null;
        }
        return DT;
    }
    public int InsertData(string BrCode, string PrCode, string AccCode, string CustNo, string SrNo, string Part, string Qty, double NetWt, double Rate, double Price, string EDate, string Mid, string valuer, string ornament, string remark, string sanssion, string GrossWt, string RecNo)
    {
        Result = 0;

        try
        {
            sql = "Exec RptGLoanOra @BRCD = '" + BrCode + "', @SubGlCode = '" + PrCode + "', @AccNo = '" + AccCode + "', @CustNo = '" + CustNo + "', @GLNSrNo = '" + SrNo + "', @GLNParti = '" + Part + "', @Qty = '" + Qty + "', @NetWt = '" + NetWt + "', @Rate = '" + Rate + "', @Price = '" + Price + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "',  @Flag='AD',@VALUERNAME='" + valuer + "',@ORNAMENTSKEPT='" + ornament + "',@REMARK='" + remark + "',@SANSSIONDATE='" + conn.ConvertDate(sanssion).ToString() + "', @GrossWt='" + GrossWt + "',@RecNo='" + RecNo + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int ModifyData(string BrCode, string PrCode, string AccCode, string CustNo, string SrNo, string Part, string Qty, double NetWt, double Rate, double Price, string EDate, string Mid, string valuer, string ornament, string remark, string sanssion, string GrossWt, string RecNo)
    {
        Result = 0;

        try
        {
            sql = "Exec RptGLoanOra @BRCD = '" + BrCode + "', @SubGlCode = '" + PrCode + "', @AccNo = '" + AccCode + "', @CustNo = '" + CustNo + "', @GLNSrNo = '" + SrNo + "', @GLNParti = '" + Part + "', @Qty = '" + Qty + "', @NetWt = '" + NetWt + "', @Rate = '" + Rate + "', @Price = '" + Price + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "',  @Flag='MD',@VALUERNAME='" + valuer + "',@ORNAMENTSKEPT='" + ornament + "',@REMARK='" + remark + "',@SANSSIONDATE='" + conn.ConvertDate(sanssion).ToString() + "', @GrossWt='" + GrossWt + "',@RecNo='" + RecNo + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int CheckMid(string BRCD, string PrCode, string AccNo)
    {
        try
        {
            sql = "SELECT MID FROM AVS_GLDORATable WHERE BRCD = '" + BRCD + "' AND SubGlCode = '" + PrCode + "' AND AccNo = '" + AccNo + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result = 0;
        }
        return Result;
    }

    public int AuthoriseData(string BrCode, string PrCode, string AccCode, string Mid)
    {
        try
        {
            sql = "Exec RptGLoanOra @BRCD = '" + BrCode + "', @SubGlCode = '" + PrCode + "', @AccNo = '" + AccCode + "', @MID = '" + Mid + "', @Flag='AT'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteData(string BrCode, string PrCode, string AccCode, string Mid)
    {
        try
        {
            sql = "Exec RptGLoanOra @BRCD = '" + BrCode + "', @SubGlCode = '" + PrCode + "', @AccNo = '" + AccCode + "', @MID = '" + Mid + "', @Flag='DL'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string CheckGlGroup(string BrCode, string ProdCode)
    {
        string RC = "";
        try
        {
            string sql = "Select IsNull(LOANCATEGORY, '') From LoanGl Where BrCd = '" + BrCode + "' And LoanGlCode = '" + ProdCode + "' And LoanCategory in ('LAG','CCOD')";
            RC = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RC;
        }
        return RC;
    }
    public DataTable GetGoldRept(string FBRCD, string TBRCD, string FPRCD, string FDT, string TDT, string EDT,string fl)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec Isp_AVS0041 @FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@PrdCode='" + FPRCD + "' ,@FDT='" + conn.ConvertDate(FDT) + "',@TDT='" + conn.ConvertDate(TDT) + "',@EDATE='" + conn.ConvertDate(EDT) + "',@FL='"+fl+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return DT = null;
        }
        return DT;
    }
    public DataTable GetGold5085(string FBRCD, string TBRCD, string FPRCD, string FDT, string TDT, string EDT,string CURRATE)////Added by ankita on 16/02/2018 gold loan valuation list
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec Isp_AVS0124 @FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@PrdCode='" + FPRCD + "' ,@FDT='" + conn.ConvertDate(FDT) + "',@TDT='" + conn.ConvertDate(TDT) + "',@EDATE='" + conn.ConvertDate(EDT) + "',@CurRate='"+CURRATE+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return DT = null;
        }
        return DT;
    }

    public string GetGlSrno(string Desc)
    {
        string RC = "";
        try
        {
            string sql = "Select SRNO from Lookupform1 where description='"+Desc+"' and LNO=1053";
            RC = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RC;
        }
        return RC;
    }
}