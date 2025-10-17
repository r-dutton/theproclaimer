[web] PUT /api/key-values/bulk-value-update  (Workpapers.Next.API.Controllers.Workpapers.KeyValuesController.BulkValueUpdate)  [L37–L73] status=200 [auth=AuthorizationPolicies.User]
  └─ calls WorksheetRepository.WriteQuery [L50]
  └─ calls BinderRepository.WriteQuery [L41]
  └─ write Worksheet [L50]
    └─ reads_from Worksheets
  └─ write Binder [L41]
    └─ reads_from Binders
  └─ uses_service UserService
    └─ method GetUserId [L69]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ entities 2 (writes=2, reads=0)
      └─ Binder writes=1 reads=0
      └─ Worksheet writes=1 reads=0
    └─ services 1
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

