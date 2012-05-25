using System;

namespace EntityLayer.Entities
{
    public class Brand : EntityBase
    {
        #region member variables

        private string nameAr;
        private string nameEn;
        private string shortDescriptionAr;
        private string shortDescriptionEn;
        private string descriptionAr;
        private string descriptionEn;
        private string image;

        #endregion

        #region Constructor

        public Brand()
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

        public string Image
        {
            get { return image; }
            set { image = value; }
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
            public static string Image = Columns.Image;
        }

        #endregion
    }
}