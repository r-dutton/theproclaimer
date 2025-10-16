[web] DELETE /api/ui/documents/cabinets/{id:Guid}  (Dataverse.Api.Controllers.UI.Documents.CabinetsController.Delete)  [L70–L80] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls CabinetRepository (methods: Remove,WriteQuery) [L77]
  └─ delete Cabinet [L77]
    └─ reads_from Cabinets
  └─ write Cabinet [L73]
    └─ reads_from Cabinets
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ Cabinet writes=2 reads=0

