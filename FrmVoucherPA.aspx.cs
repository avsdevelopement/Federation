using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public partial class FrmVoucherPA : System.Web.UI.Page
{
    ClsVoucherAutho VA = new ClsVoucherAutho();
    ClsAuthoVoucher VA1 = new ClsAuthoVoucher();
    ClsAuthorized AT = new ClsAuthorized();
    ClsOpenClose OC = new ClsOpenClose();
    ClsBindDropdown BD = new ClsBindDropdown();
    Mobile_Service mob = new Mobile_Service();
    DataTable DT = new DataTable();
   // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ocbs"].ToString());
    DbConnection con1 = new DbConnection();
    DbConnection conn = new DbConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["STNO"] = Request.QueryString["setno"].ToString();
            ViewState["SR"] = Request.QueryString["sr"].ToString();
            GetData();
           // ShowImage();
        }
    }
    public void GetData()
    {
        DT = new DataTable();
        DT = VA.VGetInfo(Session["BRCD"].ToString(), ViewState["STNO"].ToString(), ViewState["SR"].ToString(),Session["EntryDate"].ToString());
        if (DT.Rows.Count > 0)
        {
            txtnaration.Text = DT.Rows[0]["PARTICULARS"].ToString();
            if (DT.Rows[0]["PARTICULARS"].ToString() == "Daily Collection")
            {
                DataTable dt1=new DataTable ();
               dt1 = VA.GetTotalSetAmt(ViewState["STNO"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString());//Amruta 21/12/2017 for machine upload set for Pen
               txtamountt.Text = dt1.Rows[0]["Amount"].ToString();
                TxtProcode.Text = dt1.Rows[0]["SUBGLCODE"].ToString();
                TxtProName.Text = dt1.Rows[0]["GLNAME"].ToString();
                TxtMakName.Text = dt1.Rows[0]["MID"].ToString();
                TxtSetno.Text = ViewState["STNO"].ToString();
                ViewState["CT"] = dt1.Rows[0]["CUSTNO"].ToString();
                TxtPCMAC.Text = dt1.Rows[0]["pcMAC"].ToString();
                TxtEntrydate.Text = dt1.Rows[0]["EntryDate"].ToString();
                TxtAccNo.Text = dt1.Rows[0]["ACCNO"].ToString();
                TxtAccName.Text = dt1.Rows[0]["ACCNAME"].ToString();
                string[] TD = TxtEntrydate.Text.Split('/');
                txtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), dt1.Rows[0]["SUBGLCODE"].ToString(), TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), dt1.Rows[0]["glcode"].ToString()).ToString();
            }
            else
            {
                txtamountt.Text = DT.Rows[0]["AMT"].ToString(); //DT.Rows[0]["CREDIT"].ToString() == "0" ? DT.Rows[0]["DEBIT"].ToString() : DT.Rows[0]["CREDIT"].ToString();
                TxtProcode.Text = DT.Rows[0]["SUBGLCODE"].ToString();
                TxtProName.Text = DT.Rows[0]["GLNAME"].ToString();
                TxtMakName.Text = DT.Rows[0]["MID"].ToString();
                TxtSetno.Text = ViewState["STNO"].ToString();
                ViewState["CT"] = DT.Rows[0]["CUSTNO"].ToString();
                TxtPCMAC.Text = DT.Rows[0]["pcMAC"].ToString();
                TxtEntrydate.Text = DT.Rows[0]["EntryDate"].ToString();
                TxtAccNo.Text = DT.Rows[0]["ACCNO"].ToString();
                TxtAccName.Text = DT.Rows[0]["CUSTNAME"].ToString();
            }
            if (TxtMakName.Text == Session["LOGINCODE"].ToString())
            {
                lblMessage.Text = "Sorry Maker Cant not Authorised Record.......!!";
                ModalPopup.Show(this.Page);
                //btnSubmit.Enabled = false;
                return;
            }
            else
            {
                //Commented by Abhihsek on 22-01-2018 due to Connection reset ,timeout error (Taking more time to get data and set not authorizing) 

                //DT = new DataTable();
                //string OpDate = OC.GetAccOpenDate(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString());

                //DT = GetAccStatDetails(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), OpDate);

                //if (DT.Rows.Count > 0)
                //{
                //    grdAccStatement.DataSource = DT;
                //    grdAccStatement.DataBind();
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
                //}
                //else
                //{
                //    lblMessage.Text = "Details Not Found For This Account Number...!!";
                //    ModalPopup.Show(this.Page);
                //}
            }
        }
    }

    public DataTable GetAccStatDetails(string BrCode, string PrCode, string AccNo, string FinDate)
    {
        try
        {
            DT = new DataTable();
            string[] DTF, DTT;

            DTF = FinDate.ToString().Split('/');
            DTT = Session["EntryDate"].ToString().Split('/');

            DT = OC.GetAccStatDetails(DTF[1].ToString(), DTT[1].ToString(), DTF[2].ToString(), DTT[2].ToString(), FinDate.ToString(), Session["EntryDate"].ToString(), AccNo.ToString(), PrCode.ToString(), Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string encrypt(string encryptString)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }

    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }  

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int RC = 0;//amruta 10/06/2017 start
            
            DT = AT.GetPayMast(Session["BRCD"].ToString(), TxtSetno.Text, Session["MID"].ToString(), Session["EntryDate"].ToString());

            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["MID"].ToString() != Session["MID"].ToString())
                {
                    RC = VA1.AuthoriseEntry(Session["BRCD"].ToString(), TxtSetno.Text, Session["MID"].ToString(), Session["EntryDate"].ToString());
                }
                else
                {
                    WebMsgBox.Show("Not allow for same user...!!", this.Page);
                }
            }
            else//amruta 10/06/2017 end
            {  ///added by ashok misal 20/11/2017 for Issue of Daily Collection and also for other entries
                DT = AT.GetPayMastForDaily(Session["BRCD"].ToString(), TxtSetno.Text, Session["MID"].ToString(), Session["EntryDate"].ToString());
                if (DT.Rows[0]["MID"].ToString() != Session["MID"].ToString())
                {
                    RC = AT.AuthoriseEntry(Session["BRCD"].ToString(), TxtSetno.Text, Session["MID"].ToString(), Session["EntryDate"].ToString(), "", "");
                }
                else
                {
                    WebMsgBox.Show("Not allow for same user...!!", this.Page);
                }
            }
            
            if (RC > 0)
            {
                //Below commented by Abhishek -Purpose - taking 35 Sec to authorize 168 rows 25-01-2018

                //#region Change by Amruta 03/06/2017
                //DataTable Dt1 = new DataTable();
                //Dt1 = AT.GetAmount(Session["BRCD"].ToString(), TxtSetno.Text, Session["EntryDate"].ToString());
                //if (Dt1.Rows.Count > 0)
                //{
                //    for (int i = 0; i < Dt1.Rows.Count; i++)
                //    {
                //        string str = "";
                //        str = Dt1.Rows[i]["Amount"].ToString();
                //        str = encrypt(str);
                //        byte[] str1 = Encoding.ASCII.GetBytes(str);
                //        AT.UpdateAmount(Dt1.Rows[i]["AID"].ToString(), Dt1.Rows[i]["SubGLCODE"].ToString(), Dt1.Rows[i]["TRXTYPE"].ToString(), Dt1.Rows[i]["Amount"].ToString(), str, ViewState["SETNO"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
                //        string[] TD1 = Session["EntryDate"].ToString().Split('/');
                //        string TBNAME = "";
                //        TBNAME = TD1[2].ToString() + TD1[1].ToString();
                //        SqlCommand cmd = new SqlCommand("update AVSM_" + TBNAME + " set AMOUNT_2=@Amount_2 where SETNO = @SetNo AND BrCd = @BRCD AND  ENTRYDATE =@EDAT and AID=@AID and SUBGLCODE=@SUBGL and TRXTYPE=@TRX and Amount=@AMOUNT", con);

                //        cmd.Parameters.Add("@Amount_2", SqlDbType.Binary).Value = str1;
                //        cmd.Parameters.Add("@SetNo", SqlDbType.VarChar).Value = TxtSetno.Text;
                //        cmd.Parameters.Add("@BRCD", SqlDbType.Int).Value = Session["BRCD"].ToString();
                //        cmd.Parameters.Add("@AID", SqlDbType.Float).Value = Convert.ToDouble(Dt1.Rows[i]["AID"]);
                //        cmd.Parameters.Add("@EDAT", SqlDbType.DateTime).Value = conn.ConvertDate(Session["EntryDate"].ToString());
                //        cmd.Parameters.Add("@SUBGL", SqlDbType.VarChar).Value = Dt1.Rows[i]["SubGLCODE"].ToString();
                //        cmd.Parameters.Add("@TRX", SqlDbType.VarChar).Value = Dt1.Rows[i]["TRXTYPE"].ToString();
                //        cmd.Parameters.Add("@AMOUNT", SqlDbType.Float).Value = Convert.ToDouble(Dt1.Rows[i]["Amount"]);


                //        con.Open();
                //        cmd.ExecuteNonQuery();
                //        con.Close();
                //    }
                //}
                //byte[] sd1 = (byte[])Dt1.Rows[0]["Amount_2"];
                //string sd = Encoding.ASCII.GetString(sd1);
                //Decrypt(sd);
                //#endregion

                //string GLS1 = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
                //string[] GLS = GLS1.Split('_');
                //string[] TD = Session["EntryDate"].ToString().Split('/');
                //double BAL = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GLS[1].ToString());
                WebMsgBox.Show("Record Authorized successfully", this.Page);

                ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void PhotoSign_Click(object sender, EventArgs e)
    {
        string url = "FrmAPhotoSign.aspx?CT=123";
        NewWindows(url);
    }
    public void NewWindows(string url)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=1000,height=400,left=50,top=50,resizable=no');", true);
    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["photodb"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
    public void ShowImage()
    {
        try
        {
            DataTable dt = new DataTable();
            DataTable CN = new DataTable();
            CN = VA.GetCustName(TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString());
            if (CN.Rows.Count > 0)
            {
                string[] CustName = CN.Rows[0]["CustName"].ToString().Split('_');
                dt = GetData("select id,SignName,PhotoName,SignIMG,PhotoImg from  Imagerelation where BRCD='" + Session["BRCD"].ToString() + "' and CustNo=" + CustName[1].ToString() + " and AccNo='" + TxtAccNo.Text + "'");
            }
            ////string SaveLocation = Server.MapPath("~/Uploads/");
            ////string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads/")); //Get File List in chosen directory
            ////List<ListItem> files = new List<ListItem>();
            string FileName = "";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > Convert.ToInt32(hdnRow.Value))
                {

                    int i = Convert.ToInt32(hdnRow.Value);
                    String FilePath = "";
                    byte[] bytes = null;
                    for (int y = 0; y < 2; y++)
                    {
                        if (y == 0)
                        {
                            FilePath = dt.Rows[i]["SignIMG"].ToString();
                            if (FilePath != "")
                                bytes = (byte[])dt.Rows[i]["SignIMG"];

                        }
                        else
                        {
                            FilePath = dt.Rows[i]["PhotoImg"].ToString();
                            if (FilePath != "")
                                bytes = (byte[])dt.Rows[i]["PhotoImg"];
                        }
                        if (FilePath != "")
                        {
                            ////FileInfo file = new FileInfo(Server.MapPath("~/Uploads/"+FilePath + ".jpg")); //get individual file info
                            ////FileName = Path.GetFileName(file.FullName);  //output individual file name
                            ////string EX = Path.GetExtension(FileName);
                            ////FileName = FileName.Replace(EX, "");
                            ////string input = Server.MapPath("~/Uploads/" + FileName + EX);
                            ////string output = Server.MapPath("~/Uploads/" + FileName + "_dec" + EX);
                            ////this.Decrypt(input, output);
                            ////string base64String;
                            ////using (System.Drawing.Image image = System.Drawing.Image.FromFile(output))
                            ////{
                            ////    using (MemoryStream m = new MemoryStream())
                            ////    {
                            ////        image.Save(m, image.RawFormat);
                            ////        byte[] imageBytes = m.ToArray();
                            ////        base64String = Convert.ToBase64String(imageBytes);
                            ////    }
                            ////}
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            //base64String = Base64Decode(base64String);
                         
                                if (y == 0)
                                {
                                    Img7.Src = "data:image/tif;base64," + base64String;
                                }
                                else if (y == 1)
                                {
                                    Img8.Src = "data:image/tif;base64," + base64String;
                                }

                            //File.Delete(output);
                        }
                        else
                        {

                            if (y == 0)
                            {
                                Img7.Src = "";

                            }
                            else if (y == 1)
                            {
                                Img8.Src = "";
                            }
                        }

                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
            }
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }
}