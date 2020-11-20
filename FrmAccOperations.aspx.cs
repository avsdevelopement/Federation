using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmAccOperations : System.Web.UI.Page
{
    scustom customcs = new scustom();
    ClsAccopen accop = new ClsAccopen();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAccOperations CAO = new ClsAccOperations();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsLogMaintainance CLM = new ClsLogMaintainance();

    string FL = "", BRCD = "", PRD = "", ACC = "", STATUS = "", DDL = "", CNO="";

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
                Div_ACC.Visible = false;
                Div_DEPOSIT.Visible = false;
                Div_CUST.Visible = false;
                Div_Buttons.Visible = false;
                TxtBRCD.Text = Session["BRCD"].ToString();
                TxtBRCDName.Text = AST.GetBranchName(TxtBRCD.Text);
                autoglname.ContextKey = Session["BRCD"].ToString();
                TxtDBRCD.Text = Session["BRCD"].ToString();
                TxtDBRCDname.Text = AST.GetBranchName(TxtDBRCD.Text);
                autoglnameDP.ContextKey = Session["BRCD"].ToString();
                TxtBRCDC.Text = Session["BRCD"].ToString();
                TxtBRCDNameC.Text = AST.GetBranchName(TxtBRCDC.Text);
                autocustname.ContextKey = Session["BRCD"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
       
    }
    protected void Rdb_No_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_No.SelectedValue == "1")
            {
                Div_ACC.Visible = true;
                Div_DEPOSIT.Visible = false;
                Div_CUST.Visible = false;
                Div_Buttons.Visible = true;
            }

            else if (Rdb_No.SelectedValue == "2")
            {
                Div_ACC.Visible = false;
                Div_DEPOSIT.Visible = false;
                Div_CUST.Visible = false;
                Div_Buttons.Visible = false;
            }
            else if (Rdb_No.SelectedValue == "3")
            {
                Div_ACC.Visible = false;
                Div_DEPOSIT.Visible = false;
                Div_CUST.Visible = true;
                Div_Buttons.Visible = true;
            }
 }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
 }
     protected void TxtBRCD_TextChanged(object sender, EventArgs e)
    {
         try
        {
            if (TxtBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtBRCD.Text);
                if (bname != null)
                {
                    TxtBRCDName.Text = bname;
                    TxtPRD.Focus();
                        
                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtBRCD.Text = "";
                    TxtBRCD.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtBRCD.Text = "";
                TxtBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtPRD_TextChanged(object sender, EventArgs e)
    {
        try 
        {
            if (TxtBRCD.Text != "")
            {
                TxtPRDName.Text = AST.GetAccType(TxtPRD.Text, TxtBRCD.Text);

                string[] GL = BD.GetAccTypeGL(TxtPRD.Text, TxtBRCD.Text).Split('_');
                TxtPRDName.Text = GL[0].ToString();
                ViewState["GL"] = GL[1].ToString();
                autoglnameA.ContextKey = TxtBRCD.Text + "_" + TxtPRD.Text + "_" + ViewState["GL"].ToString();
                TxtAC.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtPRD.Text = "";
                TxtBRCD.Focus();
            }
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAC_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AN;
            AN = customcs.GetAccountName(TxtAC.Text, TxtPRD.Text, Session["BRCD"].ToString()).Split('_');
            if (AN != null)
            {
                TxtACName.Text = AN[1].ToString();
             }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                TxtAC.Text = "";
                TxtAC.Focus();
            }
            if (TxtBRCD.Text != "" && TxtPRD.Text != "" && TxtAC.Text != "")
            {
                FL = "ACSTATUS";
                BRCD = TxtBRCD.Text;
                PRD = TxtPRD.Text;
                ACC = TxtAC.Text;
                string result = CAO.SaveData(FL, TxtBRCD.Text, TxtPRD.Text, TxtAC.Text, ViewState["GL"].ToString());
                TxtAcStatus.Text = result;
            }
            if (TxtBRCD.Text != "" && TxtPRD.Text != "" && TxtAC.Text != "")
            {
                FL = "OpBal";
                BRCD = TxtBRCD.Text;
                PRD = TxtPRD.Text;
                ACC = TxtAC.Text;
                string res = CAO.GetBalance(FL, TxtBRCD.Text, TxtPRD.Text, TxtAC.Text,Session["EntryDate"].ToString());
                TxtBalance.Text = res;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtDBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtDBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtDBRCD.Text);
                if (bname != null)
                {
                    TxtDBRCDname.Text = bname;
                    TxtDPRD.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtDBRCD.Text = "";
                    TxtDBRCD.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtDBRCD.Text = "";
                TxtDBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtDPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtDBRCD.Text != "")
            {
                TxtDPRDName.Text = AST.GetAccType(TxtDPRD.Text, TxtDBRCD.Text);

                string[] GL = BD.GetAccTypeGL(TxtDPRD.Text, TxtDBRCD.Text).Split('_');
                TxtDPRDName.Text = GL[0].ToString();
                ViewState["GL"] = GL[1].ToString();
                autoglnameAC2.ContextKey = TxtDBRCD.Text + "_" + TxtDPRD.Text + "_" + ViewState["GL"].ToString();
                TxtAcNoD.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtDPRD.Text = "";
                TxtDBRCD.Focus();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAcNoD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AN;
            AN = customcs.GetAccountName(TxtAcNoD.Text, TxtDPRD.Text, Session["BRCD"].ToString()).Split('_');
            if (AN != null)
            {
                TxtAcNameD.Text = AN[1].ToString();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                TxtAcNoD.Text = "";
                TxtAcNoD.Focus();
            }
            if (TxtDBRCD.Text != "" && TxtDPRD.Text != "" && TxtAcNoD.Text != "")
            {
                FL = "ACSTATUS";
                BRCD = TxtDBRCD.Text;
                PRD = TxtDPRD.Text;
                ACC = TxtAcNoD.Text;
                string result = CAO.SaveData(FL, TxtDBRCD.Text, TxtDPRD.Text, TxtAcNoD.Text, ViewState["GL"].ToString());
                TxtStatusD.Text = result;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
  protected void TxtBRCDC_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCDC.Text != "")
            {
                string bname = AST.GetBranchName(TxtBRCDC.Text);
                if (bname != null)
                {
                    TxtBRCDNameC.Text = bname;
                    Txtcstno.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtBRCDC.Text = "";
                    TxtBRCDC.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtBRCDC.Text = "";
                TxtBRCDC.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_No.SelectedValue == "1")
            {
                if (TxtBRCD.Text != "" && TxtPRD.Text != "" && TxtAC.Text != "" && DDLStatus.SelectedValue != "")
                {
                    FL = "UPDATE";
                    BRCD = TxtBRCD.Text;
                    PRD = TxtPRD.Text;
                    ACC = TxtAC.Text;
                    DDL = DDLStatus.SelectedValue;
                    if(DDLStatus.SelectedValue =="Open")
                    {
                        int  res = CAO.Updatedate(FL, TxtBRCD.Text, TxtPRD.Text, TxtAC.Text, ViewState["GL"].ToString(), DDLStatus.SelectedValue, Session["EntryDate"].ToString());
                        if (res >=1)
                        {
                            WebMsgBox.Show("Data Updated!!!!", this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Accoperations _Acc_Open_" + TxtPRD.Text + "_" +TxtAC.Text+ "_"+ Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            Clear();
                        }
                        else
                        {
                            WebMsgBox.Show("Balance Exists on Account No " + ACC + " and Product Code "+PRD+" !", this.Page);

                        }
                    }
                    else if(DDLStatus.SelectedValue=="Close")
                    {
                        int  res = CAO.Updatedate(FL, TxtBRCD.Text, TxtPRD.Text, TxtAC.Text, ViewState["GL"].ToString(), DDLStatus.SelectedValue, Session["EntryDate"].ToString());
                        if (res >1)
                        {
                            WebMsgBox.Show("Data Updated!!!!", this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Accoperations _Acc_Close_" + TxtPRD.Text + "_" + TxtAC.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            Clear();
                        }

                        else
                        {
                            WebMsgBox.Show("Balance Exists on Account No " + ACC + " and Product Code "+PRD+" !", this.Page);
                        }
                    }
                }
            }
            else if (Rdb_No.SelectedValue == "2")
            {
                if (TxtDBRCD.Text != "" && TxtDPRD.Text != "" && TxtAcNoD.Text != "" && DDLStatus.SelectedValue != "")
                {
                    FL = "UPDATE";
                    BRCD = TxtDBRCD.Text;
                    PRD = TxtDPRD.Text;
                    ACC = TxtAcNoD.Text;
                    DDL = DDLStatusD.SelectedValue;
                    int res = CAO.Updatedate(FL, TxtDBRCD.Text, TxtDPRD.Text, TxtAcNoD.Text, ViewState["GL"].ToString(), DDLStatusD.SelectedValue, Session["EntryDate"].ToString());
                    if (res > 1)
                    {
                        WebMsgBox.Show("Data Updated!!!!", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Accoperations _Acc_Open_" + TxtDPRD.Text + "_" + TxtAcNoD.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        Clear();
                    }
                    else
                    {
                        WebMsgBox.Show("Data is Not Valid!!!", this.Page);
                    }
                }
            }
            else if (Rdb_No.SelectedValue == "3")
            {
                if (TxtBRCDC.Text != "" && Txtcstno.Text != "" && DDLStatus.SelectedValue != "")
                {
                    FL = "CUSTOMER";
                    BRCD = TxtBRCDC.Text;
                    CNO = Txtcstno.Text;
                    DDL = DDLstatusc.SelectedValue;
                    int  res = CAO.CustData(FL, TxtBRCDC.Text, Txtcstno.Text,DDLstatusc.SelectedValue);
                    if (res > 0)
                    {
                        WebMsgBox.Show("Data Updated!!!!", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Accoperations _Cust__" + Txtcstno.Text + "_"  + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        Clear();
                    }
                    else
                    {
                        WebMsgBox.Show("Data is Not Valid!!!", this.Page);
                    }
                }
            }
        }
        catch (Exception Ex)
        {
             ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void Txtcstno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Txtcstno.Text == "")
            {
                return;
            }
            string custname = accop.Getcustname(Txtcstno.Text.ToString());
            string[] name = custname.Split('_');
            TxtCname.Text = name[0].ToString();
            string RC = TxtCname.Text;
            if (RC == "")
            {
                WebMsgBox.Show("Customer not found", this.Page);
                Txtcstno.Text = "";
                Txtcstno.Focus();
                return;
            }
        }
            catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
   
    public void Clear()
    {
        DDLStatus.SelectedValue = "0";
        TxtBRCD.Text="";
        TxtBRCDName.Text="";
        TxtPRD.Text="";
        TxtPRDName.Text="";
        TxtAC.Text="";
        TxtACName.Text = "";
        TxtAcStatus.Text = "";
        TxtDBRCD.Text = "";
        TxtDBRCDname.Text = "";
         TxtDPRD.Text = "";
         TxtDPRDName.Text = "";
         TxtAcNoD.Text = "";
         TxtAcNameD.Text = "";
         TxtStatusD.Text = "";
          TxtBRCDC.Text = "";
         TxtBRCDNameC.Text = "";
          Txtcstno.Text = "";
          TxtCname.Text = "";
         DDLStatusD.SelectedValue = "0";
          DDLstatusc.SelectedValue = "0";
          TxtBalance.Text = "";
    }
    protected void TxtCname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtCname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtCname.Text = custnob[0].ToString();
                Txtcstno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
            }
             else
            {
                WebMsgBox.Show("Customer Number is Invalid....!", this.Page);
                Txtcstno.Text = "";
                Txtcstno.Focus();
            }
          }
         catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtPRDName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                string custno = TxtPRDName.Text;
                string[] CT = custno.Split('_');
                if (CT.Length > 0)
                {
                    TxtPRDName.Text = CT[0].ToString();
                    TxtPRD.Text = CT[1].ToString();
                    string[] GLS = BD.GetAccTypeGL(TxtPRD.Text, TxtBRCD.Text).Split('_');
                    ViewState["GL"] = GLS[1].ToString();
                    autoglnameA.ContextKey = TxtBRCD.Text + "_" + TxtPRD.Text + "_" + ViewState["GL"].ToString();

                }
                TxtAC.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtPRDName.Text = "";
                TxtBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    protected void TxtACName_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtACName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtACName.Text = custnob[0].ToString();
                TxtAC.Text = custnob[1].ToString();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                TxtAC.Text = "";
                TxtAC.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtDPRDName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtDBRCD.Text != "")
            {
                string custno = TxtDPRDName.Text;
                string[] CT = custno.Split('_');
                if (CT.Length > 0)
                {
                    TxtDPRDName.Text = CT[0].ToString();
                    TxtDPRD.Text = CT[1].ToString();
                    string[] GLS = BD.GetAccTypeGL(TxtDPRD.Text, TxtDBRCD.Text).Split('_');
                    ViewState["GL"] = GLS[1].ToString();
                    autoglnameAC2.ContextKey = TxtDBRCD.Text + "_" + TxtDPRD.Text + "_" + ViewState["GL"].ToString();

                }
                TxtAcNoD.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtDPRDName.Text = "";
                TxtDBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAcNameD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtAcNameD.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtAcNameD.Text = custnob[0].ToString();
                TxtAcNoD.Text = custnob[1].ToString();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                TxtAcNoD.Text = "";
                TxtAcNoD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}