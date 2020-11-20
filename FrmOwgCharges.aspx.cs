using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class FrmOwgCharges : System.Web.UI.Page
{
    scustom CST = new scustom();
    ClsOWGCharges OWG = new ClsOWGCharges();
    DbConnection conn = new DbConnection();
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
            txteffect.Text = Session["EntryDate"].ToString();
            txtotype.Text = "Tr-owg";
            txtotype.Enabled = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string sql;
        if (ViewState["Flag"].ToString() == "A")
        {
            sql = "INSERT INTO OWG_CHARGES (EFFECT_DATE, BRCD, OWG_TYPE, TYPE,  FROM_AMOUNT, TO_AMT, CHARGES,PL_AccNo,stage) VALUES ('" + txteffect.Text + "','" + Session["BRCD"].ToString() + "','" + txtotype.Text + "','" + txttype.Text + "','" + txtamt.Text + "','" + txttoamt.Text + "', '" + txtcharges.Text + "','" + txtaccno.Text + "','1001')";
            conn.sExecuteQuery(sql);
            BindGrid();
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Owgcharges_Add_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            ClearData();
            WebMsgBox.Show("Record Added Successfully.......!!", this.Page);
        }
        else if (ViewState["Flag"].ToString() == "ED")
        {
            sql = "UPDATE OWG_CHARGES SET EFFECT_DATE = '" + txteffect.Text + "'), BRCD='" + Session["BRCD"].ToString() + "', OWG_TYPE='" + txtotype.Text + "', TYPE='" + txttype.Text + "',  FROM_AMOUNT='" + txtamt.Text + "', TO_AMT='" + txttoamt.Text + "', CHARGES='" + txtcharges.Text + "', PL_ACCNO='" + txtaccno.Text + "' where owgid ='" + ViewState["strnumId"].ToString() + "' ";
            int RC = conn.sExecuteQuery(sql);
            BindGrid();
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Owgcharges_Mod_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            ClearData();
            WebMsgBox.Show("Record Modify Successfully.......!!", this.Page);
        }
        else if (ViewState["Flag"].ToString() == "D")
        {
            sql = "update OWG_CHARGES set stage='1004' where owgid='" + ViewState["strnumId"].ToString() + "'";
            int RC = conn.sExecuteQuery(sql);
            BindGrid();
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Owgcharges_Del_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            ClearData();
            WebMsgBox.Show("Record Delete Successfully.......!!", this.Page);
        }
    }
    public void BindGrid()
    {
        int RS = OWG.BindCharges(grdCharged,Session["BRCD"].ToString());
    }

    protected void grdCharged_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCharged.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    //protected void Addnew_Click(object sender, EventArgs e)
    //{
    //    ViewState["Flag"] = "A";
    //    btnSubmit.Text = "Submit";
    //    ENDN(true);
    //}
    //protected void Edit_Click(object sender, EventArgs e)
    //{
    //    Button objbtn = (Button)sender;
    //    string strid = objbtn.CommandArgument;
    //    ViewState["strnumId"] = strid.ToString();
    //    ViewState["Flag"] = "ED";
    //    btnSubmit.Text = "Modify";
    //    CallGrid();
    //    ENDN(true);
    //}
    //protected void Delete_Click(object sender, EventArgs e)
    //{
    //    Button objbtn = (Button)sender;
    //    string strid = objbtn.CommandArgument;
    //    ViewState["strnumId"] = strid.ToString();
    //    btnSubmit.Text = "Delete";
    //    CallGrid();
    //    ViewState["Flag"] = "D";
    //    ENDN(false);
    //}
    public void CallGrid()
    {
        string sql = "SELECT EFFECT_DATE,OWG_TYPE,TYPE,FROM_AMOUNT,TO_AMT,CHARGES,PL_ACCNO,LNO1.DESCRIPTION,GL.GLNAME FROM owg_charges OWG " +
                     " LEFT JOIN (SELECT DESCRIPTION,SRNO FROM LOOKUPFORM1 WHERE  LNO='1028') LNO1 ON LNO1.SRNO=OWG.TYPE" +
                     " LEFT JOIN (SELECT GLNAME,SUBGLCODE ID FROM GLMAST WHERE BRCD='" + Session["BRCD"].ToString() + "' ) GL ON GL.ID=OWG.PL_ACCNO " +
                     " WHERE  owgid='" + ViewState["strnumId"].ToString() + "'";
        DataTable DT = new DataTable();
        DT = conn.GetDatatable(sql);
        if (DT.Rows.Count > 0)
        {
            txtaccno.Text = DT.Rows[0]["pl_accno"].ToString();
            TxtAccName.Text = DT.Rows[0]["GLNAME"].ToString();
            txtamt.Text = DT.Rows[0]["From_Amount"].ToString();
            txtcharges.Text = DT.Rows[0]["Charges"].ToString();
            txteffect.Text =Convert.ToDateTime(DT.Rows[0]["effect_date"]).ToString("dd/MM/yyyy");
            txtotype.Text = DT.Rows[0]["owg_type"].ToString();           
            txttoamt.Text = DT.Rows[0]["to_amt"].ToString();
            txttype.Text = DT.Rows[0]["type"].ToString();
            TxtTName.Text = DT.Rows[0]["DESCRIPTION"].ToString();
        }
    }
    public void ENDN(bool TF)
    {
        txtaccno.Enabled = TF;
        txtamt.Enabled = TF;

        txtcharges.Enabled = TF;
        txteffect.Enabled = TF;
        txtotype.Enabled = TF;
        txttoamt.Enabled = TF;
        txttype.Enabled = TF;
    }
    public void ClearData()
    {
        txtaccno.Text = "";
        txtamt.Text = "";
        txtcharges.Text = "";
        txteffect.Text = "";
        txtotype.Text = "";
        txttoamt.Text = "";
        txttype.Text = "";
        TxtTName.Text = "";
        TxtAccName.Text = "";
    }
    protected void lnkAddNew_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "A";
        btnSubmit.Text = "Submit";
        ENDN(true);
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton objbtn = (LinkButton)sender;
        string strid = objbtn.CommandArgument;
        ViewState["strnumId"] = strid.ToString();
        ViewState["Flag"] = "ED";
        btnSubmit.Text = "Modify";
        CallGrid();
        ENDN(true);

    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton objbtn = (LinkButton)sender;
        string strid = objbtn.CommandArgument;
        ViewState["strnumId"] = strid.ToString();
        btnSubmit.Text = "Delete";
        CallGrid();
        ViewState["Flag"] = "D";
        ENDN(false);
    }
    protected void txtaccno_TextChanged(object sender, EventArgs e)
    {
        //TxtAccName.Text = OWG.GetPLHead(txtaccno.Text, Session["BRCD"].ToString());
    }
    protected void txttype_TextChanged(object sender, EventArgs e)
    {
        //TxtTName.Text = OWG.GetType(txttype.Text);
    }
}