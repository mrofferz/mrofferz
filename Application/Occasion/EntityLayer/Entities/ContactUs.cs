using System;

namespace EntityLayer.Entities
{
    public class ContactUs : EntityBase
    {
        #region member variables

        private string title;
        private string description;
        private bool isNew;
        private string name;
        private string email;
        private bool isReplied;
        private Guid? repliedBy;
        private DateTime? replyDate;
        private string reply;
        private bool isClosed;
        private Guid? closedBy;
        private DateTime? closeDate;

        #endregion

        #region Constructor

        public ContactUs()
        {
        }

        #endregion

        #region Properties

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public bool IsReplied
        {
            get { return isReplied; }
            set { isReplied = value; }
        }

        public Guid? RepliedBy
        {
            get { return repliedBy; }
            set { repliedBy = value; }
        }

        public DateTime? ReplyDate
        {
            get { return replyDate; }
            set { replyDate = value; }
        }

        public string Reply
        {
            get { return reply; }
            set { reply = value; }
        }

        public bool IsClosed
        {
            get { return isClosed; }
            set { isClosed = value; }
        }

        public Guid? ClosedBy
        {
            get { return closedBy; }
            set { closedBy = value; }
        }

        public DateTime? CloseDate
        {
            get { return closeDate; }
            set { closeDate = value; }
        }

        #endregion

        #region TableColumns

        public struct TableColumns
        {
            public static string Title = Columns.Title;
            public static string Description = Columns.Description;
            public static string IsNew = Columns.IsNew;
            public static string Name = Columns.Name;
            public static string Email = Columns.Email;
            public static string IsReplied = Columns.IsReplied;
            public static string RepliedBy = Columns.RepliedBy;
            public static string ReplyDate = Columns.ReplyDate;
            public static string Reply = Columns.Reply;
            public static string IsClosed = Columns.IsClosed;
            public static string ClosedBy = Columns.ClosedBy;
            public static string CloseDate = Columns.CloseDate;
        }

        #endregion
    }
}