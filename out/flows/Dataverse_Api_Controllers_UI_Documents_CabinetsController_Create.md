[web] POST /api/ui/documents/cabinets  (Dataverse.Api.Controllers.UI.Documents.CabinetsController.Create)  [L48–L56] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to CabinetDto (var cabinetDto) [L53]
  └─ calls CabinetRepository.Add [L52]
  └─ insert Cabinet [L52]
    └─ reads_from Cabinets
  └─ uses_service IControlledRepository<Cabinet>
    └─ method Add [L52]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L53]
      └─ ... (no implementation details available)

