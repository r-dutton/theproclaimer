[web] GET /api/binder-data/tax  (Workpapers.Next.API.Controllers.Workpapers.BinderDataController.ReadTaxDataFromDocument)  [L27–L34] [auth=AuthorizationPolicies.User]
  └─ sends_request GetTaxDataFromBinderQuery [L31]
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

