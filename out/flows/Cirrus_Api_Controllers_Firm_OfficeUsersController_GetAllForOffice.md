[web] GET /api/officeUsers/foroffice/{officeId:Guid}  (Cirrus.Api.Controllers.Firm.OfficeUsersController.GetAllForOffice)  [L56–L67] status=200 [auth=user]
  └─ maps_to OfficeUserWithUserDto [L61]
    └─ automapper.registration CirrusMappingProfile (OfficeUser->OfficeUserWithUserDto) [L131]
  └─ calls OfficeUserRepository.ReadQuery [L61]
  └─ query OfficeUser [L61]
    └─ reads_from OfficeUsers
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ OfficeUser writes=0 reads=1
    └─ mappings 1
      └─ OfficeUserWithUserDto

