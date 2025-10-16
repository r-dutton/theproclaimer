[web] GET /api/super/workpapers/{tenantId:Guid}/subscription  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.GetSubscription)  [L60–L73] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SubscriptionDto [L65]
  └─ calls TenantRepository.ReadTable [L65]
  └─ query Tenant [L65]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L65]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]

