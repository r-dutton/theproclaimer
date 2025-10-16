[web] GET /api/super/cirrus/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Cirrus.CirrusController.GetSubscription)  [L44–L62] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SubscriptionDto [L48]
  └─ uses_client CirrusClient [L60]
  └─ calls TenantRepository.ReadTable [L48]
  └─ query Tenant [L48]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L48]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]

