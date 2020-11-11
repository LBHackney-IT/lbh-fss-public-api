using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LBHFSSPublicAPI.V1.Helpers
{
    public class AnonEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _equalityComparer;
        private readonly Func<T, int> _hashCodeGenerator;

        public AnonEqualityComparer(Func<T, T, bool> eqComparator, Func<T, int> hashGen)
        {
            _equalityComparer = eqComparator;
            _hashCodeGenerator = hashGen;
        }

        public bool Equals([AllowNull] T x, [AllowNull] T y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else
                return _equalityComparer(x, y);
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return _hashCodeGenerator(obj).GetHashCode();
        }
    }
}
