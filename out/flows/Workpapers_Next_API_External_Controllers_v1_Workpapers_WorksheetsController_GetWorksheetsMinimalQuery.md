[web] GET /workpapers/v1/worksheets/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.WorksheetsController.GetWorksheetsMinimalQuery)  [L59–L65] status=200
  └─ calls WorksheetRepository.ReadQuery [L62]
  └─ query Worksheet [L62]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L62]
      └─ ... (no implementation details available)

