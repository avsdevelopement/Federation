using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public class ClsAgentCommision
{
    string name = "";
    string rtn = "";
    string sql = "", res = "";
    int rtnint = 0, result = 0;
    DbConnection conn = new DbConnection();
    DataTable dt = new DataTable();
    int Result, RM = 0;
    ClsEncryptValue Ecry = new ClsEncryptValue();
    string EntryMid, verifyMid, DeleteMid = "";
    string SCROLLNO, sResult, TableName = "";


    public ClsAgentCommision()
    {

    }

    public string GetAllProductName(int ProCode, string BRCD)
    {
        string ProductName = "";
        try
        {
            string sql = "select GLNAME from glmast where SUBGLCODE='" + ProCode + "' and BRCD='" + BRCD + "'";//BRCD added --Abhishek
            ProductName = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ProductName;
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

    public string GetAgNo(string AT, string BRCD)
    {
        try
        {
            sql = "SELECT G.GLNAME+'_'+convert(nvarchar(100),CONVERT(bigint, A.CUSTNO))D FROM GLMAST G INNER JOIN AGENTMAST A WITH(NOLOCK) ON A.AGENTCODE = G.SUBGLCODE AND A.BRCD = G.BRCD WHERE G.SUBGLCODE='" + AT + "' AND G.GLCODE in (2,15) AND G.BRCD='" + BRCD + "'";
            AT = conn.sExecuteScalar(sql);
            return AT;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public string GetTotCommission(string BRCD, string AccNo, string FDate, string TDate)
    {
        try
        {
            sql = "EXEC SP_AGENTCOMCAL '" + BRCD + "','" + AccNo + "','" + conn.ConvertDate(FDate) + "','" + conn.ConvertDate(TDate) + "'";
            // sql = "EXEC SP_AGENTCOMCAL @FLAG='COMMISION',@FDATE='" + conn.ConvertDate(FDate) + "',@TDATE='" + conn.ConvertDate(TDate) + "',@BRCD=" + BRCD + ",@ACCNO=" + AccNo + "";
            //sql = "SELECT SUM(AMOUNT) FROM AVSM_201609 WHERE BRCD='" + BRCD + "' AND ACCNO='" + AccNo + "' AND TRXTYPE = 1 AND ENTRYDATE BETWEEN '" + conn.ConvertDate(FDate).ToString() + "' AND '" + conn.ConvertDate(TDate).ToString() + "'";
            rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }

    public string AgentComm()
    {
        string RESULT = "";
        try
        {
            sql = "SELECT CHARGES FROM CHARGESMASTER WHERE CHARGESTYPE=1004";
            RESULT = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return RESULT;
    }
    public string AgentCommABOVE()
    {
        string RESULT = "";
        try
        {
            sql = "SELECT CHARGES FROM CHARGESMASTER WHERE CHARGESTYPE=1003";
            RESULT = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return RESULT;
    }


    public int AgentCommTds()
    {
        try
        {
            RM = Convert.ToInt32(conn.sExecuteScalar("SELECT CHARGES FROM CHARGESMASTER WHERE CHARGESTYPE=1007"));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RM;
    }
    public int AgentSecurity()
    {
        try
        {
            RM = Convert.ToInt32(conn.sExecuteScalar("SELECT CHARGES FROM CHARGESMASTER WHERE CHARGESTYPE=1005"));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RM;
    }
    public int AgentAMC()
    {
        try
        {
            RM = Convert.ToInt32(conn.sExecuteScalar("SELECT CHARGES FROM CHARGESMASTER WHERE CHARGESTYPE=1008"));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RM;
    }
    public string Agenttravelling()
    {
        string RESULT = "";
        try
        {

            sql = "SELECT CHARGES FROM CHARGESMASTER WHERE CHARGESTYPE=1006";
            RESULT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RESULT;
    }


    public DataTable GetPlAcc(string CHARGESTYPE)
    {
        try
        {
            dt = conn.GetDatatable("SELECT GLCODE,PLACC FROM CHARGESMASTER WHERE CHARGESTYPE='" + CHARGESTYPE + "'");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return dt = null;
        }
        return dt;
    }
    public string GetAGAccNo(string BRCD, string Subglcode,string GlCode)
    {
        string AgAccNo = "";
        try
        {
            sql = "select ACCNO from avs_acc where subglcode='"+GlCode+"' and brcd='"+BRCD+"' and custno=(select CUSTNO from avs_acc where subglcode=6 and accno='"+Subglcode+"' and brcd='"+BRCD+"')";
            AgAccNo = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return AgAccNo;
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
    public string GetPrno(string AT, string BRCD)
    {
        try
        {
            sql = "select isnull(INTACCYN,0) from GLMAST where brcd='" + BRCD + "' and SUBGLCODE='" + AT + "'";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }
    public int DeleteData(string brcd)
    {
        try
        {
            RM = 1;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RM;
    }

    public int BindGrid(GridView Gview, string BRCD, string Fdate, string Tdate, string FAcc, string TAcc)
    {
        try
        {
            sql = "EXEC SP_AGENTMULCUSTCOMM  @FDATE='" + conn.ConvertDate(Fdate) + "',@TDATE='" + conn.ConvertDate(Tdate) + "',@BRCD='" + BRCD + "',@FACCNO='" + FAcc + "',@TACCNO='" + TAcc + "'";
            //sql = "SELECT CONVERT(VARCHAR(10),SETNO)+'_'+CONVERT(VARCHAR(10),ENTRYDATE,121)SETNO,GLCODE,ACCNO,AMOUNT,PMTMODE,PARTICULARS FROM AVSM_201609 WHERE BRCD = '" + BRCD + "' AND GLCODE = 100 AND STAGE NOT IN('1004')";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DelData1(string BRCD, string ID)
    {
        DataTable DT = new DataTable();
        try
        {
            RM = conn.sExecuteQuery("UPDATE ALLVCR SET STAGE = 1004 WHERE BRCD = '" + BRCD + "' AND SETNO = '" + ID + "'");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RM = 0;
        }
        return RM;
    }

    public int DelData2(string BRCD, string ID, string date)
    {
        DataTable DT = new DataTable();
        string[] dt = date.ToString().Split('-');
        try
        {
            string month = dt[1].ToString();
            string year = dt[0].ToString();

            RM = conn.sExecuteQuery("UPDATE AVSM_" + year + "" + month + " SET STAGE = 1004 WHERE BRCD = '" + BRCD + "' AND ENTRYDATE = '" + date + "' AND SETNO = '" + ID + "'");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RM = 0;
        }
        return RM;
    }
    public string GETDATEDIFF(string MDATE, string EDATE)
    {
        sql = "SELECT DATEDIFF(day,'" + conn.ConvertDate(MDATE) + "',(SELECT DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, '" + conn.ConvertDate(EDATE) + "') + 1, 0)))) AS DiffDate";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string DATENAME(string MDATE)
    {
        sql = "SELECT DATENAME(dw,'" + conn.ConvertDate(MDATE) + "') ";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
   
    public DataTable CHECHDATA(string BRCD, string FROMDATE, string TODATE, string SUBGLCODE)
    {
        DataTable DT = new DataTable();
        //  string[] dt = date.ToString().Split('-');
        try
        {
            // string month = dt[1].ToString();
            //  string year = dt[0].ToString();

            DT = conn.GetDatatable("SELECT * FROM AVS_AGENTCOMIOSSION WHERE FROMDATE='" + conn.ConvertDate(FROMDATE) + "' AND TODATWE='" + conn.ConvertDate(TODATE) + "' AND BRCD='" + BRCD + "' AND SUBGLCODE='" + SUBGLCODE + "'");

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return DT;
    }
    public string CHECHDATA1(string BRCD, string FROMDATE, string TODATE, string SUBGLCODE)
    {
        string result = "";
        try
        {
            sql = "SELECT COUNT(*) FROM AVS_AGENTCOMIOSSION WHERE FROMDATE >='" + conn.ConvertDate(FROMDATE) + "' AND TODATWE<='" + conn.ConvertDate(TODATE) + "' And Status<>1 AND BRCD='" + BRCD + "' AND agentcode='" + SUBGLCODE + "' AND STAGE<>1004";
            result = conn.sExecuteScalar(sql);
            return result;
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public DataTable getyspmdata(string FL, string BRCD, string AGENTCODE1, string AGENTCODE2, string FDATE, string TDATE)
    {
        DataTable DT = new DataTable();
        try
        {

            sql = "EXEC SP_AGENT_MULTI_COMMISION111 '" + FL + "','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "','" + BRCD + "','" + AGENTCODE1 + "','" + AGENTCODE2 + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;

    }
    public DataTable GetRdCommision(string BRCD, string AGENTCODE1, string AGENTCODE2, string FDATE, string TDATE)
    {
        DataTable DT = new DataTable();
        try
        {

            sql = "EXEC SP_RDCOMMISSION '" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "','" + BRCD + "','" + AGENTCODE1 + "','" + AGENTCODE2 + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;

    }

   

    public DataTable GetChgInfo(string ChargesNo, string Charges, string PlAcc, string Glcode, string MinMonth)//Dhanya Shetty//To display chargesmaster in text box//
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "select CHARGES,PLACC,GLCODE,MINMONTH,MINIBAL  from CHARGESMASTER  WHERE CHARGESTYPE='" + ChargesNo + "'";

            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable getmarathwadadata(string FL, string BRCD, string AGENTCODE1, string AGENTCODE2, string FDATE, string TDATE)
    {
        DataTable DT = new DataTable();
        try
        {

            sql = "SP_MAR_CAL_COMISION '" + FL + "','" + BRCD + "','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "','" + AGENTCODE1 + "','" + AGENTCODE2 + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;

    }
    public DataTable GEtCommissionForGL(string BRCD, string SUBGLCODE,string AGENTCODE1, string AGENTCODE2, string FDATE, string TDATE)
    {
        DataTable DT = new DataTable();
        try
        {

            sql = " EXEC Isp_AVS0067 '"+BRCD+"','"+SUBGLCODE+"','"+conn.ConvertDate(FDATE)+"','"+conn.ConvertDate(TDATE)+"','"+AGENTCODE1+"','"+AGENTCODE2+"'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;

    }
    public string checkcondition(string BRCD, string FROMDATE, string TODATE, string SUBGLCODE)
    {
        string result = "";
        try
        {
            sql = "SP_CHECK_CONDITION '" + BRCD + "','" + conn.ConvertDate(FROMDATE) + "','" + conn.ConvertDate(TODATE) + "','" + SUBGLCODE + "'";
            result = conn.sExecuteScalar(sql);
            return result;
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
   
    public string CheckCharge(string ChargesNo)
    {
        try
        {
            sql = "Select count(CHARGESTYPE) from chargesmaster where CHARGESTYPE='" + ChargesNo + "'";
            res = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return res;
    }
    public string Getglcodec(string subgl, string brcd)
    {
        try
        {
            sql = "select glcode from glmast where subglcode='" + subgl + "' and brcd='" + brcd + "'";
            res = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return res;
    }

    public DataTable GetLabel()
    {
        DataTable dt=new DataTable ();
        try
        {
            sql = "exec Isp_AVS0075";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
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
  
}
