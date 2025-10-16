[web] GET /workpapers/v1/worksheets/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.WorksheetsController.GetWorksheetsDetailedQuery)  [L77–L83] status=200
  └─ calls WorksheetRepository.ReadQuery [L80]
  └─ query Worksheet [L80]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L80]
      └─ ... (no implementation details available)

