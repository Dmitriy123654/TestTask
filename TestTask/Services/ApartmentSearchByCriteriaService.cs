using Newtonsoft.Json.Linq;
using TestTask.Models;

namespace TestTask.Services
{
    public class ApartmentSearchByCriteriaService 
    {
        public void FindApartmentsInDistrict(List<Ad> ads, string district, int checkInDate, int checkOutDate)
        {
            var filteredApartments = ads
                .Where(ad => ad.AdParameters.Any(p => p.P == "area" && p.Vl.ToString() == district))
                .Where(ad => ad.AdParameters.Any(p => p.P == "booking_enabled" && (bool)p.V))
                .Where(ad => IsAvailableOnDates(ad, checkInDate, checkOutDate))
                .OrderBy(ad => Convert.ToDouble(ad.AdParameters.FirstOrDefault(p => p.P == "price_usd")?.V ?? 0))
                .ToList();
            PrintApartments(filteredApartments);
        }

        private bool IsAvailableOnDates(Ad ad, int checkInDateId, int checkOutDateId)
        {
            var bookingCalendar = ad.AdParameters.FirstOrDefault(p => p.P == "booking_calendar")?.V as JArray;
            if (bookingCalendar == null)
                return false;

            // Фильтруем некорректные значения
            var availableDateIds = bookingCalendar
                .Where(dateId => !string.IsNullOrEmpty(dateId.ToString()))
                .Select(dateId => dateId.ToObject<int?>()) 
                .Where(dateId => dateId.HasValue) 
                .Select(dateId => dateId.Value) 
                .ToList();

            return availableDateIds.Contains(checkInDateId) && availableDateIds.Contains(checkOutDateId);
        }
        public void PrintApartments(List<Ad> apartments)
        {
            if (apartments.Count == 0)
            {
                Console.WriteLine("Нет доступных квартир, удовлетворяющих заданным критериям.");
                return;
            }

            Console.WriteLine($"\nДоступные квартиры: {apartments.Count}");
            foreach (var apartment in apartments)
            {
                var address = apartment.AccountParameters.FirstOrDefault(p => p.P == "address")?.V.ToString();
                var price = Convert.ToDouble(apartment?.PriceUsd);
                var rooms = apartment.AdParameters.FirstOrDefault(p => p.P == "rooms")?.V.ToString();

                Console.WriteLine($"Адрес: {address}");
                Console.WriteLine($"Цена (USD): {price}");
                Console.WriteLine($"Количество комнат: {rooms}\n");
            }
        }
    }
}
