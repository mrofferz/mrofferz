using System;

namespace EntityLayer.Entities
{
    public class Branch : EntityBase
    {
        #region member variables

        private string nameAr;
        private string nameEn;
        private int supplierID;
        private string addressAr;
        private string addressEn;
        private string phone1;
        private string phone2;
        private string phone3;
        private string mobile1;
        private string mobile2;
        private string mobile3;
        private decimal? xCoordination;
        private decimal? yCoordination;
        private int? mapZoom;
        private string fax;
        private Location branchLocation;

        #endregion

        #region Constructor

        public Branch()
        {
            branchLocation = new Location();
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

        public int SupplierID
        {
            get { return supplierID; }
            set { supplierID = value; }
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

        public decimal? XCoordination
        {
            get { return xCoordination; }
            set { xCoordination = value; }
        }

        public decimal? YCoordination
        {
            get { return yCoordination; }
            set { yCoordination = value; }
        }

        public int? MapZoom
        {
            get { return mapZoom; }
            set { mapZoom = value; }
        }

        public Location BranchLocation
        {
            get { return branchLocation; }
            set { branchLocation = value; }
        }

        #endregion

        #region TableColumns

        public struct TableColumns
        {
            public static string NameAr = Columns.NameAr;
            public static string NameEn = Columns.NameEn;
            public static string SupplierID = Columns.SupplierID;
            public static string LocationID = Columns.LocationID;
            public static string AddressAr = Columns.AddressAr;
            public static string AddressEn = Columns.AddressEn;
            public static string Phone1 = Columns.Phone1;
            public static string Phone2 = Columns.Phone2;
            public static string Phone3 = Columns.Phone3;
            public static string Mobile1 = Columns.Mobile1;
            public static string Mobile2 = Columns.Mobile2;
            public static string Mobile3 = Columns.Mobile3;
            public static string Fax = Columns.Fax;
            public static string XCoordination = Columns.XCoordination;
            public static string YCoordination = Columns.YCoordination;
            public static string MapZoom = Columns.MapZoom;
        }

        #endregion
    }
}