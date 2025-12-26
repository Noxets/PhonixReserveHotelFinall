using Hamkare.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Infrastructure;

public static class BaseSeed
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Identity();
    }
}