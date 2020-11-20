using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class FrmAVS5090 : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsAVS5090 AT = new ClsAVS5090();
    string Flg = "";
    int Result = 0, Res = 0;

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
                Txtdate.Text = Session["EntryDate"].ToString();
                TxtMenu.Focus();
                TxtSrno.Text=AT.GetSrno();
                BindGrid();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {

            string Imagename = FileUpload1.FileName.ToString();
            string Cote = "'";
            Byte[] imgbyte = null;

            HttpPostedFile File1 = FileUpload1.PostedFile;
            imgbyte = new Byte[File1.ContentLength];
            File1.InputStream.Read(imgbyte, 0, File1.ContentLength);
            Byte[] imageArray = imgbyte;
            SqlConnection Conn = new SqlConnection(conn.DbName());
            string sql = "";

            sql = "INSERT INTO AVS5066 (Srno,Date,Menu,Activity,Requirement	,RequirementBy,Image_Name,Stage,ImageCode,SystemDate) ";
            sql += " VALUES (@Srno,@Date,@Menu,@Activity,@Requ,@ReqBy,@Imagename,@Stage,@Code,'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "')";
            SqlCommand cmd1 = new SqlCommand(sql, Conn);
            cmd1.Parameters.Add("@Srno", SqlDbType.BigInt).Value = TxtSrno.Text;
            cmd1.Parameters.Add("@Date", SqlDbType.DateTime).Value = conn.ConvertDate(Txtdate.Text).ToString();
            cmd1.Parameters.Add("@Menu", SqlDbType.VarChar).Value = TxtMenu.Text;
            cmd1.Parameters.Add("@Activity", SqlDbType.NVarChar).Value = TxtActivity.Text;
            cmd1.Parameters.Add("@Requ", SqlDbType.NVarChar).Value = TxtReq.Text;
            cmd1.Parameters.Add("@ReqBy", SqlDbType.NVarChar).Value = TxtReqBy.Text;
            cmd1.Parameters.Add("@Imagename", SqlDbType.NVarChar).Value = Imagename;
            cmd1.Parameters.Add("@Stage", SqlDbType.Int).Value = 1003;
            cmd1.Parameters.Add("@Code", SqlDbType.Binary).Value = imageArray;
            Conn.Open();
            Result = cmd1.ExecuteNonQuery();
            Conn.Close();
            if (Result > 0)
            {
                WebMsgBox.Show("Added Successfully!!!", this.Page);
                BindGrid();
            }
            Clear();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void Clear()
    {
        TxtMenu.Text = "";
        TxtActivity.Text = "";
        TxtReq.Text = "";
        TxtReqBy.Text = "";
    }
    public void BindGrid()
    {
        Result = AT.BindRequ(GrdDet, Txtdate.Text);
    }
    protected void BtnModify_Click(object sender, EventArgs e)
    {
        try
        {
            string Imagename = FileUpload1.FileName.ToString();
            string Cote = "'";
            Byte[] imgbyte = null;
            HttpPostedFile File1 = FileUpload1.PostedFile;
            imgbyte = new Byte[File1.ContentLength];
            File1.InputStream.Read(imgbyte, 0, File1.ContentLength);
            Byte[] imageArray = imgbyte;
            SqlConnection Conn = new SqlConnection(conn.DbName());
            string sql = "";
            if (lblImage.Text == "")
            {
                sql = "Update  AVS5066 set  Srno=@Srno,Date=@Date,Menu=@Menu,Activity=@Activity,Requirement=@Requ,RequirementBy=@ReqBy,Image_Name=@Imagename, ImageCode=@Code,Stage='1002' where id=@ID";
                SqlCommand cmd1 = new SqlCommand(sql, Conn);
                cmd1.Parameters.Add("@Srno", SqlDbType.BigInt).Value = TxtSrno.Text;
                cmd1.Parameters.Add("@Date", SqlDbType.DateTime).Value = conn.ConvertDate(Txtdate.Text).ToString();
                cmd1.Parameters.Add("@Menu", SqlDbType.VarChar).Value = TxtMenu.Text;
                cmd1.Parameters.Add("@Activity", SqlDbType.NVarChar).Value = TxtActivity.Text;
                cmd1.Parameters.Add("@Requ", SqlDbType.NVarChar).Value = TxtReq.Text;
                cmd1.Parameters.Add("@ReqBy", SqlDbType.NVarChar).Value = TxtReqBy.Text;
                cmd1.Parameters.Add("@Imagename", SqlDbType.NVarChar).Value = Imagename;
                cmd1.Parameters.Add("@Code", SqlDbType.Binary).Value = imageArray;
                cmd1.Parameters.Add("@ID", SqlDbType.NVarChar).Value = ViewState["id"].ToString();
                Conn.Open();
                Result = cmd1.ExecuteNonQuery();
                Conn.Close();
                if (Result > 0)
                {
                    WebMsgBox.Show("Modified Successfully!!!", this.Page);
                    BindGrid();
                }
            }
            else
            {

                sql = "Update  AVS5066 set  Srno=@Srno,Date=@Date,Menu=@Menu,Activity=@Activity,Requirement=@Requ,RequirementBy=@ReqBy,Stage='1002' where id=@ID";
                SqlCommand cmd1 = new SqlCommand(sql, Conn);
                cmd1.Parameters.Add("@Srno", SqlDbType.BigInt).Value = TxtSrno.Text;
                cmd1.Parameters.Add("@Date", SqlDbType.DateTime).Value = conn.ConvertDate(Txtdate.Text).ToString();
                cmd1.Parameters.Add("@Menu", SqlDbType.VarChar).Value = TxtMenu.Text;
                cmd1.Parameters.Add("@Activity", SqlDbType.NVarChar).Value = TxtActivity.Text;
                cmd1.Parameters.Add("@Requ", SqlDbType.NVarChar).Value = TxtReq.Text;
                cmd1.Parameters.Add("@ReqBy", SqlDbType.NVarChar).Value = TxtReqBy.Text;
                cmd1.Parameters.Add("@ID", SqlDbType.NVarChar).Value = ViewState["id"].ToString();
                Conn.Open();
                Result = cmd1.ExecuteNonQuery();
                Conn.Close();
                if (Result > 0)
                {
                    WebMsgBox.Show("Modified Successfully!!!", this.Page);
                    BindGrid();
                }
            }
            Clear();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {

            SqlConnection Conn = new SqlConnection(conn.DbName());
            string sql = "";
            sql = "Update  AVS5066 set  Stage='1004' where id=@ID";
            SqlCommand cmd1 = new SqlCommand(sql, Conn);
            cmd1.Parameters.Add("@ID", SqlDbType.NVarChar).Value = ViewState["id"].ToString();

            Conn.Open();
            Result = cmd1.ExecuteNonQuery();
            Conn.Close();
            if (Result > 0)
            {
                WebMsgBox.Show("Deleted Successfully!!!", this.Page);
                BindGrid();
            }
            Clear();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmAVS5090.aspx");
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void GrdDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdDet.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string id = objlink.CommandArgument;
            ViewState["id"] = id;
            callData();
            Btn_Submit.Visible = false;
            BtnModify.Visible = true;
            BtnAdd.Visible = true;
            BtnDelete.Visible = false;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string id = objlink.CommandArgument;
            ViewState["id"] = id;
            callData();
            Btn_Submit.Visible = false;
            BtnModify.Visible = false;
            BtnAdd.Visible = true;
            BtnDelete.Visible = true;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void callData()
    {
        try
        {
            DT = AT.GetReqDate(ViewState["id"].ToString());
            if (DT.Rows.Count > 0)
            {
                TxtSrno.Text = DT.Rows[0]["Srno"].ToString();
                Txtdate.Text = DT.Rows[0]["Date"].ToString();
                TxtMenu.Text = DT.Rows[0]["Menu"].ToString();
                TxtActivity.Text = DT.Rows[0]["Activity"].ToString();
                TxtReq.Text = DT.Rows[0]["Requirement"].ToString();
                TxtReqBy.Text = DT.Rows[0]["RequirementBy"].ToString();
                lblImage.Text = DT.Rows[0]["Image_Name"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BthExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
   
}

