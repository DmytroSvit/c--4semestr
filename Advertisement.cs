using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktyka1
{
    class Advertisement:IComparable
    {
        public Dictionary<string, string> ErrorDict = new Dictionary<string, string>();
        private string _id;
        private string _websiteUrl;
        private DateTime _startDate;
        private DateTime _endDate;
        private double _price;
        private string _photoUrl;
        private string _transactionNumber;
        public string Title { get; private set; }
        public string ID 
        {
            get 
            {
                return _id;
            }
            private set 
            {
                _id = Validation.ID(value); 
                if (_id == null) 
                    ErrorDict.Add("ID", "ID must contain 8 integer numbers."); 
            } 
        }

        public string WebsiteUrl
        {
            get
            {
                return _websiteUrl;
            }
            private set
            {
                _websiteUrl = Validation.Url(value); 
                if (_websiteUrl == null)
                    ErrorDict.Add("WebsiteUrl", "Not valid url."); 
            }
        }
        public string StartDate 
        { 
            get
            {
                return _startDate.ToString();
            }
            private set 
            {
                if (DateTime.TryParse(value, out DateTime res))
                    _startDate = res;
                else
                    ErrorDict.Add("StartDate", "Not valid input. Should be in form: yyyy-mm-dd");
            }
        }
        public string EndDate 
        {
            get 
            {
                return _endDate.ToString();
            }
            private set 
            {
                if (DateTime.TryParse(value, out DateTime res))
                    _endDate = res;
                else
                    ErrorDict.Add("EndDate", "Not valid input. Should be in form: yyyy-mm-dd");
            }
        }
        public double Price 
        {
            get 
            {
                return _price;
            }
            private set
            {
                _price = Validation.Price(value); 
                if (_price == 0)
                    ErrorDict.Add("Price", "Price must be number and can not be less than zero."); 
            }
        
        }
        public string PhotoUrl 
        {
            get
            {
                return _photoUrl;
            }
            private set
            {
                _photoUrl = Validation.Url(value);
                if (_photoUrl == null)
                    ErrorDict.Add("PhotoUrl", "Not valid url.");
            }
        }
        public string TransactionNumber 
        {
            get 
            {
                return _transactionNumber;
            }
            private set 
            {
                _transactionNumber = Validation.TransactionNumber(value); 
                if(_transactionNumber == null)
                    ErrorDict.Add("TransactionNumber", "Transaction number must be in form: XX-YYY-XX/YY, X - uppercase letter, Y - digit."); 
            }
        }

        public Advertisement(Dictionary<string, string> values)
        {
            if (values.ContainsKey("id"))
                ID = values["id"];
            else { _id = null; ErrorDict.Add("ID", "Input error. ID field not found."); }

            if (values.ContainsKey("websiteurl"))
                WebsiteUrl = values["websiteurl"];
            else { _websiteUrl = null; ErrorDict.Add("WebsiteUrl", "Input error. WebsiteUrl field not found."); }

            if (values.ContainsKey("startdate"))
                StartDate = values["startdate"];
            else { _startDate = DateTime.MinValue; ErrorDict.Add("StartDate", "Input error.  StartDate field not found."); }

            if (values.ContainsKey("enddate"))
                EndDate = values["enddate"];
            else { _startDate = DateTime.MinValue; ErrorDict.Add("EndDate", "Input error.  EndDate field not found."); }

            if (values.ContainsKey("price"))
            {
                if (double.TryParse(values["price"], out double res))
                    Price = res;
                else
                {
                    Price = 0;
                }
            }
            else { _price = 0; ErrorDict.Add("Price", "Input error.  Price field not found."); }

            if (values.ContainsKey("title"))
                Title = values["title"];
            else { Title = null; ErrorDict.Add("Title", "Input error.  Title field not found."); }

            if (values.ContainsKey("photourl"))
                PhotoUrl = values["photourl"];
            else { _photoUrl = null; ErrorDict.Add("PhotoUrl", "Input error. PhotoUrl field not found."); }

            if (values.ContainsKey("transactionnumber"))
                TransactionNumber = values["transactionnumber"];
            else { _transactionNumber = null; ErrorDict.Add("TransactionNumber", "Input error. TransactionNumber field not found."); }

            if (_startDate > _endDate)
                ErrorDict.Add("Dates", "Start date must be earlier than end date.");
        }

        public int CompareTo(object ad)
        {
            return this.ToString().ToLower().CompareTo(((Advertisement)ad).ToString().ToLower());
        }
        public override string ToString()
        {
            string res = "Advertisement with ";

            foreach (var field in (this).GetType().GetProperties())
            {
                res += $"{field.Name}: {field.GetValue(this)}, ";
            }

            return res.Remove(res.Length-2) + '.';
        }

        public static Advertisement Parse(string line)
        {
            string[] values = line.Split();
            Dictionary<string, string> fields = new Dictionary<string, string>();
            Dictionary<string, string> errors = new Dictionary<string, string>();

            foreach (var value in values)
            {
                if (value.Contains("::"))
                    fields.Add(value.Split("::")[0].ToLower(), value.Split("::")[1]);
                else if (value == "")
                { }
                else
                    errors.Add($"{value}", " There is no stated field for this value. Must be written in form: fieldName::value .");
            }

            Advertisement res = new Advertisement(fields);

            foreach (var error in errors)
            {
                res.ErrorDict.Add(error.Key, error.Value);
            }

            return res;
        }
    }
}
