[web] GET /workpapers/v1/workpaper-records/{recordId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.RecordsController.Get)  [L44–L50]
  └─ maps_to WorkpaperRecordDto [L47]
    └─ automapper.registration ExternalApiMappingProfile (WorkpaperRecord->WorkpaperRecordDto) [L169]
    └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->WorkpaperRecordDto) [L562]
  └─ calls WorkpaperRecordRepository.ReadQuery [L47]
  └─ queries WorkpaperRecord [L47]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L47]

