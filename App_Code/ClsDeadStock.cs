using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public class ClsDeadStock
{
    DbConnection conn = new DbConnection();
    string sql;
    DataTable DT;
    string result;
    int Result=0;
    int res;
	public ClsDeadStock()
	{
		
	}

    public int Insertdata(string BRCD,string PRDCODE,string ACCNO,string DESC,string ASSTNO,string ASSTDESC,string DATE,string STATUSNO,string  STATUSDESC,string SANCTIONNO,string SANCTIONDESC,
        string VENDORNAME,string PERIOD,string ITEMDETAILNO,string ITEMDETAILDESC,string TYPEITMNO,string TYPEITMDESC,string IDCODE,string BRANCHDEP,string NAME,string WARRANTY,string AMCDETAIL,
        string VALUE,string BILLNO,string BILLDATE,string CHEQUENO,string CHEQUEDATE,string ASSTLOC,string ASSTSUBLOC,string ASSLOCNO,string ASSTDESCRPTN,string PURCHASEDATE,string ENTRYDATE,string QUANTITY,
        string VALUEUNIT, string PURCHASEVALUE, string VALUEASOND, string PERDEP, string DEPTYPENO, string DEPTYPENAME, string BOOKBAL, string CLSGLBAL, string STAGE, string FL,string Mid)
    {
            try
            {
                sql = "EXEC D_DEADSTOCK  @BRCD='" + BRCD + "',@PRDCODE='" + PRDCODE + "',@ACCNO='" + ACCNO + "',@DESC='" + DESC + "',@ASSTNO='" + ASSTNO + "',@ASSTDESC='" + ASSTDESC + "',"+
                    "@DATE='" + conn.ConvertDate(DATE).ToString() + "',@STATUSNO='" + STATUSNO + "',@STATUSDESC='" + STATUSDESC + "',@SANCTIONNO='" + SANCTIONNO + "',@SANCTIONDESC='" + SANCTIONDESC + "',@VENDORNAME='" + VENDORNAME + "',"+
                  " @PERIOD= '" + PERIOD + "',@ITEMDETAILNO= '" + ITEMDETAILNO + "',@ITEMDETAILDESC='" + ITEMDETAILDESC + "',@TYPEITMNO='" + TYPEITMNO + "',@TYPEITMDESC='" + TYPEITMDESC + "',"+
                  "@IDCODE='" + IDCODE + "',@BRANCHDEP='" + BRANCHDEP + "',@NAME='" + NAME + "',@WARRANTY='" + WARRANTY + "',@AMCDETAIL='" + AMCDETAIL + "',@VALUE='" + VALUE + "',@BILLNO='" + BILLNO + "',"+
                  "@BILLDATE='" + conn.ConvertDate(BILLDATE).ToString() + "',@CHEQUENO='" + CHEQUENO + "',@CHEQUEDATE='" + conn.ConvertDate(CHEQUEDATE).ToString() + "',@ASSTLOC='" + ASSTLOC + "',@ASSTSUBLOC='" + ASSTSUBLOC + "',@ASSLOCNO='" + ASSLOCNO + "',@ASSTDESCRPTN='" + ASSTDESCRPTN + "'," +
                  "@PURCHASEDATE='" + conn.ConvertDate(PURCHASEDATE).ToString() + "',@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE).ToString() + "',@QUANTITY='" + QUANTITY + "',@VALUEUNIT='" + VALUEUNIT + "',@PURCHASEVALUE='" + PURCHASEVALUE + "',"+
                  "@VALUEASOND='" + VALUEASOND + "',@PERDEP='" + PERDEP + "',@DEPTYPENO='" + DEPTYPENO + "',@DEPTYPENAME='" + DEPTYPENAME + "',@BOOKBAL='" + BOOKBAL + "',@CLSGLBAL='" + CLSGLBAL + "',@STAGE='" + STAGE + "',@Flag='" + FL + "' ,@Mid='"+Mid+"'";
                Result = conn.sExecuteQuery(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return Result;
    }

    public int ModifyData(string BRCD, string PRDCODE, string ACCNO, string DESC, string ASSTNO, string ASSTDESC, string DATE, string STATUSNO, string STATUSDESC, string SANCTIONNO, string SANCTIONDESC,
        string VENDORNAME, string PERIOD, string ITEMDETAILNO, string ITEMDETAILDESC, string TYPEITMNO, string TYPEITMDESC, string IDCODE, string BRANCHDEP, string NAME, string WARRANTY, string AMCDETAIL,
        string VALUE, string BILLNO, string BILLDATE, string CHEQUENO, string CHEQUEDATE, string ASSTLOC, string ASSTSUBLOC, string ASSLOCNO, string ASSTDESCRPTN, string PURCHASEDATE, string ENTRYDATE, string QUANTITY,
        string VALUEUNIT, string PURCHASEVALUE, string VALUEASOND, string PERDEP, string DEPTYPENO, string DEPTYPENAME, string BOOKBAL, string CLSGLBAL, string STAGE, string FL, string Mid,string id)
    {
        try
        {
            sql = "EXEC D_DEADSTOCK @BRCD='" + BRCD + "',@PRDCODE='" + PRDCODE + "',@ACCNO='" + ACCNO + "',@DESC='" + DESC + "',@ASSTNO='" + ASSTNO + "',@ASSTDESC='" + ASSTDESC + "',"+
                    "@DATE='" + conn.ConvertDate(DATE).ToString() + "',@STATUSNO='" + STATUSNO + "',@STATUSDESC='" + STATUSDESC + "',@SANCTIONNO='" + SANCTIONNO + "',@SANCTIONDESC='" + SANCTIONDESC + "',@VENDORNAME='" + VENDORNAME + "'," +
                  " @PERIOD= '" + PERIOD + "',@ITEMDETAILNO= '" + ITEMDETAILNO + "',@ITEMDETAILDESC='" + ITEMDETAILDESC + "',@TYPEITMNO='" + TYPEITMNO + "',@TYPEITMDESC='" + TYPEITMDESC + "',"+
                  "@IDCODE='" + IDCODE + "',@BRANCHDEP='" + BRANCHDEP + "',@NAME='" + NAME + "',@WARRANTY='" + WARRANTY + "',@AMCDETAIL='" + AMCDETAIL + "',@VALUE='" + VALUE + "',@BILLNO='" + BILLNO + "',"+
                  "@BILLDATE='" + conn.ConvertDate(BILLDATE).ToString() + "',@CHEQUENO='" + CHEQUENO + "',@CHEQUEDATE='" + conn.ConvertDate(CHEQUEDATE).ToString() + "',@ASSTLOC='" + ASSTLOC + "',@ASSTSUBLOC='" + ASSTSUBLOC + "',@ASSLOCNO='" + ASSLOCNO + "',@ASSTDESCRPTN='" + ASSTDESCRPTN + "'," +
                  "@PURCHASEDATE='" + conn.ConvertDate(PURCHASEDATE).ToString() + "',@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE).ToString() + "',@QUANTITY='" + QUANTITY + "',@VALUEUNIT='" + VALUEUNIT + "',@PURCHASEVALUE='" + PURCHASEVALUE + "'," +
                  "@VALUEASOND='" + VALUEASOND + "',@PERDEP='" + PERDEP + "',@DEPTYPENO='" + DEPTYPENO + "',@DEPTYPENAME='" + DEPTYPENAME + "',@BOOKBAL='" + BOOKBAL + "',@CLSGLBAL='" + CLSGLBAL + "',@STAGE='" + STAGE + "',@Flag='" + FL + "',@Mid='" + Mid + "',@Id='"+id+"' ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;

    }
    public int DeleteData(string BRCD, string PRDCODE, string ACCNO, string STAGE, string FL, string Mid, string id)
    {
        try
        {
            sql = "EXEC D_DEADSTOCK  @BRCD='" + BRCD + "',@PRDCODE='" + PRDCODE + "',@ACCNO='" + ACCNO + "',@STAGE='" + STAGE + "',@Flag='" + FL + "',@Mid='" + Mid + "',@Id='" + id + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;

    }
    public int AuthoriseData(string BRCD, string PRDCODE, string ACCNO, string STAGE, string FL, string Mid, string id)
    {
        try
        {
            sql = "EXEC D_DEADSTOCK  @BRCD='" + BRCD + "',@PRDCODE='" + PRDCODE + "',@ACCNO='" + ACCNO + "',@STAGE='" + STAGE + "',@Flag='" + FL + "',@Mid='" + Mid + "',@Id='" + id + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;

    }

    public DataTable GetInfo(string BRCD, string PRDCODE, string ACCNO,string id)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select BRCD,PRDCODE,ACCNO,DESCRIPTION,TYPE_OF_ASSEST_NO,TYPE_OF_ASSEST_DESC,CONVERT(VARCHAR(11),DATE,103) AS DATE,STATUS_NO,STATUS_DESC,SANCTION_NO,SANCTION_DESC,VENDORS_NAME,PERIOD," +
                " ITEM_DETAILS_NO,ITEM_DETAILS_DESC,TYPE_OF_ITEM_NO,TYPE_OF_ITEM_DESC,ID_CODE,BRANCH_DEPT,NAME,WARRANTY,AMC_DETAILS,VALUE,BILL_NO,CONVERT(VARCHAR(11),BILL_DATE,103) AS BILL_DATE,CHEQUE_NO,CONVERT(VARCHAR(11),CHEQUE_DATE,103) AS CHEQUE_DATE,ASSEST_LOC_CODE," +
                " ASSEST_SUBLOC_CODE,ASSLOCNO,ASSEST_DESC,CONVERT(VARCHAR(11),PURCHASE_DATE,103) AS PURCHASE_DATE,CONVERT(VARCHAR(11),ENTRY_DATE,103) AS ENTRY_DATE,AVBL_QUANTITY,VALUE_PER_UNIT,PURCHASE_VALUE,VALUE_ASONDATE,PERDEP,DEPTYPR_NO,DEPTYPR_DESC,BOOKBAL,CLSGLBAL,STAGE from DEADSTOCK " +
            "where BRCD='" + BRCD + "'AND PRDCODE='" + PRDCODE + "'AND ACCNO='" + ACCNO + "' and Id='"+id+"' and  stage<>1004";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex); ;
        }
        return DT;
    }

    public string GetData(string FL, string code)
    {
        try
        {
             if (FL == "TOA")
            {
                sql = "select  Description from Lookupform1 where LNO='1063' and SRNO='" + code + "'";
             }
            else if (FL == "STS")
            {
                sql = "select Description from Lookupform1 where LNO='1064' and SRNO='" + code + "'";
            }
            else if (FL == "ITD")
            {
                sql = "select Description from Lookupform1 where LNO='1065' and SRNO='" + code + "'";
            }
            else if (FL == "DEP")
            {
                sql = "select Description from Lookupform1 where LNO='1066' and SRNO='" + code + "'";
            }
             else if (FL == "SNA")
             {
                 sql = "select GROUPDESC from USERGROUP where  GROUPCODE='" + code + "'";
             }
             else  if (FL == "TOI")
             {
                 sql = "SELECT ITEMNAME FROM Item_Master WHERE ITEMNO='" + code + "'";
             }
             else if (FL == "ASL")
             {
                 sql = "select Description from Lookupform1 where LNO='1067' and SRNO='" + code + "'";
             }
             result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public string AccNodisplay(string BRCD, string PRDCODE)
    {
        string val="";
        try
        {
            sql = "select accnoyn from glmast where subglcode='" + PRDCODE + "' and brcd='" + BRCD + "' and glgroup='FAF'";
              val = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
         return val;
    }

    public double GetOpenClose(string BRCD, string PRDCODE, string ACCNO, string EDate, string Flag)
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OpClBalance @BrCode = '" + BRCD + "', @SubGlCode = '" + PRDCODE + "', @AccNo = '" + ACCNO + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }

    public DataTable GetFurnitureListing(string BRCD, string ProdCode, string AccNo, string status)//Dhanya Shetty
    {
        try
        {
            sql = "EXEC D_MASTERLISTING '" + BRCD + "','" + ProdCode + "','" + AccNo + "','" + status + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public DataTable GetSubmitData(string FL)
    {
        try
        {
             if (FL == "TOA")
            {
                sql = "select SRNO, Description from Lookupform1 where LNO='1063'";
                DT = conn.GetDatatable(sql);
             }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int CheckPrevData(string BRCD, string PRD, string ACC)
    {
        try
        {
            sql = "select isnull(count(*),0) from deadstock where BRCD='" + BRCD + "' and PRDCODE='" + PRD + "' and ACCNO='" + ACC + "' and stage<>1004";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
      return Result;
     }

    public int BindGrid(GridView Gview, string brcd, string EDate, string PT)
    {
        try
        {
            if (PT == "" )
            {
                //,B.CUSTNAME AS CUSTNAME  INNER JOIN AVS_ACC A  ON A.ACCNO=L.ACCNO AND A.SUBGLCODE=L.PRDCODE AND  A.BRCD=L.BRCD " + " INNER JOIN   MASTER B ON A.CUSTNO=B.CUSTNO AND A.BRCD=B.BRCD 
                sql = "Select Id,PRDCODE,L.ACCNO  from DEADSTOCK  L where L.stage <> 1004 and L.Brcd='" + brcd + "' and L.DATE='" + conn.ConvertDate(EDate) + "' order by PRDCODE, ACCNO";
            }
            else
            {
                sql = "Select Id,PRDCODE,L.ACCNO  from DEADSTOCK  L where L.stage <> 1004 and L.Brcd='" + brcd + "'and L.PRDCODE='" + PT + "'  order by PRDCODE, ACCNO ";
            }
            res= conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return res;
    }
    public string GetStage(string BRCD, string Prdcode, string ACC)
    {
        try
        {
            sql = "SELECT STAGE FROM DEADSTOCK WHERE BRCD='" + BRCD + "' AND PRDCODE='" + Prdcode + "' and ACCNO='" + ACC + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
}