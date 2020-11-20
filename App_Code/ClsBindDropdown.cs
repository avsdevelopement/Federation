using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

public class ClsBindDropdown
{
    Mobile_Service MS = new Mobile_Service();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sQuery = "", sCustNo = "", sMobNo = "";
    string sql = "", sResult = "";
    int Result = 0;
    double balance = 0;
    string TableName = "";

    public ClsBindDropdown()
    {
    }

    public string GetBranchName(string BrCode)
    {
        try
        {
            sql = "Select MidName From BankName Where BrCd = '" + BrCode + "' And BrCd <> 0";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }




    public void BindPaymentMode(DropDownList ddldoc)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1033' and srno in(1,4)  order by SRNO";
        conn.FillDDL(ddldoc, sql);
    }
    public void BindPaymentModeRES(DropDownList ddldoc)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='2574'  order by SRNO";
        conn.FillDDL(ddldoc, sql);
    }
    public int GetProdOperate(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select IsNull(UnOperate, '0') As UnOperate From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));

            if (Result == null)
                Result = 0;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public void UpdateMobNo(string Agent, string BRCD)
    {
        try
        {
            sql = "exec Mobile '" + Agent + "','" + BRCD + "'";
            conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindPaymentModeEXPENCE(DropDownList ddldoc)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='2571'";
        conn.FillDDL(ddldoc, sql);
    }
    public void BindPaymentModeStage(DropDownList ddldoc)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='2572'";
        conn.FillDDL(ddldoc, sql);
    }
    public void BindWard(DropDownList ddlWard)
    {
        sql = "select DESCRIPTION name,srno id from LOOKUPFORM1 where LNO=2566 order by DESCRIPTION";
        conn.FillDDL(ddlWard, sql);
    }
    public string GetMaxRefid(string BRCD, string EDT, string FL)
    {
        sql = "exec SP_BIND_ACTIVITY @FLAG='" + FL + "',@BRCD='" + BRCD + "',@EDT='" + conn.ConvertDate(EDT) + "'";
        sql = conn.sExecuteScalar(sql);
        return sql;
    }
    public string GetFDSBINTStatus(string BRCD)
    {
        sql = "exec SP_BIND_ACTIVITY @FLAG='FDSBINT',@BRCD='" + BRCD + "'";
        sql = conn.sExecuteScalar(sql);
        return sql;
    }

    public void BindGlGroupForDeposit(DropDownList ddlcatnew, string GlCode)
    {
        sql = "exec SpGlMastDropDown @Flag='GetDDLD', @GlCode= '" + GlCode + "'";
        conn.FillDDL(ddlcatnew, sql);
    }

    public void BindGlGroupForLoan(DropDownList ddlcatnew, string GlCode)
    {
        sql = "exec SpGlMastDropDown @Flag='GetDDLL', @GlCode= '" + GlCode + "'";
        conn.FillDDL(ddlcatnew, sql);
    }
    public void BindINTType(DropDownList Ddltype, string BRCD)
    {
        sql = "exec SP_BIND_ACTIVITY @FLAG='CAT',@BRCD='" + BRCD + "' ";
        conn.FillDDL(Ddltype, sql);
    }
    public DataTable BindAccStatus(string AccNo, string BRCD, string PRDNO)
    {
        try
        {
            sql = "SELECT BankName, CustAccNo from Avs_InvAccountMaster where brcd='" + BRCD + "' and subglcode='" + PRDNO + "' and CustAccno='" + AccNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public void BindPart(DropDownList ddlpart)
    {
        sql = "SELECT DESCRIPTION Name,SRNO id from LOOKUPFORM1 WHERE LNO=1053 ORDER BY SRNO";
        conn.FillDDL(ddlpart, sql);
    }

    public string GetACCSTS(string BRCD, string SBGL, string ACCNO)
    {
        try
        {
            sql = "SELECT ISNULL((CASE WHEN ACC_STATUS=1 THEN 'ACTIVE'+'_'+CONVERT(VARCHAR(5),ACC_STATUS) WHEN ACC_STATUS=2 THEN 'IN-OPERATIVE'+'_'+CONVERT(VARCHAR(5),ACC_STATUS) WHEN ACC_STATUS=3 THEN 'CLOSE'+'_'+CONVERT(VARCHAR(5),ACC_STATUS) " +
                " WHEN ACC_STATUS=4 THEN 'LEAN MARK'+'_'+CONVERT(VARCHAR(5),ACC_STATUS) WHEN ACC_STATUS=5 THEN 'CREDIT FREEZE'+'_'+CONVERT(VARCHAR(5),ACC_STATUS) WHEN ACC_STATUS=6 THEN 'DEBIT FREEZE'+'_'+CONVERT(VARCHAR(5),ACC_STATUS)  WHEN ACC_STATUS=7 THEN 'TOTAL FREEZE'+'_'+CONVERT(VARCHAR(5),ACC_STATUS)" +
                " ELSE 'N/A' END),'N/A') ACCSTATUS FROM AVS_ACC " +
                " WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SBGL + "'" +
                " AND ACCNO='" + ACCNO + "'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }


    public void BindOrder(DropDownList ddl)
    {
        try
        {
            sql = "select DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2569' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindRCNO(DropDownList ddl)
    {
        try
        {
            sql = "select DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2568' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }


    public void BindMemberType(DropDownList ddlmem)
    {
        sql = "Select DESCRIPTION name,SRNO id from LOOKUPFORM1 where LNO=2567 ORDER BY SRNO";
        conn.FillDDL(ddlmem, sql);
    }
    public void BindBRANCHNAME(DropDownList DDL, string BRCD)
    {
        sql = "SELECT Convert(varchar(100),BRCD)+'-'+Convert(varchar(100),MIDNAME) name,BRCD id from BANKNAME WHERE BRCD<>0 ORDER BY BRCD";
        conn.FillDDL(DDL, sql);
    }
    public void BindNoticeCharges(DropDownList DDL)
    {
       // sql = "SELECT ID,Notice_Desc+'-'+replace(Convert(nvarchar(20),Charges),'.00','') as Name from Avs_Notice_Chrg";
        sql = "SELECT ID,NoticeType+'_'+Notice_Desc+'-'+replace(Convert(nvarchar(20),Charges),'.00','') as Name from Avs_Notice_Chrg";
        conn.FillDDL(DDL, sql);
    }
    public void BindReasons(DropDownList DDL)
    {
        sql = "SELECT dESCRIPTION Name,SRNO id FROM LOOKUPFORM1 WHERE LNO=1048 order by Description";
        conn.FillDDL(DDL, sql);
    }
    public void BindIO(DropDownList DDL, string BRCD)
    {
        sql = "SELECT DESCRIPTION name,SRNO id from LOOKUPFORM1 WHERE LNO=1042 ORDER BY SRNO";
        conn.FillDDL(DDL, sql);
    }
    public string GetBranchno(string STR)
    {
        sql = "SELECT BRCD  from BANKNAME where MIDNAME='" + STR + "' AND BRCD<>0";
        STR = conn.sExecuteScalar(sql);
        return STR;
    }
    public void BindResident(DropDownList ddlRes)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1023' order by SRNO";
        conn.FillDDL(ddlRes, sql);
    }
    public void bindloantype(DropDownList ddlloan)
    {
        sql = "select SRNO id,DESCRIPTION name from LOOKUPFORM1 where LNO=1039";
        conn.FillDDL(ddlloan, sql);
    }
    public void bindintcal(DropDownList ddlint)
    {

        sql = "select SRNO id,DESCRIPTION name from LOOKUPFORM1 where LNO=1038";
        conn.FillDDL(ddlint, sql);
    }
    public void bindstatus(DropDownList ddlstatus)
    {
        sql = "select SRNO id,DESCRIPTION name from LOOKUPFORM1 where LNO=1040";
        conn.FillDDL(ddlstatus, sql);
    }

    public void BindCalType(DropDownList DdlCalType)
    {
        sql = "select SRNO id,DESCRIPTION name from LOOKUPFORM1 where LNO=1095";
        conn.FillDDL(DdlCalType, sql);
    }

    public void BindDoc(DropDownList ddldoc)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1031' order by SRNO";
        conn.FillDDL(ddldoc, sql);
    }
    public void BindActi(DropDownList ddlact, string BRCD)
    {
        sql = "exec SP_BIND_ACTIVITY @FLAG='',@BRCD='" + BRCD + "' ";
        //sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1043' order by SRNO";
        conn.FillDDL(ddlact, sql);
    }

    public void BindPrefix(DropDownList ddlPrefix)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1030' order by SRNO";
        conn.FillDDL(ddlPrefix, sql);
    }
    public void BindExportFile(DropDownList ddExport)
    {
        sql = "SELECT DESCRIPTION Name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1032' ORDER BY SRNO";
        conn.FillDDL(ddExport, sql);
    }
    public void BindSIStatus(DropDownList ddExport)
    {
        sql = "SELECT DESCRIPTION Name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1036' ORDER BY SRNO";
        conn.FillDDL(ddExport, sql);
    }

    public void BindSISTATUS(DropDownList ddl)
    {
        sql = "SELECT DESCRIPTION Name, SRNO id FROM LOOKUPFORM1 WHERE LNO='1036' ORDER BY SRNO";
        conn.FillDDL(ddl, sql);
    }
    public void BindPosting(DropDownList ddl)
    {
        sql = "SELECT DESCRIPTION Name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1037' ORDER BY SRNO";
        conn.FillDDL(ddl, sql);
    }


    // ----------- GET IDENTITY PROOF -----------
    public void BindDdlIdentityProof(DropDownList ddlIdentity)
    {
        string sql = "select SRNO id, DESCRIPTION Name from LOOKUPFORM1 WHERE LNO = 1024 "; // Removed AND SRNO NOT IN (1,8)
        conn.FillDDL(ddlIdentity, sql);
    }
    // ----------- GET ADDRESS PROOF -----------
    public void BindAddressProof(DropDownList ddlAddress)
    {
        string sql = "SELECT SRNO id, DESCRIPTION Name FROM LOOKUPFORM1 WHERE LNO = 1006 AND SRNO <> '6'";
        conn.FillDDL(ddlAddress, sql);
    }
    public void BindGLType(DropDownList ddlGLT)
    {
        sql = "SELECT GLGROUP id,DESCRIPTION Name from BSFORMAT ORDER BY SRNO";
        conn.FillDDL(ddlGLT, sql);
    }
    public void BindOccupation(DropDownList ddlOccu)
    {
        sql = "SELECT DESCRIPTION Name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1003' ORDER BY DESCRIPTION";
        conn.FillDDL(ddlOccu, sql);
    }
    public void BindInsType(DropDownList ddlOccu,string FL)
    {
        sql = "Exec Isp_InstruOperation @Flag='" + FL + "'";
        conn.FillDDL(ddlOccu, sql);
    }
    public void BindMarritial(DropDownList ddlMarr)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1025' order by SRNO";
        conn.FillDDL(ddlMarr, sql);
    }
    public void BindNation(DropDownList ddlNation)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1026' order by SRNO";
        conn.FillDDL(ddlNation, sql);
    }
    public void BindMemType(DropDownList ddlNation)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1044' order by SRNO";
        conn.FillDDL(ddlNation, sql);
    }
    public void BindProfType(DropDownList ddlNation)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1045' order by SRNO";
        conn.FillDDL(ddlNation, sql);
    }
    public void BindEmpStatus(DropDownList ddlNation)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1046' order by SRNO";
        conn.FillDDL(ddlNation, sql);
    }
    public void BindArea(DropDownList ddlarea, string STNO)
    {
        sql = "select srno id,DESCRIPTION name from LOOKUPFORM1 where LNO=1013 and refno='" + STNO + "'";
        conn.FillDDL(ddlarea, sql);
    }
    public void BindAreaLoan(DropDownList ddlarea)
    {
        sql = "select srno id,DESCRIPTION name from LOOKUPFORM1 where LNO=1013 order by Description";
        conn.FillDDL(ddlarea, sql);
    }
    public void BindReport_Type(DropDownList ddlReport)
    {
        sql = "SELECT DESCRIPTION Name,SRNO id FROM LOOKUPFORM1 WHERE LNO=1041 order by SRNO";
        conn.FillDDL(ddlReport, sql);
    }

    public void BindDistrict(DropDownList ddlarea, string STNO)
    {
        sql = "select srno id,DESCRIPTION name from LOOKUPFORM1 where LNO=1005 and SRNO in(18,19,20,21) and refno='" + STNO + "'";
        conn.FillDDL(ddlarea, sql);
    }

    public void BindState(DropDownList ddlarea)
    {
        sql = "select DESCRIPTION name,srno id from LOOKUPFORM1 where LNO=1007 and SRNO=16 order by DESCRIPTION";
        conn.FillDDL(ddlarea, sql);
    }

    public void BindACCTYPE(DropDownList DdlAcctype)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1016' ORDER BY SRNO";
        conn.FillDDL(DdlAcctype, sql);
    }

    public void BindCustACCTYPE(DropDownList DdlAcctype)//New cust data--Dhanya Shetty 07-06-2016//
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1060' ORDER BY SRNO";
        conn.FillDDL(DdlAcctype, sql);
    }

    public void BindBloodGroup(DropDownList Ddlbldgrp)//BLOODGROUP--Dhanya Shetty 07-06-2016//
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1061' ORDER BY SRNO";
        conn.FillDDL(Ddlbldgrp, sql);
    }

    public void BindRELIGIONCODE(DropDownList DdlRelCode)//ankita 18/05/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1057' ORDER BY SRNO";
        conn.FillDDL(DdlRelCode, sql);
    }
    public void BindCASTECODE(DropDownList DdlCastecde)//ankita 18/05/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1058' ORDER BY SRNO";
        conn.FillDDL(DdlCastecde, sql);
    }
    public void BindMODEOFOPR(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1017' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public void BindActivity(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1043' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public void BindMemActivity(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1108' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public void BindSActivity(DropDownList DdlMODE, string flag)
    {
        if (flag == "1")
            sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1109' and SRNO<>'4' ORDER BY SRNO";
        else if (flag == "2")
            sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1109' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public void BindSecActivity(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1076' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public void BindNPACode(DropDownList DDL)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1110' ORDER BY SRNO";
        conn.FillDDL(DDL, sql);
    }
    public void BindAccActivity(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1047' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public void DdlODActivity(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2000' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public void DdlODInstActivity(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2000' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public void DdlODLoanActivity(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2000' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public void BindAccStatusActivity(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1077' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public void BindSHRActivty(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1049' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public void BindSHRActivtyType(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1050' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public string GetAddType(string DES)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO = '1027' AND DESCRIPTION = '" + DES + "'";
        DES = conn.sExecuteScalar(sql);
        return DES;
    }

    public void BindAddType(DropDownList ddl)
    {
        try
        {
            sql = "select DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2570' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public string GetAccType(string AccT, string BRCD)
    {
        sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE='" + AccT + "'  AND BRCD='" + BRCD + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }

    public string GetLoanCategory(string AccT, string BRCD)
    {
        sql = "SELECT LOANCATEGORY FROM LOANGL WHERE BRCD = '" + BRCD + "' AND LOANGLCODE = '" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }

    public string GetAccTypeGL(string AccT, string BRCD)
    {
        //sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),GLCODE) FROM GLMAST WHERE SUBGLCODE='" + AccT + "'  AND BRCD='" + BRCD + "' and glcode='"+GL+"'";
        //sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),GLCODE) FROM GLMAST WHERE SUBGLCODE='" + AccT + "'  AND BRCD='" + BRCD + "'  AND GLCODE=2";

        //Abhishek --Changed Bcz GLOCDE is Hardcoded
        sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),GLCODE) FROM GLMAST WHERE SUBGLCODE='" + AccT + "'  AND BRCD='" + BRCD + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }

    // Added by amol on 27/10/2017 as per Ambika mam instruction (For Cash Payment)
    public string GetDepositCat(string BrCode, string AccNo)
    {
        try
        {
            sql = "Select Category From DepositGl Where BRCD = '" + BrCode + "' And DepositGlCode = '" + AccNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetACCNo(string BRCD, string Prod, string CustNo)
    {
        string Accno = "";
        try
        {
            if (Prod == "4")
                sql = "select Accno from avs_acc where brcd=1 and subglcode='" + Prod + "' and custno='" + CustNo + "'";
            else
                sql = "select Accno from avs_acc where brcd='" + BRCD + "' and subglcode='" + Prod + "' and custno='" + CustNo + "'";
            Accno = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Accno;
    }

    public string GetAgentType(string AccT, string BRCD)//Dhanya Shetty-For Agent report
    {
        sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),GLCODE) FROM GLMAST WHERE SUBGLCODE='" + AccT + "'  AND BRCD='" + BRCD + "'AND GLCODE IN(2,15,6)";// ADDED GLCODE=15 BY ANKITA FOR BALVIKAS ON 13/07/2017
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    // For Checkin A/C Close Details --ABHISHEK
    public string GetGlDetails(string BRCD, string SUBGL, string ACCNO, string FL)
    {
        string Res = "";
        try
        {
            if (FL == "CASH")
            {
                sql = "SELECT (CONVERT(VARCHAR(10),GLCODE)+'_'+CONVERT(VARCHAR(60),GLNAME)) GLNAME FROM GLMAST WHERE GLCODE=99 AND BRCD='" + BRCD + "'";
            }
            else if (FL == "CHEQUE")
            {
                //sql = "SELECT AA.ACCNO FROM GLMAST GM  " +
                //        " INNER JOIN AVS_ACC AA ON GM.GLCODE=AA.GLCODE AND GM.BRCD=AA.BRCD AND GM.SUBGLCODE=AA.SUBGLCODE " +
                //        " WHERE AA.ACCNO='" + ACCNO + "' AND AA.SUBGLCODE='" + SUBGL + "' AND AA.BRCD='" + BRCD + "' AND GM.GLGROUP='CBB'";
                sql = "SELECT GLCODE FROM GLMAST WHERE GLGROUP='CBB' AND SUBGLCODE='" + SUBGL + "' AND BRCD='" + BRCD + "'";
            }

            Res = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    public string GetLoanGL(string acct, string brcd)
    {
        sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),SUBGLCODE) FROM GLMAST WHERE SUBGLCODE='" + acct + "' AND BRCD='" + brcd + "'";
        acct = conn.sExecuteScalar(sql);
        return acct;
    }
    public string GetInvGL1(string acct, string brcd)
    {
        sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),SUBGLCODE) FROM GLMAST WHERE SUBGLCODE='" + acct + "' AND BRCD='" + brcd + "' and glgroup='INV'";
        acct = conn.sExecuteScalar(sql);
        return acct;
    }
    public string GetInvGL(string acct, string brcd)//Amruta 30/06/2017
    {
        sql = "SELECT top(1) BankName+'_'+CONVERT(VARCHAR(10),SUBGLCODE) FROM AVS_InvAccountMaster WHERE SUBGLCODE='" + acct + "' AND BRCD='" + brcd + "'";
        acct = conn.sExecuteScalar(sql);
        return acct;
    }

    public string GetTDAAccTypeGL(string AccT, string BRCD, string GL)
    {
        sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),GLCODE)+'_'+CONVERT(VARCHAR(10),ISNULL(IR,0)) FROM GLMAST WHERE SUBGLCODE='" + AccT + "'  AND BRCD='" + BRCD + "'";
        //and glcode='" + GL + "'";
        // sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),GLCODE) FROM GLMAST WHERE SUBGLCODE='" + AccT + "'  AND BRCD='" + BRCD + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetGLGroup(string AccT, string BRCD, string GL)
    {
        sql = "SELECT GLGROUP FROM GLMAST WHERE SUBGLCODE='" + AccT + "'  AND BRCD='" + BRCD + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetAccTType(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1016' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetMOP(string AccT)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1017' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOACCT(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1016' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }

    public string GetNOACCT1(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1064' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public string GetMODENO(string aact)
    {
        sql = " SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1017' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public string GetCustName(string CNO, string BRCD)
    {
        sql = "select  ISnull(CUSTNAME,'') CUSTNAME from MASTER where CUSTNO='" + CNO + "'  and STAGE <>'1004'";
        CNO = conn.sExecuteScalar(sql);
        return CNO;
    }

    public string GetMemName(string ACCNO)
    {
        sql = "SELECT isnull(M.CUSTNAME,'')CUSTNAME FROM MASTER M INNER JOIN AVS_ACC A ON  A.CUSTNO=M.CUSTNO WHERE A.BRCD='1' and A.glcode=4 AND A.ACCNO='" + ACCNO + "' and A.STAGE <>'1004'";
        ACCNO = conn.sExecuteScalar(sql);
        return ACCNO;
    }

    public string GetStage(string CNO, string BRCD, string STAGE)
    {
        try
        {
            sql = "SELECT STAGE FROM MASTER WHERE CUSTNO='" + CNO + "' AND STAGE <>1004";
            STAGE = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return STAGE;
    }

    public string GetStageSI(string SINO, string BRCD)
    {
        string STAGE;
        sql = "SELECT STAGE FROM AVS5007 WHERE SINO='" + SINO + "' AND BRCD='" + BRCD + "' AND STAGE <>1004";
        STAGE = conn.sExecuteScalar(sql);
        return STAGE;
    }

    public string Getstage1(string CNO, string BRCD, string PRD,string RecSrno="0")//GLCDOE PARAMETR REMOVED
    {
        string RS = "";
        sql = "SELECT STAGE FROM AVS_ACC WHERE ACCNO='" + CNO + "'  AND SUBGLCODE='" + PRD + "' AND BRCD='" + BRCD + "' AND STAGE <>1004";
        RS = conn.sExecuteScalar(sql);
        return RS;
    }

    public string GetSHstage(string CNO, string BRCD, string PRD)//GLCDOE PARAMETR REMOVED
    {
        string RS = "";
        sql = "SELECT STAGE FROM AVS_ACC WHERE ACCNO='" + CNO + "'  AND SUBGLCODE='" + PRD + "' AND BRCD='" + BRCD + "' AND STAGE <>1004 AND Acc_Status<>'3'";
        RS = conn.sExecuteScalar(sql);
        return RS;
    }

    public DataTable GetAccStage(string BRCD, string PRD, string CNO)
    {
        sql = "SELECT Acc_Status, ConVert(VarChar(10), OpeningDate, 103) As OpenDate, Stage FROM AVS_ACC WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + PRD + "' AND ACCNO='" + CNO + "' And STAGE <>1004 ";
        DT = conn.GetDatatable(sql);
        return DT;
    }

    public DataTable GetCuststage(string CNO, string BRCD)//GLCDOE PARAMETR REMOVED
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "SELECT CustNo,CustName FROM Master WHERE Custno='" + CNO + "'   AND STAGE <>1004 ";//AND GLCODE='"+GL+"'
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

        return dt;
    }
    public string GetInvstage1(string CNO, string BRCD, string PRD)//GLCDOE PARAMETR REMOVED -- Amruta for Investment master
    {
        string RS = "";
        //sql = "SELECT STAGE FROM AVS_ACC WHERE ACCNO='" + CNO + "'AND GLCODE='"+GL+"' AND SUBGLCODE='"+PRD+"' AND BRCD='" + BRCD + "' AND STAGE <>1004 ";
        sql = "SELECT CONVERT(VARCHAR(5),STAGE) FROM AVS_InvAccountMaster WHERE CustAccNo='" + CNO + "' AND BankCode='" + PRD + "' AND BRCD='" + BRCD + "' AND STAGE <>1004 ";//AND GLCODE='"+GL+"'
        RS = conn.sExecuteScalar(sql);
        return RS;
    }
    public string GetstageINV(string CNO, string BRCD, string PRD)//GLCDOE PARAMETR REMOVED
    {
        string RS = "";
        // sql = "SELECT STAGE FROM AVS_ACC WHERE ACCNO='" + CNO + "'AND GLCODE='"+GL+"' AND SUBGLCODE='"+PRD+"' AND BRCD='" + BRCD + "' AND STAGE <>1004 ";
        // sql = "SELECT CONVERT(VARCHAR(5),STAGE) FROM AVS_InvAccountMaster WHERE ReceiptNo='" + CNO + "' AND SUBGLCODE='" + PRD + "' AND BRCD='" + BRCD + "' AND STAGE <>1004 ";//AND GLCODE='"+GL+"'
        sql = "SELECT CONVERT(VARCHAR(5),STAGE) FROM AVS_INVDEPOSITEMASTER WHERE CUSTACCNO='" + CNO + "' AND SUBGLCODE='" + PRD + "' AND BRCD='" + BRCD + "' AND STAGE <>1004 ";//AND GLCODE='"+GL+"'
        RS = conn.sExecuteScalar(sql);
        return RS;
    }
    public string Getstage3(string CNO, string BRCD, string PRD, string GL)//GLCDOE PARAMETR REMOVED
    {
        sql = "SELECT STAGE FROM AVS_ACC WHERE ACCNO='" + CNO + "'AND GLCODE='" + GL + "' AND SUBGLCODE='" + PRD + "' AND BRCD='" + BRCD + "' AND STAGE <>1004 ";
        // sql = "SELECT STAGE FROM AVS_ACC WHERE ACCNO='" + CNO + "' AND SUBGLCODE='" + PRD + "' AND BRCD='" + BRCD + "' AND STAGE <>1004 ";//AND GLCODE='"+GL+"'
        GL = conn.sExecuteScalar(sql);
        return GL;
    }
    public void BindRelation(DropDownList ddlRel)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1028' order by LNO";
        conn.FillDDL(ddlRel, sql);
    }

    // Bind Operation type
    public void BindOperation(DropDownList ddlRel)
    {
        sql = "select DESCRIPTION Name, SRNO id from LOOKUPFORM1 WHERE LNO LIKE '1017' order by Srno";//Dhanya Shetty//01/07/2017// Changed orderby lno to order by srno
        conn.FillDDL(ddlRel, sql);
    }

    // Bind Member Account type // LOOKUPFORM1
    public void BindAccType(DropDownList ddlRel)
    {
        sql = "select DESCRIPTION Name, SRNO id from LOOKUPFORM1 WHERE LNO LIKE '1016' order by LNO";
        conn.FillDDL(ddlRel, sql);
    }
    public void BindAccStatus11(DropDownList ddlRel)
    {
        sql = "select DESCRIPTION Name, SRNO id from LOOKUPFORM1 WHERE LNO LIKE '1047' order by LNO";
        conn.FillDDL(ddlRel, sql);
    }
    public void BindTRX(DropDownList ddlRel)
    {
        sql = "select DESCRIPTION Name, SRNO id from LOOKUPFORM1 where LNO=1043 and SRNO!=4 order by LNO";
        conn.FillDDL(ddlRel, sql);
    }
    // Bind Member Account type // LOOKUPFORM1
    public void BindAccStatus(DropDownList ddlRel)
    {
        sql = "select DESCRIPTION Name, SRNO id from LOOKUPFORM1 WHERE LNO LIKE '1052' order by LNO";
        conn.FillDDL(ddlRel, sql);
    }
    // Bind Member type // LOOKUPFORM1
    public void BindCustType(DropDownList ddlcust)
    {
        sql = "select DESCRIPTION Name, SRNO id from LOOKUPFORM1 where LNO=1016 order by SRNO";
        conn.FillDDL(ddlcust, sql);
    }
    // Bind Member Activity // LOOKUPFORM1 //ankita ghadage
    public void BindLogActivity(DropDownList ddlact)
    {
        sql = "select DESCRIPTION Name, SRNO id from LOOKUPFORM1 WHERE LNO=1054 order by SRNO";
        conn.FillDDL(ddlact, sql);
    }
    // Bind Account type // LOOKUPFORM1
    public void BindIntrstPayout(DropDownList ddlRel)
    {
        sql = "select DESCRIPTION Name, SRNO id from LOOKUPFORM1 where LNO = 1015";
        conn.FillDDL(ddlRel, sql);
    }

    public string GetAccName(string ACNO, string AT, string BRCD)
    {
        sql = "SELECT M.CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO  WHERE AC.SUBGLCODE='" + AT + "' AND AC.BRCD='" + BRCD + "' AND AC.ACCNO='" + ACNO + "'";
        ACNO = conn.sExecuteScalar(sql);
        return ACNO;
    }

    public DataTable GetUserCode(string Accno)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select * from usermaster where PERMISSIONNO=" + Accno;
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable GetAllAccName(string ACNO, string BRCD)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME,Ac.STAGE  FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO  WHERE AC.ACCNO='" + ACNO + "' and AC.BRCD='" + BRCD + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public string AccName(string ACNO, string AT, string AT1, string BRCD)
    {
        sql = "SELECT M.CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO  WHERE AC.SUBGLCODE BETWEEN '" + AT + "' AND '" + AT1 + "' AND AC.BRCD='" + BRCD + "' AND AC.ACCNO='" + ACNO + "'";
        ACNO = conn.sExecuteScalar(sql);
        return ACNO;
    }
    //// Check Customer Exist In Add,Contact,Master
    public int CheckCustomerInfo(string CTNO, string BRCD)
    {
        sql = " Select (M+A+C) TC from " +
            " (select count(*) M from master where custno='" + CTNO + "') MAST," +
            " (select count(*) A from Addmast where custno='" + CTNO + "' and brcd='" + BRCD + "') AST," +
            " (select count(*) C from Avs_Contactd where custno='" + CTNO + "' and brcd='" + BRCD + "') CST ";
        CTNO = conn.sExecuteScalar(sql);
        return Convert.ToInt32(CTNO);
    }



    public string CHKRECP(string ENTRYDATE, string BRCD,string rec)
    {

      
        string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');

        TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();


        sql = "SELECT setno FROM " + TableName + " WHERE EntryDate='" + conn.ConvertDate(ENTRYDATE) + "' AND BRCD='" + BRCD + "' and setno='"+rec+"'";
        BRCD = conn.sExecuteScalar(sql);
        return BRCD;
    }


    public string GetSetNo(string Date, string PName, string BRCD)
    {
        //To Avoid Duplication of SETNO --Abhsihek

        if (PName == "IBTSetNo")
            sql = "update avs5002 set LastNo = " +
            " (select Max(lastno)+1  from avs5002 where  EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "') " +
            " where EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "'" +
            "select Lastno From avs5002  where EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "'";
        else if (PName == "DaySetNo")
            sql = "update avs5002 set LastNo = " +
            " (select Max(lastno)+1  from avs5002 where  EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "' and BRCD='" + BRCD + "') " +
            " where EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "' and BRCD='" + BRCD + "' " +
            "select Lastno From avs5002  where EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "' and BRCD='" + BRCD + "' ";

        else if (PName == "InOutSetno")
        {
            sql = "UPDATE AVS5002 SET LASTNO=(SELECT MAX(LASTNO)+1 FROM AVS5002 WHERE ENTRYDATE='" + conn.ConvertDate(Date) + "' AND PARAMETERNAME='" + PName + "')" +
                  " WHERE ENTRYDATE='" + conn.ConvertDate(Date) + "' AND PARAMETERNAME='" + PName + "' AND BRCD='" + BRCD + "' " +
                  " SELECT LASTNO FROM AVS5002 WHERE ENTRYDATE='" + conn.ConvertDate(Date) + "' AND PARAMETERNAME='" + PName + "' AND BRCD='" + BRCD + "'";
        }
        else if (PName == "RTGSSetNo")//Dhanya Shetty//04/02/2018//For RTGS
        {
            sql = "update avs5002 set LastNo = " +
            " (select Max(lastno)+1  from avs5002 where  EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "') " +
            " where EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "'" +
            "select Lastno From avs5002  where EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "'";
        }
        PName = conn.sExecuteScalar(sql);
        return PName;
    }

    public int SetSetno(string Date, string PName, string ST, string BRCD)
    {
        sql = "update AVS5002 set LastNo='" + ST + "' where EntryDate='" + conn.ConvertDate(Date) + "' And ParameterName = '" + PName + "' ";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public string GetmonthName(string Fdate)
    {
        string[] FT = Fdate.Split('/');
        sql = "Select DateName( month , DateAdd( month , " + FT[1].ToString() + " , -1 ) )";
        Fdate = conn.sExecuteScalar(sql);
        return Fdate;
    }
    public void UpdateLastNo(string GL, string BRCD)
    {
        sql = "select MAX(accno) lno,SUBGLCODE,glcode from AVS_ACC where SUBGLCODE in (select SUBGLCODE from GLMAST where GLCODE=" + GL + ") and acc_status<>99 and brcd='" + BRCD + "' group by SUBGLCODE,glcode";
        DataTable DT = new DataTable();
        DT = conn.GetDatatable(sql);
        if (DT.Rows.Count > 0)
        {
            for (int i = 0; i <= DT.Rows.Count - 1; i++)
            {
                sql = "update GLMAST set LASTNO='" + DT.Rows[i]["lno"].ToString().Replace(".00", "") + "' where SUBGLCODE='" + DT.Rows[i]["subglcode"].ToString() + "' and glcode='" + DT.Rows[i]["glcode"].ToString() + "' and brcd='" + BRCD + "' ";
                Result = conn.sExecuteQuery(sql);
            }
        }
    }
    public string GetCashGl(string GL, string BRCD)
    {
        sql = "Select Subglcode from glmast where glcode='" + GL + "' and brcd='" + BRCD + "'";
        BRCD = conn.sExecuteScalar(sql);
        return BRCD;
    }
    public string GetCashSubglcode(string GL, string BRCD)
    {
        sql = "Select glcode from glmast where subglcode='" + GL + "' and brcd='" + BRCD + "'";
        BRCD = conn.sExecuteScalar(sql);
        return BRCD;
    }
    public DataTable GetLoanIROR(string GL, string SGL, string BRCD)
    {
        sql = "select isnull(IR,0) IR,isnull(IOR,0) IOR from GLMAST where SUBGLCODE='" + SGL + "' AND GLCODE='" + GL + "' AND  BRCD='" + BRCD + "'";

        DataTable DT = new DataTable();
        DT = conn.GetDatatable(sql);
        return DT;
    }

    public string GetLoanPL(string GL, string SGL, string BRCD)
    {
        sql = "SELECT PLACCNO FROM GLMAST WHERE GLCODE='" + GL + "' AND SUBGLCODE='" + SGL + "' AND BRCD='" + BRCD + "'";
        SGL = conn.sExecuteScalar(sql);
        return SGL;
    }

    public string GetLoanPPL(string SUBGL, string BRCD)
    {
        sql = "select isnull(PPL,0) from LOANGL where LOANGLCODE='" + SUBGL + "' and BRCD='" + BRCD + "'";
        SUBGL = conn.sExecuteScalar(sql);
        return SUBGL;
    }

    public string GetPLACC(string GL, string CHRGT, string BRCD)
    {
        sql = "SELECT isnull(PLACC,0) FROM CHARGESMASTER WHERE CHARGESTYPE='" + CHRGT + "' AND GLCODE='" + GL + "'";
        CHRGT = conn.sExecuteScalar(sql);
        return CHRGT;
    }

    public DataTable GetAMT(string BRCD, string ACCNO, string SUBGL, string EDT)
    {
        try
        {
            EDT = conn.ConvertDate(EDT);
            sql = "SELECT TOP 1 ISNULL(INSTALLMENT,0)INST_AMT,ISNULL(INTEREST_RECV,0)INT_RECV FROM  LOANSCHEDULE " +
                " WHERE CUSTACCNO='" + ACCNO + "' AND  " +
                " LOANGLCODE='" + SUBGL + "' AND " +
                " BRCD='" + BRCD + "' AND " +
                " INSTDATE BETWEEN " +
                " (sELECT CONVERT(VARCHAR(10),DATEADD(MONTH,DATEDIFF(MONTH,0,'" + EDT + "'),0),120)) AND " +
                " (SELECT CONVERT(VARCHAR(10),DATEADD(dd,-1,DATEADD(mm,DATEDIFF(mm,0,'" + EDT + "') + 1,0)),120)) ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);

        }
        return DT;
    }

    //For Photo Migration Saving and Current
    public string GetCustno(string Accno, string BRCD, string SUBGL)
    {
        sql = "SELECT isnull(custno,0) FROM AVS_ACC WHERE SUBGLCODE='" + SUBGL + "' AND ACCNO=" + Accno + " and brcd='" + BRCD + "'";
        Accno = conn.sExecuteScalar(sql);
        return Accno;
    }
    public string GetCustno1(string SUBGL, string Accno, string BRCD)
    {
        sql = "SELECT custno FROM AVS_ACC WHERE SUBGLCODE='" + SUBGL + "' AND ACCNO=" + Accno + " and brcd='" + BRCD + "'";
        Accno = conn.sExecuteScalar(sql);
        return Accno;
    }

    public void BindPayment(DropDownList ddldoc, string RefNo)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1033' and refno='" + RefNo + "' order by SRNO";
        conn.FillDDL(ddldoc, sql);
    }

    public void BindSTAGENOTICE(DropDownList ddldoc, string RefNo)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='2575' and refno='" + RefNo + "' order by SRNO";
        conn.FillDDL(ddldoc, sql);
    }


    public void BindMTDAClose(DropDownList ddldoc)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='2500' order by SRNO";
        conn.FillDDL(ddldoc, sql);
    }
    public void BindWithReceipt(DropDownList ddldoc, string RefNo)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1055' and refno='" + RefNo + "' order by SRNO";
        conn.FillDDL(ddldoc, sql);
    }
    public void BindDepositGL(DropDownList ddldepo, string GL, string BRCD)
    {
        sql = "select glname Name,subglcode id from glmast where glcode='" + GL + "' and BRCD='" + BRCD + "'";
        conn.FillDDL(ddldepo, sql);
    }

    public void BindCategry(DropDownList ddldoc)
    {
        sql = "SELECT DESCRIPTION Name, SRNO id FROM LOOKUPFORM1  WHERE LNO='1035' order by SRNO";
        conn.FillDDL(ddldoc, sql);
    }

    public void BindCategory(DropDownList ddldoc)
    {
        sql = "SELECT DESCRIPTION Name, SRNO id FROM LOOKUPFORM1  WHERE LNO='1034' order by SRNO";
        conn.FillDDL(ddldoc, sql);
    }
    public void BindProd(DropDownList ddldoc)
    {
        sql = "select DEPOSITGLCODE +' - '+ Category AS Name, DEPOSITGLCODE AS id from depositgl where CATEGORY is not null";
        conn.FillDDL(ddldoc, sql);
    }

    //Added By AmolB
    public void BindBrCode(DropDownList ddlloan, string BRCD)
    {
        sql = "select BRCD id,BANKNAME name from BANKNAME where BRCD='" + BRCD + "' "; //BRCD ADDED --Abhishek
        conn.FillDDL(ddlloan, sql);
    }

    //Added By AmolB
    public int CheckAccount(string AC, string PT, string BRCD)
    {
        try
        {
            try
            {
                sql = "SELECT CUSTNO FROM AVS_ACC WHERE SUBGLCODE='" + PT + "' AND ACCNO='" + AC + "' AND BRCD='" + BRCD + "'";
                Result = Convert.ToInt32(conn.sExecuteScalar(sql));
                return Result;
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
                return -1;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;

    }
    public void bindCashtype(DropDownList ddltype)
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1041' order by SRNO";
        conn.FillDDL(ddltype, sql);
    }
    public void Bindglname(DropDownList DDL)
    {
        sql = "select GLCATEGORY Name,GL id from gl_enquiry where GL not in (1,7,8,2,6,22,4)";
        conn.FillDDL(DDL, sql);
    }
    public void BindPlGroup(DropDownList DDL)
    {
        sql = "select PLGROUP Name,SRNO id from plformat where PLTYPE='E'";
        conn.FillDDL(DDL, sql);
    }
    public void BindGlGroup(DropDownList ddlcatnew)
    {
        sql = "select srno as id,DESCRIPTION as name from LOOKUPFORM1 where lno=1099";
        conn.FillDDL(ddlcatnew, sql);
    }
    public void bindbsformat(DropDownList ddlcatnew)
    {
        sql = "select Description as name,glgroup as id from bsformat";
        conn.FillDDL(ddlcatnew, sql);
    }
    public void BINDGLINFO(DropDownList DDL)
    {
        sql = "SELECT glgroup AS NAME,SRNO AS ID FROM BSFORMAT";
        conn.FillDDL(DDL, sql);
    }
    public void BindModuleRQ(DropDownList ddlModuleRQ)//Dhanya Shettty -for module related query//
    {
        sql = "select  glcode as id,GLNAME as Name from glmast where   subglcode=glcode and glcode<100 group by glcode ,GLNAME";
        conn.FillmoduleRqDDL(ddlModuleRQ, sql);
    }
    public void BindPmtMode(DropDownList DdlPmtMode)//Dhanya Shetty- for Avs_AllFiled
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1056' order by SRNO";
        conn.FillDDL(DdlPmtMode, sql);
    }
    public void BindChargesDescription(DropDownList DDL)//Dhanya Shetty -for Charges Updation
    {
        sql = "SELECT Convert(varchar(100),CHARGESTYPE)+'-'+Convert(varchar(100),DESCRIPTION) name,CHARGESTYPE id from CHARGESMASTER WHERE CHARGESTYPE<>0 ORDER BY CHARGESTYPE";
        conn.FillDDL(DDL, sql);
    }
    public void BindFreq(DropDownList DDL)//Dhanya Shetty -for Min Bal
    {
        sql = "SELECT DESCRIPTION name,SRNO id from LOOKUPFORM1 WHERE LNO=1042 ORDER BY SRNO";
        conn.FillDDL(DDL, sql);
    }
    public string GetFrequency(string fretype)//Dhanya Shetty -for Min Bal
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1042' AND DESCRIPTION='" + fretype + "'";
        fretype = conn.sExecuteScalar(sql);
        return fretype;
    }
    public void BindAssestLocation(DropDownList ddlAstLoc)//Dhanya Shetty//15-06-2017//For Dead stock 
    {
        sql = "SELECT MIDNAME name,BRCD id FROM BANKNAME WHERE BRCD<>0 ORDER BY BRCD";
        conn.FillDDL(ddlAstLoc, sql);
    }

    public void BindTransaction(DropDownList DDltransctntype)//Dhanya Shetty//27-06-2017//For outword
    {
        //string sql = "SELECT DESCRIPTIONMAR name,DESCRIPTION id from LOOKUPFORM1 WHERE LNO=1096 ORDER BY SRNO";
        string sql = " SELECT DESCRIPTIONMAR id ,DESCRIPTION name from LOOKUPFORM1 WHERE LNO=1096 ORDER BY SRNO";
        conn.FillDDL(DDltransctntype, sql);
    }
    public string  DispTransaction(string DDltransctntype)//Dhanya Shetty//31-03-2018//For outword
    {
        //string sql = "SELECT DESCRIPTIONMAR name,DESCRIPTION id from LOOKUPFORM1 WHERE LNO=1096 ORDER BY SRNO";
        string sql = " SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1096' AND DESCRIPTIONMAR='" + DDltransctntype + "' ORDER BY SRNO";
        DDltransctntype = conn.sExecuteScalar(sql);
        return DDltransctntype;
    }
    public void BindRetrunType(DropDownList DDLReturn)//DIpali Nagare//19-07-2017//For inword
    {
        string sql = "SELECT DESCRIPTION name,DESCRIPTIONMAR id from LOOKUPFORM1 WHERE LNO=1071 ORDER BY SRNO";
        conn.FillDDL(DDLReturn, sql);
    }

    public void BindSkipType(DropDownList DDLSkipCharges)//DIpali Nagare//19-07-2017//For inword
    {
        string sql = "SELECT DESCRIPTION name,DESCRIPTIONMAR id from LOOKUPFORM1 WHERE LNO=1072 ORDER BY SRNO";
        conn.FillDDL(DDLSkipCharges, sql);
    }

    public void BindAllowType(DropDownList DDLAllow)//DIpali Nagare//19-07-2017//For inword
    {
        string sql = "SELECT DESCRIPTION name,DESCRIPTIONMAR id from LOOKUPFORM1 WHERE LNO=1073 ORDER BY SRNO";
        conn.FillDDL(DDLAllow, sql);
    }



    public void BindFrequencyType(DropDownList DDLFrequency)//DIpali Nagare//19-07-2017//For inword
    {
        string sql = "SELECT DESCRIPTION name,DESCRIPTIONMAR id from LOOKUPFORM1 WHERE LNO=1070 ORDER BY SRNO";
        conn.FillDDL(DDLFrequency, sql);
    }



    public void BindFlatCharges(DropDownList DDLFLatCharges)//DIpali Nagare//19-07-2017//For inword
    {
        string sql = "SELECT DESCRIPTION name,DESCRIPTIONMAR id from LOOKUPFORM1 WHERE LNO=1068 ORDER BY SRNO";
        conn.FillDDL(DDLFLatCharges, sql);
    }



    public void BindPercentage(DropDownList DDLPercentage)//DIpali Nagare//19-07-2017//For inword
    {
        string sql = "SELECT DESCRIPTION name,DESCRIPTIONMAR id from LOOKUPFORM1 WHERE LNO=1069 ORDER BY SRNO";
        conn.FillDDL(DDLPercentage, sql);
    }


    public void Bindreason(DropDownList DdlReason)//DIpali Nagare//19-07-2017//For inword
    {
        string sql = "SELECT DESCRIPTION name,DESCRIPTIONMAR id from LOOKUPFORM1 WHERE LNO=1074 ORDER BY SRNO";
        conn.FillDDL(DdlReason, sql);
    }

    public string GetTransactnType(string Trans)//Dhanya Shetty//27-06-2017//For outword
    {
        //sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1096' AND DESCRIPTIONMAR='" + Trans + "'";
        sql = "SELECT DESCRIPTIONMAR FROM LOOKUPFORM1 WHERE LNO='1096' AND DESCRIPTION='" + Trans + "'";
        Trans = conn.sExecuteScalar(sql);
        return Trans;
    }

    public string GetRetrunType(string RetrunType)//Dipali Nagare//19-07-2017//For Intword
    {
        sql = "SELECT DESCRIPTION as Name,DESCRIPTIONMAR as id FROM LOOKUPFORM1 WHERE LNO='1071' AND DESCRIPTIONMAR='" + RetrunType + "'";
        RetrunType = conn.sExecuteScalar(sql);
        return RetrunType;
    }


    public string GetSkipType(string SkipType)//Dipali Nagare//19-07-2017//For Intword
    {
        sql = "SELECT DESCRIPTION as Name,DESCRIPTIONMAR as id FROM LOOKUPFORM1 WHERE LNO='1072' AND DESCRIPTIONMAR='" + SkipType + "'";
        SkipType = conn.sExecuteScalar(sql);
        return SkipType;
    }

    public string GetAllowType(string AllowType)//Dipali Nagare//19-07-2017//For Intword
    {
        sql = "SELECT DESCRIPTION as Name,DESCRIPTIONMAR as id FROM LOOKUPFORM1 WHERE LNO='1073' AND DESCRIPTIONMAR='" + AllowType + "'";
        AllowType = conn.sExecuteScalar(sql);
        return AllowType;
    }

    public string GetFrequencyType(string FrequencyType)//Dipali Nagare//19-07-2017//For Intword
    {
        sql = "SELECT DESCRIPTION as Name,DESCRIPTIONMAR as id FROM LOOKUPFORM1 WHERE LNO='1070' AND DESCRIPTIONMAR='" + FrequencyType + "'";
        FrequencyType = conn.sExecuteScalar(sql);
        return FrequencyType;
    }

    public string GetFlatCharges(string FlatCharges)//Dipali Nagare//19-07-2017//For Intword
    {
        sql = "SELECT DESCRIPTION as Name,DESCRIPTIONMAR as id FROM LOOKUPFORM1 WHERE LNO='1068' AND DESCRIPTIONMAR='" + FlatCharges + "'";
        FlatCharges = conn.sExecuteScalar(sql);
        return FlatCharges;
    }

    public string GetPercentageCharges(string PercentageCharges)//Dipali Nagare//19-07-2017//For Intword
    {
        sql = "SELECT DESCRIPTION as Name,DESCRIPTIONMAR as id FROM LOOKUPFORM1 WHERE LNO='1069' AND DESCRIPTIONMAR='" + PercentageCharges + "'";
        PercentageCharges = conn.sExecuteScalar(sql);
        return PercentageCharges;
    }

    public string GetReason(string Reason)//Dipali Nagare//19-07-2017//For Intword
    {
        sql = "SELECT DESCRIPTION as Name,DESCRIPTIONMAR as id FROM LOOKUPFORM1 WHERE LNO='1074' AND DESCRIPTIONMAR='" + Reason + "'";
        Reason = conn.sExecuteScalar(sql);
        return Reason;
    }


    //public string GetFrequencyType(string FrequencyType)//Dipali Nagare//19-07-2017//For Intword
    //{
    //    sql = "SELECT DESCRIPTION as Name,DESCRIPTIONMAR as id FROM LOOKUPFORM1 WHERE LNO='1067' AND DESCRIPTIONMAR='" + FrequencyType + "'";
    //    FrequencyType = conn.sExecuteScalar(sql);
    //    return FrequencyType;
    //}

    public void BindFileType(DropDownList ddlpart)
    {
        sql = "SELECT DESCRIPTION Name,SRNO id from LOOKUPFORM1 WHERE LNO=1066 ORDER BY SRNO";
        conn.FillDDL(ddlpart, sql);
    }

    public string GetDepoGL(string acct, string brcd)
    {
        sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),SUBGLCODE) FROM GLMAST WHERE SUBGLCODE='" + acct + "' AND BRCD='" + brcd + "' and glcode='5'";
        acct = conn.sExecuteScalar(sql);
        return acct;
    }

    public string GetDepoGLRD(string acct, string brcd)
    {
        //  Last changes by amol on 13/10/2018 (as per ambika mam instruction - ajinkyatara utkarsh int calculation)
        sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),SUBGLCODE) FROM GLMAST WHERE SUBGLCODE='" + acct + "' AND BRCD='" + brcd + "' And GLGROUP = 'DP'";
        acct = conn.sExecuteScalar(sql);
        return acct;
    }

    public void BindYear(DropDownList DDlYear)//Dhanya Shetty//14/07/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1068' ORDER BY SRNO";
        conn.FillDDL(DDlYear, sql);
    }

    public void BindDay(DropDownList DdlDay)//Dhanya Shetty//14/07/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1069' ORDER BY SRNO";
        conn.FillDDL(DdlDay, sql);
    }

    //added by amol on 26/07/2017 for insert record into avs1092 table
    public int InsertSMSRec(string BrCode, string PrCode, string AccNo, string Message, string CreateMid, string AuthMid, string EDate, string sFlag)
    {
        try
        {
            if (sFlag == "WelCome")
            {
                sql = "Select Parameter From AVS1090 Where Message = 'WelCome' And Activity = 'SMSCust' And Parameter='Y'";
                sQuery = conn.sExecuteScalar(sql);
            }
            else if (sFlag == "Receipt")
            {
                sql = "Select Parameter From AVS1090 Where Message = 'Receipt' And Activity = 'SMSRCPT' And Parameter='Y'";
                sQuery = conn.sExecuteScalar(sql);
            }
            else if (sFlag == "Payment")
            {
                sql = "Select Parameter From AVS1090 Where Message = 'Payment' And Activity = 'SMSPAY' And Parameter='Y'";
                sQuery = conn.sExecuteScalar(sql);
            }
            else if (sFlag == "LoanSanc")
            {
                sql = "Select Parameter From AVS1090 Where Message = 'LoanSanc' And Activity = 'SMSDISB' And Parameter='Y'";
                sQuery = conn.sExecuteScalar(sql);
            }

            if (sQuery == "Y")
            {
                sql = "Select CustNo From Avs_Acc Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
                sCustNo = conn.sExecuteScalar(sql);

                if (sCustNo != null && sCustNo != "")
                {
                    //sql = "Select IsNull(Mobile1, 0) From Avs_ContactD Where BrCd = '" + BrCode + "' And CustNo = '" + sCustNo + "' And Stage = '1003' And EffectDate = (Select Max(EffectDate) From Avs_ContactD Where BrCd = '" + BrCode + "' And CustNo = '" + sCustNo + "' And Stage = '1003')";
                    sql = "Select IsNull(Mobile1, 0) From Avs_ContactD Where CustNo = '" + sCustNo + "' And Stage = '1003'";
                    sMobNo = conn.sExecuteScalar(sql);

                    if (sMobNo == null)
                        sMobNo = "0";

                    //if (sMobNo != null && sMobNo != "")
                    //{
                    sql = "Insert Into AVS1092 (BRCD, CUSTNO, MOBILE, SMS_DATE, SMS_TYPE, SMS_DESCRIPTION, SMS_STATUS, OPERATOR, MID, VID, PCMAC, SYSTEMDATE) " +
                        "Values('" + BrCode + "', '" + sCustNo.ToString() + "','" + sMobNo.ToString() + "','" + conn.ConvertDate(EDate) + "','1','" + Message + "', '1', '" + sFlag.ToString() + "', '" + CreateMid + "', '" + AuthMid + "', '" + conn.PCNAME() + "', GetDate())";
                    Result = conn.sExecuteQuery(sql);

                    //for shoot sms to customer
                    string SMS = MS.Send_SMS(sCustNo, EDate);
                    //}
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int InsertSMSRecForCust(string BrCode, string sCustNo, string Message, string AuthMid, string EDate, string sFlag)
    {
        try
        {
            if (sFlag == "WelCome")
            {
                sql = "Select Parameter From AVS1090 Where Message = 'WelCome' And Activity = 'SMSCust' And Parameter='Y'";
                sQuery = conn.sExecuteScalar(sql);
            }

            if (sQuery == "Y")
            {
                //sql = "Select IsNull(Mobile1, 0) From Avs_ContactD Where BrCd = '" + BrCode + "' And CustNo = '" + sCustNo + "' And Stage = '1003' And EffectDate = (Select Max(EffectDate) From Avs_ContactD Where BrCd = '" + BrCode + "' And CustNo = '" + sCustNo + "' And Stage = '1003')";
                sql = "Select IsNull(Mobile1, 0) From Avs_ContactD Where CustNo = '" + sCustNo + "' And Stage = '1003'";
                sMobNo = conn.sExecuteScalar(sql);

                if (sMobNo == null)
                    sMobNo = "0";

                //if (sMobNo != null && sMobNo != "" && Convert.ToInt32(sMobNo.Length) == 10)
                //{
                sql = "Insert Into AVS1092 (BRCD, CUSTNO, MOBILE, SMS_DATE, SMS_TYPE, SMS_DESCRIPTION, SMS_STATUS, OPERATOR, MID, VID, PCMAC, SYSTEMDATE) " +
                        "Values('" + BrCode + "', '" + sCustNo.ToString() + "','" + sMobNo.ToString() + "','" + conn.ConvertDate(EDate) + "','1','" + Message + "', '1', '" + sFlag.ToString() + "', '" + AuthMid + "', '" + AuthMid + "', '" + conn.PCNAME() + "', GetDate())";
                Result = conn.sExecuteQuery(sql);

                //for shoot sms to customer
                string SMS = MS.Send_SMS(sCustNo, EDate);
                //}
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string BankName(string BrCode)
    {
        try
        {
            sql = "Select BankName, MidName from BankName Where BrCd = '" + BrCode + "'";
            sQuery = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sQuery;
    }

    public int AddSMS_Desc(string BRCD, string CUSTNO, string MID, string EDT, string M_NUMBER, string FL)
    {
        try
        {
            if (FL == "ADD")
            {
                sql = "EXEC SP_SMS_INSERT @FLAG='" + FL + "',@CUSTNO='" + CUSTNO + "',@MOB_NUM='" + M_NUMBER + "',@SMS_DATE='" + conn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "',@MID='" + MID + "'";
            }
            else if (FL == "MOD")
            {
                sql = "EXEC SP_SMS_INSERT @FLAG='" + FL + "',@CUSTNO='" + CUSTNO + "',@MOB_NUM='" + M_NUMBER + "',@SMS_DATE='" + conn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "',@MID='" + MID + "'";
            }
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public double ClBalance(string BrCode, string PrCode, string AccNo, string EDate, string Flag)
    {
        try
        {
            sql = "Exec SP_OpClBalance '" + BrCode + "','" + PrCode + "','" + AccNo + "','" + conn.ConvertDate(EDate).ToString() + "','" + Flag + "'";
            balance = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return balance;
    }
    //End added by amol on 26/07/2017

    //Dhanya Shetty//04/08/2017
    public void BindBorrower(DropDownList DdlBtype)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1071' ORDER BY SRNO";
        conn.FillDDL(DdlBtype, sql);
    }
    public string GetBorrower(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1071' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOBorrower(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1071' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public string GetNOItem(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1072' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindItem(DropDownList DDLItype)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1072' ORDER BY SRNO";
        conn.FillDDL(DDLItype, sql);
    }
    public string GetItem(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1072' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOISub(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1073' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindISub(DropDownList DDLIsub)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1073' ORDER BY SRNO";
        conn.FillDDL(DDLIsub, sql);
    }
    public string GetSItem(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1073' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOPur(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1074' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindPurp(DropDownList DDlpur)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1074' ORDER BY SRNO";
        conn.FillDDL(DDlpur, sql);
    }
    public string GetPurp(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1074' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOSuPur(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1075' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindSubPurp(DropDownList DDlSubPu)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1075' ORDER BY SRNO";
        conn.FillDDL(DDlSubPu, sql);
    }
    public string GetSubPurp(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1075' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOCat1(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1079' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindCat1(DropDownList DDlCat1)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1079' ORDER BY SRNO";
        conn.FillDDL(DDlCat1, sql);
    }
    public string GetCat1(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1079' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOCat2(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1081' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindCat2(DropDownList DDlCat2)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1081' ORDER BY SRNO";
        conn.FillDDL(DDlCat2, sql);
    }
    public string GetCat2(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1081' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOSWCat(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1078' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindSWCat(DropDownList DDLSWCat)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1078' ORDER BY SRNO";
        conn.FillDDL(DDLSWCat, sql);
    }
    public string GetSWCat(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1078' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOLoan(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1086' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindLoan(DropDownList DDLLoan)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1086' ORDER BY SRNO";
        conn.FillDDL(DDLLoan, sql);
    }

    public string GetNOHealth(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1087' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindHealth(DropDownList DDlhealth)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1087' ORDER BY SRNO";
        conn.FillDDL(DDlhealth, sql);
    }
    public string GetHealth(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1087' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public void BindMainCls(DropDownList DDLMain)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1082' ORDER BY SRNO";
        conn.FillDDL(DDLMain, sql);
    }
    public void BindSrNo(DropDownList DDLMain,string brcd,string accno,string prdcd)
    {
        sql = "SELECT srno name,id id  FROM AVS_LNSECURITY WHERE brcd='" + brcd + "' and acctno='" + accno + "' and prdcode='" + prdcd + "'  ORDER BY SRNO";
        conn.FillDDL(DDLMain, sql);
    }
    public string GetMainCls(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1082' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOMainCls(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1082' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindSubCls(DropDownList DDLSub)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1083' ORDER BY SRNO";
        conn.FillDDL(DDLSub, sql);
    }
    public string GetSubCls(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1083' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOSubCls(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1083' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindSancAuth(DropDownList ddlauth)//ADDED BY ANKITA ON 09/08/2017 (DDL OF LOAN LIMIT)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1097' ORDER BY SRNO";
        conn.FillDDL(ddlauth, sql);
    }

    public void BindINSTMODTYPE(DropDownList ddlauth)//ADDED BY Aniket
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2555' and LTYPE='INSTMODTYPE' ORDER BY SRNO";
        conn.FillDDL(ddlauth, sql);
    }
    public void BindRecAuth(DropDownList ddlauth)//ADDED BY ANKITA ON 09/08/2017 (DDL OF LOAN LIMIT)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1098' ORDER BY SRNO";
        conn.FillDDL(ddlauth, sql);
    }
    public string GetNOStatus(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1064' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindStatus(DropDownList DDLstatusname)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1064' ORDER BY SRNO";
        conn.FillDDL(DDLstatusname, sql);
    }
    public string GetSta(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1064' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOItemD(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1065' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindDItem(DropDownList DDlitemno)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1065' ORDER BY SRNO";
        conn.FillDDL(DDlitemno, sql);
    }
    public string GetDitm(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1065' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOASL(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1067' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindASL(DropDownList DDlASL)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1067' ORDER BY SRNO";
        conn.FillDDL(DDlASL, sql);
    }
    public string GetASL(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1067' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNODep(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1066' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindDep(DropDownList DDlDep)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1066' ORDER BY SRNO";
        conn.FillDDL(DDlDep, sql);
    }
    public string GetDep(string AccT)
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1066' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public void BindSanction(DropDownList DDlSanction)
    {
        sql = "select GROUPDESC name,GROUPCODE id  from USERGROUP order by GROUPCODE";
        conn.FillDDL(DDlSanction, sql);
    }
    public string GetNOSanction(string aact)
    {
        sql = "SELECT GROUPCODE FROM USERGROUP WHERE  GROUPDESC='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public string GetSanctn(string AccT)
    {
        sql = "SELECT GROUPDESC FROM USERGROUP WHERE GROUPCODE='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public string GetNOTypeofitm(string aact)
    {
        sql = "SELECT ITEMNO FROM Item_Master WHERE  ITEMNAME='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindTypeofitm(DropDownList DDltypeofitm)
    {
        sql = "SELECT ITEMNAME name,ITEMNO id FROM Item_Master ORDER BY ITEMNO";
        conn.FillDDL(DDltypeofitm, sql);
    }
    public string GetItemtypeno(string AccT)
    {
        sql = "SELECT ITEMNAME FROM Item_Master WHERE  ITEMNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public void BindGlCode(DropDownList ddl, string BRCD)
    {
        sql = "Select SUBGLCODE as id,GLNAME as name from glmast where glcode in (3,5) and BRCD='" + BRCD + "'";
        conn.FillDDL(ddl, sql);
    }
    public string GetLoanAccType(string AccT, string BRCD)
    {
        sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE='" + AccT + "'  AND BRCD='" + BRCD + "' and glcode='3'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public void BindHeading(DropDownList ddl)// added by ankita on 29/08/2017 for passbook parameter
    {
        try
        {
            sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1099' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindHeadingCover(DropDownList ddl)// added by ankita on 01/09/2017 for passbook parameter
    {
        try
        {
            sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1100' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindDivision(DropDownList ddl)// added by ankita on 29/08/2017 for Employer details
    {
        try
        {
            sql = "select (convert(varchar(10),RECDIV)+'-'+convert(varchar(100),DESCR)) as name,RECDIV as id from paymast where RECCODE='0'";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindOffc(DropDownList ddl, string recdiv)// added by ankita on 29/08/2017 for Employer details
    {
        try
        {
            sql = "select (convert(varchar(10),RECCODE)+'-'+convert(varchar(100),DESCR)) as name,RECCODE as id from paymast where RECDIV='" + recdiv + "'";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindDesig(DropDownList ddl)
    {
        try
        {
            sql = "select DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1101' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void Bindpurpose(DropDownList DDlpurpose)//Dhanya Shetty//22/08/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1099' ORDER BY SRNO";
        conn.FillDDL(DDlpurpose, sql);
    }
    public string GetNOPurpose(string aact)
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1099' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public void BindPost(DropDownList ddl)// ankita 06/09/2017 for post of employees
    {
        try
        {
            sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1105' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindUnitM(DropDownList DDlunit)//Dhanya Shetty//05/09/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1102' ORDER BY SRNO";
        conn.FillDDL(DDlunit, sql);
    }

    public void BindCategoryP(DropDownList DDlcategory)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1103' ORDER BY SRNO";
        conn.FillDDL(DDlcategory, sql);
    }

    public void BindStatusP(DropDownList DDlstatus)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1104' ORDER BY SRNO";
        conn.FillDDL(DDlstatus, sql);
    }
    public void BindTypeP(DropDownList DDlType)//Dhanya Shetty//11/09/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1106' ORDER BY SRNO";
        conn.FillDDL(DDlType, sql);
    }
    public void BindPaymentOut(DropDownList ddldoc, string RefNo)//Dhanya Shetty//27/09/2017//For outward
    {
        sql = "select DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  where LNO='1033' and refno='" + RefNo + "' and srno<>1  order by SRNO";
        conn.FillDDL(ddldoc, sql);
    }
    public void BindFreezType(DropDownList ddl)// ankita 05/10/2017 for account freezetype
    {
        try
        {
            sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2010' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindFreezReason(DropDownList ddl)// ankita 05/10/2017 for account freezetype reason
    {
        try
        {
            sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2011' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindEffDate(DropDownList DDlAsondate)//Dhanya Shetty//07/10/2017 for interest master
    {
        sql = "Exec IntMasterEffcdate";
        conn.FillDDL(DDlAsondate, sql);
    }
    public void BindBulkType(DropDownList ddl)// ankita 09/11/2017 for bulk sms type
    {
        try
        {
            sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2506' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BINDUSERGRP(DropDownList ddl)// ankita 17/11/2017 for user group
    {
        try
        {
            sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2508' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BINDCurrency(DropDownList ddl)// ankita 20/11/2017 for currency
    {
        try
        {
            sql = "SELECT DESCRIPTION name,DESCRIPTION id FROM LOOKUPFORM1 WHERE LNO='2503' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindLoanDoc(ListBox List)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2501' ORDER BY SRNO";
        conn.FillList(List, sql);
    }
    public void BindRDHeading(DropDownList ddl)// added by Dhanya on 02/01/2018 for RDSetting 
    {
        try
        {
            sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2512 ' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindFDHeading(DropDownList ddl)// added by Dhanya on 02/01/2018 for FDSetting 
    {
        try
        {
            sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2511 ' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public string GetAccUp(string aact)// added by Dhanya Shetty on 30/01/2018 for Account Status 
    {
        sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE LNO='1047' AND DESCRIPTION='" + aact + "'";
        aact = conn.sExecuteScalar(sql);
        return aact;
    }
    public string GetAccStatusNew(string AccT)// added by Dhanya Shetty on 30/01/2018 for Account Status 
    {
        sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE LNO='1047' AND SRNO='" + AccT + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }
    public void BindStaffName(DropDownList DDlStaffName,string Brcd)//Dhanya Shetty//05/02/2017
    {
        sql = "SELECT USERNAME name,PERMISSIONNO id FROM USERMASTER  where brcd='"+Brcd+"' and stage<>1004 ORDER BY PERMISSIONNO";
        conn.FillDDL(DDlStaffName, sql);
    }

    public string GetStaffName(string Username)// added by Dhanya Shetty //05/02/2017 
    {
        sql = "SELECT PERMISSIONNO FROM USERMASTER WHERE USERNAME='" + Username + "'";
        Username = conn.sExecuteScalar(sql);
        return Username;
    }
    public void BindAccStatusD(DropDownList DdlMODE)// added by Dhanya Shetty //27/02/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1047' and srno not in (9,10,11) ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public void BindAccDemandD(DropDownList DdlMODE)// added by Dhanya Shetty //27/02/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1047' and srno  in (10,20,30,40,90) ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public void BindActionStatus(DropDownList DdlMODE)// added by Dhanya Shetty //27/02/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2573'  ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }

    public void BindFinancialActivity(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,DESCRIPTION id FROM LOOKUPFORM1 WHERE LNO='2552' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }

    public void BindtableActivity(DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,DESCRIPTION id FROM LOOKUPFORM1 WHERE LNO='2554' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
    public string MenuItem(string Username)// added by Dhanya Shetty //05/02/2017 
    {
        sql = "SELECT MENUTITLE+'_'+CONVERT(VARCHAR(100),parentmenuid) FROM AVS5016 WHERE parentmenuid='" + Username + "' ";
        Username = conn.sExecuteScalar(sql);
        return Username;
    }

    public string MenuTitle(string Username)// added by Dhanya Shetty //05/02/2017 
    {
        sql = "SELECT MENUTITLE+'_'+CONVERT(VARCHAR(100),MenuId) FROM AVS5016 WHERE MenuId='" + Username + "' ";
        Username = conn.sExecuteScalar(sql);
        return Username;
    }

   

    public string Get_MenuId()
    {
        string str = "";
        try
        {
            sql = "Select max(MenuId+1) from avs5016";
            str = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return str;
    }

    public void BindSHRActivtyALLType (DropDownList DdlMODE)
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2565' ORDER BY SRNO";
        conn.FillDDL(DdlMODE, sql);
    }
}