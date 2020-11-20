using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDemandADD : System.Web.UI.Page
{
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
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }


                string Flag;
                Flag = Request.QueryString["Flag"].ToString();


                DT = new DataTable();

                ViewState["FLAG"] = "AD";
                if (ViewState["FLAG"].ToString() == "AD")
                {
                    lblActivity.Text = "Add File";
                }


            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }



    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (txtMemNo.Text == "" )
        {
            WebMsgBox.Show("Pls insert Blank Feilds", this.Page);
        }
        else if (txtName.Text == "" )
        {

            WebMsgBox.Show("pls insert blank fields", this.Page);
        }
        else if (txtAddress.Text == "" && txtPincode.Text == "" )
        {
            WebMsgBox.Show("pls insert blank fields", this.Page);
        }
        
        try
        {
            
         if (ViewState["FLAG"].ToString() == "AD")
         {
            int master = cm.ADDmaster(Session["BRCD"].ToString(), txtMemNo.Text, txtName.Text);
            int addmast = cm.Addmast(Session["BRCD"].ToString(),txtAddress.Text, txtPincode.Text,txtMemNo.Text);
            
            if (master > 0 && addmast > 0 )
            {
                //BINDBRID();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "MasterDetail_add" + txtMemNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                lblMessage.Text = "";
                CLEAR();
                lblMessage.Text = "RECORD ADDED SUCCESSFULLY";
                ModalPopup.Show(this.Page);
              
            }

            else
            {
                WebMsgBox.Show("RECORD NOT SAVED", this.Page);
            }
        }
          else
             if (ViewState["FLAG"].ToString() == "MD")
             {
                 int master = cm.MODIFYmaster(Session["BRCD"].ToString(), txtName.Text, txtMemNo.Text);
                 int addmast = cm.modifyaddmast(Session["BRCD"].ToString(), txtAddress.Text, txtPincode.Text, txtMemNo.Text);

                 if (master > 0 && addmast > 0)
                 {
                     //BINDBRID();
                     FL = "Modify";//Dhanya Shetty
                     string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "MasterDetail_add" + txtMemNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                     lblMessage.Text = "";
                     CLEAR();
                     lblMessage.Text = "RECORD Modify SUCCESSFULLY";
                     ModalPopup.Show(this.Page);

                 }

                 else
                 {
                     WebMsgBox.Show("RECORD NOT SAVED", this.Page);
                 }
             }
             else
                 if (ViewState["FLAG"].ToString() == "DEL")
                 {
                     int master = cm.deletemaster(Session["BRCD"].ToString(), txtName.Text, txtMemNo.Text);
                     int addmast = cm.Deleteaddmast(Session["BRCD"].ToString(), txtAddress.Text, txtPincode.Text, txtMemNo.Text);

                     if (master > 0 && addmast > 0)
                     {
                         
                         FL = "Delete";//Dhanya Shetty
                         string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "MasterDetail_add" + txtMemNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                         lblMessage.Text = "";
                         CLEAR();
                         lblMessage.Text = "RECORD Delete SUCCESSFULLY";
                         ModalPopup.Show(this.Page);

                     }

                     else
                     {
                         WebMsgBox.Show("RECORD NOT Delete", this.Page);
                     }
                  }
             }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
             
    }
    
    public void CLEAR()
    {
        txtMemNo.Text = "";
        txtName.Text = "";
        txtAddress.Text = "";
        txtPincode.Text = "";
      
    }
    
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "AD";
        if (ViewState["FLAG"].ToString() == "AD")
        {
            lblActivity.Text = "Add File";
        }
        BtnSubmit.Text = "Add";
    }
    protected void lnkModify_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "MD";
       // BindGrdMain();
        BtnSubmit.Text = "Modify";
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "DEL";
        //BindGrdMain();
        BtnSubmit.Text = "Delete";
        txtAddress.Enabled=false;
        txtName.Enabled=false;
        txtPincode.Enabled=false;
    }
    protected void lnkAuthorized_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "AT";
        //BindGrdMain();
        BtnSubmit.Text = "Authorize";
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        CLEAR();
    }
    protected void txtMemNo_TextChanged(object sender, EventArgs e)
    {
        if (ViewState["FLAG"].ToString() == "AD")
        {
            string custno = cm.mastercustno(txtMemNo.Text);
            if (custno == null)
            {
                DT = cm.addmastview(txtMemNo.Text, Session["BRCD"].ToString(), txtAddress.Text, txtPincode.Text);
                if (DT.Rows.Count > 0)
                {
                    //txtMemNo.Text = DT.Rows[0]["CUSTNO"].ToString();
                    txtAddress.Text = DT.Rows[0]["Address"].ToString();
                    txtPincode.Text = DT.Rows[0]["pincode"].ToString();
                }
                string name = cm.masterView(txtMemNo.Text);
                txtName.Text = name;
            }
        }
            if (ViewState["FLAG"].ToString() == "MD")
            {
                string cust = cm.mastercustno(txtMemNo.Text);
                if (cust ==txtMemNo.Text)
                {
                    DT = cm.addmastview(txtMemNo.Text, Session["BRCD"].ToString(), txtAddress.Text, txtPincode.Text);
                    if (DT.Rows.Count > 0)
                    {
                        //txtMemNo.Text = DT.Rows[0]["CUSTNO"].ToString();
                        txtAddress.Text = DT.Rows[0]["Address"].ToString();
                        txtPincode.Text = DT.Rows[0]["pincode"].ToString();
                    }
                    string name = cm.masterView(txtMemNo.Text);
                    txtName.Text = name;
                }
            }
                if (ViewState["FLAG"].ToString() == "DEL")
            {
                string cust = cm.mastercustno(txtMemNo.Text);
                if (cust ==txtMemNo.Text)
                {
                    DT = cm.addmastview(txtMemNo.Text, Session["BRCD"].ToString(), txtAddress.Text, txtPincode.Text);
                    if (DT.Rows.Count > 0)
                    {
                        //txtMemNo.Text = DT.Rows[0]["CUSTNO"].ToString();
                        txtAddress.Text = DT.Rows[0]["Address"].ToString();
                        txtPincode.Text = DT.Rows[0]["pincode"].ToString();
                    }
                    string name = cm.masterView(txtMemNo.Text);
                    txtName.Text = name;
                }
            }
                else
                {
                   // WebMsgBox.Show("Member No already Present", this.Page);

                }



            }

        
    
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmS0002.aspx");
    }
}