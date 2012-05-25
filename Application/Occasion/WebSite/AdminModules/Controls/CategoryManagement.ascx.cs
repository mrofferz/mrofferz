using System;
using System.Collections.Generic;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using DAL.OperationsClasses;
using EntityLayer.Entities;
using Common.StringsClasses;
using Common.UtilityClasses;
using Resources;
using System.Globalization;

public partial class CategoryManagement : BaseControl
{
    #region member variables

    private CategoryDAL categoryOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            categoryOperator = new CategoryDAL();

            if (!IsPostBack)
            {
                BindTree();
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "AdminDefault")));
        }
    }

    protected void treeCategories_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            if (treeCategories.SelectedNode != null)
            {
                if (treeCategories.SelectedNode.Depth == 0)
                {
                    txtNameAr.Text = string.Empty;
                    txtNameEn.Text = string.Empty;
                    chkCanHaveOffers.Checked = false;

                    btnAdd.Enabled = true;
                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;
                }
                else
                {
                    Category info = categoryOperator.SelectByID(Convert.ToInt32(treeCategories.SelectedNode.Value), null);
                    if (info != null)
                    {
                        txtNameAr.Text = info.NameAr;
                        txtNameEn.Text = info.NameEn;
                        chkCanHaveOffers.Checked = info.CanHaveOffers;
                    }
                    btnAdd.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "AdminDefault")));
        }
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (treeCategories.SelectedNode != null)
                {
                    Category info = new Category();

                    info.NameAr = txtNameAr.Text.Trim();
                    info.NameEn = txtNameEn.Text.Trim();
                    info.CanHaveOffers = chkCanHaveOffers.Checked;

                    if (treeCategories.SelectedNode.Depth == 0)
                    {
                        info.ParentID = null;
                    }
                    else
                    {
                        info.ParentID = Convert.ToInt32(treeCategories.SelectedNode.Value);
                    }

                    categoryOperator.Add(info);

                    BindTree();
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "AdminDefault")));
        }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (treeCategories.SelectedNode != null)
                {
                    if (treeCategories.SelectedNode.Depth > 0)
                    {
                        DeleteChildren(treeCategories.SelectedNode);
                    }
                    else
                    {
                        if (treeCategories.Nodes[0].ChildNodes != null && treeCategories.Nodes[0].ChildNodes.Count > 0)
                        {
                            foreach (TreeNode node in treeCategories.Nodes[0].ChildNodes)
                            {
                                DeleteChildren(node);
                            }
                        }
                    }
                }
                txtNameAr.Text = string.Empty;
                txtNameEn.Text = string.Empty;
                chkCanHaveOffers.Checked = false;
                BindTree();
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "AdminDefault")));
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (treeCategories.SelectedNode != null)
                {
                    if (treeCategories.SelectedNode.Depth > 0)
                    {
                        Category info = new Category();
                        info.NameAr = txtNameAr.Text.Trim();
                        info.NameEn = txtNameEn.Text.Trim();
                        info.CanHaveOffers = chkCanHaveOffers.Checked;
                        info.ID = Convert.ToInt32(treeCategories.SelectedNode.Value);
                        if (treeCategories.SelectedNode.Parent.Value != "Root")
                            info.ParentID = Convert.ToInt32(treeCategories.SelectedNode.Parent.Value);
                        else
                            info.ParentID = null;
                        categoryOperator.Update(info);
                    }
                    BindTree();
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "AdminDefault")));
        }
    }

    private void BindTree()
    {
        try
        {
            treeCategories.Nodes.Clear();

            TreeNode rootNode = new TreeNode("Root");
            rootNode.SelectAction = TreeNodeSelectAction.Select;
            treeCategories.Nodes.Add(rootNode);

            List<Category> categoriesList = categoryOperator.SelectAll(null);

            if (categoriesList != null && categoriesList.Count > 0)
            {
                CreateTreeNodes(categoriesList);
            }

            treeCategories.ExpandAll();
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "AdminDefault")));
        }
    }

    private void CreateTreeNodes(List<Category> categoriesList)
    {
        try
        {
            TreeNode node = null;

            foreach (Category info in categoriesList)
            {
                if (info.ParentID == null)
                {
                    if (IsArabic)
                        node = new TreeNode(info.NameAr, info.ID.ToString());
                    else
                        node = new TreeNode(info.NameEn, info.ID.ToString());

                    node.SelectAction = TreeNodeSelectAction.SelectExpand;
                    treeCategories.Nodes[0].ChildNodes.Add(node);
                    CreateChildNodes(node, categoriesList);
                }
            }
        }
        catch (Exception error)
        {
            throw error;
        }
    }

    private void CreateChildNodes(TreeNode parentNode, List<Category> categoriesList)
    {
        TreeNode childNode = null;

        foreach (Category info in categoriesList)
        {
            if (info.ParentID != null && info.ParentID == Convert.ToInt32(parentNode.Value))
            {
                if (IsArabic)
                    childNode = new TreeNode(info.NameAr, info.ID.ToString());
                else
                    childNode = new TreeNode(info.NameEn, info.ID.ToString());

                childNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                parentNode.ChildNodes.Add(childNode);
                CreateChildNodes(childNode, categoriesList);
            }
        }
    }

    private void DeleteChildren(TreeNode parentNode)
    {
        if (parentNode.ChildNodes.Count > 0)
        {
            foreach (TreeNode childNode in parentNode.ChildNodes)
            {
                DeleteChildren(childNode);
            }
        }
        categoryOperator.Delete(Convert.ToInt32(parentNode.Value));
    }

    protected void cvSelectNode_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (treeCategories.SelectedNode != null)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }
}