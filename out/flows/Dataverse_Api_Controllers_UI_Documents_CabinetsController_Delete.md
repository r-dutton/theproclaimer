[web] DELETE /api/ui/documents/cabinets/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.CabinetsController.Delete)  [L70–L80] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls CabinetRepository.Remove [L77]
  └─ calls CabinetRepository.WriteQuery [L73]
  └─ writes_to Cabinet [L77]
    └─ reads_from Cabinets
  └─ writes_to Cabinet [L73]
    └─ reads_from Cabinets
  └─ uses_service IControlledRepository<Cabinet>
    └─ method WriteQuery [L73]

