[web] GET /api/dataverse/financial-data  (Workpapers.Next.API.Controllers.DataverseController.GetFinancialDataSummary)  [L432–L440] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ sends_request GetBinderFinancialDataQuery -> GetBinderFinancialDataQueryHandler [L437]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetBinderFinancialDataQueryHandler.Handle [L29–L95]
      └─ uses_service UnitOfWork
        └─ method Table [L44]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ requests 1
      └─ GetBinderFinancialDataQuery
    └─ handlers 1
      └─ GetBinderFinancialDataQueryHandler

