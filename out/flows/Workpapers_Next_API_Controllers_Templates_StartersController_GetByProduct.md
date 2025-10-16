[web] GET /api/starters/byproduct/{ProductId:Guid}  (Workpapers.Next.API.Controllers.Templates.StartersController.GetByProduct)  [L61–L67] status=200
  └─ maps_to StarterDto [L66]
  └─ uses_service IMapper
    └─ method Map [L66]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUser [L64]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
  └─ sends_request AllStartersForProductQuery [L64]
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

