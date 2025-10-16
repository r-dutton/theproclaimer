[web] GET /api/worksheets/templatewithdownload  (Workpapers.Next.API.Controllers.WorksheetsController.TemplateWithDownload)  [L104–L122] status=200
  └─ maps_to WorkpaperRecordTemplateV2Dto [L118]
    └─ converts_to LegacyDocumentDto [L343]
    └─ converts_to WorkpaperRecordTemplateV2Dto [L290]
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L119]
      └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl [L17-L282]
  └─ uses_service UnitOfWork
    └─ method Table [L118]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUser [L107]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
  └─ uses_storage StorageService.CreateDownloadUrl [L119]
  └─ sends_request GetTemplateForDateTimeQuery [L107]
    └─ handled_by Workpapers.Next.Data.Queries.Templates.GetTemplateForDateTimeQueryHandler.Handle [L12–L57]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L23]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

