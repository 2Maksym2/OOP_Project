using DealHub;
using System.Text.RegularExpressions;
using System.Text;

namespace DealHub
{
    class Program
    {

        static void Main()
        {


            DealHubSystem system = new();
            system.LoadData();

            Console.OutputEncoding = UTF8Encoding.UTF8;
            RegisteredUser.MessageForUser += message => Console.WriteLine(message);
            DealHubSystem.MessageForUser += message => Console.WriteLine(message);
            Admin.MessageForUser += message => Console.WriteLine(message);
            Guest.MessageForUser += message => Console.WriteLine(message);
            bool isStart = true, flag1 = true;
            User? currentUser = null;
            while (isStart)
            {
                Console.WriteLine("");
                Console.WriteLine("DealHub найкращий сервіс оголошень");

                if (currentUser == null)
                {
                    Guest guest = new Guest();
                    guest.ShowMenu();
                }
                else if (currentUser is Admin admin)
                {
                    admin.ShowMenu();
                }
                else if (currentUser is RegisteredUser registeredUser)
                {
                    registeredUser.ShowMenu();
                }

                Console.Write("\nВиберіть дію: ");

                string choice = Console.ReadLine();


                switch (choice)
                {

                    case "1":
                        if (currentUser is Admin administrator)
                        {
                            AdminViewAds(system, administrator);
                        }
                        else
                        {
                            ViewAds(system, currentUser, choice, flag1);
                        }
                        break;

                    case "2":

                        if (currentUser == null)
                        {
                            currentUser = GuestRegistration(system, currentUser);
                            flag1 = true;
                        }
                        else if (currentUser is Admin admin)
                        {
                            AdminViewUsers(system, admin);
                        }
                        else if (currentUser is RegisteredUser regUser)
                        {
                            MyAds(system, regUser);
                        }
                        break;

                    case "3":
                        if (currentUser == null)
                        {
                            currentUser = GuestLogIn(system);
                            flag1 = true;
                        }
                        else if (currentUser is Admin admin2)
                        {
                            AdminViewComplaints(system, admin2);
                        }
                        else if (currentUser is RegisteredUser regUser1)
                        {
                            UserChats(system, regUser1);
                        }
                        break;

                    case "4":
                        if (currentUser == null)
                        {
                            Console.WriteLine("Помилка. Некоректний вибір!");
                            break;
                        }
                        else if (currentUser is Admin admin2)
                        {
                            currentUser = null;
                        }
                        else if (currentUser is RegisteredUser regUser1)
                        {
                            try
                            {
                                regUser1.ViewReviews();
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                        }
                        break;

                    case "5":
                        if (currentUser == null || currentUser is Admin admin1)
                        {
                            Console.WriteLine("Помилка. Некоректний вибір!");
                            break;
                        }
                        else if (currentUser is RegisteredUser regUser1)
                        {
                            currentUser = null;
                        }
                        break;

                    case "0":
                        system.SaveData();
                        isStart = false;
                        break;

                    default:
                        Console.WriteLine("Помилка. Некоректний вибір!");
                        break;
                }
            }
        }
        public static void AdminViewAds(DealHubSystem system, Admin administrator)
        {
            int action;
            while (true)
            {
                try
                {
                    foreach (var ad in system.AllAds)
                    {
                        Console.WriteLine($"\n[id:{ad.Id}]  {ad.Title} \n{ad.Description}");
                    }
                    Console.WriteLine("\n1. Видалити оглошення \n0. Назад");

                    while (true)
                    {
                        Console.Write("\nОберіть дію: ");
                        if (int.TryParse(Console.ReadLine(), out action) && action > -1 && action < 2)
                            break;
                        Console.WriteLine("Помилка! Введіть число 1 або 0.");
                    }
                    if (action == 0) break;

                    else if (action == 1)
                    {
                        var userAds = system.AllAds;

                        int adId = -1;
                        try
                        {
                            Console.Write("\nОберіть оголошення для видалення (id у списку): ");
                            if (!int.TryParse(Console.ReadLine(), out adId) && system.AllAds.Any(a => a.Id == adId))
                                Console.WriteLine("Помилка! Введіть коректний номер зі списку.");
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        Ad? adToDelete = system.AllAds.FirstOrDefault(a => a.Id == adId);
                        administrator.DeleteAd(system, adId);
                        system.SaveData();
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }

        }
        public static void AdminViewUsers(DealHubSystem system, Admin administrator)
        {
            int action;
            while (true)
            {
                try
                {
                    Console.WriteLine("Користувачі:");
                    foreach (var user in system.Users)
                    {
                        if (user.BlockedTime > DateTime.Now)
                        {
                            Console.WriteLine($"\n {user.Nickname} (Заблокований)");
                        }
                        else
                            Console.WriteLine($"\n {user.Nickname}");
                    }
                    Console.WriteLine("\n1.Заблокувати користувача \n0. Назад");
                    while (true)
                    {
                        Console.Write("\nОберіть дію: ");
                        if (int.TryParse(Console.ReadLine(), out action) && action > -1 && action < 2)
                            break;
                        Console.WriteLine("Помилка! Введіть число 1 або 0.");
                    }
                    if (action == 0) break;
                    else if (action == 1)
                    {
                        Console.WriteLine("Введіть ім'я користувача:");
                        string name = Console.ReadLine();

                        int banDuration;
                        while (true)
                        {
                            Console.WriteLine("Введіть час блокування в хвилинах:");
                            if (int.TryParse(Console.ReadLine(), out banDuration) && banDuration > 0)
                                break;
                            Console.WriteLine("Помилка! Введіть коректне число (мінімум 1 хвилина).");
                        }

                        DateTime banEndTime = DateTime.Now.AddMinutes(banDuration);
                        administrator.BanUser(system, name, banEndTime);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }

        }
        public static void AdminViewComplaints(DealHubSystem system, Admin administrator)
        {
            int action;
            while (true)
            {
                try
                {
                    administrator.ViewComplaints(system);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); return; }

                Console.WriteLine("\n1.Видалити скаргу зі списку \n0. Назад");

                while (true)

                {
                    Console.Write("\nОберіть дію: ");
                    if (int.TryParse(Console.ReadLine(), out action) && action > -1 && action < 2)
                        break;
                    Console.WriteLine("Помилка! Введіть число 1 або 0.");

                }

                if (action == 0) break;

                if (action == 1)

                {

                    int Choice = -1;

                    try
                    {
                        Console.Write("\nОберіть скаргу для видалення (номер у списку): ");
                        if (int.TryParse(Console.ReadLine(), out Choice) && system.Complaints.Any(a => a.Id == Choice))
                            throw new Exception("Введіть коректний номер зі списку.");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }

                    administrator.DeleteComplaint(system, Choice);
                    system.SaveData();

                }

            }

        }
        public static void ViewAds(DealHubSystem system, User currentUser, string choice, bool flag1)
        {
            int action;
            bool flag = true;
            List<Ad> adsForUser = new();

            while (true)
            {
                while (flag1)
                {
                    adsForUser = system.AllAds.Where(a => a.OwnerNickname != currentUser?.Nickname && a.IsActive == true).ToList();
                    for (int i = 0; i < adsForUser.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} {adsForUser[i].Title} - {adsForUser[i].Description}");
                    }
                    flag1 = false;
                }

                if (currentUser == null)
                {
                    break;
                }
                else if (currentUser is RegisteredUser regUser)
                {
                    int n; bool flagTocontinue = true;
                    while (true)
                    {
                        try
                        {
                            flagTocontinue = true;
                            Console.WriteLine("\n1. Обрати оголошення \n2. Пошук \n0. Вийти в меню ");
                            if (int.TryParse(Console.ReadLine(), out n) && (n == 0 || n == 1 || n == 2))
                                break;
                            throw new Exception("Помилка! Введіть 0-2");
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                    }

                    if (n == 0) { flag1 = true; break; }
                    else if (n == 1)
                    {
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введіть номер оголошення (0 для повернення):");

                                if (!int.TryParse(Console.ReadLine(), out action))
                                    throw new Exception("Помилка! Введіть число.");

                                if (action == 0)
                                {
                                    flagTocontinue = false;
                                    break;
                                }

                                if (action < 1 || action > adsForUser.Count)
                                    throw new Exception("Оголошення не знайдено");

                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        while (flagTocontinue)
                        {
                            int adId = adsForUser[n - 1].Id;
                            Ad ad = system.AllAds.First(a => a.Id == adId);

                            Console.WriteLine($"\nОголошення:");
                            Console.WriteLine($"Назва: {ad.Title}");
                            Console.WriteLine($"Опис: {ad.Description}");
                            Console.WriteLine($"Автор: {ad.OwnerNickname}");

                            Console.WriteLine("\n1. Написати повідомлення автору \n2. Оформити замовлення \n3. Відправити скаргу про оголошення \n0. Повернутися до списку оголошень");

                            while (true)
                            {
                                try
                                {
                                    Console.Write("\nОберіть дію: ");
                                    if (int.TryParse(Console.ReadLine(), out action) && action > -1 && action < 4)
                                        break;
                                    throw new Exception("Помилка! Введіть 0-3.");
                                }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }
                            if (action == 0) { flag1 = true; break; }
                            else if (action == 1)
                            {
                                try
                                {
                                    Console.Write("\nВведіть повідомлення: ");
                                    string m;
                                    do
                                    {
                                        try
                                        {
                                            m = Console.ReadLine();
                                            if (!string.IsNullOrWhiteSpace(m))
                                                break;
                                            throw new Exception("Повідомлення не може бути порожнім! Введіть ще раз:");
                                        }
                                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                                    } while (true);

                                    RegisteredUser? receiver = system.Users.FirstOrDefault(u => u.Nickname == ad.OwnerNickname);
                                    RegisteredUser? sender = currentUser as RegisteredUser;

                                    if (receiver != null)
                                    {
                                        sender.SendMessage(receiver, m);
                                        flag1 = true;
                                        system.SaveData();

                                    }
                                    else
                                    {
                                        throw new Exception("Не вдалося знайти власника оголошення.");
                                    }
                                }
                                catch (Exception ex) { Console.WriteLine(ex.Message, ex); }
                            }
                            else if (action == 2)
                            {
                                bool OrderStatus = false;
                                RegisteredUser? receiver = system.Users.FirstOrDefault(u => u.Nickname == ad.OwnerNickname);
                                try
                                {
                                    OrderStatus = Order(system, regUser, ad);
                                }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                                if (OrderStatus)
                                {
                                    Console.WriteLine("Хочете залишити відгук? (Так/Ні)");
                                    string answer = Console.ReadLine()?.Trim().ToLower();

                                    if (answer == "ні")
                                    {
                                        OrderStatus = false;
                                        return;
                                    }

                                    else if (answer == "так")
                                    {
                                        OrderStatus = false;
                                        Console.Write("\nВведіть текст відгуку (мінімум 5 символів): ");
                                        string reviewContent;

                                        while (true)
                                        {
                                            reviewContent = Console.ReadLine()?.Trim();

                                            if (!string.IsNullOrWhiteSpace(reviewContent) && reviewContent.Length >= 5)
                                            {
                                                break;
                                            }

                                            Console.WriteLine("Помилка! Відгук повинен містити мінімум 5 символів.");
                                            Console.Write("Спробуйте ще раз: ");
                                        }

                                        try
                                        {
                                            regUser.LeaveReview(receiver, reviewContent);
                                            system.SaveData();
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"❌ Сталася помилка: {ex.Message}");
                                        }
                                    }
                                }
                            }
                            else if (action == 3)
                            {
                                try
                                {
                                    string newDescription = null;
                                    do
                                    {
                                        try
                                        {
                                            Console.Write("\nДокладно опишіть скаргу : ");
                                            string nullstring = Console.ReadLine();
                                            if (string.IsNullOrWhiteSpace(nullstring))
                                            {
                                                throw new FormatException("Назва не може бути порожньою або складатися тільки з пробілів!");
                                            }
                                            newDescription = nullstring;
                                            flag = false;
                                        }
                                        catch (FormatException ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    } while (flag);
                                    flag = true; flag1 = true;
                                    RegisteredUser receiver = system.Users.First(u => u.Nickname == ad.OwnerNickname);
                                    regUser.SendComplaint(system, receiver, ad, newDescription);
                                }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }
                        }
                    }
                    else if (n == 2)
                    {
                        try
                        {
                            Category newcategory = default;
                            do
                            {
                                try
                                {
                                    Console.WriteLine("Виберіть категорію оголошення або введіть 0:");
                                    Console.WriteLine("\n1 - Побутова_техніка \n2 - Електроніка \n3 - Одяг \n4 - Догляд_за_тілом \n5 - Дім_і_сад \n6 - Дитячі_товари \n7 - Спорт \n8 - Автотовари \n9 - Зоотовари \n10 - Хобі_та_творчість");
                                    choice = Console.ReadLine();
                                    if (choice == "0") { newcategory = default; break; }
                                    else if (!Enum.TryParse(choice, true, out newcategory) || !Enum.IsDefined(typeof(Category), newcategory))
                                    {
                                        throw new FormatException("Введена категорія не знайдена в списку доступних варіантів.");
                                    }

                                    flag = false;
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine("Помилка: " + ex.Message);
                                }
                            } while (flag);
                            flag = true;

                            Console.WriteLine("Введіть частину або повну назву (залиште поле пустим для пошуку по категоріям):");
                            string newtitle = Console.ReadLine();
                            var ads = currentUser.Search(newcategory, newtitle, system);
                            if (ads.Count == 0)
                            {
                                Console.WriteLine("Жодного оголошення не знайдено");
                            }
                            else
                            {
                                foreach (var ad in ads)
                                {
                                    int index = adsForUser.IndexOf(ad);

                                    if (index != -1)
                                    {
                                        Console.WriteLine($"[{index + 1}] {ad.Title} - {ad.Description}");
                                    }
                                }
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                    }
                }
            }
        }
        public static User GuestRegistration(DealHubSystem system, User currentUser)
        {
            Console.Write("\nІм'я користувача: ");
            string? newUsername = Console.ReadLine().Trim();

            Console.Write("Пароль: ");
            string? newPassword = Console.ReadLine().Trim();

            try
            {
                currentUser = system.RegisterUser(newUsername, newPassword, false);
                if (currentUser is RegisteredUser regUser)
                {
                    Console.WriteLine("Реєстрація успішна! ");
                    system.SaveData();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return currentUser;

        }
        public static User GuestLogIn(DealHubSystem system)
        {
            User currentUser;
            try
            {
                Console.Write("\nІм'я користувача: ");
                string username = Console.ReadLine().Trim();

                Console.Write("Пароль: ");
                string password = Console.ReadLine().Trim();

                currentUser = system.Login(username, password);
                return currentUser;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }
        }
        public static void MyAds(DealHubSystem system, RegisteredUser regUser)
        {
            while (true)
            {
                string nullstring;
                bool flag = true;
                int action;
                Console.WriteLine("\n1.Переглянути свої оголошення \n2.Створити нове оголошення \n3.Редагувати оголошення \n4.Видалити оголошення \n0.Назад");

                while (true)
                {
                    try
                    {
                        Console.Write("\nОберіть дію: ");
                        if (int.TryParse(Console.ReadLine(), out action) && action > -1 && action < 5)
                            break;
                        throw new Exception("Помилка! Введіть число від 0 до 4.");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
                if (action == 0) break;
                else if (action == 1)
                {
                    try
                    {
                        List<Ad> myads = regUser.ViewMyAds();
                        for (int i = 0; i < myads.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {myads[i].OwnerNickname}   {myads[i].Title} \n {myads[i].Description} \n {myads[i].Price} грн");
                        }

                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
                else if (action == 2)
                {
                    string newTitle = null;
                    do
                    {
                        try
                        {
                            Console.Write("\nВведіть заголовок оголошення (обов'язкове поле): ");
                            nullstring = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(nullstring))
                            {
                                throw new FormatException("Назва не може бути порожньою або складатися тільки з пробілів!");
                            }
                            newTitle = nullstring;
                            flag = false;
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine("Помилка: " + ex.Message);
                        }
                    } while (flag);
                    flag = true;

                    string newDescription = null;
                    do
                    {
                        try
                        {
                            Console.Write("Введіть опис оголошення (обов'язкове поле): ");
                            nullstring = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(nullstring))
                            {
                                throw new FormatException("Назва не може бути порожньою або складатися тільки з пробілів!");
                            }
                            newDescription = nullstring;
                            flag = false;
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine("Помилка: " + ex.Message);
                        }
                    } while (flag);
                    flag = true;

                    Category category = default;
                    do
                    {
                        try
                        {
                            Console.WriteLine("Виберіть категорію оголошення:");
                            Console.WriteLine("\n1 - Побутова_техніка \n2 - Електроніка \n3 - Одяг \n4 - Догляд_за_тілом \n5 - Дім_і_сад \n6 - Дитячі_товари \n7 - Спорт \n8 - Автотовари \n9 - Зоотовари \n10 - Хобі_та_творчість");

                            if (!Enum.TryParse(Console.ReadLine(), true, out category) || !Enum.IsDefined(typeof(Category), category))
                            {
                                throw new FormatException("Введена категорія не знайдена в списку доступних варіантів.");
                            }

                            flag = false;
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine("Помилка: " + ex.Message);
                        }
                    } while (flag);
                    flag = true;
                    Console.Write("\nФото до оголошення (необов'язкове поле): ");
                    string? newImage = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newImage)) newImage = "1";

                    double price = -1;
                    do
                    {
                        try
                        {
                            Console.WriteLine("Введіть ціну поставки об'єкта (грн): ");
                            if (!double.TryParse(Console.ReadLine(), out price))
                            {
                                throw new FormatException("Тільки цифри");
                            }
                            flag = true;
                        }
                        catch (FormatException ex) { Console.WriteLine("Помилка. " + ex.Message); }
                    } while (flag == false);
                    flag = false;
                    try
                    {
                        regUser.CreateAd(system, newTitle, newDescription, category, newImage, price, regUser);
                        system.SaveData();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Помилка: " + ex.Message);
                    }
                }

                if (action == 3)
                {
                    if (regUser.Ads.Count == 0)
                    {
                        Console.WriteLine("У вас немає активних оголошень для редагування.");
                    }
                    else
                    {
                        var userAds = regUser.ViewMyAds();
                        int Choice;
                        while (true)
                        {
                            Console.Write("\nОберіть оголошення для редагування (номер у списку): ");
                            if (int.TryParse(Console.ReadLine(), out Choice) && Choice > 0 && Choice <= userAds.Count)
                                break;
                            Console.WriteLine("Помилка! Введіть коректний номер зі списку.");
                        }

                        // Отримуємо СПРАВЖНІЙ ID оголошення для редагування
                        int adId = userAds[Choice - 1].Id;
                        Console.WriteLine($"Ви обрали оголошення: {userAds[Choice - 1].Title} (ID: {adId})");

                        Ad adToEdit = regUser.Ads.First(a => a.Id == adId);

                        Console.Write("\nНовий заголовок (залиште порожнім, щоб не змінювати): ");
                        string? newTitle = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(newTitle)) newTitle = adToEdit.Title;

                        Console.Write("Новий опис (залиште порожнім, щоб не змінювати): ");
                        string? newDescription = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(newDescription)) newDescription = adToEdit.Description;

                        regUser.EditAd(adId, newTitle, newDescription);
                        system.SaveData();

                    }
                }
                else if (action == 4)
                {
                    try
                    {
                        if (regUser.Ads.Count == 0)
                        {
                            throw new Exception("У вас немає активних оголошень для видалення.");
                        }
                        else
                        {
                            var userAds = regUser.ViewMyAds();
                            int Choice;
                            while (true)
                            {
                                try
                                {
                                    Console.Write("\nОберіть оголошення для видалення (номер у списку): ");
                                    if (int.TryParse(Console.ReadLine(), out Choice) && Choice > 0 && Choice <= userAds.Count)
                                        break;
                                    throw new Exception("Введіть коректний номер зі списку.");
                                }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }

                            // Отримуємо СПРАВЖНІЙ ID оголошення для редагування
                            int adId = userAds[Choice - 1].Id;
                            Console.WriteLine($"Ви обрали оголошення: {userAds[Choice - 1].Title}");
                            regUser.DeleteAd(system, adId);
                            system.SaveData();
                            Console.WriteLine($"Оголошення {userAds[Choice - 1].Title} успішно видалено!");
                        }
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            }
        }
        public static void UserChats(DealHubSystem system, RegisteredUser regUser)
        {
            int action;
            bool flag = true;
            string nullstring;
            while (true)
            {
                Console.WriteLine("\n1. Я покупець\n2. Я продавець \n0. Повернутися в меню");
                while (true)
                {
                    Console.Write("\nОберіть дію: ");
                    if (int.TryParse(Console.ReadLine(), out action) && action > -1 && action < 3)
                        break;
                    Console.WriteLine("Помилка! Введіть число від 0 до 2.");
                }
                if (action == 0) break;
                if (action == 1 || action == 2)
                {
                    List<User> chatUsers = regUser.ViewChats(action, system);

                    if (chatUsers.Count == 0)
                    {
                        Console.WriteLine("Немає чатів.");
                    }

                    else
                    {
                        while (true)
                        {
                            Console.WriteLine("\nВаші чати:");
                            for (int i = 0; i < chatUsers.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {chatUsers[i].Nickname}");
                            }


                            Console.WriteLine("\n0. Повернутись в меню");
                            Console.Write("\nОберіть чат (номер): ");
                            int chatIndex = -1;
                            while (true)
                            {
                                try
                                {
                                    if (!int.TryParse(Console.ReadLine(), out chatIndex))
                                    {
                                        throw new Exception("Помилка! Введіть число.");
                                    }

                                    if (chatIndex == 0)
                                    {
                                        return;
                                    }

                                    if (chatIndex < 1 || chatIndex > chatUsers.Count)
                                    {
                                        throw new Exception("Чат не знайдено. Виберіть правильний номер.");
                                    }

                                    break;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            User? selectedUser = null;
                            try
                            {
                                selectedUser = chatUsers[chatIndex - 1];
                                regUser.ViewMessages(selectedUser, regUser);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Сталася помилка: {ex.Message}");
                            }

                            regUser.ViewMessages(selectedUser, regUser);

                            while (true)
                            {
                                Console.WriteLine("\n1. Відправити повідомлення \n2. Відправити скаргу про користувача \n0. Вийти з чату");

                                while (true)
                                {
                                    Console.Write("\nОберіть дію: ");
                                    if (int.TryParse(Console.ReadLine(), out action) && action >= 0 && action <= 2)
                                        break;
                                    Console.WriteLine("Помилка! Введіть 0 - 2.");
                                }
                                if (action == 0) break;
                                else if (action == 1)
                                {
                                    Console.Write("\nВведіть повідомлення: ");
                                    string messageText = Console.ReadLine();

                                    if (!string.IsNullOrWhiteSpace(messageText))
                                    {
                                        regUser.SendMessage(selectedUser as RegisteredUser, messageText);
                                        Console.WriteLine("Повідомлення відправлено!");
                                        system.SaveData();

                                    }
                                    else
                                    {
                                        Console.WriteLine("Повідомлення не може бути порожнім.");
                                    }
                                }
                                else if (action == 2)
                                {
                                    try
                                    {
                                        string newDescription = null;
                                        do
                                        {
                                            try
                                            {
                                                Console.Write("\nДокладно опишіть скаргу : ");
                                                nullstring = Console.ReadLine();
                                                if (string.IsNullOrWhiteSpace(nullstring))
                                                {
                                                    throw new FormatException("Назва не може бути порожньою або складатися тільки з пробілів!");
                                                }
                                                newDescription = nullstring;
                                                flag = false;
                                            }
                                            catch (FormatException ex)
                                            {
                                                Console.WriteLine("Помилка: " + ex.Message);
                                            }
                                        } while (flag);
                                        flag = true;
                                        RegisteredUser receiver = selectedUser as RegisteredUser;
                                        regUser.SendComplaint(system, receiver, null, newDescription);
                                    }
                                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                                }
                            }
                        }
                    }

                }

            }
        }
        public static bool Order(DealHubSystem system, RegisteredUser regUser, Ad ad)
        {
            if (ad.IsActive == false) { Console.WriteLine("Оголошення вже недоступне"); return false; }
            try
            {
                Console.WriteLine("\nОформлення замовлення:");
                Console.WriteLine("\nВведіть контактні дані:");

                string firstName;
                while (true)
                {
                    try
                    {
                        Console.Write("Ім'я: ");
                        firstName = Console.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(firstName)) break;
                        throw new Exception("Ім'я не може бути порожнім!");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }

                string lastName;
                while (true)
                {
                    try
                    {
                        Console.Write("Прізвище: ");
                        lastName = Console.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(lastName)) break;
                        throw new Exception("Прізвище не може бути порожнім!");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }

                string middleName;
                while (true)
                {
                    try
                    {
                        Console.Write("По-батькові: ");
                        middleName = Console.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(middleName)) break;
                        throw new Exception("По-батькові не може бути порожнім!");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }

                string phoneNumber;
                while (true)
                {
                    try
                    {
                        Console.Write("Номер телефону (+380): ");
                        phoneNumber = Console.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(phoneNumber) && Regex.IsMatch(phoneNumber, @"^\d{9}$"))
                            break;
                        throw new Exception("Введіть коректний номер у форматі (+380)XXXXXXXXX.");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }

                string deliveryAddress;
                while (true)
                {
                    try
                    {
                        Console.Write("Адреса доставки: ");
                        deliveryAddress = Console.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(deliveryAddress)) break;
                        throw new Exception("Адреса не може бути порожньою!");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
                string contactDetails = "Мої контактні дані для зв'яку: " + "\n" + "ПІБ" + lastName + "  " + firstName + "  " + middleName + "\n" + "Мій номер: +380 " + phoneNumber + "\n" + "Адреса доставки: " + deliveryAddress;
                RegisteredUser receiver = system.Users.First(u => u.Nickname == ad.OwnerNickname);
                regUser.OrderAd(ad, contactDetails, receiver);
                system.SaveData();
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
        }
    }
}
