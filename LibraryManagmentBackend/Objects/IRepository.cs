namespace LibraryManagementBackend.Objects
{
    /// <summary>
    /// Repository pattern used for book and readers
    /// </summary>
    /// <typeparam name="T">Class for "database" objects</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Adds <see cref="T"/> to the repository to be controlled by this repository
        /// </summary>
        /// <param name="entity">The entity to be added</param>
        public Task Add(T entity);
        /// <summary>
        /// Forces an update of the <see cref="T"/> with the given data
        /// </summary>
        /// <param name="entity">The object that will be used to update the existing object</param>
        /// <returns>True if the update was successful otherwise false</returns>
        public Task Update(T entity);
        /// <summary>
        /// Removes the <see cref="T"/> from the repoisitory
        /// </summary>
        /// <param name="entity">The <see cref="T"/> that will be deleted</param>
        /// <returns>True if the delete was successful otherwise false</returns>
        public Task Delete(T entity);
        /// <summary>
        /// Returns every object that is in the repository
        /// </summary>
        /// <returns>All the items inside the repository</returns>
        public Task<IEnumerable<T>> GetAll();
        /// <summary>
        /// Returns the object that matches the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The ID of the <see cref="T"/> that should be retreieved</param>
        /// <returns>Returns <see cref="T"/> matching the <paramref name="id"/> specified</returns>
        public Task<IEnumerable<T>> GetById(int id);
        /// <summary>
        /// Searches the repository underlying objects for the <paramref name="search"/>
        /// </summary>
        /// <param name="search">The pattern that you are search for</param>
        /// <returns>A subset of all the <see cref="T"/> inside the repository</returns>
        public Task<IEnumerable<T>> Search(string search);

    }
}
