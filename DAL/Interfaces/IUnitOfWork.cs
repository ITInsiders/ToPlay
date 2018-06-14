using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.ML.Entities;

namespace TP.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Gamer> Gamers { get; }
        IRepository<Administrator> Administrators { get; }

        IRepository<Message> Messages { get; }

        IRepository<Game> Games { get; }

        IRepository<GameGamer> GameGamers { get; }

        IRepository<Image> Images { get; }
        IRepository<UserImage> UserImages { get; }
        IRepository<GameImage> GameImages { get; }

        IRepository<Comment> Comments { get; }
        IRepository<UserComment> UserComments { get; }
        IRepository<GameComment> GameComments { get; }
        IRepository<MarkComment> MarkComments { get; }

        IRepository<SystemName> SystemNames { get; }

        IRepository<T> Set<T>() where T : class, new();

        void Save();
    }
}
