using System;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Lesson_1_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Конвертация рублей в доллары");
            Console.Write("Введите сумму в рублях: ");

            int sumRubbles;
            float currancyRate;

            try
            {
                sumRubbles = Convert.ToInt32(Console.ReadLine());   
            }
            catch (Exception)
            {
                Console.WriteLine("Введено не корректное значение!");
                Console.ReadKey();
                throw;
            }

            try
            {
                currancyRate = GetCurransyRate("431");
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка получения курса валют");
                Console.ReadKey();
                throw;
            }

            double result = Math.Round(sumRubbles / currancyRate, 2, MidpointRounding.ToEven);

            Console.WriteLine("Курс рубля к доллару: " + currancyRate);
            Console.WriteLine(sumRubbles + " byn = " + result + " $");
            Console.ReadKey();

        }
        static float GetCurransyRate(string curransyCode)
        {
            string url = "https://www.nbrb.by/api/exrates/rates/" + curransyCode;

            WebRequest webRequest = WebRequest.Create(url);
            var stream = webRequest.GetResponse().GetResponseStream();
            string content = new StreamReader(stream).ReadToEnd();

            Currensy currensyObject = JsonSerializer.Deserialize<Currensy>(content);

            return currensyObject.Cur_OfficialRate;
        }
    }

    class Currensy
    {
        public string Cur_Name { get; set; }
        public float Cur_OfficialRate { get; set; }
    }
}
