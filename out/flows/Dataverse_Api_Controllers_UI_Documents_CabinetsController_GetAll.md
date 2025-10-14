[web] GET /api/ui/documents/cabinets  (Dataverse.Api.Controllers.UI.Documents.CabinetsController.GetAll)  [L34–L46] [auth=Authentication.UserPolicy]
  └─ maps_to CabinetDto [L39]
    └─ automapper.registration DataverseMappingProfile (Cabinet->CabinetDto) [L397]
  └─ calls CabinetRepository.ReadQuery [L39]
  └─ queries Cabinet [L39]
    └─ reads_from Cabinets
  └─ uses_service IControlledRepository<Cabinet>
    └─ method ReadQuery [L39]

