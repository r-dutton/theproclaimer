[web] GET /api/workpaper-records  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetLinkedSourceAccountIds)  [L519–L560] [auth=AuthorizationPolicies.User]
  └─ calls SourceAccountRepository.ReadQuery [L529]
  └─ calls BinderRepository.ReadQuery [L524]
  └─ queries SourceAccount [L529]
    └─ reads_from SourceAccounts
  └─ queries Binder [L524]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L524]
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method ReadQuery [L529]

