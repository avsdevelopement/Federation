using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class FrmAVS5044 : System.Web.UI.Page
{
    ClsLoanRepaymentCerti LR = new ClsLoanRepaymentCerti();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    DataTable dt = new DataTable();
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    ClsCustomerDetails CD = new ClsCustomerDetails();
    DbConnection conn = new DbConnection();
    ClsLoanRecovery LRObj = new ClsLoanRecovery();
    ClsAVS5044 CC = new ClsAVS5044();

    string FL = "", OUTNO = "", BRNAME = "";
    int RES = 0;
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
                autoglname.ContextKey = Session["BRCD"].ToString();
                // Autoaccname4.ContextKey = Session["BRCD"].ToString() + "_" + txtProdCode.Text;
                autocustname.ContextKey = Session["BRCD"].ToString();
                DataTable DT = new DataTable();
                DT = LR.BranchDetails(Session["BRCD"].ToString());
                txtbrcd.Text = DT.Rows[0]["brcd"].ToString();
                txtbrname.Text = DT.Rows[0]["midname"].ToString();
                txtbrcd.Enabled = false;
                txtbrname.Enabled = false;
                txtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
                txtTDate.Text = conn.sExecuteScalar("select '31/03/'+ convert(varchar(10),(year(dateadd(month, 3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
                TxtOutNo.Text = "";
                OutNo();
                txtcstno.Focus();

            }
            catch (Exception Ex)
            {

                ExceptionLogging.SendErrorToText(Ex);
            }

        }
    }
    protected void txtaccno_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    string AT = "";
        //    string AC_Status = LR.GetAccStatus(Session["BRCD"].ToString(), txtProdCode.Text, txtaccno.Text);
        //    if (AC_Status == "1" || AC_Status == "9")
        //    {
        //        AT = LR.Getstage1(txtaccno.Text, Session["BRCD"].ToString(), txtProdCode.Text);
        //        if (AT != null)
        //        {
        //            if (AT != "1003")
        //            {
        //                lblMessage.Text = "Sorry Customer not Authorise.........!!";
        //                ModalPopup.Show(this.Page);
        //                txtaccno.Text = "";
        //                txtaccname.Text = "";
        //                txtaccno.Focus();
        //            }
        //            else
        //            {
        //                DataTable DT = new DataTable();
        //                DT = LR.GetCustName(txtProdCode.Text, txtaccno.Text, Session["BRCD"].ToString());
        //                if (DT.Rows.Count > 0)
        //                {
        //                    string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
        //                    txtaccname.Text = CustName[0].ToString();



        //                }
        //            }
        //        }
        //        else
        //        {
        //            lblMessage.Text = "Enter valid account number...!!";
        //            ModalPopup.Show(this.Page);
        //            txtaccno.Text = "";
        //            txtaccno.Focus();
        //        }
        //    }
        //    else if (AC_Status == "2")
        //    {
        //        lblMessage.Text = "Acc number " + txtaccno.Text + " is In-operative.........!!";
        //        ModalPopup.Show(this.Page);

        //    }
        //    else if (AC_Status == "3")
        //    {
        //        lblMessage.Text = "Acc number " + txtaccno.Text + " is Closed.........!!";
        //        ModalPopup.Show(this.Page);

        //    }
        //    else if (AC_Status == "4")
        //    {
        //        lblMessage.Text = "Acc number " + txtaccno.Text + " is Lean Marked / Loan Advanced .........!!";
        //        ModalPopup.Show(this.Page);

        //    }
        //    else if (AC_Status == "5")
        //    {
        //        lblMessage.Text = "Acc number " + txtaccno.Text + " is Credit Freezed.........!!";
        //        ModalPopup.Show(this.Page);

        //    }
        //    else if (AC_Status == "6")
        //    {
        //        lblMessage.Text = "Acc number " + txtaccno.Text + " is Debit Freezed.........!!";
        //        ModalPopup.Show(this.Page);

        //    }
        //    else if (AC_Status == "7")
        //    {
        //        lblMessage.Text = "Acc number " + txtaccno.Text + " is Total Freezed.........!!";
        //        ModalPopup.Show(this.Page);

        //    }
        //    else
        //    {
        //        WebMsgBox.Show("Enter Valid Account number!...", this.Page);
        //        txtaccno.Text = "";

        //    }
        //    txtaccno.Focus();
        //}

        //catch (Exception Ex)
        //{

        //    ExceptionLogging.SendErrorToText(Ex);
        //}
    }
    protected void txtProdName_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    string CUNAME = txtProdName.Text;
        //    string[] custnob = CUNAME.Split('_');
        //    if (custnob.Length > 1)
        //    {
        //        txtProdName.Text = custnob[0].ToString();
        //        txtProdCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
        //        string[] AC = LR.Getaccno(txtProdCode.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
        //        ViewState["ProdName"] = AC[0].ToString();
        //        ViewState["ProdCode"] = AC[1].ToString();
        //        Autoaccname4.ContextKey = Session["BRCD"].ToString() + "_" + txtProdCode.Text;
        //        txtaccno.Focus();
        //    }
        //}
        //catch (Exception Ex)
        //{
        //    ExceptionLogging.SendErrorToText(Ex);
        //}
    }
    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    txtProdName.Text = LR.GetAccType(txtProdCode.Text, Session["BRCD"].ToString());
        //    Autoaccname4.ContextKey = Session["BRCD"].ToString() + "_" + txtProdCode.Text;

        //}
        //catch (Exception Ex)
        //{
        //    ExceptionLogging.SendErrorToText(Ex);
        //}
    }
    protected void txtbrcd_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            //RES = LR.INSERT(TxtOutNo.Text.ToUpper(), "LOAN", txtProdCode.Text, txtaccno.Text, Session["MID"].ToString(), Session["BRCD"].ToString());

            //FL = "Insert";//Dhanya Shetty
            //string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanIntCertf _Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            //string redirectURL = "FrmRView.aspx?BRCD=" + txtbrcd.Text + "&LOANGL=" + txtProdCode.Text + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + txtaccno.Text + "&FDate=" + txtFDate.Text + "&TDate=" + txtTDate.Text + "&OUTNO=" + TxtOutNo.Text.ToUpper() + "&rptname=RptLoanRepayCerti.rdlc";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            //clear();
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }

    protected void txtaccname_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    string CUNAME = txtaccname.Text;
        //    string[] custnob = CUNAME.Split('_');
        //    if (custnob.Length > 1)
        //    {
        //        txtaccname.Text = custnob[0].ToString();
        //        txtaccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
        //    }
        //    else
        //    {
        //        lblMessage.Text = "Invalid Account Number...!";
        //        ModalPopup.Show(this.Page);
        //        return;
        //    }
        //}
        //catch (Exception Ex)
        //{
        //    ExceptionLogging.SendErrorToText(Ex);
        //}
    }
    public void Get()
    {
        try
        {
            LR.GETOUWARD(TxtOutNo.Text.ToUpper());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void bind(string outno)
    {
        try
        {
            LR.bindoutward(grdLogDetails, outno);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtOutNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            BRNAME = LR.Getbrname(Session["BRCD"].ToString());
            char[] check = TxtOutNo.Text.ToUpper().ToCharArray();
            if (check[0].ToString() != BRNAME)
            {
                WebMsgBox.Show("Invalid Outward Number..!!Please enter valid outward number", this.Page);
                TxtOutNo.Text = "";
                return;
            }
            if (TxtOutNo.Text.ToUpper() != "")
                bind(TxtOutNo.Text.ToUpper());

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void OutNo(string Cstno_FL="YES")
    {
        try
        {
            string[] FDT = txtFDate.Text.Split('/');
            string[] TDT = txtTDate.Text.Split('/');

            string Ex_OutNo = LR.CheckOutNo_Exist(Session["BRCD"].ToString(), FDT[2].ToString(), TDT[2].ToString());
            if ((Ex_OutNo != null && txtcstno.Text != null && txtcstno.Text != "" && Cstno_FL=="YES" && Cstno_FL!="NEW")
                ||
                (Ex_OutNo != null && txtcstno.Text != null && txtcstno.Text != "" && Cstno_FL == "EX" && Cstno_FL != "NEW"))
            {

                if (txtcstno.Text != null && txtcstno.Text != "")
                {
                    string Ex_OutNo_Ex = LR.CheckOutNo_Exist(Session["BRCD"].ToString(), FDT[2].ToString(),TDT[2].ToString(), txtcstno.Text);
                    if (Ex_OutNo_Ex != null)
                    {
                        Ex_OutNo = Ex_OutNo_Ex;
                    }

                }

                TxtOutNo.Text = Ex_OutNo.ToString();

                if (TxtOutNo.Text.ToUpper() != "")
                    bind(TxtOutNo.Text.ToUpper());

                 ViewState["OUTNO_YN"] = "Y";
            }
            else
            {
                BRNAME = LR.Getbrname(Session["BRCD"].ToString());
                OUTNO = LR.GetOutNo(Session["BRCD"].ToString(), FDT[2].ToString(), TDT[2].ToString());

               
                string FYY, TYY;
                FYY = FDT[2].ToString();
                TYY = TDT[2].ToString();


                FYY = FYY.Substring(2, 2);
                TYY = TYY.Substring(2, 2);


                if (OUTNO == null || OUTNO == "")
                    TxtOutNo.Text = BRNAME + FYY + TYY + "1";
                else
                    TxtOutNo.Text = BRNAME + FYY + TYY + OUTNO.ToString();

                if (TxtOutNo.Text.ToUpper() != "")
                    bind(TxtOutNo.Text.ToUpper());

                ViewState["OUTNO_YN"] = "N";
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void clear()
    {
        //txtProdCode.Text = "";
        //txtProdName.Text = "";
        //txtaccno.Text = "";
        //txtaccname.Text = "";
        OutNo();
        DataTable DT = new DataTable();
        DT = LR.BranchDetails(Session["BRCD"].ToString());
        txtbrcd.Text = DT.Rows[0]["brcd"].ToString();
        txtbrname.Text = DT.Rows[0]["midname"].ToString();
        txtbrcd.Enabled = false;
        txtbrname.Enabled = false;
        txtFDate.Text = "";
        txtTDate.Text = "";
        txtcstno.Text = "";
        txtname.Text = "";
    }
    public void GetOutReceiptDeatils()
    {
        try
        {
            int RR = CC.BindOutReceipts(grdLogDetails, Session["BRCD"].ToString(), txtcstno.Text);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtcstno_TextChanged(object sender, EventArgs e)
    {

        try
        {
            string[] FDT = txtFDate.Text.Split('/');
            string[] TDT = txtTDate.Text.Split('/');

            DT = CD.GetStage(txtcstno.Text);

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
            else if (DT.Rows[0]["STAGE"].ToString() == "" || DT.Rows[0]["STAGE"].ToString() == null)
            {
                WebMsgBox.Show("Customer Not Exists...!!", this.Page);
                return;
            }
            else
            {
                DT1 = CD.GetCustName(txtcstno.Text);
                txtname.Text = DT1.Rows[0]["CUSTNAME"].ToString();
                LR.bindAccDetail(GrdCustDetails, txtcstno.Text, txtbrcd.Text, txtFDate.Text, txtTDate.Text);
                string FL = "";
                string Ex_OutNoCno = LR.CheckOutNo_ExistCustno(Session["BRCD"].ToString(), FDT[2].ToString(), TDT[2].ToString(), txtcstno.Text);
                if (Ex_OutNoCno != null)
                {
                    if (Convert.ToInt32(Ex_OutNoCno) > 0)
                    {
                        FL = "EX";
                    }
                    else
                    {
                        FL = "NEW";
                    }

                }

                OutNo(FL);
                GetOutReceiptDeatils();
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
            string CUNAME = txtname.Text;
            string[] custnob = CUNAME.Split('_');

            if (custnob.Length > 1)
            {
                txtname.Text = custnob[0].ToString();
                txtcstno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                DT = CD.GetStage(txtname.Text);
                LR.bindAccDetail(GrdCustDetails, txtcstno.Text, txtbrcd.Text, txtFDate.Text, txtTDate.Text);
                OutNo();
                GetOutReceiptDeatils();
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
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string[] FDT = txtFDate.Text.Split('/');
            string[] TDT = txtTDate.Text.Split('/');

            if (txtcstno.Text == "")
            {
                WebMsgBox.Show("Please Enter Customer Number..!!", this.Page);
                return;
            }
            if (txtFDate.Text == "")
            {
                WebMsgBox.Show("Please Enter from date..!!", this.Page);
                return;
            }
            if (txtTDate.Text == "")
            {
                WebMsgBox.Show("Please Enter To date..!!", this.Page);
                return;
            }
            if (TxtOutNo.Text.ToUpper() == "")
            {
                WebMsgBox.Show("Please Enter Outward No..!!", this.Page);
                return;
            }
            LinkButton objlink = (LinkButton)sender;
            string[] strnumId = objlink.CommandArgument.Split('_');
            ViewState["SUBG"] = strnumId[0].ToString();
            ViewState["ACC"] = strnumId[1].ToString();
            string Result = LRObj.LSCheck("LSCHECK", Session["BRCD"].ToString(), ViewState["SUBG"].ToString(), ViewState["ACC"].ToString(), Session["EntryDate"].ToString(), txtFDate.Text, txtTDate.Text, Session["EntryDate"].ToString());
            if (Result != null && Result == "REG")
            {
                if ((ViewState["OUTNO_YN"] == null ? "N" : ViewState["OUTNO_YN"]).ToString() == "N")
                {
                    RES = LR.INSERT(TxtOutNo.Text.ToUpper(), "LOAN", ViewState["SUBG"].ToString(), ViewState["ACC"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), txtcstno.Text, FDT[2].ToString(), TDT[2].ToString());
                }

                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanIntCertf _Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?FL=1&BRCD=" + txtbrcd.Text + "&LOANGL=" + ViewState["SUBG"].ToString() + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + ViewState["ACC"].ToString() + "&FDate=" + txtFDate.Text + "&TDate=" + txtTDate.Text + "&OUTNO=" + TxtOutNo.Text.ToUpper() + "&rptname=RptLoanRepayCerti.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                clear();
            }
            else
            {
                //Added by Abhishek as per ambika mam req on 28-12-2017
                WebMsgBox.Show("Customer is not regular...!", this.Page);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void txtFDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            OutNo("NO");
            txtcstno.Text = "";
            txtname.Text = "";
            txtTDate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtTDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            OutNo("NO");
            txtcstno.Text = "";
            txtname.Text = "";
            txtcstno.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LnkAccured_Click(object sender, EventArgs e)
    {
        try
        {
            string[] FDT = txtFDate.Text.Split('/');
            string[] TDT = txtTDate.Text.Split('/');

            if (txtcstno.Text == "")
            {
                WebMsgBox.Show("Please Enter Customer Number..!!", this.Page);
                return;
            }
            if (txtFDate.Text == "")
            {
                WebMsgBox.Show("Please Enter from date..!!", this.Page);
                return;
            }
            if (txtTDate.Text == "")
            {
                WebMsgBox.Show("Please Enter To date..!!", this.Page);
                return;
            }
            if (TxtOutNo.Text.ToUpper() == "")
            {
                WebMsgBox.Show("Please Enter Outward No..!!", this.Page);
                return;
            }
            LinkButton objlink = (LinkButton)sender;
            string[] strnumId = objlink.CommandArgument.Split('_');
            ViewState["SUBG"] = strnumId[0].ToString();
            ViewState["ACC"] = strnumId[1].ToString();
            string Result = LRObj.LSCheck("LSCHECK", Session["BRCD"].ToString(), ViewState["SUBG"].ToString(), ViewState["ACC"].ToString(), Session["EntryDate"].ToString(), txtFDate.Text, txtTDate.Text, Session["EntryDate"].ToString());
            if (Result != null && Result == "REG")
            {
                if ((ViewState["OUTNO_YN"] == null ? "N" : ViewState["OUTNO_YN"]).ToString() == "N")
                {
                    RES = LR.INSERT(TxtOutNo.Text.ToUpper(), "LOAN", ViewState["SUBG"].ToString(), ViewState["ACC"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), txtcstno.Text, FDT[2].ToString(), TDT[2].ToString());
                }

                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanIntCertf _Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?FL=2&BRCD=" + txtbrcd.Text + "&LOANGL=" + ViewState["SUBG"].ToString() + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + ViewState["ACC"].ToString() + "&FDate=" + txtFDate.Text + "&TDate=" + txtTDate.Text + "&OUTNO=" + TxtOutNo.Text.ToUpper() + "&rptname=RptLoanRepayCerti.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                clear();
            }
            else
            {
                //Added by Abhishek as per ambika mam req on 28-12-2017
                WebMsgBox.Show("Customer is not regular...!", this.Page);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}