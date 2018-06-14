using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.DAL.Interfaces;
using TP.ML.Entities;

namespace TP.DAL.Repositories
{
    public partial class EFUnitOfWork : IUnitOfWork
    {
        private Repository<User> UserRepository;
        public IRepository<User> Users =>
            UserRepository ?? (UserRepository = new Repository<User>(this.DB));

        private Repository<Gamer> GamerRepository;
        public IRepository<Gamer> Gamers =>
            GamerRepository ?? (GamerRepository = new Repository<Gamer>(this.DB));

        private Repository<Administrator> AdministratorRepository;
        public IRepository<Administrator> Administrators =>
            AdministratorRepository ?? (AdministratorRepository = new Repository<Administrator>(this.DB));

        private Repository<Message> MessageRepository;
        public IRepository<Message> Messages =>
            MessageRepository ?? (MessageRepository = new Repository<Message>(this.DB));

        private Repository<Game> GameRepository;
        public IRepository<Game> Games =>
            GameRepository ?? (GameRepository = new Repository<Game>(this.DB));

        private Repository<GameGamer> GameGamerRepository;
        public IRepository<GameGamer> GameGamers =>
            GameGamerRepository ?? (GameGamerRepository = new Repository<GameGamer>(this.DB));

        private Repository<Image> ImageRepository;
        public IRepository<Image> Images =>
            ImageRepository ?? (ImageRepository = new Repository<Image>(this.DB));

        private Repository<UserImage> UserImageRepository;
        public IRepository<UserImage> UserImages =>
            UserImageRepository ?? (UserImageRepository = new Repository<UserImage>(this.DB));

        private Repository<GameImage> GameImageRepository;
        public IRepository<GameImage> GameImages =>
            GameImageRepository ?? (GameImageRepository = new Repository<GameImage>(this.DB));

        private Repository<Comment> CommentRepository;
        public IRepository<Comment> Comments =>
            CommentRepository ?? (CommentRepository = new Repository<Comment>(this.DB));

        private Repository<UserComment> UserCommentRepository;
        public IRepository<UserComment> UserComments =>
            UserCommentRepository ?? (UserCommentRepository = new Repository<UserComment>(this.DB));

        private Repository<GameComment> GameCommentRepository;
        public IRepository<GameComment> GameComments =>
            GameCommentRepository ?? (GameCommentRepository = new Repository<GameComment>(this.DB));

        private Repository<MarkComment> MarkCommentRepository;
        public IRepository<MarkComment> MarkComments =>
            MarkCommentRepository ?? (MarkCommentRepository = new Repository<MarkComment>(this.DB));

        private Repository<SystemName> SystemNameRepository;
        public IRepository<SystemName> SystemNames =>
            SystemNameRepository ?? (SystemNameRepository = new Repository<SystemName>(this.DB));

        public IRepository<T> Set<T>() where T : class, new() => new Repository<T>(this.DB);

        public void Save() => this.DB.SaveChanges();
    }
}
