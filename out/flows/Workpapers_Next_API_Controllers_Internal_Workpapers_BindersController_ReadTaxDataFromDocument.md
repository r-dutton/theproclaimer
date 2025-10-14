[web] GET /api/internal/workpapers/binders/{binderId:Guid}/tax-info  (Workpapers.Next.API.Controllers.Internal.Workpapers.BindersController.ReadTaxDataFromDocument)  [L41–L45] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ sends_request GetTaxDataFromBinderQuery [L44]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetTaxDataFromBinderQueryHandler.Handle [L40–L360]
      └─ uses_service DataverseService
        └─ method GetStandardQueryParams [L355]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L61]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L63]

