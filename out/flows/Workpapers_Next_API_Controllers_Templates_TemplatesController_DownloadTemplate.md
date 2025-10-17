[web] GET /api/templates/{id:Guid}/download  (Workpapers.Next.API.Controllers.Templates.TemplatesController.DownloadTemplate)  [L72–L77] status=200
  └─ sends_request DownloadTemplateQuery -> DownloadTemplateQueryHandler [L75]
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
  └─ impact_summary
    └─ requests 1
      └─ DownloadTemplateQuery
    └─ handlers 1
      └─ DownloadTemplateQueryHandler

