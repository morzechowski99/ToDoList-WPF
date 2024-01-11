using System.Collections.ObjectModel;

namespace ToDoList;

internal static class Extensions
{
    internal static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable<T>
    {
        var sorted = collection.ToList();
        sorted.Sort((a, b) => a.CompareTo(b));
        for (var i = 0; i < sorted.Count; i++)
        {
            collection.Move(collection.IndexOf(sorted[i]), i);
        }
    }
}