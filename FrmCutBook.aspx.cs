
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCutBook : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCutBook CB = new ClsCutBook();
    ClsBindBrDetails ASM = new ClsBindBrDetails();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    string bankcd;

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
                TxtTDate.Text = Session["EntryDate"].ToString();
                autoglname.ContextKey = Session["BRCD"].ToString();
                //added by ankita 07/10/2017 to make user frndly 
                TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtPType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] GL = BD.GetAccTypeGL(TxtPType.Text, Session["BRCD"].ToString()).Split('_'); ;
            TxtProName.Text = GL[0].ToString();
            ViewState["GL"] = GL[1].ToString();
            if (rdbAll.Checked == true)
                TxtTDate.Focus();
            else
                TxtFDate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void CustBook_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CutBk_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGrid()
    {
        try
        {
            DataTable DT = new DataTable();
            int retrow;
            DT = CB.CreateCutBook(Session["UserName"].ToString(), ViewState["GL"].ToString(), TxtPType.Text, Session["BRCD"].ToString(), TxtTDate.Text );
            if (DT.Rows.Count > 0)
            {
            }

            if (DT != null)
            {
                if (DT.Rows.Count != 0)
                {
                    GrdAccountSTS.DataSource = DT;
                    GrdAccountSTS.DataBind();
                    retrow = DT.Rows.Count;
                }
                else
                {
                    DT.Rows.Add(DT.NewRow());
                    GrdAccountSTS.DataSource = DT;
                    GrdAccountSTS.DataBind();
                    int TotalColumns = GrdAccountSTS.Rows[0].Cells.Count;
                    GrdAccountSTS.Rows[0].Cells.Clear();
                    GrdAccountSTS.Rows[0].Cells.Add(new TableCell());
                    GrdAccountSTS.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                    GrdAccountSTS.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    GrdAccountSTS.Rows[0].Cells[0].Text = "No Record Found";
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Report_Click(object sender, EventArgs e)
    {
        try
            {
                string glcode = CB.GetGlcode(Session["BRCD"].ToString(), TxtPType.Text);
                FL = "Insert";//ankita  15/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CutBk_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                bankcd = ASM.GetBankcd(Session["BRCD"].ToString());
                
                    if (rdbAll.Checked == true)
                    {
                        if (Convert.ToInt32(glcode) == 1)
                        {
                            string redirectURL = "FrmRView.aspx?FDate=" + TxtTDate.Text + "&GL=" + ViewState["GL"].ToString() + "&SGL=" + TxtPType.Text + "&MID=" + Session["UserName"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptCutBookSa.rdlc";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                        }
                        else
                        {
                            string redirectURL = "FrmRView.aspx?FDate=" + TxtTDate.Text + "&GL=" + ViewState["GL"].ToString() + "&SGL=" + TxtPType.Text + "&MID=" + Session["UserName"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptCutBook.rdlc";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                        }
                    }
                    if (rbdDepWise.Checked == true)
                    {
                         string redirectURL = "FrmRView.aspx?FDate=" + TxtTDate.Text + "&GL=" + ViewState["GL"].ToString() + "&SGL=" + TxtPType.Text + "&MID=" + Session["UserName"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptCutBook_DepWise.rdlc";
                         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    }
                    if (rdbSpecific.Checked == true)
                    {
                        string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&SGL=" + TxtPType.Text + "&MID=" + "&rptname=RptCuteBookDetails.rdlc";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    }
                    if (rdbAllRecSrno.Checked == true)
                    {
                        string redirectURL = "FrmRView.aspx?FDate=" + TxtTDate.Text + "&GL=" + ViewState["GL"].ToString() + "&SGL=" + TxtPType.Text + "&MID=" + Session["UserName"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptCuteBookRecSrno.rdlc";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    }
                }
         catch (Exception Ex)
         {
             ExceptionLogging.SendErrorToText(Ex);
         }
    }
    protected void TextReport_Click(object sender, EventArgs e)
    {
        //Session["UserName"].ToString(), ViewState["GL"].ToString(), TxtPType.Text, Session["BRCD"].ToString(), TxtDate.Text
        FL = "Insert";//ankita 15/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CutBk_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        List<object> lst = new List<object>();
        lst.Add("Cut Book");
        lst.Add(Session["UserName"].ToString());
        lst.Add(ViewState["GL"].ToString());
        lst.Add(TxtPType.Text);
        lst.Add(Session["BRCD"].ToString());
        lst.Add(TxtTDate.Text );
        CutBookTxt repObj = new CutBookTxt();
        repObj.RInit(lst);
        repObj.Start();
    }
    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        TDT.Visible = true;
        FDT.Visible = false;
    }
    protected void rdbSpecific_CheckedChanged(object sender, EventArgs e)
    {
            TDT.Visible = true;
            FDT.Visible = true;
    }
    protected void TxtProName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtProName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtProName.Text = CT[0].ToString();
                TxtPType.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtPType.Text, Session["BRCD"].ToString()).Split('_');
                //ViewState["DRGL"] = GLS[1].ToString();
                //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPType.Text + "_" + ViewState["DRGL"].ToString();

               if (TxtProName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtPType.Text = "";
                    TxtPType.Focus();

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
}