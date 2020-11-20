using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using System.Security.Cryptography;
using System.Collections;
using Ionic.Zip;


public partial class frmAVS5140 : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();

    string EntryDate = "";
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
            }
            EntryDate = Session["EntryDate"].ToString();
           // process();
           // Response.Redirect(Request.UrlReferrer.ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    //protected void btnSubmit_Click(object sender, EventArgs e)
    public void process()
    {
        try
        {
          
            //if (txtStartDate.Text.Trim() == "" || txtTime.Text.Trim() == "")
            //{
            //    WebMsgBox.Show("Please enter Start Date and Time", this.Page);
            //    return;
            //}
            //string StartDate = txtStartDate.Text.Trim();
            //string time = txtTime.Text.Trim();
            ArrayList tbName = new ArrayList();

            //tbName.Add("A50001");
            tbName.Add("ADDMAST-1");

            //tbName.Add("AGENTMAST");
            tbName.Add("ALLVCR-2");
            //tbName.Add("ALOGIN");
            tbName.Add("AVS_ACC-3");

            tbName.Add("AVS_DDS-4");

           // tbName.Add("AVS_DDS_LOCK");
            //tbName.Add("AVS_DDSHISTORY");
            tbName.Add("Avs_FreezAccDetails-5");
            //tbName.Add("AVS_InvAccountMaster");
            //tbName.Add("AVS_InvDepositeMaster");
            //tbName.Add("AVS_InvParameter");
            //tbName.Add("AVS_LienMarkDetails");
            //tbName.Add("AVS_LnTrx");

            //tbName.Add("Avs_Notice_Chrg");

            //tbName.Add("AVS_SHRALLOTMENT");
            //tbName.Add("AVS_SHRAPP");

            //tbName.Add("AVS1000");
            //tbName.Add("DEPOSITGL");
            tbName.Add("DEPOSITINFO-6");
            tbName.Add("LoanInfo-7");
            //tbName.Add("GLMAST");
            //tbName.Add("ImageMaster");
            //tbName.Add("ImageRelation");
            //tbName.Add("INTERESTMASTER");
            //tbName.Add("INTRESTMASTER");
            //tbName.Add("LIEN_MARK");
            //tbName.Add("LOANSCHEDULE");
            tbName.Add("MASTER-8");
            //tbName.Add("MEMSTAT");

            tbName.Add("SHARESINFO-9");



            //EntryDate = EntryDate;
            string StartDate1 = conn.ConvertDate(EntryDate).ToString();

            System.Collections.Generic.List<System.Web.UI.WebControls.ListItem> files = new System.Collections.Generic.List<System.Web.UI.WebControls.ListItem>();

            if (!Directory.Exists(@"C:\Backup"))
            {
                Directory.CreateDirectory(@"C:\Backup");
            }
            
            SqlConnection Conn = new SqlConnection(conn.DbName());
            try
            {
                foreach (string Tb in tbName)
                {
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();

                    string[] Tbn = Tb.Split('-');
                    string tbname = Tbn[0].ToString();


                    SqlDataAdapter sda = new SqlDataAdapter("ISP_AVS0179", Conn);
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = tbname;
                    sda.SelectCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = StartDate1;
                    sda.Fill(dt);

                    SqlDataAdapter sda1 = new SqlDataAdapter("ISP_AVS0180", Conn);
                    sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda1.SelectCommand.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = tbname;
                    sda1.Fill(dt1);
                    string[] typeArr = dt1.Rows.OfType<DataRow>().Select(k => k[1].ToString()).ToArray();


                    string fileName = Tbn[1] + "-" + DateTime.Now.Date.ToShortDateString() + DateTime.Now.ToShortTimeString();
                    fileName = fileName.Replace("/", "");
                    fileName = fileName.Replace("AM", "");
                    fileName = fileName.Replace("PM", "");
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace(":", "");



                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            StreamWriter sw;
                            using (sw = File.CreateText(@"C:\Backup\" + fileName + ".txt"))
                            {
                                string type;
                                int i;
                                int j = 0;
                                sw.Write("Insert Into " + Tb + " (");
                                for (i = 0; i < dt.Columns.Count; i++)
                                {
                                    if (i != dt.Columns.Count - 1)
                                    {
                                        sw.Write(dt.Columns[i].ColumnName + ",");
                                    }
                                    else
                                    {
                                        sw.Write(dt.Columns[i].ColumnName);

                                    }

                                }
                                sw.Write(") \r\n Values ");
                                foreach (DataRow row in dt.Rows)
                                {
                                    int RowCount = dt.Rows.Count;
                                    j += 1;
                                    sw.Write("(");
                                    object[] array = row.ItemArray;
                                    string value = string.Empty;
                                    for (i = 0; i < array.Length; i++)
                                    {
                                        type = typeArr[i];
                                        if (type == "float" || type == "bigint" || type == "numeric" || type == "int")
                                        {
                                            value = array[i].ToString();
                                            if (value.Trim() == "")
                                            {
                                                value = "null";
                                            }
                                            sw.Write(value);
                                        }
                                        else if (type == "nvarchar" || type == "varchar")
                                        {
                                            sw.Write("'" + array[i].ToString() + "'");

                                        }
                                        else if (type == "datetime2" || type == "datetime")
                                        {
                                            string date = array[i].ToString();
                                            if (date != "")
                                            {
                                                if (date.Contains("-"))
                                                {
                                                    string[] ab = date.Split('-');
                                                    string chk = ab[0];
                                                    if (chk.Length > 2)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        string date1 = ab[2];
                                                        string date2 = date1.Substring(0, 4);
                                                        date1 = date1.Substring(4);
                                                        date = date2 + "-" + ab[1] + "-" + ab[0] + " " + date1;
                                                    }
                                                }
                                                else if (date.Contains("/"))
                                                {
                                                    string[] ab = date.Split('/');
                                                    string chk = ab[0];
                                                    if (chk.Length > 2)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        string date1 = ab[2];
                                                        string date2 = date1.Substring(0, 4);
                                                        date1 = date1.Substring(4);
                                                        date = date2 + "-" + ab[1] + "-" + ab[0] + " " + date1;
                                                    }
                                                }

                                            }
                                            sw.Write("'" + date + "'");
                                        }
                                        else
                                        {
                                            sw.Write(array[i].ToString());
                                        }

                                        if (i != array.Length - 1)
                                        {
                                            sw.Write(",");
                                        }

                                    }

                                    if (j == RowCount)
                                    {
                                        sw.Write(") \r\n");
                                    }
                                    else
                                    {
                                        sw.Write("), \r\n");
                                    }
                                }
                            }
                            //StringBuilder SBTxt = new StringBuilder();                      

                            //SBTxt.Append(sw.ToString());
                            //StringBuilder SBTxt1 = new StringBuilder();
                            //SBTxt1.Append(encrypt(SBTxt.ToString()));
                            //sw.Close();



                            StreamReader sr = new StreamReader(@"C:\Backup\" + fileName + ".txt");
                            StringBuilder SBTxt = new StringBuilder();
                            SBTxt.Append(sr.ReadToEnd().ToString());
                            StringBuilder SBTxt1 = new StringBuilder();
                           // SBTxt1.Append(SBTxt.ToString());
                            SBTxt1.Append(encrypt(SBTxt.ToString()));
                            sr.Close();
                            var fs = new FileStream(@"C:\Backup\" + fileName + ".txt", FileMode.Truncate);
                            fs.Close();
                            StreamWriter sw1;
                            using (sw1 = File.CreateText(@"C:\Backup\" + fileName + ".txt"))
                            {
                                sw1.Write(SBTxt1.ToString());
                            }
                            sw1.Close();
                            files.Add(new System.Web.UI.WebControls.ListItem(@"C:\Backup\" + fileName + ".txt"));
                        }
                        catch (Exception Ex)
                        {
                            ExceptionLogging.SendErrorToText(Ex);
                        }

                        //StreamReader sr1 = new StreamReader(@"D:\MyDir\" + fileName + ".txt");
                        //StringBuilder SBTxt2 = new StringBuilder();
                        //SBTxt2.Append(sr1.ReadToEnd().ToString());
                        //StringBuilder SBTxt3 = new StringBuilder();
                        //SBTxt3.Append(Decrypt(SBTxt2.ToString()));
                        //sr1.Close();


                        


                        //Response.Clear();
                        //Response.ClearContent();
                        //Response.ClearHeaders();
                        //Response.Buffer = true;
                        //Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".txt");
                        //Response.Charset = "";
                        //Response.ContentType = "application/octet-stream";
                        //using (StringWriter writer = new StringWriter())
                        //{
                        //    writer.WriteLine(SBTxt1);
                        //    writer.Close();
                        //    Response.Output.Write(writer);
                        //}
                        //HttpContext.Current.Response.Flush();
                        //HttpContext.Current.Response.SuppressContent = true;
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();

                        //using (sw = File.CreateText(@"D:\MyDir\" + fileName + "Decrypt.txt"))
                        //{
                        //    sw.Write(SBTxt3.ToString());
                        //}
                        //sw.Close();
                    }

                }

                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    //zip.AddDirectoryByName("Files");
                    foreach (System.Web.UI.WebControls.ListItem li in files)
                    {
                        zip.AddFile(li.Value, "Your Files");
                    }
                    Response.Clear();
                    Response.BufferOutput = false;
                    string zipName = String.Format("Backup_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    zip.Save(Response.OutputStream);
                    //WebMsgBox.Show("BACKUP Successful", this.Page);
                    Response.End();
                }

                //WebMsgBox.Show("BACKUP Successful", this.Page);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
                //WebMsgBox.Show("Error while BACKUP", this.Page);
            }
            finally
            {
                Conn.Close();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void processWithDecrypt()
    {
        try
        {
            //if (txtStartDate.Text.Trim() == "" || txtTime.Text.Trim() == "")
            //{
            //    WebMsgBox.Show("Please enter Start Date and Time", this.Page);
            //    return;
            //}
            //string StartDate = txtStartDate.Text.Trim();
            //string time = txtTime.Text.Trim();
            ArrayList tbName = new ArrayList();

            //tbName.Add("A50001");
            tbName.Add("ADDMAST-1");

            //tbName.Add("AGENTMAST");
            tbName.Add("ALLVCR-2");
            //tbName.Add("ALOGIN");
            tbName.Add("AVS_ACC-3");

            tbName.Add("AVS_DDS-4");

            // tbName.Add("AVS_DDS_LOCK");
            //tbName.Add("AVS_DDSHISTORY");
            tbName.Add("Avs_FreezAccDetails-5");
            //tbName.Add("AVS_InvAccountMaster");
            //tbName.Add("AVS_InvDepositeMaster");
            //tbName.Add("AVS_InvParameter");
            //tbName.Add("AVS_LienMarkDetails");
            //tbName.Add("AVS_LnTrx");

            //tbName.Add("Avs_Notice_Chrg");

            //tbName.Add("AVS_SHRALLOTMENT");
            //tbName.Add("AVS_SHRAPP");

            //tbName.Add("AVS1000");
            //tbName.Add("DEPOSITGL");
            tbName.Add("DEPOSITINFO-6");
            tbName.Add("LoanInfo-7");
            //tbName.Add("GLMAST");
            //tbName.Add("ImageMaster");
            //tbName.Add("ImageRelation");
            //tbName.Add("INTERESTMASTER");
            //tbName.Add("INTRESTMASTER");
            //tbName.Add("LIEN_MARK");
            //tbName.Add("LOANSCHEDULE");
            tbName.Add("MASTER-8");
            //tbName.Add("MEMSTAT");

            tbName.Add("SHARESINFO-9");



            //EntryDate = EntryDate;
            string StartDate1 = conn.ConvertDate(EntryDate).ToString();

            System.Collections.Generic.List<System.Web.UI.WebControls.ListItem> files = new System.Collections.Generic.List<System.Web.UI.WebControls.ListItem>();

            if (!Directory.Exists(@"C:\Backup"))
            {
                Directory.CreateDirectory(@"C:\Backup");
            }

            SqlConnection Conn = new SqlConnection(conn.DbName());
            try
            {
                foreach (string Tb in tbName)
                {
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();

                    string[] Tbn = Tb.Split('-');
                    string tbname = Tbn[0].ToString();


                    SqlDataAdapter sda = new SqlDataAdapter("ISP_AVS0179", Conn);
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = tbname;
                    sda.SelectCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = StartDate1;
                    sda.Fill(dt);

                    SqlDataAdapter sda1 = new SqlDataAdapter("ISP_AVS0180", Conn);
                    sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda1.SelectCommand.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = tbname;
                    sda1.Fill(dt1);
                    string[] typeArr = dt1.Rows.OfType<DataRow>().Select(k => k[1].ToString()).ToArray();


                    string fileName = Tbn[1] + "-" + DateTime.Now.Date.ToShortDateString() + DateTime.Now.ToShortTimeString();
                    fileName = fileName.Replace("/", "");
                    fileName = fileName.Replace("AM", "");
                    fileName = fileName.Replace("PM", "");
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace(":", "");



                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            StreamWriter sw;
                            using (sw = File.CreateText(@"C:\Backup\" + fileName + ".txt"))
                            {
                                string type;
                                int i;
                                int j = 0;
                                sw.Write("Insert Into " + Tb + " (");
                                for (i = 0; i < dt.Columns.Count; i++)
                                {
                                    if (i != dt.Columns.Count - 1)
                                    {
                                        sw.Write(dt.Columns[i].ColumnName + ",");
                                    }
                                    else
                                    {
                                        sw.Write(dt.Columns[i].ColumnName);

                                    }

                                }
                                sw.Write(") \r\n Values ");
                                foreach (DataRow row in dt.Rows)
                                {
                                    int RowCount = dt.Rows.Count;
                                    j += 1;
                                    sw.Write("(");
                                    object[] array = row.ItemArray;
                                    string value = string.Empty;
                                    for (i = 0; i < array.Length; i++)
                                    {
                                        type = typeArr[i];
                                        if (type == "float" || type == "bigint" || type == "numeric" || type == "int")
                                        {
                                            value = array[i].ToString();
                                            if (value.Trim() == "")
                                            {
                                                value = "null";
                                            }
                                            sw.Write(value);
                                        }
                                        else if (type == "nvarchar" || type == "varchar")
                                        {
                                            sw.Write("'" + array[i].ToString() + "'");

                                        }
                                        else if (type == "datetime2" || type == "datetime")
                                        {
                                            string date = array[i].ToString();
                                            if (date != "")
                                            {
                                                if (date.Contains("-"))
                                                {
                                                    string[] ab = date.Split('-');
                                                    string chk = ab[0];
                                                    if (chk.Length > 2)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        string date1 = ab[2];
                                                        string date2 = date1.Substring(0, 4);
                                                        date1 = date1.Substring(4);
                                                        date = date2 + "-" + ab[1] + "-" + ab[0] + " " + date1;
                                                    }
                                                }
                                                else if (date.Contains("/"))
                                                {
                                                    string[] ab = date.Split('/');
                                                    string chk = ab[0];
                                                    if (chk.Length > 2)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        string date1 = ab[2];
                                                        string date2 = date1.Substring(0, 4);
                                                        date1 = date1.Substring(4);
                                                        date = date2 + "-" + ab[1] + "-" + ab[0] + " " + date1;
                                                    }
                                                }

                                            }
                                            sw.Write("'" + date + "'");
                                        }
                                        else
                                        {
                                            sw.Write(array[i].ToString());
                                        }

                                        if (i != array.Length - 1)
                                        {
                                            sw.Write(",");
                                        }

                                    }

                                    if (j == RowCount)
                                    {
                                        sw.Write(") \r\n");
                                    }
                                    else
                                    {
                                        sw.Write("), \r\n");
                                    }
                                }
                            }
                            //StringBuilder SBTxt = new StringBuilder();                      

                            //SBTxt.Append(sw.ToString());
                            //StringBuilder SBTxt1 = new StringBuilder();
                            //SBTxt1.Append(encrypt(SBTxt.ToString()));
                            //sw.Close();



                            StreamReader sr = new StreamReader(@"C:\Backup\" + fileName + ".txt");
                            StringBuilder SBTxt = new StringBuilder();
                            SBTxt.Append(sr.ReadToEnd().ToString());
                            StringBuilder SBTxt1 = new StringBuilder();
                            SBTxt1.Append(SBTxt.ToString());//For generating file without Encryption Function
                            // SBTxt1.Append(encrypt(SBTxt.ToString()));
                            sr.Close();
                            var fs = new FileStream(@"C:\Backup\" + fileName + ".txt", FileMode.Truncate);
                            fs.Close();
                            StreamWriter sw1;
                            using (sw1 = File.CreateText(@"C:\Backup\" + fileName + ".txt"))
                            {
                                sw1.Write(SBTxt1.ToString());
                            }
                            sw1.Close();
                            files.Add(new System.Web.UI.WebControls.ListItem(@"C:\Backup\" + fileName + ".txt"));
                        }
                        catch (Exception Ex)
                        {
                            ExceptionLogging.SendErrorToText(Ex);
                        }

                        //StreamReader sr1 = new StreamReader(@"D:\MyDir\" + fileName + ".txt");
                        //StringBuilder SBTxt2 = new StringBuilder();
                        //SBTxt2.Append(sr1.ReadToEnd().ToString());
                        //StringBuilder SBTxt3 = new StringBuilder();
                        //SBTxt3.Append(Decrypt(SBTxt2.ToString()));
                        //sr1.Close();





                        //Response.Clear();
                        //Response.ClearContent();
                        //Response.ClearHeaders();
                        //Response.Buffer = true;
                        //Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".txt");
                        //Response.Charset = "";
                        //Response.ContentType = "application/octet-stream";
                        //using (StringWriter writer = new StringWriter())
                        //{
                        //    writer.WriteLine(SBTxt1);
                        //    writer.Close();
                        //    Response.Output.Write(writer);
                        //}
                        //HttpContext.Current.Response.Flush();
                        //HttpContext.Current.Response.SuppressContent = true;
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();

                        //using (sw = File.CreateText(@"D:\MyDir\" + fileName + "Decrypt.txt"))
                        //{
                        //    sw.Write(SBTxt3.ToString());
                        //}
                        //sw.Close();
                    }

                }

                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    //zip.AddDirectoryByName("Files");
                    foreach (System.Web.UI.WebControls.ListItem li in files)
                    {
                        zip.AddFile(li.Value, "Your Files");
                    }
                    Response.Clear();
                    Response.BufferOutput = false;
                    string zipName = String.Format("Backup_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    zip.Save(Response.OutputStream);
                    //WebMsgBox.Show("BACKUP Successful", this.Page);
                    Response.End();
                }

                //WebMsgBox.Show("BACKUP Successful", this.Page);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
                //WebMsgBox.Show("Error while BACKUP", this.Page);
            }
            finally
            {
                Conn.Close();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public string encrypt(string encryptString)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //string EncryptionKey = "AVSINSOTECH2013";
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
    //public string Decrypt(string cipherText)
    //{
    //    string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    //    cipherText = cipherText.Replace(" ", "+");
    //    byte[] cipherBytes = Convert.FromBase64String(cipherText);
    //    using (Aes encryptor = Aes.Create())
    //    {
    //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
    //        0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
    //    });
    //        encryptor.Key = pdb.GetBytes(32);
    //        encryptor.IV = pdb.GetBytes(16);
    //        using (MemoryStream ms = new MemoryStream())
    //        {
    //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
    //            {
    //                cs.Write(cipherBytes, 0, cipherBytes.Length);
    //                cs.Close();
    //            }
    //            cipherText = Encoding.Unicode.GetString(ms.ToArray());
    //        }
    //    }
    //    return cipherText;
    //}  

    protected void btnEncrypt_Click(object sender, EventArgs e)
    {
        try
        {

            process();
           // process1();
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
    }

    private void process1()
    {
        try
        {
            
            Response.Write("File Created");
        }
        catch (Exception Ex)
        {
        }

            
    }
    protected void btnDecrypt_Click(object sender, EventArgs e)
    {
        try
        {
            processWithDecrypt();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnCustomerData_Click(object sender, EventArgs e)
    {
        try
        {
            BalanceList();
            LoanBalanceList();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BalanceList()
    {
        try
        {
            string redirectURL = "FrmRView.aspx?BrCode=" + Session["Brcd"].ToString() + "&AsOnDate=" + Session["EntryDate"].ToString() + "&isExcelDownload=1&rptname=RptCustomerBalane.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LoanBalanceList()
    {
        try
        {
            string redirectURL = "FrmRView.aspx?BrCode=" + Session["Brcd"].ToString() + "&AsOnDate=" + Session["EntryDate"].ToString() + "&isExcelDownload=1&rptname=RptLoanBalanceList.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DepositBalanceList()
    {
        try
        {
            string redirectURL = "FrmRView.aspx?BrCode=" + Session["Brcd"].ToString() + "&AsOnDate=" + Session["EntryDate"].ToString() + "&isExcelDownload=1&rptname=RptCustomerBalane.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}