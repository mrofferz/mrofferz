using System;
using System.Collections.Generic;

namespace EntityLayer.Entities
{
    public class Offer : EntityBase
    {
        #region member variables

        private int categoryID;
        private int supplierID;
        private int? brandID;
        private Currency currencyInfo;
        private string nameAr;
        private string nameEn;
        private string titleAr;
        private string titleEn;
        private string shortDescriptionAr;
        private string shortDescriptionEn;
        private string descriptionAr;
        private string descriptionEn;
        private DateTime? startDate;
        private DateTime? endDate;
        private decimal? oldPrice;
        private decimal? newPrice;
        private int? discountPercentage;
        private string image;
        private bool isProduct;
        private bool isSale;
        private int? saleUpTo;
        private bool isPackage;
        private string packageDescriptionAr;
        private string packageDescriptionEn;
        private bool isFeaturedOffer;
        private bool isBestDeal;
        private int rate;
        private int rateCount;
        private int rateTotal;
        private int likes;
        private int views;
        private bool isActive;
        private DateTime? activationDate;
        private DateTime? deactivationDate;
        private Guid? activatedBy;
        private Guid? deactivatedBy;

        #endregion

        #region Constructor

        public Offer()
        {
            currencyInfo = new Currency();
        }

        #endregion

        #region Properties

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        public int SupplierID
        {
            get { return supplierID; }
            set { supplierID = value; }
        }

        public int? BrandID
        {
            get { return brandID; }
            set { brandID = value; }
        }

        public Currency CurrencyInfo
        {
            get { return currencyInfo; }
            set { currencyInfo = value; }
        }

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

        public string TitleAr
        {
            get { return titleAr; }
            set { titleAr = value; }
        }

        public string TitleEn
        {
            get { return titleEn; }
            set { titleEn = value; }
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

        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public decimal? OldPrice
        {
            get { return oldPrice; }
            set { oldPrice = value; }
        }

        public decimal? NewPrice
        {
            get { return newPrice; }
            set { newPrice = value; }
        }

        public int? DiscountPercentage
        {
            get { return discountPercentage; }
            set { discountPercentage = value; }
        }

        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        public bool IsProduct
        {
            get { return isProduct; }
            set { isProduct = value; }
        }

        public bool IsSale
        {
            get { return isSale; }
            set { isSale = value; }
        }

        public int? SaleUpTo
        {
            get { return saleUpTo; }
            set { saleUpTo = value; }
        }

        public bool IsPackage
        {
            get { return isPackage; }
            set { isPackage = value; }
        }

        public string PackageDescriptionAr
        {
            get { return packageDescriptionAr; }
            set { packageDescriptionAr = value; }
        }

        public string PackageDescriptionEn
        {
            get { return packageDescriptionEn; }
            set { packageDescriptionEn = value; }
        }

        public bool IsFeaturedOffer
        {
            get { return isFeaturedOffer; }
            set { isFeaturedOffer = value; }
        }

        public bool IsBestDeal
        {
            get { return isBestDeal; }
            set { isBestDeal = value; }
        }

        public int Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        public int RateCount
        {
            get { return rateCount; }
            set { rateCount = value; }
        }

        public int RateTotal
        {
            get { return rateTotal; }
            set { rateTotal = value; }
        }

        public int Likes
        {
            get { return likes; }
            set { likes = value; }
        }

        public int Views
        {
            get { return views; }
            set { views = value; }
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
            public static string CategoryID = Columns.CategoryID;
            public static string SupplierID = Columns.SupplierID;
            public static string BrandID = Columns.BrandID;
            public static string CurrencyID = Columns.CurrencyID;
            public static string NameAr = Columns.NameAr;
            public static string NameEn = Columns.NameEn;
            public static string TitleAr = Columns.TitleAr;
            public static string TitleEn = Columns.TitleEn;
            public static string ShortDescriptionAr = Columns.ShortDescriptionAr;
            public static string ShortDescriptionEn = Columns.ShortDescriptionEn;
            public static string DescriptionAr = Columns.DescriptionAr;
            public static string DescriptionEn = Columns.DescriptionEn;
            public static string StartDate = Columns.StartDate;
            public static string EndDate = Columns.EndDate;
            public static string OldPrice = Columns.OldPrice;
            public static string NewPrice = Columns.NewPrice;
            public static string DiscountPercentage = Columns.DiscountPercentage;
            public static string Image = Columns.Image;
            public static string IsProduct = Columns.IsProduct;
            public static string IsSale = Columns.IsSale;
            public static string SaleUpTo = Columns.SaleUpTo;
            public static string IsPackage = Columns.IsPackage;
            public static string PackageDescriptionAr = Columns.PackageDescriptionAr;
            public static string PackageDescriptionEn = Columns.PackageDescriptionEn;
            public static string IsFeaturedOffer = Columns.IsFeaturedOffer;
            public static string IsBestDeal = Columns.IsBestDeal;
            public static string Rate = Columns.Rate;
            public static string RateCount = Columns.RateCount;
            public static string RateTotal = Columns.RateTotal;
            public static string Likes = Columns.Likes;
            public static string Views = Columns.Views;
            public static string IsActive = Columns.IsActive;
            public static string ActivationDate = Columns.ActivationDate;
            public static string ActivatedBy = Columns.ActivatedBy;
            public static string DeactivationDate = Columns.DeactivationDate;
            public static string DeactivatedBy = Columns.DeactivatedBy;
        }

        #endregion
    }
}