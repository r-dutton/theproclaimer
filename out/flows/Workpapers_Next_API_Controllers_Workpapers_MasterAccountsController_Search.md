[web] GET /api/master-accounts/search  (Workpapers.Next.API.Controllers.Workpapers.MasterAccountsController.Search)  [L76–L80] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request FindMasterAccountsQuery [L79]
    └─ handled_by Cirrus.ApplicationService.MasterAccounting.Queries.FindMasterAccountsQueryHandler.Handle [L42–L100]
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method ReadQuery [L58]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<StandardChart>
        └─ method ReadQuery [L63]
          └─ ... (no implementation details available)

