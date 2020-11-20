using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;

public partial class FrmSocietyMaster : System.Web.UI.Page
{

    
  //  ClsSocietyMaster SM = new ClsSocietyMaster();
    ClsBindDropdown BD = new ClsBindDropdown();
    DataTable DT = new DataTable();
    ClsCo_master cm = new ClsCo_master();
    DbConnection conn = new DbConnection();
    ClsCustomerMast CMT = new ClsCustomerMast();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
   string sql;
    int result;
    int RESULT;
    string Flag;
    protected void Page_Load(object sender, EventArgs e)
    
    {
       
            CUSTNOGEN();

            try
            {
                if (!IsPostBack)
                {
                    if (Session["UserName"] == null)
                    {
                        Response.Redirect("FrmLogin.aspx");
                    }

                    BD.BindState(ddlstate);
                    cm.bindcustcat(ddlCategory);

                   
                    TxtSociety.Focus();
                    string Flag;
                    Flag = Request.QueryString["Flag"].ToString();
                  //  bind.BindState(ddlstate);
                   
                    DT = new DataTable();


                    ViewState["Flag"] = Request.QueryString["Flag"].ToString();
                    if (Flag == "AD")
                    {

                        BTN_AUTH.Visible = false;
                     btn_submit.Visible=true;
                        btn_Modify.Visible=false;
                        btndelete.Visible=false;
                        txtCustNo.Enabled = false;
                    }
                    else if (Flag == "MD")
                    {

                        BTN_AUTH.Visible = false;
                     btn_submit.Visible=false;
                        btn_Modify.Visible=true;
                        btndelete.Visible=false;
                        txtCustNo.Focus();
                    }
                    else if (Flag == "DL")
                    {

                        BTN_AUTH.Visible = false;
                     btn_submit.Visible=false;
                        btn_Modify.Visible=false;
                        btndelete.Visible=true;
                        txtCustNo.Focus();
                    }
                    else if (Flag == "VW")
                    {

                        BTN_AUTH.Visible = false;
                        btn_submit.Visible = false;
                        btn_Modify.Visible = false;
                        btndelete.Visible = true;
                        txtCustNo.Focus();
                    }
                    else if (Flag == "AT")
                    {


                        btn_submit.Visible = false;
                        BTN_AUTH.Visible = true;
                        btn_Modify.Visible = false;
                        btndelete.Visible = true;
                        txtCustNo.Focus();
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        

       
    }
  
       
  

    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BD.BindDistrict(ddlDist, ddlstate.SelectedValue);
            ddlDist.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void CUSTNOGEN()
    {
        RESULT = Convert.ToInt32(CMT.GetCustNo("0"));
    }
    protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BD.BindArea(ddlTal, ddlDist.SelectedValue);
            ddlTal.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void txtvillege_TextChanged(object sender, EventArgs e)
    {
        
    }
    public void CLEAR()
    {
        TxtSociety.Text = "";
        ddlCategory.SelectedValue = "0";
        txtadd1.Text = "";
        txtadd2.Text = "";
        txtroad.Text = "";
        txtNear.Text = "";
        ddlstate.SelectedValue= "0";
        ddlDist.SelectedValue= "0";
        ddlTal.SelectedValue= "0";
        txtvillege.Text = "";
        txtpin.Text = "";
        TxtFY.Text = "";
        txtspint.Text = "";
        txtpancard.Text = "";

    }
    public void BINDBRID()
    {
        cm.BINDCUSTCO(RESULT.ToString(), Session["BRCD"].ToString(), SocietyGrid);
    }
    public void BINDSS()
    {
        cm.BINDCUSTCO(txtCustNo.Text, Session["BRCD"].ToString(), SocietyGrid);
    }
   
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (txtadd1.Text == "" && txtroad.Text == "")
        {
            WebMsgBox.Show("Pls insert Blank Feilds", this.Page);
        }
       else if (txtNear.Text == "" && txtvillege.Text == "" && txtpin.Text == "")
        {

            WebMsgBox.Show("pls insert blank fields", this.Page);
        }
        else if (TxtFY.Text == "" && txtspint.Text == "" && txtpancard.Text == "")
        {
            WebMsgBox.Show("pls insert blank fields", this.Page);
        }
        CUSTNOGEN();
        try
        {
            int ASS = cm.ADDCUST(Session["BRCD"].ToString(), RESULT.ToString(), TxtSociety.Text, txtpancard.Text, txtspint.Text, ddlCategory.SelectedItem.Text, TxtFY.Text);
            int master = cm.masterinsert(RESULT.ToString(), TxtSociety.Text, Session["BRCD"].ToString());
            int RES = cm.Insertadd(RESULT.ToString(), txtadd1.Text, txtadd2.Text, txtroad.Text, txtNear.Text, ddlstate.SelectedItem.Value, ddlDist.SelectedItem.Value, ddlTal.SelectedItem.Value, txtvillege.Text, txtpin.Text, Session["BRCD"].ToString());
            int up = cm.UpdateCustno(RESULT.ToString());////Added by ankita on 03/07/2017 to update lastno
            if (RES > 0 && ASS > 0 && master > 0 && up>0)
            {
                BINDBRID();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Co-operate_Add _" + txtCustNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                lblMessage.Text = "";
                CLEAR();
                lblMessage.Text = "RECORD ADDED SUCCESSFULLY CUSTNO IS " + RESULT;
                ModalPopup.Show(this.Page);
                CUSTNOGEN();
                //WebMsgBox.Show("RECORD ADDED SUCCESSFULLY CUSTNO IS '"+RESULT+"'", this.Page);


            }

            else
            {
                WebMsgBox.Show("RECORD NOT SAVED", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
             
                     
    }
    protected void btn_Modify_Click(object sender, EventArgs e)
   {
       if (txtCustNo.Text == "")
       {
           WebMsgBox.Show("pls insert cust no first",this.Page);
       }
       try
       {
           int rc1 = cm.modifycustadd11(Session["BRCD"].ToString(), txtCustNo.Text, TxtSociety.Text, txtpancard.Text, txtspint.Text, ddlCategory.SelectedItem.Value, TxtFY.Text);
           int mast = cm.updatemast(Session["BRCD"].ToString(), txtCustNo.Text, TxtSociety.Text);
           int RCC3 = cm.modifycustadd12(txtCustNo.Text, txtadd1.Text, txtadd2.Text, txtroad.Text, txtNear.Text, ddlstate.SelectedItem.Value, ddlDist.SelectedItem.Value, ddlTal.SelectedItem.Value, txtvillege.Text, txtpin.Text, Session["BRCD"].ToString());
           if (rc1 > 0 && RCC3 > 0 && mast>0)
           {
               WebMsgBox.Show("RECORD IS MODIFIED SUCCESSFULLY ", this.Page);
               BINDSS();
               FL = "Insert";//Dhanya Shetty
               string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Co-operate_Mod _" + txtCustNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
               CLEAR();
           }
           else
           {


           }
       }
       catch (Exception ex)
       {
           ExceptionLogging.SendErrorToText(ex);
       }
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        TxtSociety.Text = "";
         ddlCategory.SelectedItem.Text= "";
        txtadd1.Text = "";
        txtadd2.Text = "";
        txtroad.Text = "";
        txtNear.Text = "";
        ddlstate.SelectedItem.Text = "";
         ddlDist.SelectedValue="0";
           ddlTal.Text = "";
        txtvillege.Text = "";
        txtpin.Text = "";
        TxtFY.Text = "";
        txtspint.Text = "";
        txtpancard.Text = "";
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        if (txtCustNo.Text == "")
        {
            WebMsgBox.Show("pls insert cust no first", this.Page);
        }
        int RC = cm.DELETEADD(txtCustNo.Text,Session["BRCD"].ToString());
        int RCC = cm.DELETEAINFO(txtCustNo.Text, Session["BRCD"].ToString());
        if (RCC > 0 && RC > 0)
        {
            WebMsgBox.Show("RECORD IS DELETED SUCCESSFULLY ", this.Page);
            BINDBRID();
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Co-operate_Del _" + txtCustNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            CLEAR();
        }

    }
    protected void lnkedit_Click(object sender, EventArgs e)
    {
        
        LinkButton lnkedit = (LinkButton)sender;
        string str = lnkedit.CommandArgument.ToString();
              ViewState["CUSTNO"] = str.ToString();
              txtCustNo.Enabled = true;
              btn_submit.Visible = false;
              btn_Modify.Visible = true;
              data();
              data1();
    }
    public void data()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = cm.showdataCUST(ViewState["CUSTNO"].ToString(),Session["BRCD"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtCustNo.Text = DT.Rows[0]["CUSTNO"].ToString();
                TxtSociety.Text = DT.Rows[0]["ORGNAME"].ToString();
                txtpancard.Text = DT.Rows[0]["PANCARD"].ToString();
                txtspint.Text = DT.Rows[0]["SPL_INST"].ToString();
                ddlCategory.SelectedValue = DT.Rows[0]["CUSTCATG"].ToString();
                TxtFY.Text = DT.Rows[0]["REGDATE"].ToString();


            }
            else
            {
                WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void data1()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = cm.showdataCUSTADD(ViewState["CUSTNO"].ToString(), Session["BRCD"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtadd1.Text = DT.Rows[0]["FLAT_ROOMNO"].ToString();
                txtadd2.Text = DT.Rows[0]["SOCIETY_NAME"].ToString();
                txtroad.Text = DT.Rows[0]["STREET_SECTOR"].ToString();
                txtNear.Text = DT.Rows[0]["NEAR"].ToString();
                ddlstate.SelectedValue = Convert.ToInt32(DT.Rows[0]["STATE"].ToString() == "" ? "0" : DT.Rows[0]["STATE"].ToString()).ToString();
                BD.BindDistrict(ddlDist, ddlstate.SelectedValue);
                ddlDist.SelectedValue = Convert.ToInt32(DT.Rows[0]["DISTRICT"].ToString() == "" ? "0" : DT.Rows[0]["DISTRICT"].ToString()).ToString(); 
                ddlTal.SelectedValue = Convert.ToInt32(DT.Rows[0]["AREA_TALUKA"].ToString() == "" ? "0" : DT.Rows[0]["AREA_TALUKA"].ToString()).ToString();
               
                txtvillege.Text = DT.Rows[0]["CITY"].ToString();
                txtpin.Text = DT.Rows[0]["PINCODE"].ToString();



            }
            else
            {
                WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void SocietyGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        data();
        data1();
    }
    protected void txtCustNo_TextChanged(object sender, EventArgs e)
        {
        DT = cm.showdataCUSTADD(txtCustNo.Text, Session["BRCD"].ToString());
        if (DT.Rows.Count > 0)
        {
            txtadd1.Text = DT.Rows[0]["FLAT_ROOMNO"].ToString();
            txtadd2.Text = DT.Rows[0]["SOCIETY_NAME"].ToString();
            txtroad.Text = DT.Rows[0]["STREET_SECTOR"].ToString();
            txtNear.Text = DT.Rows[0]["NEAR"].ToString();
            ddlstate.SelectedValue = Convert.ToInt32(DT.Rows[0]["STATE"].ToString() == "" ? "0" : DT.Rows[0]["STATE"].ToString()).ToString();
            BD.BindDistrict(ddlDist, ddlstate.SelectedValue);
            ddlDist.SelectedValue = Convert.ToInt32(DT.Rows[0]["DISTRICT"].ToString() == "" ? "0" : DT.Rows[0]["DISTRICT"].ToString()).ToString();
            BD.BindArea(ddlTal, ddlDist.SelectedValue);

            ddlTal.SelectedValue = Convert.ToInt32(DT.Rows[0]["AREA_TALUKA"].ToString() == "" ? "0" : DT.Rows[0]["AREA_TALUKA"].ToString()).ToString();
            txtvillege.Text = DT.Rows[0]["CITY"].ToString();
            txtpin.Text = DT.Rows[0]["PINCODE"].ToString();
            BINDSS();


        }
        else
        {
            WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
        }
        DT = DT = cm.showdataCUST(txtCustNo.Text, Session["BRCD"].ToString());
        if (DT.Rows.Count > 0)
        {
            txtCustNo.Text = DT.Rows[0]["CUSTNO"].ToString();
            TxtSociety.Text = DT.Rows[0]["ORGNAME"].ToString();
            txtpancard.Text = DT.Rows[0]["PANCARD"].ToString();
            txtspint.Text = DT.Rows[0]["SPL_INST"].ToString();
            //ddlCategory.SelectedValue = Convert.ToInt32(DT.Rows[0]["CUSTCATG"].ToString() == "" ? "0" : DT.Rows[0]["CUSTCATG"].ToString()).ToString();
            TxtFY.Text = DT.Rows[0]["REGDATE"].ToString();


        }
        else
        {
            WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
        }

    }
    protected void TxtSociety_TextChanged(object sender, EventArgs e)
    {

        ddlCategory.Focus();
    }
    protected void ddlTal_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtvillege.Focus();
    }
    protected void txtpin_TextChanged(object sender, EventArgs e)
    {
        TxtFY.Focus();
    }
    protected void TxtFY_TextChanged(object sender, EventArgs e)
    {
        txtspint.Focus();
    }
    protected void txtNear_TextChanged(object sender, EventArgs e)
    {
        ddlstate.Focus();
    }
    protected void txtspint_TextChanged(object sender, EventArgs e)
    {

    }
    protected void BTN_AUTH_Click(object sender, EventArgs e)
    {
        try
        {
            string  result = cm.checkautho(Session["BRCD"].ToString(), txtCustNo.Text);
            if (result == "1003")
            {
                WebMsgBox.Show("Record Is Allready Authorised", this.Page);
                CLEAR();
            }
            else
            {
                int RS = cm.authorised(Session["BRCD"].ToString(), txtCustNo.Text);
                if (RS > 0)
                {
                    WebMsgBox.Show("Record Is authorized successfully", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Co-operate_Autho _" + txtCustNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    CLEAR();

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            throw;
        }
    }
}



  
    

