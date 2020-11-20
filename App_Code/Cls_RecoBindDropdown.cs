using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

/// <summary>
/// Summary description for Cls_RecoBindDropdown
/// </summary>
public class Cls_RecoBindDropdown
{
    DbConnection Conn = new DbConnection();
    public string sql = "";
    public int IntRes = 0;
    public string BRCD { get; set; }
    public int Result { get; set; }
    public DropDownList Ddl { get; set; }
    public string RECCODE { get; set; }
    public string RECDIV { get; set; }

	public Cls_RecoBindDropdown()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    
    public int FnBL_BindDropDown(Cls_RecoBindDropdown BD)
    {
        try
        {
            BD.sql = "SELECT Convert(varchar(100),BRCD)+'-'+Convert(varchar(100),MIDNAME) name,BRCD id from BANKNAME WHERE BRCD<>0 ORDER BY BRCD";
            Conn.FillDDL(BD.Ddl, BD.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BD.IntRes;
    }
    public int FnBL_BindRecDept(Cls_RecoBindDropdown BD)
    {
        try
        {
            // BD.sql = "Select Convert(varchar(100),RECCODE)+'-'+Convert(varchar(100),DESCR) name,RECCODE id from PAYMAST " +
            //   " where RECDIV='" + BD.RECDIV + "' and BRCD='" + BD.BRCD + "' Order by RECCODE";
            BD.sql = "Select Convert(varchar(100),DESCR)+'-'+Convert(varchar(100),RECCODE) name,RECCODE id from PAYMAST " +
                   " where RECDIV='" + BD.RECDIV + "' and BRCD='" + BD.BRCD + "' Order by RECCODE";
            Conn.FillDDL(BD.Ddl, BD.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BD.IntRes;
    }
    public int FnBL_BindRecDiv(Cls_RecoBindDropdown BD)
    {
        try
        {
            BD.sql = "Select Convert(varchar(100),RECDIV)+'-'+Convert(varchar(100),DESCR) name,RECDIV id from PAYMAST where RECCODE=0 Order by RECCODE";
            Conn.FillDDL(BD.Ddl, BD.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BD.IntRes;
    }

    public int FnBL_BindRecCustDiv(Cls_RecoBindDropdown BD, string CustNo)
    {
        try
        {
            BD.sql = "Select Convert(varchar(100),A.RECDIV)+'-'+Convert(varchar(100),A.DESCR) name,A.RECDIV id from PAYMAST A Inner join Master M On A.RECDIV = M.BRANCHNAME Where A.RECCODE=0 And M.CustNo = '" + CustNo + "' Order by A.RECCODE";
            Conn.FillDDL(BD.Ddl, BD.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BD.IntRes;
    }
    public int FnBL_BindRecCustDept(Cls_RecoBindDropdown BD, string CustNo)
    {
        try
        {
            BD.sql = "Select Convert(varchar(100),A.DESCR)+'-'+Convert(varchar(100),A.RECCODE) name,A.RECCODE id from PAYMAST A" +
                   "  Inner join Master M On A.RECDIV = M.BRANCHNAME And A.RECCODE = M.RECDEPT Where A.RECDIV='" + BD.RECDIV + "' and A.BRCD='" + BD.BRCD + "' And M.CustNo = '" + CustNo + "' Order by A.RECCODE";
            Conn.FillDDL(BD.Ddl, BD.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BD.IntRes;
    }
}