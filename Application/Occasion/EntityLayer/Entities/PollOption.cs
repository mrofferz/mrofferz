using System;

namespace EntityLayer.Entities
{
    public class PollOption : EntityBase
    {
        #region member variables

        private int pollID;
        private string textAr;
        private string textEn;
        private int votes;
        private decimal percentage;        

        #endregion

        #region Constructor

        public PollOption()
        {
        }

        #endregion

        #region Properties

        public int PollID
        {
            get { return pollID; }
            set { pollID = value; }
        }

        public string TextAr
        {
            get { return textAr; }
            set { textAr = value; }
        }

        public string TextEn
        {
            get { return textEn; }
            set { textEn = value; }
        }

        public int Votes
        {
            get { return votes; }
            set { votes = value; }
        }

        public decimal Percentage
        {
            get { return percentage; }
            set { percentage = value; }
        }

        #endregion

        #region TableColumns

        public struct TableColumns
        {
            public static string PollID = Columns.PollID;
            public static string TextAr = Columns.TextAr;
            public static string TextEn = Columns.TextEn;
            public static string Votes = Columns.Votes;
            public static string Percentage = Columns.Percentage;
        }

        #endregion
    }
}