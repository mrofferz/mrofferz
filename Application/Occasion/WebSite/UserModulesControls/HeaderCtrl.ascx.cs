using System;

public partial class HeaderCtrl : BaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ctrlSearch.initializeControl();
            ctrlSuppliersMenu.InitializeControl();
            ctrlBrandsMenu.InitializeControl();
            ctrlFairsMenu.InitializeControl();
        }
    }
}