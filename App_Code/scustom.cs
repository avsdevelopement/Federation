using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data.SqlClient;

public class scustom
{
    DbConnection DBCON = new DbConnection();
    string sql = "", sResult = "";
    int Result = 0;
    string Accname = "";
    public scustom()
    {

    }

    public void BindImageType(DropDownList DDL)
    {
        string sql = "SELECT ImageType Name FROM TypeOfImage";
        BindDDL(DDL, sql);
    }

    // ----------- LOOKUP TYPE -----------
    public void BindInstruType(DropDownList ddl)
    {
        string sql = "SELECT DESCRIPTION name, SRNO id FROM LOOKUPFORM1 where LNO = 1022";
        fillAllDDL(ddl, sql);
    }
    // ----------- End LOOKUP TYPE -------------------



    // ----------- Bind Name -----------
    public void BindBankName(DropDownList ddl)
    {
        string sql = "SELECT GLNAME name, GLCODE id FROM GLMAST WHERE GLGROUP = 'CBB'";
        fillAllDDL(ddl, sql);
    }
    // ----------- End LOOKUP TYPE -------------------

    // ----------- GET IDENTITY PROOF -----------
    public void BindDdlIdentityProof(DropDownList ddl)
    {
        string sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1031' order by SRNO";
        fillAllDDL(ddl, sql);
    }
    public void BindAccountType(DropDownList ddl)
    {
        string sql = "select DESCRIPTION as NAME,SRNO as ID from LOOKUPFORM1 where LNO=1016 order by SRNO";
        fillAllDDL(ddl, sql);
    }
    public void BindInvType(DropDownList ddl)
    {
        string sql = "select GLGROUP as id,DESCRIPTION as Name from BSFORMAT where GLGROUP in('INV','REFINV','OTHINV')";
        fillAllDDL(ddl, sql);
    }
    // ----------- End IDENTITY PROOF -------------------

    // ----------- Get new ID for outward clearing ----------
    public int GetMaxOwgId()
    {
        int dbvalue = 0;
        string sql = "select MAX(OWGID) from OWG_201607";
        int getmaxid = Convert.ToInt32(DBCON.sExecuteScalar(sql));
        if (Convert.ToInt32(DBCON.sExecuteScalar(sql)) == null)
        {
            getmaxid = 0;
        }

        int newid = getmaxid + 1;
        return newid;
    }

    // -----------  Get Account 

