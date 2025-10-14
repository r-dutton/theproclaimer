[web] GET /api/starters/byproduct/{ProductId:Guid}/active  (Workpapers.Next.API.Controllers.Templates.StartersController.GetByProductActive)  [L72–L79]
  └─ maps_to StarterDto [L78]
  └─ uses_service IMapper
    └─ method Map [L78]
  └─ uses_service UserService
    └─ method GetUser [L75]
  └─ sends_request AllStartersForProductQuery [L75]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Queries.Templates.Starters.AllStartersForProductQueryHandler.Handle [L17–L63]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L35]
      └─ uses_service UnitOfWork
        └─ method Table [L40]
      └─ uses_service UserService
        └─ method IsSuperUser [L35]

