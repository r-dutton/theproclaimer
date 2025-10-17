[web] GET /workpapers/v1/worksheets/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.WorksheetsController.GetWorksheetsDetailedQuery)  [L77–L83] status=200
  └─ calls WorksheetRepository.ReadQuery [L80]
  └─ query Worksheet [L80]
    └─ reads_from Worksheets
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Worksheet writes=0 reads=1

