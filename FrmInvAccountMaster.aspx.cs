using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmInvAccountMaster : System.Web.UI.Page
{
    ClsInvCreateReceipt ICR = new ClsInvCreateReceipt();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
    ClsInvAccountMaster IAM = new ClsInvAccountMaster();
    DataTable DT = new DataTable();
    int resultint;
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

                if (Session["UserName"] == null)
                    Response.Redirect("FrmLogin.aspx");
                btnCreate.Visible = false;
                btnModify.Visible = false;
                btnDelete.Visible = false;
                btnAuthorise.Visible = false;
                btnAdd.Visible = false;
                customcs.BindInvType(ddlInvestment);
                IntAutoGlName.ContextKey = Session["BRCD"].ToString();
                PrinAutoGlName.ContextKey = Session["BRCD"].ToString();
               // AutoGlName.ContextKey = Session["BRCD"].ToString();
                AutoCompleteExtender1.ContextKey = Session["BRCD"].ToString();
                AutoCompleteExtender2.ContextKey = Session["BRCD"].ToString();
                txtBankNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #region Click Event For LinkButton

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DivOld.Visible = false;
            DivNew.Visible = true;
            ViewState["Flag"] = "AD";
            lblActivity.Text = "ADD";

            btnCreate.Visible = true;
            btnModify.Visible = false;
            btnDelete.Visible = false;
            btnAuthorise.Visible = false;
            btnAdd.Visible = false;
            //txtPrinAccNo.Enabled = false;

            ClearData();

            txtBankNo.Text = Convert.ToInt32(IAM.GetMaxGlNo(Session["BRCD"].ToString())).ToString();
            //txtReceiptNo.Text = Convert.ToInt32(IAM.GetReceiptNo(Session["BRCD"].ToString())).ToString(); //Amruta 21/06/2017

            txtReceiptNo.Text = "1";
            txtAC.Text = "1";
            txtBResNo.Text = Convert.ToInt32(IAM.GetBoardResNo(Session["BRCD"].ToString())).ToString();

            txtBMeetDate.Text = Session["EntryDate"].ToString();
            txtOpenDate.Text = Session["EntryDate"].ToString();

            grdInvAccMaster.DataSource = null;
            grdInvAccMaster.DataBind();

            txtBankName.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            DivOld.Visible = false;
            DivNew.Visible = true;
            ViewState["Flag"] = "MD";
            lblActivity.Text = "MODIFY";

            btnCreate.Visible = false;
            btnModify.Visible = true;
            btnDelete.Visible = false;
            btnAuthorise.Visible = false;
            btnAdd.Visible = false;
            //txtPrinAccNo.Enabled = true;
            ClearData();
            BindGrid();

            txtOpenDate.Text = Session["EntryDate"].ToString();

            txtBankName.Focus();
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
            DivOld.Visible = false;
            DivNew.Visible = true;
            ViewState["Flag"] = "DL";
            lblActivity.Text = "DELETE";

            btnCreate.Visible = false;
            btnModify.Visible = false;
            btnDelete.Visible = true;
            btnAuthorise.Visible = false;
            btnAdd.Visible = false;
            //txtPrinAccNo.Enabled = false;

            ClearData();
            BindGrid();

            txtOpenDate.Text = Session["EntryDate"].ToString();

            txtBankName.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkAutho_Click(object sender, EventArgs e)
    {
        try
        {
            DivOld.Visible = false;
            DivNew.Visible = true;
            ViewState["Flag"] = "AT";
            lblActivity.Text = "AUTHORISE";

            btnCreate.Visible = false;
            btnModify.Visible = false;
            btnDelete.Visible = false;
            btnAuthorise.Visible = true;
            btnAdd.Visible = false;
            //txtPrinAccNo.Enabled = false;


            ClearData();
            BindGrid();

            txtOpenDate.Text = Session["EntryDate"].ToString();

            txtBankName.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            DivOld.Visible = false;
            DivNew.Visible = true;
            ViewState["Flag"] = "VW";
            lblActivity.Text = "VIEW";

            btnCreate.Visible = false;
            btnModify.Visible = false;
            btnDelete.Visible = false;
            btnAuthorise.Visible = false;
            btnAdd.Visible = false;
            //txtPrinAccNo.Enabled = false;

            ClearData();
            BindVerifyData();

            txtOpenDate.Text = Session["EntryDate"].ToString();

            txtBankName.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkExist_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "AE";
            lblActivity.Text = "Add Existing";
            DivNew.Visible = false;
            DivOld.Visible = true;
            btnCreate.Visible = false;
            btnModify.Visible = false;
            btnDelete.Visible = false;
            btnAuthorise.Visible = false;
            btnAdd.Visible = true;
            //txtPrinAccNo.Enabled = false;
            ClearData();

            //txtReceiptNo.Text = Convert.ToInt32(IAM.GetReceiptNo(Session["BRCD"].ToString())).ToString(); //Amruta 21/06/2017
            //txtBResNo.Text = Convert.ToInt32(IAM.GetBoardResNo(Session["BRCD"].ToString())).ToString();

            txtBMeetDate.Text = Session["EntryDate"].ToString();
            txtOpenDate.Text = Session["EntryDate"].ToString();

            grdInvAccMaster.DataSource = null;
            grdInvAccMaster.DataBind();

            txtBankName.Focus();
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }
    #endregion

    #region Click Events For Button
    protected void btnAdd_Click(object sender, EventArgs e)// Amruta 01/06/2017
    {
        if (lblActivity.Text != "")
        {
            try
            {
                resultint = IAM.InsertExistingData(Session["BRCD"].ToString(), "0", txtBankName1.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), txtBranch.Text.Trim().ToString(), txtReceiptNo.Text.Trim().ToString(), txtReceiptName.Text.Trim().ToString(), txtBResNo.Text.Trim().ToString(), txtBMeetDate.Text.Trim().ToString(), txtOpenDate.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), txtPCode.Text, txtPType.Text, ddlInvestment.SelectedValue, txtPrinAccName.Text, txtIntProdCode.Text,txtPrinProdCode.Text,"0",txtPrinAccNo.Text);
                if (resultint > 0)
                {
                    int Prod = 0, AccNo = 0;
                    string ProdName = "";
                    Prod = Convert.ToInt32(txtPCode.Text);
                    AccNo = Convert.ToInt32(txtAccNo.Text);
                    ProdName = txtBankName.Text;
                    
                    BindGrid();
                    lblMessage.Text = "Successfully Created...!!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Add _" + txtPCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearAllData();
                    Response.Redirect("FrmInvCreateReceipt.aspx?ProdCode=" + Prod + "&AccNo=" + AccNo + "&Name=" + ProdName + "");
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
        else
        {
            lnkAdd.Focus();
            lblMessage.Text = "Select Activity First...!!";
            ModalPopup.Show(this.Page);
            return;
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblActivity.Text != "")
            {
               // resultint = IAM.InsertData(Session["BRCD"].ToString(), txtBankNo.Text.Trim().ToString(), txtBankName.Text.Trim().ToString(), txtBranchNo.Text.Trim().ToString(), txtBranchName.Text.Trim().ToString(), txtReceiptNo.Text.Trim().ToString(), txtReceiptName.Text.Trim().ToString(), txtBResNo.Text.Trim().ToString(), txtBMeetDate.Text.Trim().ToString(), txtOpenDate.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                resultint = IAM.InsertData1(Session["BRCD"].ToString(), txtBankNo.Text.Trim().ToString(), txtBankName.Text.Trim().ToString(), "0", txtBranchName.Text.Trim().ToString(), txtReceiptNo.Text.Trim().ToString(), txtReceiptName.Text.Trim().ToString(), txtBResNo.Text.Trim().ToString(), txtBMeetDate.Text.Trim().ToString(), txtOpenDate.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), txtPrinAccNo.Text, txtPrinProdCode.Text, "0", txtIntProdCode.Text, ddlInvestment.SelectedValue, txtAC.Text, txtBank1.Text, txtPrinAccName.Text);
                if (resultint > 0)
                {
                    int Prod = 0, AccNo = 0;
                    string ProdName = "";
                    Prod = Convert.ToInt32(txtBankNo.Text);
                    AccNo = Convert.ToInt32(txtAC.Text);
                    ProdName = txtBankName.Text;
                   
                    BindGrid();

                    lblMessage.Text = "Successfully Added...!!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Add _" + txtPCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearAllData();
                    Response.Redirect("FrmInvCreateReceipt.aspx?ProdCode=" + Prod + "&AccNo=" + AccNo + "&Name=" + ProdName + "");
                }
            }
            else
            {
                lnkAdd.Focus();
                lblMessage.Text = "Select Activity First...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            resultint = IAM.ModifyData(Session["BRCD"].ToString(), txtBankNo.Text.Trim().ToString(), txtBankName.Text.Trim().ToString(), "0", txtBranchName.Text.Trim().ToString(), txtReceiptNo.Text.Trim().ToString(), txtReceiptName.Text.Trim().ToString(), txtBResNo.Text.Trim().ToString(), txtBMeetDate.Text.Trim().ToString(), txtOpenDate.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), txtPrinAccNo.Text, txtPrinProdCode.Text, "0", txtIntProdCode.Text, ddlInvestment.SelectedValue, txtAC.Text, txtBank1.Text, txtPrinAccName.Text);
            if (resultint > 0)
            {
                int Prod = 0, AccNo = 0;
                string ProdName = "";
                Prod = Convert.ToInt32(txtBankNo.Text);
                AccNo = Convert.ToInt32(txtAC.Text);
                ProdName = txtBankName.Text;
              
                BindGrid();

                lblMessage.Text = "Successfully Modified...!!";
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Mod _" + txtPCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearAllData();
                Response.Redirect("FrmInvCreateReceipt.aspx?ProdCode=" + Prod + "&AccNo=" + AccNo + "&Name=" + ProdName + "");
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            resultint = IAM.DeleteData(Session["BRCD"].ToString(), txtBankNo.Text.Trim().ToString(), "0", txtReceiptNo.Text.Trim().ToString(), Session["MID"].ToString(), ddlInvestment.SelectedValue);

            if (resultint > 0)
            {
                
                BindGrid();

                lblMessage.Text = "Successfully Deleted...!!";
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Del_" + txtPCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearAllData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnAuthorise_Click(object sender, EventArgs e)
    {
        try
        {
            resultint = IAM.AuthoriseData(Session["BRCD"].ToString(), txtBankNo.Text.Trim().ToString(), "0", txtReceiptNo.Text.Trim().ToString(), Session["MID"].ToString(), ddlInvestment.SelectedValue);

            if (resultint > 0)
            {
                int Prod = 0, AccNo = 0;
                string ProdName = "";
                Prod = Convert.ToInt32(txtBankNo.Text);
                AccNo = Convert.ToInt32(txtAC.Text);
                ProdName = txtBankName.Text;
               
                BindGrid();

                lblMessage.Text = "Successfully Authorise...!!";
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Auth _" + txtPCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearAllData();
                Response.Redirect("FrmInvCreateReceipt.aspx?ProdCode=" + Prod + "&AccNo=" + AccNo + "&Name="+ProdName+"");
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Text Change event

    protected void txtIntProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //if (txtIntProdCode.Text == "100")
            //{
            //    txtIntAccNo.Text = "100";
            //}
            //else
            //{
                string AC1;
                AC1 = ICR.Getaccno(txtIntProdCode.Text, Session["BRCD"].ToString());

                if (AC1 != null)
                {
                    string[] AC = AC1.Split('_'); ;
                    ViewState["IntGlCode"] = AC[0].ToString();
                    txtIntProdName.Text = AC[1].ToString();
                    //txtBResNo.Text = Convert.ToInt32(IAM.GetBoardResNo(Session["BRCD"].ToString())).ToString();
                    //txtAccNo.Text = Convert.ToInt32(IAM.GetNextReceiptNo(Session["BRCD"].ToString(), txtPCode.Text.Trim().ToString())).ToString();
                    //txtReceiptNo.Text = Convert.ToInt32(IAM.GetReceiptNo(txtPCode.Text.Trim().ToString())).ToString();
                    txtPrinProdCode.Focus();
                    //IntAutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtIntProdCode.Text + "_" + ViewState["IntGlCode"].ToString();

                    //txtIntAccNo.Text = "";
                    //txtIntAccName.Text = "";

                    //txtIntAccNo.Focus();
                }
                else
                {
                    txtIntProdCode.Text = "";
                    txtIntProdName.Text = "";
                    //txtIntAccNo.Text = "";
                    //txtIntAccName.Text = "";

                    lblMessage.Text = "Enter valid Product code...!!";
                    ModalPopup.Show(this.Page);

                    txtIntProdCode.Focus();
                    return;
                }
            //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtIntProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtIntProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtIntProdName.Text = custnob[0].ToString();
                txtIntProdCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = ICR.Getaccno(txtIntProdCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["IntGlCode"] = AC[0].ToString();
                txtPrinProdCode.Focus();
                //IntAutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtIntProdCode.Text + "_" + ViewState["IntGlCode"].ToString();

                //txtIntAccNo.Text = "";
                //txtIntAccName.Text = "";

                //txtIntAccNo.Focus();
            }
            else
            {
                txtIntProdCode.Text = "";
                txtIntProdName.Text = "";
                //txtIntAccNo.Text = "";
                //txtIntAccName.Text = "";

                txtIntProdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //protected void txtIntAccNo_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string AT = "";
    //        AT = BD.Getstage1(txtIntAccNo.Text, Session["BRCD"].ToString(), txtIntProdCode.Text);
    //        if (AT != null)
    //        {
    //            if (AT != "1003")
    //            {
    //                lblMessage.Text = "Sorry Customer not Authorise...!!";
    //                ModalPopup.Show(this.Page);

    //                //txtIntAccNo.Text = "";
    //                //txtIntAccName.Text = "";

    //               // txtIntAccNo.Focus();
    //                return;
    //            }
    //            else
    //            {
    //                DT = new DataTable();
    //                DT = ICR.GetCustName(txtIntProdCode.Text, txtIntAccNo.Text, Session["BRCD"].ToString());
    //                if (DT.Rows.Count > 0)
    //                {
    //                    string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
    //                    txtIntAccName.Text = CustName[0].ToString();
    //                }

    //                txtPrinProdCode.Focus();
    //            }
    //        }
    //        else
    //        {
    //            lblMessage.Text = "Enter valid account number...!!";
    //            ModalPopup.Show(this.Page);

    //            txtIntAccNo.Text = "";
    //            txtIntAccName.Text = "";

    //            txtIntAccNo.Focus();
    //            return;
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    //protected void txtIntAccName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string CUNAME = txtIntAccName.Text;
    //        string[] custnob = CUNAME.Split('_');
    //        if (custnob.Length > 1)
    //        {
    //            txtIntAccName.Text = custnob[0].ToString();
    //            txtIntAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

    //            txtPrinProdCode.Focus();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    protected void txtPCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtPCode.Text == "")
            {
                txtPCode.Text = "";
                txtBankNo.Focus();
                goto ext;
            }
            int result = 0;
            string GlS1;
            int.TryParse(txtPCode.Text, out result);
            txtPType.Text = customcs.GetProductName(result.ToString(), Session["BRCD"].ToString());
            GlS1 = BD.GetAccTypeGL(txtPCode.Text, Session["BRCD"].ToString());
            if (GlS1 != null)
            {
                string[] GLS = GlS1.Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                int GL = 0;
                int.TryParse(ViewState["DRGL"].ToString(), out GL);
                //txtBankNo1.Focus();

                txtBResNo.Text = Convert.ToInt32(IAM.GetBoardResNo(Session["BRCD"].ToString())).ToString();
                txtAccNo.Text = Convert.ToInt32(IAM.GetNextReceiptNo(Session["BRCD"].ToString(), txtPCode.Text.Trim().ToString())).ToString();
                txtReceiptNo.Text = Convert.ToInt32(IAM.GetReceiptNo(txtPCode.Text.Trim().ToString())).ToString();
                txtBankName1.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Valid Product code!....", this.Page);
                txtPCode.Text = "";
                txtPCode.Focus();
            }


        ext: ;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtPType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtPType.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtPType.Text = CT[0].ToString();
                txtPCode.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(txtPCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();

                int GL = 0;
                int.TryParse(ViewState["DRGL"].ToString(), out GL);
                string[] AC = IAM.Getaccno(txtPCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GLCODE"] = AC[0].ToString();

                // txtReceiptNo.Text = Convert.ToInt32(IAM.GetReceiptNo(Session["BRCD"].ToString())).ToString();
                txtBResNo.Text = Convert.ToInt32(IAM.GetBoardResNo(Session["BRCD"].ToString())).ToString();
                txtAccNo.Text = Convert.ToInt32(IAM.GetNextReceiptNo(Session["BRCD"].ToString(), txtPCode.Text.Trim().ToString())).ToString();
                txtReceiptNo.Text = Convert.ToInt32(IAM.GetReceiptNo(txtPCode.Text.Trim().ToString())).ToString();
                txtBankName1.Focus();
                if (txtPType.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    txtPCode.Text = "";
                    txtPCode.Focus();

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }

    }

    //protected void ddlInvestment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlInvestment.SelectedValue == "INV")
    //    {
    //        DivREF.Visible = false;
    //    }
    //    else 
    //    {
    //        DivREF.Visible = true;
    //    }
    //}

    protected void txtBankNo1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = IAM.Getaccno(txtPCode.Text.Trim().ToString(), Session["BRCD"].ToString());

            if (AC1 != null && AC1!="")
            {
                string[] AC = AC1.Split('_'); ;
                ViewState["GLCODE"] = AC[0].ToString();
                txtBankName1.Text = AC[1].ToString();

                txtBResNo.Text = Convert.ToInt32(IAM.GetBoardResNo(Session["BRCD"].ToString())).ToString();
                txtAccNo.Text = Convert.ToInt32(IAM.GetNextReceiptNo(Session["BRCD"].ToString(), txtPCode.Text.Trim().ToString())).ToString();
                txtReceiptNo.Text = Convert.ToInt32(IAM.GetReceiptNo(txtPCode.Text.Trim().ToString())).ToString();
                txtBranch.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                ClearData();
                txtPCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    //protected void txtBankName1_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string CUNAME = txtBankName1.Text;
    //        string[] custnob = CUNAME.Split('_');
    //        if (custnob.Length > 1)
    //        {
    //            txtBankName1.Text = custnob[0].ToString();
    //            txtBankNo1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
    //            string[] AC = IAM.Getaccno(txtBankNo1.Text, Session["BRCD"].ToString()).Split('_');
    //            ViewState["GLCODE"] = AC[0].ToString();

    //            // txtReceiptNo.Text = Convert.ToInt32(IAM.GetReceiptNo(Session["BRCD"].ToString())).ToString();
    //            txtBResNo.Text = Convert.ToInt32(IAM.GetBoardResNo(Session["BRCD"].ToString())).ToString();
    //            txtAccNo.Text = Convert.ToInt32(IAM.GetNextReceiptNo(Session["BRCD"].ToString(), txtBankNo1.Text.Trim().ToString())).ToString();
    //            txtReceiptNo.Text = Convert.ToInt32(IAM.GetReceiptNo(txtBankNo1.Text.Trim().ToString())).ToString();
    //            txtBranch.Focus();
    //        }
    //        else
    //        {
    //            ClearData();
    //            txtBankNo1.Focus();
    //        }
    //        // txtBankNo1.Focus();
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    protected void txtBankNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //string AC1;
            //AC1 = IAM.Getaccno(txtBankNo.Text.Trim().ToString(), Session["BRCD"].ToString());

            //if (AC1 != null)
            //{
            //    string[] AC = AC1.Split('_'); ;
            //    ViewState["GLCODE"] = AC[0].ToString();
            //    txtBankName.Text = AC[1].ToString();

            //    ClearDepositeDetails();

            //    txtReceiptNo.Text = Convert.ToInt32(IAM.GetNextReceiptNo(Session["BRCD"].ToString(), txtBankNo.Text.Trim().ToString())).ToString();
            //    txtBResNo.Text = Convert.ToInt32(IAM.GetBoardResNo(Session["BRCD"].ToString())).ToString();

            //    txtBranchNo.Focus();
            //}
            //else
            //{
            //    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
            //    ClearData();
            //    txtBankNo.Focus();
            //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtBankName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //string CUNAME = txtBankName.Text;
            //string[] custnob = CUNAME.Split('_');
            //if (custnob.Length > 1)
            //{
            //    txtBankName.Text = custnob[0].ToString();
            //    txtBankNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
            //    string[] AC = IAM.Getaccno(txtBankNo.Text, Session["BRCD"].ToString()).Split('_');
            //    ViewState["GLCODE"] = AC[0].ToString();

            //    ClearDepositeDetails();

            //    txtReceiptNo.Text = Convert.ToInt32(IAM.GetNextReceiptNo(Session["BRCD"].ToString(), txtBankNo.Text.Trim().ToString())).ToString();
            //    txtBResNo.Text = Convert.ToInt32(IAM.GetBoardResNo(Session["BRCD"].ToString())).ToString();

            //    txtBranchNo.Focus();
            //}
            //else
            //{
            //    ClearData();
            //    txtBankNo.Focus();
            //}

            txtBank1.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Principle Transfer

    protected void txtPrinProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = ICR.Getaccno(txtPrinProdCode.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                ViewState["PrinGlCode"] = AC[0].ToString();
                txtPrinProdName.Text = AC[1].ToString();

                //PrinAutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtPrinProdCode.Text + "_" + ViewState["PrinGlCode"].ToString();

                txtPrinAccNo.Text = ICR.GetAcc(txtPrinProdCode.Text, Session["BRCD"].ToString());
                txtPrinAccName.Text = txtReceiptName.Text;

                txtPrinAccNo.Focus();
            }
            else
            {
                txtPrinProdCode.Text = "";
                txtPrinProdName.Text = "";
                txtPrinAccNo.Text = "";
                txtPrinAccName.Text = "";

                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);

                txtPrinProdCode.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtPrinProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtPrinProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtPrinProdName.Text = custnob[0].ToString();
                txtPrinProdCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = ICR.Getaccno(txtPrinProdCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["PrinGlCode"] = AC[0].ToString();

                //PrinAutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtPrinProdCode.Text + "_" + ViewState["PrinGlCode"].ToString();

                txtPrinAccNo.Text = ICR.GetAcc(txtPrinProdCode.Text, Session["BRCD"].ToString());
                txtPrinAccName.Text = txtReceiptName.Text;
                txtPrinAccNo.Focus();
            }
            else
            {
                txtPrinProdCode.Text = "";
                txtPrinProdName.Text = "";
                txtPrinAccNo.Text = "";
                txtPrinAccName.Text = "";

                txtPrinProdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtPrinAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            AT = BD.Getstage1(txtPrinAccNo.Text, Session["BRCD"].ToString(), txtPrinProdCode.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise...!!";
                    ModalPopup.Show(this.Page);

                    txtPrinAccNo.Text = "";
                    txtPrinAccName.Text = "";

                    txtPrinAccNo.Focus();
                    return;
                }
                else
                {
                    DT = new DataTable();
                    DT = ICR.GetCustName(txtPrinProdCode.Text, txtPrinAccNo.Text, Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        txtPrinAccName.Text = CustName[0].ToString();
                    }

                   // ddlPayType.Focus();
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);

                txtPrinAccNo.Text = "";
                txtPrinAccName.Text = "";

                txtPrinAccNo.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtPrinAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtPrinAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtPrinAccName.Text = custnob[0].ToString();
                txtPrinAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                //ddlPayType.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Bind Grid

    protected void BindGrid()
    {
        try
        {
            resultint = IAM.GetAllData(grdInvAccMaster, Session["EntryDate"].ToString(),Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    protected void BindVerifyData()
    {
        try
        {
            resultint = IAM.GetAllVerifiedData(grdInvAccMaster, Session["EntryDate"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Clear Funnctions
    protected void ClearDepositeDetails()
    {
        try
        {
            txtBankName1.Text = "";
            //txtBankNo1.Text = "";
            txtBank1.Text = "";

            txtPCode.Text = "";
            txtPType.Text = "";

            ddlInvestment.SelectedValue = "0";
            txtAccNo.Text = "";

            txtBankNo.Text = "";
            txtBankName.Text = "";

            //txtBranchNo.Text = "";
            txtBranchName.Text = "";

            txtReceiptNo.Text = "";
            txtReceiptName.Text = "";
            txtBranch.Text = "";

            txtBMeetDate.Text = Session["EntryDate"].ToString();
            txtOpenDate.Text = Session["EntryDate"].ToString();
            txtBResNo.Text = "";

            txtIntProdCode.Text = "";
            txtIntProdName.Text = "";
            //txtIntAccNo.Text = "";
            //txtIntAccName.Text = "";

            txtPrinProdCode.Text = "";
            txtPrinProdName.Text = "";
            txtPrinAccNo.Text = "";
            txtPrinAccName.Text = "";

            txtBankNo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ClearData()
    {
        try
        {

            txtBankName1.Text = "";
            //txtBankNo1.Text = "";
            txtBank1.Text = "";

            txtPCode.Text = "";
            txtPType.Text = "";

            ddlInvestment.SelectedValue = "0";
            txtAccNo.Text = "";

            txtBankNo.Text = "";
            txtBankName.Text = "";

            //txtBranchNo.Text = "";
            txtBranchName.Text = "";

            txtReceiptNo.Text = "";
            txtReceiptName.Text = "";
            txtBranch.Text = "";

            txtBMeetDate.Text = Session["EntryDate"].ToString();
            txtOpenDate.Text = Session["EntryDate"].ToString();
            txtBResNo.Text = "";

            txtIntProdCode.Text = "";
            txtIntProdName.Text = "";
            //txtIntAccNo.Text = "";
            //txtIntAccName.Text = "";

            txtPrinProdCode.Text = "";
            txtPrinProdName.Text = "";
            txtPrinAccNo.Text = "";
            txtPrinAccName.Text = "";

            txtBankNo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ClearAllData()
    {
        try
        {
            txtAccNo.Text = "";
            txtBranch.Text = "";
            txtBankName1.Text = "";

            txtBank1.Text = "";
            //txtBankNo1.Text = "";
            txtPCode.Text = "";
            txtPType.Text = "";
            ddlInvestment.SelectedValue = "0";
            txtBankNo.Text = "";
            txtBankName.Text = "";
            //txtBranchNo.Text = "";
            txtBranchName.Text = "";
            txtReceiptNo.Text = "";
            txtReceiptName.Text = "";
            txtBResNo.Text = "";
            txtBMeetDate.Text = Session["EntryDate"].ToString();
            txtOpenDate.Text = Session["EntryDate"].ToString();

            txtIntProdCode.Text = "";
            txtIntProdName.Text = "";
            //txtIntAccNo.Text = "";
            //txtIntAccName.Text = "";

            txtPrinProdCode.Text = "";
            txtPrinProdName.Text = "";
            txtPrinAccNo.Text = "";
            txtPrinAccName.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtBankNo.Text = "";
            txtBankName.Text = "";

            txtBank1.Text = "";

            //txtBranchNo.Text = "";
            txtBranchName.Text = "";

            txtReceiptNo.Text = "";
            txtReceiptName.Text = "";

            txtBResNo.Text = "";
            txtBMeetDate.Text = Session["EntryDate"].ToString();
            txtOpenDate.Text = Session["EntryDate"].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion

    #region Related To Modify, Delete, Authorise 
    protected void grdInvAccMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdInvAccMaster.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumid = objlink.CommandArgument;
            string[] arr = strnumid.Split('_');
            string Branch = objlink.CommandName;
            if (Convert.ToInt32(Branch) == 0)
            {
                DivNew.Visible = false;
                DivOld.Visible = true;
            }
            else
            {
                DivNew.Visible = true;
                DivOld.Visible = false;
            }
                CallData(arr[0].ToString(), arr[1].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void CallData(string Identity, string BankCode)
    {
        try
        {
            if (ViewState["Flag"].ToString() != "AD")
            {
                DT = new DataTable();
                DT = IAM.GetInfo(Identity, BankCode);

                if (DT.Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    dt1 = IAM.GetProdCode(DT.Rows[0]["CRGL"].ToString(), DT.Rows[0]["RecGL"].ToString());
                    if (dt1.Rows.Count > 0)
                    {
                        txtPrinProdName.Text = dt1.Rows[0]["GLNAME1"].ToString();
                        txtIntProdName.Text = dt1.Rows[0]["GLNAME"].ToString();
                    }
                    txtIntProdCode.Text = DT.Rows[0]["CRGL"].ToString();
                    //txtIntAccNo.Text = DT.Rows[0]["CRAC"].ToString();
                    txtPrinProdCode.Text = DT.Rows[0]["RecGL"].ToString();
                    txtPrinAccNo.Text = DT.Rows[0]["RecAC"].ToString();
                    if (txtPrinAccNo.Text == "")
                    {
                        string CUNAME = txtPrinProdName.Text;
                        string[] custnob = CUNAME.Split('_');
                        if (custnob.Length > 1)
                        {
                            txtPrinProdName.Text = custnob[0].ToString();
                            txtPrinProdCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                            string[] AC = ICR.Getaccno(txtPrinProdCode.Text, Session["BRCD"].ToString()).Split('_');
                            ViewState["PrinGlCode"] = AC[0].ToString();

                            //PrinAutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtPrinProdCode.Text + "_" + ViewState["PrinGlCode"].ToString();

                            txtPrinAccNo.Text = ICR.GetAcc(txtPrinProdCode.Text, Session["BRCD"].ToString());
                            txtPrinAccName.Text = txtReceiptName.Text;
                            txtPrinAccNo.Focus();
                        }
                        else
                        {
                            txtPrinProdCode.Text = "";
                            txtPrinProdName.Text = "";
                            txtPrinAccNo.Text = "";
                            txtPrinAccName.Text = "";

                            txtPrinProdCode.Focus();
                        }
                    }
                    txtPCode.Text = DT.Rows[0]["ProdCode"].ToString();
                    txtPrinAccName.Text = DT.Rows[0]["PRACCNAME"].ToString();
                    txtPType.Text = DT.Rows[0]["ProdName"].ToString();
                    //txtBankNo1.Text = DT.Rows[0]["BankCode"].ToString();
                    txtBankName1.Text = DT.Rows[0]["BankName"].ToString();
                    txtBranch.Text = DT.Rows[0]["BranchName"].ToString();
                    txtAccNo.Text = DT.Rows[0]["CustAccno"].ToString();
                    txtAC.Text = DT.Rows[0]["CustAccno"].ToString();
                    ddlInvestment.SelectedValue = DT.Rows[0]["GlGroup"].ToString();
                    //if (ddlInvestment.SelectedValue == "INV")
                    //{
                    //    DivREF.Visible = false;
                    //}
                    //else 
                    //{
                    //    DivREF.Visible = true;
                    //}

                    txtBankNo.Text = DT.Rows[0]["ProdCode"].ToString();
                    txtBankName.Text = DT.Rows[0]["ProdName"].ToString();
                    txtBank1.Text = DT.Rows[0]["Bankname"].ToString();
                   // txtBranchNo.Text = DT.Rows[0]["BranchCode"].ToString();
                    txtBranchName.Text = DT.Rows[0]["Branchname"].ToString();
                    txtReceiptNo.Text = DT.Rows[0]["ReceiptNo"].ToString();
                    txtReceiptName.Text = DT.Rows[0]["ReceiptName"].ToString();
                    txtBResNo.Text = DT.Rows[0]["BoardResNo"].ToString();
                    txtBMeetDate.Text = DT.Rows[0]["BoardMeetDate"].ToString();
                    txtOpenDate.Text = DT.Rows[0]["OpeningDate"].ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion



    
}

    