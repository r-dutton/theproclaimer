[web] GET /api/clients/summary  (Workpapers.Next.API.Controllers.ClientsController.GetSummary)  [L125–L170] [auth=AuthorizationPolicies.User]
  └─ calls ClientRepository.ReadQuery [L128]
  └─ calls BinderRepository.ReadQuery [L140]
  └─ queries Binder [L140]
    └─ reads_from Binders
  └─ queries Client [L128]
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L140]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L128]

