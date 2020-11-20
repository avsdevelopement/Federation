using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmPhotoView : System.Web.UI.Page
{
    DataTable pic = new DataTable();
    scustom customcs = new scustom();
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn1 = new DbConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BD.BindDoc(ddlDocType);
        }

    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["ocbs"].ConnectionString;
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
    protected void Preview_Click(object sender, EventArgs e)
    {
        try
        {
            string id = Txtcustno.Text;
            Image1.Visible = id != "0";
            if (id != "0")
            {
                string sql = "";
                sql = "SELECT photo Data FROM avs1011 WHERE CUSTNO = " + id + " AND photo_type = '" + ddlDocType.SelectedItem.Value + "' ";
                pic = GetData(sql);
                if (pic.Rows.Count > 0)
                {
                    byte[] bytes = (byte[])pic.Rows[0]["Data"];
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    Image1.ImageUrl = "data:image/tif;base64," + base64String;
                    imgPopup.ImageUrl = "data:image/tif;base64," + base64String;
                    //lblMessage.ForeColor = System.Drawing.Color.Green;
                    //lblMessage.Text = "File Successfully Viewed";              
                    Txtcustname.Text = "";
                    Txtcustno.Text = "";
                }
                else
                {
                    //lblMessage.ForeColor = System.Drawing.Color.Red;
                    //lblMessage.Text = "No Image for that customer !!!!";
                    //ModalPopup.Show(this.Page);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ImageB_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopup.Show(this.Page);
    }
    protected void BtnUploadBulk_Click(object sender, EventArgs e)
    {
        DbConnection conn = new DbConnection();
        try
        {
            var files = Directory.GetFiles("E:\\Abhishek CBS\\PHOTO DB\\At SB FINAL\\", "*.*", SearchOption.AllDirectories);
            //  var files = Directory.GetFiles("E:\\Abhishek CBS\\Images\\", "*.*", SearchOption.AllDirectories);
            //var files = Directory.GetFiles("E:\\Abhishek CBS\\PHOTO DB\\At CA FINAL\\At CA FINAL\\", "*.*", SearchOption.AllDirectories);

            List<string> imageFiles = new List<string>();
            foreach (string filename in files)
            {
                try
                {
                    imageFiles.Add(filename);
                    string fnm = filename.Replace("E:\\Abhishek CBS\\PHOTO DB\\At SB FINAL\\", "");
                    //string fnm = filename.Replace("E:\\Abhishek CBS\\PHOTO DB\\At CA FINAL\\At CA FINAL\\", "");

                    // string fnm = filename.Replace("E:\\Abhishek CBS\\Images\\", "");
                    string[] F1 = fnm.Split('.');
                    string sql = "";
                    string[] F2 = F1[0].Split('-');
                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(filename);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);
                    //sql = "SELECT ID FROM AVS1011 WHERE ACCNO='" + F2[0].ToString().Trim() + "' AND PHOTO_TYPE='" + F2[1].ToString().Trim() + "' AND SUBGLCODE=23";
                    //SqlCommand cmd = new SqlCommand();
                    //string RC = conn.sExecuteScalar(sql); RC != null||
                    if (F2[1].ToString() == "5 (2)" || F2[1].ToString() == "3 (3)" || F2[1].ToString() == "5 (3)" || F2[1].ToString() == "1 (2)" || F2[1].ToString() == "2 (2)" || F2[1].ToString() == "7 (2)" || F2[1].ToString() == "15 (2)" || F2[1].ToString() == "3 (2)" || F2[1].ToString() == "4 (2)" || F2[1].ToString() == "8 (2)" || F2[1].ToString() == "9 (2)" || F2[1].ToString() == "6 (2)" || F2[1].ToString() == "18 (2)" || F2[1].ToString() == "14 (2)" || F2[1].ToString() == "13 (2)" || F2[1].ToString() == "12 (2)" || F2[1].ToString() == "2 (3)")
                        goto LAB;


                    InsertImage(imageData, F2[0].ToString().Trim(), F2[1].ToString().Trim());

                   //if (imageFiles.ToString() == files.ToString())
                //    HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);

                    LAB: ;
                }
                catch (Exception Ex)
                {
                    continue;
                }
            }


            //    var files=Directory.GetFiles("E:\\Abhishek CBS\\Images\\","*.*",SearchOption.AllDirectories);
            //    List<string> iamgefile=new List<string>();
            //    foreach (string filename in files)
            //    {
            //        iamgefile.Add(filename);
            //        string fnm = filename.Replace("E:\\Abhishek CBS\\Images\\", "");
            //        //int length = fnm.Length;
            //        int count = 0;
            //        char c;
            //        for (int i = 0; i < fnm.Length; i++)
            //        {
            //            if (fnm[i] == '-')
            //            {
            //                c = fnm[i];
            //                count++;
            //            }
            //        }
            //        if (count >= 2)
            //            File.Delete("E:\\Abhishek CBS\\Images\\"+fnm+"");

            //    }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            throw;
        }
    }
    public void InsertImage(byte[] IMG, string accno, string Ptype)
    {
        try
        {
           // if (Ptype == "5 (2)" ||"5 (3)" ||"1 (2)"||"2 (2)" || "7 (2)" || "15 (2)" || "3 (2)" || "4 (2)" || "8 (2)" ||"9 (2)"|| "6 (2)")
            //if(Ptype=="5(2)")
            //{
            //    return;
            //}
            SqlConnection conn = new SqlConnection(conn1.DbName());
                conn.Open();
                string sql = "";
                string custno = "";
                custno = BD.GetCustno(accno, Session["BRCD"].ToString(),"23");
                string PCMAC = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.ToString();
            
                sql = "Insert into avs1011(CUSTNO,  ACCNO, SUBGLCODE, DATEOFUPLOAD, STAGE, PHOTOSTATUS, PHOTO,photo_type,brcd,mid,pcmac,systemdate) values('" + custno + "',  " + accno + ", 23, getdate(), 1001, 1, @PHOTO,'" + Ptype + "','" + Session["BRCD"].ToString() + "','" + Session["MID"].ToString() + "','" + PCMAC + "',GETDATE())";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@PHOTO", SqlDbType.Binary).Value = IMG;
                int RC = cmd.ExecuteNonQuery();
                if (RC > 0)
                {
                }
           
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
           // throw;
            
        }
    }
    protected void Txtcustno_TextChanged(object sender, EventArgs e)
    {
        Txtcustname.Text = BD.GetCustName(Txtcustno.Text, Session["BRCD"].ToString());
    }
    protected void Report_Click(object sender, EventArgs e)
    {
        //string redirectURL = "FrmRView.aspx?rptname=RptKycDoc.rdlc";
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    }
}