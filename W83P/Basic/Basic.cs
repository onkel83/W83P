﻿using W83P.Basic;

namespace W83P.Modelle
{
    [Serializable]
    public abstract class Basic
    {
        private int _id;

        public string ID
        {
            get => _id + "";
            set => _id = Helper.StringToInt(value);
        }
    }
}
