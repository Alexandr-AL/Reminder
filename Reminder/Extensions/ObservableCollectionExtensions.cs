﻿using System.Collections.ObjectModel;

namespace Reminder.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void Add<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
                collection.Add(item);
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items) => new(items);
    }
}
