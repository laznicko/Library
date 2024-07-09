using AutoMapper;
using Common.Models;
using Library.BusinessLayer.Interfaces;
using Library.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace Library.BusinessLayer.Managers
{
    public class UserManager : IUserManager
    {
        private readonly DBContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initialization of new instance of user manager
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public UserManager(DBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get all users from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var users = await context.Users.ToListAsync();
            return mapper.Map<IEnumerable<UserModel>>(users);
        }
    }
}
