[web] GET /api/super/cirrus/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Cirrus.CirrusController.GetSubscription)  [L44–L62] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SubscriptionDto [L48]
  └─ uses_client CirrusClient [L60]
  └─ calls TenantRepository.ReadTable [L48]
  └─ queries Tenant [L48]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L48]

