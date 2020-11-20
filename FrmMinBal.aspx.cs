using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmMinBal : System.Web.UI.Page
{
    scustom customcs = new scustom();
    ClsMinBal CMB = new ClsMinBal();
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    int Result;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    public static string Flag;
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
                Txtprdcode.Focus();
                ViewState["UN_FL"] = Request.QueryString["FLAG"].ToString();
                autoglname.ContextKey = Session["BRCD"].ToString();
                AutoPlGlName.ContextKey = Session["BRCD"].ToString();
                BD.BindACCTYPE(Ddlacctype);
                BD.BindFreq(DDlFreType);
                Bindgrid("ED");
                Flag = "1";
                ViewState["Flag"] = "AD";
                BtnAdd.Text = "Submit";
                TblDiv_MainWindow.Visible = false;
                Div_grid.Visible = true;

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void Txtprdcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(Txtprdcode.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');
            ViewState["DRGL"] = GL[1].ToString();
            AutoPlGlName.ContextKey = Session["BRCD"].ToString() + "_" + Txtprdcode.Text + "_" + ViewState["DRGL"].ToString();
            string PDName = customcs.GetProductName(Txtprdcode.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                Txtprdname.Text = PDName;
                Txtaccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                Txtprdcode.Text = "";
                Txtprdcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtprdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = Txtprdname.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                Txtprdname.Text = CT[0].ToString();
                Txtprdcode.Text = CT[1].ToString();
                string[] GLS = BD.GetAccTypeGL(Txtprdcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                AutoPlGlName.ContextKey = Session["BRCD"].ToString() + "_" + Txtprdcode.Text + "_" + ViewState["DRGL"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            TxtwithChq.Text = (TxtwithChq.Text == "" ? "0" : TxtwithChq.Text);
            TxtwithoutChq.Text = (TxtwithoutChq.Text == "" ? "0" : TxtwithoutChq.Text);
            Txtchrgw.Text = (Txtchrgw.Text == "" ? "0" : Txtchrgw.Text);
            Txtchrgwit.Text = (Txtchrgwit.Text == "" ? "0" : Txtchrgwit.Text);
            TxtFree.Text = (TxtFree.Text == "" ? "0" : TxtFree.Text);
            TxtMinChg.Text = (TxtMinChg.Text == "" ? "0" : TxtMinChg.Text);
            TxtMaxChg.Text = (TxtMaxChg.Text == "" ? "0" : TxtMaxChg.Text);
            if (ViewState["Flag"].ToString() == "AD")
            {
                if (Txtprdcode.Text != "" && Txtaccno.Text != "")
                {
                    int Result = CMB.InsertData(Txtprdcode.Text, Txteffcdate.Text, DdlAlS.SelectedValue, TxtAcctype.Text, DdlSkChrg.SelectedValue, DdlTodBal.SelectedValue, TxtFno.Text, TxtwithChq.Text, Txtchrgw.Text, TxtwithoutChq.Text, Txtchrgwit.Text, TxtFree.Text, Txtaccno.Text, TxtMinChg.Text, TxtMaxChg.Text, TxtAppDate.Text, Txtpartclrs.Text, Session["BRCD"].ToString(), "1001", Session["EntryDate"].ToString(), Session["MID"].ToString());
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data Added successfully..!!", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "MinBal_Add _" + Txtprdcode.Text + "_" + Txtaccno.Text +"_"+ Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        Bindgrid("PA");
                        return;
                    }
                }
            }

            else if (ViewState["Flag"].ToString() == "MD")
            {
                if (Txtprdcode.Text != "" && Txtaccno.Text != "")
                {
                    int Result = CMB.ModifyData(Txtprdcode.Text, Txteffcdate.Text, DdlAlS.SelectedValue, TxtAcctype.Text, DdlSkChrg.SelectedValue, DdlTodBal.SelectedValue, TxtFno.Text, TxtwithChq.Text, Txtchrgw.Text, TxtwithoutChq.Text, Txtchrgwit.Text, TxtFree.Text, Txtaccno.Text, TxtMinChg.Text, TxtMaxChg.Text, TxtAppDate.Text, Txtpartclrs.Text, Session["BRCD"].ToString(), "1001", Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["Id"].ToString());
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data Modified successfully..!!", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "MinBal_Mod _" + Txtprdcode.Text + "_" + Txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        Bindgrid("PA");
                        return;
                    }
                }
            }
            if (ViewState["Flag"].ToString() == "AT")
            {

                if (Txtprdcode.Text != "" && Txtaccno.Text != "")
                {
                    string AT = CMB.GetStage(Session["BRCD"].ToString(), Txtprdcode.Text, Txtaccno.Text);
                    if (AT == "1003")
                    {
                        if (ViewState["Flag"].ToString() == "AT")
                        {
                            WebMsgBox.Show("Record Already Authorized!", this.Page);
                            return;
                        }
                    }
                    int Result = CMB.AuthoriseData(Session["BRCD"].ToString(), Txtprdcode.Text, Txtaccno.Text, Session["MID"].ToString(), ViewState["Id"].ToString());
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data Authorized successfully..!!", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "MinBal_Autho _" + Txtprdcode.Text + "_" + Txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        Bindgrid("PA");
                        return;
                    }
                }
            }
            if (ViewState["Flag"].ToString() == "DL")
            {
                if (Txtprdcode.Text != "" && Txtaccno.Text != "")
                {
                    string AT = CMB.GetStage(Session["BRCD"].ToString(), Txtprdcode.Text, Txtaccno.Text);
                    if (AT == "1003")
                    {
                        if (ViewState["Flag"].ToString() == "DL")
                        {
                            WebMsgBox.Show("Record Already Authorized, You cannot delete !", this.Page);
                            return;
                        }
                    }
                    int Result = CMB.DeleteData(Session["BRCD"].ToString(), Txtprdcode.Text, Txtaccno.Text, Session["MID"].ToString(), ViewState["Id"].ToString());
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data Deleted successfully..!!", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "MinBal_Del _" + Txtprdcode.Text + "_" + Txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        Bindgrid("PA");
                        return;
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
        ClearData();
    }
    public void ClearData()
    {
        Txtprdcode.Text = "";
        Txtprdname.Text = "";
        Txteffcdate.Text = "";
        DdlAlS.SelectedValue = "0";
        TxtAcctype.Text = "";
        Ddlacctype.SelectedValue = "0";
        DdlSkChrg.SelectedValue = "0";
        DdlTodBal.SelectedValue = "0";
        TxtFno.Text = "";
        DDlFreType.SelectedValue = "0";
        TxtwithChq.Text = "";
        TxtwithoutChq.Text = "";
        Txtchrgw.Text = "";
        Txtchrgwit.Text = "";
        TxtFree.Text = "";
        Txtaccno.Text = "";
        TxtMinChg.Text = "";
        TxtMaxChg.Text = "";
        TxtAppDate.Text = "";
        Txtpartclrs.Text = "";
        Txtaccname.Text = "";
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmMinBal.aspx?Flag=AD.aspx", true);
    }
    protected void TxtBatch_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Ddlacctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            TxtAcctype.Text = BD.GetNOACCT(Ddlacctype.SelectedItem.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DDlFreType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            TxtFno.Text = BD.GetFrequency(DDlFreType.SelectedItem.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void CallEdit()
    {
        try
        {
            if ((ViewState["Flag"].ToString() == "MD") || (ViewState["Flag"].ToString() == "DL") || (ViewState["Flag"].ToString() == "AT"))
            {
                DT = CMB.GetInfo(Session["BRCD"].ToString(), ViewState["Id"].ToString(), ViewState["PRDCODE"].ToString(), ViewState["CREDITPLACC"].ToString());
                if (DT.Rows.Count > 0)
                {
                    Txtprdcode.Text = DT.Rows[0]["PRDCODE"].ToString();
                    string PDName = customcs.GetProductName(Txtprdcode.Text, Session["BRCD"].ToString());
                    if (PDName != null)
                    {
                        Txtprdname.Text = PDName;
                    }
                    Txteffcdate.Text = DT.Rows[0]["EFFDATE"].ToString().Replace("12:00:00 AM", "");
                    DdlAlS.SelectedValue = string.IsNullOrEmpty(DT.Rows[0]["ALLSELECTED"].ToString()) ? "0" : DT.Rows[0]["ALLSELECTED"].ToString();
                    TxtAcctype.Text = DT.Rows[0]["ACCTYPE"].ToString();
                    Ddlacctype.SelectedIndex = Convert.ToInt32(DT.Rows[0]["ACCTYPE"].ToString());
                    DdlSkChrg.SelectedValue = string.IsNullOrEmpty(DT.Rows[0]["SKIPCHARGES"].ToString()) ? "0" : DT.Rows[0]["SKIPCHARGES"].ToString();
                    DdlTodBal.SelectedValue = string.IsNullOrEmpty(DT.Rows[0]["ALLOWTODBAL"].ToString()) ? "0" : DT.Rows[0]["ALLOWTODBAL"].ToString();
                    TxtFno.Text = DT.Rows[0]["FREQUENCY"].ToString();
                    DDlFreType.SelectedIndex = Convert.ToInt32(DT.Rows[0]["FREQUENCY"].ToString());
                    TxtwithChq.Text = DT.Rows[0]["MINBALWCB"].ToString();
                    TxtwithoutChq.Text = DT.Rows[0]["MINBALWTCB"].ToString();
                    Txtchrgw.Text = DT.Rows[0]["CHARGESWCB"].ToString();
                    Txtchrgwit.Text = DT.Rows[0]["CHARGESWTCB"].ToString();
                    TxtFree.Text = DT.Rows[0]["FREEINSTANCE"].ToString();
                    Txtaccno.Text = DT.Rows[0]["CREDITPLACC"].ToString();
                    string[] AN;
                    AN = customcs.GetPLAccName(Txtaccno.Text, Session["BRCD"].ToString()).Split('_');
                    if (AN != null)
                    {
                        Txtaccname.Text = AN[0].ToString();
                    }
                    TxtMinChg.Text = DT.Rows[0]["MINCHARGE"].ToString();
                    TxtMaxChg.Text = DT.Rows[0]["MAXCHARGE"].ToString();
                    TxtAppDate.Text = DT.Rows[0]["LASTAPPDATE"].ToString().Replace("12:00:00 AM", "");
                    Txtpartclrs.Text = DT.Rows[0]["PARTICULARS"].ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtaccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AN;
            AN = customcs.GetPLAccName(Txtaccno.Text, Session["BRCD"].ToString()).Split('_');
            if (AN != null)
            {
                Txtaccname.Text = AN[0].ToString();
                Txteffcdate.Focus();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                Txtaccno.Text = "";
                Txtaccno.Focus();
            }
            CallEdit();
            Bindgrid("PA");
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtaccname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = Txtaccname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                Txtaccname.Text = custnob[0].ToString();
                Txtaccno.Text = custnob[1].ToString();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                Txtaccno.Text = "";
                Txtaccno.Focus();
            }
            Bindgrid("PA");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grddisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grddisplay.PageIndex = e.NewPageIndex;
        Bindgrid("PA");
    }


    protected void Bindgrid(string Flag)
    {
        try
        {
            if (Flag == "ED")
            {
                int RS = CMB.GetGridData(grddisplay, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "", "");
            }

            else
            {
                int RS = CMB.GetGridData(grddisplay, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Txtprdcode.Text, Txtaccno.Text);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Enable(bool TF)
    {
        Txtprdcode.Enabled = TF;
        Txtprdname.Enabled = TF;
        Txtaccname.Enabled = TF;
        Txtaccno.Enabled = TF;
        Txteffcdate.Enabled = TF;
        DdlAlS.Enabled = TF;
        TxtAcctype.Enabled = TF;
        Ddlacctype.Enabled = TF;
        DdlSkChrg.Enabled = TF;
        DdlTodBal.Enabled = TF;
        TxtFno.Enabled = TF;
        DDlFreType.Enabled = TF;
        TxtwithChq.Enabled = TF;
        TxtwithoutChq.Enabled = TF;
        Txtchrgw.Enabled = TF;
        Txtchrgwit.Enabled = TF;
        TxtFree.Enabled = TF;
        TxtMinChg.Enabled = TF;
        TxtMaxChg.Enabled = TF;
        TxtAppDate.Enabled = TF;
        Txtpartclrs.Enabled = TF;

    }
    public void EnableMod(bool TF)
    {
        Txtprdcode.Enabled = TF;
        Txtprdname.Enabled = TF;
        Txtaccname.Enabled = TF;
        Txtaccno.Enabled = TF;
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            Txtprdcode.Focus();
            ViewState["Flag"] = "AD";
            BtnAdd.Visible = true;
            Flag = "1";
            TblDiv_MainWindow.Visible = true;
            Div_grid.Visible = true;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Id"] = ARR[0].ToString();
            ViewState["PRDCODE"] = ARR[1].ToString();
            ViewState["CREDITPLACC"] = ARR[2].ToString();
            ViewState["Flag"] = "MD";
            BtnAdd.Text = "Modify";
            TblDiv_MainWindow.Visible = true;
            EnableMod(false);
            CallEdit();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void LnkAutorise_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Id"] = ARR[0].ToString();
            ViewState["PRDCODE"] = ARR[1].ToString();
            ViewState["CREDITPLACC"] = ARR[2].ToString();
            ViewState["Flag"] = "AT";
            BtnAdd.Text = "Authorise";
            TblDiv_MainWindow.Visible = true;
            CallEdit();
            Enable(false);
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
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Id"] = ARR[0].ToString();
            ViewState["PRDCODE"] = ARR[1].ToString();
            ViewState["CREDITPLACC"] = ARR[2].ToString();
            ViewState["Flag"] = "DL";
            BtnAdd.Text = "Delete";
            TblDiv_MainWindow.Visible = true;
            CallEdit();
            Enable(false);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}