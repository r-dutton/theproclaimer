[web] POST /api/worksheets/legacyworksheetsmapper  (Workpapers.Next.API.Controllers.WorksheetsController.LegacyWorksheetsMapper)  [L124–L162] status=201
  └─ sends_request GetWorkpaperRecordTemplateIdFromLegacyDocumentIdQuery -> GetWorkpaperRecordTemplateIdFromLegacyDocumentIdQueryHandler [L147]
    └─ handled_by Workpapers.Next.Data.Queries.Workpapers.Legacy.GetWorkpaperRecordTemplateIdFromLegacyDocumentIdQueryHandler.Handle [L13–L34]
      └─ uses_service UnitOfWork
        └─ method SqlQuery [L24]
          └─ implementation UnitOfWork.SqlQuery
  └─ impact_summary
    └─ requests 1
      └─ GetWorkpaperRecordTemplateIdFromLegacyDocumentIdQuery
    └─ handlers 1
      └─ GetWorkpaperRecordTemplateIdFromLegacyDocumentIdQueryHandler

