using System.Net.Mail;
using System.Net;
using System.Reflection;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Services
{
    public class LotteryService : ILotteryService
    {
        private readonly StoreContext context;
        private readonly ILotteryRepository lotteryRepository;
        private readonly IGiftsRepository giftRepository;
        private readonly ILogger<LotteryService> logger;
        private readonly IConfiguration _configuration;


        public LotteryService(ILotteryRepository lotteryRepository, IGiftsRepository giftRepository, ILogger<LotteryService> logger, StoreContext storeContext, IConfiguration configuration)
        {
            this.lotteryRepository = lotteryRepository;
            this.giftRepository = giftRepository;
            this.logger = logger;
            this.context = storeContext;
            _configuration = configuration;
        }

        public async Task<int> GetAllRevenueAsync()//-----
        {
            var lottery = await lotteryRepository.GetAllRevenueAsync();
            if (lottery == null) {
                logger.LogError("No Revenue found");
                throw new KeyNotFoundException("No Revenue found");
            }
            return lottery;
        }

        public async Task<List<DtoLottery>> GetAllWinnersAsync()
        {
            var winners = await lotteryRepository.GetAllWinnersAsync();
            if (winners == null)
            {
                logger.LogWarning("No winners found");
                throw new KeyNotFoundException("No winners found");
            }
                
            return winners.Select(MapToResponseDto).ToList();
        }

        public async Task<bool> LotteryAsync()
        {
            context.lotteries.RemoveRange(context.lotteries);
            await context.SaveChangesAsync();
            var gifts = await giftRepository.GetAllGiftsAsync();
            if (gifts == null)
            {
                logger.LogError("No gifts found");
                throw new KeyNotFoundException("No gifts found");
            }
            var random = new Random();
            foreach (var gift in gifts)
            {
                var users = await lotteryRepository.LotteryAsync(gift);
                if (users == null || users.Count == 0)
                    continue;
                var winner = users[random.Next(users.Count)];
                if (winner == null)
                {
                    logger.LogWarning("Winner was null for gift {GiftId}", gift.Id);
                    return false;
                }
                var added = await lotteryRepository.AddLotteryAsync(winner.Id, gift.Id);
                if (!added)
                {
                    logger.LogError("Failed to add lottery for user {UserId} and gift {GiftId}", winner.Id, gift.Id);
                    continue;
                }
                logger.LogInformation("User {UserId} won the gift {GiftId}", winner.Id, gift.Id);
                //await SendingEmailAsync(winner, gift.Name);
                //logger.LogInformation("Email sent to user {UserId} for winning gift {GiftId}", winner.Id, gift.Id);
            }
            return true;
        }

        public async Task SendingEmailAsync(Useres user, string nameGift)
        {
            if (user == null)
            {
                logger.LogError("User is null");
                throw new ArgumentNullException(nameof(user));
            }
            if (nameGift == null)
            {
                logger.LogError("Gift name is null");
                throw new ArgumentNullException(nameof(nameGift));
            }
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("gpt05567@gmail.com"); // מי שולח
                message.To.Add(user.Email); // מי מקבל
                message.Subject = "זכית בהגרלה!"; // נושא המייל
                message.IsBodyHtml = true;

                message.Body = $@"
            <div style='direction: rtl; text-align: center; font-family: Arial;'>

    <h2 style='color: #2E86C1;'>🎉 מזל טוב {user.FirstName}! 🎉</h2>

    <p style='font-size:18px;'>
        זכית במתנה:
        <strong style='color: #27AE60;'>{nameGift}</strong>
    </p>

    <hr style='width:50%;' />

    <p style='font-size:16px; color: gray;'>
        תודה שהשתתפת בהגרלה שלנו 💙
    </p>

</div>
";

                var email = _configuration["SmtpSettings:Email"];
                var password = _configuration["SmtpSettings:Password"];

                using var smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(email, password);
                await smtp.SendMailAsync(message); // שולח את המייל
                logger.LogInformation("Email sent successfully to {Email}", user.Email);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error sending email to {Email}", user.Email);
                throw;
            }
        }

            private static DtoLottery MapToResponseDto(Lottery lottery)
            {
                return new DtoLottery
                {
                    User = lottery.User,
                    GiftName = lottery.Gift.Name,
                    UserName = lottery.User.FirstName + " " + lottery.User.LastName,
                };
            }

        public async Task<bool> LotteryDone()
        {
            return await lotteryRepository.LotteryDone();
        }
    }
    }



