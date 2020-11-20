using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsShareParameter
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0, RM;

	public ClsShareParameter()
	{
		
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

    public int BindData(GridView Gview, string BRCD)
    {
        try
        {
            sql = "SELECT * FROM AVS_SHRPARA WHERE BRCD = '" + BRCD + "' AND STAGE NOT IN ('1003','1004')";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetInfo(string BrCode, string Id)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select S.Id, S.SHR_VALUE, S.SHR_FROM, S.NO_OF_SHARES, S.EnterenceAmt, S.WelFareAmt, S.WelFareLoanAmt, S.SHARES_GL, G1.GlName ShrProdName, S.ENTRY_GL, G2.GlName EntryProdName, "+
                  "S.SAVING_GL, G3.GlName SavProdName, S.OTHERS_GL1, G4.GlName WelProdName, S.OTHERS_GL2, G5.GlName PrintProdName, S.MemberWel_GL, G6.GlName WelLoanProdName, S.Service_GL, G7.GlName ServiceProdName, "+
                  "s.BCLASS_GL, g8.glname bclassname, s.div1, g9.glname div1name, s.div2, g10.glname div2name , s.div3, g11.glname div3name, "+
                  "S.OTHERS_GL3, G12.GlName As GlName3, S.OTHERS_GL4, G13.GlName As GlName4, S.OTHERS_GL5, G14.GlName As GlName5 " +
                  "From AVS_SHRPARA S " +
                  "Left Join GlMast G1 With(NoLock) On S.BrCd = G1.BrCd And S.SHARES_GL = G1.SubGlCode "+
                  "Left Join GlMast G2 With(NoLock) On S.BrCd = G2.BrCd And S.ENTRY_GL = G2.SubGlCode " +
                  "Left Join GlMast G3 With(NoLock) On S.BrCd = G3.BrCd And S.SAVING_GL = G3.SubGlCode " +
                  "Left Join GlMast G4 With(NoLock) On S.BrCd = G4.BrCd And S.OTHERS_GL1 = G4.SubGlCode " +
                  "Left Join GlMast G5 With(NoLock) On S.BrCd = G5.BrCd And S.OTHERS_GL2 = G5.SubGlCode " +
                  "Left Join GlMast G6 With(NoLock) On S.BrCd = G6.BrCd And S.MemberWel_GL = G6.SubGlCode " +
                  "Left Join GlMast G8 With(NoLock) On S.BrCd = G8.BrCd And S.BCLASS_GL = G8.SubGlCode " +
                  "Left Join GlMast G9 With(NoLock) On S.BrCd = G9.BrCd And S.div1 = G9.SubGlCode " +
                  "Left Join GlMast G10 With(NoLock) On S.BrCd = G10.BrCd And S.div2 = G10.SubGlCode " +
                  "Left Join GlMast G11 With(NoLock) On S.BrCd = G11.BrCd And S.div3 = G11.SubGlCode " +
                  "Left Join GlMast G12 With(NoLock) On S.BrCd = G12.BrCd And S.div3 = G12.SubGlCode " +
                  "Left Join GlMast G13 With(NoLock) On S.BrCd = G13.BrCd And S.div3 = G13.SubGlCode " +
                  "Left Join GlMast G14 With(NoLock) On S.BrCd = G14.BrCd And S.div3 = G14.SubGlCode " +
                  "Left Join GlMast G7 With(NoLock) On S.BrCd = G7.BrCd And S.Service_GL = G7.SubGlCode Where S.BRCD = '" + BrCode + "' And S.Id = '" + Id + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public int CheckMid(string BrCode, string Id)
    {
        try
        {
            sql = "SELECT MID FROM AVS_SHRPARA Where Id = '" + Id + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result = 0;
        }
        return Result;
    }

    public int InsertData(string BrCode, string ShrValue, string ShrFrom, string NoOfShr, string EnterenceFee, string WelfareFee, string WelLoanFee, string ShrPrCode, string ShrEntryFee, string ShrEntryFeeName, string SavProdCode, string OtherFee1, string OtherFee1Name, string OtherFee2, string OtherFee2Name, string OtherFee3, string OtherFee3Name, string OtherFee4, string OtherFee4Name, string OtherFee5, string OtherFee5Name, string MemWelfare, string MemWelfareName, string ServiceChg, string ServiceChgName, string EDate, string Mid, string BCLASS_GL, string div1, string div2, string div3)//BCLASS_GL,div1,div2,div3 added by ankita 12/09/2017 to add dividend parameters 
    {
        try
        {
            sql = "Exec SP_ShareParam @BRCD = '" + BrCode + "', @SHR_VALUE = '" + ShrValue + "', @SHR_FROM = '" + ShrFrom + "', @NO_OF_SHARES = '" + NoOfShr + "', @EnterenceAmt = " + EnterenceFee + ", @WelFareAmt = " + WelfareFee + ", @WelLoanAmt = " + WelLoanFee + ", @SHARES_GL = '" + ShrPrCode + "',  @ENTRY_GL = '" + ShrEntryFee + "',  @ENTRY_GLName = '" + ShrEntryFeeName + "', @SAVING_GL = '" + SavProdCode + "', @OTHERS_GL1 = '" + OtherFee1 + "', @OTHERS_GL1Name = '" + OtherFee1Name + "', @OTHERS_GL2 = '" + OtherFee2 + "', @OTHERS_GL2Name = '" + OtherFee2Name + "', @OTHERS_GL3 = '" + OtherFee3 + "', @OTHERS_GL3Name= '" + OtherFee3Name + "', @OTHERS_GL4 = '" + OtherFee4 + "', @OTHERS_GL4Name = '" + OtherFee4Name + "', @OTHERS_GL5 = '" + OtherFee5 + "', @OTHERS_GL5Name = '" + OtherFee5Name + "', @MemberWel_GL = '" + MemWelfare + "', @MemberWel_GLName = '" + MemWelfareName + "', @Service_GL = '" + ServiceChg + "', @Service_GLName = '" + ServiceChgName + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag ='AD',@BCLASS_GL='" + BCLASS_GL + "',@div1='" + div1 + "',@div2='" + div2 + "',@div3='" + div3 + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int ModifyData(string Id, string BrCode, string ShrValue, string ShrFrom, string NoOfShr, string EnterenceFee, string WelfareFee, string WelLoanFee, string ShrPrCode, string ShrEntryFee, string ShrEntryFeeName, string SavProdCode, string OtherFee1, string OtherFee1Name, string OtherFee2, string OtherFee2Name, string OtherFee3, string OtherFee3Name, string OtherFee4, string OtherFee4Name, string OtherFee5, string OtherFee5Name, string MemWelfare, string MemWelfareName, string ServiceChg, string ServiceChgName, string EDate, string Mid, string BCLASS_GL, string div1, string div2, string div3)//BCLASS_GL,div1,div2,div3 added by ankita 12/09/2017 to add dividend parameters 
    {
        try
        {
            //sql = "Exec SP_ShareParam @BRCD = '" + Id + "', @SHR_VALUE = '" + ShrValue + "', @SHR_FROM = '" + ShrFrom + "', @NO_OF_SHARES = '" + NoOfShr + "', @EnterenceAmt = " + EnterenceFee + ", @WelFareAmt = " + WelfareFee + ", @WelLoanAmt = " + WelLoanFee + ", @SHARES_GL = '" + ShrPrCode + "', @ENTRY_GL = '" + ShrEntryFee + "', @SAVING_GL = '" + SavProdCode + "', @OTHERS_GL1 = '" + OtherFee1 + "', @OTHERS_GL2 = '" + OtherFee2 + "', @OTHERS_GL3 = '" + OtherFee3 + "', @OTHERS_GL4 = '" + OtherFee4 + "', @OTHERS_GL5 = '" + OtherFee5 + "', @MemberWel_GL = '" + MemWelfare + "', @Service_GL = '" + ServiceChg + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag ='MD',@BCLASS_GL='" + BCLASS_GL + "',@div1='" + div1 + "',@div2='" + div2 + "',@div3='" + div3 + "'";
            sql = "Exec SP_ShareParam @BRCD = '" + BrCode + "', @SHR_VALUE = '" + ShrValue + "', @SHR_FROM = '" + ShrFrom + "', @NO_OF_SHARES = '" + NoOfShr + "', @EnterenceAmt = " + EnterenceFee + ", @WelFareAmt = " + WelfareFee + ", @WelLoanAmt = " + WelLoanFee + ",  @SHARES_GL = '" + ShrPrCode + "',  @ENTRY_GL = '" + ShrEntryFee + "',  @ENTRY_GLName = '" + ShrEntryFeeName + "', @SAVING_GL = '" + SavProdCode + "', @OTHERS_GL1 = '" + OtherFee1 + "', @OTHERS_GL1Name = '" + OtherFee1Name + "', @OTHERS_GL2 = '" + OtherFee2 + "', @OTHERS_GL2Name = '" + OtherFee2Name + "', @OTHERS_GL3 = '" + OtherFee3 + "', @OTHERS_GL3Name= '" + OtherFee3Name + "', @OTHERS_GL4 = '" + OtherFee4 + "', @OTHERS_GL4Name = '" + OtherFee4Name + "', @OTHERS_GL5 = '" + OtherFee5 + "', @OTHERS_GL5Name = '" + OtherFee5Name + "', @MemberWel_GL = '" + MemWelfare + "', @MemberWel_GLName = '" + MemWelfareName + "', @Service_GL = '" + ServiceChg + "', @Service_GLName = '" + ServiceChgName + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag ='MD',@BCLASS_GL='" + BCLASS_GL + "',@div1='" + div1 + "',@div2='" + div2 + "',@div3='" + div3 + "',@ID='" + Id + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int AuthoriseData(string BrCode, string Id, string Mid)
    {
        try
        {
            sql = "Exec SP_ShareParam @BRCD = '" + BrCode + "', @MID = '" + Mid + "', @Flag='AT',@ID='" + Id + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteData(string BrCode, string Id, string Mid)
    {
        try
        {
            sql = "Exec SP_ShareParam @BRCD = '" + BrCode + "', @MID = '" + Mid + "', @Flag='DL',@ID='" + Id + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}