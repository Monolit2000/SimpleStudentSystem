using System.Globalization;

namespace Students.Interfaces
{
    /// <summary>
    /// Service for accessing shared resources.
    /// </summary>
    public interface ISharedResourcesService
    {
        /// <summary>
        /// Gets the resource string associated with the specified resource Id.
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        /// <returns>The resource string.</returns>
        public string GetString(string resourceId);

        /// <summary>
        /// Gets the resource string associated with the specified resource Id and culture info.
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <returns>The resource string.</returns>
        string GetString(string resourceId, CultureInfo cultureInfo);


        /// <summary>
        /// Indexer to get the resource string associated with the specified resource Id.
        /// </summary>
        /// <param name="resourceId">The resource Id.</param>
        /// <returns>The resource string.</returns>
        string this[string resourceId] { get; }
    }

}
