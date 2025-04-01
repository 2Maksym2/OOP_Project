using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DealHub.IManageAds;

namespace DealHub
{
    public class RegisteredUser : User, IManageAds
    {
        private DateTime blockedTime;
        public DateTime BlockedTime
        {
            get { return blockedTime; }
            set
            {

                if (BlockedTime > DateTime.Now)
                {
                    throw new Exception("Користувача вже заблоковано.");
                }

                blockedTime = value;
            }
        }
        private List<Review> reviews = new List<Review>();
        private List<Ad> ads = new List<Ad>();
        private List<Message> messagesSent = new List<Message>();
        private List<Message> messagesReceived = new List<Message>();


        public static event Action<string> MessageForUser;
        public List<Ad> Ads => ads;
        public List<Message> MessagesSent => messagesSent;
        public List<Message> MessagesReceived => messagesReceived;
        public List<Review> Reviews => reviews;


        public RegisteredUser(string nickname, string password) : base(nickname, password) { }


        public List<Ad> ViewMyAds()
        {
            // Копіюємо список оголошень користувача для індексації
            var userAds = this.Ads.ToList();
            return userAds;
        }
        public void OrderAd(Ad ad, string ContactDetails, RegisteredUser owner)
        {
            if (ad.IsActive == false)
            {
                throw new Exception("\nОголошення недоступне");
            }
            MessageForUser?.Invoke("\nВаше замовлення оформлено!");
            MessageForUser?.Invoke($"\n{ContactDetails}");
            if (owner != null)
            {
                string messageContent = $"\nДобрий день! Я оформив замовлення на {ad.Title}." + $"\n" + $"{ContactDetails}";
                this.SendMessage(owner, messageContent);
                MessageForUser?.Invoke("\nПовідомлення власнику оголошення відправлено!");
                ad.IsActive = false;
            }
            else
            {
                throw new Exception("\nПомилка: власника оголошення не знайдено!\"");
            }
        }
        public void CreateAd(DealHubSystem system, string title, string description, Category category, string image, double price, RegisteredUser user)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
            {
                throw new Exception("\nОголошення має містити заголовок і опис.");
            }

            int newId = system.AllAds.Count > 1 ? system.AllAds.Max(a => a.Id) + 1 : 1;
            Ad newAd = new Ad(title, description, category, image, price, user.Nickname);
            Ads.Add(newAd);
            system.AllAds.Add(newAd);

            RegisteredUser.MessageForUser?.Invoke($"Оголошення \"{title}\" успішно створене!");
        }
        public void EditAd(int adId, string newTitle, string newDescription)
        {
            Ad? ad = Ads.FirstOrDefault(a => a.Id == adId);
            if (ad == null)
            {
                throw new Exception("\nОголошення не знайдено.");
            }

            ad.Title = newTitle;
            ad.Description = newDescription;

            RegisteredUser.MessageForUser?.Invoke($"Оголошення \"{newTitle}\" успішно оновлене!");
        }
        public void DeleteAd(DealHubSystem system, int adId)
        {
            Ad? ad = Ads.FirstOrDefault(a => a.Id == adId);
            if (ad == null)
            {
                throw new Exception("\nОголошення не знайдено.");
            }

            Ads.Remove(ad);
            system.AllAds.Remove(ad);

            RegisteredUser.MessageForUser?.Invoke("Оголошення видалено.");
        }
        public void SendMessage(RegisteredUser receiver, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new Exception("\nПовідомлення не може бути порожнім.");
            }

