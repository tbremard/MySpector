namespace MySpector
{
    public class TextReplaceTransformer : ITransformer
    {
        string _oldToken; 
        string _newToken;

        public TextReplaceTransformer(string oldToken, string newToken)
        {
            _oldToken = oldToken;
            _newToken = newToken;
        }

        public IDataTruck Transform(IDataTruck data)
        {
            string text = data.GetText();
            IDataTruck ret;
            if (string.IsNullOrEmpty(text))
            {
                return DataTruck.CreateText(string.Empty);
            }
            if (string.IsNullOrEmpty(_oldToken))
            {
                return DataTruck.CreateText(text);
            }
            string replaced = text.Replace(_oldToken, _newToken);
            ret = DataTruck.CreateText(replaced);
            return ret;
        }
    }
}
