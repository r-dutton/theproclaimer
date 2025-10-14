[web] POST /api/worksheets/legacyworksheetsmapper  (Workpapers.Next.API.Controllers.WorksheetsController.LegacyWorksheetsMapper)  [L124–L162]
  └─ sends_request GetWorkpaperRecordTemplateIdFromLegacyDocumentIdQuery [L147]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Queries.Workpapers.Legacy.GetWorkpaperRecordTemplateIdFromLegacyDocumentIdQueryHandler.Handle [L13–L34]
      └─ uses_service UnitOfWork
        └─ method SqlQuery [L24]

