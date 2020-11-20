using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsSurityDetails
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result;
   
    public ClsSurityDetails()
    {
    }
    public int InsertData(string LOANGLCODE, string LOANACCNO, string SURITY_CUSTNO, string SURITYNO, string S_NAME, string ADDRESS, string S_GENDER, string MOBILENO, string EFFECTDATE,string MID, string CUSTNO, string S_DOB)
    {
        try 
        {
            string S_TITLE = "";
            if (S_GENDER == "M")
                S_TITLE = 1.ToString();
            else if (S_GENDER == "F")
                S_TITLE = 2.ToString();
            else if (S_GENDER == "T")
                S_TITLE = 3.ToString();
            


            if (S_DOB == null)
                S_DOB = "0";

            if (MOBILENO == null)
                MOBILENO = "0";
            
        sql = "INSERT INTO SURITY (LOANGLCODE,LOANACCNO,SURITY_CUSTNO,SURITYNO,S_TITLE,S_NAME,ADDRESS,S_GENDER,MOBILENO,EFFECTDATE,STATUS,LMSTATUS,STAGE,BRCD,MID,PCMAC,CUSTNO,S_DOB) " +
             " VALUES ('" + LOANGLCODE + "','" + LOANACCNO + "','" + SURITY_CUSTNO + "','" + SURITYNO + "','" + S_TITLE + "','" + S_NAME + "','" + ADDRESS + "','" + S_GENDER + "','" + MOBILENO + "','" + conn.ConvertDate (EFFECTDATE).ToString () + "','1','1','1001','1','" + MID + "','" + conn.PCNAME() + "','" + CUSTNO + "','" + S_DOB + "')";       
        Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
       
    }


    public int UpdateData(string SURITY_CUSTNO, string S_NAME,string ADDRESS, string S_GENDER, string MOBILENO, string S_DOB, string MID, string LOANGLCODE, string LOANACCNO, string ID,string BRCD)
    {
        try //BRCD ADDED -Abhishek
        {
        //sql = "UPDATE SURITY SET SURITY_CUSTNO='" + SURITY_CUSTNO + "',SURITYNO='1',S_TITLE='" + S_TITLE + "',S_NAME='" + S_NAME + "',SF_NAME='" + SF_NAME + "',SM_NAME='" + SM_NAME + "',SL_NAME='" + SL_NAME + "',ADDRESS='" + ADDRESS + "',S_GENDER='" + S_GENDER + "',MOBILENO='" + MOBILENO + "',STAGE='1002',S_DOB='" + S_DOB + "',CID='" + MID + "' WHERE LOANGLCODE='" + LOANGLCODE + "' AND LOANACCNO='" + LOANACCNO + "' AND ID='" + ID + "'";
            string S_TITLE = "";
            if (S_GENDER == "M")
                S_TITLE = 1.ToString();
            else if (S_GENDER == "F")
                S_TITLE = 2.ToString();
            else if (S_GENDER == "T")
                S_TITLE = 3.ToString();

            sql = "UPDATE SURITY SET SURITY_CUSTNO='" + SURITY_CUSTNO + "', SURITYNO='1',S_TITLE='" + S_TITLE + "',S_NAME='" + S_NAME + "',ADDRESS='" + ADDRESS + "',S_GENDER='" + S_GENDER + "', MOBILENO='" + MOBILENO + "', STAGE='1002', S_DOB='" + conn.ConvertDate(S_DOB) + "', CID='" + MID + "' WHERE LOANGLCODE='" + LOANGLCODE + "' AND LOANACCNO='" + LOANACCNO + "' AND ID='" + ID + "'AND MID<>'" + MID + "' and BRCD='" + BRCD + "'";
        Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }

    public int DelandAutho(string LOANGLCODE, string LOANACCNO, string ID, string FL,string MID,string BRCD)
    {
        try //BRCD ADDED --Abhishek
        {
        if (FL == "DL")
        {
            sql = "UPDATE SURITY SET STAGE='1004' WHERE LOANGLCODE='" + LOANGLCODE + "' AND LOANACCNO='" + LOANACCNO + "' AND ID='" + ID + "' AND MID<>'" + MID + "' and BRCD='" + BRCD + "'";
        }
        else if(FL=="AT")
        {
            sql = "UPDATE SURITY SET STAGE='1003' WHERE LOANGLCODE='" + LOANGLCODE + "' AND LOANACCNO='" + LOANACCNO + "' AND ID='" + ID + "' AND MID<>'" + MID + "' and BRCD='" + BRCD + "'";
        }
        Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }


    public int BindSurity(GridView Gview, string LOANGLCODE, string LOANACCNO, string SUBGLCODE, string FL, string BRCD) //BRCD ADDED --Abhishek
    {
        int Result = 0;
        try 
        {
            if(FL!="VW")
            {
                sql = "SELECT S.ID,GL.GLNAME,S.LOANGLCODE,S.LOANACCNO,S_NAME,S.SURITY_CUSTNO,S_GENDER,MOBILENO,S_DOB,ADDRESS FROM SURITY S INNER JOIN GLMAST GL ON GL.SUBGLCODE=S.LOANGLCODE AND GL.BRCD=S.BRCD WHERE S.LMSTATUS=1 AND S.STAGE<>1003 AND S.STAGE<>1004 and S.BRCD='" + BRCD + "'";
                // sql = "SELECT LOANACCNO,S_NAME,MOBILENO,S_DOB,ADDRESS FROM SURITY S INNER JOIN GLMAST GL ON GL.SUBGLCODE=S.LOANGLCODE AND GL.BRCD=S.BRCD WHERE S.LOANGLCODE='" + LOANGLCODE + "' AND S.LOANACCNO='" + LOANACCNO + "' AND S.STAGE<>1004 AND S.LMSTATUS=1";
                Result = conn.sBindGrid(Gview, sql);
            }
            else
            {
                sql = "SELECT S.ID,GL.GLNAME,S.LOANGLCODE,S.LOANACCNO,S_NAME,S.SURITY_CUSTNO,S_GENDER,MOBILENO,S_DOB,ADDRESS FROM SURITY S INNER JOIN GLMAST GL ON GL.SUBGLCODE=S.LOANGLCODE AND GL.BRCD=S.BRCD WHERE S.LMSTATUS=1 AND S.STAGE<>1004 and S.BRCD='" + BRCD + "'";
                // sql = "SELECT LOANACCNO,S_NAME,MOBILENO,S_DOB,ADDRESS FROM SURITY S INNER JOIN GLMAST GL ON GL.SUBGLCODE=S.LOANGLCODE AND GL.BRCD=S.BRCD WHERE S.LOANGLCODE='" + LOANGLCODE + "' AND S.LOANACCNO='" + LOANACCNO + "' AND S.STAGE<>1004 AND S.LMSTATUS=1";
                Result = conn.sBindGrid(Gview, sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }



    public string CheckStage(string ID, string BRCD) //BRCD ADDED --Abhishek
    {
        try 
        {
            sql = "SELECT STAGE FROM SURITY WHERE ID='" + ID + "' and BRCD='" + BRCD + "'";
        ID = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return ID;
    }
    public DataTable GetInfo(string FL, string LGL, string LACCNO, string BRCD) //BRCD ADDED --Abhishek
    {
        DataTable DT = new DataTable();
        try 
        {
            if (FL != "VW")
            {

                sql = "SELECT * FROM SURITY where BRCD='" + BRCD + "'";
            }
            else
            {
                sql = "SELECT S.*,G.GLNAME FROM SURITY S INNER JOIN GLMAST G ON S.LOANGLCODE=G.SUBGLCODE WHERE S.LOANGLCODE='" + LGL + "' AND S.LOANACCNO='" + LACCNO + "' and S.BRCD='" + BRCD + "'";
                
            }
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }


    public DataTable GetGDetails(string CN,string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = " SELECT MM.CUSTNAME," +
                "(CASE WHEN MM.dob<>NULL THEN CONVERT(VARCHAR,MM.DOB,111) ELSE '1990/01/01' END)DOB," +
                   " MM.CUSTSEX," +
                   "(CASE WHEN AD.ADDRESS<>NULL THEN (AD.ADDRESS+','+AD.CITY+','+AD.STATE+','+AD.PINCODE) ELSE 'N/A' END)ADDRESS," +
                   "(CASE WHEN AC.MOBILE1<>NULL THEN AC.MOBILE1 ELSE 0 END)MOBILE1," +
                   "(CASE WHEN MM.MARRITALSTATUS<>NULL THEN MM.MARRITALSTATUS ELSE 0 END)MARRITALSTATUS " +
                   "FROM MASTER MM  " +
                   "LEFT JOIN ADDMAST AD ON MM.CUSTNO=AD.CUSTNO " +
                   " LEFT JOIN AVS_CONTACTD AC ON AD.CUSTNO=AC.CUSTNO " +
                   " WHERE MM.BRCD='" + BRCD + "' AND MM.STAGE<>1004 AND MM.CUSTNO='" + CN + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public int BindRDCal(GridView GRD, string brcd, string accno, string prcode)
    {
        try
        {
            sql = "select s.LnSrNo as SRNO,s.MemberNo as Surity,s.LnSrName as Name,a.FLAT_ROOMNO as address from AVSLnSurityTable  s left join addmast a on  s.MemberNo=a.custNo and S.brcd=a.brcd  and a.addtype=1   WHERE s.BRCD='" + brcd + "' AND s.SubGlCode=" + prcode + " AND s.AccNo='" + accno + "' and s.LnType='Surity' ";
            Result = conn.sBindGrid(GRD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return Result;
    }
    public GridView grdSurity { get; set; }
}