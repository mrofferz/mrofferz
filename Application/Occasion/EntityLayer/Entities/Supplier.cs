using System;
using System.Collections.Generic;

namespace EntityLayer.Entities
{
    public class Supplier : EntityBase
    {
        #region member variables

        private string nameAr;
        private string nameEn;
        private string shortDescriptionAr;
        private string shortDescriptionEn;
        private string descriptionAr;
        private string descriptionEn;
        private string contactPerson;
        private string contactPersonMobile;
        private string contactPersonEmail;
        private string image;
        private string website;
        private string email;
        private string hotLine;
        private bool isActive;
        private DateTime? activationDate;
        private DateTime? deactivationDate;
        private Guid? activatedBy;
        private Guid? deactivatedBy;
        private List<Branch> branchList;

        #endregion

        #region Constructor

        public Supplier()
        {
            branchList = new List<Branch>();
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

        public string ShortDescriptionAr
        {
            get { return shortDescriptionAr; }
            set { shortDescriptionAr = value; }
        }

        public string ShortDescriptionEn
        {
            get { return shortDescriptionEn; }
            set { shortDescriptionEn = value; }
        }

        public string DescriptionAr
        {
            get { return descriptionAr; }
            set { descriptionAr = value; }
        }

        public string DescriptionEn
        {
            get { return descriptionEn; }
            set { descriptionEn = value; }
        }

        public string ContactPerson
        {
            get { return contactPerson; }
            set { contactPerson = value; }
        }

        public string ContactPersonMobile
        {
            get { return contactPersonMobile; }
            set { contactPersonMobile = value; }
        }

        public string ContactPersonEmail
        {
            get { return contactPersonEmail; }
            set { contactPersonEmail = value; }
        }

        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        public string Website
        {
            get { return website; }
            set { website = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string HotLine
        {
            get { return hotLine; }
            set { hotLine = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public DateTime? ActivationDate
        {
            get { return activationDate; }
            set { activationDate = value; }
        }

        public DateTime? DeactivationDate
        {
            get { return deactivationDate; }
            set { deactivationDate = value; }
        }

        public Guid? ActivatedBy
        {
            get { return activatedBy; }
            set { activatedBy = value; }
        }

        public Guid? DeactivatedBy
        {
            get { return deactivatedBy; }
            set { deactivatedBy = value; }
        }

        public List<Branch> BranchList
        {
            get { return branchList; }
            set { branchList = value; }
        }

        #endregion

        #region TableColumns

        public struct TableColumns
        {
            public static string NameAr = Columns.NameAr;
            public static string NameEn = Columns.NameEn;
            public static string ShortDescriptionAr = Columns.ShortDescriptionAr;
            public static string ShortDescriptionEn = Columns.ShortDescriptionEn;
            public static string DescriptionAr = Columns.DescriptionAr;
            public static string DescriptionEn = Columns.DescriptionEn;
            public static string ContactPerson = Columns.ContactPerson;
            public static string ContactPersonMobile = Columns.ContactPersonMobile;
            public static string ContactPersonEmail = Columns.ContactPersonEmail;
            public static string Image = Columns.Image;
            public static string Website = Columns.Website;
            public static string Email = Columns.Email;
            public static string HotLine = Columns.HotLine;
            public static string IsActive = Columns.IsActive;
            public static string ActivationDate = Columns.ActivationDate;
            public static string ActivatedBy = Columns.ActivatedBy;
            public static string DeactivationDate = Columns.DeactivationDate;
            public static string DeactivatedBy = Columns.DeactivatedBy;
        }

        #endregion
    }
}