[web] POST /api/officeUsers/  (Cirrus.Api.Controllers.Firm.OfficeUsersController.Create)  [L73–L79] status=201 [auth=user,admin]
  └─ calls OfficeUserRepository.Add [L77]
  └─ insert OfficeUser [L77]
    └─ reads_from OfficeUsers
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ OfficeUser writes=1 reads=0

