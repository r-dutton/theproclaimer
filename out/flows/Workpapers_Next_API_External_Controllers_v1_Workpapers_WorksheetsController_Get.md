[web] GET /workpapers/v1/worksheets/{worksheetId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.WorksheetsController.Get)  [L45–L51] status=200
  └─ maps_to WorksheetDto [L48]
    └─ automapper.registration ExternalApiMappingProfile (Worksheet->WorksheetDto) [L134]
    └─ automapper.registration WorkpapersMappingProfile (Worksheet->WorksheetDto) [L507]
  └─ calls WorksheetRepository.ReadQuery [L48]
  └─ query Worksheet [L48]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L48]
      └─ ... (no implementation details available)

