[web] GET /api/ui/documents/cabinets  (Dataverse.Api.Controllers.UI.Documents.CabinetsController.GetAll)  [L34–L46] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to CabinetDto [L39]
    └─ automapper.registration DataverseMappingProfile (Cabinet->CabinetDto) [L398]
  └─ calls CabinetRepository.ReadQuery [L39]
  └─ query Cabinet [L39]
    └─ reads_from Cabinets
  └─ uses_service IControlledRepository<Cabinet>
    └─ method ReadQuery [L39]
      └─ ... (no implementation details available)

