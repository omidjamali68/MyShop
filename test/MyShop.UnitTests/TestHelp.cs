using MyShop.Domain;
using MyShop.Persistence;
using MyShop.Common.Tests;

namespace MyShop.Application.UnitTests
{
    public abstract class TestHelp<TRepository>
    {
        private EFInMemoryDatabase db = new EFInMemoryDatabase();
        private ApplicationDbContext _context;
        protected ApplicationDbContext Context { 
            get 
            { 
                if (_context is null)                
                    _context = db.CreateDataContext<ApplicationDbContext>();                                                      

                return _context;
            } 
        } 
        protected IUnitOfWork UnitOfWork { get { return new UnitOfWork(Context); } }
        protected TRepository Repository { get; set; }
    }
}
