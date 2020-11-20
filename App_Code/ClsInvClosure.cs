using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

/// <summary>
/// Summary description for ClsInvClosure
/// </summary>
public class ClsInvClosure
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "";
    int Result = 0;

	public ClsInvClosure()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetDetails(string BRCD, string PRD, string ACCNO)
    {
        try
        {
            sql = "Exec Invest_CloserData '" + BRCD + "','" + PRD + "','" + ACCNO + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetIntAC(string BRCD, string SBGL,string FL)
    {
        try
        {
            if (FL == "ACNO")
                sql = "SELECT CONVERT(VARCHAR(30),INT_CRPRD)+'_'+CONVERT(VARCHAR(30),INT_RECVPRD)+'_'+CONVERT(VARCHAR(30),TDS_PAYABLEAC ) FROM AVS_INVPARAMETER  WHERE STAGE=1003 AND STATUS=1";

            else if (FL == "GLNM")
                sql = "SELECT isnull(GLNAME,'N/A') FROM GLMAST WHERE SUBGLCODE='" + SBGL + "' AND BRCD='" + BRCD + "'";

            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return BRCD;
    }

    public string GetIntAC1(string BRCD, string SBGL, string AccNo)
    {
        try
        {
           // sql = "select isnull(CONVERT(VARCHAR(30),CRGL),'')+'_'+isnull(CONVERT(VARCHAR(30),RecGL),'')+'_'+(select CONVERT(VARCHAR(30),TDS_PAYABLEAC)+'_'+isnull(CONVERT(VARCHAR(30),RecAC),'') FROM AVS_INVPARAMETER  WHERE STAGE=1003 AND STATUS=1 ) from AVS_InvAccountMaster where subglcode='" + SBGL + "' and CustAccno='" + AccNo + "' and brcd='" + BRCD + "'";
            sql = "select isnull(CONVERT(VARCHAR(30),i.CRGL),'')+'_'+isnull(CONVERT(VARCHAR(30),i.RecGL),'')+'_'+(select CONVERT(VARCHAR(30),p.TDS_PAYABLEAC)+'_'+isnull(CONVERT(VARCHAR(30),i.RecAC),'') FROM AVS_INVPARAMETER p WHERE STAGE=1003 AND STATUS=1 ) from AVS_InvAccountMaster i where i.subglcode='" + SBGL + "' and i.CustAccno='" + AccNo + "' and i.brcd='" + BRCD + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return BRCD;
    }

    public DataTable GetInvDetails(string flag, string BRCD, string FDate, string TDate, string EDT, string SGL)
    {
        try
        {
            if (flag == "'0'" || flag=="0")
                sql = "exec SP_InvRegAll '" + conn.ConvertDate(FDate).ToString() + "','" + conn.ConvertDate(TDate).ToString() + "','" + BRCD + "','" + conn.ConvertDate(EDT).ToString() + "'";
            else
                sql = "exec SP_InvRegSPE '" + conn.ConvertDate(FDate).ToString() + "','" + conn.ConvertDate(TDate).ToString() + "','" + BRCD + "','" + conn.ConvertDate(EDT).ToString() + "','" + SGL + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable GetMATInvDetails(string flag, string BRCD, string FDate, string TDate, string EDT, string SGL)
    {
        try
        {
            if (flag == "'0'" || flag == "0")
                sql = "exec SP_MATINVREGALL '"+conn.ConvertDate(FDate)+"','"+conn.ConvertDate(TDate)+"', '"+BRCD+"','"+conn.ConvertDate(EDT)+"'";
            else
                sql = "SP_MAT_INVREG '"+conn.ConvertDate(FDate)+"','"+conn.ConvertDate(TDate)+"','"+BRCD+"','"+conn.ConvertDate(EDT)+"','"+SGL+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable GetMaturity(string BRCD, string FDAT,string TDAT)
    {
        try
        {
            sql = "exec ISP_AVS0030 '" + BRCD + "','" + conn.ConvertDate(FDAT) + "','" + conn.ConvertDate(TDAT) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable GetINVReg(string flag, string BRCD, string EDT, string SGL, string FLT)
    {
        try
        {
            if (flag == "'0'" || flag == "0")
                sql = "exec SP_INV_REGALL '" + BRCD + "','" + conn.ConvertDate(EDT) + "'";
            else if (flag == "1" || flag == "'1'")
                sql = "exec SP_INV_REG '" + BRCD + "','" + conn.ConvertDate(EDT) + "','" + SGL + "'";
            else
                sql = "exec Isp_AVS0071 '" + BRCD + "','" + conn.ConvertDate(EDT) + "','" + FLT + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable GetINVIntReg(string BRCD, string FSUB, string TSUB, string EDAT)
    {
        try
        {
            sql = "exec Isp_AVS0084 '" + BRCD + "','" + conn.ConvertDate(EDAT) + "','" + FSUB + "','" + TSUB + "',1";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable BINDGRID( string BRCD,  string EDT, string SGL)
    {
        try
        {
            sql = "exec SP_INV_REG '" + BRCD + "','" + conn.ConvertDate(EDT) + "','" + SGL + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable BINDGRIDall(string BRCD, string EDT)
    {
        try
        {
            sql = "exec SP_INV_REGALL '"+BRCD+"','"+conn.ConvertDate(EDT)+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public DataTable GetDueData(string BRCD, string FDate, string TDate, string EDAT)//Dhanya Shetty
    {
        try
        {
        sql = "EXEC D_DueDateInvst '" + BRCD + "','" + conn.ConvertDate(FDate) + "','" + conn.ConvertDate(TDate) + "','" + conn.ConvertDate(EDAT) + "'";
        DT = conn.GetDatatable(sql);
        
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public DataTable GetStartData(string BRCD, string FDate, string TDate)//Dhanya Shetty
    {
        try
        {
            sql = "EXEC D_StartDateInvst'" + BRCD + "','" + conn.ConvertDate(FDate) + "','" + conn.ConvertDate(TDate) + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable GetBalList(string BRCD, string ASONDATE, string EDAT)////////Ankita Ghadage 31/05/2017 for investment balance list
    {
        try
        {
            sql = "EXEC A_INVBALLIST '" + BRCD + "','" + conn.ConvertDate(ASONDATE) + "','" + conn.ConvertDate(EDAT) + "'";
        DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable GetClosedList(string BRCD, string FDate, string TDate, string EDAT)////////Ankita Ghadage 01/06/2017 for Closed investment list
    {
        try
        {
            sql = "EXEC AN_CLOSEDINVLIST '" + BRCD + "','" + conn.ConvertDate(FDate) + "','" + conn.ConvertDate(TDate) + "','" + conn.ConvertDate(EDAT) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable bindMatureInv(string BRCD, string FDate, string TDate)
    {
        try
        {
            sql = "exec ISP_AVS0030 '"+BRCD+"','"+conn.ConvertDate(FDate)+"','"+conn.ConvertDate(TDate)+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public string GetFlag(string BRCD, string ProdCode, string Accno)
    {
        string AC = "";
        try
        {
            sql = "exec Isp_AVS0082 '"+BRCD+"','"+ProdCode+"','"+Accno+"',1";
            AC = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return AC;
    }
    public string GetAccno(string Prd, string Accno)
    {
        string accno = "";
        try
        {
            sql = "exec Isp_AVS0082 1,'"+Prd+"','"+Accno+"',2";
            accno=conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return accno;
    }

    public void UpdateLastIntDate(string ProdCode, string Accno, string Date)
    {
        try
        {
            sql = "update avs_invdepositemaster set LastIntDate='"+conn.ConvertDate(Date)+"' where subglcode='"+ProdCode+"' and custaccno='"+Accno+"'";
            conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public string GetRecGL(string BRCD, string Prd, string AccT)
    {
        try
        {
            sql = "SELECT CONVERT(VARCHAR(10),RecGL) FROM Avs_InvAccountmaster WHERE BRCD='" + BRCD + "' And SUBGLCODE='" + Prd + "'  AND CustAccno='" + AccT + "' ";
            AccT = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return AccT;
    }
    public string GetRecAC(string BRCD, string Prd, string AccT)
    {
        try
        {
            sql = "SELECT CONVERT(VARCHAR(10),RecAC) FROM Avs_InvAccountmaster WHERE BRCD='" + BRCD + "' And SUBGLCODE='" + Prd + "'  AND CustAccno='" + AccT + "' ";
            AccT = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return AccT;
    }
}
