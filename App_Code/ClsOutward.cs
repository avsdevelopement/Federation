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

/// <summary>
/// Summary description for ClsOutward
/// </summary>
public class ClsOutward
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    int Result;
    string sql;
	public ClsOutward()
	{
		
	}
    public int Insertdata(string TranscationType,string Beneficiarycode,string BeneficiaryAccNo,string InstrumentAmount,string BeneficiaryName,string DraweeLoc,string PrintLoc,string BeneAdd1,
    string BeneAdd2,string BeneAdd3,string BeneAdd4,string BeneAdd5,string InstructionRefNo,string CustRefNo,string Payment1,string Payment2,string Payment3,string Payment4,string Payment5,
        string Payment6,string Payment7,string ChequeNo,string ChqDate,string MICRNo,string IFCcode,string BeneBankName,string BenebranchName,string BeneEmailId,string Stage,string Brcd,
        string Paymenttype, string Productcode,string AccNo,string InstNo,string InstDate,string Narration,string Balance,string Mid)
    {
        try
        {
            sql = "Insert into Outward(TranscationType	,Beneficiarycode,BeneficiaryAccNo,InstrumentAmount,BeneficiaryName,DraweeLoc,PrintLoc,BeneAdd1,BeneAdd2,BeneAdd3,BeneAdd4,BeneAdd5,"+
            "InstructionRefNo,CustRefNo,Payment1,Payment2,Payment3,Payment4,Payment5,Payment6,Payment7,ChequeNo,ChqDate,MICRNo,IFCcode,BeneBankName,BenebranchName,BeneEmailId,Stage,"+
            "Brcd,PaymentType ,Productcode ,AccountNo ,InstrumentNo,InstDate ,Narration ,Balance,Mid )values('"+TranscationType+"','" + Beneficiarycode + "','" + BeneficiaryAccNo + "',"+
            "'" + InstrumentAmount + "','" + BeneficiaryName + "','"+DraweeLoc+"','"+PrintLoc+"','"+BeneAdd1+"','"+BeneAdd2+"','" + BeneAdd3 + "','"+BeneAdd4+"','"+BeneAdd5+"',"+
            "'"+InstructionRefNo+"','"+CustRefNo+"','"+Payment1+"','"+Payment2+"','"+Payment3+"','"+Payment4+"','"+Payment5+"','"+Payment6+"','" + Payment7 + "','" + ChequeNo + "',"+
            "'" + conn.ConvertDate(ChqDate).ToString() + "','" + MICRNo + "','" + IFCcode + "','" + BeneBankName + "','" + BenebranchName + "','" + BeneEmailId + "','" + Stage + "',"+
            "'" + Brcd + "','" + Paymenttype + "','" + Productcode + "','" + AccNo + "','" + InstNo + "','" + conn.ConvertDate(InstDate).ToString() + "','" + Narration + "','" + Balance + "','"+Mid+"') ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int ModifyData(string TranscationType, string Beneficiarycode, string BeneficiaryAccNo, string InstrumentAmount, string BeneficiaryName, string DraweeLoc, string PrintLoc, string BeneAdd1,
    string BeneAdd2, string BeneAdd3, string BeneAdd4, string BeneAdd5, string InstructionRefNo, string CustRefNo, string Payment1, string Payment2, string Payment3, string Payment4, string Payment5,
        string Payment6, string Payment7, string ChequeNo, string ChqDate, string MICRNo, string IFCcode, string BeneBankName, string BenebranchName, string BeneEmailId, string Stage, string Brcd)
    {
        try
        {
            sql = "update Outward set TranscationType='"+TranscationType+"',Beneficiarycode='"+Beneficiarycode+"',BeneficiaryAccNo='"+BeneficiaryAccNo+"',InstrumentAmount='"+InstrumentAmount+"',"+
                "BeneficiaryName='"+BeneficiaryName+"',DraweeLoc='"+DraweeLoc+"',PrintLoc='"+PrintLoc+"',BeneAdd1='"+BeneAdd1+"',BeneAdd2='"+BeneAdd2+"',BeneAdd3='"+BeneAdd3+"',BeneAdd4='"+BeneAdd4+"',"+
                "BeneAdd5='" + BeneAdd5 + "',InstructionRefNo='" + InstructionRefNo + "',CustRefNo='" + CustRefNo + "',Payment1='"+Payment1+"',Payment2='"+Payment2+"',Payment3='"+Payment3+"',"+
                "Payment4='"+Payment4+"',Payment5='"+Payment5+"',Payment6='"+Payment6+"',Payment7='"+Payment7+"',ChequeNo='"+ChequeNo+"',ChqDate='"+ChqDate+"',MICRNo='"+MICRNo+"',IFCcode='"+IFCcode+"',"+
                "BeneBankName='" + BeneBankName + "',BenebranchName='" + BenebranchName + "',BeneEmailId='" + BeneEmailId + "',Brcd='" + Brcd + "', Stage='1002' where Brcd='" + Brcd + "' and BeneficiaryAccNo='" + BeneficiaryAccNo + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int AuthoriseData(string Brcd, string BeneficiaryAccNo, string ID,string mid)
    {
        try
        {
            sql = "update Outward set stage=1003, Vid='" + mid + "' where  Brcd='" + Brcd + "' and BeneficiaryAccNo='" + BeneficiaryAccNo + "' and ID='" + ID + "' and stage not in(1004,1009)";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteData(string Brcd, string BeneficiaryAccNo, string ID,string mid)
    {
        try
        {
            sql = "update Outward set stage=1004, mid='"+mid+"' where  Brcd='" + Brcd + "' and BeneficiaryAccNo='" + BeneficiaryAccNo + "' and ID='" + ID + "' and stage not in(1003,1009)";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable GetInfo(string Brcd, string BeneficiaryAccNo, string ID)
{
    try 
	{
        sql = "Select PaymentType,Productcode,AccountNo,InstrumentNo,InstDate,Narration,TranscationType,Beneficiarycode,BeneficiaryAccNo,InstrumentAmount,BeneficiaryName,DraweeLoc,PrintLoc,BeneAdd1,BeneAdd2,BeneAdd3,BeneAdd4,BeneAdd5,InstructionRefNo,CustRefNo" +
            ",Payment1,Payment2,Payment3,Payment4,Payment5,Payment6,Payment7,ChequeNo,ChqDate,MICRNo,IFCcode,BeneBankName,BenebranchName,BeneEmailId,Stage,Brcd from Outward where Brcd='" + Brcd + "' and BeneficiaryAccNo='" + BeneficiaryAccNo + "' and ID='" + ID + "'";
        DT = conn.GetDatatable(sql);
	}
	catch (Exception Ex)
	{
		ExceptionLogging.SendErrorToText(Ex);
	}
    return DT;
}

    public DataTable FetchTextREcords(string Id, string BeneficiaryAccNo)
    {
        
        try
        {
            sql = "Exec Isp_AVS0019 @Id='" + Id + "',@BeneficiaryAccNo='" + BeneficiaryAccNo + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public string Getaccno(string AT, string BRCD)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),GLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public double GetOpenClose(string Brcode, string ProdCode, string AccNo, string EDate, string Flag)
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OpClBalance @BrCode = '" + Brcode + "', @SubGlCode = '" + ProdCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }

    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO)+'_'+CONVERT(VARCHAR(10),AC.GLCODE) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND M.BRCD=AC.BRCD WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int BindGrid(GridView Gview, string brcd)
    {
        try
        {
            sql = "Select Id,PaymentType,Productcode,AccountNo,BeneficiaryAccNo,InstrumentAmount,BeneficiaryName,BeneBankName,IFCcode from Outward where stage=1001 and brcd='" + brcd + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
       }
        return Result;
    }
    public int UploadStatus(string Brcd, string BeneficiaryAccNo)
    {
        try
        {
            sql = "update Outward set stage=1009 where  Brcd='" + Brcd + "' and BeneficiaryAccNo='" + BeneficiaryAccNo + "' and stage not in(1004)";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}