using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class FrmAVS5018 : System.Web.UI.Page
{

    DataTable dt = new DataTable();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
    DataTable DT = new DataTable();
    ClsCommon CMN = new ClsCommon();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsOpenClose OC = new ClsOpenClose();
    DbConnection conn = new DbConnection();
    Datecall DS = new Datecall();
    scustom customcs = new scustom();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCashReciept CurrentCls = new ClsCashReciept();
    ClsAuthorized PVOUCHER = new ClsAuthorized();
    InwordCharges DSS = new InwordCharges();

    string FL = "";
    ClsInwordOutwordCharges ClsInOut = new ClsInwordOutwordCharges();
    //int resultint;
    //string res;
    //int nonmi1;

    int result = 0;
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
                Bindgrid();
                BD.Bindreason(DdlReason);
                BD.BindPercentage(DDLPercentage);
                BD.BindFlatCharges(DDLFLatCharges);
                BD.BindFrequencyType(DDLFrequency);
                BD.BindAllowType(DDLAllow);
                BD.BindSkipType(DDLSkipCharges);
                BD.BindRetrunType(DDLReturn);
                BD.BindReasons(DdlReason);
                txtBrcode.Text = Session["BRCD"].ToString();
                txtBranch.Text = Session["BName"].ToString();
                txtEffectivedate.Text = Session["EntryDate"].ToString();
                BD.BindACCTYPE(Ddlacctype);
                txtAccType.Enabled = false;
                TxtTReturnType.Enabled = false;
                TxtSkipcharges.Enabled = false;
                TxtAllowTOD.Enabled = false;
                TxtFrequencyAppln.Enabled = false;
                TxtReason.Enabled = false;
                TxtflatChg.Enabled = false;
                Txtpert.Enabled = false;
                ClearData();
                DDLReturn.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void txtBrcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBrcode.Text != "")
            {
                string bname = AST.GetBranchName(txtBrcode.Text);
                if (bname != null)
                {
                    txtBranch.Text = bname;
                    DDLReturn.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    txtBrcode.Text = "";
                    txtAccType.Focus();
                    DDLReturn.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                txtBrcode.Text = "";
                DDLReturn.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }


    protected void AssignValues(DataTable dt)
    {
        try
        {
            TxtTReturnType.Text = dt.Rows[0]["Retrun_Type"].ToString();
            DDLReturn.SelectedValue = dt.Rows[0]["Retrun_Type"].ToString();

            txtAccType.Text = dt.Rows[0]["Acct_Type"].ToString();
          
            txtEffectivedate.Text = dt.Rows[0]["Effective_Date"].ToString();
            DDLSkipCharges.SelectedValue = dt.Rows[0]["Skip_charges"].ToString();
            TxtSkipcharges.Text = dt.Rows[0]["Skip_charges"].ToString();
            TxtFrequencyAppln.Text = dt.Rows[0]["Frequency"].ToString();
            DDLFrequency.SelectedValue = dt.Rows[0]["Frequency"].ToString();
            TxtAllowTOD.Text = dt.Rows[0]["Skip_charges"].ToString();
            DDLAllow.SelectedValue = dt.Rows[0]["Allow"].ToString();
            txtFlatrate.Text = dt.Rows[0]["Flat_rate"].ToString();
            txtInsterest.Text = dt.Rows[0]["Interset_rate"].ToString();
            txtdAcctid.Text = dt.Rows[0]["PLACCNO"].ToString();
          // txtdAccNo.Text = dt.Rows[0]["PLACCNO"].ToString();
            Txtmaxcharges.Text = dt.Rows[0]["max_charges"].ToString();
            Txtmincharges.Text = dt.Rows[0]["min_charges"].ToString();
            TxtGst.Text = dt.Rows[0]["GST_SUBGL"].ToString();
       //   TxtGstGL.Text = dt.Rows[0]["GST_SUBGL"].ToString();
            Txtgstinster.Text = dt.Rows[0]["GST_InterestRate"].ToString();
            TxtGstAmount.Text = dt.Rows[0]["GST_Amount"].ToString();
            TxtReason.Text = dt.Rows[0]["Reason"].ToString();
            DdlReason.SelectedValue = dt.Rows[0]["Reason_description"].ToString();
            TxtReason.Text = dt.Rows[0]["Flat_Charges"].ToString();
            DDLFLatCharges.SelectedValue = dt.Rows[0]["Flat_Charges"].ToString();
            TxtReason.Text = dt.Rows[0]["Percent_Charge"].ToString();
            DDLPercentage.SelectedValue = dt.Rows[0]["Percent_Charge"].ToString();
            txtLastAppDate.Text = dt.Rows[0]["Last_ApplDate"].ToString();
            txtParticular.Text = dt.Rows[0]["Particular"].ToString();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    public void ClearData()
    {
        txtAccType.Text = "";
        DDLAllow.SelectedIndex = 0;
        TxtAllowTOD.Text = "";
        txtdAccNo.Text = "";
        Txtpert.Text = "";
        DDLPercentage.SelectedIndex = 0;
        txtParticular.Text = "";
        TxtTReturnType.Text = "";
        TxtReason.Text = "";
        txtdAcctid.Text = "";
        DDLFLatCharges.SelectedIndex = 0;
        txtEffectivedate.Text = "";
        TxtflatChg.Text = "";
        TxtSkipcharges.Text = "";
        DDLReturn.SelectedIndex = 0;
        txtLastAppDate.Text = "";
        txtInsterest.Text = "";
        Txtmaxcharges.Text = "";
        Txtmincharges.Text = "";
        DdlReason.SelectedIndex = 0;
        DDLFrequency.SelectedIndex = 0;
        TxtGst.Text = "";
        TxtGstAmount.Text = "";
        Txtgstinster.Text = "";
        txtFlatrate.Text = "";


    }

    protected void btnOk_Click(object sender, EventArgs e)
    {

    }

    protected void TxtTReturnType_TextChanged(object sender, EventArgs e)
    {

        //if (TxtTReturnType.Text == "i" || TxtTReturnType.Text == "I")
        //    TxtTReturn.Text = "INWORD RETURN";
        //else if (TxtTReturnType.Text == "o" || TxtTReturnType.Text == "O")
        //    TxtTReturn.Text = "OUTWORD RETURN";
    }
    protected void TxtFrequencyAppln_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (TxtFrequencyAppln.Text == "M" || TxtFrequencyAppln.Text == "m")
        //        TxtFrequency.Text = "Monthly";
        //    else if (TxtFrequencyAppln.Text == "D" || TxtFrequencyAppln.Text == "d")
        //        TxtFrequency.Text = "Daily";
        //    else if (TxtFrequencyAppln.Text == "Q" || TxtFrequencyAppln.Text == "q")
        //        TxtFrequency.Text = "Quaterly";
        //    else if (TxtFrequencyAppln.Text == "Y" || TxtFrequencyAppln.Text == "y")
        //        TxtFrequency.Text = "Yearly";
        //    else if (TxtFrequencyAppln.Text == "H" || TxtFrequencyAppln.Text == "h")
        //        TxtFrequency.Text = "Half Yearly";


        //}
        //catch (Exception ex)
        //{
        //}
    }
    protected void TxtAllowTOD_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (TxtAllowTOD.Text == "N" || TxtAllowTOD.Text == "n")
        //        TxtAllow.Text = "NO";
        //    else if (TxtAllowTOD.Text == "Y" || TxtAllowTOD.Text == "y")
        //        TxtAllow.Text = "YES";
        //}
        //catch (Exception ex)
        //{
        //}
    }
    protected void TxtSkipcharges_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (TxtSkipcharges.Text == "N" || TxtSkipcharges.Text == "n")
        //        TxtSkipChargesType.Text = "No";
        //    else if (TxtSkipcharges.Text == "Y" || TxtSkipcharges.Text == "y")
        //        TxtSkipChargesType.Text = "YES";
        //}
        //catch (Exception ex)
        //{
        //}
    }
    protected void TxtflatChg_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (TxtflatChg.Text == "Y" || TxtflatChg.Text == "y")
        //        TxtFlatchar.Text = "YES";
        //    else if (TxtflatChg.Text == "N" || TxtflatChg.Text == "n")
        //        TxtFlatchar.Text = "NO";

        //}
        //catch (Exception ex)
        //{
        //}
    }

    protected void Txtpert_TextChanged(object sender, EventArgs e)
    {
        //if (Txtpert.Text == "Y" || Txtpert.Text == "y")
        //    Txtper.Text = "YES";
        //else if (Txtpert.Text == "N" || Txtpert.Text == "n")
        //    Txtper.Text = "NO";

    }
    protected void txtdAcctid_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(txtdAcctid.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["DRGL"] = GL[1].ToString();
            // AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtdAcctid.Text + "_" + ViewState["DRGL"].ToString();
            string PDName = customcs.GetProductName(txtdAcctid.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                txtdAccNo.Text = PDName;
                Txtmaxcharges.Focus();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                txtdAcctid.Text = "";
                Txtmaxcharges.Focus();
            }


        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }



    protected void txtdAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtdAccNo.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtdAccNo.Text = CT[0].ToString();
                txtdAcctid.Text = CT[1].ToString();
                txtdAccNo.Focus();
                string[] GLS = BD.GetAccTypeGL(txtdAcctid.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtdAcctid.Text + "_" + ViewState["DRGL"].ToString();
                Txtmaxcharges.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtReason_TextChanged(object sender, EventArgs e)
    {
        //if (TxtReason.Text == "1")
        //    DdlReason.SelectedItem.Text = "NOT DRWAN ON US";
        //else if (TxtReason.Text == "2")
        //    DdlReason.SelectedItem.Text = "OUR BR. NAME REQUIRED";
        //else if (TxtReason.Text == "3")
        //    DdlReason.SelectedItem.Text = "PAYESS ENDORSEMENT IRREGULAR";
        //else if (TxtReason.Text == "4")
        //    DdlReason.SelectedItem.Text = "PAYESS ENDORSEMENT REQUIRED";
        //else if (TxtReason.Text == "5")
        //    DdlReason.SelectedItem.Text = "PAYESS SEPARATE DISCHARGE REQUIRED";
        //else if (TxtReason.Text == "6")
        //    DdlReason.SelectedItem.Text = "PAYMENT STOPPED BY THE DRAWER";
    }





    protected void Ddlacctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtAccType.Text = BD.GetNOACCT(Ddlacctype.SelectedItem.Text);
            txtAccType.Text = Ddlacctype.SelectedValue;
            txtEffectivedate.Focus();
            //DdlModeofOpr.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void txtAccType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string acct;
            acct = BD.GetAccTType(txtAccType.Text);
            if (acct == null)
            {
                WebMsgBox.Show("Enter valid account type!!....", this.Page);
                txtAccType.Text = "";
                txtAccType.Focus();
            }
            else
            {
                Ddlacctype.SelectedValue = acct;
                //txtmopr.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }


    protected void DDLReturn_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            TxtTReturnType.Text = BD.GetRetrunType(DDLReturn.SelectedItem.Text.ToString());
            TxtTReturnType.Text = DDLReturn.SelectedValue;
            Ddlacctype.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    protected void DDLSkipCharges_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            TxtSkipcharges.Text = BD.GetSkipType(DDLSkipCharges.SelectedItem.Text.ToString());
            TxtSkipcharges.Text = DDLSkipCharges.SelectedValue;
            DDLAllow.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DDLAllow_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            TxtAllowTOD.Text = BD.GetAllowType(DDLAllow.SelectedItem.Text.ToString());
            TxtAllowTOD.Text = DDLAllow.SelectedValue;
            DDLFrequency.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void DDLFrequency_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            TxtFrequencyAppln.Text = BD.GetFrequencyType(DDLFrequency.SelectedItem.Text.ToString());
            TxtFrequencyAppln.Text = DDLFrequency.SelectedValue;
            txtFlatrate.Focus();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DDLFLatCharges_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            TxtflatChg.Text = BD.GetFlatCharges(DDLFLatCharges.SelectedItem.Text.ToString());
            TxtflatChg.Text = DDLFLatCharges.SelectedValue;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DDLPercentage_SelectedIndexChanged(object sender, EventArgs e)
    {
        {

            try
            {
                Txtpert.Text = BD.GetPercentageCharges(DDLPercentage.SelectedItem.Text.ToString());
                Txtpert.Text = DDLPercentage.SelectedValue;

            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }
    protected void DdlReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        {

            try
            {
                TxtReason.Text = BD.GetReason(DdlReason.SelectedItem.Text.ToString());
                TxtReason.Text = DdlReason.SelectedValue;
                DDLFLatCharges.Focus();
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }

        }
    }


    protected void Bindgrid()
    {
        try
        {
            ClsInOut.GetIntrestMaster(grdIntrstMaster, Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void grdIntrstMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdIntrstMaster.PageIndex = e.NewPageIndex;
        Bindgrid();

    }


    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            result = 0;
            result = ClsInOut.EntryInterest(Session["BRCD"].ToString(), txtAccType.Text, DDLReturn.SelectedValue.ToString(), Session["EntryDate"].ToString(), TxtSkipcharges.Text, DDLFrequency.SelectedValue.ToString(), DDLAllow.SelectedValue.ToString(), txtFlatrate.Text, txtInsterest.Text, txtdAcctid.Text, Txtmaxcharges.Text, Txtmincharges.Text, TxtGst.Text, Txtgstinster.Text, TxtGstAmount.Text, TxtReason.Text, DdlReason.SelectedValue.ToString(), DDLFLatCharges.SelectedValue.ToString(), DDLPercentage.SelectedValue.ToString(), txtLastAppDate.Text, txtParticular.Text);
            if (result >= 0)
            {

                WebMsgBox.Show("Data Added successfully..!!", this.Page);
                Bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "INOTchargmst_Add _" + txtAccType.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnModify_Click(object sender, EventArgs e)
    {

        try
        {
            result = ClsInOut.ModifyIntrestMaster(ViewState["ID"].ToString(), Session["BRCD"].ToString(), txtAccType.Text, DDLReturn.SelectedValue.ToString(), Session["EntryDate"].ToString(), TxtSkipcharges.Text, DDLFrequency.SelectedValue.ToString(), DDLAllow.SelectedValue.ToString(), txtFlatrate.Text, txtInsterest.Text, txtdAcctid.Text, Txtmaxcharges.Text, Txtmincharges.Text, TxtGst.Text, Txtgstinster.Text, TxtGstAmount.Text, TxtReason.Text, DdlReason.SelectedValue.ToString(), DDLFLatCharges.SelectedValue.ToString(), DDLPercentage.SelectedValue.ToString(), txtLastAppDate.Text, txtParticular.Text);
            if (result >= 0)
            {
                Bindgrid();
                WebMsgBox.Show("Data Modified Successfully.....!", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "INOTchargmst_Mod _" + txtAccType.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
                
            }
            else
            {
                WebMsgBox.Show("Data Modified UNSuccessfully.....!", this.Page);
                ClearData();
            }

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

            result = ClsInOut.DeleteIntMast(Session["BRCD"].ToString(), ViewState["ID"].ToString());
            if (result > 0)
            {
                WebMsgBox.Show("Deleted Succesfully......!", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "INOTchargmst_Del _" + txtAccType.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
            }
            Bindgrid();


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    //public void Bindgrid()  
    //   {
    //       try
    //       {


    //           SqlCommand cmd = new SqlCommand("select * from AVS5018");
    //           SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //           DataSet ds = new DataSet();
    //           sda.Fill(ds, "AVS5018");
    //           grdIntrstMaster.DataSource = ds;
    //           grdIntrstMaster.DataBind();
    //       }

    // catch (Exception Ex)
    //       {
    // }


    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        BtnSubmit.Visible = true;
        BtnModify.Visible = false;
        BtnDelete.Visible = false;
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            BtnSubmit.Visible = false;
            BtnModify.Visible = true;
            BtnDelete.Visible = false;
            LinkButton objlink = (LinkButton)sender;
            string ID = objlink.CommandArgument;
            ViewState["ID"] = ID;
            dt = ClsInOut.GetIntrestMasterID(ID, Session["BRCD"].ToString());
            AssignValues(dt);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            BtnSubmit.Visible = false;
            BtnModify.Visible = false;
            BtnDelete.Visible = true;
            LinkButton objlink = (LinkButton)sender;
            string ID = objlink.CommandArgument;
            ViewState["ID"] = ID;
            dt = ClsInOut.GetIntrestMasterID(ID, Session["BRCD"].ToString());
            AssignValues(dt);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }




    protected void lnkView_Click(object sender, EventArgs e)
    {


    }
    protected void BtnAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            result = 0;
            result = ClsInOut.EntryAuthorize(ID, Session["BRCD"].ToString(), Session["MID"].ToString());
            if (result >= 0)
            {

                WebMsgBox.Show("Data Authoriztion successfully..!!", this.Page);
                Bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "INOTchargmst_Auth _" + txtAccType.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    protected void lnkAuthor_Click(object sender, EventArgs e)
    {
        try
        {
            BtnSubmit.Visible = false;
            BtnModify.Visible = false;
            BtnDelete.Visible = false;
            BtnAuthorize.Visible = true;
            LinkButton objlink = (LinkButton)sender;
            string ID = objlink.CommandArgument;
            ViewState["ID"] = ID;
            dt = ClsInOut.GetIntrestMasterID(ID, Session["BRCD"].ToString());
            AssignValues(dt);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void txtEffectivedate_TextChanged(object sender, EventArgs e)
    {
        DDLSkipCharges.Focus();
    }
    protected void TxtGstGL_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtGstGL.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtGstGL.Text = CT[0].ToString();
                TxtGst.Text = CT[1].ToString();
                txtdAccNo.Focus();
                string[] GLS = BD.GetAccTypeGL(TxtGst.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtdAcctid.Text + "_" + ViewState["DRGL"].ToString();
                Txtmaxcharges.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtGst_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(TxtGst.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["DRGL"] = GL[1].ToString();
            //  autoglnameA.ContextKey = Session["BRCD"].ToString() + "_" + TxtGst.Text + "_" + ViewState["DRGL"].ToString();
            string PDName = customcs.GetProductName(TxtGst.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                TxtGstGL.Text = PDName;
                TxtGst.Focus();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                TxtGst.Text = "";
                TxtGst.Focus();
            }


        }   
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }

    }

    protected void BtnClear_Click1(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void BtnExit_Click1(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
}