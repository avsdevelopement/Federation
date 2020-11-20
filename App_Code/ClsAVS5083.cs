using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5083
{
    ClsAuthorized AT = new ClsAuthorized();
    DbConnection conn = new DbConnection();
    ClsCommon cmn = new ClsCommon();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    int Result = 0;


	public ClsAVS5083()
	{
		
	}

    public string GetBranchName(string BrCode)
    {
        try
        {
            sql = "Select MidName From BankName Where BrCd = '" + BrCode + "' And BrCd <> 0";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public string Getaccno(string BRCD, string AT)
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

    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DataTable DT = new DataTable();
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

    public DataTable GetADMSubGl(string BrCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select ADMGlCode, ADMSubGlCode From BankName Where BrCd = '" + BrCode + "'";
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

    public int BindDividentGrid(GridView Gview, string BrCode, string AccNo, string EDate)
    {
        try
        {
            sql = "Exec RptShareDividentBalance @BrCode = '" + BrCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "'";
            Result = cmn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetProdCode(string BrCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select IsNull(OTHERS_GL2, 0) As Admin1, IsNull(OTHERS_GL5, 0) As Admin2, IsNull(OTHERS_GL3, 0) As SGST, IsNull(OTHERS_GL4, 0) As IGST "+
                  "From AVS_SHRPARA Where BRCD = '" + BrCode + "' And EntryDate = (Select Max(EntryDate) From AVS_SHRPARA Where BRCD = '" + BrCode + "' And Stage <> 1004)";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public string GetGlCode(string BrCode, string ProdCode)
    {
        try
        {
            sql = "Select GLCODE from GLMAST where BRCD = '" + BrCode + "' and SUBGLCODE = '" + ProdCode + "'";
            ProdCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return ProdCode = "";
        }
        return ProdCode;
    }

    public int CloseShareAcc(string BrCode, string SubglCode, string AccNo, string EDate, string Mid)
    {
        try
        {
            sql = "Update Avs_Acc Set Acc_Status = '3', ClosingDate = '" + conn.ConvertDate(EDate).ToString() + "', Stage = '1003', VID = "+ Mid +", SystemDate = GetDate() " +
                  "Where BrCd = '" + BrCode + "' And SubGlCode = '" + SubglCode + "' And AccNo = '" + AccNo + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetAdminHead(string Flag)
    {
        try
        {
            sql = "Select ListValue From Parameter Where BrCd = '0' And ListField = '" + Flag + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

}