[web] GET /api/super/workpapers/{tenantId:Guid}/subscription  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.GetSubscription)  [L60–L73] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SubscriptionDto [L65]
  └─ calls TenantRepository.ReadTable [L65]
  └─ queries Tenant [L65]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L65]

