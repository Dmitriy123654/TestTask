
using Newtonsoft.Json;
using TestTask.Models;
using TestTask.Models.DTO;
using TestTask.Services;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        try
        {
            var ads = await FetchData("https://api.kufar.by/search-api/v2/search/rendered-paginated?cat=1010&cur=USD&gtsy=country-belarus~province-minsk~locality-minsk&lang=ru&size=30&typ=sell");
            var ads2 = await FetchData("https://api.kufar.by/search-api/v2/search/rendered-paginated?cat=1010&cur=USD&gtsy=country-belarus~province-minsk~locality-minsk&lang=ru&rnt=2&size=30&typ=let");
            Console.WriteLine("\tЗадание 1\n");
            AnalyzePricePerSquareMeter(ads);
            Console.WriteLine("\n\tЗадание 2");
            FindApartmentsInPolygon(ads);
            Console.WriteLine("\n\tЗадание 3");
            FindApartmentsInDistrict(ads2);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task<List<Ad>> FetchData(string url)
    {
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var root = JsonConvert.DeserializeObject<Root>(json);
        return root.Ads;
    }

    private static void AnalyzePricePerSquareMeter(List<Ad> ads)
    {
        var priceAnalysis = new PricePerSquareMeterAnalysisService();
        priceAnalysis.Analyze(ads);
    }

    private static void FindApartmentsInPolygon(List<Ad> ads)
    {
        var polygonPoints = new List<double[]>
        {
            new double[] { 27.5000, 53.9000 },
            new double[] { 27.7000, 53.9000 },
            new double[] { 27.7000, 53.9500 },
            new double[] { 27.5000, 53.9500 },
            new double[] { 27.5000, 53.9000 }
        };
        var pointInPolygonChecker = new SearchApartmentInClosedFigureService();
        var foundApartments = pointInPolygonChecker.FindApartmentsInPolygon(ads, polygonPoints);
        SearchApartmentInClosedFigureService.PrintResults(foundApartments);
    }

    private static void FindApartmentsInDistrict(List<Ad> ads)
    {
        var apartmentService = new ApartmentSearchByCriteriaService();
        var checkInDateId = 20001;
        var checkOutDateId = 20088;
        var district = "Октябрьский";
        apartmentService.FindApartmentsInDistrict(ads, district, checkInDateId, checkOutDateId);
    }
}
























