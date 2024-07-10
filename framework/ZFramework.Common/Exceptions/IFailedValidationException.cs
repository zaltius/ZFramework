namespace ZFramework.Common.Exceptions
{
    /// <summary>
    /// Contract that represents a failed validation. All validation exceptions should inherit it.
    /// </summary>
    public interface IFailedValidationException : IException
    {
    }
}