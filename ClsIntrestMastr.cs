using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

public class ClsIntrestMastr
{
    int rtn;
    string sql;
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
    int result = 0;

    public ClsIntrestMastr()
    {

    }

    // Interest Master Entry

    public int EntryInterest(string DEPOSIT_TYPE, string TDCUSTTYPE, string DEPOSITGL, string PERIODTYPE, string PERIODFROM, string PERIODTO, string RATE, string PENALTY, string STAGE,
        string BRCD, string MID, string PCMAC, string EFFECTDATE, string InstAmt, string MatAmt, string ADMIN, string AfterMaturity)
    {
        try
        {

            sql = " INSERT INTO A50001 (SRNO,DEPOSIT_TYPE, TDCUSTTYPE, DEPOSITGL, PERIODTYPE, PERIODFROM, PERIODTO, RATE, PENALTY, STAGE, BRCD, MID, PCMAC, EFFECTDATE,INSTAMT,MATAMT,ADMIN_CHG,AFTERMATROI) " +
              " VALUES ((select Isnull((max(SRNO)+1),0)FROM A50001), '" + DEPOSIT_TYPE + "', '" + TDCUSTTYPE + "', '" + DEPOSITGL + "', '" + PERIODTYPE + "', '" + PERIODFROM + "', '" + PERIODTO + "', " +
              "'" + RATE + "', '" + PENALTY + "', '" + STAGE + "', '" + BRCD + "', '" + MID + "', '" + PCMAC + "', '" + conn.ConvertDate(EFFECTDATE).ToString() + "','" + InstAmt + "','" + MatAmt + "','" + ADMIN + "','" + AfterMaturity + "')";
            rtn = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rtn;
    }

    // Get All Interest master Data
    public DataTable GetIntrestMaster(GridView GView, string BRCD)
    {
        try
        {


            sql = "select isnull(Convert(varchar(15),A5.EFFECTDATE,103),'01/01/1900') EFFECTDATE ,A5.SRNO, A5.ID,A5.DEPOSITGL, LK.DESCRIPTION CUSTTYPE,A5.PERIODTYPE,A5.PERIODFROM," +
               " A5.PERIODTYPE2, A5.PERIODTO, A5.RATE,A5.PENALTY ,A5.INSTAMT,A5.MATAMT,A5.ADMIN_CHG,A5.AFTERMATROI from A50001 A5 " +

              " INNER JOIN LOOKUPFORM1 LK ON LK.SRNO=A5.TDCUSTTYPE AND LNO='1016' " +
              " WHERE STAGE<>'1004' ORDER BY DEPOSITGL,EFFECTDATE,CUSTTYPE ";
            //" WHERE BRCD='"+BRCD+"' AND STAGE<>'1004' ORDER BY SRNO DESC";
            conn.sBindGrid(GView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }


    // Get Interest master Data by ID
    public DataTable GetIntrestMasterID(string ID, string BRCD)
    {
        try
        {
            //sql = "select A5.SRNO, A5.TDCUSTTYPE, A5.DEPOSITGL, LK.DESCRIPTION, A5.PERIODTYPE,A5.PERIODFROM, A5.PERIODTYPE2, A5.PERIODTO, A5.RATE,A5.PENALTY from A50001 A5 " +
            //      " INNER JOIN LOOKUPFORM1 LK ON LK.SRNO=A5.TDCUSTTYPE AND LK.LNO='1016' " +
            //      " WHERE A5.BRCD='" + BRCD + "' AND A5.SRNO = '" + ID + "' AND A5.STAGE<>'1004'";       

            sql = "select isnull(Convert(varchar(15),A5.EFFECTDATE,103),'01/01/1900') EFFECTDATE , A5.SRNO,A5.ID ,A5.TDCUSTTYPE, A5.DEPOSITGL, GM.GLNAME, " +
                    " A5.PERIODTYPE,A5.PERIODFROM, A5.PERIODTYPE2, A5.PERIODTO, " +


                    " A5.RATE,A5.PENALTY,A5.INSTAMT,A5.MATAMT,A5.ADMIN_CHG,A5.AFTERMATROI from A50001 A5  " +

                    " INNER JOIN LOOKUPFORM1  LK ON LK.SRNO=A5.TDCUSTTYPE AND LK.LNO='1016'  " +
                    " INNER JOIN GLMAST GM ON A5.DEPOSITGL=GM.SUBGLCODE AND A5.BRCD=GM.BRCD " +
                    " WHERE A5.ID = '" + ID + "' AND A5.STAGE<>'1004'"; //BRCD Removed 02/05/2017 to do modification--Abhishek 
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }

    // Update Interest master

    public int ModifyIntrestMaster(string DEPOSIT_TYPE, string TDCUSTTYPE, string DEPOSITGL, string PERIODTYPE, string PERIODFROM, string PERIODTO,
        string RATE, string PENALTY, string STAGE, string BRCD, string MID, string PCMAC, string EFFECTDATE, string ID, string InstAmt, string MatAmt, string Admin, string AfterMaturity)
    {
        try
        {

            sql = " UPDATE A50001 SET TDCUSTTYPE='" + TDCUSTTYPE + "', DEPOSITGL='" + DEPOSITGL + "', PERIODTYPE='" + PERIODTYPE + "', PERIODFROM='" + PERIODFROM + "'," +
                "PERIODTO='" + PERIODTO + "', RATE='" + RATE + "', PENALTY='" + PENALTY + "', STAGE='1002', BRCD='" + BRCD + "', VID='" + MID + "', PCMAC='" + PCMAC + "', " +
                "EFFECTDATE='" + conn.ConvertDate(EFFECTDATE) + "' ,INSTAMT='" + InstAmt + "',MATAMT='" + MatAmt + "',ADMIN_CHG='" + Admin + "',AFTERMATROI='" + AfterMaturity + "' " +

                  " WHERE ID = '" + ID + "' AND STAGE<>'1004'"; //BRCD Removed 02/05/2017 to do modification--Abhishek  
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }

    public int DeleteIntMast(string BRCD, string ID)
    {
        try
        {
            //sql = "select A5.SRNO, A5.TDCUSTTYPE, A5.DEPOSITGL, LK.DESCRIPTION, A5.PERIODTYPE,A5.PERIODFROM, A5.PERIODTYPE2, A5.PERIODTO, A5.RATE,A5.PENALTY from A50001 A5 " +
            //      " INNER JOIN LOOKUPFORM1 LK ON LK.SRNO=A5.TDCUSTTYPE AND LK.LNO='1016' " +
            //      " WHERE A5.BRCD='" + BRCD + "' AND A5.SRNO = '" + ID + "' AND A5.STAGE<>'1004'";   BRCD='" + BRCD + "' AND    

            sql = "UPDATE A50001 SET STAGE=1004 " +
                    " WHERE  ID = '" + ID + "' AND STAGE<>'1004'"; //Removed brcd//Dhanya Shetty//13-06-2017//
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }

    public int BindFilter(GridView GD, string PRDCODE, string MEMTYPE)
    {
        try
        {
            sql = "select isnull(Convert(varchar(15),A5.EFFECTDATE,103),'01/01/1900') EFFECTDATE ,A5.SRNO,A5.ID, A5.DEPOSITGL, LK.DESCRIPTION CUSTTYPE,A5.PERIODTYPE,A5.PERIODFROM," +
                "A5.PERIODTYPE2, A5.PERIODTO, A5.RATE,A5.PENALTY,A5.INSTAMT,A5.MATAMT,A5.ADMIN_CHG,A5.AFTERMATROI from A50001 A5 " +

              " INNER JOIN LOOKUPFORM1 LK ON LK.SRNO=A5.TDCUSTTYPE AND LNO='1016' " +
              " WHERE STAGE<>'1004' AND A5.DEPOSITGL='" + PRDCODE + "' AND A5.TDCUSTTYPE='" + MEMTYPE + "' ORDER BY ID DESC";
            result = conn.sBindGrid(GD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public DataTable Display(string EDAT, string Prd)//Dhanya Shetty//06/10/2017
    {
        try
        {
            sql = "select BRCD,DEPOSITGL,DEPOSIT_TYPE,L.DESCRIPTION as MemberType,PERIODTYPE,PERIODFROM,PERIODTO,RATE,PENALTY,INSTAMT,MATAMT,ADMIN_CHG from A50001 I " +
           " inner join  LOOKUPFORM1 L on lno=1016 and L.Srno=I.TDCUSTTYPE where  EFFECTDATE='" + conn.ConvertDate(EDAT).ToString() + "' and DEPOSITGL='" + Prd + "' and stage<>1004";
            dt = conn.GetDatatable(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable DisplayData(string EDAT, string Prd, string MemType, string Flag, string TDAT)//Dhanya Shetty//06/10/2017
    {
        try
        {
            sql = "Exec IntMasterReport @Effdate='" + EDAT + "',@prd='" + Prd + "',@custtype='" + MemType + "',@Flag='" + Flag + "',@TEffdate='" + TDAT + "'";
            dt = conn.GetDatatable(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

}