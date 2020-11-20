using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmInword_Parameter : System.Web.UI.Page
{
    ClsOwgPara OP = new ClsOwgPara();
    int Result;
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

            BindGrid();
            ViewState["Flag"] = "A";
            TxtEffectDT.Text = Session["EntryDate"].ToString();
        }
    }
    public void BindGrid()
    {
        OP.BindGrid(grdMaster, Session["BRCD"].ToString());
    }
    public void CallEdite()
    {
        DataTable DT = new DataTable();
        DT = OP.GetInfo(ViewState["strnumId"].ToString(), Session["BRCD"].ToString());
        if (DT.Rows.Count > 0)
        {
            TxtEffectDT.Text = Convert.ToDateTime(DT.Rows[0]["EFFECTDATE"]).ToString("dd/MM/yyyy");
            TxtCLGName.Text = DT.Rows[0]["CLG_GL_NAME"].ToString();
            TxtCLGNO.Text = DT.Rows[0]["CLG_GL_NO"].ToString();
            TxtEntryDT.Text = DT.Rows[0]["ENTRYDATE"].ToString();
            TxtOWGType.Text = DT.Rows[0]["OWG_TYPE"].ToString();
            TxtRTGLName.Text = DT.Rows[0]["RETURN_GL_NAME"].ToString();
            TxtRTGLNo.Text = DT.Rows[0]["RETURN_GL_NO"].ToString();
            TxtFunDT.Text = DT.Rows[0]["FUNNDINGDATE"].ToString();
        }
    }
    protected void grdMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdMaster.PageIndex = e.NewPageIndex;

        BindGrid();
    }

    protected void Submit_Click(object sender, EventArgs e)
    {

        if (ViewState["Flag"].ToString() == "A")
        {
            Result = OP.Insertpara("INSERT", "0", TxtEffectDT.Text, Session["BRCD"].ToString(), TxtOWGType.Text, TxtEntryDT.Text, TxtFunDT.Text, "1", TxtCLGNO.Text, TxtCLGName.Text, TxtRTGLNo.Text, TxtRTGLName.Text,Session["MID"].ToString(),"IN");
            BindGrid();
            ClearData();
            WebMsgBox.Show("Record Added Successfully........!!", this.Page);
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Inw_Paramtr_Add_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            return;
        }
        else if (ViewState["Flag"].ToString() == "ED")
        {
            string FL = "";
            if (ViewState["Flag"].ToString() == "ED")
            {
                FL = "UPDATE";
            }
            Result = 0; //OP.Insertpara("UPDATE", ViewState["strnumId"].ToString(), TxtEffectDT.Text, Session["BRCD"].ToString(), TxtOWGType.Text, TxtEntryDT.Text, TxtFunDT.Text, "1", TxtCLGNO.Text, TxtCLGName.Text, TxtRTGLNo.Text, TxtRTGLName.Text);
            BindGrid();
            ClearData();
            WebMsgBox.Show("Record Modify Successfully........!!", this.Page);
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Inw_Paramtr_Mod_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            return;
        }
        else if (ViewState["Flag"].ToString() == "D")
        {
            Result = OP.DeletePara(ViewState["strnumId"].ToString());
            BindGrid();
            ClearData();
            WebMsgBox.Show("Record Delete Successfully........!!", this.Page);
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Inw_Paramtr_Del_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            return;
        }
    }
    public void ClearData()
    {
        TxtEffectDT.Text = "";
        TxtCLGName.Text = "";
        TxtCLGNO.Text = "";
        TxtEntryDT.Text = "";
        TxtOWGType.Text = "";
        TxtRTGLName.Text = "";
        TxtRTGLNo.Text = "";
        TxtFunDT.Text = "";
    }
    public void ENDN(bool TF)
    {
        TxtEffectDT.Enabled = TF;
        TxtCLGName.Enabled = TF;
        TxtCLGNO.Enabled = TF;
        TxtEntryDT.Enabled = TF;
        TxtOWGType.Enabled = TF;
        TxtRTGLName.Enabled = TF;
        TxtRTGLNo.Enabled = TF;
        TxtFunDT.Enabled = TF;
    }
    protected void TxtCLGNO_TextChanged(object sender, EventArgs e)
    {
        TxtCLGName.Text = OP.GetGLName(TxtCLGNO.Text, Session["BRCD"].ToString());
    }
    protected void TxtRTGLNo_TextChanged(object sender, EventArgs e)
    {
        TxtRTGLName.Text = OP.GetGLName(TxtRTGLNo.Text, Session["BRCD"].ToString());
    }
    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "A"; ;
        Submit.Text = "Submit";
        ClearData();
        ENDN(true);
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton objlink = (LinkButton)sender;
        string strnumid = objlink.CommandArgument;
        ViewState["strnumId"] = strnumid;
        ViewState["Flag"] = "ED";
        Submit.Text = "Modify";
        ENDN(true);
        CallEdite();
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton objlink = (LinkButton)sender;
        string strnumid = objlink.CommandArgument;
        ViewState["strnumId"] = strnumid;
        ViewState["Flag"] = "D";
        Submit.Text = "Delete";
        ENDN(false);
        CallEdite();
    }
}