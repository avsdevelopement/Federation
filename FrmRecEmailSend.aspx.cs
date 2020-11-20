using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Web.Util;
using System.Globalization;
using System.IO.Compression;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Web.Util;
public partial class FrmRecEmailSend : System.Web.UI.Page
{
    ClsRecEmailSend UM = new ClsRecEmailSend();
    DataTable Dt = new DataTable();
    string STR = "";
    int Result = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();

        }

    }


    protected void Btn_ConfirmSendMail_Click(object sender, System.EventArgs e)
    {
        try
        {
            //Attachment
            if (F_UploadAttachment.HasFile)
            {
                string FileNameatc = Path.GetFileName(F_UploadAttachment.PostedFile.FileName);
                string Extensionatc = Path.GetExtension(F_UploadAttachment.PostedFile.FileName);
                string FolderPathatc = "~/Uploads/";
                string FilePathatc = Server.MapPath(FolderPathatc);
                FilePathatc = FilePathatc + FileNameatc;
                F_UploadAttachment.SaveAs(FilePathatc);

                DataTable DataT = new DataTable();
                //  DataT = UM.GetEmailIds("1");
                DataT = UM.GetEmailCr("EP");
                if (DataT.Rows.Count > 0)
                {
                    if (DataT.Rows[0]["PWD"].ToString() != "0" && DataT.Rows[0]["MAILID"].ToString() != "0")
                    {
                        if (TxtEmailID.Text != "" && TxtEmailID.Text != null)
                        {
                            MailMessage mail = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                            // 
                            //smtpout.secureserver.ne

                            // get the userID, Pass
                            //string userID = "audit@avsinsotech.com";
                            //string password = "audit@2017";

                            string userID = DataT.Rows[0]["MAILID"].ToString();
                            string password = DataT.Rows[0]["PWD"].ToString();


                            string aa = TxtEmailID.Text.Trim();
                            mail.From = new MailAddress(userID);
                            mail.To.Add(aa);
                            mail.Subject = TxtSubject.Text;
                            mail.Body = TxtBody.Text;


                            // Attach file
                            mail.Attachments.Add(new Attachment(FilePathatc));
                            // SmtpServer.Port = 3535;
                            SmtpServer.Port = 587;
                            SmtpServer.Host = "smtp.gmail.com";
                            SmtpServer.UseDefaultCredentials = false;
                            //  SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                            SmtpServer.Credentials = new System.Net.NetworkCredential(userID, password);
                            //SmtpServer.EnableSsl = false;
                            SmtpServer.EnableSsl = true;
                            SmtpServer.Send(mail);
                            mail.To.Clear();
                            mail.Attachments.Clear();
                            Result = UM.InsertData("Insert", Session["BRCD"].ToString(), TxtEmailID.Text, Session["MID"].ToString(), Session["EntryDate"].ToString());


                            if (Result > 0)
                            {
                                WebMsgBox.Show("Email sent successfully", this.Page);
                                BindGrid();

                            }

                        }
                    }
                    else
                    {
                        WebMsgBox.Show("Add Emailid and Password in from Parameter Creation...", this.Page);
                    }
                }
                else
                {
                    WebMsgBox.Show("Add Emailid and Password in from Parameter Creation...", this.Page);
                }
            }
            else
            {
                WebMsgBox.Show("Select the file to Attach to your Email...!", this.Page);
            }
        }
        catch (System.Exception Ex)
        {
            WebMsgBox.Show("" + Ex + "", this.Page);
            ExceptionLogging.SendErrorToText(Ex);

        }
    }
    public void BindGrid()
    {
        try
        {
            UM.BindGrid(GrdShow, "Grid", Session["BRCD"].ToString(), Session["EntryDate"].ToString());
        }
        catch (System.Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        //GrdShow
    }
}