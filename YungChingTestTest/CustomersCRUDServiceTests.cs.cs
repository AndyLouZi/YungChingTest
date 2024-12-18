using NUnit.Framework;
using Moq;
using Microsoft.EntityFrameworkCore;
using Infrasturcture;
using InterFace;
using Models;
using Services;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestFixture]
    public class CustomersCRUDServiceTests
    {
        private Mock<ApplicationDbContext> _mockContext;
        private ICustomersCRUDService _customersCRUDService;

        [SetUp]
        public void Setup()
        {
            // 設置 InMemory 資料庫模擬
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "northwind")
                .Options;

            // 初始化模擬的 DbContext 和 Service
            _mockContext = new Mock<ApplicationDbContext>(options);
            _customersCRUDService = new CustomersCRUDService(_mockContext.Object);
        }


        [Test]
        public void CreateCustomer_ShouldReturnFalse_WhenCustomerIsNull()
        {
            // Arrange
            Customers? customer = null;

            // Act
            var result = _customersCRUDService.CreateCustomer(customer);

        }



    }
}
