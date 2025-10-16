[web] DELETE /api/officeUsers/{id:int}  (Cirrus.Api.Controllers.Firm.OfficeUsersController.Delete)  [L95–L101] status=200 [auth=user,admin]
  └─ calls OfficeUserRepository (methods: Remove,WriteQuery) [L99]
  └─ delete OfficeUser [L99]
    └─ reads_from OfficeUsers
  └─ write OfficeUser [L98]
    └─ reads_from OfficeUsers
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ OfficeUser writes=2 reads=0

