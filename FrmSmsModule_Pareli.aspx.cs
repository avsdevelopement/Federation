using System;
using System.Data;
using System.IO;
using System.Text;

public partial class FrmSmsModule_Pareli : System.Web.UI.Page
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
    string Accno = "", MobileNo = "", Message = "", Time = "", BRCD = "",sResult="";
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

                txtMessageDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtMin.Text = Convert.ToString(Convert.ToUInt32(min) + 2);
                txtHr.Text = Convert.ToString(Convert.ToUInt32(hour));
                SMS = new SmsModule(Convert.ToString(Session["BankName"]), Convert.ToString(Session["BranchName"]).Trim());               
             //   SMS.GlCodeBind(ddlPrdType);
               



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
            ClearData();
            WebMsgBox.Show("File Created Successfully", this.Page);

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

            string FolderPath = @"D:\SMS\"; //   +DateTime.Now.ToString("ddMMyyyy");
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
            FilePath = @"D:\SMS\" + FileName + ".txt";

            if ((System.IO.File.Exists(FilePath))) //To remove Existing File frm D:\SMS Folder
            {
                System.IO.File.Delete(FilePath);
            }

            if (ddlPrdType.SelectedValue == "1")
            {
                DT = SMS.GetAllDirectorNo();
                DT1 = DT.Copy();
            }
            else
            {
                DT1 = SMS.GetSpecificCustomer(ddlPrdType.SelectedValue, txtFromCustNo.Text, txtToCustNo.Text,Convert.ToString(Session["BRCD"]));

            }



            string[] ExistsFiles = Directory.GetFiles(@"D:\SMS\");


            FileName = "SMS_" + TodayDate + "";



            FilePath = @"D:\SMS\" + FileName + ".txt";

            if (DT1.Rows.Count > 0)
            {


               for (int K = 0; K <= DT1.Rows.Count - 1; K++)
               //  for (int K = 0; K <= 3 - 1; K++)

                {
                    string Message = "";
                    Message += "|" + DT1.Rows[K]["ACCNO"].ToString() + "|" + DT1.Rows[K]["BRCD"].ToString() + "|" + DT1.Rows[K]["MOBILE1"].ToString() + "|" + sb.ToString() + "|" + txtMessageDate.Text + ":" + txtHr.Text + ":" + txtMin.Text + "|";

                    using (StreamWriter writer = new StreamWriter(FilePath, true))
                    {
                        writer.WriteLine(Message);
                        writer.Close();
                    }

                    SendMessage(DT1.Rows[K]["BRCD"].ToString(), DT1.Rows[K]["ACCNO"].ToString(), DT1.Rows[K]["MOBILE1"].ToString(), sb.ToString(), FilePath, DT1.Rows[K]["MOBILE1"].ToString());
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

    public void SendMessage(string BRCD, string Accno, string MobileNo, string Message, string FilePath, string MobNo)// To Send Message
    {
        try
        {
            string SMS = MS.Send_SMSALL(Accno, Message, MobileNo);
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
            DT = SMS.GetCustNameByGlCode(ddlPrdType.SelectedValue, txtFromCustNo.Text);
            if (DT.Rows.Count > 0)
            {
                txtFromCustName.Text = Convert.ToString(DT.Rows[0]["CustName"]);
            }
            else
            {
                WebMsgBox.Show("Please Enter proper Customer no And Try Again", this.Page);
                txtFromCustName.Text = "";
                return;
            }

            sResult = BD.Getstage1(CNO: txtFromCustNo.Text, BRCD: Convert.ToString(Session["BRCD"]), PRD: ddlPrdType.SelectedValue);
            if (sResult.Equals("1004"))
            {
                WebMsgBox.Show("This Account No is Closed...", this.Page);
                txtFromCustName.Text = "";
                return;
            }
            else
            {

                txtMessageEng.Focus();
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
            txtFromCustName.Visible = false;
            txtFromCustNo.Text = "";
            txtFromCustNo.Visible = false;
            txtToCustNo.Text = "";
            txtMessage.Text = "";
            txtMessageEng.Text = "";
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
            if (ddlPrdType.SelectedValue.Equals("4"))
            {
                ddlCustType.Enabled = true;
                txtFromCustNo.Text = "";
                txtFromCustName.Text = "";
                txtToCustNo.Text = "";
                txtToCustName.Text = "";
            }
            else if (ddlPrdType.SelectedValue.Equals("1"))
            {
                ddlCustType.Enabled = false;
                ddlCustType.SelectedValue = "1";
                txtFromCustName.Visible = false;
                txtFromCustNo.Visible = false;
                txtToCustName.Visible = false;
                txtToCustNo.Visible = false;
                txtFromCustNo.Text = "";
                txtFromCustName.Text = "";
                txtToCustNo.Text = "";
                txtToCustName.Text = "";
            }

            else
            {
                ddlCustType.Enabled = false;
               
                txtFromCustNo.Text = "";
                txtFromCustName.Text = "";
                txtToCustNo.Text = "";
                txtToCustName.Text = "";
            }
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

   


   



      

}