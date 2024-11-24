using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;

namespace ShopTARgv23.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            //Arrange
            RealEstateDto dto = new();

            dto.Location = "asd";
            dto.Size = 123;
            dto.RoomNumber = 123;
            dto.BuildingType = "asd";
            dto.CreatedAt = DateTime.Now;
            dto.ModifiedAt = DateTime.Now;

            //Act
            var result = await Svc<IRealEstate>().Create(dto);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
        {
            //Arrange
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("9fdcacaa-8842-478a-ad87-472766b485c8");

            //Act
            await Svc<IRealEstate>().Details(guid);

            //Assert
            Assert.NotEqual(wrongGuid, guid);

        }

        [Fact]
        public async Task Should_GetByIdRealEstate_WhenReturnsEqual()
        {
            //Arrange
            Guid correctGuid = Guid.Parse("9fdcacaa-8842-478a-ad87-472766b485c8");
            Guid guid = Guid.Parse("9fdcacaa-8842-478a-ad87-472766b485c8");

            //Act
            await Svc<IRealEstate>().Details(guid);

            //Assert
            Assert.Equal(correctGuid, guid);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealestate()
        {
            RealEstateDto realEstate = MockRealEstateData();

            var created = await Svc<IRealEstate>().Create(realEstate);
            var result = await Svc<IRealEstate>().Delete((Guid)created.Id);

            Assert.Equal(result, created);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealestate()
        {
            RealEstateDto realEstate = MockRealEstateData();

            var created1 = await Svc<IRealEstate>().Create(realEstate);
            var created2 = await Svc<IRealEstate>().Create(realEstate);

            var result = await Svc<IRealEstate>().Delete((Guid)created2.Id);

            Assert.NotEqual(created1.Id, result.Id);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            var guid = Guid.Parse("9fdcacaa-8842-413a-ad87-472766b485c8");

            //new data
            RealEstateDto dto = MockRealEstateData();

            //from db data
            RealEstate domain = new();

            domain.Id = Guid.Parse("9fdcacaa-8842-413a-ad87-472766b485c8");
            domain.Location = "qwerty";
            domain.Size = 123;
            domain.RoomNumber = 3;
            domain.BuildingType = "qwerty";
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

            var result = await Svc<IRealEstate>().Update(dto);

            Assert.Equal(domain.Id, guid);
            Assert.DoesNotMatch(domain.Location, dto.Location);
            Assert.DoesNotMatch(domain.RoomNumber.ToString(), dto.RoomNumber.ToString());
            Assert.NotEqual(domain.Size, dto.Size);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateDataVersion2()
        {
            RealEstateDto dto = MockRealEstateData();
            RealEstateDto update = MockRealEstateData2();

            var created1 = await Svc<IRealEstate>().Create(dto);
            var result = await Svc<IRealEstate>().Update(update);

            Assert.Equal(created1.Location, result.Location);
            Assert.Equal(created1.RoomNumber, result.RoomNumber);
            Assert.True(created1.Size == result.Size);

            Assert.DoesNotMatch(result.Location, created1.Location);
            Assert.NotEqual(result.ModifiedAt, created1.ModifiedAt);
        }

        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenNotUpdateData()
        {
            RealEstateDto dto = MockRealEstateData();
            var created = await Svc<IRealEstate>().Create(dto);

            RealEstateDto nullUpdate = MockNullRealEstateData();
            var result = await Svc<IRealEstate>().Update(nullUpdate);

            var nullId = nullUpdate.Id;

            Assert.True(dto.Id == nullId);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenRealEstateDoesNotExist()
        {
            Guid nonExistingId = Guid.NewGuid();

            var result = await Svc<IRealEstate>().Details(nonExistingId);

            Assert.Null(result);
        }

        private RealEstateDto MockRealEstateData2()
        {
            RealEstateDto realEstate = new()
            {
                Location = "asd",
                Size = 100,
                RoomNumber = 2,
                BuildingType = "asd",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };
            return realEstate;
        }
        private RealEstateDto MockRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Location = "asd2",
                Size = 50,
                RoomNumber = 1,
                BuildingType = "asd",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };
            return realEstate;
        }
        private RealEstateDto MockNullRealEstateData()
        {
            RealEstateDto nullDto = new()
            {
                Id = null,
                Location = "asd",
                Size = 100,
                RoomNumber = 2,
                BuildingType = "asd",
                CreatedAt = DateTime.Now.AddYears(-1),
                ModifiedAt = DateTime.Now.AddYears(-1),
            };
            return nullDto;
        }
    }
}