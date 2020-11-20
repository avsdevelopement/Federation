using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class FrmRecoveryMaster : System.Web.UI.Page
{
    ClsRecoveryMaster RM = new ClsRecoveryMaster();
    Cls_RecoBindDropdown BD = new Cls_RecoBindDropdown();
    DbConnection Conn = new DbConnection();
    int Res = 0;
    string MM, YY, EDT, STRRes;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBr();
            TxtCustno.Focus();
        }
    }
   
    public void BindBr()
    {
        BD.Ddl = ddlBrCode;
        BD.FnBL_BindDropDown(BD);
        ddlBrCode.SelectedValue = Session["BRCD"].ToString();// Amruta 
    }
    protected void Grd_MasterCust_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //Label lb = (Label)Grd_MasterCust.Rows[e.RowIndex].FindControl("Lbl_ID");

            TextBox TxtGCustno = (TextBox)Grd_MasterCust.Rows[e.RowIndex].FindControl("TxtCustNo") as TextBox;
            TextBox TxtMM = (TextBox)Grd_MasterCust.Rows[e.RowIndex].FindControl("TxtMonth") as TextBox;
            TextBox TXtYY = (TextBox)Grd_MasterCust.Rows[e.RowIndex].FindControl("TxtYear") as TextBox;
            EDT = Session["EntryDate"].ToString();

            TextBox TxtStatus = (TextBox)Grd_MasterCust.Rows[e.RowIndex].FindControl("TxtStatus") as TextBox;

            if (Rdb_RecType.SelectedValue == "S")
            {
                RM.Flag = "MODSEND";
            }
            else
            {
                RM.Flag = "MODPOST";
            }
            RM.Brcd = ddlBrCode.SelectedValue.ToString();
            RM.Mid = Session["MID"].ToString();
            RM.Edt = EDT;
            RM.Custno = TxtCustno.Text;
            RM.Status = TxtStatus.Text;
            RM.MM = TxtMM.Text;
            RM.YY = TXtYY.Text;
            RM.Custno = TxtGCustno.Text;
            Res = RM.FnBL_UpdateData(RM);
            if (Res > 0)
            {
                WebMsgBox.Show("Updated Succesfully......!", this.Page);
                BindGrid(Rdb_RecType.SelectedValue == "S" ? "ShowSEND" : "ShowPOST");
            }
            else
            {
                WebMsgBox.Show("Error occured while updating (already authorized)......!", this.Page);

            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGrid(string FL)
    {
        try
        {
            RM.Flag = FL;
            RM.Custno = TxtCustno.Text;
            RM.Brcd = Session["BRCD"].ToString();
            RM.GD = Grd_MasterCust;
            Res = RM.FnBL_BindGrid(RM);
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
            BindGrid(Rdb_RecType.SelectedValue == "S" ? "ShowSEND" : "ShowPOST");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //BindGrid(Rdb_RecType.SelectedValue == "S" ? "ShowSEND" : "ShowPOST");
            try
            {
                foreach (GridViewRow GR in Grd_MasterCust.Rows)
                {
                    TextBox TxtGCustno = (TextBox)Grd_MasterCust.Rows[GR.RowIndex].FindControl("TxtCustNo") as TextBox;
                    TextBox TxtMM = (TextBox)Grd_MasterCust.Rows[GR.RowIndex].FindControl("TxtMonth") as TextBox;
                    TextBox TXtYY = (TextBox)Grd_MasterCust.Rows[GR.RowIndex].FindControl("TxtYear") as TextBox;
                    EDT = Session["EntryDate"].ToString();

                    TextBox TxtStatus = (TextBox)Grd_MasterCust.Rows[GR.RowIndex].FindControl("TxtStatus") as TextBox;

                    if (Rdb_RecType.SelectedValue == "S")
                    {
                        RM.Flag = "MODSEND";
                    }
                    else
                    {
                        RM.Flag = "MODPOST";
                    }
                    RM.Brcd = ddlBrCode.SelectedValue.ToString();
                    RM.Mid = Session["MID"].ToString();
                    RM.Edt = EDT;
                    RM.Custno = TxtCustno.Text;
                    RM.Status = TxtStatus.Text;
                    RM.MM = TxtMM.Text;
                    RM.YY = TXtYY.Text;
                    RM.Custno = TxtGCustno.Text;
                    Res = RM.FnBL_UpdateData(RM);
                    
                }
                if (Res > 0)
                {
                    WebMsgBox.Show("Updated Succesfully......!", this.Page);
                    BindGrid(Rdb_RecType.SelectedValue == "S" ? "ShowSEND" : "ShowPOST");
                }
                 
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}