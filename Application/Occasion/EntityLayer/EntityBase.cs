using System;

namespace EntityLayer
{
    public abstract class EntityBase
    {
        #region member variables

        private int id;
        private DateTime creationDate;
        private DateTime? modificationDate;
        private Guid? createdBy;
        private Guid? modifiedBy;

        #endregion

        #region Properties

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }

        public Guid? CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public DateTime? ModificationDate
        {
            get { return modificationDate; }
            set { modificationDate = value; }
        }

        public Guid? ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }

        #endregion

        #region CommonColumns

        public struct CommonColumns
        {
            public static string ID = Columns.ID;
            public static string IsArabic = Columns.IsArabic;
            public static string CreationDate = Columns.CreationDate;
            public static string ModificationDate = Columns.ModificationDate;
            public static string CreatedBy = Columns.CreatedBy;
            public static string ModifiedBy = Columns.ModifiedBy;
            public static string BrandID = Columns.BrandID;
            public static string CategoryID = Columns.CategoryID;
            public static string BranchID = Columns.BranchID;
            public static string OfferID = Columns.OfferID;
            public static string ParentID = Columns.ParentID;
            public static string ProductID = Columns.ProductID;
            public static string SupplierID = Columns.SupplierID;
            public static string FairID = Columns.FairID;
            public static string Value = Columns.Value;
            public static string PollID = Columns.PollID;
            public static string PollOptionID = Columns.PollOptionID;
        }

        #endregion
    }
}