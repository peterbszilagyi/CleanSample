using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using CleanSample.Domain.Attributes;

namespace CleanSample.Domain.BaseClasses
{
	public abstract class ValueObject : IEquatable<ValueObject>
	{
		private List<PropertyInfo> properties;
		private List<FieldInfo> fields;

		public static bool operator ==(ValueObject obj1, ValueObject obj2)
		{
			if (Equals(obj1, null))
			{
				return Equals(obj2, null);
			}
			return obj1.Equals(obj2);
		}

		public static bool operator !=(ValueObject obj1, ValueObject obj2)
		{
			return !(obj1 == obj2);
		}

		public bool Equals(ValueObject obj)
		{
			return Equals(obj as object);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) return false;

			return GetProperties().All(p => PropertiesAreEqual(obj, p))
				&& GetFields().All(f => FieldsAreEqual(obj, f));
		}

		private bool PropertiesAreEqual(object obj, PropertyInfo p)
		{
			return object.Equals(p.GetValue(this, null), p.GetValue(obj, null));
		}

		private bool FieldsAreEqual(object obj, FieldInfo f)
		{
			return object.Equals(f.GetValue(this), f.GetValue(obj));
		}

		private IEnumerable<PropertyInfo> GetProperties()
		{
			return properties ?? (properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
					.Where(p => !Attribute.IsDefined(p, typeof(IgnoreMemberAttribute))).ToList());
		}

		private IEnumerable<FieldInfo> GetFields()
		{
			return fields ?? (fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
					.Where(f => !Attribute.IsDefined(f, typeof(IgnoreMemberAttribute))).ToList());
		}

		public override int GetHashCode()
		{
			unchecked   //allow overflow
			{
				int hash = 17;
				foreach (var prop in GetProperties())
				{
					var value = prop.GetValue(this, null);
					hash = HashValue(hash, value);
				}

				foreach (var field in GetFields())
				{
					var value = field.GetValue(this);
					hash = HashValue(hash, value);
				}

				return hash;
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Readability", "RCS1123:Add parentheses according to operator precedence.", Justification = "<Pending>")]
		private int HashValue(int seed, object value)
		{
			var currentHash = value?.GetHashCode() ?? 0;

			return seed * 23 + currentHash;
		}

		public ValueObject GetCopy()
		{
			return MemberwiseClone() as ValueObject;
		}
	}
}
