[web] GET /workpapers/v1/workpaper-records/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.RecordsController.GetRecordsMinimalQuery)  [L58–L64]
  └─ calls WorkpaperRecordRepository.ReadQuery [L61]
  └─ queries WorkpaperRecord [L61]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L61]

