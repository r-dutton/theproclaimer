[web] GET /api/starters/byproduct/{ProductId:Guid}  (Workpapers.Next.API.Controllers.Templates.StartersController.GetByProduct)  [L61–L67]
  └─ maps_to StarterDto [L66]
  └─ uses_service IMapper
    └─ method Map [L66]
  └─ uses_service UserService
    └─ method GetUser [L64]
  └─ sends_request AllStartersForProductQuery [L64]
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

