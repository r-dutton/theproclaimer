[web] GET /workpapers/v1/worksheets/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.WorksheetsController.GetWorksheetsMinimalQuery)  [L59–L65] status=200
  └─ calls WorksheetRepository.ReadQuery [L62]
  └─ query Worksheet [L62]
    └─ reads_from Worksheets
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Worksheet writes=0 reads=1

