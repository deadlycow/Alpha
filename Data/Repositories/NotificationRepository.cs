using Data.Contexts;
using Data.Entities;
using Domain.Models;

namespace Data.Repositories;

public class NotificationRepository(DataContext context) : BaseRepository<NotificationEntity, Notification>(context)
{
}
