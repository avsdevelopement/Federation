using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAvs51208 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int result = 0;
    string sroname = "", AC_Status = "", results = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        BD.BindAccDemandD(DdlMODE: ddlstatus);
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {

    }
    protected void lnkModify_Click(object sender, EventArgs e)
    {

    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {

    }
    protected void lnkAuthorized_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {

    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {

    }
    protected void txtCaseNO_TextChanged(object sender, EventArgs e)
    {

    }
    //protected void txtMember_TextChanged(object sender, EventArgs e)
    //{
    //    txtMemberName.Text = SRO.GetMemberID(txtMember.Text);
    //}
    //protected void txtMemberName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string custno = txtMemberName.Text;
    //        string[] CT = custno.Split('_');
    //        if (CT.Length > 0)
    //        {
    //            txtMemberName.Text = CT[0].ToString();
    //            txtMember.Text = CT[1].ToString();


    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
}