            Message message = new Message(this.Nickname, receiver.Nickname, content);
            MessagesSent.Add(message);
            receiver.MessagesReceived.Add(message);
            MessageForUser?.Invoke($"Повідомлення надіслано {receiver.Nickname}: \"{content}\"");
        }
        public List<User> ViewChats(int n, DealHubSystem system)
        {
            List<User> buyerChats = new();
            List<User> sellerChats = new();

            var allUsersNicknames = MessagesReceived.Select(m => m.senderNickname)
                .Concat(MessagesSent.Select(m => m.receiverNickname))
                .Distinct()
                .ToList();

            var allUsers = allUsersNicknames
                .Select(nickname => system.Users.FirstOrDefault(u => u.Nickname == nickname)) 
                .Where(user => user != null)
                .ToList();

            foreach (var user in allUsers)
            {
                var chatMessages = MessagesReceived.Where(m => m.senderNickname == user.Nickname)
                    .Concat(MessagesSent.Where(m => m.receiverNickname == user.Nickname))
                    .OrderBy(m => m.SentAt)
                    .ToList();

                if (chatMessages.Count == 0) continue;

                bool iAmSeller = chatMessages.First().receiverNickname == this.Nickname;

                if (iAmSeller)
                    sellerChats.Add(user);
                else
                    buyerChats.Add(user);
            }
            if (n == 1) return buyerChats;
            if (n == 2) return sellerChats;

            return new List<User>();
        }
        public void ViewMessages(User receiver, User sender)
        {
            if (receiver == null || sender == null)
            {
                throw new Exception("\nПомилка: один з користувачів не знайдений.");
            }

            var chatMessages = MessagesReceived
                .Where(m => m.senderNickname == receiver.Nickname)
                .Concat(MessagesSent.Where(m => m.receiverNickname == receiver.Nickname))
                .OrderBy(m => m.SentAt)
                .ToList();

            MessageForUser.Invoke($"\n Чат між {sender.Nickname} та {receiver.Nickname}:");
            foreach (var message in chatMessages)
            {
                string name = message.senderNickname == sender.Nickname ? "Ви" : receiver.Nickname;
                MessageForUser.Invoke($"[{message.SentAt}] {name}: {message.Content}");
            }
        }
        public void LeaveReview(RegisteredUser receiver, string content)
        {
            if (string.IsNullOrWhiteSpace(content) || content.Length < 5)
            {
                throw new Exception("\nВідгук повинен містити мінімум 5 символів.");
            }

            Review review = new Review(this.Nickname, content);
            receiver.Reviews.Add(review);
            MessageForUser?.Invoke($"Відгук для {receiver.Nickname} успішно додано!");
        }
        public void ViewReviews()
        {
            MessageForUser?.Invoke($"\nВідгуки про {Nickname}:");

            if (reviews.Count == 0)
            {
                throw new Exception("\nНемає відгуків.");
            }

            foreach (var review in reviews)
            {
                MessageForUser?.Invoke($"[{review.CreatedAt}] {review.AuthorName}: {review.Content}");
            }
        }
        public Complaint SendComplaint(DealHubSystem system, RegisteredUser? receiver, Ad? receiverAd, string description)
        {
            if (receiver == null && receiverAd == null)
            {
                throw new Exception("\nВи повинні обрати або користувача, або оголошення для скарги.");
            }
            if (string.IsNullOrWhiteSpace(description) || description.Length < 10)
            {
                throw new Exception("\nОпис скарги повинен містити мінімум 10 символів.");
            }

            Complaint complaint = new Complaint(this, receiver, receiverAd, description);
            system.AddComplaint(complaint);
            MessageForUser?.Invoke($"Скарга подана від {Nickname} на {(receiver != null ? $"користувача {receiver.Nickname}" : $"оголошення {receiverAd.Title}")}.");
            return complaint;
        }
        public override void ShowMenu()
        {
            MessageForUser?.Invoke("Меню");
            MessageForUser?.Invoke("1.Проглянути оголошення");
            MessageForUser?.Invoke("2.Робота з моїми оголошеннями");
            MessageForUser?.Invoke("3.Чат");
            MessageForUser?.Invoke("4.Переглянути відгуки");
            MessageForUser?.Invoke("5.Вийти");
            MessageForUser?.Invoke("0.Закрити програму");
        }

    }
}
