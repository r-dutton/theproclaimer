[web] GET /api/matter-text-templates  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.GetBinderParams)  [L181–L215] status=200
  └─ calls WorksheetRepository.ReadQuery [L205]
  └─ calls WorkpaperRecordRepository.ReadQuery [L190]
  └─ calls BinderRepository.ReadQuery [L183]
  └─ query Worksheet [L205]
    └─ reads_from Worksheets
  └─ query WorkpaperRecord [L190]
    └─ reads_from WorkpaperRecords
  └─ query Binder [L183]
    └─ reads_from Binders
  └─ impact_summary
    └─ entities 3 (writes=0, reads=3)
      └─ Binder writes=0 reads=1
      └─ WorkpaperRecord writes=0 reads=1
      └─ Worksheet writes=0 reads=1

