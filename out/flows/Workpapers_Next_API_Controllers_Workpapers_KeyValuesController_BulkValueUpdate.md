[web] PUT /api/key-values/bulk-value-update  (Workpapers.Next.API.Controllers.Workpapers.KeyValuesController.BulkValueUpdate)  [L37–L73] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L41]
  └─ calls WorksheetRepository.WriteQuery [L50]
  └─ write Binder [L41]
    └─ reads_from Binders
  └─ write Worksheet [L50]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L41]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<Worksheet>
    └─ method WriteQuery [L50]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L69]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

