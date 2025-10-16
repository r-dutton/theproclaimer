[web] GET /api/dataverse/financial-data  (Workpapers.Next.API.Controllers.DataverseController.GetFinancialDataSummary)  [L432–L440] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ sends_request GetBinderFinancialDataQuery [L437]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetBinderFinancialDataQueryHandler.Handle [L29–L95]
      └─ uses_service UnitOfWork
        └─ method Table [L44]
          └─ ... (no implementation details available)

