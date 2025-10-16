[web] GET /api/clients/summary  (Workpapers.Next.API.Controllers.ClientsController.GetSummary)  [L125–L170] status=200 [auth=AuthorizationPolicies.User]
  └─ uses_client ClientRepository [L150]
  └─ uses_client ClientRepository [L128]
  └─ calls BinderRepository.ReadQuery [L140]
  └─ query Binder [L140]
    └─ reads_from Binders
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Binder writes=0 reads=1
    └─ clients 1
      └─ ClientRepository

