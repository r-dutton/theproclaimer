[web] GET /api/officeUsers/{id}  (Cirrus.Api.Controllers.Firm.OfficeUsersController.Get)  [L41–L47] status=200 [auth=user]
  └─ maps_to OfficeUserDto [L44]
    └─ automapper.registration DataverseMappingProfile (OfficeUser->OfficeUserDto) [L143]
    └─ automapper.registration CirrusMappingProfile (OfficeUser->OfficeUserDto) [L130]
  └─ calls OfficeUserRepository.ReadQuery [L44]
  └─ query OfficeUser [L44]
    └─ reads_from OfficeUsers
  └─ uses_service IControlledRepository<OfficeUser>
    └─ method ReadQuery [L44]
      └─ ... (no implementation details available)

