[web] GET /api/officeUsers/foroffice/{officeId:Guid}  (Cirrus.Api.Controllers.Firm.OfficeUsersController.GetAllForOffice)  [L56–L67] [auth=user]
  └─ maps_to OfficeUserWithUserDto [L61]
    └─ automapper.registration CirrusMappingProfile (OfficeUser->OfficeUserWithUserDto) [L131]
    └─ automapper.registration DataverseMappingProfile (OfficeUser->OfficeUserWithUserDto) [L143]
  └─ calls OfficeUserRepository.ReadQuery [L61]
  └─ queries OfficeUser [L61]
    └─ reads_from OfficeUsers
  └─ uses_service IControlledRepository<OfficeUser>
    └─ method ReadQuery [L61]

