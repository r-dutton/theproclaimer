[web] GET /workpapers/v1/worksheets/{worksheetId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.WorksheetsController.Get)  [L45–L51] status=200
  └─ maps_to WorksheetDto [L48]
    └─ automapper.registration WorkpapersMappingProfile (Worksheet->WorksheetDto) [L507]
    └─ automapper.registration ExternalApiMappingProfile (Worksheet->WorksheetDto) [L134]
  └─ calls WorksheetRepository.ReadQuery [L48]
  └─ query Worksheet [L48]
    └─ reads_from Worksheets
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Worksheet writes=0 reads=1
    └─ mappings 1
      └─ WorksheetDto

