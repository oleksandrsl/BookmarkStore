using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using BookmarksStore.Services;
using BookmarksStore.Models;
using BookmarksStore.Services.StorageService;

namespace BookmarksStoreUnitTests.Services
{
    [TestFixture]
    public class CatalogServiceUnitTests
    {
        CatalogService _catalogService;
        Mock<BookmarkStorage> _mockBookmarkProvider;
        ApplicationUser _user = new ApplicationUser();

        List<CatalogModel> _catalogs = new List<CatalogModel>()
            {
                new CatalogModel() { Id = 1, Title = "Social",ParentId = 0, OwnerId = "1" },
                new CatalogModel() { Id =2, Title = "Search",ParentId = 0, OwnerId = "1" },
                new CatalogModel() { Id =3, Title = "Holidat",ParentId = 0, OwnerId = "1" },
                new CatalogModel() { Id = 4, Title = "Other",ParentId = 1, OwnerId = "2" },
                new CatalogModel() { Id = 5, Title = "Social",ParentId = 0, OwnerId = "1" }
        };

        [SetUp]
        public void Init()
        {

            _mockBookmarkProvider = new Mock<BookmarkStorage>();
            _catalogService = new CatalogService();
            _catalogService.Init(_mockBookmarkProvider.Object);


        }

        [Test]
        public void Load_ShouldReturnCatalogs()
        {

            List<CatalogModel> expectedResult;
            List<CatalogModel> actualResult;

            var ownerIds = new string[] { "1", "2" };

            for (var i = 0; i < ownerIds.Length; i++)
            {

                expectedResult = _catalogs.FindAll(a => a.OwnerId == ownerIds[i]);
                _mockBookmarkProvider.Setup(a => a.List()).Returns(expectedResult);
                actualResult = _catalogService.Load().ToList();


                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public void LoadByParentId_ShouldReturnExpectedCatalogs_InCaseWhenCatalogWithParentIdExist()
        {

            List<CatalogModel> expectedResult;
            List<CatalogModel> actualResult;

            var parentIds = new int[] { 0, 1 };
            var ownerIds = new string[] { "1", "2" };

            for (var i = 0; i < parentIds.Length; i++)
            {
                expectedResult = _catalogs.FindAll(a => a.OwnerId == ownerIds[i] && a.ParentId == parentIds[i]);
                _mockBookmarkProvider.Setup(a => a.FindByParentId(It.IsAny<int>())).Returns(expectedResult);

                actualResult = _catalogService.LoadByParentId(parentIds[i]).ToList();

                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public void LoadByParentId_ShouldReturnEmptyList_InCaseWhenParentNotExist()
        {

            var parentIds = new int[] { 100, 200, 300 };
            var ownerIds = new string[] { "1", "2", "1" };

            for (var i = 0; i < parentIds.Length; i++)
            {

                var expectedResult = new List<CatalogModel>();
                var actualResult = _catalogService.LoadByParentId(parentIds[i]);
                _mockBookmarkProvider.Setup(a => a.FindByParentId(It.IsAny<int>())).Returns(expectedResult);
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public void LoadById_ShouldReturnExpectedCatalog_InCaseWhenCatalogWithIdExist()
        {
            var catalogIds = new int[] { 1, 2, 3, 4, 5 };
            for (var i = 0; i < catalogIds.Length; i++)
            {
                var currentCatalogId = catalogIds[i];
                var expectedResult = _catalogs.First(a => a.Id == currentCatalogId);
                _mockBookmarkProvider.Setup(a => a.FindById(It.IsAny<int>())).Returns(expectedResult);
                var actualResult = _catalogService.LoadById(currentCatalogId);

                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        [Test]
        public void LoadById_ShouldReturnEmptyCatalog_InCaseWhenCatalogWithIdNotExist()
        {
            var catalogIds = new int[] { 100, 200, 300, 400, 500 };
            for (var i = 0; i < catalogIds.Length; i++)
            {
                var currentCatalogId = catalogIds[i];
                var expectedResult = new CatalogModel();
                _mockBookmarkProvider.Setup(a => a.FindById(It.IsAny<int>())).Returns(expectedResult);
                var actualResult = _catalogService.LoadById(currentCatalogId);

                Assert.AreEqual(expectedResult, actualResult);
            }
        }
    }
}
