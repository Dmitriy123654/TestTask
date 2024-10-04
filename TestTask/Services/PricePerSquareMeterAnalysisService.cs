using Newtonsoft.Json.Linq;
using TestTask.Models.DTO;
using TestTask.Models;

namespace TestTask.Services
{
    class PricePerSquareMeterAnalysisService
    {
        public void Analyze(List<Ad> ads)
        {
            // Фильтрация квартир по стоимости
            var filteredAds = ads.Where(ad => Convert.ToDouble(ad.PriceUsd) > 10000).ToList();
            EnsureSquareMeterExists(filteredAds);
            PrintFloorPriceDependence(filteredAds);
            PrintRoomsPriceDependence(filteredAds);
            PrintMetroPriceDependence(filteredAds);
        }

        private void EnsureSquareMeterExists(List<Ad> ads)
        {
            foreach (var ad in ads)
            {
                if (!ad.AdParameters.Any(p => p.P == "square_meter"))
                {
                    var price = Convert.ToDouble(ad.PriceUsd);
                    var size = Convert.ToDouble(ad.AdParameters.FirstOrDefault(p => p.P == "size")?.V ?? 0);

                    if (price > 0 && size > 0)
                    {
                        var squareMeter = price / size / 100.0;
                        ad.AdParameters.Add(new AdParameter
                        {
                            Pl = "Цена за м²",
                            Vl = "",
                            P = "square_meter",
                            V = Math.Round(squareMeter, 0),
                            Pu = "psm"
                        });
                    }
                }
            }
        }
        private void PrintFloorPriceDependence(List<Ad> ads)
        {
            var floorPrices = ads
                .Where(ad => ad.AdParameters.Any(p => p.P == "floor" && p.V != null)) 
                .GroupBy(ad =>
                {
                    var floorParam = ad.AdParameters.FirstOrDefault(p => p.P == "floor");
                    if (floorParam?.V is JArray array && array.Count > 0)
                    {
                        return Convert.ToInt32(array[0]);
                    }
                    return -1;
                })
                .Select(g => new FloorPriceDTO
                {
                    Floor = g.Key,
                    AveragePricePerSquareMeter = g.Average(ad =>
                        Convert.ToDouble(ad.AdParameters.FirstOrDefault(p => p.P == "square_meter")?.V ?? 0))
                })
                .Where(info => info.Floor >= 0) 
                .OrderBy(info => info.Floor) 
                .ToList();

            Console.WriteLine("Зависимость стоимости квадратного метра от этажа квартиры:");
            foreach (var item in floorPrices)
            {
                Console.WriteLine($"Этаж: {item.Floor}, Средняя стоимость за м²: {item.AveragePricePerSquareMeter:F2}");
            }
        }


        private void PrintRoomsPriceDependence(List<Ad> ads)
        {
            var roomsPrices = ads
                .GroupBy(ad => ad.AdParameters.FirstOrDefault(p => p.P == "rooms")?.V)
                .Where(g => g.Key != null) 
                .Select(g => new RoomPriceDTO
                {
                    Rooms = Convert.ToInt32(g.Key),
                    AveragePrice = g.Average(ad => Convert.ToDouble(ad.AdParameters.FirstOrDefault(p => p.P == "square_meter")?.V ?? 0))
                })
                .OrderBy(info => info.Rooms)
                .ToList();

            Console.WriteLine("\nЗависимость стоимости квадратного метра от количества комнат:");
            foreach (var item in roomsPrices)
            {
                Console.WriteLine($"Количество комнат: {item.Rooms}, Средняя стоимость за м²: {item.AveragePrice:F2}");
            }
        }

        private void PrintMetroPriceDependence(List<Ad> ads)
        {
            var metroPrices = ads
                .Where(ad => ad.AdParameters.FirstOrDefault(p => p.P == "metro")?.Vl != null)
                .GroupBy(ad => ((JArray)ad.AdParameters.FirstOrDefault(p => p.P == "metro")?.Vl).First())
                .Select(g => new MetroPriceDTO
                {
                    MetroStation = g.Key.ToString(),
                    AveragePrice = g.Average(ad => Convert.ToDouble(ad.AdParameters.FirstOrDefault(p => p.P == "square_meter")?.V ?? 0))
                })
                .ToList();

            Console.WriteLine("\nЗависимость стоимости от ближайшей станции метро:");
            foreach (var item in metroPrices)
            {
                Console.WriteLine($"Станция метро: {item.MetroStation}, Средняя стоимость за м²: {item.AveragePrice:F2}");
            }
        }
    }
}
