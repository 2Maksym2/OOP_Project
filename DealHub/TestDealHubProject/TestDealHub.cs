using DealHub;


namespace TestDealHubProject
{
    [TestClass]
    public class DealHubSystemTest
    {
        [TestMethod]
        public void AddComplaintTest()
        {
            // Arrange
            var system = new DealHubSystem();
            var sender = new RegisteredUser("SenderUser", "qwerty");
            var receiver = new RegisteredUser("ReceiverUser", "qwerty");
            var receiverAd = new Ad { Id = 1, Title = "Test Ad", OwnerNickname = receiver.Nickname };
            string description = "Неправильний опис товару";

            var complaint = new Complaint(sender, receiver, receiverAd, description);
            int expectedComplaintCount = 1;

            // Act
            system.AddComplaint(complaint);

            // Assert
            Assert.AreEqual(expectedComplaintCount, system.Complaints.Count, "Кількість скарг у системі повинна збільшитися.");

        }

        [TestMethod]
        public void RegisterUser_ShouldRegisterNewUser()
        {
            // Arrange
            var system = new DealHubSystem();

            // Act
            var user = system.RegisterUser("NewUser", "password", false);

            // Assert
            Assert.AreEqual("NewUser", user.Nickname);
        }

        [TestMethod]
        [DataRow("User", "Password")]
        public void Test_LoginSuccess(string m, string n)
        {
            // Arrange
            DealHubSystem system = new();
            system.RegisterUser("User", "Password", false);

            // Act
            var user = system.Login(m, n);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual("User", user.Nickname);
        }

