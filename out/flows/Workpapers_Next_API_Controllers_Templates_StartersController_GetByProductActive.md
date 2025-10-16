[web] GET /api/starters/byproduct/{ProductId:Guid}/active  (Workpapers.Next.API.Controllers.Templates.StartersController.GetByProductActive)  [L72–L79] status=200
  └─ maps_to StarterDto [L78]
  └─ uses_service IMapper
    └─ method Map [L78]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUser [L75]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
  └─ sends_request AllStartersForProductQuery [L75]
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

