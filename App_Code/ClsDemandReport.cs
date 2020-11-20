using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

/// <summary>
/// Summary description for ClsDemandReport
/// </summary>
public class ClsDemandReport
{
    DataTable DT = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";
    public int Res = 0;
    public string Sql = "";
    public string FL { get; set; }
    public string FBRCD { get; set; }
    public string TBRCD { get; set; }
    public string RECCODE { get; set; }
    public string RECDIV { get; set; }
    public string ASONDT { get; set; }
    public string MM { get; set; }
    public string YY { get; set; }

    public DataTable Fn_BLGetDemandRep(ClsDemandReport RS)
    {
        try
        {

            sql = "Exec Isp_DemandReport @Flag='" + RS.FL + "',@FBrcd='" + RS.FBRCD + "',@TBrcd='" + RS.TBRCD + "',@Edt='" + Conn.ConvertDate(RS.ASONDT) + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "'";
            RS.DT = Conn.GetDatatable(RS.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RS.DT;
    }
}