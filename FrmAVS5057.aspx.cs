using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmAVS5057 : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsAccountSTS AST = new ClsAccountSTS();
    scustom cc = new scustom();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsOpenClose OC = new ClsOpenClose();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
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
                string YEAR = "";
                YEAR = DateTime.Now.Date.Year.ToString();//DT.Year.ToString();

                TxtTDate.Text = Session["EntryDate"].ToString();
                TxtBRCD.Text = Session["BRCD"].ToString();
                TxtBrname.Text = AST.GetBranchName(TxtBRCD.Text);
                autoglname.ContextKey = Session["BRCD"].ToString();
                // TxtBRCD.Focus();
                TxtAccType.Focus();
                TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            }
            catch (Exception Ex)
            {

                ExceptionLogging.SendErrorToText(Ex);
            }

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
                    TxtBrname.Text = bname;
                    TxtAccType.Focus();

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
    protected void TxtAccType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                TxtATName.Text = AST.GetAccType(TxtAccType.Text, TxtBRCD.Text);

                string[] GL = BD.GetAccTypeGL(TxtAccType.Text, TxtBRCD.Text).Split('_');
                TxtATName.Text = GL[0].ToString();
                ViewState["GL"] = GL[1].ToString();
                AutoAccname.ContextKey = TxtBRCD.Text + "_" + TxtAccType.Text + "_" + ViewState["GL"].ToString();

                TxtAccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtAccType.Text = "";
                TxtBRCD.Focus();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void TxtATName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtAccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                string AT = "";
                // AT = BD.GetStage1(TxtAccno.Text, Session["BRCD"].ToString(), ViewState["Flag"].ToString());
                AT = BD.Getstage1(TxtAccno.Text, TxtBRCD.Text, TxtAccType.Text);
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise.........!!";
                    ModalPopup.Show(this.Page);
                    TxtAccHName.Text = "";
                    TxtAccno.Text = "";
                    TxtAccno.Focus();
                }
                else
                {

                    DataTable DT = new DataTable();
                    DT = AST.GetCustName(TxtAccType.Text, TxtAccno.Text, TxtBRCD.Text);
                    if (DT.Rows.Count > 0)
                    {
                        string[] TD = TxtTDate.Text.Split('/');
                        TxtAccHName.Text = DT.Rows[0]["CustName"].ToString();
                        TxtOPDT.Text = Convert.ToDateTime(DT.Rows[0]["OPENINGDATE"]).ToString("dd/MM/yyyy");
                        //--Abhishek
                        string RES = AST.GetAccStatus(TxtBRCD.Text, TxtAccno.Text, TxtAccType.Text);
                        if (RES == "1")
                            TxtACStatus.Text = "Active";
                        else if (RES == "9")
                            TxtACStatus.Text = "Suit File";
                        else
                            TxtACStatus.Text = "Deactive";
                        TxtClearBal.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtAccType.Text, TxtAccno.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();
                        TxtUnClearBal.Text = OC.GetOpenClose("Unclear", TD[2].ToString(), TD[1].ToString(), TxtAccType.Text, TxtAccno.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();
                    }

                    TxtFDate.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtAccno.Text = "";
                TxtBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void TxtAccHName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                string custno = TxtATName.Text;
                string[] CT = custno.Split('_');
                if (CT.Length > 0)
                {
                    TxtATName.Text = CT[0].ToString();
                    TxtAccType.Text = CT[1].ToString();
                    string[] GLS = BD.GetAccTypeGL(TxtAccType.Text, TxtBRCD.Text).Split('_');
                    ViewState["GL"] = GLS[1].ToString();
                    AutoAccname.ContextKey = TxtBRCD.Text + "_" + TxtAccType.Text + "_" + ViewState["GL"].ToString();

                }
                TxtAccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code First....!", this.Page);
                TxtATName.Text = "";
                TxtBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            Multiple.Visible = true;
             Div_CLEARBAL.Visible = true;
                GrdAccountSTS.Visible = true;
                BindGrid();
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "AccStat_Grd" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    //protected void Btn_UnClearBal_Click(object sender, EventArgs e)
    //{

    //}
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void Btn_Clear_Click(object sender, EventArgs e)
    {

    }
    protected void GrdAccountSTS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdAccountSTS.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    public void BindGrid()
    {
        try
        {
            DataTable DT = new DataTable();
            string FDT = "";
            string[] DTF;

            DTF = TxtFDate.Text.ToString().Split('/');
            FDT = TxtFDate.Text.ToString();
            string[] DTT = TxtTDate.Text.ToString().Split('/');

            DT = AST.AccountStatment(DTF[1].ToString(), DTT[1].ToString(), DTF[2].ToString(), DTT[2].ToString(), FDT, TxtTDate.Text.Trim(), TxtAccno.Text, TxtAccType.Text, Session["MID"].ToString(), Session["BRCD"].ToString(), ViewState["GL"].ToString(), TxtBRCD.Text);
            if (DT.Rows.Count > 0)
            {
                GrdAccountSTS.DataSource = DT;
                GrdAccountSTS.DataBind();
            }
            else
            {
                WebMsgBox.Show("No Unclear Records Found...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}