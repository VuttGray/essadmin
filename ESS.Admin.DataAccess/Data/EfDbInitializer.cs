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
            _dataContext.Database.EnsureDeleted();
            _dataContext.Database.EnsureCreated();

            _dataContext.AddRange(FakeDataFactory.Users);
            _dataContext.SaveChanges();

            _dataContext.AddRange(FakeDataFactory.Messages);
            _dataContext.SaveChanges();
        }
    }
}
