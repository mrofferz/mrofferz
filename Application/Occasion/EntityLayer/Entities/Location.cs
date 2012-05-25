using System;

namespace EntityLayer.Entities
{
    public class Location : EntityBase
    {
        #region member variables

        private string districtAr;
        private string districtEn;

        #endregion

        #region Constructor

        public Location()
        {
        }

        #endregion

        #region Properties

        public string DistrictAr
        {
            get { return districtAr; }
            set { districtAr = value; }
        }

        public string DistrictEn
        {
            get { return districtEn; }
            set { districtEn = value; }
        }

        #endregion

        #region TableColumns

        public struct TableColumns
        {
            public static string DistrictAr = Columns.DistrictAr;
            public static string DistrictEn = Columns.DistrictEn;
        }

        #endregion
    }
}