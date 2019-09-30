using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

/// <summary>
/// Custom ObservableCollection allows OnPropertyChanged to fire if an item inside the collections property changes
/// without the Collection itself changing (Not standard functionality in C#)
/// 
/// Found online, implemented by Alex
/// </summary>
namespace Koinonia.Models
{ 
    public class CustomObservableCollection<T> : ObservableCollection<T>
    {
        public CustomObservableCollection() { }
        public CustomObservableCollection(IEnumerable<T> items) : this()
        {
            foreach (var item in items)
                this.Add(item);
        }
        public void ReportItemChange(T item)
        {
            NotifyCollectionChangedEventArgs args =
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Replace,
                    item,
                    item,
                    IndexOf(item));
            OnCollectionChanged(args);
        }
    }
}