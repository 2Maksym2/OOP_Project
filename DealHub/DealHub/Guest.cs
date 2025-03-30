using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DealHub
{
    public class Guest : User
    {
        public Guest() { }
        public static event Action<string> MessageForUser;
        public override void ShowMenu()
        {
            MessageForUser?.Invoke("Меню");
            MessageForUser?.Invoke("1.Проглянути оголошення");
            MessageForUser?.Invoke("2.Зареєструватися");
            MessageForUser?.Invoke("3.Увійти");
            MessageForUser?.Invoke("0.Закрити програму");

        }
    }
}
