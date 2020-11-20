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
public partial class AddcustDetails : System.Web.UI.Page
{
    ClsCo_master cm = new ClsCo_master();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            txtCustNo.Focus();
            cm.BINDRISK(ddlaml);
            cm.bindorgtype(ddlorgtype);
            cm.bindkyc(ddlTal);
        }
    }
    protected void TXTRELOFF_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        int RCC = cm.UPDATECODETAILS(txtturnover.Text, txtemp.Text, txtloc.Text, txtcurreny.Text, txtbank1.Text, txtreson.Text, ddlorgtype.SelectedValue, txtbusinesstype.Text, txtnoonbranch.Text, txtestd.Text, txtplaceestd.Text, txtregno.Text, ddlaml.SelectedValue, txtauth.Text, txtcommdt.Text, txtdatesc.Text, ddlTal.SelectedValue, TXTRELOFF.Text, txtadhar.Text, txtregplace.Text, txthoadd.Text, txtcity.Text, txtpin.Text, txtphone.Text, txtfax.Text, Session["BRCD"].ToString(),txtCustNo.Text);
        if (RCC > 0)
        {
            WebMsgBox.Show("Record is Inserted successfully..", this.Page);
            BINDDATA();
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Additional_Cust_Add _" + txtCustNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            CLEAR();

        }
        else
        {
            WebMsgBox.Show("Record not saved pls check all field", this.Page);
        }
    }
    protected void txtCustNo_TextChanged(object sender, EventArgs e)
    {
        string rc = cm.getsoci(txtCustNo.Text, Session["BRCD"].ToString());
        TxtSociety.Text = rc;
        BINDDATA();
        DATA2();
        txtturnover.Focus();
    }
    public void CLEAR()
    {
        txtturnover.Text = "";
        TxtSociety.Text = "";
        txtemp.Text = "";
        txtloc.Text = "";
        txtcurreny.Text = "";
        txtbank1.Text = "";
        txtreson.Text = "";
        ddlorgtype.SelectedValue= "0";
        txtbusinesstype.Text = "";
        txtnoonbranch.Text = "";
        txtestd.Text = "";
        txtplaceestd.Text = "";
        txtregno.Text = "";
        ddlaml.SelectedValue = "0";
        txtauth.Text = "";
        txtcommdt.Text = "";
        txtdatesc.Text = "";
        ddlTal.SelectedValue= "0";
        TXTRELOFF.Text = "";
        txtadhar.Text = "";
        txtregplace.Text = "";
        txthoadd.Text = "";
        txtcity.Text = "";
        txtpin.Text = "";
        txtphone.Text = "";
        txtfax.Text = "";
        txtCustNo.Text = "";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        CLEAR();
    }
    protected void BTNMOD_Click(object sender, EventArgs e)
    {
        int RCC = cm.UPDATECODETAILS(txtturnover.Text, txtemp.Text, txtloc.Text, txtcurreny.Text, txtbank1.Text, txtreson.Text, ddlorgtype.SelectedValue, txtbusinesstype.Text, txtnoonbranch.Text, txtestd.Text, txtplaceestd.Text, txtregno.Text, ddlaml.SelectedValue, txtauth.Text, txtcommdt.Text, txtdatesc.Text, ddlTal.SelectedValue, TXTRELOFF.Text, txtadhar.Text, txtregplace.Text, txthoadd.Text, txtcity.Text, txtpin.Text, txtphone.Text, txtfax.Text, Session["BRCD"].ToString(), txtCustNo.Text);
        if (RCC > 0)
        {
            WebMsgBox.Show("Record is MODIFIED successfully..", this.Page);
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Additional_Cust_Mod _" + txtCustNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            CLEAR();

        }
        else
        {
            WebMsgBox.Show("Record not MODIFIED pls check all field", this.Page);
        }
    }
    protected void lnkedit_Click(object sender, EventArgs e)
    {

        LinkButton lnkedit = (LinkButton)sender;
        string str = lnkedit.CommandArgument.ToString();
        ViewState["CUSTNO"] = str.ToString();
    }
    public void DATA1()
    {
        DT=cm.SHOWDATA11(ViewState["CUSTNO"].ToString(), Session["BRCD"].ToString());
        if (DT.Rows.Count > 0)
        {
            txtCustNo.Text = DT.Rows[0]["CUSTNO"].ToString();
            TxtSociety.Text = DT.Rows[0]["ORGNAME"].ToString();
            txtturnover.Text = DT.Rows[0]["TURNOVER"].ToString();
            txtemp.Text = DT.Rows[0]["NOOFEMP"].ToString();
            txtloc.Text = DT.Rows[0]["LOC"].ToString();
            txtcurreny.Text = DT.Rows[0]["CURRENCIES"].ToString();
            txtbank1.Text = DT.Rows[0]["OTHER_BANKERS"].ToString();
            txtreson.Text = DT.Rows[0]["RESONTOCUST"].ToString();
            ddlorgtype.SelectedValue = Convert.ToInt32(DT.Rows[0]["ORGTYPE"].ToString() == "" ? "0" : DT.Rows[0]["ORGTYPE"].ToString()).ToString();
            txtbusinesstype.Text = DT.Rows[0]["BUSLINE"].ToString();
            txtnoonbranch.Text = DT.Rows[0]["NOOFBR"].ToString();
            txtestd.Text = DT.Rows[0]["DATEESTD"].ToString();
            txtplaceestd.Text = DT.Rows[0]["PLACEOSESTD"].ToString();
            txtregno.Text = DT.Rows[0]["REGNO"].ToString();
            ddlaml.SelectedValue = Convert.ToInt32(DT.Rows[0]["AML_RATING"].ToString() == "" ? "0" : DT.Rows[0]["AML_RATING"].ToString()).ToString();
            txtauth.Text = DT.Rows[0]["REGAUTH"].ToString();
            txtcommdt.Text = DT.Rows[0]["COMDATE"].ToString();
            txtdatesc.Text = DT.Rows[0]["DATESCUST"].ToString();
            ddlTal.SelectedValue = Convert.ToInt32(DT.Rows[0]["KYC"].ToString() == "" ? "0" : DT.Rows[0]["KYC"].ToString()).ToString();
            TXTRELOFF.Text = DT.Rows[0]["RELOFF"].ToString();
            txtadhar.Text = DT.Rows[0]["ADHAR"].ToString();
            txtregplace.Text = DT.Rows[0]["REGPALCE"].ToString();
            txthoadd.Text = DT.Rows[0]["HO_ADD"].ToString();
            txtcity.Text = DT.Rows[0]["CITY"].ToString();
            txtpin.Text = DT.Rows[0]["PIN"].ToString();
            txtphone.Text = DT.Rows[0]["PHONENO"].ToString();
            txtfax.Text = DT.Rows[0]["FAXNO"].ToString();

         

        }
       

    }
    public void DATA2()
    {
        DT = cm.SHOWDATA11(txtCustNo.Text,Session["BRCD"].ToString());
        if (DT.Rows.Count > 0)
        {
            txtCustNo.Text = DT.Rows[0]["CUSTNO"].ToString();
            TxtSociety.Text = DT.Rows[0]["ORGNAME"].ToString();
            txtturnover.Text = DT.Rows[0]["TURNOVER"].ToString();
            txtemp.Text = DT.Rows[0]["NOOFEMP"].ToString();
            txtloc.Text = DT.Rows[0]["LOC"].ToString();
            txtcurreny.Text = DT.Rows[0]["CURRENCIES"].ToString();
            txtbank1.Text = DT.Rows[0]["OTHER_BANKERS"].ToString();
            txtreson.Text = DT.Rows[0]["RESONTOCUST"].ToString();
            ddlorgtype.SelectedValue = Convert.ToInt32(DT.Rows[0]["ORGTYPE"].ToString() == "" ? "0" : DT.Rows[0]["ORGTYPE"].ToString()).ToString();
            txtbusinesstype.Text = DT.Rows[0]["BUSLINE"].ToString();
            txtnoonbranch.Text = DT.Rows[0]["NOOFBR"].ToString();
            txtestd.Text = DT.Rows[0]["DATEESTD"].ToString();
            txtplaceestd.Text = DT.Rows[0]["PLACEOSESTD"].ToString();
            txtregno.Text = DT.Rows[0]["REGNO"].ToString();
            ddlaml.SelectedValue = Convert.ToInt32(DT.Rows[0]["AML_RATING"].ToString() == "" ? "0" : DT.Rows[0]["AML_RATING"].ToString()).ToString();
            txtauth.Text = DT.Rows[0]["REGAUTH"].ToString();
            txtcommdt.Text = DT.Rows[0]["COMDATE"].ToString();
            txtdatesc.Text = DT.Rows[0]["DATESCUST"].ToString();
            ddlTal.SelectedValue = Convert.ToInt32(DT.Rows[0]["KYC"].ToString() == "" ? "0" : DT.Rows[0]["KYC"].ToString()).ToString();
            TXTRELOFF.Text = DT.Rows[0]["RELOFF"].ToString();
            txtadhar.Text = DT.Rows[0]["ADHAR"].ToString();
            txtregplace.Text = DT.Rows[0]["REGPALCE"].ToString();
            txthoadd.Text = DT.Rows[0]["HO_ADD"].ToString();
            txtcity.Text = DT.Rows[0]["CITY"].ToString();
            txtpin.Text = DT.Rows[0]["PIN"].ToString();
            txtphone.Text = DT.Rows[0]["PHONENO"].ToString();
            txtfax.Text = DT.Rows[0]["FAXNO"].ToString();

        
        }
    }
    public void BINDDATA()
    {
        cm.BINDDATACO(txtCustNo.Text, Session["BRCD"].ToString(), SocietyGrid);
       
    }
    protected void SocietyGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        DATA1();
    }
    protected void ddlTal_SelectedIndexChanged(object sender, EventArgs e)
    {
        TXTRELOFF.Focus();
    }
}