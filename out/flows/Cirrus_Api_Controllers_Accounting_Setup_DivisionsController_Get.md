[web] GET /api/accounting/divisions/{id}  (Cirrus.Api.Controllers.Accounting.Setup.DivisionsController.Get)  [L41–L47] status=200 [auth=user]
  └─ maps_to DivisionDto [L44]
    └─ automapper.registration CirrusMappingProfile (Division->DivisionDto) [L225]
  └─ calls DivisionRepository.ReadQuery [L44]
  └─ query Division [L44]
    └─ reads_from Divisions
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Division writes=0 reads=1
    └─ mappings 1
      └─ DivisionDto

