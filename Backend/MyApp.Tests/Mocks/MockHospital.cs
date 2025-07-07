using MyApp.Domain.Entities;

namespace MyApp.Tests.Mocks
{
    public class MockHospital
    {
        public static HospitalsEntity MockHospitalEntity()
        {
            return new HospitalsEntity
            {
                HospitalId = 1,
                Name = "Mock Hospital",
                MunicipalityId = 1,
                Address = "123 Mock St",
                PhoneNumber = "123-456-7890",
                Email = "hospital@gmail.com",
                IsActive = true,
            };
        }

        public static HospitalsEntity MockHospitalEntityTwo()
        {
            return new HospitalsEntity
            {
                HospitalId = 2,
                Name = "Mock Hospital2",
                MunicipalityId = 1,
                Address = "123 Mock St",
                PhoneNumber = "123-456-7890",
                Email = "hospital@gmail.com",
                IsActive = true,
            };
        }
    }
}
