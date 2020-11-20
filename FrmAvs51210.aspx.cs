using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAvs51210 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
    DataTable DT = new DataTable();
    ClsCaseStatus ccs = new ClsCaseStatus();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int result = 0;
    string sroname = "", AC_Status = "", results = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
            Response.Redirect("FrmLogin.aspx");
        if (!IsPostBack)
        {
            BindGrid();
            BtnSubmit.Visible = false;
            //ViewState["Flag"] = Request.QueryString["Flag"].ToString();
            if (Convert.ToString(ViewState["Flag"]) == "AD")
            {
                BtnSubmit.Text = "Add";
            }
            else if (Convert.ToString(ViewState["Flag"]) == "MD")
            {
                BtnSubmit.Text = "Modify";
            }
            else if (Convert.ToString(ViewState["Flag"]) == "AT")
            {
                BtnSubmit.Text = "Authorise";
            }
            else if (Convert.ToString(ViewState["Flag"]) == "CN")
            {
                BtnSubmit.Text = "Cancel";
            }
        }
    }
    public void cleardata()
    {
        txtCaseY.Text = "";
        txtCaseNO.Text = "";
        txtDate.Text = "";
        txtmem.Text = "";
        txtSocietyName.Text = "";
        txtSociadd.Text = "";
        txtdefaltername.Text = "";
        txtCity.Text = "";
        txtPincode.Text = "";
        txtrcno.Text = "";
        txtMovDate.Text = "";
        //txtDecretalName.Text = "";
        txtDecretalAmount.Text = "";
        txtCaseStatus.Text = "";
        txtCaseStatusName.Text = "";
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "AD";
        if (Convert.ToString(ViewState["Flag"]) == "AD")
        {
            BtnSubmit.Visible = true;
            BtnSubmit.Text = "Add";
            txtDate.Enabled = true;
            txtmem.Enabled = true;
            txtSocietyName.Enabled = true;
            txtSociadd.Enabled = true;
            txtdefaltername.Enabled = true;
            txtCity.Enabled = true;
            txtPincode.Enabled = true;
            txtrcno.Enabled = true;
            txtMovDate.Enabled = true;
            txtDecretalAmount.Enabled = true;
            txtCaseStatus.Enabled = true;
            txtCaseStatusName.Enabled = true;
           // txtDecretalName.Enabled = true;
        }
    }

    protected void lnkModify_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "MD";
        if (Convert.ToString(ViewState["Flag"]) == "MD")
        {
            BtnSubmit.Visible = true;
            BtnSubmit.Text = "Modify";
            txtDate.Enabled = true;
            txtmem.Enabled = true;
            txtSocietyName.Enabled = true;
            txtSociadd.Enabled = true;
            txtdefaltername.Enabled = true;
            txtCity.Enabled = true;
            txtPincode.Enabled = true;
            txtrcno.Enabled = true;
            txtMovDate.Enabled = true;
            txtDecretalAmount.Enabled = true;
            txtCaseStatus.Enabled = true;
            txtCaseStatusName.Enabled = true;
           // txtDecretalName.Enabled = true;
        }

    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "CN";
        if (Convert.ToString(ViewState["Flag"]) == "CN")
        {
            BtnSubmit.Text = "Cancel";
        }
    }
    protected void lnkAuthorized_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "AT";
        if (Convert.ToString(ViewState["Flag"]) == "AT")
        {
            BtnSubmit.Text = "Authorise";
            BtnSubmit.Visible = true;
            txtDate.Enabled = false;
            txtmem.Enabled = false;
            txtSocietyName.Enabled = false;
            txtSociadd.Enabled = false;
            txtdefaltername.Enabled = false;
            txtCity.Enabled = false;
            txtPincode.Enabled = false;
            txtrcno.Enabled = false;
            txtMovDate.Enabled = false;
            txtDecretalAmount.Enabled = false;
            txtCaseStatus.Enabled = false;
            txtCaseStatusName.Enabled = false;
           // txtDecretalName.Enabled = false;
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (txtCaseY.Text == "")
        {
            WebMsgBox.Show("Please Enter Case Year", this.Page);
            return;
        }
        if (txtCaseNO.Text == "")
        {
            WebMsgBox.Show("Please Enter Case No", this.Page);
            return;
        }
        if (txtDate.Text == "")
        {
            WebMsgBox.Show("Please Enter Date", this.Page);
            return;
        }
        if (txtmem.Text == "")
        {
            WebMsgBox.Show("Please Enter Society Type & MemberNo", this.Page);
            return;
        }
        if (txtSocietyName.Text == "")
        {
            WebMsgBox.Show("Please Enter Society Name", this.Page);
            return;
        }
        if (txtSociadd.Text == "")
        {
            WebMsgBox.Show("Please Enter Society Address", this.Page);
            return;
        }
        if (txtdefaltername.Text == "")
        {
            WebMsgBox.Show("Please Enter Defaulter Name", this.Page);
            return;
        }
        if (txtCity.Text == "")
        {
            WebMsgBox.Show("Please Enter City", this.Page);
            return;
        }
        if (txtPincode.Text == "")
        {
            WebMsgBox.Show("Please Enter Pincode", this.Page);
            return;
        }
        if (txtrcno.Text == "")
        {
            WebMsgBox.Show("Please Enter R.C.NO", this.Page);
            return;
        }
        if (txtMovDate.Text == "")
        {
            WebMsgBox.Show("Please Enter R.C.DATE", this.Page);
            return;
        }
        //if (txtDecretalName.Text == "")
        //{
        //    WebMsgBox.Show("Please Enter Decretal Name", this.Page);
        //    return;
        //}
        if (txtdefaltername.Text == "")
        {
            WebMsgBox.Show("Please Enter defalter name", this.Page);
            return;
        }
        if (txtDecretalAmount.Text == "")
        {
            WebMsgBox.Show("Please Enter Decretal Amount", this.Page);
            return;
        }
        if (txtCaseStatus.Text == "")
        {
            WebMsgBox.Show("Please Enter Case Status", this.Page);
            return;
        }
        if (txtCaseStatusName.Text == "")
        {
            WebMsgBox.Show("Please Enter Case Status Name", this.Page);
            return;
        }


        if (Convert.ToString(ViewState["Flag"]) == "AD")
        {
            BtnSubmit.Text = "Add";
            result = ccs.Insert(Session["BRCD"].ToString(), txtCaseY.Text, txtCaseNO.Text, txtDate.Text, txtmem.Text, txtSocietyName.Text, txtSociadd.Text, txtCity.Text, txtPincode.Text, txtrcno.Text, txtMovDate.Text,  txtdefaltername.Text, txtDecretalAmount.Text, txtCaseStatus.Text);
        }
        if (result > 0)
        {
            cleardata();
            BindGrid();
            WebMsgBox.Show("ADDED Successfully", this.Page);
            return;

        }
        else if (Convert.ToString(ViewState["Flag"]) == "MD")
        {
            BtnSubmit.Text = "Modify";

            result = ccs.Modify(Session["BRCD"].ToString(), txtCaseY.Text, txtCaseNO.Text, txtDate.Text, txtmem.Text, txtSocietyName.Text, txtSociadd.Text, txtCity.Text, txtPincode.Text, txtrcno.Text, txtMovDate.Text,  txtdefaltername.Text, txtDecretalAmount.Text, txtCaseStatus.Text);
            if (result > 0)
            {
                cleardata();
                BindGrid();
                WebMsgBox.Show("Modify Successfully", this.Page);
                return;

            }
        }
        else if (Convert.ToString(ViewState["Flag"]) == "AT")
        {
            BtnSubmit.Text = "Authorise";

            result = ccs.Authorise(Session["BRCD"].ToString(), txtCaseY.Text, txtCaseNO.Text, txtDate.Text, txtmem.Text, txtSocietyName.Text, txtSociadd.Text, txtCity.Text, txtPincode.Text, txtrcno.Text, txtMovDate.Text,  txtdefaltername.Text, txtDecretalAmount.Text, txtCaseStatus.Text);
            if (result > 0)
            {
                cleardata();
                BindGrid();
                WebMsgBox.Show("Authorise Successfully", this.Page);
                return;

            }
        }
        else if (Convert.ToString(ViewState["Flag"]) == "CN")
        {
            BtnSubmit.Text = "Cancel";

            result = ccs.Delete(Session["BRCD"].ToString(), txtCaseY.Text, txtCaseNO.Text, txtDate.Text, txtmem.Text, txtSocietyName.Text, txtSociadd.Text, txtCity.Text, txtPincode.Text, txtrcno.Text, txtMovDate.Text,  txtdefaltername.Text, txtDecretalAmount.Text, txtCaseStatus.Text);
            if (result > 0)
            {
                cleardata();
                BindGrid();
                WebMsgBox.Show("Delete Successfully", this.Page);
                return;

            }
        }
    }
    protected void txtCaseNO_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["Flag"]) != "AD")
        {
            DT = ccs.getdata(Session["BRCD"].ToString(), txtCaseY.Text, txtCaseNO.Text);
            if (DT.Rows.Count > 0)
            {
                txtDate.Text = DT.Rows[0]["APPLICTIONDATE"].ToString();
                txtmem.Text = DT.Rows[0]["societyTyp"].ToString();
                txtSocietyName.Text = DT.Rows[0]["SOCIETYNAME"].ToString();
                txtSociadd.Text = DT.Rows[0]["SOCIETYADDRESS"].ToString();
                txtCity.Text = DT.Rows[0]["CITY"].ToString();
                txtPincode.Text = DT.Rows[0]["PINCODE"].ToString();
                txtrcno.Text = DT.Rows[0]["RCNO"].ToString();
                txtMovDate.Text = DT.Rows[0]["RCDATE"].ToString();
                txtdefaltername.Text = DT.Rows[0]["DEFAULTERNAME"].ToString();
                //txtDecretalName.Text = DT.Rows[0]["DecretalName"].ToString();
                txtDecretalAmount.Text = DT.Rows[0]["DECRETALAMOUNT"].ToString();
                txtCaseStatus.Text = DT.Rows[0]["CASESTATUS"].ToString();
                string res = ccs.GetCaseStatus(txtCaseStatus.Text);
                txtCaseStatusName.Text = res;
            }
            else
            {
                WebMsgBox.Show("Record not present with Case year and Case No.", this.Page);
                return;
            }
        }
    }
    protected void txtCaseStatus_TextChanged(object sender, EventArgs e)
    {
        string res = ccs.GetCaseStatus(txtCaseStatus.Text);
        txtCaseStatusName.Text = res;
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        cleardata();
        BindGrid();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }
    public void BindGrid()
    {
        try
        {
            ccs.Getinfotable(GridCase, Session["BRCD"].ToString());

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}