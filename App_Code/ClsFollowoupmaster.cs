using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsFollowoupmaster
/// </summary>
public class ClsFollowoupmaster
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
	public ClsFollowoupmaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int insertdata(string brcd, string CASE_YEAR, string CASENO, string srno, string file_status, string fdate, string f_remarks, string nextdate, string mid, string edate,string MemNo)
    {
        sql = "INSERT INTO AVS_2002(ENTRYDATE,BRCD,CASE_YEAR,CASENO,SRNO,FILE_STATUS,F_DATE,F_REEAMRKS,NEXT_F_DT,STAGE,MID,MEMBERNO) VALUES('" + conn.ConvertDate(edate) + "','" + brcd + "','" + CASE_YEAR + "','" + CASENO + "','" + srno + "','" + file_status + "','" + conn.ConvertDate(fdate) + "','" + f_remarks + "','" + conn.ConvertDate(nextdate) + "','1001','" + mid + "','" + MemNo + "')";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
}