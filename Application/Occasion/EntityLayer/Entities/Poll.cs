using System;
using System.Collections.Generic;

namespace EntityLayer.Entities
{
    public class Poll : EntityBase
    {
        #region member variables

        private string titleAr;
        private string titleEn;
        private bool isCurrent;
        private DateTime? currentDate;
        private Guid? currentBy;
        private bool isNew;
        private bool isArchived;
        private DateTime? archiveDate;
        private Guid? archivedBy;
        private int totalVotes;
        private List<PollOption> options;

        #endregion

        #region Constructor

        public Poll()
        {
            options = new List<PollOption>();
        }

        #endregion

        #region Properties

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

        public bool IsCurrent
        {
            get { return isCurrent; }
            set { isCurrent = value; }
        }

        public DateTime? CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; }
        }

        public Guid? CurrentBy
        {
            get { return currentBy; }
            set { currentBy = value; }
        }

        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        public bool IsArchived
        {
            get { return isArchived; }
            set { isArchived = value; }
        }

        public DateTime? ArchiveDate
        {
            get { return archiveDate; }
            set { archiveDate = value; }
        }

        public Guid? ArchivedBy
        {
            get { return archivedBy; }
            set { archivedBy = value; }
        }

        public int TotalVotes
        {
            get { return totalVotes; }
            set { totalVotes = value; }
        }

        public List<PollOption> Options
        {
            get { return options; }
            set { options = value; }
        }

        #endregion

        #region TableColumns

        public struct TableColumns
        {
            public static string TitleAr = Columns.TitleAr;
            public static string TitleEn = Columns.TitleEn;
            public static string IsCurrent = Columns.IsCurrent;
            public static string CurrentDate = Columns.CurrentDate;
            public static string CurrentBy = Columns.CurrentBy;
            public static string IsNew = Columns.IsNew;
            public static string IsArchived = Columns.IsArchived;
            public static string ArchiveDate = Columns.ArchiveDate;
            public static string ArchivedBy = Columns.ArchivedBy;
            public static string TotalVotes = Columns.TotalVotes;
        }

        #endregion
    }
}