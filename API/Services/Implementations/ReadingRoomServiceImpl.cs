using System;
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
    public class ReadingRoomServiceImpl : IReadingRoomService
    {
        private IReadingRoomRepository rroomRepository = new ReadingRoomRepositoryImpl();
        private ISeatRepository seatRepository = new SeatRepositoryImpl();
        private IFacultyRepository facultyRepository = new FacultyRepositoryImpl();

        private ReadingRoomTransformer transformer = new ReadingRoomTransformer();
        private SeatTransformer seatTransformer = new SeatTransformer();

        public ReadingRoomDTO Add(ReadingRoomDTO rroom)
        {
            READING_ROOM rroomToAdd, addedRRoom;
            ReadingRoomDTO retVal;

            retVal = null;

            if (CheckHelper.IsFilled(rroom))
            {
                try
                {
                    rroomToAdd = transformer.TransformFromDTO(-1, rroom);
                    addedRRoom = rroomRepository.Add(rroomToAdd);

                    if (CheckHelper.IsFilled(addedRRoom))
                    {
                        addedRRoom.FACULTY = facultyRepository.GetById(rroom.FacultyId);
                        retVal = transformer.TransformToDTO(addedRRoom);
                    }
                }
                catch (Exception) { }
            }

            return retVal;
        }

        public ReadingRoomDTO Update(long id, ReadingRoomDTO rroom)
        {
            READING_ROOM rroomToUpdate, updatedRRoom, oldRRoom;
            ReadingRoomDTO retVal;

            retVal = null;
            oldRRoom = rroomRepository.GetById(id);

            if (CheckHelper.IsFilled(rroom) && CheckHelper.IsFilled(oldRRoom))
            {
                try
                {
                    rroomToUpdate = transformer.TransformFromDTO(id, rroom);
                    rroomToUpdate.FACULTY = oldRRoom.FACULTY;
                    rroomToUpdate.UNIVERSITY = oldRRoom.UNIVERSITY;
                    rroomToUpdate.SEATs = oldRRoom.SEATs;

                    updatedRRoom = rroomRepository.Update(rroomToUpdate);

                    if (CheckHelper.IsFilled(updatedRRoom))
                    {
                        retVal = transformer.TransformToDTO(updatedRRoom);
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
                rroomRepository.Delete(id);
                isDeleted = true;
            }
            catch (Exception) { }

            return isDeleted;
        }

        public List<ReadingRoomDTO> GetAll()
        {
            List<ReadingRoomDTO> retVal = null;
            List<READING_ROOM> entries;

            try
            {
                entries = rroomRepository.GetAll();
                if (CheckHelper.IsFilled(entries))
                {
                    retVal = transformer.TransformToDTO(entries);
                }
            }
            catch (Exception) { }

            return retVal;
        }

        public ReadingRoomDTO GetById(long id)
        {
            READING_ROOM entry;
            ReadingRoomDTO retVal = null;

            try
            {
                entry = rroomRepository.GetById(id);
                if (CheckHelper.IsFilled(entry))
                {
                    retVal = transformer.TransformToDTO(entry);
                }
            }
            catch (Exception) { }

            return retVal;
        }

        public List<SeatDTO> GetSittingSchema(long rroomID)
        {
            List<SeatDTO> retVal = null;
            List<SEAT> schema = null;

            if (rroomID > 0)
            {
                schema = seatRepository.GetForReadingRoom(rroomID);
                if (CheckHelper.IsFilled(schema))
                {
                    retVal = seatTransformer.TransformToDTO(schema);
                }
            }

            return retVal;
        }


        public void AddSeats(List<SeatDTO> schedule)
        {
            SEAT seat;

            if (CheckHelper.IsFilled(schedule))
            {
                foreach (SeatDTO dto in schedule)
                {
                    seat = seatTransformer.TransformFromDTO(-1, dto);
                    seatRepository.Add(seat);
                }
            }
        }

        public void UpdateSeats(List<SeatDTO> schedule)
        {
            SEAT seat;

            if (CheckHelper.IsFilled(schedule))
            {
                foreach (SeatDTO dto in schedule)
                {
                    seat = seatTransformer.TransformFromDTO(dto.Id, dto);
                    seatRepository.Update(seat);
                }
            }
        }
    }
}