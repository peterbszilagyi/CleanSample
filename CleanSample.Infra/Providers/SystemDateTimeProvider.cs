using CleanSample.App.Common.Interface;
using System;

namespace CleanSample.Infrastructure.Providers
{
    /// <summary>
    /// Represents built-in System.DateTime methods but wrapped by IDateTime to make it testable
    /// </summary>
    public sealed class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
