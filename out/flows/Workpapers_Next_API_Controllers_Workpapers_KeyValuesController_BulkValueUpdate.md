[web] PUT /api/key-values/bulk-value-update  (Workpapers.Next.API.Controllers.Workpapers.KeyValuesController.BulkValueUpdate)  [L37–L73] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L41]
  └─ calls WorksheetRepository.WriteQuery [L50]
  └─ writes_to Binder [L41]
    └─ reads_from Binders
  └─ writes_to Worksheet [L50]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L41]
  └─ uses_service IControlledRepository<Worksheet>
    └─ method WriteQuery [L50]
  └─ uses_service UserService
    └─ method GetUserId [L69]

