using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;

public class ClsLogin
{
    DbConnection conn = new DbConnection();
    string sql;
    int Result;
    DataTable DT,DT1;

    public ClsLogin()
    {
    }

    public string GetSessionTime()
    {
        string Time = "";
        try
        {
            sql = "select ListValue from Parameter where LISTFIELD='SessionTime'";
            Time = conn.sExecuteScalar(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Time;
    }

    public DataTable GetDetaile(string LoginId, string Pass)
    {
        try
        {
            if (Pass == "")
            {
                sql = "Select B.BankCD As BankCode, B.BankName, B.MidName As BranchName, " +
                      "(Select IsNull(BrCd, 0) From BankName Where BranchDesc = 'HO') As HOBRCD, U.BrCd, " +
                      "Left(U.UserName, Case When CharIndex(' ', U.UserName) > 0 Then CharIndex(' ', U.UserName) Else Len(U.UserName) End) As UserName, " +
                      "U.LoginCode, U.PermissionNo, U.UserGroup, U.MultiLog, U.EPassWord From UserMaster U " +
                      "Inner Join BankName B With(NoLock) On U.BrCd = B.BrCd " +
                      "Where U.LoginCode = '" + LoginId + "' ";
            }
            else
            {
                sql = "Select B.BankCD As BankCode, B.BankName, B.MidName As BranchName, " +
                      "(Select IsNull(BrCd, 0) From BankName Where BranchDesc = 'HO') As HOBRCD, U.BrCd, " +
                      "Left(U.UserName, Case When CharIndex(' ', U.UserName) > 0 Then CharIndex(' ', U.UserName) Else Len(U.UserName) End) As UserName, " +
                      "U.LoginCode, U.PermissionNo, U.UserGroup, U.MultiLog, U.EPassWord From UserMaster U " +
                      "Inner Join BankName B With(NoLock) On U.BrCd = B.BrCd " +
                      "Where U.LoginCode = '" + LoginId + "' And EPassword = '" + Pass + "' ";
            }
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetDetaile1(string LoginId, string Pass, string BRCD)
    {
        try
        {
            sql = "exec SP_ChangeBranch @LoginId='" + LoginId + "',@Pass='" + Pass + "',@BRCD='" + BRCD + "'";//amruta24/04/2017
           
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string CheckLoginStatus(string LoginId, string Pass)
    {
        try
        {
            sql = "Select (Case When UserGroup <> '1' Then UserStatus Else '0' End) As UserStatus From UserMaster "+
                  "Where LoginCode = '" + LoginId + "' And EPassword = '" + Pass + "'" ;
            Pass = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Pass;
    }

    public int UpdateLoginsts(string LoginId, string Pass,string OC,string BRCD)
    {
        try
        {
            sql = "Update Usermaster SET UserStatus = '" + OC + "' Where LoginCode = '" + LoginId + "' AND EPassword = '" + Pass + "' And BrCd = '" + BRCD + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable getpassword()
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select * from UserMaster where EPASSWORD is null";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public void updatePassword(string UserName, string Password, string EPassword)
    {
        try
        {
            sql = "update UserMaster set Epassword='" + EPassword + "' where Logincode='" + UserName + "' and password='" + Password + "'";
            conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public string openDay(string BRCD)
    {
        string wdt = "";
        try
        {
            sql = "Select ListValue From Parameter Where ListField = 'DayOpen' And BrCd = '" + BRCD + "' ";
            wdt = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return wdt;
    }

    public DataTable GetBankName(string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "select * from  (select bankname from Bankname where brcd=0) main,(select MIDNAME branchName from Bankname where brcd='" + BRCD + "') Bank";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetBankNameDT(string FBC)
    {
        DataTable DT = new DataTable();
        try
        {
            if (FBC.Trim() != "0000")
                sql = "select MIDNAME BrName from Bankname where brcd='" + FBC + "'";
            else
                sql = "select 'Consolidated' BrName";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable getaddBankno(string BRCD) //Reg no and Date added by Abhishek 
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT Address1+','+ADDRESS2 as Address2,MOBILE,(select Address1+','+ADDRESS2 from bankname where brcd=1)as bankadd,REGISTRATIONNO,BANKNAME_MAR,regno,Dateyear FROM Bankname WHERE BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetGlname(string BRCD,string subglcode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "select GLNAME from glmast where brcd='"+BRCD+"' and subglcode='"+subglcode+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable getaddregno(string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT Address1+','+ADDRESS2 as Address2,REGISTRATIONNO FROM Bankname WHERE BRCD=1";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetBankNameATTACH(string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = " select convert(varchar(100),BANKNAME)+'_'+convert(varchar(100),BRANCHNAME) BANKNAME,BANKCD from  " +
                 " (select bankname,BANKCD from Bankname where brcd=0) main,(select MIDNAME branchName from Bankname where brcd='" + BRCD + "') Bank ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetBankNameDetails(string BranchID)
    {
        DataTable DT = new DataTable();
        try
        {
            if (BranchID.Trim() != "0000")
            {
                sql = "select MIDNAME branchName from Bankname where brcd='" + BranchID + "'";
            }
            else
            {
                sql = "select 'Consolidated' branchName";
            }
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public void CheckMonthTable(string EDT)
    {
        try
        {
            string[] E = EDT.Split('/');
            string AVSM, AVSB;
            AVSM = "AVSM_" + E[2].ToString() + E[1].ToString();
            AVSB = "AVSB_" + E[2].ToString() + E[1].ToString();

            sql = "SELECT * FROM dba_tables where table_name = '" + AVSM + "'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                return;
            }
            else
            {
                string Oracle = "CREATE TABLE " + AVSM + "(AID NUMBER NOT NULL,ENTRYDATE DATE NULL,POSTINGDATE DATE NULL,FUNDINGDATE DATE NULL,GLCODE NUMBER NULL,SUBGLCODE NUMBER NULL,ACCNO NUMBER(15,0) NULL,PARTICULARS VARCHAR2(255) NULL, PARTICULARS2 VARCHAR2(255) NULL,Amount NUMBER(13,2) NULL,Amount_1 VARCHAR2(255)  NULL,TRXTYPE NUMBER NULL,ACTIVITY NUMBER NULL,PMTMODE VARCHAR2(10) NULL,SETNO NUMBER NULL,SCROLLNO NUMBER NULL,INSTRUMENTNO NUMBER NULL,INSTRUMENTDATE DATE NULL,INSTBANKCD NUMBER NULL,INSTBRCD NUMBER NULL,STAGE NUMBER NULL,RTIME DATE NULL,BRCD NUMBER NULL,MID VARCHAR(50) NULL,CID VARCHAR(50) NULL,VID VARCHAR(50) NULL,PCMAC VARCHAR2(100) NULL,PAYMAST VARCHAR2(50) NULL,CUSTNO NUMBER(13,2) NULL,CUSTNAME VARCHAR2(100) NULL,REFID VARCHAR2(15) NULL,SYSTEMDATE DATE NULL)";
                SqlCommand cmdoracle = new SqlCommand(Oracle, conn.GetDBConnection());
                int RC = cmdoracle.ExecuteNonQuery();

                Oracle = "";
                RC = 0;
                Oracle = "CREATE SEQUENCE " + AVSM + "_seq";
                cmdoracle = new SqlCommand(Oracle, conn.GetDBConnection());
                RC = cmdoracle.ExecuteNonQuery();

                Oracle = "";
                RC = 0;
                Oracle = "CREATE OR REPLACE TRIGGER " + AVSM + " " +
                          "BEFORE INSERT ON " + AVSM + "  " +
                          "FOR EACH ROW  WHEN (new.AID IS NULL) " +
                          "BEGIN  SELECT " + AVSM + "_seq.NEXTVAL  INTO   :new.AID  FROM   dual; END;";
                cmdoracle = new SqlCommand(Oracle, conn.GetDBConnection());
                RC = cmdoracle.ExecuteNonQuery();
            }

            sql = "SELECT * FROM dba_tables where table_name = '" + AVSB + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                return;
            }
            else
            {
                string Oracle = Oracle = "CREATE TABLE " + AVSB + "(BID NUMBER NOT NULL,GLCODE NUMBER,SUBGLCODE NUMBER NULL,ENTRYDATE DATE,ACCNO NUMBER NULL,TRXTYPE NUMBER NULL,AMOUNT number(15, 2) NULL,  STAGE   number null,  MID varchar(50) null,  CID varchar(50) null,  VID varchar(50) null,PCMAC varchar(50) null, SYSTEMDATE date null,BRCD NUMBER NULL  ) ";
                SqlCommand cmdoracle = new SqlCommand(Oracle, conn.GetDBConnection());
                int RC = cmdoracle.ExecuteNonQuery();

                Oracle = "";
                RC = 0;
                Oracle = "CREATE SEQUENCE " + AVSB + "_seq";
                cmdoracle = new SqlCommand(Oracle, conn.GetDBConnection());
                RC = cmdoracle.ExecuteNonQuery();

                Oracle = "";
                RC = 0;
                Oracle = "CREATE OR REPLACE TRIGGER " + AVSB + " " +
                          "BEFORE INSERT ON " + AVSB + "  " +
                          "FOR EACH ROW  WHEN (new.AID IS NULL) " +
                          "BEGIN  SELECT " + AVSB + "_seq.NEXTVAL  INTO   :new.AID  FROM   dual; END;";
                cmdoracle = new SqlCommand(Oracle, conn.GetDBConnection());
                RC = cmdoracle.ExecuteNonQuery();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public int RealizedUser(string UserId, string BRCD)
    {
        try
        {
            //sql = "Update UserMaster Set UserStatus = '0' Where BrCd = '" + BRCD + "' And MultiLog <> 'Y' And UserStatus = '1'";

            //  Commented and added by amol on 02/10/2018 (bcoz user release issue)
            sql = "Update UserMaster Set UserStatus = (Case When UserGroup <> '1' Then '0' Else '1' End) Where LoginCode = '" + UserId + "' And UserStatus = '1' ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable DashboardDetails(string BrCd, string EntryDate, string Flag)
    {
        try
        {
            sql = "Exec SP_DashBoardDetails '" + BrCd + "','" + conn.ConvertDate(EntryDate).ToString() + "','" + Flag + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable DashboardDetailsMaturity(string BrCd, string EntryDate, string Flag)//Dhanya Shetty
    {
        try
        {
            sql = "Exec SP_DashBoardDetails '" + BrCd + "','" + conn.ConvertDate(EntryDate).ToString() + "','" + Flag + "'";
            DT1 = new DataTable();
            DT1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT1;
    }

    public int Getinfotable(GridView Gview, string BrCd, string EntryDate, string Flag)
    {
        try
        {
            sql = "Exec SP_DashBoardDetails '" + BrCd + "','" + conn.ConvertDate(EntryDate).ToString() + "','" + Flag + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetUserGroup(string BRCD, string Logincode)
    {
        try
        {
            sql = "SELECT USERGROUP FROM USERMASTER WHERE LOGINCODE='" + Logincode + "' AND BRCD='" + BRCD + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetPath(string FrmName)
    {
        try
        {
            sql = "EXEC SP_MENU_OPERATIONS @FLAG='PATH',@FRM_NAME='" + FrmName + "'";
            FrmName = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return FrmName;
    }

    public DataTable InseryMAC(string Address,string a)// amruta 24/04/2017
    {
        try
        {
            sql = "Select * from MAC where MAC='" + Address + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public void GetMAC(string Address,string a)// amruta 24/04/2017
    {
        try
        {
            sql = "insert into MAC (MAC) values ('" + Address + "')";
            conn.sExecuteQuery(sql);
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DataTable CheckMAC(string Address)//amruta 24/04/2017
    {
        try
        {
            sql = "Select * from MAC where MAC='" + Address + "' and Status=1";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string getdate(string Date)
    {
        string Date1 = "";
        try
        {
            sql = "select convert(varchar(10),getdate(),121) where convert(varchar(10),getdate(),121)<'"+conn.ConvertDate(Date)+"'";
            Date1 = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Date1;
    }

    public int GetBackup(string Name)
    {
        int i = 0;
        try
        {
            sql = "exec SP_BACKUP '"+Name+"'";
            i = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            i = 0;
            ExceptionLogging.SendErrorToText(ex);
        }
        return i;
    }

    public string CheckLoginStage(string LoginId, string Pass)
    {
        try
        {
            sql = "Select stage From UserMaster Where LoginCode = '" + LoginId + "' And EPassword = '" + Pass + "'";
            Pass = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Pass;
    }

    public DataTable Getinfotabledt(string BrCd)
    {
        DataTable dtnew = new DataTable();
        try
        {
            sql = "SELECT DISTINCT(SUBGLCODE) FROM GLMAST WHERE GLCODE='5' AND BRCD='"+BrCd+"'  order by SUBGLCODE";
            dtnew = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dtnew;
    }
    public DataTable DashboardDetails_INST(string BrCd, string EntryDate, string FDate, string Flag)
    {
        try
        {
            sql = "Exec SP_LoanInst_DASH '" + BrCd + "','" + conn.ConvertDate(EntryDate).ToString() + "','" + conn.ConvertDate(FDate).ToString() + "','" + Flag + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

}