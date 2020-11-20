using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class FrmUpdateInfo : System.Web.UI.Page
{

    scustom customcs = new scustom();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCashReciept CurrentCls = new ClsCashReciept();
    ClsAuthorized PVOUCHER = new ClsAuthorized();
    ClsCommon CMN = new ClsCommon();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsOpenClose OC = new ClsOpenClose();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    Datecall DS = new Datecall();
    string res,FL="";
    int nonmi1;
   
    protected void Page_Load(object sender, EventArgs e)
    {

    if (!IsPostBack)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }

                BindGrid();
                autoglname.ContextKey = Session["BRCD"].ToString();
                TxtBrcd.Text = Session["BRCD"].ToString();
                Txtprodcode.Focus();
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }
    public void ClearData()
    {
        Txtprodcode.Text = "";
        txtname.Text = "";
        //  TxtBrcd.Text = "";
        txtlastdate.Text = "";
        txtaccno.Text = "";
        TxtAccName.Text = "";
        Txtupdate.Text = "";
    }
    public void BindGrid()
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }



    protected void Txtprodcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL( Txtprodcode.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["DRGL"] = GL[1].ToString();
            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + Txtprodcode.Text + "_" + ViewState["DRGL"].ToString();
            string PDName = customcs.GetProductName(Txtprodcode.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                txtname.Text = PDName;
                txtaccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                Txtprodcode.Text = "";
                Txtprodcode.Focus();
            }


        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtaccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AN;
            AN = customcs.GetAccountName(txtaccno.Text, Txtprodcode.Text, Session["BRCD"].ToString()).Split('_');
            if (AN != null)
            {
                TxtAccName.Text = AN[1].ToString();
                res = DS.AccNodisplay(TxtBrcd.Text, Txtprodcode.Text, txtaccno.Text);
                txtlastdate.Text = res;
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                txtaccno.Text = "";
                txtaccno.Focus();
            }

        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtAccName.Text = custnob[0].ToString();
                txtaccno.Text = custnob[1].ToString();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                txtaccno.Text = "";
                txtaccno.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtname.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtname.Text = CT[0].ToString();
                Txtprodcode.Text = CT[1].ToString();
                  txtaccno.Focus();
                string[] GLS = BD.GetAccTypeGL(Txtprodcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + Txtprodcode.Text + "_" + ViewState["DRGL"].ToString();
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
            
            nonmi1 = DS.nomiupdate1(Session["BRCD"].ToString(),Txtupdate.Text,txtlastdate.Text, Txtprodcode.Text, txtaccno.Text);
            if (nonmi1 > 0)
            {
                WebMsgBox.Show("DATA UPDATE SUCCESSFULLY", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Updateinfo _" + Txtprodcode.Text + "_" + txtaccno.Text + "_" + txtlastdate.Text + "_" + Txtupdate.Text+"_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
                return;
            }
        }
        catch (Exception )
        {
            //throw ex;
        }
        }

 

    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
}


