using System;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Commons.Commons.Extensions
{
    public static class ListExtensions
    {
        public static void AddRange<T>(this IList<T> source, IEnumerable<T> newElements)
        {
            foreach (var newElement in newElements)
            {
                source.Add(newElement);
            }
        }

        public static void AddRange<T>(this IList<T> source, IList<T> newElements)
        {
            foreach (var newElement in newElements)
            {
                source.Add(newElement);
            }
        }

        public static void Replace<T>(this IList<T> soruce, IList<T> newElements)
        {
            soruce.Clear();
            soruce.AddRange(newElements);
        }

        /// <summary>
        /// Añade un elemento que no este dentro de la lista 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="entity"></param>
        public static void AddNoDuplicate<T>(this IList<T> source, T entity)
        {
            if (!source.Contains(entity))
            {
                source.Add(entity);
            }
        }

        public static List<T> Map<T>(this IList<T> source, Func<T, T> method)
        {
            var newList = new List<T>();
            foreach (var item in source)
            {
                var newItem = method(item);
                newList.Add(newItem);
            }

            return newList;
        }
    }
}




