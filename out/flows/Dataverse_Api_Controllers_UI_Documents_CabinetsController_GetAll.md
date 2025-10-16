[web] GET /api/ui/documents/cabinets  (Dataverse.Api.Controllers.UI.Documents.CabinetsController.GetAll)  [L34–L46] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to CabinetDto [L39]
    └─ automapper.registration DataverseMappingProfile (Cabinet->CabinetDto) [L398]
  └─ calls CabinetRepository.ReadQuery [L39]
  └─ query Cabinet [L39]
    └─ reads_from Cabinets
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Cabinet writes=0 reads=1
    └─ mappings 1
      └─ CabinetDto

