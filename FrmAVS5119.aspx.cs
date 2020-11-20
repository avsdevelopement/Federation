using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5119 : System.Web.UI.Page
{
    ClsMultiTransaction MT = new ClsMultiTransaction();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS5119 FU = new ClsAVS5119();
    DataTable DT = new DataTable();
    string RefNumber = "", SetNo = "";
    double TotalDrAmt = 0;
    int Result = 0;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                FileUpload1.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click Event

    protected void btnTrail_Click(object sender, EventArgs e)
    {
        try
        {
            string FilePath = "", ConStr = "";

            if (FileUpload1.HasFile)
            {
                FilePath = Server.MapPath("~/Document/" + FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(FilePath);

                ImportDataFromExcel(FilePath, Extension, ConStr, "Yes");

                //  Bind Grid
                BindGrid();
            }
            else
            {
                WebMsgBox.Show("Plz Select the excel file first...!!", this.Page);
                FileUpload1.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            DT = new DataTable();
            DT = FU.GetTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                RefNumber = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                ViewState["RID"] = (Convert.ToInt32(RefNumber) + 1).ToString();
                SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                if (Convert.ToDouble(SetNo.ToString()) > 0)
                {
                    //  Insert All Transaction Into M-Table
                    Result = FU.Authorized(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), SetNo.ToString(), ViewState["RID"].ToString(), "File Uploading");

                    //  Insert one by one transaction into avs_LnTrx Table
                    if (Result > 0)
                    {
                        DT = new DataTable();
                        DT = FU.GetLoanTrans(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            Result = FU.LoanTrx(DT.Rows[i]["BrCd"].ToString(), DT.Rows[i]["LoanGlCode"].ToString(), DT.Rows[i]["SubGlCode"].ToString(), DT.Rows[i]["AccountNo"].ToString(), DT.Rows[i]["HeadDesc"].ToString(), DT.Rows[i]["TrxType"].ToString(),
                                DT.Rows[i]["Activity"].ToString(), DT.Rows[i]["ScrollNo"].ToString(), DT.Rows[i]["Narration"].ToString(), DT.Rows[i]["Amount"].ToString(), SetNo, "1003", DT.Rows[i]["MID"].ToString(), "0", DT.Rows[i]["EntryDate"].ToString(), ViewState["RID"].ToString());

                            if (Result > 0 && DT.Rows[i]["HeadDesc"].ToString() == "2" && DT.Rows[i]["TrxType"].ToString() == "2")
                            {
                                string IntApp = FU.GetIntApp(DT.Rows[i]["BrCd"].ToString(), DT.Rows[i]["LoanGlCode"].ToString());
                                if (Convert.ToDouble(IntApp.ToString()) == 1)
                                {
                                    Result = FU.UpdateLastIntDate(DT.Rows[i]["BrCd"].ToString(), DT.Rows[i]["LoanGlCode"].ToString(), DT.Rows[i]["AccountNo"].ToString(), DT.Rows[i]["EntryDate"].ToString(), DT.Rows[i]["MID"].ToString());
                                }
                            }
                        }

                        ClearAllData();
                        WebMsgBox.Show("File uploaded successfully with setno : " + Convert.ToInt32(SetNo), this.Page);
                        return;
                    }
                }
            }
            else
            {
                FileUpload1.Focus();
                WebMsgBox.Show("Please execute trail first...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            //Delete All Data From Temporary Table Here
            FU.RemoveData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
            return;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Functions

    protected void ClearAllData()
    {
        try
        {
            txtParti.Text = "";
            grdTrans.DataSource = null;
            grdTrans.DataBind();

            FileUpload1.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BindGrid()
    {
        try
        {
            DT = new DataTable();
            DT = FU.BindGridTrans(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                grdTrans.DataSource = DT;
                grdTrans.DataBind();

                //Calculate Sum and display in Footer Row
                decimal DebitTotal = DT.AsEnumerable().Sum(row => row.Field<decimal>("Debit"));
                decimal CreditTotal = DT.AsEnumerable().Sum(row => row.Field<decimal>("Credit"));
                grdTrans.FooterRow.Cells[0].Text = "Total";
                grdTrans.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                grdTrans.FooterRow.Cells[7].Text = DebitTotal.ToString("N2");
                grdTrans.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                grdTrans.FooterRow.Cells[8].Text = CreditTotal.ToString("N2");
                grdTrans.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            }
            else
            {
                grdTrans.DataSource = null;
                grdTrans.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ImportDataFromExcel(string FilePath, string Extension, string ConStr, string isHDR)
    {
        string TrxType = "", GlCode = "";
        DT = new DataTable();

        try
        {
            switch (Extension.ToLower())
            {
                case ".xls": //Excel 97-03
                    ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                    break;

                case ".xlsx": //Excel 07
                    ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    break;
            }

            ConStr = String.Format(ConStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(ConStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            cmdExcel.Connection = connExcel;
            connExcel.Open();
            cmdExcel.CommandText = "Select * From [Sheet1$]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(DT);
            connExcel.Close();

            if (DT.Rows.Count > 0)
            {
                if (txtParti.Text.ToString() == "")
                    txtParti.Text = "By System";

                //  Remover temporary data from table userwise
                FU.RemoveData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    GlCode = FU.GetGlCode(DT.Rows[i][0].ToString(), DT.Rows[i][1].ToString());
                    if (DT.Rows[i][5].ToString().ToUpper() == "CR")
                        TrxType = "1";
                    else
                        TrxType = "2";

                    if (GlCode.ToString() == "3" && TrxType.ToString() == "1")
                    {
                        //Added for loan case
                        Result = 1;
                        TotalDrAmt = Convert.ToDouble(DT.Rows[i][3].ToString());
                        DataTable LoanDT = new DataTable();
                        LoanDT = MT.GetLoanTotalAmount(DT.Rows[i][0].ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), Session["EntryDate"].ToString());

                        if (LoanDT.Rows[0]["IntCalType"].ToString() == "1" || LoanDT.Rows[0]["IntCalType"].ToString() == "2")
                        {
                            //  For Interest Receivable
                            if (Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()))
                            {
                                if (LoanDT.Rows[0]["IntCalType"].ToString() == "1")
                                {
                                    // Interest Received credit to GL 11
                                    Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), LoanDT.Rows[0]["InterestRecGl"].ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        DT.Rows[i][3].ToString(), "1", "7", "TR", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                    if (Result > 0)
                                    {
                                        if (Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) > 0)
                                        {
                                            // Interest Received Amt Credit To 4 In AVS_LnTrx
                                            Result = FU.InsertLoanTrx(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][1].ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(), DT.Rows[i][2].ToString(), "4", "1", "7", "Interest Received Credit", DT.Rows[i][3].ToString(), Session["MID"].ToString());
                                        }
                                    }
                                }
                                else if (LoanDT.Rows[0]["IntCalType"].ToString() == "2")
                                {
                                    // Interest Received credit to GL 3
                                    Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), GlCode.ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        DT.Rows[i][3].ToString(), "1", "7", "TR", "0", "01/01/1900", "1003", Session["MID"].ToString());
                                }
                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()));
                            }
                            else if (Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt > 0)
                            {
                                if (LoanDT.Rows[0]["IntCalType"].ToString() == "1")
                                {
                                    // Interest Received credit to GL 11
                                    Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), LoanDT.Rows[0]["InterestRecGl"].ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        TotalDrAmt.ToString(), "1", "7", "TR", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                    if (Result > 0)
                                    {
                                        if (Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) > 0)
                                        {
                                            // Interest Received Amt Credit To 4 In AVS_LnTrx
                                            Result = FU.InsertLoanTrx(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][1].ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(), DT.Rows[i][2].ToString(), "4", "1", "7", "Interest Received Credit", TotalDrAmt.ToString(), Session["MID"].ToString());
                                        }
                                    }
                                }
                                else if (LoanDT.Rows[0]["IntCalType"].ToString() == "2")
                                {
                                    // Interest Received credit to GL 3
                                    Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), GlCode.ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        TotalDrAmt.ToString(), "1", "7", "TR", "0", "01/01/1900", "1003", Session["MID"].ToString());
                                }
                                TotalDrAmt = 0;
                            }

                            //  For Interest
                            if (LoanDT.Rows[0]["IntCalType"].ToString() == "1")
                            {
                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())))
                                {
                                    //interest Credit to GL 11
                                    Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), LoanDT.Rows[0]["InterestGl"].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())).ToString(), "1", "7", "TR", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                    if (Result > 0)
                                    {
                                        //Current Interest Debit To 2 In AVS_LnTrx
                                        Result = FU.InsertLoanTrx(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][1].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), "2", "2", "7", "Interest Debit", LoanDT.Rows[0]["CurrInterest"].ToString(), Session["MID"].ToString());
                                    }

                                    if (Result > 0)
                                    {
                                        //Current Interest Credit To 2 In AVS_LnTrx
                                        Result = FU.InsertLoanTrx(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][1].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())).ToString(), Session["MID"].ToString());
                                    }

                                    if (Result > 0)
                                    {
                                        //interest Applied Contra To GL 11
                                        Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), LoanDT.Rows[0]["InterestGl"].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                            Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())).ToString(), "2", "11", "TR_INT", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                        if (Result > 0)
                                        {
                                            //interest Applied Credit to GL 100
                                            Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), "100", LoanDT.Rows[0]["PlAccNo1"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                                Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())).ToString(), "1", "11", "TR_INT", "0", "01/01/1900", "1003", Session["MID"].ToString());
                                        }
                                    }

                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())));
                                }
                                else if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                {
                                    //interest Credit to GL 11
                                    Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), LoanDT.Rows[0]["InterestGl"].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        TotalDrAmt.ToString(), "1", "7", "TR", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                    if (Result > 0)
                                    {
                                        //Current Interest Debit To 2 In AVS_LnTrx
                                        Result = FU.InsertLoanTrx(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][1].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), "2", "2", "7", "Interest Debit", LoanDT.Rows[0]["CurrInterest"].ToString(), Session["MID"].ToString());
                                    }

                                    if (Result > 0)
                                    {
                                        //Current Interest Credit To 2 In AVS_LnTrx
                                        Result = FU.InsertLoanTrx(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][1].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), "2", "1", "7", "Interest Credit", TotalDrAmt.ToString(), Session["MID"].ToString());
                                    }

                                    if (Result > 0)
                                    {
                                        //interest Applied Contra To GL 11
                                        Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), LoanDT.Rows[0]["InterestGl"].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                            TotalDrAmt.ToString(), "2", "11", "TR_INT", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                        if (Result > 0)
                                        {
                                            //interest Applied Credit to GL 100
                                            Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), "100", LoanDT.Rows[0]["PlAccNo1"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                                TotalDrAmt.ToString(), "1", "11", "TR_INT", "0", "01/01/1900", "1003", Session["MID"].ToString());
                                        }
                                    }

                                    TotalDrAmt = 0;
                                }
                            }
                            else if (LoanDT.Rows[0]["IntCalType"].ToString() == "2")
                            {
                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())))
                                {
                                    //interest Received Credit to GL 3
                                    Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), GlCode.ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())).ToString(), TrxType, "7", "TR", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                    //  Added As Per ambika mam Instruction 22-06-2017
                                    if (Result > 0)
                                    {
                                        //interest Applied Debit To GL 3
                                        Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), GlCode.ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                            Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())).ToString(), "2", "11", "TR_INT", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                        if (Result > 0)
                                        {
                                            //interest Applied Credit to GL 100
                                            Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), "100", LoanDT.Rows[0]["PlAccNo1"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                            Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())).ToString(), "1", "11", "TR_INT", "0", "01/01/1900", "1003", Session["MID"].ToString());
                                        }
                                    }
                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())));
                                }
                                else if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                {
                                    //interest Received Credit to GL 3
                                    Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), GlCode.ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        TotalDrAmt.ToString(), "1", "7", "TR", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                    //  Added As Per ambika mam Instruction 22-06-2017
                                    if (Result > 0)
                                    {
                                        //interest Applied Debit To GL 3
                                        Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), GlCode.ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                            TotalDrAmt.ToString(), "2", "11", "TR_INT", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                        if (Result > 0)
                                        {
                                            //interest Applied Credit to GL 100
                                            Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), "100", LoanDT.Rows[0]["PlAccNo1"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                                TotalDrAmt.ToString(), "1", "11", "TR_INT", "0", "01/01/1900", "1003", Session["MID"].ToString());
                                        }
                                    }
                                    TotalDrAmt = 0;
                                }
                            }

                            //Principle O/S Credit To Specific GL (e.g 3)
                            if (Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString()))
                            {
                                Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), GlCode.ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        LoanDT.Rows[0]["Principle"].ToString(), "1", "7", "TR", "0", "01/01/1900", "1003", Session["MID"].ToString());
                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString()));
                            }
                            else if (Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt > 0)
                            {
                                Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), GlCode.ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        TotalDrAmt.ToString(), "1", "7", "TR", "0", "01/01/1900", "1003", Session["MID"].ToString());
                                TotalDrAmt = 0;
                            }
                        }
                        else if (LoanDT.Rows[0]["IntCalType"].ToString() == "3")
                        {
                            //  Credit to principle (E.G Glcode is '3')
                            Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), GlCode.ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        TotalDrAmt.ToString(), "1", "7", "TR", "0", "01/01/1900", "1003", Session["MID"].ToString());

                            if (Result > 0)
                            {
                                if (TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["InterestRec"].ToString()) +
                                    Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) +
                                    Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString()))))
                                    TotalDrAmt = Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["InterestRec"].ToString()) +
                                         Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) +
                                         Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())));

                                //  Debit to principle (E.G Glcode is '3')
                                Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), GlCode.ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        TotalDrAmt.ToString(), "2", "7", "TR_INT", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                //  For Interest Receivable
                                if (Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()))
                                {
                                    // Interest Received credit to GL 11
                                    Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), LoanDT.Rows[0]["InterestRecGl"].ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        DT.Rows[i][3].ToString(), "1", "7", "TR_INT", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                    if (Result > 0)
                                    {
                                        if (Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) > 0)
                                        {
                                            // Interest Received Amt Credit To 4 In AVS_LnTrx
                                            Result = FU.InsertLoanTrx(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][1].ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(), DT.Rows[i][2].ToString(), "4", "1", "7", "Interest Received Credit", DT.Rows[i][3].ToString(), Session["MID"].ToString());
                                        }
                                    }
                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()));
                                }
                                else if (Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt > 0)
                                {
                                    // Interest Received credit to GL 11
                                    Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), LoanDT.Rows[0]["InterestRecGl"].ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        TotalDrAmt.ToString(), "1", "7", "TR_INT", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                    if (Result > 0)
                                    {
                                        // Interest Received Amt Credit To 4 In AVS_LnTrx
                                        Result = FU.InsertLoanTrx(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][1].ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(), DT.Rows[i][2].ToString(), "4", "1", "7", "Interest Received Credit", TotalDrAmt.ToString(), Session["MID"].ToString());
                                    }
                                    TotalDrAmt = 0;
                                }

                                //  For Interest
                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())))
                                {
                                    //interest Credit to GL 11
                                    Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), LoanDT.Rows[0]["InterestGl"].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())).ToString(), "1", "7", "TR_INT", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                    if (Result > 0 && Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString()) > 0)
                                    {
                                        //Current Interest Debit To 2 In AVS_LnTrx
                                        Result = FU.InsertLoanTrx(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][1].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), "2", "2", "7", "Interest Debit", LoanDT.Rows[0]["CurrInterest"].ToString(), Session["MID"].ToString());
                                    }

                                    if (Result > 0)
                                    {
                                        //Current Interest Credit To 2 In AVS_LnTrx
                                        Result = FU.InsertLoanTrx(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][1].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())).ToString(), Session["MID"].ToString());
                                    }

                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())));
                                }
                                else if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                {
                                    //interest Credit to GL 11
                                    Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), LoanDT.Rows[0]["InterestGl"].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                                        TotalDrAmt.ToString(), "1", "7", "TR_INT", "0", "01/01/1900", "1003", Session["MID"].ToString());

                                    if (Result > 0 && Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString()) > 0)
                                    {
                                        //Current Interest Debit To 2 In AVS_LnTrx
                                        Result = FU.InsertLoanTrx(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][1].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), "2", "2", "7", "Interest Debit", LoanDT.Rows[0]["CurrInterest"].ToString(), Session["MID"].ToString());
                                    }

                                    if (Result > 0)
                                    {
                                        //Current Interest Credit To 2 In AVS_LnTrx
                                        Result = FU.InsertLoanTrx(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][1].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), DT.Rows[i][2].ToString(), "2", "1", "7", "Interest Credit", TotalDrAmt.ToString(), Session["MID"].ToString());
                                    }

                                    TotalDrAmt = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        Result = FU.InsertData(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i][0].ToString(), DT.Rows[i][4].ToString(), GlCode, DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), txtParti.Text.ToString(),
                            DT.Rows[i][3].ToString(), TrxType, "7", "TR", "0", "01/01/1900", "1003", Session["MID"].ToString());
                    }
                }
            }
            else
            {
                WebMsgBox.Show("Rows not exists in excel file...!!", this.Page);
                FileUpload1.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}