using LibraryManagementBackend.Objects;

namespace LibraryManagementBackend.Repositories
{
    internal interface IManage<T> : IRepository<T> , IBorrow where T : class
    {
    }
}