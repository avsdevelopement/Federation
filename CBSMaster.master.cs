using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
public partial class CBSMaster : System.Web.UI.MasterPage
{
    DbConnection conn1 = new DbConnection();
    DataTable Menus = new DataTable();
    ClsBlankQ CB = new ClsBlankQ();
    DataTable DT = new DataTable();
    ClsCommon CM = new ClsCommon();
    ClsLogin LG = new ClsLogin();
    string sql = "", sResult = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();

            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1d);
            Response.Expires = -1500;
            Response.CacheControl = "no-cache";
            Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!this.IsPostBack)
            {
                PageLoad();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void PageLoad()
    {
        try
        {
            hdnSession.Value = Session["SessionTimeout"].ToString();
            lblUserName.Text = string.IsNullOrEmpty(Session["UserName"].ToString()) == true ? "0" : Session["UserName"].ToString();
            TxtWorkigDate.Text = LG.openDay(Session["BRCD"].ToString());
            Session["EntryDate"] = LG.openDay(Session["BRCD"].ToString());
            
            DT = LG.GetBankNameATTACH(Session["BRCD"].ToString());
            if (DT.Rows.Count > 0)
            {
                string BB = DT.Rows[0]["BankName"].ToString();
                string[] BRB = BB.Split('_');
                lblbankName.Text = BRB[0].ToString() + " - " + BRB[1].ToString();
                Session["BName"] = BRB[1].ToString();
                Session["BankName"] = BRB[0].ToString(); //Amruta 09/05/2018
            }

            if (Session.Contents.Count == 0)
                LG.UpdateLoginsts(Session["UID"].ToString(), Session["PWD"].ToString(), "0", Session["BRCD"].ToString());
            this.BindMenu();

            Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout)) + ";URL=FrmLogin.aspx?LG=" + Session["LOGINCODE"] + "&BD=" + Session["BRCD"].ToString() + "&UG=" + Session["UGRP"].ToString());
            Session["BNKCDE"] = CB.GetBnkCde(Session["BName"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void rptMenu_OnItemBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    if (Menus != null)
                    {
                        DataRowView drv = e.Item.DataItem as DataRowView;
                        string ID = drv["MenuId"].ToString();

                        string Title = drv["MenuTitle"].ToString();
                        DataRow[] rows = Menus.Select("ParentMenuId=" + ID);
                        if (rows.Length > 0)
                        {

                            StringBuilder sb = new StringBuilder();
                            sb.Append("<ul id='" + Title + "' class='sub-menu collapse'>");
                            foreach (var item in rows)
                            {
                                string parentId = item["MenuId"].ToString();

                                string parentTitle = item["MenuTitle"].ToString();

                                DataRow[] parentRow = Menus.Select("ParentMenuId=" + parentId);

                                if (parentRow.Count() > 0)
                                {
                                    sb.Append("<li data-toggle='collapse' style='" + item["CssFont"] + "'  data-target='#" + parentTitle + "' class='collapsed'><a href='" + item["PageUrl"] + "'>" + item["MenuTitle"] + "<span class='arrow'></span></a>");
                                    sb.Append("</li>");
                                }
                                else
                                {
                                    sb.Append("<li><a style='" + item["CssFont"] + "' href='" + item["PageUrl"] + "'>" + item["MenuTitle"] + "</a>");
                                    sb.Append("</li>");
                                }
                                sb = CreateChild(sb, parentId, parentTitle, parentRow);
                            }
                            sb.Append("</ul>");
                            (e.Item.FindControl("ltrlSubMenu") as Literal).Text = sb.ToString();
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    private StringBuilder CreateChild(StringBuilder sb, string parentId, string parentTitle, DataRow[] parentRows)
    {
        try
        {
            if (parentRows.Length > 0)
            {
                sb.Append("<ul id='" + parentTitle + "' class='sub-menu collapse'>");
                foreach (var item in parentRows)
                {
                    string childId = item["MenuId"].ToString();
                    string childTitle = item["MenuTitle"].ToString();
                    DataRow[] childRow = Menus.Select("ParentMenuId=" + childId);

                    if (childRow.Count() > 0)
                    {
                        sb.Append("<li data-toggle='collapse' style='" + item["CssFont"] + "' data-target='#" + childTitle + "' class='collapsed'><a href='" + item["PageUrl"] + "'>" + item["MenuTitle"] + "<span class='arrow'></span></a>");
                        sb.Append("</li>");
                    }
                    else
                    {
                        sb.Append("<li><a href='" + item["PageUrl"] + "' style='" + item["CssFont"] + "' >" + item["MenuTitle"] + "</a>");
                        sb.Append("</li>");
                    }
                    CreateChild(sb, childId, childTitle, childRow);
                }
                sb.Append("</ul>");
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sb;
    }

    private void BindMenu()
    {
        try
        {
            if (Session["UGRP"].ToString() == "99")
                Menus = GetData("SELECT [MenuId], [ParentMenuId], [MenuTitle], [PageDesc], [PageUrl],[CssFont] FROM [AVS5016] WHERE [STATUS] IN (1," + Session["UGRP"].ToString() + ")");
            else
                Menus = GetData("SELECT [MenuId], [ParentMenuId], [MenuTitle], [PageDesc], [PageUrl],[CssFont] FROM [AVS5016] WHERE [STATUS]=1");
            DataView view = new DataView(Menus);
            view.RowFilter = "ParentMenuId=0";
            this.rptCategories.DataSource = view;
            this.rptCategories.DataBind();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    private DataTable GetData(string query)
    {
        DT = new DataTable();
        try
        {
            string constr = conn1.DbName();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(DT);
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    //Added By Amol ON 20170104 for Search Menu with Specific Keyword
    protected void btnSearch_ServerClick(object sender, EventArgs e)
    {
        try
        {
            if (txtSearch.Value.ToString() == "")
                this.BindMenu();
            else
                this.BindMenuSearch();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    private void BindMenuSearch()
    {
        try
        {
            sql = "Select [MenuId], [ParentMenuId], [MenuTitle], [PageDesc], [PageUrl], [CssFont] From [AVS5016] " +
                  "Where [PageURL] Not In ('Javascript:;', '', '#') And [MenuTitle] like '%" + txtSearch.Value + "%' ";
            Menus = GetData(sql);
            DataView view = new DataView(Menus);
            //view.RowFilter = "ParentMenuId=0";
            this.rptCategories.DataSource = view;
            this.rptCategories.DataBind();
            txtSearch.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindPath()
    {
        try
        {
            string FName = Path.GetFileName(Request.Path);

            if (FName != null)
            {
                Lbl_Path.Text = LG.GetPath(FName);
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public int ForceLogout(string BRCD, string UGRP, string LC)
    {
        int TT = 0;
        try
        {
            if (UGRP != "1")
            {
                int Res = LG.RealizedUser(HttpContext.Current.Session["LOGINCODE"].ToString(), HttpContext.Current.Session["BRCD"].ToString());
                if (Res > 0)
                {
                    TT = 1;
                }
            }
            else
            {
                TT = 1;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return TT;
    }

    protected void Lnk_RDM_Click(object sender, EventArgs e)
    {
        try
        {
            string YN = CM.GetRDMYN();
            string url = "";
            if (YN != null)
            {
                string[] LB = YN.Split('_');
                if (LB[0] == "Y" && LB[1] == "1008")// TZssspm Palghar
                {
                    url = HttpContext.Current.Request.Url.AbsoluteUri;
                    Session["CBSBackLink"] = url.ToString();
                    string URL = "http://103.228.152.231/RDMTZSSSPM/FrmBlank.aspx?MID=" + Session["MID"].ToString() + "&LGC=" + Session["LOGINCODE"].ToString() + "&UNM=" + Session["UserName"].ToString() + "&BC=" + Session["BRCD"].ToString() + "&UGP=" + Session["UGRP"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&LL=" + Session["CBSBackLink"].ToString();
                    //string URL = "http://192.168.1.15/RDM/FrmBlank.aspx?MID=" + Session["MID"].ToString() + "&LGC=" + Session["LOGINCODE"].ToString() + "&UNM=" + Session["UserName"].ToString() + "&BC=" + Session["BRCD"].ToString() + "&UGP=" + Session["UGRP"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&LL=" + Session["CBSBackLink"].ToString();
                    HttpContext.Current.Response.Redirect(URL, false);
                }
                else if (LB[0] == "Y" && LB[1] == "1009")//MSEB Kalyan
                {
                    url = HttpContext.Current.Request.Url.AbsoluteUri;
                    Session["CBSBackLink"] = url.ToString();

                    string URL = "http://103.228.152.231/RDMMseb/FrmBlank.aspx?MID=" + Session["MID"].ToString() + "&LGC=" + Session["LOGINCODE"].ToString() + "&UNM=" + Session["UserName"].ToString() + "&BC=" + Session["BRCD"].ToString() + "&UGP=" + Session["UGRP"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&LL=" + Session["CBSBackLink"].ToString();
                    // string URL = "http://114.79.177.218:81/RDMMSEB/FrmBlank.aspx?MID=" + Session["MID"].ToString() + "&LGC=" + Session["LOGINCODE"].ToString() + "&UNM=" + Session["UserName"].ToString() + "&BC=" + Session["BRCD"].ToString() + "&UGP=" + Session["UGRP"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&LL=" + Session["CBSBackLink"].ToString();
                    HttpContext.Current.Response.Redirect(URL, false);
                }
                else
                {
                    WebMsgBox.Show("Access restricted to this module.....Contact to Support!", this.Page);
                }
            }
            else
            {
                WebMsgBox.Show("Access restricted to this module.....Contact to Support!", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}
