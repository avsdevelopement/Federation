using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class FrmPhotoSign : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ocbs"].ToString());
    DbConnection conn1 = new DbConnection();
    DataTable pic = new DataTable();
    ClsPhotoSign PS = new ClsPhotoSign();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsPhotoSign PC = new ClsPhotoSign();
    scustom customcs = new scustom();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            customcs.BindDdlIdentityProof(ddlDocType);
            customcs.BindDdlIdentityProof(ddlDocType1);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                Byte[] imgbyte = null;
                string strname = FileUpload1.FileName.ToString();
                string ext = Path.GetExtension(strname);
                string contenttype = String.Empty;
                HttpPostedFile File = FileUpload1.PostedFile;
                imgbyte = new Byte[File.ContentLength];
                File.InputStream.Read(imgbyte, 0, File.ContentLength);
                //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/upload/") + strname);

                switch (ext)
                {
                    case ".jpg":
                        contenttype = "image/jpg";
                        break;
                    case ".jpeg":
                        contenttype = "image/jpeg";
                        break;
                    case ".gif":
                        contenttype = "image/gif";
                        break;
                    case ".png":
                        contenttype = "image/png";
                        break;
                    case ".bmp":
                        contenttype = "image/bmp";
                        break;
                    case ".tif":
                        contenttype = "image/tif";
                        break;
                }
                if (contenttype != String.Empty)
                {
                    Stream fs = FileUpload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = imgbyte; //br.ReadBytes((Int32)fs.Length);
                    string BRCD = Session["BRCD"].ToString();
                    string Mid = Session["MID"].ToString();
                    SqlConnection conn = new SqlConnection(conn1.DbName());
                    SqlCommand cmd = new SqlCommand("INSERT INTO AVS1011(CUSTNO,  ACCNO, SUBGLCODE, SRNO, DATEOFUPLOAD, STAGE, PHOTOSTATUS, RTIME, BRCD, MID,  PCMAC, PHOTO,photo_type) VALUES " +
                                                                       "(@CustNo,@AccNo,@SubGlCode,@SrNo,@Date,@Stage, @PStatus,@Rtrim,@BranchCode, @MCode,@PCMac, @Data,@ImageType)", conn);

                    cmd.Parameters.Add("@CustNo", SqlDbType.Int).Value = Txtcustno.Text;
                    cmd.Parameters.Add("@AccNo", SqlDbType.Int).Value = "0";
                    cmd.Parameters.Add("@SubGlCode", SqlDbType.Int).Value = "0";
                    cmd.Parameters.Add("@SrNo", SqlDbType.Int).Value = "0";
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = conn1.ConvertDate(Session["EntryDate"].ToString());
                    cmd.Parameters.Add("@Stage", SqlDbType.VarChar).Value = "1001";
                    cmd.Parameters.Add("@PStatus", SqlDbType.VarChar).Value = "1001";
                    cmd.Parameters.Add("@Rtrim", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@BranchCode", SqlDbType.Int).Value = BRCD;
                    cmd.Parameters.Add("@MCode", SqlDbType.Int).Value = Mid;
                    cmd.Parameters.Add("@Cid", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;
                    cmd.Parameters.Add("@PCMac", SqlDbType.VarChar).Value = conn1.PCNAME();
                    cmd.Parameters.Add("@ImageType", SqlDbType.NVarChar).Value = ddlDocType.SelectedValue.ToString();


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    WebMsgBox.Show("File Uploaded Successfully", this.Page);
                    Txtcustname.Text = "";
                    Txtcustno.Text = "";
                }
                else
                {
                    WebMsgBox.Show("File format not support select only jpg/png/gif/jpeg/bmp formats", this.Page);
                }
            }
            else
            {
                WebMsgBox.Show("Plz Select the image first !!!!", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void PreviewPhoto_Click(object sender, EventArgs e)
    {
        string id = Txtcustno.Text;
        Image1.Visible = id != "0";
        if (id != "0")
        {
            pic = GetData("SELECT photo Data FROM avs1011 WHERE CUSTNO = " + id + " AND photo_type = '" + ddlDocType.SelectedValue.ToString() + "' ");
            if (pic.Rows.Count > 0)
            {
                byte[] bytes = (byte[])pic.Rows[0]["Data"];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                Image1.ImageUrl = "data:image/tif;base64," + base64String;
                //imgPopup.ImageUrl = "data:image/tif;base64," + base64String;

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "File Successfully Viewed";
                ModalPopup.Show(this.Page);
                Txtcustname.Text = "";
                Txtcustno.Text = "";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "No Image for that customer !!!!";
                ModalPopup.Show(this.Page);
            }
        }
    }

    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        string constr = conn1.DbName();
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

    protected void ImgPhotoscan_Click(object sender, ImageClickEventArgs e)
    {
        int ctno, brcd;
        //Flag = "P";
        //lblHeader.Text = "Photo Scanning";
        // PhotoSing.Visible = true;
        // GM = new GetMaster();
        ctno = Convert.ToInt32(string.IsNullOrEmpty(Txtcustno.Text) ? "0" : Txtcustno.Text);
        brcd = Convert.ToInt32(Session["Brcd"]);
        //custno = GM.CheckCustExist(ctno, brcd);
        //if (custno > 0)
        //{
        //    DataTable DTsql = new DataTable();
        //    //DTsql = GM.CheckData(custno, brcd, Flag);
        //    if (DTsql.Rows.Count > 0)
        //    {
        //        ImgMap.ImageUrl = "~/DisplayImg.ashx?id=" + Txtcustno.Text + "&Type=" + Flag;
        //    }
        //}
        //else
        //{
        //    ImgMap.ImageUrl = "~/Images/Userimg.png";
        //}
    }
    protected void ImgSignaturescan_Click(object sender, ImageClickEventArgs e)
    {
        // PhotoSing.Visible = true;
    }


    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        string custno = Txtcustno.Text.ToString();
        PS.Delete_photosign(custno,Session["BRCD"].ToString());
        WebMsgBox.Show("Delete successfully", this.Page);

    }
    protected void BtnAuthorize_Click(object sender, EventArgs e)
    {
        string cutno = Txtcustno.Text.ToString();
        PS.Authorize_photosign(cutno, Session["BRCD"].ToString());
        WebMsgBox.Show("Authorize successfully", this.Page);
    }
    protected void Txtcustno_TextChanged(object sender, EventArgs e)
    {
        try
        {

            DataTable dt1 = new DataTable();
            //string AT;
            //AT = "";
            //AT = BD.GetCustName(Txtcustno.Text, Session["BRCD"].ToString());
            //if (AT != "1003")
            //{
            //    lblMessage.Text = "Sorry Customer not Authorise.........!!";
            //    ModalPopup.Show(this.Page);
            //    clear();
            //}
            string sql1 = "SELECT CUSTNAME FROM MASTER WHERE CUSTNO='" + Txtcustno.Text + "' AND BRCD='"+Session["BRCD"].ToString()+"'";
            dt1 = conn1.GetDatatable(sql1);
            if (dt1.Rows.Count != 0)
            {
                Txtcustname.Text = dt1.Rows[0][0].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);

        }
    }
    public void clear()
    {
        Txtcustno.Text = "";
        Txtcustname.Text = "";

    }

    public SqlConnection GetDBConnection()
    {
        SqlConnection conn = new SqlConnection(conn1.DbName());
        try
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn = new SqlConnection();
                conn.Open();
            }
        }
        catch (Exception ex)
        {
            conn.Dispose();
            HttpContext.Current.Response.Redirect(ex.ToString(), true);
            return null;
        }
        return conn;
    }
    protected void ImageB_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopup.Show(this.Page);
        //Popupimage();
    }
    public void Popupimage()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#alertModal').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), true);
    }
    protected void BTNKYC_Click(object sender, EventArgs e)
    {
        customcs.bindKyc(Session["BRCD"].ToString(),ddlDocType1.SelectedValue,grdMaster);
    }
    protected void grdMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
}
