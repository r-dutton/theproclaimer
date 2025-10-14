[web] GET /api/reportance/cirrus/starters  (Workpapers.Next.API.Controllers.Reportance.CirrusController.GetStarters)  [L58–L69] [auth=AuthorizationPolicies.M2M]
  └─ maps_to StarterDto [L68]
  └─ uses_service IMapper
    └─ method Map [L68]
  └─ sends_request AllStartersForProductQuery [L65]
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

