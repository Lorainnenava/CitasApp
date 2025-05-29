using MyApp.Application.DTOs.ChangeHospitalRequests;
using MyApp.Domain.Entities;

namespace MyApp.Tests.Mocks
{
    public class MockChangeHospitalRequest
    {
        public static ChangeHospitalRequestCreateRequest MockChangeHospitalRequestCreateRequest()
        {
            return new ChangeHospitalRequestCreateRequest
            {
                UserId = 1,
                CurrentHospitalId = 1,
                NewHospitalId = 2,
                ReasonForChange = "Need better facilities"
            };
        }

        public static ChangeHospitalRequestCreateRequest MockChangeHospitalRequestCreateRequestDouble()
        {
            return new ChangeHospitalRequestCreateRequest
            {
                UserId = 1,
                CurrentHospitalId = 1,
                NewHospitalId = 1,
                ReasonForChange = "Need better facilities"
            };
        }

        public static ChangeHospitalRequestsEntity MockChangeHospitalRequestEntity()
        {
            return new ChangeHospitalRequestsEntity
            {
                ChangeHospitalRequestId = 1,
                UserId = 1,
                CurrentHospitalId = 1,
                NewHospitalId = 2,
                StatusId = 13,
                ReasonForChange = "Need better facilities",
            };
        }

        public static ChangeHospitalRequestCreateRequest MockChangeHospitalRequestEntityInvalid()
        {
            return new ChangeHospitalRequestCreateRequest
            {
                UserId = 1,
                CurrentHospitalId = 1,
                NewHospitalId = 2,
                ReasonForChange = ""
            };
        }
    }
}
