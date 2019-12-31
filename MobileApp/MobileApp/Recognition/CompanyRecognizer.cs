using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MobileApp.Database.DTO;
using MobileApp.Extensions;
using MobileApp.Model.Recognition;

namespace MobileApp.Recognition
{
    class CompanyRecognizer : BaseRecognizer
    {
        private readonly Company _company;

        private Word _postalCodeWord;
        private Word _addressWord;
        private Word _companyNameWord;
        private Word _vatIdNumberWord;
        private Word _cityWord;

        public CompanyRecognizer(List<Word> words, Company company) : base(words)
        {
            _company = company ?? new Company();
        }

        public Company GetRecognizedCompany()
        {
            GetPostalCode();
            GetAddress();
            GetCompanyName();
            GetCity();
            GetVatIdentificationNumber();


            _company.PostalCode = _postalCodeWord?.Text;
            _company.Address = _addressWord?.Text;
            _company.Name = _companyNameWord?.Text;
            _company.City = _cityWord?.Text;
            _company.VatIdentificationNumber = _vatIdNumberWord?.Text;

            return _company;
        }

        private void GetPostalCode()
        {
            var postalCodePattern = @"^\d{2}[-]\d{3}$";
            var postalCodeWord = _words.Where(x => Regex.IsMatch(x.Text, postalCodePattern))
                .OrderBy(x => x.BoundingRect.Y).LastOrDefault();

            if (postalCodeWord != null)
            {
                _postalCodeWord = postalCodeWord;
            }
        }

        private void GetAddress()
        {
            var addressKeywords = new[] { "os", "ul", "al" };

            var addressTypeWord = _words.Where(x => x.Text.ToLower().StartsWithAny(addressKeywords) && x.Text.Length < 4)
                .OrderBy(x => x.BoundingRect.Y).LastOrDefault();

            if (addressTypeWord == null)
            {
                return;
            }

            var buffer = Word.Empty;
            var nextWord = addressTypeWord;
            do
            {
                if (buffer.Text != null && Regex.IsMatch(buffer.Text, "[0-9]"))
                {
                    break;
                }
                buffer += nextWord;
                buffer.Text += " ";
                nextWord = GetFromRight(nextWord, true);
            } while (nextWord != null);

            _addressWord = buffer;
        }

        private void GetCompanyName()
        {
            var lines = WordProcessor.GroupInLines(_words);

            var firstLine = lines[0];

            var buffer = Word.Empty;
            firstLine.ForEach(w =>
            {
                buffer += w;
                buffer.Text += " ";
            });

            _companyNameWord = buffer;
        }

        private void GetVatIdentificationNumber()
        {
            _vatIdNumberWord = _words.FirstOrDefault(w => ValidateNip(w.Text));
        }

        private void GetCity()
        {
            _cityWord = GetFromRight(_postalCodeWord, true);
        }

        public bool ValidateNip(string nip)
        {
            nip = nip.Trim().Replace("-", string.Empty);
            const string nipPattern = @"^\d{10}$";
            if (!Regex.IsMatch(nip, nipPattern))
            {
                return false;
            }

            int[] weights = { 6, 5, 7, 2, 3, 4, 5, 6, 7, 0 };
            var sum = nip.Zip(weights, (digit, weight) => (digit - '0') * weight)
                .Sum();

            return sum % 11 == nip[9] - '0';
        }
    }
}
