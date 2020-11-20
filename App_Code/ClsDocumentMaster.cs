using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public class ClsDocumentMaster
{
    string sql = "";
    int Result = 0;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    public ClsDocumentMaster()
    {
    }
    public DataTable GetKycDetail(string BRCD,string KTP)
    {
        try
        {
            sql = "Exec SP_KYCDETAIL @BRCD='" + BRCD + "',@KTP='"+KTP+"'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

}