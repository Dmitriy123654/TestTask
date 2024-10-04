using Newtonsoft.Json.Linq;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Tests
{
    [TestFixture]
    public class SearchApartmentInClosedFigureServiceTests
    {
        private SearchApartmentInClosedFigureService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new SearchApartmentInClosedFigureService();
        }

        [Test]
        public void FindApartmentsInPolygon_ValidPolygon_FindsApartments()
        {
            // Arrange
            var ads = new List<Ad>
            {
                new Ad
                {
                    AdId = 1,
                    AdParameters = new List<AdParameter>
                    {
                        new AdParameter { P = "coordinates", V = new JArray(27.5, 53.7) }//входит
                    }
                },
                new Ad
                {
                    AdId = 2,
                    AdParameters = new List<AdParameter>
                    {
                        new AdParameter { P = "coordinates", V = new JArray(27.1, 53.6) }//входит
                    }
                },
                new Ad
                {
                    AdId = 3,
                    AdParameters = new List<AdParameter>
                    {
                        new AdParameter { P = "coordinates", V = new JArray(28.0, 54.0) }// не входит
                    }
                }
            };
            var polygonPoints = new List<double[]>
            {
                new double[] { 27.0, 53.5 },
                new double[] { 27.6, 53.5 },
                new double[] { 27.6, 54.0 },
                new double[] { 27.0, 54.0 },
                new double[] { 27.0, 53.5 }
            };

            // Act
            var foundApartments = _service.FindApartmentsInPolygon(ads, polygonPoints);

            // Assert
            Assert.That(foundApartments.Count, Is.EqualTo(2)); // Квартиры с ID 1 и 2 находятся внутри полигона
            Assert.IsTrue(foundApartments.Exists(ad => ad.AdId == 1));
            Assert.IsTrue(foundApartments.Exists(ad => ad.AdId == 2));
            Assert.IsFalse(foundApartments.Exists(ad => ad.AdId == 3));
        }

        [Test]
        public void FindApartmentsInPolygon_InvalidPolygon_ThrowsArgumentException()
        {
            // Arrange
            var ads = new List<Ad>();
            var invalidPolygonPoints = new List<double[]>
            {
                new double[] { 27.0, 53.5 },
                new double[] { 27.0, 53.5 } //меньше 3 точек
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                _service.FindApartmentsInPolygon(ads, invalidPolygonPoints));

            Assert.That(exception.Message, Is.EqualTo("\nПередаваемые точки не представляют собой замкнутую фигуру."));
        }

        [Test]
        public void FindApartmentsInPolygon_ValidPolygon_NoApartmentsFound()
        {
            // Arrange
            var ads = new List<Ad>
            {
                new Ad
                {
                    AdId = 4,
                    AdParameters = new List<AdParameter>
                    {
                        new AdParameter { P = "coordinates", V = new JArray(28.0, 54.0) }
                    }
                }
            };
            var polygonPoints = new List<double[]>
            {
                new double[] { 27.0, 53.5 },
                new double[] { 27.6, 53.5 },
                new double[] { 27.6, 54.0 },
                new double[] { 27.0, 54.0 },
                new double[] { 27.0, 53.5 }
            };

            // Act
            var foundApartments = _service.FindApartmentsInPolygon(ads, polygonPoints);

            // Assert
            Assert.That(foundApartments.Count, Is.EqualTo(0)); 
        }

        [Test]
        public void IsPolygonValid_InvalidPolygon_ReturnsFalse()
        {
            // Arrange
            var invalidPolygonPoints = new List<double[]>
            {
                new double[] { 27.0, 53.5 },
                new double[] { 27.0, 53.5 } // Not valid 
            };

            // Act
            var isValid = _service.IsPolygonValid(invalidPolygonPoints);

            // Assert
            Assert.IsFalse(isValid);
        }

        [Test]
        public void IsPointInPolygon_ValidPoint_ReturnsTrue()
        {
            // Arrange
            var polygonPoints = new List<double[]>
            {
                new double[] { 0, 0 },
                new double[] { 0, 5 },
                new double[] { 5, 5 },
                new double[] { 5, 0 },
                new double[] { 0, 0 }
            };
            double x = 2.5;
            double y = 2.5;

            // Act
            var isInside = _service.IsPointInPolygon(x, y, polygonPoints);

            // Assert
            Assert.IsTrue(isInside);
        }

        [Test]
        public void IsPointInPolygon_InvalidPoint_ReturnsFalse()
        {
            // Arrange
            var polygonPoints = new List<double[]>
            {
                new double[] { 0, 0 },
                new double[] { 0, 5 },
                new double[] { 5, 5 },
                new double[] { 5, 0 },
                new double[] { 0, 0 }
            };
            double x = 6;
            double y = 6;

            // Act
            var isInside = _service.IsPointInPolygon(x, y, polygonPoints);

            // Assert
            Assert.IsFalse(isInside);
        }
    }
}