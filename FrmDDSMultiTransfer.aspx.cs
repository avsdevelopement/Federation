using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Web.Services;

public partial class FrmDDSMultiTransfer : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsCommon CM = new ClsCommon();
    SqlDataAdapter Adapt = new SqlDataAdapter();
    DataTable DT, DTTEST;
    SqlCommand CMD = new SqlCommand();
    SqlDataReader SDR;
    ClsAuthorized AT = new ClsAuthorized();
    ClsDDSMultiTransfer DR = new ClsDDSMultiTransfer();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsOpenClose OC = new ClsOpenClose();
    DataTable DT2 = new DataTable();
    string RESULT = "", FL = "";
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string sql = "";
    string AccOpeningDate;
    string ENTRYDATE;// crct file trn. date
    string accno; //cusst acc opening 

    string CREDIT;// allocated collection amt
    string DEBIT = "0";// //by default 0
    string ACTIVITY = "7";//by default 7
    string SETNO = "";// avs100 lastno+1
    string SCROLLNO;// auto increment number pass
    string INSTRUMENTNO = "0";
    string INSTBANKCD = "0";
    string INSTBRCD;
    string STAGE = "1001";
    string RTIME;// call rtime()
    string BRCD;
    string MID;
    string CID = "0";
    string VID = "0";
    string PCMAC;// = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.ToString();
    string PAYMAST = "0";
    string CUSTNO;// find in master
    string CUSTNAME;// find in master
    string REFID = "0";
    int Result = 0;





    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //Session["BRCD"] = "1";
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                AccOpeningDate = Session["EntryDate"].ToString();
                INSTBRCD = Session["BRCD"].ToString();
                BRCD = INSTBRCD; //Session["BRCD"].ToString();
                MID = Session["MID"].ToString();
                Btn_MobileUpload.Visible = false;
                autoAgname.ContextKey = Session["BRCD"].ToString();
                string MD = "";
                MD = DR.GetParaModify();
                if (MD == "N")
                {
                    gridadd.Columns[4].Visible = false;
                }
                else
                {
                    gridadd.Columns[4].Visible = true;
                }

                string BKCD = CM.GetBankCode();
                if (BKCD != null)
                {

                    ViewState["BKCD"] = BKCD;
                    if (BKCD == "1007")
                    {

                        BtnAkytUpload.Visible = true;
                        BtnAkytPosting.Visible = true;
                    }
                    else
                    {
                        BtnAkytUpload.Visible = false;
                        BtnAkytPosting.Visible = false;
                    }
                }


                // BindPCRX();

            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
                //Response.Redirect("FrmLogin.aspx", true);
            }
        }
    }

    #region DDS upload
    public DateTime rtime()
    {
        //DateTime ClientDateTime = DateTime.Now;
        DateTime _localTime = new DateTime();
        try
        {
            _localTime = DateTime.Now.Date;// TimeZoneInfo.ConvertTimeBySystemTimeZoneId(ClientDateTime, "Arab Standard Time");
            return _localTime;// system time
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
        return _localTime;
    }
    // ------------------------------------ get name of agent start ---------------------------

    protected void TxtAgentNo_TextChanged(object sender, EventArgs e)
    {
        try
        {

            DbConnection conn = new DbConnection();
            string CNName = "";
            string sql = "select AGENTNAME from AGENTMAST where AGENTCODE='" + TxtAgentNo.Text + "' and brcd='" + Session["BRCD"] + "'";
            CNName = conn.sExecuteScalar(sql);
            if (CNName != null)
            {
                TxtAgentName.Text = CNName.ToString();// dt1.Rows[0][0].ToString();
                string[] TD = Session["EntryDate"].ToString().Split('/');
                string SGL = DR.GetSubgLc(Session["BRCD"].ToString());
                TxtAGAmt.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), SGL, TxtAgentNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "6").ToString();
                //OC.GetOpenClose("", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();
            }
            else
            {
                WebMsgBox.Show("Invalid Agentcode...!", this.Page);
                TxtAgentNo.Text = "";
                TxtAgentNo.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }

    }

    //-----------------------------  get name of agent end --------------------------------------

    //----------------------------- BtnUpload2_Click  -------------------------------------------

    protected void BtnUpload_Click(object sender, EventArgs e) // uploding file 
    {
        try
        {

            //ProcessStartInfo PSI = new ProcessStartInfo();

            //PSI.FileName = System.Configuration.ConfigurationManager.AppSettings["EXEFilePath"];
            //if (System.IO.File.Exists(PSI.FileName))
            //{
            //    PSI.WorkingDirectory = Environment.CurrentDirectory;
            //    Process Proc = Process.Start(PSI);
            //    WebMsgBox.Show("IN EXE PROCESS", this.Page);
            //}
            //else
            //{
            //    WebMsgBox.Show("file not found", this.Page);

            //}

            DbConnection conn = new DbConnection();
            sql = "Select * from  AFTEKR where agentcode='" + TxtAgentNo.Text + "' and brcd='" + Session["BRCD"].ToString() + "'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                sql = "Delete from AFTEKR where agentcode='" + TxtAgentNo.Text + "' and brcd='" + Session["BRCD"].ToString() + "'";
                Result = conn.sExecuteQuery(sql);
                if (Result > 0)
                {

                    readtxt();
                    BindGrid();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_" + TxtAgentNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
            }
            else
            {
                //TxtAGAmt.Text = DR.GetAmount(TxtAgentNo.Text, Session["EntryDate"].ToString(), Session["BRCD"].ToString()); GET AGENT TODAYS AMOUNT
                readtxt();
                BindGrid();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void BtnUpload_Click1(object sender, EventArgs e) // uploding file 
    {
        Process process = new Process();
        process.StartInfo.FileName = "DB2PC.exe";
        process.StartInfo.Arguments = "if any";
        process.Start();
    }

    public void readtxt()
    {
        DbConnection conn = new DbConnection();
        try
        {
            if (FileUPControl.HasFile || FileUPControl.PostedFile != null)
            {
                lblprocess.Text = "";
                string delimeter = ",";
                string PRAMB;
                string path = Path.GetFileName(FileUPControl.FileName);

                int RC = 0;

                if (path == "")
                {
                    WebMsgBox.Show("Sorry File Not Found", this.Page);
                    return;
                }
                string[] ValidTypes = { "DAT", "dat" };
                string Exten = System.IO.Path.GetExtension(FileUPControl.PostedFile.FileName);
                bool Valid = false;
                for (int i = 0; i < ValidTypes.Length; i++)
                {
                    if (Exten == "." + ValidTypes[i])
                    {
                        Valid = true;
                        break;
                    }
                }
                if (Valid)
                {
                    FileUPControl.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + path);

                    string fileName = Server.MapPath("~/Uploads/" + path);
                    DataTable DT = new DataTable();

                    StreamReader sr = new StreamReader(fileName);

                    string sql = "SELECT LISTVALUE FROM PARAMETER WHERE LISTFIELD='BalajiM' AND BRCD=" + Session["BRCD"];
                    PRAMB = conn.sExecuteScalar(sql);
                    string allData = sr.ReadToEnd();
                    //lblprocess.Text = "Creating Temp Table.....";
                    if (PRAMB == "Y")
                    {

                        DT.Columns.Add("Accno");
                        DT.Columns.Add("TranAmt");
                        DT.Columns.Add("Blank1");
                        DT.Columns.Add("Blank2");
                        DT.Columns.Add("Entrydate");
                        DT.Columns.Add("Blank3");

                    }

                    else
                    {
                        DT.Columns.Add("Entrydate");
                        DT.Columns.Add("Accno");
                        DT.Columns.Add("TranAmt");
                        DT.Columns.Add("Receiptno");
                        DT.Columns.Add("ReceiptPrint");
                        DT.Columns.Add("TransferT");
                    }

                    DT2.Columns.Add("ID");
                    DT2.Columns.Add("NAME");
                    DT2.Columns.Add("ACCNO");
                    DT2.Columns.Add("AMOUNT");
                    DT2.Columns.Add("REMARK");

                    string[] rows = allData.Split("\r\n".ToCharArray());
                    int count = 0;
                    int ij = 0;
                    foreach (string r in rows)
                    {

                        if (ij == 0)
                        {
                            ij++;
                            continue;
                        }
                        string[] items = r.Split(delimeter.ToCharArray());
                        string ACN = items[0].ToString().Trim();
                        int value;
                        if (int.TryParse(ACN, out value)) // Added for Pen PCRX File requirement --Abhishek 09/06/2017
                        {
                            RESULT = DR.CheckACValid(Session["BRCD"].ToString(), ACN.ToString(), TxtAgentNo.Text);
                            // if (RESULT!=null && Convert.ToInt32(RESULT) !=Convert.ToInt32(ACN) && items.Length > 5 )

                            if (items.Length > 5 && Convert.ToInt32(RESULT) > 0 && RESULT != null)
                            {
                                DT.Rows.Add(items);
                            }

                            if (Convert.ToInt32(RESULT) == 0 && items.Length > 5 && RESULT != null)
                            {
                                count++;
                                DT2.Rows.Add(count, items[2].Trim(), items[0], items[1], "A/C Not Created");
                            }
                        }

                        lblprocess.Text = "";
                        lblprocess.Text = "For Record No " + ij + "....";
                        ij++;
                    }

                    if (File.Exists(Server.MapPath("~/Uploads/" + path)))
                    {
                        sr.Close();
                        //File.Delete(Server.MapPath("~/Uploads/" + path));
                    }

                    sr.Close();
                    Result = DR.InsertData(TxtAgentNo.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());

                    if (Result > 0)
                    {
                        for (int i = 0; i <= DT.Rows.Count - 1; i++)
                        {
                            if (PRAMB != "Y")
                            {

                                RC = DR.UpdateAFTData(TxtAgentNo.Text, Session["EntryDate"].ToString(), conn.ConvertToDDSPostDate(DT.Rows[i]["Entrydate"].ToString().Replace(".", "/")), DT.Rows[i]["Accno"].ToString(), Session["BRCD"].ToString(), DT.Rows[i]["Entrydate"].ToString(), DT.Rows[i]["TranAmt"].ToString(), "CHGAMT");
                            }
                            else if (PRAMB == "Y")
                            {

                                RC = DR.UpdateAFTData(TxtAgentNo.Text, Session["EntryDate"].ToString(), conn.ConvertToDDSPostDate(DT.Rows[i]["Entrydate"].ToString().Replace(".", "/")), DT.Rows[i]["Accno"].ToString(), Session["BRCD"].ToString(), DT.Rows[i]["TranAmt"].ToString(), DT.Rows[i]["Entrydate"].ToString(), "CHGAMT");
                                if (RC > 0)
                                    continue;
                                else
                                {
                                    break;
                                }

                            }
                        }
                        BindGrid();
                        BindInvalidGrid();
                        TxttotalAmt.Text = DR.GetAgentCol(TxtAgentNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString());
                        TxtDiffrence.Text = (Convert.ToDouble(TxtAGAmt.Text) - Convert.ToDouble(TxttotalAmt.Text)).ToString();

                        WebMsgBox.Show("Uploading Completed..!", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_UPLOAD_" + TxtAgentNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        return;
                    }
                    else
                    {
                        WebMsgBox.Show("Sorry Data Already Uploaded", this.Page);
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("WARNING : File Extension is not Supported....!", this.Page);
                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Browse the file First.....", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }

    }
    //----------------------------- BtnUpload2_Click end --------------------------------------------------

    //----------------------------- btnClose_Click start -------------------------------------

    protected void btnClose_Click(object sender, EventArgs e)
    {
    }

    //----------------------------- btnClose_Click end -------------------------------------

    public void BindGrid()
    {
        int RS = BindContact(gridadd);
    }

    public void BindInvalidGrid()
    {
        Grd_NoAcc.DataSource = DT2;
        Grd_NoAcc.DataBind();
    }

    protected void UploadPC_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        try
        {
            readtxt();
        }
        catch (Exception Ex)
        {
            //WebMsgBox.Show(Ex.Message);
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void GetCashDetails()
    {

    }
    public int BindContact(GridView Gview)
    {
        int Result = 0;
        try
        {
            string sql = "SELECT Convert(varchar(11),ENTRYDATE,103)ENTRYDATE,Convert(varchar(11),POSTINGDATE,103)POSTINGDATE,AGENTCODE,BRCD,TRANSACTIONDATE,ACNO,TRANSAMT ,RECEIPTNO,NOOFRECEIPTPRINTED,TRANSTIME ,STAGE from AFTEKR  WHERE AGENTCODE ='" + TxtAgentNo.Text + "' AND STAGE <>'1004' AND BRCD='" + Session["BRCD"].ToString() + "' AND TRANSAMT <>0 order by entrydate,acno";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }
    public int BindAuthorize(GridView Gview) // SHOW AUTHORIZE DATA
    {
        //string getdt = AccOpeningDate.ToString();
        //string[] datearray = getdt.Split('-');
        //string TBNAME = datearray[0].ToString() + datearray[1].ToString();
        //string sql1 = "SELECT BRCD,ENTRYDATE,POSTINGDATE TRANSACTIONDATE,GLCODE ,SUBGLCODE AGENTCODE,ACCNO ACNO,AMOUNT TRANSAMT,INSTRUMENTNO RECEIPTNO, REFID TRANSTIME,'1' NOOFRECEIPTPRINTED FROM AVSM_" + TBNAME + "  WHERE subglcode ='" + TxtAgentNo.Text + "' and entrydate='" + Convert.ToDateTime(AccOpeningDate).ToString("dd/MM/yyyy") + "' AND STAGE ='1003' AND BRCD='" + Session["BRCD"] + "' ";
        //// string sql1 = " select  ENTRYDATE,FUNDINGDATE  ENTRYDATE,GLCODE,SUBGLCODE ANGENTCODE,ACCNO ACNO,AMOUNT TRANSAMT,BRCD BRCD FROM " + TBNAME + "  WHERE subglcode ='"+TxtAgentNo.Text+"' and entrydate='"+Convert.ToDateTime(Txtdate.Text).ToString("dd/MM/yyyy")+"' AND STAGE ='1003' AND BRCD='"+Session["BRCD"]+"' ";
        ////string sql1 = " select  ENTRYDATE,FUNDINGDATE AS ENTRYDATE,GLCODE,SUBGLCODE AS ANGENTCODE,ACCNO AS ACNO,AMOUNT AS TRANSAMT,BRCD  AS BRCD FROM " + TBNAME + "  WHERE subglcode ='" + TxtAgentNo.Text + "' and entrydate='" + Convert.ToDateTime(Txtdate.Text).ToString("dd/MM/yyyy") + "' AND STAGE ='1003' AND BRCD='" + Session["BRCD"] + "' ";
        //int Result = conn.sBindGrid(Gview, sql1);
        return Result;
    }

    protected void BtnPosting_Click1(object sender, EventArgs e)   // posting 
    {
        DbConnection conn = new DbConnection();
        DataTable dt1 = new DataTable();
        ClsAuthorized cac = new ClsAuthorized();
        string PARTICULARS = "Daily Collection";
        string PARTICULARS2 = "";
        string PMTMODE = "TR-DCM";
        string INSTRUMENTDATE = "01/01/1900";
        string PCMAC = conn.PCNAME();
        int TRXTYPE;
        string custno;
        string custname;
        string SETNO = "";
        string SCROLLNO = "";


        try
        {
            if (Convert.ToInt32(TxtDiffrence.Text) >= 0)
            {
                if (Grd_NoAcc.Rows.Count == 0 || Rdb_Upload.SelectedValue == "1")
                {
                    SETNO = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                    if (Rdb_Upload.SelectedValue == "1")
                    {
                        dt1 = DR.GetPostingData(TxtAgentNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "");
                    }
                    else
                    {
                        dt1 = DR.GetPostingData(TxtAgentNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "MOB");
                    }


                    for (int i = 0; i < dt1.Rows.Count; i++) // Posting date added for tracking entry --Abhishek 21-06-2017
                    {
                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt1.Rows[i]["POSTINGDATE"].ToString(), "2", TxtAgentNo.Text, dt1.Rows[i]["ACNO"].ToString(), PARTICULARS, PARTICULARS2, dt1.Rows[i]["TRANSAMT"].ToString(), "1", "30", PMTMODE.ToString(), SETNO.ToString(), "0", INSTRUMENTDATE.ToString(), "0", Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), " 0", "0", " 0", "0", TxtAgentName.Text, "0", "0");
                        if (Result > 0)
                        {
                        }
                        lblprocess.Text = "Acc No :" + dt1.Rows[i]["ACNO"].ToString() + ".......";
                    }
                    if (Result > 0)
                    {
                        string SUBGL = BD.GetCashGl("6", Session["BRCD"].ToString());

                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "6", SUBGL.ToString(), TxtAgentNo.Text, PARTICULARS, PARTICULARS2, TxttotalAmt.Text, "2", "30", PMTMODE.ToString(), SETNO.ToString(), "0", INSTRUMENTDATE.ToString(), "0", Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), " 0", "0", " 0", "0", TxtAgentName.Text, "0", "0");
                        if (Result > 0)
                        {
                        }

                        if (Rdb_Upload.SelectedValue == "2") //if mobile upload selected then Update ALLVCR stage to 1003
                        {
                            Result = DR.UpdateALLVCRStage(Session["BRCD"].ToString(), TxtAgentNo.Text);
                        }
                        lblMessage.Text = "Record Post Successfully and Voucher No Is " + SETNO;
                        int RC = DR.UpdateSetNo("7", SETNO);
                        ChekcEntry();
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    }
                }
                else
                {
                    lblMessage.Text = "Before Posting Create the Invalid Accounts....!";
                    ModalPopup.Show(this.Page);
                    return;
                }

            }
            else
            {
                lblMessage.Text = "";
                lblMessage.Text = "Agent Balance Insufficient, First Credit the Amount..!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    public void ChekcEntry()
    {
        try
        {
            DbConnection conn = new DbConnection();
            sql = "Select * from  AFTEKR where agentcode='" + TxtAgentNo.Text + "' and brcd='" + Session["BRCD"].ToString() + "'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                sql = "Delete from AFTEKR where agentcode='" + TxtAgentNo.Text + "' and brcd='" + Session["BRCD"].ToString() + "'";
                Result = conn.sExecuteQuery(sql);
                BindGrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_" + TxtAgentNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    public void ChekcEntryAkyt()
    {
        try
        {
            DbConnection conn = new DbConnection();
            sql = "Select * from  AFTEKR where agentcode in ('" + TxtAgentNo.Text + "','303','1') and brcd='" + Session["BRCD"].ToString() + "'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                sql = "Delete from AFTEKR where agentcode in ('" + TxtAgentNo.Text + "','303','1') and brcd='" + Session["BRCD"].ToString() + "'";
                Result = conn.sExecuteQuery(sql);
                BindGrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_" + TxtAgentNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void gridadd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridadd.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlnk = (LinkButton)sender;
            string strnum = objlnk.CommandArgument;
            string[] arr = strnum.Split('_');
            ViewState["strnumId"] = arr[0];
            ViewState["EDT"] = arr[1].Replace(" 12:00:00", "");
            ViewState["PDT"] = arr[2].Replace(" 12:00:00", "");
            GetAmount();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void GetAmount()
    {
        try
        {

            if (Rdb_Upload.SelectedValue == "1")
                TxtCAmt.Text = DR.GetAmtInfo(TxtAgentNo.Text, ViewState["strnumId"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString());
            else
                TxtCAmt.Text = DR.GetAmtInfoMOb(TxtAgentNo.Text, ViewState["strnumId"].ToString(), ViewState["EDT"].ToString(), Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnchnage_Click(object sender, EventArgs e)
    {
        try
        {
            int rc = 0;
            if (Rdb_Upload.SelectedValue == "1")
            {

                rc = DR.UpdateAFTData(TxtAgentNo.Text, Session["EntryDate"].ToString(), ViewState["PDT"].ToString(), ViewState["strnumId"].ToString(), Session["BRCD"].ToString(), TxtCAmt.Text, "", "ADDACC");

                if (rc > 0)
                {
                    TxttotalAmt.Text = DR.GetAgentCol(TxtAgentNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString());
                    TxtDiffrence.Text = (Convert.ToDouble(TxtAGAmt.Text) - Convert.ToDouble(TxttotalAmt.Text)).ToString();
                    BindGrid();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_" + TxtAgentNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    TxtCAmt.Text = "";
                }
            }
            else
            {
                rc = DR.UpdateAFTDataMOB(TxtAgentNo.Text, ViewState["EDT"].ToString(), ViewState["PDT"].ToString(), ViewState["strnumId"].ToString(), Session["BRCD"].ToString(), TxtCAmt.Text, "", Session["MID"].ToString());
                if (rc > 0)
                {
                    TxttotalAmt.Text = DR.GetAgentColMob(TxtAgentNo.Text, Session["BRCD"].ToString());
                    TxtDiffrence.Text = (Convert.ToDouble(TxtAGAmt.Text) - Convert.ToDouble(TxttotalAmt.Text)).ToString();
                    BindGrid();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_" + TxtAgentNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    TxtCAmt.Text = "";
                }
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            System.Diagnostics.Process.Start("file:///C://DB2PCold.exe");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnopenexe_Click(object sender, EventArgs e)
    {
        // CRAETED ASHOK MISAL....
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo.FileName = "C:\\Balaji\\DB2PC.EXE ";
        //start and wait for the exe the run to completion changes by ashok
        process.Start();
        process.WaitForExit();
    }
    protected void Rdb_Upload_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_Upload.SelectedValue == "1")//machine upload
            {
                Btn_MobileUpload.Visible = false;
                btnUpload2.Visible = true;

            }
            else if (Rdb_Upload.SelectedValue == "2")//Mobile Upload
            {
                Btn_MobileUpload.Visible = true;
                btnUpload2.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_MobileUpload_Click(object sender, EventArgs e)
    {
        try
        {
            int Res = 0;
            string STRS = "";
          //  STRS = DR.GetAftekrStatus(Session["BRCD"].ToString(), TxtAgentNo.Text);
            if (Convert.ToInt32(STRS) > 0)
            {
            //    Res = DR.DeleteAftekr(Session["BRCD"].ToString(), TxtAgentNo.Text);
                if (Res > 0)
                {
                    readtxtMob();
                    BindGrid();
                    FL = "Insert";//Dhanya Shetty
                    string Res1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_ Mob_Upload_" + TxtAgentNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                }

            }
            else
            {
                readtxtMob();
                BindGrid();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        ///  DR.bindg(Session["BRCD"].ToString(), TxtAgentNo.Text, gridadd);
    }

    public void readtxtMob()
    {
        DbConnection conn = new DbConnection();
        try
        {

            lblprocess.Text = "";
            string delimeter = ",";
            string PRAMB;


            int RC = 0;
            DataTable DT = new DataTable();
            DataTable DT3 = new DataTable();
            DataTable DT4 = new DataTable();

            PRAMB = DR.BalajiParameter(Session["BRCD"].ToString());

            if (PRAMB == "Y")
            {

                DT.Columns.Add("Accno");
                DT.Columns.Add("TranAmt");
                DT.Columns.Add("Blank1");
                DT.Columns.Add("Blank2");
                DT.Columns.Add("Entrydate");
                DT.Columns.Add("Blank3");

            }

            else
            {
                DT.Columns.Add("Entrydate");
                DT.Columns.Add("Accno");
                DT.Columns.Add("TranAmt");
                DT.Columns.Add("Receiptno");
                DT.Columns.Add("ReceiptPrint");
                DT.Columns.Add("TransferT");
            }

            DT2.Columns.Add("ID");
            DT2.Columns.Add("NAME");
            DT2.Columns.Add("ACCNO");
            DT2.Columns.Add("AMOUNT");
            DT2.Columns.Add("REMARK");

            Result = DR.InsertData_Mob(Session["EntryDate"].ToString(), Session["BRCD"].ToString(), TxtAgentNo.Text);


            if (Result > 0)
            {
                DT3 = DR.GetAccMaster(Session["BRCD"].ToString(), TxtAgentNo.Text, Session["EntryDate"].ToString(), "INV");
                if (DT3.Rows.Count > 0)
                {
                    //DT2.Rows.Add(DT3.Rows);
                    foreach (DataRow DRow in DT3.Rows)
                    {
                        DT2.ImportRow(DRow);
                    }
                }


                BindGrid();
                BindInvalidGrid();
                TxttotalAmt.Text = DR.GetAgentColMob(TxtAgentNo.Text, Session["BRCD"].ToString());
                TxtDiffrence.Text = (Convert.ToDouble(TxtAGAmt.Text) - Convert.ToDouble(TxttotalAmt.Text)).ToString();

                WebMsgBox.Show("Uploading completed..!", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_" + TxtAgentNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                return;
            }
            else
            {
                WebMsgBox.Show("Sorry Data Alredy Uploaded", this.Page);
                return;
            }


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }

    }
    protected void TxtAgentName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AGNAME = TxtAgentName.Text;
            if (AGNAME != null)
            {
                string[] AgentCode = AGNAME.Split('_');
                if (AgentCode.Length > 1)
                {
                    TxtAgentNo.Text = AgentCode[0].ToString();
                    TxtAgentName.Text = (string.IsNullOrEmpty(AgentCode[1].ToString()) ? "" : AgentCode[1].ToString());
                    string[] TD = Session["EntryDate"].ToString().Split('/');
                    string SGL = DR.GetSubgLc(Session["BRCD"].ToString());
                    TxtAGAmt.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), SGL, TxtAgentNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "6").ToString();
                    FileUPControl.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Invalid AgentName....!", this.Page);
                TxtAgentName.Text = "";
                TxtAgentName.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {

    }
    public void BindPCRX()
    {
        DataTable tblcsv = new DataTable();
        //creating columns  
        tblcsv.Columns.Add("No");
        tblcsv.Columns.Add("City");
        tblcsv.Columns.Add("Name");
        tblcsv.Columns.Add("Designation1");
        tblcsv.Columns.Add("Date");
        tblcsv.Columns.Add("Designation");
        //getting full file path of Uploaded file  
        string CSVFilePath = Path.GetFullPath("C:\\Balaji\\pcrx.DAT");
        //Reading All text  
        string ReadCSV = File.ReadAllText(CSVFilePath);
        //spliting row after new line  
        foreach (string csvRow in ReadCSV.Split('\n'))
        {
            if (!string.IsNullOrEmpty(csvRow))
            {
                //Adding each row into datatable  
                tblcsv.Rows.Add();
                int count = 0;
                foreach (string FileRec in csvRow.Split(','))
                {
                    tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;
                    count++;
                }
            }
            //Calling Bind Grid Functions  
            Bindgrid(tblcsv);

        }
    }
    public void Bindgrid(DataTable csvdt)
    {
        GridPCRX.DataSource = csvdt;
        GridPCRX.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Runbat1()", true);
        BindPCRX();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
    }
    public void readtxt1()
    {
        DbConnection conn = new DbConnection();
        try
        {
            lblprocess.Text = "";
            string delimeter = ",";
            string PRAMB;
            string path = Path.GetFileName("C:\\Balaji\\pcrx.DAT");

            int RC = 0;

            if (path == "")
            {
                WebMsgBox.Show("Sorry File Not Found", this.Page);
                return;
            }
            string[] ValidTypes = { "DAT", "dat" };
            // string Exten = System.IO.Path.GetExtension(FileUPControl.PostedFile.FileName);
            if (File.Exists(Server.MapPath("~/Uploads/" + path)))
            {
                File.Delete(Server.MapPath("~/Uploads/" + path));
            }
            System.IO.File.Copy("C:\\Balaji\\pcrx.DAT", Server.MapPath("~/Uploads/") + path);


            string fileName = Server.MapPath("~/Uploads/" + path);
            DataTable DT = new DataTable();

            StreamReader sr = new StreamReader(fileName);

            string sql = "SELECT LISTVALUE FROM PARAMETER WHERE LISTFIELD='BalajiM' AND BRCD=" + Session["BRCD"];
            PRAMB = conn.sExecuteScalar(sql);
            string allData = sr.ReadToEnd();
            //lblprocess.Text = "Creating Temp Table.....";
            if (PRAMB == "Y")
            {

                DT.Columns.Add("Accno");
                DT.Columns.Add("TranAmt");
                DT.Columns.Add("Blank1");
                DT.Columns.Add("Blank2");
                DT.Columns.Add("Entrydate");
                DT.Columns.Add("Blank3");

            }

            else
            {
                DT.Columns.Add("Entrydate");
                DT.Columns.Add("Accno");
                DT.Columns.Add("TranAmt");
                DT.Columns.Add("Receiptno");
                DT.Columns.Add("ReceiptPrint");
                DT.Columns.Add("TransferT");
            }

            DT2.Columns.Add("ID");
            DT2.Columns.Add("NAME");
            DT2.Columns.Add("ACCNO");
            DT2.Columns.Add("AMOUNT");
            DT2.Columns.Add("REMARK");

            string[] rows = allData.Split("\r\n".ToCharArray());
            int count = 0;
            int ij = 0;
            foreach (string r in rows)
            {

                if (ij == 0)
                {
                    ij++;
                    continue;
                }
                string[] items = r.Split(delimeter.ToCharArray());
                string ACN = items[0].ToString().Trim();
                int value;
                if (int.TryParse(ACN, out value)) // Added for Pen PCRX File requirement --Abhishek 09/06/2017
                {
                    RESULT = DR.CheckACValid(Session["BRCD"].ToString(), ACN.ToString(), TxtAgentNo.Text);
                    // if (RESULT!=null && Convert.ToInt32(RESULT) !=Convert.ToInt32(ACN) && items.Length > 5 )

                    if (items.Length > 5 && Convert.ToInt32(RESULT) > 0 && RESULT != null)
                    {
                        DT.Rows.Add(items);
                    }

                    if (Convert.ToInt32(RESULT) == 0 && items.Length > 5 && RESULT != null)
                    {
                        count++;
                        DT2.Rows.Add(count, items[2].Trim(), items[0], items[1], "A/C Not Created");
                    }
                }

                lblprocess.Text = "";
                lblprocess.Text = "For Record No " + ij + "....";
                ij++;
            }

            if (File.Exists(Server.MapPath("~/Uploads/" + path)))
            {
                sr.Close();
                //File.Delete(Server.MapPath("~/Uploads/" + path));
            }

            sr.Close();
            Result = DR.InsertData(TxtAgentNo.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());

            if (Result > 0)
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    if (PRAMB != "Y")
                    {

                        RC = DR.UpdateAFTData(TxtAgentNo.Text, Session["EntryDate"].ToString(), conn.ConvertToDDSPostDate(DT.Rows[i]["Entrydate"].ToString().Replace(".", "/")), DT.Rows[i]["Accno"].ToString(), Session["BRCD"].ToString(), DT.Rows[i]["Entrydate"].ToString(), DT.Rows[i]["TranAmt"].ToString(), "CHGAMT");
                    }
                    else if (PRAMB == "Y")
                    {

                        RC = DR.UpdateAFTData(TxtAgentNo.Text, Session["EntryDate"].ToString(), conn.ConvertToDDSPostDate(DT.Rows[i]["Entrydate"].ToString().Replace(".", "/")), DT.Rows[i]["Accno"].ToString(), Session["BRCD"].ToString(), DT.Rows[i]["TranAmt"].ToString(), DT.Rows[i]["Entrydate"].ToString(), "CHGAMT");
                        if (RC > 0)
                            continue;
                        else
                        {
                            break;
                        }

                    }
                }
                BindGrid();
                BindInvalidGrid();
                TxttotalAmt.Text = DR.GetAgentCol(TxtAgentNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString());
                TxtDiffrence.Text = (Convert.ToDouble(TxtAGAmt.Text) - Convert.ToDouble(TxttotalAmt.Text)).ToString();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_" + TxtAgentNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }

    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            DbConnection conn = new DbConnection();
            sql = "Select * from  AFTEKR where agentcode='" + TxtAgentNo.Text + "' and brcd='" + Session["BRCD"].ToString() + "'";
            DataTable DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                sql = "Delete from AFTEKR where agentcode='" + TxtAgentNo.Text + "' and brcd='" + Session["BRCD"].ToString() + "'";
                Result = conn.sExecuteQuery(sql);
                if (Result > 0)
                {
                    readtxt1();
                    BindGrid();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_" + TxtAgentNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
            }
            else
            {
                //TxtAGAmt.Text = DR.GetAmount(TxtAgentNo.Text, Session["EntryDate"].ToString(), Session["BRCD"].ToString()); GET AGENT TODAYS AMOUNT
                readtxt1();
                BindGrid();
            }
            DataTable dt1 = new DataTable();
            ClsAuthorized cac = new ClsAuthorized();
            string PARTICULARS = "Daily Collection";
            string PARTICULARS2 = "";
            string PMTMODE = "TR-DCM";
            string INSTRUMENTDATE = "01/01/1900";
            string PCMAC = conn.PCNAME();
            int TRXTYPE;
            string custno;
            string custname;
            string SETNO = "";
            string SCROLLNO = "";
            if (Convert.ToInt32(TxtDiffrence.Text) >= 0)
            {
                if (Grd_NoAcc.Rows.Count == 0 || Rdb_Upload.SelectedValue == "1")
                {
                    SETNO = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                    if (Rdb_Upload.SelectedValue == "1")
                    {
                        dt1 = DR.GetPostingData(TxtAgentNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "");
                    }
                    else
                    {
                        dt1 = DR.GetPostingData(TxtAgentNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "MOB");
                    }


                    for (int i = 0; i < dt1.Rows.Count; i++) // Posting date added for tracking entry --Abhishek 21-06-2017
                    {
                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "2", TxtAgentNo.Text, dt1.Rows[i]["ACNO"].ToString(), PARTICULARS, PARTICULARS2, dt1.Rows[i]["TRANSAMT"].ToString(), "1", "30", PMTMODE.ToString(), SETNO.ToString(), "0", INSTRUMENTDATE.ToString(), "0", Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), " 0", "0", " 0", "0", TxtAgentName.Text, "0", "0");
                        if (Result > 0)
                        {
                        }
                        lblprocess.Text = "Acc No :" + dt1.Rows[i]["ACNO"].ToString() + ".......";
                    }
                    if (Result > 0)
                    {
                        string SUBGL = BD.GetCashGl("6", Session["BRCD"].ToString());

                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "6", SUBGL.ToString(), TxtAgentNo.Text, PARTICULARS, PARTICULARS2, TxttotalAmt.Text, "2", "30", PMTMODE.ToString(), SETNO.ToString(), "0", INSTRUMENTDATE.ToString(), "0", Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), " 0", "0", " 0", "0", TxtAgentName.Text, "0", "0");
                        if (Result > 0)
                        {
                        }

                        if (Rdb_Upload.SelectedValue == "2") //if mobile upload selected then Update ALLVCR stage to 1003
                        {
                            Result = DR.UpdateALLVCRStage(Session["BRCD"].ToString(), TxtAgentNo.Text);
                        }
                        lblMessage.Text = "Record Post Successfully and Voucher No Is " + SETNO;
                        int RC = DR.UpdateSetNo("7", SETNO);
                        ChekcEntry();
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    }
                }
                else
                {
                    lblMessage.Text = "Before Posting Create the Invalid Accounts....!";
                    ModalPopup.Show(this.Page);
                    return;
                }

            }
            else
            {
                lblMessage.Text = "";
                lblMessage.Text = "Agent Balance Insufficient, First Credit the Amount..!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void btnUpload_Click2(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = DR.getAllVCRData(TxtAgentNo.Text, Session["BRCD"].ToString(), Session["BNKCDE"].ToString(), Session["EntryDate"].ToString(), "1");
            //insert into m & allvcr
            DR.InsertAllVCRData(dt);
            DataTable DT = new DataTable();
            DT = DR.getAllVCRData(TxtAgentNo.Text, Session["BRCD"].ToString(), Session["BNKCDE"].ToString(), Session["EntryDate"].ToString(), "1");
            DR.InsertMTABLlE(dt, Session["EntryDate"].ToString());
            DT = DR.getAllVCRData(TxtAgentNo.Text, Session["BRCD"].ToString(), Session["BNKCDE"].ToString(), Session["EntryDate"].ToString(), "2");
            WebMsgBox.Show("Upload Successfully!!!", this.Page);
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_" + TxtAgentNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }


    protected void BtnAkytUpload_Click(object sender, EventArgs e)
    {
        try
        {

            Akyt_OperateFile();


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Akyt_OperateFile()
    {
        try
        {
            string Prdcode = "", Accno = "", Glcode = "";
            if (FileUPControl.HasFile || FileUPControl.PostedFile != null)
            {
                lblprocess.Text = "";
                string delimeter = ",";
                string PRAMB;
                string path = Path.GetFileName(FileUPControl.FileName);

                int RC = 0;

                if (path == "")
                {
                    WebMsgBox.Show("Sorry File Not Found", this.Page);
                    return;
                }
                string[] ValidTypes = { "DAT", "dat" };
                string Exten = System.IO.Path.GetExtension(FileUPControl.PostedFile.FileName);
                bool Valid = false;
                for (int i = 0; i < ValidTypes.Length; i++)
                {
                    if (Exten == "." + ValidTypes[i])
                    {
                        Valid = true;
                        break;
                    }
                }
                if (Valid)
                {
                    FileUPControl.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + path);

                    string fileName = Server.MapPath("~/Uploads/" + path);
                    DataTable DT = new DataTable();

                    StreamReader sr = new StreamReader(fileName);

                    string sql = "SELECT LISTVALUE FROM PARAMETER WHERE LISTFIELD='BalajiM' AND BRCD=" + Session["BRCD"];
                    PRAMB = conn.sExecuteScalar(sql);
                    string allData = sr.ReadToEnd();


                    DT.Columns.Add("Accno");
                    DT.Columns.Add("TranAmt");
                    DT.Columns.Add("Blank1");
                    DT.Columns.Add("Blank2");
                    DT.Columns.Add("Entrydate");
                    DT.Columns.Add("Blank3");

                    DT2.Columns.Add("ID");
                    DT2.Columns.Add("NAME");
                    DT2.Columns.Add("ACCNO");
                    DT2.Columns.Add("AMOUNT");
                    DT2.Columns.Add("REMARK");


                    string[] rows = allData.Split("\r\n".ToCharArray());
                    int count = 0;
                    int ij = 0;
                    foreach (string r in rows)
                    {

                        if (ij == 0)
                        {
                            ij++;
                            continue;
                        }
                        string[] items = r.Split(delimeter.ToCharArray());
                        string ACN = items[0].ToString().Trim();

                        int value;
                        string RDSubgl="";
                        if (Session["BRCD"].ToString() == "1")
                        {
                            RDSubgl = "60";
                        }
                        else if(Session["BRCD"].ToString() == "2")
                        {
                            RDSubgl = "30";
                        }

                        if (int.TryParse(ACN, out value)) // Added for Pen PCRX File requirement --Abhishek 09/06/2017
                        {

                            Prdcode = items[0].Substring(0, 2);
                            if (Prdcode == "20")
                            {
                                Prdcode = "1";
                                Glcode = "1";
                            }
                            else if (Prdcode == RDSubgl)
                            {
                                Prdcode = "303";
                                Glcode = "5";
                            }
                            else if (Prdcode == "00")
                            {
                                Prdcode = TxtAgentNo.Text;
                                Glcode = "2";
                            }

                            ACN = items[0].Substring(2, 4).ToString();
                            RESULT = DR.CheckACValid(Session["BRCD"].ToString(), ACN.ToString(), Prdcode);


                            if (items.Length > 5 && Convert.ToInt32(RESULT) == 1)
                            {
                                Result = DR.Am_Operations("Insert", items[2].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), conn.ConvertToDDSPostDateAKYT(items[4].ToString()), Glcode, Prdcode, ACN.ToString(), items[1].ToString(), TxtAgentNo.Text, Session["MID"].ToString(), Glcode.ToString());
                            }

                            if (Convert.ToInt32(RESULT) == 0)
                            {
                                count++;
                                DT2.Rows.Add(count, items[2].Trim(), items[0], items[1], "A/C Not Created");
                                Result = DR.Am_Operations("Insert_Invalid", items[2].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), conn.ConvertToDDSPostDateAKYT(items[4].ToString()), Glcode, Prdcode, ACN, items[1].ToString(), TxtAgentNo.Text, Session["MID"].ToString(), Glcode.ToString());

                            }
                        }

                        lblprocess.Text = "";
                        lblprocess.Text = "For Record No " + ij + "....";
                        ij++;
                    }

                    if (File.Exists(Server.MapPath("~/Uploads/" + path)))
                    {
                        sr.Close();
                        //File.Delete(Server.MapPath("~/Uploads/" + path));
                    }

                    sr.Close();
                  
                    DR.Am_BindGrid(gridadd, "ShowGrid", Session["BRCD"].ToString(), Session["MID"].ToString());

                    BindInvalidGrid();

                    TxttotalAmt.Text = DR.Am_SumAmount("SumAMount", Session["BRCD"].ToString(), Session["MID"].ToString());
                    TxtDiffrence.Text = (Convert.ToDouble(TxtAGAmt.Text) - Convert.ToDouble(TxttotalAmt.Text)).ToString();

                    WebMsgBox.Show("Uploading Completed..!", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Machine-DB-PC_UPLOAD_" + TxtAgentNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;

                }
                else
                {
                    WebMsgBox.Show("WARNING : File Extension is not Supported....!", this.Page);
                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Browse the file First.....", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }



    protected void BtnAkytPosting_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(TxtDiffrence.Text) >= 0)
            {
                string RS = DR.Am_PostEntry("Post", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), TxtAgentNo.Text, Session["MID"].ToString());
                if (RS != null)
                {
                    WebMsgBox.Show("Posted Sucessfully with Setno - " + RS + "...!", this.Page);
                    ClearData();

                }
                else
                {
                    WebMsgBox.Show("Posting Failed...!", this.Page);
                }
            }
            else
            {
                WebMsgBox.Show("Agent Balance is low,Posting Aborted...!", this.Page);
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ClearData()
    {
        TxtAgentNo.Text = "";
        TxtAgentName.Text = "";
        TxtAGAmt.Text = "";
        TxttotalAmt.Text = "";
        TxtDiffrence.Text = "";
        Grd_NoAcc.DataSource = null;
        Grd_NoAcc.DataBind();

        gridadd.DataSource = null;
        gridadd.DataBind();
        TxtAgentNo.Focus();

    }
    protected void BtnClearUpload_Click(object sender, EventArgs e)
    {
        try
        {
            int RR = DR.Am_Manipulate("Delete", Session["BRCD"].ToString(), Session["MID"].ToString());
            if (RR >= 0)
            {
                WebMsgBox.Show("Upload canceled successfully....!", this.Page);
                ClearData();
            }
            else
            {
                WebMsgBox.Show("Data not available....!", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion

    #region DDS PC to DB File 
    protected void Btn_Run_Exe_Click(object sender, EventArgs e)
    {
        try
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = @"E:/DDL/ALT_AVSCORE/Balaji/PC2DB.exe ";
            //start and wait for the exe the run to completion changes by ashok
            process.Start();
            process.WaitForExit();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Create_Click(object sender, EventArgs e)
    {
        try
        {
            CreateTextFile();
            lblMessage.Text = "File Create Sussessfully.. Download the File!";
            ModalPopup.Show(this.Page);
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "PC-DB-Created_" + TxtAgentNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void CreateTextFile()
    {
        try
        {
            DataTable DT = new DataTable();
            string ZB = "";
            if (Chk_ZeroBal.Checked == true)
                ZB = "ZB_Y";
            else
                ZB = "ZB_N";

            DT = DR.GetWritePCTX(Session["BRCD"].ToString(), TxtAgentNo.Text, "2", Session["EntryDate"].ToString(), "DDSCT", ZB);
            string path1 = "PCRT.dat";
            string[] EDT1;
            string EDT2 = "";
            EDT1 = Session["EntryDate"].ToString().Split('/');
            EDT2 = EDT1[0].ToString() + "." + EDT1[1].ToString() + "." + EDT1[2].ToString();

            // string DPath = @"D:\Balaji";
            string pp = Request.PhysicalApplicationPath;
            string path = pp + path1;

            string DateF = "";
            string SFilename = "";
            Response.Clear();
            Response.Buffer = true;
            DateF = DateTime.Now.ToShortDateString();
            SFilename = DateF;

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=pctx.dat");
            Response.Charset = "";
            Response.ContentType = "application/octet-stream";
           
            if (DT.Rows.Count > 0)
            {
                string STR = "";
                string DRT = "";
                foreach (DataRow Dr in DT.Rows)
                {
                    STR = "";
                    for (int i = 0; i <= DT.Columns.Count - 1; i++)
                    {
                        if (i == 3)
                        {
                            double TB1 = Math.Round(Convert.ToDouble(Dr[i].ToString()), 0);
                            // int TB = Convert.ToInt32(TB1);
                            var a = (int)Convert.ToDouble(Dr[i].ToString());
                            int TB = Convert.ToInt32(a);

                            string BAL = "";
                            BAL = TB.ToString();
                            if (BAL.Length == 1)
                            {
                                BAL = "00000" + BAL;
                            }
                            else if (BAL.Length == 2)
                            {
                                BAL = "0000" + BAL;
                            }
                            else if (BAL.Length == 3)
                            {
                                BAL = "000" + BAL;
                            }
                            else if (BAL.Length == 4)
                            {
                                BAL = "00" + BAL;
                            }
                            else if (BAL.Length == 5)
                            {
                                BAL = "0" + BAL;
                            }
                            DRT = BAL;
                        }
                        else if (i == 4)
                        {
                            DRT = Dr[i].ToString().Replace("/", ".");
                        }
                        else
                        {
                            DRT = Dr[i].ToString();
                        }
                        if (i == DT.Columns.Count - 1)
                            STR = STR + DRT;
                        else
                            STR = STR + DRT + ",";
                    }

                    using (StringWriter writer = new StringWriter())
                    {
                        writer.WriteLine(STR);

                        writer.Close();
                        Response.Output.Write(writer);
                    }

                }
            }


           

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();



          


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    #endregion
}