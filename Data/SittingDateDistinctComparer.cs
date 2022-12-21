using System.Diagnostics.CodeAnalysis;

namespace Restaurant.Data
{
    public class SittingDateDistinctComparer : EqualityComparer<Sitting>
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        public override bool Equals(Sitting? x, Sitting? y) => x.DateAvailable.Date.Equals(y.DateAvailable.Date);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        public override int GetHashCode([DisallowNull] Sitting obj)
        {
            return obj.DateAvailable.Date.GetHashCode();
        }
    }
}


