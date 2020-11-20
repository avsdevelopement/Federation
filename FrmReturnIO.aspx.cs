using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmReturnIO : System.Web.UI.Page
{
    scustom customcs = new scustom();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsReturnIO RI = new ClsReturnIO();
    ClsInOut IO = new ClsInOut();
    ClsCommon CMM = new ClsCommon();
    int Result = 0;
    string STR = "";
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

            autoglname.ContextKey = Session["BRCD"].ToString();
            DdlReturnType.Focus();
            BD.BindReasons(DdlReason);
            IO.GetSumGrid(GrdDetails, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "ZONEGRID"); ////added by ankita on 20/07/2017
            TxtCheqDate.Text = Session["EntryDate"].ToString(); ////added by ankita on 20/07/2017
            ENDN(false);
            NEWENDN(false);
        }
    }
  
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            string FL = "";
            if (DdlReturnType.SelectedValue == "1")
                FL = "I";
            else if (DdlReturnType.SelectedValue == "2")
                FL = "O";

            string CHCNT = "";
            CHCNT = RI.GetChqCount(Session["BRCD"].ToString(), TxtCheqNo.Text, FL, Session["EntryDate"].ToString(),TxtAccType.Text,TxtAccNo.Text,"ACCH");
            if (Convert.ToInt32(CHCNT) <= 0)
            {
                        string TRXTYPE = "", PMTMD = "", ACT = "", PAYMAST = "", CD = "", NARR = ""; ;
                        string SetN = BD.GetSetNo(Session["EntryDate"].ToString(), "InOutSetno", Session["BRCD"].ToString()).ToString();
                        int SETNO = Convert.ToInt32(SetN);

                        if (DdlReturnType.SelectedValue == "1")
                        {
                            TRXTYPE = "1";
                            PMTMD = "IC";
                            ACT = "31";
                            PAYMAST = "IC";
                            CD = "C";
                            NARR = TxtNarration.Text + " IW/R CLG " + TxtAccType.Text + "-" + TxtAccNo.Text + "";
                        }
                        else if (DdlReturnType.SelectedValue == "2")
                        {
                            TRXTYPE = "2";
                            PMTMD = "OC";
                            ACT = "32";
                            PAYMAST = "OC";
                            CD = "D";
                            NARR = TxtNarration.Text + " OW/R CLG " + TxtAccType.Text + "-" + TxtAccNo.Text + "";
                        }

                        if (ViewState["GL"] == null || ViewState["GL"].ToString() == "")
                        {
                            string [] CDD=BD.GetAccTypeGL(TxtAccType.Text, Session["BRCD"].ToString()).ToString().Split('_');
                            ViewState["GL"] = CDD[1].ToString();
                        }
                        int RESULT = RI.InOutReturn(DdlReturnType.SelectedValue, Session["EntryDate"].ToString(), ViewState["GL"].ToString(), TxtAccType.Text, TxtAccNo.Text, NARR.ToString(), TxtCAmount.Text, TRXTYPE.ToString(), ACT.ToString(), SETNO.ToString(), PMTMD.ToString(), TxtCheqNo.Text, TxtCheqDate.Text, TxtBankCD.Text, TxtBRCD.Text, "1001", Session["BRCD"].ToString(), Session["MID"].ToString(), PAYMAST.ToString(), ViewState["CUSTNO"].ToString(), ViewState["CUSTNAME"].ToString(), "3", "", CD.ToString(), DdlReason.SelectedValue,"ORT");
                        ViewState["GL"] = "";
                        if (RESULT > 0)
                        {
                            //lblMessage.Text = " Return Voucher Number '" + SETNO;
                            //ModalPopup.Show(this.Page);
                            if (DdlReturnType.SelectedValue == "1")
                            {
                                BindReturn("I", "C");
                                LBL_IO.Text = "Inward Return";
                            }
                            else if (DdlReturnType.SelectedValue == "2")
                            {
                                BindReturn("O", "D");
                                LBL_IO.Text = "Outward Return";
                            }
                            Clear();
                            DdlReturnType.Focus();
                            WebMsgBox.Show(" Return Voucher Number " + SetN + " ", this.Page);
                            FL = "Insert";//Dhanya 15/09/2017
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ReturnIO_" +SetN+ "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                                      
                        }
            }
            else
            {
                WebMsgBox.Show("Chq No. " + TxtCheqNo.Text + " is Already Returned...! ", this.Page);
                Clear();
                DdlReturnType.Focus();

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAcctypeName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtAcctypeName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtAcctypeName.Text = CT[0].ToString();
                TxtAccType.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtAccType.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GL"] = GLS[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtAccType.Text + "_" + ViewState["GL"].ToString();

                int GL = 0;
                int.TryParse(ViewState["GL"].ToString(), out GL);
                TxtAccNo.Focus();
               

                if (TxtAcctypeName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtAccType.Text = "";
                    TxtAccType.Focus();

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void TxtAccType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtAccType.Text == "")
            {
                TxtAcctypeName.Text = "";
                TxtAccNo.Focus();
                goto ext;
            }
            int result = 0;
            string GlS1;
            int.TryParse(TxtAccType.Text, out result);
            TxtAcctypeName.Text = customcs.GetProductName(result.ToString(), Session["BRCD"].ToString());
            GlS1 = BD.GetAccTypeGL(TxtAccType.Text, Session["BRCD"].ToString());
            if (GlS1 != null)
            {
                string[] GLS = GlS1.Split('_');
                ViewState["GL"] = GLS[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtAccType.Text + "_" + ViewState["GL"].ToString();
                int GL = 0;
                int.TryParse(ViewState["GL"].ToString(), out GL);
               TxtAccNo.Focus();
           
            }
            else
            {
                WebMsgBox.Show("Enter Valid Product code!....", this.Page);
                TxtAccType.Text = "";
                TxtAcctypeName.Text = "";
                TxtAccType.Focus();
            }

       ext: ;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }

    }
    protected void TxtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            // AT = BD.GetStage1(TxtAccno.Text, Session["BRCD"].ToString(), ViewState["Flag"].ToString());
            AT = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), TxtAccType.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise.........!!";
                    ModalPopup.Show(this.Page);
                    Clear();
                }
                else
                {
                   
                    if (TxtAccNo.Text == "")
                    {
                        TxtAccName.Text = "";
                        goto ext;
                    }

                    DataTable dt1 = new DataTable();
                    if (TxtAccNo.Text != "" & TxtAccType.Text != "")
                    {
                        string PRD = "";
                        string[] CN;
                        PRD = TxtAccType.Text;
                        CN = customcs.GetAccountName(TxtAccNo.Text.ToString(), PRD, Session["BRCD"].ToString()).Split('_');
                        ViewState["CUSTNO"] = CN[0].ToString();
                        TxtAccName.Text = CN[1].ToString();
                        if (TxtAccName.Text == "" & TxtAccNo.Text != "")
                        {
                            WebMsgBox.Show("Please enter valid Account number", this.Page);
                            TxtAccNo.Text = "";
                            TxtAccNo.Focus();
                            return;
                        }

                        string FL = "";
                        if (DdlReturnType.SelectedValue == "1")
                            FL = "I";
                        else if (DdlReturnType.SelectedValue == "2")
                            FL = "O";
                        string CHCNT = "";
                        CHCNT = RI.GetChqCount(Session["BRCD"].ToString(), TxtCheqNo.Text, FL, Session["EntryDate"].ToString(),TxtAccType.Text,TxtAccNo.Text,"ACCH");
                        if (Convert.ToInt32(CHCNT) <= 0)
                        {
                            DataTable DT = new DataTable();
                            DT = RI.GetInfo(Session["BRCD"].ToString(), DdlReturnType.SelectedValue.ToString(), TxtAccType.Text, TxtAccNo.Text, TxtCheqNo.Text, TxtCheqDate.Text, Session["EntryDate"].ToString());
                            if (DT.Rows.Count > 0)
                            {
                                TxtAccType.Text = DT.Rows[0]["PRDUCT_CODE"].ToString();
                                TxtAccNo.Text = DT.Rows[0]["ACC_NO"].ToString();
                                TxtBankCD.Text = DT.Rows[0]["BANK_CODE"].ToString();
                                TxtBankName.Text = DT.Rows[0]["BankName"].ToString();
                                TxtBRCD.Text = DT.Rows[0]["BRANCH_CODE"].ToString();
                                TxtBRCDName.Text = DT.Rows[0]["BranchName"].ToString();
                                TxtNarration.Text = DT.Rows[0]["PARTICULARS"].ToString();
                                TxtCheqDate.Text = DT.Rows[0]["INSTRUDATE"].ToString().Replace("12:00:00", "");
                                TxtCheqNo.Text = DT.Rows[0]["INSTRU_NO"].ToString();
                                TxtCAmount.Text = DT.Rows[0]["INSTRU_AMOUNT"].ToString();
                                ViewState["CUSTNO"] = DT.Rows[0]["CUSTNO"].ToString();
                                ViewState["CUSTNAME"] = DT.Rows[0]["CUSTNAME"].ToString();
                                NEWENDN(false);
                                ////added by ankita on 20/07/2017
                                SumIwOw();
                                Photo_Sign();
                            }

                            else
                            {
                                WebMsgBox.Show("No Records Found.....!", this.Page);
                                Clear();
                                
                            }
                        }
                        else
                        {
                            WebMsgBox.Show("Chq No. " + TxtCheqNo.Text + " is Already Returned...! ", this.Page);
                            Clear();
                            DdlReturnType.Focus();

                        }
                       
                    }

                    if (TxtAccNo.Text == "" || TxtAccType.Text == "")
                    {
                        TxtAccName.Text = "";
                        goto ext;
                    }
                    dt1 = customcs.GetAccNoAccType(TxtAccType.Text, TxtAccNo.Text, Session["BRCD"].ToString());

                    if (dt1 != null && dt1.Rows.Count != 0)
                    {
                    }
                    else
                    {

                    }
                ext: ;
                }
            }
            else
            {
                WebMsgBox.Show("Enter Valid Account number!...", this.Page);
                TxtAccNo.Text = "";
                TxtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
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
                TxtAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
               if (TxtAccNo.Text == "")
                {
                    TxtAccName.Text = "";
                    return;
                }
               ////added by ankita on 20/07/2017
               SumIwOw();
               Photo_Sign();
               TxtCheqNo.Focus();
            }
            else
            {
                lblMessage.Text = "Invalid Account Number.........!!";
                ModalPopup.Show(this.Page);
                return;
            }
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }

    }
    protected void TxtBankCD_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtBankName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtBRCD_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtBRCDName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Btn_Clear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void Clear()
    {
        DdlReturnType.SelectedValue = "0";
        TxtAccType.Text = "";
        TxtAcctypeName.Text = "";
        TxtAccNo.Text = "";
        TxtAccName.Text = "";
        TxtCheqNo.Text = "";
        TxtCheqDate.Text = "";
        TxtBankCD.Text = "";
        TxtBankName.Text = "";
        TxtBRCD.Text = "";
        TxtBRCDName.Text = "";
        TxtCAmount.Text = "";
        TxtNarration.Text = "";
      //  TxtRes.Text = "";
       // TxtResName.Text = "";

        DdlReason.SelectedValue = "0";
        DdlReturnType.Focus();
    }
    protected void ENDN(bool TF)
    {
        TxtBankCD.Enabled = TF;
        TxtBankName.Enabled =TF;
        TxtBRCD.Enabled =TF;
        TxtBRCDName.Enabled =TF;
        TxtCAmount.Enabled =TF;
        TxtNarration.Enabled =TF;
        //TxtRes.Enabled =TF;
        //TxtResName.Enabled =TF;
    }
    protected void Grd_IOReturn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType ==DataControlRowType.DataRow)
            {
                e.Row.Font.Bold = true;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);            

        }
    }
    protected void TxtCheqNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CHCNT = "";
            string FL = "";
            if (DdlReturnType.SelectedValue == "1")
                FL = "I";
            else if (DdlReturnType.SelectedValue == "2")
                FL = "O";

            //CHCNT = RI.GetChqCount(Session["BRCD"].ToString(), TxtCheqNo.Text, FL, Session["EntryDate"].ToString(),"","","CH");
            //if (Convert.ToInt32(CHCNT) <= 0)
            //{
                DataTable DT = new DataTable();
                DT = RI.GetInfo(Session["BRCD"].ToString(), DdlReturnType.SelectedValue.ToString(), TxtAccType.Text, TxtAccNo.Text, TxtCheqNo.Text, TxtCheqDate.Text, Session["EntryDate"].ToString());
                if (DT.Rows.Count>0)
                {
                    TxtAccType.Text = DT.Rows[0]["PRDUCT_CODE"].ToString();
                    TxtAccNo.Text = DT.Rows[0]["ACC_NO"].ToString();
                    if(TxtAccType.Text!=null)
                    TxtAcctypeName.Text = customcs.GetProductName(TxtAccType.Text, Session["BRCD"].ToString());

                    if (TxtAccNo.Text != null)
                    {
                        string[] CN;
                        CN = customcs.GetAccountName(TxtAccNo.Text.ToString(), TxtAccType.Text, Session["BRCD"].ToString()).Split('_');
                        ViewState["CUSTNO"] = CN[0].ToString();
                        TxtAccName.Text = CN[1].ToString();
                        Photo_Sign();
                    }
                    TxtBankCD.Text = DT.Rows[0]["BANK_CODE"].ToString();
                    TxtBankName.Text = DT.Rows[0]["BankName"].ToString();
                    TxtBRCD.Text = DT.Rows[0]["BRANCH_CODE"].ToString();
                    TxtBRCDName.Text = DT.Rows[0]["BranchName"].ToString();
                    TxtNarration.Text = DT.Rows[0]["PARTICULARS"].ToString();
                    TxtCheqDate.Text = DT.Rows[0]["INSTRUDATE"].ToString().Replace("12:00:00", "");
                    TxtCheqNo.Text = DT.Rows[0]["INSTRU_NO"].ToString();
                    TxtCAmount.Text = DT.Rows[0]["INSTRU_AMOUNT"].ToString();
                    ViewState["CUSTNO"] = DT.Rows[0]["CUSTNO"].ToString();
                    ViewState["CUSTNAME"] = DT.Rows[0]["CUSTNAME"].ToString();
                    NEWENDN(false);
                    Photo_Sign(); ////added by ankita on 20/07/2017
                    DdlReason.Focus();
                }

                else 
                {
                    WebMsgBox.Show("A/C No. and A/C Type Needed.........", this.Page);
                    TxtAccType.Focus();
                    NEWENDN(true);
                }
            //}
            //else
            //{
            //    WebMsgBox.Show("Chq No. " + TxtCheqNo.Text + " is Already Returned...! ", this.Page);
            //    Clear();
            //    DdlReturnType.Focus();

            //}
            

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void NEWENDN(bool TF)
    {
        TxtAccType.Enabled = TF;
        TxtAcctypeName.Enabled = TF;
        TxtAccNo.Enabled = TF;
        TxtAccName.Enabled = TF;
    }
    protected void DdlReturnType_TextChanged(object sender, EventArgs e)
    {
        try
        {
          
            if(DdlReturnType.SelectedValue=="1")
            {
                BindReturn("I","C");
                LBL_IO.Text = "Inward Return";
            }
            else if (DdlReturnType.SelectedValue == "2")
            {
                BindReturn("O","D");
                LBL_IO.Text = "Outward Return";
            }
            TxtCheqNo.Focus();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindReturn(string FL,string CD)
    {
        try
        {
            Result = RI.GetReturnEntries(Grd_IOReturn, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), FL,CD);
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Grd_IOReturn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grd_IOReturn.PageIndex = e.NewPageIndex;
            if (DdlReturnType.SelectedValue == "1")
            {
                BindReturn("I", "C");
                LBL_IO.Text = "Inward Return";
            }
            else if (DdlReturnType.SelectedValue == "2")
            {
                BindReturn("O", "D");
                LBL_IO.Text = "Outward Return";
            }
        
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void SumIwOw()     ////added by ankita on 20/07/2017
    {
        try
        {
            DataTable DT;
            DT = CMM.IWOWSum("DETAILS", "CDETAILS", Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            if (DT.Rows.Count > 0)
            {
                TxtIwcTotal.Text = DT.Rows[0]["SumIWC"].ToString();
                TxtIwcRTotal.Text = DT.Rows[0]["SumIWCR"].ToString();
                TxtIWCUTotal.Text = DT.Rows[0]["SumIWCU"].ToString();

                TxtOwcTotal.Text = DT.Rows[0]["SumOWC"].ToString();
                TxtOwcRTotal.Text = DT.Rows[0]["SumOWCR"].ToString();
                TxtOWCUTotal.Text = DT.Rows[0]["SumOWCU"].ToString();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Photo_Sign()     ////added by ankita on 20/07/2017
    {
        try
        {
            string FileName = "";
            DataTable dt = CMM.ShowIMAGE(ViewState["CUSTNO"].ToString(), Session["BRCD"].ToString(), TxtAccNo.Text);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                String FilePath = "";
                byte[] bytes = null;
                for (int y = 0; y < 2; y++)
                {
                    if (y == 0)
                    {
                        FilePath = dt.Rows[i]["SignIMG"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])dt.Rows[i]["SignIMG"];

                    }
                    else
                    {
                        FilePath = dt.Rows[i]["PhotoImg"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])dt.Rows[i]["PhotoImg"];
                    }
                    if (FilePath != "")
                    {

                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);


                        if (y == 0)
                        {

                            Img1.Src = "data:image/tif;base64," + base64String;
                        }
                        else if (y == 1)
                        {
                            Img2.Src = "data:image/tif;base64," + base64String;
                        }
                    }
                    else
                    {
                        if (y == 0)
                        {

                            Img1.Src = "";
                        }
                        else if (y == 1)
                        {
                            Img2.Src = "";
                        }
                    }
                }
            }
            else
            {
                Img1.Src = "";
                Img2.Src = "";
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}