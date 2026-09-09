using LubricentroVelezV2.Models;
using Microsoft.EntityFrameworkCore;

namespace LubricentroVelezV2.Repositories.Implementation
{
    public class OrdenesContextFactory : IDbContextFactory<OrdenesContext>
    {
        public OrdenesContext CreateDbContext()
        {
            return new OrdenesContext();
        }
    }
}
