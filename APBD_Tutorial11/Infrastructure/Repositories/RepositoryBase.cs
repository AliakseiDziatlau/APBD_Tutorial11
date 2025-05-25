using APBD_Tutorial11.Infrastructure.Persistence;

namespace APBD_Tutorial11.Infrastructure.Repositories;

public class RepositoryBase
{
    protected readonly AppDbContext _context;

    public RepositoryBase(AppDbContext context)
    {
        _context = context;
    }
}