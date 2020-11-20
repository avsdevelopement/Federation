using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmCreateSlab : System.Web.UI.Page
{
    ClsCreateSlab CS = new ClsCreateSlab();
    int resultint;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            divInsert.Visible = false;
            txtTotalSlab.Focus();
        }
    }

    protected void txtTotalSlab_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtTotalSlab.Text = txtTotalSlab.Text.ToString() == "" ? "0" : txtTotalSlab.Text.ToString();

            if(Convert.ToDouble(txtTotalSlab.Text.ToString()) < 6)
            {
                EmptyGridBind(Convert.ToInt32(txtTotalSlab.Text.Trim().ToString()));
            }
            else
            {
                txtTotalSlab.Focus();
                grdInsert.DataSource = null;
                grdInsert.DataBind();
                WebMsgBox.Show("Enter Below 5 slab ..!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void EmptyGridBind(int i)
    {
        try
        {
            if (i >= 5)
            i = 5;

            grdInsert.DataSource = Enumerable.Range(1, i).Select(a => new
            {
                ID = a,
                Name = String.Format("Test Name {0}", a)
            });

            grdInsert.DataBind();
            divInsert.Visible = true;

            txtTotalSlab.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int j = 0;

            CS.DeleteData(Session["MID"].ToString());

            foreach (GridViewRow gvRow in this.grdInsert.Rows)
            {
                int SrNo = Convert.ToInt32(((TextBox)gvRow.FindControl("txtSrNo")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtSrNo")).Text);
                int FromSlab = Convert.ToInt32(((TextBox)gvRow.FindControl("txtFromSlab")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtFromSlab")).Text);
                int ToSlab = Convert.ToInt32(((TextBox)gvRow.FindControl("txtToSlab")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("txtToSlab")).Text);

                if (ToSlab != 0)
                {
                    resultint = CS.InsertData(Session["MID"].ToString(), FromSlab.ToString(), ToSlab.ToString(), SrNo.ToString());
                }
                else
                {
                    return;
                }

                j = j + 1;
            }

            if (resultint > 0)
            {
                divInsert.Visible = false;
                grdInsert.DataSource = null;
                grdInsert.DataBind();
                EmptyGridBind(0);

                txtTotalSlab.Text = "";
                lblMessage.Text = "Slab Saved Successfully...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmPeriodWiseClassOfOD.aspx", true);
    }
}