[web] DELETE /api/officeUsers/{id:int}  (Cirrus.Api.Controllers.Firm.OfficeUsersController.Delete)  [L95–L101] status=200 [auth=user,admin]
  └─ calls OfficeUserRepository.Remove [L99]
  └─ calls OfficeUserRepository.WriteQuery [L98]
  └─ delete OfficeUser [L99]
    └─ reads_from OfficeUsers
  └─ write OfficeUser [L98]
    └─ reads_from OfficeUsers
  └─ uses_service IControlledRepository<OfficeUser>
    └─ method WriteQuery [L98]
      └─ ... (no implementation details available)

