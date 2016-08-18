using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Constants;
using API.Services.Interfaces;
using DAL;
using DAL.DTOs;
using DAL.Helpers;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;

namespace API.Services.Implementations
{
    public class UserServiceImpl : IUserService
    {
        private IUserRepository userRepository = new UserRepositoryImpl();

        public UserDTO Add(UserDTO user)
        {
            return null;
        }

        public UserDTO Update(long id, UserDTO user)
        {
            return null;
        }

        public bool Delete(long id)
        {
            return true;
        }

        public List<UserDTO> GetAll()
        {
            return null;
        }

        public UserDTO GetById(long id)
        {
            return null;
        }

        public List<ReservationDTO> GetReservations(long userId)
        {
            return null;
        }

        public List<PostDTO> GetPosts(long userId)
        {
            return null;
        }

        public List<ThreadDTO> GetThreads(long userId)
        {
            return null;
        }

        public List<ReportDTO> GetReports(long userId)
        {
            return null;
        }

        public JWTDTO GetJWT(LoginDTO credentials)
        {
            string token = null;
            USER user;
            JWTDTO retVal;

            user = userRepository.CheckCredentials(credentials.Username, credentials.Password);
            var payload = new Dictionary<string, object>();
            retVal = new JWTDTO(){ Success = false };
            

            if (CheckHelper.IsFilled(user))
            {
                payload.Add("Id", user.USR_ID);
                token = JWT.JsonWebToken.Encode(payload,
                                    AppConstants.JWT_SECRET_KEY,
                                    JWT.JwtHashAlgorithm.HS256);
                retVal = new JWTDTO()
                {
                    Role = user.USR_ROLE,
                    Success = true,
                    Token = token
                };
            }

            return retVal;
        }


        public bool isAuthorized(long id, string role, string forbiddenRole)
        {
            bool isAuthorized = false;
            USER user;

            user = userRepository.GetById(id);

            if (CheckHelper.IsFilled(user))
            {
                isAuthorized = (CheckHelper.IsFilled(role) && user.USR_ROLE.Equals(role))
                    || (CheckHelper.IsFilled(forbiddenRole) && !user.USR_ROLE.Equals(forbiddenRole));
            }

            return isAuthorized;
        }
    }
}