    // ----------- Get Account holder Name from Account no -------
    public string GetAccountName(string AccNo, string SubGlCode, string brcd)
    {
        string sql = "SELECT (CONVERT(VARCHAR(10),A.CUSTNO)+'_'+CUSTNAME+'_'+Convert(VARCHAR(50),A.ACCNO)) CUSTNAME FROM AVS_ACC A INNER JOIN MASTER B ON A.CUSTNO=B.CUSTNO WHERE A.CUSTNO = B.CUSTNO AND A.ACCNO='" + AccNo + "' AND A.SUBGLCODE='" + SubGlCode + "' AND A.BRCD='" + brcd + "'";
        string Accname = DBCON.sExecuteScalar(sql);
        return Accname;
    }
    public string GetAccountNme(string AccNo, string SubGlCode, string brcd)//Dhanya Shetty // Check acc status<>3//09/08/2017
    {
        string sql = "SELECT (CONVERT(VARCHAR(10),A.CUSTNO)+'_'+CUSTNAME+'_'+Convert(VARCHAR(10),A.ACCNO)) CUSTNAME FROM AVS_ACC A INNER JOIN MASTER B ON A.CUSTNO=B.CUSTNO WHERE A.CUSTNO = B.CUSTNO AND A.ACCNO='" + AccNo + "' AND A.SUBGLCODE='" + SubGlCode + "' AND A.BRCD='" + brcd + "' and A.ACC_STATUS<>3 ";
        string Accname = DBCON.sExecuteScalar(sql);
        return Accname;
    }
    public DataTable GetAccNme(string AccNo, string SubGlCode, string brcd)//Dhanya Shetty // Check acc status<>3//09/08/2017
    {
        DataTable dt = new DataTable();
        sql = "Select (ConVert(VarChar(10), ConVert(BigInt, A.CustNo))+ '_' + CustName +'_'+ ConVert(VarChar(15), ConVert(BigInt, A.AccNo))) CustName, B.Spl_Instruction, A.Opr_Type From AVS_ACC A Inner Join Master B On A.CustNo = B.CustNo Where A.BrCd = '" + brcd + "' And A.SUBGLCODE = '" + SubGlCode + "' And A.ACCNO = '" + AccNo + "' and A.ACC_STATUS<>3";
        dt = DBCON.GetDatatable(sql);
        return dt;
    }
    public string GetCustName(string CustNo, string SubGlCode, string brcd)
    {

        string sql = "SELECT (CONVERT(VARCHAR(10),B.CUSTNO)+'_'+CUSTNAME) CUSTNAME FROM MASTER B WHERE B.CUSTNO='" + CustNo + "' And stage=1003";
        string Accname = DBCON.sExecuteScalar(sql);
        return Accname;
    }
    public DataTable GetAccName(string AccNo, string SubGlCode, string brcd)
    {
        DataTable dt = new DataTable();
        sql = "Select (ConVert(VarChar(10), ConVert(BigInt, A.CustNo))+ '_' + CustName +'_'+ ConVert(VarChar(15), ConVert(BigInt, A.AccNo))) CustName, B.Spl_Instruction, A.Opr_Type From AVS_ACC A " +
            "Inner Join Master B On A.CustNo = B.CustNo Where A.BrCd = '" + brcd + "' And A.SUBGLCODE = '" + SubGlCode + "' And A.ACCNO = '" + AccNo + "'";
        dt = DBCON.GetDatatable(sql);
        return dt;
    }
    public string GetAccName_DDSloan(string AccNo, string SubGlCode, string brcd)
    {
        try
        {
            sql = " Select B.CUSTNAME from AVS_ACC A inner join Master B on A.Custno=B.Custno where A.Brcd='" + brcd + "' and A.Subglcode='" + SubGlCode + "' and A.ACCNO='" + AccNo + "' and A.ACC_STATUS<>'3'";
            sql = DBCON.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetAccountNameINV(string AccNo, string SubGlCode, string brcd)
    {
        string sql = "SELECT (CONVERT(VARCHAR(50),A.BankName)+'_'+Convert(VARCHAR(10),A.Custaccno)) CUSTNAME FROM AVS_InvAccountMaster A WHERE A.CustAccno='" + AccNo + "' AND A.SubGlCOde='" + SubGlCode + "' AND A.BRCD='" + brcd + "'";
        string Accname = DBCON.sExecuteScalar(sql);
        return Accname;
    }

    public string CheckCustStatus(string BrCode, string CustNo)
    {
        try
        {
            sql = "Select Stage From Master Where CustNo = '" + CustNo + "'";
            sResult = DBCON.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int CheckAccount(string AC, string PT, string BRCD)
    {
        try
        {
            sql = "Select CustNo From Avs_Acc Where BrCd = '" + BRCD + "' And SubGlCode = '" + PT + "' And AccNo = '" + AC + "'";
            Result = Convert.ToInt32(DBCON.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetAccountNameDDS(string AccNo, string SubGlCode, string brcd)
    {
        string Accname = "";
        try
        {
            string sql = "SELECT (CONVERT(VARCHAR(10),A.CUSTNO)+'_'+CUSTNAME+'_'+Convert(VARCHAR(10),Convert(int,A.ACCNO))) CUSTNAME FROM AVS_ACC A " +
                    "INNER JOIN MASTER B WITH (NOLOCK) ON A.CUSTNO=B.CUSTNO " +
                    "WHERE A.ACCNO='" + AccNo + "' AND A.SUBGLCODE='" + SubGlCode + "' AND A.BRCD='" + brcd + "' and A.GLCODE='2' and A.Stage<>'1004'";

            Accname = DBCON.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return Accname;
    }
    //------------------------------------------------------------

    // ----------- Get Product NAME / GL NAME -------
    public string GetProductName(string PrdCode, string BRCD)//BRCD ADDED --Abhishek
    {
        string sql = "select ISNULL(GLNAME,'') from glmast WHERE SUBGLCODE ='" + PrdCode + "' and BRCD='" + BRCD + "'";
        string ProductName = DBCON.sExecuteScalar(sql);
        return ProductName;
    }
    //------------------------------------------------------------

    // ----------- Get AccountType Name ---------
    public string GetAccountTypeName(string AccType)
    {
        string sql = "select DESCRIPTION from lookupform1 where LNO='1016' AND SRNO='" + AccType + "'";
        string AccTypeName = DBCON.sExecuteScalar(sql);
        return AccTypeName;
    }
    // ------------------------------------------

    // ----------- Get Bank Name ---------
    public string GetBankName(string BnkCode)
    {
        string sql = "SELECT DESCR FROM RBIBANK WHERE BANKRBICD='" + BnkCode + "' and BRANCHRBICD='0' AND STATECD ='400'";
        string bankname = DBCON.sExecuteScalar(sql);
        return bankname;
    }
    // -----------------------------------

    // ----------- Get Branch Name ---------
    public string GetBranchName(string BnkCode, string BrnchCode)
    {

        string sql = "SELECT DESCR FROM RBIBANK WHERE BANKRBICD='" + BnkCode + "'and BRANCHRBICD='" + BrnchCode + "' AND STATECD ='400'";
        string branchname = DBCON.sExecuteScalar(sql);
        return branchname;
    }
    // -----------------------------------
    // ----------- Get Account Number and Account Type ---------
    public DataTable GetAccNoAccType(string PrdCode, string AccNo, string BRCD)//BRCD ADDED --Abhishek
    {
        DataTable dt = new DataTable();
        string sql = "select ACC_TYPE, OPR_TYPE, LNO1.DESCRIPTION OPRTYPE, LNO2.DESCRIPTION ACCTYPE from avs_acc ac " +
                        "LEFT JOIN (select DESCRIPTION , SRNO from lookupform1 where LNO='1017')LNO1 ON LNO1.SRNO=AC.OPR_TYPE " +
                        "LEFT JOIN (select DESCRIPTION , SRNO from lookupform1 where LNO='1016')LNO2 ON LNO2.SRNO=AC.ACC_TYPE " +
                        "where SUBGLCODE ='" + PrdCode + "' AND ACCNO='" + AccNo + "' and BRCD='" + BRCD + "'";
        dt = DBCON.GetDatatable(sql);
        return dt;
    }
    // -----------------------------------    
    // ----------- Get OpType Name ---------
    public string GetOpTypeName(string OpType)
    {
        string sql = "select DESCRIPTION from lookupform1 where LNO='1017' AND SRNO='" + OpType + "'";
        string OpTypeName = DBCON.sExecuteScalar(sql);
        return OpTypeName;
    }
    // ------------------------------------------

    public void BindFYDate(DropDownList ddlFY)
    {
        string sql = "SELECT SRNO id,DESCRIPTION name FROM tbladtLookup WHERE LNO='1010' ORDER BY SRNO";
        fillAllDDL(ddlFY, sql);
    }

    public void fillAllDDL(DropDownList ddl, string dQuery)
    {
        DataSet objDs = new DataSet();
        SqlCommand objCmd = new SqlCommand();

        try
        {
            SqlDataAdapter objDA = new SqlDataAdapter();

            objCmd.CommandText = dQuery;
            objCmd.Connection = DBCON.GetDBConnection();
            objDA.SelectCommand = objCmd;
            objDA.Fill(objDs);
            ddl.DataSource = objDs;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            //Retdt = null;
        }

        finally
        {
            objCmd.Dispose();
        }
    }

    public void BindDDL(DropDownList ddl, string dQuery)
    {
        DataSet objDs = new DataSet();
        SqlCommand objCmd = new SqlCommand();

        try
        {
            SqlDataAdapter objDA = new SqlDataAdapter();

            objCmd.CommandText = dQuery;
            objCmd.Connection = DBCON.GetDBConnection();
            objDA.SelectCommand = objCmd;
            objDA.Fill(objDs);
            ddl.DataSource = objDs;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception Ex)
        {

        }
        finally
        {
            objCmd.Dispose();
        }

    }

    public int BindClearing(GridView Gview)
    {
        sql = "select * from OWG_201607 ORDER BY OWGID DESC";
        Result = DBCON.sBindGrid(Gview, sql);
        return Result;
    }

    public int bindKyc(string BRCD, string KYC, GridView Gview)
    {
        sql = "select a.CUSTNO,A.DATEOFUPLOAD,A.PHOTO_TYPE,m.CUSTNAME from avs1011 a inner join master M On A.Custno = M.Custno WHERE A.PHOTO_TYPE='" + KYC + "' AND a.STAGE<>1004 and a.brcd='" + BRCD + "'";
        Result = DBCON.sBindGrid(Gview, sql);
        return Result;
    }

    public string GetPLAccName(string PrdCode, string brcd)//Dhanya Shetty for minimum balance
    {
        sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),SUBGLCODE) FROM GLMAST WHERE SUBGLCODE='" + PrdCode + "' AND BRCD='" + brcd + "' AND GLCODE=100 ";
        string Accname = DBCON.sExecuteScalar(sql);
        return Accname;
    }
    public string GetFAFProductName(string PrdCode, string BRCD)//Dhanya Shetty for Dead stock(to get FAF name)15-06-2017
    {
        string sql = "select ISNULL(GLNAME,'') from glmast WHERE SUBGLCODE ='" + PrdCode + "' and BRCD='" + BRCD + "' and GLGROUP='FAF'";
        string ProductName = DBCON.sExecuteScalar(sql);
        return ProductName;
    }
    public string GetItemName(string ItmCode, string BRCD)//Dhanya Shetty for item master//06-09-2017
    {
        string sql = "select ISNULL(Item_name,'') from AVS5029 WHERE Item_code ='" + ItmCode + "' and BRCD='" + BRCD + "'";
        string ProductName = DBCON.sExecuteScalar(sql);
        return ProductName;
    }
    public string GetCNameBD(string AccNo, string brcd)//Dhanya Shetty // //02/02/2018
    {
        string sql = "SELECT (CONVERT(VARCHAR(10),A.CUSTNO)+'_'+CUSTNAME+'_'+Convert(VARCHAR(50),A.ACCNO)) CUSTNAME FROM AVS_ACC A INNER JOIN MASTER B ON A.CUSTNO=B.CUSTNO WHERE A.CUSTNO = B.CUSTNO AND A.ACCNO='" + AccNo + "'   AND A.BRCD='" + brcd + "'";
        string Accname = DBCON.sExecuteScalar(sql);
        return Accname;
    }

}