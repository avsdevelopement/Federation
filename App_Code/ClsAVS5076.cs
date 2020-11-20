using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5076
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    int Result = 0;

	public ClsAVS5076()
	{
		
	}

  
    public string GetProductName(string BrCode, string ProdCode)
    {
        try
        {
            sql = "Select (IsNull(GlName, '') +'_'+ ConVert(VarChar(10), GlCode) +'_'+ ConVert(VarChar(10), SubGlCode)) As Name " +
                  "From GlMast Where BrCd = '" + BrCode + "' And SubGlCode ='" + ProdCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetAccountName(string BrCode, string ProdCode, string AccNo)
    {
        try
        {
            sql = "Select (M.CustName+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.AccNo))+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.CustNo))) As Name " +
                  "From Master M With(NoLock) " +
                  "Inner Join Avs_Acc A With(NoLock) On A.CustNo = M.CustNo " +
                  "Where A.BrCd = '" + BrCode + "' And A.SubGlCode = '" + ProdCode + "' And A.AccNo = '" + AccNo + "' And A.Stage <> '1004'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

   
}