using Newtonsoft.Json.Linq;
using TestTask.Models;

namespace TestTask.Services
{
    public class SearchApartmentInClosedFigureService
    {
        public List<Ad> FindApartmentsInPolygon(List<Ad> ads, List<double[]> polygonPoints)
        {
            if (!IsPolygonValid(polygonPoints))
            {
                throw new ArgumentException("\nПередаваемые точки не представляют собой замкнутую фигуру.");
            }

            List<Ad> apartmentsInPolygon = new List<Ad>();

            foreach (var ad in ads)
            {
                var coordinates = ad.AdParameters.FirstOrDefault(p => p.P == "coordinates")?.V as JArray;
                if (coordinates != null && coordinates.Count == 2)
                {
                    double x = coordinates[0].ToObject<double>();
                    double y = coordinates[1].ToObject<double>();

                    if (IsPointInPolygon(x, y, polygonPoints))
                    {
                        apartmentsInPolygon.Add(ad);
                    }
                }
            }

            return apartmentsInPolygon; 
        }

        public bool IsPolygonValid(List<double[]> polygonPoints)
        {
            if (polygonPoints == null || polygonPoints.Count < 3)
            {
                return false;
            }
            foreach (var point in polygonPoints)
            {
                if (point.Length != 2 ||
                    point[0] < -180 || point[0] > 180 || // долгота
                    point[1] < -90 || point[1] > 90) //  широта
                {
                    return false;
                }
            }
            double[] firstPoint = polygonPoints[0];
            double[] lastPoint = polygonPoints[polygonPoints.Count - 1];

            return firstPoint[0] == lastPoint[0] && firstPoint[1] == lastPoint[1];
        }

        public bool IsPointInPolygon(double x, double y, List<double[]> polygonPoints)
        {
            bool inside = false;
            int j = polygonPoints.Count - 1;

            for (int i = 0; i < polygonPoints.Count; i++)
            {
                if (((polygonPoints[i][1] > y) != (polygonPoints[j][1] > y)) &&
                    (x < (polygonPoints[j][0] - polygonPoints[i][0]) * (y - polygonPoints[i][1]) / (polygonPoints[j][1] - polygonPoints[i][1]) + polygonPoints[i][0]))
                {
                    inside = !inside;
                }
                j = i;
            }

            return inside;
        }

        public static void PrintResults(List<Ad> apartments)
        {
            int nubmer = 1;
            Console.WriteLine($"\nКоличество квартир в заданной фигуре: {apartments.Count}");
            foreach (var apartment in apartments)
            {
                Console.WriteLine($"{nubmer++}. ID Квартиры: {apartment.AdId}");
            }
        }
    }
}