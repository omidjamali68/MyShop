using Cooking.Infrastructure.Test;
using MyShop.Domain;
using MyShop.Persistence;

namespace MyShop.Application.UnitTests
{
    public abstract class TestHelp<TType>
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
        protected TType Repository { get; set; }
    }
}
