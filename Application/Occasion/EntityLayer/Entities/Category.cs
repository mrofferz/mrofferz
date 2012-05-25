using System;

namespace EntityLayer.Entities
{
    public class Category : EntityBase
    {
        #region member variables

        private string nameAr;
        private string nameEn;
        private int? parentID;
        private bool hasChildren;
        private bool hasOffers;
        private bool canHaveOffers;        

        #endregion

        #region Constructor

        public Category()
        {
        }

        #endregion

        #region Properties

        public string NameAr
        {
            get { return nameAr; }
            set { nameAr = value; }
        }

        public string NameEn
        {
            get { return nameEn; }
            set { nameEn = value; }
        }

        public int? ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }

        public bool HasChildren
        {
            get { return hasChildren; }
            set { hasChildren = value; }
        }

        public bool HasOffers
        {
            get { return hasOffers; }
            set { hasOffers = value; }
        }

        public bool CanHaveOffers
        {
            get { return canHaveOffers; }
            set { canHaveOffers = value; }
        }

        #endregion

        #region TableColumns

        public struct TableColumns
        {
            public static string NameAr = Columns.NameAr;
            public static string NameEn = Columns.NameEn;
            public static string ParentID = Columns.ParentID;
            public static string HasChildren = Columns.HasChildren;
            public static string HasOffers = Columns.HasOffers;
            public static string CanHaveOffers = Columns.CanHaveOffers;
        }

        #endregion
    }
}
