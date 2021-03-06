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

namespace API.Services.Implementations
{
    public class UniversityServiceImpl : IUniversityService
    {
        private IUniversityRepository uniRepository = new UniversityRepositoryImpl();
        private IFacultyRepository facultyRepository = new FacultyRepositoryImpl();

        private UniversityTransformer transformer = new UniversityTransformer();

        public UniversityDTO Add(UniversityDTO uni)
        {
            UNIVERSITY uniToAdd, addedUni;
            UniversityDTO retVal;
            
            retVal = null;

            if (CheckHelper.IsFilled(uni))
            {
                try
                {
                    uniToAdd = transformer.TransformFromDTO(-1, uni);
                    addedUni = uniRepository.Add(uniToAdd);

                    if (CheckHelper.IsFilled(addedUni))
                    {
                        retVal = transformer.TransformToDTO(addedUni);
                    }
                }
                catch (Exception) { }
            }

            return retVal;
        }

        public UniversityDTO Update(long id, UniversityDTO uni)
        {
            UNIVERSITY uniToUpdate, updatedUni, oldUni;
            UniversityDTO retVal;

            retVal = null;
            oldUni = uniRepository.GetById(id);

            if (CheckHelper.IsFilled(uni) && CheckHelper.IsFilled(oldUni))
            {
                try
                {
                    uniToUpdate = transformer.TransformFromDTO(id, uni);
                    uniToUpdate.READING_ROOM = oldUni.READING_ROOM;
                    uniToUpdate.FACULTies = oldUni.FACULTies;

                    updatedUni = uniRepository.Update(uniToUpdate);

                    if (CheckHelper.IsFilled(updatedUni))
                    {
                        retVal = transformer.TransformToDTO(updatedUni);
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
                uniRepository.Delete(id);
                isDeleted = true;
            }
            catch (Exception){}

            return isDeleted;
        }

        public List<UniversityDTO> GetAll()
        {
            List<UniversityDTO> retVal = null;
            List<UNIVERSITY> entries;

            try
            {
                entries = uniRepository.GetAll();
                if (CheckHelper.IsFilled(entries))
                {
                    retVal = transformer.TransformToDTO(entries);
                }
            }
            catch (Exception){}

            return retVal;
        }

        public UniversityDTO GetById(long id)
        {
            UNIVERSITY entry;
            UniversityDTO retVal = null;

            try
            {
                entry = uniRepository.GetById(id);
                if (CheckHelper.IsFilled(entry))
                {
                    retVal = transformer.TransformToDTO(entry);
                }
            }
            catch (Exception){}

            return retVal;
        }
        
        public List<FacultyDTO> GetFaculties(long uniId)
        {
            List<FACULTY> entries = null;
            List<FacultyDTO> retVal = null;
            FacultyTransformer facultyTransformer;

            entries = facultyRepository.GetForUniversity(uniId);

            if (CheckHelper.IsFilled(entries))
            {
                facultyTransformer = new FacultyTransformer();
                retVal = facultyTransformer.TransformToDTO(entries);
            }

            return retVal;
        }

        public List<ReadingRoomDTO> GetReadingRooms(long uniId)
        {
            List<READING_ROOM> entries = null;
            List<ReadingRoomDTO> retVal = null;
            ReadingRoomTransformer rroomTransformer;

            //entries = uniRepository.GetReadingRooms(uniId);
            rroomTransformer = new ReadingRoomTransformer();

            if(CheckHelper.IsFilled(entries)) 
            {
                retVal = rroomTransformer.TransformToDTO(entries);
            }

            return retVal;
        }
    }
}