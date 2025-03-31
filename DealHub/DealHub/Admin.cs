using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub
{
    public class Admin : User
    {
        public static event Action<string> MessageForUser;
        public Admin(string nickname, string password) : base(nickname, password)
        {
            IsAdmin = true;
        }

        public bool ViewComplaints(DealHubSystem system)
        {
            if (system.Complaints.Count == 0)
            {
                throw new Exception("\nСкарги не знайдено");
            }

            MessageForUser?.Invoke("\nСписок скарг:");
            foreach (var complaint in system.Complaints)
            {
                string target = complaint.AdTitle == null ? $"Користувач: {complaint.ReceiverName} \nДеталі: {complaint.Description}" : $"Оголошення:  [adId: {complaint.AdId}] {complaint.AdTitle} \nДеталі: {complaint.Description}";
                MessageForUser?.Invoke($"[{complaint.Id}] {complaint.Title} - {target}");
            }
            return true;
        }

        public bool DeleteComplaint(DealHubSystem system, int complaintId)
        {
            Complaint? complaint = system.Complaints.FirstOrDefault(c => c.Id == complaintId);

            if (complaint == null)
            {
                throw new Exception("\nСкарга не знайдена");
            }

            system.Complaints.Remove(complaint);
            MessageForUser?.Invoke($"Скарга успішно розглянута і видалена.");
            return true;
        }

        public void DeleteAd(DealHubSystem system, int adId)
        {
            Ad? adToDelete = system.AllAds.FirstOrDefault(a => a.Id == adId);
            if (adToDelete == null)
            {
                throw new Exception("\nОголошення не знайдено");
            }

            // Видаляємо оголошення у власника
            RegisteredUser? owner = system.Users.FirstOrDefault(u => u.Nickname == adToDelete.OwnerNickname);
            if (owner != null)
            {
                owner.Ads.Remove(adToDelete);
                MessageForUser?.Invoke($"Оголошення {adToDelete.Title} видалено");
            }

            // Видаляємо оголошення з системи
            system.AllAds.Remove(adToDelete);

        }
        public void BanUser(DealHubSystem system, string username, DateTime EndBan)
        {
            RegisteredUser? userToBan = system.Users.FirstOrDefault(a => a.Nickname == username);
            if (userToBan == null)
            {
                throw new Exception("\nКористувача не знайдено");
            }
            else if (userToBan.BlockedTime > DateTime.Now)
            {
                throw new Exception("Користувача вже заблоковано");
            }
            // Блокуємо користувача та деактивуємо його оголошення
            else
            {
                userToBan.BlockedTime = EndBan;
                foreach (var ad in userToBan.Ads)
                {
                    ad.IsActive = false;
                }
                MessageForUser?.Invoke($"Користувача {userToBan.Nickname} заблоковано.");
            }
            // Оновлюємо дані 
            system.SaveData();

        }
        public override void ShowMenu()
        {
            MessageForUser?.Invoke("Меню");
            MessageForUser?.Invoke("1.Проглянути оголошення");
            MessageForUser?.Invoke("2.Проглянути користувачів");
            MessageForUser?.Invoke("3.Проглянути скарги");
            MessageForUser?.Invoke("4.Вийти");
            MessageForUser?.Invoke("0.Закрити програму");

        }
    }
}
