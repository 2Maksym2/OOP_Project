using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DealHub
{
    public class DealHubSystem
    {
        private const string UsersFile = "D:\\labs\\Practice2\\OOP_Project\\DealHub\\DealHub\\bin\\Debug\\net8.0\\users.json";
        private const string AdminsFile = "D:\\labs\\Practice2\\OOP_Project\\DealHub\\DealHub\\bin\\Debug\\net8.0\\admins.json";
        private const string ComplaintsFile = "D:\\labs\\Practice2\\OOP_Project\\DealHub\\DealHub\\bin\\Debug\\net8.0\\complaints.json";

        private List<RegisteredUser> users = new();
        private List<Admin> admins = new();
        private List<Complaint> complaints = new();
        private List<Ad> allAds = new();

        public List<Ad> AllAds => allAds;
        public List<RegisteredUser> Users => users;
        public List<Admin> Admins => admins;
        public List<Complaint> Complaints => complaints;

        public static event Action<string> MessageForUser;


        public void AddComplaint(Complaint complaint)
        {
            complaints.Add(complaint);
            SaveData();
        }
        public User RegisterUser(string nickname, string password, bool? isAdmin)
        {
            // Перевірка наявності у списках користувачів та адміністраторів
            if (users.Any(u => u.Nickname == nickname) || admins.Any(a => a.Nickname == nickname))
            {
                throw new Exception("Користувач із таким нікнеймом вже існує.");
            }

            // Створення користувача
            if (isAdmin == true)
            {
                Admin newAdmin = new Admin(nickname, password);
                admins.Add(newAdmin);
                MessageForUser?.Invoke($"Користувач {nickname} успішно зареєстрований!");
                return newAdmin;
            }
            else
            {
                RegisteredUser newUser = new RegisteredUser(nickname, password);
                users.Add(newUser);
                MessageForUser?.Invoke($"Користувач {nickname} успішно зареєстрований!");
                return newUser;
            }


        }
        public User? Login(string nickname, string password)
        {
            User? user = users.FirstOrDefault(u => u.Nickname == nickname);

            if (user == null)
            {
                user = admins.FirstOrDefault(a => a.Nickname == nickname);
            }

            if (user == null)
            {
                throw new Exception("Користувача з таким ніком не знайдено.");
            }

            if (user.Password != password)
            {
                throw new Exception("Неправильний пароль.");
            }

            if (user is RegisteredUser user1 && user1.BlockedTime > DateTime.Now)
            {
                throw new Exception("Ваш акаунт заблоковано за ігнорування політики сайту.");
            }

            MessageForUser?.Invoke($"Вітаємо, {user.Nickname}!");
            return user;
        }
        public void LoadData()
        {
            try
            {
                if (File.Exists(UsersFile))
                {
                    string text = File.ReadAllText(UsersFile, Encoding.UTF8);
                    users = JsonConvert.DeserializeObject<List<RegisteredUser>>(text);
                }
            }
            catch (Exception)
            {
                throw new Exception("Помилка завантаження користувачів");
            }

            try
            {
                if (File.Exists(AdminsFile))
                {
                    string text = File.ReadAllText(AdminsFile, Encoding.UTF8);
                    admins = JsonConvert.DeserializeObject<List<Admin>>(text);
                }
            }
            catch (Exception)
            {
                throw new Exception("Помилка завантаження адміністраторів");
            }

            try
            {
                if (File.Exists(ComplaintsFile))
                {
                    string text = File.ReadAllText(ComplaintsFile, Encoding.UTF8);
                    complaints = JsonConvert.DeserializeObject<List<Complaint>>(text);
                }
            }
            catch (Exception)
            {
                throw new Exception("Помилка завантаження скарг");
            }

            allAds.Clear();
            foreach (var user in users)
            {
                allAds.AddRange(user.Ads);
            }

            Ad.UpdateTotalAdsCreated(allAds);
            Complaint.UpdateTotalComplaintsCreated(Complaints);
        }
        public void SaveData()
        {
            try
            {
                string usersJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(UsersFile, usersJson, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new Exception("Помилка збереження користувачів");
            }

            try
            {
                string adminsJson = JsonConvert.SerializeObject(admins, Formatting.Indented);
                File.WriteAllText(AdminsFile, adminsJson, Encoding.UTF8);
            }
            catch (Exception)
            {
                throw new Exception("Помилка збереження адміністраторів");
            }

            try
            {
                string complaintsJson = JsonConvert.SerializeObject(complaints, Formatting.Indented);
                File.WriteAllText(ComplaintsFile, complaintsJson, Encoding.UTF8);
            }
            catch (Exception)
            {
                throw new Exception("Помилка збереження скарг");
            }
        }
    }
}
