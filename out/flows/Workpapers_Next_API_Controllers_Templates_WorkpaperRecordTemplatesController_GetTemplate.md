[web] GET /api/workpaper-record-templates/{id:Guid}/template  (Workpapers.Next.API.Controllers.Templates.WorkpaperRecordTemplatesController.GetTemplate)  [L78–L82] status=200
  └─ sends_request GetWorkpaperRecordTemplateToInsertQuery -> GetWorkpaperRecordTemplateToInsertQueryHandler [L81]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Templates.GetWorkpaperRecordTemplateToInsertQueryHandler.Handle [L36–L71]
      └─ maps_to TemplateLightDto [L69]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L62]
          └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L57]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetWorkpaperRecordTemplateToInsertQuery
    └─ handlers 1
      └─ GetWorkpaperRecordTemplateToInsertQueryHandler
    └─ mappings 1
      └─ TemplateLightDto

