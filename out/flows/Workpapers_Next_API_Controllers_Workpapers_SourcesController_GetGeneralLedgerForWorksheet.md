[web] GET /api/sources/{type}/ledger/for-worksheet  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetGeneralLedgerForWorksheet)  [L404–L431] [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.ReadQuery [L417]
  └─ queries WorkpaperRecord [L417]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L417]

