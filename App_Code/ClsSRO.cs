using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsSRO
/// </summary>
public class ClsSRO
{
    DbConnection conn = new DbConnection();
    DataTable dt1 = new DataTable();
    ClsEncryptValue Ecry = new ClsEncryptValue();
    int res = 0;
    string sql = "", srno = "", custno = "", stage = "", usergrp = "";
    string TableName = "";
    string EntryMid, verifyMid, DeleteMid = "";
    string SCROLLNO, sResult;
    int Result, record;
    public ClsSRO()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Insert(string BRCD, string EFFECT_DT, string BOARD_RESO, string REG_SANCTION, string REMARK, string EMP_ID, string MID, string sroname)
    {
        try
        {
            sql = "exec AN_S001 @FL='AD',@BRCD='" + BRCD + "',@EFFECT_DT='" + conn.ConvertDate(EFFECT_DT) + "',@BOARD_RESO='" + BOARD_RESO + "',@REG_SANCTION='" + REG_SANCTION + "',@REMARK='" + REMARK + "',@EMP_ID='" + EMP_ID + "',@MID='" + MID + "',@SRONAME='" + sroname + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public DataTable GetCaseYear(string EFFECT_DT)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S002 @FL='GETCASEYEARCASENO',@R_O_DT='" + conn.ConvertDate(EFFECT_DT) + "'";
            dt = conn.GetDatatable(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable GetDefaulter(string caseno, string caseyear)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec SP_AVS701 @FLAG='GETDEFAULTER',@CASE_YEAR='" + caseyear + "',@CASENO='" + caseno + "'";
            dt = conn.GetDatatable(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public string CheckCASENOEXISTS(string caseno, string caseyear)
    {
        try
        {
            sql = "exec SP_AVS701 @FLAG='CHKCASENOEXISTS',@CASE_YEAR='" + caseyear + "',@CASENO='" + caseno + "'";
            stage = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return stage;
    }

    public string CHKMEMBERNOEXISTS(string caseyear, string MEMBERNO, string caseno)
    {
        try
        {
            sql = "exec SP_AVS701 @FLAG='CHKMEMBERNOEXISTS',@CASE_YEAR='" + caseyear + "',@CASENO='" + caseno + "',@MEMBERNO='" + MEMBERNO + "'";
            stage = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return stage;
    }

    public string CHKMID(string caseyear, string MEMBERNO, string caseno)
    {
        try
        {
            sql = "exec SP_AVS701 @FLAG='CHKMID',@CASE_YEAR='" + caseyear + "',@CASENO='" + caseno + "',@MEMBERNO='" + MEMBERNO + "'";
            stage = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return stage;
    }



    public DataTable GetCaseYearFile(string EFFECT_DT)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S003 @FL='GETCASEYEARCASENO',@EDATE='" + conn.ConvertDate(EFFECT_DT) + "'";
            dt = conn.GetDatatable(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public string GetCaseNo(string EFFECT_DT, string CaseYear)
    {
        try
        {
            sql = "exec AN_S002 @FL='GETCASEYEARCASENO',@R_O_DT='" + conn.ConvertDate(EFFECT_DT) + "',@CASE_YEAR='" + CaseYear + "'";
            srno = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return srno;
    }
    public int Modify(string BRCD, string SRNO, string EFFECT_DT, string BOARD_RESO, string REG_SANCTION, string REMARK, string EMP_ID, string VID, string sroname)
    {
        try
        {
            sql = "exec AN_S001 @FL='MD',@BRCD='" + BRCD + "',@SRNO='" + SRNO + "',@EFFECT_DT='" + conn.ConvertDate(EFFECT_DT) + "',@BOARD_RESO='" + BOARD_RESO + "',@REG_SANCTION='" + REG_SANCTION + "',@REMARK='" + REMARK + "',@EMP_ID='" + EMP_ID + "',@VID='" + VID + "',@SRONAME='" + sroname + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int Authorise(string BRCD, string SRNO, string VID)
    {
        try
        {
            sql = "exec AN_S001 @FL='AT',@BRCD='" + BRCD + "',@SRNO='" + SRNO + "',@VID='" + VID + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int Cancel(string BRCD, string SRNO, string VID)
    {
        try
        {
            sql = "exec AN_S001 @FL='CA',@BRCD='" + BRCD + "',@SRNO='" + SRNO + "',@VID='" + VID + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public string GetSrno()
    {
        try
        {
            sql = "select (isnull(max(srno),0))+1 srno from avs_2000 where STAGE<>'1004'";
            srno = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return srno;
    }
    public DataTable ViewDetails(string SRNO, string brcd)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S001 @FL='VW',@BRCD='" + brcd + "',@SRNO='" + SRNO + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public int InsertProposalSale(string BRCD, string CASE_YEAR, string CASENO, string EDATE, string DETAILSPROERTY, string PROPETYREGNO, string FLATNO, string RATEFORPERSQMETER, string Branchname,
       string WARDNO, string MOSTAMTPAIDNAME, string PUBLISHDATE, string LASTAUCTIONAMT, string AUCTIONDATE, string AUCTIONAMTNO, string AUCTIONAMT, string MostAUCTIONAMT,
       string DEFAULTERNAME, string ArrearnDate, string MID, string CID, string VID, string PCMAC, string Font)
    {
        try
        {
            sql = "exec AN_SALERECOVERYNOTICE  @FL='AD',@BRCD='" + BRCD + "',@CASE_YEAR='" + CASE_YEAR + "',@CASENO='" + CASENO + "',@EDATE='" + EDATE + "',@DETAILSPROERTY='" + DETAILSPROERTY + "',@PROPETYREGNO='" + PROPETYREGNO + "'," +
            "@FLATNO='" + FLATNO + "',@RATEFORPERSQMETER='" + RATEFORPERSQMETER + "',@Branchname='" + Branchname + "',@WARDNO='" + WARDNO + "',@MOSTAMTPAIDNAME='" + MOSTAMTPAIDNAME + "',@PUBLISHDATE='" + PUBLISHDATE + "',@LASTAUCTIONAMT='" + LASTAUCTIONAMT + "',@AUCTIONDATE='" + AUCTIONDATE + "'," +
            "@AUCTIONAMTNO='" + AUCTIONAMTNO + "',@AUCTIONAMT='" + AUCTIONAMT + "',@MostAUCTIONAMT='" + MostAUCTIONAMT + "',@DEFAULTERNAME='" + DEFAULTERNAME + "',@ArrearnDate='" + ArrearnDate + "',@MID='" + MID + "',@CID='" + CID + "',@VID='" + VID + "',@PCMAC='" + PCMAC + "',@font='" + Font + "' ";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    //----------------------sales Modify
    public int ModifyProposalSale(string BRCD, string CASE_YEAR, string CASENO, string SRO_NO, string EDATE, string DETAILSPROERTY, string PROPETYREGNO, string FLATNO, string RATEFORPERSQMETER, string Branchname,
       string WARDNO, string MOSTAMTPAIDNAME, string PUBLISHDATE, string LASTAUCTIONAMT, string AUCTIONDATE, string AUCTIONAMTNO, string AUCTIONAMT, string MostAUCTIONAMT, string DEFAULTERNAME, string ArrearnDate,
              string MID, string CID, string VID, string PCMAC, string Font)
    {
        try
        {
            sql = "exec AN_SALERECOVERYNOTICE  @FL='AD',@BRCD='" + BRCD + "',@CASE_YEAR='" + CASE_YEAR + "',@CASENO='" + CASENO + "', @SRO_NO='" + SRO_NO + "',@EDATE='" + EDATE + "',@DETAILSPROERTY='" + DETAILSPROERTY + "',@PROPETYREGNO='" + PROPETYREGNO + "'," +
            "@FLATNO='" + FLATNO + "',@RATEFORPERSQMETER='" + RATEFORPERSQMETER + "',@Branchname='" + Branchname + "',@WARDNO='" + WARDNO + "',@MOSTAMTPAIDNAME='" + MOSTAMTPAIDNAME + "',@PUBLISHDATE='" + PUBLISHDATE + "',@LASTAUCTIONAMT='" + LASTAUCTIONAMT + "',@AUCTIONDATE='" + AUCTIONDATE + "'," +
            "@AUCTIONAMTNO='" + AUCTIONAMTNO + "',@AUCTIONAMT='" + AUCTIONAMT + "',@MostAUCTIONAMT='" + MostAUCTIONAMT + "',@DEFAULTERNAME='" + DEFAULTERNAME + "',@ArrearnDate='" + ArrearnDate + "',@MID='" + MID + "',@CID='" + CID + "',@VID='" + VID + "',@PCMAC='" + PCMAC + "' ,@font='" + Font + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    //---------------------sales Delete
    public int CancelproposalSale(string BRCD, string CASENO, string CASEYEAR, string VID)
    {
        try
        {
            sql = "exec AN_SALERECOVERYNOTICE @FL='CA',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYEAR + "',@VID='" + VID + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    //-------------------------sale
    public DataTable ViewDetailSale(string BRCD, string CASENO, string CASE_YEAR, string ID)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_SALERECOVERYNOTICE @FL='VW',@BRCD='" + BRCD + "',@CASENO	='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@SRO_NO='" + ID + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    //------------------------sales
    public int BindGrdSales(GridView grd, string BRCD, string CASENO, string CASE_YEAR)
    {
        try
        {
            sql = "exec AN_SALERECOVERYNOTICE @FL='BINDGRID',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR	='" + CASE_YEAR + "'";
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    
    public int InsertDem(string BRCD, string CASENO, string CASEYAER, string MEMNO, string DEFNAME, string SRO_NO, string C_F_N_101, string C_F_D_101, string C_F_N_91, string C_F_DT_91, string NOTICE_ISS_DT, string TOT_RECV, string DIV_CITY, string COURT_NAME, string BSD_SRO, string AWARD_EXP, string TALATHI_OW, string COMP_OW, string REMARK1,
        string CaseSts, string MID, string R_O_DT, string PrincipleAmount, string Rate, string Fdate, string Tdate, string diffMonth, string totInt, string WARD, string PINCODE,
        string COSTPROCESS, string COSTAPPLICATION, string DESIGNATION, string COMM_NAME, string COM_MOBILE1, string COM_MOBILE2, string COM_ADDRESS, string COM_WARD,
        string COM_CITY, string COM_PINCODE, string DEFAULTVALUE, string MEMTYPE, string RCNOTYPE, string ORDERBY, string PROPERTYTYPE, string PROPERTYNO, string FloorNO,
        string EXECUTIONCHARG, string AMOUNT, string PAYMENTMODE, string CHEQUENO)//string S_O_DT, string ImmuvableDate, string MovableDate, 
    {
        try
        {
            sql = "exec AN_S002 @FL='AD',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYAER + "',@MEMBERNO='" + MEMNO + "',@DEFAULTERNAME='" + DEFNAME + "'," +
                "@SRO_NO='" + SRO_NO + "',@C_F_N_101='" + C_F_N_101 + "',@C_F_D_101='" + conn.ConvertDate(C_F_D_101) + "',@C_F_N_91='" + C_F_N_91 + "',@C_F_DT_91='" + conn.ConvertDate(C_F_DT_91) + "'," +
                "@NOTICE_ISS_DT='" + conn.ConvertDate(NOTICE_ISS_DT) + "',@TOT_RECV='" + TOT_RECV + "',@DIV_CITY='" + DIV_CITY + "',@COURT_NAME='" + COURT_NAME + "'," +
                "@BSD_SRO='" + BSD_SRO + "',@AWARD_EXP='" + AWARD_EXP + "',@TALATHI_OW='" + TALATHI_OW + "',@COMP_OW='" + COMP_OW + "',@REMARK1='" + REMARK1 + "',@CASESTS='" + CaseSts + "'," +
                "@MID='" + MID + "',@R_O_DT='" + conn.ConvertDate(R_O_DT) + "',@PRINCIPLE='" + (PrincipleAmount) + "',@RATE='" + (Rate) + "',@FROMDATE='" + conn.ConvertDate(Fdate) + "'," +
                "@TODATE='" + conn.ConvertDate(Tdate) + "',@MONTH='" + (diffMonth) + "',@TOTINT='" + (totInt) + "',@WARD='" + (WARD) + "',@PINCODE='" + (PINCODE) + "'," +
                "@COSTPROCESS='" + (COSTPROCESS) + "',@COSTAPPLICATION='" + (COSTAPPLICATION) + "',@DESIGNATION='" + DESIGNATION + "' ,@COMM_NAME='" + COMM_NAME + "'," +
                "@COM_MOBILE1='" + COM_MOBILE1 + "',@COM_MOBILE2='" + COM_MOBILE2 + "',@COM_ADDRESS='" + COM_ADDRESS + "',@COM_WARD='" + COM_WARD + "' ,@COM_CITY='" + COM_WARD + "' ," +
                "@COM_PINCODE='" + COM_WARD + "',@DEFAULTVALUE='" + DEFAULTVALUE + "',@MEMTYPE='" + MEMTYPE + "',@RCNOTYPE='" + RCNOTYPE + "',@ORDERBY='" + ORDERBY + "'," +
                "@PROPERTYTYPE='" + PROPERTYTYPE + "',@PROPERTYTYPENO='" + PROPERTYNO + "',@FloorNO='" + FloorNO + "',@EXECUTIONCHARG='" + EXECUTIONCHARG + "',@AMOUNT='" + AMOUNT + "',@PAYMENTMODE='" + PAYMENTMODE + "',@CHEQUENO='" + CHEQUENO + "'";//@S_O_DT='" + conn.ConvertDate(S_O_DT) + "',@IMMATTDATE='" + conn.ConvertDate(ImmuvableDate) + "',@MOVATTDATE='" + conn.ConvertDate(MovableDate) + "'
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int InsertDefaulterName(string ID, string BRCD, string SRO_NO, string NOTICE_ISS_DT, string STAGE, string MID, string CID, string VID, string SYSTEMDATE, string CASENO, string CASE_YEAR, string MEMBERNO, string DEFAULTERNAME, string DEFAULTPROPERTY, string CORRESPONDEADD, string CASESTATUS, string OCC_ADD, string OCC_DETAIL, string MOBILE2, string MOBILE1)
    {
        try
        {
            sql = "INSERT INTO AVS_2001_Defulter(iD,BRCD,SRO_NO,NOTICE_ISS_DT,STAGE,MID,CID,VID,SYSTEMDATE,CASENO,CASE_YEAR,MEMBERNO,DEFAULTERNAME,DEFAULTPROPERTY,CORRESPONDENCEADDRESS,CASESTATUS,OCC_ADD,OCC_DETAIL,MOBILE2,MOBILE1)VALUES('" + ID + "','" + BRCD + "','" + SRO_NO + "','" + conn.ConvertDate(NOTICE_ISS_DT) + "','" + STAGE + "','" + MID + "','" + CID + "','" + VID + "','" + conn.ConvertDate(SYSTEMDATE) + "','" + CASENO + "','" + CASE_YEAR + "','" + MEMBERNO + "','" + DEFAULTERNAME + "','" + DEFAULTPROPERTY + "','" + CORRESPONDEADD + "','" + CASESTATUS + "','" + OCC_ADD + "','" + OCC_DETAIL + "','" + MOBILE2 + "','" + MOBILE1 + "')";
            res = conn.sExecuteQuery(sql);

        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
        return res;
    }
    public int InsertDefaulterNameMarathi(string ID, string BRCD, string SRO_NO, string NOTICE_ISS_DT, string STAGE, string MID, string CID, string VID, string SYSTEMDATE, string CASENO, string CASE_YEAR, string MEMBERNO, string DEFAULTERNAME, string DEFAULTPROPERTY, string CORRESPONDEADD, string CASESTATUS, string OCC_ADD, string OCC_DETAIL, string MOBILE2, string MOBILE1)
    {
        try
        {
            sql = "INSERT INTO AVS_2001_DefulterMarathi(iD,BRCD,SRO_NO,NOTICE_ISS_DT,STAGE,MID,CID,VID,SYSTEMDATE,CASENO,CASE_YEAR,MEMBERNO,DEFAULTERNAME,DEFAULTPROPERTY,CORRESPONDENCEADDRESS,CASESTATUS,OCC_ADD,OCC_DETAIL,MOBILE2,MOBILE1)VALUES('" + ID + "','" + BRCD + "','" + SRO_NO + "','" + conn.ConvertDate(NOTICE_ISS_DT) + "','" + STAGE + "','" + MID + "','" + CID + "','" + VID + "','" + conn.ConvertDate(SYSTEMDATE) + "','" + CASENO + "','" + CASE_YEAR + "','" + MEMBERNO + "','" + DEFAULTERNAME + "','" + DEFAULTPROPERTY + "','" + CORRESPONDEADD + "','" + CASESTATUS + "','" + OCC_ADD + "','" + OCC_DETAIL + "','" + MOBILE2 + "','" + MOBILE1 + "')";
            res = conn.sExecuteQuery(sql);

        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
        return res;
    }

    //public int InsertDETUPSETNOTICE(string BRCD,string CASE_YEAR,string CASENO,string MEMBERNO, string PRDCD, string SRO_NO,string  EDATE,string DESIGNATION,string WARD,string ADDRESS,
    //      string SROORDERREFNO,string SROORDERDATE  ,string  AREAOFPROPERTY,string  ATTACHEDPROPERTYTITLE ,string  VALUATIONDATE,string VALUERNAME,string REGNO ,string REGDATE,
    //      string MARKETVALUE, string FAIRMARKETVALUE, string SUBREGVALUE, string CONCERNSUBREGDESIGATION, string SRNO, string YEAR, string DETAILSPROERTY, string DPREGNO, string PRICE,
    //      string RATEFORPERSQMETER, string MID, string CID, string VID, string PCMAC)//string S_O_DT, string ImmuvableDate, string MovableDate, 
    //{
    //    try
    //    {
    //        sql = "exec AN_SOO2_UPSETNOTICE  @FL='AD',@BRCD='" + BRCD + "',@CASE_YEAR='" + CASE_YEAR + "',@CASENO='" + CASENO + "',@MEMBERNO='" + MEMBERNO + "', @PRDCD='" + PRDCD + "',@SRO_NO='" + SRO_NO + "',@EDATE='" +conn.ConvertDate(EDATE) + "',@DESIGNATION='" + DESIGNATION + "', @WARD='" + WARD + "', @ADDRESS='" + ADDRESS + "'," +
    //      "@SROORDERREFNO='" + SROORDERREFNO + "',@SROORDERDATE='" + conn.ConvertDate(SROORDERDATE) + "'  , @AREAOFPROPERTY='" + AREAOFPROPERTY + "',@ATTACHEDPROPERTYTITLE='" + ATTACHEDPROPERTYTITLE + "' , @VALUATIONDATE='" + conn.ConvertDate(VALUATIONDATE) + "', @VALUERNAME='" + VALUERNAME + "',@REGNO ='" + REGNO + "',@REGDATE='" +conn.ConvertDate( REGDATE) + "'," +
    //     " @MARKETVALUE='" + MARKETVALUE + "',@FAIRMARKETVALUE='" + FAIRMARKETVALUE + "',@SUBREGVALUE='" + SUBREGVALUE + "',@CONCERNSUBREGDESIGATION='" + CONCERNSUBREGDESIGATION + "',@SRNO='" + SRNO + "',@YEAR='" + YEAR + "',@DETAILSPROERTY='" + DETAILSPROERTY + "',@DPREGNO='" + DPREGNO + "', @PRICE='" + PRICE + "'," +
    //     " @RATEFORPERSQMETER='" + RATEFORPERSQMETER + "',@STAGE='1001',@MID='" + MID + "',@CID='" + CID + "',@VID='" + VID + "',@PCMAC='" + PCMAC + "'";
    //        res = conn.sExecuteQuery(sql);
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionLogging.SendErrorToText(ex);
    //    }
    //    return res;
    //}
    public int InsertDETUPSETNOTICE(string BRCD, string CASE_YEAR, string CASENO, string MEMBERNO, string PRDCD, string SRO_NO, string EDATE, string DESIGNATION, string WARD, string ADDRESS,
          string SROORDERREFNO, string SROORDERDATE, string AREAOFPROPERTY, string ATTACHEDPROPERTYTITLE, string VALUATIONDATE, string VALUERNAME, string REGNO, string REGDATE,
          string MARKETVALUE, string FAIRMARKETVALUE, string SUBREGVALUE, string CONCERNSUBREGDESIGATION, string SRNO, string YEAR, string DETAILSPROERTY, string DPREGNO, string PRICE,
          string RATEFORPERSQMETER, string MID, string CID, string VID, string PCMAC, string BranchName, string RecoveryOfficeNo, string Rec_OfficerName, string RecOfficerCastNo, string RecoveryAdmissionType, string RecoveryAdmissionNO
, string RecoveryAdmissionDate, string CertificateDate98, string WardName, string WardAddress, string AwardAmount, string PrincipalAmount, string InterestAmount, string FromInterestDate, string TotalRecoveryAmount, string PropertyType, string HouseNo, string FlatNo, string Arrearsno
, string Arrearsname, string ArrearsAddress, string PostalAddress, string BusinessAddress, string OrderofPossessionDate, string OfficerType, string officerHouseno, string officerFlat, string officerDesignation, string officerAddress, string officerName, string Font)//string S_O_DT, string ImmuvableDate, string MovableDate, 
    {
        try
        {
            sql = "exec AN_SOO2_UPSETNOTICE  @FL='AD',@BRCD='" + BRCD + "',@CASE_YEAR='" + CASE_YEAR + "',@CASENO='" + CASENO + "',@MEMBERNO='" + MEMBERNO + "', @PRDCD='" + PRDCD + "',@SRO_NO='" + SRO_NO + "',@EDATE='" + conn.ConvertDate(EDATE) + "',@DESIGNATION='" + DESIGNATION + "', @WARD='" + WARD + "', @ADDRESS='" + ADDRESS + "'," +
          "@SROORDERREFNO='" + SROORDERREFNO + "',@SROORDERDATE='" + conn.ConvertDate(SROORDERDATE) + "'  , @AREAOFPROPERTY='" + AREAOFPROPERTY + "',@ATTACHEDPROPERTYTITLE='" + ATTACHEDPROPERTYTITLE + "' , @VALUATIONDATE='" + conn.ConvertDate(VALUATIONDATE) + "', @VALUERNAME='" + VALUERNAME + "',@REGNO ='" + REGNO + "',@REGDATE='" + conn.ConvertDate(REGDATE) + "'," +
         " @MARKETVALUE='" + MARKETVALUE + "',@FAIRMARKETVALUE='" + FAIRMARKETVALUE + "',@SUBREGVALUE='" + SUBREGVALUE + "',@CONCERNSUBREGDESIGATION='" + CONCERNSUBREGDESIGATION + "',@SRNO='" + SRNO + "',@YEAR='" + YEAR + "',@DETAILSPROERTY='" + DETAILSPROERTY + "',@DPREGNO='" + DPREGNO + "', @PRICE='" + PRICE + "'," +
         " @RATEFORPERSQMETER='" + RATEFORPERSQMETER + "',@STAGE='1001',@MID='" + MID + "',@CID='" + CID + "',@VID='" + VID + "',@PCMAC='" + PCMAC + "' , @BranchName ='" + BranchName + "',@RecoveryOfficerNo='" + RecoveryOfficeNo + "',@Rec_OfficerName ='" + Rec_OfficerName + "',@RecOfficerCastNo ='" + RecOfficerCastNo + "',@RecoveryAdmissionType ='" + RecoveryAdmissionType + "',@RecoveryAdmissionNO ='" + RecoveryAdmissionNO + "',@RecoveryAdmissionDate ='" + conn.ConvertDate(RecoveryAdmissionDate) + "'," +
         " @CertificateDate98 ='" + conn.ConvertDate(CertificateDate98) + "',@WardName='" + WardName + "',@WardAddress ='" + WardAddress + "',@AwardAmount ='" + AwardAmount + "',@PrincipalAmount ='" + PrincipalAmount + "',@InterestAmount='" + InterestAmount + "' ,@FromInterestDate ='" + conn.ConvertDate(FromInterestDate) + "',@TotalRecoveryAmount='" + TotalRecoveryAmount + "' ,@PropertyType ='" + PropertyType + "',@HouseNo ='" + HouseNo + "',@FlatNo ='" + FlatNo + "',@Arrearsno='" + Arrearsno + "' ,@Arrearsname ='" + Arrearsname + "',@ArrearsAddress ='" + ArrearsAddress + "',@PostalAddress='" + PostalAddress + "' ,@BusinessAddress='" + BusinessAddress + "' ,@OrderofPossessionDate ='" + conn.ConvertDate(OrderofPossessionDate) + "',@OfficerType ='" + OfficerType + "',@officerHouseno ='" + officerHouseno + "',@officerFlat ='" + officerFlat + "'," +
         " @officerDesignation ='" + officerDesignation + "',@officerAddress='" + officerAddress + "' ,@officerName='" + officerName + "',@Font='" + Font + "' ";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }


    public int Insertdataup(string BRCD, string CASE_YEAR, string CASENO,  string EDATE,  string SRNO, string YEAR, string DETAILSPROERTY, string DPREGNO, string PRICE,
         string RATEFORPERSQMETER, string MID, string CID, string VID, string PCMAC)//string S_O_DT, string ImmuvableDate, string MovableDate, 
    {
        try
        {
            sql = "exec AN_SOO2_UPSETNOTICE  @FL='UAD',@BRCD='" + BRCD + "',@CASE_YEAR='" + CASE_YEAR + "',@CASENO='" + CASENO + "',@EDATE='" + conn.ConvertDate(EDATE) + "',@SRNO='" + SRNO + "',@YEAR='" + YEAR + "',@DETAILSPROERTY='" + DETAILSPROERTY + "',@DPREGNO='" + DPREGNO + "', @PRICE='" + PRICE + "'," +
         " @RATEFORPERSQMETER='" + RATEFORPERSQMETER + "',@STAGE='1001',@MID='" + MID + "',@CID='" + CID + "',@VID='" + VID + "',@PCMAC='" + PCMAC + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }



    //public int ModifyDETUPSETNOTICE(string BRCD, string CASE_YEAR, string CASENO, string MEMBERNO, string PRDCD, string SRO_NO, string EDATE, string DESIGNATION, string WARD, string ADDRESS,
    //     string SROORDERREFNO, string SROORDERDATE, string AREAOFPROPERTY, string ATTACHEDPROPERTYTITLE, string VALUATIONDATE, string VALUERNAME, string REGNO, string REGDATE,
    //     string MARKETVALUE, string FAIRMARKETVALUE, string SUBREGVALUE, string CONCERNSUBREGDESIGATION, string SRNO, string YEAR, string DETAILSPROERTY, string DPREGNO, string PRICE,
    //     string RATEFORPERSQMETER, string MID, string CID, string VID, string PCMAC)//string S_O_DT, string ImmuvableDate, string MovableDate, 
    //{
    //    try
    //    {
    //        sql = "exec AN_SOO2_UPSETNOTICE  @FL='MD',@BRCD='" + BRCD + "',@CASE_YEAR='" + CASE_YEAR + "',@CASENO='" + CASENO + "',@MEMBERNO='" + MEMBERNO + "', @PRDCD='" + PRDCD + "',@SRO_NO='" + SRO_NO + "',@EDATE='" + conn.ConvertDate(EDATE) + "',@DESIGNATION='" + DESIGNATION + "', @WARD='" + WARD + "', @ADDRESS='" + ADDRESS + "'," +
    //      "@SROORDERREFNO='" + SROORDERREFNO + "',@SROORDERDATE='" + conn.ConvertDate(SROORDERDATE) + "'  , @AREAOFPROPERTY='" + AREAOFPROPERTY + "',@ATTACHEDPROPERTYTITLE='" + ATTACHEDPROPERTYTITLE + "' , @VALUATIONDATE='" + conn.ConvertDate(VALUATIONDATE) + "', @VALUERNAME='" + VALUERNAME + "',@REGNO ='" + REGNO + "',@REGDATE='" + conn.ConvertDate(REGDATE) + "'," +
    //     " @MARKETVALUE='" + MARKETVALUE + "',@FAIRMARKETVALUE='" + FAIRMARKETVALUE + "',@SUBREGVALUE='" + SUBREGVALUE + "',@CONCERNSUBREGDESIGATION='" + CONCERNSUBREGDESIGATION + "',@SRNO='" + SRNO + "',@YEAR='" + YEAR + "',@DETAILSPROERTY='" + DETAILSPROERTY + "',@DPREGNO='" + DPREGNO + "', @PRICE='" + PRICE + "'," +
    //     " @RATEFORPERSQMETER='" + RATEFORPERSQMETER + "',@STAGE='1001',@MID='" + MID + "',@CID='" + CID + "',@VID='" + VID + "',@PCMAC='" + PCMAC + "'";
    //        res = conn.sExecuteQuery(sql);
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionLogging.SendErrorToText(ex);
    //    }
    //    return res;
    //}

//    public int ModifyDETUPSETNOTICE(string BRCD, string CASE_YEAR, string CASENO, string MEMBERNO, string PRDCD, string SRO_NO, string EDATE, string DESIGNATION, string WARD, string ADDRESS,
//         string SROORDERREFNO, string SROORDERDATE, string AREAOFPROPERTY, string ATTACHEDPROPERTYTITLE, string VALUATIONDATE, string VALUERNAME, string REGNO, string REGDATE,
//         string MARKETVALUE, string FAIRMARKETVALUE, string SUBREGVALUE, string CONCERNSUBREGDESIGATION, string SRNO, string YEAR, string DETAILSPROERTY, string DPREGNO, string PRICE,
//         string RATEFORPERSQMETER, string MID, string CID, string VID, string PCMAC, string BranchName, string RecoveryOfficeNo, string Rec_OfficerName, string RecOfficerCastNo, string RecoveryAdmissionType, string RecoveryAdmissionNO
//, string RecoveryAdmissionDate, string CertificateDate98, string WardName, string WardAddress, string AwardAmount, string PrincipalAmount, string InterestAmount, string FromInterestDate, string TotalRecoveryAmount, string PropertyType, string HouseNo, string FlatNo, string Arrearsno
//, string Arrearsname, string ArrearsAddress, string PostalAddress, string BusinessAddress, string OrderofPossessionDate, string OfficerType, string officerHouseno, string officerFlat, string officerDesignation, string officerAddress, string officerName, string Font)//string S_O_DT, string ImmuvableDate, string MovableDate, 
//    {
//        try
//        {
//            sql = "exec AN_SOO2_UPSETNOTICE  @FL='MD',@BRCD='" + BRCD + "',@CASE_YEAR='" + CASE_YEAR + "',@CASENO='" + CASENO + "',@MEMBERNO='" + MEMBERNO + "', @PRDCD='" + PRDCD + "',@SRO_NO='" + SRO_NO + "',@EDATE='" + conn.ConvertDate(EDATE) + "',@DESIGNATION='" + DESIGNATION + "', @WARD='" + WARD + "', @ADDRESS='" + ADDRESS + "'," +
//          "@SROORDERREFNO='" + SROORDERREFNO + "',@SROORDERDATE='" + conn.ConvertDate(SROORDERDATE) + "'  , @AREAOFPROPERTY='" + AREAOFPROPERTY + "',@ATTACHEDPROPERTYTITLE='" + ATTACHEDPROPERTYTITLE + "' , @VALUATIONDATE='" + conn.ConvertDate(VALUATIONDATE) + "', @VALUERNAME='" + VALUERNAME + "',@REGNO ='" + REGNO + "',@REGDATE='" + conn.ConvertDate(REGDATE) + "'," +
//         " @MARKETVALUE='" + MARKETVALUE + "',@FAIRMARKETVALUE='" + FAIRMARKETVALUE + "',@SUBREGVALUE='" + SUBREGVALUE + "',@CONCERNSUBREGDESIGATION='" + CONCERNSUBREGDESIGATION + "',@SRNO='" + SRNO + "',@YEAR='" + YEAR + "',@DETAILSPROERTY='" + DETAILSPROERTY + "',@DPREGNO='" + DPREGNO + "', @PRICE='" + PRICE + "'," +
//         " @RATEFORPERSQMETER='" + RATEFORPERSQMETER + "',@STAGE='1001',@MID='" + MID + "',@CID='" + CID + "',@VID='" + VID + "',@PCMAC='" + PCMAC + "', @BranchName ='" + BranchName + "',@RecoveryOfficerNo='" + RecoveryOfficeNo + "',@Rec_OfficerName ='" + Rec_OfficerName + "',@RecOfficerCastNo ='" + RecOfficerCastNo + "',@RecoveryAdmissionType ='" + RecoveryAdmissionType + "',@RecoveryAdmissionNO ='" + RecoveryAdmissionNO + "',@RecoveryAdmissionDate ='" + conn.ConvertDate(RecoveryAdmissionDate) + "'," +
//         " @CertificateDate98 ='" + conn.ConvertDate(CertificateDate98) + "',@WardName='" + WardName + "',@WardAddress ='" + WardAddress + "',@AwardAmount ='" + AwardAmount + "',@PrincipalAmount ='" + PrincipalAmount + "',@InterestAmount='" + InterestAmount + "' ,@FromInterestDate ='" + conn.ConvertDate(FromInterestDate) + "',@TotalRecoveryAmount='" + TotalRecoveryAmount + "' ,@PropertyType ='" + PropertyType + "',@HouseNo ='" + HouseNo + "',@FlatNo ='" + FlatNo + "',@Arrearsno='" + Arrearsno + "' ,@Arrearsname ='" + Arrearsname + "',@ArrearsAddress ='" + ArrearsAddress + "',@PostalAddress='" + PostalAddress + "' ,@BusinessAddress='" + BusinessAddress + "' ,@OrderofPossessionDate ='" + conn.ConvertDate(OrderofPossessionDate) + "',@OfficerType ='" + OfficerType + "',@officerHouseno ='" + officerHouseno + "',@officerFlat ='" + officerFlat + "'," +
//         " @officerDesignation ='" + officerDesignation + "',@officerAddress='" + officerAddress + "' ,@officerName='" + officerName + "',@Font='" + Font + "' ";
//            res = conn.sExecuteQuery(sql);
//        }
//        catch (Exception ex)
//        {
//            ExceptionLogging.SendErrorToText(ex);
//        }
//        return res;
//    }
    public int ModifyDETUPSETNOTICE(string BRCD, string CASE_YEAR, string CASENO, string MEMBERNO, string PRDCD, string SRO_NO, string EDATE, string DESIGNATION, string WARD, string ADDRESS,
         string SROORDERREFNO, string SROORDERDATE, string AREAOFPROPERTY, string ATTACHEDPROPERTYTITLE, string VALUATIONDATE, string VALUERNAME, string REGNO, string REGDATE,
         string MARKETVALUE, string FAIRMARKETVALUE, string SUBREGVALUE, string CONCERNSUBREGDESIGATION, string SRNO, string YEAR, string DETAILSPROERTY, string DPREGNO, string PRICE,
         string RATEFORPERSQMETER, string MID, string CID, string VID, string PCMAC, string BranchName, string RecoveryOfficeNo, string Rec_OfficerName, string RecOfficerCastNo, string RecoveryAdmissionType, string RecoveryAdmissionNO
, string RecoveryAdmissionDate, string CertificateDate98, string WardName, string WardAddress, string AwardAmount, string PrincipalAmount, string InterestAmount, string FromInterestDate, string TotalRecoveryAmount, string PropertyType, string HouseNo, string FlatNo, string Arrearsno
, string Arrearsname, string ArrearsAddress, string PostalAddress, string BusinessAddress, string OrderofPossessionDate, string OfficerType, string officerHouseno, string officerFlat, string officerDesignation, string officerAddress, string officerName, string Font)//string S_O_DT, string ImmuvableDate, string MovableDate, 
    {
        try
        {
            sql = "exec AN_SOO2_UPSETNOTICE  @FL='MD',@BRCD='" + BRCD + "',@CASE_YEAR='" + CASE_YEAR + "',@CASENO='" + CASENO + "',@MEMBERNO='" + MEMBERNO + "', @PRDCD='" + PRDCD + "',@SRO_NO='" + SRO_NO + "',@EDATE='" + conn.ConvertDate(EDATE) + "',@DESIGNATION='" + DESIGNATION + "', @WARD='" + WARD + "', @ADDRESS='" + ADDRESS + "'," +
          "@SROORDERREFNO='" + SROORDERREFNO + "',@SROORDERDATE='" + conn.ConvertDate(SROORDERDATE) + "'  , @AREAOFPROPERTY='" + AREAOFPROPERTY + "',@ATTACHEDPROPERTYTITLE='" + ATTACHEDPROPERTYTITLE + "' , @VALUATIONDATE='" + conn.ConvertDate(VALUATIONDATE) + "', @VALUERNAME='" + VALUERNAME + "',@REGNO ='" + REGNO + "',@REGDATE='" + conn.ConvertDate(REGDATE) + "'," +
         " @MARKETVALUE='" + MARKETVALUE + "',@FAIRMARKETVALUE='" + FAIRMARKETVALUE + "',@SUBREGVALUE='" + SUBREGVALUE + "',@CONCERNSUBREGDESIGATION='" + CONCERNSUBREGDESIGATION + "',@SRNO='" + SRNO + "',@YEAR='" + YEAR + "',@DETAILSPROERTY='" + DETAILSPROERTY + "',@DPREGNO='" + DPREGNO + "', @PRICE='" + PRICE + "'," +
         " @RATEFORPERSQMETER='" + RATEFORPERSQMETER + "',@STAGE='1001',@MID='" + MID + "',@CID='" + CID + "',@VID='" + VID + "',@PCMAC='" + PCMAC + "', @BranchName ='" + BranchName + "',@RecoveryOfficerNo='" + RecoveryOfficeNo + "',@Rec_OfficerName ='" + Rec_OfficerName + "',@RecOfficerCastNo ='" + RecOfficerCastNo + "',@RecoveryAdmissionType ='" + RecoveryAdmissionType + "',@RecoveryAdmissionNO ='" + RecoveryAdmissionNO + "',@RecoveryAdmissionDate ='" + conn.ConvertDate(RecoveryAdmissionDate) + "'," +
         " @CertificateDate98 ='" + conn.ConvertDate(CertificateDate98) + "',@WardName='" + WardName + "',@WardAddress ='" + WardAddress + "',@AwardAmount ='" + AwardAmount + "',@PrincipalAmount ='" + PrincipalAmount + "',@InterestAmount='" + InterestAmount + "' ,@FromInterestDate ='" + conn.ConvertDate(FromInterestDate) + "',@TotalRecoveryAmount='" + TotalRecoveryAmount + "' ,@PropertyType ='" + PropertyType + "',@HouseNo ='" + HouseNo + "',@FlatNo ='" + FlatNo + "',@Arrearsno='" + Arrearsno + "' ,@Arrearsname ='" + Arrearsname + "',@ArrearsAddress ='" + ArrearsAddress + "',@PostalAddress='" + PostalAddress + "' ,@BusinessAddress='" + BusinessAddress + "' ,@OrderofPossessionDate ='" + conn.ConvertDate(OrderofPossessionDate) + "',@OfficerType ='" + OfficerType + "',@officerHouseno ='" + officerHouseno + "',@officerFlat ='" + officerFlat + "'," +
         " @officerDesignation ='" + officerDesignation + "',@officerAddress='" + officerAddress + "' ,@officerName='" + officerName + "',@Font='" + Font + "' ";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int ModifyDETUPSETNOTICEDET(string BRCD, string CASE_YEAR, string CASENO, string EDATE,  string SRNO, string YEAR, string DETAILSPROERTY, string DPREGNO, string PRICE,
         string RATEFORPERSQMETER, string MID, string CID, string VID, string PCMAC)//string S_O_DT, string ImmuvableDate, string MovableDate, 
    {
        try
        {
            sql = "exec AN_SOO2_UPSETNOTICE  @FL='UMD',@BRCD='" + BRCD + "',@CASE_YEAR='" + CASE_YEAR + "',@CASENO='" + CASENO + "',@EDATE='" + conn.ConvertDate(EDATE) + "',@SRNO='" + SRNO + "',@YEAR='" + YEAR + "',@DETAILSPROERTY='" + DETAILSPROERTY + "',@DPREGNO='" + DPREGNO + "', @PRICE='" + PRICE + "'," +
         " @RATEFORPERSQMETER='" + RATEFORPERSQMETER + "',@STAGE='1001',@MID='" + MID + "',@CID='" + CID + "',@VID='" + VID + "',@PCMAC='" + PCMAC + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int InsertDefaulterNameS( string BRCD,  string CASENO, string CASE_YEAR)
    {
        try
        {
            sql = "INSERT INTO AVS_2005_SOCIETY(DEFAULTERNAME)VALUES((select DEFAULTERNAME from AVS_2001_Defulter where caseno='" + CASENO + "'and CASE_YEAR='" + CASE_YEAR + "' ) )";//and iD='"+ID+"'
            res = conn.sExecuteQuery(sql);

        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
        return res;
    }

    public int InsertCommitDetail(string BRCD, string CASENO, string CASEYAER, string MEMNO, string SRO_NO, string NOTICE_ISS_DT,
      string CaseSts, string MID, string DESIGNATION, string COMM_NAME, string COM_MOBILE1, string COM_MOBILE2, string COM_ADDRESS, string PROPERTYTYPE, string PROPERTYTYPENO, string FloorNO)//string S_O_DT, string ImmuvableDate, string MovableDate, 
    {
        try
        {
            sql = "exec AN_S002 @FL='ADC',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYAER + "',@MEMBERNO='" + MEMNO + "'," +
                "@SRO_NO='" + SRO_NO + "',@NOTICE_ISS_DT='" + conn.ConvertDate(NOTICE_ISS_DT) + "'," +
                "@MID='" + MID + "',@DESIGNATION='" + DESIGNATION + "' ,@COMM_NAME='" + COMM_NAME + "'," +
                "@COM_MOBILE1='" + COM_MOBILE1 + "',@COM_MOBILE2='" + COM_MOBILE2 + "',@COM_ADDRESS='" + COM_ADDRESS + "',@PROPERTYTYPE1='" + PROPERTYTYPE + "',@PROPERTYTYPENO1='" + PROPERTYTYPENO + "',@FloorNO1='" + FloorNO + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int ModifyCommitDetail(string BRCD, string CASENO, string CASEYAER, string MEMNO, string SRO_NO, string NOTICE_ISS_DT,
      string CaseSts, string MID, string DESIGNATION, string COMM_NAME, string COM_MOBILE1, string COM_MOBILE2, string COM_ADDRESS, string PROPERTYTYPE, string PROPERTYTYPENO, string FloorNO)//string S_O_DT, string ImmuvableDate, string MovableDate, 
    {
        try
        {
            sql = "exec AN_S002 @FL='MDC',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYAER + "',@MEMBERNO='" + MEMNO + "'," +
                "@SRO_NO='" + SRO_NO + "',@NOTICE_ISS_DT='" + conn.ConvertDate(NOTICE_ISS_DT) + "'," +
                "@MID='" + MID + "',@DESIGNATION='" + DESIGNATION + "' ,@COMM_NAME='" + COMM_NAME + "'," +
                "@COM_MOBILE1='" + COM_MOBILE1 + "',@COM_MOBILE2='" + COM_MOBILE2 + "',@COM_ADDRESS='" + COM_ADDRESS + "',@PROPERTYTYPE1='" + PROPERTYTYPE + "',@PROPERTYTYPENO1='" + PROPERTYTYPENO + "',@FloorNO1='" + FloorNO + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int InsertCommitDetailMARATHI(string BRCD, string CASENO, string CASEYAER, string MEMNO, string SRO_NO, string NOTICE_ISS_DT,
      string CaseSts, string MID, string DESIGNATION, string COMM_NAME, string COM_MOBILE1, string COM_MOBILE2, string COM_ADDRESS, string PROPERTYTYPE, string PROPERTYTYPENO, string FloorNO)//string S_O_DT, string ImmuvableDate, string MovableDate, 
    {
        try
        {
            sql = "exec AN_S002 @FL='ADCM',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYAER + "',@MEMBERNO='" + MEMNO + "'," +
                "@SRO_NO='" + SRO_NO + "',@NOTICE_ISS_DT='" + conn.ConvertDate(NOTICE_ISS_DT) + "'," +
                "@MID='" + MID + "',@DESIGNATION='" + DESIGNATION + "' ,@COMM_NAME='" + COMM_NAME + "'," +
                "@COM_MOBILE1='" + COM_MOBILE1 + "',@COM_MOBILE2='" + COM_MOBILE2 + "',@COM_ADDRESS='" + COM_ADDRESS + "',@PROPERTYTYPE1='" + PROPERTYTYPE + "',@PROPERTYTYPENO1='" + PROPERTYTYPENO + "',@FloorNO1='" + FloorNO + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int ModifyCommitDetailMARATHI(string BRCD, string CASENO, string CASEYAER, string MEMNO, string SRO_NO, string NOTICE_ISS_DT,
      string CaseSts, string MID, string DESIGNATION, string COMM_NAME, string COM_MOBILE1, string COM_MOBILE2, string COM_ADDRESS, string PROPERTYTYPE, string PROPERTYTYPENO, string FloorNO)//string S_O_DT, string ImmuvableDate, string MovableDate, 
    {
        try
        {
            sql = "exec AN_S002 @FL='MDCM',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYAER + "',@MEMBERNO='" + MEMNO + "'," +
                "@SRO_NO='" + SRO_NO + "',@NOTICE_ISS_DT='" + conn.ConvertDate(NOTICE_ISS_DT) + "'," +
                "@MID='" + MID + "',@DESIGNATION='" + DESIGNATION + "' ,@COMM_NAME='" + COMM_NAME + "'," +
                "@COM_MOBILE1='" + COM_MOBILE1 + "',@COM_MOBILE2='" + COM_MOBILE2 + "',@COM_ADDRESS='" + COM_ADDRESS + "',@PROPERTYTYPE1='" + PROPERTYTYPE + "',@PROPERTYTYPENO1='" + PROPERTYTYPENO + "',@FloorNO1='" + FloorNO + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public string GetCaseStatus(string srno)// added by Dhanya Shetty //27/02/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1047' and srno='" + srno + "'";
        srno = conn.sExecuteScalar(sql);
        return srno;
    }

    public void GetCaseStatus(DropDownList ddl)
    {
        try
        {
            sql = "select DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1047' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void GetActionStatus(DropDownList ddl)
    {
        try
        {
            sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2573' ORDER BY SRNO";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public string GetActionStatus(string srno)// added by Dhanya Shetty //27/02/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2573'  and SRNO='" + srno + "'";
        srno = conn.sExecuteScalar(sql);
        return srno;
    }
    public string GetMemberID(string MemberID)
    {
        sql = "exec AN_S002 @FL='GETMEMBERNAME',@MEMBERNO='" + MemberID + "'";// AND MID='" + MID + "' 
        MemberID = conn.sExecuteScalar(sql);
        return MemberID;
    }
    public string GetMemberID2(string MemberID ,string FL)
    {
        sql = "exec AN_S002 @FL='" + FL + "',@MEMBERNO='" + MemberID + "'";// AND MID='" + MID + "' 
        MemberID = conn.sExecuteScalar(sql);
        return MemberID;
    }

    public int ModifyDem(string BRCD, string CASENO, string CASEYAER, string MEMNO, string DEFNAME, string SRO_NO, string C_F_N_101, string C_F_D_101, string C_F_N_91, string C_F_DT_91, string NOTICE_ISS_DT, string TOT_RECV, string DIV_CITY, string COURT_NAME, string BSD_SRO, string AWARD_EXP, string TALATHI_OW, string COMP_OW, string REMARK1,
        string CaseSts, string MID, string R_O_DT, string PrincipleAmount, string Rate, string Fdate, string Tdate, string diffMonth, string totInt, string WARD, string PINCODE,
        string COSTPROCESS, string COSTAPPLICATION, string DESIGNATION, string COMM_NAME, string COM_MOBILE1, string COM_MOBILE2, string COM_ADDRESS, string COM_WARD,
        string COM_CITY, string COM_PINCODE, string DEFAULTVALUE, string MEMTYPE, string RCNOTYPE, string ORDERBY, string PROPERTYTYPE, string PROPERTYNO, string FloorNO,
        string EXECUTIONCHARG, string AMOUNT, string PAYMENTMODE, string CHEQUENO)// string S_O_DT, string ImmuvableDate, string MovableDate, 
    {
        try
        {
            sql = "exec AN_S002 @FL='MD',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYAER + "',@MEMBERNO='" + MEMNO + "',@DEFAULTERNAME='" + DEFNAME + "'," +
               "@SRO_NO='" + SRO_NO + "',@C_F_N_101='" + C_F_N_101 + "',@C_F_D_101='" + conn.ConvertDate(C_F_D_101) + "',@C_F_N_91='" + C_F_N_91 + "',@C_F_DT_91='" + conn.ConvertDate(C_F_DT_91) + "'," +
               "@NOTICE_ISS_DT='" + conn.ConvertDate(NOTICE_ISS_DT) + "',@TOT_RECV='" + TOT_RECV + "',@DIV_CITY='" + DIV_CITY + "',@COURT_NAME='" + COURT_NAME + "'," +
               "@BSD_SRO='" + BSD_SRO + "',@AWARD_EXP='" + AWARD_EXP + "',@TALATHI_OW='" + TALATHI_OW + "',@COMP_OW='" + COMP_OW + "',@REMARK1='" + REMARK1 + "',@CASESTS='" + CaseSts + "'," +
               "@MID='" + MID + "',@R_O_DT='" + conn.ConvertDate(R_O_DT) + "',@PRINCIPLE='" + (PrincipleAmount) + "',@RATE='" + (Rate) + "',@FROMDATE='" + conn.ConvertDate(Fdate) + "'," +
               "@TODATE='" + conn.ConvertDate(Tdate) + "',@MONTH='" + (diffMonth) + "',@TOTINT='" + (totInt) + "',@WARD='" + (WARD) + "',@PINCODE='" + (PINCODE) + "'," +
               "@COSTPROCESS='" + (COSTPROCESS) + "',@COSTAPPLICATION='" + (COSTAPPLICATION) + "',@DESIGNATION='" + DESIGNATION + "' ,@COMM_NAME='" + COMM_NAME + "'," +
               "@COM_MOBILE1='" + COM_MOBILE1 + "',@COM_MOBILE2='" + COM_MOBILE2 + "',@COM_ADDRESS='" + COM_ADDRESS + "',@COM_WARD='" + COM_WARD + "' ,@COM_CITY='" + COM_WARD + "' ," +
               "@COM_PINCODE='" + COM_WARD + "',@DEFAULTVALUE='" + DEFAULTVALUE + "',@MEMTYPE='" + MEMTYPE + "',@RCNOTYPE='" + RCNOTYPE + "',@ORDERBY='" + ORDERBY + "'," +
               "@PROPERTYTYPE='" + PROPERTYTYPE + "',@PROPERTYTYPENO='" + PROPERTYNO + "',@FloorNO='" + FloorNO + "',@EXECUTIONCHARG='" + EXECUTIONCHARG + "',@AMOUNT='" + AMOUNT + "',@PAYMENTMODE='" + PAYMENTMODE + "',@CHEQUENO='" + CHEQUENO + "'";//@S_O_DT='" + conn.ConvertDate(S_O_DT) + "',@IMMATTDATE='" + conn.ConvertDate(ImmuvableDate) + "',@MOVATTDATE='" + conn.ConvertDate(MovableDate) + "'

            // sql = "exec AN_S002 @FL='MD',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYAER + "',@SRO_NO='" + SRO_NO + "'," +
            //     "@MEMBERNO='" + MEMNO + "',@DEFAULTERNAME='" + DEFNAME + "', @C_F_N_101='" + C_F_N_101 + "',@C_F_D_101='" + conn.ConvertDate(C_F_D_101) + "',"+
            //     "@C_F_N_91='" + C_F_N_91 + "',@C_F_DT_91='" + conn.ConvertDate(C_F_DT_91) + "',@NOTICE_ISS_DT='" + conn.ConvertDate(NOTICE_ISS_DT) + "',"+
            //     "@TOT_RECV='" + TOT_RECV + "',@DIV_CITY='" + DIV_CITY + "',@COURT_NAME='" + COURT_NAME + "',@BSD_SRO='" + BSD_SRO + "',@AWARD_EXP='" + AWARD_EXP + "',"+
            //     "@TALATHI_OW='" + TALATHI_OW + "',@COMP_OW='" + COMP_OW + "',@REMARK1='" + REMARK1 + "',@CASESTS='" + CaseSts + "',@VID='" + VID + "',"+
            //     "@R_O_DT='" + conn.ConvertDate(R_O_DT) + "',@PRINCIPLE='" + (PrincipleAmount) + "',@RATE='" + (Rate) + "',@FROMDATE='" + conn.ConvertDate(Fdate) + "',"+
            //     "@TODATE='" + conn.ConvertDate(Tdate) + "',@MONTH='" + (diffMonth) + "',@TOTINT='" + (totInt) + "',@WARD='" + (WARD) + "',@PINCODE='" + (PINCODE) + "',"+
            //     "@COSTPROCESS='" + (COSTPROCESS) + "',@COSTAPPLICATION='" + (COSTAPPLICATION) + "',@DEFAULTVALUE='" + DEFAULTVALUE + "',@MEMTYPE='" + MEMTYPE + "',"+
            // "@RCNOTYPE='" + RCNOTYPE + "',@ORDERBY='" + ORDERBY + "',@PROPERTYTYPE='" + PROPERTYTYPE + "',@PROPERTYTYPENO='" + PROPERTYNO + "',@FloorNO='" + FloorNO + "',"+
            //" @EXECUTIONCHARG='" + EXECUTIONCHARG + "',@AMOUNT='" + AMOUNT + "',@PAYMENTMODE='" + PAYMENTMODE + "',@CHEQUENO='" + CHEQUENO + "'";//@S_O_DT='" + conn.ConvertDate(S_O_DT) + "',@IMMATTDATE='" + conn.ConvertDate(ImmuvableDate) + "',@MOVATTDATE='" + conn.ConvertDate(MovableDate) + "'
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int ModifyDefulter(string BRCD, string CASENO, string CASEYEAR, string SRO_NO, string STAGE, string MEMNO, string CaseSts, string VID, string DEFNAME, string NOTICE_ISS_DT, string ID, string DEFAULTPROPERTY, string CORRESPONDENCEADDRESS, string OCC_DETAIL, string OCC_ADD, string MOBILE1, string MOBILE2)
    {
        try
        {
            sql = "UPDATE AVS_2001_Defulter Set  BRCD='" + BRCD + "',STAGE='" + STAGE + "',SRO_NO='" + SRO_NO + "',MEMBERNO='" + MEMNO + "',DEFAULTERNAME='" + DEFNAME + "',NOTICE_ISS_DT='" + conn.ConvertDate(NOTICE_ISS_DT) + "',CASESTATUS='" + CaseSts + "',MID='" + VID + "',DEFAULTPROPERTY='" + DEFAULTPROPERTY + "',CORRESPONDENCEADDRESS='" + CORRESPONDENCEADDRESS + "',OCC_DETAIL='" + OCC_DETAIL + "',OCC_ADD='" + OCC_ADD + "',MOBILE1='" + MOBILE1 + "',MOBILE2='" + MOBILE2 + "' WHERE CASENO='" + CASENO + "' AND  CASE_YEAR='" + CASEYEAR + "' AND ID='" + ID + "' ";


            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int ModifyDefulterMARATHI(string BRCD, string CASENO, string CASEYEAR, string SRO_NO, string STAGE, string MEMNO, string CaseSts, string VID, string DEFNAME, string NOTICE_ISS_DT, string ID, string DEFAULTPROPERTY, string CORRESPONDENCEADDRESS, string OCC_DETAIL, string OCC_ADD, string MOBILE1, string MOBILE2)
    {
        try
        {
            sql = "UPDATE AVS_2001_DefulterMarathi Set  BRCD='" + BRCD + "',STAGE='" + STAGE + "',SRO_NO='" + SRO_NO + "',MEMBERNO='" + MEMNO + "',DEFAULTERNAME='" + DEFNAME + "',NOTICE_ISS_DT='" + conn.ConvertDate(NOTICE_ISS_DT) + "',CASESTATUS='" + CaseSts + "',MID='" + VID + "',DEFAULTPROPERTY='" + DEFAULTPROPERTY + "',CORRESPONDENCEADDRESS='" + CORRESPONDENCEADDRESS + "',OCC_DETAIL='" + OCC_DETAIL + "',OCC_ADD='" + OCC_ADD + "',MOBILE1='" + MOBILE1 + "',MOBILE2='" + MOBILE2 + "' WHERE CASENO='" + CASENO + "' AND  CASE_YEAR='" + CASEYEAR + "' AND ID='" + ID + "' ";


            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int DeleteDefaulter(string BRCD ,string CASENO, string CASEYEAR,string ID)
    {
        try
        {
            sql = " UPDATE AVS_2001_Defulter SET STAGE='1004' WHERE BRCD='" + BRCD + "' and CASENO='" + CASENO + "'AND CASE_YEAR='" + CASEYEAR + "' and id='" + ID + "' ";


            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int DeleteDefaulterMarathi(string BRCD, string CASENO, string CASEYEAR, string ID)
    {
        try
        {
            sql = " UPDATE AVS_2001_DefulterMarathi SET STAGE='1004' WHERE BRCD='" + BRCD + "' and CASENO='" + CASENO + "'AND CASE_YEAR='" + CASEYEAR + "' and id='" + ID + "' ";


            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public string GetBANGLCDName(string brcd, string Subgl)
    {
        try
        {
            sql = "Select GLNAME From glmast Where BrCd = '" + brcd + "' And SUBGLCODE='" + Subgl + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public string GetPRCDName(string brcd, string Subgl)
    {
        try
        {
            sql = "Select GLNAME From glmast Where BrCd = '" + brcd + "' And SUBGLCODE='" + Subgl + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }


    public string GetBankName(string BRCD)
    {
        try
        {
            sql = " SELECT DESCR FROM RBIBank WHERE BANKRBICD='" + BRCD + "' ";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
    public string GetBranchName(string BRCD, string BRANCHCD)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(100),BRANCHCD))+'-'FROM RBIBank WHERE BANKRBICD='" + BRCD + "' ";
            BRANCHCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRANCHCD;
    }





    public int AuthoriseDefaulter(string CASENO, string CASEYEAR)
    {
        try
        {
            sql = " UPDATE AVS_2001_Defulter SET STAGE='1003' WHERE CASENO='" + CASENO + "'AND CASE_YEAR='" + CASEYEAR + "' ";


            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int ModifyMember(string BRCD, string MEMNO, string ADDRESS)
    {
        try
        {
            sql = " UPDATE Addmast  SET STAGE='1003',ADDRESS='" + ADDRESS + "'  WHERE BRCD='" + BRCD + "'AND CustNo='" + MEMNO + "' ";


            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }


    public int AuthoriseDem(string BRCD, string CASENO, string CASEYEAR, string SRO_NO, string VID, string CaseSts)
    {
        try
        {
            sql = "exec AN_S002 @FL='AT',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYEAR + "',@SRO_NO='" + SRO_NO + "',@VID='" + VID + "',@CASESTS='" + CaseSts + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int CancelDem(string BRCD, string CASENO, string CASEYEAR, string SRO_NO, string VID)
    {
        try
        {
            sql = "exec AN_S002 @FL='CA',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYEAR + "',@SRO_NO='" + SRO_NO + "',@VID='" + VID + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int CancelUPSETNOTICE(string BRCD, string CASENO, string CASEYEAR, string SRO_NO, string VID)
    {
        try
        {
            sql = "exec AN_SOO2_UPSETNOTICE @FL='CA',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYEAR + "',@SRO_NO='" + SRO_NO + "',@VID='" + VID + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int CancelUPSETNOTICEdet(string BRCD, string CASENO, string CASEYEAR, string id)
    {
        try
        {
            sql = "exec AN_SOO2_UPSETNOTICE @FL='UCA',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYEAR + "',@id='" + id + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public DataTable ViewDetailsDem(string BRCD, string CASENO, string CASE_YEAR)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S002 @FL='VW',@BRCD='" + BRCD + "',@CASENO	='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable ViewDetailUPSETP(string BRCD, string CASENO, string CASE_YEAR,string ID )
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_SOO2_UPSETNOTICE @FL='VW',@BRCD='" + BRCD + "',@CASENO	='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@SRO_NO='" + ID + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable ViewDetailUPSETPDet(string BRCD, string CASENO, string CASE_YEAR, string ID)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_SOO2_UPSETNOTICE @FL='UVW',@BRCD='" + BRCD + "',@CASENO	='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@SRO_NO='" + ID + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable ViewDetailsCOMMI(string BRCD, string CASENO, string CASE_YEAR ,string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S002 @FL='VWC',@BRCD='" + BRCD + "',@CASENO	='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@ID='" + id + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable ViewDetailsCOMMIMARATHI(string BRCD, string CASENO, string CASE_YEAR, string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S002 @FL='VWCM',@BRCD='" + BRCD + "',@CASENO	='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@ID='" + id + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable ViewDetailsBank(string CASENO, string CASE_YEAR)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S002 @FL='BANKNOTICE_VIEW',@CASENO ='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable ViewDetailsDefaulter(string BRCD, string CASENO, string CASE_YEAR ,string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Select * from AVS_2001_Defulter where BRCD ='" + BRCD + "'and CASENO	='" + CASENO + "'and CASE_YEAR='" + CASE_YEAR + "'and id='"+id+"' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable ViewDetailsDefaulterMarathi(string BRCD, string CASENO, string CASE_YEAR, string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Select * from AVS_2001_DefulterMarathi where BRCD ='" + BRCD + "'and CASENO	='" + CASENO + "'and CASE_YEAR='" + CASE_YEAR + "'and id='" + id + "' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable deletecomminte(string BRCD, string CASENO, string CASE_YEAR, string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "update AVS_2001_Committee set  stage='1004'  where BRCD ='" + BRCD + "'and CASENO	='" + CASENO + "'and CASE_YEAR='" + CASE_YEAR + "'and id='" + id + "' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable deletecommintemarathi(string BRCD, string CASENO, string CASE_YEAR, string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "update AVS_2001_CommitteeMarathi set  stage='1004'  where BRCD ='" + BRCD + "'and CASENO	='" + CASENO + "'and CASE_YEAR='" + CASE_YEAR + "'and id='" + id + "' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable ViewDetailsDefaulterS(string BRCD, string CASENO, string CASE_YEAR)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Select * from AVS_2001_Defulter where BRCD ='" + BRCD + "'and CASENO	='" + CASENO + "'and CASE_YEAR='" + CASE_YEAR + "' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable ViewDetailSCOREC(string BRCD, string CASENO, string CASE_YEAR,string ID)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S003PAY @FL='VW',@BRCD='" + BRCD + "',@CASENO	='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@SOCIeTYID='" + ID + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable ViewDetailAUCTIONNOTCE(string BRCD, string CASENO, string CASE_YEAR, string ID)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_PUBLICNOTICE @FL='VW',@BRCD='" + BRCD + "',@CASENO	='" + CASENO + "',@CASEYEAR='" + CASE_YEAR + "',@id='" + ID + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable ViewDetailSCASESTATUS(string BRCD, string CASENO, string CASE_YEAR,string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S008CASE @FL='VW',@BRCD='" + BRCD + "',@CASENO	='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@SOCIeTYID='" + id + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable ViewDetailSACTIONSTATUS(string BRCD, string CASENO, string CASE_YEAR,string id )
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S009ACTION @FL='VW',@BRCD='" + BRCD + "',@CASENO	='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@SOCIeTYID='" + id + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }


    public DataTable ViewDetailSTATEMENTACC(string BRCD, string CASENO, string CASE_YEAR)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S002_demo @FL='VWACCST',@BRCD='" + BRCD + "',@CASENO	='"+CASENO + "',@CASE_YEAR='" + CASE_YEAR + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable ViewDetailSTATEMENTACCAll(string BRCD, string CASENO, string CASE_YEAR,string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S002_demo @FL='VWACCSTALL',@BRCD='" + BRCD + "',@CASENO	='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@ID='" + id + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable DownloadSocDetail(string BRCD)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S003PAY @FL='VWC',@BRCD='" + BRCD + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable DownloadACTION(string BRCD, string CASENO, string CASE_YEAR, string ActionStatus1, string NOTICESTAGE)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S002_demo @FL='VWCAction',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "', @CASE_YEAR='" + CASE_YEAR + "' , @ActionStatus1='" + ActionStatus1 + "' ,@NOTICESTAGE='" + NOTICESTAGE + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable Downloadcase1(string BRCD, string CASENO, string CASE_YEAR, string casestaus1)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S002_demo @FL='VWCcase',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "', @CASE_YEAR='" + CASE_YEAR + "' , @CaseStatus1='" + casestaus1 + "' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable DownloadCase(string BRCD)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S008CASE @FL='VWC',@BRCD='" + BRCD + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public string CHKGETCASENO(string BRCD, string CASENO, string CASE_YEAR)
    {
        string CASENO1 = "";
        try
        {
            sql = "select caseno from AVS_2001 where BRCD ='" + BRCD + "'and CASENO	='" + CASENO + "'and CASE_YEAR='" + CASE_YEAR + "' ";
            CASENO1 = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return CASENO1;
    }
    public string GetSROName(string SRO_NO)
    {
        string sroname = "";
        try
        {
            sql = "select SRONAME from avs_2000 where srno='" + SRO_NO + "'";
            sroname = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sroname;
    }
    public int InsertFile(string ENTRYDATE, string BRCD, string CASENO, string CASEY, string SRNO, string FILE_STATUS, string F_DATE, string F_REEAMRKS, string NEXT_F_DT, string STATUS, string MID, string MEMBERNO)
    {
        try
        {
            sql = "exec AN_S003 @FL='AD',@EDATE='" + conn.ConvertDate(ENTRYDATE) + "',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEY + "',@SRONO='" + SRNO + "',@FILESTATUS='" + FILE_STATUS + "',@FILEDATE='" + conn.ConvertDate(F_DATE) + "',@FILEREMARK='" + F_REEAMRKS + "',@NXTFLDATE='" + conn.ConvertDate(NEXT_F_DT) + "',@STATUS='" + STATUS + "',@MID='" + MID + "',@MEMBERNO ='" + MEMBERNO + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }



    public int ModifyFile(string BRCD, string CASENO, string CASE_YEAR, string SRNO, string FILE_STATUS, string F_DATE, string F_REEAMRKS, string NEXT_F_DT, string STATUS, string VID, string MEMBERNO)
    {
        try
        {
            sql = "exec AN_S003 @FL='MD',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@SRONO='" + SRNO + "',@FILESTATUS='" + FILE_STATUS + "',@FILEDATE='" + conn.ConvertDate(F_DATE) + "',@FILEREMARK='" + F_REEAMRKS + "',@NXTFLDATE='" + conn.ConvertDate(NEXT_F_DT) + "',@STATUS='" + STATUS + "',@VID='" + VID + "',@MEMBERNO ='" + MEMBERNO + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int AuthoriseFile(string BRCD, string CASENO, string CASE_YEAR, string VID)
    {
        try
        {
            sql = "exec AN_S003 @FL='AT',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@VID='" + VID + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int CancelFile(string BRCD, string CASENO, string CASE_YEAR, string VID)
    {
        try
        {
            sql = "exec AN_S003 @FL='CA',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@VID='" + VID + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public DataTable ViewDetailsFile(string BRCD, string CASENO, string CASE_YEAR)//string SRNO
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S003 @FL='VW',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "'";//@MID='" + MID + "'
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public void BindGrd(GridView grd, string brcd, string CASENO, string CASE_YEAR)
    {
        try
        {
            sql = "select ID,convert(varchar(10),ENTRYDATE,103)ENTRYDATE,BRCD,CASE_YEAR,CASENO,SRNO,FILE_STATUS,convert(varchar(10),F_DATE,103)F_DATE,F_REEAMRKS,convert(varchar(10),NEXT_F_DT,103)NEXT_F_DT from avs_2002 where brcd='" + brcd + "' and CASENO='" + CASENO + "' and CASE_YEAR	='" + CASE_YEAR + "'";
            conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindMainGrd(GridView grd, string brcd, string EDATE)
    {
        try
        {
            sql = "SELECT S.ID,convert(varchar(10),S.ENTRYDATE,103)ENTRYDATE,S.BRCD,S.CASE_YEAR,S.CASENO,S.SRNO,S.FILE_STATUS,convert(varchar(10),S.F_DATE,103)F_DATE,S.F_REEAMRKS,convert(varchar(10),S.NEXT_F_DT,103)NEXT_F_DT from AVS_2002 S  where S.brcd='" + brcd + "' and S.NEXT_F_DT='" + conn.ConvertDate(EDATE) + "'";
            //            sql = "select S.ID,convert(varchar(10),S.ENTRYDATE,103)ENTRYDATE,S.BRCD,S.PRCDCD,S.ACCNO,S.SRNO,S.FILE_STATUS,convert(varchar(10),S.F_DATE,103)F_DATE,S.F_REEAMRKS,convert(varchar(10),S.NEXT_F_DT,103)NEXT_F_DT,M.CUSTNAME ACCNAME from AVS_2002 S INNER JOIN MASTER M ON M.BRCD=S.BRCD INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND AC.BRCD=M.BRCD AND AC.ACCNO=S.ACCNO AND AC.SUBGLCODE=S.PRCDCD where S.brcd='" + brcd + "' and S.NEXT_F_DT='" + conn.ConvertDate(EDATE) + "'";
            conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public DataTable ViewMainGrid(string BRCD, string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select S.ID,convert(varchar(10),S.ENTRYDATE,103)ENTRYDATE,S.BRCD,S.CASE_YEAR,S.CASENO,S.SRNO,S.FILE_STATUS,convert(varchar(10),S.F_DATE,103)F_DATE,S.F_REEAMRKS,convert(varchar(10),S.NEXT_F_DT,103)NEXT_F_DT ,MEMBERNO from AVS_2002 S  where S.brcd='" + BRCD + "' and S.id='" + id + "'";
            // sql = "select S.ID,convert(varchar(10),S.ENTRYDATE,103)ENTRYDATE,S.BRCD,S.PRCDCD,S.ACCNO,S.SRNO,S.FILE_STATUS,convert(varchar(10),S.F_DATE,103)F_DATE,S.F_REEAMRKS,convert(varchar(10),S.NEXT_F_DT,103)NEXT_F_DT,M.CUSTNAME ACCNAME from AVS_2002 S INNER JOIN MASTER M ON M.BRCD=S.BRCD INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND AC.BRCD=M.BRCD AND AC.ACCNO=S.ACCNO AND AC.SUBGLCODE=S.PRCDCD  where S.brcd='" + BRCD + "' and S.id='" + id + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable GarDetails(string brcd, string accno, string subgl, string edate)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Select GL.GLname ,AM.CustNo, M.CustName, Mu.CustName As Gurantor1,Mu1.CustName As Gurantor2 ,Mu2.CustName As Gurantor3 ,D.MOBILE1 AS GurMob1, " +
                    "D1.MOBILE1 as GurMob2,D2.MOBILE1 as GurMob3 from Avs_Acc AM Inner join Master M On AM.Brcd = M.Brcd And AM.Custno = M.CustNo Inner join Glmast GL On Gl.Brcd = Am.Brcd And Am.Subglcode = GL.Subglcode " +
                    "Left join Surity Gu On AM.Brcd = GU.Brcd And AM.Subglcode = GU.LoanGlcode And Gu.LoanAccno = AM.Accno And GU.SURITYNO='1' " +
                    "Left join Surity Gu1 On AM.Brcd = GU1.Brcd And AM.Subglcode = GU1.LoanGlcode And Gu1.LoanAccno = AM.Accno And GU1.SURITYNO='2' " +
                    "Left join Surity Gu2 On AM.Brcd = GU2.Brcd And AM.Subglcode = GU2.LoanGlcode And Gu2.LoanAccno = AM.Accno And GU2.SURITYNO='3' " +
                    "Left join Master MU On Gu.SURITY_CUSTNO = MU.CustNo " +
                    "Left join Master MU1 On Gu1.SURITY_CUSTNO = MU1.CustNo " +
                    "Left join Master MU2 On Gu2.SURITY_CUSTNO = MU2.CustNo " +
                    "left join AVS_CONTACTD D ON  Gu.Brcd = D.Brcd And Gu.SURITY_CUSTNO = D.CustNo " +
                    "left join AVS_CONTACTD D1 ON  Gu1.Brcd = D1.Brcd And Gu1.SURITY_CUSTNO = D1.CustNo " +
                    "left join AVS_CONTACTD D2 ON  Gu2.Brcd = D2.Brcd And Gu2.SURITY_CUSTNO = D2.CustNo " +
                    "where am.BRCD='" + brcd + "' and am.SUBGLCODE='" + subgl + "' and am.ACCNO='" + accno + "' " +
                    "Group by GL.GLname ,AM.CustNo,M.CustName, Mu.CustName,Mu1.CustName,Mu2.CustName,D.MOBILE1,D1.MOBILE1,D2.MOBILE1";

            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable GetContact(string brcd, string accno, string subgl)
    {
        try
        {
            sql = "select custno from avs_acc where brcd='" + brcd + "' and accno='" + accno + "' and subglcode='" + subgl + "'";
            custno = conn.sExecuteScalar(sql);
            sql = "select MOBILE1,(case when tel_off=null OR tel_off='0' then TEL_RES else TEL_OFF end)tel from AVS_CONTACTD where brcd='" + brcd + "' and Custno='" + custno + "'";
            dt1 = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt1;
    }
    public DataTable GetCustNoNameGL1(string SubGlCode, string AccNo, string BrCode, string fl)
    {
        try
        {//ankita 22/11/2017 brcd removed
            if (fl == "AD")
                sql = "Select AC.CUSTNO CUSTNO, AC.ACC_TYPE, AC.OPR_TYPE, M.CUSTNAME From Master M Inner Join Avs_Acc AC With(NoLock) ON AC.CUSTNO = M.CUSTNO Where AC.BRCD = '" + BrCode + "' And AC.SUBGLCODE = '" + SubGlCode + "' And AC.ACCNO = '" + AccNo + "' and AC.ACC_STATUS=1";
            else
                sql = "Select AC.CUSTNO CUSTNO, AC.ACC_TYPE, AC.OPR_TYPE, M.CUSTNAME From Master M Inner Join Avs_Acc AC With(NoLock) ON AC.CUSTNO = M.CUSTNO Where AC.BRCD = '" + BrCode + "' And AC.SUBGLCODE = '" + SubGlCode + "' And AC.ACCNO = '" + AccNo + "'";

            dt1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt1;
    }
    public string GETSTAGE(string BRCD, string CASENO, string CASEY)
    {
        try
        {
            sql = "select stage from avs_2001 where BRCD='" + BRCD + "' and CASENO='" + CASENO + "' and CASE_YEAR='" + CASEY + "'";
            stage = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return stage;
    }

    public string GETCommite(string BRCD, string CASENO, string CASEY)
    {
        try
        {
            sql = "select caseno from AVS_2001_Committee where BRCD='" + BRCD + "' and CASENO='" + CASENO + "' and CASE_YEAR='" + CASEY + "'";
            stage = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return stage;
    }
    public string GETSTAGEM(string CASENO, string CASEY)
    {
        try
        {
            sql = "select stage from AVS_CASE_STATUS where CASENO='" + CASENO + "' and CASE_YEAR='" + CASEY + "'";
            stage = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return stage;
    }
    public string GETSTAGE1(string MEMNo, string CASENO, string CASEY)
    {
        try
        {
            sql = "exec SP_AVS701 @FLAG='CHKSTAGE',@MEMBERNO='" + MEMNo + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEY + "'";
            stage = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return stage;
    }
    public string ChkUser(string brcd, string uid, string uname)
    {
        try
        {
            sql = "select usergroup from USERMASTER where USERGROUP=1 and logincode='" + uid + "' and USERNAME='" + uname + "'";
            usergrp = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return usergrp;
    }
    public int BindGrdDem(GridView grd, string BRCD, string CASENO, string CASE_YEAR)
    {
        try
        {
            sql = "exec AN_S002 @FL='VW',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR	='" + CASE_YEAR + "'";
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int BindGrdCommitee(GridView grd, string BRCD, string CASENO, string CASE_YEAR)
    {
        try
        {
            sql = "exec AN_S002 @FL='VWCD',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR	='" + CASE_YEAR + "'";
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int BindGrdCommiteeMARATHI(GridView grd, string BRCD, string CASENO, string CASE_YEAR)
    {
        try
        {
            sql = "exec AN_S002 @FL='VWCDM',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR	='" + CASE_YEAR + "'";
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int BindGrdUPSETPZ(GridView grd, string BRCD, string CASENO, string CASE_YEAR)
    {
        try
        {
            sql = "exec AN_SOO2_UPSETNOTICE @FL='VWCD',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR	='" + CASE_YEAR + "'";
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int BindGrdUPSETPZdata(GridView grd, string BRCD, string CASENO, string CASE_YEAR)
    {
        try
        {
            sql = "exec AN_SOO2_UPSETNOTICE @FL='UVWCD',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR	='" + CASE_YEAR + "'";
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int BindGrdDef(GridView grd, string BRCD, string CASENO, string CASE_YEAR)
    {
        try
        {
            sql = "exec AN_S002 @FL='VWD',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR	='" + CASE_YEAR + "'";
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int BindGrdDemMain(GridView grd, string BRCD)
    {
        try
        {
            sql = "exec AN_S002 @FL='BINDGRID' ";//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int BindGrdSCOIETY(GridView grd, string BRCD)
    {
        try
        {
            sql = "exec AN_S003PAY @FL='BINDGRIDVIEW' ";//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int BindGrdSTATEMENT(GridView grd, string BRCD,string casey,string caseno)
    {
        try
        {
            sql = "exec AN_S002_demo @FL='BINDACCST' ,@CASENO='" + caseno + "' ,@CASE_YEAR='" + casey + "'";//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int BindGrdSTATEMENTAll(GridView grd, string BRCD, string casey, string caseno)
    {
        try
        {
            sql = "exec AN_S002_demo @FL='BINDACCST1' ,@CASENO='" + caseno + "' ,@CASE_YEAR='" + casey + "'";//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int BindGrdCASE(GridView grd, string BRCD)
    {
        try
        {
            sql = "exec AN_S008CASE @FL='BINDGRIDVIEW' ";//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int BindGrdACTION(GridView grd, string BRCD)
    {
        try
        {
            sql = "exec AN_S009ACTION @FL='BINDGRIDVIEW' ";//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int BindPUBLICAUCNOT(GridView grd, string BRCD)
    {
        try
        {
            sql = "exec AN_PUBLICNOTICE @FL='BINDGRID' ";//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }


    public int BindGrdMovement(GridView grd, string BRCD)
    {
        try
        {
            sql = "exec AN_S005 @FL='BINDGRID' ,@BRCD='"+BRCD+"'" ;//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int BindGrdCOMMITEE(GridView grd, string BRCD)
    {
        try
        {
            sql = "exec AN_S002 @FL='BINDGRIDCOMMIT' ";//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int BindGrdDemScociety(GridView grd, string BRCD)
    {
        try
        {
            sql = "exec AN_S003PAY @FL='BINDGRID' ";//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }


    public int Getinfotable(GridView Gview, string caseyear, string sbrcd, string EDT, string caseno)
    {
        try
        {
            string[] TD = EDT.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "Select ConVert(VarChar(10),isnull(a.SETNO,'0'))+'_'+ConVert(VarChar(10),isnull(A.Amount,'0'))+'_'+ConVert(VarChar(10)," +
                  "isnull(A.SUBGLCODE,'0'))+'_'+ConVert(VarChar(10),isnull(a.ACCNO,'0')) Dens, A.SETNO SETNO, A.SUBGLCODE AT,format(A.ENTRYDATE ,'dd/MM/yyyy')ENTRYDATE," +
 "A.ACCNO ACNO,ACC.BRCD, ACC.CASENO,ACC.CASE_YEAR, A.INSTRUMENTNO,A.INSTRUMENTDATE From " + TableName + " A " +
  "  Left Join UserMaster UM ON UM.PERMISSIONNO=A.MID Left Join AVS_2001 ACC ON ACC.CASENO=A.CustNo AND ACC.BRCD = A.BRCD and ACC.CASE_YEAR=A.CustName" +
  " AND A.SUBGLCODE=A.SUBGLCODE Where A.BRCD='" + sbrcd + "' AND A.STAGE = '1003' and ACC.CASE_YEAR='" + caseyear + "' and Acc.CASENO='" + caseno + "'  " +

   " AND A.TrxType <> '1' AND A.ACTIVITY='4'" +
   "  Order By A.SETNO,A.SCROLLNO  ";//AND A.ENTRYDATE = '" + conn.ConvertDate(EDT) + "'

            res = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return res;
    }
    public int Getinfotable1(GridView Gview, string caseyear, string sbrcd,  string caseno)
    {
        try
        {


            sql = "Select ConVert(VarChar(10),isnull(a.SETNO,'0'))+'_'+ConVert(VarChar(10)," +//isnull(A.Amount,'0'))+'_'+ConVert(VarChar(10),
                  "isnull(A.SUBGLCODE,'0'))+'_'+ConVert(VarChar(10),isnull(a.ACCNO,'0')) Dens, A.SETNO SETNO, A.SUBGLCODE AT,format(A.ENTRYDATE ,'dd/MM/yyyy')ENTRYDATE," +
 "A.ACCNO ACNO,ACC.BRCD, ACC.CASENO,ACC.CASE_YEAR, A.INSTRUMENTNO,A.INSTRUMENTDATE,A.SCROLLNO From AllVCr A " +
  "  Left Join UserMaster UM ON UM.PERMISSIONNO=A.MID Left Join AVS_2001 ACC ON ACC.CASENO=A.CustNo AND ACC.BRCD = A.BRCD and ACC.CASE_YEAR=A.CustName" +
  " AND A.SUBGLCODE=A.SUBGLCODE Where A.BRCD='" + sbrcd + "' AND A.STAGE = '1003' and A.CustName='" + caseyear + "' and A.CustNo='" + caseno + "'  " +

   " AND A.TrxType <> '2' AND A.ACTIVITY='4'" +
   "  Order By A.SETNO,A.SCROLLNO  ";//AND A.ENTRYDATE = '" + conn.ConvertDate(EDT) + "'

            res = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return res;
    }
    public int BindGrdDefName(GridView grd,string brcd, string CASENO, string CASE_YEAR)
    {
        try
        {
            sql = "Select iD,BRCD,CASENO,CASE_YEAR,DEFAULTERNAME,DEFAULTPROPERTY from AVS_2001_Defulter where BRCD='" + brcd + "' and CASENO='" + CASENO + "'and CASE_YEAR='" + CASE_YEAR + "' and stage<>'1004' ";//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int BindGrdDefNameMARATHI(GridView grd, string brcd, string CASENO, string CASE_YEAR)
    {
        try
        {
            sql = "Select iD,BRCD,CASENO,CASE_YEAR,DEFAULTERNAME,DEFAULTPROPERTY from AVS_2001_DefulterMarathi where BRCD='" + brcd + "' and CASENO='" + CASENO + "'and CASE_YEAR='" + CASE_YEAR + "' and stage<>'1004' ";//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public string GETACCSTAGE(string BRCD, string SUBGL, string ACCNO)
    {
        try
        {
            sql = "select stage from AVS_ACC where BRCD='" + BRCD + "' and SUBGLCODE='" + SUBGL + "' and ACCNO='" + ACCNO + "'";
            stage = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return stage;
    }
    public string GetmidVid(string brcd, string accno, string subgl)
    {
        string midvid = "";
        try
        {
            sql = "SELECT (U.USERNAME+'_'+isnull(u1.username,'')) fdsh FROM USERMASTER U INNER JOIN avs_2001 A ON U.PERMISSIONNO=A.MID " +
                    "left join USERMASTER U1 ON U1.PERMISSIONNO=A.vid WHERE A.PRDCD='" + subgl + "' AND A.STAGE<>1004 AND A.BRCD='" + brcd + "' AND A.ACCNO='" + accno + "'";
            midvid = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return midvid;
    }


    public int InsertDefaulter(string APPLICATIONDATE, string BRCD, string CASENO, string CASEY, string MEMBERNO, string DEFAULTER_NAME, string DEFAULTER_PROPERTY, string CORSPOND_ADD, string CITY, string PINCODE, string WARD, string OCC_DETAIL, string OCC_ADD, string MOBILE1, string MOBILE2, string MID)
    {
        try
        {
            sql = "exec SP_AVS701 @FLAG='AD',@APPLICATIONDATE='" + conn.ConvertDate(APPLICATIONDATE) + "',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@MEMBERNO='" + MEMBERNO + "',@CASE_YEAR='" + CASEY + "',@DEFAULTER_NAME='" + DEFAULTER_NAME + "',@DEFAULTER_PROPERTY='" + DEFAULTER_PROPERTY + "',@CORSPOND_ADD='" + CORSPOND_ADD + "',@CITY='" + CITY + "',@WARD='" + WARD + "',@PINCODE='" + PINCODE + "',@OCC_DETAIL='" + OCC_DETAIL + "',@MOBILE1='" + MOBILE1 + "',@MOBILE2='" + MOBILE2 + "',@MID='" + MID + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int AuthorizeDefaulter(string CASENO, string CASEY, string MEMBERNO)
    {
        try
        {
            sql = "exec SP_AVS701 @FLAG='ATH',@CASENO='" + CASENO + "',@MEMBERNO='" + MEMBERNO + "',@CASE_YEAR='" + CASEY + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int DeletDefaulter(string CASENO, string CASEY, string MEMBERNO)
    {
        try
        {
            sql = "exec SP_AVS701 @FLAG='DLT',@CASENO='" + CASENO + "',@MEMBERNO='" + MEMBERNO + "',@CASE_YEAR='" + CASEY + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int BindGrdDefaulter(GridView grd)
    {
        try
        {
            sql = "exec SP_AVS701 @FLAG='GRID'";
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public DataTable ViewDetailsDefaulter(string CASENO, string CASE_YEAR)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec SP_AVS701 @FLAG='VW',@CASENO	='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public string DefaulterNo(string CASENO, string CASE_YEAR)//Dhanya Shetty-For Log Detail
    {
        try
        {
            sql = "select isnull(max(id),0)+1 from AVS_2001_DEFULTER where caseno='" + CASENO + "'and case_year='" + CASE_YEAR + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string MarathiDefaulterNo(string CASENO, string CASE_YEAR)//Dhanya Shetty-For Log Detail
    {
        try
        {
            sql = "select isnull(max(id),0)+1 from AVS_2001_DefulterMarathi where caseno='" + CASENO + "'and case_year='" + CASE_YEAR + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable GetNoticeUpdate()
    {
        DataTable dt = new DataTable();
        try
        {

            dt = conn.GetDatatable(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
   
    public string GetCasevisti(string CaseNo, string CaseYear)
    {
        try
        {
            sql = "SELECT D.VISITDATE  FROM AVS_NOTICE_STATUS D WHERE D.CASENO='" + CaseNo + "' AND D.CASE_YEAR='" + CaseYear + "' and D.NOTICEID=4";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }
    public string GetCasesymbolic(string CaseNo, string CaseYear)
    {
        try
        {
            sql = "SELECT D.SYMOLICDATE FROM AVS_NOTICE_STATUS D WHERE D.CASENO='" + CaseNo + "' AND D.CASE_YEAR='" + CaseYear + "' and D.NOTICEID=5";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public string GetCase(string CaseNo, string CaseYear)
    {
        try
        {
            sql = "SELECT CASENO FROM AVS_2001 D WHERE D.CASENO='" + CaseNo + "' AND D.CASE_YEAR='" + CaseYear + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }
  
    public string GetCaseStatusStage(string CaseYear, string caseno)
    {

        try
        {
            sql = "exec SP_CASE_STATUS @FL='GETCASESTATUS' , @CASENO='" + caseno + "' , @CASE_YEAR='" + CaseYear + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }


    public int BindNotice(GridView grd, string caseno, string CaseYear)
    {
        try
        {
            sql = "SELECT N.ID,N.NAME,S.TOTALNOTICEGENERATED,S.LASTNOTICEDATE  FROM AVS_NOTICE N LEFT JOIN AVS_NOTICE_STATUS S ON S.NOTICEID=N.ID AND  S.CASENO='" + caseno + "' AND S.CASE_YEAR='" + CaseYear + "'  ";//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int InsertCaseStatus(string CASENO = null, string CASE_YEAR = null, string CASE_STAGE = null, string DATE = null, string CASE_STATUS = null, string REMARK = null, string PAY_AMT = null, string PAY_DATE = null, string BANK_ATTCH_DATE = null, string IMM_ATTCH_DATE = null, string MOV_ATTCH_DATE = null, string MID = null, string BRCD = null, string STAGE = null)
    {
        try
        {
            sql = "exec SP_CASE_STATUS @FL='AD',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@DATE= '" + conn.ConvertDate(DATE) + "',@CASE_STATUS ='" + CASE_STATUS + "',@REMARK='" + REMARK + "' ,@PAY_AMT='" + PAY_AMT + "' ,@PAY_DATE='" + conn.ConvertDate(PAY_DATE) + "' ,@BANK_ATTCH_DATE='" + conn.ConvertDate(BANK_ATTCH_DATE) + "' ,@IMM_ATTCH_DATE='" + conn.ConvertDate(IMM_ATTCH_DATE) + "' ,@MOV_ATTCH_DATE='" + conn.ConvertDate(MOV_ATTCH_DATE) + "' ,@STAGE='" + STAGE + "',@MID='" + MID + "' ";

            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int ModifyCaseStatus(string CASENO, string CASE_YEAR, string CASE_STAGE, string DATE, string CASE_STATUS, string REMARK, string PAY_AMT, string PAY_DATE, string BANK_ATTCH_DATE, string IMM_ATTCH_DATE, string MOV_ATTCH_DATE, string MID, string BRCD, string STAGE)
    {
        try
        {
            sql = "exec SP_CASE_STATUS @FL='MD',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@DATE= '" + conn.ConvertDate(DATE) + "',@CASE_STATUS ='" + CASE_STATUS + "',@REMARK='" + REMARK + "' ,@PAY_AMT='" + PAY_AMT + "' ,@PAY_DATE='" + conn.ConvertDate(PAY_DATE) + "' ,@BANK_ATTCH_DATE='" + conn.ConvertDate(BANK_ATTCH_DATE) + "' ,@IMM_ATTCH_DATE='" + conn.ConvertDate(IMM_ATTCH_DATE) + "' ,@MOV_ATTCH_DATE='" + conn.ConvertDate(MOV_ATTCH_DATE) + "' ,@STAGE='" + STAGE + "',@MID='" + MID + "' ,@BRCD='" + BRCD + "'";

            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int AuthoriseCaseStatus(string BRCD, string CASENO, string CASE_YEAR, string MID)
    {
        try
        {
            sql = "exec SP_CASE_STATUS @FL='AT',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@MID='" + MID + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int CancleCaseStatus(string BRCD, string CASENO, string CASE_YEAR, string MID)
    {
        try
        {
            sql = "exec SP_CASE_STATUS @FL='AT',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@MID='" + MID + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public DataTable ViewDetailsCaseStatus(string CASENO, string CASE_YEAR)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec SP_CASE_STATUS @FL='VW',@CASENO	='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public int BindGrdCaseStatus(GridView grd, string CASENO, string CASE_YEAR)
    {
        try
        {
            sql = "exec SP_CASE_STATUS @FL='VW',@CASENO='" + CASENO + "',@CASE_YEAR	='" + CASE_YEAR + "'";
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int BankAccAttchment(string CASENO, string CASEYEAR, string ToBankDesignation, string BAnkAddress, string BankAcNo)
    {
        try
        {
            sql = "exec AN_S002 @FL='BANKNOTICE_INSERT',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYEAR + "',@ToBankDesignation='" + ToBankDesignation + "',@BAnkAddress='" + BAnkAddress + "',@BankAcNo='" + BankAcNo + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int InserttSociety(string BRCD, string PRDCD, string SRO_NO, string CASE_YEAR, string CASENO, string MEMBERNO, string DEFAULTERNAME, string accno,
        string PRCDCD, string rate, string principle, string ENTRYDATE, string fromdate, string todate, string month, string AMOUNT, string PAYMENTMODE,
        string CHEQUENO, string STAGE, string MID)
    {
        try
        {
            sql = "exec AN_S003PAY @FL='AD',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@MEMBERNO='" + MEMBERNO + "',@DEFAULTERNAME='" + DEFAULTERNAME + "'," +
                "@accno='" + accno + "',@PRCDCD='" + PRCDCD + "',@rate='" + rate + "',@principle='" + principle + "',@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "'," +
                "@fromdate='" + conn.ConvertDate(fromdate) + "',@todate='" + conn.ConvertDate(todate) + "',@month='" + month + "',@AMOUNT='" + AMOUNT + "',@PAYMENTMODE='" + PAYMENTMODE + "',@CHEQUENO='" + CHEQUENO + "',@STAGE='" + STAGE + "',@MID='" + MID + "'";

            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }



    public int INSERPUBLICNOTIC(string ISSUERecovery, string CaseNo, string CASEYEAR, string Date, string InspectionDATE, string TendersDATE, string SealDateDATE, string MID, string CID, string VID, string PCMAC, string brcd, string InspectionFromTime, string InspectiontoTime, string TendersFromTiom, string TendersTOTiom, string SealDateFromTimeE, string SealDateToTimeE,string Reserve_price,string Money_Deposit)
    {
        try
        {
            sql = "exec AN_PUBLICNOTICE @FL='AD',@ISSUERecovery='" + ISSUERecovery + "' ,@CaseNo='" + CaseNo + "',@CASEYEAR='" + CASEYEAR + "',@Date='" + conn.ConvertDate(Date) + "',@InspectionDATE='" + conn.ConvertDate(InspectionDATE) + "',@TendersDATE='" + conn.ConvertDate(TendersDATE) + "',@SealDateDATE='" + conn.ConvertDate(SealDateDATE) + "',@CID='" + CID + "',"+
                " @InspectionFromTime='" + InspectionFromTime + "'	,@InspectiontoTime='" + InspectiontoTime + "'	,@TendersFromTiom='" + TendersFromTiom + "'	,@TendersTOTiom	='" + TendersTOTiom + "',@SealDateFromTimeE='" + SealDateFromTimeE + "',@SealDateToTimeE='" + SealDateToTimeE + "',	@VID='" + VID + "',@PCMAC='" + PCMAC + "',@brcd='" + brcd + "' ,@MID='" + MID + "',@Reserve_price='" + Reserve_price + "',@Money_Deposit='" + Money_Deposit + "'";

            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int MODIFYPUBLICNOTIC(string ISSUERecovery, string CaseNo, string CASEYEAR, string Date, string InspectionDATE, string TendersDATE, string SealDateDATE, string MID, string CID, string VID, string PCMAC, string brcd, string InspectionFromTime, string InspectiontoTime, string TendersFromTiom, string TendersTOTiom, string SealDateFromTimeE, string SealDateToTimeE, string Reserve_price, string Money_Deposit)
    {
        try
        {
            sql = "exec AN_PUBLICNOTICE @FL='MD',@ISSUERecovery='" + ISSUERecovery + "' ,@CaseNo='" + CaseNo + "',@CASEYEAR='" + CASEYEAR + "',@Date='" + conn.ConvertDate(Date) + "',@InspectionDATE='" + conn.ConvertDate(InspectionDATE) + "',@TendersDATE='" + conn.ConvertDate(TendersDATE) + "',@SealDateDATE='" + conn.ConvertDate(SealDateDATE) + "',@CID='" + CID + "'," +
                " @InspectionFromTime='" + InspectionFromTime + "'	,@InspectiontoTime='" + InspectiontoTime + "'	,@TendersFromTiom='" + TendersFromTiom + "'	,@TendersTOTiom	='" + TendersTOTiom + "',@SealDateFromTimeE='" + SealDateFromTimeE + "',@SealDateToTimeE='" + SealDateToTimeE + "',	@VID='" + VID + "',@PCMAC='" + PCMAC + "',@brcd='" + brcd + "' ,@MID='" + MID + "',@Reserve_price='" + Reserve_price + "',@Money_Deposit='" + Money_Deposit + "'";

            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int DELETEPUBLICNOTIC(string ISSUERecovery, string CaseNo, string CASEYEAR, string Date, string InspectionDATE, string TendersDATE, string SealDateDATE, string MID, string CID, string VID, string PCMAC, string brcd)
    {
        try
        {
            sql = "exec AN_PUBLICNOTICE @FL='DL',@ISSUERecovery='" + ISSUERecovery + "' ,@CaseNo='" + CaseNo + "',@CASEYEAR='" + CASEYEAR + "',@Date='" + conn.ConvertDate(Date) + "',@InspectionDATE='" + conn.ConvertDate(InspectionDATE) + "',@TendersDATE='" + conn.ConvertDate(TendersDATE) + "',@SealDateDATE='" + conn.ConvertDate(SealDateDATE) + "',@CID='" + CID + "',@VID='" + VID + "',@PCMAC='" + PCMAC + "',@brcd='" + brcd + "' ,@MID='" + MID + "'";

            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }


    public int SocietyRec(string BRCD, string PRDCD, string SRO_NO, string CASE_YEAR, string CASENO, string MEMBERNO,string SOCIETYNAME, string DEFAULTERNAME, string accno,
      string PRCDCD, string ENTRYDATE, string RECAMOUNT, string PAID_DEF_AMOUNT, string STAGE, string MID, string REMARK, string CASESTATUSNO, string ACTIONSTATUSNO, string Balance)
    {
        try
        {
            sql = "exec AN_S003PAY @FL='ADC',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@SRO_NO='" + SRO_NO + "',@MEMBERNO='" + MEMBERNO + "',@SOCIETYNAME='" + SOCIETYNAME + "',@DEFAULTERNAME='" + DEFAULTERNAME + "'," +
                "@accno='" + accno + "',@PRCDCD='" + PRCDCD + "',@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "',@STAGE='" + STAGE + "',@CASESTATUSNO='" + CASESTATUSNO + "',@ACTIONSTATUSNO='" + ACTIONSTATUSNO + "',@REMARK='" + REMARK + "',@Balance='" + Balance + "'," +
                "@AMOUNT='" + RECAMOUNT + "',@PAID_DEF_AMOUNT='" + PAID_DEF_AMOUNT + "',@MID='" + MID + "'";

            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }


    public int ADDCASESTATUS(string BRCD, string PRDCD, string SRO_NO, string CASE_YEAR, string CASENO, string MEMBERNO, string SOCIETYNAME, string DEFAULTERNAME, string accno,
      string PRCDCD, string ENTRYDATE, string RECAMOUNT, string PAID_DEF_AMOUNT, string STAGE, string MID, string REMARK, string CASESTATUSNO, string ACTIONSTATUSNO, string Balance)
    {
        try
        {
            sql = "exec AN_S008CASE @FL='ADC',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@SRO_NO='" + SRO_NO + "',@MEMBERNO='" + MEMBERNO + "',@SOCIETYNAME='" + SOCIETYNAME + "',@DEFAULTERNAME='" + DEFAULTERNAME + "'," +
                "@accno='" + accno + "',@PRCDCD='" + PRCDCD + "',@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "',@STAGE='" + STAGE + "',@CASESTATUSNO='" + CASESTATUSNO + "',@ACTIONSTATUSNO='" + ACTIONSTATUSNO + "',@REMARK='" + REMARK + "',@Balance='" + Balance + "'," +
                "@AMOUNT='" + RECAMOUNT + "',@PAID_DEF_AMOUNT='" + PAID_DEF_AMOUNT + "',@MID='" + MID + "'";

            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int ADDACTIONSTATUS(string BRCD, string PRDCD, string SRO_NO, string CASE_YEAR, string CASENO, string MEMBERNO, string SOCIETYNAME, string DEFAULTERNAME, string accno,
      string PRCDCD, string ENTRYDATE, string RECAMOUNT, string PAID_DEF_AMOUNT, string STAGE, string MID, string REMARK, string CASESTATUSNO, string ACTIONSTATUSNO, string Balance)
    {
        try
        {
            sql = "exec AN_S009ACTION @FL='ADC',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@SRO_NO='" + SRO_NO + "',@MEMBERNO='" + MEMBERNO + "',@SOCIETYNAME='" + SOCIETYNAME + "',@DEFAULTERNAME='" + DEFAULTERNAME + "'," +
                "@accno='" + accno + "',@PRCDCD='" + PRCDCD + "',@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "',@STAGE='" + STAGE + "',@CASESTATUSNO='" + CASESTATUSNO + "',@ACTIONSTATUSNO='" + ACTIONSTATUSNO + "',@REMARK='" + REMARK + "',@Balance='" + Balance + "'," +
                "@AMOUNT='" + RECAMOUNT + "',@PAID_DEF_AMOUNT='" + PAID_DEF_AMOUNT + "',@MID='" + MID + "'";

            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int MODISocietyRec(string BRCD, string PRDCD, string SRO_NO, string CASE_YEAR, string CASENO, string MEMBERNO, string SOCIETYNAME, string DEFAULTERNAME, string accno,
     string PRCDCD, string ENTRYDATE, string RECAMOUNT, string PAID_DEF_AMOUNT, string STAGE, string MID, string REMARK, string CASESTATUSNO, string ACTIONSTATUSNO, string Balance,string ID)
    {
        try
        {
            sql = "exec AN_S003PAY @FL='MDC',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@SRO_NO='" + SRO_NO + "',@MEMBERNO='" + MEMBERNO + "',@SOCIETYNAME='" + SOCIETYNAME + "',@DEFAULTERNAME='" + DEFAULTERNAME + "'," +
                "@accno='" + accno + "',@PRCDCD='" + PRCDCD + "',@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "',@STAGE='" + STAGE + "',@CASESTATUSNO='" + CASESTATUSNO + "',@ACTIONSTATUSNO='" + ACTIONSTATUSNO + "',@REMARK='" + REMARK + "',@Balance='" + Balance + "',@SOCIeTYID='" + ID + "'," +
                "@AMOUNT='" + RECAMOUNT + "',@PAID_DEF_AMOUNT='" + PAID_DEF_AMOUNT + "',@MID='" + MID + "'";

            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int MODICASESTATUS(string BRCD, string PRDCD, string SRO_NO, string CASE_YEAR, string CASENO, string MEMBERNO, string SOCIETYNAME, string DEFAULTERNAME, string accno,
    string PRCDCD, string ENTRYDATE, string RECAMOUNT, string PAID_DEF_AMOUNT, string STAGE, string MID, string REMARK, string CASESTATUSNO, string ACTIONSTATUSNO, string Balance,string id)
    {
        try
        {
            sql = "exec AN_S008CASE @FL='MDC',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@SRO_NO='" + SRO_NO + "',@MEMBERNO='" + MEMBERNO + "',@SOCIETYNAME='" + SOCIETYNAME + "',@DEFAULTERNAME='" + DEFAULTERNAME + "'," +
                "@accno='" + accno + "',@PRCDCD='" + PRCDCD + "',@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "',@STAGE='" + STAGE + "',@CASESTATUSNO='" + CASESTATUSNO + "',@ACTIONSTATUSNO='" + ACTIONSTATUSNO + "',@REMARK='" + REMARK + "',@Balance='" + Balance + "',@SOCIeTYID='" + id + "'," +
                "@AMOUNT='" + RECAMOUNT + "',@PAID_DEF_AMOUNT='" + PAID_DEF_AMOUNT + "',@MID='" + MID + "'";

            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int MODIACTIONSTATUS(string BRCD, string PRDCD, string SRO_NO, string CASE_YEAR, string CASENO, string MEMBERNO, string SOCIETYNAME, string DEFAULTERNAME, string accno,
  string PRCDCD, string ENTRYDATE, string RECAMOUNT, string PAID_DEF_AMOUNT, string STAGE, string MID, string REMARK, string CASESTATUSNO, string ACTIONSTATUSNO, string Balance)
    {
        try
        {
            sql = "exec AN_S009ACTION @FL='MDC',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASE_YEAR + "',@SRO_NO='" + SRO_NO + "',@MEMBERNO='" + MEMBERNO + "',@SOCIETYNAME='" + SOCIETYNAME + "',@DEFAULTERNAME='" + DEFAULTERNAME + "'," +
                "@accno='" + accno + "',@PRCDCD='" + PRCDCD + "',@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "',@STAGE='" + STAGE + "',@CASESTATUSNO='" + CASESTATUSNO + "',@ACTIONSTATUSNO='" + ACTIONSTATUSNO + "',@REMARK='" + REMARK + "',@Balance='" + Balance + "'," +
                "@AMOUNT='" + RECAMOUNT + "',@PAID_DEF_AMOUNT='" + PAID_DEF_AMOUNT + "',@MID='" + MID + "'";

            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int DeletCASESTATUS(string BRCD, string CASENO, string CASEYEAR,string id)
    {
        try
        {
            sql = "exec AN_S008CASE @FL='CA',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYEAR + "',@SOCIeTYID='"+id+"'";


            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int DeletACTIONSTATUS(string BRCD, string CASENO, string CASEYEAR,string id)
    {
        try
        {
            sql = "exec AN_S009ACTION @FL='CA',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYEAR + "',@SOCIeTYID='" + id + "'";


            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int DeletSocietyRec(string BRCD, string CASENO, string CASEYEAR,string id)
    {
        try
        {
            sql = "exec AN_S003PAY @FL='CA',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYEAR + "',@SOCIeTYID='" + id + "'";


            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int DeleteCashRecipt(string brcd, string caseno, string caseyear, string setno, string ENTRYDATE ,string id)
    {
        try
        {
            string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');

            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();



            sql = "update AllVCR   set Stage = '1004' Where BRCD='" + brcd + "' AND STAGE = '1003' and CustName='" + caseyear + "' and CustNo='" + caseno + "' and  setno='" + setno + "' and SCROLLNO='" + id + "' ";
            res = conn.sExecuteQuery(sql);
            sql = "update  " + TableName + "  set Stage = '1004' Where BRCD='" + brcd + "' AND STAGE = '1003' and CustName='" + caseyear + "' and CustNo='" + caseno + "'and  setno='" + setno + "'and  ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "' and SCROLLNO='" + id + "' ";
            res = conn.sExecuteQuery(sql);
            sql = "update  Avs_2001_Recipt  set Stage = '1004' Where BRCD='" + brcd + "' AND STAGE = '1003' and CustName='" + caseyear + "' and CustNo='" + caseno + "'and  setno='" + setno + "'and  ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "' and SCROLLNO='" + id + "' ";
         
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }


    public DataTable ViewCashRecipt(string BRCD, string CASENO, string CASE_YEAR, string setno, string ENTRYDATE, string id)
    
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Select * from  AllVCR  Where BRCD='" + BRCD + "'  and CustName='" + CASE_YEAR + "' and CustNo='" + CASENO + "' and  setno='" + setno + "' and SCROLLNO='" + id + "' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public int ModCashRecipt(string brcd, string caseno, string caseyear, string setno, string ENTRYDATE, string id, string GLCODE, string SUBGLCODE, string CR, string INSTRUMENTNO, string INSTRUMENTDATE, string INSTBANKCD, string PayType, string PAYMAST)
    {
        try
        {
            string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');

            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();



           // sql = "update AllVCR   set Stage = '1002' Where BRCD='" + brcd + "'  and CustName='" + caseyear + "' and CustNo='" + caseno + "' and  setno='" + setno + "' and SCROLLNO='" + id + "' ";

            sql = "update AllVCR   set Stage = '1003',ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "',glcode='" + GLCODE + "',SUBGLCODE='" + SUBGLCODE + "' ,CREDIT='" + CR + "',SETNO='" + setno + "',SCROLLNO='" + id + "',PMTMODE='" + PayType + "',PAYMAST='" + PAYMAST + "',INSTRUMENTNO='" + INSTRUMENTNO + "',INSTRUMENTDATE='" + conn.ConvertDate(INSTRUMENTDATE) + "',INSTBANKCD='" + INSTBANKCD + "' Where BRCD='" + brcd + "'  and CustName='" + caseyear + "' and CustNo='" + caseno + "' and  setno='" + setno + "' and SCROLLNO='" + id + "'";
            res = conn.sExecuteQuery(sql);
          //  sql = "update  " + TableName + "  set Stage = '1004' Where BRCD='" + brcd + "' AND STAGE = '1003' and CustName='" + caseyear + "' and CustNo='" + caseno + "'and  setno='" + setno + "'and  ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "' and SCROLLNO='" + id + "' ";
            sql = "update " + TableName + "   set Stage = '1003',ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "',glcode='" + GLCODE + "',SUBGLCODE='" + SUBGLCODE + "' ,AMOUNT='" + CR + "' ,SETNO='" + setno + "',SCROLLNO='" + id + "',PMTMODE='" + PayType + "',PAYMAST='" + PAYMAST + "',INSTRUMENTNO='" + INSTRUMENTNO + "',INSTRUMENTDATE='" + conn.ConvertDate(INSTRUMENTDATE )+ "',INSTBANKCD='" + INSTBANKCD + "' Where BRCD='" + brcd + "'  and CustName='" + caseyear + "' and CustNo='" + caseno + "' and  setno='" + setno + "' and SCROLLNO='" + id + "'";
            res = conn.sExecuteQuery(sql);
            sql = "update Avs_2001_Recipt   set Stage = '1003',ENTRYDATE='" +conn.ConvertDate( ENTRYDATE) + "',glcode='" + GLCODE + "',SUBGLCODE='" + SUBGLCODE + "' ,AMOUNT='" + CR + "' ,SETNO='" + setno + "',SCROLLNO='" + id + "',PMTMODE='" + PayType + "',PAYMAST='" + PAYMAST + "',INSTRUMENTNO='" + INSTRUMENTNO + "',INSTRUMENTDATE='" +conn.ConvertDate( INSTRUMENTDATE )+ "',INSTBANKCD='" + INSTBANKCD + "' Where BRCD='" + brcd + "'  and CustName='" + caseyear + "' and CustNo='" + caseno + "' and  setno='" + setno + "' and SCROLLNO='" + id + "'";
          
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }


    //public DataTable RptSRNOMONTHLYRPTS(string flag, string FDATE, string TDATE, string FSRNO, string TSRNO)
    //{
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        sql = "Exec SP_SROMonthlyRPTS @FLAG='" + flag + "', @FDATE='" + conn.ConvertDate(FDATE) + "',@TDATE='" + conn.ConvertDate(TDATE) + "',@FSRNO='" + FSRNO + "',@TSRNO='" + TSRNO + "' ";
    //        dt = conn.GetDatatable(sql);
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionLogging.SendErrorToText(ex);
    //    }
    //    return dt;
    //}

    public DataTable RptSRNOMONTHLYRPTS(string flag, string FDATE, string TDATE, string FSRNO, string TSRNO, string STAGE)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Exec SP_SROMonthlyRPTS @FLAG='" + flag + "', @FDATE='" + conn.ConvertDate(FDATE) + "',@TDATE='" + conn.ConvertDate(TDATE) + "',@FSRNO='" + FSRNO + "',@TSRNO='" + TSRNO + "',@STAGE='" + STAGE + "' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable RPTNOOFCASERPT(string flag, string FDATE, string TDATE, string FSRNO, string TSRNO)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Exec SP_NOOFCASERPT @FLAG='" + flag + "', @FDATE='" + conn.ConvertDate(FDATE) + "',@TDATE='" + conn.ConvertDate(TDATE) + "',@FSRNO='" + FSRNO + "',@TSRNO='" + TSRNO + "' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable RptEXECUSIONMONTHLYRPTS(string flag, string FDATE, string TDATE, string FSRNO, string TSRNO ,string stage)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Exec SP_ExecutionMonthRPT @FLAG='" + flag + "', @FDATE='" + conn.ConvertDate(FDATE) + "',@TDATE='" + conn.ConvertDate(TDATE) + "',@FSRNO='" + FSRNO + "',@TSRNO='" + TSRNO + "',@STAGE1='"+stage+"' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable RptCOSTOFMONTHLYRPTS(string flag, string FDATE, string TDATE, string FSRNO, string TSRNO,string STAGE)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Exec SP_COSTOFPROCESSMonthRPT @FLAG='" + flag + "', @FDATE='" + conn.ConvertDate(FDATE) + "',@TDATE='" + conn.ConvertDate(TDATE) + "',@FSRNO='" + FSRNO + "',@TSRNO='" + TSRNO + "',@STAGE1='"+STAGE+"' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable RptNOCASE(string flag,string CASEY,string CASENO)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Exec AN_S002 @FL='" + flag + "',@CASE_YEAR='" + CASEY + "',@CASENO='" + CASENO + "' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public int STATEMENTACC(string BRCD, string SRO_NO, string CASE_YEAR, string CASENO, string MEMBERNO, string DEFAULTERNAME, string ENTRYDATE, string MID, string CID, string VID, string PCMAC, string Remark, string PAYMENTMODE, string CHEQUENO, string principalamt2, string Todate1, string fromdate1, string chequedate, string OTHECHS, string CaseStatus1, string ACTIONSTATUS1, string TOTINT2, string AMTPAID, string BAL, string hrem, string NOTICESTAGE)
    {
        try
        {
            sql = "INSERT INTO avs_2001_History (BRCD, PRDCD, SRO_NO, C_F_N_101, C_F_D_101, C_F_N_91, C_F_DT_91, NOTICE_ISS_DT, TOT_RECV, DIV_CITY, COURT_NAME, BSD_SRO, AWARD_EXP, TALATHI_OW, COMP_OW, REMARK1, RAMARK2, STAGE, MID, CID, VID, SYSTEMDATE, PCMAC, R_O_Dt, S_O_Dt, sro_ow_no, comp_install, addto_loanee, addto_Gur1, addto_Gur2, addto_Gur3, Avs_STATUS, Casestatus, CASE_YEAR, CASENO, MEMBERNO, DEFAULTERNAME, accno, PRCDCD, rate, principle, fromdate, todate, month, MOVATTDATE, totint, WARD, PINCODE, IMMATTDATE, COSTPROCESS, COSTAPPLICATION, DEFAULTPROPERTY, CORRESPONDENCEADDRESS, MOBILE1, MOBILE2, OCC_DETAIL, OCC_ADD, DEFAULTVALUE, DESIGNATION, COMM_NAME, COM_MOBILE1, COM_MOBILE2, COM_ADDRESS, COM_WARD, COM_CITY, COM_PINCODE, TOTALNOTICEGENERATE, LASTNOTICEDATE, MEMTYPE, RCNOTYPE, ORDERBY, PROPERTYTYPE, PROPERTYTYPENO, ToBankDesignation, BAnkAddress, BankAcNo, FloorNO, EXECUTIONCHARG, AMOUNT, PAYMENTMODE, CHEQUENO, principalamt2, Todate1, fromdate1, chequedate, othercharges, CaseStatus1, ActionStatus1, Remark, ENTRYDATE1, TOTINT2, AMTPAID,REMBAL,HREMBAL,NOTICESTAGE)" +
                                        "  SELECT BRCD, PRDCD, SRO_NO, C_F_N_101, C_F_D_101, C_F_N_91, C_F_DT_91, NOTICE_ISS_DT, TOT_RECV, DIV_CITY, COURT_NAME, BSD_SRO, AWARD_EXP, TALATHI_OW, COMP_OW, REMARK1, RAMARK2, STAGE, MID, CID, VID, SYSTEMDATE, PCMAC, R_O_Dt, S_O_Dt, sro_ow_no, comp_install, addto_loanee, addto_Gur1, addto_Gur2, addto_Gur3, Avs_STATUS, Casestatus, CASE_YEAR, CASENO, MEMBERNO, DEFAULTERNAME, accno, PRCDCD, rate, principle, fromdate, todate, month, MOVATTDATE, totint, WARD, PINCODE, IMMATTDATE, COSTPROCESS, COSTAPPLICATION, DEFAULTPROPERTY, CORRESPONDENCEADDRESS, MOBILE1, MOBILE2, OCC_DETAIL, OCC_ADD, DEFAULTVALUE, DESIGNATION, COMM_NAME, COM_MOBILE1, COM_MOBILE2, COM_ADDRESS, COM_WARD, COM_CITY, COM_PINCODE, TOTALNOTICEGENERATE, LASTNOTICEDATE, MEMTYPE, RCNOTYPE, ORDERBY, PROPERTYTYPE, PROPERTYTYPENO, ToBankDesignation, BAnkAddress, BankAcNo, FloorNO, EXECUTIONCHARG, AMOUNT, PAYMENTMODE, CHEQUENO, principalamt2, Todate1, fromdate1, chequedate, othercharges, CaseStatus1, ActionStatus1, Remark, ENTRYDATE1, TOTINT2, AMTPAID, REMBAL,HREMBAL,NOTICESTAGE" +
                          "  FROM   AVS_2001 WHERE  BRCD='" + BRCD + "' and CASENO='" + CASENO + "' AND CASE_YEAR='" + CASE_YEAR + "'";
          
                   
             Result = conn.sExecuteQuery(sql);
             if (Result > 0)
             {
                 sql = "exec AN_S002_demo @FL='MDACCST',@BRCD='" + BRCD + "',   @SRO_NO='" + SRO_NO + "', @CASE_YEAR='" + CASE_YEAR + "',  @CASENO='" + CASENO + "', @MEMBERNO='" + MEMBERNO + "'  ,@DEFAULTERNAME='" + DEFAULTERNAME + "'," +
               "@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "',@STAGE='1001',@MID='" + MID + "',@CID='" + CID + "',@VID='" + VID + "',@PCMAC='" + PCMAC + "',@REMARK='" + Remark + "',	@PAYMENTMODE='" + PAYMENTMODE + "'," +
               "@CHEQUENO='" + CHEQUENO + "' ,@principalamt2='" + principalamt2 + "' ,	@Todate1='" + conn.ConvertDate(Todate1) + "' ,	@fromdate1='" + conn.ConvertDate(fromdate1) + "' ,@chequedate='" + conn.ConvertDate(chequedate) + "' ,@othercharges='" + OTHECHS + "'," +
               "@CaseStatus1='" + CaseStatus1 + "',@ActionStatus1='" + ACTIONSTATUS1 + "',@TOTINT2='" + TOTINT2 + "',@AMTPAID='" + AMTPAID + "',@TOT_RECV='" + BAL + "',@HREM='" + hrem + "',@NOTICESTAGE='" + NOTICESTAGE + "'";
      
                
                           Result = conn.sExecuteQuery(sql);
             }

             }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public int MODIFYRSTATEMENTACC(string BRCD, string SRO_NO, string CASE_YEAR, string CASENO, string MEMBERNO, string DEFAULTERNAME, string ENTRYDATE, string MID, string CID, string VID, string PCMAC, string Remark, string PAYMENTMODE, string CHEQUENO, string principalamt2, string Todate1, string fromdate1, string chequedate, string OTHECHS, string CaseStatus1, string ACTIONSTATUS1, string TOTINT2, string AMTPAID, string BAL, string hrem, string NOTICESTAGE)
    {
        try
        {
            //sql = "INSERT INTO avs_2001_History (BRCD, PRDCD, SRO_NO, C_F_N_101, C_F_D_101, C_F_N_91, C_F_DT_91, NOTICE_ISS_DT, TOT_RECV, DIV_CITY, COURT_NAME, BSD_SRO, AWARD_EXP, TALATHI_OW, COMP_OW, REMARK1, RAMARK2, STAGE, MID, CID, VID, SYSTEMDATE, PCMAC, R_O_Dt, S_O_Dt, sro_ow_no, comp_install, addto_loanee, addto_Gur1, addto_Gur2, addto_Gur3, Avs_STATUS, Casestatus, CASE_YEAR, CASENO, MEMBERNO, DEFAULTERNAME, accno, PRCDCD, rate, principle, fromdate, todate, month, MOVATTDATE, totint, WARD, PINCODE, IMMATTDATE, COSTPROCESS, COSTAPPLICATION, DEFAULTPROPERTY, CORRESPONDENCEADDRESS, MOBILE1, MOBILE2, OCC_DETAIL, OCC_ADD, DEFAULTVALUE, DESIGNATION, COMM_NAME, COM_MOBILE1, COM_MOBILE2, COM_ADDRESS, COM_WARD, COM_CITY, COM_PINCODE, TOTALNOTICEGENERATE, LASTNOTICEDATE, MEMTYPE, RCNOTYPE, ORDERBY, PROPERTYTYPE, PROPERTYTYPENO, ToBankDesignation, BAnkAddress, BankAcNo, FloorNO, EXECUTIONCHARG, AMOUNT, PAYMENTMODE, CHEQUENO, principalamt2, Todate1, fromdate1, chequedate, othercharges, CaseStatus1, ActionStatus1, Remark, ENTRYDATE1, TOTINT2, AMTPAID,REMBAL,HREMBAL,NOTICESTAGE)" +
            //                            "  SELECT BRCD, PRDCD, SRO_NO, C_F_N_101, C_F_D_101, C_F_N_91, C_F_DT_91, NOTICE_ISS_DT, TOT_RECV, DIV_CITY, COURT_NAME, BSD_SRO, AWARD_EXP, TALATHI_OW, COMP_OW, REMARK1, RAMARK2, STAGE, MID, CID, VID, SYSTEMDATE, PCMAC, R_O_Dt, S_O_Dt, sro_ow_no, comp_install, addto_loanee, addto_Gur1, addto_Gur2, addto_Gur3, Avs_STATUS, Casestatus, CASE_YEAR, CASENO, MEMBERNO, DEFAULTERNAME, accno, PRCDCD, rate, principle, fromdate, todate, month, MOVATTDATE, totint, WARD, PINCODE, IMMATTDATE, COSTPROCESS, COSTAPPLICATION, DEFAULTPROPERTY, CORRESPONDENCEADDRESS, MOBILE1, MOBILE2, OCC_DETAIL, OCC_ADD, DEFAULTVALUE, DESIGNATION, COMM_NAME, COM_MOBILE1, COM_MOBILE2, COM_ADDRESS, COM_WARD, COM_CITY, COM_PINCODE, TOTALNOTICEGENERATE, LASTNOTICEDATE, MEMTYPE, RCNOTYPE, ORDERBY, PROPERTYTYPE, PROPERTYTYPENO, ToBankDesignation, BAnkAddress, BankAcNo, FloorNO, EXECUTIONCHARG, AMOUNT, PAYMENTMODE, CHEQUENO, principalamt2, Todate1, fromdate1, chequedate, othercharges, CaseStatus1, ActionStatus1, Remark, ENTRYDATE1, TOTINT2, AMTPAID, REMBAL,HREMBAL,NOTICESTAGE" +
            //              "  FROM   AVS_2001 WHERE  BRCD='" + BRCD + "' and CASENO='" + CASENO + "' AND CASE_YEAR='" + CASE_YEAR + "'";


            //Result = conn.sExecuteQuery(sql);
            //if (Result > 0)
            {
                sql = "exec AN_S002_demo @FL='MDACCST',@BRCD='" + BRCD + "',   @SRO_NO='" + SRO_NO + "', @CASE_YEAR='" + CASE_YEAR + "',  @CASENO='" + CASENO + "', @MEMBERNO='" + MEMBERNO + "'  ,@DEFAULTERNAME='" + DEFAULTERNAME + "'," +
              "@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "',@STAGE='1001',@MID='" + MID + "',@CID='" + CID + "',@VID='" + VID + "',@PCMAC='" + PCMAC + "',@REMARK='" + Remark + "',	@PAYMENTMODE='" + PAYMENTMODE + "'," +
              "@CHEQUENO='" + CHEQUENO + "' ,@principalamt2='" + principalamt2 + "' ,	@Todate1='" + conn.ConvertDate(Todate1) + "' ,	@fromdate1='" + conn.ConvertDate(fromdate1) + "' ,@chequedate='" + conn.ConvertDate(chequedate) + "' ,@othercharges='" + OTHECHS + "'," +
              "@CaseStatus1='" + CaseStatus1 + "',@ActionStatus1='" + ACTIONSTATUS1 + "',@TOTINT2='" + TOTINT2 + "',@AMTPAID='" + AMTPAID + "',@TOT_RECV='" + BAL + "',@HREM='" + hrem + "',@NOTICESTAGE='" + NOTICESTAGE + "'";


                Result = conn.sExecuteQuery(sql);
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public int CANSTATEMENTACC(string BRCD, string CASE_YEAR, string CASENO,string id)
    {
        try
        {
           
            {
                sql = "exec AN_S002_demo @FL='CAACCSTAll',@BRCD='" + BRCD + "',   @CASE_YEAR='" + CASE_YEAR + "',  @CASENO='" + CASENO + "', @ID='" + id + "'";


                Result = conn.sExecuteQuery(sql);
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public int MODSTATEMENTACCALL(string BRCD, string SRO_NO, string CASE_YEAR, string CASENO, string MEMBERNO, string DEFAULTERNAME, string ENTRYDATE, string MID, string CID, string VID, string PCMAC, string Remark, string PAYMENTMODE, string CHEQUENO, string principalamt2, string Todate1, string fromdate1, string chequedate, string OTHECHS, string CaseStatus1, string ACTIONSTATUS1, string TOTINT2, string AMTPAID, string BAL, string hrem, string id, string NOTICESTAGE)
    {
        try
        {
            //sql = "INSERT INTO avs_2001_History (BRCD, PRDCD, SRO_NO, C_F_N_101, C_F_D_101, C_F_N_91, C_F_DT_91, NOTICE_ISS_DT, TOT_RECV, DIV_CITY, COURT_NAME, BSD_SRO, AWARD_EXP, TALATHI_OW, COMP_OW, REMARK1, RAMARK2, STAGE, MID, CID, VID, SYSTEMDATE, PCMAC, R_O_Dt, S_O_Dt, sro_ow_no, comp_install, addto_loanee, addto_Gur1, addto_Gur2, addto_Gur3, Avs_STATUS, Casestatus, CASE_YEAR, CASENO, MEMBERNO, DEFAULTERNAME, accno, PRCDCD, rate, principle, fromdate, todate, month, MOVATTDATE, totint, WARD, PINCODE, IMMATTDATE, COSTPROCESS, COSTAPPLICATION, DEFAULTPROPERTY, CORRESPONDENCEADDRESS, MOBILE1, MOBILE2, OCC_DETAIL, OCC_ADD, DEFAULTVALUE, DESIGNATION, COMM_NAME, COM_MOBILE1, COM_MOBILE2, COM_ADDRESS, COM_WARD, COM_CITY, COM_PINCODE, TOTALNOTICEGENERATE, LASTNOTICEDATE, MEMTYPE, RCNOTYPE, ORDERBY, PROPERTYTYPE, PROPERTYTYPENO, ToBankDesignation, BAnkAddress, BankAcNo, FloorNO, EXECUTIONCHARG, AMOUNT, PAYMENTMODE, CHEQUENO, principalamt2, Todate1, fromdate1, chequedate, othercharges, CaseStatus1, ActionStatus1, Remark, ENTRYDATE1, TOTINT2, AMTPAID,REMBAL,HREMBAL)" +
            //                            "  SELECT BRCD, PRDCD, SRO_NO, C_F_N_101, C_F_D_101, C_F_N_91, C_F_DT_91, NOTICE_ISS_DT, TOT_RECV, DIV_CITY, COURT_NAME, BSD_SRO, AWARD_EXP, TALATHI_OW, COMP_OW, REMARK1, RAMARK2, STAGE, MID, CID, VID, SYSTEMDATE, PCMAC, R_O_Dt, S_O_Dt, sro_ow_no, comp_install, addto_loanee, addto_Gur1, addto_Gur2, addto_Gur3, Avs_STATUS, Casestatus, CASE_YEAR, CASENO, MEMBERNO, DEFAULTERNAME, accno, PRCDCD, rate, principle, fromdate, todate, month, MOVATTDATE, totint, WARD, PINCODE, IMMATTDATE, COSTPROCESS, COSTAPPLICATION, DEFAULTPROPERTY, CORRESPONDENCEADDRESS, MOBILE1, MOBILE2, OCC_DETAIL, OCC_ADD, DEFAULTVALUE, DESIGNATION, COMM_NAME, COM_MOBILE1, COM_MOBILE2, COM_ADDRESS, COM_WARD, COM_CITY, COM_PINCODE, TOTALNOTICEGENERATE, LASTNOTICEDATE, MEMTYPE, RCNOTYPE, ORDERBY, PROPERTYTYPE, PROPERTYTYPENO, ToBankDesignation, BAnkAddress, BankAcNo, FloorNO, EXECUTIONCHARG, AMOUNT, PAYMENTMODE, CHEQUENO, principalamt2, Todate1, fromdate1, chequedate, othercharges, CaseStatus1, ActionStatus1, Remark, ENTRYDATE1, TOTINT2, AMTPAID, REMBAL,HREMBAL" +
            //              "  FROM   AVS_2001 WHERE  BRCD='" + BRCD + "' and CASENO='" + CASENO + "' AND CASE_YEAR='" + CASE_YEAR + "'";


           // Result = conn.sExecuteQuery(sql);
              {
                sql = "exec AN_S002_demo @FL='MDACCSTAll',@BRCD='" + BRCD + "',   @SRO_NO='" + SRO_NO + "', @CASE_YEAR='" + CASE_YEAR + "',  @CASENO='" + CASENO + "', @MEMBERNO='" + MEMBERNO + "'  ,@DEFAULTERNAME='" + DEFAULTERNAME + "'," +
              "@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "',@STAGE='1001',@MID='" + MID + "',@CID='" + CID + "',@VID='" + VID + "',@PCMAC='" + PCMAC + "',@REMARK='" + Remark + "',	@PAYMENTMODE='" + PAYMENTMODE + "'," +
              "@CHEQUENO='" + CHEQUENO + "' ,@principalamt2='" + principalamt2 + "' ,	@Todate1='" + conn.ConvertDate(Todate1) + "' ,	@fromdate1='" + conn.ConvertDate(fromdate1) + "' ,@chequedate='" + conn.ConvertDate(chequedate) + "' ,@othercharges='" + OTHECHS + "'," +
              "@CaseStatus1='" + CaseStatus1 + "',@ActionStatus1='" + ACTIONSTATUS1 + "',@TOTINT2='" + TOTINT2 + "',@AMTPAID='" + AMTPAID + "',@TOT_RECV='" + BAL + "',@HREM='" + hrem + "',@ID='" + id + "',@NOTICESTAGE='" + NOTICESTAGE + "'";


                Result = conn.sExecuteQuery(sql);
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }



    public int Authorized(string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS,
       string PARTICULARS2, string AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE,
       string INSTBANKCD, string INSTBRCD, string STAGE, string RTIME, string BRCD, string MID, string CID, string VID, string PAYMAST, string CUSTNO,
       string CUSTNAME, string REFID, string Token, string RecSrno = "0")
    {
        try
        {

            string CR, DR;
            string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');

            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            CR = DR = "";
            if (TRXTYPE == "1")
            {
                CR = AMOUNT;
                DR = "0";
            }
            else if (TRXTYPE == "2")
            {
                DR = AMOUNT;
                CR = "0";
            }
            if (ACCNO == "")
                ACCNO = "0";

            if (Convert.ToDouble(SETNO) < 20000)
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where BrCd = '" + BRCD + "' And EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            else
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            SCROLLNO = conn.sExecuteScalar(sql);

            REFID = REFID.ToString() == "" ? "0" : REFID;
            EntryMid = Ecry.GetMK(MID.ToString());

            sql = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID, PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE,TrxType) " +
                "VALUES('" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "','" + PARTICULARS + "','" + CR + "','" + DR + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "', '" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "',GETDATE(),'" + TRXTYPE + "')";
            Result = conn.sExecuteQuery(sql);

            sql = "INSERT INTO " + TableName + " (RECSRNO,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD, MID,VID, PCMAC,PAYMAST, CUSTNO, CUSTNAME, REFID, F1, TokenNo, RefBrcd, OrgBrCd, SystemDate) " +
               " VALUES ('" + RecSrno + "','1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','" + VID + "', '" + conn.PCNAME().ToString() + "', '" + PAYMAST + "', '" + CUSTNO + "', '" + CUSTNAME + "', '" + REFID + "', '" + EntryMid + "', '" + Token + "', '" + CID + "', '" + VID + "', GetDate())";
            Result = conn.sExecuteQuery(sql);
            sql = "INSERT INTO Avs_2001_Recipt (RECSRNO,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD, MID,VID, PCMAC,PAYMAST, CUSTNO, CUSTNAME, REFID, F1, TokenNo, RefBrcd, OrgBrCd, SystemDate) " +
             " VALUES ('" + RecSrno + "','1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','" + VID + "', '" + conn.PCNAME().ToString() + "', '" + PAYMAST + "', '" + CUSTNO + "', '" + CUSTNAME + "', '" + REFID + "', '" + EntryMid + "', '" + Token + "', '" + CID + "', '" + VID + "', GetDate())";
            Result = conn.sExecuteQuery(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
  


    

    public DataTable ShowNoticeDetails( string caseno, string CaseYear)//S------------------------
    {

        DataTable dt = new DataTable();
        try
        {
            sql = "SELECT N.ID,N.NAME,S.TOTALNOTICEGENERATED,S.LASTNOTICEDATE, S.CASE_YEAR,S.CASENO FROM AVS_NOTICE N LEFT JOIN AVS_NOTICE_STATUS S ON S.NOTICEID=N.ID AND  S.CASENO='" + caseno + "' AND S.CASE_YEAR='" + CaseYear + "'  ";//and NOTICE_ISS_DT='" + conn.ConvertDate(edate) + "' AND STAGE<>'1004'
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public int ADDdate(string noticedid,string LASTNOTICEDATE,string caseno, string CaseYear)
    {
        
        try
        {
            string sql = "SELECT * FROM  AVS_NOTICE_STATUS where  LASTNOTICEDATE='" + LASTNOTICEDATE + "' and CASENO='" + caseno + "' AND CASE_YEAR='" + CaseYear + "'  and NOTICEID='" + noticedid + "' ";
            string Result = conn.sExecuteScalar(sql);

            sql = "SELECT TOTALNOTICEGENERATED FROM  AVS_NOTICE_STATUS  where  LASTNOTICEDATE='" + LASTNOTICEDATE + "' and CASENO='" + caseno + "' AND CASE_YEAR='" + CaseYear + "'  and NOTICEID='" + noticedid + "' ";
            string Result1 = conn.sExecuteScalar(sql);

            if (Result != null)
            {
                string rs = Result1;

                sql = "insert into AVS_NOTICE_STATUS_BACK (CASENO,CASE_YEAR,NOTICEID,TOTALNOTICEGENERATED,LASTNOTICEDATE)Values('" + caseno + "','" + CaseYear + "','" + noticedid + "','" + rs + "','" + LASTNOTICEDATE + "' )";
                string Result2 = conn.sExecuteScalar(sql);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return record;
    }



        public int newDate(string newdate, string noticedid, string LASTNOTICEDATE, string caseno, string CaseYear)
    {
        try
        {
            sql = "update  AVS_NOTICE_STATUS set LASTNOTICEDATE='" + conn.ConvertDate(newdate) + "' where  LASTNOTICEDATE='" + conn.ConvertToDate(LASTNOTICEDATE) + "' and CASENO='" + caseno + "' AND CASE_YEAR='" + CaseYear + "'  and NOTICEID='" + noticedid + "' ";
             int Result = conn.sExecuteQuery(sql);

            
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return record;
    }
    
}
    
    


                     