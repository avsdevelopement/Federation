using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


public class ClsMinBal
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql;
    int result;
	public ClsMinBal()
	{
	}
    public int InsertData(string Prd,string EffDate,string Allslected,string AccType,string skipcharges,string AllowToTbBal,string Frequency,string MinBalWC,string ChargesWC,string MinBalWTC,string ChargesWTC,
        string FreeInstance,string Creditacc,string Mincharge,string MaxCharge,string LastAppDate,string Particulars,string Brcd,string Stage,string SystemDate,string Mid)
    {
        try
        {
            sql = "Insert into AVS_P_Minbal(PRDCODE,EFFDATE,ALLSELECTED,ACCTYPE,SKIPCHARGES,ALLOWTODBAL,FREQUENCY,MINBALWCB,CHARGESWCB,MINBALWTCB,CHARGESWTCB,FREEINSTANCE,CREDITPLACC,MINCHARGE," +
            "MAXCHARGE,LASTAPPDATE,PARTICULARS,BRCD,STAGE,SYSTEMDATE,Mid)values('" + Prd + "','" + conn.ConvertDate(EffDate).ToString() + "','" + Allslected + "','" + AccType + "','" + skipcharges + "','" + AllowToTbBal + "','" + Frequency + "','" + MinBalWC + "','" + ChargesWC + "','" + MinBalWTC + "','" + ChargesWTC + "','" + FreeInstance + "'," +
           " '" + Creditacc + "','" + Mincharge + "','" + MaxCharge + "','" + conn.ConvertDate(LastAppDate).ToString() + "','" + Particulars + "','" + Brcd + "','" + Stage + "','" + conn.ConvertDate(SystemDate).ToString() + "','"+Mid+"')";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result = 0;
        }
    }

    public int ModifyData(string Prd, string EffDate, string Allslected, string AccType, string skipcharges, string AllowToTbBal, string Frequency, string MinBalWC, string ChargesWC, string MinBalWTC, string ChargesWTC,
       string FreeInstance, string Creditacc, string Mincharge, string MaxCharge, string LastAppDate, string Particulars, string Brcd, string Stage, string MID, string SYSTEMDATE, string id)
    {
             try
        {
            sql = "Update AVS_P_Minbal set  PRDCODE='" + Prd + "',EFFDATE='" +conn.ConvertDate(EffDate).ToString()+ "',ALLSELECTED='" + Allslected + "',ACCTYPE='" + AccType + "',SKIPCHARGES='" + skipcharges + "',ALLOWTODBAL='" + AllowToTbBal + "',FREQUENCY='" + Frequency + "',MINBALWCB='" + MinBalWC + "',CHARGESWCB='" + ChargesWC + "',MINBALWTCB='" + MinBalWTC + "',CHARGESWTCB='" + ChargesWTC + "'," +
                "FREEINSTANCE='" + FreeInstance + "',CREDITPLACC='" + Creditacc + "',MINCHARGE='" + Mincharge + "',MAXCHARGE='" + MaxCharge + "',LASTAPPDATE='" + conn.ConvertDate(LastAppDate).ToString() + "',PARTICULARS='" + Particulars + "',BRCD='" + Brcd + "',MID='" + MID + "',SYSTEMDATE='" + conn.ConvertDate(SYSTEMDATE).ToString() + "' ,STAGE='1002' " +
                " where BRCD='" + Brcd + "'AND PRDCODE='" + Prd + "'AND CREDITPLACC='" + Creditacc + "' and ID='" + id + "' and stage<>1004  ";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result = 0;
        }
 }
    public int DeleteData(string Brcd, string Prd, string Creditacc, string MID, string id)
    {
        try
        {
            sql = "update AVS_P_Minbal set stage=1004,MID='" + MID + "' where  BRCD='" + Brcd + "'AND PRDCODE='" + Prd + "'AND CREDITPLACC='" + Creditacc + "'  and ID='" + id + "' and stage not in(1003,1004)";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result = 0;
        }
    }
    public int AuthoriseData(string Brcd, string Prd, string Creditacc, string MID, string id)
    {
        try
        {
            sql = "update AVS_P_Minbal set stage=1003,MID='" + MID + "' where BRCD='" + Brcd + "'AND PRDCODE='" + Prd + "'AND CREDITPLACC='" + Creditacc + "'and ID='" + id + "' and stage<>1003 ";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public DataTable GetInfo(string Brcd,string id, string Prd, string Creditacc)
    {
          DataTable DT = new DataTable();
        try
        {
            sql = "Select PRDCODE,CONVERT(VARCHAR(11), EFFDATE,103) AS EFFDATE,ALLSELECTED,ACCTYPE,SKIPCHARGES,ALLOWTODBAL,FREQUENCY,MINBALWCB,CHARGESWCB,MINBALWTCB,CHARGESWTCB,FREEINSTANCE,CREDITPLACC,MINCHARGE," +
            "MAXCHARGE,CONVERT(VARCHAR(11), LASTAPPDATE,103) AS LASTAPPDATE,PARTICULARS,BRCD,STAGE from AVS_P_Minbal where BRCD='" + Brcd + "'AND PRDCODE='" + Prd + "'AND CREDITPLACC='" + Creditacc + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex); ;
        }
        return DT;
    }

    public int GetGridData(GridView GView, string BRCD, string EDate, string PT, string AC)
    {
        try
        {
            if (PT == "" && AC == "")
            {
                sql = "select Id,PRDCODE,CREDITPLACC  from AVS_P_Minbal L " +
                //,B.CUSTNAME AS CUSTNAME,(case when L.Mid=999 then 'upload' else  UU.USERNAME end ) as MakerName 
                //" INNER JOIN AVS_ACC A  ON A.ACCNO=L.CREDITPLACC AND A.SUBGLCODE=L.PRDCODE AND  A.BRCD=L.BRCD  INNER JOIN   MASTER B ON A.CUSTNO=B.CUSTNO AND A.BRCD=B.BRCD " +
                //"Left  JOIN USERMASTER UU ON  L.MID=UU.PERMISSIONNO  
                "where L.stage not in (1004) and L.BRCD='" + BRCD + "' and L.SYSTEMDATE='" + conn.ConvertDate(EDate) + "' order by PRDCODE, CREDITPLACC";
            }
            else
            {
                sql = "select Id,PRDCODE,CREDITPLACC from AVS_P_Minbal L " +
               // ,B.CUSTNAME AS CUSTNAME,(case when L.Mid=999 then 'upload' else  UU.USERNAME end ) as MakerName 
               // " INNER JOIN AVS_ACC A  ON A.ACCNO=L.CREDITPLACC AND A.SUBGLCODE=L.PRDCODE AND  A.BRCD=L.BRCD  INNER JOIN   MASTER B ON A.CUSTNO=B.CUSTNO AND A.BRCD=B.BRCD " +
               // "Left  JOIN USERMASTER UU ON  L.MID=UU.PERMISSIONNO  
               " where L.stage not in (1004) and L.BRCD='" + BRCD + "'  and L.PRDCODE='" + PT + "' and L.CREDITPLACC='" + AC + "' order by PRDCODE, CREDITPLACC";
            }
            result = conn.sBindGrid(GView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
         }
        return result;
    }
    public string GetStage(string BRCD, string Prdcode, string ACC)
    {
        try
        {
            sql = "SELECT STAGE FROM AVS_P_Minbal WHERE BRCD='" + BRCD + "' AND PRDCODE='" + Prdcode + "' and CREDITPLACC='" + ACC + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
}


