using System;

namespace CleanSample.Domain.Attributes
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public sealed class IgnoreMemberAttribute : Attribute
	{
	}
}
