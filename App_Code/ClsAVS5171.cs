using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

public class ClsAVS5171
{
    DataTable DT = new DataTable();
    DbConnection Conn = new DbConnection();
    int Res = 0;
    public string STR = "", sql = "";

    public GridView GRD { get; set; }
    public string BRCD { get; set; }
    public string MID { get; set; }
    public string ASONDT { get; set; }
    public string FL { get; set; }

    public string SBAL { get; set; }
    public string SINST { get; set; }
    public string SINTR { get; set; }


    public string S1Bal { get; set; }
    public string S1Inst { get; set; }
    public string S1Intr { get; set; }
    public string S2Bal { get; set; }
    public string S2Inst { get; set; }
    public string S2Intr { get; set; }
    public string S10Bal { get; set; }
    public string S10Inst { get; set; }
    public string S10Intr { get; set; }
    public string S3Bal { get; set; }
    public string S4Bal { get; set; }
    public string S5Bal { get; set; }
    public string S6Bal { get; set; }
    public string S7Bal { get; set; }
    public string S8Bal { get; set; }
    public string S9Bal { get; set; }
    public string ID { get; set; }
    public string RECCODE { get; set; }
    public string RECDIV { get; set; }
    public string MM { get; set; }
    public string YY { get; set; }
    public string DeathFund { get; set; }
    public string CustNO { get; set; }
    public string CustName { get; set; }
    public string Quate { get; set; }
    public string Narration { get; set; }
    public string MW { get; set; }
    public string US { get; set; }
    public string TYPERECOVERY { get; set; }
    public string BANKCODE { get; set; }

    public ClsAVS5171()
    {
        //
    }

