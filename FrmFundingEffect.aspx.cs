using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmFundingEffect : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsFunddingDate FD = new ClsFunddingDate();
    DbConnection conn = new DbConnection();
    ClsEncryptValue Encry = new ClsEncryptValue();
    ClsMultiVoucher MV = new ClsMultiVoucher();
    ClsCommon CMM = new ClsCommon();
    string Message = "", ResStr="";
    int RC = 0;

    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            BindGrid();
        }
    }

    public void BindGrid()
    {
        try
        {
            FD.Getinfotable(grdOwgData, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "O");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Fundding_Click(object sender, EventArgs e)
    {
        try
        {
            if (Chk_IsReturnDone.Checked == true)
            {
                if (grdOwgData.Rows.Count > 0)
                {
                    foreach (GridViewRow gvRow in this.grdOwgData.Rows)
                    {
                        int SetNO = Convert.ToInt32(((Label)gvRow.FindControl("SET_NO")).Text);
                        int PrCode = Convert.ToInt32(((Label)gvRow.FindControl("AT")).Text);
                        int AccNo = Convert.ToInt32(((Label)gvRow.FindControl("lblAccNo")).Text);
                        string InstNo = ((Label)gvRow.FindControl("INSTNO")).Text.ToString();
                        double Balance = Convert.ToDouble(((Label)gvRow.FindControl("Amount")).Text);
                        string F_STATUS = (((Label)gvRow.FindControl("Fund_Status")).Text);
                        string INSTTYPE = ((Label)gvRow.FindControl("Lbl_InstruType")).Text.ToString();
                        string BANK_CODE = ((Label)gvRow.FindControl("Lbl_BankCode")).Text.ToString();
                        string BRANCH_CODE = ((Label)gvRow.FindControl("Lbl_BranchCode")).Text.ToString();
                        string INSTRUDATE = ((Label)gvRow.FindControl("Lbl_InstruDate")).Text.ToString();
                        string ACC_TYPE = ((Label)gvRow.FindControl("Lbl_AccType")).Text.ToString();
                        string OPRTN_TYPE = ((Label)gvRow.FindControl("Lbl_OprType")).Text.ToString();
                        string Parti = ((Label)gvRow.FindControl("Parti")).Text.ToString();
                        string Amount = ((Label)gvRow.FindControl("Amount")).Text.ToString();



                        if (F_STATUS != "Funded")
                        {
                            string GLCODE = BD.GetAccTypeGL(PrCode.ToString(), Session["BRCD"].ToString());
                            if (GLCODE != null)
                            {
                                string[] GL = GLCODE.Split('_');
                                if (GL[1].ToString() == "3")
                                {
                                    ResStr = OW_LoanEntries(PrCode.ToString(), AccNo.ToString(), Amount.ToString(), InstNo.ToString(), INSTTYPE.ToString(), BANK_CODE.ToString(), BRANCH_CODE.ToString(), Parti.ToString(), INSTRUDATE.ToString(),
                                        ACC_TYPE.ToString(), OPRTN_TYPE.ToString());
                                }
                            }

                            RC = FD.FundAuthorize(SetNO.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), "O");
                        }

                        if (RC > 0)
                        {
                            //added by amol on 10/08/2017 For Insert record into avs1092 table
                            Balance = BD.ClBalance(Session["BRCD"].ToString(), PrCode.ToString(), AccNo.ToString(), Session["EntryDate"].ToString(), "ClBal");
                            Message = "Your Account " + AccNo.ToString() + " is Credited with INR " + Convert.ToDouble(Balance.ToString()) + " on " + conn.ConvertToDate(Session["EntryDate"].ToString()).ToString() + " Ref CHQ " + InstNo + " Your Balance (Subject to cheque realisation) is INR " + Balance + " RS.";
                            BD.InsertSMSRec(Session["BRCD"].ToString(), PrCode.ToString(), AccNo.ToString(), Message, Session["MID"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "Receipt");
                        }
                    }
                    if (RC > 0)
                    {
                        BindGrid();
                        WebMsgBox.Show("Funding Successfully Done......!!", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OutwardClearing_FudingEffect _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    }
                }
            }
            else
            {
                WebMsgBox.Show("Is Outward Return process done ?  If Yes , Tick the Checkbox....!", this.Page);
                Chk_IsReturnDone.Focus ();

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string OW_LoanEntries(string PrdCode,string Accno,string Amount,string InstruNo,string InstruType,string BankCode,string BranchCode,string Parti,string InstruDate,string AccType,string OprType)
    {
        try
        {
            DataTable DT = new DataTable();

            DT = MV.GetLoanTotalAmount(Session["BRCD"].ToString(),PrdCode.Trim().ToString(), Accno.Trim().ToString(), Session["EntryDate"].ToString(), "");

            if (DT.Rows.Count > 0)
            {

                // string IntAmt = (Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString())).ToString(); Commented as per Pen Reuquiremetn 04/09/2017
                string IntAmt = (Convert.ToDouble(DT.Rows[0]["Interest"]) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()).ToString();
                //string PenAmt = (Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString())).ToString(); Commented as per Pen Reuquiremetn 04/09/2017
                string PenAmt = "0";    // DT.Rows[0]["PInterest"].ToString();
                string RecAmt = "0";   // DT.Rows[0]["InterestRec"].ToString();
                string NotAmt = "0";   // DT.Rows[0]["NoticeChrg"].ToString();
                string SerAmt = "0";   // DT.Rows[0]["ServiceChrg"].ToString();
                string CouAmt = "0";   // DT.Rows[0]["CourtChrg"].ToString();
                string SurAmt = "0";   // DT.Rows[0]["SurChrg"].ToString();
                string OthAmt = "0";   // DT.Rows[0]["OtherChrg"].ToString();
                string BanAmt = "0";   // DT.Rows[0]["BankChrg"].ToString();
                string InsAmt = "0";   // DT.Rows[0]["InsChrg"].ToString();
                string IPriAmt = "0", IIntAmt = "0", IPenAmt = "0", IRecAmt = "0", INotAmt = "0", ISerAmt = "0", ICouAmt = "0", ISurAmt = "0", IOthAmt = "0", IBanAmt = "0", IInsAmt = "0";
                string PriAmt = "";
                if (Convert.ToDouble(DT.Rows[0]["Principle"]) < 0) //If Balance is Excess 05-12-2017
                {
                    PriAmt = Math.Abs(Convert.ToDouble(DT.Rows[0]["Principle"])).ToString();
                    IPriAmt = Amount;
                    
                }
                else
                {
                    PriAmt = DT.Rows[0]["Principle"].ToString();

                    double TotalDr = Convert.ToDouble(Amount);
                    if (TotalDr > Convert.ToDouble(InsAmt))
                    {
                        TotalDr = TotalDr - Convert.ToDouble(InsAmt);
                        IInsAmt = InsAmt;
                        if (TotalDr > Convert.ToDouble(BanAmt))
                        {
                            TotalDr = TotalDr - Convert.ToDouble(BanAmt);

                            IBanAmt = BanAmt;
                            if (TotalDr > Convert.ToDouble(OthAmt))
                            {
                                TotalDr = TotalDr - Convert.ToDouble(OthAmt);
                                IOthAmt = OthAmt;
                                if (TotalDr > Convert.ToDouble(SurAmt))
                                {
                                    TotalDr = TotalDr - Convert.ToDouble(SurAmt);
                                    ISurAmt = SurAmt;
                                    if (TotalDr > Convert.ToDouble(CouAmt))
                                    {
                                        TotalDr = TotalDr - Convert.ToDouble(CouAmt);
                                        ICouAmt = CouAmt;
                                        if (TotalDr > Convert.ToDouble(SerAmt))
                                        {
                                            TotalDr = TotalDr - Convert.ToDouble(SerAmt);
                                            ISerAmt = SerAmt;
                                            if (TotalDr > Convert.ToDouble(NotAmt))
                                            {
                                                TotalDr = TotalDr - Convert.ToDouble(NotAmt);
                                                INotAmt = NotAmt;
                                                if (TotalDr > Convert.ToDouble(RecAmt))
                                                {
                                                    TotalDr = TotalDr - Convert.ToDouble(RecAmt);
                                                    IRecAmt = RecAmt;
                                                    if (TotalDr > Convert.ToDouble(PenAmt))
                                                    {
                                                        TotalDr = TotalDr - Convert.ToDouble(PenAmt);
                                                        IPenAmt = PenAmt;
                                                        if (TotalDr > Convert.ToDouble(IntAmt))
                                                        {
                                                            TotalDr = TotalDr - Convert.ToDouble(IntAmt);
                                                            IIntAmt = IntAmt;
                                                            if (TotalDr > Convert.ToDouble(PriAmt))
                                                            {
                                                                TotalDr = TotalDr - Convert.ToDouble(PriAmt);
                                                            }
                                                            else
                                                            {
                                                                IPriAmt = TotalDr.ToString();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            IIntAmt = TotalDr.ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        IPenAmt = TotalDr.ToString();
                                                    }
                                                }
                                                else
                                                {
                                                    IRecAmt = TotalDr.ToString();
                                                }
                                            }
                                            else
                                            {
                                                INotAmt = TotalDr.ToString();
                                            }
                                        }
                                        else
                                        {
                                            ISerAmt = TotalDr.ToString();
                                        }
                                    }
                                    else
                                    {
                                        ICouAmt = TotalDr.ToString();
                                    }
                                }
                                else
                                {
                                    ISurAmt = TotalDr.ToString();
                                }
                            }
                            else
                            {
                                IOthAmt = TotalDr.ToString();
                            }
                        }
                        else
                        {
                            IBanAmt = TotalDr.ToString();
                        }
                    }
                    else
                    {
                        IInsAmt = TotalDr.ToString();
                    }
                }


                string EntryMid = Encry.GetMK(Session["MID"].ToString());
                string REFERENCEID = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");

                ResStr = CMM.Uni_LoanEntries("OWC_FUNDING", Session["EntryDate"].ToString(), "3", PrdCode.ToString(), Session["BRCD"].ToString(),
                                        Accno.ToString(), AccType, InstruType, OprType, "1", Amount, Parti, "Loan", "32",
                                        "TR-INT", InstruNo, InstruDate, BankCode, BranchCode, "1003", Session["MID"].ToString(), "0", "0", "OC",
                                       "0", "", IPriAmt, IIntAmt, IPenAmt, IRecAmt, INotAmt, ISerAmt, ICouAmt, ISurAmt, IOthAmt, IBanAmt, IInsAmt,
                                        EntryMid, REFERENCEID);


            }
            else
            {
                WebMsgBox.Show("Loan Account Balance Details not found...!", this.Page);
                
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResStr;
    }
}