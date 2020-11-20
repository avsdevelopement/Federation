using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

/// <summary>
/// Summary description for ClsCancelPosting
/// </summary>
public class ClsCancelPosting
{
    DataTable DT = new DataTable();
    DbConnection Conn = new DbConnection();
    int Res = 0;
    public string STR = "", sql = "";

    public GridView GD { get; set; }
    public string Flag { get; set; }
    public string SFlag { get; set; }
    public string Brcd { get; set; }
    public string Paymast { get; set; }
    public string Edt { get; set; }
    public string Setno { get; set; }
    public string Mid { get; set; }


	public ClsCancelPosting()
	{
		
	}

    public string FnBl_GetData(ClsCancelPosting CP)
    {
        try
        {
            sql = "Exec Isp_AVS0086 @Flag = '" + CP.Flag + "', @Brcd = '" + CP.Brcd + "', @Edt = '" + CP.Conn.ConvertDate(Edt) + "', @Setno = '" + CP.Setno + "', @Mid = '" + CP.Mid + "'";
            STR = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return STR;
    }

    public int FnBl_BindData(ClsCancelPosting CP)
    {
        try
        {
            sql = "Exec Isp_AVS0086 @Flag = '" + CP.Flag + "', @Brcd = '" + CP.Brcd + "', @Edt = '" + CP.Conn.ConvertDate(Edt) + "', @Setno = '" + CP.Setno + "', @Mid = '" + CP.Mid + "'";
            Res = Conn.sBindGrid(GD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public int FnBl_CancelPosting(ClsCancelPosting CP)
    {
        try
        {
            sql = "Exec Isp_AVS0086 @Flag = '" + CP.Flag + "', @Brcd = '" + CP.Brcd + "', @Edt = '" + CP.Conn.ConvertDate(Edt) + "', @Setno = '" + CP.Setno + "', @Mid = '" + CP.Mid + "'";
            Res = Conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

}