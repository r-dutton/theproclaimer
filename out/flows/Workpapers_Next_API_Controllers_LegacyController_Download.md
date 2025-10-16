[web] GET /api/files/download  (Workpapers.Next.API.Controllers.LegacyController.Download)  [L69–L87] status=200
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L84]
      └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl [L17-L282]
  └─ uses_service UnitOfWork
    └─ method Table [L72]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUser [L76]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
  └─ uses_storage StorageService.CreateDownloadUrl [L84]
  └─ sends_request GetTemplateForDateTimeQuery [L76]
    └─ handled_by Workpapers.Next.Data.Queries.Templates.GetTemplateForDateTimeQueryHandler.Handle [L12–L57]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L23]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

