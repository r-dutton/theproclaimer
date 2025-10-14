[web] POST /api/data-cloud/binders  (Workpapers.Next.API.Controllers.Internal.DataCloud.DataCloudController.GetBindersBulk)  [L40–L49] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ maps_to BinderUltraLightDto [L45]
    └─ automapper.registration WorkpapersMappingProfile (Binder->BinderUltraLightDto) [L440]
  └─ calls BinderRepository.ReadQuery [L45]
  └─ queries Binder [L45]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L45]

