[web] GET /api/reportance/cirrus/starters  (Workpapers.Next.API.Controllers.Reportance.CirrusController.GetStarters)  [L58–L69] status=200 [auth=AuthorizationPolicies.M2M]
  └─ maps_to StarterDto [L68]
  └─ uses_service IMapper
    └─ method Map [L68]
      └─ ... (no implementation details available)
  └─ sends_request AllStartersForProductQuery [L65]
    └─ handled_by Workpapers.Next.Data.Queries.Templates.Starters.AllStartersForProductQueryHandler.Handle [L17–L63]
      └─ uses_service UserService
        └─ method IsSuperUser [L35]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L35]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service UnitOfWork
        └─ method Table [L40]
          └─ ... (no implementation details available)

