using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

/// <summary>
/// Summary description for ClsLrAndNr
/// </summary>
public class ClsLrAndNr
{

    DataTable DT = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";
    public int Res = 0;
    public string Sql = "";
    public string FL { get; set; }
    public string BRCD { get; set; }
    public string RECCODE { get; set; }
    public string RECDIV { get; set; }
    public string ASONDT { get; set; }
    public string MM { get; set; }
    public string YY { get; set; }


    public DataTable Fn_BLGetLNRNRReport(ClsLrAndNr RS)
    {
        try
        {
            //LRNR
            sql = "Exec Isp_LRandNR @Flag='" + RS.FL + "',@Brcd='" + RS.BRCD + "',@Edt='" + Conn.ConvertDate(RS.ASONDT) + "',@ForMM='" + RS.MM + "',@ForYY='" + RS.YY + "',@RecDiv='" + RS.RECDIV + "',@RecCode='" + RS.RECCODE + "'";
            RS.DT = Conn.GetDatatable(RS.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RS.DT;
    }
}