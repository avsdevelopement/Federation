using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsAVS5054
/// </summary>
public class ClsAVS5054
{
    DbConnection conn = new DbConnection();
    string para = "";
    string sql = "";
	public ClsAVS5054()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GETSMSPARA(string BRCD)
    {
        try
        {
            para = "select listvalue from parameter where listfield='SMS_YN' AND BRCD='" + BRCD + "'";
            para = conn.sExecuteScalar(para);
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
        return para;
    }
    public DataTable GetMobileNo(string BRCD, string Flag)
    {
        DataTable DT = new DataTable();
        try
        {
            if (Flag == "0")
                sql = "SELECT DISTINCT MOBILE1 FROM AVS_ACC A LEFT JOIN Avs_Contactd C ON A.CUSTNO=C.Custno WHERE A.GLCODE=4 AND a.BRCD='" + BRCD + "' AND ACC_STATUS<>'3' AND A.STAGE<>1004 AND C.STAGE<>1004 AND MOBILE1 IS NOT NULL AND MOBILE1<>'0' AND LEN(MOBILE1)=10";
            else
                sql = "SELECT DISTINCT MOBILE1 FROM AVS_CONTACTD WHERE STAGE<>1004 AND MOBILE1 IS NOT NULL AND MOBILE1<>'0' AND LEN(MOBILE1)=10";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public int InsertRecord(string BRCD,string MSG,string Entrydate,string Mobile,string Flag,string Date,string Type)
    {
        int Result1 = 0;
        try
        {
            if(Flag=="0")
                sql = "Insert Into AVS1092_BULK (MOBILE,SMS_DATE,SMS_DESCRIPTION,BRCD,SMS_Status,SMS_TYPE,SEND_DATE) " +
                " VALUES('" + Mobile + "','" + conn.ConvertDate(Entrydate) + "','" + MSG + "','" + BRCD + "','1','" + Type + "','" + conn.ConvertDate(Date) + "')";
            else
                sql = "Insert Into AVS1092_BULK (MOBILE,SMS_DATE,SMS_DESCRIPTION,BRCD,SMS_Status,SMS_TYPE,SEND_DATE) " +
                " VALUES('" + Mobile + "','" + conn.ConvertDate(Entrydate) + "',N'" + MSG + "','" + BRCD + "','1','" + Type + "','" + conn.ConvertDate(Date) + "')";
            Result1 = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result1;
    }
}