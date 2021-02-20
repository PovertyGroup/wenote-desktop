using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Wenote.ValidationRules {
    internal class UsernameValidationRule : ValidationRule {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            var str = (value ?? "").ToString();
            return Regex.IsMatch(str, @"^[A-Za-z0-9]{6,16}$") ? 
                    new ValidationResult(false, "格式不正确，只能包含字母和数字，长度为 6-16") :
                    ValidationResult.ValidResult;
        }
    }
}
