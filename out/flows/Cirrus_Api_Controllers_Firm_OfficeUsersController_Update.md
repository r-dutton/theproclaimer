[web] PUT /api/officeUsers/{id}  (Cirrus.Api.Controllers.Firm.OfficeUsersController.Update)  [L84–L90] status=200 [auth=user,admin]
  └─ calls OfficeUserRepository.WriteQuery [L87]
  └─ write OfficeUser [L87]
    └─ reads_from OfficeUsers
  └─ uses_service IControlledRepository<OfficeUser>
    └─ method WriteQuery [L87]
      └─ ... (no implementation details available)

