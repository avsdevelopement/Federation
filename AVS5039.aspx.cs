using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AVS5039 : System.Web.UI.Page
{
    ClsDivDeptAdd DP = new ClsDivDeptAdd();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string MaxDivNo = DP.GetDiv();
            txtdiv.Text = MaxDivNo;
            string MAxDEptNo = DP.GetDeptId( MaxDivNo);
            txtdept.Text = MAxDEptNo;
            btnModify.Visible = false;
            txtdesc.Focus();
            BtnNew.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int Result = DP.SaveData(txtdiv.Text, txtdept.Text, txtdesc.Text, txtRemarks.Text, txtDeleteLag.Text, txttempid.Text,Session["BRCD"].ToString());
        if (Result > 0)
        {
            ClearAll();
            string MaxDivNo = DP.GetDiv();
            txtdiv.Text = MaxDivNo;
            string MAxDEptNo = DP.GetDeptId(MaxDivNo);
            txtdept.Text = MAxDEptNo;
            txtdesc.Focus();
            lblMessage.Text = " Division Created Successfully...!";
            ModalPopup.Show(this.Page);
        }
        else
        {
            lblMessage.Text = "Recovery Div Create Failed...!";
            ModalPopup.Show(this.Page);
        }
    }
    protected void txtdiv_TextChanged(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
            string MAxDEptNo = DP.GetDeptId( txtdiv.Text);
            txtdept.Text = MAxDEptNo;
            txtdesc.Focus();
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        
    }
    public void clearall()
    {
        txtdiv.Text = "";
        txtdept.Text = "";
        txtdesc.Text = "";
        txtRemarks.Text = "";
        txtDeleteLag.Text = "";
        txttempid.Text = "";
    }
    public void BindGrid()
    {
        DP.bindgrid(txtdiv.Text, Grdcrdrdetailes);

    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        string MaxDivNo = DP.GetDiv();
        txtdiv.Text = MaxDivNo;
        string MAxDEptNo = DP.GetDeptId( MaxDivNo);
        txtdept.Text = MAxDEptNo;
      }
    protected void lnkedit_Click(object sender, EventArgs e)
    {
        LinkButton lnkedit = (LinkButton)sender;
        string str = lnkedit.CommandArgument.ToString();
        string[] ARR = str.Split(',');
        ViewState["RECDIV"] = ARR[0].ToString();
        ViewState["RECCODE"] = ARR[1].ToString();
        btnModify.Visible = true;
        
       
    }
    protected void Grdcrdrdetailes_SelectedIndexChanged(object sender, EventArgs e)
    {
        data();
    }
    public void data()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = DP.showdata( ViewState["RECDIV"].ToString(), ViewState["RECCODE"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtdiv.Text = DT.Rows[0]["recdiv"].ToString();
                txtdept.Text = DT.Rows[0]["reccode"].ToString();
                txtdesc.Text = DT.Rows[0]["DESCR"].ToString();
                txtRemarks.Text = DT.Rows[0]["REMARK"].ToString();
                txtDeleteLag.Text = DT.Rows[0]["DELETELAG"].ToString();
                txttempid.Text = DT.Rows[0]["TEMPID"].ToString();
               

            }
            else
            {
                WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        int Result = DP.ModifyData(txtdiv.Text, txtdept.Text, txtdesc.Text, txtRemarks.Text, txtDeleteLag.Text, txttempid.Text);
        if (Result > 0)
        {
            ClearAll();
            string MaxDivNo = DP.GetDiv();
            txtdiv.Text = MaxDivNo;
            string MAxDEptNo = DP.GetDeptId(MaxDivNo);
            txtdept.Text = MAxDEptNo;
            txtdesc.Focus();
          
            lblMessage.Text = "Recovery Div Modify Successfully...!";
            ModalPopup.Show(this.Page);
        }
        else
        {
            lblMessage.Text = "Recovery Div Create Failed...!";
            ModalPopup.Show(this.Page);
        }

    }
    public void ClearAll()
    {
        txtdiv.Text = "";
        txtdept.Text = "";
        txtdesc.Text = "";
        txtRemarks.Text = "";
        txtDeleteLag.Text = "";
        txttempid.Text = "";
    }
  
    protected void Grdcrdrdetailes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grdcrdrdetailes.PageIndex = e.NewPageIndex;
    }
}