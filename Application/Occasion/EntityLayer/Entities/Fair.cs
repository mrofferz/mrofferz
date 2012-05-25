using System;

namespace EntityLayer.Entities
{
    public class Fair : EntityBase
    {
        #region member variables

        private string nameAr;
        private string nameEn;        
        private string addressAr;
        private string addressEn;
        private string contactPerson;
        private string contactPersonMobile;
        private string contactPersonEmail;
        private string phone1;
        private string phone2;
        private string phone3;
        private string mobile1;
        private string mobile2;
        private string mobile3;
        private string fax;
        private string website;
        private string email;
        private string descriptionAr;
        private string descriptionEn;
        private string shortDescriptionAr;
        private string shortDescriptionEn;
        private DateTime startDate;
        private DateTime endDate;
        private string image;
        private int? rate;
        private int? rateCount;
        private int? rateTotal;
        private int? likes;
        private bool isActive;
        private DateTime? activationDate;
        private DateTime? deactivationDate;
        private Guid? activatedBy;
        private Guid? deactivatedBy;
        private Location locationInfo;
        
        #endregion

        #region Constructor

        public Fair()
        {
            locationInfo = new Location();
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

        public Location LocationInfo
        {
            get { return locationInfo; }
            set { locationInfo = value; }
        }

        public string AddressAr
        {
            get { return addressAr; }
            set { addressAr = value; }
        }

        public string AddressEn
        {
            get { return addressEn; }
            set { addressEn = value; }
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

        public string Phone1
        {
            get { return phone1; }
            set { phone1 = value; }
        }

        public string Phone2
        {
            get { return phone2; }
            set { phone2 = value; }
        }

        public string Phone3
        {
            get { return phone3; }
            set { phone3 = value; }
        }

        public string Mobile1
        {
            get { return mobile1; }
            set { mobile1 = value; }
        }

        public string Mobile2
        {
            get { return mobile2; }
            set { mobile2 = value; }
        }

        public string Mobile3
        {
            get { return mobile3; }
            set { mobile3 = value; }
        }

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
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

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        public int? Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        public int? RateCount
        {
            get { return rateCount; }
            set { rateCount = value; }
        }

        public int? RateTotal
        {
            get { return rateTotal; }
            set { rateTotal = value; }
        }

        public int? Likes
        {
            get { return likes; }
            set { likes = value; }
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

        #endregion

        #region TableColumns

        public struct TableColumns
        {
            public static string NameAr = Columns.NameAr;
            public static string NameEn = Columns.NameEn;
            public static string LocationID = Columns.LocationID;
            public static string AddressAr = Columns.AddressAr;
            public static string AddressEn = Columns.AddressEn;
            public static string ContactPerson = Columns.ContactPerson;
            public static string ContactPersonMobile = Columns.ContactPersonMobile;
            public static string ContactPersonEmail = Columns.ContactPersonEmail;
            public static string Phone1 = Columns.Phone1;
            public static string Phone2 = Columns.Phone2;
            public static string Phone3 = Columns.Phone3;
            public static string Mobile1 = Columns.Mobile1;
            public static string Mobile2 = Columns.Mobile2;
            public static string Mobile3 = Columns.Mobile3;
            public static string Fax = Columns.Fax;
            public static string Website = Columns.Website;
            public static string Email = Columns.Email;
            public static string DescriptionAr = Columns.DescriptionAr;
            public static string DescriptionEn = Columns.DescriptionEn;
            public static string ShortDescriptionAr = Columns.ShortDescriptionAr;
            public static string ShortDescriptionEn = Columns.ShortDescriptionEn;
            public static string StartDate = Columns.StartDate;
            public static string EndDate = Columns.EndDate;
            public static string Image = Columns.Image;
            public static string Rate = Columns.Rate;
            public static string RateCount = Columns.RateCount;
            public static string RateTotal = Columns.RateTotal;
            public static string Likes = Columns.Likes;
            public static string IsActive = Columns.IsActive;
            public static string ActivationDate = Columns.ActivationDate;
            public static string ActivatedBy = Columns.ActivatedBy;
            public static string DeactivationDate = Columns.DeactivationDate;
            public static string DeactivatedBy = Columns.DeactivatedBy;
        }

        #endregion
    }
}