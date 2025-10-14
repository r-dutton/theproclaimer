[web] DELETE /api/officeUsers/{id:int}  (Cirrus.Api.Controllers.Firm.OfficeUsersController.Delete)  [L95–L101] [auth=user,admin]
  └─ calls OfficeUserRepository.Remove [L99]
  └─ calls OfficeUserRepository.WriteQuery [L98]
  └─ writes_to OfficeUser [L99]
    └─ reads_from OfficeUsers
  └─ writes_to OfficeUser [L98]
    └─ reads_from OfficeUsers
  └─ uses_service IControlledRepository<OfficeUser>
    └─ method WriteQuery [L98]

