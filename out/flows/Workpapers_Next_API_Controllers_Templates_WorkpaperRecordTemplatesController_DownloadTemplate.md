[web] GET /api/workpaper-record-templates/{id:Guid}/download-template  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.DownloadTemplate)  [L84–L89] status=200
  └─ sends_request DownloadTemplateQuery [L88]
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
  └─ sends_request GetWorkpaperRecordTemplateToInsertQuery [L87]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetWorkpaperRecordTemplateToInsertQueryHandler.Handle [L36–L71]
      └─ maps_to TemplateLightDto [L69]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L57]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L62]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L69]
          └─ ... (no implementation details available)

