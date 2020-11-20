using System;
using System.Data;
using System.IO;
using System.Text;

public partial class FrmSmsModule : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsOpenClose OC = new ClsOpenClose();
    ClsCommon cmn = new ClsCommon();
    ClsBindDropdown BD = new ClsBindDropdown();
    SmsModule SMS = new SmsModule();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    DateTime Today = DateTime.Now.Date;
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    DataTable GTTable = new DataTable();

    Mobile_Service MS = new Mobile_Service();
    StringBuilder sb = new StringBuilder();

    string FL = "";
    string prdType = "", custType = "";
    string Accno = "", MobileNo = "", Message = "", Time = "", BRCD = "";
    private int SmsResult = 0;


    private Double Balance = 0.0F;

    protected void Page_Load(object sender, EventArgs e)
    {
        String hour = DateTime.Now.ToString("HH");
        if (Convert.ToInt32(hour) > 12 && Convert.ToInt32(hour) <= 24)
        {
            hour = Convert.ToString(Convert.ToInt32(hour) - 12);
        }
        String min = DateTime.Now.ToString("mm");


        try
        {
            if (!IsPostBack)
            {

                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }

                txtFromCustNo.Visible = false;
                txtFromCustName.Visible = false;
                txtToCustNo.Visible = false;
                txtToCustName.Visible = false;

                txtMin.Text = Convert.ToString(Convert.ToUInt32(min) + 2);

                txtHr.Text = Convert.ToString(Convert.ToUInt32(hour));


                SMS = new SmsModule(Convert.ToString(Session["BankName"]), Convert.ToString(Session["BranchName"]).Trim());
                txtBalMessage.Text = SmsModule.MessageFormat;
                SMS.GlCodeBind(ddlPrdType);
                SMS.GlCodeBindForBal(ddlBalPrdType);



            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //if (TxtBRCD.Text != "")
            //{
            //    TxtATName.Text = AST.GetAccType(TxtAccType.Text, TxtBRCD.Text);

            //    string[] GL = BD.GetAccTypeGL(TxtAccType.Text, TxtBRCD.Text).Split('_');
            //    TxtATName.Text = GL[0].ToString();
            //    ViewState["GL"] = GL[1].ToString();
            //    AutoAccname.ContextKey = TxtBRCD.Text + "_" + TxtAccType.Text + "_" + ViewState["GL"].ToString();

            //    TxtAccno.Focus();
            //}
            //else
            //{
            //    WebMsgBox.Show("Enter Branch Code First....!", this.Page);
            //    TxtAccType.Text = "";
            //    TxtBRCD.Focus();
            //}

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }



    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {



            DateTime d1, d2;
            if (DateTime.TryParse(txtMessageDate.Text, out d1) &&
                DateTime.TryParse(DateTime.Now.ToString("dd/MM/yyyy"), out d2) &&
                d2 > d1)
            {
                WebMsgBox.Show("Back Date is Not Allowed", this.Page);
                return;
            }

            if (txtMessage.Text == "" && rdbSMSMar.Checked == true)
            {
                WebMsgBox.Show("Please Enter the Message and Try Again", this.Page);
                return;
            }
            else if (txtMessageEng.Text == "" && rdbSMSEng.Checked == true)
            {
                WebMsgBox.Show("Please Enter the Message and Try Again", this.Page);
                return;
            }
            else if (txtMessageDate.Text == "" || (txtMin.Text == "" && txtHr.Text == ""))
            {
                WebMsgBox.Show("Please Enter the Message Date and Try Again", this.Page);
                return;
            }
            GenerateTextFile();
            WebMsgBox.Show("File Created Successfully", this.Page);
            ClearData();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //Code to Create  text file for sms 

    #region textsmsForNow
    public void GenerateTextFile()
    {
        try
        {

            string FolderPath = @"D:\SMS\" + Convert.ToString(Session["BankCode"]); //   +DateTime.Now.ToString("ddMMyyyy");
            string TodayDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string FileName = "", FilePath = "", message = "";

            // string [] PrdArr=new string[100];
            string[] PrdArr = { String.Empty };
            int acs = 0;
            if (rdbSMSEng.Checked)
            {

                sb.Append(txtMessageEng.Text);
            }
            else if (rdbSMSMar.Checked)
            {
                sb.Append(txtMessage.Text);
            }


            if (!Directory.Exists(FolderPath))// To Create SMS Directory
                Directory.CreateDirectory(FolderPath);
            FilePath = FolderPath + FileName + ".txt";

           // FilePath = @"D:\SMS\" + FileName + ".txt";

            if ((System.IO.File.Exists(FilePath))) //To remove Existing File frm D:\SMS Folder
            {
                System.IO.File.Delete(FilePath);
            }

            if (ddlPrdType.SelectedValue == "0")
            {
                DT = SMS.GetAllGlCode();
                PrdArr = new String[DT.Rows.Count];
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        PrdArr[i] = Convert.ToString(DT.Rows[i]["SUBGLCODE"]);
                    }
                }
                DT1 = SMS.GetAllCustomer(Convert.ToString(Session["BRCD"]));
            }
            else
            {
                DT1 = SMS.GetSpecificCustomer(ddlPrdType.SelectedValue, txtFromCustNo.Text, txtToCustNo.Text, Convert.ToString(Session["BRCD"]));

            }



            string[] ExistsFiles = Directory.GetFiles(FolderPath);


            FileName = "SMS_" + TodayDate + "";



            FilePath =FolderPath + FileName + ".txt";

            if (DT1.Rows.Count > 0)
            {


                for (int K = 0; K <= DT1.Rows.Count - 1; K++)
                {
                    string Message = "";
                    Message += "|" + DT1.Rows[K]["ACCNO"].ToString() + "|" + DT1.Rows[K]["BRCD"].ToString() + "|" + DT1.Rows[K]["MOBILE1"].ToString() + "|" + sb.ToString() + "|" + txtMessageDate.Text + ":" + txtHr.Text + ":" + txtMin.Text + "|";

                    using (StreamWriter writer = new StreamWriter(FilePath, true))
                    {
                        writer.WriteLine(Message);
                        writer.Close();
                    }
                    if (rdbSMSMar.Checked)
                    {
                        SendMessage(DT1.Rows[K]["BRCD"].ToString(), DT1.Rows[K]["ACCNO"].ToString(), DT1.Rows[K]["MOBILE1"].ToString(), sb.ToString(), FilePath, DT1.Rows[K]["MOBILE1"].ToString(), "2");

                    }
                    else
                    {
                        SendMessage(DT1.Rows[K]["BRCD"].ToString(), DT1.Rows[K]["ACCNO"].ToString(), DT1.Rows[K]["MOBILE1"].ToString(), sb.ToString(), FilePath, DT1.Rows[K]["MOBILE1"].ToString(), "0");

                    }
                }


                // SendMessage(FilePath);
            }

            else
            {
                WebMsgBox.Show("No Records to Be Displayed", this.Page);
                return;
            }


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }


    #endregion
    #region textsmsforlater
    public void GenerateTextFileForLater()// for sending message later via Shedular
    {
        try
        {
            if (txtHr.Text.Length == 1)
            {
                txtHr.Text = "0" + txtHr.Text;
            }
            if (txtMin.Text.Length == 1)
            {
                txtMin.Text = "0" + txtMin.Text;
            }


            string FolderPath = @"D:\SMS\Send"; //   +DateTime.Now.ToString("ddMMyyyy");
            string TodayDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string FileName = "", FilePath = "";

            // string [] PrdArr=new string[100];
            string[] PrdArr = { String.Empty };
            int acs = 0;
            if (rdbSMSEng.Checked == true)
            {
                sb.Append(txtMessageEng.Text);
            }
            else if (rdbSMSMar.Checked == true)
            {
                sb.Append(txtMessage.Text);
            }
            if (!Directory.Exists(FolderPath))// To Create SMS Directory
                Directory.CreateDirectory(FolderPath);
            FilePath = @"D:\SMS\Send" + FileName + ".txt";

            if ((System.IO.File.Exists(FilePath))) //To remove Existing File frm D:\SMS Folder
            {
                System.IO.File.Delete(FilePath);
            }

            if (ddlPrdType.SelectedValue == "0")
            {
                DT = SMS.GetAllGlCode();
                PrdArr = new String[DT.Rows.Count];
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        PrdArr[i] = Convert.ToString(DT.Rows[i]["SUBGLCODE"]);
                    }
                }
                DT1 = SMS.GetAllCustomer(Convert.ToString(Session["BRCD"]));
            }
            else
            {
                DT1 = SMS.GetSpecificCustomer(ddlPrdType.SelectedValue, txtFromCustNo.Text, txtToCustNo.Text, Convert.ToString(Session["BRCD"]));

            }



            string[] ExistsFiles = Directory.GetFiles(@"D:\SMS\SEND");


            FileName = "SMS_" + TodayDate + txtHr.Text + txtMin.Text + "";



            FilePath = @"D:\SMS\Send\" + FileName + ".txt";

            if (DT1.Rows.Count > 0)
            {


                for (int K = 0; K <= DT1.Rows.Count - 1; K++)
                {
                    string Message = "";
                    Message += DT1.Rows[K]["ACCNO"].ToString() + "|" + DT1.Rows[K]["BRCD"].ToString() + "|" + DT1.Rows[K]["MOBILE1"].ToString() + "|" + sb.ToString() + "|" + txtMessageDate.Text + ":" + txtHr.Text + ":" + txtMin.Text + "|";

                    using (StreamWriter writer = new StreamWriter(FilePath, true))
                    {
                        writer.WriteLine(Message);
                        writer.Close();
                    }
                }
                // SendMessage(FilePath);
            }

            else
            {
                WebMsgBox.Show("No Records to Be Displayed", this.Page);
                return;
            }


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }


    #endregion

    #region textsms
    public void GenerateTextFileWithStatus(string FilePath, string Status, string AccNo, string Message, string BRCD, string MobileNo)
    {
        try
        {

            string FolderPath = @"D:\SMS\Reponse\"; //   +DateTime.Now.ToString("ddMMyyyy");
            string TodayDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string FileName = Path.GetFileName(FilePath);


            if (!Directory.Exists(FolderPath))// To Create SMS Directory
                Directory.CreateDirectory(FolderPath);
            FilePath = @"D:\SMS\Reponse\" + FileName + ".txt";

            //if ((System.IO.File.Exists(FilePath))) //To remove Existing File frm D:\SMS Folder
            //{
            //    System.IO.File.Delete(FilePath);
            //}




            if (!System.IO.File.Exists(FilePath))
            {
                Message = "|" + AccNo + "|" + BRCD + "|" + MobileNo + "|" + Message + "|" + Status + "|";

                using (StreamWriter writer = new StreamWriter(FilePath, true))
                {
                    writer.WriteLine(Message);
                    writer.Close();
                }
            }


            else if (System.IO.File.Exists(FilePath))
            {
                using (StreamWriter w = File.AppendText(FilePath))
                {
                    Message = "|" + AccNo + "|" + BRCD + "|" + MobileNo + "|" + Message + "|" + Status + "|";
                    w.WriteLine(Message);
                    w.Close();
                }
            }








        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }


    #endregion


    #region Message Send

    public void SendMessage(string FilePath)// To Send Message
    {
        try
        {


            String path = FilePath;
            if (!File.Exists(path))
            {
                WebMsgBox.Show("File Not Exist", this.Page);
                return;
            }
            else
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    string line;
                    string[] lines = new string[100];

                    int i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines = line.Split('|');
                        //lines[i] = line;
                        Accno = lines[0];
                        BRCD = lines[1];
                        MobileNo = lines[2];
                        Message = lines[3];
                        Time = lines[4];
                        // string SMS = MS.Send_SMSALL(Accno, Message, MobileNo);
                        //GenerateTextFileWithStatus(FilePath, SMS, Accno, Message, BRCD);

                        i++;

                    }




                }





            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion




    #region Message Send With All Parameters

    public void SendMessage(string BRCD, string Accno, string MobileNo, string Message, string FilePath, string MobNo, string messageType)// To Send Message
    {
        try
        {
            string SMS = MS.Send_SMSALL(CustNo: Accno, Message: Message, MobileNo: MobileNo, messageType: messageType);
            GenerateTextFileWithStatus(FilePath, SMS, Accno, Message, BRCD, MobNo);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion




    //  Added by amol on 03/10/2018 for log details
    public void LogDetails()
    {
        try
        {

            //string SMS = MS.Send_SMS(CustNo, EDate);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    protected void rdbSMSMar_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbSMSEng.Checked == true)
            {
                txtMessage.Visible = false;
                txtMessageEng.Visible = true;
            }
            else if (rdbSMSMar.Checked == true)
            {
                txtMessage.Visible = true;
                txtMessageEng.Visible = false;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void rdbSMSEng_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            if (rdbSMSEng.Checked == true)
            {
                txtMessage.Visible = false;
                txtMessageEng.Visible = true;
            }
            else if (rdbSMSMar.Checked == true)
            {
                txtMessage.Visible = true;
                txtMessageEng.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlCustType_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlCustType.SelectedValue == "1")
            {

                txtFromCustNo.Visible = false;
                txtFromCustName.Visible = false;
                txtToCustNo.Visible = false;
                txtToCustName.Visible = false;
            }
            else if (ddlCustType.SelectedValue == "2")
            {

                txtFromCustNo.Visible = true;
                txtFromCustName.Visible = true;
                txtToCustNo.Visible = false;
                txtToCustName.Visible = false;
            }
            else if (ddlCustType.SelectedValue == "3")
            {

                txtFromCustNo.Visible = true;
                txtFromCustName.Visible = true;
                txtToCustNo.Visible = true;
                txtToCustName.Visible = true;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtFromCustName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = SMS.GetCustName(ddlPrdType.SelectedValue, txtFromCustNo.Text);
            if (DT.Rows.Count > 0)
            {
                txtFromCustName.Text = Convert.ToString(DT.Rows[0]["CustName"]);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtFromCustNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = SMS.GetCustName(ddlPrdType.SelectedValue, txtFromCustNo.Text);
            if (DT.Rows.Count > 0)
            {
                txtFromCustName.Text = Convert.ToString(DT.Rows[0]["CustName"]);
            }
            else
            {
                WebMsgBox.Show("Please Enter proper account no And Try Again", this.Page);
                txtFromCustName.Text = "";
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ClearData()
    {
        try
        {
            ddlCustType.SelectedIndex = -1;
            ddlPrdType.SelectedIndex = -1;
            txtFromCustName.Text = "";
            txtFromCustNo.Text = "";
            txtToCustNo.Text = "";
            txtMessage.Text = "";
            txtMessageEng.Text = "";
            ddlBalPrdType.SelectedIndex = -1;
            HdnField1.Value = "0";

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlPrdType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtFromCustNo.Text = "";
            txtFromCustName.Text = "";
            txtToCustNo.Text = "";
            txtToCustName.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSubmitLater_Click(object sender, EventArgs e)
    {
        try
        {
            GenerateTextFileForLater();
            WebMsgBox.Show("File Created Successfully", this.Page);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {

    }

    protected void btnCheckHidden_Click(object sender, EventArgs e)
    {
        try
        {
            WebMsgBox.Show(HdnField1.Value, this.Page);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSendBalSMS_Click(object sender, EventArgs e)
    {

        try
        {


            // string [] PrdArr=new string[100];
            string[] PrdArr = { String.Empty };



            if (ddlBalPrdType.SelectedValue == "0")
            {
                DT = SMS.GetAllGlCodeForBal();
                PrdArr = new String[DT.Rows.Count];
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        PrdArr[i] = Convert.ToString(DT.Rows[i]["SUBGLCODE"]);
                    }
                }
                DT1 = SMS.GetAllCustomer(Convert.ToString(Session["BRCD"]));
            }
            else
            {
                DT1 = SMS.GetSpecificCustomer(ddlBalPrdType.SelectedValue, txtFromCustNo.Text, txtToCustNo.Text, Convert.ToString(Session["BRCD"]));

            }



            if (DT1.Rows.Count > 0)
            {


                for (int K = 0; K <= DT1.Rows.Count - 1; K++)
                {

                    Message = "";

                    if (ddlBalPrdType.SelectedValue == "0")
                    {

                        DataTable TempGlTable = GetGlRecords(Convert.ToString(DT1.Rows[K]["CustNo"]), Convert.ToString(DT1.Rows[K]["BRCD"]));


                        for (int l = 0; l < TempGlTable.Rows.Count; l++)
                        {
                            Message += "AccNo= " + Convert.ToString(TempGlTable.Rows[l]["SubGlCode"]) + "/ " + Convert.ToString(TempGlTable.Rows[l]["AccNo"]) + " and Balance is " + GetBalance(Convert.ToString(TempGlTable.Rows[l]["SubGlCode"]), Convert.ToString(TempGlTable.Rows[l]["AccNo"]), Convert.ToString(TempGlTable.Rows[l]["BRCD"])) + ". ";
                        }
                        Message += "Thank You From " + Convert.ToString(Session["BankName"]) + " ( " + Convert.ToString(Session["BranchName"]).Trim() + " ) ";

                    }
                    else
                    {
                        Message = "Your Account No " + Convert.ToString(ddlBalPrdType.SelectedValue) + "/" + Convert.ToString(DT1.Rows[K]["Accno"]) + " Available Balance is " + GetBalance(Convert.ToString(ddlBalPrdType.SelectedValue), Convert.ToString(DT1.Rows[K]["Accno"]), Convert.ToString(DT1.Rows[K]["BRCD"])) + " . Thank You From " + Convert.ToString(Session["BankName"]) + " ( " + Convert.ToString(Session["BranchName"]).Trim() + " ) ";

                    }


                    //string SMS = MS.Send_SMSALL(Convert.ToString(DT1.Rows[K]["Accno"]), Message, Convert.ToString(DT1.Rows[K]["MOBILE1"]));


                    if (ddlBalPrdType.SelectedValue == "0")
                    {
                        SmsResult = BD.InsertSMSRec(Convert.ToString(DT1.Rows[K]["BRCD"]), Convert.ToString(DT1.Rows[K]["SubGlCode"]), Convert.ToString(DT1.Rows[K]["Accno"]).Trim().ToString(), Message, Convert.ToString(Session["MID"]), Convert.ToString(Session["MID"]), Convert.ToString(Session["EntryDate"]), "Payment");
                    }
                    else
                    {
                        SmsResult = BD.InsertSMSRec(Convert.ToString(DT1.Rows[K]["BRCD"]), ddlBalPrdType.SelectedValue.Trim().ToString(), Convert.ToString(DT1.Rows[K]["Accno"]).Trim().ToString(), Message, Convert.ToString(Session["MID"]), Convert.ToString(Session["MID"]), Convert.ToString(Session["EntryDate"]), "Payment");

                    }



                    // SMS.SendSMS(Convert.ToString(DT.Rows[K]["BRCD"]), Convert.ToString(DT.Rows[K]["SubGlCode"]), Convert.ToString(DT.Rows[K]["AccNo"]), Convert.ToString(Session["EntryDate"]), Convert.ToString(Session["EntryDate"]));
                    //string Message = "";
                    //Message += DT1.Rows[K]["ACCNO"].ToString() + "|" + DT1.Rows[K]["BRCD"].ToString() + "|" + DT1.Rows[K]["MOBILE1"].ToString() + "|" + sb.ToString() + "|" + txtMessageDate.Text + ":" + txtHr.Text + ":" + txtMin.Text + "|";

                    //using (StreamWriter writer = new StreamWriter(FilePath, true))
                    //{
                    //    writer.WriteLine(Message);
                    //    writer.Close();
                    //}


                }

                WebMsgBox.Show("Message Sent Successfully", this.Page);
                ClearData();
                return;
                // SendMessage(FilePath);
            }

            else
            {
                WebMsgBox.Show("No Records to Be Displayed", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }



    #region ForGetBalance
    public Double GetBalance(string SubGlCode, string Accno, string BRCD)
    {

        string ToDate = "";

        ToDate = Session["EntryDate"].ToString();
        try
        {
            DataTable DT = new DataTable();
            DT = AST.GetCustName(SubGlCode, Accno, BRCD);
            if (DT.Rows.Count > 0)
            {
                string[] TD = ToDate.Split('/');

                string[] GL = BD.GetAccTypeGL(SubGlCode, BRCD).Split('_');
                //TxtATName.Text = GL[0].ToString();
                ViewState["GL"] = GL[1].ToString();

                //--Abhishek
                string RES = AST.GetAccStatus(BRCD, Accno, SubGlCode);
                Balance = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), SubGlCode, Accno.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString());
            }

        }
        catch (Exception Ex)
        {
            Balance = 0.0F;
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Balance;

    }

    #endregion


    #region GetAllGlRecords of Customers
    public DataTable GetGlRecords(string CustNo, string BRCD)
    {

        try
        {

            GTTable = SMS.GetAllGlRecord(CustNo, BRCD);


        }
        catch (Exception Ex)
        {
            GTTable = null;
            ExceptionLogging.SendErrorToText(Ex);
        }
        return GTTable;
    }

    #endregion







}