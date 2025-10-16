[web] GET /api/master-accounts/search  (Workpapers.Next.API.Controllers.Workpapers.MasterAccountsController.Search)  [L76–L80] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request FindMasterAccountsQuery -> FindMasterAccountsQueryHandler [L79]
    └─ handled_by Cirrus.ApplicationService.MasterAccounting.Queries.FindMasterAccountsQueryHandler.Handle [L42–L100]
      └─ uses_service IControlledRepository<StandardChart> (Scoped (inferred))
        └─ method ReadQuery [L63]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardChartRepository.ReadQuery
      └─ uses_service IControlledRepository<MasterAccount> (Scoped (inferred))
        └─ method ReadQuery [L58]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.MasterAccountRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ FindMasterAccountsQuery
    └─ handlers 1
      └─ FindMasterAccountsQueryHandler

