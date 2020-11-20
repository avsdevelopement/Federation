using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AVSCore.CommonUtility;


public partial class FrmTextReport : System.Web.UI.Page
{
    P009100 P9 = new P009100();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Report_Click(object sender, EventArgs e)
    {
        P9 = new P009100();
    }    
}