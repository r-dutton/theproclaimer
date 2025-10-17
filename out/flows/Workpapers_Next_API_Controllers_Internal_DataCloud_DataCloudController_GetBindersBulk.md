[web] POST /api/data-cloud/binders  (Workpapers.Next.API.Controllers.Internal.DataCloud.DataCloudController.GetBindersBulk)  [L40–L49] status=201 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ maps_to BinderUltraLightDto [L45]
    └─ automapper.registration WorkpapersMappingProfile (Binder->BinderUltraLightDto) [L440]
  └─ calls BinderRepository.ReadQuery [L45]
  └─ query Binder [L45]
    └─ reads_from Binders
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Binder writes=0 reads=1
    └─ mappings 1
      └─ BinderUltraLightDto

