using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepository
     : Repository<Product>,
       IProductRepository
    {
        public ProductRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
