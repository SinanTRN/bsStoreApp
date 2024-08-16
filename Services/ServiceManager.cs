using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServicesManager
    {
        private readonly Lazy<IBookServices> _bookService;
        public ServiceManager(IRepositoryManager repositoryManager) 
        {
            _bookService= new Lazy<IBookServices>(()=>new BookManager( repositoryManager));
        } 
        public IBookServices BookService => _bookService.Value;
    }
}
