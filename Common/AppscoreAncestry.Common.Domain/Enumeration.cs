using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AppscoreAncestry.Domain.Seedwork
{
    public abstract class Enumeration<TKey>
    {
        public string Name { get; set; }

        public TKey Id { get; set; }

        protected Enumeration()
        {
        }

        protected Enumeration(TKey id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
        
        public static IEnumerable<T> GetAll<T>() where T : Enumeration<TKey>, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var instance = new T();

                if (info.GetValue(instance) is T locatedValue)
                {
                    yield return locatedValue;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration<TKey> otherValue))
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static T FromValue<T>(TKey value) where T : Enumeration<TKey>, new()
        {
            var matchingItem = Parse<T, TKey>(value, "value", item => Equals(item.Id, value));
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration<TKey>, new()
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration<TKey>, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem != null) return matchingItem;
            var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));

            throw new InvalidOperationException(message);
        }
    }
}
