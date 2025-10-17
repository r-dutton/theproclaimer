[web] GET /api/workpaper-records  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetLinkedSourceAccountIds)  [L519–L560] status=200 [auth=AuthorizationPolicies.User]
  └─ calls SourceAccountRepository.ReadQuery [L529]
  └─ calls BinderRepository.ReadQuery [L524]
  └─ query SourceAccount [L529]
    └─ reads_from SourceAccounts
  └─ query Binder [L524]
    └─ reads_from Binders
  └─ uses_service SourceAccountRepository
    └─ method ReadQuery [L529]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceAccountRepository.ReadQuery [L8-L38]
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ Binder writes=0 reads=1
      └─ SourceAccount writes=0 reads=1
    └─ services 1
      └─ SourceAccountRepository

