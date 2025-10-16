[web] PUT /api/ui/documents/cabinets/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.CabinetsController.Update)  [L58–L68] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls CabinetRepository.WriteQuery [L61]
  └─ write Cabinet [L61]
    └─ reads_from Cabinets
  └─ uses_service IControlledRepository<Cabinet>
    └─ method WriteQuery [L61]
      └─ ... (no implementation details available)

