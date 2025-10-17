[web] GET /api/clients/{id:guid}/binders  (Workpapers.Next.API.Controllers.ClientsController.GetChildrenAndBinders)  [L78–L122] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to BinderListItemClientDto [L102]
    └─ automapper.registration WorkpapersMappingProfile (Binder->BinderListItemClientDto) [L438]
  └─ maps_to ClientWithBindersDto [L95]
    └─ automapper.registration WorkpapersMappingProfile (Client->ClientWithBindersDto) [L72]
  └─ uses_client ClientRepository [L95]
  └─ uses_client ClientRepository [L87]
  └─ calls BinderRepository.ReadQuery [L102]
  └─ query Binder [L102]
    └─ reads_from Binders
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Binder writes=0 reads=1
    └─ clients 1
      └─ ClientRepository
    └─ mappings 2
      └─ BinderListItemClientDto
      └─ ClientWithBindersDto

