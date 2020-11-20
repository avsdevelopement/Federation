using System;
using Microsoft.VisualBasic;
using System.Text;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

/// <summary>
/// Summary description for ModalPopup
/// </summary>
public class ModalPopup
{
    public ModalPopup()
    {
    }
    public static void Show(System.Web.UI.Page Page)
    {
        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append(@"<script type='text/javascript'>");
        //sb.Append("$('#alertModal').find('.modal-body p').text(asdasdssadadsasad);");
        //sb.Append("$('#alertModal').modal('show');");
        //sb.Append(@"</script>");
        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "AddShowModalScript", sb.ToString(), false);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#alertModal').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "AddShowModalScript", sb.ToString(), false);
    }
}