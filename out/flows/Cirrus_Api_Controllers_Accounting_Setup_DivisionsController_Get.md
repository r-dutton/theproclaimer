[web] GET /api/accounting/divisions/{id}  (Cirrus.Api.Controllers.Accounting.Setup.DivisionsController.Get)  [L41–L47] status=200 [auth=user]
  └─ maps_to DivisionDto [L44]
    └─ automapper.registration CirrusMappingProfile (Division->DivisionDto) [L225]
  └─ calls DivisionRepository.ReadQuery [L44]
  └─ query Division [L44]
    └─ reads_from Divisions
  └─ uses_service IControlledRepository<Division>
    └─ method ReadQuery [L44]
      └─ ... (no implementation details available)

