namespace Wörds
{
    using System.Collections.Generic;

    public static class CollectionExtensions
    {
        /// <summary>
        ///     Moves the first occurence of a specific object from the source, adds it to the destination
        ///     and returns true.
        ///     If the source doesn't contain the item, the move is cancelled and false is returned.
        /// </summary>
        public static bool Move<T>(this ICollection<T> source, T item, ICollection<T> destination)
        {
            if (!source.Remove(item))
                return false;

            destination.Add(item);
            return true;
        }
    }
}