using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmChangeCust : System.Web.UI.Page
{
    ClsTest CT = new ClsTest();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
        DataTable dt = new DataTable();
        dt=CT.GetLast(Session["BRCD"].ToString());
        hdnLast.Value = dt.Rows[0]["No"].ToString();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = CT.getdata(txtCust.Text);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
           CT.UpdateRecords(dt.Rows[i]["AT"].ToString(),dt.Rows[i]["CustName"].ToString(),dt.Rows[i]["CustNo"].ToString(),Convert.ToInt32(hdnLast.Value),dt.Rows[i]["AC"].ToString());
           DataTable dt1 = new DataTable();
           dt1 = CT.GetLast(Session["BRCD"].ToString());
           hdnLast.Value = dt1.Rows[0]["No"].ToString();
        }
           // WebMsgBox.Show("No Record Found!!!", this.Page);
        BindGrid();
    }
    public void BindGrid()
    {
        DataTable dt = new DataTable();
        dt = CT.getdata(txtCust.Text);
        griddetails.DataSource = dt;
        griddetails.DataBind();
        if (Convert.ToInt32(dt.Rows.Count) == 0 || dt.Rows.Count == null)
        {
            WebMsgBox.Show("No Record Found!!!", this.Page);
        }
    }
}