    public int FnBl_RecOperations(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {
                //RO.sql = " Exec Isp_Recovery_Statement_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForYY='" + RO.YY + "',@ForMM='" + RO.MM + "'";
                RO.sql = " Exec Isp_Recovery_Create_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForYY='" + RO.YY + "',@ForMM='" + RO.MM + "'";
            }
            else
            {
                //  RO.sql = " Exec Isp_Recovery_Statement @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForYY='" + RO.YY + "',@ForMM='" + RO.MM + "'";
                RO.sql = " Exec Isp_Recovery_Create @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForYY='" + RO.YY + "',@ForMM='" + RO.MM + "'";

            }
            RO.Res = Conn.sBindGrid(RO.GRD, RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public int FnBl_RecCustOperations(ClsAVS5171 RO)//amruta
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {
                RO.sql = " Exec Isp_Recovery_CustStatement_X_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForYY='" + RO.YY + "',@ForMM='" + RO.MM + "',@CustNo='" + RO.CustNO + "'";
            }
            else
            {
                RO.sql = " Exec Isp_Recovery_CustStatement_X @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForYY='" + RO.YY + "',@ForMM='" + RO.MM + "',@CustNo='" + RO.CustNO + "'";
            }
            RO.Res = Conn.sBindGrid(RO.GRD, RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public int FnBl_ModifyCalc(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {
                RO.sql = "Exec Isp_Recovery_Statement_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "', " +
                                                    " @PS1Bal='" + RO.S1Bal + "',@PS1Inst='" + RO.S1Inst + "',@PS1Intr='" + RO.S1Intr + "'," +
                                                    " @PS2Bal='" + RO.S2Bal + "',@PS2Inst='" + RO.S2Inst + "',@PS2Intr='" + RO.S2Intr + "'," +
                                                    " @PS10Bal='" + RO.S10Bal + "',@PS10Inst='" + RO.S10Inst + "',@PS10Intr='" + RO.S10Intr + "'," +
                                                    " @PS3Bal='" + RO.S3Bal + "',@PS4Bal='" + RO.S4Bal + "',@PS5Bal='" + RO.S5Bal + "',@PS6Bal='" + RO.S6Bal + "'," +
                                                    "@PS7Bal='" + RO.S7Bal + "',@PS8Bal='" + RO.S8Bal + "',@PS9Bal='" + RO.S9Bal + "'," +
                                                    " @Id='" + RO.ID + "'";
            }
            else
            {
                RO.sql = "Exec Isp_Recovery_Statement @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "', " +
                                                    " @PS1Bal='" + RO.S1Bal + "',@PS1Inst='" + RO.S1Inst + "',@PS1Intr='" + RO.S1Intr + "'," +
                                                    " @PS2Bal='" + RO.S2Bal + "',@PS2Inst='" + RO.S2Inst + "',@PS2Intr='" + RO.S2Intr + "'," +
                                                    " @PS3Bal='" + RO.S3Bal + "',@PS4Bal='" + RO.S4Bal + "',@PS5Bal='" + RO.S5Bal + "',@PS6Bal='" + RO.S6Bal + "'," +
                                                    "@PS7Bal='" + RO.S7Bal + "',@PS8Bal='" + RO.S8Bal + "',@PS9Bal='" + RO.S9Bal + "'," +
                                                    " @Id='" + RO.ID + "'";
            }

            RO.Res = Conn.sExecuteQuery(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public int FnBl_CustModifyCalc(ClsAVS5171 RO)
    {
        try
        {

            RO.sql = "Exec Isp_Recovery_CustStatement @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "', " +
                                                " @PS1Bal='" + RO.S1Bal + "',@PS1Inst='" + RO.S1Inst + "',@PS1Intr='" + RO.S1Intr + "'," +
                                                " @PS2Bal='" + RO.S2Bal + "',@PS2Inst='" + RO.S2Inst + "',@PS2Intr='" + RO.S2Intr + "'," +
                                                " @PS3Bal='" + RO.S3Bal + "',@PS4Bal='" + RO.S4Bal + "',@PS5Bal='" + RO.S5Bal + "',@PS6Bal='" + RO.S6Bal + "'," +
                                                "@PS7Bal='" + RO.S7Bal + "',@PS8Bal='" + RO.S8Bal + "',@PS9Bal='" + RO.S9Bal + "'," +
                                                " @Id='" + RO.ID + "'";
            RO.Res = Conn.sExecuteQuery(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public int FnBl_AuthoCalc(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {

                RO.sql = "Exec Isp_Recovery_Statement_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "', " +
                                                    " @Id='" + RO.ID + "'";

            }
            else
            {
                RO.sql = "Exec Isp_Recovery_Statement @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "', " +
                                                   " @Id='" + RO.ID + "'";

            }
            Res = Conn.sExecuteQuery(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public int FnBl_CreateCalc(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {
                RO.sql = " Exec Isp_Recovery_Create_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecDiv='" + RO.RECDIV + "',@RecCode='" + RO.RECCODE + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "',@DeathFund='" + RO.DeathFund + "',@Narration='" + RO.Narration + "',@MW='" + RO.MW + "',@US='" + RO.US + "'";
            }
            else
            {
                RO.sql = " Exec Isp_Recovery_Create @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecDiv='" + RO.RECDIV + "',@RecCode='" + RO.RECCODE + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "',@DeathFund='" + RO.DeathFund + "',@Narration='" + RO.Narration + "',@MW='" + RO.MW + "',@US='" + RO.US + "'";
            }
            RO.Res = Conn.sExecuteQuery(RO.sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }


    public int FnBl_CreateCalcAll(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {
                RO.sql = " Exec Isp_Recovery_Statement_1009 @Flag='" + RO.FL + "',@TypeRecovery='" + RO.TYPERECOVERY + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "',@DeathFund='" + RO.DeathFund + "',@Narration='" + RO.Narration + "',@MW='" + RO.MW + "',@US='" + RO.US + "'";
            }
            else
            {
                RO.sql = " Exec Isp_Recovery_Statement @Flag='" + RO.FL + "',@TypeRecovery='" + RO.TYPERECOVERY + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "',@DeathFund='" + RO.DeathFund + "',@Narration='" + RO.Narration + "',@MW='" + RO.MW + "',@US='" + RO.US + "'";
            }
            RO.Res = Conn.sExecuteQuery(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public int FnBl_CreateCustCalc(ClsAVS5171 RO)//amruta
    {
        try
        {
            RO.sql = " Exec Isp_Recovery_CustStatement @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "',@DeathFund='" + RO.DeathFund + "',@CustNo='" + RO.CustNO + "'";
            RO.Res = Conn.sExecuteQuery(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public string FnBL_GetExistsRecovery(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {
                RO.sql = "Exec Isp_Recovery_Statement_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "'";
            }
            else
            {
                RO.sql = "Exec Isp_Recovery_Statement @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "'";
            }
            RO.sql = Conn.sExecuteScalar(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.sql;
    }

    public string FnBL_GetCustExistsRecovery(ClsAVS5171 RO)
    {
        try
        {
            RO.sql = "Exec Isp_Recovery_CustStatement @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "',@CustNo='" + RO.CustNO + "'";
            RO.sql = Conn.sExecuteScalar(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.sql;
    }

    public string FnBL_GetMID(ClsAVS5171 RO)
    {

        try
        {
            if (RO.BANKCODE == "1009")
            {
                RO.sql = "Exec Isp_Recovery_Statement_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@Id='" + RO.ID + "'";
            }
            else
            {
                RO.sql = "Exec Isp_Recovery_Statement @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@Id='" + RO.ID + "'";
            }
            RO.sql = Conn.sExecuteScalar(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.sql;
    }

    public DataTable GetLableName(ClsAVS5171 RO)
    {
        try
        {
            RO.sql = "Exec Isp_LabelName @brcd='" + RO.BRCD + "',@BKCODE='" + RO.BANKCODE + "'";
            RO.DT = Conn.GetDatatable(RO.sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return RO.DT;
    }

    public int FnBL_GetSumGrid(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {
                // RO.sql = "Exec Isp_Recovery_Statement_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "'";
                RO.sql = "Exec Isp_Recovery_Create_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "'";
            }
            else
            {
                //   RO.sql = "Exec Isp_Recovery_Statement @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "'";
                RO.sql = "Exec Isp_Recovery_Create @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "'";
            }
            RO.Res = Conn.sBindGrid(RO.GRD, RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public string FnBL_CheckMaster(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {

                RO.sql = "Exec Isp_Recovery_Statement_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "'";
            }
            else
            {
                RO.sql = "Exec Isp_Recovery_Statement @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "'";
            }
            RO.sql = Conn.sExecuteScalar(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.sql;
    }

    public string FnBL_CheckCustMaster(ClsAVS5171 RO)//amruta
    {
        try
        {
            RO.sql = "Exec Isp_Recovery_CustStatement @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@CustNo='" + RO.CustNO + "'";
            RO.sql = Conn.sExecuteScalar(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.sql;
    }

    public string GetCustName(ClsAVS5171 RO)
    {
        try
        {
            RO.sql = "exec ISP_GetCustName @Custno='" + RO.CustNO + "', @BRCD='" + RO.BRCD + "'";
            RO.sql = Conn.sExecuteScalar(RO.sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return RO.sql;
    }

    public int FnBl_CreateCalcAcc(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {
                RO.sql = " Exec Isp_Recovery_Create_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecDiv='" + RO.RECDIV + "',@RecCode='" + RO.RECCODE + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "',@DeathFund='" + RO.DeathFund + "',@Narration='" + RO.Narration + "',@MW='" + RO.MW + "',@US='" + RO.US + "'";
            }
            else
            {
                RO.sql = " Exec Isp_Recovery_Create @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecDiv='" + RO.RECDIV + "',@RecCode='" + RO.RECCODE + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "',@DeathFund='" + RO.DeathFund + "',@Narration='" + RO.Narration + "',@MW='" + RO.MW + "',@US='" + RO.US + "'";
            }
            RO.Res = Conn.sExecuteQuery(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public int FnBl_BindSingle(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {
                RO.sql = " Exec Isp_Recovery_Create_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecDiv='" + RO.RECDIV + "',@RecCode='" + RO.RECCODE + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "',@Custno='" + RO.CustNO + "'";
            }
            else
            {
                RO.sql = " Exec Isp_Recovery_Create @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecDiv='" + RO.RECDIV + "',@RecCode='" + RO.RECCODE + "',@ForMM='" + RO.MM + "',@ForYY='" + RO.YY + "',@CustNO='" + RO.CustNO + "'";
            }
            RO.Res = Conn.sBindGrid(RO.GRD, RO.sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public int FnBl_ModifyX(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {
                RO.sql = "Exec Isp_Recovery_Create_1009 @Flag='" + RO.FL + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@PSBal='" + RO.SBAL + "',@PSInst='" + RO.SINST + "',@PSIntr='" + RO.SINTR + "',@Id='" + RO.ID + "'";
            }
            else
            {
                RO.sql = "Exec Isp_Recovery_Create @Flag='" + RO.FL + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@PSBal='" + RO.SBAL + "',@PSInst='" + RO.SINST + "',@PSIntr='" + RO.SINTR + "',@Id='" + RO.ID + "'";
            }
            RO.Res = Conn.sExecuteQuery(RO.sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public int FnBl_RecOperationsNew(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {
                RO.sql = " Exec Isp_Recovery_Create_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForYY='" + RO.YY + "',@ForMM='" + RO.MM + "'";
            }
            else
            {
                RO.sql = " Exec Isp_Recovery_Create @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "',@ForYY='" + RO.YY + "',@ForMM='" + RO.MM + "'";
            }
            RO.Res = Conn.sBindGrid(RO.GRD, RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public int FnBl_AuthoCalcNew(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {

                RO.sql = "Exec Isp_Recovery_Create_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@ForMM='" + RO.MM + "', " +
                    " @ForYY='" + RO.YY + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "'";
            }
            else
            {
                RO.sql = "Exec Isp_Recovery_Create @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@ForMM='" + RO.MM + "', " +
                    " @ForYY='" + RO.YY + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "'";
            }
            RO.Res = Conn.sExecuteQuery(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public int FnBl_ALL_AuthoCalcNew(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {

                RO.sql = "Exec Isp_Recovery_Create_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@ForMM='" + RO.MM + "', " +
                    " @ForYY='" + RO.YY + "',@RecDiv='" + RO.RECDIV + "'";
            }
            else
            {
                RO.sql = "Exec Isp_Recovery_Create @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@ForMM='" + RO.MM + "', " +
                    " @ForYY='" + RO.YY + "',@RecDiv='" + RO.RECDIV + "'";
            }
            RO.Res = Conn.sExecuteQuery(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.Res;
    }

    public string FnBl_CheckMid(ClsAVS5171 RO)
    {
        try
        {
            if (RO.BANKCODE == "1009")
            {
                RO.sql = "Exec Isp_Recovery_Create_1009 @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@ForMM='" + RO.MM + "', " +
                    " @ForYY='" + RO.YY + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "'";
            }
            else
            {
                RO.sql = "Exec Isp_Recovery_Create @Flag='" + RO.FL + "',@Brcd='" + RO.BRCD + "',@OnDate='" + Conn.ConvertDate(RO.ASONDT) + "',@Mid='" + RO.MID + "',@ForMM='" + RO.MM + "', " +
                    " @ForYY='" + RO.YY + "',@RecCode='" + RO.RECCODE + "',@RecDiv='" + RO.RECDIV + "'";
            }
            RO.sql = Conn.sExecuteScalar(RO.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RO.sql;
    }
}