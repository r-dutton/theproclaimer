[web] GET /api/dataverse/financial-data  (Workpapers.Next.API.Controllers.DataverseController.GetFinancialDataSummary)  [L431–L439] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ sends_request GetBinderFinancialDataQuery [L436]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetBinderFinancialDataQueryHandler.Handle [L29–L95]
      └─ uses_service UnitOfWork
        └─ method Table [L44]

