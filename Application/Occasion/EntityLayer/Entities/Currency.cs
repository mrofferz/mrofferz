using System;

namespace EntityLayer.Entities
{
    public class Currency : EntityBase
    {
        #region member variables

        private string unitAr;
        private string unitEn;

        #endregion

        #region Constructor

        public Currency()
        {
        }

        #endregion

        #region Properties

        public string UnitAr
        {
            get { return unitAr; }
            set { unitAr = value; }
        }

        public string UnitEn
        {
            get { return unitEn; }
            set { unitEn = value; }
        }

        #endregion

        #region TableColumns

        public struct TableColumns
        {
            public static string UnitAr = Columns.UnitAr;
            public static string UnitEn = Columns.UnitEn;
        }

        #endregion
    }
}
