[web] PUT /api/offices/{id}  (Cirrus.Api.Controllers.Firm.OfficesController.Update)  [L90–L96] status=200 [auth=user,admin]
  └─ calls OfficeRepository.WriteQuery [L93]
  └─ write Office [L93]
    └─ reads_from Offices
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Office writes=1 reads=0

