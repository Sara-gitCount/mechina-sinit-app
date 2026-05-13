using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DonorsService : IDonorsService
    {
        private readonly IDonorsRepository donorsRepository; 
        private readonly ILogger<DonorsService> logger;


        public DonorsService(IDonorsRepository donorsRepository, ILogger<DonorsService> logger)
        {
            this.donorsRepository = donorsRepository;
            this.logger = logger;
        }
        public async Task<bool> CreateDonorAsync(DtoDonorsCreate donor)
        {
            if (donor == null) { 
            logger.LogError("Faild to create a null donor");
            throw new ArgumentNullException(nameof(donor));
        }
            var add= await donorsRepository.CreateDonorAsync(MapToResponseDto(donor));
            if (!add)
            {
                logger.LogError("Faild to create donor");
                throw new Exception("Errors adding donor");
            }
            logger.LogInformation("Donor created");
            return add;
        }

        public async Task<bool> DeleteDonorAsync(int id)
        {
            if (id == 0)
            {
                logger.LogError("faild to delete donor with invalid id 0");
                throw new ArgumentException("Invalid id");
            }
            var deleted = await donorsRepository.DeleteDonorAsync(id);
            if (!deleted)
            {
                logger.LogError("Donor with id {Id} not found for deletion", id);
                throw new KeyNotFoundException($"Donor with id {id} was not found");
            }
            logger.LogInformation("Donor with id {Id} deleted successfully", id);
            return deleted;
        }

        public async Task<List<DtoDonors>> GetAllDonors()
        {
            var donors = await donorsRepository.GetAllDonorsAsync();
            if (donors == null)
                logger.LogWarning("No donors found in GetAllDonors");
            return donors.Select(MapToResponseDto).ToList();
        }

        public async Task<bool> UpdateDonorAsync(DtoDonorsUpdate donor, int id)
        {
            if (id == 0)
            {
                logger.LogError("Faild to update donor with invalid id 0");
                throw new ArgumentException("Invalid id");
            }
            if (donor == null)
            {
                logger.LogError("Faild to update a null donor");
                throw new ArgumentNullException(nameof(donor));
            }
            var d=await donorsRepository.GetDonorByIdAsync(id);
            if (d == null)
            {
                logger.LogError("Donor with id {Id} not found for update", id);
                throw new KeyNotFoundException($"Donor with id {id} was not found");
            }
            d.Phone=donor.Phone;
            d.FirstName=donor.FirstName;
            d.LastName=donor.LastName;  
            d.Email=donor.Email;
            var updated= await donorsRepository.UpdateDonorAsync(d);
            if (updated == null)
            {
                logger.LogError("Faild to update donor with id {Id}", id);
                throw new KeyNotFoundException($"Faild to update donor with id{id}");
            }
            return updated;
        }


        public async Task<DtoDonorsCreate> GetByNameAsync(string name)
        {
            if (name == null || name == "")
            {
                logger.LogError("Faild to get donor with null or empty name");
                throw new ArgumentNullException(nameof(name));
            }
            var d = await donorsRepository.GetByNameAsync(name);
            if (d == null)
            {
                logger.LogWarning("Donor with name {Name} not found", name);
                throw new KeyNotFoundException($"Faild to get donor with name {name}");
            }
            // return MapToResponseDtoDonorCreate(d);
            try
            {
                return MapToResponseDtoDonorCreate(d);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed mapping donor to DTO");
                throw;
            }

        }

        public async Task<DtoDonors> GetByEmailAsync(string email)
        {
            if (email == null || email == "")
            {
                logger.LogError("Faild to get donor with null or empty email");
                throw new ArgumentNullException(nameof(email));
            }
            var d =await donorsRepository.GetByEmailAsync(email);
            if (d == null)
            {
                logger.LogWarning("Donor with email {Email} not found", email);
                throw new KeyNotFoundException($"Faild to get donor with email {email}");
            }
            return MapToResponseDto(d);
        }

        public async Task<DtoDonors> GetByGiftAsync(string nameGift)
        {

            if (nameGift=="")
            {
                logger.LogError("Faild to get donor with invalid nameGift empty");    
                throw new ArgumentNullException(nameof(nameGift));
            }
            var d = await donorsRepository.GetByGiftAsync(nameGift);
            if (d == null)
            {
                logger.LogWarning("Donor with nameGift {nameGift} not found", nameGift);
                throw new KeyNotFoundException($"Faild to get donor with nameGift {nameGift}");
            }
            return MapToResponseDto(d);
        }

        public async Task<bool> AddDonotationAsync(int giftId, int donorId)
        {
            if(giftId<=0)
            {
                logger.LogError("invalid giftId");
                throw new ArgumentNullException(nameof(giftId));
            }
            if(donorId <= 0)
            {
                logger.LogError("invalid donorId");
                throw new ArgumentException("invalid donorId",nameof(donorId));
            }
            var result=await donorsRepository.AddDonotationAsync(giftId, donorId);
            if (!result)
            {
                logger.LogInformation("faild to add donation to donor with id ", donorId);
                throw new KeyNotFoundException($"faild to add donation to donor with id {donorId}" );
            }
            return result;
        }

        public async Task<Donors> GetDonorByIdAsync(int id)
        {
            if (id <= 0)
            {
                logger.LogError("invalid id");
                throw new ArgumentException("invalid id", nameof(id));
            }
            var donor = await donorsRepository.GetDonorByIdAsync(id);
            if (donor == null)
            {
                logger.LogWarning("Donor with id {Id} not found", id);
                throw new KeyNotFoundException($"Donor with id {id} was not found");
            }
            return donor;
        }

        private static DtoDonors MapToResponseDto(Donors donor)//הופך מ DONOR ל DTODONOR
        {
            return new DtoDonors
            {
                FirstName = donor.FirstName,
                LastName = donor.LastName,
                Phone = donor.Phone,
                Email = donor.Email,
                Donations = donor.Donations?.Select(g => new DtoGifts_D
                  {
                     Name = g.Name,
                     Description = g.Description,
                     Image = g.Image,
                     CategoryId = g.CategoryId  
                }).ToList() ?? new List<DtoGifts_D>()
            };
        }
        private static Donors MapToResponseDto(DtoDonorsCreate donor)//הופך מ DONOR ל DTODONOR
        {
            return new Donors
            {
                Id = donor.Id,
                FirstName = donor.FirstName,
                LastName = donor.LastName,
                Phone = donor.Phone,
                Email = donor.Email,
                Donations = new List<Gifts>(), 
            };
        }

        private static DtoDonorsCreate MapToResponseDtoDonorCreate(Donors donor)//הופך מ DONOR ל DTODONOR
        {
            return new DtoDonorsCreate
            {
                Id = donor.Id,
                FirstName = donor.FirstName,
                LastName = donor.LastName,
                Phone = donor.Phone,
                Email = donor.Email
            };
        }

        private static DtoDonorsUpdate MapToResponseDtoDonorUpdate(Donors donor)//הופך מ DONOR ל DTODONOR
        {
            return new DtoDonorsUpdate
            {
                FirstName = donor.FirstName,
                LastName = donor.LastName,
                Phone = donor.Phone,
                Email = donor.Email
            };
        }
    }
}
