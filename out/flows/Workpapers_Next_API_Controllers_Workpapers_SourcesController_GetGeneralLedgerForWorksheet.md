[web] GET /api/sources/{type}/ledger/for-worksheet  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetGeneralLedgerForWorksheet)  [L404–L431] status=200 [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.ReadQuery [L417]
  └─ query WorkpaperRecord [L417]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L417]
      └─ ... (no implementation details available)

