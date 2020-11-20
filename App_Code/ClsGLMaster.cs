using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsGLMaster
{
    string sql = "";
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    int Result;

    public ClsGLMaster()
    {
    }

    public int CheckGlExist(int CLID, int Glcode)
    {
        try
        {
            sql = "SELECT * FROM  tbl_HSGLMAST WHERE CLIENTID=" + CLID + " AND GLCODE=" + Glcode;
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                return Result = -1;
            }
            else
            {
                return Result = 1;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetGlcode(string CLID)
    {
        try
        {
            sql = "SELECT ISNULL(MAX(SUBGLCODE),100)+1 FROM GLMAST WHERE  BRCD=" + CLID;
            CLID = conn.sExecuteScalar(sql);
            if (Convert.ToInt32(CLID) <= 100)
            {
                CLID = "101";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return CLID;
    }

    public string GETGlName(string SUBGL)
    {

        sql = "select GL id from gl_enquiry where GL not in (1,7,8,2,6,22,4) and gl='" + SUBGL + "'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }


    public string GETCurrentPLName(string GlName)
    {

        sql = "select SRNO  from plformat where PLTYPE='E' and  PLGROUP ='" + GlName + "'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }


    public string GetDeposit(string BRCD)
    {
        try
        {
            sql = "SELECT ISNULL(MAX(SUBGLCODE),300)+1 FROM GLMAST WHERE  BRCD='" + BRCD + "' AND GLCODE='5'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }

    public string GetLoan(string BRCD)
    {
        try
        {
            sql = "SELECT ISNULL(MAX(SUBGLCODE),200)+1 FROM GLMAST WHERE  BRCD='" + BRCD + "' AND GLCODE='3'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }

    public string GetDDS(string BRCD)
    {
        try
        {
            sql = "SELECT ISNULL(MAX(SUBGLCODE),1000)+1 FROM GLMAST WHERE  BRCD='" + BRCD + "' AND GLCODE='2'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }

    public string GetOtherGL(string BRCD)
    {
        try
        {
            sql = "SELECT MAX(SUBGLCODE)+1 FROM GLMAST WHERE  BRCD='" + BRCD + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }

    public int InsertOpening(string BRCD, string MID, string TRX, string glcode, string glname, string glgroup, string GLBALANCE, string Subglcode, string EDT)
    {
        try
        {
            string[] TD = EDT.Split('/');
            string TBNAME = "";
            TBNAME = TD[2].ToString() + TD[1].ToString();
            sql = "insert into glmast(glcode,glname,glgroup,GLBALANCE,Glpriority,Subglcode,Brcd,BTYPE) values " +
                "('" + glcode + "','" + glname + "','" + glgroup + "','" + GLBALANCE + "','1','" + Subglcode + "','" + BRCD + "','" + TRX + "')";
            Result = conn.sExecuteQuery(sql);
            if (glcode == "3")
            {
                glname = "Receivable Int. On " + glname;
                sql = "insert into glmast(glcode,glname,glgroup,GLBALANCE,Glpriority,Subglcode,Brcd,BTYPE) values " +
                "('11','" + glname + "','IR','" + GLBALANCE + "','1','" + Subglcode + "','" + BRCD + "','" + TRX + "')";
                Result = conn.sExecuteQuery(sql);

                sql = "insert into glmast(glcode,glname,glgroup,GLBALANCE,Glpriority,Subglcode,Brcd,BTYPE) values " +
                "('12','" + glname + "','IR','" + GLBALANCE + "','1','" + Subglcode + "','" + BRCD + "','" + TRX + "')";
                Result = conn.sExecuteQuery(sql);
            }
            else if (glcode == "5")
            {
                glname = "Payable Int. On " + glname;
                sql = "insert into glmast(glcode,glname,glgroup,GLBALANCE,Glpriority,Subglcode,Brcd,BTYPE) values " +
                           "('10','" + glname + "','" + glgroup + "','" + GLBALANCE + "','1','" + Subglcode + "','" + BRCD + "','" + TRX + "')";
                Result = conn.sExecuteQuery(sql);
            }
            if (Result > 0)
            {
                if (TRX == "2")
                {
                    GLBALANCE = "-" + GLBALANCE;
                }
                if (glcode != "3" || glcode != "5")
                {
                    sql = "INSERT INTO AVSB_" + TBNAME + "(GLCODE,SUBGLCODE,ENTRYDATE,ACCNO,TRXTYPE,AMOUNT,STAGE,MID,CID,VID,PCMAC,SYSTEMDATE,BRCD)" +
                     " VALUES('" + glcode + "','" + Subglcode + "','" + conn.ConvertDate(EDT).ToString() + "','0','3','" + GLBALANCE + "','1001','" + MID + "','0','0','" + conn.PCNAME() + "','" + conn.ConvertDate(DateTime.Now.Date.ToString("dd/MM/yyyy")).ToString() + "','" + BRCD + "')";
                    Result = conn.sExecuteQuery(sql);
                    if (Result > 0)
                    {
                        sql = "INSERT INTO AVSBG_" + TBNAME + "(GLCODE,SUBGLCODE,ENTRYDATE,ACCNO,TRXTYPE,AMOUNT,STAGE,MID,CID,VID,PCMAC,SYSTEMDATE,BRCD)" +
                                        " VALUES('" + glcode + "','" + Subglcode + "','" + conn.ConvertDate(EDT).ToString() + "','0','3','" + GLBALANCE + "','1001','" + MID + "','0','0','" + conn.PCNAME() + "','" + conn.ConvertDate(DateTime.Now.Date.ToString("dd/MM/yyyy")).ToString() + "','" + BRCD + "')";
                        Result = conn.sExecuteQuery(sql);
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int BindGL(string BRCD, GridView Gview)
    {
        try
        {
            sql = "select GLBALANCE,glcode,Subglcode,glname,glgroup,(case when btype='1' then 'CR' when btype='2' then 'DR' END) TYPE from glmast where brcd='" + BRCD + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string getsubglcode1(string glcode, string brcd)
    {
        string gl = "";
        try
        {
            sql = "EXEC Isp_AVS0036 '" + glcode + "','" + brcd + "' ";
            gl = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return gl;
    }




    public int InsertGlCategory(string Category, string GlCode)
    {
        
        try
        {
            sql = "exec SpGlMastDropDown @Flag='Insert', @GlCode= '" + GlCode + "', @LoanCategory='" + Category + "' ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            Result = 0;
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public DataTable getsubglcode(string glcode, string brcd)
    {
        DataTable DT = new DataTable();
        sql = "select max(subglcode)+1 AS SUBGLCODE,max(placcno) as PLACCNO from glmast where  glcode='" + glcode + "' and  brcd='" + brcd + "' ";
        DT = conn.GetDatatable(sql);
        return DT;
    }

    public DataTable getglno(string brcd)
    {
        DataTable DT = new DataTable();
        sql = "SELECT MAX(GLCODE)+1 AS GLCODE FROM GLMAST WHERE BRCD='" + brcd + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }

    public string getrecint(string glcode, string Subglcode)
    {
        string result = "";
        try
        {
            sql = "exec Isp_AVS0037 '" + glcode + "','" + Subglcode + "'";
            result = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public int InsertMainGL(string BrCode, string GlCode, string PrCode, string GlName, string GlGroup, string GlPriority, string PlAccNo, string LastNo, string IR, string IOR,
        string Marathi, string IntAccYN, string CSHDr, string CSHCr, string TRFDr, string TRFCr, string CLGDr, string CLGCr, string UnOperate, string ImplDate, string OpenBal, string fontstyle)
    {
        try
        {
            sql = "Insert Into GlMast (BRCD, GLCODE, SUBGLCODE, GLNAME, GLGROUP, GLPRIORITY, PLACCNO, LASTNO, IR, IOR, GLMarathi, INTACCYN,ACCNOYN ,CASHDR, CASHCR, TRFDR, TRFCR, CLGDR, CLGCR, UnOperate, ImplimentDate, OpeningBal,GLMarathiFonttype) " +
                  "Values('" + BrCode + "', '" + GlCode + "', '" + PrCode + "', '" + GlName + "', '" + GlGroup + "', '" + GlPriority + "', '" + PlAccNo + "', '" + LastNo + "', '" + IR + "', '" + IOR + "', N'" + Marathi + "', " +
                  "'" + IntAccYN + "','" + IntAccYN + "', '" + CSHDr + "', '" + CSHCr + "', '" + TRFDr + "', '" + TRFCr + "', '" + CLGDr + "', '" + CLGCr + "', '" + UnOperate + "', '" + conn.ConvertDate(ImplDate) + "', '" + OpenBal + "','" + fontstyle + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int InsertMainPL(string BrCode, string GlCode, string PrCode, string GlName, string GlGroup, string PlGroup, string GlPriority, string PlAccNo, string LastNo, string IR, string IOR,
        string Marathi, string IntAccYN, string CSHDr, string CSHCr, string TRFDr, string TRFCr, string CLGDr, string CLGCr, string UnOperate, string ImplDate, string OpenBal, string fontstyle)
    {
        try
        {
            sql = "Insert Into GlMast (BRCD, GLCODE, SUBGLCODE, GLNAME, GLGROUP, PLGROUP, GLPRIORITY, PLACCNO, LASTNO, IR, IOR, GLMarathi, INTACCYN, ACCNOYN,CASHDR, CASHCR, TRFDR, TRFCR, CLGDR, CLGCR, UnOperate, ImplimentDate, OpeningBal,GLMarathiFonttype) " +
                  "Values('" + BrCode + "', '" + GlCode + "', '" + PrCode + "', '" + GlName + "', '" + GlGroup + "', '" + PlGroup + "', '" + GlPriority + "', '" + PlAccNo + "', '" + LastNo + "', '" + IR + "', '" + IOR + "', N'" + Marathi + "', " +
                  "'" + IntAccYN + "', '" + IntAccYN + "','" + CSHDr + "', '" + CSHCr + "', '" + TRFDr + "', '" + TRFCr + "', '" + CLGDr + "', '" + CLGCr + "', '" + UnOperate + "', '" + conn.ConvertDate(ImplDate) + "', '" + OpenBal + "','" + fontstyle + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    //Dhanya Shetty//14/02/2018//Last no must be updated in current brcd only 
    //public int INSERTPL(string brcd, string GLCODE, string SUBGLNAME, string GLGOUP, string SUBCODE, string PLACCNO, string LASTNO, string GLPRIORITY, string Marathi, string ImplDate, string OpenBal)
    //{
    //    try
    //    {
    //        sql = "INSERT INTO GLMAST (GLCODE,GLNAME,GLGROUP,GLPRIORITY,SUBGLCODE,BRCD,PLACCNO,LASTNO,GLMarathi, ImplimentDate, OpeningBal) " +
    //          "VALUES('" + GLCODE + "','" + SUBGLNAME + "','" + GLGOUP + "','" + GLPRIORITY + "','" + SUBCODE + "','" + brcd + "', " +
    //          "'" + PLACCNO + "','" + LASTNO + "',N'" + Marathi + "', '" + conn.ConvertDate(ImplDate) + "', '" + OpenBal + "')";
    //        Result = conn.sExecuteQuery(sql);
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return Result;
    //}

    //public int INSERTPL1(string brcd, string GLCODE, string SUBGLNAME, string GLGOUP, string SUBCODE, string PLACCNO, string LASTNO, string GLPRIORITY, string Marathi, string PLGROUP, string ImplDate, string OpenBal)
    //{
    //    try
    //    {
    //        sql = "INSERT INTO GLMAST (GLCODE,GLNAME,GLGROUP,GLPRIORITY,SUBGLCODE,BRCD,PLACCNO,LASTNO,GLMarathi,PLGROUP, ImplimentDate, OpeningBal) " +
    //          "VALUES('" + GLCODE + "','" + SUBGLNAME + "','" + GLGOUP + "','" + GLPRIORITY + "','" + SUBCODE + "','" + brcd + "','" + PLACCNO + "', " +
    //          "'" + LASTNO + "',N'" + Marathi + "','" + PLGROUP + "', '" + conn.ConvertDate(ImplDate) + "', '" + OpenBal + "')";
    //        Result = conn.sExecuteQuery(sql);
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return Result;
    //}

    public int UpdateMainGL(string BrCode, string GlCode, string PrCode, string GlName, string GlGroup, string GlPriority, string PlAccNo, string LastNo,
        string IR, string IOR, string Marathi, string IntAccYN, string CSHDr, string CSHCr, string TRFDr, string TRFCr, string CLGDr, string CLGCr,
        string UnOperate, string ImplDate, string OpenBal, string CBrCode, string IsMultiApply, string fontstyle)
    {
        try
        {
            sql = "Update GlMast Set GLNAME = '" + GlName + "', GLGROUP = '" + GlGroup + "', GLPRIORITY = '" + GlPriority + "', PLACCNO = '" + PlAccNo + "', LASTNO = '" + LastNo + "', " +
                  "IR = '" + IR + "', IOR = '" + IOR + "', GLMarathi = 'N''" + Marathi + "', INTACCYN = '" + IntAccYN + "' ,ACCNOYN='" + IntAccYN + "' , CASHDR = '" + CSHDr + "', CASHCR = '" + CSHCr + "', " +
                  "TRFDR = '" + TRFDr + "', TRFCR = '" + TRFCr + "', CLGDR = '" + CLGDr + "', CLGCR = '" + CLGCr + "', UnOperate = '" + UnOperate + "', " +
                  "ImplimentDate = '" + conn.ConvertDate(ImplDate) + "', OpeningBal = '" + OpenBal + "',GLMarathiFonttype='" + fontstyle + "'";

            if (IsMultiApply.Equals("Y"))
            {
                sql += "Where GLCODE = '" + GlCode + "' And SUBGLCODE = '" + PrCode + "' ";
            }
            else
            {
                sql += "Where BRCD = '" + BrCode + "' And GLCODE = '" + GlCode + "' And SUBGLCODE = '" + PrCode + "' ";
            }


            // "Update GlMast Set LASTNO = '0' Where BRCD = '" + CBrCode + "' And GLCODE = '" + GlCode + "' And SUBGLCODE = '" + PrCode + "' ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int UpdateMainPL(string BrCode, string GlCode, string PrCode, string GlName, string GlGroup, string PlGroup, string GlPriority, string PlAccNo, string LastNo,
        string IR, string IOR, string Marathi, string IntAccYN, string CSHDr, string CSHCr, string TRFDr, string TRFCr, string CLGDr, string CLGCr,
        string UnOperate, string ImplDate, string OpenBal, string CBrCode, string IsMultiApply, string fontstyle)
    {
        try
        {
            sql = "Update GlMast Set GLNAME = '" + GlName + "', GLGROUP = '" + GlGroup + "', PLGROUP = '" + PlGroup + "', GLPRIORITY = '" + GlPriority + "', PLACCNO = '" + PlAccNo + "', " +
                  "LASTNO = '" + LastNo + "', IR = '" + IR + "', IOR = '" + IOR + "', GLMarathi = '" + Marathi + "', INTACCYN = '" + IntAccYN + "', ACCNOYN='" + IntAccYN + "',CASHDR = '" + CSHDr + "', " +
                  "CASHCR = '" + CSHCr + "', TRFDR = '" + TRFDr + "', TRFCR = '" + TRFCr + "', CLGDR = '" + CLGDr + "', CLGCR = '" + CLGCr + "', " +
                  "UnOperate = '" + UnOperate + "', ImplimentDate = '" + conn.ConvertDate(ImplDate) + "', OpeningBal = '" + OpenBal + "',GLMarathiFonttype='" + fontstyle + "'";
            if (IsMultiApply.Equals("Y"))
            {
                sql += "Where GLCODE = '" + GlCode + "' And SUBGLCODE = '" + PrCode + "' ";
            }
            else
            {
                sql += "Where BRCD = '" + BrCode + "' And GLCODE = '" + GlCode + "' And SUBGLCODE = '" + PrCode + "' ";
            }


            //"Update GlMast Set LASTNO = '0' Where BRCD = '" + CBrCode + "' And GLCODE = '" + GlCode + "' And SUBGLCODE = '" + PrCode + "' ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    //Dhanya Shetty//14/02/2018//Last no must be updated in current brcd only 
    //public int UPDATEGL(string GLNAME, string PLACCNO, string LASTNO, string GLPRIORITY, string GlGroup, string BRCD, string SUBGLCODE, string GLCODE, string Marathi, string CBrcd, string ImplDate, string OpenBal)
    //{
    //    try
    //    {
    //        //  Modified by amol on 20/02/2018 for implimentdate and opening Balance
    //        sql = "UPDATE GLMAST SET GLNAME='" + GLNAME + "',GLGROUP='" + GlGroup + "',PLACCNO='" + PLACCNO + "',GLPRIORITY='" + GLPRIORITY + "',GLMarathi=N'" + Marathi + "', " +
    //          "ImplimentDate = '" + conn.ConvertDate(ImplDate) + "', OpeningBal = '" + OpenBal + "' " +
    //          "WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SUBGLCODE + "' AND GLCODE='" + GLCODE + "'  " +
    //          "UPDATE GLMAST SET LASTNO='" + LASTNO + "' WHERE BRCD='" + CBrcd + "' AND SUBGLCODE='" + SUBGLCODE + "' AND GLCODE='" + GLCODE + "' ";
    //        Result = conn.sExecuteQuery(sql);
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return Result;
    //}

    //public int UPDATEGL1(string GLNAME, string PLACCNO, string LASTNO, string GLPRIORITY, string GlGroup, string BRCD, string SUBGLCODE, string GLCODE, string Marathi, string PLGroup, string CBrcd, string ImplDate, string OpenBal)
    //{
    //    try
    //    {
    //        //  Modified by amol on 20/02/2018 for implimentdate and opening Balance
    //        sql = "UPDATE GLMAST SET GLNAME='" + GLNAME + "',GLGROUP='" + GlGroup + "',PLACCNO='" + PLACCNO + "',GLPRIORITY='" + GLPRIORITY + "',GLMarathi=N'" + Marathi + "', " +
    //          "PLGROUP='" + PLGroup + "', ImplimentDate = '" + conn.ConvertDate(ImplDate) + "', OpeningBal = '" + OpenBal + "' " +
    //          "WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SUBGLCODE + "' AND GLCODE='" + GLCODE + "'  " +
    //          "UPDATE GLMAST SET LASTNO='" + LASTNO + "' WHERE BRCD='" + CBrcd + "' AND SUBGLCODE='" + SUBGLCODE + "' AND GLCODE='" + GLCODE + "' ";
    //        Result = conn.sExecuteQuery(sql);
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return Result;
    //}

    public int DeleteMainGL(string BrCode, string GlCode, string PrCode)
    {
        try
        {
            sql = "Delete From GlMast Where BRCD = '" + BrCode + "' And GLCODE = '" + GlCode + "' And SUBGLCODE = '" + PrCode + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteLoanInfo(string BrCode, string PrCode)
    {
        try
        {
            sql = "Delete From LoanGl Where BRCD = '" + BrCode + "' And LoanGlCode = '" + PrCode + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteDeposit(string BrCode, string PrCode)
    {
        try
        {
            sql = "Delete From DepositGl Where BRCD = '" + BrCode + "' And DepositGlCode = '" + PrCode + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GETLOAN(string BRCD, string SUBGL)
    {
        sql = "EXEC SP_MODIFYGLMAST @FLAG='LOAN',@BRCD='" + BRCD + "',@SUBGLCODE='" + SUBGL + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }

    public DataTable GETDEPOSIT(string BRCD, string SUBGL)
    {
        DataTable DT = new DataTable();
        sql = "EXEC SP_MODIFYGLMAST @FLAG='DEPOSIT',@BRCD='" + BRCD + "',@SUBGLCODE='" + SUBGL + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }

    public DataTable GETOTHER(string BRCD, string SUBGL)
    {
        DataTable DT = new DataTable();
        sql = "EXEC SP_MODIFYGLMAST @FLAG='OTHER',@BRCD='" + BRCD + "',@SUBGLCODE='" + SUBGL + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }

    public string getgp(string glgpname)
    {
        sql = "select glgroup from bsformat where  description='" + glgpname + "'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }

    public int loangl(string loancode, string loantype, string loacat, string brcd)
    {
        sql = "insert into loangl (loanglcode,loantype,loancategory,BRCD)values('" + loancode + "','" + loantype + "','" + loacat + "','" + brcd + "')";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }

    public int Updateloangl(string loancode, string loantype, string loacat, string brcd, string IsMultiApply)
    {
        if (IsMultiApply.Equals("Y"))
        {
            sql = "update loangl set  loantype='" + loantype + "',loancategory='" + loacat + "' where LoanGlCode='" + loancode + "'";

        }
        else
        {
            sql = "update loangl set  loantype='" + loantype + "',loancategory='" + loacat + "' where LoanGlCode='" + loancode + "' and BRCD='" + brcd + "'";

        }
       // sql = "insert into loangl (loanglcode,loantype,loancategory,BRCD)values('" + loancode + "','" + loantype + "','" + loacat + "','" + brcd + "')";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }


    public int DEPOSITGL(string DCODE, string DTYPE, string DCAT, string BRCD)
    {
        sql = "insert into depositgl (depositGLcode,deposittype,category,brcd)VALUES('" + DCODE + "','" + DTYPE + "','" + DCAT + "','" + BRCD + "')";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }


    public int UpdateDEPOSITGL(string DCODE, string DTYPE, string DCAT, string BRCD, string IsMultiApply)
    {
        if (IsMultiApply.Equals("Y"))
        {
            sql = "update depositgl set  deposittype='" + DTYPE + "',category='" + DCAT + "' where depositGLcode='" + DCODE + "'";

        }
        else
        {
            sql = "update depositgl set  deposittype='" + DTYPE + "',category='" + DCAT + "' where depositGLcode='" + DCODE + "' and BRCD='" + BRCD + "'";

        }
       // sql = "insert into depositgl (depositGLcode,deposittype,category,brcd)VALUES('" + DCODE + "','" + DTYPE + "','" + DCAT + "','" + BRCD + "')";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public string GETPLNAME(string BRCD, string SUBGL)
    {
        sql = "SELECT  DISTINCT GLNAME FROM GLMAST WHERE SUBGLCODE='" + SUBGL + "' AND BRCD='" + BRCD + "'";
        string RESULT = conn.sExecuteScalar(sql);
        return RESULT;
    }

    public DataTable getbranchcode()
    {
        sql = "SELECT BRCD FROM BANKNAME";
        DataTable DT = new DataTable();
        DT = conn.GetDatatable(sql);
        return DT;
    }
}