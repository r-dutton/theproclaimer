[web] POST /api/ui/documents/cabinets  (Dataverse.Api.Controllers.UI.Documents.CabinetsController.Create)  [L48–L56] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to CabinetDto (var cabinetDto) [L53]
  └─ calls CabinetRepository.Add [L52]
  └─ insert Cabinet [L52]
    └─ reads_from Cabinets
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Cabinet writes=1 reads=0
    └─ mappings 1
      └─ CabinetDto

