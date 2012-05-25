using System;

namespace EntityLayer.Entities
{
    public class KeyValue
    {
        #region member variables

        private string key;
        private object value;

        #endregion

        #region Constructors

        public KeyValue()
        {
        }

        public KeyValue(string keyParam, object valueParam)
        {
            key = keyParam;
            value = valueParam;
        }

        #endregion

        #region Properties

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        #endregion
    }
}
