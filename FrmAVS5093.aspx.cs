using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmAVS5093 : System.Web.UI.Page
{
    ClsCustomerMast CM = new ClsCustomerMast();
    ClsBindDropdown BD = new ClsBindDropdown();
    DataTable DT = new DataTable();
    ClsAVS5093 Obj = new ClsAVS5093();
    int ResInt = 0;
    string ResStr = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AutoCustName.ContextKey = Session["BRCD"].ToString();
            TxtCustno.Focus();
        }
    }
    public void ClearData()
    {
        TxtCustno.Text = "";
        TxtCustname.Text = "";
        TxtLoaneeAccName.Text = "";
        TxtLoaneeAccno.Text = "";
        TxtLoaneeSubgl.Text = "";
        TxtLoaneeSubglname.Text = "";
        TxtS_Custno.Text = "";
        TxtS_Custname.Text = "";
        TxtPriAmt.Text = "";
        TxtIntAmt.Text = "";
        TxtRecDept.Text = "";
        TxtRecDeptName.Text = "";
        TxtRecDiv.Text = "";
        TxtRecDivName.Text = "";
        Ddl_Status.SelectedValue = "0";
        GridLoanAccno.DataSource = null;
        GridSuretyDeduct.DataSource = null;
        GridSurities.DataSource = null;
        GridLoanAccno.DataBind();
        GridSuretyDeduct.DataBind();
        GridSurities.DataBind();
        Rdb_Type.SelectedValue = "I";
        TxtCustno.Focus();

    }
    public void CallCustDetails(string Custno)
    {
        try
        {
            DT = Obj.GetDepartment("GetDept", Custno, Session["BRCD"].ToString());
            if (DT.Rows.Count > 0)
            {
                TxtRecDiv.Text = DT.Rows[0]["RecDiv"].ToString();
                TxtRecDivName.Text = DT.Rows[0]["RecDivName"].ToString();
                TxtRecDept.Text = DT.Rows[0]["RecDept"].ToString();
                TxtRecDeptName.Text = DT.Rows[0]["RecDeptName"].ToString();

                ResInt = Obj.BindLoanAcc(GridLoanAccno, "Bind_LoanAcc", Custno, Session["BRCD"].ToString());
                ResInt = Obj.BindSuretiesDeduct(GridSuretyDeduct, "BindSureDeduct", TxtCustno.Text, Session["BRCD"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtCustno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = CM.GetStage(Session["BRCD"].ToString(), TxtCustno.Text);

            if (AT == "1003")
            {
                TxtCustname.Text = BD.GetCustName(TxtCustno.Text, Session["BRCD"].ToString());
                CallCustDetails(TxtCustno.Text);
                return;
            }
            else if (AT == "1004")
            {
                ClearData();
                WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                return;
            }
            else
            {
                WebMsgBox.Show("Invalid Customer No...!!", this.Page);
                TxtCustno.Text = "";
                TxtCustno.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GridLoanAccno_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label Lbl_Subgl = (Label)GridLoanAccno.Rows[e.RowIndex].FindControl("LblLn_Subglcode") as Label;
            Label Lbl_Subglname = (Label)GridLoanAccno.Rows[e.RowIndex].FindControl("LblLn_GlName") as Label;
            Label Lbl_Accno = (Label)GridLoanAccno.Rows[e.RowIndex].FindControl("LblLn_Accno") as Label;
            Label Lbl_CustName = (Label)GridLoanAccno.Rows[e.RowIndex].FindControl("LblLn_CustName") as Label;

            TxtLoaneeSubgl.Text = Lbl_Subgl.Text;
            TxtLoaneeSubglname.Text = Lbl_Subglname.Text;
            TxtLoaneeAccno.Text = Lbl_Accno.Text;
            TxtLoaneeAccName.Text = Lbl_CustName.Text;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtCustname_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string CustNo = TxtCustname.Text;
            string[] Cust = CustNo.ToString().Split('_');
            if (Cust.Length > 1)
            {
                TxtCustno.Text = Cust[1].ToString();
                TxtCustname.Text = Cust[0].ToString();
                CallCustDetails(TxtCustno.Text);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnSubmit1_Click(object sender, EventArgs e)
    {
        try
        {
            ResInt = Obj.BindSureties(GridSurities, "Bind_Sure", TxtCustno.Text, TxtLoaneeSubgl.Text, TxtLoaneeAccno.Text, Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GridSurities_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label Lbls_S_Custno = (Label)GridSurities.Rows[e.RowIndex].FindControl("LblLns_Surety_Custno") as Label;
            Label Lbls_S_CustName = (Label)GridSurities.Rows[e.RowIndex].FindControl("Lbllns_Surety_Custname") as Label;
            Label Lbls_S_MemNo = (Label)GridSurities.Rows[e.RowIndex].FindControl("LblLns_Surety_MemNo") as Label;

            TxtS_Custno.Text = Lbls_S_Custno.Text;
            TxtS_Custname.Text = Lbls_S_CustName.Text;
            TxtS_MemNo.Text = Lbls_S_MemNo.Text;
            
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
            if (Rdb_Type.SelectedValue == "I") //Create
            {
                ResInt = Obj.FnInsert_Sure("Insert_Sure", Session["Brcd"].ToString(), TxtRecDiv.Text, TxtRecDept.Text, TxtCustno.Text, TxtCustname.Text, TxtLoaneeSubgl.Text, TxtLoaneeAccno.Text, TxtS_Custno.Text,TxtS_MemNo.Text, TxtS_Custname.Text, TxtPriAmt.Text, TxtIntAmt.Text == "" ? "0" : TxtIntAmt.Text, Session["Entrydate"].ToString(), Ddl_Status.SelectedValue, Session["MID"].ToString(), Session["EntryDate"].ToString());
                if (ResInt > 0)
                {
                    WebMsgBox.Show("From Surety added Successfully....!", this.Page);
                    ClearData();
                    ResInt = Obj.BindSuretiesDeduct(GridSuretyDeduct, "BindSureDeduct", TxtCustno.Text, Session["BRCD"].ToString());

                }
                else
                {
                    WebMsgBox.Show("From Surety already added....!", this.Page);
                }
            }
            else if (Rdb_Type.SelectedValue == "M")// Modify
            {
                ResInt = Obj.FnManu_ModSurety("Modify_Sure", ViewState["Deduct_ID"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Ddl_Status.SelectedValue.ToString(), TxtPriAmt.Text, TxtIntAmt.Text == "" ? "0" : TxtIntAmt.Text);
                if (ResInt > 0)
                {
                    WebMsgBox.Show("From Surety modified Successfully....!", this.Page);
                    ClearData();
                    ResInt = Obj.BindSuretiesDeduct(GridSuretyDeduct, "BindSureDeduct", TxtCustno.Text, Session["BRCD"].ToString());

                }
                else
                {
                    WebMsgBox.Show("Modify Operation failed....!", this.Page);
                }
            }
            else if (Rdb_Type.SelectedValue == "C")// Delete 
            {
                ResInt = Obj.FnManu_DelSurety("Delete_Sure",ViewState["Deduct_ID"].ToString(),Session["MID"].ToString(),Session["EntryDate"].ToString());
                if (ResInt > 0)
                {
                    WebMsgBox.Show("From Surety cancled Successfully....!", this.Page);
                    ClearData();
                    ResInt = Obj.BindSuretiesDeduct(GridSuretyDeduct, "BindSureDeduct", TxtCustno.Text, Session["BRCD"].ToString());

                }
                else
                {
                    WebMsgBox.Show("Cancel Operation failed....!", this.Page);
                }
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GridSuretyDeduct_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label Lblss_ID = (Label)GridSuretyDeduct.Rows[e.RowIndex].FindControl("Lbllnsd_Id") as Label;
            Label Lblss_S_Custno = (Label)GridSuretyDeduct.Rows[e.RowIndex].FindControl("LblLnsd_Surety_Custno") as Label;
            Label Lblss_S_CustName = (Label)GridSuretyDeduct.Rows[e.RowIndex].FindControl("Lbllnsd_Surety_Custname") as Label;
            Label Lblss_PriAmt = (Label)GridSuretyDeduct.Rows[e.RowIndex].FindControl("LblLnsd_PriAmt") as Label;
            Label Lblss_IntrAmt = (Label)GridSuretyDeduct.Rows[e.RowIndex].FindControl("LblLnsd_Intramt") as Label;
            Label Lblss_Status = (Label)GridSuretyDeduct.Rows[e.RowIndex].FindControl("Lbllnsd_Remark") as Label;
            Label Lblss_MemNo = (Label)GridSuretyDeduct.Rows[e.RowIndex].FindControl("Lbllnsd_Surety_S_Memno") as Label;

            ViewState["Deduct_ID"] = Lblss_ID.Text;
            TxtS_Custno.Text = Lblss_S_Custno.Text;
            TxtS_Custname.Text = Lblss_S_CustName.Text;
            TxtPriAmt.Text = Lblss_PriAmt.Text;
            TxtIntAmt.Text = Lblss_IntrAmt.Text;
            TxtS_MemNo.Text = Lblss_MemNo.Text;
            Ddl_Status.SelectedValue = Lblss_Status.Text == "Started" ? "1" : "2";

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}