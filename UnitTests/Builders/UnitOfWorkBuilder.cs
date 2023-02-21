using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Repositories;

namespace UnitTests.Builders
{
    public static class UnitOfWorkBuilder
    {
        public static UnitOfWork Build(DataContext context)
        {
            return new UnitOfWork(context);
        }
    }
}
