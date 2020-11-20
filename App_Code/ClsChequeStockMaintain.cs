using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsChequeStockMaintain
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsChequeStockMaintain()
	{
		

	}

    public string GetAccType(string AccT, string BRCD)
    {
        sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE='" + AccT + "'  AND BRCD='" + BRCD + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }

    public string GetStage(string BRCD, string ProdCode, string id)
    {
        try
        {
            sql = "SELECT STAGE FROM AVS_INSTRUMENTSTOCK WHERE BRCD = '" + BRCD + "' AND SUBGLCODE = '" + ProdCode + "' AND ID = '" + id + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }

    public int BindGrid(GridView Gview, string BRCD)    
    {        
        try
        {
            sql = "SELECT id, SUBGLCODE, SUBGLNAME, NOOFLEAVES, EFFECTDATE,STAGE FROM AVS_INSTRUMENTSTOCK WHERE BRCD = '" + BRCD + "' AND STAGE NOT IN ('1003','1004')";
        Result = conn.sBindGrid(Gview, sql);
        }
        catch(Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetInfo(string BRCD, string ID)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select * from AVS_INSTRUMENTSTOCK where brcd='" + BRCD + "' and id='" + ID + "'";            
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public int ChequeStockInst(string brcd, string PrCode, string PrName, string NoLeaves, string mid, string pcname, string EDate)
    {
        int RM; 
        try
        {
            sql = "Exec SP_INSTRUMENTSTOCK @BRCD='" + brcd + "',@PCODE='" + PrCode + "',@PNAME='" + PrName + "',@NOLEAVES='" + NoLeaves + "', @EDATE = '" + conn.ConvertDate(EDate).ToString() + "',@MID='" + mid + "',@PCMAC='" + pcname + "',@Type = 'INSRT'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int ChequeStockAth(string id, string brcd, string mid)
    {
        int RM;
        try
        {
            sql = "Exec SP_INSTRUMENTSTOCK @BRCD='" + brcd + "',@MID='" + mid + "',@ID='" + id + "', @Type = 'AUTHO'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int ChequeStockModfy(string id, string brcd, string PrCode, string PrName, string NoLeave)
    {
        int RM;
        try
        {
            sql = "Exec SP_INSTRUMENTSTOCK @BRCD='" + brcd + "',@PCODE='" + PrCode + "',@PNAME='" + PrName + "',@NOLEAVES='" + NoLeave + "', @ID='" + id + "', @Type = 'MODFY'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int ChequeStockDel(string id, string brcd)
    {
        int RM;
        try
        {
            sql = "Exec SP_INSTRUMENTSTOCK @BRCD='" + brcd + "',@ID='" + id + "', @Type = 'DEL'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
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
}