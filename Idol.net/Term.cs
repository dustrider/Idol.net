﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rbi.Search.Configuration;

namespace Rbi.Search
{
    public class Term
    {
        internal bool? Inverted { get; set; }
        internal OperatorType? Operator { get; set; }
        internal List<Field> Fields { get; set; }
        internal TermType TermType { get; set; }
        internal List<string> Values { get; set; }
        internal List<Term> Terms { get; set; }

        internal Term(IEnumerable<Field> fields, IEnumerable<string> values)
        {
            Fields = new List<Field>(fields);
            Values = new List<string>(values);
        }

        internal Term(IEnumerable<Field> fields, string value)
            : this(fields, new[] { value })
        {

        }

        internal Term(Field field, IEnumerable<string> values)
            : this(new[] { field }, values)
        {

        }


        internal Term(Field field, string value)
            : this(new[] { field }, new[] { value })
        {

        }

        internal Term(params Term[] terms)
        {
            Terms = new List<Term>(terms);
        }

        public override string ToString()
        {
            if (Terms == null)
            {
                string termString = String.Format("{0}{{{1}}}:{2}", TermType.ToString().ToUpperInvariant(), String.Join(",", Values), String.Join(",", Fields.Select(field => field.Name)));
                if (Inverted.HasValue && Inverted.Value)
                {
                    termString = "(NOT+" + termString + ")";
                }
                return termString;
            }

            var sb = new StringBuilder();
            sb.Append("(");
            foreach (var term in Terms)
            {
                if (term.Operator.HasValue)
                {
                    sb.Append("+" + term.Operator.ToString().ToUpperInvariant() + "+");
                }
                sb.Append(term);
            }
            sb.Append(")");

            return sb.ToString();
        }


        public static Term Or(params Term[] terms)
        {
            if ((terms == null) || (terms.Count() < 1))
            {
                throw new ArgumentNullException("terms", "At least one term required to Or");
            }

            for (int i = 1; i < terms.Length; i++)
            {
                terms[i].Operator = OperatorType.Or;
            }
            return new Term(terms);
        }

        public static Term And(params Term[] terms)
        {
            if ((terms == null) || (terms.Count() < 1))
            {
                throw new ArgumentNullException("terms", "At least one term required to And");
            }

            for (int i = 1; i < terms.Length; i++)
            {
                terms[i].Operator = OperatorType.And;
            }
            return new Term(terms);
        }

        public Term Or(Term right)
        {
            if (right == null)
            {
                throw new ArgumentNullException("right", "Cannot Or against a null term");
            }

            right.Operator = OperatorType.Or;
            return new Term(this, right);
        }

        public Term And(Term right)
        {
            if (right == null)
            {
                throw new ArgumentNullException("right", "Cannot And against a null term");
            }

            right.Operator = OperatorType.And;
            return new Term(this, right);
        }

        public static Term operator &(Term term, Term term2)
        {
            if (term == null)
            {
                throw new ArgumentNullException("term", "Term cannot be null");
            }
            if (term2 == null)
            {
                throw new ArgumentNullException("term2", "Term cannot be null");
            }

            if (term.Terms != null)
            {
                term2.Operator = OperatorType.And;
                term.Terms.Add(term2);
                return term;
            }

            term2.Operator = OperatorType.And;
            return new Term { Terms = new List<Term> { term, term2 } };
        }

        public static Term operator |(Term term, Term term2)
        {
            if (term == null)
            {
                throw new ArgumentNullException("term", "Term cannot be null");
            }
            if (term2 == null)
            {
                throw new ArgumentNullException("term2", "Term cannot be null");
            }

            if (term.Terms != null)
            {
                term2.Operator = OperatorType.Or;
                term.Terms.Add(term2);
                return term;
            }

            term2.Operator = OperatorType.Or;
            return new Term { Terms = new List<Term> { term, term2 } };
        }

        public static Term operator !(Term term)
        {
            if (term != null)
            {
                term.Inverted = true;
            }
            return term;
        }
    }
}
