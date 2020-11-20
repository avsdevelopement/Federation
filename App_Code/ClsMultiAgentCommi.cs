using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for ClsMultiAgentCommi
/// </summary>
public class ClsMultiAgentCommi
{
    DbConnection conn = new DbConnection();
    string sql = "";
    string Result = "";
    DataTable DT = new DataTable();  
	public ClsMultiAgentCommi()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetMultiAgent(string Fdate,string TDate,string Brcd,string Ref_Agent,string Subglcode)
    {
        DataTable DT = new DataTable();
        try
        {

            sql = "EXEC Isp_AVS0076 '"+conn.ConvertDate(Fdate)+"','"+conn.ConvertDate(TDate)+"','"+Brcd+"','"+Ref_Agent+"','"+Subglcode+"'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;

    }
    public string AgentComm()
    {
        string RESULT = "";
        try
        {
            sql = "SELECT CHARGES FROM CHARGESMASTER WHERE CHARGESTYPE=1011";
            RESULT = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return RESULT;
    }
    public string AgentCommRD()
    {
        string RESULT = "";
        try
        {
            sql = "SELECT CHARGES FROM CHARGESMASTER WHERE CHARGESTYPE=1012";
            RESULT = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return RESULT;
    }
    public string AgentCommDaily()
    {
        string RESULT = "";
        try
        {
            sql = "SELECT CHARGES FROM CHARGESMASTER WHERE CHARGESTYPE=1013";
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
            DT = conn.GetDatatable("SELECT GLCODE,PLACC FROM CHARGESMASTER WHERE CHARGESTYPE='" + CHARGESTYPE + "'");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return DT = null;
        }
        return DT;
    }
   
    public DataTable GetPostData(string fdate, string tdate, string brcd)
    {
        DataTable DT = new DataTable();
        try
        {

            //sql = "EXEC Isp_AVS0077 '" + prcd + "','" + Ref_agent + "','" + brcd + "','" + conn.ConvertDate(fdate) + "','" + conn.ConvertDate(tdate) + "'";
            sql = "EXEC Isp_AVS0079 '','','" + conn.ConvertDate(fdate) + "','" + conn.ConvertDate(tdate) + "','"+brcd+"'";
            DT = conn.GetDatatable(sql);
           

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;

    }
}