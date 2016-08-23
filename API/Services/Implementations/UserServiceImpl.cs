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
using DAL.Transformers.Implementations;
using Newtonsoft.Json;

namespace API.Services.Implementations
{
    public class UserServiceImpl : IUserService
    {
        private IUserRepository userRepository = new UserRepositoryImpl();
        private IFacultyRepository facultyRepository = new FacultyRepositoryImpl();

        private UserTransformer transformer = new UserTransformer();

        public UserDTO Add(UserDTO user)
        {
            USER userToAdd, addedUser;
            UserDTO retVal;

            retVal = null;

            if (CheckHelper.IsFilled(user))
            {
                try
                {
                    userToAdd = transformer.TransformFromDTO(-1, user);
                    addedUser = userRepository.Add(userToAdd);

                    if (CheckHelper.IsFilled(addedUser))
                    {
                        addedUser.FACULTY = facultyRepository.GetById(user.FacultyId);
                        retVal = transformer.TransformToDTO(addedUser);
                    }
                }
                catch (Exception e) {
                }
            }

            return retVal;
        }

        public UserDTO Update(long id, UserDTO user)
        {
            USER userToUpdate, updatedUser, oldUser;
            UserDTO retVal;

            retVal = null;
            oldUser = userRepository.GetById(id);

            if (CheckHelper.IsFilled(user) && CheckHelper.IsFilled(oldUser))
            {
                try
                {
                    userToUpdate = transformer.TransformFromDTO(id, user);
                    userToUpdate.RESERVATIONs = oldUser.RESERVATIONs;
                    userToUpdate.POSTs = oldUser.POSTs;
                    userToUpdate.THREADs = oldUser.THREADs;
                    userToUpdate.REPORTs = oldUser.REPORTs;

                    updatedUser = userRepository.Update(userToUpdate);

                    if (CheckHelper.IsFilled(updatedUser))
                    {
                        retVal = transformer.TransformToDTO(updatedUser);
                    }
                }
                catch (Exception) { }
            }

            return retVal;
        }

        public bool Delete(long id)
        {
            bool isDeleted = false;

            try
            {
                userRepository.Delete(id);
                isDeleted = true;
            }
            catch (Exception) { }

            return isDeleted;
        }

        public List<UserDTO> GetAll()
        {
            List<UserDTO> retVal = null;
            List<USER> entries;

            try
            {
                entries = userRepository.GetAll();
                if (CheckHelper.IsFilled(entries))
                {
                    retVal = transformer.TransformToDTO(entries);
                }
            }
            catch (Exception) { }

            return retVal;
        }

        public UserDTO GetById(long id)
        {
            USER entry;
            UserDTO retVal = null;

            try
            {
                entry = userRepository.GetById(id);
                if (CheckHelper.IsFilled(entry))
                {
                    retVal = transformer.TransformToDTO(entry);
                }
            }
            catch (Exception) { }

            return retVal;
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


        public UserDTO GetLoggedUser(string jwt)
        {
            UserDTO retVal;
            USER user;
            string payload;
            Dictionary<string, object>  values;
            long id;

            payload = JWT.JsonWebToken.Decode(jwt, AppConstants.JWT_SECRET_KEY);
            values = JsonConvert.DeserializeObject<Dictionary<string, object>>(payload);
            id = Convert.ToInt64(values["Id"]);
            retVal = GetById(id);            

            return retVal;
        }
    }
}