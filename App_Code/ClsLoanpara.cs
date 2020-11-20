using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for ClsLoanpara
/// </summary>
public class ClsLoanpara
{
    DbConnection conn = new DbConnection();
    string Result, sql;
    DataTable DT = new DataTable();
    public ClsLoanpara()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string GETMAX(string BRCD)
    {
        sql = "SELECT MAX(SUBGLCODE)+1 FROM GLMAST WHERE GLCODE=3 AND BRCD='" + BRCD + "'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string GETMAX11(string BRCD)
    {
        sql = "SELECT MAX(SUBGLCODE)+1 FROM GLMAST WHERE GLCODE=11 AND BRCD='" + BRCD + "'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public void BindSTATUS(DropDownList DDL)
    {
        sql = "SELECT DESCRIPTION name,SRNO id from LOOKUPFORM1 WHERE LNO=1040 ORDER BY SRNO";
        conn.FillDDL(DDL, sql);
    }
    public void BindINTCAT(DropDownList DDL)
    {
        sql = "SELECT DESCRIPTION name,SRNO id from LOOKUPFORM1 WHERE LNO=1039 ORDER BY SRNO";
        conn.FillDDL(DDL, sql);
    }
    public void BindLOANTYPE(DropDownList DDL)
    {
        sql = "SELECT DESCRIPTION name,SRNO id from LOOKUPFORM1 WHERE LNO=1050 ORDER BY SRNO";
        conn.FillDDL(DDL, sql);
    }
    public int INSERTLOANGL(string PDCODDE, string PDNAME, string OIRCODE, string OIRNAME, string CURRANCY, string LOANLIMIT, string ROI, string PENALINT, string STATUS, string EFFECTDATE, string LOANTYPE, string PLACC, string PLACCNAME, string PLHEAD, string PLHEADNAME, string INTCAL, string BRCD, string subgl, string glloan)
    {
        sql = "INSERT INTO LOANGL (LOANGLCODE,LOANTYPE,OIRGL,OIRGLSUB,CURRANCY,LOANLIMIT,ROI,PENALINT,STATUS,EFFECTIVEDATE,LOANCATEGORY,PGL,PPL,PAH,PENAL_ACCHEAD,INTCALTYPE,BRCD,intid,intsub,STAGE) VALUES ('" + PDCODDE + "','" + PDNAME + "','" + OIRCODE + "','" + OIRNAME + "','" + CURRANCY + "','" + LOANLIMIT + "','" + ROI + "','" + PENALINT + "','" + STATUS + "','" + conn.ConvertDate(EFFECTDATE) + "','" + LOANTYPE + "','" + PLACC + "','" + PLACCNAME + "','" + PLHEAD + "','" + PLHEADNAME + "','" + INTCAL + "','" + BRCD + "','" + glloan + "','" + subgl + "','1001')";

        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int INSERTGL11(string INTR, string INTRNAME, string BRCD)
    {
        sql = "INSERT INTO GLMAST (GLCODE,SUBGLCODE,GLNAME,BRCD)VALUES(11,'" + INTR + "','" + INTRNAME + "','" + BRCD + "')";
        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public string GETGLCO(string BRCD, string SUBGL)
    {
        sql = "SELECT GLCODE FROM GLMAST WHERE SUBGLCODE='" + SUBGL + "' AND BRCD='" + BRCD + "'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public int insert3(string INTR, string INTRNAME, string BRCD)
    {
        sql = "INSERT INTO GLMAST (GLCODE,SUBGLCODE,GLNAME,BRCD)VALUES(3,'" + INTR + "','" + INTRNAME + "','" + BRCD + "')";
        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public string getorgname(string SUBGL, string BRCD)
    {
        sql = "select GLNAME from GLMAST where SUBGLCODE='" + SUBGL + "' and BRCD='" + BRCD + "'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public int bindgrid(string pcode, string brcd, GridView Gview)
    {
        sql = "SELECT LG.LOANGLCODE,GL.GLNAME,LG.LOANTYPE,LG.LOANLIMIT,LG.ROI,LG.PENALINT,LG.intsub FROM LOANGL LG  Inner Join GLMAST GL On LG.BRCD = GL.BRCD And LG.intid= GL.GLCODE And LG.intsub = GL.Subglcode WHERE GLCODE=11 AND LG.LOANGLCODE='" + pcode + "' AND LG.BRCD='" + brcd + "'";
        int Result = conn.sBindGrid(Gview, sql);
        return Result;
    }
    public DataTable showdata(string LOANGLCODE, string BRCD)
    {
        DataTable DT = new DataTable();
        sql = "SELECT LOANGLCODE,LOANTYPE,LOANCATEGORY,ROI,PERIOD,PGL,PPL,OTHERCHG,INTCALTYPE,LOANLIMIT,PENALINT,EFFECTIVEDATE,Int_App,SECURED FROM LOANGL WHERE LOANGLCODE='" + LOANGLCODE + "'  AND BRCD='" + BRCD + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public int UPDATELOAN(string LOANTYPE, string OIRL, string OIRGLSUB, string CURRENCY, string LOANLIMIT, string ROI, string PENAL, string STATUS, string EFFDATE, string LOANCAT, string PGL, string PPL, string PAH, string PENHEAD, string INTCAL, string INTID, string INTSUB, string LGCODE, string BRCD)
    {
        sql = "UPDATE LOANGL SET LOANTYPE='" + LOANTYPE + "',OIRGL='" + OIRL + "',OIRGLSUB='" + OIRGLSUB + "',CURRANCY='" + CURRENCY + "',LOANLIMIT='" + LOANLIMIT + "',ROI='" + ROI + "',PENALINT='" + PENAL + "',STATUS='" + STATUS + "',EFFECTIVEDATE='" + EFFDATE + "',LOANCATEGORY='" + LOANCAT + "',PGL='" + PGL + "',PPL='" + PPL + "',PAH='" + PAH + "',PENAL_ACCHEAD='" + PENHEAD + "',INTCALTYPE='" + INTCAL + "',intid='" + INTID + "',intsub='" + INTSUB + "' WHERE LOANGLCODE='" + LGCODE + "' AND BRCD='" + BRCD + "',STAGE=1002";
        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int UPDATGLMAST11(string GLNAME, string SUGLCODE, string BRCD)
    {
        sql = "UPDATE  GLMAST SET GLNAME='" + GLNAME + "' WHERE BRCD='" + BRCD + "'AND GLCODE='11'AND SUBGLCODE='" + SUGLCODE + "'";
        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int UPDATGLMAST3(string GLNAME, string SUGLCODE, string BRCD)
    {
        sql = "UPDATE  GLMAST SET GLNAME='" + GLNAME + "' WHERE BRCD='" + BRCD + "'AND GLCODE='3'AND SUBGLCODE='" + SUGLCODE + "'";
        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int DELETELOAN(string SUGLCODE, string BRCD)
    {
        sql = "DELETE FROM LOANGL WHERE LOANGLCODE='" + SUGLCODE + "' AND BRCD='" + BRCD + "'";
        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int DELETEGLMAST3(string SUGLCODE, string BRCD)
    {
        sql = "DELETE FROM GLMAST WHERE GLCODE=3 AND SUBGLCODE='" + SUGLCODE + "' AND BRCD='" + BRCD + "'";
        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int DELETEGLMAST11(string SUGLCODE, string BRCD)
    {
        sql = "DELETE FROM GLMAST WHERE GLCODE=11 AND SUBGLCODE='" + SUGLCODE + "' AND BRCD='" + BRCD + "'";
        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int authorize(string glcode, string brcd)
    {
        sql = "UPDATE LOANGL SET STAGE=1003 WHERE LOANGLCODE='" + glcode + "' AND BRCD='" + brcd + "'";
        int result = conn.sExecuteQuery(sql);
        return result;
    }
    public int INSERTDATA(string LOANGL, string LOANTYPE, string LOANCAT, string ROI, string PERIOD, string PGL, string PPL, string OTHERCHR, string INCALTYPE, string LOANLIMIT, string PENINT, string EFFECTDATE, string BRCD, string INT_APP, string SECURED)
    {
        sql = "INSERT INTO LOANGL (LOANGLCODE,LOANTYPE,LOANCATEGORY,ROI,PERIOD,PGL,PPL,OTHERCHG,INTCALTYPE,LOANLIMIT,PENALINT,EFFECTIVEDATE,BRCD,Int_App,SECURED) VALUES('" +
            LOANGL + "','" + LOANTYPE + "','" + LOANCAT + "','" + ROI + "','" + PERIOD + "','" + PGL + "','" + PPL + "','" + OTHERCHR + "','" + INCALTYPE + "','" + LOANLIMIT + "','" + PENINT + "','" + conn.ConvertDate(EFFECTDATE) + "','" + BRCD + "','" + INT_APP + "','" + SECURED + "')";
        int result = conn.sExecuteQuery(sql);
        return result;
    }

    public int UPDATEdata(string LOANGL, string LOANTYPE, string LOANCAT, string ROI, string PERIOD, string PGL, string PPL, string OTHERCHR, string INCALTYPE, string LOANLIMIT, string PENINT, string EFFECTDATE, string BRCD, string INT_APP, string SECURED)
    {
        sql = "UPDATE LOANGL SET LOANTYPE='" + LOANTYPE + "',   LOANCATEGORY='" + LOANCAT + "',ROI='" + ROI + "',PERIOD='" + PERIOD + "',PGL='" + PGL + "',PPL='" + PPL + "',OTHERCHG='" + OTHERCHR + "',INTCALTYPE='" + INCALTYPE + "',LOANLIMIT='" + LOANLIMIT + "',PENALINT='" + PENINT + "',EFFECTIVEDATE='" + conn.ConvertDate(EFFECTDATE) + "',Int_App='" + INT_APP + "',SECURED='" + SECURED + "' WHERE BRCD='" + BRCD + "' AND LOANGLCODE='" + LOANGL + "'";
        int result = conn.sExecuteQuery(sql);
        return result;
    }
}