using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

public class ClsCancelEntry 
{
    ClsEncryptValue Ecry = new ClsEncryptValue();
    DbConnection DbConn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    DataTable DT = new DataTable();
    string EntryMid, verifyMid, DeleteMid = "";
    string sResult, TableName, sql = "";
    int result;

	public ClsCancelEntry()
	{
	}
    public DataTable GetPayMast(string BRCD, string EntryDate, string SetNo)
    {
        try
        {
            string[] TD;
            string tbname;

            TD = EntryDate.Split('/');
            tbname = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            sql = "select PAYMAST from " + tbname + " where brcd='"+BRCD+"' and entrydate='"+DbConn.ConvertDate(EntryDate)+"' and setno='"+SetNo+"'";
            DT = DbConn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    //Showing all entries in one 
    public int Getinfotable_All(GridView Gview, string smid, string sbrcd, string EDT)
    {
        try
        {
            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "SELECT (ConVert(VarChar(10), AM.SetNo)+'_'+ConVert(VarChar(10), AM.ScrollNo)+'_'+ConVert(VarChar(10), AM.MID)) As ScrollNo, AM.SETNO, " +
                      " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                      " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                      " ,MM.CUSTNAME,AM.PARTICULARS, " +
                      " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                      " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                      " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                      " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                      " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                      " LEFT JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                      " INNER JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                      " WHERE AM.BRCD='" + sbrcd + "' AND AM.STAGE NOT IN( '1003','1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' And SetNo < 20000 AND AM.GLCODE<>99 AND AM.ACTIVITY  NOT IN (31,32) AND AM.PAYMAST<>'Loanapp' " +
                      " ORDER BY AM.SETNO,AM.SCROLLNO ";
            result = DbConn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int GetinfotableAdmin_All(GridView Gview, string smid, string sbrcd, string EDT)
    {
        try
        {

            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "SELECT (ConVert(VarChar(10), AM.SetNo)+'_'+ConVert(VarChar(10), AM.ScrollNo)+'_'+ConVert(VarChar(10), AM.MID)) As ScrollNo, AM.SETNO, " +
                  " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                  " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                  " ,MM.CUSTNAME,AM.PARTICULARS, " +
                  " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                  " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                  " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                  " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                  " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                  " LEFT JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                  " INNER JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                  " WHERE AM.BRCD='" + sbrcd + "' AND AM.STAGE NOT IN('1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' And SetNo < 20000 AND AM.GLCODE<>99 AND AM.ACTIVITY  NOT IN (31,32)  AND AM.PAYMAST<>'Loanapp'" +
                  " ORDER BY AM.SETNO,AM.SCROLLNO ";
            result = DbConn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int GetByVoucherGrid_spe(string brcd, string mid, GridView grdv, string sn, string EDT)
    {
        try
        {
            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "SELECT AM.SETNO, " +
                " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                " ,MM.CUSTNAME,AM.PARTICULARS, " +
                " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                " Left  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " + //Changed Inner join to Left fro Usermaster-- Abhsihek
                " WHERE AM.BRCD='" + brcd + "' AND AM.SETNO='" + sn + "'AND AM.STAGE NOT IN( '1003','1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' AND  AM.ACTIVITY  NOT IN (31,32) " +
                " ORDER BY AM.SETNO,AM.SCROLLNO ";
            result = DbConn.sBindGrid(grdv, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int GetByVoucherGridAdmin_Spe(string brcd, string mid, GridView grdv, string sn, string EDT)
    {
        try
        {
            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "SELECT (ConVert(VarChar(10), AM.SetNo)+'_'+ConVert(VarChar(10), AM.ScrollNo)+'_'+ConVert(VarChar(10), AM.MID)) As ScrollNo, AM.SETNO, " +
                  " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                  " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                  " ,MM.CUSTNAME,AM.PARTICULARS, " +
                  " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                  " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                  " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                  " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                  " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                  " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                  " Left  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " + //Changed Inner join to Left for User master -- Abhsihek
                  " WHERE AM.BRCD='" + brcd + "' AND AM.SETNO='" + sn + "'AND AM.STAGE NOT IN('1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' "+
                  " And AM.SetNo < 20000 And AM.GlCode <> '99' AND AM.ACTIVITY  NOT IN (31,32)  AND AM.PAYMAST<>'Loanapp' " +
                  " ORDER BY AM.SETNO,AM.SCROLLNO ";
            result = DbConn.sBindGrid(grdv, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    // end Showing all entries in one

    public int Getinfotable(GridView Gview, string smid, string sbrcd, string EDT,string FL)
    {
        try
        {
            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            if (FL == "OTH")
            {
                sql = "SELECT AM.SETNO, " +
                      " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                      " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                      " ,MM.CUSTNAME,AM.PARTICULARS, " +
                      " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                      " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                      " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                      " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                      " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                      " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                      " INNER  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                      " WHERE AM.BRCD='" + sbrcd + "' AND AM.STAGE NOT IN( '1003','1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' AND AM.GLCODE<>99 AND AM.ACTIVITY NOT IN (31,32) " +
                      " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }

            else if (FL == "DEP")
            {
                sql = "SELECT AM.SETNO, " +
                      " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                      " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                      " ,MM.CUSTNAME,AM.PARTICULARS, " +
                      " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                      " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                      " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                      " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                      " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                      " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                      " INNER  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                      " WHERE AM.BRCD='" + sbrcd + "' AND AM.STAGE NOT IN( '1003','1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' AND AM.GLCODE<>99 and AM.glcode in (5,10) AND AM.ACTIVITY  NOT IN (31,32) " +
                      " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }
            else if (FL == "LOANINST")
            {
                sql = "SELECT AM.SETNO, " +
                      " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                      " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                      " ,MM.CUSTNAME,AM.PARTICULARS, " +
                      " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                      " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                      " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                      " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                      " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                      " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                      " INNER  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                      " WHERE AM.BRCD='" + sbrcd + "' AND AM.STAGE NOT IN( '1003','1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' AND AM.GLCODE IN (3,11,12) AND AM.GLCODE<>99 AND AM.ACTIVITY  NOT IN (31,32) " +
                      " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }

            result = DbConn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }


    public int GetByVoucherGrid(string brcd,string mid,GridView grdv,string sn,string EDT,string FL)
    {
        try
        {

            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            if (FL == "OTH")
            {
                sql = "SELECT AM.SETNO, " +
                         " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                         " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                         " ,MM.CUSTNAME,AM.PARTICULARS, " +
                         " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                         " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                         " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                         " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                         " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                         " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                         " INNER  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                         " WHERE AM.BRCD='" + brcd + "' AND AM.SETNO='" + sn + "' And SetNo < 20000 AND AM.STAGE NOT IN( '1003','1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' AND AM.ACTIVITY  NOT IN (31,32) " +
                         " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }
            else if (FL == "DEP")
            {
                sql = "SELECT AM.SETNO, " +
                         " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                         " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                         " ,MM.CUSTNAME,AM.PARTICULARS, " +
                         " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                         " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                         " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                         " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                         " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                         " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                         " INNER  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                         " WHERE AM.BRCD='" + brcd + "' AND AM.SETNO='" + sn + "' And SetNo < 20000 AND AM.STAGE NOT IN( '1003','1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' and AM.glcode in (5,10) AND AM.ACTIVITY  NOT IN (31,32)" +
                         " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }
            else if (FL == "LOANINST")
            {
                sql = "SELECT AM.SETNO, " +
                         " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                         " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                         " ,MM.CUSTNAME,AM.PARTICULARS, " +
                         " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                         " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                         " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                         " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                         " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                         " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                         " INNER  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                         " WHERE AM.BRCD='" + brcd + "' AND AM.SETNO='" + sn + "' And SetNo < 20000 AND AM.STAGE NOT IN( '1003','1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' AND AM.GLCODE IN (3,11,12) AND AM.ACTIVITY  NOT IN (31,32) " +
                         " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }

            result = DbConn.sBindGrid(grdv,sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }


    public int GetinfotableAdmin(GridView Gview, string smid, string sbrcd, string EDT, string FL)
    {
        try
        {
            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            if (FL == "OTH")
            {
                sql = "SELECT AM.SETNO, " +
                      " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                      " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                      " ,MM.CUSTNAME,AM.PARTICULARS, " +
                      " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                      " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                      " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                      " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                      " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                      " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                      " INNER  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                      " WHERE AM.BRCD='" + sbrcd + "' AND AM.STAGE NOT IN('1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' AND AM.GLCODE<>99 AND AM.ACTIVITY NOT IN (31,32) " +
                      " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }

            else if (FL == "DEP")
            {
                sql = "SELECT AM.SETNO, " +
                      " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                      " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                      " ,MM.CUSTNAME,AM.PARTICULARS, " +
                      " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                      " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                      " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                      " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                      " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                      " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                      " INNER  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                      " WHERE AM.BRCD='" + sbrcd + "' AND AM.STAGE NOT IN('1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' AND AM.GLCODE<>99 and AM.glcode in (5,10) AND AM.ACTIVITY  NOT IN (31,32) " +
                      " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }
            else if (FL == "LOANINST")
            {
                sql = "SELECT AM.SETNO, " +
                      " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                      " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                      " ,MM.CUSTNAME,AM.PARTICULARS, " +
                      " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                      " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                      " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                      " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                      " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                      " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                      " INNER  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                      " WHERE AM.BRCD='" + sbrcd + "' AND AM.STAGE NOT IN('1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' AND AM.GLCODE IN (3,11,12) AND AM.GLCODE<>99 AND AM.ACTIVITY  NOT IN (31,32) " +
                      " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }

            result = DbConn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int GetByVoucherGridAdmin(string brcd, string mid, GridView grdv, string sn, string EDT, string FL)
    {
        try
        {
            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            if (FL == "OTH")
            {
                sql = "SELECT AM.SETNO, " +
                         " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                         " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                         " ,MM.CUSTNAME,AM.PARTICULARS, " +
                         " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                         " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                         " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                         " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                         " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                         " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                         " INNER  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                         " WHERE AM.BRCD='" + brcd + "' AND AM.SETNO='" + sn + "' And SetNo < 20000 AND AM.STAGE NOT IN('1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' AND AM.ACTIVITY  NOT IN (31,32) " +
                         " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }
            else if (FL == "DEP")
            {
                sql = "SELECT AM.SETNO, " +
                         " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                         " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                         " ,MM.CUSTNAME,AM.PARTICULARS, " +
                         " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                         " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                         " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                         " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                         " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                         " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                         " INNER  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                         " WHERE AM.BRCD='" + brcd + "' AND AM.SETNO='" + sn + "' And SetNo < 20000 AND AM.STAGE NOT IN('1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' and AM.glcode in (5,10) AND AM.ACTIVITY  NOT IN (31,32)" +
                         " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }
            else if (FL == "LOANINST")
            {
                sql = "SELECT AM.SETNO, " +
                         " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                         " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                         " ,MM.CUSTNAME,AM.PARTICULARS, " +
                         " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                         " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                         " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                         " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                         " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                         " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                         " INNER  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                         " WHERE AM.BRCD='" + brcd + "' AND AM.SETNO='" + sn + "' And SetNo < 20000 AND AM.STAGE NOT IN('1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT) + "' " +
                         " AND AM.PAYMAST = 'LOANINST' AND AM.ACTIVITY  NOT IN (31,32) " +
                         " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }

            result = DbConn.sBindGrid(grdv, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
  
    public int CancelVoucher(string setno, string brcd, string EDT, string GLCODE, string SUBGLCODE, string AccNo, string MID,string FL)
    {
        try
        {
            if (FL == "LON")
            {
                sql = "Exec Sp_CancelSetNo @BrCode = '" + brcd + "', @EntDate = '" + DbConn.ConvertDate(EDT) + "', @SetNo = '" + setno + "', @Mid = '"+ MID +"', @Flag = 'LON'";
            }
            else
            {
                sql = "EXEC SP_CANCEL_ENTRY @FLAG='" + FL + "',@SETNO='" + setno + "',@EDT='" + DbConn.ConvertDate(EDT) + "',@BRCD='" + brcd + "',@MID='" + MID + "'";
            }
            result = DbConn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public DataTable GetVInfo(string setno, string brcd, string EDT,string str)
    {
        DataTable DT = new DataTable();
        try
        {
            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            if (str == "ABB-MultiTRF")
            {
                sql = "SELECT CONVERT(VARCHAR(10),AM.SETNO)+'_'+CONVERT(VARCHAR(10),AM.GLCODE)+'_'+CONVERT(VARCHAR(10),(CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END)) SETNO, " +
                     " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                     " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                     " ,MM.CUSTNAME,AM.PARTICULARS, " +
                     " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                     " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                     " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                     " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                     " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO  " +
                     " LEFT JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                     //" INNER JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +   //Commented by amol On 23/02/2018
                     " Left Join USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO) = AM.MID " +
                     " WHERE AM.refBRCD='" + brcd + "' AND AM.SETNO='" + setno + "' AND AM.STAGE NOT IN('1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT).ToString() + "' " +
                     " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }
            else
            {
                sql = "SELECT CONVERT(VARCHAR(10),AM.SETNO)+'_'+CONVERT(VARCHAR(10),AM.GLCODE)+'_'+CONVERT(VARCHAR(10),(CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END)) SETNO, " +
                      " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                      " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                      " ,MM.CUSTNAME,AM.PARTICULARS, " +
                      " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                      " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                      " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                      " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                      " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                      " LEFT JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                      //" INNER JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +        //Commented by amol On 23/02/2018
                      " Left Join USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO) = AM.MID " + 
                      " WHERE AM.BRCD='" + brcd + "' AND AM.SETNO='" + setno + "' AND AM.STAGE NOT IN('1004') AND AM.ENTRYDATE='" + DbConn.ConvertDate(EDT).ToString() + "' " +
                      " ORDER BY AM.SETNO,AM.SCROLLNO ";
            }
            DT = DbConn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int GetSetMid(string BrCode, string EDate, string SetNo)
    {
        try
        {
            string[] TD; TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select Distinct Mid From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + DbConn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "'";
            result = Convert.ToInt32(DbConn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public string CheckStage(string setno, string EDT, string BRCD)
    {
        string RC = "";
        try
        {
            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            string sql = "select Stage from " + TableName + " where SETNO='" + setno + "' and EntryDate='" + DbConn.ConvertDate(EDT) + "' and STAGE<>1004 and BRCD='" + BRCD + "'";
            RC = DbConn.sExecuteScalar(sql);
            return RC;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RC;
        }
    }

    public int CancelEntryLoan(string BrCode, string PrCode, string AccNo, string SetNo, string EntryMid, string VerifyMid, string EDate)
    {
        try
        {
            string[] TD; TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            DeleteMid = Ecry.GetVK(VerifyMid);

            //  Added by amol on 14092017 for cancel loan set only
            sql = "Select Top 1 RefId From " + TableName + " Where brcd = '" + BrCode + "' And EntryDate = '" + DbConn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And Mid = '" + EntryMid + "'";
            sResult = DbConn.sExecuteScalar(sql);

            if (sResult != null && sResult != "")
            {
                sql = "UPDATE " + TableName + " SET STAGE = 1004, CID = '" + VerifyMid + "', F3 = '" + DeleteMid + "', SystemDate = GetDate() WHERE BRCD = '" + BrCode + "' AND ENTRYDATE = '" + DbConn.ConvertDate(EDate) + "' And RefId = '" + sResult + "' And Mid = '" + EntryMid + "'";
                result = DbConn.sExecuteQuery(sql);

                if (result > 0)
                {
                    sql = "Update Avs_LnTrx Set Stage = 1004, Vid = '" + VerifyMid + "', Vid_EntryDate = '" + DbConn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + DbConn.ConvertDate(EDate) + "' And RefId = '" + sResult + "' And Mid = '" + EntryMid + "'";
                    DbConn.sExecuteQuery(sql);

                    // Added by amol On 31/01/2018 for increase and decrease cash
                    sql = "Update B Set B.No_Of_Notes = (Case When A.Inn = 0 Then B.No_Of_Notes + Out When A.Out = 0 Then B.No_Of_Notes - Inn End) " +
                          "From AVS5012 A " +
                          "Inner Join AVS5011 B With(NoLock) On A.BrCd = B.BrCd And A.CounterNo = B.V_TYPE And A.NoteType = B.Note_Type " +
                          "Where A.BrCd = '" + BrCode + "' And A.EffectDate = '" + DbConn.ConvertDate(EDate) + "' And A.SetNo = '" + SetNo + "' And A.Stage <> '1004'";
                    DbConn.sExecuteQuery(sql);

                    // Added by amol On 30/01/2018 for cancel cash denomination voucher
                    sql = "Update avs5012 Set Stage = '1004' Where BrCd = '" + BrCode + "' And EffectDate = '" + DbConn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    DbConn.sExecuteQuery(sql);

                    if (result > 0)
                    {
                        sql = "Update LoanInfo Set LASTINTDATE = PREV_INTDT,LMSTATUS=1, MOD_DATE = '" + DbConn.ConvertDate(EDate).ToString() + "' Where BrCd= '" + BrCode + "' And LOANGLCODE = '" + PrCode + "' And CUSTACCNO = '" + AccNo + "'"; 
                        result = DbConn.sExecuteQuery(sql);
                    }
                    
                    //  Added by Amol on 11-10-2017 for suit file
                    if (result > 0)
                    {
                        sql = "Select IsNull(Case When Acc_Status = '' Then '1' Else Acc_Status End, '1') From AVS5032 Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
                        sResult = DbConn.sExecuteScalar(sql);

                        if (sResult == "" || sResult == null)
                            sResult = "1";

                        sql = "Update Avs_Acc Set ACC_STATUS = '" + sResult + "' Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
                        result = DbConn.sExecuteQuery(sql);
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int CancelEntryLoan1(string BrCode, string PrCode, string AccNo, string SetNo, string Mid, string EDate)
    {
        DataTable dt1 = new DataTable();
        try
        {
            string[] TD; TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            DeleteMid = Ecry.GetVK(Mid);

            sql = "UPDATE " + TableName + " SET STAGE = 1004, CID = '" + Mid + "', F3 = '" + DeleteMid + "', SystemDate = GetDate() WHERE SETNO = '" + SetNo + "' AND REFBRCD = '" + BrCode + "' AND  ENTRYDATE = '" + DbConn.ConvertDate(EDate) + "'";
            result = DbConn.sExecuteQuery(sql);

            if (result > 0)
            {

                // Added by amol On 31/01/2018 for increase and decrease cash
                sql = "Update B Set B.No_Of_Notes = (Case When A.Inn = 0 Then B.No_Of_Notes + Out When A.Out = 0 Then B.No_Of_Notes - Inn End) " +
                      "From AVS5012 A " +
                      "Inner Join AVS5011 B With(NoLock) On A.BrCd = B.BrCd And A.CounterNo = B.V_TYPE And A.NoteType = B.Note_Type " +
                      "Where A.BrCd = '" + BrCode + "' And A.EffectDate = '" + DbConn.ConvertDate(EDate) + "' And A.SetNo = '" + SetNo + "' And A.Stage <> '1004'";
                DbConn.sExecuteQuery(sql);

                // Added by amol On 30/01/2018 for cancel cash denomination voucher
                sql = "Update avs5012 Set Stage = '1004' Where BrCd = '" + BrCode + "' And EffectDate = '" + DbConn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                DbConn.sExecuteQuery(sql);

                sql = "select distinct brcd  from " + TableName + " where RefBrcd='" + BrCode + "' and setno='" + SetNo + "' and ENTRYDATE='" + DbConn.ConvertDate(EDate) + "' and brcd<>refbrcd";// Amruta 12/07/2017
                dt1 = DbConn.GetDatatable(sql);
                if (dt1.Rows.Count > 0)
                {
                    sql = "update AVS_LNTrx set stage=1004 Where BrCd in ( '" + dt1.Rows[0]["brcd"].ToString() + "','" + dt1.Rows[1]["brcd"].ToString() + "') And subglcode = '" + PrCode + "' And AccountNo = '" + AccNo + "' and  setno='" + SetNo + "'"; // Amruta 06/07/2017 add LMSTATUS=1 as Ambika Madam
                    DbConn.sExecuteQuery(sql);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int CancelEntryMulti(string SetNo, string EDate, string BrCode, string EntryMid, string VerifyMid)
    {
        try
        {
            string[] TD; TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            DeleteMid = Ecry.GetVK(VerifyMid);

            //  Added by amol on 14092017 for cancel MultiVoucher set only
            sql = "Select Top 1 RefId From " + TableName + " Where brcd = '" + BrCode + "' And EntryDate = '" + DbConn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And Mid = '" + EntryMid + "'";
            sResult = DbConn.sExecuteScalar(sql);

            if (sResult != null && sResult != "")
            {
                sql = "UPDATE " + TableName + " SET STAGE = 1004, CID = '" + VerifyMid + "', F3 = '" + DeleteMid + "', SystemDate = GetDate() WHERE BRCD = '" + BrCode + "' AND ENTRYDATE = '" + DbConn.ConvertDate(EDate) + "' And RefId = '" + sResult + "' And Mid = '" + EntryMid + "'";
                result = DbConn.sExecuteQuery(sql);

                if (result > 0)
                {
                    sql = "Update Avs_LnTrx Set Stage = 1004, Vid = '" + VerifyMid + "', Vid_EntryDate = '" + DbConn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + DbConn.ConvertDate(EDate) + "' And RefId = '" + sResult + "' And Mid = '" + EntryMid + "'";
                    DbConn.sExecuteQuery(sql);

                    // Added by amol On 31/01/2018 for increase and decrease cash
                    sql = "Update B Set B.No_Of_Notes = (Case When A.Inn = 0 Then B.No_Of_Notes + Out When A.Out = 0 Then B.No_Of_Notes - Inn End) " +
                          "From AVS5012 A " +
                          "Inner Join AVS5011 B With(NoLock) On A.BrCd = B.BrCd And A.CounterNo = B.V_TYPE And A.NoteType = B.Note_Type " +
                          "Where A.BrCd = '" + BrCode + "' And A.EffectDate = '" + DbConn.ConvertDate(EDate) + "' And A.SetNo = '" + SetNo + "' And A.Stage <> '1004'";
                    DbConn.sExecuteQuery(sql);

                    // Added by amol On 30/01/2018 for cancel cash denomination voucher
                    sql = "Update avs5012 Set Stage = '1004' Where BrCd = '" + BrCode + "' And EffectDate = '" + DbConn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    DbConn.sExecuteQuery(sql);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int  CancelEntryAgentCo(string SetNo, string EDate, string BrCode, string EntryMid, string VerifyMid) //added by ashok misal
    {
        try
        {
            string[] TD; TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            DeleteMid = Ecry.GetVK(VerifyMid);

            sql = "UPDATE " + TableName + " SET STAGE = 1004, CID = '" + VerifyMid + "', F3 = '" + DeleteMid + "', SystemDate = GetDate() WHERE BRCD = '" + BrCode + "' AND ENTRYDATE = '" + DbConn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
            result = DbConn.sExecuteQuery(sql);

            if (result > 0)
            {
                // Added by amol On 31/01/2018 for increase and decrease cash
                sql = "Update B Set B.No_Of_Notes = (Case When A.Inn = 0 Then B.No_Of_Notes + Out When A.Out = 0 Then B.No_Of_Notes - Inn End) " +
                      "From AVS5012 A " +
                      "Inner Join AVS5011 B With(NoLock) On A.BrCd = B.BrCd And A.CounterNo = B.V_TYPE And A.NoteType = B.Note_Type " +
                      "Where A.BrCd = '" + BrCode + "' And A.EffectDate = '" + DbConn.ConvertDate(EDate) + "' And A.SetNo = '" + SetNo + "' And A.Stage <> '1004'";
                DbConn.sExecuteQuery(sql);

                // Added by amol On 30/01/2018 for cancel cash denomination voucher
                sql = "Update avs5012 Set Stage = '1004' Where BrCd = '" + BrCode + "' And EffectDate = '" + DbConn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                DbConn.sExecuteQuery(sql);

                sql = "UPDATE AVS_AGENTCOMIOSSION SET STAGE=1004 where  ENTRYDATE='" + DbConn.ConvertDate(EDate) + "' AND SETNO='" + SetNo + "' AND BRCD='" + BrCode + "'";
                result = DbConn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int CancelEntryMobileMultiPosting(string SetNo, string EDate, string BrCode, string EntryMid, string VerifyMid) //added by ashok misal
    {
        try
        {
            string[] TD; TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            DeleteMid = Ecry.GetVK(VerifyMid);

            sql = "UPDATE " + TableName + " SET STAGE = 1004, CID = '" + VerifyMid + "', F3 = '" + DeleteMid + "', SystemDate = GetDate() WHERE BRCD = '" + BrCode + "' AND ENTRYDATE = '" + DbConn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
            result = DbConn.sExecuteQuery(sql);

            if (result > 0)
            {
                // Added by amol On 31/01/2018 for increase and decrease cash
                //sql = "Update B Set B.No_Of_Notes = (Case When A.Inn = 0 Then B.No_Of_Notes + Out When A.Out = 0 Then B.No_Of_Notes - Inn End) " +
                //      "From AVS5012 A " +
                //      "Inner Join AVS5011 B With(NoLock) On A.BrCd = B.BrCd And A.CounterNo = B.V_TYPE And A.NoteType = B.Note_Type " +
                //      "Where A.BrCd = '" + BrCode + "' And A.EffectDate = '" + DbConn.ConvertDate(EDate) + "' And A.SetNo = '" + SetNo + "' And A.Stage <> '1004'";
                //DbConn.sExecuteQuery(sql);

                // Added by amol On 30/01/2018 for cancel cash denomination voucher
                //sql = "Update avs5012 Set Stage = '1004' Where BrCd = '" + BrCode + "' And EffectDate = '" + DbConn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                //DbConn.sExecuteQuery(sql);

                sql = "update allvcr set STAGE='1001' where ref_agent=(SELECT TOP 1 Ref_Agent from "+TableName+" where entrydate='" + DbConn.ConvertDate(EDate) + "' and brcd='" + BrCode + "' and setno='" + SetNo + "' AND Ref_Agent IS NOT NULL AND Ref_Agent<>0) and brcd='" + BrCode + "' and convert(varchar(10),RTIME,121)='" + DbConn.ConvertDate(EDate) + "'";
                result = DbConn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int CancelSIPost(string BrCode, string EDate, string SetNo, string Mid)
    {
        try
        {
            sql = "Exec SP_CancelSIPost @BrCode = '" + BrCode + "', @EntryDate = '" + DbConn.ConvertDate(EDate).ToString() + "', @SetNo = '" + SetNo + "', @Mid = '" + Mid + "'";
            result = DbConn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int CancelReverse(string SetNo, string EDate, string BrCode, string VerifyMid)
    {
        try
        {
            string[] TD; TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            DeleteMid = Ecry.GetVK(VerifyMid);

            sql = "UPDATE " + TableName + " SET STAGE = 1004, CID = '" + VerifyMid + "', F3 = '" + DeleteMid + "', SystemDate = GetDate() WHERE BRCD = '" + BrCode + "' AND ENTRYDATE = '" + DbConn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
            result = DbConn.sExecuteQuery(sql);

            sql = "Update Avs_LnTrx Set Stage = 1004, Vid = '" + VerifyMid + "', Vid_EntryDate = '" + DbConn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + DbConn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
            DbConn.sExecuteQuery(sql);

            // Added by amol On 31/01/2018 for increase and decrease cash
            sql = "Update B Set B.No_Of_Notes = (Case When A.Inn = 0 Then B.No_Of_Notes + Out When A.Out = 0 Then B.No_Of_Notes - Inn End) " +
                  "From AVS5012 A " +
                  "Inner Join AVS5011 B With(NoLock) On A.BrCd = B.BrCd And A.CounterNo = B.V_TYPE And A.NoteType = B.Note_Type " +
                  "Where A.BrCd = '" + BrCode + "' And A.EffectDate = '" + DbConn.ConvertDate(EDate) + "' And A.SetNo = '" + SetNo + "' And A.Stage <> '1004'";
            DbConn.sExecuteQuery(sql);

            // Added by amol On 30/01/2018 for cancel cash denomination voucher
            sql = "Update avs5012 Set Stage = '1004' Where BrCd = '" + BrCode + "' And EffectDate = '" + DbConn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
            DbConn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int CancelDDS(string SETNO, string EDT, string BRCD,string MID)
    {
        try
        {
            sql = "EXEC SP_CANCEL_ENTRY @FLAG='DDSCLOSE',@SETNO='" + SETNO + "',@EDT='" + DbConn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "',@MID='" + MID + "'";
            result = DbConn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int CancelCRCP(string SETNO, string EDT, string BRCD,string MID)
    {
        try
        {
            sql = "EXEC SP_CANCEL_ENTRY @FLAG='CRCP',@SETNO='" + SETNO + "',@EDT='" + DbConn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "',@MID='" + MID + "'";
            result = DbConn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int CancelCRCP1(string SETNO, string EDT, string BRCD, string MID)
    {
        try
        {
            sql = "EXEC SP_CANCEL_ENTRYABB @FLAG='CRCP',@SETNO='" + SETNO + "',@EDT='" + DbConn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "',@MID='" + MID + "'";
            result = DbConn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int CancelTDA(string SETNO, string EDT, string BRCD, string MID)
    {
        try
        {
            sql = "EXEC SP_CANCEL_ENTRY @FLAG='DP',@SETNO='" + SETNO + "',@EDT='" + DbConn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "',@MID='" + MID + "'";
            result = DbConn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int CancelSIPOST(string SETNO, string EDT, string BRCD, string MID)
    {
        try
        {
            sql = "EXEC SP_CANCEL_ENTRY @FLAG='SIPOST',@SETNO='" + SETNO + "',@EDT='" + DbConn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "',@MID='" + MID + "'";
            result = DbConn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int UpdateInv(string setno, string brcd, string EDT, string GLCODE, string SUBGLCODE, string AccNo, string MID, string FL)
    {
        string CustNo = "", subglcode = "";
        string[] TD; TD = EDT.Split('/');
        TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
        try
        {
            sql = "select Accno from "+TableName+" where setno='"+setno+"' and entrydate='"+DbConn.ConvertDate(EDT)+"' and BRCD='"+brcd+"' and trxtype=1 and accno<>0";
            CustNo = DbConn.sExecuteScalar(sql);
            sql = "select subglcode from " + TableName + " where setno='" + setno + "' and entrydate='" + DbConn.ConvertDate(EDT) + "' and BRCD='" + brcd + "' and trxtype=1 and accno<>0";
            subglcode = DbConn.sExecuteScalar(sql);
            sql = "update avs_invaccountmaster set stage=1003,AccStatus=1 where brcd='"+brcd+"' and subglcode='"+subglcode+"' and Custaccno='"+CustNo+"'";
            result = DbConn.sExecuteQuery(sql);
            sql = "update avs_invdepositemaster set stage=1003 where brcd='" + brcd + "' and subglcode='" + subglcode + "' and Custaccno='" + CustNo + "'";
            result = DbConn.sExecuteQuery(sql);
            sql = "select Accno from " + TableName + " where setno='" + setno + "' and entrydate='" + DbConn.ConvertDate(EDT) + "' and BRCD='" + brcd + "' and trxtype=2 and accno<>0";
            CustNo = DbConn.sExecuteScalar(sql);
            if (CustNo != "")
            {
                sql = "update avs_invaccountmaster set stage=1004,AccStatus=3 where brcd='" + brcd + "' and subglcode='" + subglcode + "' and Custaccno='" + CustNo + "'";
                DbConn.sExecuteQuery(sql);
                sql = "update avs_invdepositemaster set stage=1004 where brcd='" + brcd + "' and subglcode='" + subglcode + "' and Custaccno='" + CustNo + "'";
                DbConn.sExecuteQuery(sql);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public int UpdatePrintingMstr(string setno, string brcd, string EDT, string MID)//Dhanya Shetty//09/03/2018
    {
        try
        {
            sql = "Exec ISP_AVS0133 @Brcd='" + brcd + "',@Edate	='" + DbConn.ConvertDate(EDT) + "',@Setno='" + setno + "',@Mid='" + MID + "'";
            result = DbConn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
}