using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class ClsAgentCreation
{
    DbConnection conn = new DbConnection();
    string sql = "";
    string sql1 = "";
    int Result = 0;

    public ClsAgentCreation()
    {

    }

    public int insert_agentcreation(string BRCD, string AgentCode, string Custno, string CusName, string MID, string Edate, string Pass, string Mobile)
    {
        try
        {
            sql = " Exec SP_AGENTCREATE @BRCD = '" + BRCD + "', @ACODE = '" + AgentCode + "', @CNO = '" + Custno + "', @CNAME = '" + CusName + "', @EDATE = '" + conn.ConvertDate(Edate).ToString() + "', @MID = '" + MID + "', @PNAME = '" + conn.PCNAME().ToString() + "',@PASS='" + Pass + "',@MOBILE='" + Mobile + "', @TYPE = 'AD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return Result;
    }
    public int insertBalVikas(string flag, string BRCD, string AgentCode, string Custno, string CusName, string Pass, string Mobile)
    {
        try
        {
            sql = " Exec Isp_AVS0069 @FLAG='" + flag + "',@BRCD='" + BRCD + "',@CUSTNO='" + Custno + "',@SUBGLCODE='" + AgentCode + "',@AGENTNAME='" + CusName + "',@MOBILENO='" + Mobile + "',@PASSWARD='" + Pass + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return Result;
    }

    public int Modify_agentcreation(string brcd, string AgentCode, string Custno, string AgentName, string Pass, string Mobile)
    {
        try
        {
            sql = "SP_AGENTCREATE @BRCD = '" + brcd + "', @ACODE = '" + AgentCode + "', @CNO = '" + Custno + "', @CNAME = '" + AgentName + "', @PNAME = '" + conn.PCNAME().ToString() + "',@PASS='" + Pass + "',@MOBILE='" + Mobile + "', @TYPE = 'MD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int ModifyBalVikas(string flag, string BRCD, string AgentCode, string Custno, string AgentName, string Pass, string Mobile)
    {
        try
        {
            sql = " Exec Isp_AVS0069 @FLAG='" + flag + "',@BRCD='" + BRCD + "',@CUSTNO='" + Custno + "',@SUBGLCODE='" + AgentCode + "',@AGENTNAME='" + AgentName + "',@MOBILENO='" + Mobile + "',@PASSWARD='" + Pass + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int Delete_agentcreation(string BRCD, string agentcode, string Custno, string MID)
    {
        try
        {
            sql = "SP_AGENTCREATE @BRCD = '" + BRCD + "', @ACODE = '" + agentcode + "', @CNO = '" + Custno + "', @PNAME = '" + conn.PCNAME().ToString() + "', @TYPE = 'DL' , @MID='" + MID + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int DeleteBalVikas(string flag, string BRCD, string agentcode, string Custno, string custname)
    {
        try
        {
            sql = "Exec Isp_AVS0069 @FLAG='" + flag + "',@BRCD='" + BRCD + "',@CUSTNO='" + Custno + "',@SUBGLCODE='" + agentcode + "',@AGENTNAME='" + custname + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int Authorize_agentcreation(string BRCD, string agentcode, string Custno, string MID)
    {
        try
        {
            sql = "SP_AGENTCREATE @BRCD = '" + BRCD + "', @ACODE = '" + agentcode + "', @CNO = '" + Custno + "', @PNAME = '" + conn.PCNAME().ToString() + "', @TYPE = 'AT', @MID='" + MID + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int AuthorizeBalvikas(string flag, string BRCD, string agentcode, string Custno)
    {
        try
        {
            sql = "Exec Isp_AVS0069 @FLAG='" + flag + "',@BRCD='" + BRCD + "',@CUSTNO='" + Custno + "',@SUBGLCODE='" + agentcode + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetStage(string CNO, string BRCD)
    {
        sql = "SELECT STAGE FROM MASTER WHERE CUSTNO='" + CNO + "' AND BRCD='" + BRCD + "' AND STAGE <>1004";
        string STAGE = conn.sExecuteScalar(sql);
        return STAGE;
    }

    public string GetStage1(string CNO, string ACODE, string BRCD)
    {
        sql = "SELECT STAGE FROM AGENTMAST WHERE CUSTNO = '" + CNO + "' AND AGENTCODE = '" + ACODE + "' AND BRCD = '" + BRCD + "' AND STAGE <> 1004";
        string STAGE = conn.sExecuteScalar(sql);
        return STAGE;
    }

    public string Getcustname(string custno, string BRCD)
    {
        try
        {
            sql = "SELECT (CUSTNAME+'_'+Convert(VARCHAR(10),CUSTNO)) CUSTNAME FROM MASTER WHERE CUSTNO='" + custno + "' "; //and brcd='" + BRCD + "' FOR UNIFICATION 
            custno = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return custno;
    }
    public DataTable showdata(string agentcode, string brcd)
    {
        sql = "SELECT agentcode,agentname,custno,M_NUMBER,APASSWORD FROM AGENTMAST WHERE BRCD='" + brcd + "' AND AGENTCODE='" + agentcode + "'";
        DataTable dt = new DataTable();
        dt = conn.GetDatatable(sql);
        return dt;

    }
    public int CheckAgent(string brcd, string agntno)
    {
        try
        {
            sql = "select isnull(count(*),0) from AGENTMAST where AGENTCODE='" + agntno + "' and brcd='" + brcd + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable GetCustNAme(string Custno, string brcd)
    {
        DataTable dt = new DataTable();
        sql = "select custname,convert(varchar(10),OPENINGDATE,103) as OPENINGDATE from master where brcd='" + brcd + "' and custno='" + Custno + "'";
        dt = conn.GetDatatable(sql);
        return dt;
    }
    public DataTable GetAgentDetails(string agentcode, string brcd)
    {
        DataTable dt = new DataTable();
        sql = "select custno,agentname,m_number,Apassword from agentmast where brcd='" + brcd + "' and agentcode<1200 and agentcode='" + agentcode + "'";
        dt = conn.GetDatatable(sql);
        return dt;
    }
    public DataTable CheckaccExit(string agentcode, string brcd)
    {
        DataTable dt = new DataTable();
        sql = "select * from avs_acc where brcd='" + brcd + "' and subglcode='" + agentcode + "' and acc_status=1";
        dt = conn.GetDatatable(sql);
        return dt;
    }
    public void InsertAgentData(DataTable dt)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(conn.DbNameMob1()))
            {
                con.Open();
                SqlBulkCopy sqlBulk = new SqlBulkCopy(conn.DbNameMob1());
                //Give your Destination table name
                sqlBulk.DestinationTableName = "AgentMaster";
                sqlBulk.WriteToServer(dt);
                con.Close();

            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }

    }
    public DataTable getagentdata(string agcd, string brcd)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select CUSTNO,AGENTCODE,AGENTNAME,a.BRCD,STAGE,SYSTEMDATE,MID,CID,VID,PCMAC,APassword,M_NUMBER,BANKCD,BANKNAME,MIDNAME from AGENTMAST a inner join BANKNAME b on b.BRCD=a.BRCD where AGENTCODE='" + agcd + "' and a.BRCD='" + brcd + "' and a.STAGE<>1004";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;

    }

    public int DelagentMob(string agcd, string brcd,string bankcd)
    { 
        
        try
        {
            sql = "delete from agentmaster where AGENTCODE='" + agcd + "' and BRCD='" + brcd + "' and bankcd='" + bankcd + "' and STAGE<>1004";
            Result = conn.sExecuteQueryMob(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;

    }

}