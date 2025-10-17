[web] GET /api/offices/{id}  (Cirrus.Api.Controllers.Firm.OfficesController.Get)  [L43–L49] status=200 [auth=user]
  └─ maps_to OfficeDto [L46]
    └─ automapper.registration CirrusMappingProfile (Office->OfficeDto) [L128]
  └─ calls OfficeRepository.ReadQuery [L46]
  └─ query Office [L46]
    └─ reads_from Offices
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ mappings 1
      └─ OfficeDto

