using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Acme.BookStore.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore;

/* Inherit your application services from this class.
 */
public abstract class BookStoreAppService : ApplicationService, ITransientDependency
{
    protected BookStoreAppService()
    {
        LocalizationResource = typeof(BookStoreResource);
    }
    
    }