        [TestMethod]
        [DataRow("User", "WrongPassword")] // неправильний пароль
        [DataRow("User1", "Password")]     // користувач не існує
        public void Test_InvalidLogin(string m, string n)
        {
            // Arrange
            DealHubSystem system = new();
            system.RegisterUser("User", "Password", false);

            // Act + Assert
            Assert.ThrowsException<Exception>(() => system.Login(m, n));
        }
    }


    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestSearch_ByCategory()
        {
            // Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User", "pasword");
            system.AllAds.AddRange(new List<Ad>
        {
                new Ad { Category = Category.Електроніка, Title = "iPhone 13 Pro" },
            new Ad { Category = Category.Електроніка, Title = "Samsung Galaxy S21" },
            new Ad { Category = Category.Автотовари, Title = "Toyota Corolla 2020" },
            new Ad { Category = Category.Автотовари, Title = "Tesla Model S" },
            new Ad { Category = Category.Одяг, Title = "Nike Air Max" }
        });

            //Act
            var result = user.Search(Category.Автотовари, null, system);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(a => a.Title == "Toyota Corolla 2020"));
            Assert.IsTrue(result.Any(a => a.Title == "Tesla Model S"));
        }

        [TestMethod]
        public void TestSearch_ByTitle()
        {
            // Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User", "pasword");
            system.AllAds.AddRange(new List<Ad>
        {
            new Ad { Category = Category.Електроніка, Title = "iPhone 13 Pro" },
            new Ad { Category = Category.Електроніка, Title = "Samsung Galaxy S21" },
            new Ad { Category = Category.Автотовари, Title = "Toyota Corolla 2020" },
            new Ad { Category = Category.Автотовари, Title = "Tesla Model S" },
            new Ad { Category = Category.Одяг, Title = "Nike Air Max" }
        });
            // Act
            var result = user.Search(null, "iPhone", system);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("iPhone 13 Pro", result[0].Title);
        }

        [TestMethod]
        public void TestSearch_ByCategoryAndTitle()
        {
            // Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User", "pasword");
            system.AllAds.AddRange(new List<Ad>
        {
                new Ad { Category = Category.Електроніка, Title = "iPhone 13 Pro" },
            new Ad { Category = Category.Електроніка, Title = "Samsung Galaxy S21" },
            new Ad { Category = Category.Автотовари, Title = "Toyota Corolla 2020" },
            new Ad { Category = Category.Автотовари, Title = "Tesla Model S" },
            new Ad { Category = Category.Одяг, Title = "Nike Air Max" }
        });
            // Act
            var result = user.Search(Category.Автотовари, "Tesla", system);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Tesla Model S", result[0].Title);
        }

        [TestMethod]
        public void TestSearch_NoResults()
        {
            // Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User", "pasword");
            system.AllAds.AddRange(new List<Ad>
        {
                new Ad { Category = Category.Електроніка, Title = "iPhone 13 Pro" },
            new Ad { Category = Category.Електроніка, Title = "Samsung Galaxy S21" },
            new Ad { Category = Category.Автотовари, Title = "Toyota Corolla 2020" },
            new Ad { Category = Category.Автотовари, Title = "Tesla Model S" },
            new Ad { Category = Category.Одяг, Title = "Nike Air Max" }
        });
            // Act
            var result = user.Search(Category.Електроніка, "Nokia", system);

            // Assert
            Assert.AreEqual(0, result.Count);
        }
    }


    [TestClass]
    public class RegisteredUserTest
    {
        [TestMethod]
        public void TestCreateAd()
        {
            //Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User1", "pasword");
            //Act
            user.CreateAd(system, "Laptop", "Gaming Laptop", Category.Електроніка, "image.jpg", 1200.0, user);
            //Assert
            Assert.AreEqual(1, system.AllAds.Count);
            Assert.AreEqual("Laptop", system.AllAds[0].Title);
        }
        [TestMethod]
        public void TestCreateAd_EmptyTitle()
        {
            //Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User1", "pasword");
            //Act + Assert
            Assert.ThrowsException<Exception>(() => user.CreateAd(system, "", "Gaming Laptop", Category.Електроніка, "image.jpg", 1200.0, user));
        }


        [TestMethod]
        public void TestEditAd()
        {
            //Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User1", "pasword");
            user.CreateAd(system, "Old Title", "Old Description", Category.Електроніка, "image.jpg", 500.0, user);
            int adId = system.AllAds[0].Id;
            //Act
            user.EditAd(adId, "New Title", "New Description");
            //Assert
            Assert.AreEqual("New Title", system.AllAds[0].Title);
        }
        [TestMethod]
        public void TestEditAd_AdNotFound()
        {
            //Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User1", "pasword");
            user.CreateAd(system, "Old Title", "Old Description", Category.Електроніка, "image.jpg", 500.0, user);
            int adId = system.AllAds[0].Id;

            //Act + Assert
            Assert.ThrowsException<Exception>(() => user.EditAd(999, "New Title", "New Description"));

        }


        [TestMethod]
        public void TestDeleteAd()
        {
            //Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User1", "pasword");

            user.CreateAd(system, "Ad to Delete", "Description", Category.Автотовари, "car.jpg", 20000.0, user);
            //Act
            user.DeleteAd(system, system.AllAds[0]);
            //Assert
            Assert.AreEqual(0, system.AllAds.Count);
        }
      
        [TestMethod]
        public void TestDeleteAd_AdNotFound()
        {
            // Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User1", "password");

            Ad nonExistentAd = new Ad(); // створюємо фейкове оголошення, яке не додано до system чи user

            // Act + Assert
           Assert.ThrowsException<Exception>(() => user.DeleteAd(system, nonExistentAd));
        }


        [TestMethod]
        public void TestOrderAd()
        {
            //Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User1", "pasword");
            var owner = new RegisteredUser("User2", "pasword");

            Ad ad = new Ad { Title = "Bike", Category = Category.Спорт };
            owner.CreateAd(system, ad.Title, "Great condition", ad.Category, "bike.jpg", 300.0, owner);
            //Act
            user.OrderAd(ad, "user@email.com", owner);
            //Assert
            Assert.IsFalse(ad.IsActive);
        }

        [TestMethod]
        public void TestOrderAd_AdNonActive()
        {
            //Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User1", "pasword");
            var owner = new RegisteredUser("User2", "pasword");
            Ad ad = new Ad { Title = "Bike", Category = Category.Спорт, IsActive = false };
            owner.CreateAd(system, ad.Title, "Great condition", ad.Category, "bike.jpg", 300.0, owner);

            //Act + Assert
            Assert.ThrowsException<Exception>(() => user.OrderAd(ad, "user@email.com", owner));
        }


        [TestMethod]
        public void TestSendMessage()
        {
            //Arrange
            var system = new DealHubSystem();
            var sender = new RegisteredUser("User1", "pasword");
            var receiver = new RegisteredUser("User2", "pasword");
            //Act
            sender.SendMessage(receiver, "Hello, how much is the price?");
            //Assert
            Assert.AreEqual(1, receiver.MessagesReceived.Count);
        }
        [TestMethod]
        public void TestSendMessage_EmptyMessage()
        {
            //Arrange
            var system = new DealHubSystem();
            var sender = new RegisteredUser("User1", "pasword");
            var receiver = new RegisteredUser("User2", "pasword");
            //Act + Assert
            Assert.ThrowsException<Exception>(() => sender.SendMessage(receiver, ""));
        }


        [TestMethod]
        public void TestLeaveReview()
        {
            //Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User1", "pasword");
            var owner = new RegisteredUser("User2", "pasword");
            //Act
            user.LeaveReview(owner, "Great seller!");
            //Assert
            Assert.AreEqual(1, owner.Reviews.Count);
        }
        [TestMethod]
        public void TestLeaveReview_EmptyOrLessThan5Symbols()
        {
            //Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User1", "pasword");
            var owner = new RegisteredUser("User2", "pasword");

            //Act + Assert
            Assert.ThrowsException<Exception>(() => user.LeaveReview(owner, ""));
            Assert.ThrowsException<Exception>(() => user.LeaveReview(owner, "user"));

        }


        [TestMethod]
        public void TestSendComplaint()
        {
            //Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User1", "pasword");
            var owner = new RegisteredUser("User2", "pasword");
            //Act
            Complaint complaint = user.SendComplaint(system, owner, null, "Scam detected");
            //Assert
            Assert.AreEqual("Scam detected", complaint.Description);
        }
        [TestMethod]
        public void TestSendComplaint_EmptyOrLessThan10Symbols()
        {
            //Arrange
            var system = new DealHubSystem();
            var user = new RegisteredUser("User1", "pasword");
            var owner = new RegisteredUser("User2", "pasword");
            //Act + Assert
            Assert.ThrowsException<Exception>(() => user.SendComplaint(system, owner, null, ""));
            Assert.ThrowsException<Exception>(() => user.SendComplaint(system, owner, null, "Bad user"));
        }

    }

    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void ViewComplaints_NoComplaints()
        {
            // Arrange
            var system = new DealHubSystem();
            var admin = new Admin("Admin", "password");

            // Act + Assert
            Assert.ThrowsException<Exception>(() => admin.ViewComplaints(system));
        }
        [TestMethod]
        public void DeleteComplaint_ShouldRemoveComplaint()
        {
            // Arrange
            var system = new DealHubSystem();
            var admin = new Admin("Admin", "password");
            var complaint = new Complaint(new RegisteredUser("User1", "password"), new RegisteredUser("User2", "password"), null, "Погане оголошення");

            system.Complaints.Add(complaint);

            // Act
            bool result = admin.DeleteComplaint(system, complaint.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(system.Complaints.Contains(complaint));
        }
        [TestMethod]
        public void DeleteComplaint_ComplaintDoesNotExist()
        {
            // Arrange
            var system = new DealHubSystem();
            var admin = new Admin("Admin", "password");

            // Act + Assert
            Assert.ThrowsException<Exception>(() => admin.DeleteComplaint(system, 999));
        }
        [TestMethod]
        public void DeleteAd_ShouldRemoveAd_WhenAdExists()
        {
            // Arrange
            var system = new DealHubSystem();
            var admin = new Admin("Admin", "password");
            var user = new RegisteredUser("User1", "password");
            var ad = new Ad { Id = 1, Title = "Продам авто", OwnerNickname = user.Nickname };

            user.Ads.Add(ad);
            system.Users.Add(user);
            system.AllAds.Add(ad);

            // Act
            admin.DeleteAd(system, ad.Id);

            // Assert
            Assert.IsFalse(system.AllAds.Contains(ad));
            Assert.IsFalse(user.Ads.Contains(ad));
        }
        [TestMethod]
        public void DeleteAd_AdDoesNotExist()
        {
            // Arrange
            var system = new DealHubSystem();
            var admin = new Admin("Admin", "password");

            // Act + Assert
            Assert.ThrowsException<Exception>(() => admin.DeleteAd(system, 999));
        }
        [TestMethod]
        public void BanUser_ShouldBanUser()
        {
            // Arrange
            var system = new DealHubSystem();
            var admin = new Admin("Admin", "password");
            var user = new RegisteredUser("User1", "password");
            var endBanTime = DateTime.Now.AddMinutes(10);
            system.Users.Add(user);

            // Act
            admin.BanUser(system, "User1", endBanTime);

            // Assert
            Assert.IsTrue(user.BlockedTime > DateTime.Now);
        }

        [TestMethod]
        public void BanUser_ShouldThrowException_UserDoesNotExist()
        {
            // Arrange
            var system = new DealHubSystem();
            var admin = new Admin("Admin", "password");
            var endBanTime = DateTime.Now.AddMinutes(10);

            // Act + Assert
            Assert.ThrowsException<Exception>(() => admin.BanUser(system, "User1", endBanTime));
        }


    }

}


