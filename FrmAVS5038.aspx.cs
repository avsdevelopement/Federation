using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS5038 : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown bd = new ClsBindDropdown();
    ClsCustomerDetails CD = new ClsCustomerDetails();
    ClsAVS5038 AVS5038 = new ClsAVS5038();
    DataTable DT = new DataTable();
    int RES = 0;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
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
                //autoCustname.ContextKey = Session["BRCD"].ToString();//ankita 22/11/2017 brcd removed
                bd.BindPost(ddlPost);
                TxtSrno.Text = AVS5038.getsrno();
                bindgrid();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtDirectNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = CD.GetCustName(TxtDirectNo.Text);//ankita 22/11/2017 brcd removed
            TxtDirectName.Text = DT.Rows[0]["CUSTNAME"].ToString();
            TxtMobile.Text = AVS5038.getmobile(TxtDirectNo.Text);
            TxtMobile.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtDirectName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtDirectName.Text;
            string[] custnob = CUNAME.Split('_');

            if (custnob.Length > 1)
            {
                TxtDirectName.Text = custnob[0].ToString();
                TxtDirectNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                TxtMobile.Text = AVS5038.getmobile(TxtDirectNo.Text);
                DT = CD.GetStage(TxtDirectNo.Text);

                if (DT.Rows[0]["STAGE"].ToString() == "1001")
                {
                    WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                    return;
                }
                else if (DT.Rows[0]["STAGE"].ToString() == "1004")
                {
                    WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                    return;
                }
            }
            TxtMobile.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            RES = AVS5038.insert(TxtSrno.Text, TxtDirectName.Text, ddlPost.SelectedItem.Text, TxtMobile.Text, TxtDirectNo.Text, conn.ConvertDate(TxtFdate.Text),  conn.ConvertDate(TxtToDate.Text), ddlSMS.SelectedValue, Session["MID"].ToString());
            if (RES > 0)
            {
                WebMsgBox.Show("Record Saved successfully..!!", this.Page);
                
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DirectorDetails_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clear();
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnModify_Click(object sender, EventArgs e)
    {
        try
        {
            RES = AVS5038.Modify(TxtSrno.Text, TxtDirectName.Text, ddlPost.SelectedItem.Text, TxtMobile.Text, TxtDirectNo.Text, conn.ConvertDate(TxtFdate.Text), conn.ConvertDate(TxtToDate.Text), ddlSMS.SelectedValue, Session["MID"].ToString());
            if (RES > 0)
            {
                WebMsgBox.Show("Record Modified successfully..!!", this.Page);
               
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DirectorDetails_Mod _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clear();
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            RES = AVS5038.Delete(TxtSrno.Text, Session["MID"].ToString());
            if (RES > 0)
            {
                WebMsgBox.Show("Record Deleted successfully..!!", this.Page);
               
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DirectorDetails_Del _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clear();
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void grdDirector_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDirector.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
            BtnSave.Visible = true;
            BtnModify.Visible = false;
            BtnDelete.Visible = false;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkMod_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            viewdetails(ViewState["ID"].ToString());
            BtnSave.Visible = false;
            BtnModify.Visible = true;
            BtnDelete.Visible = false;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkDel_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            viewdetails(ViewState["ID"].ToString());
            BtnSave.Visible = false;
            BtnModify.Visible = false;
            BtnDelete.Visible = true;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    public void bindgrid()
    {
        try
        {
            AVS5038.BindData(grdDirector);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void viewdetails(string id)
    {
        try
        {
            DT = AVS5038.getdata(id);
            if (DT.Rows.Count > 0)
            {
                TxtSrno.Text = DT.Rows[0]["DIRNO"].ToString();
                TxtDirectNo.Text = DT.Rows[0]["CUSTNO"].ToString();
                TxtDirectName.Text = DT.Rows[0]["DIRNAME"].ToString();
                TxtFdate.Text = DT.Rows[0]["FROMDATE"].ToString();
                TxtToDate.Text = DT.Rows[0]["TODATE"].ToString();
                TxtMobile.Text = DT.Rows[0]["MOBILENO"].ToString();
                ddlPost.SelectedValue = DT.Rows[0]["POST"].ToString();
                ddlSMS.SelectedValue = DT.Rows[0]["SMSYN"].ToString();

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void clear()
    {
        TxtDirectNo.Text = "";
        TxtDirectName.Text = "";
        TxtFdate.Text = "";
        TxtToDate.Text = "";
        TxtMobile.Text = "";
        ddlPost.SelectedValue = "0";
        ddlSMS.SelectedValue = "0";
        TxtSrno.Text = AVS5038.getsrno();
    }
}