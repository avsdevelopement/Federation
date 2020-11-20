using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmMenuPermission : System.Web.UI.Page
{
    ClsMenuPermission MP = new ClsMenuPermission();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }

        GetMenuHead();
    }

    public void GetMenuHead()
    {
        DataTable rs = new DataTable();
        rs = MP.GetHead("SELECT [MenuId], [ParentMenuId], [MenuTitle], [PageDesc], [PageUrl],[CssFont] FROM [AVS5016] WHERE [ParentMenuId] = '0'");

        ClearBalanceSheetView();

        InitBalanceSheetView(rs);
    }

    private void InitBalanceSheetView(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode tnParent = new TreeNode();

                    tnParent.Text = dr["MenuTitle"].ToString();
                    tnParent.Value = dr["MenuId"].ToString();
                    tnParent.PopulateOnDemand = true;
                    tnParent.ToolTip = "Click to get Child";
                    tnParent.SelectAction = TreeNodeSelectAction.SelectExpand;
                    tnParent.Expand();
                    tnParent.Selected = true;

                    string s = tnParent.Text;
                    string fs = "<span style=\"color: #CC0000\">" + s + "</span>";
                    tnParent.Text = fs;

                    TreeView1.Nodes.Add(tnParent);
                    FillSubChild(tnParent, tnParent.Value);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void FillSubChild(TreeNode parent, string ParentId)
    {

        DataTable rs1 = new DataTable();
        rs1 = MP.GetHead("SELECT [MenuId], [ParentMenuId], [MenuTitle], [PageDesc], [PageUrl],[CssFont] FROM [AVS5016] WHERE ParentMenuId = '" + ParentId + "'");

        parent.ChildNodes.Clear();

        foreach (DataRow dr in rs1.Rows)
        {
            TreeNode Subchild = new TreeNode();
            Subchild.Text = dr["MenuTitle"].ToString().Trim();
            Subchild.Value = dr["MenuId"].ToString().Trim();
            if (Subchild.ChildNodes.Count == 0)
            {
                Subchild.PopulateOnDemand = true;
            }

            Subchild.ToolTip = "Click to get Child";
            Subchild.SelectAction = TreeNodeSelectAction.SelectExpand;
            Subchild.Expand();
            Subchild.Selected = true;

            string s = Subchild.Text;
            string fs = "<span style=\"color: #0000FF\">" + s + "</span>";
            Subchild.Text = fs;

            TreeView1.Nodes.Add(Subchild);
            FillSubChild(Subchild, Subchild.Value);
        }
    }

    private void ClearBalanceSheetView()
    {
        try
        {
            if (TreeView1.Nodes.Count > 0)
            {
                TreeView1.Nodes.Clear();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {

    }

    protected void Exit_Click(object sender, EventArgs e)
    {

    }
}