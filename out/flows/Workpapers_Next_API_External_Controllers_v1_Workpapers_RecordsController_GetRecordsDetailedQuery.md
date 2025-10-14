[web] GET /workpapers/v1/workpaper-records/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.RecordsController.GetRecordsDetailedQuery)  [L76–L82]
  └─ calls WorkpaperRecordRepository.ReadQuery [L79]
  └─ queries WorkpaperRecord [L79]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L79]

