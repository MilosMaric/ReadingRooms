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
using DAL.Transformers.Implementations;

namespace API.Services.Implementations
{
    public class ReservationServiceImpl : IReservationService
    {
        private IReservationRepository reservationRepository = new ReservationRepositoryImpl();
        private ISeatRepository seatRepository = new SeatRepositoryImpl();
        private IUserRepository userRepository = new UserRepositoryImpl();

        private ReservationTransformer transformer = new ReservationTransformer();
        private SeatTransformer seatTransformer = new SeatTransformer();
        private UserTransformer userTransformer = new UserTransformer();

        public ReservationDTO Add(ReservationDTO res)
        {
            RESERVATION resToAdd, addedRes;
            ReservationDTO retVal;

            retVal = null;

            if (CheckHelper.IsFilled(res))
            {
                try
                {
                    resToAdd = transformer.TransformFromDTO(-1, res);
                    addedRes = reservationRepository.Add(resToAdd);
                    
                    if (CheckHelper.IsFilled(addedRes))
                    {
                        retVal = FinalizeDTOTransformation(transformer.TransformToDTO(addedRes));
                    }
                }
                catch (Exception) { }
            }

            return retVal;        
        }

        public ReservationDTO Update(long id, ReservationDTO res)
        {
            RESERVATION resToUpdate, updatedRes, oldRes;
            ReservationDTO retVal;

            retVal = null;
            oldRes = reservationRepository.GetById(id);

            if (CheckHelper.IsFilled(res) && CheckHelper.IsFilled(oldRes))
            {
                try
                {
                    resToUpdate = transformer.TransformFromDTO(id, res);
                    resToUpdate.USER = oldRes.USER;
                    resToUpdate.SEATs = oldRes.SEATs;

                    updatedRes = reservationRepository.Update(resToUpdate);

                    if (CheckHelper.IsFilled(updatedRes))
                    {
                        retVal = FinalizeDTOTransformation(transformer.TransformToDTO(updatedRes));
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
                reservationRepository.Delete(id);
                isDeleted = true;
            }
            catch (Exception) { }

            return isDeleted;        
        }

        public List<ReservationDTO> GetAll()
        {
            List<ReservationDTO> retVal = null;
            List<RESERVATION> entries;

            try
            {
                entries = reservationRepository.GetAll();
                if (CheckHelper.IsFilled(entries))
                {
                    retVal = FinalizeDTOTransformation(transformer.TransformToDTO(entries));
                }
            }
            catch (Exception) { }

            return retVal;        }

        public ReservationDTO GetById(long id)
        {
            RESERVATION entry;
            ReservationDTO retVal = null;

            try
            {
                entry = reservationRepository.GetById(id);
                if (CheckHelper.IsFilled(entry))
                {
                    retVal = FinalizeDTOTransformation(transformer.TransformToDTO(entry));
                }
            }
            catch (Exception) { }

            return retVal;        
        }

        public List<ReservationDTO> GetStudentsReservations(long id)
        {
            List<ReservationDTO> retVal = null;
            List<RESERVATION> entries;

            try 
	        {	        
		        entries = reservationRepository.GetStudentsReservations(id);
                if(CheckHelper.IsFilled(entries))
                {
                    retVal = FinalizeDTOTransformation(transformer.TransformToDTO(entries));
                }
	        }
	        catch (Exception) {}

            return retVal;
        }

        public List<ReservationDTO> GetReservationsForReadingRoom(long readingRoomId)
        {
            List<ReservationDTO> retVal = null;
            List<RESERVATION> entries;

            try 
	        {	        
		        entries = reservationRepository.GetReservationsForReadingRoom(readingRoomId);
                if(CheckHelper.IsFilled(entries))
                {
                    retVal = FinalizeDTOTransformation(transformer.TransformToDTO(entries));
                }
	        }
	        catch (Exception) {}

            return retVal;        
        }

        public ReservationDTO FinalizeDTOTransformation(ReservationDTO dto)
        {
            USER user = userRepository.GetById(dto.User.Id);
            dto.User = userTransformer.TransformToDTO(user);

            RESERVATION reservation = reservationRepository.GetById(dto.Id);
            if(CheckHelper.IsFilled(reservation) && CheckHelper.IsFilled(reservation.SEATs))
            {
                dto.Seats = new List<SeatDTO>();
                foreach (SEAT seat in reservation.SEATs)
	            {
                    SeatDTO seatDTO = seatTransformer.TransformToDTO(seat);
                    if(CheckHelper.IsFilled(seatDTO))
                    {
		                dto.Seats.Add(seatDTO);
                    }
	            }
            }

            return dto;
        }
    

        public List<ReservationDTO> FinalizeDTOTransformation(List<ReservationDTO> dto)
        {
 	        List<ReservationDTO> reservations = null; 

            if(CheckHelper.IsFilled(dto))
            {
                reservations = new List<ReservationDTO>();
                foreach (ReservationDTO res in dto)
	            {
		            reservations.Add(FinalizeDTOTransformation(res));
	            }
            }

            return reservations;
        }
    }
}