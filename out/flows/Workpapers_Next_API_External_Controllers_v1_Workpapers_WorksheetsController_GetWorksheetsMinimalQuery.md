[web] GET /workpapers/v1/worksheets/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.WorksheetsController.GetWorksheetsMinimalQuery)  [L59–L65]
  └─ calls WorksheetRepository.ReadQuery [L62]
  └─ queries Worksheet [L62]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L62]

