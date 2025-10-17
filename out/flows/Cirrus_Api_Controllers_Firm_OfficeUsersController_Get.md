[web] GET /api/officeUsers/{id}  (Cirrus.Api.Controllers.Firm.OfficeUsersController.Get)  [L41–L47] status=200 [auth=user]
  └─ maps_to OfficeUserDto [L44]
    └─ automapper.registration CirrusMappingProfile (OfficeUser->OfficeUserDto) [L130]
  └─ calls OfficeUserRepository.ReadQuery [L44]
  └─ query OfficeUser [L44]
    └─ reads_from OfficeUsers
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ OfficeUser writes=0 reads=1
    └─ mappings 1
      └─ OfficeUserDto

