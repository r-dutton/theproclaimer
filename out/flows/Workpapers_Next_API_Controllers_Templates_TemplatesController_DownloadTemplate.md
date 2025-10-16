[web] GET /api/templates/{id:Guid}/download  (Workpapers.Next.API.Controllers.Templates.TemplatesController.DownloadTemplate)  [L72–L77] status=200
  └─ sends_request DownloadTemplateQuery [L75]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.DownloadTemplateQueryHandler.Handle [L24–L60]
      └─ uses_service StorageService
        └─ method CreateDownloadUrl [L58]
          └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl [L17-L282]
      └─ uses_service IControlledRepository<Template>
        └─ method ReadQuery [L45]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L57]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_storage StorageService.CreateDownloadUrl [L58]

