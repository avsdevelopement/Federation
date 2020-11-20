using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDocDetails : System.Web.UI.Page
{
    ClsDocDetails DOC = new ClsDocDetails();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            dt = DOC.GetDocDetails(txtRef.Text);
            if (dt.Rows.Count > 0)
            {
                byte[] bytes1 = null;
                byte[] bytes2 = null;
                byte[] bytes3 = null;
                byte[] bytes4 = null;
                byte[] bytes5 = null;
                byte[] bytes6 = null;
                if (dt.Rows[0]["PHOTO_IMG"].ToString() != "NA")
                {
                    //bytes1 = (byte[])dt.Rows[0]["PHOTO_IMG"];
                    //string base64String = Convert.ToBase64String(dt.Rows[0]["PHOTO_IMG"]);
                    img1.Src = "data:image/tif;base64," + dt.Rows[0]["PHOTO_IMG"];
                }
                if (dt.Rows[0]["SIGN"].ToString() != "NA")
                {
                    //bytes2 = (byte[])dt.Rows[0]["SIGN"];
                    //string base64String = Convert.ToBase64String(bytes2, 0, bytes2.Length);
                    img2.Src = "data:image/tif;base64," + dt.Rows[0]["SIGN"];
                }
                if (dt.Rows[0]["PANCARD"].ToString() != "NA")
                {
                    //bytes3 = (byte[])dt.Rows[0]["PANCARD"];
                    //string base64String = Convert.ToBase64String(bytes3, 0, bytes3.Length);
                    img3.Src = "data:image/tif;base64," + dt.Rows[0]["PANCARD"];
                }
                if (dt.Rows[0]["AADHARCARD"].ToString() != "NA")
                {
                    //bytes4 = (byte[])dt.Rows[0]["AADHARCARD"];
                    //string base64String = Convert.ToBase64String(bytes4, 0, bytes4.Length);
                    img4.Src = "data:image/tif;base64," + dt.Rows[0]["AADHARCARD"];
                }
                if (dt.Rows[0]["OTHERONE"].ToString() != "NA")
                {
                    //bytes5 = (byte[])dt.Rows[0]["OTHERONE"];
                    //string base64String = Convert.ToBase64String(bytes5, 0, bytes5.Length);
                    img5.Src = "data:image/tif;base64," + dt.Rows[0]["OTHERONE"];
                }
                if (dt.Rows[0]["OTHERTWO"].ToString() != "NA")
                {
                    //bytes6 = (byte[])dt.Rows[0]["OTHERTWO"];
                    //string base64String = Convert.ToBase64String(bytes6, 0, bytes6.Length);
                    img6.Src = "data:image/tif;base64," + dt.Rows[0]["OTHERTWO"];
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}