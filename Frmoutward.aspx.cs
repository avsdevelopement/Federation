using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Frmoutward : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    DataTable DT = new DataTable();
    ClsOutward OW = new ClsOutward();
    ClsAuthorized AT = new ClsAuthorized();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    DateTime Today = DateTime.Now.Date;
    public static string Flag;
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                ViewState["UN_FL"] = Request.QueryString["FLAG"].ToString();
                ddlPayType.Focus();
                BD.BindTransaction(DDltransctntype);
                BD.BindPaymentOut(ddlPayType, "1");
                BindGrid();
                Flag = "1";
                ViewState["Flag"] = "AD";
                BtnSubmit.Text = "Submit";
                TblDiv_MainWindow.Visible = false;
                Div_grid.Visible = true;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Cleardata()
    {
        DDltransctntype.SelectedValue = "0";
        Txtbeneficrycode.Text = "";
        TxtbeneAccno.Text = "";
        TxtInstAmt.Text="";
        Txtbenename.Text = "";
        Txtdraweloc.Text = "";
        Txtprntloc.Text = "";
        Txtbeneadd1.Text = "";
        Txtbeneadd2.Text="";
        Txtbeneadd3.Text="";
        Txtbeneadd4.Text="";
        Txtbeneadd5.Text="";
        Txtinstrefno.Text="";
        Txtcustrefno.Text = "";
        Txtpayd1.Text = "";
        Txtpayd2.Text = "";
        Txtpayd3.Text = "";
        Txtpayd4.Text = "";
        Txtpayd5.Text = "";
        Txtpayd6.Text = "";
        Txtpayd7.Text = "";
        Txtchequeno.Text = "";
        Txtchqtrndate.Text = "";
        Txtmicrno.Text = "";
        Txtifccode.Text = "";
        Txtbenebankname.Text = "";
        Txtbenebranchnme.Text = "";
        Txtifccode.Text = "";
        Txtbeneemail.Text = "";
        Txttransactiontype.Text = "";
        ddlPayType.SelectedValue = "0";
        txtProdType1.Text = "";
        txtProdName1.Text = "";
            TxtAccNo1.Text = "";
            TxtAccName1.Text = "";
                txtBalance.Text = "";
                Txtinstno.Text = "";
                    TxtChequeDate.Text = "";
                    txtNarration.Text = "";
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string Date = Today.ToString("dd-MM-yyyy");
            double BAL,AMT;
            BAL = Convert.ToDouble(txtBalance.Text == "" ? "0" : txtBalance.Text);
            AMT = Convert.ToDouble(TxtInstAmt.Text == "" ? "0" : TxtInstAmt.Text);
            if (AMT <= BAL)
            {
                if (ViewState["Flag"].ToString() == "AD")
                {
                    if (DDltransctntype.SelectedValue =="0")
                    {
                        WebMsgBox.Show("Please select Transaction Type", this.Page);
                        DDltransctntype.Focus();
                        return;
                    }
                    if (TxtInstAmt.Text == "")
                    {

                        WebMsgBox.Show("Please enter Instrument Amount", this.Page);
                        TxtInstAmt.Focus();
                        return;

                    }
                    if (DDltransctntype.SelectedItem.Text == "D" && Txtdraweloc.Text == "")
                    {
                        WebMsgBox.Show("Please enter Drawee Location ", this.Page);
                        Txtdraweloc.Focus();
                        return;
                    }
                    if (DDltransctntype.SelectedItem.Text == "N" && Txtcustrefno.Text == "")
                    {
                        WebMsgBox.Show("Please enter Cust ref No ", this.Page);
                        Txtcustrefno.Focus();
                        return;
                    }
                    if (Txtpayd5.Text == "")
                    {

                        WebMsgBox.Show("Please enter Payment Details 5", this.Page);
                        Txtpayd5.Focus();
                        return;

                    }
                    if (Txtpayd6.Text == "")
                    {

                        WebMsgBox.Show("Please enter Payment Details 6", this.Page);
                        Txtpayd6.Focus();
                        return;

                    }
                    if (Txtpayd7.Text == "")
                    {

                        WebMsgBox.Show("Please enter Payment Details 7", this.Page);
                        Txtpayd7.Focus();
                        return;

                    }
                    if (Txtchqtrndate.Text == "")
                    {
                        WebMsgBox.Show("Please enter Cheque Date", this.Page);
                        Txtchqtrndate.Focus();
                        return;
                    }
                    if (DDltransctntype.SelectedItem.Text == "R"  && Txtifccode.Text == "")
                    {
                        WebMsgBox.Show("Please enter IFC Code", this.Page);
                        Txtifccode.Focus();
                        return;
                    }

                    if ( DDltransctntype.SelectedItem.Text == "N" && Txtifccode.Text == "")
                    {
                        WebMsgBox.Show("Please enter IFC Code", this.Page);
                        Txtifccode.Focus();
                        return;
                    }
                    if (DDltransctntype.SelectedItem.Text == "R" && Txtbenebankname.Text == "")
                    {
                        WebMsgBox.Show("Please enter BeneBankName", this.Page);
                        Txtbenebankname.Focus();
                        return;
                    }
                    if ( DDltransctntype.SelectedItem.Text == "N" && Txtbenebankname.Text == "")
                    {
                        WebMsgBox.Show("Please enter BeneBankName", this.Page);
                        Txtbenebankname.Focus();
                        return;
                    }
                    int Result = OW.Insertdata(DDltransctntype.SelectedItem.Text, Txtbeneficrycode.Text, TxtbeneAccno.Text, TxtInstAmt.Text, Txtbenename.Text, Txtdraweloc.Text, Txtprntloc.Text, Txtbeneadd1.Text,
                        Txtbeneadd2.Text, Txtbeneadd3.Text, Txtbeneadd4.Text, Txtbeneadd5.Text, Txtinstrefno.Text, Txtcustrefno.Text, Txtpayd1.Text, Txtpayd2.Text, Txtpayd3.Text, Txtpayd4.Text, Txtpayd5.Text,
                        Txtpayd6.Text, Txtpayd7.Text, Txtchequeno.Text, Txtchqtrndate.Text, Txtmicrno.Text, Txtifccode.Text, Txtbenebankname.Text, Txtbenebranchnme.Text, Txtbeneemail.Text, "1001", Session["BRCD"].ToString(),
                        ddlPayType.SelectedItem.Text, txtProdType1.Text, TxtAccNo1.Text, Txtinstno.Text, TxtChequeDate.Text, txtNarration.Text, txtBalance.Text, Session["MID"].ToString());
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data Added successfully..!!", this.Page);
                       
                        BindGrid();
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OutwardRTGS_Add _" + TxtbeneAccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        Cleardata();
                        return;
                    }
                }
                else if (ViewState["Flag"].ToString() == "AT")
                {
                    string ST = "";
                    int Result = OW.AuthoriseData(Session["BRCD"].ToString(), TxtbeneAccno.Text, ViewState["Id"].ToString(), Session["MID"].ToString());
                    if (Result > 0)
                    {
                       
                        double CBAl = Convert.ToDouble(txtBalance.Text);
                        double Total = Convert.ToDouble(TxtInstAmt.Text);
                        string CbalT = Convert.ToString(CBAl - Total);
                        ST = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();
                        if (ddlPayType.SelectedValue == "2")
                        {

                            int RM = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), txtProdType1.Text, TxtAccNo1.Text, txtNarration.Text, "RTGS/NFT",
                               CbalT, "1", "7", "TR", ST, Txtinstno.Text, TxtChequeDate.Text, "0", "0", "1001", "0", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "OutwardDetail_TRF", "0", TxtAccName1.Text, "0", "0");
                            if (RM > 0)
                            {
                                RM = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), txtProdType1.Text, TxtbeneAccno.Text, txtNarration.Text, "RTGS/NFT",
                               TxtInstAmt.Text, "2", "7", "TR", ST, Txtinstno.Text, TxtChequeDate.Text, "0", "0", "1001", "0", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "OutwardDetail_TRF", "0", Txtbenename.Text, "0", "0");
                                if (RM > 0)
                                {
                                    WebMsgBox.Show("Data authorised successfully and Voucher Posted  successfully With Set No: " + ST + "!!", this.Page);
                                    GenerateText();
                                    BindGrid();
                                    FL = "Insert";//Dhanya Shetty
                                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OutwardRTGS_Auth_T _" + TxtbeneAccno.Text + "_" + ST + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                    Cleardata();

                                    return;
                                }
                            }
                        }
                        else if (ddlPayType.SelectedValue == "4")
                        {
                            int RM = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), txtProdType1.Text, TxtAccNo1.Text, txtNarration.Text, "RTGS/NFT",
                               CbalT, "1", "7", "TR", ST, Txtinstno.Text, TxtChequeDate.Text, "0", "0", "1001", "0", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "OutwardDetail_Chq", "0", TxtAccName1.Text, "0", "0");
                            if (RM > 0)
                            {
                                RM = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), txtProdType1.Text, TxtbeneAccno.Text, txtNarration.Text, "RTGS/NFT",
                               TxtInstAmt.Text, "2", "7", "TR", ST, Txtinstno.Text, TxtChequeDate.Text, "0", "0", "1001", "0", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "OutwardDetail_Chq", "0", Txtbenename.Text, "0", "0");
                                if (RM > 0)
                                {
                                    WebMsgBox.Show(" Data authorised successfully and Voucher Posted  successfully With Set No: " + ST + "!!", this.Page);
                                    GenerateText();
                                    BindGrid();
                                    FL = "Insert";//Dhanya Shetty
                                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OutwardRTGS_Auth_Cq _" + TxtbeneAccno.Text + "_" + ST + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                    Cleardata();

                                    return;
                                }
                            }
                        }
                    }

                }
                else if (ViewState["Flag"].ToString() == "DL")
                {
                    int Result = OW.DeleteData(Session["BRCD"].ToString(), TxtbeneAccno.Text, ViewState["Id"].ToString(), Session["MID"].ToString());
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data Deleted successfully..!!", this.Page);
                      
                        BindGrid();
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OutwardRTGS_Del _" + TxtbeneAccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        Cleardata();
                        return;
                    }

                }
            }
            else
            {
                WebMsgBox.Show("Amount is greater than balance!", this.Page);
                TxtInstAmt.Text = "";
                TxtInstAmt.Focus();
            }
        }
       
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btnclear_Click(object sender, EventArgs e)
    {
        Cleardata();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
  
    
    protected void DDltransctntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Txttransactiontype.Text = BD.GetTransactnType(DDltransctntype.SelectedItem.Text.ToString());
            Txtbeneficrycode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void CallEdit()
    {
        try
        {
            DT = OW.GetInfo(Session["BRCD"].ToString(), ViewState["BeneficiaryAccNo"].ToString(), ViewState["Id"].ToString());
                if (DT.Rows.Count > 0)
                {
                    DDltransctntype.SelectedItem.Text = string.IsNullOrEmpty(DT.Rows[0]["TranscationType"].ToString()) ? "0" : DT.Rows[0]["TranscationType"].ToString();
                    Txtbeneficrycode.Text = DT.Rows[0]["Beneficiarycode"].ToString();
                    Txttransactiontype.Text =  BD.GetTransactnType(DT.Rows[0]["TranscationType"].ToString()); 
                    TxtbeneAccno.Text = DT.Rows[0]["BeneficiaryAccNo"].ToString();
                    TxtInstAmt.Text = DT.Rows[0]["InstrumentAmount"].ToString();
                    Txtbenename.Text = DT.Rows[0]["BeneficiaryName"].ToString();
                    Txtdraweloc.Text = DT.Rows[0]["DraweeLoc"].ToString();
                    Txtprntloc.Text = DT.Rows[0]["PrintLoc"].ToString();
                    Txtbeneadd1.Text = DT.Rows[0]["BeneAdd1"].ToString();
                    Txtbeneadd2.Text = DT.Rows[0]["BeneAdd2"].ToString();
                    Txtbeneadd3.Text = DT.Rows[0]["BeneAdd3"].ToString();
                    Txtbeneadd4.Text = DT.Rows[0]["BeneAdd4"].ToString();
                    Txtbeneadd5.Text = DT.Rows[0]["BeneAdd5"].ToString();
                    Txtinstrefno.Text = DT.Rows[0]["InstructionRefNo"].ToString();
                    Txtcustrefno.Text = DT.Rows[0]["CustRefNo"].ToString();
                    Txtpayd1.Text = DT.Rows[0]["Payment1"].ToString();
                    Txtpayd2.Text = DT.Rows[0]["Payment2"].ToString();
                    Txtpayd3.Text = DT.Rows[0]["Payment3"].ToString();
                    Txtpayd4.Text = DT.Rows[0]["Payment4"].ToString();
                    Txtpayd5.Text = DT.Rows[0]["Payment5"].ToString();
                    Txtpayd6.Text = DT.Rows[0]["Payment6"].ToString();
                    Txtpayd7.Text = DT.Rows[0]["Payment7"].ToString();
                    Txtchequeno.Text = DT.Rows[0]["ChequeNo"].ToString();
                    Txtchqtrndate.Text = DT.Rows[0]["ChqDate"].ToString().Replace("12:00:00 AM", "");
                    Txtmicrno.Text = DT.Rows[0]["MICRNo"].ToString();
                    Txtifccode.Text = DT.Rows[0]["IFCcode"].ToString();
                    Txtbenebankname.Text = DT.Rows[0]["BeneBankName"].ToString();
                    Txtbenebranchnme.Text = DT.Rows[0]["BenebranchName"].ToString();
                  
                    Txtbeneemail.Text = DT.Rows[0]["BeneEmailId"].ToString();
                    BD.BindPayment(ddlPayType, "1");
                    ddlPayType.SelectedValue = string.IsNullOrEmpty(DT.Rows[0]["PaymentType"].ToString()) ? "0" : DT.Rows[0]["PaymentType"].ToString() == "CHEQUE" ? "4" : DT.Rows[0]["PaymentType"].ToString() == "CASH" ? "1" : DT.Rows[0]["PaymentType"].ToString() == "TRANSFER" ? "2":"0";
                    Paymenttype();
                Txtinstno.Text = DT.Rows[0]["InstrumentNo"].ToString();
                    TxtChequeDate.Text = DT.Rows[0]["InstDate"].ToString().Replace("12:00:00 AM", ""); ;
                    txtNarration.Text = DT.Rows[0]["Narration"].ToString();
                    txtProdType1.Text = DT.Rows[0]["Productcode"].ToString();
                     string AC1;
                      AC1 = OW.Getaccno(txtProdType1.Text, Session["BRCD"].ToString());

                    if (AC1 != null)
                    {
                        string[] AC = AC1.Split('_'); ;
                        ViewState["GLCODE1"] = AC[0].ToString();
                        txtProdName1.Text = AC[1].ToString();
                    }
                        TxtAccNo1.Text = DT.Rows[0]["AccountNo"].ToString();
                     DT = OW.GetCustName(txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString());
                     if (DT.Rows.Count > 0)
                     {
                         string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                         TxtAccName1.Text = CustName[0].ToString();
                         ViewState["GLCODE"] = CustName[2].ToString();
                     }
                     txtBalance.Text = OW.GetOpenClose(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
       
                }
            }
     
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Enable(bool TF)
    {
        DDltransctntype.Enabled = TF;
        Txtbeneficrycode.Enabled = TF;
        TxtbeneAccno.Enabled = TF;
        TxtInstAmt.Enabled = TF;
        Txtbenename.Enabled = TF;
        Txtdraweloc.Enabled = TF;
        Txtprntloc.Enabled = TF;
        Txtbeneadd1.Enabled = TF;
        Txtbeneadd2.Enabled = TF;
        Txtbeneadd3.Enabled = TF;
        Txtbeneadd4.Enabled = TF;
        Txtbeneadd5.Enabled = TF;
        Txtinstrefno.Enabled = TF;
        Txtcustrefno.Enabled = TF;
        Txtpayd1.Enabled = TF;
        Txtpayd2.Enabled = TF;
        Txtpayd3.Enabled = TF;
        Txtpayd4.Enabled = TF;
        Txtpayd5.Enabled = TF;
        Txtpayd6.Enabled = TF;
        Txtpayd7.Enabled = TF;
        Txtchequeno.Enabled = TF;
        Txtchqtrndate.Enabled = TF;
        Txtmicrno.Enabled = TF;
        Txtifccode.Enabled = TF;
        Txtbenebankname.Enabled = TF;
        Txtbenebranchnme.Enabled = TF;
        Txtifccode.Enabled = TF;
        Txtbeneemail.Enabled = TF;
        ddlPayType.Enabled = TF;
        txtProdType1.Enabled = TF;
        txtProdName1.Enabled = TF;
        TxtAccNo1.Enabled = TF;
        TxtAccName1.Enabled = TF;
        txtBalance.Enabled = TF;
        Txtinstno.Enabled = TF;
        TxtChequeDate.Enabled = TF;
        txtNarration.Enabled = TF;
    }
    protected void TxtbeneAccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtInstAmt.Focus();
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void GenerateText()//Code to generate text file//
    {
        DateTime Today = DateTime.Now.Date;
        string Date = Today.ToString("ddMM");
        string Date1 = Today.ToString("yyyy-MM-dd");
       string MPath = @"C:\AVS\" + Date1;
       string[] filePaths = Directory.GetFiles(@"C:\AVS\ ");
       string FileName = "";
       //string SaveLocation = Server.MapPath(@"C:\AVS\ ");
       int acs = 0 ;
       foreach (string path in filePaths) //iterate the file list
       {
           FileInfo file = new FileInfo(path); //get individual file info
           FileName = Path.GetFileName(file.FullName);  //output individual file name
          
           string[] acc = FileName.Split('.');
           if (acc[0].ToString() == "SPURBAN_HRP01RBI_HRP01RBI" + Date)
           {
               if (acs < Convert.ToInt32(acc[1]))
               {
                   acs = Convert.ToInt32(acc[1].ToString());
               }
           }
       }
       if (acs == 0)
       {
           FileName = "SPURBAN_HRP01RBI_HRP01RBI" + Date + ".001.txt";
       }
       else
       {
           acs = acs + 1;
           if(acs<10)
               FileName = "SPURBAN_HRP01RBI_HRP01RBI" + Date + ".00" + acs + ".txt";
               else
           FileName = "SPURBAN_HRP01RBI_HRP01RBI" + Date + ".0"+acs+".txt";
       }

       string FilePath = @"C:\AVS\" +FileName;
        //if (!Directory.Exists(MPath))
        //    Directory.CreateDirectory(MPath);
        //if (File.Exists(FilePath))
        //{
        //    File.Delete(FilePath);
        //}
        try
        {

            DT = OW.FetchTextREcords(ViewState["Id"].ToString(), TxtbeneAccno.Text);
          for (int K = 0; K <= DT.Rows.Count - 1; K++)
            {
               
                string Message="";
                Message += DT.Rows[K][0].ToString() + ","+DT.Rows[K][1].ToString() + "," + DT.Rows[K][2].ToString() + "," + DT.Rows[K][3].ToString() + "," + DT.Rows[K][4].ToString() + "," + DT.Rows[K][5].ToString() + "," + DT.Rows[K][6].ToString() + "," +
                     DT.Rows[K][7].ToString() + "," + DT.Rows[K][8].ToString() + "," + DT.Rows[K][9].ToString() + "," + DT.Rows[K][10].ToString() + "," + DT.Rows[K][11].ToString() + "," + DT.Rows[K][12].ToString()+ "," +
                     DT.Rows[K][13].ToString() + "," +DT.Rows[K][14].ToString() + "," +DT.Rows[K][15].ToString() + "," +DT.Rows[K][16].ToString() + "," +DT.Rows[K][17].ToString() + "," +DT.Rows[K][18].ToString() + "," +
                     DT.Rows[K][19].ToString() + "," +DT.Rows[K][20].ToString() + "," +DT.Rows[K][21].ToString() + "," +DT.Rows[K][22].ToString() + "," +DT.Rows[K][23].ToString() + "," +
                     DT.Rows[K][24].ToString() + "," +DT.Rows[K][25].ToString() + "," +DT.Rows[K][26].ToString() + "," +DT.Rows[K][27].ToString() ;
                 using (StreamWriter writer = new StreamWriter(FilePath, true))
                {
                    writer.WriteLine(Message);
                    writer.Close();
                 }
            }
            int Res = OW.UploadStatus(Session["BRCD"].ToString(), TxtbeneAccno.Text);
            if (Res > 0)
            {
                WebMsgBox.Show("Text file generated successfully!!!",this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OutwardRTGS_Txtfile _" + TxtbeneAccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Paymenttype();
    }

    public void Paymenttype()
    {
        try
        {
            if (ddlPayType.SelectedValue.ToString() == "0")
        {
            Transfer.Visible = false;
            Transfer1.Visible = false;
            DivAmount.Visible = false;
        }
        else if (ddlPayType.SelectedValue.ToString() == "1")
        {
            ViewState["PAYTYPE"] = "CASH";
            Transfer.Visible = false;
            Transfer1.Visible = false;
            DivAmount.Visible = true;
            txtNarration.Text = "By Cash";
            Clear();
            BtnSubmit.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "2")
        {
            ViewState["PAYTYPE"] = "TRANSFER";
            Transfer.Visible = true;
            Transfer1.Visible = false;
            DivAmount.Visible = true;
            txtNarration.Text = "By TRF";
            autoglname1.ContextKey = Session["BRCD"].ToString();
           Clear();
            txtProdType1.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "4")
        {
            ViewState["PAYTYPE"] = "CHEQUE";
            Transfer.Visible = true;
            Transfer1.Visible = true;
            DivAmount.Visible = true;
            txtNarration.Text = "By TRF";
            autoglname1.ContextKey = Session["BRCD"].ToString();
            Clear();
            txtProdType1.Focus();
        }
        else
        {
            Clear();
            Transfer.Visible = false;
            Transfer1.Visible = false;
        }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void txtProdType1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = OW.Getaccno(txtProdType1.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                ViewState["GLCODE1"] = AC[0].ToString();
                txtProdName1.Text = AC[1].ToString();
                AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text + "_" + ViewState["GLCODE1"].ToString();

                if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) >= 100)
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";

                    TxtAccNo1.Text = txtProdType1.Text.ToString();
                    TxtAccName1.Text = txtProdName1.Text.ToString();

                    txtBalance.Text = OW.GetOpenClose(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                    Txtinstno.Focus();
                }
                else
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    txtBalance.Text = "";

                    TxtAccNo1.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                txtProdType1.Text = "";
                txtProdName1.Text = "";
                TxtAccNo1.Text = "";
                TxtAccName1.Text = "";
                txtBalance.Text = "";
                txtProdType1.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtProdName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtProdName1.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtProdName1.Text = custnob[0].ToString();
                txtProdType1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                string[] AC = OW.Getaccno(txtProdType1.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GLCODE1"] = AC[0].ToString();
                AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text + "_" + ViewState["GLCODE1"].ToString();

                if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) >= 100)
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";

                    TxtAccNo1.Text = txtProdType1.Text.ToString();
                    TxtAccName1.Text = txtProdName1.Text.ToString();

                    txtBalance.Text = OW.GetOpenClose(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                    Txtinstno.Focus();
                }
                else
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    txtBalance.Text = "";

                    TxtAccNo1.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAccNo1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            AT = BD.Getstage1(TxtAccNo1.Text, Session["BRCD"].ToString(), txtProdType1.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise...!!";
                    ModalPopup.Show(this.Page);
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    TxtAccNo1.Focus();
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = OW.GetCustName(txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        TxtAccName1.Text = CustName[0].ToString();
                        ViewState["GLCODE"] = CustName[2].ToString();
                        txtBalance.Text = OW.GetOpenClose(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                       Txtinstno.Focus();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo1.Text = "";
                TxtAccName1.Text = "";
                TxtAccNo1.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAccName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtAccName1.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtAccName1.Text = custnob[0].ToString();
                TxtAccNo1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
        txtBalance.Text = OW.GetOpenClose(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
               Txtinstno.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Clear()
    {
        try
        {
            txtProdType1.Text = "";
            txtProdName1.Text = "";                                                                    
            TxtAccNo1.Text = "";
            TxtAccName1.Text = "";
            txtBalance.Text = "";
            Txtinstno.Text = "";
            TxtChequeDate.Text = "";
            TxtChequeDate.Text = Session["EntryDate"].ToString();
            txtProdType1.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtProdType1.Focus();
        }

    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            ddlPayType.Focus();
            lblstatus.Text = "New Entry";
            ViewState["Status"] = "new";
            ViewState["Flag"] = "AD";
             BtnSubmit.Visible = true;
            Flag = "1";
            TblDiv_MainWindow.Visible = true;
            Div_grid.Visible = true;
            Cleardata();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
   
    protected void LnkAutorise_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Id"] = ARR[0].ToString();
            ViewState["BeneficiaryAccNo"] = ARR[1].ToString();
            ViewState["Flag"] = "AT";
            BtnSubmit.Text = "Authorise";
            TblDiv_MainWindow.Visible = true;
            CallEdit();
            
            Enable(false);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
       }
    }

    protected void grdoutward_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdoutward.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void grdoutward_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdoutward_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    public void BindGrid()
    {
        int RS = OW.BindGrid(grdoutward,Session["BRCD"].ToString());
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
      
            try
            {
                LinkButton lnkedit = (LinkButton)sender;
                string str = lnkedit.CommandArgument.ToString();
                string[] ARR = str.Split(',');
                ViewState["Id"] = ARR[0].ToString();
                ViewState["BeneficiaryAccNo"] = ARR[1].ToString();
                ViewState["Flag"] = "DL";
                BtnSubmit.Text = "Delete";
                TblDiv_MainWindow.Visible = true;
                CallEdit();

                Enable(false);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }

    }
    protected void Txtbeneficrycode_TextChanged(object sender, EventArgs e)
    {
        TxtbeneAccno.Focus();
    }
    protected void TxtInstAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double BAL, AMT;
            BAL = Convert.ToDouble(txtBalance.Text == "" ? "0" : txtBalance.Text);
            AMT = Convert.ToDouble(TxtInstAmt.Text == "" ? "0" : TxtInstAmt.Text);
            if (AMT >= BAL)
            {
                WebMsgBox.Show("Amount is greater than balance!", this.Page);
                TxtInstAmt.Text = "";
                TxtInstAmt.Focus();
            }
            else
            {
                Txtbenename.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
       
    }
    protected void Txtbenename_TextChanged(object sender, EventArgs e)
    {
        Txtdraweloc.Focus();
    }
    protected void Txtdraweloc_TextChanged(object sender, EventArgs e)
    {
        Txtprntloc.Focus();
    }
    protected void Txtprntloc_TextChanged(object sender, EventArgs e)
    {
        Txtbeneadd1.Focus();
    }
    protected void Txtbeneadd1_TextChanged(object sender, EventArgs e)
    {
        Txtbeneadd2.Focus();
    }
    protected void Txtbeneadd2_TextChanged(object sender, EventArgs e)
    {
        Txtbeneadd3.Focus();
    }
    protected void Txtbeneadd3_TextChanged(object sender, EventArgs e)
    {
        Txtbeneadd4.Focus();
    }
    protected void Txtbeneadd4_TextChanged(object sender, EventArgs e)
    {
        Txtbeneadd5.Focus();
    }
    protected void Txtbeneadd5_TextChanged(object sender, EventArgs e)
    {
        Txtinstrefno.Focus();
    }
    protected void Txtinstrefno_TextChanged(object sender, EventArgs e)
    {

        Txtcustrefno.Focus();
    }
    protected void Txtcustrefno_TextChanged(object sender, EventArgs e)
    {
        Txtpayd1.Focus();
    }
    protected void Txtpayd1_TextChanged(object sender, EventArgs e)
    {
        Txtpayd2.Focus();
    }
    protected void Txtpayd2_TextChanged(object sender, EventArgs e)
    {
        Txtpayd3.Focus();
    }
    protected void Txtpayd3_TextChanged(object sender, EventArgs e)
    {
        Txtpayd4.Focus();
    }
    protected void Txtpayd4_TextChanged(object sender, EventArgs e)
    {
        Txtpayd5.Focus();
    }
    protected void Txtpayd5_TextChanged(object sender, EventArgs e)
    {
        Txtpayd6.Focus();
    }
    protected void Txtpayd6_TextChanged(object sender, EventArgs e)
    {
        Txtpayd7.Focus();
    }
    protected void Txtpayd7_TextChanged(object sender, EventArgs e)
    {
        Txtchequeno.Focus();
    }
    protected void Txtchequeno_TextChanged(object sender, EventArgs e)
    {
        Txtchqtrndate.Focus();
    }
    protected void Txtchqtrndate_TextChanged(object sender, EventArgs e)
    {
        Txtmicrno.Focus();
    }
    protected void Txtmicrno_TextChanged(object sender, EventArgs e)
    {
        Txtifccode.Focus();
    }
    protected void Txtifccode_TextChanged(object sender, EventArgs e)
    {
        Txtbenebankname.Focus();
    }
    protected void Txtbenebankname_TextChanged(object sender, EventArgs e)
    {
        Txtbenebranchnme.Focus();
    }
    protected void Txtbenebranchnme_TextChanged(object sender, EventArgs e)
    {
        Txtbeneemail.Focus();
    }
    protected void Txtbeneemail_TextChanged(object sender, EventArgs e)
    {
        BtnSubmit.Focus();
    }
}