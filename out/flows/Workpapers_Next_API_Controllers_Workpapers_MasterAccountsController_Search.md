[web] GET /api/master-accounts/search  (Workpapers.Next.API.Controllers.Workpapers.MasterAccountsController.Search)  [L76–L80] [auth=AuthorizationPolicies.Administrator]
  └─ sends_request FindMasterAccountsQuery [L79]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.MasterAccounting.Queries.FindMasterAccountsQueryHandler.Handle [L42–L100]
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method ReadQuery [L58]
      └─ uses_service IControlledRepository<StandardChart>
        └─ method ReadQuery [L63]

