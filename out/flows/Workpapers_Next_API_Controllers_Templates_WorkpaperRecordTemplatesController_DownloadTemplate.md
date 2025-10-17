[web] GET /api/workpaper-record-templates/{id:Guid}/download-template  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.DownloadTemplate)  [L84–L89] status=200
  └─ sends_request DownloadTemplateQuery -> DownloadTemplateQueryHandler [L88]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.DownloadTemplateQueryHandler.Handle [L24–L60]
      └─ uses_service StorageService
        └─ method CreateDownloadUrl [L58]
          └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl [L17-L282]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L57]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Template> (Scoped (inferred))
        └─ method ReadQuery [L45]
          └─ implementation Workpapers.Next.Data.Repository.Templates.TemplateRepository.ReadQuery
      └─ uses_storage StorageService.CreateDownloadUrl [L58]
  └─ sends_request GetWorkpaperRecordTemplateToInsertQuery -> GetWorkpaperRecordTemplateToInsertQueryHandler [L87]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetWorkpaperRecordTemplateToInsertQueryHandler.Handle [L36–L71]
      └─ maps_to TemplateLightDto [L69]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L62]
          └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L57]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
  └─ impact_summary
    └─ requests 2
      └─ DownloadTemplateQuery
      └─ GetWorkpaperRecordTemplateToInsertQuery
    └─ handlers 2
      └─ DownloadTemplateQueryHandler
      └─ GetWorkpaperRecordTemplateToInsertQueryHandler
    └─ mappings 1
      └─ TemplateLightDto

