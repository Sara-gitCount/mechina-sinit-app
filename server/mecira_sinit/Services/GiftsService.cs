using System.Collections.Generic;
using System.Drawing;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class GiftsService : IGiftsService
    {
        private readonly IGiftsRepository giftsRepository;
        private readonly ILogger<GiftsService> logger;

        public GiftsService(IGiftsRepository GiftsRepository, ILogger<GiftsService> logger)
 {
            giftsRepository = GiftsRepository;
            this.logger = logger;
        }
        
        public async Task<List<DtoGifts>> GetAllGiftsAsync()
        {
            var gifts=await giftsRepository.GetAllGiftsAsync();
            if(gifts==null)
                logger.LogWarning("No gifts found in the repository.");
            return gifts.Select(g => new DtoGifts
            {
                Id=g.Id,
                Name = g.Name,
                Description = g.Description,
                Image = g.Image,
                Price = g.Price,
                DonorId = g.DonorId,
                CategoryName = g.Category.Name
            }
            ).ToList();
            //return gifts.Select(MapToResponseDto).ToList();
        }

        public async Task<bool> UpdateGiftAsync(DtoGiftsUpdate gift,int id) //למה לא DTO?
        {
            if (id <= 0)
            {
                logger.LogError("UpdateGiftAsync called with invalid id: 0");
                throw new ArgumentException("Invalid id");
            }
            if (gift == null)
            {
                logger.LogError("UpdateGiftAsync called with null gift object");
                throw new ArgumentNullException(nameof(gift));
            }
            var g= await giftsRepository.GetByIdAsync(id);
            if (g == null)
            {
                logger.LogError($"Gift with id{id} not found");
                throw new KeyNotFoundException($"Gift with id {id} not found");
            }
            var gNew = new Gifts()
            {
                Id = id,
                Name = gift.Name,
                Description = gift.Description,
                Image = gift.Image,
                Price = gift.Price,
                CategoryId = gift.CategoryId,
                DonorId = gift.DonorId,
            };
            var updated= await giftsRepository.UpdateGiftAsync(gNew);
            if (updated == null)
            {
                logger.LogError($"Failed to update gift with id{id}");
                throw new KeyNotFoundException($"Faild to update gift with id{id}");
            }
            logger.LogInformation($"Gift with id{id} updated successfully");
            return updated;
        }

        public async Task<bool> DeleteGiftAsync(int id)
        {
            if (id <= 0)
            {
                logger.LogError("DeleteGiftAsync called with invalid id: 0");
                throw new ArgumentException("Invalid id");
            }
           var deleted= await giftsRepository.DeleteGiftAsync(id);
            if (!deleted)
            {
                logger.LogError($"Gifts with id {id} was not found");  
                throw new KeyNotFoundException($"Gifts with id {id} was not found");
            }
            logger.LogInformation($"Gifts with id {id} was deleted successfully");
            return deleted;
        }

        public async Task<bool> CreateGiftAsync(DtoGiftsUpdate gift)
        {
            if (gift == null)
            {
                logger.LogError("CreateGiftAsync called with null gift object");
                throw new ArgumentNullException(nameof(gift));
            }
            var g = new Gifts
            {
                Name = gift.Name,
                Description = gift.Description,
                Price= gift.Price,
                DonorId= gift.DonorId,
               Image= gift.Image,
                CategoryId= gift.CategoryId,
            };
           var add= await giftsRepository.CreateGiftAsync(g);
            if (!add)
            {
                logger.LogError("Failed to add new gift to the repository");
                throw new Exception("Errors adding gift");
            }
            logger.LogInformation("New gift added successfully to the repository"); 
            return add;
        }

        public async Task<DtoDonors> GetDonorsAsync(int id)
        {
            if (id <= 0)
            {
                logger.LogError("GetDonorsAsync called with invalid id: 0");
                throw new ArgumentException("Invalid id");
            }
            var d= await giftsRepository.GetDonorsAsync(id);
            if (d == null)
            {
                logger.LogError($"Donor with id {id} not found");
                throw new KeyNotFoundException($"Donor with id {id} not found");
            }
            return MapToResponseDto(d);
        }

        public async Task<DtoGiftsUpdate> GetGiftsByNameAsync(string name)
        {
            if (name == null || name == "")
            {
                logger.LogError("GetGiftsByNameAsync called with null or empty name");
                throw new ArgumentNullException(nameof(name));
            }
            var gift = await giftsRepository.GetGiftsByNameAsync(name);
            if (gift == null)
            {
                logger.LogError($"Gift with name {name} not found");
                return null;           
            }
            return MapToResponseDto(gift);
        }

        public async Task<List<DtoGifts>> GetGiftsByDonorNameAsync(string donorName)
        {
            if (donorName == null || donorName == "")
            {
                logger.LogError("GetGiftsByDonorNameAsync called with null or empty donorName");
                throw new ArgumentNullException(nameof(donorName));
            }
            var gifts = await giftsRepository.GetGiftsByDonorNameAsync(donorName);
            if (gifts == null)
                logger.LogError($"Gift with donorName {donorName} not found");
              //  throw new KeyNotFoundException($"gift with donorName {donorName} not found");
            return gifts.Select(MapToResponseDto_).ToList();
        }

        public async Task<List<DtoGifts_D>> GetGiftsByNumOfUsersAsync(int numOfUsers)
        {
            if (numOfUsers <= 0)
            {
                logger.LogError("GetGiftsByNumOfUsersAsync called with non-positive numOfUsers");   
                throw new ArgumentException("Nuber of users must be greater than zero", nameof(numOfUsers));
            }
            var gifts =await giftsRepository.GetGiftsNumOfUsersByAsync(numOfUsers);
           // if (gifts == null || gifts.Count==0) throw new KeyNotFoundException($"No gift found  for {numOfUsers} users");

            return gifts.Select(MapToResponseDto_D).ToList();
        }
        
        public async Task<List<DtoGifts>> GetOrderByPrice_CategoryAsync()
        {
            var gifts=await giftsRepository.GetOrderByPrice_CategoryAsync();
            if(gifts == null)
                logger.LogWarning("No gifts found in the repository.");
            return gifts.Select(g => new DtoGifts
            {
                Name = g.Name,
                Description = g.Description,
                Image = g.Image,
                Price = g.Price,
                DonorId = g.DonorId,
                CategoryName = g.Category.Name,
            }
            ).ToList();
        }

        public async Task<DtoGiftsUpdate> GetByIdAsync(int id)
        {
            if(id <= 0)
            {
                logger.LogError("GetById called with invalid id: 0");
                throw new ArgumentException("Invalid id");
            }
            var gift= await giftsRepository.GetByIdAsync(id);
            if (gift == null)
            {
                logger.LogError($"Gift with id{id} not found");
                throw new KeyNotFoundException($"Gift with id {id} not found");
            }   
            return MapToResponseDto(gift);
        }

        private static DtoGifts_D MapToResponseDto_D(Gifts gift)
        {
            return new DtoGifts_D
            {
                Name = gift.Name,
                Description = gift.Description,
                Image = gift.Image,
                CategoryId = gift.CategoryId,
            };
        }

        private static DtoGifts  MapToResponseDto_(Gifts gift)
        {
            return new DtoGifts
            {
                Id = gift.Id,
                Name = gift.Name,
                Description = gift.Description,
                Image = gift.Image,
                Price = gift.Price,
                CategoryName = gift.Category?.Name,
                 DonorId= gift.DonorId
            };
        }

        private static DtoGiftsUpdate MapToResponseDto(Gifts gift)
        {
            return new DtoGiftsUpdate
            {
                Name = gift.Name,
                Description = gift.Description,
                Image = gift.Image,
                CategoryId = gift.CategoryId,
                Price = gift.Price,
                 DonorId= gift.DonorId,
            };
        }
        private static DtoDonors MapToResponseDto(Donors donor)//הופך מ DONOR ל DTODONOR
        {
            return new DtoDonors
            {
                FirstName = donor.FirstName,
                LastName = donor.LastName,
                Donations = { },// donor.Donations,
                Phone = donor.Phone,
                Email = donor.Email,
            };
        }

    }
}
