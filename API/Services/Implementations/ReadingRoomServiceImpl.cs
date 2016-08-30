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
        private IUniversityRepository universityRepository = new UniversityRepositoryImpl();

        private ReadingRoomTransformer transformer = new ReadingRoomTransformer();
        private SeatTransformer seatTransformer = new SeatTransformer();

        public ReadingRoomDTO Add(ReadingRoomDTO rroom)
        {
            READING_ROOM rroomToAdd, addedRRoom;
            ReadingRoomDTO retVal;
            SEAT seat;
            List<SEAT> seats = new List<SEAT>();
            long seatID;

            retVal = null;

            if (CheckHelper.IsFilled(rroom))
            {
                try
                {
                    seatID = seatRepository.getNextId();
                    for (int i = 0; i < rroom.Dimension; i++)
                    {
                        seat = new SEAT()
                        {
                            SEAT_LABEL = "" + (i + 1),
                            SEAT_POSITION = i + 1,
                            RROOM_ID = rroom.Id,
                            SEAT_ID = seatID+i
                        };
                        seats.Add(seat);
                    }

                    rroomToAdd = transformer.TransformFromDTO(-1, rroom);
                    rroomToAdd.SEATs = seats;
                    addedRRoom = rroomRepository.Add(rroomToAdd);

                    if (CheckHelper.IsFilled(addedRRoom))
                    {
                        addedRRoom.FACULTY = facultyRepository.GetById(rroom.FacultyId);
                        retVal = transformer.TransformToDTO(addedRRoom);
                    }
                }
                catch (Exception) { throw;  }
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


        public int GetNumberOfFreeSeats(long id, DateTime ETA, DateTime ETD)
        {
            int numOfFreeSeats = 0;
            List<SEAT> schedule = new List<SEAT>();

            schedule = seatRepository.GetForReadingRoom(id);
            foreach (var seat in schedule)
            {
                if (seatRepository.IsFree(seat.SEAT_ID, ETA, ETD)) 
                {
                    numOfFreeSeats++;
                }
            }

            return numOfFreeSeats;
        }


        public List<ReadingRoomStatusDTO> GetStatuses(DateTime ETA, DateTime ETD)
        {
            List<ReadingRoomDTO> allReadingRooms;
            List<ReadingRoomStatusDTO> retVal = null;
            ReadingRoomStatusDTO status;
            FACULTY faculty;
            int freeSeats;
            UNIVERSITY uni;

            allReadingRooms = GetAll();

            if (CheckHelper.IsFilled(allReadingRooms)) 
            {
                retVal = new List<ReadingRoomStatusDTO>();
                foreach (ReadingRoomDTO rroom in allReadingRooms)
                {
                    faculty = facultyRepository.GetById(rroom.FacultyId);
                    uni = universityRepository.GetById(faculty.UNI_ID);
                    freeSeats = GetNumberOfFreeSeats(rroom.Id, ETA, ETD);

                    status = new ReadingRoomStatusDTO()
                    {
                        ReadingRoom = rroom,
                        FacultyId = faculty.FAC_ID,
                        FacultyName = faculty.FAC_NAME,
                        UniversityId = uni.UNI_ID,
                        UniversityName = uni.UNI_NAME,
                        FreeSeats = freeSeats
                    };

                    retVal.Add(status);
                }
            }

            return retVal;
        }
    }
}