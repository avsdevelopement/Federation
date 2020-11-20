using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmParameterReg : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsParaReg cparg = new ClsParaReg();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        }
    }
    protected void Btnaddnew_Click(object sender, EventArgs e)
    {
        string listf = txtlistfield.Text.ToString();
        string listv = txtlistvalue.Text.ToString();
        cparg.paraaddnew(listf, listv, Session["BRCD"].ToString(), Session["MID"].ToString());
        WebMsgBox.Show("Record add successfully....!!!!!", this.Page);
        BindGrid();
        Clear();
    }

    public void Clear()
    {
        txtlistfield.Text = "";
        txtlistvalue.Text = "";
    }

    //public void bindgrid()
    //{
    //    string sql = "select * from parameter where LISTFIELD='"+txtlistfield.Text+"' and LISTVALUE='"+txtlistvalue.Text+"'";

    //}

    public void BindGrid()
    {
        int RS = BindContact(grdParaReg);
    }
    public int BindContact(GridView Gview)
    {
        //string sql = "select * from parameter where LISTFIELD='"+txtlistfield.Text+"' and LISTVALUE='"+txtlistvalue.Text+"' AND STAGE <>'1004' ";
        string sql = "select * from parameter where brcd='1' AND STAGE <>'1004' ";
        int Result = conn.sBindGrid(Gview, sql);
        return Result;
    }
}