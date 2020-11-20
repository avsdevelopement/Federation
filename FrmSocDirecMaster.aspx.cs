using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class FrmSocDirecMaster : System.Web.UI.Page
{
    ClsCo_master cm = new ClsCo_master();
    DbConnection conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCustNo.Focus();
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }

    }



    protected void BtnCreat_Click(object sender, EventArgs e)
    {
        if (txtCustNo.Text == "")
        {
            WebMsgBox.Show(" Please Insert Client No",this.Page);
        }
        if (txtDirName.Text == "")
        {
            WebMsgBox.Show(" Please Insert Director Name", this.Page);
        }
        if (txtDesignation.Text == "")
        {
            WebMsgBox.Show("Please insert Designation for director", this.Page);
        }
        if (txtAdd1.Text == "" && txtAdd2.Text == "")
        {
            WebMsgBox.Show("pls insert Address of Director", this.Page);
        }
        if(txtOffAdd.Text=="" && txtNatAdd.Text=="")
        {
         WebMsgBox.Show("pls insert Address of Director", this.Page);
        }
        if (txtMOb1.Text == "")
        {
            WebMsgBox.Show("pls insert mobile no ..", this.Page);
        }
        if (txtPanNo.Text == "")
        {
            WebMsgBox.Show("pls insert pan card no",this.Page);

        }
        if (txtadhar.Text == "")
        {
            WebMsgBox.Show("pls insert adhar card no ..", this.Page);
        }
        else
        {
            int rc = cm.insertcooprative(Session["LOGINCODE"].ToString(), Session["BRCD"].ToString(), txtCustNo.Text, TxtSociety.Text, txtDirName.Text, txtDesignation.Text, txtAdd1.Text, txtAdd2.Text, txtCity.Text, txtPinCode.Text, txtOffAdd.Text, txtNatAdd.Text, txtMOb1.Text, txtMob2.Text, txtDOB.Text, txtPanNo.Text, txtadhar.Text, TxtSrno.Text);
            if (rc > 0)
            {
                WebMsgBox.Show("Record is inserted successfully", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SocietyDirector_Add _" + txtCustNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clear();
                getsrno();


            }
            else
            {
                WebMsgBox.Show("Record not  found", this.Page);
            }
        }
    }
    public int bindgrid()
    {
        int RC = cm.bindgrid(txtCustNo.Text,Session["BRCD"].ToString(), grdvoucher);
        return RC;
    }
    public void clear()
    {
        txtDirName.Text = "";
        txtDesignation.Text = "";
        txtAdd1.Text = "";
        txtAdd2.Text = "";
        txtCity.Text = "";
        txtPinCode.Text = "";
        txtOffAdd.Text = "";
        txtNatAdd.Text = "";
        txtMOb1.Text = "";
        txtMob2.Text = "";
        txtDOB.Text = "";
        txtPanNo.Text = "";
        txtadhar.Text = "";
        TxtSrno.Text = "";
    }

    public void CLEARDATA()
    {
        txtCustNo.Text = "";
        TxtSociety.Text = "";
        txtDirName.Text = "";
        txtDesignation.Text = "";
        txtAdd1.Text = "";
        txtAdd2.Text = "";
        txtCity.Text = "";
        txtPinCode.Text = "";
        txtOffAdd.Text = "";
        txtNatAdd.Text = "";
        txtMOb1.Text = "";
        txtMob2.Text = "";
        txtDOB.Text = "";
        txtPanNo.Text = "";
        txtadhar.Text = "";
        TxtSrno.Text = "";

    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
         int RC=cm.modifycorporate(Session["LOGINCODE"].ToString(),Session["BRCD"].ToString(), txtCustNo.Text, TxtSociety.Text, txtDirName.Text, txtDesignation.Text, txtAdd1.Text, txtAdd2.Text, txtCity.Text, txtPinCode.Text, txtOffAdd.Text, txtNatAdd.Text, txtMOb1.Text, txtMob2.Text, txtDOB.Text, txtPanNo.Text, txtadhar.Text,TxtSrno.Text);
         if (RC > 0)
         {

             WebMsgBox.Show("Record is modified successfully", this.Page);
             bindgrid();
             FL = "Insert";//Dhanya Shetty
             string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SocietyDirector_Mod _" + txtCustNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
             CLEARDATA();
           
         }
         else
         {
             WebMsgBox.Show("Record Not Found", this.Page);
         }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (txtCustNo.Text == "")
        {
            WebMsgBox.Show("pls insert client no for delation..", this.Page);
        }
        else
        {
            int RC = cm.deletecorporate(Session["LOGINCODE"].ToString(), Session["BRCD"].ToString(), TxtSrno.Text, txtCustNo.Text);
            if (RC > 0)
            {
                WebMsgBox.Show("Record is Deleted Successfully...", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SocietyDirector_Del _" + txtCustNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                CLEARDATA();
                
            }
            else
            {
                WebMsgBox.Show("record is not deleted", this.Page);
            }
        }
    }
  
    protected void txtCustNo_TextChanged(object sender, EventArgs e)
    {
        getsrno();
        bindgrid();
        txtDirName.Focus();
    }
    public void getsrno()
    {
        string rc = cm.getsoci(txtCustNo.Text, Session["brcd"].ToString());
        TxtSociety.Text = rc;
        string r = cm.getdetails(txtCustNo.Text);
        int re = Convert.ToInt32(r) + 1;
        TxtSrno.Text = re.ToString();
    }


    protected void lnkedit_Click(object sender, EventArgs e)
    {
          LinkButton lnkedit = (LinkButton)sender;
        string str = lnkedit.CommandArgument.ToString();
        string[] ARR = str.Split(',');
        ViewState["SrNo"] = ARR[0].ToString();
        ViewState["SocietyNo"] = ARR[1].ToString();
        BtnCreat.Visible = false;
    }
    public void data()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = cm.showdata(ViewState["SrNo"].ToString(), ViewState["SocietyNo"].ToString(), Session["BRCD"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtCustNo.Text = DT.Rows[0]["CUSTNO"].ToString();
                TxtSociety.Text = DT.Rows[0]["SOCIETYNAME"].ToString();
                TxtSrno.Text = DT.Rows[0]["SrNo"].ToString();
                txtDirName.Text = DT.Rows[0]["DIRECTORNAME"].ToString();
                txtDesignation.Text = DT.Rows[0]["DESIGNATION"].ToString();
                txtAdd1.Text = DT.Rows[0]["ADDRESS1"].ToString();
                txtAdd2.Text = DT.Rows[0]["ADDRESS2"].ToString();
                txtCity.Text = DT.Rows[0]["CITY"].ToString();
                txtPinCode.Text = DT.Rows[0]["PINCODE"].ToString();
                txtOffAdd.Text = DT.Rows[0]["OFFADRR"].ToString();
                txtNatAdd.Text = DT.Rows[0]["NATADDR"].ToString();
                txtMOb1.Text = DT.Rows[0]["MOBILE1"].ToString();
                txtMob2.Text = DT.Rows[0]["MOBILE2"].ToString();
                txtDOB.Text = DT.Rows[0]["DOB"].ToString();
                txtPanNo.Text = DT.Rows[0]["PANCARD"].ToString();
                txtadhar.Text = DT.Rows[0]["ADHARCARD"].ToString();

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
    protected void grdvoucher_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdvoucher_SelectedIndexChanged(object sender, EventArgs e)
    {
        data();
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        CLEARDATA();
    }
    protected void txtadhar_TextChanged(object sender, EventArgs e)
    {
        BtnCreat.Focus();
    }
    protected void GRDCUSTCO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GRDCUSTCO_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}