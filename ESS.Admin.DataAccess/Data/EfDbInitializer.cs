using ESS.Admin.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESS.Admin.DataAccess.Data
{
    public class EfDbInitializer : IDbInitializer
    {
        private readonly DataContext _dataContext;

        public EfDbInitializer(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void InitializeDb()
        {
            // Method intentionally left empty
        }
    }
}
