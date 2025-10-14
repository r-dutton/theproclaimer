[web] GET /api/matter-text-templates  (Workpapers.Next.API.Controllers.Workpapers.MatterTextTemplatesController.GetBinderParams)  [L181–L215]
  └─ calls BinderRepository.ReadQuery [L183]
  └─ calls WorkpaperRecordRepository.ReadQuery [L190]
  └─ calls WorksheetRepository.ReadQuery [L205]
  └─ queries Binder [L183]
    └─ reads_from Binders
  └─ queries WorkpaperRecord [L190]
    └─ reads_from WorkpaperRecords
  └─ queries Worksheet [L205]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L183]
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L190]
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L205]

