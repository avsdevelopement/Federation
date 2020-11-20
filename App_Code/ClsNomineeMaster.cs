using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsNomineeMaster
{
    string sql = "";
    DbConnection conn = new DbConnection();
    int Result;
    string Res = "";

    public ClsNomineeMaster()
    {
       
    }

    public int InsertNom(string CUSTNO, string NOMINEENAME, string RELATION, string DOB, string GLCODE, string SUBGLCODE, string ACCNO, string EFFECTDATE, string BRCD, string MID)
    {
        sql = "SELECT ISNULL(MAX(SRNO),0)+1 FROM NOMINEEDETAILS WHERE ACCNO='" + ACCNO + "' AND BRCD='" + BRCD + "'";
        string SRNO = conn.sExecuteScalar(sql);
        try
        {
            sql = "INSERT INTO NOMINEEDETAILS(CUSTNO,NOMINEENAME,SRNO,RELATION,DOB,GLCODE,SUBGLCODE,ACCNO,EFFECTDATE,BRCD,STAGE,MID,PCMAC) " +
            " VALUES('" + CUSTNO + "','" + NOMINEENAME + "','" + SRNO + "','" + RELATION + "','" + conn.ConvertDate(DOB).ToString() + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + conn.ConvertDate(EFFECTDATE).ToString() + "','" + BRCD + "','1001','" + MID + "','" + conn.PCNAME() + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int InsertNominee(string BrCode, string CustNo, string GlCode, string PrCode, string AccNo, string NomName, string Relation, string DOB, string EDate, string MId, string Gender, string Age, string SrNo, string Add1, string Add2, string Add3, string City, string PinCode, string MobNo, string PanCard)
    {
        //string SRNO = "0";

        try
        {
            //sql = "SELECT MAX(ISNULL(A.SRNO, 0)) + 1 AS SRNO FROM (SELECT MAX(ISNULL(SrNo, 0)) AS SRNO FROM NOMINEEDETAILS)A";
            //SRNO = conn.sExecuteScalar(sql);

            sql = "Exec RptNomineeDetails @BRCD = '" + BrCode + "', @CUSTNO = '" + CustNo + "', @GLCODE = '" + GlCode + "', @SUBGLCODE = '" + PrCode + "', @ACCNO = '" + AccNo + "', @NOMINEENAME = '" + NomName + "', @RELATION = '" + Relation + "', @DOB = '" + conn.ConvertDate(DOB).ToString() + "', @EFFECTDATE = '" + conn.ConvertDate(EDate).ToString() + "', @MID = '" + MId + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @GENDER = '" + Gender + "', @AGE = '" + Age + "', @SRNO = '" + SrNo + "', @Address1 = '" + Add1 + "', @Address2 = '" + Add2 + "', @Address3 = '" + Add3 + "', @City = '" + City + "', @PinCode = '" + PinCode + "', @MobNo = '" + MobNo + "', @PanNo = '" + PanCard + "', @Flag = 'AD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        //return (SRNO + '_' + Result).ToString();
        return Result;
    }

    public int UpdateNominee(string BrCode, string SrNo, string PrCode, string AccNo, string NomName, string Relation, string DOB, string EDate, string MId, string Gender, string Age, string Add1, string Add2, string Add3, string City, string PinCode, string MobNo, string PanCard)
    {
        try 
        {
            sql = "Exec RptNomineeDetails @BRCD = '" + BrCode + "', @SUBGLCODE = '" + PrCode + "', @ACCNO = '" + AccNo + "', @NOMINEENAME = '" + NomName + "', @RELATION = '" + Relation + "', @DOB = '" + conn.ConvertDate(DOB).ToString() + "', @EFFECTDATE = '" + conn.ConvertDate(EDate).ToString() + "', @MID = '" + MId + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @GENDER = '" + Gender + "', @AGE = '" + Age + "', @SRNO = '" + SrNo + "', @Address1 = '" + Add1 + "', @Address2 = '" + Add2 + "', @Address3 = '" + Add3 + "', @City = '" + City + "', @PinCode = '" + PinCode + "', @MobNo = '" + MobNo + "', @PanNo = '" + PanCard + "', @Flag = 'MD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }   
        return Result;
    }

    public int AuthoNominee(string BrCode, string SrNo, string PrCode, string AccNo, string Mid)
    {
        try 
        {
            sql = "Exec RptNomineeDetails @BRCD = '" + BrCode + "', @SUBGLCODE = '" + PrCode + "', @ACCNO = '" + AccNo + "', @SRNO = '" + SrNo + "', @MID = '" + Mid + "', @Flag = 'AT'";
        Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }   
        return Result;
    }

    public int DelNominee(string BrCode, string SrNo, string PrCode, string AccNo)
    {
        try
        {
            sql = "Exec RptNomineeDetails @BRCD = '" + BrCode + "', @SUBGLCODE = '" + PrCode + "', @ACCNO = '" + AccNo + "', @SRNO = '" + SrNo + "', @Flag = 'DL'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int BindData(GridView Gview, string BrCode)
    {
        try
        {
            sql = "select CustNo,CustAccno,Depositglcode,prnamt,rateofint,openingdate,duedate,period,intamt,maturityamt from depositinfo where brcd='" + BrCode + "' AND LMSTATUS=1 and  stage in(1001,1002)";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }   
        return Result;
    }


    public int BindDataCBS2(GridView Gview, string BrCode)
    {
        try
        {
            sql = "select isnull(RECSRNO,'0') RecSrno,CustNo,CustAccno,Depositglcode,prnamt,rateofint,openingdate,duedate,period,intamt,maturityamt,isnull(RECEIPT_NO,0)RECEIPT_NO from depositinfo where brcd='" + BrCode + "' AND LMSTATUS=1 and  stage in(1001,1002)";
           // sql = "select CustNo,CustAccno,Depositglcode,prnamt,rateofint,openingdate,duedate,period,intamt,maturityamt,RECSRNO from depositinfo where brcd='" + BrCode + "' AND LMSTATUS=1 and  stage in(1001,1002)";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int BindDataPro(GridView Gview, string BrCode,string pcode,string accno)
    {
        try
        {
            sql = "select CustNo,CustAccno,Depositglcode,prnamt,rateofint,openingdate,duedate,period,intamt,maturityamt from depositinfo where brcd='" + BrCode + "' AND depositglcode='" + pcode + "' and LMSTATUS='1' and custaccno='" + accno + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }


    public int BindDataProCBS2(GridView Gview, string BrCode, string pcode, string accno)
    {
        try
        {
            sql = "select isnull(RECSRNO,'0') RecSrno,CustNo,CustAccno,Depositglcode,prnamt,rateofint,openingdate,duedate,period,intamt,maturityamt,isnull(RECEIPT_NO,0)RECEIPT_NO from depositinfo where brcd='" + BrCode + "' AND depositglcode='" + pcode + "' and LMSTATUS='1' and custaccno='" + accno + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string GetProductName(string PrdCode, string BRCD)//BRCD ASHOK
    {
        string sql = "select ISNULL(GLNAME,'') from glmast WHERE SUBGLCODE ='" + PrdCode + "' and BRCD='" + BRCD + "'";
        string ProductName = conn.sExecuteScalar(sql);
        return ProductName;
    }
      public string GetAccnameaa(string brcd,string pcode,string accno)//BRCD ASHOK
    {
        string sql = "select custname from avs_acc a inner join master m on a.custno=m.custno  where a.subglcode='" + pcode + "' and a.brcd='" + brcd + "' and a.accno='" + accno + "'"; //and a.brcd=m.brcd UNIFICATION 
        string ProductName = conn.sExecuteScalar(sql);
        return ProductName;
    }
     public string GEtIntPayout(string brcd,string pcode)//BRCD ASHOK
    {
        string sql = "select category from depositgl where brcd='"+brcd+"'  and depositglcode='"+pcode+"'";
        string ProductName = conn.sExecuteScalar(sql);
        return ProductName; 
    }
     public string GetRecieptNo(string brcd, string pcode)//BRCD ASHOK
     {
         string sql = "select max(RECEIPT_NO)+1 from depositinfo where brcd='"+brcd+"' and depositglcode='"+pcode+"'";
         string ProductName = conn.sExecuteScalar(sql);
         return ProductName;
     }
    public string GetGLName(string AccT, string BRCD)
    {
        try 
        {
        sql = "SELECT GLNAME + '-' + Convert(varchar(10),glcode) FROM GLMAST WHERE SUBGLCODE='" + AccT + "' AND BRCD='" + BRCD + "'";
        BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
       }   
        return BRCD;
    }
    
    public string GetAccName(string ACNO, string AT, string BRCD)
    {
        try 
        {
            sql = "SELECT M.CUSTNAME+'-'+ Convert(varchar(10),AC.GLCODE)+'-'+ Convert(varchar(10),CONVERT(INT,M.CUSTNO)) FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO  WHERE AC.SUBGLCODE='" + AT + "' AND AC.BRCD='" + BRCD + "' AND AC.ACCNO='" + ACNO + "'";////ADDED CONVET TO INT BY ANKITA ON 07/07/2017
        ACNO = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }   
        return ACNO;
    }

    public DataTable GetInfo(string BrCode, string SrNo, string PrCode, string AccNo)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec RptNomineeDetails @BRCD = '" + BrCode + "', @SUBGLCODE = '" + PrCode + "', @ACCNO = '" + AccNo + "', @SRNO = '" + SrNo + "', @Flag = 'VW'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public string CheckStage(string SRNO, string BRCD)//BRCD ADDED --Abhishek
    {
        try 
        {
            sql = "select Stage from NOMINEEDETAILS where SRNO='" + SRNO + "' and BRCD='" + BRCD + "'";
        SRNO = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }   
        return SRNO;
    }

    public int CheckMid(string BRCD, string PrCode, string AccNo, string SrNo)
    {
        try
        {
            sql = "SELECT MID FROM NOMINEEDETAILS WHERE BRCD = '" + BRCD + "' AND SubGlCode = '" + PrCode + "' AND AccNo = '" + AccNo + "' AND SrNo = '" + SrNo + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result = 0;
        }
        return Result;
    }

    public int BindNominee(GridView Gview, string Brcd, string PrCode, string AccNo)//Dhanya Shetty//31/07/2017
   {
        try
        {
            sql = "Select SRNO,SUBGLCODE,ACCNO,NOMINEENAME,RELATION,DOB from NOMINEEDETAILS where  BRCD = '" + Brcd + "' AND SubGlCode = '" + PrCode + "' AND AccNo = '" + AccNo + "' and stage not in(1004)";
           Result = conn.sBindGrid(Gview, sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;

    }

    public string GetSRno(string brcd, string subglcode, string accno)//Dhanya Shetty//31/07/2017
    {
        try
        {
            sql = "select srno from nomineedetails where BRCD='" + brcd + "' and  SUBGLCODE='" + subglcode + "' and  ACCNO='" + accno + "'";
            Res = conn.sExecuteScalar(sql);
           
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public string DisplaySrno(string brcd,string accno,string subgl)//Dhanya Shetty//31/07/2017
    {
        string SRNO = "";
        try
        {
            sql = "select isnull(max(srno),'0')+1  AS SRNO from NOMINEEDETAILS where brcd='" + brcd + "' and accno='" + accno + "' and SUBGLCODE ='" + subgl + "'";
            //sql = "SELECT MAX(ISNULL(A.SRNO, 0)) + 1 AS SRNO FROM (SELECT MAX(ISNULL(SrNo, 0)) AS SRNO FROM NOMINEEDETAILS)A";
            SRNO = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            
            ExceptionLogging.SendErrorToText(Ex);
        }

        return SRNO;
    }
    public string GetStage(string BRCD, string Prdcode, string ACC, string SrNo)
    {
        try
        {
            sql = "SELECT STAGE FROM NOMINEEDETAILS WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + Prdcode + "' and accno='" + ACC + "'AND SrNo = '" + SrNo + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
}