[web] POST /api/officeUsers/  (Cirrus.Api.Controllers.Firm.OfficeUsersController.Create)  [L73–L79] [auth=user,admin]
  └─ calls OfficeUserRepository.Add [L77]
  └─ writes_to OfficeUser [L77]
    └─ reads_from OfficeUsers
  └─ uses_service IControlledRepository<OfficeUser>
    └─ method Add [L77]

