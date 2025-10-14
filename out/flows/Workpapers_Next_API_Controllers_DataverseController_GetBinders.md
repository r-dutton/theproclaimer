[web] GET /api/dataverse/binders  (Workpapers.Next.API.Controllers.DataverseController.GetBinders)  [L351–L360] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId,AuthorizationPolicies.RequireDataverseTenantAndUserId]
  └─ maps_to BinderListItemDto [L356]
    └─ automapper.registration WorkpapersMappingProfile (Binder->BinderListItemDto) [L431]
  └─ calls BinderRepository.ReadQuery [L356]
  └─ queries Binder [L356]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L356]

