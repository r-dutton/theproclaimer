[web] GET /api/clients/summary  (Workpapers.Next.API.Controllers.ClientsController.GetSummary)  [L125–L170] status=200 [auth=AuthorizationPolicies.User]
  └─ calls ClientRepository.ReadQuery [L128]
  └─ calls BinderRepository.ReadQuery [L140]
  └─ query Binder [L140]
    └─ reads_from Binders
  └─ query Client [L128]
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L140]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L128]
      └─ ... (no implementation details available)

