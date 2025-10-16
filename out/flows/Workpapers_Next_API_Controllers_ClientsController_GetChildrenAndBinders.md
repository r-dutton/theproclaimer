[web] GET /api/clients/{id:guid}/binders  (Workpapers.Next.API.Controllers.ClientsController.GetChildrenAndBinders)  [L78–L122] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to BinderListItemClientDto [L102]
    └─ automapper.registration WorkpapersMappingProfile (Binder->BinderListItemClientDto) [L438]
  └─ maps_to ClientWithBindersDto [L95]
    └─ automapper.registration WorkpapersMappingProfile (Client->ClientWithBindersDto) [L72]
  └─ calls ClientRepository.ReadQuery [L87]
  └─ calls BinderRepository.ReadQuery [L102]
  └─ query Binder [L102]
    └─ reads_from Binders
  └─ query Client [L87]
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L102]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L87]
      └─ ... (no implementation details available)

