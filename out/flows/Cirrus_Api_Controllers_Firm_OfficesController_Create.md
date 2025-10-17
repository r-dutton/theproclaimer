[web] POST /api/offices/  (Cirrus.Api.Controllers.Firm.OfficesController.Create)  [L79–L85] status=201 [auth=user,admin]
  └─ calls OfficeRepository.Add [L83]
  └─ insert Office [L83]
    └─ reads_from Offices
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Office writes=1 reads=0

