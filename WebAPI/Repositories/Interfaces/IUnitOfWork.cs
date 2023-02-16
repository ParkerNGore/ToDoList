namespace WebAPI.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IListItemRepository ListItem { get; }
        IListTypeRepository ListType { get; }
        void Complete();
    }
}
