[web] GET /workpapers/v1/workpaper-records/{recordId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.RecordsController.Get)  [L44–L50] status=200
  └─ maps_to WorkpaperRecordDto [L47]
    └─ automapper.registration ExternalApiMappingProfile (WorkpaperRecord->WorkpaperRecordDto) [L169]
    └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->WorkpaperRecordDto) [L562]
  └─ calls WorkpaperRecordRepository.ReadQuery [L47]
  └─ query WorkpaperRecord [L47]
    └─ reads_from WorkpaperRecords
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ WorkpaperRecord writes=0 reads=1
    └─ mappings 1
      └─ WorkpaperRecordDto

