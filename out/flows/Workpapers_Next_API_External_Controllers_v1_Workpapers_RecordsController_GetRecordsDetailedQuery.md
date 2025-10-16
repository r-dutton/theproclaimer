[web] GET /workpapers/v1/workpaper-records/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.RecordsController.GetRecordsDetailedQuery)  [L76–L82] status=200
  └─ calls WorkpaperRecordRepository.ReadQuery [L79]
  └─ query WorkpaperRecord [L79]
    └─ reads_from WorkpaperRecords
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ WorkpaperRecord writes=0 reads=1

