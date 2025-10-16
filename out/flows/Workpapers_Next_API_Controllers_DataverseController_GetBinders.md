[web] GET /api/dataverse/binders  (Workpapers.Next.API.Controllers.DataverseController.GetBinders)  [L352–L361] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId,AuthorizationPolicies.RequireDataverseTenantAndUserId]
  └─ maps_to BinderListItemDto [L357]
    └─ automapper.registration WorkpapersMappingProfile (Binder->BinderListItemDto) [L431]
  └─ calls BinderRepository.ReadQuery [L357]
  └─ query Binder [L357]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L357]
      └─ ... (no implementation details available)

