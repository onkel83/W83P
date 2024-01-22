using W83P.Basic;

namespace W83P.Modelle
{
    public abstract class Basic
    {
        private int _id;

        public string ID
        {
            get => _id + "";
            set => _id = Konstanten.StringToInt(value);
        }
    }
}
