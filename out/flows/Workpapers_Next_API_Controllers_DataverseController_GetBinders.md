[web] GET /api/dataverse/binders  (Workpapers.Next.API.Controllers.DataverseController.GetBinders)  [L352–L361] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId,AuthorizationPolicies.RequireDataverseTenantAndUserId]
  └─ maps_to BinderListItemDto [L357]
    └─ automapper.registration WorkpapersMappingProfile (Binder->BinderListItemDto) [L431]
  └─ calls BinderRepository.ReadQuery [L357]
  └─ query Binder [L357]
    └─ reads_from Binders
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Binder writes=0 reads=1
    └─ mappings 1
      └─ BinderListItemDto

