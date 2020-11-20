using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsJointAccountOpening
{
    string sql = "";
    DbConnection conn = new DbConnection();
    int Result;

	public ClsJointAccountOpening()
	{
		
	}

    public int BindData(GridView Gview, string BRCD, string AT, string AC)
    {
        try
        {
            sql = "SELECT (CONVERT(VARCHAR(10),JOINTSRNO)+'_'+CONVERT(VARCHAR(10),CUSTNO)) AS JOINTSRNO,JOINTNAME,JOINTRELATION,JOINTAGE,JOINTDOB,JOINTGENDER,PHNO,STATE,DISTRICT,TALUKA,CITY,DOCTYPE,DOCNO,DOCDATE,DOCTYPE1,DOCNO1 FROM JOINT WHERE BRCD='" + BRCD + "' and stage NOT IN ('1003','1004') AND SUBGLCODE='"+AT+"' AND ACCNO='"+AC+"'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int BindDataVW(GridView Gview, string BRCD, string AT, string AC)/////Added by ankita to display records in grid while it is in view tab
    {
        try
        {
            sql = "SELECT (CONVERT(VARCHAR(10),JOINTSRNO)+'_'+CONVERT(VARCHAR(10),CUSTNO)) AS JOINTSRNO,JOINTNAME,JOINTRELATION,JOINTAGE,JOINTDOB,JOINTGENDER,PHNO,STATE,DISTRICT,TALUKA,CITY,DOCTYPE,DOCNO,DOCDATE,DOCTYPE1,DOCNO1 FROM JOINT WHERE BRCD='" + BRCD + "' and stage='1003' AND SUBGLCODE='" + AT + "' AND ACCNO='" + AC + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
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
            sql = "SELECT M.CUSTNAME+'-'+CONVERT(VARCHAR(10),AC.CUSTNO)+'-'+ Convert(varchar(10),AC.GLCODE) FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND AC.BRCD=M.BRCD WHERE AC.SUBGLCODE='" + AT + "' AND AC.BRCD='" + BRCD + "' AND AC.ACCNO='" + ACNO + "'";
            ACNO = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ACNO;
    }

    public int InsertJointDetails(string BRCD, string CUSTNO, string CustName, string glcode, string PrCode, string AccNo, string JointName, string DOB, string Age, string Relation, string PhoneNo, string Gender, string DocType1, string DocNo1, string State, string Dist, string Tal, string City, string DocType, string docNo, string DocDate, string EFFECTDATE, string MID, string JOINTCUSTNO)
    {
        try
        {
            sql = "EXEC SP_JointAccountDetails @JOINTNAME = '" + JointName + "', @JOINTRELATION = '" + Relation + "', @ENTRYDATE = '" + conn.ConvertDate(EFFECTDATE).ToString() + "', @GLCODE = '" + glcode + "', @SUBGLCODE = '" + PrCode + "', @CUSTNO = '" + CUSTNO + "', @CUSTNAME = '" + CustName + "', @ACCNO = '" + AccNo + "', @BRCD = '" + BRCD + "', @MID = '" + MID + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @JOINTAGE = '" + Age + "', @JOINTDOB = '" + conn.ConvertDate(DOB).ToString() + "', @JOINTGENDER = '" + Gender + "', @PHNO = '" + PhoneNo + "', @STATE = '" + State + "', @DISTRICT = '" + Dist + "', @TALUKA = '" + Tal + "', @CITY = '" + City + "', @DOCTYPE = '" + DocType + "', @DOCNO = '" + docNo + "', @DOCDATE = '" + DocDate + "', @DOCTYPE1 = '" + DocType1 + "', @DOCNO1 = '" + DocNo1 + "',@JOINTCUSTNO='"+JOINTCUSTNO+"', @Type = 'INSRT'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int UpdateJointDetails(string SrNo, string BRCD, string CUSTNO, string CustName, string glcode, string PrCode, string AccNo, string JointName, string DOB, string Age, string Relation, string PhoneNo, string Gender, string DocType1, string DocNo1, string State, string Dist, string Tal, string City, string DocType, string docNo, string DocDate, string EFFECTDATE, string MID, string JOINTCUSTNO)
    {
        try
        {
            sql = "EXEC SP_JointAccountDetails @JOINTNAME = '" + JointName + "', @JOINTRELATION = '" + Relation + "', @ENTRYDATE = '" + conn.ConvertDate(EFFECTDATE).ToString() + "', @GLCODE = '" + glcode + "', @SUBGLCODE = '" + PrCode + "', @CUSTNO = '" + CUSTNO + "', @CUSTNAME = '" + CustName + "', @ACCNO = '" + AccNo + "', @BRCD = '" + BRCD + "', @MID = '" + MID + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @JOINTAGE = '" + Age + "', @JOINTDOB = '" + conn.ConvertDate(DOB).ToString() + "', @JOINTGENDER = '" + Gender + "', @PHNO = '" + PhoneNo + "', @STATE = '" + State + "', @DISTRICT = '" + Dist + "', @TALUKA = '" + Tal + "', @CITY = '" + City + "', @DOCTYPE = '" + DocType + "', @DOCNO = '" + docNo + "', @DOCDATE = '" + DocDate + "', @DOCTYPE1 = '" + DocType1 + "', @DOCNO1 = '" + DocNo1 + "', @SRNUM = '" + SrNo + "',@JOINTCUSTNO='" + JOINTCUSTNO + "', @Type = 'MODFY'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteJointDetails(string SRNO, string brcd, string custno, string glcode, string subglcode, string VID, string AccNo)
    {
        try
        {
            sql = "EXEC SP_JointAccountDetails @GLCODE = '" + glcode + "', @SUBGLCODE = '" + subglcode + "', @CUSTNO = '" + custno + "', @ACCNO = '" + AccNo + "', @BRCD = '" + brcd + "', @SRNUM = '" + SRNO + "', @Type = 'DEL'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int AuthoJointDetails(string SRNO, string brcd, string custno, string glcode, string subglcode, string VID, string AccNo)
    {
        try
        {
            sql = "EXEC SP_JointAccountDetails @GLCODE = '" + glcode + "', @SUBGLCODE = '" + subglcode + "', @CUSTNO = '" + custno + "', @ACCNO = '" + AccNo + "', @BRCD = '" + brcd + "', @SRNUM = '" + SRNO + "', @MID = " + VID + ", @Type = 'AUTHO'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string CheckStage(string SRNO, string brcd, string custno, string glcode, string subglcode, string ACCNO)
    {
        try
        {
            sql = "SELECT STAGE FROM JOINT WHERE BRCD='" + brcd + "' AND GLCODE = '" + glcode + "' AND SUBGLCODE = '" + subglcode + "' AND CUSTNO = '" + custno + "' AND JOINTSRNO = '" + SRNO + "'";
            SRNO = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return SRNO;
    }
    public string CheckMID(string SRNO, string brcd, string custno, string glcode, string subglcode, string ACCNO)
    {
        try
        {
            sql = "SELECT MID FROM JOINT WHERE BRCD='" + brcd + "' AND GLCODE = '" + glcode + "' AND SUBGLCODE = '" + subglcode + "' AND CUSTNO = '" + custno + "' AND JOINTSRNO = '" + SRNO + "'";
            SRNO = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return SRNO;
    }
    public DataTable GetInfo(string BRCD, string CUSTNO, string ID)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT J.*, G.GLNAME, M.CUSTNAME FROM JOINT J " +
                  "INNER JOIN GLMAST G WITH(NOLOCK) ON G.BRCD = J.BRCD AND G.GLCODE = J.GLCODE AND G.SUBGLCODE = J.SUBGLCODE " +
                  "INNER JOIN MASTER M WITH(NOLOCK) ON M.BRCD = J.BRCD AND M.CUSTNO = J.CUSTNO " +
                  "WHERE J.BRCD = '" + BRCD + "' and J.JOINTSRNO = '" + ID + "' AND J.CUSTNO = '" + CUSTNO + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }
    public DataTable GetInfoVw(string BRCD, string CUSTNO)/////Added by ankita to display records on textbox while it is in view tab
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT J.*, G.GLNAME, M.CUSTNAME FROM JOINT J " +
                  "INNER JOIN GLMAST G WITH(NOLOCK) ON G.BRCD = J.BRCD AND G.GLCODE = J.GLCODE AND G.SUBGLCODE = J.SUBGLCODE " +
                  "INNER JOIN MASTER M WITH(NOLOCK) ON M.BRCD = J.BRCD AND M.CUSTNO = J.CUSTNO " +
                  "WHERE J.BRCD = '" + BRCD + "' AND J.CUSTNO = '" + CUSTNO + "' AND J.STAGE='1003'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public DataTable GetCustInfo(string BRCD, string CUSTNO)/////Added by ankita to display CUSTOMER RECORDS 13/06/2017
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT  M.FIRSTNAME,M.LASTNAME,M.SURNAME,CONVERT(VARCHAR(10),M.DOB,103)DOB,M.CUSTAGE,M.CUSTSEX,A.STATE,A.DISTRICT,A.AREA_TALUKA,A.PINCODE,A.CITY,A.adoctype,A.adocno,A.adocdate," +
                  "I.DOC_TYPE,I.DOC_NO,I.DOC_DATE, D.MOBILE1 from MASTER M " +
                  "LEFT JOIN ADDMAST A ON M.BRCD=A.BRCD AND M.CUSTNO=A.CUSTNO " +
                  "LEFT JOIN IDENTITY_PROOF I ON M.BRCD=I.BRCD AND M.CUSTNO=I.CUSTNO " +
                  "LEFT JOIN AVS_CONTACTD D ON M.BRCD=D.BRCD AND M.CUSTNO=D.Custno " +
                  "WHERE M.BRCD='" + BRCD + "' AND M.CUSTNO='" + CUSTNO + "' AND M.STAGE=1003";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }
}