using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for ClsRecoveryMaster
/// </summary>
public class ClsRecoveryMaster
{
    DataTable DT = new DataTable();
    DbConnection Conn = new DbConnection();

    int Res = 0;
    public string STR = "", sql = "";

    public ClsRecoveryMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public GridView GD { get; set; }
    public string Flag { get; set; }
    public string SFlag { get; set; }
    public string Brcd { get; set; }
    public string Custno { get; set; }
    public string Edt { get; set; }
    public string Setno { get; set; }
    public string Mid { get; set; }
    public string Id { get; set; }
    public string Status { get; set; }
    public string MM { get; set; }
    public string YY { get; set; }

    public int FnBL_BindGrid(ClsRecoveryMaster RM)
    {
        try
        {
            sql = "Exec Isp_AVS0088 @Flag='" + RM.Flag + "',@Brcd='" + RM.Brcd + "',@Custno='" + RM.Custno + "'";
            Res = Conn.sBindGrid(GD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public int FnBL_UpdateData(ClsRecoveryMaster RM)
    {
        try
        {

            sql = "Exec Isp_AVS0088 @Flag='" + RM.Flag + "',@Status='" + RM.Status + "',@Brcd='" + RM.Brcd + "',@Edt='" + Conn.ConvertDate(RM.Edt) + "',@Mid='" + RM.Mid + "',@ForMM='" + RM.MM + "',@ForYY='" + RM.YY + "',@Custno='" + RM.Custno + "'";
            Res = Conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
}
