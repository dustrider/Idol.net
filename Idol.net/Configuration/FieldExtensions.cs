using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rbi.Search.Configuration
{
    public static class FieldExtensions
    {
        private static bool IsWild(string condition)
        {
            return Regex.Match(condition, @"^.*[\*\?].*$").Success;
        }

        #region Match Type Extensions <String>
        public static Term IsEqual(this MatchField<string> field, string condition)
        {
            return new Term(field, condition) { TermType = IsWild(condition) ? TermType.Wild : TermType.Match };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsEqualAll(this MatchField<string> field, params string[] conditions)
        {
            //validate input
            if ((conditions == null) || (conditions.Count() < 1))
            {
                throw new InvalidOperationException("IsEqualAll requires at least one condition ");
            }

            //If any of the conditions are wild cards we need to behave differently
            var isWild = false;
            foreach (var condition in conditions)
            {
                if (IsWild(condition))
                    isWild = true;
            }

            //If it has a wild card statement, instead join them using individual matches and wilds
            if (isWild)
            {
                var terms = new List<Term>();
                var first = true;
                foreach (var condition in conditions)
                {
                    var term = new Term(field, condition) { TermType = IsWild(condition) ? TermType.Wild : TermType.Match };
                    if (!first)
                    {
                        term.Operator = OperatorType.And;
                    }
                    else
                    {
                        first = false;
                    }
                    terms.Add(term);
                }
                return new Term(terms.ToArray());
            }

            //Since its not wild, use a MatchAll command
            return new Term(field, conditions) { TermType = TermType.MatchAll };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsEqualOnly(this MatchField<string> field, EqualOnlyBehaviour behaviour, params string[] conditions)
        {
            if (behaviour == EqualOnlyBehaviour.SkipFieldCheck)
            {
                return new Term(field, conditions) { TermType = TermType.MatchCover };
            }

            return (new Term(field, conditions) { TermType = TermType.MatchCover }) & field.Exists();
        }

        public static Term Exists(this MatchField<string> field)
        {
            return new Term(field, String.Empty) { TermType = TermType.Exists };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsNotEqual(this MatchField<string> field, string condition)
        {
            var wild = IsWild(condition);
            return new Term(field, condition) { TermType = wild ? TermType.Wild : TermType.Match, Inverted = true };
        }


        #endregion

        #region Match Type Extensions <int>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsEqual(this MatchField<int> field, int condition)
        {
            return new Term(field, condition.ToString()) { TermType = TermType.Match };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsEqualAll(this MatchField<int> field, params int[] conditions)
        {
            //Since its not wild (as an integer type), use a MatchAll command
            return new Term(field, conditions.Select(i => i.ToString())) { TermType = TermType.MatchAll };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsNotEqual(this MatchField<int> field, int condition)
        {
            return new Term(field, condition.ToString()) { TermType = TermType.Match, Inverted = true };
        }
        #endregion

        #region Numeric Type Extensions
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsEqual(this NumericField field, int condition)
        {
            return new Term(field, condition.ToString()) { TermType = TermType.Equal };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static Term IsNotEqual(this NumericField field, int condition)
        {
            return new Term(field, condition.ToString()) { TermType = TermType.Equal, Inverted = true };
        }
        #endregion

        public static Term IsInRadius(this LocationField field, double latitude, double longitude, double radius)
        {
            return new Term(field, new[] { latitude.ToString(), longitude.ToString(), radius.ToString() }) { TermType = TermType.DistSpherical };
        }

    }
}
