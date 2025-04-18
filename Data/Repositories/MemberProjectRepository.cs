using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;
public class MemberProjectRepository(DataContext context) : BaseRepository<MemberProjectEntity, MemberProject>(context), IMemberProjectRepository
{
}
