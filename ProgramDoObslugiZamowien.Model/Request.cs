using System.Text.RegularExpressions;

namespace ProgramDoObslugiZamowien.Model
{
    public class Request
    {
        public string ClientId { get; private set; }
        public long RequestId { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public double Price { get; private set; }
        private const string ValidCharacters = @"[0-9a-zA-Z]";

        public bool SetValidateClientId(string data)
        {
            if(ValidateClientId(data))
            {
                ClientId = data;
                return true;
            }
            return false;           
        }
        public bool SetValidateParseRequestId(string data)
        {
            if (data == null || long.TryParse(data, out long result) == false)
                return false;

            RequestId = long.Parse(data);
            return true;
        }
        public bool SetValidateName(string data)
        {
            if(ValidateName(data))
            {
                Name = data;
                return true;
            }
            return false;
        }
        
        public bool SetValidateParseQuantity(string data)
        {
            if (data == null || int.TryParse(data, out int result) == false)
                return false;

            Quantity = int.Parse(data);
            return true;
        }
        public bool SetValidateParsePrice(string data)
        {
            if (data == null)
                return false;

            data = data.Replace('.', ',');
            if (double.TryParse(data, out double result) == false)
                return false;

            Price = double.Parse(data);
            return true;
        }
        public static bool ValidateName(string data)
        {

            if (data == null || data.Length > 255 )
                return false;
            foreach (var d in data)
            {
                if (!Regex.IsMatch(d.ToString(), ValidCharacters) && d != ' ')
                    return false;
            }
            return true;
        }
        public static bool ValidateClientId(string data)
        {
            if (data == null || data.Length > 6)
                return false;
            foreach (var d in data)
            {
                if (!Regex.IsMatch(d.ToString(), ValidCharacters))
                    return false;
            }
            return true;
        }
    }
}
