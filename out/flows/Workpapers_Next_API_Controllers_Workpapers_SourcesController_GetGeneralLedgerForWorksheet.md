[web] GET /api/sources/{type}/ledger/for-worksheet  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetGeneralLedgerForWorksheet)  [L404–L431] status=200 [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.ReadQuery [L417]
  └─ query WorkpaperRecord [L417]
    └─ reads_from WorkpaperRecords
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ WorkpaperRecord writes=0 reads=1

