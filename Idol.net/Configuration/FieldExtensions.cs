using System.Text.RegularExpressions;

namespace Rbi.Search.Configuration
{
    public static class FieldExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsEqual(this MatchField<string> field, string condition)
        {
            return new Term(field, condition) { TermType = Regex.Match(condition, @"^.*[\*\?].*$").Success ? TermType.Wild : TermType.Match };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsNotEqual(this MatchField<string> field, string condition)
        {
            var wild = Regex.Match(condition, @"^.*[\*\?].*$").Success;
            return new Term(field, condition) { TermType = wild ? TermType.Wild : TermType.NotMatch, Inverted = wild };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsEqual(this MatchField<int> field, int condition)
        {
            return new Term(field, condition.ToString()) { TermType = TermType.Match, };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsNotEqual(this MatchField<int> field, int condition)
        {
            return new Term(field, condition.ToString()) { TermType = TermType.NotMatch };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsEqual(this NumericField field, int condition)
        {
            return new Term(field, condition.ToString()) { TermType = TermType.Equal };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsNotEqual(this NumericField field, int condition)
        {
            return new Term(field, condition.ToString()) { TermType = TermType.NotEqual };
        }

        public static Term IsInRadius(this LocationField field, double latitude, double longitude, double radius)
        {
            return new Term(field, new[] { latitude.ToString(), longitude.ToString(), radius.ToString() }) { TermType = TermType.DistSpherical };
        }

    }
}
