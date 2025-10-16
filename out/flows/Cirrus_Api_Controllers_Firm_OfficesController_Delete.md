[web] DELETE /api/offices/{id:Guid}  (Cirrus.Api.Controllers.Firm.OfficesController.Delete)  [L101–L107] status=200 [auth=user,admin]
  └─ calls OfficeRepository (methods: Remove,WriteQuery) [L105]
  └─ delete Office [L105]
    └─ reads_from Offices
  └─ write Office [L104]
    └─ reads_from Offices
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ Office writes=2 reads=0

