using System.Reflection;

namespace TemplateApi.Commons.Enums.Abstract
{
    public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>> where TEnum : Enumeration<TEnum>
    {
        public int Id { get; protected init; }
        public string Name { get; protected init; } = string.Empty;

        private static readonly Dictionary<int, TEnum> EnumerationDict = CreateEnumerations();

        private static Dictionary<int, TEnum> CreateEnumerations()
        {
            var enumerationType = typeof(TEnum);

            var fieldsForType = enumerationType
                .GetFields(
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fieldInfo =>
                    enumerationType.IsAssignableFrom(fieldInfo.FieldType))
                .Select(fieldInfo =>
                    (TEnum)fieldInfo.GetValue(default)!);

            return fieldsForType.ToDictionary(x => x.Id);
        }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static TEnum FromValue(int id)
        {
            return EnumerationDict.TryGetValue(id, out TEnum enumeration) ? enumeration : default;
        }

        public static TEnum FromName(string name)
        {
            return EnumerationDict.Values.SingleOrDefault(e => e.Name == name);
        }

        public static IReadOnlyCollection<TEnum> GetValues() => EnumerationDict.Values.ToList();

        public bool Equals(Enumeration<TEnum> other)
        {
            if (other == null)
                return false;

            return GetType() == other.GetType() &&
                Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is Enumeration<TEnum> && Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
