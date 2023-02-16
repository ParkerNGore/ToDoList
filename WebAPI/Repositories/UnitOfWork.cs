using WebAPI.Data;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DataContext context;
        private bool disposed;
        public UnitOfWork(DataContext context)
        {
            this.context = context;
            ListItem = new ListItemRepository();
            ListType = new ListTypeRepository();
        }
        public IListItemRepository ListItem { get; private set; }
        public IListTypeRepository ListType { get; private set; }
        ~UnitOfWork()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                context.Dispose();
           
            disposed = true;
        }
        public void Complete() => context.SaveChanges();

    }
}
