[web] GET /workpapers/v1/workpaper-records/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.RecordsController.GetRecordsMinimalQuery)  [L58–L64] status=200
  └─ calls WorkpaperRecordRepository.ReadQuery [L61]
  └─ query WorkpaperRecord [L61]
    └─ reads_from WorkpaperRecords
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ WorkpaperRecord writes=0 reads=1

