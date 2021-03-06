﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Services.Interfaces;
using DAL;
using DAL.DTOs;
using DAL.Helpers;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using DAL.Transformers;
using DAL.Transformers.Implementations;

namespace API.Services.Implementations
{
    public class FacultyServiceImpl : IFacultyService
    {
        private IFacultyRepository facultyRepository = new FacultyRepositoryImpl();
        private IUniversityRepository uniRepository = new UniversityRepositoryImpl();
        private IUserRepository userRepository = new UserRepositoryImpl();
        private IReadingRoomRepository rroomRepository = new ReadingRoomRepositoryImpl();

        private FacultyTransformer transformer = new FacultyTransformer();

        public FacultyDTO Add(FacultyDTO faculty)
        {
            FACULTY facToAdd, addedFac;
            FacultyDTO retVal;

            retVal = null;

            if (CheckHelper.IsFilled(faculty))
            {
                try
                {
                    facToAdd = transformer.TransformFromDTO(-1, faculty);
                    addedFac = facultyRepository.Add(facToAdd);

                    if (CheckHelper.IsFilled(addedFac))
                    {
                        addedFac.UNIVERSITY = uniRepository.GetById(faculty.UniversityId);
                        retVal = transformer.TransformToDTO(addedFac);
                    }
                }
                catch (Exception) { }
            }

            return retVal;
        }

        public FacultyDTO Update(long id, FacultyDTO faculty)
        {
            FACULTY facToUpdate, updatedFac, oldFac;
            FacultyDTO retVal;

            retVal = null;
            oldFac = facultyRepository.GetById(id);

            if (CheckHelper.IsFilled(faculty) && CheckHelper.IsFilled(oldFac))
            {
                try
                {
                    facToUpdate = transformer.TransformFromDTO(id, faculty);
                    facToUpdate.READING_ROOM = oldFac.READING_ROOM;
                    facToUpdate.BLOGs = oldFac.BLOGs;
                    facToUpdate.USERs = oldFac.USERs;

                    updatedFac = facultyRepository.Update(facToUpdate);

                    if (CheckHelper.IsFilled(updatedFac))
                    {
                        retVal = transformer.TransformToDTO(updatedFac);
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
                facultyRepository.Delete(id);
                isDeleted = true;
            }
            catch (Exception e) {}

            return isDeleted;
        }

        public List<FacultyDTO> GetAll()
        {
            List<FacultyDTO> retVal = null;
            List<FACULTY> entries;

            try
            {
                entries = facultyRepository.GetAll();
                if (CheckHelper.IsFilled(entries))
                {
                    retVal = transformer.TransformToDTO(entries);
                }
            }
            catch (Exception) { }

            return retVal;
        }

        public FacultyDTO GetById(long id)
        {
            FACULTY entry;
            FacultyDTO retVal = null;

            try
            {
                entry = facultyRepository.GetById(id);
                if (CheckHelper.IsFilled(entry))
                {
                    retVal = transformer.TransformToDTO(entry);
                }
            }
            catch (Exception) { }

            return retVal;
        }

        public List<ReadingRoomDTO> GetReadingRooms(long facId)
        {
            FACULTY faculty = null;
            List<ReadingRoomDTO> retVal = null;
            ReadingRoomTransformer rroomTransformer;
            List<READING_ROOM> rrooms;

            rrooms = rroomRepository.GetRRoomsForFaculty(facId);
            rroomTransformer = new ReadingRoomTransformer();

            if (CheckHelper.IsFilled(rrooms))
            {
                retVal = rroomTransformer.TransformToDTO(rrooms);
            }

            return retVal;
        }

        public List<UserDTO> GetStudents(long facId)
        {            
            List<UserDTO> retVal = null;
            UserTransformer userTransformer;
            List<USER> students;


            students = userRepository.GetForFaculty(facId);
            userTransformer = new UserTransformer();

            if (CheckHelper.IsFilled(students))
            {
                retVal = userTransformer.TransformToDTO(students);
            }

            return retVal;
        }

        public List<BlogDTO> GetBlogs(long facId)
        {
            FACULTY faculty = null;
            List<BlogDTO> retVal = null;
            BlogTransformer blogTransformer;

            // TODO : Implementirati metodu za dobavljanje blogova koji pripadaju fakultetu sa prosledjenim ID. 
            //faculty = facultyRepository.GetById(facId);
            blogTransformer = new BlogTransformer();

            if (CheckHelper.IsFilled(faculty) && CheckHelper.IsFilled(faculty.BLOGs))
            {
                retVal = blogTransformer.TransformToDTO(faculty.BLOGs.ToList());
            }

            return retVal;
        }
    }
}