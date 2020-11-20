using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5075
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    int Result = 0;

	public ClsAVS5075()
	{
		
	}

    public string GetCustName(string BrCode, string CustNo)
    {
        try
        {
            sql = "Select (CustName +'_'+ ConVert(VarChar(10), ConVert(BigInt, IsNull(CustNo, 0))) +'_'+ ConVert(VarChar(10), ConVert(BigInt, IsNull(Group_CustNo, 0)))) CustName From Master Where CustNo = '" + CustNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int ChangeCustomer(string BrCode, string OldCustNo, string NewCustNo)
    {
        try
        {
            sql = "Exec ISP_AVS0112 @BrCode = '" + BrCode + "', @OldCustNo = '" + OldCustNo + "', @NewCustNo = '" + NewCustNo + "' ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int GETAccData(GridView grd, string custno)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select BRCD,GLCODE,SUBGLCODE,ACCNO,convert(varchar(10),OPENINGDATE,103)OPENINGDATE from avs_acc where custno='" + custno + "' order by brcd";
            Result = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
}