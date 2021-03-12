using System;

namespace CleanSample.App.Common.Interface
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
